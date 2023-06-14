<%@ Control Language="vb" AutoEventWireup="false" Codebehind="reportData.ascx.vb" Inherits="Wyeth.Alf.reportData" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<DIV class="reportSummary">
	<SPAN class="reportSummaryField">Workday:</SPAN>
	<asp:Label id="lblWorkdays" Runat="server"></asp:Label>
	(ref. Last Order Date)<BR>
	<SPAN class="reportSummaryField">Last Order Entry:</SPAN>
	<asp:Label id="lblReportLastEntry" Runat="server"></asp:Label><BR>
	<!-- FÜR CUSTOM FELDER -->
	<div id="forReport" runat="server"></div>
	<!-- FÜR DEN DRUCK VORGESEHENE FELDER -->
	<div id="forPrint" runat="server" class="forPrint"></div>
	<DIV class="forPrint">
		<SPAN class="reportSummaryField" id="lblPrintDateCaption" runat="server">Print 
			Date:</SPAN>
		<asp:Label id="lblPrintDate" Runat="server"></asp:Label><BR>
	</DIV>
</DIV>
