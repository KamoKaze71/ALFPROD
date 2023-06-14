<%@ Page Language="vb" AutoEventWireup="false" Codebehind="printReport.aspx.vb" Inherits="Wyeth.Alf.printReport" enableViewState="True" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title></title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%= Request.ApplicationPath %>/printStyles.css" type="text/css" rel="stylesheet">
		<script src="<%= Request.ApplicationPath %>/util/printReport.js" language="javascript"></script>
		<style> 
		.forPrint { DISPLAY: inline } 
		.headers { FONT-SIZE: 8pt;PADDING-RIGHT: 15px; PADDING-LEFT: 15px; PADDING-BOTTOM: 15px; PADDING-TOP: 15px } 
		.dgitem { FONT-SIZE: 8pt;PADDING-RIGHT: 4px; PADDING-LEFT: 4px; PADDING-BOTTOM: 4px; MARGIN: 2px; PADDING-TOP: 4px; BORDER-BOTTOM: #fff 4px solid; BACKGROUND-COLOR: #eee } 
		#toggleCustomers { FONT-SIZE: 8pt; MARGIN-BOTTOM: 15px } 
		</style>
	</HEAD>
	<body style="OVERFLOW: auto" class="Body" MS_POSITIONING="GridLayout" onload="alignWaitingBox();">
		<form id="frm" method="post" runat="server">
			<div class="toolbar" id="pnlToolbar">
				<TABLE>
					<TR>
						<TD noWrap><asp:Label Runat="server" ID="lblNumber">Number of lines:&nbsp;&nbsp;</asp:Label></TD>
						<TD><asp:dropdownlist id="ddLines" Runat="server" CssClass="select" Width="100"></asp:dropdownlist></TD>
						<TD noWrap><asp:Label Runat="server" ID="lblOrientation">Orientation:</asp:Label></TD>
						<TD><asp:dropdownlist id="ddlOrientation" Runat="server" CssClass="select" Width="100">
								<asp:ListItem Selected="True" Value="1">Portrait</asp:ListItem>
								<asp:ListItem Value="2">Landscape</asp:ListItem>
							</asp:dropdownlist>&nbsp;&nbsp;</TD>
						<TD align="right" width="100%">
							<!--<asp:button id="btnGenerate" Runat="server" CssClass="button_common" style="BACKGROUND-POSITION:3px 1px; LEFT:-3px; BACKGROUND-IMAGE:url(../images/pr_refresh.gif); BACKGROUND-REPEAT:no-repeat; POSITION:relative; TOP:-1px"
								Width="100" Text="Generate" Height="22"></asp:button>
								-->
							<button class="button_common" style="BACKGROUND-POSITION:3px 1px; LEFT:-3px; BACKGROUND-IMAGE:url(../images/pr_refresh.gif); WIDTH:100px; BACKGROUND-REPEAT:no-repeat; POSITION:relative; TOP:-1px; HEIGHT:22px"
								onclick="showWaitingBox(true);frm.submit();" type="button">Generate</button>
							<button class="button_common" style="BACKGROUND-POSITION:3px 2px; BACKGROUND-IMAGE:url(../images/pr_print.gif); WIDTH:100px; BACKGROUND-REPEAT:no-repeat; POSITION:relative; TOP:-1px; HEIGHT:22px"
								onclick="javascript:window.print();" type="button" value="Print">Print</button>
							<button class="button_common" style="BACKGROUND-POSITION:3px 1px; LEFT:3px; BACKGROUND-IMAGE:url(../images/pr_close.gif); WIDTH:100px; BACKGROUND-REPEAT:no-repeat; POSITION:relative; TOP:-1px; HEIGHT:22px"
								onclick="javascript:window.close();" type="button" value="Close">Close</button>
						</TD>
					</TR>
				</TABLE>
			</div>
			<div align="center" id="waiting" class="waiting" style="DISPLAY:none">
				<span class="waiting_text">
					Loading<br>
					Please Wait ... </span>
			</div>
			<div class="mainDiv">
				<asp:literal EnableViewState="False" id="litPrint" runat="server"></asp:literal>
			</div>
		</form>
	</body>
</HTML>
