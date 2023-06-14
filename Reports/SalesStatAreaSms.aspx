<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../Util/printReportCtl.ascx" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SalesStatAreaSms.aspx.vb" Inherits="Wyeth.Alf.SalesStatAreaSms"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 TRANSITIONAL//EN">
<HTML>
	<HEAD>
		<TITLE>SalesStatArea</TITLE>
		<META http-equiv="Content-Type" content="text/html; charset=Windows-1252">
		<META content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<META content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<META content="JavaScript" name="vs_defaultClientScript">
		<META content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../JS/ClientScripts.js"></SCRIPT>
		<asp:literal id="_clientScript" runat="server"></asp:literal>
	</HEAD>
	<BODY>
		<FORM id="Form1" method="post" RUNAT="server">
			<DIV class="noprint" style="POSITION: absolute; TOP: 33px">
				<TABLE class="reportsFilter" cellSpacing="0" cellPadding="0" width="100%">
					<TR>
						<TD class="field" noWrap>Start Date:</TD>
						<TD><BOX:WYETHTEXTBOX id="txtStartDate" RUNAT="server" TOOLTIP="Please enter a date" FRIENDLYNAME="Report Start Date"
								ALLOWBLANK="false" FIELDTYPE="DATE"></BOX:WYETHTEXTBOX></TD>
						<TD><A onclick="OpenDatePopUp('Form1.txtStartDate');"><IMG src="/Images/kalender.gif" border="0"></A></TD>
						<TD class="field" noWrap>End Date:</TD>
						<TD>
							<BOX:WYETHTEXTBOX id="txtEndDate" RUNAT="server" TOOLTIP="Please enter a date" FRIENDLYNAME="Report End Date"
								ALLOWBLANK="false" FIELDTYPE="DATE"></BOX:WYETHTEXTBOX></TD>
						<TD><A onclick="OpenDatePopUp('Form1.txtEndDate');"><IMG src="/Images/kalender.gif" border="0"></A></TD>
						<TD><ASP:DROPDOWNLIST id="ddTPGSelect" RUNAT="server"></ASP:DROPDOWNLIST></TD>
						<td width="100%"></td>
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
								<ASP:BUTTON id="ExportExcel" RUNAT="server" CSSCLASS="button" TEXT="Export to Excel" VISIBLE="true"></ASP:BUTTON>
								<prt:printReportCtl id="prtControl" runat="server"></prt:printReportCtl>
								<ASP:BUTTON id="btn_send_sms" RUNAT="server" CSSCLASS="button" TEXT="Send Sms" VISIBLE="true"></ASP:BUTTON>&nbsp;
							</DIV>
						</TD>
					</TR>
				</TABLE>
			</DIV>
			<ASP:PANEL id="ReportPanel" RUNAT="server" CSSCLASS="ReportPanel">
				<DIV align="left">
					<asp:Button id="btn_lvl1" runat="server" CssClass="button_common" Text="Level1"></asp:Button>
					<asp:Button id="btn_lvl2" runat="server" CssClass="button_common" Text="Level2"></asp:Button>
					<asp:Button id="btn_lvl3" runat="server" CssClass="button_common" text="Level3"></asp:Button></DIV>
				<C1WEBGRID:C1WEBGRID id="MyGrid" RUNAT="server" EnableViewState="False" AUTOGENERATECOLUMNS="False" DEFAULTCOLUMNWIDTH="120px"
					DEFAULTROWHEIGHT="22px" SHOWFOOTER="True">
					<Columns>
						<c1webgrid:C1BoundColumn Visible="False" DataField="SARE_NAME">
							<GroupInfo Position="Header" HeaderText="TOTAL {0}" OutlineMode="StartCollapsed"></GroupInfo>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Visible="False" DataField="CUST_WYETH_NAME">
							<GroupInfo Position="Header" HeaderText="TOTAL {0}" OutlineMode="StartCollapsed"></GroupInfo>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn SortExpression="PROD_Description" HeaderText="Product" DataField="PROD_Description"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" SortExpression="PERCENTAGE_UNITS" HeaderText="Units" DataField="PERCENTAGE_UNITS">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" SortExpression="PERCENTAGE_VALUE" HeaderText="Value" DataField="PERCENTAGE_VALUE">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Visible="False" DataField="sare_id"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Visible="False" DataField="MOBILE"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Visible="False" DataField="sms_active"></c1webgrid:C1BoundColumn>
					</Columns>
				</C1WEBGRID:C1WEBGRID>
			</ASP:PANEL>
			<asp:Label id="lblSuccess" runat="server" CssClass="success" Width="100%" Visible="False"></asp:Label></FORM>
		</FORM><asp:panel id="panelOut" runat="server" Width="100%" Height="100%">
			<asp:Label id="lblOut" runat="server" Width="100%" Height="100%"></asp:Label>
		</asp:panel>
	</BODY>
</HTML>
