<%@ Page Language="vb" AutoEventWireup="false" Codebehind="IN.aspx.vb" Inherits="Wyeth.Alf._IN"%>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>IN</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<LINK type="text/css" href="../printing.css" rel="stylesheet" media="print">
  </HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div class="HL">
				<asp:Label id="lblPageTitle" runat="server"></asp:Label>
			</div>
			<p>	</P>
				<DIV class="reportsButtonBar">
					<TABLE>
						<TR>
							<TD nowrap><REP:REPORTDATA id="repData" RUNAT="server" /></TD>
							<td width="100%" align="center">
								<DIV class="noprint">
									<ASP:BUTTON ID="ExportExcel" RUNAT="server" CSSCLASS="button" TEXT="Export to Excel" VISIBLE="true"></ASP:BUTTON>&nbsp;
									<BUTTON class="button" onclick="javascript:window.print();" type="button">Print</BUTTON>&nbsp;
									<BUTTON class="button" onclick="javascript:window.close();" type="button">Close 
										Window</BUTTON>
								</DIV> 
							</td>
						</TR>
					</TABLE>
				</DIV>
			<p></P>
				<asp:panel ID="reportPanel" CssClass="ReportPanel" Runat="server"><C1WebGrid:C1WebGrid id=MyGrid runat="server" AllowAutoSort="True" ShowFooter="True" Width="100%" AutoGenerateColumns="False">
						<COLUMNS>
							<c1webgrid:C1BoundColumn DataField="tran_date" HeaderText="Date">
								<ITEMSTYLE WRAP="False"></ITEMSTYLE>
							</c1webgrid:C1BoundColumn>
							<c1webgrid:C1BoundColumn DataField="code_code" HeaderText="Code"></c1webgrid:C1BoundColumn>
							<c1webgrid:C1BoundColumn DataField="units" Aggregate="Sum" HeaderText="Units"></c1webgrid:C1BoundColumn>
							<c1webgrid:C1BoundColumn DataField="STDCOGS" HeaderText="Standard Unit Costs"></c1webgrid:C1BoundColumn>
							<c1webgrid:C1BoundColumn DataField="TOTALCOGS" Aggregate="Sum" HeaderText="Total Units Costs"></c1webgrid:C1BoundColumn>
						</COLUMNS>
					</C1WebGrid:C1WebGrid>
				</asp:panel>
		</form></P>
	</body>
</HTML>
