<%@ Page Language="vb" AutoEventWireup="false" Codebehind="JDEUploadConfirmation.aspx.vb" Inherits="Wyeth.Alf.JDEUploadConfirmation"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>JDEUploadConfirmation</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<asp:literal id="_clientScript" runat="server"></asp:literal>
		<asp:literal id="Literal1" runat="server"></asp:literal>
		<STYLE>#lblProcessMonth { FONT-WEIGHT: bold; FONT-SIZE: 11pt }
	#lblItems { FONT-WEIGHT: bold; FONT-SIZE: 11pt }
	#lblstartProcessing { FONT-WEIGHT: bold; FONT-SIZE: 11pt }
		</STYLE>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<DIV class="reportsButtonBar">
				<TABLE id="Table1" width="100%" align="center" RUNAT="server">
					<TR>
						<TD><asp:label id="lblProcessMonth" runat="server"></asp:label></TD>
					</TR>
				</TABLE>
			</DIV>
			<P></P>
			<table width="100%" align="center" RUNAT="server">
				<tr>
					<td><asp:label id="lblInfo" runat="server" HEIGHT="40px" Visible="True"></asp:label></td>
				</tr>
			</table>
			<p></p>
			<TABLE width="100%" align="center">
				<TR>
					<TD align="center"><ASP:BUTTON id="btn_confirm_upload_jde" TEXT="Confirm Final JDE Approval" RUNAT="server" CSSCLASS="button"></ASP:BUTTON></TD>
				</TR>
			</TABLE>
			<P></P>
			<TABLE width="100%" align="center">
				<TBODY>
					<TR>
						<TD align="center"><STRONG><asp:label id="lblProcessSuccess" runat="server" HEIGHT="40px" Visible="False"></asp:label></STRONG></TD>
					</TR>
			</ASP:PANEL></form>
		</TBODY></TABLE>
	</body>
</HTML>
