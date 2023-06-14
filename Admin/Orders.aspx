<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Orders.aspx.vb" Inherits="Wyeth.Alf.Orders"%>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<%@ Register TagPrefix="sum" Namespace="Wyeth.Alf" Assembly="Alf" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<TITLE>Orders</TITLE>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<!-- DATAGRID. LISTE DER DATEN -->
		<FORM id="Form1" method="post" runat="server">
			<asp:Panel Runat="server" ID="viewData">
				<DIV class="HL">
					<asp:label id="lblPageTitle" runat="server"></asp:label></DIV>
				<TABLE cellPadding="3">
					<TR>
						<TD class="field">Start-Date:</TD>
						<TD>
							<box:wyethtextbox id="inputStartDate" tabIndex="10" runat="server" FieldType="DATE" FriendlyName="Start-Date"
								AllowBlank="false"></box:wyethtextbox></TD>
						<TD><A onclick="window.open('../Util/Datepicker.aspx?textbox=Form1.inputStartDate','cal','width=225,height=200,left=270,top=180')"
								href="#"><IMG alt="Click here to select pick a date" src="/images/kalender.gif" border="0">
							</A>
						</TD>
						<TD class="field">End-Date:</TD>
						<TD>
							<box:wyethtextbox id="inputEndDate" tabIndex="20" runat="server" FieldType="DATE" FriendlyName="End-Date"
								AllowBlank="false"></box:wyethtextbox></TD>
						<TD><A onclick="window.open('../Util/Datepicker.aspx?textbox=Form1.inputEndDate','cal','width=225,height=200,left=270,top=180')"
								href="#"><IMG alt="Click here to select pick a date" src="/images/kalender.gif" border="0">
							</A>
						</TD>
						<TD>
							<asp:dropdownlist id="dropdownDistributor" tabIndex="30" EnableViewState="True" Runat="server"></asp:dropdownlist></TD>
						<TD>
							<asp:dropdownlist id="dropdownLine" tabIndex="40" EnableViewState="True" Runat="server"></asp:dropdownlist></TD>
						<TD><BUTTON class="button" id="btnDateEnter" tabIndex="50" type="submit" runat="server">View 
								Data</BUTTON></TD>
					</TR>
				</TABLE>
				<asp:CompareValidator id="CompareValidator1" Runat="server" Operator="GreaterThan" ControlToCompare="inputStartDate"
					ControlToValidate="inputEndDate" ErrorMessage="End-Date must be after Start-Date." Type="Date" Display="None"></asp:CompareValidator>
				<sum:ValidationSummary id="valSummary" runat="server"></sum:ValidationSummary>
				<BR>
				<asp:datagrid id="dataGridOrders" Runat="server" AllowSorting="true" AutoGenerateColumns="False">
					<Columns>
						<asp:BoundColumn DataField="stoc_id" Visible="false"></asp:BoundColumn>
						<asp:BoundColumn DataField="code_code" HeaderText="Code" SortExpression="code_code"></asp:BoundColumn>
						<asp:BoundColumn DataField="stoc_unit" HeaderText="Units" SortExpression="stoc_unit"></asp:BoundColumn>
						<asp:BoundColumn DataField="prod_presentation" HeaderText="Product-name" SortExpression="prod_presentation"></asp:BoundColumn>
						<asp:BoundColumn DataField="trandate" HeaderText="Date" SortExpression="trandate"></asp:BoundColumn>
						<asp:BoundColumn DataField="line_description" HeaderText="Line" SortExpression="line_description"></asp:BoundColumn>
						<asp:EditCommandColumn EditText="edit"></asp:EditCommandColumn>
					</Columns>
				</asp:datagrid>
			</asp:Panel>
			<!-- DATEN EDITIEREN -->
			<asp:Panel Runat="server" ID="viewUpdate">
				<TABLE cellPadding="2" width="100%">
					<TR>
						<TD class="field" noWrap>Unit:</TD>
						<TD width="100%"><INPUT class="formField" id="stoc_unit" type="text" runat="server"></TD>
					</TR>
					<TR>
						<TD class="endline" align="right" colSpan="20"><BUTTON id="back" type="button" runat="server">back</BUTTON>
						</TD>
					</TR>
				</TABLE>
			</asp:Panel>
		</FORM>
	</body>
</HTML>
