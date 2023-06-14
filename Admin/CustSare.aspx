<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CustSare.aspx.vb" Inherits="Wyeth.Alf.CustSare"%>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>CustSare</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../JS/ClientScripts.js"> </script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			  <asp:Label id="lblPageTitle" runat="server" CssClass="lblPageTitle"></asp:Label>
				
		<asp:panel id="FilterPanel" CssClass="FilterPanel" runat="server">
						<asp:Button id="Button_Add" runat="server" Width="110px" CssClass="button_common" Text="Add new Record"></asp:Button>
				<asp:DropDownList id="dd_sare" runat="server" Width="232px" AutoPostBack="True"></asp:DropDownList>
		</asp:panel>
			
			<asp:panel id="GridPanel" cssclass="GridPanel" runat="server">
				<C1WebGrid:C1WebGrid id="MyGrid" runat="server" Width="100%" AutoGenerateColumns="False">
					<Columns>
						<c1webgrid:C1BoundColumn DataField="cust_id" Visible="False"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="sare_id" Visible="False"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="cust_name"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="cusr_percent"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1ButtonColumn Visible="False" ButtonType="PushButton" Text="Button"></c1webgrid:C1ButtonColumn>
						<c1webgrid:C1ButtonColumn ButtonType="PushButton" Text="Delete" CommandName="Delete">
							<HeaderStyle Width="60px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</c1webgrid:C1ButtonColumn>
					</Columns>
				</C1WebGrid:C1WebGrid>
			</asp:panel>
			
			<asp:panel id="EditPanel" CssClass="FilterPanel" runat="server" Visible="False">
				<TABLE cellSpacing="2" cellPadding="2" width="100%" border="0">
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD>Sales Rep</TD>
						<TD>
							<asp:DropDownList id="dd_edit_sare" runat="server" Width="232px" CssClass="formfield"></asp:DropDownList></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD>Customer</TD>
						<TD>
							<asp:DropDownList id="dd_cust" runat="server" CssClass="formfield"></asp:DropDownList></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD>Percent:</TD>
						<TD class="formfield">
							<asp:TextBox id="txtpercent" runat="server" CssClass="formfield"></asp:TextBox></TD>
					</TR>
				</TABLE>
				<asp:Button id="Button_insert" runat="server" Width="110px" CssClass="button_common" Text="Insert"></asp:Button>
				<asp:Button id="Button_update" runat="server" Width="110px" CssClass="button_common" Text="Update"></asp:Button>
				<asp:Button id="Button_cancel" runat="server" Width="110px" CssClass="button_common" Text="Cancel"></asp:Button>
			</asp:panel><input type="hidden" id="inpID" runat="server" NAME="inpID"></form>
	</body>
</HTML>
