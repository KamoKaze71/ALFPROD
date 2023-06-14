<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SerViceControll.aspx.vb" Inherits="Wyeth.Alf.SerViceControll"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>SerViceControll</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Styles.css" type="text/css" rel="stylesheet">
		<meta http-equiv="refresh" content="5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<br>
			<br>
			<br>
			<table align="center" width="60%">
				<tr align="center">
					<td><asp:button id="MyButton" runat="server" Text="Button" CssClass="button" Width="150px"></asp:button>
					</td>
				</tr>
				<tr>
					<td><br>
						<br>
						<br>
					</td>
				</tr>
				<tr>
					<td align="center" height="30">
						<asp:label id="MyLabel" runat="server" Height="100%" Width="100%" Font-Bold="True"></asp:label>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
