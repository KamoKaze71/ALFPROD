<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ProcessingStatus.aspx.vb" Inherits="Wyeth.Alf.ProcessingStatus"%>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ProcessingStatus</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:dropdownlist id="ddyear" runat="server" Width="184px"></asp:dropdownlist></form>
		<C1WEBGRID:C1WEBGRID id="MyGrid" runat="server" AutoGenerateColumns="False" DefaultRowHeight="22px" DefaultColumnWidth="120px">
			<Columns>
				<c1webgrid:C1BoundColumn DataField="pm_date" HeaderText="Processing Month">
					<HEADERSTYLE CSSCLASS="headlineSeperator"></HEADERSTYLE>
				</c1webgrid:C1BoundColumn>
				<c1webgrid:C1BoundColumn DataField="pm_status" HeaderText="Logistics"></c1webgrid:C1BoundColumn>
				<c1webgrid:C1BoundColumn DataField="pm_status_by" HeaderText="By">
					<HEADERSTYLE CSSCLASS="headlineSeperator"></HEADERSTYLE>
				</c1webgrid:C1BoundColumn>
				<c1webgrid:C1BoundColumn DataField="pm_finance_approval" HeaderText="Finance Approval"></c1webgrid:C1BoundColumn>
				<c1webgrid:C1BoundColumn DataField="pm_finance_approval_by" HeaderText="By">
					<HEADERSTYLE CSSCLASS="headlineSeperator"></HEADERSTYLE>
				</c1webgrid:C1BoundColumn>
				<c1webgrid:C1BoundColumn DataField="pm_jde_processed" HeaderText="JDE Processing"></c1webgrid:C1BoundColumn>
				<c1webgrid:C1BoundColumn DataField="pm_jde_processed_by" HeaderText="By">
					<HEADERSTYLE CSSCLASS="headlineSeperator"></HEADERSTYLE>
				</c1webgrid:C1BoundColumn>
				<c1webgrid:C1BoundColumn DataField="pm_jde_final_approval" HeaderText="Final JDE Approval"></c1webgrid:C1BoundColumn>
				<c1webgrid:C1BoundColumn DataField="pm_jde_final_approval_by" HeaderText="By"></c1webgrid:C1BoundColumn>
			</Columns>
		</C1WEBGRID:C1WEBGRID>
	</body>
</HTML>
