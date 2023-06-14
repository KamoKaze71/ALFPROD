<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Import.aspx.vb" Inherits="Wyeth.Alf.import"%>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid, Version=1.1.20034.32, Culture=neutral, PublicKeyToken=589f1fc067ff4031" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>import</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:panel id="Panel1" style="Z-INDEX: 111; LEFT: 224px; POSITION: absolute; TOP: 104px" runat="server"
				Width="376px" Height="128px">
				<asp:label id="LabelOUT" runat="server" Width="376px" Height="256px"></asp:label>
			</asp:panel><asp:button id="Button_Import_Sanova" style="Z-INDEX: 101; LEFT: 16px; POSITION: absolute; TOP: 40px"
				runat="server" Width="160px" Text="Import Sanova Data"></asp:button><asp:dropdownlist id="ddTransmissions" style="Z-INDEX: 104; LEFT: 184px; POSITION: absolute; TOP: 440px"
				runat="server" Width="400px"></asp:dropdownlist><asp:dropdownlist id="ddimportDay" style="Z-INDEX: 102; LEFT: 184px; POSITION: absolute; TOP: 400px"
				runat="server" Width="400px" AutoPostBack="True"></asp:dropdownlist><asp:button id="Button2" style="Z-INDEX: 103; LEFT: 0px; POSITION: absolute; TOP: 440px" runat="server"
				Width="161px" Text="Delete Transmission"></asp:button><asp:dropdownlist id="dddistribSelect" style="Z-INDEX: 105; LEFT: 16px; POSITION: absolute; TOP: 8px"
				runat="server" Width="209px"></asp:dropdownlist><asp:textbox id="txtPath" style="Z-INDEX: 106; LEFT: 200px; POSITION: absolute; TOP: 40px" runat="server"
				AutoPostBack="True"></asp:textbox><asp:radiobuttonlist id="rbl_AutomaticUpdate" style="Z-INDEX: 107; LEFT: 368px; POSITION: absolute; TOP: 24px"
				runat="server" Width="192px" Height="72px" AutoPostBack="True">
				<asp:ListItem Value="Manual">Manual Update</asp:ListItem>
				<asp:ListItem Value="automatic" Selected="True">Automatic Update</asp:ListItem>
			</asp:radiobuttonlist><asp:button id="Button_KD" style="Z-INDEX: 108; LEFT: 56px; POSITION: absolute; TOP: 152px"
				runat="server" Width="144px" Text="Import KD" Enabled="False"></asp:button><asp:button id="Button_ART" style="Z-INDEX: 109; LEFT: 56px; POSITION: absolute; TOP: 176px"
				runat="server" Width="144px" Text="Import ART" Enabled="False"></asp:button><asp:button id="Button_BW" style="Z-INDEX: 110; LEFT: 56px; POSITION: absolute; TOP: 200px"
				runat="server" Width="144px" Text="Import BW" Enabled="False"></asp:button>
			<asp:Label id="Label1" style="Z-INDEX: 112; LEFT: 8px; POSITION: absolute; TOP: 400px" runat="server"
				Width="136px" Height="24px">View Sql Loader Logs</asp:Label></form>
	</body>
</HTML>
