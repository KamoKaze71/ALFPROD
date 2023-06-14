<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ReturnsCheckReport.aspx.vb" Inherits="Wyeth.Alf.ReturnsCheckReport"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ReturnsCheckReport</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="JavaScript" src="../JS/ClientScripts.js" type="text/javascript">	</SCRIPT>
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<ASP:PANEL id="FilterPanel" RUNAT="server" CSSCLASS="FilterPanel">
				<DIV class="noprint">
					<TABLE class="reportsFilter" cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD class="field" noWrap>Start Date:</TD>
							<TD>
								<BOX:WYETHTEXTBOX id="txtStartDate" RUNAT="server" FIELDTYPE="DATE" ALLOWBLANK="false" FRIENDLYNAME="Report Start Date"
									TOOLTIP="Please enter a date"></BOX:WYETHTEXTBOX></TD>
							<TD align="left"><A onclick="OpenDatePopUp('Form1.txtStartDate');" href="#"><IMG src="/Images/kalender.gif" align="absMiddle" border="0">
								</A>
							</TD>
							<TD class="field" noWrap>End Date:</TD>
							<TD>
								<BOX:WYETHTEXTBOX id="txtEndDate" RUNAT="server" FIELDTYPE="DATE" ALLOWBLANK="false" FRIENDLYNAME="Report End Date"
									TOOLTIP="Please enter a date"></BOX:WYETHTEXTBOX></TD>
							<TD align="left"><A onclick="OpenDatePopUp('Form1.txtEndDate');" href="#"><IMG src="/Images/kalender.gif" align="absMiddle" border="0"></A></TD>
							<TD align="right">Distibutor:&nbsp;
								<ASP:DROPDOWNLIST id="ddDistribSelect" RUNAT="server"></ASP:DROPDOWNLIST>&nbsp;&nbsp; 
								Line:&nbsp;
								<ASP:DROPDOWNLIST id="ddlineselect" RUNAT="server"></ASP:DROPDOWNLIST>group by
								<ASP:DROPDOWNLIST id="groupby" RUNAT="server"></ASP:DROPDOWNLIST></TD>
						</TR>
						<TR>
							<TD align="right" colSpan="8">:&nbsp;
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
									<ASP:BUTTON id="btnGenRep" CSSCLASS="button" RUNAT="server" TEXT="Generate Report"></ASP:BUTTON>&nbsp;
									<ASP:BUTTON id="ExportExcel" CSSCLASS="button" RUNAT="server" TEXT="Export to Excel" VISIBLE="true"></ASP:BUTTON>&nbsp;<BUTTON class="button" onclick="javascript:window.print();" type="button">Print 
										Report</BUTTON>
								</DIV>
							</TD>
						</TR>
					</TABLE>
				</DIV>
			</ASP:PANEL><asp:button id="btn_lvl1" runat="server" CssClass="button_common" Text="Level 1"></asp:button><asp:button id="btn_lvl2" runat="server" CssClass="button_common" Text="Level2"></asp:button><asp:button id="btn_lvl3" runat="server" CssClass="button_common" text="Level3"></asp:button><asp:button id="btn_lvl4" runat="server" CssClass="button_common" text="Level4"></asp:button><ASP:PANEL id="GridPanel" RUNAT="server" CSSCLASS="GridPanel">
				<C1WEBGRID:C1WEBGRID id="MyGrid" RUNAT="server" EnableViewState="False" ShowFooter="True" DEFAULTCOLUMNWIDTH="120px"
					DEFAULTROWHEIGHT="22px" AUTOGENERATECOLUMNS="False" GROUPINDENT="10px" WIDTH="100%">
					<Columns>
						<c1webgrid:C1BoundColumn Visible="False" DataField="Type">
							<GroupInfo Position="Header" HeaderText="Custom"></GroupInfo>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Visible="False" HeaderText="BewegKz" DataField="BEWEGKZ">
							<GroupInfo Position="Header" HeaderText="Custom" OutlineMode="StartCollapsed"></GroupInfo>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Visible="False" DataField="PRGR_CODE">
							<GroupInfo Position="Header" HeaderText="TOTAL {0}" OutlineMode="StartCollapsed"></GroupInfo>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Visible="False" DataField="PROD_DESCRIPTION">
							<GroupInfo Position="Header" HeaderText="TOTAL {0}" OutlineMode="StartCollapsed"></GroupInfo>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Prod. Number" DataField="PROD_PHZNR"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Presentation" DataField="PROD_PRESENTATION"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Date" DataField="ORHE_DATE">
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="CUST_NAME"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Units" DataField="ORPO_UNITS">
							<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
							<FooterStyle Wrap="False" HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Value" DataField="ORPO_VALUE">
							<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
							<FooterStyle Wrap="False" HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="FG Units" DataField="ORPO_FGUNITS">
							<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
							<FooterStyle Wrap="False" HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="FG Value" DataField="ORPO_FGVALUE">
							<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
							<FooterStyle Wrap="False" HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="TCogs" DataField="t_cogs">
							<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
							<FooterStyle Wrap="False" HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
					</Columns>
				</C1WEBGRID:C1WEBGRID>
			</ASP:PANEL></form>
	</body>
</HTML>
