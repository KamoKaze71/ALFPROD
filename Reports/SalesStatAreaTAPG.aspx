<%@ Register TagPrefix="ppt" TagName="printPreviewCtl" Src="../Util/printPreviewCtl.ascx" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SalesStatAreaTAPG.aspx.vb" Inherits="Wyeth.Alf.SalesStatAreaTAPG"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 TRANSITIONAL//EN">
<HTML>
	<HEAD>
		<TITLE>SalesStatArea</TITLE>
		<META HTTP-EQUIV="Content-Type" CONTENT="text/html; charset=Windows-1252">
		<META CONTENT="Microsoft Visual Studio .NET 7.1" NAME="GENERATOR">
		<META CONTENT="Visual Basic .NET 7.1" NAME="CODE_LANGUAGE">
		<META CONTENT="JavaScript" NAME="vs_defaultClientScript">
		<META CONTENT="http://schemas.microsoft.com/intellisense/ie5" NAME="vs_targetSchema">
		<LINK HREF="../Styles.css" TYPE="text/css" REL="stylesheet">
		<SCRIPT LANGUAGE="JavaScript" SRC="../JS/ClientScripts.js"></SCRIPT>
	</HEAD>
	<BODY MS_POSITIONING="GridLayout">
		<FORM ID="Form1" METHOD="post" RUNAT="server">
			<DIV CLASS="HL"><ASP:LABEL ID="lblPageTitle" RUNAT="server"></ASP:LABEL></DIV>
			<ASP:PANEL ID="FilterPanel" RUNAT="server" CSSCLASS="FilterPanel">
				<DIV class="noprint">
					<TABLE class="reportsFilter" cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD class="field" noWrap>Start Date:</TD>
							<TD>
								<BOX:WYETHTEXTBOX id="txtStartDate" RUNAT="server" FIELDTYPE="DATE" ALLOWBLANK="false" FRIENDLYNAME="Report Start Date"
									TOOLTIP="Please enter a date"></BOX:WYETHTEXTBOX></TD>
							<TD><A onclick="OpenDatePopUp('Form1.txtStartDate');"><IMG src="/Images/kalender.gif" border="0"></A></TD>
							<TD class="field" noWrap>End Date:</TD>
							<TD>
								<BOX:WYETHTEXTBOX id="txtEndDate" RUNAT="server" FIELDTYPE="DATE" ALLOWBLANK="false" FRIENDLYNAME="Report End Date"
									TOOLTIP="Please enter a date"></BOX:WYETHTEXTBOX></TD>
							<TD><A onclick="OpenDatePopUp('Form1.txtEndDate');"><IMG src="/Images/kalender.gif" border="0"></A></TD>
							<TD>
								<ASP:DROPDOWNLIST id="ddLine" RUNAT="server"></ASP:DROPDOWNLIST></TD>
							<TD width="100%">
								<TABLE cellSpacing="0" cellPadding="0">
									<TR vAlign="middle">
										<TD class="field" noWrap>Group by:</TD>
										<TD>
											<ASP:DROPDOWNLIST id="groupBy" RUNAT="server"></ASP:DROPDOWNLIST></TD>
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
									<ASP:BUTTON id="ExportExcel" RUNAT="server" CSSCLASS="button" TEXT="Export to Excel" VISIBLE="true"></ASP:BUTTON>&nbsp;<INPUT class="button" onclick="self.print();" type="button" value="Print">
									<BR>
								</DIV>
							</TD>
						</TR>
					</TABLE>
				</DIV>
			</ASP:PANEL>
			<P></P>
			<DIV CLASS="noprint"><ASP:BUTTON ID="btnLevel1" RUNAT="server" CSSCLASS="button_common" TEXT="Level1"></ASP:BUTTON>&nbsp;
				<ASP:BUTTON ID="btnLevel2" RUNAT="server" CSSCLASS="button_common" TEXT="Level2"></ASP:BUTTON>&nbsp;
				<ASP:BUTTON ID="btnLevel3" RUNAT="server" CSSCLASS="button_common" TEXT="Level3"></ASP:BUTTON></DIV>
			<ASP:PANEL ID="ReportPanel" RUNAT="server" CSSCLASS="ReportPanel">
				<C1WEBGRID:C1WEBGRID id="MyGrid" RUNAT="server" SHOWFOOTER="True" DEFAULTROWHEIGHT="22px" DEFAULTCOLUMNWIDTH="120px"
					AUTOGENERATECOLUMNS="False" EnableViewState="False">
					<Columns>
						<c1webgrid:C1BoundColumn Visible="False" SortExpression="TAPG_DESCRIPTION" HeaderText="TPGroup" DataField="TAPG_DESCRIPTION">
							<GroupInfo Position="Header" HeaderText="TOTAL {0}" OutlineMode="StartCollapsed"></GroupInfo>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Visible="False" HeaderText="Sales Rep." DataField="SARE_NAME">
							<GroupInfo Position="Header" HeaderText="TOTAL {0}" OutlineMode="StartCollapsed"></GroupInfo>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Visible="False" SortExpression="CUSTOMER_NAME" HeaderText="Customer Name" DataField="CUSTOMER_NAME">
							<GroupInfo Position="Header" HeaderText="TOTAL {0}" OutlineMode="StartCollapsed"></GroupInfo>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn SortExpression="PROD_Description" HeaderText="Product" DataField="PROD_Description">
							<GroupInfo Position="Header" HeaderText="TOTAL {0}" OutlineMode="StartCollapsed"></GroupInfo>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" SortExpression="PERCENTAGE_UNITS" HeaderText="Units" DataField="PERCENTAGE_UNITS">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" SortExpression="PERCENTAGE_VALUE" HeaderText="Value" DataField="PERCENTAGE_VALUE">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" SortExpression="PERCENTAGE_FGUNITS" HeaderText="FGUnits" DataField="PERCENTAGE_FGUNITS">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" SortExpression="PERCENTAGE_FGVALUE" HeaderText="FGValue" DataField="PERCENTAGE_FGVALUE">
							<HeaderStyle CssClass="headlineSeperator"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Visible="False" DataField="CUDI_CUSTOMER_NR"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Visible="False" DataField="sare_id"></c1webgrid:C1BoundColumn>
					</Columns>
				</C1WEBGRID:C1WEBGRID>
			</ASP:PANEL></FORM>
	</BODY>
</HTML>
