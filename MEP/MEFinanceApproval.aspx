<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="MEFinanceApproval.aspx.vb" Inherits="Wyeth.Alf.FMA"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FMA</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<asp:literal id="_clientScript" runat="server"></asp:literal>
		<SCRIPT LANGUAGE="JavaScript" SRC="../JS/ClientScripts.js"> </SCRIPT>
		<STYLE>#lblCompletedAction { FONT-WEIGHT: bold; FONT-SIZE: 11pt }
	#lblFAMEApprove { FONT-WEIGHT: bold; FONT-SIZE: 11pt }
	#lblstartProcessing { FONT-WEIGHT: bold; FONT-SIZE: 11pt }
		</STYLE>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" RUNAT="server">
			<DIV class="HL"><ASP:LABEL id="lblPageTitle" RUNAT="server"></ASP:LABEL></DIV>
			<DIV CLASS="noprint">
				<ASP:PANEL id="FilterPanel" RUNAT="server" CSSCLASS="FilterPanel">
					<TABLE class="reportsFilter" id="Table1" cellSpacing="0" cellPadding="0" width="100%">
					</TABLE>
					<DIV class="reportsButtonBar">
						<TABLE id="Table2">
							<TR>
								<TD noWrap>
									<REP:REPORTDATA id="repData" RUNAT="server"></REP:REPORTDATA></TD>
								<TD align="center" width="100%">
									<DIV class="noprint">&nbsp;
										<ASP:BUTTON id="ExportExcel" RUNAT="server" CSSCLASS="button" TEXT="Export to Excel" VISIBLE="true"></ASP:BUTTON>&nbsp;<BUTTON class="button" onclick="javascript:window.print();" type="button">Print 
											Report</BUTTON>
									</DIV>
								</TD>
							</TR>
						</TABLE>
					</DIV>
				</ASP:PANEL></DIV>
			<P></P>
			<ASP:LABEL id="lblCompletedAction" RUNAT="server"></ASP:LABEL><C1WEBGRID:C1WEBGRID id="MyGrid" DEFAULTROWHEIGHT="22px" DEFAULTCOLUMNWIDTH="120px" AUTOGENERATECOLUMNS="False"
				SHOWFOOTER="True" runat="server">
				<COLUMNS>
					<c1webgrid:C1BoundColumn VISIBLE="False" DATAFIELD="TypeNumber"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn VISIBLE="False" DATAFIELD="type">
						<GROUPINFO HEADERTEXT="Total {0}" POSITION="Header"></GROUPINFO>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DATAFIELD="code_code"></c1webgrid:C1BoundColumn>
					<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="prod_presentation"></C1WEBGRID:C1BOUNDCOLUMN>
					<c1webgrid:C1BoundColumn DATAFIELD="stoc_date_open" HEADERTEXT="Date open">
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
					<c1webgrid:C1BoundColumn DATAFIELD="stoc_unit" HEADERTEXT="Units" AGGREGATE="Sum">
						<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DATAFIELD="stoc_invoice_value" HEADERTEXT="Invoice Value" AGGREGATE="Sum">
						<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DATAFIELD="stoc_accrued_value" HEADERTEXT="Accrued Value" AGGREGATE="Sum">
						<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DATAFIELD="stoc_diff_value" HEADERTEXT="Diff to GIT" AGGREGATE="Sum">
						<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DATAFIELD="stoc_diff_value_accrued" HEADERTEXT="Diff to accrued" AGGREGATE="Sum">
						<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DATAFIELD="stoc_comment" HEADERTEXT="Comment">
						<ITEMSTYLE WRAP="False"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
				</COLUMNS>
			</C1WEBGRID:C1WEBGRID>
			<P></P>
			<DIV CLASS="reportsButtonBar">
				<DIV CLASS="noprint">
					<TABLE WIDTH="100%">
						<TBODY>
							<TR>
								<TD NOWRAP><ASP:LABEL id="lblFAMEApprove" RUNAT="server"></ASP:LABEL></TD>
								<TD NOWRAP WIDTH="100%">
									<Asp:BUTTON CssCLASS="button" runat="server" id="btn_confirm" Text="Confirm"></Asp:BUTTON>
								</TD>
							</TR>
					</TABLE>
				</DIV>
			</DIV>
		</FORM>
	</body>
</HTML>
</Asp:BUTTON></TR></TBODY></DIV></DIV></FORM> </BODY></HTML>
