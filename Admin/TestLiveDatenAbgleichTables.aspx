<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TestLiveDatenAbgleichTables.aspx.vb" Inherits="Wyeth.Alf.TestLiveDatenAbgleichTables"%>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TestLiveDatenAbgleichTables</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<asp:literal id="_clientScript" runat="server"></asp:literal>
		<SCRIPT LANGUAGE="JavaScript" SRC="../JS/ClientScripts.js"> </SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
			<form id="Form1" method="post" runat="server">
		
			<asp:Button id="btn_save" runat="server" Visible="true" Text="Save" CssClass="button"></asp:Button>
			<C1WebGrid:C1WebGrid id="MyGrid" runat="server" DefaultColumnWidth="120px" DefaultRowHeight="22px" AutoGenerateColumns="False">
				<Columns>
					<c1webgrid:C1BoundColumn HeaderText="Number" DataField="IMCT_NUMBER"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Table Name" DataField="IMCT_NAME"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Type" DataField="IMCT_TYPE"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Status" DataField="IMCT_STATUS"></c1webgrid:C1BoundColumn>
				</Columns>
			</C1WebGrid:C1WebGrid>
		</form>
	</body>
</HTML>
