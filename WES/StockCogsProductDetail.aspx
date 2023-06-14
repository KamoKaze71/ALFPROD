<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="StockCogsProductDetail.aspx.vb" Inherits="Wyeth.Alf.StockCogsProductDetail"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>StockCogsProductDetail</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK HREF="../Styles.css" TYPE="text/css" REL="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<ASP:PANEL ID="FilterPanel" CSSCLASS="FilterPanel" RUNAT="server">
				<TABLE>
					<TR>
						<TD>
							<REP:REPORTDATA id="repData" RUNAT="server"></REP:REPORTDATA></TD>
					</TR>
				</TABLE>
			</ASP:PANEL>
			<ASP:PANEL ID="GridPanelFA" CSSCLASS="GridPanel" runat="server">
				<C1WEBGRID:C1WEBGRID id="myGrid" runat="server" EnableViewState="False" width="100%" DefaultColumnWidth="120px"
					DefaultRowHeight="22px" ShowFooter="True" AllowSorting="True" AutoGenerateColumns="False">
					<COLUMNS>
						<c1webgrid:C1BoundColumn DataField="tran_date" SortExpression="tran_date" HeaderText="Date">
							<ITEMSTYLE WRAP="False"></ITEMSTYLE>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_phznr" SortExpression="prod_phznr" HeaderText="Product No."></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_presentation" SortExpression="prod_presentation" HeaderText="Presentation"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="order_no" SortExpression="order_no" HeaderText="Order No."></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="customer_no" SortExpression="customer_no" HeaderText="Customer No."></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="customer_name" SortExpression="customer_name" HeaderText="Customer Name"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="units" SortExpression="units" HeaderText="Units"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="STDCOGS" SortExpression="STDCOGS" HeaderText="Standard Unit Costs"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="TOTALCOGS" SortExpression="TOTALCOGS" HeaderText="Total Unit Costs"></c1webgrid:C1BoundColumn>
					</COLUMNS>
				</C1WEBGRID:C1WEBGRID>
			</ASP:PANEL>
			<ASP:PANEL ID="GridPanelStock" RUNAT="server" CSSCLASS="GridPanel">
				<TABLE>
					<TR>
						<TD></TD>
						<TD></TD>
					</TR>
					<TR>
						<TD></TD>
						<TD></TD>
					</TR>
					<TR>
						<TD></TD>
						<TD></TD>
					</TR>
				</TABLE>
				<C1WebGrid:C1WebGrid id="MyGridStock" runat="server" EnableViewState="False" width="100%" DefaultColumnWidth="120px"
					DefaultRowHeight="22px" ShowFooter="True" AllowSorting="True" AutoGenerateColumns="False">
					<COLUMNS>
						<c1webgrid:C1BoundColumn DataField="tran_date" SortExpression="tran_date" HeaderText="Date">
							<ITEMSTYLE WRAP="False"></ITEMSTYLE>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_phznr" SortExpression="prod_phznr" HeaderText="Product No."></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_presentation" SortExpression="prod_presentation" HeaderText="Presentation"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="ORDER_NO" SortExpression="ORDER_NO" HeaderText="Order No."></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="UNITS" SortExpression="UNITS" HeaderText="Units"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="STDCOGS" SortExpression="STDCOGS" HeaderText="Standard Unit Costs"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="TOTALCOGS" SortExpression="TOTALCOGS" HeaderText="Total Unit Costs"></c1webgrid:C1BoundColumn>
					</COLUMNS>
				</C1WebGrid:C1WebGrid>
			</ASP:PANEL>
		</form>
	</body>
</HTML>
