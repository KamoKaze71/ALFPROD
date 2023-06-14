<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../Util/printReportCtl.ascx" %>
<%@ Page Language="vb" AutoEventWireup="true" Codebehind="DailySales.aspx.vb" Inherits="Wyeth.Alf.DailySails" uiCulture="de-AT"%>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>DailySails</title>
		<meta content="True" name="vs_snapToGrid">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<LINK media="print" href="../printing.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../JS/ClientScripts.js"></script>
		<asp:literal id="_clientScript" runat="server"></asp:literal>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div class="HL"><asp:label id="lblPageTitle" runat="server"></asp:label></div>
			<asp:panel id="FilterPanel" Runat="server" CssClass="FilterPanel">
				<DIV class="noprint">
					<TABLE class="reportsFilter" cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD noWrap>
								<box:wyethtextbox id="txtStartDate" runat="server" ToolTip="Please enter a date" FieldType="DATE"
									FriendlyName="Report Date" AllowBlank="false"></box:wyethtextbox>&nbsp;
								<asp:Image id="StartImage" runat="server" ImageUrl="/images/KALENDER.GIF" ImageAlign="AbsMiddle"></asp:Image></TD>
							<TD>
								<asp:dropdownlist id="ddBudget" runat="server" Width="104px"></asp:dropdownlist></TD>
							<TD width="100%">
								<TABLE cellSpacing="0" cellPadding="2">
									<TR vAlign="middle">
										<TD><INPUT id="rbl_ROUND" type="radio" CHECKED value="Round" name="rbl_ROUND" runat="server"></TD>
										<TD>1000</TD>
										<TD><INPUT id="rbl_EXACT" type="radio" value="Exact" name="rbl_ROUND" runat="server"></TD>
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
									<asp:Button id="btnGenRep" CssClass="button" Runat="server" Text="Generate Report"></asp:Button>&nbsp;
									<asp:Button id="ExportExcel" runat="server" CssClass="button" Text="Export to Excel" Visible="true"></asp:Button>&nbsp;
									<prt:printReportCtl id="prtControl" runat="server"></prt:printReportCtl></DIV>
							</TD>
						</TR>
					</TABLE>
				</DIV>
			</asp:panel>
			<p></p>
			<TABLE id="DailySalesTable" cellSpacing="1" width="100%" runat="server">
				<TR>
					<TD colSpan="4">
						<c1webgrid:c1webgrid id="MyGrid" name="MyGrid" runat="server" width="100%" AutoGenerateColumns="False"
							GroupIndent="10px" ShowFooter="True" DefaultColumnWidth="120px" DefaultRowHeight="22px">
							<Columns>
								<c1webgrid:C1BoundColumn Visible="False" DataField="PROD_SEGMENT">
									<HeaderStyle Wrap="False"></HeaderStyle>
									<ItemStyle Wrap="False"></ItemStyle>
									<FooterStyle Font-Bold="True"></FooterStyle>
									<GroupInfo FooterText="TOTAL {0}" ImageCollapsed="" Position="Footer" HeaderText="{0}" ImageExpanded=""
										OutlineMode="None"></GroupInfo>
								</c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn Visible="False" DataField="PRGR_DESCRIPTION">
									<HeaderStyle Wrap="False"></HeaderStyle>
									<ItemStyle Wrap="False"></ItemStyle>
									<FooterStyle Font-Bold="True"></FooterStyle>
								</c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn HeaderText="Segement / Product" DataField="PROD_DESCRIPTION">
									<HeaderStyle Wrap="False" Width="150px" CssClass="headlineSeperator"></HeaderStyle>
									<ItemStyle Wrap="False"></ItemStyle>
								</c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Actuals" DataField="ActDay">
									<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
								</c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Budget" DataField="BudDay">
									<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
								</c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="% vs BU " DataField="ActBudDay">
									<HeaderStyle Wrap="False" HorizontalAlign="Center" CssClass="headlineSeperator"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
								</c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Actuals" DataField="TotalCumDayAct">
									<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
								</c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Budget" DataField="BudCumDay">
									<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
								</c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText=" % vs BU" DataField="ActBudMtd">
									<HeaderStyle Wrap="False" HorizontalAlign="Center" CssClass="headlineSeperator"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
								</c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Projected" DataField="ProMonth">
									<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
								</c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Budget" DataField="BUDGET">
									<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
								</c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="% vs BU" DataField="ProBudMonth">
									<HeaderStyle Wrap="False" HorizontalAlign="Center" CssClass="headlineSeperator"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
								</c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Actuals" DataField="ActYTD">
									<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
								</c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Budget" DataField="BudYTD">
									<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
								</c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn HeaderText="% vs BU" DataField="ActBudYTD">
									<HeaderStyle Wrap="False" HorizontalAlign="Center" CssClass="headlineSeperator"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
								</c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Projected" DataField="ProYear">
									<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
								</c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Budget" DataField="BudYear">
									<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
								</c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn HeaderText="% vs BU" DataField="ProBudYear">
									<HeaderStyle Wrap="False" HorizontalAlign="Center" CssClass="headlineSeperator"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
								</c1webgrid:C1BoundColumn>
							</Columns>
						</c1webgrid:c1webgrid></TD>
				</TR>
			</TABLE>
		</form>
		<script language="javascript" src="../js/additionalHeaderRows.js"></script>
		<script language="javascript">
			<!--
				//hinzufügen von doppelten headlines. aufgrund von bugs im c1.webgrid
				//muss man es mittels javascript lösen. 
		 	if(document.all.MyGrid)
		 	{
				createRow("myGrid");
				addTableCell("myGrid", "CURRENCY: EUR", "1", "currency", 0);
				addTableCell("myGrid", "Day", "3", "", 1);
				addTableCell("myGrid", "Month to Date", "3", "", 2);
				addTableCell("myGrid", "Month", "3", "", 3);
				addTableCell("myGrid", "Year To Date", "3", "", 4);
				addTableCell("myGrid", "Year", "3", "", 5);
				}
			//-->
		</script>
	</body>
</HTML>
