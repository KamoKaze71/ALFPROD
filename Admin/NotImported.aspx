<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid, Version=1.1.20034.32, Culture=neutral, PublicKeyToken=589f1fc067ff4031" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="NotImported.aspx.vb" Inherits="Wyeth.Alf.NotImportedKD"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>NotImportedKD</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<asp:Label id="lblPageTitle" runat="server" Width="100%" CssClass="lblPageTitle"></asp:Label>
		<c1webgrid:C1WebGrid id="MyGridART" style="Z-INDEX: 108; LEFT: 16px; POSITION: absolute; TOP: 256px"
			runat="server" Height="88px" Width="624px"></c1webgrid:C1WebGrid>
		<c1webgrid:C1WebGrid id="MyGridBW" style="Z-INDEX: 109; LEFT: 16px; POSITION: absolute; TOP: 392px" runat="server"
			Width="624px"></c1webgrid:C1WebGrid>
		<form id="Form1" method="post" runat="server">
			<C1WEBGRID:C1WEBGRID id="MyGridKD" style="Z-INDEX: 110; LEFT: 16px; POSITION: absolute; TOP: 120px" runat="server"
				Height="72px" Width="625px"></C1WEBGRID:C1WEBGRID>
		</form>
	</body>
</HTML>
