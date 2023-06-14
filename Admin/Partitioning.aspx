<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Partitioning.aspx.vb" Inherits="Wyeth.Alf.Partitioning"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Partitioning</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<LINK media="print" href="../printing.css" type="text/css" rel="stylesheet">
		<style>.headers { PADDING-RIGHT: 15px; PADDING-LEFT: 15px; PADDING-BOTTOM: 15px; PADDING-TOP: 15px }
	.line { BACKGROUND-COLOR: #aaa }
	.section { BORDER-BOTTOM: #aaa 2px solid }
	.item { PADDING-RIGHT: 4px; PADDING-LEFT: 4px; PADDING-BOTTOM: 4px; MARGIN: 2px; PADDING-TOP: 4px; BACKGROUND-COLOR: #eee }
	.dgitem { PADDING-RIGHT: 4px; PADDING-LEFT: 4px; PADDING-BOTTOM: 4px; MARGIN: 2px; PADDING-TOP: 4px; BORDER-BOTTOM: #fff 4px solid; BACKGROUND-COLOR: #eee }
	#toggleCustomers { FONT-SIZE: 8pt; MARGIN-BOTTOM: 15px }
		</style>
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
							<TD width="100%">
								<asp:Button id="btnGenerate" Runat="server" EnableViewState="False" CssClass="button" Text="Select group"></asp:Button>&nbsp;
								<asp:Button id="btnNewGroup" Runat="server" EnableViewState="False" CssClass="button" Text="Create new group"></asp:Button></TD>
						</TR>
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
									<asp:Button id="btnEditGroup" Runat="server" EnableViewState="False" CssClass="button" Text="Modify this group"></asp:Button>&nbsp;
									<asp:Button id="btnDeleteGroup" Runat="server" EnableViewState="False" CssClass="button" Text="Delete this group"></asp:Button></DIV>
							</TD>
						</TR>
					</TABLE>
				</DIV>
				<DIV>
					<TABLE class="section" cellSpacing="0" cellPadding="5" width="100%">
						<TR>
							<TD class="field headers" vAlign="top" noWrap width="120">Products:<BR>
								<p></p><button class=button>Add product</button></TD>
							<TD vAlign="top">
								<asp:DataList id="dlProducts" Width="100%" Runat="server" ItemStyle-Width="33%" BorderStyle="None"
									RepeatLayout="Table" RepeatColumns="3" GridLines="None">
									<ItemTemplate>
										<div class="item">
											<table cellspacing="0" cellpadding="0">
												<tr>
													<td width="100%">
														<%# container.dataitem("CTRY_ID") %>
														-
														<%# container.dataitem("CTRY_DESCRIPTION") %>
													</td>
													<td>
														<asp:LinkButton Runat="server" ID="Linkbutton1" CommandArgument='<%# container.dataitem("CTRY_ID") %>' CommandName=delete>&nbsp;<img src="../Images/icon_trash.gif" alt="Remove this product" border="0" align="absmiddle"></asp:LinkButton></td>
												</tr>
											</table>
										</div>
									</ItemTemplate>
								</asp:DataList>
								<asp:Label id="lblnoProducts" Runat="server" Visible="False">No products added.</asp:Label></TD>
						</TR>
					</TABLE>
				</DIV>
				<DIV>
					<TABLE class="section" cellSpacing="0" cellPadding="5" width="100%">
						<TR>
							<TD class="field headers" vAlign="top" noWrap width="120">Sales Reps:<BR>
								<p></p><button class=button>Add Sales Rep</button></TD>
							<TD vAlign="top">
								<asp:DataList id="dlSalesReps" Runat="server" ItemStyle-Width="33%" BorderStyle="None" RepeatLayout="Table"
									RepeatColumns="3" GridLines="None" width="100%">
									<ItemTemplate>
										<div class="item">
											<table cellspacing="0" cellpadding="0">
												<tr>
													<td width="100%">
														<%# container.dataitem.item(0) %>
														-
														<%# container.dataitem.item(1) %>
													</td>
													<td>
														<asp:LinkButton Runat="server" ID="Linkbutton2" CommandArgument='<%# container.dataitem.item(0) %>' CommandName=delete>&nbsp;<img src="../Images/icon_trash.gif" alt="Remove this sales-rep" border="0" align="absmiddle"></asp:LinkButton></td>
												</tr>
											</table>
										</div>
									</ItemTemplate>
								</asp:DataList>
								<asp:Label id="lblnoSalesReps" Runat="server" Visible="False">No sales-reps added.</asp:Label></TD>
						</TR>
					</TABLE>
				</DIV>
				<DIV>
					<TABLE class="section" cellSpacing="0" cellPadding="5" width="100%">
						<TR>
							<TD class="field headers" vAlign="top" noWrap width="120">Customers:<BR>
								<p></p><button class=button>Add customer</button></TD>
							<TD vAlign="top">
								<DIV id="toggleCustomers">
									<asp:LinkButton id="linkCustomers" Runat="server">View customers</asp:LinkButton></DIV>
								<asp:DataGrid id="dgCustomers" Runat="server" GridLines="None" ShowHeader="False" CellPadding="4"
									CellSpacing="0" AutoGenerateColumns="False" Width=100%>
									<Columns>
										<asp:BoundColumn DataField="CTRY_DESCRIPTION" HeaderText="Customer">
											<HeaderStyle CssClass="head" Wrap=False></HeaderStyle>
											<ItemStyle CssClass="dgitem" Wrap=False></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="CTRY_ID" HeaderText="% assigned">
											<HeaderStyle Wrap="False" CssClass="head"></HeaderStyle>
											<ItemStyle CssClass="dgitem" Wrap=False></ItemStyle>
										</asp:BoundColumn>
										<asp:TemplateColumn HeaderText="Sales-Reps">
											<HeaderStyle Wrap="False" CssClass="head" Width=100%></HeaderStyle>
											<ItemStyle CssClass="dgitem" Width=100%></ItemStyle>
											<ItemTemplate>
												<asp:DataList ID="dlDatagridSalesReps" RepeatDirection="Horizontal" RepeatLayout="Flow" GridLines="None"
													Runat="server">
													<ItemTemplate>
														&nbsp;&nbsp;&nbsp;<input type=text class=formField style="width:25px;" value='<%# container.dataitem %>'>%&nbsp;
														<%# container.dataitem %>
													</ItemTemplate>
												</asp:DataList>
												&nbsp;&nbsp;&nbsp;&nbsp;<b><A href="">SAVE</A></b>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn>
											<ItemStyle CssClass="dgitem"></ItemStyle>
											<ItemTemplate>
												<img src="../Images/icon_trash.gif" alt="Remove this customer" border="0" align="absmiddle">
											</ItemTemplate>
										</asp:TemplateColumn>
									</Columns>
								</asp:DataGrid></TD>
						</TR>
					</TABLE>
				</DIV>
			</asp:panel></form>
	</body>
</HTML>
