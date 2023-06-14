<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../Util/printReportCtl.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="InvlBatchReport.aspx.vb" Inherits="Wyeth.Alf.InvlBatchReport"%>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Inventory Batch Report</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<LINK media="print" href="../printing.css" type="text/css" rel="stylesheet">
		<asp:literal id="_clientScript" runat="server"></asp:literal>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<ASP:PANEL id="FilterPanel" CSSCLASS="FilterPanel" RUNAT="server">
				<DIV class="noprint">
					<TABLE class="reportsFilter" cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD style="WIDTH: 126px">
								<BOX:WYETHTEXTBOX id="txtStartDate" RUNAT="server" ALLOWBLANK="false" FRIENDLYNAME="Report Date" FIELDTYPE="DATE"
									TOOLTIP="Please enter a start date"></BOX:WYETHTEXTBOX></TD>
							<TD>
								<asp:Image id="StartImage" runat="server" ImageUrl="/images/KALENDER.GIF" ImageAlign="AbsMiddle"></asp:Image></TD>
							<TD style="WIDTH: 126px">&nbsp;
								<BOX:WYETHTEXTBOX id="txtEndDate" RUNAT="server" ALLOWBLANK="false" FRIENDLYNAME="Report Date" FIELDTYPE="DATE"
									TOOLTIP="Please enter a end date"></BOX:WYETHTEXTBOX></TD>
							<TD>
								<asp:Image id="endImage" runat="server" ImageUrl="/images/KALENDER.GIF" ImageAlign="AbsMiddle"></asp:Image></TD>
							<TD style="WIDTH: 201px" noWrap>
								<asp:DropDownList id="ddproduct" runat="server" Width="328px"></asp:DropDownList></TD>
							<TD>
								<ASP:DROPDOWNLIST id="ddLineSelect" RUNAT="server"></ASP:DROPDOWNLIST>&nbsp;&nbsp;
								<ASP:DROPDOWNLIST id="ddDistribselect" RUNAT="server"></ASP:DROPDOWNLIST></TD>
						</TR>
					</TABLE>
				</DIV>
				<DIV class="reportsButtonBar">
					<TABLE>
						<TR>
							<TD noWrap>
								<REP:REPORTDATA id="repData" RUNAT="server"></REP:REPORTDATA></TD>
							<TD align="center"></TD>
							<TD align="center" width="100%">
								<DIV class="noprint">
									<ASP:BUTTON id="btnGenRep" CSSCLASS="button" RUNAT="server" TEXT="Generate Report"></ASP:BUTTON>&nbsp;
									<ASP:BUTTON id="ExportExcel" CSSCLASS="button" RUNAT="server" TEXT="Export to Excel" VISIBLE="true"></ASP:BUTTON>&nbsp;
									<PRT:PRINTREPORTCTL id="prtControl" runat="server"></PRT:PRINTREPORTCTL></DIV>
							</TD>
						</TR>
					</TABLE>
				</DIV>
			</ASP:PANEL><asp:panel id="ReportPanel" Runat="server" CssClass="ReportPanel">
				<C1WEBGRID:C1WEBGRID id="MyGrid" runat="server" Width="100%" DefaultRowHeight="22px" DefaultColumnWidth="120px"
					AutoGenerateColumns="False" AllowSorting="True" AllowAutoSort="True">
					<Columns>
						<c1webgrid:C1BoundColumn SortExpression="PROD_PHZNR" HeaderText="Prod. No." DataField="PROD_PHZNR"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn SortExpression="PROD_PRESENTATION" HeaderText="Presentation" DataField="PROD_PRESENTATION">
							<GroupInfo Position="Header" HeaderText="{0} "></GroupInfo>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn SortExpression="INVL_BATCH_NUMBER" HeaderText="Batch No." DataField="INVL_BATCH_NUMBER"></c1webgrid:C1BoundColumn>
					</Columns>
				</C1WEBGRID:C1WEBGRID>
			</asp:panel></form>
	</body>
</HTML>
