<%@ Page Language="vb" AutoEventWireup="false" Codebehind="APO_Inventory.aspx.vb" Inherits="Wyeth.Alf.APO_Inventory"%>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../../Util/reportData.ascx" %>
<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../../Util/printReportCtl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>APO Inventory</title>
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
								<box:wyethtextbox id="txtStartDate" runat="server" AllowBlank="false" FriendlyName="Report Date" FieldType="DATE"
									ToolTip="Please enter a date"></box:wyethtextbox>
								<asp:Image id="StartImage" runat="server" ImageAlign="AbsMiddle" ImageUrl="/images/KALENDER.GIF"></asp:Image></TD>
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
									<ASP:BUTTON id="BUTTON1" RUNAT="server" TEXT="Download File" CSSCLASS="button"></ASP:BUTTON>
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
					<c1webgrid:C1BoundColumn HeaderText="Type" DataField="vtype"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Version" DataField="VERSION"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Date" DataField="caldate"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Plant" DataField="PLANT"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Item No." DataField="zmatnr"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Unit" DataField="unit"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Unrestricted" DataField="zic_sale">
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<FooterStyle HorizontalAlign="Right"></FooterStyle>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Batch No." DataField="zlotnum"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Exp. Date" DataField="zexpdte"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="QA Date" DataField="zQAAvDte"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Blocked" DataField="zic_blk">
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<FooterStyle HorizontalAlign="Right"></FooterStyle>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Restricted" DataField="zic_valqt">
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<FooterStyle HorizontalAlign="Right"></FooterStyle>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Quality Insp." DataField="zic_rel">
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<FooterStyle HorizontalAlign="Right"></FooterStyle>
					</c1webgrid:C1BoundColumn>
				</Columns>
			</c1webgrid:c1webgrid></TD>
		</form>
	</body>
</HTML>
