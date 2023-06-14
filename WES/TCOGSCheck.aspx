<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TCOGSCheck.aspx.vb" Inherits="Wyeth.Alf.TCOGSCheck" debug="True"%>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../Util/printReportCtl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TCOGSCheck</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<asp:literal id="_clientScript" runat="server"></asp:literal>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<DIV class="reportsButtonBar">
				<TABLE width=100%>
					<TR>
						<TD noWrap><REP:REPORTDATA id="repData" RUNAT="server"></REP:REPORTDATA></TD>
						<TD align="center" width="100%">
							<DIV class="noprint"><BUTTON class="button" id="BUTTON1" type="button" RUNAT="server">Export 
									to Excel</BUTTON> &nbsp;
								<prt:printreportctl id="prtControl" runat="server"></prt:printreportctl></DIV>
						</TD>
					</TR>
				</TABLE>
			</DIV>
			<ASP:PANEL id="reportpanel" RUNAT="server" CSSCLASS="reportpanel">
				<TABLE>
					<TR>
						<TD align=left height=40px>
							<asp:Label id="lblTcogs" Runat="server" height=100%></asp:Label></TD>
					</TR>
					<TR>
						<TD>
							<C1WebGrid:C1WebGrid id="MyGrid" runat="server" AUTOGENERATECOLUMNS="False" DEFAULTROWHEIGHT="22px" DEFAULTCOLUMNWIDTH="120px"
								WIDTH="152px" EnableViewState="False">
								<Columns>
									<c1webgrid:C1BoundColumn DataField="prod_id" Visible="False"></c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="prod_phznr"></c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="prod_presentation"></c1webgrid:C1BoundColumn>
									<c1webgrid:C1ButtonColumn Visible="False" Text="Button"></c1webgrid:C1ButtonColumn>
								</Columns>
							</C1WebGrid:C1WebGrid></TD>
					</TR>
				</TABLE>
			</ASP:PANEL></form>
		<P></P>
	</body>
</HTML>
