<%@ Page Language="vb" AutoEventWireup="false" Codebehind="JDEHistory.aspx.vb" Inherits="Wyeth.Alf.JDEHistory"%>
<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../Util/printReportCtl.ascx" %>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>JDE History</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="JavaScript" src="../JS/ClientScripts.js"> </SCRIPT>
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE class="reportsFilter" cellSpacing="0" cellPadding="0" width="100%">
				<TR>
					<TD>
						<ASP:DROPDOWNLIST id="ddmonth" RUNAT="server" WIDTH="176px"></ASP:DROPDOWNLIST>
					</TD>
				</TR>
			</TABLE>
			<DIV></DIV>
			<DIV class="reportsButtonBar">
				<TABLE>
					<TR>
						<TD noWrap>
							<REP:REPORTDATA id="repData" RUNAT="server"></REP:REPORTDATA></TD>
						<TD align="center" width="100%">
							<DIV class="noprint">
								<ASP:BUTTON id="btnGenRep" RUNAT="server" CSSCLASS="button" TEXT="Generate Report"></ASP:BUTTON>&nbsp;
								<ASP:BUTTON id="ExportExcel" RUNAT="server" CSSCLASS="button" TEXT="Download File"></ASP:BUTTON>&nbsp;
								<prt:printReportCtl id="prtControl" runat="server"></prt:printReportCtl></DIV>
						</TD>
					</TR>
				</TABLE>
			</DIV>
			<ASP:PANEL id="GridPanel" RUNAT="server" CSSCLASS="GridPanel" Width="100%">
				<C1WebGrid:C1WebGrid id="MyGrid" runat="server" AutoGenerateColumns="False" DefaultColumnWidth="120px"
					DefaultRowHeight="22px">
					<Columns>
						<c1webgrid:C1BoundColumn Visible="False" DataField="acre_name">
							<GroupInfo Position="Header" HeaderText="TOTAL {0}" OutlineMode="StartCollapsed"></GroupInfo>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_description" visible="false">
							<GroupInfo Position="Header" HeaderText="TOTAL {0}" OutlineMode="StartCollapsed"></GroupInfo>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_presentation"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Country/ Company" DataField="debit_country_company"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Cost Center" DataField="acre_debit_costcenter"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Dep." DataField="acre_debit_department"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Acc." DataField="acre_debit_account"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Subs." DataField="acre_debit_subsidiary"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Amount" DataField="Amount_debit">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Country/ Company" DataField="credit_country_company"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Cost Center" DataField="acre_credit_costcenter"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Dep." DataField="acre_credit_department"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Acc." DataField="acre_credit_account"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Subs." DataField="acre_credit_subsidiary"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Amount" DataField="Amount_Credit">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</c1webgrid:C1BoundColumn>
					</Columns>
				</C1WebGrid:C1WebGrid>
			</ASP:PANEL>
		</form>
	</body>
</HTML>
