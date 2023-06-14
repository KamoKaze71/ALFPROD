<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DeleteLogs.aspx.vb" Inherits="Wyeth.Alf.DeleteLogs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>DeleteLogs</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<asp:literal id="_clientScript" runat="server"></asp:literal>
		<LINK href="../../Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../../JS/ClientScripts.js"> </script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" width="98%">
				<TR align="left">
					
					<TD class="field" noWrap>Start Date:</TD>
					<TD>
						<BOX:WYETHTEXTBOX id="txtStartDate" TOOLTIP="Please enter a date" FRIENDLYNAME="Report Start Date"
							ALLOWBLANK="false" FIELDTYPE="DATE" RUNAT="server"></BOX:WYETHTEXTBOX></TD>
					<TD><IMG src="/Images/kalender.gif" border="0" runat="server" id="imgstartkal"></TD>
					<TD class="field" noWrap>End Date:</TD>
					<TD>
						<BOX:WYETHTEXTBOX id="txtEndDate" TOOLTIP="Please enter a date" FRIENDLYNAME="Report End Date" ALLOWBLANK="false"
							FIELDTYPE="DATE" RUNAT="server"></BOX:WYETHTEXTBOX></TD>
					<TD><IMG src="/Images/kalender.gif" border="0" runat="server" id="imgendkal"></TD>
				</TR>
				<tr>
					<td colspan="6" align="center"><p></p>
					</td>
				</tr>
				<tr>
					<td colspan="6" align="center"><asp:button id="Button_DeleteLogs" runat="server" CssClass="button" Text="Delete Logs" width="250px"></asp:button></td>
				</tr>
			</TABLE>
			<asp:Label id="lblOut" width="100%" height="40px" runat="server"></asp:Label>
		</form>
	</body>
</HTML>
