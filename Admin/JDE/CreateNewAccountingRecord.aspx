<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CreateNewAccountingRecord.aspx.vb" Inherits="Wyeth.Alf.CreateNewAccountingRecord"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>CreateNewAccountingRecord</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Styles.css" type="text/css" rel="stylesheet">
		<asp:literal id="_clientScript" runat="server"></asp:literal>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table width="100%">
				<tr>
					<td style="WIDTH: 135px">Name:</td>
					<td><asp:textbox id="txtName" runat="server" CssClass="formfield"></asp:textbox></td>
				</tr>
				<tr>
					<td style="WIDTH: 135px">Description:</td>
					<td><asp:textbox id="txtDescription" runat="server" CssClass="formfield"></asp:textbox></td>
				<tr>
					<td colSpan="2">
						<div class="endline" align="right"><asp:button id="buttonSave" CssClass="button" Text="Save" Runat="server"></asp:button>&nbsp;<button class="button" onclick="window.close();" type="button">Cancel</button>
						</div>
					</td>
				</tr>
			</table>
			&nbsp;</form>
	</body>
</HTML>
