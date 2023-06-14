<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TargetInput.aspx.vb" Inherits="Wyeth.Alf.TargetInput" EnableViewstate="true" %>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<%@ Register TagPrefix="prt" TagName="printReportCtl" Src="../../Util/printReportCtl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TargetInput</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Styles.css" type="text/css" rel="stylesheet">
		<asp:literal id="_clientScript" runat="server"></asp:literal>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table width="100%">
				<tr>
					<td>Year:&nbsp;<asp:dropdownlist id="ddYear" runat="server" AutoPostBack="True"></asp:dropdownlist>&nbsp; 
						TPG:&nbsp;<asp:dropdownlist id="ddTPG" runat="server" AutoPostBack="True"></asp:dropdownlist>&nbsp; 
						Sales Rep:&nbsp;<asp:dropdownlist id="ddSare" runat="server" AutoPostBack="True"></asp:dropdownlist>&nbsp; 
						Version:&nbsp;<asp:dropdownlist id="ddVersion" runat="server" AutoPostBack="True"></asp:dropdownlist>
					</td>
				</tr>
				<tr>
					<td colSpan="4">
						<DIV class="reportsButtonBar" style="TEXT-ALIGN: left">&nbsp;<asp:button id="btn_show_targets" runat="server" CssClass="button" Text="Show Targets"></asp:button>&nbsp;<asp:button id="btn_new_version" runat="server" CssClass="button" Text="new Target Version"></asp:button>&nbsp;<INPUT class="button" onclick="self.print();" type="button" value="Print">
							<!--<prt:printreportctl id="prtControl" runat="server"></prt:printreportctl> --></DIV>
					</td>
				</tr>
			</table>
			<asp:panel id="EditPanel" runat="server" CssClass="EditPanel" Visible="true" Width="100%">
				<asp:label id="lblSare" style="TEXT-ALIGN: center" runat="server" CssClass="headline" Visible="true"
					Width="100%"></asp:label>
				<asp:label id="lblsuccess" runat="server" Visible="false" Width="100%"></asp:label>
				<TABLE height="100%" align="center">
					<TR>
						<TD style="HEIGHT: 14px" align="left" colSpan="6">
							<asp:Label id="lblTargetType" runat="server"></asp:Label><br>
							<asp:label id="lblCurrency" runat="server" CssClass="currency" Visible="true" Width="408px"></asp:label></TD>
					</TR>
					<TR class="headline" height="20">
						<TD class="tableBgColor2Class" align="left" width="200">Product</TD>
						<TD class="tableBgColor2Class" align="center">Q1</TD>
						<TD class="tableBgColor2Class" align="center">Q2</TD>
						<TD class="tableBgColor2Class" align="center">Q3</TD>
						<TD class="tableBgColor2Class" align="center">Q4</TD>
						<TD class="tableBgColor2Class" align="center">Total:</TD>
					</TR>
					<asp:Repeater id="TargetRepeater" runat="server" EnableViewState="True">
						<ItemTemplate>
							<tr class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
								<td>
									<asp:label visible="True" runat="server" id="txtProductCCdesc">
										<%# DataBinder.Eval(Container.DataItem,"PROD_CC_DESCRIPTION") %>
									</asp:label>
									<input type=hidden  id="txtCCid" runat="server"  value='<%# DataBinder.Eval(Container.DataItem,"PROD_CC_ID") %>'>
								</td>
								<td align="right" width="100px">
									<box:wyethtextbox id="txtQ1" runat="server" TabIndex=1 EnableViewState="false" FieldType="DOUBLE" AllowBlank="TRUE" FriendlyName="Q1" CssClass="formfieldNumber" Text='<%# DataBinder.Eval(Container.DataItem,"TARG_Q1_VALUE") %>'>
									</box:wyethtextbox>
								</td>
								<td align="right" width="100px">
									<box:wyethtextbox id="txtQ2" runat="server" TabIndex=2 EnableViewState="false" FieldType="DOUBLE" AllowBlank="TRUE" FriendlyName="Q2" CssClass="formfieldNumber" Text='<%# DataBinder.Eval(Container.DataItem,"TARG_Q2_VALUE") %>'>
									</box:wyethtextbox>
								</td>
								<td align="right" width="100px">
									<box:wyethtextbox id="txtQ3" runat="server" TabIndex=3 EnableViewState="false" FieldType="DOUBLE" AllowBlank="TRUE" FriendlyName="Q3" CssClass="formfieldNumber" Text='<%# DataBinder.Eval(Container.DataItem,"TARG_Q3_VALUE") %>'>
									</box:wyethtextbox>
								</td>
								<td align="right" width="100px">
									<box:wyethtextbox id="txtQ4" runat="server" TabIndex=4 EnableViewState="false" FieldType="DOUBLE" AllowBlank="TRUE" FriendlyName="Q4" CssClass="formfieldNumber" Text='<%# DataBinder.Eval(Container.DataItem,"TARG_Q4_VALUE") %>'>
									</box:wyethtextbox>
								</td>
								<td align="right" width="100px">
									<asp:label id="lblSumQ" runat="server" Font-Bold="True"></asp:label></td>
								</td>
							</tr>
						</ItemTemplate>
					</asp:Repeater>
					<TR class="headline">
						<TD align="left" width="200">Total:</TD>
						<TD align="right">
							<asp:label id="lblSumQ1" runat="server"></asp:label></TD>
						<TD align="right">
							<asp:label id="lblSumQ2" runat="server"></asp:label></TD>
						<TD align="right">
							<asp:label id="lblSumQ3" runat="server"></asp:label></TD>
						<TD align="right">
							<asp:label id="lblSumQ4" runat="server"></asp:label></TD>
						<TD align="right">
							<asp:label id="lblSumTotal" runat="server"></asp:label></TD>
					</TR>
				</TABLE>
				<TABLE cellSpacing="2" cellPadding="2" border="0">
					<TR>
						<TD class="endline">
							<asp:Button id="Button_save_editpanel" runat="server" CssClass="Button" Text="Save" Width="110px"></asp:Button></TD>
					</TR>
				</TABLE>
			</asp:panel><asp:label id="lblTapg_descr" runat="server" Width="100%" visible="False"></asp:label><asp:label id="lbltapgid" runat="server" visible="False"></asp:label></form>
	</body>
</HTML>
