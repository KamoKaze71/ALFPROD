<%@ Page Language="vb" AutoEventWireup="false" Codebehind="AMS_SalesStat.aspx.vb" Inherits="Wyeth.Alf.AMS_SalesStat"%>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../Util/printReportCtl.ascx" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>AMS_SalesStat</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<ASP:PANEL ID="FilterPanel" RUNAT="server" CSSCLASS="FilterPanel">
				<DIV class="noprint">
					<TABLE class="reportsFilter" cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD class="field" noWrap>Start Date:</TD>
							<TD>
								<asp:DropDownList id="ddstartDate" runat="server" Width="176px"></asp:DropDownList></TD>
							<TD style="WIDTH: 163px"></TD>
							<TD class="field" noWrap>End Date:</TD>
							<TD>
								<asp:DropDownList id="ddEndDate" runat="server" Width="176px"></asp:DropDownList></TD>
							<TD style="WIDTH: 186px"></TD>
							<TD>
								<ASP:DROPDOWNLIST id="ddLine" RUNAT="server"></ASP:DROPDOWNLIST></TD>
							<TD width="100%">
								<TABLE cellSpacing="0" cellPadding="0">
									<TR vAlign="middle">
										<TD class="field" noWrap>Group by:</TD>
										<TD>
											<ASP:DROPDOWNLIST id="groupByDD" RUNAT="server"></ASP:DROPDOWNLIST></TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
					</TABLE>
				</DIV>
				<DIV class="reportsButtonBar">
					<TABLE>
						<TR>
							<TD noWrap><STRONG>First historic order entry:</STRONG>&nbsp; 1995-12-01<BR>
								<STRONG>Last historic order entry:</STRONG> &nbsp; 2002-12-31</TD>
							<TD align="center" width="100%">
								<DIV class="noprint">
									<ASP:BUTTON id="btnGenRep" CSSCLASS="button" RUNAT="server" TEXT="Generate Report"></ASP:BUTTON>&nbsp;
									<ASP:BUTTON id="ExportExcel" CSSCLASS="button" RUNAT="server" TEXT="Export to Excel" VISIBLE="true"></ASP:BUTTON>&nbsp;
									<PRT:PRINTREPORTCTL id="prtControl" runat="server"></PRT:PRINTREPORTCTL><BR>
								</DIV>
							</TD>
						</TR>
					</TABLE>
				</DIV>
			</ASP:PANEL>
			<P></P>
			<DIV CLASS="noprint"><ASP:BUTTON ID="btn_lvl1" RUNAT="server" CSSCLASS="button_common" TEXT="Level1"></ASP:BUTTON>&nbsp;
				<ASP:BUTTON ID="btn_lvl2" RUNAT="server" CSSCLASS="button_common" TEXT="Level2"></ASP:BUTTON>&nbsp;
				<ASP:BUTTON ID="btn_lvl3" RUNAT="server" CSSCLASS="button_common" TEXT="Level3"></ASP:BUTTON></DIV>
			<C1WebGrid:C1WebGrid id="MyGrid" runat="server" DefaultColumnWidth="120px" DefaultRowHeight="22px" AutoGenerateColumns="False"
				EnableViewState="False">
				<Columns>
					<c1webgrid:C1BoundColumn Visible="False" HeaderText="Product" DataField="kurzbez">
						<GroupInfo Position="Header" HeaderText="Total {0}"></GroupInfo>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Presentation" DataField="bez"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Prod. No." DataField="phznr"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Cust. No." DataField="kdnr"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Code" DataField="cod_kdkenn"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn HeaderText="Name" DataField="NAME"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Units" DataField="menge"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="Value" DataField="wert"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="FGUnits" DataField="nrmenge"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn Aggregate="Sum" HeaderText="FGValue" DataField="nrwert"></c1webgrid:C1BoundColumn>
				</Columns>
			</C1WebGrid:C1WebGrid>
		</form>
	</body>
</HTML>
