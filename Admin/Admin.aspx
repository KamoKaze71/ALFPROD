<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Admin.aspx.vb" Inherits="Wyeth.Alf.Admin"%>
<%@ Register TagPrefix="Menu" Namespace="Wyeth.Alf" Assembly="alf" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Admin</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<Menu:Menu runat="server" Category="Main Menu" id="Menu1" MenuIDParent="1" CssStyle="tableBgColor1Class" type="vertical"
				CssStyleHover="tableMouseoverColor" MenuWidth="70%" />
		</form>
	</body>
</HTML>
