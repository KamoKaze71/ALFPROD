<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="MEHistory.aspx.vb" Inherits="Wyeth.Alf.MEP"%>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../Util/printReportCtl.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>MEP</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<STYLE>#lblCompletedAction {FONT_SIZE: 11pt; FONT-WEIGHT:bold}</STYLE>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<DIV class="HL"><ASP:LABEL id="lblPageTitle" RUNAT="server"></ASP:LABEL></DIV>
			<ASP:PANEL id="FilterPanel" RUNAT="server" CSSCLASS="FilterPanel">
				<DIV CLASS="noprint">
					<TABLE CLASS="reportsFilter" ID="Table1" CELLSPACING="0" CELLPADDING="0" WIDTH="100%">
					</TABLE>
				</DIV>
				<DIV CLASS="reportsButtonBar">
					<TABLE ID="Table2">
						<TR>
							<TD NOWRAP><REP:REPORTDATA id="repData" RUNAT="server"></REP:REPORTDATA></TD>
							<TD align="center" width="100%">
								<DIV CLASS="noprint"><ASP:DROPDOWNLIST id="ddMonthEnd" RUNAT="server" WIDTH="104px"></ASP:DROPDOWNLIST><ASP:BUTTON id="btn_Invoices" RUNAT="server" CSSCLASS="button" WIDTH="144px" CAUSESVALIDATION="False"
										HEIGHT="19px" TEXT="Generate Report"></ASP:BUTTON>&nbsp;
									<ASP:BUTTON id="ExportExcel" RUNAT="server" CSSCLASS="button" TEXT="Export to Excel" VISIBLE="true"></ASP:BUTTON>&nbsp;&nbsp;<prt:printreportctl id="prtControl" runat="server"></prt:printreportctl></DIV>
	
								</DIV>
							</TD>
						</TR>
					</TABLE>
				</DIV>
			</ASP:PANEL>
			<C1WEBGRID:C1WEBGRID id="MyGridLogs" RUNAT="server" WIDTH="100%" DefaultRowHeight="22px" DefaultColumnWidth="120px"
				AUTOGENERATECOLUMNS="False">
				<COLUMNS>
					<c1webgrid:C1BoundColumn DataField="hidden" Visible="False">
						<GROUPINFO POSITION="Header" HEADERTEXT="Logs for selected Month" OUTLINEMODE="StartCollapsed"></GROUPINFO>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DataField="logs_source" HeaderText="Source"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DataField="logs_description" HeaderText="Description"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DataField="logs_date_changed" HeaderText="Date"></c1webgrid:C1BoundColumn>
				</COLUMNS>
			</C1WEBGRID:C1WEBGRID>
			<p></p>
			<ASP:LABEL id="lblCompletedAction" RUNAT="server">The following Actions have been completed for selected month:</ASP:LABEL><C1WEBGRID:C1WEBGRID id="MyGrid" runat="server" DEFAULTROWHEIGHT="22px" DEFAULTCOLUMNWIDTH="120px" AUTOGENERATECOLUMNS="False"
				SHOWFOOTER="True">
				<COLUMNS>
					<c1webgrid:C1BoundColumn DATAFIELD="TypeNumber" VISIBLE="False"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DATAFIELD="type" VISIBLE="False">
						<GROUPINFO POSITION="Header" HEADERTEXT="Total {0}"></GROUPINFO>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DATAFIELD="code_code"></c1webgrid:C1BoundColumn>
					<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="prod_presentation"></C1WEBGRID:C1BOUNDCOLUMN>
					<c1webgrid:C1BoundColumn DATAFIELD="stoc_date_stock" HEADERTEXT="Date open">
						<ITEMSTYLE WRAP="False"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DATAFIELD="stoc_date_accrued" HEADERTEXT="Date accrued">
						<ITEMSTYLE WRAP="False"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DATAFIELD="stoc_date_correct" HEADERTEXT="Date correct">
						<ITEMSTYLE WRAP="False"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DATAFIELD="stoc_order_number" HEADERTEXT="Order No.">
						<ITEMSTYLE WRAP="False"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DATAFIELD="stoc_invoice_number" HEADERTEXT="Wyeth Invoice No.">
						<ITEMSTYLE WRAP="False"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DATAFIELD="stoc_unit" AGGREGATE="Sum" HEADERTEXT="Units">
						<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DATAFIELD="stoc_invoice_value" AGGREGATE="Sum" HEADERTEXT="Invoice Value">
						<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DATAFIELD="stoc_accrued_value" AGGREGATE="Sum" HEADERTEXT="Accrued Value">
						<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DATAFIELD="stoc_diff_value" AGGREGATE="Sum" HEADERTEXT="Diff WE to GIT">
						<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DATAFIELD="stoc_diff_value_accrued" AGGREGATE="Sum" HEADERTEXT="Diff WE to Accrual">
						<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DATAFIELD="stoc_comment" HEADERTEXT="Comment">
						<ITEMSTYLE WRAP="True"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
				</COLUMNS>
			</C1WEBGRID:C1WEBGRID></form>
		</FORM>
	</body>
</HTML>
