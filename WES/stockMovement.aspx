<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../Util/printReportCtl.ascx" %>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="stockMovement.aspx.vb" Inherits="Wyeth.Alf.stockMovement" %>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>stockMovement</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<LINK media="print" href="../printing.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../JS/ClientScripts.js" type="text/javascript">	</script>
		<asp:literal id="_clientScript" runat="server"></asp:literal>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div class="HL"><asp:label id="lblPageTitle" runat="server"></asp:label></div>
			<asp:panel id="FilterPanel" CssClass="FilterPanel" Runat="server">
				<DIV class="noprint">
					<TABLE class="reportsFilter" cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD class="field" noWrap>Start Date:</TD>
							<TD>
								<BOX:WYETHTEXTBOX id="txtStartDate" runat="server" FieldType="DATE" AllowBlank="false" FriendlyName="Report Start Date"
									ToolTip="Please enter a date"></BOX:WYETHTEXTBOX></TD>
							<TD>
								<asp:Image id="StartImage" runat="server" ImageAlign="AbsMiddle" ImageUrl="/images/KALENDER.GIF"></asp:Image></TD>
							<TD class="field" noWrap>End Date:</TD>
							<TD>
								<BOX:WYETHTEXTBOX id="txtEndDate" runat="server" FieldType="DATE" AllowBlank="false" FriendlyName="Report End Date"
									ToolTip="Please enter a date"></BOX:WYETHTEXTBOX></TD>
							<TD>
								<asp:Image id="EndImage" runat="server" ImageAlign="AbsMiddle" ImageUrl="/images/KALENDER.GIF"></asp:Image>
							<TD>
								<asp:DropDownList id="ddlineselect" runat="server"></asp:DropDownList></TD>
							<TD width="100%">
								<asp:DropDownList id="ddDistribSelect" runat="server"></asp:DropDownList>
								<asp:DropDownList id="ddValuesSelect" runat="server"></asp:DropDownList></TD>
						</TR>
						<TR>
							<TD colSpan="6">
								<asp:DropDownList id="ddProduct" runat="server" Width="100%"></asp:DropDownList></TD>
						</TR>
					</TABLE>
				</DIV>
				<DIV class="reportsButtonBar">
					<TABLE>
						<TR>
							<TD noWrap>
								<rep:reportData id="repData" runat="server"></rep:reportData></TD>
							<TD align="center" width="100%">
								<DIV class="noprint">
									<asp:Button id="btnGenRep" CssClass="button" Runat="server" Text="Generate Report"></asp:Button>&nbsp;
									<asp:Button id="ExportExcel" runat="server" CssClass="button" Text="Export to Excel" Visible="true"></asp:Button>&nbsp;
									<prt:printReportCtl id="prtControl" runat="server"></prt:printReportCtl></DIV>
							</TD>
						</TR>
					</TABLE>
				</DIV>
			</asp:panel>
			<p></p>
			<asp:panel id="GridPanel" CssClass="ReportPanel" Runat="server">
				<C1WebGrid:C1WebGrid id="MYGRid" runat="server" EnableViewState="False" ShowFooter="True" AllowAutoSort="True"
					width="100%" DefaultColumnWidth="120px" DefaultRowHeight="22px" AutoGenerateColumns="False">
					<COLUMNS>
						<c1webgrid:C1BoundColumn DataField="prod_id" Visible="False" SortExpression="prod_id"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prgr_description" Visible="True" SortExpression="prgr_description" HeaderText="Product Group"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="Prod_phznr" SortExpression="Prod_phznr" HeaderText="Prod.No.">
							<HeaderStyle cssClass="headlineSeperatorLeft"></HeaderStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="Prod_presentation" SortExpression="Prod_presentation" HeaderText="Presentation">
							<HeaderStyle cssClass="headlineSeperator"></HeaderStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="StartBalance" SortExpression="StartBalance" HeaderText="Start">
							<HeaderStyle cssClass="headlineSeperator"></HeaderStyle>
							<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="WE" SortExpression="WE" HeaderText="IN">
							<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="OUT" SortExpression="OUT" HeaderText="OUT">
							<HeaderStyle cssClass="headlineSeperator"></HeaderStyle>
							<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="OUT_FG" SortExpression="OUT_FG" HeaderText="FG">
							<HeaderStyle cssClass="headlineSeperator"></HeaderStyle>
							<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="KORR_ohne_um" SortExpression="KORR" HeaderText="CORR">
							<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="UM" SortExpression="UM" HeaderText="UM">
							<HeaderStyle cssClass="headlineSeperator"></HeaderStyle>
							<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="EndBalance" SortExpression="EndBalance" HeaderText="End">
							<HeaderStyle cssClass="headlineSeperator"></HeaderStyle>
							<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
						</c1webgrid:C1BoundColumn>
					</COLUMNS>
					<FOOTERSTYLE FONT-BOLD="True" HORIZONTALALIGN="Right"></FOOTERSTYLE>
				</C1WebGrid:C1WebGrid>
				<C1WebGrid:C1WebGrid id="MyGridValues" runat="server" EnableViewState="False" ShowFooter="True" AllowAutoSort="True"
					width="100%" DefaultColumnWidth="120px" DefaultRowHeight="22px" AutoGenerateColumns="False">
					<Columns>
						<c1webgrid:C1BoundColumn Visible="False" SortExpression="prod_id" DataField="prod_id"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prgr_description" Visible="True" SortExpression="prgr_description"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn SortExpression="Prod_phznr" HeaderText="Prod.No." DataField="Prod_phznr">
							<HeaderStyle CssClass="headlineSeperatorLeft"></HeaderStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn SortExpression="Prod_presentation" HeaderText="Presentation" DataField="Prod_presentation">
							<HeaderStyle CssClass="headlineSeperator"></HeaderStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn SortExpression="StartBalance" HeaderText="Start" DataField="StartBalance_std_cogs">
							<HeaderStyle CssClass="headlineSeperator"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn SortExpression="WE" HeaderText="IN" DataField="WE_std_cogs">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn SortExpression="OUT" HeaderText="OUT" DataField="OUT_std_cogs">
							<HeaderStyle CssClass="headlineSeperator"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn SortExpression="OUT_FG" HeaderText="FG" DataField="OUT_FG_std_cogs">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<HeaderStyle CssClass="headlineSeperator"></HeaderStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn SortExpression="KORR" HeaderText="CORR" DataField="KORR_ohne_um_std_cogs">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="UM_std_cogs" SortExpression="UM" HeaderText="UM">
							<HeaderStyle CssClass="headlineSeperator"></HeaderStyle>
							<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn SortExpression="EndBalance" HeaderText="End" DataField="EndBalance_std_cogs">
							<HeaderStyle CssClass="headlineSeperator"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</c1webgrid:C1BoundColumn>
					</Columns>
					<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
				</C1WebGrid:C1WebGrid>
			</asp:panel></form>
	</body>
</HTML>
