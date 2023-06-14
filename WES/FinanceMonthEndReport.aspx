<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FinanceMonthEndReport.aspx.vb" Inherits="Wyeth.Alf.FinanceMonthEndReport"%>
<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../Util/printReportCtl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FinanceMonthEndReport</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="JavaScript" src="../JS/ClientScripts.js"> </SCRIPT>
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<DIV class="HL"><ASP:LABEL id="lblPageTitle" WIDTH="100%" RUNAT="server"></ASP:LABEL></DIV>
			<ASP:PANEL id="FilterPanel" RUNAT="server" CSSCLASS="FilterPanel">
				<DIV class="noprint">
					<TABLE class="reportsFilter" cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD>
								<ASP:DROPDOWNLIST id="ddmonth" RUNAT="server" WIDTH="176px"></ASP:DROPDOWNLIST></TD>
							<TD>
								<ASP:DROPDOWNLIST id="ddlineselect" RUNAT="server"></ASP:DROPDOWNLIST></TD>
							<TD width="100%">
								<ASP:DROPDOWNLIST id="ddDistribSelect" RUNAT="server"></ASP:DROPDOWNLIST></TD>
							<TD width="100%"></TD>
						</TR>
					</TABLE>
				</DIV>
				<DIV class="reportsButtonBar">
					<TABLE>
						<TR>
							<TD noWrap>
								<REP:REPORTDATA id="repData" RUNAT="server"></REP:REPORTDATA></TD>
							<TD align="center" width="100%">
								<DIV class="noprint">
									<ASP:BUTTON id="btnGenRep" RUNAT="server" CSSCLASS="button" TEXT="Generate Report"></ASP:BUTTON>&nbsp;&nbsp;
									<ASP:BUTTON id="ExportExcel" RUNAT="server" CSSCLASS="button" TEXT="Export to Excel" VISIBLE="true"></ASP:BUTTON>&nbsp;&nbsp;
									<prt:printreportctl id="prtControl" runat="server"></prt:printreportctl></DIV>
								<DIV></DIV>
							</TD>
						</TR>
					</TABLE>
				</DIV>
			</ASP:PANEL>
			<asp:Button id="btn_lvl1" Text="Level1" CssClass="button_common" runat="server"></asp:Button>
			<asp:Button id="btn_lvl2" Text="Level2" CssClass="button_common" runat="server"></asp:Button>
			<asp:Button id="btn_lvl3" text="Level3" CssClass="button_common" runat="server"></asp:Button>
			<asp:Button id="btn_lvl4" runat="server" CssClass="button_common" text="Level4"></asp:Button>
			<ASP:PANEL id="GridPanel" RUNAT="server" CSSCLASS="GridPanel">
				<C1WEBGRID:C1WEBGRID id="MyGrid" RUNAT="server" WIDTH="100%" ENABLEVIEWSTATE="False" SHOWFOOTER="True"
					AUTOGENERATECOLUMNS="False" visible="True">
					<COLUMNS>
						<C1WEBGRID:C1BOUNDCOLUMN VISIBLE="False" DATAFIELD="code_id"></C1WEBGRID:C1BOUNDCOLUMN>
						<C1WEBGRID:C1BOUNDCOLUMN VISIBLE="False" DATAFIELD="prod_cc_id"></C1WEBGRID:C1BOUNDCOLUMN>
						<C1WEBGRID:C1BOUNDCOLUMN VISIBLE="False" DATAFIELD="BEWEGKZ">
							<GROUPINFO OUTLINEMODE="StartCollapsed" HEADERTEXT="TOTAL {0}" POSITION="Header"></GROUPINFO>
						</C1WEBGRID:C1BOUNDCOLUMN>
						<C1WEBGRID:C1BOUNDCOLUMN VISIBLE="False" DATAFIELD="prgr_description">
							<GROUPINFO HEADERTEXT="TOTAL {0}" POSITION="Header"></GROUPINFO>
						</C1WEBGRID:C1BOUNDCOLUMN>
						<c1webgrid:C1BoundColumn DataField="CC" Visible="False">
							<GROUPINFO POSITION="Header" HEADERTEXT="TOTAL {0}"></GROUPINFO>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="PROD_PHZNR"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="PROD_PRESENTATION"></c1webgrid:C1BoundColumn>
						<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="units" HEADERTEXT="Units" AGGREGATE="Sum">
							<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
							<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
						</C1WEBGRID:C1BOUNDCOLUMN>
						<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="total_cogs" HEADERTEXT="Total Cogs" AGGREGATE="Sum">
							<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
							<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
						</C1WEBGRID:C1BOUNDCOLUMN>
						<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="value" HEADERTEXT="Total Value" AGGREGATE="Sum">
							<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
							<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
						</C1WEBGRID:C1BOUNDCOLUMN>
						<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="comm" HEADERTEXT="Comment">
							<ITEMSTYLE HORIZONTALALIGN="left"></ITEMSTYLE>
							<FOOTERSTYLE HORIZONTALALIGN="left"></FOOTERSTYLE>
						</C1WEBGRID:C1BOUNDCOLUMN>
					</COLUMNS>
				</C1WEBGRID:C1WEBGRID>
				<P></P>
				<C1WEBGRID:C1WEBGRID id="MyGridInvoices" RUNAT="server" SHOWFOOTER="True" AUTOGENERATECOLUMNS="False"
					DEFAULTCOLUMNWIDTH="120px" DEFAULTROWHEIGHT="22px" Visible="False">
					<COLUMNS>
						<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="TypeNumber" VISIBLE="False">
							<GROUPINFO OUTLINEMODE="StartCollapsed"></GROUPINFO>
						</C1WEBGRID:C1BOUNDCOLUMN>
						<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="type" VISIBLE="False">
							<GROUPINFO POSITION="Header" HEADERTEXT="Total {0}" OUTLINEMODE="StartCollapsed"></GROUPINFO>
						</C1WEBGRID:C1BOUNDCOLUMN>
						<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="cc" VISIBLE="False">
							<GROUPINFO POSITION="Header" HEADERTEXT="Total {0}" OUTLINEMODE="StartCollapsed"></GROUPINFO>
						</C1WEBGRID:C1BOUNDCOLUMN>
						<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="code_code"></C1WEBGRID:C1BOUNDCOLUMN>
						<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="prod_presentation"></C1WEBGRID:C1BOUNDCOLUMN>
						<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="stoc_date_open" HEADERTEXT="Date open">
							<ITEMSTYLE WRAP="False"></ITEMSTYLE>
						</C1WEBGRID:C1BOUNDCOLUMN>
						<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="stoc_date_accrued" HEADERTEXT="Date accrued">
							<ITEMSTYLE WRAP="False"></ITEMSTYLE>
						</C1WEBGRID:C1BOUNDCOLUMN>
						<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="stoc_date_correct" HEADERTEXT="Date correct">
							<ITEMSTYLE WRAP="False"></ITEMSTYLE>
						</C1WEBGRID:C1BOUNDCOLUMN>
						<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="stoc_order_number" HEADERTEXT="Order No.">
							<ITEMSTYLE WRAP="False"></ITEMSTYLE>
						</C1WEBGRID:C1BOUNDCOLUMN>
						<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="stoc_invoice_number" HEADERTEXT="Wyeth Invoice No.">
							<ITEMSTYLE WRAP="False"></ITEMSTYLE>
						</C1WEBGRID:C1BOUNDCOLUMN>
						<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="stoc_unit" AGGREGATE="Sum" HEADERTEXT="Units">
							<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right"></ITEMSTYLE>
						</C1WEBGRID:C1BOUNDCOLUMN>
						<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="stoc_invoice_value" AGGREGATE="Sum" HEADERTEXT="Invoice Value">
							<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right"></ITEMSTYLE>
						</C1WEBGRID:C1BOUNDCOLUMN>
						<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="stoc_accrued_value" AGGREGATE="Sum" HEADERTEXT="Accrued Value">
							<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right"></ITEMSTYLE>
						</C1WEBGRID:C1BOUNDCOLUMN>
						<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="stoc_diff_value" AGGREGATE="Sum" HEADERTEXT="Diff WE to GIT">
							<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right"></ITEMSTYLE>
						</C1WEBGRID:C1BOUNDCOLUMN>
						<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="stoc_diff_value_accrued" AGGREGATE="Sum" HEADERTEXT="Diff WE to Accrual">
							<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right"></ITEMSTYLE>
						</C1WEBGRID:C1BOUNDCOLUMN>
						<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="stoc_comment" HEADERTEXT="Comment">
							<ITEMSTYLE WRAP="False"></ITEMSTYLE>
						</C1WEBGRID:C1BOUNDCOLUMN>
					</COLUMNS>
				</C1WEBGRID:C1WEBGRID>
			</ASP:PANEL></form>
	</body>
</HTML>
