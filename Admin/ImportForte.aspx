<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ImportForte.aspx.vb" Inherits="Wyeth.Alf.ImportForte"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ImportForte</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div class="hl">
				<asp:label id="lblPageTitle" runat="server"></asp:label>
			</div>
			<table STYLE="WIDTH: 680px; HEIGHT: 462px">
				<tr>
					<td>
						<table height="100%" ALIGN="center">
							<tr>
								<td><asp:button id="button_forte_all" runat="server" CssClass="button_common" Text="Complete Forte Import"
										Width="320px"></asp:button></td>
							</tr>
							<tr>
							<tr>
								<td></td>
							</tr>
							<tr>
								<td><asp:button id="Button_forte_products" runat="server" CssClass="button_common" Text="Forte Products"
										Width="320px"></asp:button></td>
							</tr>
							<tr>
								<td><asp:button id="Button_forte_countries" runat="server" CssClass="button_common" Text="Forte Countries"
										Width="320px"></asp:button></td>
							</tr>
							<tr>
								<td><asp:button id="Button_forte_budget" runat="server" CssClass="button_common" Text="Forte Budget"
										Width="320px"></asp:button></td>
							</tr>
							<tr>
								<td><asp:button id="Button_forte_tcogs" runat="server" CssClass="button_common" Text="Forte TCogs"
										Width="320px"></asp:button></td>
							</tr>
							<tr>
								<td><asp:button id="Button_forte_fx_rate" runat="server" CssClass="button_common" Text="Forte FX Rate"
										Width="320px"></asp:button></td>
							</tr>
							<tr>
								<td><asp:button id="Button_forte_currencies" runat="server" CssClass="button_common" Text="Forte Currencies"
										Width="320px"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<td>
						<asp:panel id="EditPanel" runat="server" BackColor="GhostWhite" BorderColor="Black" BorderStyle="Solid"
							BorderWidth="1px" WIDTH="621px" HEIGHT="202px">
							<asp:label id="LabelOUT" runat="server" Width="602px" Height="200px"></asp:label>
						</asp:panel>
					</td>
				</TR>
			</table>
		</form>
	</body>
</HTML>
