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


function c1g_ginit(gridid)
{
	var props = new Object();
	props.groupedRows = new Array();
	c1g_Props[gridid] = props;
}


function c1g_showHide(e, imgCollapsed, imgExpanded, groupby)
{
	if (!e) 
		e = window.event;
	// find row that was clicked
	var src = null; //event.srcElement;
	if (e.srcElement)
		src = e.srcElement;
	else
		src = e.currentTarget;

	var evtsrc = src;
  
	while (src != null && src.tagName != "TR")
		src = c1g_getParent(src)

	if (src == null)
		 return;

	var clientRowID = c1g_getattr(src, "ID");	

	// get the node level
	var level = c1g_getNodeLevel(src);
	if (level < 0)
		 return;
	
	// loop through all rows below this one until the next node
	var table = c1g_getParent(src);
	while (table != null && table.tagName != "TABLE")
		table = c1g_getParent(table);
	if (table == null)
		return;

	var rows = table.rows;
	var display = "";
	var lastlvl = true;
	var hdr = false;
	for (var row = src.rowIndex + 1; row < rows.length; row++)
	{
		// stop at the next node higher than or equal to the source
		var rlevel = c1g_getNodeLevel(rows[row]);
		
		if (rlevel > -1 && rlevel <= level) break;
		if (c1g_getattr(rows[row], "pager") || c1g_getParent(rows[row]).tagName == "TFOOT") break;
		
		// handle detail rows
		if ((groupby && (level < rlevel || rlevel < 0)) || (!groupby && rlevel < 0))
		{
			// hiding or showing?
			if (display.length == 0)
				display = (rows[row].style.display == "none")? "": "none";
	
			// flip visibility
			if( groupby )
			{
				// if we're expanding only expand the next level up
				if( display == "" )
				{
					// group row
					// if( level < rlevel )
					if (rlevel == level + 1 || !hdr)
					{
						hdr = (rlevel == level + 1);
						lastlvl = false;
						var img = c1g_getImgElement(rows[row]);
						if( img.src )
							img.src = imgCollapsed;
						rows[row].style.display = display;
					}

					// datarow
					if (lastlvl && rlevel < 0 && !hdr)
						rows[row].style.display = display;
				}
				else
				{
					if( level < rlevel )
					{
						var img = c1g_getImgElement(rows[row]);
						if( img.src )
							img.src = imgExpanded;
						rows[row].style.display = display;
					}
					if( rlevel < 0 )
						rows[row].style.display = display;
				}
			}
			else
			{
				if( rlevel < 0 )
					rows[row].style.display = display;
			}
		}
	}


	// flip image
	src = evtsrc;
	if (src.tagName == "IMG")
		src.src = (display == "none")? imgCollapsed: imgExpanded;

		
	var gridid=c1g_getGridID(evtsrc);
	if (typeof(c1g_reinit) != "undefined")
		c1g_reinit(gridid);

	var grid = new c1g_getObj(gridid);
	var split = c1g_getattr(grid.obj, "Spliting");
	if (split != null)
		//c1g_reLayout(grid);
		c1g_reLayout(gridid);


	var fieldName = "__" + gridid + "_GroupedRows";
	var field = document.getElementById(fieldName);
	if (field == null) //NN or GroupPostback=false
	{
		for (var i = 0; i < document.forms.length; i++)
			if (typeof(document.forms[i].elements[fieldName]) != "undefined")
			{
				field = document.forms[i].elements[fieldName]
				break;				
			}
	}
	
	if (field != null)
	{
		var props = c1g_Props[gridid];
		clientRowID = level + "~" + clientRowID;

		if (typeof(props.groupedRows[clientRowID]) == "undefined")
			props.groupedRows[clientRowID] = clientRowID;
		else
		{
			var na = new Array();
			for (var i in props.groupedRows)
				if (props.groupedRows[i] != clientRowID)
					na[i] = props.groupedRows[i];

			props.groupedRows = na;
		}			

		var val = "";
		for (var i in props.groupedRows)
			val = val + props.groupedRows[i] + ":";

		field.value = val;
	}

}