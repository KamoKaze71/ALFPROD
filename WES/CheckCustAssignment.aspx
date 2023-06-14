<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CheckCustAssignment.aspx.vb" Inherits="Wyeth.Alf.CheckCustAssignment"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TCOGSCheck</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<DIV CLASS="HL">
			<ASP:LABEL ID="lblPageTitle" RUNAT="server"></ASP:LABEL>
		</DIV>
		<DIV CLASS="reportsButtonBar">
			<TABLE>
				<TR>
					<TD NOWRAP><REP:REPORTDATA ID="repData" RUNAT="server" /></TD>
					<TD WIDTH="100%" ALIGN="center">
						<DIV CLASS="noprint">
							<BUTTON CLASS="button" TYPE="button" RUNAT="server" ID="BUTTON1">Export to Excel</BUTTON>
							&nbsp; <BUTTON CLASS="button" ONCLICK="javascript:window.print();" TYPE="button">Print</BUTTON>
						</DIV>
					</TD>
				</TR>
			</TABLE>
		</DIV>
		<form id="Form1" method="post" runat="server">
			<asp:DataGrid id="MyGrid" style="Z-INDEX: 101; LEFT: 224px; POSITION: absolute; TOP: 120px" runat="server"></asp:DataGrid>
		</form>
	</body>
</HTML>
