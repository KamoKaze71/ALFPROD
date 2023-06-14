<%@ Page Language="vb" AutoEventWireup="false" Codebehind="NewTargetVersion.aspx.vb" Inherits="Wyeth.Alf.NewTargetVersion"%>
<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>NewTargetVersion</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div class="reportsbuttonBar" style="TEXT-ALIGN: right" align="right"></div>
			<table width="100%">
				<tr>
					<td class="head" colSpan="2">Create new version for:</td>
				</tr>
				<tr>
					<TD>Year:</TD>
					<td><asp:dropdownlist id="ddyear" runat="server" Width="300px" AutoPostBack="True" CssClass="formfield"></asp:dropdownlist></td>
				</tr>
				<tr>
					<TD style="HEIGHT: 13px">TPG:</TD>
					<td style="HEIGHT: 13px"><asp:dropdownlist id="ddTPG" runat="server" Width="300px" AutoPostBack="True" CssClass="formfield"></asp:dropdownlist></td>
				</tr>
				<tr>
					<TD>Sales Rep:</TD>
					<td><asp:dropdownlist id="ddSare" runat="server" Width="300px" AutoPostBack="True" CssClass="formfield"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td>Version:</td>
					<td><asp:label Runat="server" id="lblVersion" CssClass="formfield"><%# New_version %></asp:label>
					</td>
				</tr>
			</table>
			<p></p>
			<table width="100%">
				<tr>
					<td class="head" colSpan="2">Copy values from:</td>
				</tr>
				<tr>
					<td><asp:checkbox id="chk_box_take_values" runat="server" AutoPostBack="True" Text="Copy values from previous version"
							Checked="True"></asp:checkbox></td>
				</tr>
				<tr>
					<td>
						<p>
							<TABLE id="tblCopy" width="100%" runat="server" visible="false">
								<TR>
									<TD style="HEIGHT: 17px">Year:</TD>
									<TD style="HEIGHT: 17px"><asp:dropdownlist id="ddyear_old" runat="server" Width="300px" CssClass="formfield"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD>Sales Rep:</TD>
									<TD><asp:dropdownlist id="ddSare_old" runat="server" Width="300px" AutoPostBack="True" CssClass="formfield"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD>Version:</TD>
									<TD><asp:dropdownlist id="ddVersion_old" runat="server" Width="300px" CssClass="formfield"></asp:dropdownlist></TD>
								</TR>
							</TABLE>
						</p>
					</td>
				</tr>
				<tr class="endline">
					<td><asp:button id="btn_save" runat="server" CssClass="button" Text="Create new version"></asp:button><BUTTON class="button" type="button" runat="server" id="btnclose">Close 
					Window</BUTTON>
					</td>
				</tr>
			</table>
			<asp:label id="lblsuccess" Font-Size="10pt" Width="100%" Runat="server"></asp:label>
		</form>
	</body>
</HTML>
