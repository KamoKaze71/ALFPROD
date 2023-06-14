<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CustomerGroup.aspx.vb" Inherits="Wyeth.Alf.CustomerGroup"%>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>CustomerGroup</title>
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
				<asp:Label id="lblPageTitle" runat="server"></asp:Label>
			</div>
			<asp:panel id="FilterPanel" CssClass="FilterPanel" runat="server">
				<asp:Button id="Button_Add" runat="server" CssClass="button" Text="Add new Record" Width="110px"></asp:Button>
			</asp:panel>
			<asp:panel id="EditPanel" CssClass="EditPanel" runat="server" Visible="False">
				<TABLE cellSpacing="2" cellPadding="2" width="100%" border="0">
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field" style="WIDTH: 134px" noWrap>Customer Group ID</TD>
						<TD>
							<asp:TextBox id="txtCustomerGrID" runat="server" CssClass="formfield" Enabled="False"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field" style="WIDTH: 134px">Group Code</TD>
						<TD>
							<asp:TextBox id="txtCustomerGrCode" runat="server" CssClass="formfield"></asp:TextBox>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field" style="WIDTH: 134px">Group Description</TD>
						<TD>
							<asp:TextBox id="txtCustomerGrDescription" runat="server" CssClass="formfield"></asp:TextBox></TD>
					</TR>
				</TABLE>
				<asp:Button id="Button_Insert" runat="server" CssClass="button" Text="Insert" Width="110px"></asp:Button>
				<asp:Button id="Button_update" runat="server" CssClass="button" Text="Update" Width="110px"></asp:Button>
				<asp:Button id="Button_Cancel" runat="server" CssClass="button" Text="Cancel" Width="110px"></asp:Button>
			</asp:panel>
			<asp:panel id="GridPanel" CssClass="GridPanel" runat="server" Width="90%">
				<C1WebGrid:C1WebGrid id="MyGrid" runat="server" Width="100%" cssclass="GridPanel" AutoGenerateColumns="False">
					<Columns>
						<c1webgrid:C1BoundColumn Visible="False" DataField="cugr_id"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="cugr_code" HeaderText="Code"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="cugr_description" HeaderText="Description"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1ButtonColumn Visible="False" Text="Button"></c1webgrid:C1ButtonColumn>
						<c1webgrid:C1ButtonColumn ButtonType="PushButton" HeaderText="Delete" Text="Delete" CommandName="Delete">
							<HeaderStyle Width="60px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</c1webgrid:C1ButtonColumn>
					</Columns>
				</C1WebGrid:C1WebGrid>
			</asp:panel>
			<input type="hidden" id="inpID" runat="server" NAME="inpID"></form>
	</body>
</HTML>
