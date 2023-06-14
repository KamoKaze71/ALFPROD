<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Distributor.aspx.vb" Inherits="Wyeth.Alf.Distributor"%>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Distributor</title>
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
			<div class="reportsButtonBar" runat="server" id="FilterPanel"><table width="100%">
					<tr align="left">
						<td>
							<asp:Button id="Button_add" runat="server" CssClass="button" Text="Add new Record" Width="110px"></asp:Button>
						</td>
					</tr>
				</table>
			</div>
			<asp:panel id="GridPanel" runat="server" cssclass="GridPanel">
				<C1WebGrid:C1WebGrid id="MyGrid" runat="server" Width="100%" AutoGenerateColumns="False" DefaultColumnWidth="120px"
					DefaultRowHeight="22px">
					<Columns>
						<c1webgrid:C1BoundColumn DataField="dist_id" Visible="False"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="dist_number" Visible="False" HeaderText="No."></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="dist_name" HeaderText="Name"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="dist_description" HeaderText="Description"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1ButtonColumn Visible="False" ButtonType="PushButton" Text="Button"></c1webgrid:C1ButtonColumn>
						<c1webgrid:C1ButtonColumn ButtonType="PushButton" HeaderText="Delete" Text="Delete" CommandName="Delete">
							<HeaderStyle Width="60px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</c1webgrid:C1ButtonColumn>
					</Columns>
				</C1WebGrid:C1WebGrid>
			</asp:panel><asp:panel id="EditPanel" runat="server" CssClass="EditPanel" Visible="False">
				<TABLE cellSpacing="2" cellPadding="2" width="100%" border="0">
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field" style="WIDTH: 92px">ID</TD>
						<TD>
							<asp:TextBox id="txtxDistID" runat="server" Enabled="False" cssClass="formfield"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field" style="WIDTH: 92px">Number</TD>
						<TD>
							<asp:TextBox id="txtDistNo" runat="server" cssClass="formfield"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field" style="WIDTH: 92px">Name</TD>
						<TD>
							<asp:TextBox id="txtDistName" runat="server" cssClass="formfield"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field" style="WIDTH: 92px">Description</TD>
						<TD>
							<asp:TextBox id="txtDistDescription" runat="server" cssClass="formfield"></asp:TextBox></TD>
					</TR>
				</TABLE>
				<asp:Button id="Button_Insert" runat="server" Width="110px" Text="Insert" CssClass="button"></asp:Button>
				<asp:Button id="Button_update" runat="server" Width="110px" Text="Update" CssClass="button"></asp:Button>
				<asp:Button id="Button_Cancel" runat="server" Width="110px" Text="Cancel" CssClass="button"></asp:Button>
			</asp:panel><input id="inpID" type="hidden" name="inpID" runat="server"></form>
	</body>
</HTML>
