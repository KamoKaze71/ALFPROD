<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FxRate.aspx.vb" Inherits="Wyeth.Alf.FxRate"%>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FyRate</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../JS/ClientScripts.js"> </script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<div class="hl">
				<asp:Label id="lblPageTitle" runat="server"></asp:Label>
			</div>
			<DIV class="reportsButtonBar" id="FilterPanel">
				<TABLE width="98%">
					<TR align="left">
						<TD>
							<asp:DropDownList id="ddcurr_id_from" runat="server" AutoPostBack="True"></asp:DropDownList></TD>
						<TD>
							<asp:DropDownList id="ddcurr_id_to" runat="server" AutoPostBack="True"></asp:DropDownList></TD>
						</TD>
				   </TR>
				</TABLE>
			</DIV>
			<asp:panel id="GridPanel" cssclass="GridPanel" runat="server" width="97%">
				<C1WebGrid:C1WebGrid id="Mygrid" runat="server" width="99%" AutoGenerateColumns="False" EnableViewState="False">
					<Columns>
						<c1webgrid:C1BoundColumn DataField="fxrt_id" Visible="False"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="fxrt_rate" HeaderText="Fx Rate"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="fxrt_date_from" HeaderText="Date From"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="curr_id_from" Visible="False" HeaderText="curr_id_from"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="curr_id_to" Visible="False" HeaderText="curr_id_to"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="code_id_budget_type" Visible="False" HeaderText="Budget Type"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="code_code" HeaderText="Budget Type"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="code_from" HeaderText="Currency From"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="code_to" HeaderText="Currency To"></c1webgrid:C1BoundColumn>
					</Columns>
				</C1WebGrid:C1WebGrid>
			</asp:panel></form>
	</body>
</HTML>
