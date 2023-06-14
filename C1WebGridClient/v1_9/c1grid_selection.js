var c1grid_selections = [];
var wasSelected = true;

function c1grid_getHeadIdx(srcElem)
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

function c1grid_initSelection(selectedElement, elementCssText, elementClass,
	cellCssText, cellClasses)
{
	this.selectedElement = selectedElement;
	this.elementCssText = elementCssText;
	this.elementClass = elementClass;
	this.cellCssText = cellCssText;
	this.cellClasses = cellClasses;
}

function c1grid_restoreStyle(gridID)
{
	if (!c1grid_selections[gridID]) return;
	c1grid_selections[gridID].selectedElement.style.cssText =
		c1grid_selections[gridID].elementCssText;
	c1grid_selections[gridID].selectedElement.className =
		c1grid_selections[gridID].elementClass;	
	if (c1grid_selections[gridID].selectedElement.tagName == "TR" && c1grid_selections[gridID].cellCssText != null)
		for (j = 0; j < c1grid_selections[gridID].selectedElement.cells.length; j++)
		{
			c1grid_selections[gridID].selectedElement.cells[j].style.cssText = c1grid_selections[gridID].cellCssText[j];
			c1grid_selections[gridID].selectedElement.cells[j].className = c1grid_selections[gridID].cellClasses[j];
		}
	else if (c1grid_selections[gridID].selectedElement.tagName == "TH")
	{
	  var table = c1grid_selections[gridID].selectedElement;
	  while (table.tagName != "TABLE")
			table = table.parentNode;
		var i = c1grid_getHeadIdx(c1grid_selections[gridID].selectedElement);
		for (j = 0; j < table.rows.length; j++)
		{
		  var cell = table.rows[j].cells[i];
		  if (cell && cell.parentNode.getAttribute("nodelevel") == null)
		  {
				cell.style.cssText = c1grid_selections[gridID].cellCssText[j];
				cell.className = c1grid_selections[gridID].cellClasses[j];
			}
		}
	}
}

function c1grid_click(event, gridID) {
	var newElement;

	var src = null;
	if (event.srcElement)
		src = event.srcElement;
	else
		src = event.currentTarget;

	if (src.tagName == "TD" && (
								c1g_getattr(c1g_getParent(src), "nodelevel") != null ||
								c1g_getattr(c1g_getParent(src), "pager") != null )) return;
	var grid = src; 
	while( grid != null && grid.tagName != "TABLE")
		grid = c1g_getParent(grid);
	if (grid == null)	return;

	//var editItemIndex = grid.obj.attributes["EditItemIndex"];
	//if (editItemIndex.value != -1) return;

	var selectedIndex = parseInt((new c1g_getObj("__" + gridID + "_SelectedIndex")).obj.value);

	if (wasSelected && selectedIndex != -1)
	{
		for (j = 0; j < grid.rows.length; j++)
		{
			if (c1g_getattr(grid.rows[j], "selected") == "true")
			{
				var row=grid.rows[j];
				c1grid_selections[gridID] =
										   new c1grid_initSelection(row,
					c1g_getattr(row, "bk-style"), c1g_getattr(row, "bk-class"), null, null); 
				break;
			}
		}
	}

	wasSelected = false;	  

	if (src.tagName == "TD")
	{
		if (src.parentNode.parentNode.tagName == "TFOOT") return;
		newElement = src.parentNode;
		if (typeof(c1webgrid_OnRowSelect) != "undefined")
			if (!c1webgrid_OnRowSelect(gridID, newElement)) return;

		if (newElement)
		{
			c1grid_restoreStyle(gridID);
			var cellsCssText = [];
			var cellsClasses = [];
			c1grid_selections[gridID] =
									   new c1grid_initSelection(newElement, newElement.style.cssText,
				newElement.className, cellsCssText, cellsClasses);
			for (j = 0; j < newElement.cells.length; j++)
			{
				cellsCssText[j] = newElement.cells[j].style.cssText;
				cellsClasses[j] = newElement.cells[j].className;
				newElement.cells[j].className = null;
				newElement.className = null;
				newElement.cells[j].style.cssText = "";
			}
			newElement.style.cssText = "";
			newElement.className = gridID + "-SelectedItem";
			var cnt = 0;
			for (j = 0; j < newElement.parentNode.rows.length; j++)
			{
//				if (!newElement.parentNode.rows[j].attributes["nodelevel"])
				if (!c1g_getattr(newElement.parentNode.rows[j], "nodelevel"))
					cnt++;
				if (newElement.parentNode.rows[j] == newElement)
				{
					cnt -= ((grid.tHead != null)?1:0);
					(new c1g_getObj("__" + gridID + "_SelectedIndex")).obj.value = cnt.toString();
					break;
				}
			}
		}	
	}
	/*
	else if (event.srcElement.tagName == "TH")
	{
	newElement = event.srcElement;
	if (typeof(c1webgrid_OnColSelect) != "undefined")
	if (!c1webgrid_OnColSelect(gridID, newElement)) return;
	c1grid_restoreStyle(gridID);
	var table = event.srcElement;
	while (table.tagName != "TABLE")
	table = table.parentNode;
	var i = c1grid_getHeadIdx(event.srcElement);
	var cellsCssText = [];
	c1grid_selections[gridID] =
	new c1grid_initSelection(newElement, newElement.style.cssText,
	cellsCssText);	
	for (j = 0; j < table.rows.length; j++)
	{
	var cell = table.rows[j].cells[i];
	if (cell && cell.parentNode.getAttribute("nodelevel") == null)
	{
	cellsCssText[j] = cell.style.cssText;
	cell.style.cssText = selectionCssText;
	}
	}
	}
*/
}