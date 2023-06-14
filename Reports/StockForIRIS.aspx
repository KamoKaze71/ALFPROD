<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../Util/printReportCtl.ascx" %>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="StockForIRIS.aspx.vb" Inherits="Wyeth.Alf.StockForIRIS"%>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>StockForIRIS</title>
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
			<DIV class="HL"><ASP:LABEL id="lblPageTitle" WIDTH="100%" RUNAT="server"></ASP:LABEL></DIV>
			<ASP:PANEL id="FilterPanel" RUNAT="server" CSSCLASS="FilterPanel">
				<DIV class="noprint">
					<TABLE class="reportsFilter" cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD style="WIDTH: 126px">
								<BOX:WYETHTEXTBOX id="txtStartDate" RUNAT="server" ALLOWBLANK="false" FRIENDLYNAME="Report Date" FIELDTYPE="DATE"
									TOOLTIP="Please enter a date"></BOX:WYETHTEXTBOX>&nbsp;
								<asp:Image id="StartImage" runat="server" ImageUrl="/images/KALENDER.GIF" ImageAlign="AbsMiddle"></asp:Image></TD>
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
									<ASP:BUTTON id="btnGenRep" RUNAT="server" CSSCLASS="button" TEXT="Generate Report"></ASP:BUTTON>&nbsp;
									<ASP:BUTTON id="ExportExcel" RUNAT="server" CSSCLASS="button" TEXT="Export to Excel" VISIBLE="true"></ASP:BUTTON>&nbsp;
									<prt:printReportCtl id="prtControl" runat="server"></prt:printReportCtl></DIV>
							</TD>
						</TR>
					</TABLE>
				</DIV>
			</ASP:PANEL><asp:panel id="ReportPanel" CssClass="ReportPanel" Runat="server">
				<C1WEBGRID:C1WEBGRID id="MyGrid" runat="server" Width="100%" DefaultRowHeight="22px" DefaultColumnWidth="120px"
					AutoGenerateColumns="False" AllowSorting="True" AllowAutoSort="True">
					<Columns>
						<C1WebGrid:C1BoundColumn SortExpression="PROD_PHZNR" HeaderText="Prod. No." DataField="PROD_PHZNR"></C1WebGrid:C1BoundColumn>
						<C1WebGrid:C1BoundColumn SortExpression="PROD_PRESENTATION" HeaderText="Presentation" DataField="PROD_PRESENTATION">
							<GroupInfo Position="Header" HeaderText="{0} "></GroupInfo>
						</C1WebGrid:C1BoundColumn>
						<C1WebGrid:C1BoundColumn Aggregate="Sum" SortExpression="INVL_UNITS" HeaderText="Units" DataField="INVL_UNITS">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</C1WebGrid:C1BoundColumn>
						<C1WebGrid:C1BoundColumn SortExpression="INVL_BATCH_NUMBER" HeaderText="Batch No." DataField="INVL_BATCH_NUMBER"></C1WebGrid:C1BoundColumn>
						<C1WebGrid:C1BoundColumn SortExpression="INVL_DATE_EXP" HeaderText="Exp. Date" DataField="INVL_DATE_EXP"></C1WebGrid:C1BoundColumn>
						<C1WebGrid:C1TemplateColumn HeaderText="Sellable">
							<HeaderStyle Width="0%"></HeaderStyle>
							<ItemTemplate>
								<asp:CheckBox id="CheckQuarantine" runat="server"></asp:CheckBox>
							</ItemTemplate>
						</C1WebGrid:C1TemplateColumn>
						<C1WebGrid:C1BoundColumn Visible="False" SortExpression="CODE_CODE" DataField="CODE_CODE"></C1WebGrid:C1BoundColumn>
						<C1WebGrid:C1BoundColumn Visible="False" SortExpression="CODE_DESCRIPTION" DataField="CODE_DESCRIPTION"></C1WebGrid:C1BoundColumn>
					</Columns>
				</C1WEBGRID:C1WEBGRID>
			</asp:panel></form>
	</body>
</HTML>
