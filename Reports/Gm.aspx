<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Gm.aspx.vb" Inherits="Wyeth.Alf.GM" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../Util/printReportCtl.ascx" %>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>GM Report</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<LINK type="text/css" href="../printing.css" rel="stylesheet" media="print">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div class="HL">
				<asp:label id="lblPageTitle" runat="server" Width="100%"></asp:label>
			</div>
			<asp:panel id="FilterPanel" runat="server">
				<DIV class="noprint">
					<TABLE class="reportsFilter" cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD>
								<asp:dropdownlist id="ddMonth" runat="server" Width="120px"></asp:dropdownlist></TD>
							<TD>
								<asp:dropdownlist id="ddBudget" runat="server" Width="120px"></asp:dropdownlist></TD>
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
									<asp:Button id="genReport" Runat="server" EnableViewState="False" CssClass="button" Text="Generate Report"></asp:Button>
									<asp:Button id="ExportExcel" runat="server" CssClass="button" Text="Export to Excel" Visible="true"></asp:Button>&nbsp;
									<prt:printReportCtl id="prtControl" runat="server"></prt:printReportCtl></DIV>
							</TD>
						</TR>
					</TABLE>
				</DIV>
				<BR>
			</asp:panel>
			<p></p>
			<asp:panel id="ReportPanel" Runat="server" cssClass="ReportPanel">
				<TABLE id="Table_gm" cellSpacing="1" cellPadding="2" width="100%" runat="server">
					<TR>
						<TD colSpan="9">
							<C1WEBGRID:C1WEBGRID id="MyGrid" runat="server" EnableViewState="False" CssClass="Grid" width="100%"
								ShowHeader="False" DefaultRowHeight="22px" DefaultColumnWidth="120px" DESIGNTIMEDRAGDROP="117" AutoGenerateColumns="False">
								<Columns>
									<c1webgrid:C1BoundColumn HeaderText="Segment / Product" DataField="GM_LINETITLE">
										<HeaderStyle Wrap="False" HorizontalAlign="Left" Width="20%" CssClass="headlineSeperator"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn HeaderText="Actuals" DataField="VALUE">
										<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="10%"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn HeaderText="Budget" DataField="BUBU_VALUE">
										<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="10%"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn HeaderText="PIA" DataField="BUBE_VALUE">
										<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="10%"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn HeaderText="Last Year" DataField="LAST_VALUE">
										<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="10%"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn HeaderText="% vs BU" DataField="BU_PERCENT">
										<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="10%"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn HeaderText="% vs PIA" DataField="BE_PERCENT">
										<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="10%"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn HeaderText="% vs LY" DataField="LAST_YEAR_PERCENT">
										<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="10%" CssClass="headlineSeperator"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn HeaderText="Actuals" DataField="VALUE_COM">
										<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="10%"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn HeaderText="PIA" DataField="VALUE_COM_BUBE">
										<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="10%"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn HeaderText="Budget" DataField="VALUE_COM_BUBU">
										<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="10%"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn HeaderText="Last Year" DataField="VALUE_COM_LAST">
										<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="10%"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn HeaderText="% vs BU" DataField="BU_COM_PERCENT">
										<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="10%"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn HeaderText="% vs PIA" DataField="BE_COM_PERCENT">
										<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn HeaderText="% vs LY" DataField="LAST_COM_YEAR_PERCENT">
										<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="10%" CssClass="headlineSeperator"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									</c1webgrid:C1BoundColumn>
								</Columns>
							</C1WEBGRID:C1WEBGRID></TD>
					</TR>
					<TR height="40">
						<TD colSpan="10"></TD>
					</TR>
					<TR>
						<TD colSpan="9">
							<C1WEBGRID:C1WEBGRID id="MyGrid_US" runat="server" EnableViewState="False" cssClass="Grid" width="100%"
								ShowHeader="False" DefaultRowHeight="22px" DefaultColumnWidth="120px" AutoGenerateColumns="False"
								cellpadding="2">
								<Columns>
									<c1webgrid:C1BoundColumn HeaderText="Segment / Product" DataField="GM_LINETITLE">
										<HeaderStyle Wrap="False" HorizontalAlign="Left" Width="20%" CssClass="headlineSeperator"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn HeaderText="Actuals" DataField="VALUE_US">
										<HeaderStyle Wrap="False" HorizontalAlign="Right" Width="10%"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn HeaderText="Budget" DataField="BUBU_VALUE_US">
										<HeaderStyle Wrap="False" HorizontalAlign="Right" Width="10%"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn HeaderText="PIA" DataField="BUBE_VALUE_US">
										<HeaderStyle Wrap="False" HorizontalAlign="Right" Width="10%"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn HeaderText="Last Year" DataField="LAST_VALUE_US">
										<HeaderStyle Wrap="False" HorizontalAlign="Right" Width="10%"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn HeaderText="% vs BU" DataField="BU_PERCENT_US">
										<HeaderStyle Wrap="False" HorizontalAlign="Right" Width="10%"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn HeaderText="% vs PIA" DataField="BE_PERCENT_US">
										<HeaderStyle Wrap="False" HorizontalAlign="Right" Width="10%"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn HeaderText="% vs LY" DataField="LAST_YEAR_PERCENT_US">
										<HeaderStyle Wrap="False" HorizontalAlign="Right" Width="10%" CssClass="headlineSeperator"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn HeaderText="Actuals" DataField="VALUE_COM_US">
										<HeaderStyle Wrap="False" HorizontalAlign="Right" Width="10%"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn HeaderText="PIA" DataField="VALUE_COM_BUBE_US">
										<HeaderStyle Wrap="False" HorizontalAlign="Right" Width="10%"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn HeaderText="Budget" DataField="VALUE_COM_BUBU_US">
										<HeaderStyle Wrap="False" HorizontalAlign="Right" Width="10%"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn HeaderText="Last Year" DataField="VALUE_COM_LAST_US">
										<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="10%"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn HeaderText="% vs. BU " DataField="BU_COM_PERCENT_US">
										<HeaderStyle Wrap="False" Width="10%"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn HeaderText="vs PIA %" DataField="BE_COM_PERCENT_US">
										<HeaderStyle Wrap="False" HorizontalAlign="Right" Width="10%"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									</c1webgrid:C1BoundColumn>
									<c1webgrid:C1BoundColumn HeaderText="% vs LY" DataField="LAST_COM_YEAR_PERCENT_US">
										<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="10%" CssClass="headlineSeperator"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									</c1webgrid:C1BoundColumn>
								</Columns>
							</C1WEBGRID:C1WEBGRID></TD>
					</TR>
					<TR>
						<TD></TD>
					</TR>
				</TABLE>
			</asp:panel></form>
		<script language="javascript" src="../js/additionalHeaderRows.js"></script>
		<script language="javascript">
			<!--
				//hinzufügen von doppelten headlines. aufgrund von bugs im c1.webgrid
				//muss man es mittels javascript lösen. 
				createRow("myGrid");
				addTableCell("myGrid", "CURRENCY: EUR", "1", "currency", 0);
				addTableCell("myGrid", "Month to Date", "5", "", 1);
				addTableCell("myGrid", "Year to Date", "5", "", 2);
				
				createRow("myGrid_US");
				addTableCell("myGrid_US", "CURRENCY: USD", "1", "currency", 0);
				addTableCell("myGrid_US", "Month to Date", "5", "", 1);
				addTableCell("myGrid_US", "Year to Date", "5", "", 2);			
			//-->
		</script>
	</body>
</HTML>
