//
// Copyright (c) 2002 ComponentOne L.L.C. All rights reserved.
// Version 1.1.20024.1
//
var c1g_Props = [];
var c1g_curGrid;
var c1g_dragMode = false;
var c1g_resizeMode = false;
var c1g_DHTML = (document.getElementById || document.all || document.layers);

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

function rsearch(str, strToLook)
{
	for(a=str.length;a>=0;a--) {
		
		if(str.substr (a,1) == strToLook)
			return a;
	}
			
	return -1;
}

function c1g_getGridID(srcElem)
{
	var str = c1g_getattr(srcElem, "id");
	while( srcElem != null && !(str && srcElem.tagName == "TABLE"))
	{
		srcElem = c1g_getParent(srcElem);
		if( srcElem != null )
			str = c1g_getattr(srcElem, "id");
	}
	if (srcElem == null) return;
	str = c1g_getattr(srcElem, "C1id");
	return str;

/*
	var idx = str.indexOf("_Row");
	if (idx < 0)
		idx = rsearch(str, "_");

	if( idx < 0 )
		return str;
	else
		return str.substring(0,idx);
*/
}
function c1g_getParent(srcElem)
{
	if( srcElem.parentElement )
		return srcElem.parentElement;
	else
		return srcElem.parentNode;
}

function c1g_getUniqueID(gridId)
{
	//return gridId;
  var grid = new c1g_getObj(gridId);
  return c1g_getattr(grid.obj, "uniqueID");
}

function c1g_getattr(obj, att)
{
    if (obj.getAttribute)
        return obj.getAttribute(att);
    if (obj.attributes) {
        var a = obj.attributes[att];
        if (a) return a.value;
    } else
        return obj[att];
}

function c1g_getObj(name)
{
	if (document.getElementById)
	{
		this.obj = document.getElementById(name);
		this.style = document.getElementById(name).style;
	}
	else if (document.all)
	{
		this.obj = document.all[name];
		this.style = document.all[name].style;
	}
	else if (document.layers)
	{
		this.obj = document.layers[name];
		this.style = document.layers[name];
	}
}

function c1g_findPosX(o)
{
	if (typeof(o) != 'object' || o == null)
		return 0;
	else
		return o.offsetLeft + c1g_findPosX(o.offsetParent);
}

function c1g_findPosY(o)
{
	if (typeof(o) != 'object' || o == null)
		return 0;
	else
		return o.offsetTop + c1g_findPosY(o.offsetParent);
}

function c1g_getElementById(tagId) 
{
	var obj;
	if(document.all)
		obj=document.all[tagId];
	else
		obj=document.getElementById(tagId);
	if(obj && obj.length && obj[0].id==tagId)
		obj=obj[0];
	return obj;
}

function c1g_getSubDiv(gn, row, col)
{
	var div = null;
	div=c1g_getElementById(gn + "_grid" + row + col + "div");
	if (div==null)
		div=c1g_getElementById(gn + "_Row_grid" + row + col + "div");

	return div;
}

function c1g_getSubTable(gn, row, col)
{
	var div=c1g_getSubDiv(gn, row, col);
	if (div!=null)
		return div.firstChild;
		
	return null;
}
