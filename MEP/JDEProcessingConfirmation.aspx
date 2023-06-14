<%@ Page Language="vb" AutoEventWireup="false" Codebehind="JDEProcessingConfirmation.aspx.vb" Inherits="Wyeth.Alf.JDEProcessingConfirmation"%>
<HTML>
	<HEAD>
		<title>JDE Processing Confirmation</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="JavaScript" src="../JS/ClientScripts.js"> </SCRIPT>
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<asp:literal id="_clientScript" runat="server"></asp:literal>
	</HEAD>
	<BODY>
		<form id="Form1" method="post" runat="server">
			<TABLE width="100%" align="center">
				<tr>
					<td height="50"><asp:label id="lblLastProcessing" runat="server" Visible="true" Height="40px"></asp:label>
						<p></p>
						</STRONG></td>
					</TD></tr>
				<TR>
					<TD align="center">
						<p></p>
						<ASP:BUTTON id="btn_process_jde" CSSCLASS="button" RUNAT="server" TEXT="Confirm Processing"></ASP:BUTTON><BUTTON class="button" onclick="javascript:window.close();" type="button">Cancel</BUTTON>
					</TD>
				</TR>
			</TABLE>
			<asp:Panel id="panelSuccess" runat="server" VISIBLE="true" WIDTH="100%">
				<TABLE width="100%" align="center">
					<TR>
						<TD align="center">
							<P></P>
							<STRONG>
								<asp:Label id="lblProsessSuccess" runat="server" Visible="true" HEIGHT="40px"></asp:Label></STRONG></TD>
					</TR>
				</TABLE>
			</asp:Panel></form>
		</SCRIPT>
	</BODY>
</HTML>
