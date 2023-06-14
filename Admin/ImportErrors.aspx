<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ImportErrors.aspx.vb" Inherits="Wyeth.Alf.ImportErrors"%>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid, Version=1.1.20034.32, Culture=neutral, PublicKeyToken=589f1fc067ff4031" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
    <title>ImportErrors</title>
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
	<DIV CLASS=reportsButtonBar>
<TABLE>
  <TR>
    <TD NOWRAP><REP:REPORTDATA id=repData RUNAT="server"></REP:REPORTDATA></TD>
    <TD align=center width="100%">
      <DIV CLASS=noprint>&nbsp; <ASP:BUTTON id=ExportExcel RUNAT="server" CSSCLASS="button" TEXT="Export to Excel" VISIBLE="true"></ASP:BUTTON>&nbsp;<BUTTON 
      CLASS=button ONCLICK=javascript:window.print(); TYPE=button>Print 
      Report</BUTTON> </DIV>
      </TD></TR></TABLE>
      </DIV>
      <ASP:PANEL ID=reportPanel CSSCLASS=reportpanel RUNAT=server><C1WEBGRID:C1WEBGRID id=MyGrid RUNAT="server" AUTOGENERATECOLUMNS="False" DEFAULTROWHEIGHT="22px" DEFAULTCOLUMNWIDTH="120px" width="100%">
<COLUMNS>
<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="logs_date_changed" HEADERTEXT="Date"></C1WEBGRID:C1BOUNDCOLUMN>
<C1WEBGRID:C1BOUNDCOLUMN DATAFIELD="logs_description" HEADERTEXT="Description"></C1WEBGRID:C1BOUNDCOLUMN>
</Columns></C1WEBGRID:C1WEBGRID>
      
      
      </ASP:PANEL>
			
			
    </form>

  </body>
</HTML>
