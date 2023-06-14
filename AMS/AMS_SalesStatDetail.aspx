<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="AMS_SalesStatDetail.aspx.vb" Inherits="Wyeth.Alf.AMS_SalesStatDetail"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<TITLE>SalesStatCustomer</TITLE>
		<META content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<META content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<META content="JavaScript" name="vs_defaultClientScript">
		<META content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<LINK media="print" href="../printing.css" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../JS/ClientScripts.js"></SCRIPT>
	</HEAD>
	<BODY MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" RUNAT="server">
			<DIV class="HL"><ASP:LABEL id="lblPageTitle" RUNAT="server"></ASP:LABEL></DIV>
			<ASP:PANEL id="FilterPanel" RUNAT="server" CSSCLASS="FilterPanel">
				<DIV class="noprint">
					<TABLE class="reportsFilter" cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD class="field" noWrap>Start Date:</TD>
							<TD>
								<BOX:WYETHTEXTBOX id="txtStartDate" RUNAT="server" FIELDTYPE="DATE" ALLOWBLANK="false" FRIENDLYNAME="Report Start Date"
									TOOLTIP="Please enter a date"></BOX:WYETHTEXTBOX></TD>
							<TD><A onclick="OpenDatePopUpAMS('Form1.txtStartDate');" href="#"><IMG src="/Images/kalender.gif" border="0"></A></TD>
							<TD class="field" noWrap>End Date:</TD>
							<TD>
								<BOX:WYETHTEXTBOX id="txtEndDate" RUNAT="server" FIELDTYPE="DATE" ALLOWBLANK="false" FRIENDLYNAME="Report End Date"
									TOOLTIP="Please enter a date"></BOX:WYETHTEXTBOX></TD>
							<TD><A onclick="OpenDatePopUpAMS('Form1.txtEndDate');" href="#"><IMG src="/Images/kalender.gif" border="0"></A></TD>
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
							<TD noWrap><STRONG>First historic order entry:</STRONG>&nbsp;1995-12-01<BR>
								<STRONG>Last historic order entry:</STRONG>&nbsp;2002-12-31</TD>
							<TD align="center" width="100%">
								<DIV class="noprint">
									<ASP:BUTTON id="ExportExcel" RUNAT="server" CSSCLASS="button" TEXT="Export to Excel" VISIBLE="true"></ASP:BUTTON>&nbsp;<BR>
								</DIV>
							</TD>
						</TR>
					</TABLE>
				</DIV>
			</ASP:PANEL>
			<P></P>
			<ASP:LABEL id="Lblsuccess" RUNAT="server" CSSCLASS="success" HEIGHT="40px" WIDTH="100%">Download Area Only<br>AMS Data available from 1995-12-01 until 2002-12-31</ASP:LABEL>
			<P></P>
			<ASP:LABEL id="lbDownloadArea" RUNAT="server" WIDTH="100%">Generate Flat File Sales Report by clicking Export to Excel</ASP:LABEL><ASP:PANEL id="ReportPanel" RUNAT="server" CSSCLASS="ReportPanel">
				<C1WebGrid:C1WebGrid id="MyGrid" Visible="False" DefaultColumnWidth="120px" DefaultRowHeight="22px" AutoGenerateColumns="False"
					runat="server">
					<Columns>
						<c1webgrid:C1BoundColumn HeaderText="Order Date" DataField="datum"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Visible="true" HeaderText="Product" DataField="kurzbez"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Presentation" DataField="bez"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Product Group" DataField="PRODGR"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Phznr" DataField="phznr"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Auftrags Nr" DataField="AUFTRAGSNR"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="KdNr." DataField="kdnr"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="KonzNr" DataField="KONZNUMMER"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Name" DataField="NAME1"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Name" DataField="NAME2"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Units" DataField="menge"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Value" DataField="wert"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="FGUnits" DataField="nrmenge"></c1webgrid:C1BoundColumn>
					</Columns>
				</C1WebGrid:C1WebGrid>
			</ASP:PANEL></FORM>
	</BODY>
</HTML>
