<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Product.aspx.vb" Inherits="Wyeth.Alf.Product"%>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Product</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../JS/ClientScripts.js"> </script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			
			<DIV class="reportsButtonBar">
				<TABLE width="95%" cellpadding="2">
				<tr>
					<td noWrap>OBS/Non Obs:</td>
						<td noWrap><asp:DropDownList id="ddProductObs" runat="server" Width="120px" CssClass="formfield"></asp:DropDownList></TD>
						<td noWrap>TaPG:</td>
						<td noWrap><asp:DropDownList id="ddTapg" runat="server" Width="120px" CssClass="formfield"></asp:DropDownList></td>
					<td></td>
					
					</tr>
					<TR>
						<TD noWrap>Keyword:</TD>
						<TD noWrap>
							<asp:TextBox id="txtSearch" runat="server" Width="184px" CssClass="formfield"></asp:TextBox></TD>
						<td>Line:</td>
						<TD noWrap>
							<asp:DropDownList id="ddLineSelect" runat="server" Width="180px"></asp:DropDownList></TD>
						
						<TD noWrap>
							<asp:Button id="btn_show_products" runat="server" CssClass="button" CausesValidation="False"
								Text="Search"></asp:Button></TD>
						<TD noWrap>
							<ASP:BUTTON id="ExportExcel" RUNAT="server" VISIBLE="true" TEXT="Export to Excel" CSSCLASS="button"></ASP:BUTTON></TD>
						<TD noWrap><BUTTON class="button" onclick="javascript:window.print();" type="button">Print</BUTTON>
						</TD>
					</TR>
					
				</TABLE>
			</DIV>
			<asp:panel id="GridPanel" runat="server" cssclass="GridPanel" VISIBLE="true">
				<C1WebGrid:C1WebGrid id="MyGrid" runat="server" Width="100%" AllowAutoSort="True" AllowSorting="True"
					EnableViewState="False" DefaultRowHeight="22px" DefaultColumnWidth="120px" AutoGenerateColumns="False">
					<Columns>
						<c1webgrid:C1BoundColumn DataField="prod_id" Visible="False" SortExpression="prod_id" HeaderText="Product ID"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_phznr" SortExpression="prod_phznr" HeaderText="Poduct No."></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_description" SortExpression="prod_description" HeaderText="Product"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_presentation" SortExpression="prod_presentation" HeaderText="Presentation"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_packsize" SortExpression="prod_packsize" HeaderText="Packsize"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_strength" SortExpression="prod_strength" HeaderText="Strength"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_bus_unit" Visible="False" SortExpression="prod_bus_unit" HeaderText="Bus Unit"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_Segment" Visible="False" SortExpression="prod_Segment"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_Sub_segment" Visible="False" SortExpression="prod_Sub_segment"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_cc_id" Visible="False" SortExpression="prod_cc_id"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_cc_description" Visible="False" SortExpression="prod_cc_description"
							HeaderText="CC Description"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_fap" Visible="False" SortExpression="prod_fap" HeaderText="FAP"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_wws_item_code" Visible="False" SortExpression="prod_wws_item_code"
							HeaderText="WWS Item Code"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_fsd" Visible="False" SortExpression="prod_fsd" HeaderText="FSD"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_plant_item_number" Visible="False" SortExpression="prod_plant_item_number"
							HeaderText="P Item No."></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_manufacturer" Visible="False" SortExpression="prod_manufacturer"
							HeaderText="Manufacturer"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_mfglocation" Visible="False" SortExpression="prod_mfglocation" HeaderText="MfG Location"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_packer_code" Visible="False" SortExpression="prod_packer_code" HeaderText="Packer Code"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_invoicer_code" Visible="False" SortExpression="prod_invoicer_code"
							HeaderText="Invoicer"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_routing" Visible="False" SortExpression="prod_routing" HeaderText="Routing"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_info" Visible="False" SortExpression="prod_info"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_forte_product_id" Visible="False" SortExpression="prod_forte_product_id"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_status" Visible="False" SortExpression="prod_status"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_obs_code" Visible="False" SortExpression="prod_obs_code" HeaderText="Obs Code"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prgr_id" Visible="False" SortExpression="prgr_id"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="prod_id_sample_product" Visible="False" SortExpression="prod_id_sample_product"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="ctry_id" Visible="False" SortExpression="ctry_id"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="curr_id" Visible="False" SortExpression="curr_id"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="line_id" Visible="False" SortExpression="line_id"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="tapg_id" Visible="False" SortExpression="tapg_id"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="PROD_UNITS_MEASURE" Visible="False" SortExpression="PROD_UNITS_MEASURE"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1ButtonColumn Visible="False" ButtonType="PushButton" Text="Button"></c1webgrid:C1ButtonColumn>
					</Columns>
				</C1WebGrid:C1WebGrid>
			</asp:panel></form>
	</body>
</HTML>
