<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="RollbackConfirmation.aspx.vb" Inherits="Wyeth.Alf.RollbackConfirmation"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
    <title>RollbackConfirmation</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
     <SCRIPT LANGUAGE="JavaScript" SRC="../JS/ClientScripts.js"> </SCRIPT>
    <LINK href="../Styles.css" type=text/css rel=stylesheet >
  </HEAD>
 <BODY> 
   <FORM ID="Form1" METHOD="post" RUNAT="server">
	 
    
<DIV CLASS="HL">
				<ASP:LABEL ID="lblPageTitle" RUNAT="server"></ASP:LABEL>
			</DIV>
			<P></P>
			
			<DIV CLASS="reportsButtonBar">
				<TABLE WIDTH="100%" ALIGN="center">
					<TR>
						<TD ALIGN=right>
						<DIV CLASS="noprint"><BUTTON CLASS="button" ONCLICK="javascript:window.close();" TYPE="button">Close Window</BUTTON></DIV>
						</TD>
					</TR>
				</TABLE>
			</DIV>
			
			<P></P>
			<TABLE WIDTH="100%" ALIGN=center>
			<TR><TD ALIGN=center>
			<ASP:BUTTON ID=btn_process_invoices CSSCLASS="button" RUNAT="server" TEXT="Rollback Invoices"></ASP:BUTTON>
			 <P></P>
			 </TD></TR>
			 </TABLE>


<ASP:PANEL ID=panelSuccess RUNAT="server" VISIBLE="False" WIDTH="100%">
<TABLE WIDTH="100%" align=center>
  <TR>
    <TD align=center><STRONG><ASP:LABEL id=lblProsessSuccess RUNAT="server" HEIGHT="40px"></ASP:LABEL></STRONG></TD></TR>
  <TR>
    <TD align=center><P></P><BUTTON CLASS=button 
      ONCLICK=javascript:window.opener.print(); 
  TYPE=button>Print</BUTTON></TD></TR></TABLE>
</ASP:PANEL>

     </FORM>
    </BODY>
</HTML>
