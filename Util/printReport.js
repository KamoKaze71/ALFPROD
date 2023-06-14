function alignWaitingBox()
{
	var currentDiv = document.all['waiting'];
	var newPosY = window.screen.height/2 - 70;
	var newPosX = window.screen.width/2 - 200;
	
	currentDiv.style.top = newPosY;
	currentDiv.style.left = newPosX;
}

function showWaitingBox(flag)
{
	if(flag)
	{
		document.all['waiting'].style.display = "inline";
		document.body.style.cursor ='wait';
	}
	else
		document.all['waiting'].style.display = "none";
}

function printAuto()
{
	window.print();
	setTimeout("window.close()",5000);
}
