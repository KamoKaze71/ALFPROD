//
// Copyright (c) 2002 ComponentOne L.L.C. All rights reserved.
// Version 1.1.20024.1
//

function c1g_canGroup(idx)
{
	var colGroup = document.getElementById(dragItem.gridid).childNodes[1];
	var col = colGroup.childNodes[idx];
	return (c1g_getattr(col, "AllowGroup") == null) ? true : false;
}



function c1g_canMove(idx, elem)
{
	var colGroup = document.getElementById(c1g_getGridID(elem)).childNodes[1];
	var col = colGroup.childNodes[idx];
	return (c1g_getattr(col, "AllowMove") == null) ? true : false;
}


function c1g_headersCnt(gridid)
{
	var grid = document.getElementById(gridid);
	return grid.childNodes[1].childNodes.length;
}

                       
function c1g_findPosX(elem)
{
	var res = 0;
  
	if (elem)
	do 
	{
		res += elem.offsetLeft + (elem.border == undefined ? 0 : parseInt(elem.border));
		elem = elem.offsetParent;
	} 
	while (elem != null);
   
	return res;
}


function c1g_findPosY(elem)
{
	var res = 0;

	if (elem)
	do 
	{
		res += elem.offsetTop + (elem.border == undefined ? 0 : parseInt(elem.border));
		elem = elem.offsetParent;
	} 
	while (elem != null);
   
	return res;
}



function c1g_groupCount(gridid)
{
	return c1g_getattr(document.getElementById(gridid), "GroupCount")
}





function c1g_init(gridid)
{
	//alert("NS C1G_INIT " + gridid);
	var props = new c1g_state();
	props.groupedRows = new Array();
	c1g_Props[gridid] = props;        

	for (var i = 0; i < c1g_groupCount(gridid); i++)
		document.getElementById(gridid + "_GCOL_" + i).style.cursor = "default";
         
	dragItem = new Object();
	dragItem.obj = null;
	dragItem.colIdx = -1;
	dragItem.grpIdx = -1;
	dragItem.gridid = "";

	imgDown = document.getElementById(gridid + "_ImgColDn");
	imgUp = document.getElementById(gridid + "_ImgColUp");
	
	document.getElementById(gridid).style.cursor = "default";
	
	document.onmousedown = c1g_mouseDown;
	document.onmousemove = c1g_mouseMove;
	document.onmouseup = c1g_mouseUp;
}




function c1g_enableDragItem(hdr, gridid, x, y, ishdr)
{
	try
	{
		dragItem.obj = document.getElementById("~~dragElem");

		var o = hdr;
		while (o.firstChild != null)
			o = o.firstChild;	
	 	
		dragItem.obj.innerHTML = (typeof(o.innerHTML) == "undefined") ? o.nodeValue : o.innerHTML;
		dragItem.obj.style.width = hdr.offsetWidth - 5;
		dragItem.obj.style.height = hdr.offsetHeight - 5;
		dragItem.obj.style.left = x - (parseInt(dragItem.obj.style.width) / 2);;
		dragItem.obj.style.top = y - (parseInt(dragItem.obj.style.height) / 2);;


		while (o.parentNode != null)
			if (typeof(o.color) == "undefined")
				o = o.parentNode;
		else
			break;
    
		if (typeof(o.color) == "undefined")
			o.color = "black";

		dragItem.obj.style.color = o.color;

		if (ishdr)
		{
			dragItem.colIdx = hdr.cellIndex;
			dragItem.obj.style.backgroundColor = hdr.parentNode.bgColor;
		}
		else
		{
			dragItem.grpIdx = hdr.cellIndex;
			dragItem.obj.style.backgroundColor = hdr.bgColor;
		}
             
		dragItem.gridid = gridid;	
	}
	catch(e)
	{
		dragItem.obj = null;
		dragItem.colIdx = -1;
		dragItem.grpIdx = -1;
		dragItem.gridid = "";
	}   
}


function c1g_disableDragItem()
{
	if (dragItem.obj)
	{
		dragItem.obj.style.visibility = "hidden";
		dragItem.obj = null;
		dragItem.colIdx = -1;
		dragItem.grpIdx = -1;
		dragItem.gridid = "";
	} 
}


function c1g_mouseInGrid(e, gridid)
{
	var grid = document.getElementById(gridid);
	if (grid)
	{
		var pX = c1g_findPosX(grid);
		var pY = c1g_findPosY(grid);
           
		if ((e.pageX > pX) && (e.pageX < pX + grid.offsetWidth) && (e.pageY > pY) && (e.pageY < pY + grid.offsetHeight))
			return true;
	}

	return false;
}


function c1g_mouseInPanel(e, gridid)
{
	var panel = document.getElementById(gridid + "_Group");
	
	if (panel)
	{
		var pX = c1g_findPosX(panel);
		var pY = c1g_findPosY(panel);
	  
		if ((e.pageX > pX) && (e.pageX < pX + panel.offsetWidth) && (e.pageY > pY) && (e.pageY < pY + panel.offsetHeight))
		{
			for (var i = 0; i < c1g_groupCount(gridid); i++)
			{
				var item = document.getElementById(gridid + "_GCOL_" + i);
				pX = c1g_findPosX(item);
				pY = c1g_findPosY(item);

				if ((e.pageX > pX) && (e.pageX < pX + item.offsetWidth) && (e.pageY > pY) && (e.pageY < pY + item.offsetHeight))
					return item;
			}
			return null;
		}
	}
	
	return -1;
}


function c1g_mouseInHeader(e, gridid)
{
	var hpanel = document.getElementById(gridid + "_Header");
	var pX = c1g_findPosX(hpanel);
	var pY = c1g_findPosY(hpanel);

	if ((e.pageX > pX) && (e.pageX < pX + hpanel.offsetWidth) && (e.pageY > pY) && (e.pageY < pY + hpanel.offsetHeight))
	{
		hpanel = hpanel.childNodes[1];

		for (var i = 0; i < hpanel.cells.length; i++)
		{
			var h = hpanel.cells[i];
			pX = c1g_findPosX(h);
			py = c1g_findPosY(h);
	     
			if ((e.pageX > pX) && (e.pageX < pX + h.offsetWidth) && (e.pageY > pY) && (e.pageY < pY + h.offsetHeight))		
				return h; 
		}
	}
	  
	return null;
}

                

function c1g_isLeft(e, item)
{
	return (e.pageX < c1g_findPosX(item) + (item.offsetWidth / 2));
}        


            
function c1g_mouseUp(e)
{
	if (dragItem.obj)
	{
		var idx = 0;
		var item = c1g_mouseInHeader(e, dragItem.gridid);

		if (item)
		{
			idx = item.cellIndex;
			if (dragItem.colIdx != -1)
			{
				if (item.cellIndex != dragItem.colIdx)
				{
					if ((dragItem.colIdx - 1 != item.cellIndex) && (dragItem.colIdx + 1 != item.cellIndex))
						if (item.cellIndex < dragItem.colIdx)
						{
							if (!(c1g_isLeft(e, item)))
								idx++;
						}
						else 
							if (c1g_isLeft(e, item))
								idx--;
		 
					__doPostBack(c1g_getUniqueID(dragItem.gridid), "ColMove:" + dragItem.colIdx + ":" + idx);
				}
			}
			else
			{ 
				if (!(c1g_isLeft(e, item)))
					idx++;
				__doPostBack(c1g_getUniqueID(dragItem.gridid), "GroupColMove:G:" + dragItem.grpIdx + ":C:" + idx);
			}
		}
		else
		{
			var gCount = c1g_groupCount(dragItem.gridid);
			item = c1g_mouseInPanel(e, dragItem.gridid);
			if (item != -1 && c1g_canGroup(dragItem.colIdx))
			{
				if (item == null)
				{
					if (dragItem.colIdx != -1)
						__doPostBack(c1g_getUniqueID(dragItem.gridid), "GroupColMove:C:" + dragItem.colIdx + ":G:" + gCount);
					else
						if (dragItem.grpIdx != gCount - 1)
							__doPostBack(c1g_getUniqueID(dragItem.gridid), "GroupColMove:G:" + dragItem.grpIdx + ":G:" + gCount);
				}
				else
				{
					idx = item.cellIndex;
					if (dragItem.colIdx != -1)
					{
						if (!(c1g_isLeft(e, item)))
							idx++;
						__doPostBack(c1g_getUniqueID(dragItem.gridid), "GroupColMove:C:" + dragItem.colIdx + ":G:" + idx);
					}
					else 
						if (dragItem.grpIdx != item.cellIndex)
						{
							if ((dragItem.grpIdx - 1 !== item.cellIndex) && (dragItem.grpIdx + 1 != item.cellIndex))
								if (item.cellIndex < dragItem.grpIdx)
								{
									if (!(c1g_isLeft(e, item)))
										idx++;
								}
								else 
									if (c1g_isLeft(e, item))
										idx--;

							__doPostBack(c1g_getUniqueID(dragItem.gridid), "GroupColMove:G:" + dragItem.grpIdx + ":G:" + idx);
						}
				}
			}
			else
				if (c1g_mouseInGrid(e, dragItem.gridid) && (c1g_headersCnt(dragItem.gridid) == 0))
					__doPostBack(c1g_getUniqueID(dragItem.gridid), "GroupColMove:G:" + dragItem.grpIdx + ":C:0");
		} 
	}
	
	c1g_disableDragItem();
	c1g_hideArrows();
}



function c1g_showArrows(hdr, left)
{
	if (hdr)
	{
		var pX = c1g_findPosX(hdr);
		var pY = c1g_findPosY(hdr);

		if (left)
		{
			imgDown.style.left = pX - (imgDown.width/ 2);
			imgDown.style.top = pY - imgDown.height;
			imgUp.style.left = pX - (imgDown.width/ 2);
			imgUp.style.top = pY + hdr.offsetHeight;
		}
		else
		{ 
			imgDown.style.left = pX + hdr.offsetWidth - (imgDown.width/ 2);
			imgDown.style.top = pY  - imgDown.height;
			imgUp.style.left = pX + hdr.offsetWidth - (imgDown.width/ 2);
			imgUp.style.top = pY + hdr.offsetHeight;
		}

		imgUp.style.visibility = "visible";
		imgDown.style.visibility = "visible";
	}
}



function c1g_hideArrows()
{
	imgUp.style.visibility = "hidden";
	imgDown.style.visibility = "hidden";
}



function c1g_mouseMove(e)
{
	if (dragItem.obj)
	{
		dragItem.obj.style.visibility = "visible";

		var draw = false;
		var left = true;

		dragItem.obj.style.left = e.pageX - (parseInt(dragItem.obj.style.width) / 2);
		dragItem.obj.style.top = e.pageY - (parseInt(dragItem.obj.style.height) / 2);

		var item = c1g_mouseInHeader(e, dragItem.gridid);
		if (item)
		{
			left = c1g_isLeft(e, item);
			draw = ((dragItem.grpIdx != -1) || ((dragItem.colIdx != -1) && (dragItem.colIdx != item.cellIndex)));
			if (dragItem.colIdx != -1)
			{
				if (item.cellIndex == dragItem.colIdx + 1)
					left = false;
				else
					if (item.cellIndex == dragItem.colIdx - 1)
						left = true;
			}
		}
		else
		{
			var gCount = c1g_groupCount(dragItem.gridid);
			item = c1g_mouseInPanel(e, dragItem.gridid);
			if (item != -1 && c1g_canGroup(dragItem.colIdx))
			{
				if (item == null)
				{
					if (gCount > 0)
					{
						if ((dragItem.colIdx != -1) || (dragItem.grpIdx == -1) || (dragItem.grpIdx != gCount - 1))
						{
							item = document.getElementById(dragItem.gridid + "_GCOL_" + (gCount - 1));
							left = false;
							draw = true;
						}
					}	
					else
					{
						draw = true;
						item = document.getElementById(dragItem.gridid + "_Group");
						left = true;
					}
				}
				else
				{
					left = c1g_isLeft(e, item);
					draw = ((dragItem.colIdx != -1) || ((dragItem.grpIdx != -1) && (dragItem.grpIdx != item.cellIndex)));
			         
					if (dragItem.grpIdx != -1)	
					{
						if (item.cellIndex == dragItem.grpIdx + 1)
							left = false;
						else
							if (item.cellIndex == dragItem.grpIdx - 1)
								left = true;
					}
				}
			}
			else if (c1g_mouseInGrid(e, dragItem.gridid) && (c1g_headersCnt(dragItem.gridid) == 0))
			{
				draw = true;
				item = document.getElementById(dragItem.gridid);
			}
		}
	}

	draw ? c1g_showArrows(item, left) : c1g_hideArrows();
}




function c1g_mouseDown(e)
{
	var item = e.target;
	var gridid = c1g_getGridID(item);

	if ((gridid) && c1g_getattr(document.getElementById(gridid), "AllowColMoving"))
	{
		var hdr = c1g_mouseInHeader(e, gridid);
	  
		if ((hdr) && (c1g_canMove(hdr.cellIndex, item)))	
			c1g_enableDragItem(hdr, gridid, e.pageX, e.pageY, true);	
		else
		{
			hdr = c1g_mouseInPanel(e, gridid);
			if ((hdr != -1) && (hdr))
				c1g_enableDragItem(hdr, gridid, e.pageX, e.pageY, false);
		}
	}

	return false;
}


var dragItem = null;
var imgDown = null;
var imgUp = null;

document.writeln('<div id="~~dragElem" align="CENTER" style="cursor:default;position:absolute;overflow:hidden;visibility:hidden;color:White;background-color:#4169E1;border-color:#6495ED;border-width:2px;border-style:Solid;font-family:Arial;font-size:10pt;font-weight:bold;z-index:10000;"></div>');