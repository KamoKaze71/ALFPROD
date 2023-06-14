<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="OUT_FG.aspx.vb" Inherits="Wyeth.Alf.OUT_FG"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>Sales FreeGoods</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<LINK type="text/css" href="../printing.css" rel="stylesheet" media="print">
  </HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div class="HL">
				<asp:Label id="lblPageTitle" runat="server"></asp:Label>
			</div>
			<p></p>
			<DIV class="reportsButtonBar">
				<TABLE>
					<TR>
						<TD nowrap><REP:REPORTDATA ID="repData" RUNAT="server"></REP:REPORTDATA></TD>
						<td width="100%" align="center">
							<DIV class="noprint">
								<ASP:BUTTON ID="ExportExcel" RUNAT="server" CSSCLASS="button" TEXT="Export to Excel" VISIBLE="true"></ASP:BUTTON>&nbsp;
								<BUTTON class="button" onclick="javascript:window.print();" type="button">Print</BUTTON>&nbsp;
								<BUTTON class="button" onclick="javascript:window.close();" type="button">Close 
									Window</BUTTON>
							</DIV>
						</td>
					</TR>
				</TABLE>
			</DIV>
			<p></p>
			<asp:Panel ID="Gridpanel" CssClass="GridPanel" Runat="server"><C1WebGrid:C1WebGrid id=MyGrid runat="server" AllowAutoSort="True" width="100%" AutoGenerateColumns="False" ShowFooter="True">
					<COLUMNS>
						<c1webgrid:C1BoundColumn DataField="tran_Date" SortExpression="tran_Date" HeaderText="Date">
							<ITEMSTYLE WRAP="False"></ItemStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="order_no" SortExpression="order_no" HeaderText="Order No."></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="code_code" SortExpression="code_code" HeaderText="Code"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="customer_no" SortExpression="customer_no" HeaderText="Customer No."></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="customer_name" SortExpression="customer_name" HeaderText="Customer Name"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="units" SortExpression="units" HeaderText="Units"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="STDCOGS" SortExpression="STDCOGS" HeaderText="Standard Unit Costs"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="TOTALCOGS" SortExpression="TOTALCOGS" HeaderText="Total Unit Costs"></c1webgrid:C1BoundColumn>
					</Columns>
				</C1WebGrid:C1WebGrid>
			</asp:Panel>
		</form>
	</body>
</HTML>
