<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ApplicationMessages.aspx.vb" Inherits="Wyeth.Alf.ApllicationMessages"%>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ApllicationMessages</title>
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
				<asp:Label id="lblPageTitle" runat="server" Width="100%"></asp:Label>
			</div>
			<div id="FilterPanel" class="reportsButtonBar" runat="server">
				<table width="100%">
					<tr align="left">
						<td>
							<asp:Button id="Button_Add" runat="server" CssClass="button" Width="110px" Text="Add new Record"></asp:Button>
						</td>
					</tr>
				</table>
			</div>
			<asp:panel id="GridPanel" cssclass="GridPanel" runat="server">
				<C1WebGrid:C1WebGrid id="MyGrid" runat="server" Width="100%" AutoGenerateColumns="False">
					<Columns>
						<c1webgrid:C1BoundColumn DataField="AMsg_ID" Visible="False"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="AMsg_Number" HeaderText="Message No."></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="AMsg_Message" HeaderText="Message"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1ButtonColumn Visible="False" Text="Button"></c1webgrid:C1ButtonColumn>
						<c1webgrid:C1ButtonColumn ButtonType="PushButton" HeaderText="Delete" Text="Delete" CommandName="Delete">
							<HeaderStyle Width="60px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</c1webgrid:C1ButtonColumn>
					</Columns>
				</C1WebGrid:C1WebGrid>
			</asp:panel>
			<asp:panel id="EditPanel" runat="server" Visible="False" cssClass="EditPanel">
				<TABLE cellSpacing="0" cellPadding="2" width="100%" align="center" border="0">
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field" noWrap>ID:</TD>
						<TD width="100%">
							<asp:TextBox id="txtMessageID" runat="server" Width="100%" cssClass="formfield" Enabled="False"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field">Message No:</TD>
						<TD width="100%">
							<asp:TextBox id="txtMessageNo" runat="server" Width="100%" cssClass="formfield"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field">Message:</TD>
						<TD width="100%">
							<asp:TextBox id="txtMessageMessage" runat="server" Width="100%" cssClass="formfield"></asp:TextBox></TD>
					</TR>
				</TABLE>
				<asp:Button id="Button_Insert" runat="server" Width="110px" Text="Insert" CssClass="button"></asp:Button>
				<asp:Button id="Button_update" runat="server" Width="110px" Text="Update" CssClass="button"></asp:Button>
				<asp:Button id="Button_Cancel" runat="server" Width="110px" Text="Cancel" CssClass="button"></asp:Button>
			</asp:panel>
			<input id="inpID" type="hidden" name="inpID" runat="server"></form>
	</body>
</HTML>
