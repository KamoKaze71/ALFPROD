<%@ Page Language="vb" AutoEventWireup="false" Codebehind="UMCrossCheck.aspx.vb" Inherits="Wyeth.Alf.UMCrossCheck"%>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../Util/printReportCtl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>UMCrossCheck</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<SCRIPT LANGUAGE="JavaScript" SRC="../JS/ClientScripts.js" TYPE="text/javascript">	</SCRIPT>
		<asp:literal id="_clientScript" runat="server"></asp:literal>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<FORM ID="Form1" METHOD="post" RUNAT="server">
			<div class="HL">
				<asp:label id="lblPageTitle" runat="server"></asp:label>
			</div>
					<TABLE class="reportsFilter" cellSpacing="0" cellPadding="0" width="100%">
				<TR>
					<TD CLASS="field" NOWRAP>Start Date:</TD>
					<TD>
						<BOX:WYETHTEXTBOX ID="txtStartDate" RUNAT="server" FIELDTYPE="DATE" ALLOWBLANK="false" FRIENDLYNAME="Report Start Date"
							TOOLTIP="Please enter a date"></BOX:WYETHTEXTBOX></TD>
					<TD><asp:Image id="StartImage" runat="server" ImageAlign="AbsMiddle" ImageUrl="/images/KALENDER.GIF"></asp:Image></TD>
					</TD>
					<TD CLASS="field" NOWRAP>End Date:</TD>
					<TD>
						<BOX:WYETHTEXTBOX ID="txtEndDate" RUNAT="server" FIELDTYPE="DATE" ALLOWBLANK="false" FRIENDLYNAME="Report End Date"
							TOOLTIP="Please enter a date"></BOX:WYETHTEXTBOX></TD>
					<TD><asp:Image id="EndImage" runat="server" ImageAlign="AbsMiddle" ImageUrl="/images/KALENDER.GIF"></asp:Image></TD>
					<TD></TD>
					<TD WIDTH="100%">
						<ASP:DROPDOWNLIST ID="ddDistribSelect" RUNAT="server"></ASP:DROPDOWNLIST></TD>
				</TR>
			</TABLE>
			<DIV></DIV>
			<DIV CLASS="reportsButtonBar">
				<TABLE>
					<TR>
						<TD NOWRAP>
							<REP:REPORTDATA ID="repData" RUNAT="server"></REP:REPORTDATA></TD>
						<TD ALIGN="center" WIDTH="100%">
							<DIV CLASS="noprint">
								<ASP:BUTTON ID="btnGenRep" RUNAT="server" CSSCLASS="button" TEXT="Generate Report"></ASP:BUTTON>&nbsp;
								<prt:printReportCtl id="prtControl" runat="server"></prt:printReportCtl>
							</DIV>
						</TD>
					</TR>
				</TABLE>
			</DIV>
			<P></P>
			<ASP:PANEL RUNAT="server" ID="gridpanel" CSSCLASS="GridPanel">
				<C1WEBGRID:C1WEBGRID id="MyGrid" runat="server" DefaultColumnWidth="120px" DefaultRowHeight="22px" AutoGenerateColumns="False"
					WIDTH="656px">
					<COLUMNS>
						<c1webgrid:C1BoundColumn DataField="prod_id_act" Visible="False"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_id_sam" Visible="False"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="ACTPhznr" HeaderText="Prod. No."></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="ACTPresentation" HeaderText="Presentation"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="ACTUnits" HeaderText="Units"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="Empty"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="SAMPhznr" HeaderText="Prod. No."></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="SAMPresentation" HeaderText="Presentation"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="SAMUnits" HeaderText="Units"></c1webgrid:C1BoundColumn>
					</COLUMNS>
				</C1WEBGRID:C1WEBGRID>
			</ASP:PANEL>
		</FORM>
	</body>
</HTML>
