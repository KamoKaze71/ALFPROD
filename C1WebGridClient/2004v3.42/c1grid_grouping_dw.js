//
// Copyright (c) 2002 ComponentOne L.L.C. All rights reserved.
// Version 1.1.20024.1
//

function c1g_getNodeLevel(row)
{
	var node = c1g_getattr(row, "nodeLevel");
	return (node == null)? -1: parseInt(node);
}
function c1g_getImgElement(row)
{
	return row.cells[0].firstChild;
}

function c1g_showHide(e, imgCollapsed, imgExpanded, groupby)
{
	if( !e ) e = window.event;
	
	// find row that was clicked
	var src = null; //event.srcElement;
	if( e.srcElement )
		src = e.srcElement;
	else
		src = e.currentTarget;
	var evtsrc = src;

	while (src != null && src.tagName != "TR")
		src = c1g_getParent(src)
	if (src == null) return;

	// get the node level
	var level = c1g_getNodeLevel(src);
	if (level < 0) return;
	//var params = "GroupImageClick:" + level + ":" + c1g_getattr(src, "rowClientID");
	var params = "GroupImageClick:" + level + ":" + c1g_getattr(src, "ID");
	__doPostBack(c1g_getUniqueID(c1g_getGridID(src)), params);
}
