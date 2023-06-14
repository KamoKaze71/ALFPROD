<%@ Page Language="vb" AutoEventWireup="false" Codebehind="BackOrders_Orders.aspx.vb" Inherits="Wyeth.Alf.BackOrders_Orders"%>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../Util/printReportCtl.ascx" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Inventory Batch Report</title>
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
			<ASP:PANEL id="FilterPanel" CSSCLASS="FilterPanel" RUNAT="server">
<DIV class="noprint">
					<TABLE class="reportsFilter" cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD>
								<BOX:WYETHTEXTBOX id="txtStartDate" RUNAT="server" ALLOWBLANK="false" FRIENDLYNAME="Report Date" FIELDTYPE="DATE"
									TOOLTIP="Please enter a start date"></BOX:WYETHTEXTBOX></TD>
							<TD>
								<asp:Image id="StartImage" runat="server" ImageUrl="/images/KALENDER.GIF" ImageAlign="AbsMiddle"></asp:Image></TD>
							<TD>&nbsp;
								<BOX:WYETHTEXTBOX id="txtEndDate" RUNAT="server" ALLOWBLANK="false" FRIENDLYNAME="Report Date" FIELDTYPE="DATE"
									TOOLTIP="Please enter a end date"></BOX:WYETHTEXTBOX></TD>
							<TD>
								<asp:Image id="endImage" runat="server" ImageUrl="/images/KALENDER.GIF" ImageAlign="AbsMiddle"></asp:Image></TD>
							<TD noWrap width="100%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
									<PRT:PRINTREPORTCTL id="prtControl" runat="server"></PRT:PRINTREPORTCTL></DIV>
							</TD>
						</TR>
					</TABLE>
				</DIV><BR>
<ASP:BUTTON id="btnLevel1" RUNAT="server" CSSCLASS="button_common" TEXT="Level1"></ASP:BUTTON>&nbsp; 
<ASP:BUTTON id="btnLevel2" RUNAT="server" CSSCLASS="button_common" TEXT="Level2"></ASP:BUTTON>&nbsp; 
<BR>
			</ASP:PANEL><asp:panel id="ReportPanel" Runat="server" CssClass="ReportPanel">
				<C1WEBGRID:C1WEBGRID id="MyGrid" runat="server" DefaultRowHeight="22px" DefaultColumnWidth="120px" AutoGenerateColumns="False"
					AllowAutoSort="True" Width="100%">
					<Columns>
						<c1webgrid:C1BoundColumn Visible="False" SortExpression="ORHE_DATE" HeaderText="Date" DataField="ORHE_DATE">
							<GroupInfo Position="Header" HeaderText="{1}: {0}" OutlineMode="StartCollapsed"></GroupInfo>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn SortExpression="CUST_NAME" HeaderText="Customer" DataField="CUST_NAME"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn SortExpression="ORHE_ORDER_NUMBER" HeaderText="Order No." DataField="ORHE_ORDER_NUMBER"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" SortExpression="Orders_ordered" HeaderText="Orders ordered" DataField="Orders_ordered">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" SortExpression="Orders_uncomplete" HeaderText="Orders incomplete"
							DataField="Orders_uncomplete">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn SortExpression="percentage_complete" HeaderText="%" DataField="percentage_complete">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
							<FooterStyle Wrap="False" HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
					</Columns>
				</C1WEBGRID:C1WEBGRID>
			</asp:panel></form>
	</body>
</HTML>
