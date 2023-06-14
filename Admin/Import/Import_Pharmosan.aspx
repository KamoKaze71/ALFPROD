<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Import_Pharmosan.aspx.vb" Inherits="Wyeth.Alf.Import_EnbrelTargets"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Import_EnbrelTargets</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Styles.css" type="text/css" rel="stylesheet">
		<asp:literal id="_clientScript" runat="server"></asp:literal>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:TextBox id="txtProgress" style="Z-INDEX: 101; LEFT: 16px; POSITION: absolute; TOP: -8px"
				runat="server" Visible="False"></asp:TextBox>
			<table width="100%" height="100%">
				<TBODY>
					<tr>
						<td>
							<asp:Button id="btn_upload" runat="server" Visible="True" Text="Upload Files" CssClass="button"></asp:Button>
						</td>
						<td><asp:Button id="btnImport" runat="server" Visible="True" Text="Import Files" CssClass="button"></asp:Button></td>
						<td>
						<td><asp:Button id="btnDelete" runat="server" Visible="True" Text="Delete Transmission" CssClass="button"></asp:Button></td>
					</tr>
					<tr height="100%">
						<td colspan="2">
							<asp:Label id="lblOut" runat="server" Height="100%" Width="100%"></asp:Label></td>
					</tr>
				</TBODY>
			</table>
		</form>
		<script language="javascript">
		
		if (window.document.forms[0].txtProgress.value != 'none')
		{

	 window.document.forms[0].submit();
		}
		
		</script>
	</body>
</HTML>
