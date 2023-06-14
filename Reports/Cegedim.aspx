<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Cegedim.aspx.vb" Inherits="Wyeth.Alf.Cegedim"%>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../Util/printReportCtl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Cegedim</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../JS/ClientScripts.js"></SCRIPT>
		<asp:literal id="_clientScript" runat="server"></asp:literal>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<DIV class="HL"><ASP:LABEL id="lblPageTitle" RUNAT="server"></ASP:LABEL></DIV>
			<ASP:PANEL id="FilterPanel" RUNAT="server" CSSCLASS="FilterPanel">
				<DIV class="noprint">
					<TABLE class="reportsFilter" cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD class="field" noWrap>Start Date:</TD>
							<TD>
								<BOX:WYETHTEXTBOX id="txtStartDate" RUNAT="server" FIELDTYPE="DATE" ALLOWBLANK="false" FRIENDLYNAME="Report Start Date"
									TOOLTIP="Please enter a date"></BOX:WYETHTEXTBOX></TD>
							<TD>&nbsp;
								<asp:Image id="StartImage" runat="server" ImageUrl="/images/kalender.gif"></asp:Image></TD>
							<TD class="field" noWrap>End Date:</TD>
							<TD>
								<BOX:WYETHTEXTBOX id="txtEndDate" RUNAT="server" FIELDTYPE="DATE" ALLOWBLANK="false" FRIENDLYNAME="Report End Date"
									TOOLTIP="Please enter a date"></BOX:WYETHTEXTBOX></TD>
							<TD>
								<asp:Image id="EndImage" runat="server" ImageUrl="/images/kalender.gif"></asp:Image></TD>
							<TD>
								<ASP:DROPDOWNLIST id="ddlineselect" RUNAT="server"></ASP:DROPDOWNLIST></TD>
							<TD width="100%">
								<ASP:DROPDOWNLIST id="ddDistribSelect" RUNAT="server"></ASP:DROPDOWNLIST></TD>
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
									<ASP:BUTTON id="ExportExcel" RUNAT="server" CSSCLASS="button" TEXT="Export to File" VISIBLE="true"></ASP:BUTTON>&nbsp;
									<prt:printReportCtl id="prtControl" runat="server"></prt:printReportCtl>
								</DIV>
							</TD>
						</TR>
					</TABLE>
				</DIV>
			</ASP:PANEL>
			<asp:panel id="ReportPanel" cssClass="ReportPanel" Runat="server">
				<C1WebGrid:C1WebGrid id="MyGrid" runat="server" EnableViewState="False" DefaultRowHeight="22px" DefaultColumnWidth="120px"
					AutoGenerateColumns="False" width="100%">
					<Columns>
						<c1webgrid:C1BoundColumn DataField="cust_id" Visible="False"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="CUDI_CUSTOMER_NR" HeaderText="Cust No."></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="PROD_PHZNR" HeaderText="Product No."></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="ORHE_DATE" HeaderText="Date"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="ORPO_UNITS" HeaderText="Units">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="ORPO_VALUE" HeaderText="Value">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="ORPO_FGUNITS" HeaderText="FGUnits" DataFormatString="{0:0}">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_id" Visible="False" HeaderText="prod_id"></c1webgrid:C1BoundColumn>
					</Columns>
				</C1WebGrid:C1WebGrid>
			</asp:panel></form>
	</body>
</HTML>
