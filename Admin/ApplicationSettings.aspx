<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ApplicationSettings.aspx.vb" Inherits="Wyeth.Alf.ApplicationSettings"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ApplicationSettings</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../JS/ClientScripts.js"> </script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div class="hl"><asp:label id="lblPageTitle" runat="server" Width="100%"></asp:label></div>
			<div class="reportsButtonBar" id="FilterPanel" runat="server">
				<table width="100%">
					<tr left>
						<td>
							<asp:Button id="Button_add" runat="server" CssClass="Button" Width="110px" Text="Add new Record"></asp:Button></td>
					</tr>
				</table>
			</div>
			<asp:panel id="EditPanel" runat="server" Visible="False" CssClass="EditPanel">
				<TABLE>
					<TR>
						<TD class="field" style="WIDTH: 123px">ID</TD>
						<TD style="WIDTH: 712px">
							<asp:TextBox id="txtapseID" runat="server" Width="665" cssClass="formfield" Enabled="False"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field" style="WIDTH: 123px">Variable Name</TD>
						<TD style="WIDTH: 712px">
							<asp:TextBox id="txtApseVariable" runat="server" Width="665px" cssClass="formfield"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field" style="WIDTH: 123px">Variable Value</TD>
						<TD style="WIDTH: 712px">
							<asp:TextBox id="txtapseValue" runat="server" Width="665" cssClass="formfield"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field" style="WIDTH: 123px">Description</TD>
						<TD style="WIDTH: 712px">
							<asp:TextBox id="txtApSeDescription" runat="server" Width="665" cssClass="formfield" Height="64px"></asp:TextBox></TD>
					</TR>
				</TABLE>
				<asp:Button id="Button_Insert" runat="server" Width="110px" Text="Insert" CssClass="Button"></asp:Button>
				<asp:Button id="Button_Update" runat="server" Width="110px" Text="Update" CssClass="Button"></asp:Button>
				<asp:Button id="Button_Cancel" runat="server" Width="110px" Text="Cancel" CssClass="Button"></asp:Button>
			</asp:panel>
			<asp:panel id="GridPanel" runat="server" cssclass="GridPanel">
				<C1WebGrid:C1WebGrid id="MyGrid" runat="server" Width="100%" AutoGenerateColumns="False">
					<Columns>
						<c1webgrid:C1BoundColumn Visible="False" DataField="apse_id"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="apse_variable" HeaderText="Variable"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="apse_value" HeaderText="Value"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="apse_description" HeaderText="Description"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1ButtonColumn Visible="False" Text="Button"></c1webgrid:C1ButtonColumn>
						<c1webgrid:C1ButtonColumn ButtonType="PushButton" HeaderText="Delete" Text="Delete" CommandName="Delete">
							<HeaderStyle Width="60px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</c1webgrid:C1ButtonColumn>
					</Columns>
				</C1WebGrid:C1WebGrid>
			</asp:panel><input id="inpID" type="hidden" runat="server">
		</form>
	</body>
</HTML>
