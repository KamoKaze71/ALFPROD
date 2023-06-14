<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../Util/printReportCtl.ascx" %>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SalesForeCastIris.aspx.vb" Inherits="Wyeth.Alf.ForcastIris"%>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ForcastIris</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<asp:literal id="_clientScript" runat="server"></asp:literal>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<DIV CLASS="HL">
				<ASP:LABEL ID="lblPageTitle" RUNAT="server"></ASP:LABEL>
			</DIV>
			<ASP:PANEL ID="FilterPanel" CSSCLASS="FilterPanel" RUNAT="server">
				<DIV class="noprint">
					<TABLE class="reportsFilter" cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD class="field" noWrap>Start Date:</TD>
							<TD>
								<BOX:WYETHTEXTBOX id="txtStartDate" RUNAT="server" TOOLTIP="Please enter a date" FRIENDLYNAME="Report Start Date"
									ALLOWBLANK="false" FIELDTYPE="DATE"></BOX:WYETHTEXTBOX></TD>
							<TD>
								<asp:Image id="StartImage" runat="server" ImageUrl="/images/KALENDER.GIF"></asp:Image></TD>
							<TD class="field" noWrap>End Date:</TD>
							<TD>
								<BOX:WYETHTEXTBOX id="txtEndDate" RUNAT="server" TOOLTIP="Please enter a date" FRIENDLYNAME="Report End Date"
									ALLOWBLANK="false" FIELDTYPE="DATE"></BOX:WYETHTEXTBOX></TD>
							<TD>
								<asp:Image id="EndImage" runat="server" ImageUrl="/images/KALENDER.GIF"></asp:Image>
							<TD>
								<ASP:DROPDOWNLIST id="ddlineselect" RUNAT="server"></ASP:DROPDOWNLIST></TD>
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
									<ASP:BUTTON id="btnGenRep" RUNAT="server" CSSCLASS="button" TEXT="Generate Report"></ASP:BUTTON>&nbsp;
									<ASP:BUTTON id="ExportExcel" RUNAT="server" CSSCLASS="button" TEXT="Export to Excel" VISIBLE="true"></ASP:BUTTON>&nbsp;
									<prt:printReportCtl id="prtControl" runat="server"></prt:printReportCtl></DIV>
							</TD>
						</TR>
					</TABLE>
				</DIV>
			</ASP:PANEL>
			<P></P>
			<asp:Panel Runat="server" ID="ReportPanel" CssClass="ReportPanel">
				<C1WebGrid:C1WebGrid id="MyGrid" runat="server" Width="100%" AutoGenerateColumns="False" DefaultColumnWidth="120px"
					DefaultRowHeight="22px">
					<COLUMNS>
						<c1webgrid:C1BoundColumn DataField="CUDI_CUSTOMER_NR" HeaderText="Customer No.">
							<ITEMSTYLE WRAP="False"></ITEMSTYLE>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="CUST_NAME" HeaderText="Customer Name">
							<ITEMSTYLE WRAP="False"></ITEMSTYLE>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="CUST_DEPARTMENT" HeaderText="Customer Department">
							<ITEMSTYLE WRAP="False"></ITEMSTYLE>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="PROD_PHZNR" HeaderText="Prod. No.">
							<ITEMSTYLE WRAP="False"></ITEMSTYLE>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="PROD_CC_DESCRIPTION" HeaderText="Cost Center">
							<ITEMSTYLE WRAP="False"></ITEMSTYLE>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="ORPO_UNITS" HeaderText="Units">
							<ITEMSTYLE WRAP="False"></ITEMSTYLE>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="ORPO_FGUNITS" HeaderText="FG Units"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="ORHE_DATE" HeaderText="Date">
							<ITEMSTYLE WRAP="False"></ITEMSTYLE>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="LINE_CODE" HeaderText="Line">
							<ITEMSTYLE WRAP="False"></ITEMSTYLE>
						</c1webgrid:C1BoundColumn>
					</COLUMNS>
				</C1WebGrid:C1WebGrid>
			</asp:Panel>
		</form>
	</body>
</HTML>
