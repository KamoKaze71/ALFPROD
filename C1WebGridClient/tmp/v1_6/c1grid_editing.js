var editedCell = null;
var edited_state = new Array();


function c1g_selectStart(event) 
{
	event.cancelBubble = true;
	event.returnValue = false;
}


function c1g_dblclick(event, gridID)
{
  if (event.srcElement == null || event.srcElement.tagName != "TD") return;
  var dataField = c1g_getCellDataField(event.srcElement);
  if (dataField)
    c1g_editCell(gridID, dataField, event.srcElement);
}

function c1g_getCellDataField(cell)
{
  var colgroup = cell.parentNode.parentNode.parentNode.firstChild;
  return colgroup.childNodes[event.srcElement.cellIndex].getAttribute("DataField");
}


function c1g_updateEditedData(gridID)
{
  var result = "";
  for (row in edited_state)
  {
    for (cell in edited_state[row])
    {
      var editedCellData = row + "\03" + cell + "\03" + (edited_state[row])[cell];
      result += ((result == "")? "":"\02") + editedCellData;
    }
  }
  (new c1g_getObj("__" + gridID + "_EditedData")).obj.value = result;
}


function c1g_ebKeyDown(event, gridID)
{
	if (event.keyCode==13 || event.keyCode==27)
	{
		event.cancelBubble = true;
		event.returnValue = false;
    if (event.keyCode == 27)
      event.srcElement.setAttribute("cancelled", true);
    c1g_ebLeave(event, gridID);
		return false;
	}
}


function c1g_ebLeave(event, gridID)
{
  var textedit = event.srcElement;
  if (textedit.style.display == "")
    textedit.style.display = "none";
  if (textedit.getAttribute("cancelled"))
  {
    textedit.removeAttribute("cancelled");
  } 
  else if (editedCell != null && editedCell.innerText != textedit.value)
  {
    editedCell.innerText = textedit.value;
    var rowIndex = parseInt(editedCell.parentNode.getAttribute("ItemIndex"));
    var dataField = editedCell.getAttribute("DataField");
    
    if (!edited_state[rowIndex])
      edited_state[rowIndex] = new Array();
     
    (edited_state[rowIndex])[dataField] = textedit.value;

    c1g_updateEditedData(gridID);       
  }
  if (editedCell)
		editedCell.removeAttribute("DataField");  
  editedCell = null;
}

function c1g_taKeyDown(event, gridID)
{
	if (event.keyCode == 27)
	{
		event.srcElement.setAttribute("cancelled", true);
		c1g_taLeave(event, gridID);
		return false;
	}
}


function c1g_taLeave(event, gridID)
{
  var textarea = event.srcElement;
  if (textarea.style.display == "")
    textarea.style.display = "none";
  if (textarea.getAttribute("cancelled"))
  {
    textarea.removeAttribute("cancelled");
  } 
  else if (editedCell != null && editedCell.innerText != textarea.value)
  {
    editedCell.innerText = textarea.value;
    var rowIndex = parseInt(editedCell.parentNode.getAttribute("ItemIndex"));
    var dataField = editedCell.getAttribute("DataField");
    
    if (!edited_state[rowIndex])
      edited_state[rowIndex] = new Array();
     
    (edited_state[rowIndex])[dataField] = textarea.value;

    c1g_updateEditedData(gridID);       
  }
  if (editedCell)
		editedCell.removeAttribute("DataField");  
  editedCell = null;
}

function c1g_ddKeyDown(event, gridID)
{
	if (event.keyCode==27)
	{
		event.cancelBubble = true;
		event.returnValue = false;
    event.srcElement.setAttribute("cancelled", true);
    c1g_ddLeave(event, gridID);
		return false;
	}
}


function c1g_ddLeave(event, gridID)
{
  var dropdown = event.srcElement;
  if (dropdown.style.display == "")
    dropdown.style.display = "none";
  if (dropdown.getAttribute("cancelled"))
  {
    dropdown.removeAttribute("cancelled");
  } 
  else if (editedCell != null)
  {
    var newvalue = dropdown.options[dropdown.selectedIndex].innerText;
   
    if (editedCell.innerText != newvalue)
    {
      editedCell.innerText = newvalue;
      var rowIndex = parseInt(editedCell.parentNode.getAttribute("ItemIndex"));
			var dataField = editedCell.getAttribute("DataField");
    
      if (!edited_state[rowIndex])
        edited_state[rowIndex] = new Array();
     
      (edited_state[rowIndex])[dataField] =
        dropdown.options[dropdown.selectedIndex].value;

      c1g_updateEditedData(gridID);       
    }
  }
  if (editedCell)
		editedCell.removeAttribute("DataField");
  editedCell = null; 
}


function c1g_editCell(gridID, dataField, cell)
{
  var column = cell.parentNode.parentNode.parentNode.firstChild.children[cell.cellIndex];
  var valueList = eval(column.getAttribute("ValueList"));
  var miltilineEdit = column.getAttribute("Multiline");
  var zIndex = (new c1g_getObj(gridID)).obj.style.zIndex;
  if (zIndex)
    zIndex = 1 + parseInt(zIndex);
  if (valueList)
  {
    var dropdown = (new c1g_getObj(gridID + "_dd")).obj;
    if (dropdown == null || !cell.parentNode.getAttribute("ItemIndex")) return; 
    
		while (dropdown.childNodes.length>0)
			dropdown.removeChild(dropdown.childNodes[0]);

		for (var i = 0; i < valueList.length; i++)
		{
			if (valueList[i])
			{
				var option = document.createElement("OPTION");
				dropdown.appendChild(option);
				option.value = valueList[i][0];
				option.innerText = valueList[i][1];
				if (cell.innerText == valueList[i][1])
					option.selected = true;
			}
		}   
    dropdown.style.left = c1g_findPosX(cell);
    dropdown.style.top = c1g_findPosY(cell);
    dropdown.style.width = cell.clientWidth;
    dropdown.style.height = cell.clientHeight;
    if (dropdown.style.display == "none")
    {
      dropdown.style.display = "";
      if (zIndex)
        dropdown.style.zIndex = zIndex;
    }
    editedCell = cell;
    editedCell.setAttribute("DataField", dataField);
    dropdown.focus();
  }
  else if (miltilineEdit)
  {
    var textarea = (new c1g_getObj(gridID + "_ta")).obj;
    if (textarea == null || !cell.parentNode.getAttribute("ItemIndex")) return;
    textarea.style.left = c1g_findPosX(cell);
    textarea.style.top = c1g_findPosY(cell);
    textarea.style.width = cell.clientWidth;
    textarea.style.height = cell.clientHeight;
    textarea.innerText = cell.innerText;
    if (textarea.style.display == "none")
    {
      textarea.style.display = "";
      if (zIndex)
        textarea.style.zIndex = zIndex;     
    }
    editedCell = cell;
    editedCell.setAttribute("DataField", dataField);
    textarea.focus();
    textarea.select();
  }
  else
  {
    var textedit = (new c1g_getObj(gridID + "_eb")).obj;
    if (textedit == null || !cell.parentNode.getAttribute("ItemIndex")) return;
    textedit.style.left = c1g_findPosX(cell);
    textedit.style.top = c1g_findPosY(cell);
    textedit.style.width = cell.clientWidth;
    textedit.style.height = cell.clientHeight;
    textedit.innerText = cell.innerText;
    if (textedit.style.display == "none")
    {
      textedit.style.display = "";
      if (zIndex)
        textedit.style.zIndex = zIndex;     
    }
    editedCell = cell;
    editedCell.setAttribute("DataField", dataField);
    textedit.focus();
    textedit.select();
  }
}