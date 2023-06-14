<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../Util/printReportCtl.ascx" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TargetStatistics.aspx.vb" Inherits="Wyeth.Alf.TargetStatistics"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TargetStatistics</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table>
				<tr>
					<td><asp:dropdownlist id="ddYear" runat="server"></asp:dropdownlist></td>
					<td><asp:dropdownlist id="ddTPG" runat="server" AutoPostBack="True"></asp:dropdownlist></td>
					<td>
						<TABLE id="tblRound" cellSpacing="0" cellPadding="2" runat="server">
							<TR vAlign="middle">
								<TD><INPUT id="rbl_ROUND" type="radio" CHECKED value="Round" name="rbl_ROUND" RUNAT="server"></TD>
								<TD>1000</TD>
								<TD><INPUT id="rbl_EXACT" type="radio" value="Exact" name="rbl_ROUND" RUNAT="server"></TD>
								<TD>Decimals</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</table>
			<DIV class="reportsButtonBar">
				<TABLE>
					<TR>
						<TD noWrap><rep:reportdata id="repData" runat="server"></rep:reportdata></TD>
						<TD align="center" width="100%">
							<DIV class="noprint"><asp:button id="btnGenRep" Text="Generate Report" CssClass="button" Runat="server"></asp:button>&nbsp;
								<asp:button id="ExportExcel" runat="server" Text="Export to Excel" CssClass="button" Visible="true"></asp:button>&nbsp;
								<prt:printreportctl id="prtControl" runat="server"></prt:printreportctl></DIV>
						</TD>
					</TR>
				</TABLE>
			</DIV>
			<br>
			<asp:label id="lblTargetType" runat="server"></asp:label><br>
			<asp:label id="lblCurrency" runat="server" CssClass="currency" Visible="true" Width="408px"></asp:label><br><Br>
			<ASP:BUTTON id="btnLevel1" RUNAT="server" CSSCLASS="button_common" TEXT="Level1"></ASP:BUTTON>&nbsp;&nbsp;<ASP:BUTTON id="btnLevel2" RUNAT="server" CSSCLASS="button_common" TEXT="Level2"></ASP:BUTTON><br>
			<br>
			<C1WEBGRID:C1WEBGRID id="MyGrid" runat="server" AutoGenerateColumns="False" DefaultRowHeight="22px" DefaultColumnWidth="120px">
				<Columns>
					<c1webgrid:C1BoundColumn Visible="False" HeaderText="Sales Rep" DataField="sare_name">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<GroupInfo Position="Header" HeaderText="Custom" OutlineMode="StartCollapsed">
							<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
						</GroupInfo>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Cost Center" DataField="prod_cc_description">
						<HeaderStyle CssClass="headlineSeperator"></HeaderStyle>
						<ItemStyle Wrap="False"></ItemStyle>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Sales Value" DataField="sales_q1_value">
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Target Value" DataField="targ_q1_value">
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="%" DataField="per_q1_value">
						<HeaderStyle CssClass="headlineSeperator"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Sales Value" DataField="sales_q2_value">
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Target Value" DataField="targ_q2_value">
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="%" DataField="per_q2_value">
						<HeaderStyle CssClass="headlineSeperator"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Sales Value" DataField="sales_q3_value">
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Target Value" DataField="targ_q3_value">
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="%" DataField="per_q3_value">
						<HeaderStyle CssClass="headlineSeperator"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Sales Value" DataField="sales_q4_value">
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Target Value" DataField="targ_q4_value">
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="%" DataField="per_q4_value">
						<HeaderStyle CssClass="headlineSeperator"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Sales Value" DataField="sales_total_year">
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Target Value" DataField="targ_total_year">
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="%" DataField="per_year_value">
						<HeaderStyle CssClass="headlineSeperator"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn Aggregate="Max" HeaderText="Target Version" DataField="targ_version">
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn Aggregate="Custom" Visible="False" DataField="sare_number">
						<GroupInfo>
							<HeaderStyle Width="0px"></HeaderStyle>
						</GroupInfo>
					</c1webgrid:C1BoundColumn>
				</Columns>
				<FooterStyle HorizontalAlign="Right"></FooterStyle>
			</C1WEBGRID:C1WEBGRID></form>
		<SCRIPT language="javascript" src="../js/additionalHeaderRows.js"></SCRIPT>
		<SCRIPT language="javascript">
			<!--
				//hinzufügen von doppelten headlines. aufgrund von bugs im c1.webgrid
				//muss man es mittels javascript lösen. 
			if(document.all.MyGrid)
		 	{
				createRow("myGrid");
				addTableCell("myGrid","", "1", "currency", 0);
				addTableCell("myGrid", "Q1", "3", "", 1);
				addTableCell("myGrid", "Q2", "3", "", 2);
				addTableCell("myGrid", "Q3", "3", "", 3);
				addTableCell("myGrid", "Q4", "3", "", 4);
				addTableCell("myGrid", "Year", "3", "", 5);
			}
			//-->
		</SCRIPT>
	</body>
</HTML>
