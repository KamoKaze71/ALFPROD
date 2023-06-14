<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SalesFlatFile.aspx.vb" Inherits="Wyeth.Alf.SalesFlatFile"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<TITLE>SalesStatCustomer</TITLE>
		<META CONTENT="Microsoft Visual Studio .NET 7.1" NAME="GENERATOR">
		<META CONTENT="Visual Basic .NET 7.1" NAME="CODE_LANGUAGE">
		<META CONTENT="JavaScript" NAME="vs_defaultClientScript">
		<META CONTENT="http://schemas.microsoft.com/intellisense/ie5" NAME="vs_targetSchema">
		<LINK HREF="../Styles.css" TYPE="text/css" REL="stylesheet">
		<LINK MEDIA="print" HREF="../printing.css" TYPE="text/css" REL="stylesheet">
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
								<BOX:WYETHTEXTBOX id="txtStartDate" RUNAT="server" TOOLTIP="Please enter a date" FRIENDLYNAME="Report Start Date"
									ALLOWBLANK="false" FIELDTYPE="DATE"></BOX:WYETHTEXTBOX></TD>
							<TD><A onclick="OpenDatePopUp('Form1.txtStartDate');" href="#"><IMG src="/Images/kalender.gif" border="0"></A></TD>
							<TD class="field" noWrap>End Date:</TD>
							<TD>
								<BOX:WYETHTEXTBOX id="txtEndDate" RUNAT="server" TOOLTIP="Please enter a date" FRIENDLYNAME="Report End Date"
									ALLOWBLANK="false" FIELDTYPE="DATE"></BOX:WYETHTEXTBOX></TD>
							<TD><A onclick="OpenDatePopUp('Form1.txtEndDate');" href="#"><IMG src="/Images/kalender.gif" border="0"></A></TD>
							<TD style="WIDTH: 140px">
								<ASP:DROPDOWNLIST id="ddProductSelect" RUNAT="server"></ASP:DROPDOWNLIST></TD>
							<TD width="100%">
								<ASP:DROPDOWNLIST id="ddLine" RUNAT="server" AutoPostBack="True"></ASP:DROPDOWNLIST></TD>
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
									<ASP:BUTTON id="ExportExcel" RUNAT="server" CSSCLASS="button" VISIBLE="true" TEXT="Export to Excel"></ASP:BUTTON>&nbsp;<BR>
								</DIV>
							</TD>
						</TR>
					</TABLE>
				</DIV>
			</ASP:PANEL>
			<P></P>
			<ASP:LABEL ID="Lblsuccess" RUNAT="server" CSSCLASS="success" WIDTH="100%" HEIGHT="40px">Download Area Only<br> Data available from 2003-01-01 onwards</ASP:LABEL>
			<P></P>
			<ASP:LABEL ID="lbDownloadArea" RUNAT="server" WIDTH="100%">Generate Flat File Sales Report by clicking Export to Excel</ASP:LABEL>
			<ASP:PANEL ID="ReportPanel" RUNAT="server" CSSCLASS="ReportPanel">
				<C1WEBGRID:C1WEBGRID id="MyGrid" RUNAT="server" DEFAULTCOLUMNWIDTH="120px" DEFAULTROWHEIGHT="22px" AUTOGENERATECOLUMNS="False">
					<ItemStyle Wrap="False"></ItemStyle>
					<AlternatingItemStyle Wrap="False"></AlternatingItemStyle>
					<Columns>
						<c1webgrid:C1BoundColumn DataField="Tran_date" SortExpression="Tran_date" HeaderText="Order Date">
							<HeaderStyle Wrap="False"></HeaderStyle>
							<ItemStyle Wrap="False"></ItemStyle>
							<FooterStyle Wrap="False"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="PROD_SEGMENT" SortExpression="PROD_SEGMENT" HeaderText="Segment">
							<HeaderStyle Wrap="False"></HeaderStyle>
							<ItemStyle Wrap="False"></ItemStyle>
							<FooterStyle Wrap="False"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="PRGR_DESCRIPTION" SortExpression="PRGR_DESCRIPTION" HeaderText="Product Group">
							<HeaderStyle Wrap="False"></HeaderStyle>
							<ItemStyle Wrap="False"></ItemStyle>
							<FooterStyle Wrap="False"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="PROD_DESCRIPTION" SortExpression="PROD_DESCRIPTION" HeaderText="Description">
							<HeaderStyle Wrap="False"></HeaderStyle>
							<ItemStyle Wrap="False"></ItemStyle>
							<FooterStyle Wrap="False"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="PROD_PHZNR" HeaderText="Product No.">
							<HeaderStyle Wrap="False"></HeaderStyle>
							<ItemStyle Wrap="False"></ItemStyle>
							<FooterStyle Wrap="False"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="PROD_PRESENTATION" HeaderText="Presentation">
							<HeaderStyle Wrap="False"></HeaderStyle>
							<ItemStyle Wrap="False"></ItemStyle>
							<FooterStyle Wrap="False"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="CUGR_DESCRIPTION" SortExpression="CUGR_DESCRIPTION" HeaderText="Customer Group">
							<HeaderStyle Wrap="False"></HeaderStyle>
							<ItemStyle Wrap="False"></ItemStyle>
							<FooterStyle Wrap="False"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="CUST_NAME" SortExpression="CUST_NAME" HeaderText="Customer Name">
							<HeaderStyle Wrap="False"></HeaderStyle>
							<ItemStyle Wrap="False"></ItemStyle>
							<FooterStyle Wrap="False"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="CUST_zip" SortExpression="CUST_plz" HeaderText="ZIP">
							<HeaderStyle Wrap="False"></HeaderStyle>
							<ItemStyle Wrap="False"></ItemStyle>
							<FooterStyle Wrap="False"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="cust_city" SortExpression="cust_city" HeaderText="City">
							<HeaderStyle Wrap="False"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
							<FooterStyle Wrap="False"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="CUDI_CUSTOMER_NR" SortExpression="cudi_customer_nr" HeaderText="Customer No.">
							<HeaderStyle Wrap="False"></HeaderStyle>
							<ItemStyle Wrap="False"></ItemStyle>
							<FooterStyle Wrap="False"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="Order_no" SortExpression="Order_no" HeaderText="Order No.">
							<HeaderStyle Wrap="False"></HeaderStyle>
							<ItemStyle Wrap="False"></ItemStyle>
							<FooterStyle Wrap="False"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="ORPO_UNITS" SortExpression="ORPO_UNITS" HeaderText="Units">
							<HeaderStyle Wrap="False"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
							<FooterStyle Wrap="False" HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="ORPO_VALUE" SortExpression="ORPO_VALUE" HeaderText="Value">
							<HeaderStyle Wrap="False"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
							<FooterStyle Wrap="False" HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="ORPO_FGUNITS" SortExpression="ORPO_FGUNITS" HeaderText="FGUnits">
							<HeaderStyle Wrap="False"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
							<FooterStyle Wrap="False" HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="ORPO_FGVALUE" SortExpression="ORPO_FGVALUE" HeaderText="FGValue">
							<HeaderStyle Wrap="False" CssClass="headlineSeperator"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
							<FooterStyle Wrap="False" HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="std_cogs" SortExpression="std_cogs" HeaderText="STD Cogs">
							<HeaderStyle Wrap="False" CssClass="headlineSeperator"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
							<FooterStyle Wrap="False" HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="total_cogs" SortExpression="total_cogs" HeaderText="Total Cogs">
							<HeaderStyle Wrap="False" CssClass="headlineSeperator"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
							<FooterStyle Wrap="False" HorizontalAlign="Right"></FooterStyle>
						</c1webgrid:C1BoundColumn>
					</Columns>
					<EditItemStyle Wrap="False"></EditItemStyle>
					<SelectedItemStyle Wrap="False"></SelectedItemStyle>
				</C1WEBGRID:C1WEBGRID>
			</ASP:PANEL></FORM>
	</BODY>
</HTML>
