<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../Util/printReportCtl.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SalesStatCustomer2.aspx.vb" Inherits="Wyeth.Alf.SalesStatCustomer2"%>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 TRANSITIONAL//EN">
<HTML>
	<HEAD>
		<TITLE>SalesStatCustomer</TITLE>
		<META CONTENT="Microsoft Visual Studio .NET 7.1" NAME="GENERATOR">
		<META CONTENT="Visual Basic .NET 7.1" NAME="CODE_LANGUAGE">
		<META CONTENT="JavaScript" NAME="vs_defaultClientScript">
		<META CONTENT="http://schemas.microsoft.com/intellisense/ie5" NAME="vs_targetSchema">
		<LINK HREF="../Styles.css" TYPE="text/css" REL="stylesheet">
		<LINK MEDIA="print" HREF="../printing.css" TYPE="text/css" REL="stylesheet">
		<SCRIPT LANGUAGE="JavaScript" SRC="../JS/ClientScripts.js"></SCRIPT>
	</HEAD>
	<BODY MS_POSITIONING="GridLayout">
		<FORM ID="Form1" METHOD="post" RUNAT="server">
			<DIV CLASS="HL"><ASP:LABEL ID="lblPageTitle" RUNAT="server"></ASP:LABEL></DIV>
			<ASP:PANEL ID="FilterPanel" RUNAT="server" CSSCLASS="FilterPanel">
				<DIV class="noprint">
					<TABLE class="reportsFilter" cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD class="field" noWrap>Start Date:</TD>
							<TD>
								<BOX:WYETHTEXTBOX id="txtStartDate" RUNAT="server" TOOLTIP="Please enter a date" FRIENDLYNAME="Report Start Date"
									ALLOWBLANK="false" FIELDTYPE="DATE"></BOX:WYETHTEXTBOX></TD>
							<TD><A onclick="OpenDatePopUp('Form1.txtStartDate');" href="#"><IMG src="/Images/kalender.gif" border="0"></A></TD>
							<TD class="field" noWrap>End Date:</TD>
							<TD>
								<BOX:WYETHTEXTBOX id="txtEndDate" RUNAT="server" TOOLTIP="Please enter a date" FRIENDLYNAME="Report End Date"
									ALLOWBLANK="false" FIELDTYPE="DATE"></BOX:WYETHTEXTBOX></TD>
							<TD><A onclick="OpenDatePopUp('Form1.txtEndDate');" href="#"><IMG src="/Images/kalender.gif" border="0"></A></TD>
							<TD>
								<ASP:DROPDOWNLIST id="ddLine" RUNAT="server"></ASP:DROPDOWNLIST></TD>
							<TD width="100%">
								<TABLE cellSpacing="0" cellPadding="2">
									<TR vAlign="middle">
										<TD class="field" noWrap>Group by:</TD>
										<TD>
											<ASP:DROPDOWNLIST id="groupBy" RUNAT="server"></ASP:DROPDOWNLIST></TD>
									</TR>
								</TABLE>
							</TD>
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
									<prt:printReportCtl id="prtControl" runat="server"></prt:printReportCtl><BR>
								</DIV>
							</TD>
						</TR>
					</TABLE>
				</DIV>
			</ASP:PANEL>
			<P></P>
			<DIV CLASS="noprint">
				<ASP:BUTTON ID="btnLevel1" CSSCLASS="button_common" RUNAT="server" TEXT="Level 1"></ASP:BUTTON>&nbsp;
				<ASP:BUTTON ID="btnLevel2" CSSCLASS="button_common" RUNAT="server" TEXT="Level 2"></ASP:BUTTON>&nbsp;
			</DIV>
			<P></P>
			<ASP:PANEL ID="ReportPanel" RUNAT="server" CSSCLASS="ReportPanel">
				<C1WEBGRID:C1WEBGRID id="MyGrid" RUNAT="server" DEFAULTCOLUMNWIDTH="120px" DEFAULTROWHEIGHT="22px" AutoGenerateColumns="False">
					<COLUMNS>
						<c1webgrid:C1BoundColumn DataField="PROD_DESCRIPTION" Visible="False" SortExpression="PROD_DESCRIPTION" HeaderText="Description">
							<GROUPINFO POSITION="Header" HEADERTEXT="TOTAL {0}" OUTLINEMODE="StartCollapsed"></GROUPINFO>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="CUST_NAME" SortExpression="CUST_NAME" HeaderText="Customer Name">
							<GROUPINFO OUTLINEMODE="StartCollapsed"></GROUPINFO>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="ORPO_UNITS" SortExpression="ORPO_UNITS" Aggregate="Sum" HeaderText="Units">
							<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
							<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="ORPO_VALUE" SortExpression="ORPO_VALUE" Aggregate="Sum" HeaderText="Value">
							<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
							<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="ORPO_FGUNITS" SortExpression="ORPO_FGUNITS" Aggregate="Sum" HeaderText="FGUnits">
							<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
							<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="ORPO_FGVALUE" SortExpression="ORPO_FGVALUE" Aggregate="Sum" HeaderText="FGValue">
							<HEADERSTYLE CSSCLASS="headlineSeperator"></HEADERSTYLE>
							<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
							<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="CUDI_CUSTOMER_NR" Visible="False"></c1webgrid:C1BoundColumn>
					</COLUMNS>
				</C1WEBGRID:C1WEBGRID>
			</ASP:PANEL></FORM>
	</BODY>
</HTML>
