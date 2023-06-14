<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PremarinReport.aspx.vb" Inherits="Wyeth.Alf.PremarinReport"%>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../Util/printReportCtl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PremarinReport</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<SCRIPT LANGUAGE="JavaScript" SRC="../JS/ClientScripts.js"></SCRIPT>
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
								<BOX:WYETHTEXTBOX id="txtStartDate" RUNAT="server" TOOLTIP="Please enter a date" FIELDTYPE="DATE"
									FRIENDLYNAME="Report Start Date" ALLOWBLANK="false"></BOX:WYETHTEXTBOX></TD>
							<TD><asp:Image id="StartImage" runat="server" ImageAlign="AbsMiddle" ImageUrl="/images/KALENDER.GIF"></asp:Image>
							</TD>
							<TD class="field" noWrap>End Date:</TD>
							<TD>
								<BOX:WYETHTEXTBOX id="txtEndDate" RUNAT="server" TOOLTIP="Please enter a date" FIELDTYPE="DATE" FRIENDLYNAME="Report End Date"
									ALLOWBLANK="false"></BOX:WYETHTEXTBOX></TD>
							<TD><asp:Image id="EndImage" runat="server" ImageAlign="AbsMiddle" ImageUrl="/images/KALENDER.GIF"></asp:Image>
							<TD></TD>
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
			</ASP:PANEL><asp:panel id="ReportPanel" CssClass="ReportPanel" Runat="server">
				<C1WEBGRID:C1WEBGRID id="MyGrid" runat="server" DefaultRowHeight="22px" DefaultColumnWidth="120px" Width="100%"
					AutoGenerateColumns="False">
					<COLUMNS>
						<c1webgrid:C1BoundColumn DataField="prod_phznr" HeaderText="Prod. No."></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_presentation" HeaderText="Presentation"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_strength" HeaderText="Strength mg"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_packsize" HeaderText="Tabs per Package"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="INVL_UNITS" HeaderText="No. Packages In Stock">
							<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="total_number_tablets_stock" HeaderText="Total Number of Tablets in Stock:">
							<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="total_mggrams_stock" HeaderText="Total Grams of Activity in Stock">
							<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="TOTAL_number_PACKAGES_SOLD" HeaderText="Usage No. of Packages:">
							<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="TOTAL_number_tablets_sold" HeaderText="Total No of Tablets Usage">
							<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="total_mggrams_sold" HeaderText="Grams of Activity Usage">
							<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
						</c1webgrid:C1BoundColumn>
					</COLUMNS>
				</C1WEBGRID:C1WEBGRID>
			</asp:panel>
		</form>
	</body>
</HTML>
