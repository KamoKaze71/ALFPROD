<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ViewGIT.aspx.vb" Inherits="Wyeth.Alf.ViewGIT"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
    <title>ViewGIT</title>
<meta content="Microsoft Visual Studio .NET 7.1" name=GENERATOR>
<meta content="Visual Basic .NET 7.1" name=CODE_LANGUAGE>
<meta content=JavaScript name=vs_defaultClientScript>
<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema><LINK href="../Styles.css" type=text/css rel=stylesheet ><LINK href="../Styles.css" type=text/css rel=stylesheet >
  </HEAD>
<body MS_POSITIONING="GridLayout">
<form id=Form1 method=post runat="server">
<DIV CLASS="HL">
				<ASP:LABEL ID="lblPageTitle" RUNAT="server"></ASP:LABEL>
			</DIV>
			<P></P>
			
			<DIV CLASS="reportsButtonBar">
				<TABLE>
					<TR>
						<TD NOWRAP><REP:REPORTDATA ID="repData" RUNAT="server"></REP:REPORTDATA></TD>
						<TD WIDTH="100%" ALIGN="center">
							<DIV CLASS="noprint">
								<BUTTON CLASS="button" ONCLICK="javascript:window.print();" TYPE="button">Print</BUTTON>&nbsp;
								<BUTTON CLASS="button" ONCLICK="javascript:window.close();" TYPE="button">Close 
									Window</BUTTON>
							</DIV>
						</TD>
					</TR>
				</TABLE>
			</DIV>
			
			<P></P>
<asp:panel id=WEPanel runat="server" Width="100%" Height="216px" BACKCOLOR="White" BORDERCOLOR="White">
<TABLE WIDTH="100%">
  <TR CLASS=tableBgColor1Class 
  ONMOUSEOVER="this.className='tableMouseoverColor';" 
  ONMOUSEOUT="this.className='tableBgColor1Class';">
    <TD COLSPAN=2><STRONG>WE</STRONG></TD></TR>
  <TR CLASS=tableBgColor1Class 
  ONMOUSEOVER="this.className='tableMouseoverColor';" 
  ONMOUSEOUT="this.className='tableBgColor1Class';">
    <TD STYLE="WIDTH: 127px">Stock Date</TD>
    <TD><asp:TextBox id=txtStockDateStock runat="server" Enabled="False" CssClass="formfield"></asp:TextBox></TD></TR>
  <TR CLASS=tableBgColor1Class 
  ONMOUSEOVER="this.className='tableMouseoverColor';" 
  ONMOUSEOUT="this.className='tableBgColor1Class';">
    <TD STYLE="WIDTH: 127px">Stock Date Accrued</TD>
    <TD><ASP:TEXTBOX id=txtStockDateAccrued RUNAT="server" Enabled="False" CSSCLASS="formfield"></ASP:TEXTBOX></TD></TR>
  <TR CLASS=tableBgColor1Class 
  ONMOUSEOVER="this.className='tableMouseoverColor';" 
  ONMOUSEOUT="this.className='tableBgColor1Class';">
    <TD STYLE="WIDTH: 127px">Stock Date Correct</TD>
    <TD><ASP:TEXTBOX id=txtStockDateCorrect RUNAT="server" Enabled="False" CSSCLASS="formfield"></ASP:TEXTBOX></TD></TR>
  <TR CLASS=tableBgColor1Class 
  ONMOUSEOVER="this.className='tableMouseoverColor';" 
  ONMOUSEOUT="this.className='tableBgColor1Class';">
    <TD STYLE="WIDTH: 127px">Product No.</TD>
    <TD><asp:TextBox id=txtPhznr runat="server" Enabled="False" CssClass="formfield"></asp:TextBox></TD></TR>
  <TR CLASS=tableBgColor1Class 
  ONMOUSEOVER="this.className='tableMouseoverColor';" 
  ONMOUSEOUT="this.className='tableBgColor1Class';">
    <TD STYLE="WIDTH: 127px">Presentation</TD>
    <TD><asp:TextBox id=txtPresentation runat="server" Enabled="False" CssClass="formfield"></asp:TextBox></TD></TR>
  <TR CLASS=tableBgColor1Class 
  ONMOUSEOVER="this.className='tableMouseoverColor';" 
  ONMOUSEOUT="this.className='tableBgColor1Class';">
    <TD STYLE="WIDTH: 127px">Order No.</TD>
    <TD><asp:TextBox id=txtOrderNo runat="server" Enabled="False" CssClass="formfield"></asp:TextBox></TD></TR>
  <TR CLASS=tableBgColor1Class 
  ONMOUSEOVER="this.className='tableMouseoverColor';" 
  ONMOUSEOUT="this.className='tableBgColor1Class';">
    <TD STYLE="WIDTH: 127px">Wyeth Order No.</TD>
    <TD><asp:TextBox id=txtWyethInvoiceNo runat="server" Enabled="False" CssClass="formfield"></asp:TextBox></TD></TR>
  <TR CLASS=tableBgColor1Class 
  ONMOUSEOVER="this.className='tableMouseoverColor';" 
  ONMOUSEOUT="this.className='tableBgColor1Class';">
    <TD STYLE="WIDTH: 127px">Units</TD>
    <TD><asp:TextBox id=txtUnits runat="server" Enabled="False" CssClass="formfield"></asp:TextBox></TD></TR>
  <TR CLASS=tableBgColor1Class 
  ONMOUSEOVER="this.className='tableMouseoverColor';" 
  ONMOUSEOUT="this.className='tableBgColor1Class';">
    <TD STYLE="WIDTH: 127px; HEIGHT: 22px">Invoice Value</TD>
    <TD STYLE="HEIGHT: 22px"><asp:TextBox id=txtInvoiceValue runat="server" Width="170px" Enabled="False" CssClass="formfield"></asp:TextBox>&nbsp;<ASP:TEXTBOX id=currInvoice RUNAT="server" Width="65" Enabled="False" CSSCLASS="formfield"></ASP:TEXTBOX></TD></TR>
  <TR CLASS=tableBgColor1Class 
  ONMOUSEOVER="this.className='tableMouseoverColor';" 
  ONMOUSEOUT="this.className='tableBgColor1Class';">
    <TD STYLE="WIDTH: 127px">Invoice Value Accrued</TD>
    <TD><asp:TextBox id=txtInvoiceValueAccrued runat="server" Width="174px" Enabled="False" CssClass="formfield"></asp:TextBox>&nbsp;<ASP:TEXTBOX id=currAccrued RUNAT="server" Width="65px" Enabled="False" CSSCLASS="formfield"></ASP:TEXTBOX></TD></TR>
  <TR CLASS=tableBgColor1Class 
  ONMOUSEOVER="this.className='tableMouseoverColor';" 
  ONMOUSEOUT="this.className='tableBgColor1Class';">
    <TD STYLE="WIDTH: 127px">Difference</TD>
    <TD><asp:TextBox id=txtDifferenceAccrued runat="server" Enabled="False" CssClass="formfield"></asp:TextBox></TD></TR>
  <TR>
    <TD STYLE="WIDTH: 127px">Difference to GIT</TD>
    <TD><asp:TextBox id=txtDiffGIT runat="server" Enabled="False" CssClass="formfield"></asp:TextBox></TD></TR>
  <TR CLASS=tableBgColor1Class 
  ONMOUSEOVER="this.className='tableMouseoverColor';" 
  ONMOUSEOUT="this.className='tableBgColor1Class';">
    <TD STYLE="WIDTH: 127px">Commnent</TD>
    <TD><asp:TextBox id=txtComment runat="server" Height="31px" Enabled="False" CssClass="formfield" Rows="2"></asp:TextBox></TD></TR></TABLE>
</asp:panel>

<asp:panel id=GITPanel runat="server" Width="100%">
<TABLE WIDTH="100%">
  <TR CLASS=tableBgColor1Class 
  ONMOUSEOVER="this.className='tableMouseoverColor';" 
  ONMOUSEOUT="this.className='tableBgColor1Class';">
    <TD COLSPAN=2><STRONG>GIT</STRONG> </TD></TR>
  <TR CLASS=tableBgColor1Class 
  ONMOUSEOVER="this.className='tableMouseoverColor';" 
  ONMOUSEOUT="this.className='tableBgColor1Class';">
    <TD>Stock Date</TD>
    <TD><asp:textbox id=txtStockDateStockGIT runat="server" Width="227px" Enabled="False" CssClass="formfield"></asp:textbox></TD></TR>
  <TR CLASS=tableBgColor1Class 
  ONMOUSEOVER="this.className='tableMouseoverColor';" 
  ONMOUSEOUT="this.className='tableBgColor1Class';">
    <TD STYLE="HEIGHT: 23px">Stock Date Correct</TD>
    <TD STYLE="HEIGHT: 23px"><ASP:TEXTBOX id=txtStockDateCorrectGIT RUNAT="server" Width="227px" Enabled="False" CSSCLASS="formfield"></ASP:TEXTBOX></TD></TR>
  <TR CLASS=tableBgColor1Class 
  ONMOUSEOVER="this.className='tableMouseoverColor';" 
  ONMOUSEOUT="this.className='tableBgColor1Class';">
    <TD>Product No.</TD>
    <TD><asp:TextBox id=txtPhznrGIT runat="server" Width="227px" Enabled="False" CssClass="formfield"></asp:TextBox></TD></TR>
  <TR CLASS=tableBgColor1Class 
  ONMOUSEOVER="this.className='tableMouseoverColor';" 
  ONMOUSEOUT="this.className='tableBgColor1Class';">
    <TD>Presentation</TD>
    <TD><asp:TextBox id=txtPresentationGIt runat="server" Width="366px" Enabled="False" CssClass="formfield"></asp:TextBox></TD></TR>
  <TR CLASS=tableBgColor1Class 
  ONMOUSEOVER="this.className='tableMouseoverColor';" 
  ONMOUSEOUT="this.className='tableBgColor1Class';">
    <TD>Order No.</TD>
    <TD><asp:TextBox id=txtOrderNoGIT runat="server" Width="366px" Enabled="False" CssClass="formfield"></asp:TextBox></TD></TR>
  <TR CLASS=tableBgColor1Class 
  ONMOUSEOVER="this.className='tableMouseoverColor';" 
  ONMOUSEOUT="this.className='tableBgColor1Class';">
    <TD>Wyeth Order No.</TD>
    <TD><asp:TextBox id=txtWyethInvoiceNoGIT runat="server" Width="366px" Enabled="False" CssClass="formfield"></asp:TextBox></TD></TR>
  <TR CLASS=tableBgColor1Class 
  ONMOUSEOVER="this.className='tableMouseoverColor';" 
  ONMOUSEOUT="this.className='tableBgColor1Class';">
    <TD STYLE="HEIGHT: 22px">Units</TD>
    <TD STYLE="HEIGHT: 22px"><asp:TextBox id=txtUnitsGIT runat="server" Width="227px" Enabled="False" CssClass="formfield"></asp:TextBox></TD></TR>
  <TR CLASS=tableBgColor1Class 
  ONMOUSEOVER="this.className='tableMouseoverColor';" 
  ONMOUSEOUT="this.className='tableBgColor1Class';">
    <TD>Invoice Value</TD>
    <TD><asp:TextBox id=txtInvoiceValueGIT runat="server" Width="227" Enabled="False" CssClass="formfield"></asp:TextBox>&nbsp;<ASP:TEXTBOX id=txtCurrGIT RUNAT="server" Width="56px" Enabled="False" CSSCLASS="formfield"></ASP:TEXTBOX></TD></TR>
  <TR CLASS=tableBgColor1Class 
  ONMOUSEOVER="this.className='tableMouseoverColor';" 
  ONMOUSEOUT="this.className='tableBgColor1Class';">
    <TD>Comment</TD>
    <TD><asp:TextBox id=txtCommentGIT runat="server" Height="31px" Enabled="False" CssClass="formfield" Rows="2"></asp:TextBox></TD></TR></TABLE>
 </asp:panel>
</form>
    
  </body>
</HTML>
