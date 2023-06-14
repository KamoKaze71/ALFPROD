<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Currency.aspx.vb" Inherits="Wyeth.Alf.Currency"%>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Currency</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../JS/ClientScripts.js"> </script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:Label id="lblPageTitle" runat="server" Width="100%" CssClass="lblPageTitle"></asp:Label>
			<asp:panel id="FilterPanel" cssclass="FilterPanel" runat="server" Width="100%" Height="32px"
				Visible="True">
				<asp:Button id="Button_Add" runat="server" Width="96px" Text="Add new Record" cssClass="button"></asp:Button>
			</asp:panel>
			<asp:panel id="EditPanel" cssclass="EditPanel" runat="server" Visible="False">
				<TABLE cellSpacing="0" cellPadding="2" width="100%" border="0">
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field">Currency ID</TD>
						<TD><IMG height="1" alt="" src="images/trans.gif" width="10" border="0"></TD>
						<TD width="100%">
							<asp:TextBox id="txtCurrId" runat="server" CssClass="formfield" Enabled="False"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field">Code</TD>
						<TD><IMG height="1" alt="" src="images/trans.gif" width="10" border="0"></TD>
						<TD width="100%">
							<asp:TextBox id="txtCurrCode" runat="server" CssClass="formfield"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field">Description</TD>
						<TD><IMG height="1" alt="" src="images/trans.gif" width="10" border="0"></TD>
						<TD width="100%">
							<asp:TextBox id="txtCurrDescription" runat="server" CssClass="formfield"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD align="right" colSpan="3">
							<asp:Button id="Button_Insert" runat="server" Text="Insert" cssClass="button"></asp:Button>
							<asp:Button id="Button_update" runat="server" Text="Update" cssClass="button"></asp:Button>
							<asp:Button id="Button_Cancel" runat="server" Text="Cancel" cssClass="button"></asp:Button></TD>
					</TR>
				</TABLE>
			</asp:panel>
			<asp:panel id="GridPanel" cssclass="GridPanel" runat="server">
				<C1WebGrid:C1WebGrid id="MyGrid" runat="server" AutoGenerateColumns="False" width="100%">
					<Columns>
						<c1webgrid:C1BoundColumn Visible="False" DataField="curr_id"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="curr_code" HeaderText="Code"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="curr_description" HeaderText="Description"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1ButtonColumn Visible="False" ButtonType="PushButton" Text="Button"></c1webgrid:C1ButtonColumn>
						<c1webgrid:C1ButtonColumn ButtonType="PushButton" Text="Delete" CommandName="Delete">
							<HeaderStyle Width="60px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</c1webgrid:C1ButtonColumn>
					</Columns>
				</C1WebGrid:C1WebGrid>
			</asp:panel>
			<input type="hidden" id="inpID" runat="server" NAME="inpID"></form>
	</body>
</HTML>
