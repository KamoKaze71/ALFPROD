<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../../Util/printReportCtl.ascx" %>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../../Util/reportData.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TargetApproval.aspx.vb" Inherits="Wyeth.Alf.TargetApproval" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TargetApproval</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Styles.css" type="text/css" rel="stylesheet">
		<asp:literal id="_clientScript" runat="server"></asp:literal>
	</HEAD>
	<body Mxmlns:c1webgrid="urn:http://www.componentone.com/schemas/c1webgrid">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" width="100%">
				<TR>
					<TD style="HEIGHT: 34px">Year:&nbsp;
						<asp:dropdownlist id="ddYear" runat="server"></asp:dropdownlist>&nbsp;Target-Product-Group:&nbsp;
						<asp:dropdownlist id="ddtapg" runat="server" AutoPostBack="True"></asp:dropdownlist>&nbsp;&nbsp;Sales 
						Rep:
						<asp:dropdownlist id="ddSare" runat="server"></asp:dropdownlist>&nbsp;</TD>
				</TR>
				<TR>
					<TD colSpan="4">
						<DIV class="reportsButtonBar" style="TEXT-ALIGN: left">
							<table>
								<tr>
									<td><rep:reportdata id="repData" runat="server"></rep:reportdata>&nbsp;&nbsp;</td>
									<td align="center">
										<DIV class="noprint"><asp:button id="btn_show_targets" runat="server" CssClass="button" Text="Show Targets"></asp:button>&nbsp;
											<asp:button id="ExportExcel" runat="server" CssClass="button" Text="Export to Excel" Visible="true"></asp:button>&nbsp;
											<prt:printreportctl id="prtControl" runat="server"></prt:printreportctl></DIV>
									</td>
								</tr>
							</table>
						</DIV>
					</TD>
				</TR>
			</TABLE>
			<asp:panel id="reportpanel" runat="server">
				<asp:Label id="lblTargetType" runat="server"></asp:Label>
				<br>
				<asp:label id="lblCurrency" runat="server" Visible="true" Font-Size="smaller"></asp:label>
				<C1WebGrid:C1WebGrid id="MyGrid" runat="server" AutoGenerateColumns="False" DefaultRowHeight="22px" DefaultColumnWidth="120px">
					<Columns>
						<c1webgrid:C1BoundColumn Visible="False" DataField="sare_name">
							<GroupInfo Position="Header" HeaderText="{0}" OutlineMode="StartExpanded"></GroupInfo>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Visible="False" DataField="targ_version">
							<GroupInfo Position="Header" HeaderText="Version: v_{0}" OutlineMode="StartCollapsed"></GroupInfo>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_cc_description"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Q1" DataField="targ_q1_value">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Q2" DataField="targ_q2_value">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Q3" DataField="targ_q3_value">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Q4" DataField="targ_q4_value">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1TemplateColumn Aggregate="Custom" HeaderText="Total Year">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle Font-Bold="True" Wrap="False" HorizontalAlign="Right"></ItemStyle>
						</c1webgrid:C1TemplateColumn>
						<c1webgrid:C1BoundColumn Aggregate="Max" HeaderText="Approval" DataField="user_name_approval">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<FooterStyle HorizontalAlign="Center"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Visible="False" DataField="sare_id"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Visible="False" DataField="tapg_id"></c1webgrid:C1BoundColumn>
					</Columns>
				</C1WebGrid:C1WebGrid>
			</asp:panel></form>
	</body>
</HTML>
