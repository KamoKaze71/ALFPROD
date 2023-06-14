<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ForteDownloadME.aspx.vb" Inherits="Wyeth.Alf.ForteDownloadME" responseEncoding="Windows-1252"%>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
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
			<DIV class="HL"><ASP:LABEL id="lblPageTitle" RUNAT="server"></ASP:LABEL></DIV>
			<ASP:PANEL id="FilterPanel" RUNAT="server" CSSCLASS="FilterPanel">
				<DIV class="noprint">
					<TABLE class="reportsFilter" cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD class="field" noWrap>Start Date:</TD>
							<TD>
								<BOX:WYETHTEXTBOX id="txtStartDate" RUNAT="server" FIELDTYPE="DATE" ALLOWBLANK="false" FRIENDLYNAME="Report Start Date"
									TOOLTIP="Please enter a date"></BOX:WYETHTEXTBOX></TD>
							<TD><A onclick="OpenDatePopUp('Form1.txtStartDate');" href="#"><IMG src="/Images/kalender.gif" align="absMiddle" border="0">
								</A>
							</TD>
							<TD class="field" noWrap>End Date:</TD>
							<TD>
								<BOX:WYETHTEXTBOX id="txtEndDate" RUNAT="server" FIELDTYPE="DATE" ALLOWBLANK="false" FRIENDLYNAME="Report End Date"
									TOOLTIP="Please enter a date"></BOX:WYETHTEXTBOX></TD>
							<TD><A onclick="OpenDatePopUp('Form1.txtEndDate');" href="#"><IMG src="/Images/kalender.gif" align="absMiddle" border="0"></A></TD>
							<TD></TD>
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
			</ASP:PANEL><asp:button id="btn_lvl1" runat="server" CssClass="button_common" Text="Level 1"></asp:button><asp:button id="btn_lvl2" runat="server" CssClass="button_common" Text="Level2"></asp:button><ASP:PANEL id="GridPanel" RUNAT="server" CSSCLASS="GridPanel">
				<C1WEBGRID:C1WEBGRID id="MyGrid" RUNAT="server" EnableViewState="False" ShowFooter="True" DEFAULTCOLUMNWIDTH="120px"
					DEFAULTROWHEIGHT="22px" AUTOGENERATECOLUMNS="False" GROUPINDENT="10px" WIDTH="100%">
					<Columns>
						
							
						
						<c1webgrid:C1BoundColumn HeaderText="LEID" DataField="LEID"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Country" DataField="CountryID"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Product No." DataField="ProductID"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Customer ID" DataField="CustomerID"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="TimeFrame" DataField="Timeframe"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Date" DataField="Date_"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Type" DataField="Type">
							<GroupInfo Position="Header" HeaderText="Total {1}: {0}"></GroupInfo>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Currency" DataField="ccy"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Units" DataField="units">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Total Value" DataField="value">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Total Cogs" DataField="TCOGS">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						
					</Columns>
				</C1WEBGRID:C1WEBGRID>
			</ASP:PANEL></FORM>
	</BODY>
</HTML>
