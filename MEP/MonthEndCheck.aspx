<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="MonthEndCheck.aspx.vb" Inherits="Wyeth.Alf.MonthEndCheck"%>
<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../Util/printReportCtl.ascx" %>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>MonthEndCheck</title>
	</HEAD>
	<BODY>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<asp:literal id="_clientScript" runat="server"></asp:literal>
		<script language="JavaScript" src="../JS/ClientScripts.js"></script>
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<form id="Form1" method="post" runat="server">
			<asp:panel id="FilterPanel" runat="server" cssClass="FilterPanel">
				<DIV class="noprint">
					<TABLE class="reportsFilter" id="Table2" cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD>
								<asp:DropDownList id="ddmonth" runat="server" Width="176px"></asp:DropDownList>
								<asp:DropDownList id="ddDistribSelect" runat="server"></asp:DropDownList>
								<asp:DropDownList id="ddlineselect" runat="server"></asp:DropDownList>
								<asp:DropDownList id="ddProduct" runat="server" Width="100%" Visible="False"></asp:DropDownList></TD>
						</TR>
					</TABLE>
				</DIV>
				<DIV class="reportsButtonBar">
					<TABLE id="Table3">
						<TR>
							<TD noWrap>
								<REP:REPORTDATA id="repData" runat="server"></REP:REPORTDATA></TD>
							<TD align="center" width="100%">
								<DIV class="noprint">
									<asp:Button id="btnGenRep" Text="Generate Report" CssClass="button" Runat="server"></asp:Button>&nbsp;
									<asp:Button id="ExportExcel" runat="server" Visible="true" Text="Export to Excel" CssClass="button"></asp:Button>&nbsp;
									<PRT:PRINTREPORTCTL id="prtControl" runat="server"></PRT:PRINTREPORTCTL></DIV>
							</TD>
						</TR>
					</TABLE>
				</DIV>
			</asp:panel>
			<table id="Table1">
				<tr>
					<td>
						<asp:Label id="lblNoTCogs" runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td><C1WebGrid:C1WebGrid id="MyGridNoTCogs" runat="server" AUTOGENERATECOLUMNS="False" EnableViewState="False">
							<Columns>
								<C1WebGrid:C1BoundColumn DataField="prod_id" Visible="False"></C1WebGrid:C1BoundColumn>
								<C1WebGrid:C1BoundColumn DataField="prod_phznr" HeaderText="PhzNr"></C1WebGrid:C1BoundColumn>
								<C1WebGrid:C1BoundColumn DataField="prod_presentation" HeaderText="Presentation"></C1WebGrid:C1BoundColumn>
								<C1WebGrid:C1ButtonColumn Visible="False" Text="Button"></C1WebGrid:C1ButtonColumn>
							</Columns>
						</C1WebGrid:C1WebGrid></td>
				</tr>
				<tr>
					<td>
						<asp:Label id="LblSamProdNoAct" runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td><C1WebGrid:C1WebGrid id="MyGridSamNoAct" runat="server" AUTOGENERATECOLUMNS="False" EnableViewState="False">
							<Columns>
								<C1WebGrid:C1BoundColumn DataField="prod_id" Visible="False"></C1WebGrid:C1BoundColumn>
								<C1WebGrid:C1BoundColumn DataField="prod_phznr" HeaderText="PhzNr"></C1WebGrid:C1BoundColumn>
								<C1WebGrid:C1BoundColumn DataField="prod_presentation" HeaderText="Presentation"></C1WebGrid:C1BoundColumn>
								<C1WebGrid:C1ButtonColumn Visible="False" Text="Button"></C1WebGrid:C1ButtonColumn>
							</Columns>
						</C1WebGrid:C1WebGrid><p></p>
					</td>
				</tr>
				<tr>
					<td><asp:Label id="lblUM" runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td><C1WEBGRID:C1WEBGRID id="MyGridUM" runat="server" AutoGenerateColumns="False" EnableViewState="False">
							<Columns>
								<c1webgrid:C1BoundColumn DataField="prod_id_act" Visible="False"></c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn DataField="prod_id_sam" Visible="False"></c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn DataField="ACTPhznr" HeaderText="PhzNr"></c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn DataField="ACTPresentation" HeaderText="Presentation"></c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn DataField="ACTUnits" HeaderText="Units"></c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn DataField="Empty"></c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn DataField="SAMPhznr" HeaderText="PhzNr"></c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn DataField="SAMPresentation" HeaderText="Presentation"></c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn DataField="SAMUnits" HeaderText="Units"></c1webgrid:C1BoundColumn>
							</Columns>
						</C1WEBGRID:C1WEBGRID><p></p>
					</td>
				</tr>
				<tr>
					<td><asp:Label id="lblLogistics" runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td><C1WebGrid:C1WebGrid id="MyGridLogistics" runat="server" AUTOGENERATECOLUMNS="False" EnableViewState="False">
							<Columns>
								<C1WebGrid:C1BoundColumn DataField="TypeNumber" Visible="False"></C1WebGrid:C1BoundColumn>
								<C1WebGrid:C1BoundColumn DataField="type" Visible="False">
									<GroupInfo Position="Header" HeaderText="Total {0}"></GroupInfo>
								</C1WebGrid:C1BoundColumn>
								<C1WebGrid:C1BoundColumn DataField="code_code"></C1WebGrid:C1BoundColumn>
								<C1WebGrid:C1BoundColumn DataField="stoc_date_open" HeaderText="Date open">
									<ItemStyle Wrap="False"></ItemStyle>
								</C1WebGrid:C1BoundColumn>
								<C1WebGrid:C1BoundColumn DataField="stoc_date_accrued" HeaderText="Date accrued">
									<ItemStyle Wrap="False"></ItemStyle>
								</C1WebGrid:C1BoundColumn>
								<C1WebGrid:C1BoundColumn DataField="stoc_date_correct" HeaderText="Date correct">
									<ItemStyle Wrap="False"></ItemStyle>
								</C1WebGrid:C1BoundColumn>
								<C1WebGrid:C1BoundColumn DataField="stoc_order_number" HeaderText="Order No.">
									<ItemStyle Wrap="False"></ItemStyle>
								</C1WebGrid:C1BoundColumn>
								<C1WebGrid:C1BoundColumn DataField="stoc_invoice_number" HeaderText="Wyeth Invoice No.">
									<ItemStyle Wrap="False"></ItemStyle>
								</C1WebGrid:C1BoundColumn>
								<C1WebGrid:C1BoundColumn DataField="stoc_unit" Aggregate="Sum" HeaderText="Units">
									<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
								</C1WebGrid:C1BoundColumn>
								<C1WebGrid:C1BoundColumn DataField="stoc_invoice_value" Aggregate="Sum" HeaderText="Invoice Value">
									<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
								</C1WebGrid:C1BoundColumn>
								<C1WebGrid:C1BoundColumn DataField="stoc_accrued_value" Aggregate="Sum" HeaderText="Accrued Value">
									<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
								</C1WebGrid:C1BoundColumn>
								<C1WebGrid:C1BoundColumn DataField="stoc_diff_value" Aggregate="Sum" HeaderText="Diff WE to GIT">
									<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
								</C1WebGrid:C1BoundColumn>
								<C1WebGrid:C1BoundColumn DataField="stoc_diff_value_accrued" Aggregate="Sum" HeaderText="Diff WE to Accrual">
									<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
								</C1WebGrid:C1BoundColumn>
								<C1WebGrid:C1BoundColumn DataField="stoc_comment" HeaderText="Comment">
									<ItemStyle Wrap="False"></ItemStyle>
								</C1WebGrid:C1BoundColumn>
							</Columns>
						</C1WebGrid:C1WebGrid><p></p>
					</td>
				</tr>
				<tr>
					<td><asp:Label id="lblStockUnits" runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td>
						<C1WebGrid:C1WebGrid id="MYGRidStockUnits" runat="server" EnableViewState="False" AutoGenerateColumns="False"
							DefaultColumnWidth="120px" DefaultRowHeight="22px" width="100%" AllowAutoSort="True" ShowFooter="True">
							<COLUMNS>
								<c1webgrid:C1BoundColumn DataField="prod_id" Visible="False" SortExpression="prod_id"></c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn DataField="prgr_description" Visible="True" SortExpression="prgr_description" HeaderText="Product Group"></c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn DataField="Prod_phznr" SortExpression="Prod_phznr" HeaderText="Prod.No.">
									<HeaderStyle cssClass="headlineSeperatorLeft"></HeaderStyle>
								</c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn DataField="Prod_presentation" SortExpression="Prod_presentation" HeaderText="Presentation">
									<HeaderStyle cssClass="headlineSeperator"></HeaderStyle>
								</c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn DataField="StartBalance" SortExpression="StartBalance" HeaderText="Start">
									<HeaderStyle cssClass="headlineSeperator"></HeaderStyle>
									<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
								</c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn DataField="WE" SortExpression="WE" HeaderText="IN">
									<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
								</c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn DataField="OUT" SortExpression="OUT" HeaderText="OUT">
									<HeaderStyle cssClass="headlineSeperator"></HeaderStyle>
									<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
								</c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn DataField="OUT_FG" SortExpression="OUT_FG" HeaderText="FG">
									<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
								</c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn DataField="KORR" SortExpression="KORR" HeaderText="CORR">
									<HeaderStyle cssClass="headlineSeperator"></HeaderStyle>
									<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
								</c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn DataField="EndBalance" SortExpression="EndBalance" HeaderText="End">
									<HeaderStyle cssClass="headlineSeperator"></HeaderStyle>
									<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
								</c1webgrid:C1BoundColumn>
							</COLUMNS>
							<FOOTERSTYLE FONT-BOLD="True" HORIZONTALALIGN="Right"></FOOTERSTYLE>
						</C1WebGrid:C1WebGrid><p></p>
					</td>
				</tr>
				<tr>
					<td><asp:Label id="lblStockValues" runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td><C1WebGrid:C1WebGrid id="MyGridStockValues" runat="server" EnableViewState="False" AutoGenerateColumns="False"
							DefaultColumnWidth="120px" DefaultRowHeight="22px" width="100%" AllowAutoSort="True" ShowFooter="True">
							<Columns>
								<c1webgrid:C1BoundColumn Visible="False" SortExpression="prod_id" DataField="prod_id"></c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn DataField="prgr_description" Visible="True" SortExpression="prgr_description"></c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn SortExpression="Prod_phznr" HeaderText="Prod.No." DataField="Prod_phznr">
									<HeaderStyle CssClass="headlineSeperatorLeft"></HeaderStyle>
								</c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn SortExpression="Prod_presentation" HeaderText="Presentation" DataField="Prod_presentation">
									<HeaderStyle CssClass="headlineSeperator"></HeaderStyle>
								</c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn SortExpression="StartBalance" HeaderText="Start" DataField="StartBalance_std_cogs">
									<HeaderStyle CssClass="headlineSeperator"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn SortExpression="WE" HeaderText="IN" DataField="WE_std_cogs">
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn SortExpression="OUT" HeaderText="OUT" DataField="OUT_std_cogs">
									<HeaderStyle CssClass="headlineSeperator"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn SortExpression="OUT_FG" HeaderText="FG" DataField="OUT_FG_std_cogs">
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn SortExpression="KORR" HeaderText="CORR" DataField="KORR_std_cogs">
									<HeaderStyle CssClass="headlineSeperator"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn SortExpression="EndBalance" HeaderText="End" DataField="EndBalance_std_cogs">
									<HeaderStyle CssClass="headlineSeperator"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</c1webgrid:C1BoundColumn>
							</Columns>
							<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
						</C1WebGrid:C1WebGrid><p></p>
					</td>
				</tr>
			</table>
		</form>
	</BODY>
</HTML>
