	function submitButton(button, evt) {
	evt = (evt) ? evt : window.event;
	if ((evt.which && evt.which == 13) || (evt.keyCode && evt.keyCode == 13)) {
		button.click();
		return false;
	} else {
		return true;
	}
}
	
	function getconfirm()
		{ 
		if (confirm('Do you want to delete this record?')==true)
				return true;
		else
		{
		window.document.forms[0].inpID.value='dontedit';
			return false;
		}
			}
	function getconfirm_sare()
		{ 
		if (confirm('This will delete the Sales Rep and all assignments to Customers, Products and Target Product Groups. The values of the targets will be deleted. Do you want to delete this entry?')==true)
		{ 	if (confirm('Deleting the Sales Rep cannot be recovered. Do you really want to continue?')==true)
				return true;
					else
					{window.document.forms[0].inpID.value='dontedit';
					return false;
					}
			}
		else
		{
		window.document.forms[0].inpID.value='dontedit';
			return false;
		}
			}
	
	function getconfirmDelete()
		{ 
		if (confirm('Do you want to delete this record?')==true)
				return true;
		else
		{
		return false;
		}
			}
			
	function getconfirmMEC()
		{ 
		if (confirm('Do you want to Process these Items?')==true)
				return true;
		else
		{
			return false;
		}
			}
			
	function getconfirmRollback()
		{ 
		if (confirm('Do you want to Rollback this month?')==true)
				return true;
		else
		{
			return false;
		}
			}
    function getconfirmApproval()
		{ 
		if (confirm('Do you want to approve this month?')==true)
				return true;
		else
		{
			return false;
		}
			}
			
	function getconfirmGIT()
		{ 
		if (confirm('Do you want to remove this GIT Assignment?')==true)
				return true;
		else
		return false;
		}
		
		
		function getconfirmGITComment()
		{ 
		if(! document.forms[0].txtDiff)
		return true;
		   else{
		if (window.document.forms[0].txtDiff.value!='0,00' && window.document.forms[0].txtComment.value=='' && window.document.forms[0].txtDiff.visible==true)
				{
				alert('GIT Difference detected! Please enter a Comment');
				return false;
				}
		else	 
		return true;
		}
				}
		function closeGITAssignWindows()
		{
		//window.opener.Form1.btn_cancel_git_assignment.visible=false;
		//window.opener.Form1.BTN_GIT_ASSIGN.Visible.visible=true;
	self.close();
	window.opener.history.back();
			
		}
			
function OpenPopUp(page,title){
	window.open(page,title,'width=750,height=650,left=70,top=80,scrollbars=yes');
}
function OpenPopUpSmall(page,title){
	window.open(page,title,'width=400,height=400,left=70,top=80,scrollbars=no');
}
function OpenModalPopUp(page,title){
	window.showModalDialog(page,title,'width=750,height=650,left=70,top=80,scrollbars=no,status=no');
   }

 function OpenDatePopUp (ret_field){	
	var width = 158;
	var height = 222;
	var left = 300;
	var top = 150;
	ret_field='../Util/Datepicker.aspx?textbox='+ ret_field;
	var calendarWindow = window.open(ret_field ,'ChooseaDate','width=' + width + ',height=' + height ,'left=' + left ,'top=' + top);
	calendarWindow.focus();
}
 function OpenDatePopUpAMS (ret_field){	
	var width = 158;
	var height = 222;
	var left = 300;
	var top = 150;
	ret_field='../Util/DatepickerAMS.aspx?textbox='+ ret_field;
	var calendarWindow = window.open(ret_field ,'ChooseaDate','width=' + width + ',height=' + height ,'left=' + left ,'top=' + top);
	calendarWindow.focus();
}
function TakeGITValues (txtOrderNo,txtInvoiceNumber,txtComment,currID,txtInvoiceValueGIT)
{
	
	txtInvoiceValueGIT=txtInvoiceValueGIT.replace(',','.')
	
	var tmpInvoiceValue=window.opener.Form1.txtInvoiceValue.value.replace('.','')
	tmpInvoiceValue=tmpInvoiceValue.replace(',','.')
	
	//var x = parseFloat(txtInvoiceValueGIT)					
	//var y = parseFloat(tmpInvoiceValue)  //.replace(',','.')
	 
	 if	 (txtOrderNo != '&nbsp;'){
	 window.opener.Form1.txtOrderNo.value=	txtOrderNo;
	 }
	 if (txtInvoiceNumber != '&nbsp;') {
	 window.opener.Form1.txtInvoiceNumber.value=txtInvoiceNumber;
	 }
	 if (txtComment != '&nbsp;') {
	 window.opener.Form1.txtComment.value=txtComment;
	 }
	 var tmpDiff=tmpInvoiceValue-txtInvoiceValueGIT;
	
	 tmpDiff=Math.round(tmpDiff*100)/100;
	 tmpDiff=tmpDiff.toString();
	 
	
	
	 var len=tmpDiff.lastIndexOf('.')
	 var len2;
	 len2=tmpDiff.length;
	 var lendiff=len2-len;
	 
	 if (len==-1) {
	 tmpDiff=tmpDiff +'.00';
				 }
		 else if (lendiff==2)
		{
		  tmpDiff=tmpDiff +'0';
	//Math.round(tmp*100)/100
	 //window.opener.Form1.txtdiffhidden.value=window.opener.Form1.txtdiffhidden.value.substr(0,len+3)
	 //window.opener.Form1.txtDiff.value=window.opener.Form1.txtDiff.value.substr(0,len+3)
				}
		
	
	 window.opener.Form1.ddInvoiceCurrencyid.value=currID;
	 window.opener.Form1.txtInvoiceValue.value=txtInvoiceValueGIT;
	 window.opener.Form1.txtInvoiceValueHidden.value=txtInvoiceValueGIT;
	 window.opener.Form1.txtDiff.value=tmpDiff;
	 window.opener.Form1.txtdiffhidden.value=tmpDiff;
	 
	
	 currencyFormat(window.opener.Form1.txtDiff,'.',',',event);
	 currencyFormat(window.opener.Form1.txtInvoiceValue,'.',',',event);
	 currencyFormat(window.opener.Form1.txtdiffhidden,'.',',',event);
	 currencyFormat(window.opener.Form1.txtInvoiceValueHidden,'.',',',event);
	 
	 
	 if (window.opener.Form1.txtInvoiceValue==''){
	  window.opener.Form1.txtInvoiceValue.value='0,00';
	 }
	  if (window.opener.Form1.txtDiff.value==''){
	  window.opener.Form1.txtDiff.value='0,00';
	 }
	 //window.opener.Form1.txtDiff.enable=false;
	 //window.opener.Form1.txtInvoiceValue.enable=false;
		 
	 window.opener.Form1.txtDiff.focus;
	 opener.focus();
	 self.close();
	}

function currencyFormat(fld, milSep, decSep, e) {
var sep = 0;
var negative=false;
var key = '';
var i = j = 0;
var len = len2 = 0;
var strCheck = '0123456789';
var aux = aux2 = '';
var whichCode = (window.Event) ? e.which : e.keyCode;
if (whichCode == 13) return true;  // Enter
//key = String.fromCharCode(whichCode);  // Get key value from key code
//if (strCheck.indexOf(key) == -1) return false;  // Not a valid key
len = fld.value.length;
if (fld.value.charAt(0)=='-')
{
fld.value=fld.value.substr(1, len-1);
negative=true;
len = fld.value.length;
}
for(i = 0; i < len; i++)
if ((fld.value.charAt(i) != '0') && (fld.value.charAt(i) != decSep)) break;
aux = '';
for(; i < len; i++)
if (strCheck.indexOf(fld.value.charAt(i))!=-1) aux += fld.value.charAt(i);
aux += key;
len = aux.length;
if (len == 0) fld.value = '';
if (len == 1) fld.value = '0'+ decSep + '0' + aux;
if (len == 2) fld.value = '0'+ decSep + aux;
if (len > 2) {
aux2 = '';
for (j = 0, i = len - 3; i >= 0; i--) {
if (j == 3) {
aux2 += milSep;
j = 0;
}
aux2 += aux.charAt(i);
j++;
}
fld.value = '';
len2 = aux2.length;
for (i = len2 - 1; i >= 0; i--)
fld.value += aux2.charAt(i);
fld.value += decSep + aux.substr(len - 2, len);
}
if (negative==true)
{   fld.value= '-' + fld.value	;
}
if (fld.value=='')
{   fld.value='0,00';
}
return false;
}

 function ClosePopUpAndRefreshOpener(qrystr)
   {   win.opener.location.href = qrystr;
       self.close();
       }


	