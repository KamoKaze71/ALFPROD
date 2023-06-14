<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="WWSReport.aspx.vb" Inherits="Wyeth.Alf.WWSReport"%>
<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../Util/printReportCtl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>WWS</title>
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
				<asp:Label id="lblPageTitle" runat="server" />
			</div>
			<asp:Panel Runat="server" CssClass="FilterPanel" id="FilterPanel">
				<DIV class="noprint">
					<TABLE class="reportsFilter" cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD>
								<asp:dropdownlist id="ddMonth" runat="server" Width="173px"></asp:dropdownlist></TD>
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
									<asp:Button id="genReport" CssClass="button" Runat="server" EnableViewState="False" Text="Generate Report"></asp:Button>
									<asp:Button id="btn_wwsdownload" runat="server" CssClass="button" Width="143px" Text="Export To File"></asp:Button>&nbsp;
									<prt:printReportCtl id="prtControl" runat="server"></prt:printReportCtl></DIV>
							</TD>
						</TR>
					</TABLE>
				</DIV>
			</asp:Panel>
			<p></p>
			<asp:Panel Runat="server" CssClass="ReportPanel" id="ReportPanel">
				<C1WEBGRID:C1WEBGRID id="MyGrid" runat="server" Width="100%" ShowFooter="True" AutoGenerateColumns="False"
					DefaultColumnWidth="120px" DefaultRowHeight="22px">
					<Columns>
						<C1WEBGRID:C1BoundColumn HeaderText="Ctry From" DataField="CtryFrom"></C1WEBGRID:C1BoundColumn>
						<C1WEBGRID:C1BoundColumn HeaderText="Ctry To" DataField="CtryTo"></C1WEBGRID:C1BoundColumn>
						<C1WEBGRID:C1BoundColumn HeaderText="Date" DataField="Datum"></C1WEBGRID:C1BoundColumn>
						<C1WEBGRID:C1BoundColumn HeaderText="Currency" DataField="CURR_CODE"></C1WEBGRID:C1BoundColumn>
						<C1WEBGRID:C1BoundColumn HeaderText="Profit Center" DataField="ProfitCenter"></C1WEBGRID:C1BoundColumn>
						<C1WEBGRID:C1BoundColumn HeaderText="Local Item No." DataField="PROD_PHZNR"></C1WEBGRID:C1BoundColumn>
						<C1WEBGRID:C1BoundColumn HeaderText="Country" DataField="CTRY_CODE"></C1WEBGRID:C1BoundColumn>
						<C1WEBGRID:C1BoundColumn HeaderText="Mfg. Location" DataField="PROD_MFGLOCATION"></C1WEBGRID:C1BoundColumn>
						<C1WEBGRID:C1BoundColumn HeaderText="Type" DataField="DATATYPE"></C1WEBGRID:C1BoundColumn>
						<C1WEBGRID:C1BoundColumn HeaderText="Units" DataField="UNITS">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</C1WEBGRID:C1BoundColumn>
						<C1WEBGRID:C1BoundColumn HeaderText="Sales Value" DataField="VALUE">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</C1WEBGRID:C1BoundColumn>
						<C1WEBGRID:C1BoundColumn HeaderText="Total COGS" DataField="total_cogs">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</C1WEBGRID:C1BoundColumn>
					</Columns>
				</C1WEBGRID:C1WEBGRID>
			</asp:Panel>
		</form>
	</body>
</HTML>
