<%@ Page Language="vb" AutoEventWireup="false" Codebehind="StockForGDW.aspx.vb" Inherits="Wyeth.Alf.StockForGDW"%>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../Util/printReportCtl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>StockForGDW</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<LINK media="print" href="../printing.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../JS/ClientScripts.js"></script>
		<asp:literal id="_clientScript" runat="server"></asp:literal>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:panel id="FilterPanel" CssClass="FilterPanel" Runat="server">
				<DIV class="noprint">
					<TABLE class="reportsFilter" cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD noWrap>
								<box:wyethtextbox id="txtStartDate" runat="server" AllowBlank="false" FriendlyName="Report Date" FieldType="DATE"
									ToolTip="Please enter a date"></box:wyethtextbox>
								<asp:Image id="StartImage" runat="server" ImageAlign="AbsMiddle" ImageUrl="/images/KALENDER.GIF"></asp:Image></TD>
							<TD width="100%">
								<ASP:DROPDOWNLIST id="ddDist" RUNAT="server" AutoPostBack="false"></ASP:DROPDOWNLIST></TD>
						</TR>
					</TABLE>
				</DIV>
				<DIV class="reportsButtonBar">
					<TABLE>
						<TR>
							<TD noWrap>
								<REP:REPORTDATA id="repData" runat="server"></REP:REPORTDATA></TD>
							<TD align="center" width="100%">
								<DIV class="noprint">
									<asp:Button id="btnGenRep" CssClass="button" Runat="server" Text="Generate Report"></asp:Button>&nbsp;
									<asp:Button id="ExportExcel" runat="server" CssClass="button" Text="Export to Excel" Visible="true"></asp:Button>&nbsp;
									<PRT:PRINTREPORTCTL id="prtControl" runat="server"></PRT:PRINTREPORTCTL></DIV>
							</TD>
						</TR>
					</TABLE>
				</DIV>
			</asp:panel>
			<p></p>
			<c1webgrid:c1webgrid id="MyGrid" runat="server" DefaultRowHeight="22px" DefaultColumnWidth="120px" ShowFooter="True"
				GroupIndent="10px" AutoGenerateColumns="False" width="100%" name="MyGrid">
				<Columns>
					<c1webgrid:C1BoundColumn HeaderText="Product No." DataField="product_no"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Saleable" DataField="saleable"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="WIP" DataField="WIP"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Non Saleable" DataField="non_saleable"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="LOC Unit / STD: Cost" DataField="loc_unit_std_cogs"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="In Transit" DataField="in_transit"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="In Transit Unit Cost" DataField="in_transit_unit_cost"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Product Presentation" DataField="presentation"></c1webgrid:C1BoundColumn>
				</Columns>
			</c1webgrid:c1webgrid></form>
	</body>
</HTML>
