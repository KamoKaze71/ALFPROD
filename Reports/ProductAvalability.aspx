<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../Util/printReportCtl.ascx" %>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ProductAvalability.aspx.vb" Inherits="Wyeth.Alf.ProductAvalability"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ProductAvalability</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<LINK media="print" href="../printing.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../JS/ClientScripts.js"></script>
		<asp:literal id="_clientScript" runat="server"></asp:literal>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:panel id="FilterPanel" Runat="server" CssClass="FilterPanel">
				<DIV class="noprint">
					<TABLE class="reportsFilter" cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD noWrap>
								<box:wyethtextbox id="txtStartDate" runat="server" AllowBlank="false" FriendlyName="Report Date" FieldType="DATE"
									ToolTip="Please enter a date"></box:wyethtextbox>
								<asp:Image id="StartImage" runat="server" ImageAlign="AbsMiddle" ImageUrl="/images/KALENDER.GIF"></asp:Image></TD>
							<TD noWrap>
								<box:wyethtextbox id="txtEnddate" runat="server" AllowBlank="false" FriendlyName="Report Date" FieldType="DATE"
									ToolTip="Please enter a date"></box:wyethtextbox>
								<asp:Image id="EndImage" runat="server" ImageAlign="AbsMiddle" ImageUrl="/images/KALENDER.GIF"></asp:Image></TD>
							<TD width="100%">
								<ASP:DROPDOWNLIST id="ddLine" AutoPostBack="false" RUNAT="server"></ASP:DROPDOWNLIST></TD>
						<TR>
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
			<c1webgrid:c1webgrid id="MyGrid" name="MyGrid" runat="server" width="100%" AutoGenerateColumns="False"
				GroupIndent="10px" ShowFooter="True" DefaultColumnWidth="120px" DefaultRowHeight="22px">
				<Columns>
					<c1webgrid:C1BoundColumn HeaderText="Prod. No." DataField="product_no"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Lines ordered" DataField="lines_ordered"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Lines completed" DataField="lines_complete"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Units Ordered" DataField="units_ordered"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Units complete" DataField="units_complete"></c1webgrid:C1BoundColumn>
				</Columns>
			</c1webgrid:c1webgrid></TD>
		</form>
	</body>
</HTML>
