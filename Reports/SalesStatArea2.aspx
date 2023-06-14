<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../Util/printReportCtl.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SalesStatArea2.aspx.vb" Inherits="Wyeth.Alf.SalesStatArea2" responseEncoding="Windows-1252" %>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 TRANSITIONAL//EN">
<HTML>
	<HEAD>
		<TITLE>SalesStatArea</TITLE>
		<META http-equiv="Content-Type" content="text/html; charset=Windows-1252">
		<META content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<META content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<META content="JavaScript" name="vs_defaultClientScript">
		<META content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../JS/ClientScripts.js"></SCRIPT>
	</HEAD>
	<BODY MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" RUNAT="server">
			<DIV class="HL"><ASP:LABEL id="lblPageTitle" RUNAT="server"></ASP:LABEL></DIV>
			<ASP:PANEL id="FilterPanel" RUNAT="server" CSSCLASS="FilterPanel">
				<DIV class="noprint">
					<TABLE class="reportsFilter" cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD class="field" noWrap>Start Date:</TD>
							<TD>
								<BOX:WYETHTEXTBOX id="txtStartDate" RUNAT="server" TOOLTIP="Please enter a date" FRIENDLYNAME="Report Start Date"
									ALLOWBLANK="false" FIELDTYPE="DATE"></BOX:WYETHTEXTBOX></TD>
							<TD><A onclick="OpenDatePopUp('Form1.txtStartDate');"><IMG src="/Images/kalender.gif" border="0"></A></TD>
							<TD class="field" noWrap>End Date:</TD>
							<TD>
								<BOX:WYETHTEXTBOX id="txtEndDate" RUNAT="server" TOOLTIP="Please enter a date" FRIENDLYNAME="Report End Date"
									ALLOWBLANK="false" FIELDTYPE="DATE"></BOX:WYETHTEXTBOX></TD>
							<TD><A onclick="OpenDatePopUp('Form1.txtEndDate');"><IMG src="/Images/kalender.gif" border="0"></A></TD>
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
			<DIV class="noprint"><ASP:BUTTON id="btnLevel1" RUNAT="server" CSSCLASS="button_common" TEXT="Level1"></ASP:BUTTON>&nbsp;
				<ASP:BUTTON id="btnLevel2" RUNAT="server" CSSCLASS="button_common" TEXT="Level2"></ASP:BUTTON>&nbsp;
				<ASP:BUTTON id="btnLevel3" RUNAT="server" CSSCLASS="button_common" TEXT="Level3"></ASP:BUTTON></DIV>
			<ASP:PANEL id="ReportPanel" RUNAT="server" CSSCLASS="ReportPanel">
				<C1WEBGRID:C1WEBGRID id="MyGrid" RUNAT="server" AUTOGENERATECOLUMNS="False" DEFAULTCOLUMNWIDTH="120px"
					DEFAULTROWHEIGHT="22px" SHOWFOOTER="True">
					<COLUMNS>
						<c1webgrid:C1BoundColumn DataField="SARE_NAME" Visible="False">
							<GROUPINFO POSITION="Header" HEADERTEXT="TOTAL {0}" OUTLINEMODE="StartCollapsed"></GROUPINFO>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="PROD_DESCRIPTION" Visible="False" SortExpression="PROD_DESCRIPTION" HeaderText="Description">
							<GROUPINFO POSITION="Header" HEADERTEXT="TOTAL {0}" OUTLINEMODE="StartCollapsed"></GROUPINFO>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="CUSTOMER_NAME" Visible="False" SortExpression="CUSTOMER_NAME" HeaderText="Customer Name">
							<GROUPINFO POSITION="Header" HEADERTEXT="TOTAL {0}" OUTLINEMODE="StartCollapsed"></GROUPINFO>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="PERCENTAGE_UNITS" SortExpression="PERCENTAGE_UNITS" Aggregate="Sum" HeaderText="Units">
							<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
							<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="PERCENTAGE_VALUE" SortExpression="PERCENTAGE_VALUE" Aggregate="Sum" HeaderText="Value">
							<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
							<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="PERCENTAGE_FGUNITS" SortExpression="PERCENTAGE_FGUNITS" Aggregate="Sum"
							HeaderText="FGUnits">
							<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
							<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="PERCENTAGE_FGVALUE" SortExpression="PERCENTAGE_FGVALUE" Aggregate="Sum"
							HeaderText="FGValue">
							<HEADERSTYLE CSSCLASS="headlineSeperator"></HEADERSTYLE>
							<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
							<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="CUDI_CUSTOMER_NR" Visible="False"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="sare_id" Visible="False"></c1webgrid:C1BoundColumn>
					</COLUMNS>
				</C1WEBGRID:C1WEBGRID>
			</ASP:PANEL></FORM>
	</BODY>
</HTML>
