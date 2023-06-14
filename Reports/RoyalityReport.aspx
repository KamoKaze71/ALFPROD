<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="RoyalityReport.aspx.vb" Inherits="Wyeth.Alf.RoyalityReport"%>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../Util/printReportCtl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>RoyalityReport</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<SCRIPT LANGUAGE="JavaScript" SRC="../JS/ClientScripts.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<DIV CLASS="HL">
				<ASP:LABEL ID="lblPageTitle" RUNAT="server"></ASP:LABEL>
			</DIV>
			<ASP:PANEL ID="Panel1" CSSCLASS="FilterPanel" RUNAT="server">
				<DIV class="noprint">
					<TABLE class="reportsFilter" cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD class="field" noWrap>
								<ASP:DROPDOWNLIST id="ddInvoicerSelect" RUNAT="server" WIDTH="192px"></ASP:DROPDOWNLIST>
								<ASP:DROPDOWNLIST id="ddYearSelect" RUNAT="server" WIDTH="160px"></ASP:DROPDOWNLIST></TD>
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
			<asp:Panel id="ReportPanel" Runat="server" cssClass="ReportPanel">
				<TABLE width="100%" RUNAT="server">
					<TR>
						<TD class="head" align="center">Royality Values</TD>
					</TR>
					<TR>
						<TD>
							<C1WEBGRID:C1WEBGRID id="MyGrid" runat="server" DefaultRowHeight="22px" DefaultColumnWidth="120px" ShowFooter="True"
								AutoGenerateColumns="False" width="100%">
								<COLUMNS>
									<c1webgrid:C1BoundColumn DataField="prod_description" HeaderText="Product"></c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="prod_phznr" HeaderText="Local Item Code"></c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="Prod_plant_item_number" HeaderText="Plant Item"></c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="prod_presentation" HeaderText="Presentation">
										<ITEMSTYLE WRAP="False"></ITEMSTYLE>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="val_jan" HeaderText="Jan">
										<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
										<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="val_feb" HeaderText="Feb">
										<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
										<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="val_mar" HeaderText="Mar">
										<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
										<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="val_apr" HeaderText="Apr">
										<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
										<FOOTERSTYLE HORIZONTALALIGN="Center"></FOOTERSTYLE>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="val_may" HeaderText="May">
										<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
										<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="val_jun" HeaderText="Jun">
										<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
										<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="val_jul" HeaderText="Jul">
										<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
										<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="val_aug" HeaderText="Aug">
										<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
										<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="val_sep" HeaderText="Sep">
										<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
										<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="val_oct" HeaderText="Oct">
										<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
										<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="val_nov" HeaderText="Nov">
										<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
										<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="val_dec" HeaderText="Dec">
										<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
										<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
									</c1webgrid:C1BoundColumn>
								</COLUMNS>
							</C1WEBGRID:C1WEBGRID></TD>
					</TR>
					<TR height="25">
						<TD></TD>
					</TR>
					<TR>
						<TD class="head" align="center">Royality Units</TD>
					</TR>
					<TR>
						<TD>
							<C1WEBGRID:C1WEBGRID id="myGridUnits" runat="server" DefaultRowHeight="22px" DefaultColumnWidth="120px"
								ShowFooter="True" AutoGenerateColumns="False" Width="100%">
								<COLUMNS>
									<c1webgrid:C1BoundColumn DataField="prod_description" HeaderText="Product"></c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="prod_phznr" HeaderText="Local Item Code"></c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="Prod_plant_item_number" HeaderText="Plant Item"></c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="prod_presentation" HeaderText="Presentation"></c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="uni_jan" HeaderText="Jan">
										<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
										<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="uni_feb" HeaderText="Feb">
										<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
										<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="uni_mar" HeaderText="Mar">
										<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
										<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="uni_apr" HeaderText="Apr">
										<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
										<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="uni_may" HeaderText="May">
										<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
										<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="uni_jun" HeaderText="Jun">
										<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
										<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="uni_jul" HeaderText="Jul">
										<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
										<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="uni_aug" HeaderText="Aug">
										<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
										<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="uni_sep" HeaderText="Sep">
										<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
										<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="uni_oct" HeaderText="Oct">
										<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
										<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="uni_nov" HeaderText="Nov">
										<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
										<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="uni_dec" HeaderText="Dec">
										<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
										<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
									</c1webgrid:C1BoundColumn>
								</COLUMNS>
							</C1WEBGRID:C1WEBGRID></TD>
					</TR>
				</TABLE>
			</asp:Panel>
		</form>
	</body>
</HTML>
