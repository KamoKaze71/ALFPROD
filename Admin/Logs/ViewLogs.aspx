<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ViewLogs.aspx.vb" Inherits="Wyeth.Alf.ViewLogs" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ViewLogs</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Styles.css" type="text/css" rel="stylesheet">
		<asp:literal id="_clientScript" runat="server"></asp:literal>
		<script language="JavaScript" src="../../JS/ClientScripts.js"> </script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<div class="reportsButtonBar">
				<table width="98%" >
					<tr align="left">
						<td><asp:dropdownlist id="MyDropDown" runat="server" Height="24px" AutoPostBack="True"></asp:dropdownlist></td>
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
						<td width=100% align=center><asp:button id="Button_showLogs" runat="server" width="200px" Text="Show Logs" CssClass="button"></asp:button><br>
							<input type="button" id="button_deleteLogs" style="WIDTH: 200px" runat="server" Class="button" width="250px"></input></td>
					</tr>
				</table>
			</div>
			<C1WEBGRID:C1WEBGRID id="MyGrid" runat="server" Width="100%" AutoGenerateColumns="False" OnItemCommand="MyGrid_ItemCommand"
				DefaultColumnWidth="120px" DefaultRowHeight="22px">
				<Columns>
					<c1webgrid:C1BoundColumn HeaderText="Date" DataField="Logs_Date_Changed"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Source" DataField="Logs_Source"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn Visible="False" HeaderText="User Id" DataField="Logs_User_Id"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn Visible="False" DataField="logs_description"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1ButtonColumn Visible="False" ButtonType="PushButton" Text="Button"></c1webgrid:C1ButtonColumn>
				</Columns>
			</C1WEBGRID:C1WEBGRID>
			<asp:Panel id="detailPanel" runat="server" Height="376px" Visible="false">
				<INPUT class="button" onclick="history.back()" type="button" value="Cancel">
				<asp:TextBox id="detailLogText" runat="server" Height="100%" CssClass="formfield" Width="100%"
					TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
			</asp:Panel>
		</form>
	</body>
</HTML>
