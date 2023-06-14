<%@ Page Language="vb" AutoEventWireup="false" Codebehind="MER.aspx.vb" Inherits="Wyeth.Alf.MER"%>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
    <title>MER</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5"><LINK href="../Styles.css" type=text/css 
rel=stylesheet >
  </HEAD>
  <body MS_POSITIONING="GridLayout">

    <form id="Form1" method="post" runat="server">
	  
				  <DIV CLASS="HL">
				<ASP:LABEL ID="lblPageTitle" RUNAT="server"></ASP:LABEL>
			</DIV>
			<P></P>
				   <ASP:PANEL ID=FilterPanel CSSCLASS="FilterPanel" RUNAT="server">
<DIV CLASS=noprint>
<TABLE CLASS=reportsFilter ID=Table1 CELLSPACING=0 CELLPADDING=0 WIDTH="100%"></TABLE></DIV>
<DIV CLASS=reportsButtonBar>
<TABLE ID=Table2>
  <TR>
    <TD NOWRAP><REP:REPORTDATA id=repData RUNAT="server"></REP:REPORTDATA></TD>
    <TD align=center width="100%">
      <DIV CLASS=noprint><ASP:DROPDOWNLIST id=ddMonthEnd RUNAT="server" WIDTH="192px"></ASP:DROPDOWNLIST><ASP:BUTTON id=btn_Invoices RUNAT="server" CSSCLASS="button" WIDTH="144px" TEXT="Rollback Invoices" HEIGHT="19px" CAUSESVALIDATION="False"></ASP:BUTTON>&nbsp; 
<ASP:BUTTON id=ExportExcel RUNAT="server" CSSCLASS="button" TEXT="Export to Excel" VISIBLE="true"></ASP:BUTTON>&nbsp;<BUTTON 
      CLASS=button ONCLICK=javascript:window.print(); TYPE=button>Print 
      Report</BUTTON> 
</DIV></TD></TR></TABLE></DIV></ASP:PANEL>
    
    
    </form>

  </body>
</HTML>
