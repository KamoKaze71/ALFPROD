<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../Util/printReportCtl.ascx" %>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Ruii.aspx.vb" Inherits="Wyeth.Alf.ruii"%>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ruii</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="JavaScript" src="../JS/ClientScripts.js"></SCRIPT>
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<asp:literal id="_clientScript" runat="server"></asp:literal>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<DIV class="HL"><ASP:LABEL id="lblPageTitle" WIDTH="100%" RUNAT="server"></ASP:LABEL></DIV>
			<ASP:PANEL id="FilterPanel" RUNAT="server" CSSCLASS="FilterPanel">
				<DIV class="noprint">
					<TABLE class="reportsFilter" cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD style="WIDTH: 124px">
								<BOX:WYETHTEXTBOX id="txtStartDate" RUNAT="server" TOOLTIP="Please enter a date" FIELDTYPE="DATE"
									FRIENDLYNAME="Report Date" ALLOWBLANK="false"></BOX:WYETHTEXTBOX>&nbsp;
								<asp:Image id="StartImage" runat="server" ImageAlign="AbsMiddle" ImageUrl="/images/KALENDER.GIF"></asp:Image></TD>
							</TD>
							<TD>
								<ASP:DROPDOWNLIST id="ddLineSelect" RUNAT="server" AutoPostBack="True"></ASP:DROPDOWNLIST>
								<asp:DropDownList id="ddProduct" runat="server" Width="100%"></asp:DropDownList></TD>
						</TR>
					</TABLE>
				</DIV>
				<DIV class="reportsButtonBar">
					<TABLE id="Table1">
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
			</ASP:PANEL><asp:panel id="ReportPanel" cssClass="ReportPanel" Runat="server">
				<C1WebGrid:C1WebGrid id="MyGrid" runat="server" Width="100%" cssclass="Grid" AutoGenerateColumns="False"
					DefaultColumnWidth="120px" DefaultRowHeight="22px">
					<Columns>
						<c1webgrid:C1BoundColumn HeaderText="Product No." DataField="PROD_PHZNR">
							<GroupInfo FooterText="Total for {0}" ImageCollapsed="" Position="Footer" ImageExpanded=""
								OutlineMode="None">
								<FooterStyle Font-Bold="True"></FooterStyle>
							</GroupInfo>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Presentation" DataField="PROD_PRESENTATION"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Units" DataField="INVL_UNITS">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Exp. Date" DataField="INVL_DATE_EXP">
							<ItemStyle Wrap="False"></ItemStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Batch No." DataField="INVL_BATCH_NUMBER"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="TCOGS Value" DataField="total_cogs">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Average" HeaderText="Hist. Use" DataField="ORPO_UNITS">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Hist. Range Div" DataField="HIST_RG_DIVIDENT">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
							<GroupInfo FooterText="{1}: {0}">
								<FooterStyle HorizontalAlign="Right"></FooterStyle>
							</GroupInfo>
						</c1webgrid:C1BoundColumn>
					</Columns>
				</C1WebGrid:C1WebGrid>
			</asp:panel></form>
	</body>
</HTML>
