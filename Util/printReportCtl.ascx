<%@ Control Language="vb" AutoEventWireup="false" Codebehind="printReportCtl.ascx.vb" Inherits="Wyeth.Alf.printReportCtl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<style type="text/css">
	#menu { PADDING-RIGHT: 0px; PADDING-LEFT: 0px; LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 0px; WIDTH: 100px; PADDING-TOP: 0px; LIST-STYLE-TYPE: none; POSITION: static; TOP: 0px }
	#preview { LEFT: 0px; WIDTH: 100px; POSITION: absolute; TOP: 0px }
</style>
<span id="menu">
	<asp:button id="btnPrint" style="BACKGROUND-POSITION: right 50%; BACKGROUND-IMAGE: url(/images/bg_button.gif); BACKGROUND-REPEAT: no-repeat"
		Text="Print PDF" Runat="server" CssClass="button"></asp:button>
	<span id="preview" style="DISPLAY:none"></span></span>
<script language="javascript">

	function swap(obj) {
		if (obj.style.display == 'none') {
			obj.style.display = 'inline';
		} else {
			obj.style.display = 'none';
		}
	}

function position(obj) {
		//alert(findPosX(obj));
		preview.style.left = findPosX(obj);
		preview.style.top = findPosY(obj)+20;
	}

	function findPosX(obj)
	{
		var curleft = 0;
		if (obj.offsetParent)
		{
			while (obj.offsetParent)
			{
				curleft += obj.offsetLeft
				obj = obj.offsetParent;
			}
		}
		else if (obj.x)
			curleft += obj.x;
		return curleft;
	}	

	function findPosY(obj)
	{
		var curtop = 0;
		if (obj.offsetParent)
		{
			while (obj.offsetParent)
			{
				curtop += obj.offsetTop
				obj = obj.offsetParent;
			}
		}
		else if (obj.y)
			curtop += obj.y;
		return curtop;
	}

</script>
