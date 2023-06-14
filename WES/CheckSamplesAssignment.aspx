<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../Util/printReportCtl.ascx" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SamplesAssignment.aspx.vb" Inherits="Wyeth.Alf.SamplesAssignment" enableViewState="True"%>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>SamplesAssignment</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../JS/ClientScripts.js" type="text/javascript">	</SCRIPT>
		<asp:literal id="_clientScript" runat="server"></asp:literal>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<DIV class="HL"><ASP:LABEL id="lblPageTitle" RUNAT="server"></ASP:LABEL></DIV>
			<div style="PADDING-BOTTOM: 3px; PADDING-TOP: 3px"><asp:DropDownList id="ddobsCode" runat="server" Width="170px"></asp:DropDownList>
			</div>
			<DIV class="reportsButtonBar" style="PADDING-BOTTOM: 3px">
				<TABLE>
					<TR>
						<TD noWrap><REP:REPORTDATA id="repData" RUNAT="server"></REP:REPORTDATA></TD>
						<TD noWrap align="center" width="100%">&nbsp;
							<DIV class="noprint"><asp:Button id="btn_generate" runat="server" Text="Generate Report" CssClass="button"></asp:Button><BUTTON CLASS="button" ID="Button3" TYPE="button" RUNAT="server">Export 
									to Excel</BUTTON>&nbsp;<prt:printReportCtl id="prtControl" runat="server"></prt:printReportCtl>
							</DIV>
						</TD>
					</TR>
				</TABLE>
			</DIV>
			<C1WEBGRID:C1WEBGRID id="MyGrid" runat="server" DEFAULTCOLUMNWIDTH="120px" DEFAULTROWHEIGHT="22px" AUTOGENERATECOLUMNS="False">
				<COLUMNS>
					<c1webgrid:C1BoundColumn DataField="act_prod_id" Visible="False"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DataField="act_prod_phznr" HeaderText="Product No."></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DataField="act_prod_description" HeaderText="Product"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DataField="act_prod_presentation" HeaderText="Presentation"></c1webgrid:C1BoundColumn>
					<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="act_stdcogs" HEADERTEXT="Cogs">
						<HEADERSTYLE WRAP="False" HORIZONTALALIGN="Center" CSSCLASS="headlineSeperator"></HEADERSTYLE>
					</C1WEBGRID:C1BOUNDCOLUMN>
					<c1webgrid:C1BoundColumn DataField="empty">
						<HEADERSTYLE WRAP="False" HORIZONTALALIGN="Center" CSSCLASS="headlineSeperator"></HEADERSTYLE>
					</c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DataField="sam_prod_id" Visible="False"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DataField="sam_prod_phznr" HeaderText="Product No."></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DataField="sam_prod_description" HeaderText="Description"></c1webgrid:C1BoundColumn>
					<c1webgrid:C1BoundColumn DataField="sam_prod_presentation" HeaderText="Presentation"></c1webgrid:C1BoundColumn>
					<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="sam_stdcogs" HEADERTEXT="Cogs"></C1WEBGRID:C1BOUNDCOLUMN>
				</COLUMNS>
			</C1WEBGRID:C1WEBGRID></form>
		<SCRIPT LANGUAGE="javascript" SRC="../js/additionalHeaderRows.js"></SCRIPT>
		<SCRIPT LANGUAGE="javascript">
			<!--
				//hinzufügen von doppelten headlines. aufgrund von bugs im c1.webgrid
				//muss man es mittels javascript lösen. 
				createRow("myGrid");
				addTableCell("myGrid", "Actuals", "4", "", 0);
				addTableCell("myGrid", "", "1", "", 1);
				addTableCell("myGrid", "Samples", "4", "", 2);
					
			//-->
		</SCRIPT>
	</body>
</HTML>
