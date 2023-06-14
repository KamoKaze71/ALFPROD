<%@ Page Language="vb" AutoEventWireup="false" Codebehind="JDEProcessing.aspx.vb" Inherits="Wyeth.Alf.JDEProcessing"%>
<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../Util/printReportCtl.ascx" %>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>JDE Processing</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="JavaScript" src="../JS/ClientScripts.js"> </SCRIPT>
		<asp:literal id="_clientScript" runat="server"></asp:literal>
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<STYLE>#lblProcessMonth { FONT-WEIGHT: bold; FONT-SIZE: 11pt }
	#lblItems { FONT-WEIGHT: bold; FONT-SIZE: 11pt }
	#lblstartProcessing { FONT-WEIGHT: bold; FONT-SIZE: 11pt }
		</STYLE>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<DIV class="reportsButtonBar">
				<TABLE>
					<TR>
						<TD noWrap><asp:label id="lblProcessMonth" runat="server"></asp:label>
						<TD align="center" width="100%">
							<DIV class="noprint"><prt:printreportctl id="prtControl" runat="server"></prt:printreportctl></DIV>
						</TD>
					</TR>
				</TABLE>
			</DIV>
			<asp:label id="lblItems" runat="server" Font-Bold="True">ALF will processs the following Items for JDE Export:</asp:label><ASP:PANEL id="GridPanel" Width="100%" CSSCLASS="GridPanel" RUNAT="server">
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
			<DIV class="reportsButtonBar" style="HEIGHT: 25px">
				<DIV class="noprint">
					<TABLE width="100%">
						<TR>
							<TD noWrap><asp:label id="lblstartProcessing" runat="server"></asp:label></TD>
							<TD align="left" width="100%"><ASP:BUTTON id="btn_process_jde" runat="server" text="Start Processing" CssCLASS="button"></ASP:BUTTON></TD>
						</TR>
					</TABLE>
				</DIV>
			</DIV>
		</form>
	</body>
</HTML>
