<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="MEClosing.aspx.vb" Inherits="Wyeth.Alf.MEC" %>
<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../Util/printReportCtl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>MEC</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<SCRIPT LANGUAGE="JavaScript" SRC="../JS/ClientScripts.js"> </SCRIPT>
		<STYLE>#lblProcessMonth { FONT-WEIGHT: bold; FONT-SIZE: 11pt }
	#lblItems { FONT-WEIGHT: bold; FONT-SIZE: 11pt }
	#lblstartProcessing { FONT-WEIGHT: bold; FONT-SIZE: 11pt }
		</STYLE>
	</HEAD>
	<BODY MS_POSITIONING="GridLayout">
		<FORM ID="Form1" METHOD="post" RUNAT="server">
			<DIV CLASS="HL">
				<ASP:LABEL ID="lblPageTitle" RUNAT="server"></ASP:LABEL>
			</DIV>
			<DIV CLASS="reportsButtonBar">
				<TABLE ALIGN="center" WIDTH="100%" RUNAT="server">
					<TR>
						<TD><asp:Label id="lblProcessMonth" runat="server"></asp:Label></TD>
						<TD><ASP:BUTTON id="ExportExcel" RUNAT="server" VISIBLE="true" TEXT="Export to Excel" CSSCLASS="button"></ASP:BUTTON>&nbsp;&nbsp;
							<prt:printreportctl id="prtControl" runat="server"></prt:printreportctl>
							<DIV></DIV>
						</TD>
					</TR>
				</TABLE>
			</DIV>
			<P></P>
			<asp:Label id="lblItems" runat="server" Font-Bold="True">ALF will process the following Items:</asp:Label>
			<C1WebGrid:C1WebGrid id="MyGrid" runat="server" AUTOGENERATECOLUMNS="False" SHOWFOOTER="True">
				<COLUMNS>
					<c1webgrid:C1BoundColumn Visible="False" DataField="TypeNumber"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn Visible="False" DataField="type">
						<GROUPINFO HEADERTEXT="Total {0}" POSITION="Header"></GROUPINFO>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DataField="code_code"></c1webgrid:C1BoundColumn>
					<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="prod_presentation"></C1WEBGRID:C1BOUNDCOLUMN>
					<c1webgrid:C1BoundColumn DataField="stoc_date_stock" HeaderText="Date open">
						<ITEMSTYLE WRAP="False"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DataField="stoc_date_accrued" HeaderText="Date accrued">
						<ITEMSTYLE WRAP="False"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DataField="stoc_date_correct" HeaderText="Date correct">
						<ITEMSTYLE WRAP="False"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DataField="stoc_order_number" HeaderText="Order No.">
						<ITEMSTYLE WRAP="False"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DataField="stoc_invoice_number" HeaderText="Wyeth Invoice No.">
						<ITEMSTYLE WRAP="False"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DataField="stoc_unit" HeaderText="Units" Aggregate="Sum">
						<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DataField="stoc_invoice_value" HeaderText="Invoice Value" Aggregate="Sum">
						<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DataField="stoc_accrued_value" HeaderText="Accrued Value" Aggregate="Sum">
						<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DataField="stoc_diff_value" HeaderText="Diff WE to GIT" Aggregate="Sum">
						<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DataField="stoc_diff_value_accrued" HeaderText="Diff WE to Accrual" Aggregate="Sum">
						<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DataField="stoc_comment" HeaderText="Comment">
						<ITEMSTYLE WRAP="True"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
				</COLUMNS>
			</C1WebGrid:C1WebGrid>
			<P></P>
			<DIV CLASS="reportsButtonBar" STYLE="HEIGHT: 25px">
				<DIV CLASS="noprint">
					<TABLE WIDTH="100%">
						<TR>
							<TD NOWRAP>
								<asp:Label id="lblstartProcessing" runat="server"></asp:Label></TD>
							<TD ALIGN="left" WIDTH="100%"><BUTTON ID="btn_process_invoices" CLASS="button" ONCLICK="OpenPopUp('ProcessingConfirmation.aspx');"
									TYPE="button" runat="server"> Processing</BUTTON></TD>
						</TR>
					</TABLE>
				</DIV>
			</DIV>
		</FORM>
	</BODY>
</HTML>
