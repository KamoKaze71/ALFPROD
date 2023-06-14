<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="StockCogsProduct.aspx.vb" Inherits="Wyeth.Alf.StockCogsProduct" responseEncoding="Windows-1252"%>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 TRANSITIONAL//EN">
<HTML>
	<HEAD>
		<TITLE>StockCogs</TITLE>
		<META content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<META content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<META content="JavaScript" name="vs_defaultClientScript">
		<META content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="JavaScript" src="../JS/ClientScripts.js" type="text/javascript">	</SCRIPT>
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<BODY MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" RUNAT="server">
			<DIV CLASS="HL">
				<ASP:LABEL ID="lblPageTitle" RUNAT="server"></ASP:LABEL>
			</DIV>
			<ASP:PANEL ID="FilterPanel" CSSCLASS="FilterPanel" RUNAT="server">
				<DIV class="noprint">
					<TABLE class="reportsFilter" cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD class="field" noWrap>Start Date:</TD>
							<TD>
								<BOX:WYETHTEXTBOX id="txtStartDate" RUNAT="server" TOOLTIP="Please enter a date" FRIENDLYNAME="Report Start Date"
									ALLOWBLANK="false" FIELDTYPE="DATE"></BOX:WYETHTEXTBOX></TD>
							<TD><A onclick="OpenDatePopUp('Form1.txtStartDate');" href="#"><IMG src="/Images/kalender.gif" align="absMiddle" border="0">
								</A>
							</TD>
							<TD class="field" noWrap>End Date:</TD>
							<TD>
								<BOX:WYETHTEXTBOX id="txtEndDate" RUNAT="server" TOOLTIP="Please enter a date" FRIENDLYNAME="Report End Date"
									ALLOWBLANK="false" FIELDTYPE="DATE"></BOX:WYETHTEXTBOX></TD>
							<TD><A onclick="OpenDatePopUp('Form1.txtEndDate');" href="#"><IMG src="/Images/kalender.gif" align="absMiddle" border="0"></A></TD>
							<TD>
								<ASP:DROPDOWNLIST id="ddlineselect" RUNAT="server"></ASP:DROPDOWNLIST></TD>
							<TD width="100%">
								<ASP:DROPDOWNLIST id="ddDistribSelect" RUNAT="server"></ASP:DROPDOWNLIST></TD>
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
									<ASP:BUTTON id="ExportExcel" RUNAT="server" CSSCLASS="button" TEXT="Export to Excel" VISIBLE="true"></ASP:BUTTON>&nbsp;<BUTTON class="button" onclick="javascript:window.print();" type="button">Print 
										Report</BUTTON>
								</DIV>
							</TD>
						</TR>
					</TABLE>
				</DIV>
			</ASP:PANEL>
			<asp:Button id="btn_lvl1" Text="Level 1" CssClass="button_common" runat="server"></asp:Button>
			<asp:Button id="btn_lvl2" Text="Level2" CssClass="button_common" runat="server"></asp:Button>
			<asp:Button id="btn_lvl3" text="Level3" CssClass="button_common" runat="server"></asp:Button>
			<asp:Button id="btn_lvl4" runat="server" CssClass="button_common" text="Level4"></asp:Button>
			<ASP:PANEL id="GridPanel" RUNAT="server" CSSCLASS="GridPanel">
				<C1WEBGRID:C1WEBGRID id="MyGrid" RUNAT="server" WIDTH="100%" GROUPINDENT="10px" AUTOGENERATECOLUMNS="False"
					DEFAULTROWHEIGHT="22px" DEFAULTCOLUMNWIDTH="120px" ShowFooter="True" EnableViewState="False">
					<COLUMNS>
						<c1webgrid:C1BoundColumn DataField="code_id" Visible="False"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_cc_id" Visible="False"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="BEWEGKZ" Visible="False">
							<GROUPINFO POSITION="Header" HEADERTEXT="TOTAL {0}" OUTLINEMODE="StartCollapsed"></GROUPINFO>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prgr_description" Visible="False">
							<GROUPINFO POSITION="Header" HEADERTEXT="TOTAL {0}"></GROUPINFO>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="CC" Visible="False">
							<GROUPINFO POSITION="Header" HEADERTEXT="TOTAL {0}"></GROUPINFO>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="PROD_PHZNR"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="PROD_PRESENTATION"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="units" Aggregate="Sum" HeaderText="Units">
							<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
							<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="total_cogs" Aggregate="Sum" HeaderText="Total Cogs">
							<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
							<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
						</c1webgrid:C1BoundColumn>
						<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="value" AGGREGATE="Sum" HEADERTEXT="Total Value">
							<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
							<FOOTERSTYLE HORIZONTALALIGN="Right"></FOOTERSTYLE>
						</C1WEBGRID:C1BOUNDCOLUMN>
					</COLUMNS>
				</C1WEBGRID:C1WEBGRID>
			</ASP:PANEL></FORM>
	</BODY>
</HTML>
