<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ProcessingConfirmation.aspx.vb" Inherits="Wyeth.Alf.WebForm1" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Invoice Processing Confirmation</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<SCRIPT LANGUAGE="JavaScript" SRC="../JS/ClientScripts.js"> </SCRIPT>
	</HEAD>
	<BODY>
		<form id="Form1" method="post" runat="server">
			<DIV CLASS="HL">
				<ASP:LABEL ID="lblPageTitle" RUNAT="server"></ASP:LABEL>
			</DIV>
			<asp:Label id="lblTcogs" runat="server"></asp:Label>
			<C1WebGrid:C1WebGrid id="MyGrid" runat="server" AUTOGENERATECOLUMNS="False" SHOWFOOTER="True">
				<COLUMNS>
					<c1webgrid:C1BoundColumn Visible="False" DataField="TypeNumber"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn Visible="False" DataField="type">
						<GROUPINFO HEADERTEXT="Total {0}" POSITION="Header"></GROUPINFO>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DataField="code_code"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DataField="stoc_date_open" HeaderText="Date open">
						<ITEMSTYLE WRAP="False"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DataField="stoc_date_accrued" HeaderText="Date accrued">
						<ITEMSTYLE WRAP="False"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DataField="stoc_date_correct" HeaderText="Date correct">
						<ITEMSTYLE WRAP="False"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DataField="stoc_order_number" HeaderText="Order No.">
						<ITEMSTYLE WRAP="False"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DataField="stoc_invoice_number" HeaderText="Wyeth Invoice No.">
						<ITEMSTYLE WRAP="False"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DataField="stoc_unit" HeaderText="Units" Aggregate="Sum">
						<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DataField="stoc_invoice_value" HeaderText="Invoice Value" Aggregate="Sum">
						<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DataField="stoc_accrued_value" HeaderText="Accrued Value" Aggregate="Sum">
						<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DataField="stoc_diff_value" HeaderText="Diff WE to GIT" Aggregate="Sum">
						<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DataField="stoc_diff_value_accrued" HeaderText="Diff WE to Accrual" Aggregate="Sum">
						<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DataField="stoc_comment" HeaderText="Comment">
						<ITEMSTYLE WRAP="False"></ITEMSTYLE>
					</c1webgrid:C1BoundColumn>
				</COLUMNS>
			</C1WebGrid:C1WebGrid>
			<P></P>
			<TABLE WIDTH="100%" ALIGN="center">
				<TR>
					<TD ALIGN="center">
						<ASP:BUTTON ID="btn_process_invoices" CSSCLASS="button" RUNAT="server" TEXT="Confirm Processing"></ASP:BUTTON>
						<BUTTON CLASS="button" ONCLICK="javascript:window.close();" TYPE="button">Cancel</BUTTON>
					</TD>
				</TR>
			</TABLE>
			<asp:Panel id="panelSuccess" runat="server" VISIBLE="False" WIDTH="100%">
				<TABLE width="100%" align="center">
					<TR>
						<TD align="center">
							<P></P>
							<STRONG>
								<asp:Label id="lblProsessSuccess" runat="server" HEIGHT="40px"></asp:Label></STRONG></TD>
					</TR>
					<TR>
						<TD align="center">
							<P></P>
							<BUTTON class="button" onclick="javascript:window.opener.print();" type="button">Print</BUTTON></TD>
					</TR>
				</TABLE>
			</asp:Panel>
		</form>
	</BODY>
</HTML>
