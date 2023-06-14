<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="StockProduct.aspx.vb" Inherits="Wyeth.Alf.StockProduct"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>Stock Movement</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<LINK type="text/css" href="../printing.css" rel="stylesheet" media="print">
		<script language="JavaScript" src="../JS/ClientScripts.js" type="text/javascript">	</script>
</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:label id="lblPageTitle" runat="server" CssClass="HL" Width="100%"></asp:label>
			<p></p>
			<DIV class="reportsButtonBar">
				<table>
					<tr>
						<td nowrap><REP:REPORTDATA ID="repData" RUNAT="server"></REP:REPORTDATA></td>
						<td width="100%" align="center">
							<DIV class="noprint"><ASP:BUTTON id=ExportExcel RUNAT="server" CSSCLASS="button" TEXT="Export to Excel" VISIBLE="true"></ASP:BUTTON>
								<BUTTON class="button" onclick="javascript:window.print();" type="button">Print</BUTTON>&nbsp;
								<BUTTON class="button" onclick="javascript:window.close();" type="button">
									Close Window</BUTTON>
							</DIV>
						</td>
					</tr>
				</table>
			</DIV>
			<p></p>
			<asp:Panel ID="GridPanel" cssclass="Gridpanel" Runat="server"><C1WEBGRID:C1WEBGRID id=MYGRid runat="server" ShowFooter="True" AllowAutoSort="True" width="100%" AutoGenerateColumns="False" DefaultColumnWidth="120px" DefaultRowHeight="22px">
					<COLUMNS>
						<c1webgrid:C1BoundColumn DataField="Tran_date" SortExpression="tran_date" HeaderText="Date"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_id" Visible="False" SortExpression="prod_id"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Start"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="WE" SortExpression="WE" HeaderText="IN"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="OUT" SortExpression="OUT" HeaderText="OUT"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="OUT_FG" SortExpression="OUT_FG" HeaderText="FG"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="KORR" SortExpression="KORR" HeaderText="CORR"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="UM" SortExpression="UM" HeaderText="UM"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="invl_units" SortExpression="invl_units" HeaderText="End"></c1webgrid:C1BoundColumn>
					</COLUMNS>
				</C1WEBGRID:C1WEBGRID>
			</asp:Panel>
		</form>
	</body>
</HTML>
