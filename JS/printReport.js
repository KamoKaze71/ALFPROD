function alignWaitingBox()
{
	var currentDiv = document.all['waiting'];
	var newPosY = window.screen.height/2 - 130;
	var newPosX = window.screen.width/2 - 75;
	
	currentDiv.style.top = newPosY;
	currentDiv.style.left = newPosX;
}

function showWaitingBox(flag)
{
	if(flag)
		document.all['waiting'].style.display = "inline";
	else
		document.all['waiting'].style.display = "none";
}

function printAuto()
{
	//showWaitingBox(true);
	window.print();
	window.close();
}