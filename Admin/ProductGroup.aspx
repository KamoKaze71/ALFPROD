<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ProductGroup.aspx.vb" Inherits="Wyeth.Alf.ProductGroup"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ProductGroup</title>
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
			<asp:panel id="FilterPanel" runat="server" cssclass="FilterPanel">
				<DIV class="reportsButtonBar">
					<TABLE width="100%">
						<TR align="left">
							<TD>
								<asp:Button id="Button_Add" runat="server" Width="110px" Text="Add new Record" CssClass="button"
									Visible="False"></asp:Button></TD>
						</TR>
					</TABLE>
				</DIV>
			</asp:panel><asp:panel id="GridPanel" runat="server" Height="100%" Width="100%" cssclass="GridPanel">
				<C1WebGrid:C1WebGrid id="MyGrid" runat="server" AutoGenerateColumns="False" width="100%">
					<Columns>
						<c1webgrid:C1BoundColumn Visible="False" DataField="prgr_id"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prgr_code" HeaderText="Code"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prgr_description" HeaderText="Description"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1ButtonColumn Visible="False" Text="Button"></c1webgrid:C1ButtonColumn>
						<c1webgrid:C1ButtonColumn Visible="False" ButtonType="PushButton" HeaderText="Delete" Text="Delete" CommandName="Delete">
							<HeaderStyle Width="60px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</c1webgrid:C1ButtonColumn>
					</Columns>
				</C1WebGrid:C1WebGrid>
			</asp:panel><asp:panel id="EditPanel" runat="server" CssClass="EditPanel" Visible="False">
				<TABLE cellSpacing="0" cellPadding="2" width="100%" border="0">
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field">&nbsp;ID</TD>
						<TD><IMG height="1" alt="" src="images/trans.gif" width="10" border="0"></TD>
						<TD width="100%">
							<asp:TextBox id="txtPrGrId" runat="server" CssClass="formfield" Enabled="False"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field">Code</TD>
						<TD><IMG height="1" alt="" src="images/trans.gif" width="10" border="0"></TD>
						<TD width="100%">
							<asp:TextBox id="txtPrGrCode" runat="server" CssClass="formfield" Enabled="False"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field">Description</TD>
						<TD><IMG height="1" alt="" src="images/trans.gif" width="10" border="0"></TD>
						<TD width="100%">
							<asp:TextBox id="txtPrGrDescription" runat="server" CssClass="formfield" Enabled="False"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD align="right" colSpan="3">
							<asp:Button id="Button_Insert" runat="server" Text="Insert" CssClass="Button" Enabled="False"></asp:Button>
							<asp:Button id="Button_update" runat="server" Text="Update" CssClass="Button" Enabled="False"></asp:Button>
							<asp:Button id="Button_cancel" runat="server" Text="Cancel" CssClass="Button"></asp:Button></TD>
					</TR>
				</TABLE>
			</asp:panel><input id="inpID" type="hidden" name="inpID" runat="server">
		</form>
	</body>
</HTML>
