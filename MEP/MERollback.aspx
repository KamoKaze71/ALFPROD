<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../Util/printReportCtl.ascx" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="MERollback.aspx.vb" Inherits="Wyeth.Alf.MER"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>MER</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../JS/ClientScripts.js"> </SCRIPT>
		<STYLE>#lblProcessMonth { FONT-WEIGHT: bold; FONT-SIZE: 11pt }
	#lblstartProcessing { FONT-WEIGHT: bold; FONT-SIZE: 11pt }
	#lblstartProcessing { FONT-WEIGHT: bold; FONT-SIZE: 11pt }
		</STYLE>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" RUNAT="server">
			<DIV class="HL" STYLE="HEIGHT: 30px"><ASP:LABEL id="lblPageTitle" RUNAT="server"></ASP:LABEL></DIV>
			<DIV class="reportsButtonBar">
				<TABLE width="100%" align="center">
					<TR>
						<TD><ASP:LABEL id="lblProcessMonth" RUNAT="server"></ASP:LABEL><BR>
						</TD>
						<TD>
							<DIV class="noprint"><ASP:BUTTON id="ExportExcel" RUNAT="server" VISIBLE="true" TEXT="Export to Excel" CSSCLASS="button"></ASP:BUTTON>&nbsp;&nbsp;
								<prt:printreportctl id="prtControl" runat="server"></prt:printreportctl></DIV>
							<DIV></DIV>
						</TD>
					</TR>
				</TABLE>
			</DIV>
			<P></P>
			<C1WEBGRID:C1WEBGRID id="MyGrid" RUNAT="server" SHOWFOOTER="True" AUTOGENERATECOLUMNS="False">
				<COLUMNS>
					<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="TypeNumber" VISIBLE="False"></C1WEBGRID:C1BOUNDCOLUMN>
					<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="type" VISIBLE="False">
						<GROUPINFO POSITION="Header" HEADERTEXT="Total {0}"></GROUPINFO>
					</C1WEBGRID:C1BOUNDCOLUMN>
					<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="code_code"></C1WEBGRID:C1BOUNDCOLUMN>
					<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="prod_presentation"></C1WEBGRID:C1BOUNDCOLUMN>
					<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="stoc_date_stock" HEADERTEXT="Date open">
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
					<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="stoc_unit" HEADERTEXT="Units" AGGREGATE="Sum">
						<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right"></ITEMSTYLE>
					</C1WEBGRID:C1BOUNDCOLUMN>
					<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="stoc_invoice_value" HEADERTEXT="Invoice Value" AGGREGATE="Sum">
						<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right"></ITEMSTYLE>
					</C1WEBGRID:C1BOUNDCOLUMN>
					<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="stoc_accrued_value" HEADERTEXT="Accrued Value" AGGREGATE="Sum">
						<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right"></ITEMSTYLE>
					</C1WEBGRID:C1BOUNDCOLUMN>
					<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="stoc_diff_value" HEADERTEXT="Diff WE to GIT" AGGREGATE="Sum">
						<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right"></ITEMSTYLE>
					</C1WEBGRID:C1BOUNDCOLUMN>
					<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="stoc_diff_value_accrued" HEADERTEXT="Diff WE to Accrual" AGGREGATE="Sum">
						<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right"></ITEMSTYLE>
					</C1WEBGRID:C1BOUNDCOLUMN>
					<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="stoc_comment" HEADERTEXT="Comment">
						<ITEMSTYLE WRAP="True"></ITEMSTYLE>
					</C1WEBGRID:C1BOUNDCOLUMN>
				</COLUMNS>
			</C1WEBGRID:C1WEBGRID>
			<P></P>
			<DIV class="reportsButtonBar" STYLE="HEIGHT: 25px">
				<DIV class="noprint">
					<TABLE width="100%" align="center" RUNAT="server" ID="tblRollback">
						<TR>
							<TD NOWRAP><ASP:LABEL id="lblstartProcessing" RUNAT="server"></ASP:LABEL></TD>
							<TD WIDTH="100%">
								<BUTTON class="button" id="btn_process_invoices" type="button" ONCLICK="OpenPopUpSmall('RollbackConfirmation.aspx');">
									Rollback Invoices</BUTTON>
							</TD>
						</TR>
					</TABLE>
					<DIV></DIV>
		</FORM>
		</DIV></DIV>
	</body>
</HTML>
