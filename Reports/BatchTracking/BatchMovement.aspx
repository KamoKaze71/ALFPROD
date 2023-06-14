<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../../Util/printReportCtl.ascx" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../../Util/reportData.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="BatchMovement.aspx.vb" Inherits="Wyeth.Alf.BatchMovement" %>
<HTML>
	<HEAD>
		<TITLE>BatchMovement</TITLE>
		<META CONTENT="Microsoft Visual Studio .NET 7.1" NAME="GENERATOR">
		<META CONTENT="Visual Basic .NET 7.1" NAME="CODE_LANGUAGE">
		<META CONTENT="JavaScript" NAME="vs_defaultClientScript">
		<META CONTENT="http://schemas.microsoft.com/intellisense/ie5" NAME="vs_targetSchema">
		<LINK HREF="../../Styles.css" TYPE="text/css" REL="stylesheet">
		<LINK MEDIA="print" HREF="../../printing.css" TYPE="text/css" REL="stylesheet">
		<SCRIPT LANGUAGE="JavaScript" SRC="../../JS/ClientScripts.js"></SCRIPT>
		<asp:literal id="_clientScript" runat="server"></asp:literal>
	</HEAD>
	<BODY MS_POSITIONING="GridLayout">
		<FORM ID="Form1" METHOD="post" RUNAT="server">
			<ASP:PANEL ID="FilterPanel" RUNAT="server" CSSCLASS="FilterPanel">
				<DIV class="noprint">
					<TABLE class="reportsFilter" cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD class="field" noWrap>Start Date:</TD>
							<TD noWrap>
								<box:wyethtextbox id="txtStartDate" runat="server" AllowBlank="false" FriendlyName="Report Date" FieldType="DATE"
									ToolTip="Please enter a date"></box:wyethtextbox>
								<asp:Image id="StartImage" runat="server" ImageAlign="AbsMiddle" ImageUrl="/images/KALENDER.GIF"></asp:Image></TD>
							<TD class="field" noWrap>End Date:</TD>
							<TD noWrap>
								<box:wyethtextbox id="txtEndDate" runat="server" AllowBlank="false" FriendlyName="Report Date" FieldType="DATE"
									ToolTip="Please enter a date"></box:wyethtextbox>
								<asp:Image id="EndImage" runat="server" ImageAlign="AbsMiddle" ImageUrl="/images/KALENDER.GIF"></asp:Image></TD>
							<TD>
								<ASP:DROPDOWNLIST id="ddLine" RUNAT="server"></ASP:DROPDOWNLIST></TD>
							<TD width="100%">
								<TABLE cellSpacing="0" cellPadding="2">
									<TR vAlign="middle">
										<TD class="field" noWrap>Batch No.</TD>
										<TD>
											<box:wyethtextbox id="txtBatchNr" AllowBlank="true" FieldType="Text" Runat="server"></box:wyethtextbox></TD>
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
			<DIV CLASS="noprint">
				<ASP:BUTTON ID="btnLevel1" CSSCLASS="button_common" RUNAT="server" TEXT="Level 1"></ASP:BUTTON>&nbsp;
				<ASP:BUTTON ID="btnLevel2" CSSCLASS="button_common" RUNAT="server" TEXT="Level 2"></ASP:BUTTON>&nbsp;
			</DIV>
			<P></P>
			<ASP:PANEL ID="ReportPanel" RUNAT="server">
				<C1WEBGRID:C1WEBGRID id="MyGrid" RUNAT="server" AutoGenerateColumns="False" DefaultColumnWidth="120px"
					DefaultRowHeight="22px">
					<Columns>
						<c1webgrid:C1BoundColumn HeaderText="Batch No." DataField="stoc_batch_number">
							<GroupInfo OutlineMode="StartCollapsed"></GroupInfo>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Custom" HeaderText="Phznr" DataField="prod_phznr"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Custom" HeaderText="Presentation" DataField="PROD_PRESENTATION"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Custom" HeaderText="exp. Date" DataField="stoc_exp_date">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Units" DataField="stoc_unit">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Code" DataField="code_code">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Code" DataField="code_description">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="GRP Number" DataField="stoc_grp_number">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="GRP Date" DataField="stoc_grp_date"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Stock Date" DataField="stoc_date_stock"></c1webgrid:C1BoundColumn>
					</Columns>
				</C1WEBGRID:C1WEBGRID>
			</ASP:PANEL></FORM>
	</BODY>
</HTML>
