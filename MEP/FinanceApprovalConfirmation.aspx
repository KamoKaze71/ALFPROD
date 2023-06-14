<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FinanceApprovalConfirmation.aspx.vb" Inherits="Wyeth.Alf.FinanceApprovalConfirmation"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FinanceApprovalConfirmation</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT LANGUAGE="JavaScript" SRC="../JS/ClientScripts.js"> </SCRIPT>
		<LINK HREF="../Styles.css" TYPE="text/css" REL="stylesheet">
		<asp:literal id="_clientScript" runat="server"></asp:literal>
	</HEAD>
	<BODY>
		<FORM ID="Form1" METHOD="post" RUNAT="server">
			<DIV CLASS="HL">
				<ASP:LABEL ID="lblPageTitle" RUNAT="server"></ASP:LABEL>
			</DIV>
			<P></P>
			<TABLE WIDTH="100%" ALIGN="center">
				<tr height="50">
					<td><asp:Label Runat="server" ID="lblInfo" Height="40px"></asp:Label>
					</td>
				</tr>
				<TR>
					<TD ALIGN="center" NOWRAP>
						<ASP:BUTTON ID="btn_approve_month" CSSCLASS="button" RUNAT="server" TEXT="Approve Month"></ASP:BUTTON>
						<BUTTON CLASS="button" ONCLICK="javascript:window.close();" TYPE="button">Cancel</BUTTON>
						<P></P>
					</TD>
				</TR>
			</TABLE>
			<ASP:PANEL ID="panelSuccess" RUNAT="server" VISIBLE="False" WIDTH="100%">
				<TABLE width="100%" align="center">
					<TR>
						<TD align="center"><STRONG>
								<ASP:LABEL id="lblProsessSuccess" RUNAT="server" HEIGHT="40px"></ASP:LABEL></STRONG></TD>
					</TR>
					<TR>
						<TD align="center"><BUTTON class="button" onclick="javascript:window.opener.print();" type="button">Print</BUTTON></TD>
					</TR>
				</TABLE>
			</ASP:PANEL>
		</FORM>
	</BODY>
</HTML>
