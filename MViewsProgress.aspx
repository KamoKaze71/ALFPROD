<%@ Page Language="vb" AutoEventWireup="false" Codebehind="MViewsProgress.aspx.vb" Inherits="Wyeth.Alf.MViewsProgress"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>MViewsProgress</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<meta http-equiv="refresh" content="5">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<ASP:PANEL ID="Panel1" RUNAT="server" CSSCLASS="infobox" WIDTH="90%" HEIGHT="300px">
			<ASP:LABEL id="lblProgress" RUNAT="server" Width="100%"></ASP:LABEL>
			<BR>
			<BR>
			<TABLE id="progress" cellSpacing="0" cellPadding="0" RUNAT="server" width=100%>
				<TR>
					<TD id="inprogress" bgColor="red" RUNAT="server" align=left>
						<ASP:LABEL id="lblpercent" RUNAT="server" BackColor=green  Font-Bold=True Font-Size=12></ASP:LABEL></TD>
					<TD bgColor="gainsboro"></TD>
				</TR>
			</TABLE>
		</ASP:PANEL>
	</body>
</HTML>
