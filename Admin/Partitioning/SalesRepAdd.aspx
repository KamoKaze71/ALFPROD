<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SalesRepAdd.aspx.vb" Inherits="Wyeth.Alf.SalesRepAdd"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>SalesRep add</title>
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
					<td valign="top"><div class="HL">Add Sales-Rep</div>
					</td>
				</tr>
				<tr>
					<td><asp:Label ID="labelMessage" Runat="server" Visible=False CssClass=nosuccess></asp:Label></td>
				</tr>
				<tr>
					<td valign="top" align="center">
						<table cellpadding="5">
							<tr>
								<td><asp:DropDownList ID="dropdownProducts" Runat="server" AutoPostBack="True"></asp:DropDownList></td>
							</tr>
							<tr>
								<td>
									<div style="PADDING-RIGHT:10px;PADDING-LEFT:10px;PADDING-BOTTOM:10px;PADDING-TOP:10px">
										<asp:Label ID="labelProductInfo" Runat="server" CssClass="field"></asp:Label>
									</div>
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
