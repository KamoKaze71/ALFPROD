<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Holidays.aspx.vb" Inherits="Wyeth.Alf.Holidays"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Holidays</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../JS/ClientScripts.js"> </script>
		<asp:literal id="_clientScript" runat="server"></asp:literal>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div class="hl"><asp:label id="lblPageTitle" runat="server"></asp:label></div>
			<div class="reportsButtonBar" id="FilterPanel" runat="server">
				<table width="97%">
					<tr align="left">
						<td><asp:button id="Button_Add" runat="server" Text="Add new Record" Width="110px" CssClass="button"></asp:button></td>
					</tr>
				</table>
			</div>
			<asp:panel id="GridPanel" runat="server" cssclass="GridPanel">
				<C1WebGrid:C1WebGrid id="MyGrid" runat="server" DefaultRowHeight="22px" DefaultColumnWidth="120px" width="100%"
					AutoGenerateColumns="False">
					<Columns>
						<c1webgrid:C1BoundColumn DataField="holi_id" Visible="False"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="holi_day" HeaderText="Day"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="weekDay" HeaderText="Day of Week"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1ButtonColumn Visible="False" Text="Button"></c1webgrid:C1ButtonColumn>
						<c1webgrid:C1ButtonColumn ButtonType="PushButton" HeaderText="Delete" Text="Delete" CommandName="Delete">
							<HeaderStyle Width="60px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</c1webgrid:C1ButtonColumn>
					</Columns>
				</C1WebGrid:C1WebGrid>
			</asp:panel><asp:panel id="EditPanel" runat="server" CssClass="EditPanel" Visible="False">
				<TABLE cellSpacing="2" cellPadding="2" width="100%" border="0">
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field" style="WIDTH: 31px">ID</TD>
						<TD>
							<asp:TextBox id="txtholidayID" runat="server" CssClass="formfield" Enabled="False"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field" style="WIDTH: 31px">Day</TD>
						<TD>
							<IMG id="kalenderImage" src="/Images/kalender.gif" border="0" runat="server"><asp:TextBox id="txtHolidayDay" runat="server" CssClass="formfield"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field" style="WIDTH: 31px">Day of Week</TD>
						<TD>
							<asp:TextBox id="txtDayOfWeek" runat="server" CssClass="formfield" Enabled="false"></asp:TextBox></TD>
					</TR>
				</TABLE>
				<asp:Button id="Button_Insert" runat="server" CssClass="button" Width="110px" Text="Insert"></asp:Button>
				<asp:Button id="Button_Update" runat="server" CssClass="button" Width="110px" Text="Update"></asp:Button>
				<asp:Button id="Button_Cancel" runat="server" CssClass="button" Width="110px" Text="Cancel"></asp:Button>
			</asp:panel><input id="inpID" type="hidden" name="inpID" runat="server">
		</form>
	</body>
</HTML>
