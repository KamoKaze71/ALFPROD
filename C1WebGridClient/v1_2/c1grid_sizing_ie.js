//
// Copyright (c) 2002 ComponentOne L.L.C. All rights reserved.
// Version 1.1.20024.1
//
var isOpera = (navigator.userAgent.indexOf('Opera') != -1);
var isIE = (!isOpera && navigator.userAgent.indexOf('MSIE') != -1)
var DHTML = (document.getElementById || document.all || document.layers);

function c1g_state()
{
	this.colHitTest = new Array();
	this.groupHitTest = new Array();
	this.groupInfo = null;
	this.objDragItem = null;
	this.objResizeItem = null;
	this.dstIdx = -1;
	this.srcIdx = -1;
	this.grpDstIdx = -1;
	this.grpSrcIdx = -1;
	this.grpCount = 0;
	this.allowColSizing = false;
	this.allowColMoving = false;
}

function c1g_hitTestInfo(elem)
{
	var left = c1g_findPosX(elem);
	this.left = left;
	this.top = c1g_findPosY(elem);
	this.right = left + elem.offsetWidth;
	this.srcElem = elem;
}


function c1g_init(gridid)
{
	var props = new c1g_state();
	c1g_Props[gridid] = props;

	var grid = new c1g_getObj(gridid);
	var groupcnt = parseInt(c1g_getattr(grid.obj,"GroupCount"));
	props.grpCount = groupcnt;
	var allow = c1g_getattr(grid.obj, "AllowColSizing");
	props.allowColSizing = allow != null;
	allow = c1g_getattr(grid.obj, "AllowColMoving");
	props.allowColMoving = allow != null;

	// find the table with data
	var data;
	if( groupcnt < 0 )
	{
		data = grid.obj;
	}
	else
	{
		var table = new c1g_getObj(gridid+"_Data");
		data = table.obj;
	}

	// now get the col sizes
	var tr = data.tHead.children[0];
	if( tr.tagName != "TR") return;

	if( props.allowColMoving )
		data.tHead.style.cursor = "hand";

	var count = tr.children.length;
	var i;
	for(i=0; i < count; i++)
	{
		var colheader = tr.children[i];
		props.colHitTest[i] = new c1g_hitTestInfo(colheader);	
		colheader.attachEvent("onmousedown", c1g_colHeaderMouseDown);
	}
	
	// now do any grouped columns
	if( groupcnt == 0 )
	{
		var groups = new c1g_getObj(gridid+"_Group");
		groups = groups.obj.rows[0].children[0];
		props.groupHitTest[0] = new c1g_hitTestInfo(groups);
	}
	else if( groupcnt > 0 )
	{
		var groups = new c1g_getObj(gridid+"_Group");
		groups = groups.obj.rows[0].children[0];
		props.groupInfo = new c1g_hitTestInfo(groups);
		for(i=0; i < groupcnt; i++)
		{
			var grphead = new c1g_getObj(gridid+"_GCOL_"+i);
			grphead = grphead.obj;
			props.groupHitTest[i] = new c1g_hitTestInfo(grphead);
			props.groupHitTest[i].left -= 4;
			props.groupHitTest[i].right -= 4;
			grphead.attachEvent("onmousedown", c1g_groupHeaderMouseDown);
			if( props.allowColMoving )
				grphead.style.cursor = "hand";
		}
	}

	document.attachEvent("onmousemove",c1g_onMouseMove);
	document.attachEvent("onmouseup",c1g_onMouseUp);
	document.attachEvent("onselectstart",c1g_onSelect);
}

function c1g_onSelect(e)
{
	if( !DHTML ) return;
	if( !e ) e = window.event;

	if( c1g_dragMode || c1g_resizeMode) e.returnValue = false;
}

function c1g_colHeaderIndex(elem)
{
	var props = c1g_Props[c1g_curGrid];
	var count = props.colHitTest.length;
	var i;
	for(i=0; i < count; i++)
	{
		if( elem == props.colHitTest[i].srcElem )
			return i;
	}

	return -1;
}

function c1g_grpColHeaderIndex(elem)
{
	var props = c1g_Props[c1g_curGrid];
	var count = props.groupHitTest.length;
	var i;
	for(i=0; i < count; i++)
	{
		if( elem == props.groupHitTest[i].srcElem )
			return i;
	}

	return -1;
}

function c1g_createDropElem(props, src)
{
	props.objDragItem = document.createElement("DIV");
	props.objDragItem.innerHTML		= src.innerHTML;
	props.objDragItem.style.height		= src.currentStyle.height;
	props.objDragItem.style.width		= src.offsetWidth;
	props.objDragItem.style.background 	= src.currentStyle.backgroundColor;
	props.objDragItem.style.fontColor	= src.currentStyle.fontColor;
	props.objDragItem.style.position 	= "absolute";
}
function c1g_colHeaderMouseDown(e)
{
	if( !DHTML ) return;
	if( !e ) e = window.event;
	
	var src 	= e.srcElement;
	var c 	= e.srcElement;
	var gridid = c1g_getGridID(src);

	var props = c1g_Props[gridid];
	c1g_curGrid = gridid;

	if (props.objResizeItem != null)
	{
	  if (!props.allowColSizing) return;
		c1g_resizeMode = true;	
	}
	else
	{
		if( !props.allowColMoving ) return;
		c1g_dragMode 	= true;
		while (src.tagName != "TH") 
			src = src.parentElement;

		// Create our header on the fly
		c1g_createDropElem(props, src);
		props.srcIdx = c1g_colHeaderIndex(src);
		while (c.offsetParent != null) 
		{
			props.objDragItem.style.y += c.offsetTop;
			props.objDragItem.style.x += c.offsetLeft;
			c = c.offsetParent;
		}
 		//props.objDragItem.style.borderStyle	= "outset";
		props.objDragItem.style.display	= "none";
		src.insertBefore(props.objDragItem);
	}
}


function c1g_groupHeaderMouseDown(e)
{
	if( !DHTML ) return;
	if( !e ) e = window.event;

	var src 	= e.srcElement;
	var c 	= e.srcElement;
	var gridid = c1g_getGridID(src);

	var props = c1g_Props[gridid];
	if( !props.allowColMoving ) return

	c1g_dragMode = true;
	c1g_curGrid = gridid;
	
	while (src.tagName != "TD") 
		src = src.parentElement;

	// Create our header on the fly
	c1g_createDropElem(props, src);
	props.grpSrcIdx = c1g_grpColHeaderIndex(src);
	while (c.offsetParent != null) 
        {
		props.objDragItem.style.y += c.offsetTop;
		props.objDragItem.style.x += c.offsetLeft;
		c = c.offsetParent;
	}
	props.objDragItem.style.display	= "none";

	src.insertBefore(props.objDragItem);
}

function c1g_hideInsPt(gridid)
{
	var up = new c1g_getObj(gridid+"_ImgColUp");
	var dn = new c1g_getObj(gridid+"_ImgColDn");

	dn.style.visibility = "hidden";
	up.style.visibility = "hidden";
}
function c1g_showColInsPt(gridid, posX, posY, height)
{
	var up = new c1g_getObj(gridid+"_ImgColUp");
	var	dn = new c1g_getObj(gridid+"_ImgColDn");

	dn.style.visibility = "visible";
	dn.style.left = posX;
	dn.style.top = posY - dn.obj.offsetHeight;
	
	up.style.visibility = "visible";
	up.style.left = posX;
	up.style.top = posY + height;
}

function c1g_inCol(x, y, info)
{
	if( (x > info.left) && (x < info.right) && (y > info.top) && 
	    (y < (info.top+info.srcElem.offsetHeight)) )
		return true;
	return false;
}

function c1g_hitTestCols(gridid, x, y)
{
	var props = c1g_Props[gridid];
	var up = new c1g_getObj(gridid+"_ImgColUp");
	var count = props.colHitTest.length;
	for(var i=0; i < count; i++)
	{
		var srcElem = props.colHitTest[i].srcElem;
		var pos = props.colHitTest[i].left;
		if( c1g_inCol(x, y, props.colHitTest[i]) )
		{
			if( i == props.srcIdx ) 
			{
				c1g_hideInsPt(gridid);
				props.dstIdx = -1;
				return false;
			}
			props.dstIdx = i;
			if( props.srcIdx != -1 && i > props.srcIdx )
				pos = pos + srcElem.offsetWidth;
			c1g_showColInsPt(gridid, pos-(up.obj.offsetWidth/2), props.colHitTest[i].top, srcElem.offsetHeight);
			return true;
		}
	}
	
	if( props.grpSrcIdx != -1 )
	{
		count--;
		pos = props.colHitTest[count].right;
		if( (x > pos) && (y > props.colHitTest[count].top) && 
				(y < (props.colHitTest[count].top+srcElem.offsetHeight)) )
		{
			props.dstIdx = count+1;
			pos = props.colHitTest[count].right;
			c1g_showColInsPt(gridid, pos-(up.obj.offsetWidth/2), props.colHitTest[count].top, props.colHitTest[count].srcElem.offsetHeight);
			return true;
		}
	}
	c1g_hideInsPt(gridid);
	props.dstIdx = -1;
	return false;
}

function c1g_hitTestGroup(gridid, x, y)
{
	var props = c1g_Props[gridid];
	var up = new c1g_getObj(gridid+"_ImgColUp");
	var count = props.groupHitTest.length;
	for(var i=0; i < count; i++)
	{
		var pos = props.groupHitTest[i].left;
		if( c1g_inCol(x, y, props.groupHitTest[i]) )
		{
			if( i == props.grpSrcIdx || (props.grpSrcIdx !=-1 && i-1 == props.grpSrcIdx) ) 
			{
				c1g_hideInsPt(gridid);
				props.grpDstIdx = -1;
				return false;
			}
			props.grpDstIdx = i;
			c1g_showColInsPt(gridid, pos-(up.obj.offsetWidth/2), props.groupHitTest[i].top, props.groupHitTest[0].srcElem.offsetHeight);
			return true;
		}
	}

	// check to see if we're to the right of the last grouped column
	if( props.grpCount > 0 && (x > props.groupHitTest[count-1].right) && 
		(y > props.groupInfo.top) && (y < (props.groupInfo.top+props.groupInfo.srcElem.offsetHeight)) )
	{
		props.grpDstIdx = count;
		c1g_showColInsPt(gridid, props.groupHitTest[count-1].right-(up.obj.offsetWidth/2), props.groupHitTest[count-1].top, props.groupHitTest[count-1].srcElem.offsetHeight);
		return true;
	}
	c1g_hideInsPt(gridid);
	props.grpDstIdx = -1;
	
	return false;
}

function c1g_getHeadIdx(srcElem)
{
	if( srcElem.tagName == "TH" )
	{
		var row = srcElem.parentElement;
		for(i=0; i < row.children.length; i++)
		{
			if( row.children[i] == srcElem )
				return i;
		}
	}
	return -1;
}

function c1g_chooseCursor(e)
{
	if( !DHTML || e.srcElement == null) return;
	
	if( c1g_dragMode ) return;

	var state = c1g_Props[c1g_getGridID(e.srcElement)];
	if( state && !state.allowColSizing )
		return;
	if( !c1g_resizeMode && state)
	{
		var posx = e.clientX;
		var posy = e.clientY;
		var srcElem = e.srcElement;
		if( srcElem.tagName == "TH" )
		{
			var left = c1g_findPosX(srcElem);
			var header = srcElem.parentElement.parentElement;
			var edge = left + srcElem.offsetWidth;
			if( posx >= edge-4 && posx <= edge+4 )
			{
				header.style.cursor = "w-resize";
				state.objResizeItem = srcElem;
				state.resizeWidth = srcElem.offsetWidth;
				state.posX = edge;
				state.srcIdx = c1g_getHeadIdx(srcElem);
				return;
			}
			state.objResizeItem = null;
			if( state.allowColMoving )
				header.style.cursor = "hand";
			else
				header.style.cursor = "default";
			state.srcIdx = -1;
		}
	}
}

function c1g_onMouseMove(e)
{
	c1g_chooseCursor(e);
	if( !(c1g_dragMode || c1g_resizeMode) || c1g_curGrid == null ) return;

	var props = c1g_Props[c1g_curGrid];
	if (c1g_resizeMode)
	{
		var colgroups = props.objResizeItem.parentNode.parentNode.previousSibling;
		var newwidth = props.resizeWidth - (props.posX - e.clientX);
		if( newwidth > 0 )
		{
			var col = colgroups.childNodes[props.srcIdx];
			col.width = newwidth;

  	  var table = props.objResizeItem;
	    while (table.tagName != "TABLE")
		  	table = table.parentNode;

      var tr = table.tHead.children[0];
			for (var i = 0;i<tr.children.length;i++)
			{
			  var colheader = tr.children[i];
			  props.colHitTest[i] = new c1g_hitTestInfo(colheader);	
			}			
			
		}	
		e.cancelBubble = false;
		e.returnValue = false;
	}
	else if (c1g_dragMode)
	{
		var midWObj = props.objDragItem.parentElement.offsetWidth / 2;
		var midHObj = props.objDragItem.parentElement.offsetHeight / 2;

		// Save mouse's position in the document
		var intTop = e.clientY + document.body.scrollTop;
		var intLeft = e.clientX + document.body.scrollLeft;


		var cx=0,cy=0;
		var elCurrent = props.objDragItem.offsetParent;
    
		while (elCurrent.offsetParent != null) 
		{
			cx += elCurrent.offsetTop;
			cy += elCurrent.offsetLeft;
			elCurrent = elCurrent.offsetParent;
		}

		props.objDragItem.style.pixelTop  = intTop  - cx - midHObj;
		props.objDragItem.style.pixelLeft = intLeft - cy - midWObj;

		if(props.objDragItem.style.display == "none") props.objDragItem.style.display = "";

		if( !c1g_hitTestCols(c1g_curGrid, intLeft, intTop) )
		{
			// check group move
			c1g_hitTestGroup(c1g_curGrid, intLeft, intTop);
		}

		e.cancelBubble = false;
		e.returnValue = false;
	}
}

function c1g_onMouseUp(e)
{
	if(!(c1g_dragMode || c1g_resizeMode))	return;

	if (c1g_dragMode)
	{
		c1g_dragMode = false;
		c1g_hideInsPt(c1g_curGrid);

		var props = c1g_Props[c1g_curGrid];
		var iSelected = props.objDragItem.selectIndex;
	
		props.objDragItem.style.display = "none";
		props.objDragItem.removeNode(true);
		props.objDragItem = null;

		if( props.srcIdx != -1 )
		{
			if( props.dstIdx != -1 )
				__doPostBack(c1g_getUniqueID(c1g_curGrid), "ColMove:"+props.srcIdx+":"+props.dstIdx);
			else if( props.grpDstIdx != -1 )
			{
				__doPostBack(c1g_getUniqueID(c1g_curGrid), "GroupColMove:"+"C:" + props.srcIdx + ":G:" + props.grpDstIdx);
			}
		}
		else if( props.grpSrcIdx != -1 )
		{
			if( props.grpDstIdx != -1 )
				__doPostBack(c1g_getUniqueID(c1g_curGrid), "GroupColMove:"+"G:" + props.grpSrcIdx + ":G:" + props.grpDstIdx);
			else if( props.dstIdx != -1 )
				__doPostBack(c1g_getUniqueID(c1g_curGrid), "GroupColMove:"+"G:" + props.grpSrcIdx + ":C:" + props.dstIdx);
		}
	}
	else if (c1g_resizeMode)
	{
		c1g_resizeMode = false;
	}

	c1g_curGrid = null;
}
