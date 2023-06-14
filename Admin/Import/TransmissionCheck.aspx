<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TransmissionCheck.aspx.vb" Inherits="Wyeth.Alf.TransmissionCheck"%>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TransmissionCheck</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			
		</form>
		<script language="javascript" src="../../js/additionalHeaderRows.js"></script>
		<script language="javascript">
			<!--
				//hinzufügen von doppelten headlines. aufgrund von bugs im c1.webgrid
				//muss man es mittels javascript lösen. 
			
				createRow("myGrid");
				addTableCell("myGrid", "LIVE SERVER", "5", "", 0);
				addTableCell("myGrid", "TEST SERVER", "5", "", 1);
				
			//-->
		</script>
	</body>
	
</HTML>
