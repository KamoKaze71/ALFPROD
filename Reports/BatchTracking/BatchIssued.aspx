<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../../Util/printReportCtl.ascx" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../../Util/reportData.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="BatchIssued.aspx.vb" Inherits="Wyeth.Alf.BatchIssued" %>
<HTML>
	<HEAD>
		<TITLE>BatchIssued</TITLE>
		<META content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<META content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<META content="JavaScript" name="vs_defaultClientScript">
		<META content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Styles.css" type="text/css" rel="stylesheet">
		<LINK media="print" href="../../printing.css" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../../JS/ClientScripts.js"></SCRIPT>
		<asp:literal id="_clientScript" runat="server"></asp:literal>
	</HEAD>
	<BODY MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" RUNAT="server">
			<ASP:PANEL id="FilterPanel" RUNAT="server" CSSCLASS="FilterPanel">
				<DIV class="noprint">
					<TABLE class="reportsFilter" cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD class="field" noWrap>Start Date:</TD>
							<TD noWrap>
								<box:wyethtextbox id="txtStartDate" runat="server" ToolTip="Please enter a date" FieldType="DATE"
									FriendlyName="Report Date" AllowBlank="false"></box:wyethtextbox>
								<asp:Image id="StartImage" runat="server" ImageUrl="/images/KALENDER.GIF" ImageAlign="AbsMiddle"></asp:Image></TD>
							<TD class="field" noWrap>End Date:</TD>
							<TD noWrap>
								<box:wyethtextbox id="txtEndDate" runat="server" ToolTip="Please enter a date" FieldType="DATE" FriendlyName="Report Date"
									AllowBlank="false"></box:wyethtextbox>
								<asp:Image id="EndImage" runat="server" ImageUrl="/images/KALENDER.GIF" ImageAlign="AbsMiddle"></asp:Image></TD>
							<TD>
								<ASP:DROPDOWNLIST id="ddLine" RUNAT="server"></ASP:DROPDOWNLIST></TD>
							<TD width="100%">
								<TABLE cellSpacing="0" cellPadding="2">
									<TR vAlign="middle">
										<TD class="field" noWrap>Batch No.</TD>
										<TD>
											<box:wyethtextbox id="txtBatchNr" FieldType="Text" AllowBlank="true" Runat="server"></box:wyethtextbox></TD>
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
								<REP:REPORTDATA id="repData" RUNAT="server"></REP:REPORTDATA></TD>
							<TD align="center" width="100%">
								<DIV class="noprint">
									<ASP:BUTTON id="btnGenRep" RUNAT="server" CSSCLASS="button" TEXT="Generate Report"></ASP:BUTTON>&nbsp;
									<ASP:BUTTON id="ExportExcel" RUNAT="server" CSSCLASS="button" TEXT="Export to Excel" VISIBLE="true"></ASP:BUTTON>&nbsp;
									<prt:printReportCtl id="prtControl" runat="server"></prt:printReportCtl><BR>
								</DIV>
							</TD>
						</TR>
					</TABLE>
				</DIV>
			</ASP:PANEL>
			<P></P>
			<DIV class="noprint"><ASP:BUTTON id="btnLevel1" RUNAT="server" CSSCLASS="button_common" TEXT="Level 1"></ASP:BUTTON>&nbsp;
				<ASP:BUTTON id="btnLevel2" RUNAT="server" CSSCLASS="button_common" TEXT="Level 2"></ASP:BUTTON>&nbsp;
			</DIV>
			<P></P>
			<ASP:PANEL id="ReportPanel" RUNAT="server" CSSCLASS="ReportPanel">
				<C1WEBGRID:C1WEBGRID id="MyGrid" RUNAT="server" AUTOGENERATECOLUMNS="False" EnableViewState="False" DefaultColumnWidth="120px"
					DefaultRowHeight="22px">
					<Columns>
						<c1webgrid:C1BoundColumn HeaderText="Batch No." DataField="orpo_batch_number">
							<GroupInfo Position="Header" HeaderText="TOTAL {0}" OutlineMode="StartCollapsed"></GroupInfo>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Custom" HeaderText="Phznr" DataField="prod_phznr"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Custom" HeaderText="Presentation" DataField="PROD_PRESENTATION"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Custom" HeaderText="Exp. Date" DataField="orpo_exp_date">
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Units" DataField="orpo_units">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Order Date" DataField="orhe_date">
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Customer No." DataField="CUDI_CUSTOMER_NR"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Customer Name" DataField="CUST_NAME"></c1webgrid:C1BoundColumn>
					</Columns>
				</C1WEBGRID:C1WEBGRID>
			</ASP:PANEL></FORM>
	</BODY>
</HTML>
