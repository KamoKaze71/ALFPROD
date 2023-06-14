<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Country.aspx.vb" Inherits="Wyeth.Alf.Country"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Country</title>
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
			<asp:panel id="FilterPanel" cssclass="FilterPanel" runat="server">
				<asp:Button id="Button_Add" runat="server" Width="110px" CssClass="button" Text="Add new Record"
					Visible="False"></asp:Button>
			</asp:panel>
			<asp:panel id="GridPanel" cssclass="GridPanel" runat="server">
				<P></P>
				<C1WebGrid:C1WebGrid id="MyGrid" runat="server" Width="100%" DefaultRowHeight="22px" DefaultColumnWidth="120px"
					AutoGenerateColumns="False">
					<Columns>
						<c1webgrid:C1BoundColumn Visible="False" DataField="ctry_id"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Code" DataField="ctry_code"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Description" DataField="ctry_description"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Visible="False" HeaderText="Currency" DataField="curr_id"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Currency" DataField="curr_code"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="PL" DataField="CTRY_PL_COUNTRY"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="BS" DataField="CTRY_BS_COUNTRY"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1ButtonColumn Visible="False" Text="Button"></c1webgrid:C1ButtonColumn>
						<c1webgrid:C1ButtonColumn CommandName="Delete" ButtonType="PushButton" HeaderText="Delete" Text="Delete">
							<HeaderStyle Width="60px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</c1webgrid:C1ButtonColumn>
					</Columns>
				</C1WebGrid:C1WebGrid>
			</asp:panel><asp:panel id="EditPanel" runat="server" BorderWidth="1px" BorderStyle="Solid" BorderColor="Black"
				BackColor="GhostWhite" Visible="False" Width="90%">
				<TABLE cellSpacing="2" cellPadding="2" width="100%" border="0">
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field" style="WIDTH: 94px">Country ID</TD>
						<TD>
							<asp:TextBox id="txtCtry_id" runat="server" CssClass="formfield" Enabled="False"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field" style="WIDTH: 94px">Description</TD>
						<TD>
							<asp:TextBox id="txtCtry_description" runat="server" CssClass="formfield" Enabled="False"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field" style="WIDTH: 94px">Currency</TD>
						<TD>
							<asp:DropDownList id="ddCurrencyId" runat="server" CssClass="formfield"></asp:DropDownList></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field" style="WIDTH: 94px">Code</TD>
						<TD>
							<asp:TextBox id="txtCtry_code" runat="server" CssClass="formfield" Enabled="False"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field" style="WIDTH: 94px">PL COUNTRY
						</TD>
						<TD>
							<asp:TextBox id="txtplCountry" runat="server" CssClass="formfield"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field" style="WIDTH: 94px">BS COUNTRY
						</TD>
						<TD>
							<asp:TextBox id="txtbsCountry" runat="server" CssClass="formfield"></asp:TextBox></TD>
					</TR>
				</TABLE>
				<asp:Button id="Button_insert" runat="server" Width="110px" CssClass="button" Text="Insert"></asp:Button>
				<asp:Button id="Button_update" runat="server" Width="110px" CssClass="button" Text="Update"></asp:Button>
				<asp:Button id="button_cancel" runat="server" Width="110px" CssClass="button" Text="Cancel"></asp:Button>
			</asp:panel><input type="hidden" id="inpID" runat="server" NAME="inpID"></form>
	</body>
</HTML>
