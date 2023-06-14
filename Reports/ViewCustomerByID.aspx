<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ViewCustomerByID.aspx.vb" Inherits="Wyeth.Alf.CegedimDeatil"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
    <title>Cegedim Details</title>
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
			<ASP:PANEL ID="FilterPanel" CSSCLASS="FilterPanel" RUNAT="server">
<DIV CLASS=reportsButtonBar>
<TABLE>
  <TR>
    <TD NOWRAP><REP:REPORTDATA id=repData RUNAT="server"></REP:REPORTDATA></TD>
    <TD align=center width="100%">
      <DIV CLASS=noprint>&nbsp; <BUTTON id=close  CLASS="button" ONCLICK=javascript:window.close();>Close Window"></BUTTON>&nbsp;<BUTTON 
      CLASS=button ONCLICK=javascript:window.print(); TYPE=button>Print 
      Report</BUTTON> </DIV></TD></TR></TABLE></DIV>
			</ASP:PANEL>
			
			
			
			<ASP:PANEL ID="EditPanel" RUNAT="server" VISIBLE="True" CSSCLASS="EditPanel">
<TABLE CELLSPACING=2 CELLPADDING=2 WIDTH="100%" BORDER=0>
  <TR CLASS=tableBgColor2Class 
  ONMOUSEOVER="this.className='tableMouseoverColor';" 
  ONMOUSEOUT="this.className='tableBgColor2Class';">
    <TD CLASS=field WIDTH=120>Customer No.</TD>
    <TD COLSPAN=3><ASP:TEXTBOX id=txtCustID RUNAT="server" CSSCLASS="formField" ENABLED="False" Width="532px"></ASP:TEXTBOX></TD></TR>
  <TR CLASS=tableBgColor1Class 
  ONMOUSEOVER="this.className='tableMouseoverColor';" 
  ONMOUSEOUT="this.className='tableBgColor1Class';">
    <TD CLASS=field>Name</TD>
    <TD COLSPAN=3><ASP:TEXTBOX id=txtCustName RUNAT="server" CSSCLASS="formField" Enabled="False" Width="531px"></ASP:TEXTBOX></TD></TR>
  <TR CLASS=tableBgColor2Class 
  ONMOUSEOVER="this.className='tableMouseoverColor';" 
  ONMOUSEOUT="this.className='tableBgColor2Class';">
    <TD CLASS=field>Department</TD>
    <TD COLSPAN=3><ASP:TEXTBOX id=txtCustDepartment RUNAT="server" CSSCLASS="formField" Enabled="False" Width="531px"></ASP:TEXTBOX></TD></TR>
  <TR CLASS=tableBgColor1Class 
  ONMOUSEOVER="this.className='tableMouseoverColor';" 
  ONMOUSEOUT="this.className='tableBgColor1Class';">
    <TD CLASS=field>Wyeth Name</TD>
    <TD COLSPAN=3><ASP:TEXTBOX id=txtCustWyethName RUNAT="server" CSSCLASS="formField" Enabled="False" Width="531px"></ASP:TEXTBOX></TD></TR>
  <TR CLASS=tableBgColor2Class 
  ONMOUSEOVER="this.className='tableMouseoverColor';" 
  ONMOUSEOUT="this.className='tableBgColor2Class';">
    <TD CLASS=field>Address</TD>
    <TD COLSPAN=3><ASP:TEXTBOX id=txtCustAddress RUNAT="server" CSSCLASS="formField" Enabled="False" Width="531px"></ASP:TEXTBOX></TD></TR>
  <TR CLASS=tableBgColor1Class 
  ONMOUSEOVER="this.className='tableMouseoverColor';" 
  ONMOUSEOUT="this.className='tableBgColor1Class';">
    <TD CLASS=field>City</TD>
    <TD STYLE="WIDTH: 284px"><ASP:TEXTBOX id=txtCustCity RUNAT="server" CSSCLASS="formField" Enabled="False" Width="245px"></ASP:TEXTBOX></TD>
    <TD CLASS=field STYLE="WIDTH: 28px" WIDTH=28>ZIP</TD>
    <TD><ASP:TEXTBOX id=txtCustZip RUNAT="server" CSSCLASS="formField" Enabled="False" Width="207px"></ASP:TEXTBOX></TD></TR>
  <TR CLASS=tableBgColor2Class 
  ONMOUSEOVER="this.className='tableMouseoverColor';" 
  ONMOUSEOUT="this.className='tableBgColor2Class';">
    <TD CLASS=field STYLE="HEIGHT: 18px">Customer Group</TD>
    <TD STYLE="HEIGHT: 18px" COLSPAN=3><ASP:DROPDOWNLIST id=ddCustGroup RUNAT="server" CSSCLASS="formField" Enabled="False" Width="537px"></ASP:DROPDOWNLIST></TD></TR>
  <TR CLASS=tableBgColor1Class 
  ONMOUSEOVER="this.className='tableMouseoverColor';" 
  ONMOUSEOUT="this.className='tableBgColor1Class';">
    <TD CLASS=field>Stat Type</TD>
    <TD COLSPAN=3><ASP:DROPDOWNLIST id=ddCustStatType RUNAT="server" CSSCLASS="formField" Enabled="False" Width="541px"></ASP:DROPDOWNLIST></TD></TR></TABLE>
   </ASP:PANEL>
	   </form>
  </body>
</HTML>
