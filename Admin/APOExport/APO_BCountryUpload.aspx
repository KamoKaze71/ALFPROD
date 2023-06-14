<%@ Page Language="vb" AutoEventWireup="false" Codebehind="APO_BCountryUpload.aspx.vb" Inherits="Wyeth.Alf.APO_BCountryUpload"%>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>APO_BCountryUpload</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Styles.css" type="text/css" rel="stylesheet">
		<LINK media="print" href="../../printing.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../../JS/ClientScripts.js"></script>
		<asp:literal id="_clientScript" runat="server"></asp:literal>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE class="reportsFilter" cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td><INPUT class="formfield" id="MyFile" type="file" name="MyFile" runat="server"></td>
				</tr>
				<tr>
					<td><asp:Button id="btn_upload" Runat="server" Text="upload file" CssClass="button"></asp:Button></td>
				</tr>
			</TABLE>
			<table height="100%" width="100%">
				<tr height="100%" valign="top">
					<td width="150" class="tableBgColor1Class"><asp:CheckBoxList id="chkReports" runat="server"></asp:CheckBoxList></td>
					<td><asp:Label ID="lblOut" Runat="server"></asp:Label></td>
				</tr>
			</table>
			<asp:textbox id="txtProgress" Runat="server" style="VISIBILITY: hidden">none</asp:textbox>
			<script language="javascript">
		
		if (window.document.forms[0].txtProgress.value != 'none')
		{

	 window.document.forms[0].submit();
		}
		
			</script>
		</form>
	</body>
</HTML>
