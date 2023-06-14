function setWindowSize(win, height, width) {
	win.resizeTo(width, height);
}

function showConfirm(message) {
	var result;
	result = confirm(message);
	if (result) {
		return true
	} else {
		return false
	}
}

function closePopup(win, pageToRefresh) {
	//alert(win.opener.parent.frames.main.location);
	win.opener.location.href = win.opener.location.href;
	win.close();
}

function closePopupAndRedirect(url) {
	window.opener.location.href = url;
	window.close();
}

function showPopup(pageUrl, w, h) {
	var newWin = window.open(pageUrl, "newWindow", 'width=' + w + ',height=' + h + ',left=70,top=80,scrollbars=yes,status=0');
	newWin.focus();
}

function checkPercentageField(field, percentUsedField, percentFreeField) {
	if (isNumber(parseKomma(field.value))) {
		if ((parseKomma(field.value) > 100)) {
			alert("Value must be lower or equal 100%");
			field.focus();
			return false;
		} else {
			field.value = roundValue(parseKomma(field.value));
			calculatePercent(percentUsedField, percentFreeField);
			field.value = field.value.replace(".", ",");
			return true;
		}
	} else {
		alert("Value must be a number.");
		field.focus();
		return false;
	}
}

function submitButton(button, evt) {
	evt = (evt) ? evt : window.event;
	if ((evt.which && evt.which == 13) || (evt.keyCode && evt.keyCode == 13)) {
		button.click();
		return false;
	} else {
		return true;
	}
}

function calculatePercent(percentUsed, percentFree) {
	var percentTotal = 0;

	for (i=0; i < Form1.length; i++) {
		if (Form1[i].name.indexOf("salesRepPercentage") > -1) {
			percentTotal += parseFloat(parseKomma(Form1[i].value));
		}
	}

	setPercentUsed(percentUsed, parseFloat(percentTotal));
	setPercentFree(percentFree, parseFloat(100 - percentTotal));
}

function setPercentUsed(field, value) {
	field.innerText = roundValue(parseFloat(value)) + "%";
	if (value == 100) {
		field.style.color = "green";
	} else {
		field.style.color = "red";
	}
	
	if (value > 100) {
		value = 100;
	}

	//barUsed.style.width = value + "px";
}

function setPercentFree(field, value) {
	field.innerText = roundValue(parseFloat(value)) + "%";
	if (value > 0) {
		field.style.color = "green";
	} else {
		field.style.color = "red";
	}
	
	if (value < 0) {
		value = 0;
	}
	
	//barLeft.style.width = value + "px";
}

function roundValue(val) {
	x = parseInt(val * 100);
	Math.round(x);
	return parseFloat(x / 100);
}

function allowOnlyNumbers(field, evt){
	var keyBackSpace = 8;
	var keyTab = 9;
	var keyRightArrow = 39;
	var keyLeftArrow = 37;
	var keyDel = 46;
	var komma = 188; //190 - punkt;

	evt = (evt) ? evt : window.event
	var kc = evt.keyCode;

	if (kc >= 48 && kc <= 57  || kc >= 96 && kc <= 105 || kc == keyBackSpace || kc == keyTab || kc == keyRightArrow || kc == keyLeftArrow || kc == keyDel || kc == komma) {
		} else {
			if (evt.preventDefault) {
				evt.preventDefault();
				return false;
			} else {
				evt.keyCode = 0;
				evt.returnValue = false;
		}       
	}
}

function parseKomma(val) {
	return val.replace(",",".");
}

function isNumber(myString) {
    var isInteger = new Boolean()
    isInteger = true
    var myChar = ""
    var myInt=0

    if (myString != "" && typeof(myString) == "string") {
		myInt = parseFloat(myString);
		if (isNaN(myInt)) {
            isInteger = false
        }
    } else {
        isInteger = false
    }

    return isInteger
}