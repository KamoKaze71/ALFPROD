<%@ Page Language="vb" AutoEventWireup="false" Codebehind="AlfService.aspx.vb" Inherits="Wyeth.Alf.AlfService"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>AlfService</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Styles.css" type="text/css" rel="stylesheet">
		<meta http-equiv="refresh" content="5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div class="reportsButtonBar">
				<asp:DropDownList id="DropDownList1" runat="server" Width="168px" AutoPostBack="True">
					<asp:ListItem Value="ALFService">ALFService</asp:ListItem>
					<asp:ListItem Value="ALFExportService">ALFExportService</asp:ListItem>
				</asp:DropDownList>
				<asp:TextBox style="VISIBILITY: hidden" ID="txtrem" Runat="server" EnableViewState="true">ALFService</asp:TextBox>
			</div>
			<br>
			<br>
			<br>
			<table align="center" width="60%">
				<tr align="center">
					<td><asp:button id="MyButton" runat="server" Text="Button" CssClass="button" Width="150px"></asp:button>
					</td>
				</tr>
				<tr>
					<td><br>
						<br>
						<br>
					</td>
				</tr>
				<tr>
					<td align="center" height="30">
						<asp:label id="MyLabel" runat="server" Height="100%" Width="100%" Font-Bold="True"></asp:label>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
