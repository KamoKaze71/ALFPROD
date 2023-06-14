<%@ Page Language="vb" AutoEventWireup="false" Codebehind="MVRefresh.aspx.vb" Inherits="Wyeth.Alf.MVRefresh"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>MVRefresh</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table width="100%" border="0">
				<tr>
					<td align="center">
						<p></p>
						<asp:Button id="btn_mv_refresh" runat="server" Text="Refresh MVs" Width="241px" Height="24px"
							CSSCLASS="button"></asp:Button>
					</td>
				</tr>
				<tr>
					<td align="center">
						<asp:Panel id="Panel1" runat="server" Width="80%" Height="100%" BACKCOLOR="GhostWhite">
							<asp:Label id="lblOut" runat="server" Height="100%" Width="100%" BackColor="GhostWhite"></asp:Label>
						</asp:Panel>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
