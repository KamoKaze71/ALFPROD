<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="AssignGit.aspx.vb" Inherits="Wyeth.Alf.AssignGit"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
    <title>AssignGit</title>
<meta content="Microsoft Visual Studio .NET 7.1" name=GENERATOR>
<meta content="Visual Basic .NET 7.1" name=CODE_LANGUAGE>
<meta content=JavaScript name=vs_defaultClientScript>
<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema>
<SCRIPT language=JavaScript src="../JS/ClientScripts.js">	</SCRIPT>
<LINK href="../Styles.css" type=text/css rel=stylesheet >
  </HEAD>
<body MS_POSITIONING="GridLayout">
<form id=Form1 method=post runat="server">
<DIV class=HL><ASP:LABEL id=lblPageTitle WIDTH="100%" RUNAT="server"></ASP:LABEL></DIV>
<DIV class=reportsButtonBar>
<TABLE>
  <TR>
    <TD><REP:REPORTDATA id=repData RUNAT="server"></REP:REPORTDATA></TD>
    <TD align=center width="60%">
      <DIV class=noprint><BUTTON class=button 
      onclick=javascript:window.print(); type=button 
      >Print</BUTTON>&nbsp;<BUTTON class=button 
      onclick=javascript:closeGITAssignWindows(); type=button 
      >Close</BUTTON> 
</DIV></TD></TR></TABLE></DIV><asp:panel id=GridPanel runat="server" CSSCLASS="GridPanel" Width="90%"><C1WebGrid:C1WebGrid id=MyGrid runat="server" Width="100%" DEFAULTCOLUMNWIDTH="120px" DEFAULTROWHEIGHT="22px" AUTOGENERATECOLUMNS="False">
<COLUMNS>
<c1webgrid:C1BoundColumn DataField="stoc_id" Visible="False"></c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="dist_id" Visible="False"></c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="stoc_date_stock" HeaderText="Date">
<ITEMSTYLE WRAP="False">
</ItemStyle>
</c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="stoc_order_number" HeaderText="Order No."></c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="stoc_invoice_number" HeaderText="Wyeth Invoice Number"></c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="stoc_unit" HeaderText="Units"></c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="stoc_invoice_value" HeaderText="Invoice Value"></c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="curr_code" HeaderText="Currency"></c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="stoc_comment" HeaderText="Comment"></c1webgrid:C1BoundColumn>
<c1webgrid:C1ButtonColumn ButtonType="PushButton" Text="Assign GIT to WE"></c1webgrid:C1ButtonColumn>
<c1webgrid:C1BoundColumn DataField="CURR_ID_INVOICE" Visible="False"></c1webgrid:C1BoundColumn>
</Columns>
</C1WebGrid:C1WebGrid></asp:panel><asp:label id=lblNotFound runat="server" Width="448px" Height="40px" Visible="False"></asp:label><asp:textbox id=txtstock_id_we style="Z-INDEX: 101; LEFT: 152px; POSITION: absolute; TOP: 400px" runat="server" Visible="False"></asp:textbox></form>

  </body>
</HTML>
