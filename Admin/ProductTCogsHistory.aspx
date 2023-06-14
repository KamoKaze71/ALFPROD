<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ProductTCogsHistory.aspx.vb" Inherits="Wyeth.Alf.ProductTCogsHistory"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
    <title>ProductTCogsHistory</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5"><LINK href="../Styles.css" type=text/css 
rel=stylesheet >
  </HEAD>
  <BODY MS_POSITIONING="GridLayout">

    <FORM ID="Form1" METHOD="post" RUNAT="server">
   
		
		<DIV CLASS="HL">
				<ASP:LABEL ID="lblPageTitle" RUNAT="server"></ASP:LABEL>
			</DIV>
			<ASP:PANEL ID="FilterPanel" CSSCLASS="FilterPanel" RUNAT="server">
<DIV CLASS=reportsButtonBar>
<TABLE>
  <TR>
    <TD NOWRAP></TD>
    <TD align=center width="100%">
      <DIV CLASS=noprint>&nbsp;<BUTTON CLASS=button ID=close 
      ONCLICK=javascript:window.close(); TYPE=button>Close 
      Window</BUTTON>&nbsp;<BUTTON CLASS=button 
      ONCLICK=javascript:window.print(); TYPE=button>Print Report</BUTTON> 
    </DIV></TD></TR></TABLE></DIV>
			</ASP:PANEL><c1webgrid:C1WebGrid id=MyGrid runat="server" DefaultColumnWidth="120px" DefaultRowHeight="22px" AutoGenerateColumns="False" Width="192px">
<COLUMNS>
<c1webgrid:C1BoundColumn DataField="cogs_Date_from" HeaderText="Date From"></c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="cogs_date_to" HeaderText="Date To"></c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="cogs_std_cogs" HeaderText="STD COGS"></c1webgrid:C1BoundColumn>
<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="curr_code" HEADERTEXT="Currency"></C1WEBGRID:C1BOUNDCOLUMN>

</Columns>
</c1webgrid:C1WebGrid><asp:Label id=lblNoTCogs  Width="100%" runat="server" CssClass="nosuccess">Label</asp:Label></FORM></BODY>
</HTML>
