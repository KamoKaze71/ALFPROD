//
// Copyright (c) 2002 ComponentOne L.L.C. All rights reserved.
// Version 1.1.20024.1
//
function c1g_getGridWidth(gn)
{
	var colWidth=0;
	var div=c1g_getElementById(gn + "_maindiv");
	if (div!=null)
		colWidth+=c1g_getTableWidth(div.firstChild);

	return colWidth;
}

function c1g_getGridHeight(gn)
{
	var rowHeight=0;
	var div=c1g_getElementById(gn + "_maindiv");
	if (div!=null)
		rowHeight+=c1g_getTableHeight(div.firstChild);

	return rowHeight;
}

//0-left 1-top 2-right 3-bottom
function c1g_getBorderWidth(table, border)
{
	var borderstyle;
	var borderwidth;
	
	switch(border)
	{
		case 0:
		borderstyle=table.style.borderLeftStyle;
		borderwidth=table.style.borderLeftWidth;
		break;
		
		case 1:
		borderstyle=table.style.borderTopStyle;
		borderwidth=table.style.borderTopWidth;
		break;
		
		case 2:
		borderstyle=table.style.borderRightStyle;
		borderwidth=table.style.borderRightWidth;
		break;
		
		case 3:
		borderstyle=table.style.borderBottomStyle;
		borderwidth=table.style.borderBottomWidth;
		break;
	}
	
	if (borderstyle)
	{
		if (borderstyle!="none" && borderwidth)
			return parseInt(borderwidth);
	}else
	{
		borderwidth=table.border;
		if (borderwidth)	
			return parseInt(borderwidth);
	}
	
	return 0;
}

function c1g_getTableWidth(table)
{	
	var colWidth=0;
	if (table!=null && table.rows.length>0)
	{
		var cellspacing = 0;
		if (table.cellSpacing)
			cellspacing = parseInt(table.cellSpacing);

		colWidth = c1g_getBorderWidth(table, 0) + c1g_getBorderWidth(table, 2);
	
		if (table.firstChild != null)
		{
			if (table.firstChild.tagName=="COLGROUP")
			{
				var cols = table.firstChild;
				for(var i=0; i< cols.childNodes.length; i++)
				{
					var col = cols.childNodes[i];
					if (col.style.display=="")
					{
						if (col.width != "")
							colWidth+=parseInt(col.width) + cellspacing;
						else
				      colWidth+=parseInt(col.offsetWidth) + cellspacing;
					}
				}
			}else
			{
				if (table.rows.length>0)
				{
					var firstRow=table.rows[0];
					for(var i=0; i < firstRow.childNodes.length; i++)
					{
						var col = firstRow.childNodes[i];
						if(col.style.display=="" && col.offsetWidth>0)
							colWidth+=col.offsetWidth + cellspacing;
					}
				}
			}
		}
	}

	return colWidth;
}

function c1g_getTableHeight(table)
{	
	var rowHeight=0;

	if (table!=null && table.rows!=null)
	{
		var cellspacing = 0;
		if (table.cellSpacing)
			cellspacing = parseInt(table.cellSpacing);

		rowHeight = c1g_getBorderWidth(table, 1) + c1g_getBorderWidth(table, 3);

		for(var i=0; i < table.rows.length; i++)
		{
			var row = table.rows[i];
			if(row.style.display=="" && row.offsetHeight>0)
				rowHeight+=row.offsetHeight + cellspacing;
		}
	}

	return rowHeight;
}

function c1g_getSubGridWidth(gn, row, col)
{
	var table=c1g_getSubTable(gn, row, col);
	if (table!=null)
		return c1g_getTableWidth(table);
		
	return 0;
}


function c1g_getSubGridHeight(gn, row, col)
{
	var table=c1g_getSubTable(gn, row, col);
	if (table!=null)
		return c1g_getTableHeight(table);

	return 0;
}

function c1g_syncDummySize(gn)
{
	var dummy=c1g_getElementById(gn + "_dummydiv");
	if (dummy!=null)
	{
		dummy.style.width=c1g_getGridWidth(gn);	
		dummy.style.height=c1g_getGridHeight(gn);
	}
}

function c1g_resetPos(gn)
{
	var dummy=c1g_getElementById(gn + "_dummydiv");
	if (dummy!=null)
	{
		dummy.style.left=0;
		dummy.style.top=0;
	}
	var table00, table01, tale10, table11;

	var div00=c1g_getSubDiv(gn, 0, 0);
	if (div00!=null)
	{
		table00=div00.firstChild;
		div00.style.left=0;
		div00.style.top=0;
	}

	var div01=c1g_getSubDiv(gn, 0, 1);
	if (div01!=null)
	{
		table01=div01.firstChild;
		div01.style.left=0;
		div01.style.top=0;
	}

	var div10=c1g_getSubDiv(gn, 1, 0);
	if (div10!=null)
	{
		div10.style.left=0;
		div10.style.top=0;
		table10=div10.firstChild;
	}
	
	var div11=c1g_getSubDiv(gn, 1, 1);
	if (div11!=null)
	{
		div11.style.left=0;
		div11.style.top=0;
		table11=div11.firstChild;
	}
}

function c1g_syncMainDivSize(gn)
{
	var sv=c1g_getElementById(gn + "_scrolldiv");
	var gv=c1g_getElementById(gn + "_maindiv");
	if ((sv!=null) && (gv!=null))
	{
		gv.style.left=sv.offsetLeft;
		gv.style.top=sv.offsetTop;
	
		gv.style.width=sv.clientWidth;
		gv.style.height=sv.clientHeight;
	}
}

function c1g_syncTableCols(table1, table2)
{
	if ((table1!=null) && (table2!=null))
	{
		var colGroup1=table1.firstChild;
		var colGroup2=table2.firstChild;
		if ((colGroup1!=null) && (colGroup2!=null) && (colGroup1.childNodes.length==colGroup2.childNodes.length))
		{
			for(var i=0; i < colGroup1.childNodes.length; i++)
			{	
				var col1 = colGroup1.childNodes[i];
				var col2 = colGroup2.childNodes[i];
				var maxWidth=Math.max(col1.width, col2.width);
				col1.width=col2.width=(maxWidth==0 ? "" : maxWidth);
			}
		}
	}
}

function c1g_syncTableRows(table1, table2)
{
	if ((table1!=null) && (table2!=null))
	{
		if (table1.rows.length==table2.rows.length)
		{
			for(var i=0; i<table1.rows.length; i++)
			{
				var row1=table1.rows[i];
				var row2=table2.rows[i];
				if (row1.offsetHeight > row2.offsetHeight)
				{
					row2.style.height=row1.offsetHeight;
					row1.style.height=row1.offsetHeight;					
				}
				else
				{
					row1.style.height=row2.offsetHeight;
					row2.style.height=row2.offsetHeight;					
				}
			}
		}
	}
}


function c1g_syncRCs(gn)
{
	var	table00=c1g_getSubTable(gn, 0, 0);
	var	table01=c1g_getSubTable(gn, 0, 1);
	var	table10=c1g_getSubTable(gn, 1, 0);
	var	table11=c1g_getSubTable(gn, 1, 1);

	c1g_syncTableCols(table00, table10);
	c1g_syncTableCols(table01, table11);
	c1g_syncTableRows(table00, table01);
	c1g_syncTableRows(table10, table11);
}

function c1g_syncMainTable(gn)
{
	var totalwidth = 0;	
	var totalheight = 0;	
	var grid = new c1g_getElementById(gn);
	var groupcount=parseInt(c1g_getattr(grid,"GroupCount"));
	var maindiv=c1g_getElementById(gn + "_maindiv");
	if (maindiv!=null)
	{
		var colGroups=grid.firstChild;
		if (colGroups!=null && colGroups.tagName=="COLGROUP" && colGroups.childNodes.length==2)	
		{
			var width;
			if (colGroups.childNodes[0].style.display!="none")
			{
				width=c1g_getSubGridWidth(gn, 1, 0);
				totalwidth=width;
				colGroups.childNodes[0].width = (width==0) ? "" : width;
			}
			
			width=c1g_getSubGridWidth(gn, 1, 1);
			totalwidth+=width;
			colGroups.childNodes[1].width=(width==0) ? "" : width;
		}

		if (grid.rows.length==2)
		{
			var height;
			if (grid.rows[0].style.display!="none")
			{
				height=c1g_getSubGridHeight(gn, 0, 1);
				totalheight = height;
				grid.rows[0].style.height=(height==0) ? "" : height;

			}
				
			height=c1g_getSubGridHeight(gn, 1, 1);
			totalheight += height;
			grid.rows[1].style.height=(height==0) ? "" : height;
		}
		
		grid.style.left=0;
		grid.style.top=0;
		grid.style.width = (totalwidth==0) ? "" : totalwidth;
		grid.style.height = (totalheight==0) ? "" : totalheight;

		maindiv.firstChild.style.left=0;
		maindiv.firstChild.style.top=0;

		maindiv.firstChild.style.width=(totalwidth==0) ? "" : totalwidth;
		maindiv.firstChild.style.height=(totalheight==0) ? "" : totalheight;
	}
}

var savedTop=0;
var savedLeft=0;
function c1g_onMainDivScroll(gn)
{
	var mv=c1g_getElementById(gn + "_maindiv");
	if (mv!=null)
	{
		var sv=c1g_getElementById(gn + "_scrolldiv");
		if (sv != null)
		{
			var i = sv.scrollLeft;
			i += mv.scrollLeft;
			sv.scrollLeft = i;

			i = sv.scrollTop;
			i += mv.scrollTop;
			sv.scrollTop = i;
		}

		mv.scrollLeft=0;
		mv.scrollTop=0;
	}
}

function c1g_onScroll(gn)
{
	var sv=c1g_getElementById(gn + "_scrolldiv");

	if (sv!=null)
	{
    	if (sv.scrollLeft!=savedLeft)
		{
			var table01=c1g_getSubTable(gn, 0, 1);
			if (table01!=null)
				table01.style.left=-sv.scrollLeft;

			var table11=c1g_getSubTable(gn, 1, 1);
			if (table11!=null)
				table11.style.left=-sv.scrollLeft;

			savedLeft=sv.scrollLeft;
		}

    	if (sv.scrollTop!=savedTop)
		{
			var table10=c1g_getSubTable(gn, 1, 0);
			if (table10!=null)
				table10.style.top=-sv.scrollTop;

			var table11=c1g_getSubTable(gn, 1, 1);
			if (table11!=null)
				table11.style.top=-sv.scrollTop;

			savedTop=sv.scrollTop;
		}

	  var o = c1g_getElementById("__" + gn + "_Scroll");
		if (o)
			o.value = savedLeft + "," + savedTop;
	}

	var props = c1g_Props[gn];
	c1g_initHeadPos(props);
}

function c1g_layOut(gn, scrollX, scrollY)
{
	c1g_syncMainTable(gn);
	c1g_syncDummySize(gn);
	c1g_resetPos(gn);
	c1g_syncMainDivSize(gn);

	var scroller = c1g_getElementById(gn + "_scrolldiv");
	if (scroller != null)
	{
		scroller.scrollLeft = scrollX;
		scroller.scrollTop = scrollY;
	}
}

function c1g_reLayout(gn)
{
	c1g_syncMainTable(gn);
	c1g_syncDummySize(gn);
	c1g_syncMainDivSize(gn);
}