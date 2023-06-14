<%@ Page Language="vb" AutoEventWireup="false" Codebehind="NewEntityReport.aspx.vb" Inherits="Wyeth.Alf.NewEntityReport"%>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>ImportErrors</title>
<meta content="Microsoft Visual Studio .NET 7.1" name=GENERATOR>
<meta content="Visual Basic .NET 7.1" name=CODE_LANGUAGE>
<meta content=JavaScript name=vs_defaultClientScript>
<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema><LINK href="../Styles.css" type=text/css rel=stylesheet >
  </HEAD>
<body MS_POSITIONING="GridLayout">
<form id=Form1 method=post runat="server">
<DIV class=reportsButtonBar>
<TABLE>
  <TR>
    <TD noWrap><REP:REPORTDATA id=repData RUNAT="server"></REP:REPORTDATA></TD>
    <TD align=center width="100%">
      <DIV class=noprint>&nbsp;		<asp:DropDownList id="ddDistribSelect" runat="server"></asp:DropDownList>&nbsp; <asp:Button id="btnGenRep" CssClass="button" Runat="server" Text="Generate Report"></asp:Button> &nbsp;	&nbsp;<ASP:BUTTON id=ExportExcel RUNAT="server" CSSCLASS="button" TEXT="Export to Excel" VISIBLE="true"></ASP:BUTTON>&nbsp;<BUTTON 
      class=button onclick=javascript:window.print(); type=button 
      >Print Report</BUTTON> 
</DIV></TD></TR></TABLE></DIV><ASP:PANEL id=reportPanel RUNAT="server" CSSCLASS="reportpanel">
<asp:panel id=GridPanel runat="server" CssClass="GridPanel" Width="100%">
<C1WEBGRID:C1WEBGRID id=MyGrid runat="server" Width="100%" AutoGenerateColumns="False" DefaultColumnWidth="120px" DefaultRowHeight="22px" AllowAutoSort="True" AllowSorting="True">
						<Columns>
							<c1webgrid:C1BoundColumn Visible="False" SortExpression="cust_id" ReadOnly="True" HeaderText="Customer ID" 
 DataField="cust_id"></c1webgrid:C1BoundColumn>
							<c1webgrid:C1BoundColumn SortExpression="cust_date_changed" ReadOnly="True" HeaderText="Imported at" DataField="cust_date_changed">
								<ItemStyle Wrap="False"></ItemStyle>
							</c1webgrid:C1BoundColumn>
							<c1webgrid:C1BoundColumn SortExpression="cudi_customer_nr" ReadOnly="True" HeaderText="Customer No." DataField="cudi_customer_nr"></c1webgrid:C1BoundColumn>
							<c1webgrid:C1BoundColumn SortExpression="cust_name" ReadOnly="True" HeaderText="Customer Name" DataField="cust_name"></c1webgrid:C1BoundColumn>
							<c1webgrid:C1BoundColumn SortExpression="cust_department" HeaderText="Department" DataField="cust_department"></c1webgrid:C1BoundColumn>
							<c1webgrid:C1BoundColumn SortExpression="cust_street" ReadOnly="True" HeaderText="Address" DataField="cust_street"></c1webgrid:C1BoundColumn>
							<c1webgrid:C1BoundColumn SortExpression="cust_zip" ReadOnly="True" HeaderText="ZIP" DataField="cust_zip"></c1webgrid:C1BoundColumn>
							<c1webgrid:C1BoundColumn SortExpression="cust_city" ReadOnly="True" HeaderText="City" DataField="cust_city"></c1webgrid:C1BoundColumn>
							<c1webgrid:C1TemplateColumn HeaderText="TPG">
								<ItemTemplate>
									<asp:DropDownList id="ddtapg2" runat="server" Width="270px"></asp:DropDownList>
									<asp:Button id="btn_assign" runat="server" CssClass="button" Text="Assign To TPG"></asp:Button>
								</ItemTemplate>
							</C1webgrid:C1TemplateColumn>
							<c1webgrid:C1ButtonColumn CommandName="Row" Visible="False" Text="Button"></c1webgrid:C1ButtonColumn>
						</Columns>
					</C1WEBGRID:C1WEBGRID></asp:panel></ASP:PANEL></FORM>
	</body>
</HTML>
