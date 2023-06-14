<%@ Page Language="vb" AutoEventWireup="false" Codebehind="StockCogsOLD.aspx.vb" Inherits="Wyeth.Alf.StockCogs"%>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid, Version=1.1.20034.32, Culture=neutral, PublicKeyToken=589f1fc067ff4031" %>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>StockCogs</title>
<meta content="Microsoft Visual Studio .NET 7.1" name=GENERATOR>
<meta content="Visual Basic .NET 7.1" name=CODE_LANGUAGE>
<meta content=JavaScript name=vs_defaultClientScript>
<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema><LINK href="../Styles.css" type=text/css rel=stylesheet >
  </HEAD>
<body MS_POSITIONING="GridLayout">
<form id=Form1 method=post runat="server"><asp:label id=lblPageTitle runat="server" CssClass="lblPageTitle" Width="100%"></asp:label><asp:panel 
id=FilterPanel CssClass="FilterPanel" Runat="server">
<TABLE WIDTH="100%">
  <TR>
    <TD>Start Date</TD>
    <TD><BOX:WYETHTEXTBOX id=txtStartDate runat="server" FieldType="DATE" AllowBlank="false" FriendlyName="Report Start Date" ToolTip="Please enter a date"></BOX:WYETHTEXTBOX><A 
      ONCLICK="window.open('../Util/Datepicker.aspx?textbox=Form1.txtStartDate','cal','width=250,height=225,left=270,top=180')" 
      HREF="javascript:;"><IMG SRC="/alf/Images/kalender.gif" align=absMiddle 
      border=0></A> </TD>
    <TD ROWSPAN=3><asp:Button id=btn_refresh runat="server" CssClass="button_common" Text="View Data"></asp:Button></TD>
    <TD ROWSPAN=3><asp:DropDownList id=ddLineselect runat="server" Width="144px"></asp:DropDownList><asp:DropDownList id=ddDistribSelect runat="server"></asp:DropDownList></TD>
    <TD align=right rowSpan=3>
      <TABLE WIDTH="100%">
        <TR align=right>
          <TD>Report Date From:</TD>
          <TD><asp:Label id=lblReportDateFrom runat="server"></asp:Label></TD></TR>
        <TR align=right>
          <TD>Report Date To:</TD>
          <TD><asp:Label id=lblReportDateTO runat="server"></asp:Label></TD></TR>
        <TR align=right>
          <TD>Last Order Entry: </TD>
          <TD><asp:Label id=lblLastOrderEntry runat="server"></asp:Label></TD></TR></TABLE></TD></TR>
  <TR>
    <TD>End Date </TD>
    <TD><BOX:WYETHTEXTBOX id=txtEndDate runat="server" FieldType="DATE" AllowBlank="false" FriendlyName="Report End Date" ToolTip="Please enter a date" AutoPostBack="True"></BOX:WYETHTEXTBOX><A 
      ONCLICK="window.open('../Util/Datepicker.aspx?textbox=Form1.txtEndDate','cal','width=250,height=225,left=270,top=180')" 
      HREF="javascript:;"><IMG SRC="/alf/Images/kalender.gif" align=absMiddle 
      border=0></A> </TD></TR></TABLE></asp:panel><asp:panel id=GridPanel CssClass="GridPanel" Runat="server"><C1WebGrid:C1WebGrid id=MyGrid runat="server" GroupIndent="10px" width="100%" AutoGenerateColumns="False" DefaultRowHeight="22px" DefaultColumnWidth="120px">
<COLUMNS>
<c1webgrid:C1BoundColumn DataField="prod_id" Visible="False">
<GROUPINFO OUTLINEMODE="StartCollapsed">
</GroupInfo>
</c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="BEWEGKZ_DESC" Visible="False" HeaderText="Code">
<GROUPINFO POSITION="Header" HEADERTEXT="TOTAL {0}" OUTLINEMODE="StartCollapsed">
</GroupInfo>
</c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="prgr_description" Visible="False" HeaderText="Product Group">
<GROUPINFO POSITION="Header" HEADERTEXT="TOTAL {0}">
</GroupInfo>
</c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="PROD_DESCRIPTION" Visible="False" HeaderText="Description">
<GROUPINFO POSITION="Header" HEADERTEXT="TOTAL {0}">
</GroupInfo>
</c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="prod_phznr" Visible="False" HeaderText="Product. No.">
<GROUPINFO POSITION="Header" HEADERTEXT="TOTAL {0}">
</GroupInfo>
</c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="tran_date" HeaderText="Date"></c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="order_no" HeaderText="Order No."></c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="units" Aggregate="Sum" HeaderText="Units">
<ITEMSTYLE HORIZONTALALIGN="Right">
</ItemStyle>

<FOOTERSTYLE HORIZONTALALIGN="Right">
</FooterStyle>
</c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="std_cogs" HeaderText="STD Cogs">
<ITEMSTYLE HORIZONTALALIGN="Right">
</ItemStyle>
</c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="total_cogs" Aggregate="Sum" HeaderText="Total Cogs">
<ITEMSTYLE HORIZONTALALIGN="Right">
</ItemStyle>

<FOOTERSTYLE HORIZONTALALIGN="Right">
</FooterStyle>
</c1webgrid:C1BoundColumn>
</Columns>
</C1WebGrid:C1WebGrid></asp:panel></FORM>
	</body>
</HTML>
