<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Loading.aspx.vb" Inherits="Wyeth.Alf.Loading" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Loading</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript">
	
		var iLoopCounter = 1;
		var iMaxLoop = 5;
		var iIntervalId;
		
		function BeginPageLoad() {
			location.href = "<%= Request.QueryString("Page")%>";
			iIntervalId = window.setInterval("iLoopCounter=UpdateProgress(iLoopCounter, iMaxLoop)", 500);
		}
	
		function EndPageLoad() {
			window.clearInterval(iIntervalId);
			Progress.innerText = "Page Loaded -- Not Transferring";
		}
	
		function UpdateProgress(iCurrentLoopCounter, iMaximumLoops) {
		
			iCurrentLoopCounter += 1;
			
			if (iCurrentLoopCounter <= iMaximumLoops) {
				Progress.innerText += ".";
				return iCurrentLoopCounter;
			}
			else {
				Progress.innerText = "";
				return 1;
			}
		}
		</script>
	</HEAD>
	<body onload="BeginPageLoad()" onunload="EndPageLoad()">
		<form id="Form1" method="post" runat="server">
			<table border="0" cellpadding="0" cellspacing="0" width="99%" height="99%" align="center" valign="middle">
				<tr>
					<td align="center" valign="middle">
						<font color="Red" size="5">
							<span id="Message">Loading&nbsp;-- Please Wait</span>
							<span id="Progress" style="WIDTH:25px;TEXT-ALIGN:left"></span>
						</font>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
