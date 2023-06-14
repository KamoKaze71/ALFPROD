<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TestLiveDatenAbgleich.aspx.vb" Inherits="Wyeth.Alf.TestLiveDatenAbgleich"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TestLiveDatenAbgleich</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<asp:literal id="_clientScript" runat="server"></asp:literal>
		<SCRIPT LANGUAGE="JavaScript" SRC="../JS/ClientScripts.js"> </SCRIPT>
		<asp:literal id="Literal1" runat="server"></asp:literal>
	</HEAD>
	<body>
		<form id="Form1" title="" name="" method="post" runat="server">
			<div class="reportsButtonBar">
				<asp:Button id="btn_Import" runat="server" Visible="true" Text="Get Data from live Server" CssClass="button"></asp:Button>
				<asp:Button id="btnEditTables" runat="server" CssClass="button" Text="Edit List of tables" Visible="true"></asp:Button>
				<table width="95%">
				</table>
			</div>
			<asp:panel id="auto_panel" runat="server" width="100%">
				<BR>
				<asp:Label id="lblOut" Visible="True" Runat="server"></asp:Label>
			</asp:panel><br>
			<asp:textbox id="txtProgress" style="VISIBILITY: hidden" runat="server">none</asp:textbox>
			<asp:textbox id="iidx" style="VISIBILITY: hidden" runat="server">none</asp:textbox></form>
		<script language="javascript">
		
		if (window.document.forms[0].txtProgress.value != 'none')
		{

	 window.document.forms[0].submit();
		}
		
		</script>
	</body>
</HTML>
