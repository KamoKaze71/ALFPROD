<%@ Register TagPrefix="Menu" Namespace="Wyeth.Alf" Assembly="alf" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SysAdmin.aspx.vb" Inherits="Wyeth.Alf.SysAdmin"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>SysAdmin</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<Menu:Menu runat="server" Category="Main Menu" id="Menu1" MenuIDParent="17" CssStyle="tableBgColor1Class"
				type="vertical" MenuWidth="70%" CssStyleHover="tableMouseoverColor" />
		</form>
	</body>
</HTML>
