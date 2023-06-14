<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../../Util/printReportCtl.ascx" %>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../../Util/reportData.ascx" %>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="APO_BCountry.aspx.vb" Inherits="Wyeth.Alf.APO_BCountry1"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>APO B Country</title>
		<meta content="True" name="vs_snapToGrid">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Styles.css" type="text/css" rel="stylesheet">
		<LINK media="print" href="../../printing.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../../JS/ClientScripts.js"></script>
		<asp:literal id="_clientScript" runat="server"></asp:literal>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:panel id="FilterPanel" Runat="server" CssClass="FilterPanel">
				<DIV class="noprint">
					<TABLE class="reportsFilter" cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD noWrap>
								<box:wyethtextbox id="txtStartDate" runat="server" ToolTip="Please enter a date" FieldType="DATE"
									FriendlyName="Report Date" AllowBlank="false"></box:wyethtextbox>
								<asp:Image id="StartImage" runat="server" ImageUrl="/images/KALENDER.GIF" ImageAlign="AbsMiddle"></asp:Image></TD>
						<TR>
						</TR>
					</TABLE>
				</DIV>
				<DIV class="reportsButtonBar">
					<TABLE>
						<TR>
							<TD noWrap>
								<REP:REPORTDATA id="repData" runat="server"></REP:REPORTDATA></TD>
							<TD align="center" width="100%">
								<DIV class="noprint">
									<asp:Button id="btnGenRep" CssClass="button" Runat="server" Text="Generate Report"></asp:Button>&nbsp;
									<ASP:BUTTON id="BUTTON1" CSSCLASS="button" TEXT="Download File" RUNAT="server"></ASP:BUTTON>
									<asp:Button id="ExportExcel" runat="server" CssClass="button" Text="Export to Excel" Visible="true"></asp:Button>&nbsp;
									<PRT:PRINTREPORTCTL id="prtControl" runat="server"></PRT:PRINTREPORTCTL></DIV>
							</TD>
						</TR>
					</TABLE>
				</DIV>
			</asp:panel>
			<p></p>
			<c1webgrid:c1webgrid id="MyGrid" name="MyGrid" runat="server" width="100%" AutoGenerateColumns="False"
				GroupIndent="10px" ShowFooter="True" DefaultColumnWidth="120px" DefaultRowHeight="22px">
				<Columns>
					<c1webgrid:C1BoundColumn HeaderText="Date" DataField="calday"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Location" DataField="zafflcc"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Item No." DataField="ZAFFLJMAT"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Ship Date" DataField="ZFCST_DATE"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Units CDF" AllowGroup="False" DataField="ZFCST_QTY_CDF"><ItemStyle HorizontalAlign="Right"></ItemStyle><FooterStyle HorizontalAlign="Right"></FooterStyle></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Units" AllowGroup="False" DataField="unit"><ItemStyle HorizontalAlign="Right"></ItemStyle><FooterStyle HorizontalAlign="Right"></FooterStyle></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Units  Samples" AllowGroup="False" DataField="ZFCST_QTY_SAMPLES"><ItemStyle HorizontalAlign="Right"></ItemStyle><FooterStyle HorizontalAlign="Right"></FooterStyle></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Units ARID" AllowGroup="False" DataField="ZFCST_QTY_ARID"><ItemStyle HorizontalAlign="Right"></ItemStyle><FooterStyle HorizontalAlign="Right"></FooterStyle></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Units ADHOC" AllowGroup="False" DataField="ZFCST_QTY_ADHOC"><ItemStyle HorizontalAlign="Right"></ItemStyle><FooterStyle HorizontalAlign="Right"></FooterStyle></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Units UPSIDE" AllowGroup="False" DataField="ZFCST_QTY_UPSIDE">
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<FooterStyle HorizontalAlign="Right"></FooterStyle>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Country to" DataField="country_to"></c1webgrid:C1BoundColumn>
				</Columns>
			</c1webgrid:c1webgrid></TD>
		</form>
	</body>
</HTML>
