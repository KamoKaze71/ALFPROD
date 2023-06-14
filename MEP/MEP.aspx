<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="MEP.aspx.vb" Inherits="Wyeth.Alf.MEP"%>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid, Version=1.1.20034.32, Culture=neutral, PublicKeyToken=589f1fc067ff4031" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
    <title>MEP</title>
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
			<P>
				   <ASP:PANEL ID=FilterPanel CSSCLASS="FilterPanel" RUNAT="server">
<DIV CLASS=noprint>
<TABLE CLASS=reportsFilter ID=Table1 CELLSPACING=0 CELLPADDING=0 WIDTH="100%"></TABLE></DIV>
<DIV CLASS=reportsButtonBar>
<TABLE ID=Table2>
  <TR>
    <TD NOWRAP><REP:REPORTDATA id=repData RUNAT="server"></REP:REPORTDATA></TD>
    <TD align=center width="100%">
      <DIV CLASS=noprint><ASP:DROPDOWNLIST id=ddMonthEnd RUNAT="server" WIDTH="192px"></ASP:DROPDOWNLIST><ASP:BUTTON id=btn_Invoices RUNAT="server" CSSCLASS="button" WIDTH="144px" TEXT="Process Invoices" HEIGHT="19px" CAUSESVALIDATION="False"></ASP:BUTTON>&nbsp; 
<ASP:BUTTON id=ExportExcel RUNAT="server" CSSCLASS="button" TEXT="Export to Excel" VISIBLE="true"></ASP:BUTTON>&nbsp;<BUTTON 
      CLASS=button ONCLICK=javascript:window.print(); TYPE=button>Print 
      Report</BUTTON> </DIV></TD></TR></TABLE></DIV></ASP:PANEL>
    
    
    
 <C1WebGrid:C1WebGrid id=MyGrid runat="server" SHOWFOOTER="True" AUTOGENERATECOLUMNS="False" DEFAULTCOLUMNWIDTH="120px" DEFAULTROWHEIGHT="22px">
<COLUMNS>
<c1webgrid:C1BoundColumn DataField="TypeNumber" Visible="False"></c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="type" Visible="False">
<GROUPINFO POSITION="Header" HEADERTEXT="Total {0}">
</GroupInfo>
</c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="code_code"></c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="stoc_date_open" HeaderText="Date open">
<ITEMSTYLE WRAP="False">
</ItemStyle>
</c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="stoc_date_accrued" HeaderText="Date accrued">
<ITEMSTYLE WRAP="False">
</ItemStyle>
</c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="stoc_date_correct" HeaderText="Date correct">
<ITEMSTYLE WRAP="False">
</ItemStyle>
</c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="stoc_order_number" HeaderText="Order No.">
<ITEMSTYLE WRAP="False">
</ItemStyle>
</c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="stoc_invoice_number" HeaderText="Wyeth Invoice No.">
<ITEMSTYLE WRAP="False">
</ItemStyle>
</c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="stoc_unit" Aggregate="Sum" HeaderText="Units">
<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right">
</ItemStyle>
</c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="stoc_invoice_value" Aggregate="Sum" HeaderText="Invoice Value">
<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right">
</ItemStyle>
</c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="stoc_accrued_value" Aggregate="Sum" HeaderText="Accrued Value">
<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right">
</ItemStyle>
</c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="stoc_diff_value" Aggregate="Sum" HeaderText="Diff to GIT">
<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right">
</ItemStyle>
</c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="stoc_diff_value_accrued" Aggregate="Sum" HeaderText="stoc_diff value_accrued">
<ITEMSTYLE WRAP="False" HORIZONTALALIGN="Right">
</ItemStyle>
</c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="stoc_comment" HeaderText="Comment">
<ITEMSTYLE WRAP="False">
</ItemStyle>
</c1webgrid:C1BoundColumn>
</Columns></C1WebGrid:C1WebGrid>

    </form>

  </body>
</HTML>
