<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid, Version=1.1.20034.32, Culture=neutral, PublicKeyToken=589f1fc067ff4031" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DailySails.aspx.vb" Inherits="Wyeth.Alf.DailySails" %>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
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
		<LINK type="text/css" href="../printing.css" rel="stylesheet" media="print">
		<script language="JavaScript" src="../JS/ClientScripts.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div class="HL">
				<asp:label id="lblPageTitle" runat="server"></asp:label>
			</div>
			<asp:panel id="FilterPanel" CssClass="FilterPanel" Runat="server">
				<DIV class="noprint">
					<TABLE class="reportsFilter" cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD noWrap>
								<box:wyethtextbox id="txtStartDate" runat="server" AllowBlank="false" FriendlyName="Report Date" FieldType="DATE"
									ToolTip="Please enter a date"></box:wyethtextbox>&nbsp;<A onclick="OpenDatePopUp('Form1.txtStartDate');" href=#><IMG src="/images/kalender.gif" align="absMiddle" border="0"></A>
							</TD>
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
								<rep:reportData id="repData" runat="server"></rep:reportData>
							</TD>
							<TD align="center" width="100%">
								<DIV class="noprint">
									<asp:Button id="btnGenRep" Runat="server" CssClass="button" Text="Generate Report"></asp:Button>&nbsp;
									<asp:Button id="ExportExcel" runat="server" CssClass="button" Text="Export to Excel" Visible="true"></asp:Button>&nbsp;<BUTTON class="button" onclick="javascript:window.print();" type="button">Print 
										Report</BUTTON>
								</DIV>
							</TD>
						</TR>
					</TABLE>
				</DIV>
			</asp:panel>
			<p></p>
			<asp:panel id="ReportPanel" CssClass="ReportPanel" Runat="server">
				<TABLE id="DailySalesTable" cellSpacing="1" width="100%" runat="server">
					<TR>
						<TD colSpan="4">
							<c1webgrid:c1webgrid id="MyGrid" runat="server" width="100%" AutoGenerateColumns="False" GroupIndent="10px"
								ShowFooter="True" EnableViewState="False" DefaultColumnWidth="120px" DefaultRowHeight="22px">
								<Columns>
									<c1webgrid:C1BoundColumn DataField="PROD_SEGMENT" Visible="False">
										<HeaderStyle wrap=false></HeaderStyle>
										<FooterStyle Font-Bold="True"></FooterStyle>
										<GroupInfo FooterText="TOTAL {0}" ImageCollapsed="" Position="Footer" HeaderText="{0}" ImageExpanded=""
											OutlineMode="None"></GroupInfo>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="PRGR_DESCRIPTION" Visible="False">
										<HeaderStyle Wrap="False"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
										<FooterStyle Font-Bold="True"></FooterStyle>
										<GroupInfo FooterText="TOTAL {0}" Position="Footer" HeaderText="{0}" OutlineMode="None"></GroupInfo>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="PROD_DESCRIPTION" HeaderText="Segement / Product">
										<HeaderStyle Wrap="False" cssClass="headlineSeperator" width="150px"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="ActDay" Aggregate="Sum" HeaderText="Actuals">
										<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="BudDay" Aggregate="Sum" HeaderText="Budget">
										<HeaderStyle Wrap="False" HorizontalAlign="Center" cssClass="headlineSeperator"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="TotalCumDayAct" Aggregate="Sum" HeaderText="Actuals">
										<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="BudCumDay" Aggregate="Sum" HeaderText="Budget">
										<HeaderStyle Wrap="False" HorizontalAlign="Center" cssClass="headlineSeperator"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="ProMonth" Aggregate="Sum" HeaderText="Projected">
										<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn DataField="BUDGET" Aggregate="Sum" HeaderText="Budget">
										<HeaderStyle Wrap="False" HorizontalAlign="Center" cssClass="headlineSeperator"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</c1webgrid:C1BoundColumn>
								</Columns>
							</c1webgrid:c1webgrid></TD>
					</TR>
				</TABLE>
			</asp:panel></form>
			
			<script language=javascript src="../js/additionalHeaderRows.js"></script>
			<script language=javascript>
			<!--
				//hinzufügen von doppelten headlines. aufgrund von bugs im c1.webgrid
				//muss man es mittels javascript lösen. 
				createRow("myGrid");
				addTableCell("myGrid", "CURRENCY: EUR", "1", "currency", 0);
				addTableCell("myGrid", "Day", "2", "", 1);
				addTableCell("myGrid", "Month to Date", "2", "", 2);
				addTableCell("myGrid", "Month", "2", "", 3);
			//-->
			</script>
	</body>
</HTML>
