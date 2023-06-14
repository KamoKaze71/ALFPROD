<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="NotImported.aspx.vb" Inherits="Wyeth.Alf.NotImportedKD"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title></title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Styles.css" type="text/css" rel="stylesheet">
		<asp:literal id="_clientScript" runat="server"></asp:literal>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div id="FilterPanel" runat="server" Class="reportsButtonBar" style="TEXT-ALIGN: left">
				<asp:Button id="Button_delete" runat="server" Text="Delete all Records in Table" CausesValidation="False"
					CssClass="button"></asp:Button><input style="LEFT: 4mm; POSITION: relative" type="button" class="button" runat="server"
					id="btn_cancel" value="Cancel"></input></TABLE>
			</div>
			<c1webgrid:C1WebGrid id="MYgRID" runat="server" EnableViewState="False" visible="False" DefaultColumnWidth="120px"
				DefaultRowHeight="22px" Width="100%"></c1webgrid:C1WebGrid>
		</form>
	</body>
</HTML>
