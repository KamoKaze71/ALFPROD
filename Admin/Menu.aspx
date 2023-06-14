<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Menu.aspx.vb" Inherits="Wyeth.Alf.Menu1"%>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Menu</title>
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
					<tr align="left">
						<td><asp:button id="Button_Add" runat="server" Width="110px" Text="Add new Record" CssClass="button"></asp:button></td>
					</tr>
				</table>
			</div>
			<asp:panel id="GridPanel" runat="server" CssClass="GridPanel">
				<C1WebGrid:C1WebGrid id="MyGrid" runat="server" AutoGenerateColumns="False" width="100%" DefaultColumnWidth="120px"
					DefaultRowHeight="22px">
					<Columns>
						<c1webgrid:C1BoundColumn HeaderText="ID" DataField="menu_id"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Name" DataField="menu_name"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Label" DataField="menu_label"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Link" DataField="menu_link"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Target Frame" DataField="menu_target"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Display No." DataField="menu_display_number"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Category" DataField="menu_category"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="menu_id_parent"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1ButtonColumn CommandName="Select" Text="Select" visible=false></c1webgrid:C1ButtonColumn>
						<c1webgrid:C1ButtonColumn CommandName="Delete" ButtonType="PushButton" Text="Delete"></c1webgrid:C1ButtonColumn>
						<c1webgrid:C1BoundColumn DataField="menu_access_id"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Visible="False" DataField="menu_display"></c1webgrid:C1BoundColumn>
					</Columns>
				</C1WebGrid:C1WebGrid>
			</asp:panel><asp:panel id="EditPanel" runat="server" CssClass="EditPanel" Visible="False">
<TABLE cellSpacing="2" cellPadding="2" width="100%" border="0">
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field" width="150">Menu ID</TD>
						<TD>
							<asp:TextBox id="txtMenuID" runat="server" Enabled="False" cssClass="formfield"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field">Page Title</TD>
						<TD>
							<asp:TextBox id="txtMenuName" runat="server" cssClass="formfield"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field">Label</TD>
						<TD>
							<asp:TextBox id="txtMenuLabel" runat="server" cssClass="formfield"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field">Link</TD>
						<TD>
							<asp:TextBox id="txtMenuLink" runat="server" cssClass="formfield"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field">Target</TD>
						<TD>
							<asp:TextBox id="txtMenuTarget" runat="server" cssClass="formfield"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field">Display No.</TD>
						<TD>
							<asp:TextBox id="txtMenuDisplayNo" runat="server" Width="328px" cssClass="formfield"></asp:TextBox>
							<asp:CheckBox id="chk_display" runat="server" Text="Display in Menu" Checked="True"></asp:CheckBox></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field">Category</TD>
						<TD>
							<asp:TextBox id="txtMenuCategory" runat="server" cssClass="formfield"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD class="field">Module Section</TD>
						<TD>
							<asp:DropDownList id="ddMenuModulID" runat="server" cssclass="formfield"></asp:DropDownList></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field">Parent ID</TD>
						<TD>
							<asp:DropDownList id="ddMenuIDParent" runat="server" cssclass="formfield"></asp:DropDownList></TD>
					</TR>
				</TABLE>
<asp:Button id="Button_Update" runat="server" Width="110px" Text="Update" cssClass="button"></asp:Button>
<asp:Button id="Button_Insert" runat="server" Width="110px" Text="Insert" cssClass="button"></asp:Button>
<asp:Button id="Button_Cancel" runat="server" Width="110px" Text="Cancel" cssClass="button"></asp:Button></TD></asp:panel><input id="inpID" type="hidden" name="inpID" runat="server"></form>
	</body>
</HTML>
