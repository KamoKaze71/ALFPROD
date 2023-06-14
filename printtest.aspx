<%@ Register TagPrefix="rep" TagName="reportData" Src="Util/reportData.ascx" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="printtest.aspx.vb" Inherits="Wyeth.Alf.printtest"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>printtest</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<style>
			.rect1 { 
				BORDER-RIGHT: #9a9a9a 0.5pt dashed; 
				BORDER-TOP: #9a9a9a 0.5pt dashed; 
				BORDER-LEFT: #9a9a9a 0.5pt dashed; 
				BORDER-BOTTOM: #9a9a9a 0.5pt dashed; 
				POSITION: static;
				WIDTH: 19cm;  
				HEIGHT: 26cm 
			}
			.headers { PADDING-RIGHT: 15px; PADDING-LEFT: 15px; PADDING-BOTTOM: 15px; PADDING-TOP: 15px }
			.dgitem { PADDING-RIGHT: 4px; PADDING-LEFT: 4px; PADDING-BOTTOM: 4px; MARGIN: 2px; PADDING-TOP: 4px; BORDER-BOTTOM: #fff 4px solid; BACKGROUND-COLOR: #eee }
			.pagebreak { PAGE-BREAK-BEFORE: always }
			#toggleCustomers { FONT-SIZE: 8pt; MARGIN-BOTTOM: 15px }
		</style>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE class="section" cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td>
						<asp:Button ID="btnPrint" Runat="server" Text="Print Report" CssClass="button" OnClick="btnPrint_onClick"></asp:Button>
					</td>
				</tr>
				<tr>
					<td>
						<DIV class="reportsButtonBar">
							<TABLE>
								<TR>
									<TD noWrap>
										<rep:reportData id="repData" runat="server"></rep:reportData>
									</TD>
									<td width="100%">
										&nbsp;
									</td>
								</TR>
							</TABLE>
						</DIV>
					</td>
				</tr>
				<TR>
					<TD vAlign="top">
						<asp:DataGrid Visible="true" id="dgPrint" Runat="server" AutoGenerateColumns="False" GridLines="None"
							CellSpacing="0" CellPadding="0" ShowHeader="True">
							<Columns>
								<asp:BoundColumn DataField="CTRY_DESCRIPTION" HeaderText="Customer">
									<HeaderStyle CssClass="headers"></HeaderStyle>
									<ItemStyle Width="500" CssClass="dgitem"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CTRY_ID" HeaderText="assigned">
									<HeaderStyle Wrap="False" CssClass="headers"></HeaderStyle>
									<ItemStyle Width="100" CssClass="dgitem"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
						</asp:DataGrid>
					</TD>
				</TR>
				<tr>
					<td>
						<c1webgrid:c1webgrid id="MyGrid" runat="server" width="100%" AutoGenerateColumns="False" GroupIndent="10px"
							ShowFooter="false" EnableViewState="False" DefaultColumnWidth="120px" DefaultRowHeight="22px">
							<Columns>
								<c1webgrid:C1BoundColumn DataField="PROD_SEGMENT" Visible="False">
									<HeaderStyle wrap="false"></HeaderStyle>
									<FooterStyle Font-Bold="True"></FooterStyle>
									<GroupInfo FooterText="TOTAL {0}" ImageCollapsed="" Position="Footer" ImageExpanded="" OutlineMode="None"></GroupInfo>
								</c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn DataField="PRGR_DESCRIPTION" Visible="False">
									<HeaderStyle Wrap="False"></HeaderStyle>
									<ItemStyle Wrap="False"></ItemStyle>
									<FooterStyle Font-Bold="True"></FooterStyle>
								</c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn DataField="PROD_DESCRIPTION" HeaderText="Segement / Product">
									<HeaderStyle Wrap="False" cssClass="headlineSeperator" width="150px"></HeaderStyle>
									<ItemStyle Wrap="False"></ItemStyle>
								</c1webgrid:C1BoundColumn>
								<c1webgrid:C1BoundColumn DataField="ActDay" Aggregate="Sum" HeaderText="Actuals">
										<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
								</c1webgrid:C1BoundColumn>
							</Columns>
						</c1webgrid:c1webgrid>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
