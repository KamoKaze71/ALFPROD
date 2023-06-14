<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Codes.aspx.vb" Inherits="Wyeth.Alf.Codes"%>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Codes</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../JS/ClientScripts.js"> </script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<DIV class="HL">
				<asp:Label id="lblPageTitle" runat="server"></asp:Label>
			</DIV>
			<div id="FilterPanel" runat="server" Class="reportsButtonBar" style="TEXT-ALIGN: left">
				<asp:Button id="Button_Add" runat="server" Text="Add new Record" CausesValidation="False" CssClass="button"></asp:Button></TD></TABLE>
			</div>
			<asp:panel id="EditPanel" runat="server" Visible="False" CssClass="EditPanel">
				<TABLE cellSpacing="2" cellPadding="2" width="100%" border="0">
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field" style="WIDTH: 97px">ID</TD>
						<TD>
							<box:WyethTextBox id="txtCodeID" runat="server" CssClass="formfield" Enabled="False"></box:WyethTextBox></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field" style="WIDTH: 97px">Code</TD>
						<TD>
							<box:WyethTextBox id="txtCodeCode" runat="server" CssClass="formfield"></box:WyethTextBox></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field" style="WIDTH: 97px">Description</TD>
						<TD>
							<asp:TextBox id="txtCodeDescription" runat="server" CssClass="formfield"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field" style="WIDTH: 97px">Category</TD>
						<TD>
							<asp:TextBox id="txtCodeCategory" runat="server" CssClass="formfield"></asp:TextBox></TD>
					</TR>
				</TABLE>
				<asp:Button id="Button_insert" runat="server" Text="Insert" CssClass="button" Width="110px"></asp:Button>
				<asp:Button id="Button_update" runat="server" Text="Update" CssClass="button" Width="110px"></asp:Button>
				<asp:Button id="Button_cancel" runat="server" Text="Cancel" CssClass="button" Width="110px"></asp:Button>
			</asp:panel>
			<asp:panel id="GridPanel" cssclass="GridPanel" runat="server">
				<C1WebGrid:C1WebGrid id="MyGrid" runat="server" AllowSorting="True" DefaultRowHeight="22px" DefaultColumnWidth="120px"
					width="100%" AutoGenerateColumns="False" AllowAutoSort="True">
					<Columns>
						<c1webgrid:C1BoundColumn Visible="False" SortExpression="code_id" DataField="code_id"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn SortExpression="code_code" HeaderText="Code" DataField="code_code"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn SortExpression="code_description" HeaderText="Description" DataField="code_description"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn SortExpression="code_category" HeaderText="Category" DataField="code_category"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1ButtonColumn Visible="False" ButtonType="PushButton" Text="Button"></c1webgrid:C1ButtonColumn>
						<c1webgrid:C1ButtonColumn CommandName="Delete" ButtonType="PushButton" HeaderText="Delete" Text="Delete">
							<HeaderStyle Width="60px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</c1webgrid:C1ButtonColumn>
					</Columns>
				</C1WebGrid:C1WebGrid>
			</asp:panel>
			<input type="hidden" id="inpID" runat="server" NAME="inpID"></form>
	</body>
</HTML>
