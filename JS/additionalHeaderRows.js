////////////////////////////////////////////////////////////////////////
// MICHAL G. 8.1.2003
//
// hinzufügen von doppelten headlines. aufgrund von bugs im c1.webgrid
// muss man es mittels javascript lösen. 
// zuerst muss man die row adden und dann kann man einzelne cells adden.
// Beispiel:
//
// createRow("myGrid");
// addTableCell("myGrid", "CURRENCY: EUR", "1", "currency", 0);
// addTableCell("myGrid", "Month to Date", "5", "", 1);
// addTableCell("myGrid", "Year to Date", "5", "", 2);
//
////////////////////////////////////////////////////////////////////////

function createRow(tableID) {
	var table = document.getElementById(tableID);
	var row = table.insertRow(table.rows[0]);
}

function addTableCell (tableID, message, colspan, cssclass, postion) { 
	var table = document.getElementById(tableID);
	var row = table.rows[0];
	var cell = row.insertCell(postion);
	
	if (cssclass == "") {
		cssclass = "head";
	}
	
	cell.appendChild(document.createTextNode(message));
	cell.setAttribute("align", "center");
	cell.setAttribute("colSpan", colspan);
	cell.className = cssclass;
	cell.style.borderWidth = "0px";
}