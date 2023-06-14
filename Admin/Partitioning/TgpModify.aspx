<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TgpModify.aspx.vb" Inherits="Wyeth.Alf.TgpModify"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Modify Target-Product-Group</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Styles.css" type="text/css" rel="stylesheet">
		<LINK media="print" href="../../printing.css" type="text/css" rel="stylesheet">
		<LINK href="partitioning.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="partitioning.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" leftmargin="0" topmargin="0" style="OVERFLOW:auto">
		<form id="Form1" method="post" runat="server">
			<table cellpadding="0" cellspacing="0" width="100%" height="100%">
				<tr>
					<td valign="top"><div class="HL"><asp:Label ID="pageTitle" Runat="server"></asp:Label></div>
					</td>
				</tr>
				<tr>
					<td align="center"><asp:Label Runat="server" ID="errorDesc" CssClass="nosuccess" Visible="False">Description cannot be empty.</asp:Label></td>
				</tr>
				<tr>
					<td valign="top" align="center">
						<table cellpadding="5" width="90%">
							<tr>
								<td class="field" nowrap><strong>Description:</strong></td>
								<td width="100%"><asp:TextBox ID="tpgDescription" Runat="server" CssClass="formField"></asp:TextBox></td>
								<td class="field" nowrap><strong>Type:</strong></td>
								<td width="100%">
									<asp:DropDownList id="ddTPGType" runat="server"></asp:DropDownList></td>
							</tr>
							<tr>
								<td class="field" nowrap><asp:Label ID="dateLabel" Runat="server" Visible="False" CssClass="field"><strong>Date 
											changed:</strong></asp:Label></td>
								<td class="field" colspan="3">
									<asp:Label ID="labelDateLastChanged" Runat="server"></asp:Label>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td valign="bottom" colspan="10">
						<div class="endline" align="right">
							<asp:Button ID="buttonSave" Runat="server" CssClass="button" Text="Save"></asp:Button>&nbsp;
							<button class="button" onclick="window.close();" type="button">Cancel</button>
						</div>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
