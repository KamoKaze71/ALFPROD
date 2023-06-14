<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ImportDistributor.aspx.vb" Inherits="Wyeth.Alf.ImportDistributor"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ImportDistributor</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<asp:literal id="_clientScript" runat="server"></asp:literal>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<div class="reportsButtonBar">
				<table width="95%">
					<tr align="left">
						<td><asp:dropdownlist id="dddistribSelect" runat="server"></asp:dropdownlist></td>
						<td><ASP:RADIOBUTTONLIST id="rbl_AutomaticUpdate" BORDERCOLOR="Black" BORDERSTYLE="Solid" BORDERWIDTH="1px"
								AUTOPOSTBACK="True" RUNAT="server" WIDTH="176px">
								<ASP:LISTITEM VALUE="automatic" SELECTED="True">Automatic Update</ASP:LISTITEM>
								<ASP:LISTITEM VALUE="manual" SELECTED="false">Manual Update</ASP:LISTITEM>
							</ASP:RADIOBUTTONLIST></td>
						<td>
							<asp:Button id="btn_delete_transmission" runat="server" CssClass="button" Text="Delete Transmission"
								CausesValidation="False"></asp:Button></td>
					</tr>
				</table>
			</div>
			<asp:panel id="auto_panel" runat="server">
				<TABLE height="100%" cellPadding="3" border="1">
					<TR>
						<TD align="center" width="300">
							<asp:Button id="btn_connect" CssClass="button" Text="Connect to FTP Server" Runat="server"></asp:Button></TD>
						<TD width="100%">
							<asp:Button id="btn_Import" runat="server" CssClass="button" Text="Import Selected Files" Visible="False"></asp:Button></TD>
					</TR>
					<TR height="100%">
						<TD rowSpan="3">
							<asp:ListBox id="lb_ftpfiles" runat="server" Height="100%" Width="100%" Visible="False"></asp:ListBox></TD>
						<TD vAlign="top" rowSpan="3">
							<asp:Label id="lblOut" Runat="server"></asp:Label></TD>
					</TR>
				</TABLE>
			</asp:panel><asp:panel id="manual_panel" Runat="server">
				<TABLE>
					<TR>
						<TD>
							<asp:button id="btn_manual_import" runat="server" CssClass="button" Text="Import Sanova Data"></asp:button></TD>
						<TD>
							<asp:textbox id="txtPath" runat="server" CssClass="formfield" ToolTip="Please Enter the path on the Server where the import Files are"></asp:textbox></TD>
					</TR>
					<TR>
						<TD colSpan="2">
							<asp:Label id="lblOut_manual" Runat="server"></asp:Label></TD>
						</TD></TR>
				</TABLE>
			</asp:panel></form>
	</body>
</HTML>
