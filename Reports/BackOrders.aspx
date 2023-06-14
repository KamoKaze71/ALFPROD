<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../Util/printReportCtl.ascx" %>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="BackOrders.aspx.vb" Inherits="Wyeth.Alf.BackOrders"%>
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
								<BOX:WYETHTEXTBOX id="txtStartDate" RUNAT="server" TOOLTIP="Please enter a start date" FIELDTYPE="DATE"
									FRIENDLYNAME="Report Date" ALLOWBLANK="false"></BOX:WYETHTEXTBOX></TD>
							<TD>
								<asp:Image id="StartImage" runat="server" ImageAlign="AbsMiddle" ImageUrl="/images/KALENDER.GIF"></asp:Image></TD>
							<TD>&nbsp;
								<BOX:WYETHTEXTBOX id="txtEndDate" RUNAT="server" TOOLTIP="Please enter a end date" FIELDTYPE="DATE"
									FRIENDLYNAME="Report Date" ALLOWBLANK="false"></BOX:WYETHTEXTBOX></TD>
							<TD>
								<asp:Image id="endImage" runat="server" ImageAlign="AbsMiddle" ImageUrl="/images/KALENDER.GIF"></asp:Image></TD>
							<TD noWrap width="100%">&nbsp;&nbsp;
								<asp:DropDownList id="ddproduct" runat="server" Width="328px"></asp:DropDownList>&nbsp;&nbsp;
								<ASP:DROPDOWNLIST id="ddLineSelect" RUNAT="server" AutoPostBack="True"></ASP:DROPDOWNLIST>&nbsp;&nbsp;
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
									&nbsp;<INPUT class="button" style="WIDTH: 83px; HEIGHT: 20px" onclick="self.print();" type="button"
										value="Print">
									<PRT:PRINTREPORTCTL id="prtControl" runat="server" visible="false"></PRT:PRINTREPORTCTL></DIV>
							</TD>
						</TR>
					</TABLE>
				</DIV><BR>
<ASP:BUTTON id="btnLevel1" RUNAT="server" CSSCLASS="button_common" TEXT="Level1"></ASP:BUTTON>&nbsp; 
<ASP:BUTTON id="btnLevel2" RUNAT="server" CSSCLASS="button_common" TEXT="Level2"></ASP:BUTTON>&nbsp; 
<ASP:BUTTON id="btnLevel3" RUNAT="server" CSSCLASS="button_common" TEXT="Level3"></ASP:BUTTON><BR>
			</ASP:PANEL><asp:panel id="ReportPanel" Runat="server" CssClass="ReportPanel">
				<C1WEBGRID:C1WEBGRID id="MyGrid" runat="server" Width="100%" AutoGenerateColumns="False" DefaultColumnWidth="120px"
					DefaultRowHeight="22px" EnableViewState="False">
					<Columns>
						<c1webgrid:C1BoundColumn Visible="False" SortExpression="PROD_DESCRIPTION" HeaderText="Product" DataField="PROD_DESCRIPTION">
							<GroupInfo FooterText="{1}: {0}" Position="Header" HeaderText="{1}: {0}" OutlineMode="StartCollapsed"></GroupInfo>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Visible="False" SortExpression="PROD_PRESENTATION" HeaderText="Presentation" DataField="PROD_PRESENTATION">
							<GroupInfo Position="Header" HeaderText="{1}: {0}"></GroupInfo>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn SortExpression="PROD_PHZNR" HeaderText="Prod. No." DataField="PROD_PHZNR"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn SortExpression="orhe_date" HeaderText="Date" DataField="orhe_date">
							<ItemStyle Wrap="False"></ItemStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn SortExpression="orhe_order_number" HeaderText="Order No." DataField="orhe_order_number"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn SortExpression="CUST_NAME" HeaderText="Customer" DataField="CUST_NAME"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" SortExpression="units_ordered" HeaderText="Units ordered" DataField="units_ordered">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" SortExpression="values_ordered" HeaderText="Values ordered" DataField="values_ordered">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" SortExpression="units_uncomplete" HeaderText="Units incomplete "
							DataField="units_uncomplete">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" SortExpression="values_uncomplete" HeaderText="Values incomplete"
							DataField="values_uncomplete">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" SortExpression="Lines_shipped_ordered" HeaderText="Lines ordered "
							DataField="Lines_shipped_ordered">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" SortExpression="Lines_shipped_uncomplete" HeaderText="Lines incomplete"
							DataField="Lines_shipped_uncomplete">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn SortExpression="percentage_lines" HeaderText="% Lines completed" DataField="percentage_lines">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn SortExpression="percentage_units" HeaderText="% Units or Values completed" DataField="percentage_units">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
					</Columns>
				</C1WEBGRID:C1WEBGRID>
			</asp:panel></form>
	</body>
</HTML>
