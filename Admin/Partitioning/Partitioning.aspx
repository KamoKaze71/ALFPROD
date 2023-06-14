<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Partitioning.aspx.vb" Inherits="Wyeth.Alf.Partitioning"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Partitioning</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Styles.css" type="text/css" rel="stylesheet">
		<LINK media="print" href="../../printing.css" type="text/css" rel="stylesheet">
		<LINK href="partitioning.css" type="text/css" rel="stylesheet">
		<asp:literal id="_clientScript" runat="server"></asp:literal>
		<script language="javascript" src="partitioning.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div class="HL"><asp:label id="lblPageTitle" runat="server" Width="100%"></asp:label></div>
			<asp:panel id="FilterPanel" runat="server">
				<DIV class="noprint">
					<TABLE class="reportsFilter" cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD class="field" noWrap>Select Target-Product-Group:</TD>
							<TD>
								<asp:dropdownlist id="ddTargetGroup" runat="server"></asp:dropdownlist></TD>
							<TD width="100%" align=right>
								<Button id="btnGenerate" Class="button" onclick="location.href='Partitioning.aspx?id=' + ddTargetGroup.value + '&pageTitle=<%= request.QueryString("pageTitle") %>';">Select group</Button>&nbsp;
								<BUTTON class="button" type="button" onclick="showPopup('TgpModify.aspx?id=', 440, 170);">Create new group</BUTTON>&nbsp;
								
								<button onclick="window.print();" class="button">Print</button>
							
								
							</TD>
							
						</TR>
						<tr>
						<td  colspan=3 align=right >	<asp:button id="btnRefreshMV"  CssClass ="button" runat="server" Text="Refresh MVs"></asp:button></td>
						</tr>
					</TABLE>
				</DIV>
			</asp:panel>
			<p></p>
			<asp:panel id="pnlGroupData" EnableViewState="True" Runat="server">
				<DIV class="reportsButtonBar">
					<TABLE>
						<TR>
							<TD class="field" noWrap><STRONG>
									<asp:Label id="lblGroupName" Runat="server"></asp:Label></STRONG></TD>
							<TD align="right" width="100%">
								<DIV class="noprint">
									<BUTTON class="button" type="button" onclick="showPopup('TgpModify.aspx?id=<%= request.QueryString("id") %>', 440, 170);">Modify this group</BUTTON>&nbsp;
									<asp:Button id="btnDeleteGroup" Text="Delete this group" CssClass="button" EnableViewState="False"
										Runat="server"></asp:Button></DIV>
								<div class="forPrint" style="font-size:8pt;">
									<%= date.now() %>
								</div>
							</TD>
						</TR>
					</TABLE>
				</DIV>
				<div class="forPrint">
					<div class="title">Assigned Products</div>
				</div>
				<DIV>
					<TABLE class="section" cellSpacing="0" cellPadding="5" width="100%">
						<TR>
							<TD class="field headers" vAlign="top" noWrap>Products:
								<div class="noprint">
								<BR>
								<P></P>
								<BUTTON class="button" type="button" onclick="showPopup('ProductAdd.aspx?id=<%= request.QueryString("id") %>', 390, 220);">Add product</BUTTON></div></TD>
							<TD vAlign="top">
								<asp:DataList id="dlProducts" Width="100%" Runat="server" GridLines="None" RepeatColumns="3" RepeatLayout="Table"
									BorderStyle="None" ItemStyle-Width="33%" ItemStyle-VerticalAlign=Top>
									<ItemTemplate>
										<div class="item">
											<table cellspacing="0" cellpadding="0">
												<tr>
													<td width="100%">
														<%# container.dataitem("prod_presentation") %>
														&nbsp;(<%# container.dataitem("prod_phznr") %>)
													</td>
													<td>
														<div class="noprint">
														<asp:LinkButton Runat="server" ID="Linkbutton1" CommandArgument='<%# container.dataitem("prod_id") %>' CommandName=delete>&nbsp;<img src="../../Images/icon_trash.gif" alt="Remove this product" border="0" align="absmiddle"></asp:LinkButton>
														</div>	
													</td>
												</tr>
											</table>
										</div>
									</ItemTemplate>
								</asp:DataList>
								<asp:Label id="lblnoProducts" Runat="server" Visible="False">No products added.</asp:Label></TD>
						</TR>
					</TABLE>
				</DIV>
				<div class="forPrint">
					<div class="title">Assigned Sales Reps</div>
				</div>
				<DIV>
					<TABLE class="section" cellSpacing="0" cellPadding="5" width="100%">
						<TR>
							<TD class="field headers" vAlign="top" noWrap>Sales Reps:
								<div class="noprint">
								<BR>
								<P></P>
								<BUTTON class="button" type="button" onclick="showPopup('SalesRepAdd.aspx?id=<%= request.QueryString("id") %>', 350, 190);">Add Sales Rep</BUTTON>
								</div>
								</TD>
							<TD vAlign="top">
								<asp:DataList id="dlSalesReps" Runat="server" GridLines="None" RepeatColumns="3" RepeatLayout="Table"
									BorderStyle="None" ItemStyle-Width="33%" width="100%" ItemStyle-VerticalAlign=Top>
									<ItemTemplate>
										<div class="item">
											<table cellspacing="0" cellpadding="0">
												<tr>
													<td width="100%">
														<%# container.dataitem.item("fullname") %>
													</td>
													<td>
														<div class="noprint">
														<asp:LinkButton Runat="server" ID="Linkbutton2" CommandArgument='<%# container.dataitem.item("sare_id") %>' CommandName=delete>&nbsp;<img src="../../Images/icon_trash.gif" alt="Remove this sales-rep" border="0" align="absmiddle"></asp:LinkButton>
														</div>
													</td>
												</tr>
											</table>
										</div>
									</ItemTemplate>
								</asp:DataList>
								<asp:Label id="lblnoSalesReps" Runat="server" Visible="False">No sales-reps added.</asp:Label></TD>
						</TR>
					</TABLE>
				</DIV>
				<div class="forPrint">
					<div class="title">Assigned Customers incl. Sales Reps</div>
				</div>
				<DIV>
					<TABLE class="section" cellSpacing="0" cellPadding="5" width="100%">
						<TR>
							<TD class="field headers" vAlign="top" noWrap>Customers:
								<div class="noprint">
								<BR>
								<P></P>
								<BUTTON class="button" type="button" onclick="showPopup('CustomerAdd.aspx?id=<%= request.QueryString("id") %>', 640, 190);">Add customer</BUTTON>
								</div>
								</TD>
							<TD vAlign="top">
								<div class="noprint">
									<DIV id="toggleCustomers">
										<a href="" runat=server id=viewCustomersLink></a>
									</DIV>
								</div>
								<asp:DataGrid id="dgCustomers" Width="100%" Runat="server" GridLines="None" AutoGenerateColumns="False"
									CellSpacing="0" CellPadding="4" ShowHeader="False">
									<Columns>
										<asp:BoundColumn DataField="customer" HeaderText="Customer">
											<HeaderStyle CssClass="head" Wrap="False"></HeaderStyle>
											<ItemStyle CssClass="dgitem" Wrap="False" VerticalAlign=Top></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="cusr_percent" HeaderText="% assigned">
											<HeaderStyle Wrap="False" CssClass="head"></HeaderStyle>
											<ItemStyle CssClass="dgitem" Wrap="False" HorizontalAlign=Center VerticalAlign=Top Width=50></ItemStyle>
										</asp:BoundColumn>
										<asp:TemplateColumn HeaderText="Sales-Reps">
											<HeaderStyle Wrap="False" CssClass="head" Width="100%"></HeaderStyle>
											<ItemStyle CssClass="dgitem" Width="100%" VerticalAlign=top></ItemStyle>
											<ItemTemplate>
												<asp:DataList ID="dlDatagridSalesReps" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="2"
													GridLines="None" Runat="server">
													<ItemTemplate>
														<input type="hidden" runat="server" value='<%# container.dataitem.id %>' id="sareID">
														<nobr><span class="salesRep"><%# container.dataitem.percentage %>%&nbsp;
															<%# container.dataitem.name %></span>
														</nobr>
													</ItemTemplate>
												</asp:DataList>
												<span class="noprint">
													<span onclick="showPopup('CustomerAdd.aspx?id=<%= request.QueryString("id") %>&c=<%# container.dataitem("cust_id") %>', 480, 190);" style="color:#C50202;cursor:hand;">edit precentage-values</span>
												</span>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn>
											<ItemStyle CssClass="dgitem"></ItemStyle>
											<ItemTemplate>
												<div class="noprint">
												<asp:LinkButton ID=removeCustomer Runat=server CommandName=delCustomer CommandArgument='<%# container.dataitem("cust_id") %>'>
													<img src="../../Images/icon_trash.gif" alt="Remove this customer" border="0" align="absmiddle">
												</asp:LinkButton>
												&nbsp;
												</div>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:BoundColumn DataField="cust_id" Visible="False" />
										<asp:BoundColumn DataField="fullname" Visible="False" />
									</Columns>
								</asp:DataGrid></TD>
						</TR>
					</TABLE>
				</DIV>
			</asp:panel></form>
	</body>
</HTML>
