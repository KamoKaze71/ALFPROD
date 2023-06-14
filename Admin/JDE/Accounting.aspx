<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Accounting.aspx.vb" Inherits="Wyeth.Alf.Accounting" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Accounting</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Styles.css" type="text/css" rel="stylesheet">
		<asp:literal id="_clientScript" runat="server"></asp:literal>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" width="100%">
				<TR align="left">
					<TD style="WIDTH: 164px">Select Accounting Record:</TD>
					<TD style="WIDTH: 106px"><asp:dropdownlist id="ddAccountingRecordName" runat="server" Width="312px"></asp:dropdownlist></TD>
					<TD><asp:button id="btn_select_type" runat="server" Width="142px" Text="Select Accounting Record"
							CssClass="button"></asp:button></TD>
					<TD><asp:button id="btn_CreateNewAccRe" runat="server" Width="162px" Text="Create New Accounting Record"
							CssClass="button"></asp:button></TD>
				</TR>
			</TABLE>
			<p></p>
			<div class="reportsbuttonBar" id="tblacrename" runat="server">
				<TABLE width="100%">
					<TR>
						<TD class="field" noWrap><STRONG><asp:label id="lblAcReName" Runat="server"></asp:label></STRONG></TD>
						<TD align="right" width="100%">
							<DIV class="noprint"><asp:button id="btn_modify" runat="server" Text="Edit Accounting Name" CssClass="button"></asp:button>&nbsp;
								<asp:button id="btnDeleteAcre" Text="Delete this Accounting Record" CssClass="button" Runat="server"
									EnableViewState="False"></asp:button></DIV>
							<div class="forPrint" style="FONT-SIZE: 8pt">
								<%= date.now() %>
							</div>
						</TD>
					</TR>
				</TABLE>
			</div>
			<p></p>
			<TABLE id="tblmain" cellPadding="2" width="100%" border="0" runat="server">
				<TR>
					<TD align="center" bgColor="#99cc99" colSpan="5" style="WIDTH: 389px">Debit Account</TD>
					<TD align="center" bgColor="#ff6666" colSpan="5">Credit Account</TD>
				</TR>
				<TR>
					<TD align="center" bgColor="#99cc99" colSpan="5" style="WIDTH: 389px"><asp:radiobuttonlist id="rbl_pl_debit" runat="server" RepeatDirection="Horizontal" AutoPostBack="True">
							<asp:ListItem Value="BS">BS</asp:ListItem>
							<asp:ListItem Value="PL">PL</asp:ListItem>
						</asp:radiobuttonlist></TD>
					<TD align="center" bgColor="#ff6666" colSpan="5"><asp:radiobuttonlist id="rbl_pl_credit" runat="server" RepeatDirection="Horizontal" AutoPostBack="True">
							<asp:ListItem Value="BS">BS</asp:ListItem>
							<asp:ListItem Value="PL">PL</asp:ListItem>
						</asp:radiobuttonlist></TD>
				</TR>
				<tr>
					<td colspan="3" align="center" bgColor="#99cc99">
						Company Code</td>
					<td bgColor="#99cc99">Account</td>
					<td bgColor="#99cc99">Subsidiary</td>
					<td colspan="3" align="center" bgColor="#ff6666">
						Company Code</td>
					<td bgColor="#ff6666">Account</td>
					<td bgColor="#ff6666">Subsidiary</td>
				</tr>
				<TR align="left">
					<TD bgColor="#99cc99" style="WIDTH: 76px"><asp:textbox id="txtCtry_debit" runat="server" Width="70" CssClass="formfield" alt="Debit Country Code"
							Enabled="false"></asp:textbox></TD>
					<TD bgColor="#99cc99"><asp:dropdownlist id="ddCC_debit" runat="server" Width="70" CssClass="formfield">
							<asp:ListItem Value="0000">General</asp:ListItem>
							<asp:ListItem Value="PRODUCT">Product</asp:ListItem>
							<asp:ListItem Value="NONE">None</asp:ListItem>
						</asp:dropdownlist></TD>
					<TD bgColor="#99cc99"><asp:textbox id="txtDepartment_debit" runat="server" Width="70" CssClass="formfield" MaxLength="5"></asp:textbox></TD>
					<TD bgColor="#99cc99"><asp:textbox id="txtAccount_debit" runat="server" Width="70" CssClass="formfield" MaxLength="5"></asp:textbox></TD>
					<TD bgColor="#99cc99" style="WIDTH: 77px"><asp:textbox id="txtSubsidiary_debit" runat="server" Width="70" CssClass="formfield" MaxLength="4"></asp:textbox></TD>
					<TD bgColor="#ff6666"><asp:textbox id="txtCtry_Credit" runat="server" Width="70" CssClass="formfield" Enabled="false"></asp:textbox></TD>
					<TD bgColor="#ff6666"><asp:dropdownlist id="ddCC_credit" runat="server" Width="70" CssClass="formfield">
							<asp:ListItem Value="0000">General</asp:ListItem>
							<asp:ListItem Value="PRODUCT">Product</asp:ListItem>
							<asp:ListItem Value="NONE">None</asp:ListItem>
						</asp:dropdownlist></TD>
					<TD bgColor="#ff6666"><asp:textbox id="txtDepartment_credit" runat="server" Width="70" CssClass="formfield" MaxLength="5"></asp:textbox></TD>
					<TD bgColor="#ff6666"><asp:textbox id="txtAccount_credit" runat="server" Width="70" CssClass="formfield" MaxLength="5"></asp:textbox></TD>
					<TD bgColor="#ff6666"><asp:textbox id="txtSubsidiary_credit" runat="server" Width="70" CssClass="formfield" MaxLength="4"></asp:textbox></TD>
				</TR>
				<tr>
					<td colSpan="12">
						<table width="100%" bgColor="gainsboro">
							<tr align="center" width="100%">
								<td>Line:<asp:dropdownlist id="ddLineSelect" runat="server"></asp:dropdownlist></td>
								<td>Active:<asp:checkbox id="chk_active" runat="server" Checked="true"></asp:checkbox></td>
								<td></td>
								<td>Invert:<asp:dropdownlist id="ddInvertSelect" runat="server">
										<asp:ListItem Value="-1">YES</asp:ListItem>
										<asp:ListItem Value="1">NO</asp:ListItem>
										<asp:ListItem Value="0,0150">0,015</asp:ListItem>
										<asp:ListItem Value="-0,0150">-0,015</asp:ListItem>
									</asp:dropdownlist></td>
							</tr>
						</table>
					</td>
				</tr>
			</TABLE>
			<p></p>
			<p></p>
			<TABLE id="tblbuttons" width="100%" runat="server">
				<TR align="left">
					<TD noWrap><asp:button id="btn_Add_bewegkz" runat="server" Width="110px" Text="Add Booking Code" CssClass="button"></asp:button></TD>
				</TR>
			</TABLE>
			<table id="tblDatalist" runat="server">
				<tr>
					<td><asp:datalist id="dlBewegKZs" Runat="server" width="100%" ItemStyle-VerticalAlign="Top" ItemStyle-Width="33%"
							BorderStyle="None" RepeatLayout="Table" RepeatColumns="3" GridLines="None" CellPadding="4">
							<ItemStyle VerticalAlign="Top"></ItemStyle>
							<ItemTemplate>
								<div class="item">
									<table cellspacing="0" cellpadding="0">
										<tr>
											<td width="100%">
												<%# container.dataitem.item("code_code") %>
												<%# container.dataitem.item("code_description") %>
											</td>
											<td>
												<div class="noprint">
													<asp:LinkButton Runat="server" ID="Linkbutton2" CommandArgument='<%# container.dataitem.item("code_id") %>' CommandName=delete>&nbsp;<img src="../../Images/icon_trash.gif" alt="Remove this Booking Code" border="0" align="absmiddle"></asp:LinkButton>
												</div>
											</td>
										</tr>
									</table>
								</div>
							</ItemTemplate>
						</asp:datalist></td>
				</tr>
			</table>
			<table id="tblSave" width="100%" runat="server">
				<tr>
					<td colSpan="2">
						<div class="endline" align="right"><asp:button id="buttonSave" Text="Save" CssClass="button" Runat="server"></asp:button>&nbsp;
						</div>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
