<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DeleteTransmission.aspx.vb" Inherits="Wyeth.Alf.DeleteTransmission"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>DeleteTransmission</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<asp:literal id="_clientScript" runat="server"></asp:literal>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table width="100%" height="100%">
				<tr>
					<td style="WIDTH: 56px; HEIGHT: 38px"><ASP:DROPDOWNLIST id="ddTransmissions" CSSCLASS="formfield" RUNAT="server" Width="152px"></ASP:DROPDOWNLIST></td>
					<td style="HEIGHT: 38px">
						<ASP:BUTTON id="Button_delete_transmission" TEXT="Delete Transmission" CSSCLASS="button" RUNAT="server"
							Visible="True"></ASP:BUTTON></td>
				</tr>
				<tr>
					<td colspan="2"><asp:Label ID="lblOut" Runat="server"></asp:Label></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
