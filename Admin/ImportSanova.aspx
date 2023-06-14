<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid, Version=1.1.20034.32, Culture=neutral, PublicKeyToken=589f1fc067ff4031" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ImportSanova.aspx.vb" Inherits="Wyeth.Alf.ImportSanova"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>import</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../JS/ClientScripts.js"> </script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div class="hl">
				<asp:label id="lblPageTitle" runat="server" Width="100%"></asp:label>
			</div>
			<table width="100%" cellpadding="3">
				<tr>
					<td colSpan="2"><asp:dropdownlist id="dddistribSelect" runat="server" Width="209px"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td colSpan="2"></td>
				</tr>
				<TR>
					<TD style="WIDTH: 204px"><ASP:BUTTON id="Button_delete_transmission" WIDTH="136px" TEXT="Delete Transmission" CSSCLASS="button_common"
							RUNAT="server"></ASP:BUTTON></TD>
					<TD><ASP:DROPDOWNLIST id="ddTransmissions" WIDTH="440px" CSSCLASS="formfield" RUNAT="server"></ASP:DROPDOWNLIST></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 204px; HEIGHT: 17px">
						<asp:button id="btn_manual_import" runat="server" Text="Import Sanova Data" CssClass="button"></asp:button></TD>
					<TD style="HEIGHT: 17px">
						<asp:textbox id="txtPath" runat="server" CssClass="formfield" ToolTip="Please Enter the path on the Server where the import Files are"></asp:textbox></TD>
				</TR>
				<tr>
					<td colSpan="2"></td>
				</tr>
				<TR>
					<TD colSpan="2"><ASP:RADIOBUTTONLIST id="rbl_AutomaticUpdate" WIDTH="176px" RUNAT="server" HEIGHT="33px" AUTOPOSTBACK="True"
							BORDERWIDTH="1px" BORDERSTYLE="Solid" BORDERCOLOR="Black" BACKCOLOR="GhostWhite">
							<ASP:LISTITEM VALUE="automatic" SELECTED="True">Automatic Update</ASP:LISTITEM>
							<ASP:LISTITEM VALUE="manual" SELECTED="false">Manual Update</ASP:LISTITEM>
						</ASP:RADIOBUTTONLIST></TD>
				</TR>
				<TR>
					<TD colSpan="2"></TD>
				</TR>
				<tr>
					<TD colSpan="2">
						<TABLE width="100%">
							<TR>
								<TD><ASP:BUTTON id="Button_KD" WIDTH="141px" TEXT="Import KD" CSSCLASS="button_common" RUNAT="server"
										ENABLED="False"></ASP:BUTTON></TD>
								<TD><ASP:BUTTON id="Button_ART" WIDTH="142px" TEXT="Import ART" CSSCLASS="button_common" RUNAT="server"
										ENABLED="False"></ASP:BUTTON></TD>
								<TD><ASP:BUTTON id="Button_BW" WIDTH="142px" TEXT="Import BW" CSSCLASS="button_common" RUNAT="server"
										ENABLED="False"></ASP:BUTTON></TD>
								<TD><ASP:BUTTON id="Button_MView" WIDTH="142px" TEXT="Refresh MViews" CSSCLASS="button_common" RUNAT="server"
										ENABLED="False"></ASP:BUTTON></TD>
							</TR>
						</TABLE>
					</TD>
				</tr>
				<TR>
					<td colSpan="2">
						<asp:Label id="LabelOut" runat="server" Width="592px" Height="248px"></asp:Label></td>
				</TR>
			</table>
		</form>
	</body>
</HTML>
