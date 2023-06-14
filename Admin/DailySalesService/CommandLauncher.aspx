<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CommandLauncher.aspx.vb" Inherits="Wyeth.Alf.WebForm" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Styles.css" type="text/css" rel="stylesheet">
		<asp:literal id="_clientScript" runat="server"></asp:literal>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" width="90%" align="center">
				<TR align="center">
					<TD align="center">
						<asp:button id="MyButton" runat="server" Text="restart DailySales.exe" CssClass="button" Width="150px"></asp:button></TD>
				</TR>
				<TR>
					<TD><BR>
						<BR>
						<BR>
					</TD>
				</TR>
				<TR>
					<TD align="center" height="30" >
						<asp:label id="MyLabel" runat="server" Width="100%" Height="100%" Font-Bold="True"></asp:label></TD>
				</TR>
				<tr>
					<td><asp:Label ID="lblOut" Runat="server"></asp:Label></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
