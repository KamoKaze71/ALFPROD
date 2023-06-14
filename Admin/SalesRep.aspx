<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SalesRep.aspx.vb" Inherits="Wyeth.Alf.SalesRep"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>SalesRep</title>
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
			<div class="hl"><asp:label id="lblPageTitle" runat="server" Width="100%"></asp:label></div>
			<DIV class="reportsButtonBar" id="FilterPanel" runat="server">
				<TABLE width="97%">
					<TR align="left">
						<TD><asp:button id="Button_Add" runat="server" Width="110px" CssClass="button" Text="Add new Record"></asp:button></TD>
					</TR>
				</TABLE>
			</DIV>
			<asp:panel id="EditPanel" runat="server" Width="90%" Visible="False" BackColor="GhostWhite"
				BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">
				<TABLE cellSpacing="2" cellPadding="2" width="100%" border="0">
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field" style="WIDTH: 82px">First Name</TD>
						<TD>
							<asp:TextBox id="txtsarefirstname" runat="server" cssclass="formfield"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field" style="WIDTH: 82px">Last Name</TD>
						<TD>
							<asp:TextBox id="txtsarelastname" runat="server" cssclass="formfield"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field" style="WIDTH: 82px">Short Name</TD>
						<TD>
							<asp:TextBox id="txtShortName" runat="server" cssclass="formfield"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field" style="WIDTH: 82px">Intranet User
						</TD>
						<TD>
							<asp:DropDownList id="ddIntranetUser" runat="server" cssclass="formfield"></asp:DropDownList>
							<asp:TextBox id="txtIntraUser" runat="server" Visible="False" cssclass="formfield"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field" style="WIDTH: 82px">Type</TD>
						<TD>
							<asp:DropDownList id="ddsarecodetype_id" runat="server" cssclass="formfield"></asp:DropDownList></TD>
					</TR>
				</TABLE>
				<asp:Button id="Button_Insert" runat="server" Width="110px" Text="Insert" CssClass="button"></asp:Button>
				<asp:Button id="Button_Update" runat="server" Width="110px" Text="Update" CssClass="button"></asp:Button>
				<asp:Button id="Button_Cancel" runat="server" Width="110px" Text="Cancel" CssClass="button"></asp:Button>
			</asp:panel><asp:panel id="GridPanel" runat="server" cssclass="GridPanel">
				<C1WebGrid:C1WebGrid id="MyGrid" runat="server" DefaultRowHeight="22px" DefaultColumnWidth="120px" width="100%"
					AutoGenerateColumns="False" EnableViewState="False">
					<Columns>
						<c1webgrid:C1BoundColumn Visible="False" DataField="sare_id"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="First Name" DataField="sare_first_name"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Last Name" DataField="sare_last_name"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Short Name" DataField="sare_short_name"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Visible="False" DataField="sare_number"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Visible="False" DataField="code_id_sales_rep_type"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Type" DataField="code_description"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1ButtonColumn Visible="False" Text="Button"></c1webgrid:C1ButtonColumn>
						<c1webgrid:C1ButtonColumn CommandName="Delete" ButtonType="PushButton" HeaderText="Delete" Text="Delete"></c1webgrid:C1ButtonColumn>
					</Columns>
				</C1WebGrid:C1WebGrid>
			</asp:panel><input id="inpID" type="hidden" name="inpID" runat="server">
			<asp:textbox id="txtsare_id" style="Z-INDEX: 101; LEFT: 168px; POSITION: absolute; TOP: 304px"
				runat="server" Visible="False" cssclass="formfield"></asp:textbox></form>
	</body>
</HTML>
