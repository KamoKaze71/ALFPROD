<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TaPrGr.aspx.vb" Inherits="Wyeth.Alf.TaPrGr"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TaPrGr</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../JS/ClientScripts.js"> </script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div class=hl>
			<asp:Label id="lblPageTitle" runat="server" Width="100%"></asp:Label>
			</div>
			<asp:panel id="FilterPanel" runat="server" cssclass="FilterPanel">
				<asp:Button id="Button_Add" runat="server" Width=" 110px" CssClass="button" Text="Add new Record"></asp:Button>
			</asp:panel><asp:panel id="EditPanel" runat="server" Visible="False" CssClass="EditPanel">
<TABLE cellSpacing="2" cellPadding="2" width="100%" border="0">
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field" style="WIDTH: 95px">ID:</TD>
						<TD>
							<asp:TextBox id="txttapg_id" runat="server" CssClass="formfield"></asp:TextBox>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field" style="WIDTH: 95px">Description:</TD>
						<TD>
							<asp:TextBox id="txttapg_description" runat="server" CssClass="formfield"></asp:TextBox></TD>
					</TR>
				</TABLE>
<asp:Button id="Button_Insert" runat="server" Width=" 110px" CssClass="button" Text="Insert"></asp:Button>
<asp:Button id="Button_update" runat="server" Width=" 110px" CssClass="button" Text="Update"></asp:Button>
<asp:Button id="Button_Cancel" runat="server" Width=" 110px" CssClass="button" Text="Cancel"></asp:Button></TD></asp:panel><asp:panel id="GridPanel" runat="server" cssclass="GridPanel">
				<C1WebGrid:C1WebGrid id="MyGrid" runat="server" Width="100%" AutoGenerateColumns="False">
					<Columns>
						<c1webgrid:C1BoundColumn DataField="tapg_id"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="tapg_description" HeaderText="Description"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1ButtonColumn Visible="False" Text="Button"></c1webgrid:C1ButtonColumn>
						<c1webgrid:C1ButtonColumn ButtonType="PushButton" HeaderText="Delete" Text="Delete" CommandName="Delete">
							<HeaderStyle Width="60px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</c1webgrid:C1ButtonColumn>
					</Columns>
				</C1WebGrid:C1WebGrid>
			</asp:panel><input id="inpID" type="hidden" name="inpID" runat="server"></form>
	</body>
</HTML>
