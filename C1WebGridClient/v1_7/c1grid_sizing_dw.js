//
// Copyright (c) 2002 ComponentOne L.L.C. All rights reserved.
// Version 1.1.20024.1
//

function c1g_ungroup(gridid, e)
{
	var src = e.currentTarget;

	while( src && src.tagName != "TD" )
		src = c1g_getParent(src);
	
	if( !src ) return;

	var idx = c1g_getattr(src, "Idx");
	
	__doPostBack(c1g_getUniqueID(gridid),"GroupHeadClick:0:" + idx);
}
function c1g_group(gridid, e)
{
	var src = e.currentTarget;

	while( src && src.tagName != "TH" )
		src = c1g_getParent(src);
	
	if( !src ) return;
	
	var idx = c1g_getattr(src, "Idx");
	__doPostBack(c1g_getUniqueID(gridid),"GroupHeadClick:1:" + idx);
}
function c1g_init(gridid)
{
	var props = new c1g_state();
	c1g_Props[gridid] = props;
}

