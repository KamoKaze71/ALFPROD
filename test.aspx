<%@ Register TagPrefix="dg" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="test.aspx.vb" Inherits="Wyeth.Alf.ddtest"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>test</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<LINK type="text/css" href="printing.css" rel="stylesheet" media="print">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<dg:wyethDataGrid ID="myGrid" Runat="server" />
			<br>
			<asp:Button Runat="server" ID="btn" Text="excel" />
		</form>
	</body>
</HTML>
