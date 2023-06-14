<%@ Register TagPrefix="IntranetMenu" Namespace="wyeth.alf" Assembly="alf" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Header.aspx.vb" Inherits="Wyeth.Alf.Header" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ALF</title>
		<meta content="False" name="vs_snapToGrid">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="index_style.css" type="text/css" rel="stylesheet">
		<script language="javascript">
var strJSAff = 'austria-ece';
var currSelected;

//*********************************************************************
//* LINK FOR THE MENU.
//*********************************************************************
function select(link) {
	try {
		currSelected.className = 'topnav navbackground'
	} catch(e) {
	}
	link.className = 'navbackgroundHilite'
	currSelected = link;
}
		</script>
	</HEAD>
	<body style="BACKGROUND-ATTACHMENT: fixed; BACKGROUND-IMAGE: url(Images/headeroben.jpg); BACKGROUND-REPEAT: no-repeat" onselectstart="javascript:select('td0');">
	
		<table border="0" height="100%">
			<tr valign=bottom >
				<td valign=bottom>
				<IMG SRC="Images/logo.gif"  ALIGN=absmiddle>
				<span class="header_small">Austria Logistics and Finance System</span>
				<INTRANETMENU:INTRANETMENU id="Menu1" MenuHeader="True" MenuCtryID="56" Height="23px" runat="server"></INTRANETMENU:INTRANETMENU>
				</td>
			</tr>
		</table>
	</body>
</HTML>
