<%@ Page Language="vb" AutoEventWireup="false" Codebehind="UploadFiles.aspx.vb" Inherits="Wyeth.Alf.UploadFiles"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>UploadFiles</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Styles.css" type="text/css" rel="stylesheet">
		<asp:literal id="_clientScript" runat="server"></asp:literal>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table width="90%" align="center">
				<tr>
					<td><INPUT class="formfield" id="MyFile" type="file" name="MyFile" runat="server"></td>
				</tr>
				<tr>
					<td><asp:Button id="btn_upload" Runat="server" Text="upload file" CssClass="button"></asp:Button></td>
				</tr>
				<tr>
					<td>
						<asp:Label id="lblOut_manual" runat="server" Width="95%"></asp:Label>
					</td>
				</tr>
				<tr>
					<td align="right">
						<div class="endline" align="right">
							<button class="button" runat="server" type="button" id="btn_cancel">Close</button>
						</div>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
