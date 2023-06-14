<%@ Page Language="vb" AutoEventWireup="false" Codebehind="NetSales.aspx.vb" Inherits="Wyeth.Alf.NetSales" %>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../Util/printReportCtl.ascx" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>NetSales</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body xmlns:c1webgrid="urn:http://www.componentone.com/schemas/c1webgrid">
		<form id="Form1" method="post" runat="server">
			<div class="HL"><asp:label id="lblPageTitle" runat="server" Width="100%"></asp:label></div>
			<asp:panel id="FilterPanel" runat="server" cssClass="FilterPanel">
				<DIV class="noprint">
					<TABLE class="reportsFilter" cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD>
								<asp:DropDownList id="ddmonth" runat="server" Width="176px"></asp:DropDownList></TD>
							<TD>
								<asp:dropdownlist id="ddBudget" runat="server" Width="104px"></asp:dropdownlist></TD>
							<TD width="100%">
								<TABLE cellSpacing="0" cellPadding="2">
									<TR vAlign="middle">
										<TD><INPUT id="rbl_ROUND" type="radio" CHECKED value="Round" name="rbl_ROUND" RUNAT="server"></TD>
										<TD>1000</TD>
										<TD><INPUT id="rbl_EXACT" type="radio" value="Exact" name="rbl_ROUND" RUNAT="server"></TD>
										<TD>Decimals</TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
					</TABLE>
				</DIV>
				<DIV class="reportsButtonBar">
					<TABLE>
						<TR>
							<TD noWrap>
								<rep:reportData id="repData" runat="server"></rep:reportData></TD>
							<TD align="center" width="100%">
								<DIV class="noprint">
									<asp:Button id="btnGenRep" Runat="server" CssClass="button" Text="Generate Report"></asp:Button>&nbsp;
									<asp:Button id="ExportExcel" runat="server" CssClass="button" Text="Export to Excel" Visible="true"></asp:Button>&nbsp; 
									<!--<asp:Button id="btnPrint" onclick="btnPrint_onClick" runat="server" CssClass="button" Text="Print Report" Visible="true"></asp:Button>//-->
									<prt:printReportCtl id="prtControl" runat="server"></prt:printReportCtl></DIV>
							</TD>
						</TR>
					</TABLE>
				</DIV>
			</asp:panel>
			<P></P>
			<asp:panel id="ReportPanel" CssClass="ReportPanel" Runat="server">
				<TABLE id="NetSalesTable" width="100%" RUNAT="server">
					<TR>
						<TD colSpan="3">
							<C1WebGrid:C1WebGrid id="MyGrid" runat="server" Width="100%" DefaultRowHeight="22px" DefaultColumnWidth="120px"
								AutoGenerateColumns="False" GroupIndent="15px">
								<Columns>
									<c1webgrid:C1BoundColumn Visible="False" DataField="PROD_SEGMENT">
										<GroupInfo FooterText="TOTAL {0}" Position="Footer">
											<FooterStyle Wrap="False"></FooterStyle>
											<HeaderStyle Wrap="False"></HeaderStyle>
										</GroupInfo>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn Visible="False" DataField="PRGR_DESCRIPTION"></c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn HeaderText="Segment / Product" DataField="PROD_DESCRIPTION">
										<HeaderStyle CssClass="headlineSeperator"></HeaderStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Actuals" DataField="CURACT">
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
										<FooterStyle Wrap="False"></FooterStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Budget" DataField="CURBUD">
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
										<FooterStyle Wrap="False"></FooterStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn HeaderText="% vs Budget" DataField="ACTBUD">
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
										<FooterStyle Wrap="False"></FooterStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Last Year" DataField="CURACTLY">
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
										<FooterStyle Wrap="False" HorizontalAlign="Right"></FooterStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn HeaderText="% vs LY" DataField="ACTACT">
										<HeaderStyle CssClass="headlineSeperator"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
										<FooterStyle Wrap="False" HorizontalAlign="Right"></FooterStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Actuals" DataField="CURACTCOM">
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
										<FooterStyle Wrap="False" HorizontalAlign="Right"></FooterStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Budget" DataField="CUMBUD">
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
										<FooterStyle Wrap="False" HorizontalAlign="Right"></FooterStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn HeaderText="% vs Budget" DataField="ACTBUDCOM">
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
										<FooterStyle Wrap="False" HorizontalAlign="Right"></FooterStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Last Year" DataField="CURACTCOMLY">
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
										<FooterStyle Wrap="False"></FooterStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn HeaderText="% vs LY" DataField="ACTACTCOM">
										<HeaderStyle CssClass="headlineSeperator"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
										<FooterStyle Wrap="False" HorizontalAlign="Right"></FooterStyle>
									</c1webgrid:C1BoundColumn>
								</Columns>
								<FooterStyle HorizontalAlign="Right"></FooterStyle>
							</C1WebGrid:C1WebGrid></TD>
					</TR>
				</TABLE>
			</asp:panel></form>
		<SCRIPT language="javascript" src="../js/additionalHeaderRows.js"></SCRIPT>
		<SCRIPT language="javascript">
			<!--
				//hinzufügen von doppelten headlines. aufgrund von bugs im c1.webgrid
				//muss man es mittels javascript lösen. 
				createRow("myGrid");
				addTableCell("myGrid", "CURRENCY: EUR", "1", "currency", 0);
				addTableCell("myGrid", "Month to Date", "5", "", 1);
				addTableCell("myGrid", "Year to Date", "5", "", 2);
			//-->
		</SCRIPT>
	</body>
</HTML>
