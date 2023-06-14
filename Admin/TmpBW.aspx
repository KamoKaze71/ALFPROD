<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TmpBW.aspx.vb" Inherits="Wyeth.Alf.TmpBW"%>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid, Version=1.1.20034.32, Culture=neutral, PublicKeyToken=589f1fc067ff4031" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TmpBW</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<c1webgrid:c1webgrid id="MyGrid" runat="server" EnableViewState="False" autoGenerateColumns="False" width="100%">
				<Columns>
					<c1webgrid:C1BoundColum  HeaderText="Code"></c1webgrid:C1BoundColumn>
				</Columns>
			</c1webgrid:c1webgrid>
		</form>
	</body>
</HTML>
