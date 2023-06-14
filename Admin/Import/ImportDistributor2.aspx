<%@ Page Language="vb"  enableViewStateMac="false" AutoEventWireup="false" Codebehind="ImportDistributor2.aspx.vb" Inherits="Wyeth.Alf.ImportDistributor2"%>
<%@ Register TagPrefix="uc1" TagName="tempTableButtons" Src="../../Util/tempTableButtons.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ImportDistributor</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Styles.css" type="text/css" rel="stylesheet">
		<asp:literal id="_clientScript" runat="server"></asp:literal>
	</HEAD>
	<body>
		<form id="Form1" title="" name="" method="post" runat="server">
			<div class="reportsButtonBar">
				<table width="95%">
					<tr align="left">
						<td><asp:dropdownlist id="dddistribSelect" runat="server" AutoPostBack="True"></asp:dropdownlist></td>
						<td><ASP:RADIOBUTTONLIST id="rbl_AutomaticUpdate" WIDTH="176px" RUNAT="server" AUTOPOSTBACK="True" BORDERWIDTH="1px"
								BORDERSTYLE="Solid" BORDERCOLOR="Black">
								<ASP:LISTITEM VALUE="automatic" SELECTED="True">Automatic Update</ASP:LISTITEM>
								<ASP:LISTITEM VALUE="manual" SELECTED="false">Manual Update</ASP:LISTITEM>
							</ASP:RADIOBUTTONLIST></td>
						<td><uc1:temptablebuttons id="TempTableButtons1" runat="server"></uc1:temptablebuttons><input class="button" id="btn_delete_transmission" type="button" value="Delete Transmisison"
								runat="server"></input><asp:button id="btn_add_broken_bw" runat="server" Visible="False" Text="Add Broken BW" CssClass="button"></asp:button></td>
					</tr>
				</table>
			</div>
			<asp:panel id="auto_panel" runat="server">
				<TABLE id="tbl_import" height="79%" cellPadding="3" border="0" runat="server">
					<TR class="tableBgColor2Class" vAlign="top" height="30">
						<TD style="WIDTH: 433px" align="center" width="433">
							<asp:Button id="btn_connect" CssClass="button" Text="Connect to FTP Server" Runat="server"></asp:Button>
							<asp:Button id="btnConnectMuenster" CssClass="button" Text="Connect to Muenster Server" Visible="False"
								Runat="server"></asp:Button>
							<asp:Button id="btn_upload" runat="server" CssClass="button" Text="Upload Files" Visible="False"
								Width="188px"></asp:Button></TD>
						<TD width="100%">
							<asp:Button id="btn_Import" runat="server" CssClass="button" Text="Import Selected Files" Visible="False"></asp:Button>
							<asp:button id="btn_manual_import" runat="server" CssClass="button" Text="Import Sanova Data"
								Visible="False"></asp:button>
							<asp:button id="btn_importmuenster" runat="server" CssClass="button" Text="Import selected Muenster Data"
								Visible="False"></asp:button>
							<asp:button id="btn_load_into_tempTable" runat="server" CssClass="button" Text="Load Into tempTables"
								Visible="False"></asp:button>
							<asp:button id="btn_view_ftp_Files" runat="server" CssClass="button" Text="View Files" Visible="False"></asp:button>
							<asp:button id="btn_import_pharmosan" runat="server" CssClass="button" Text="Import Pharmosan  Data"
								Visible="False"></asp:button></TD>
					</TR>
					<TR class="tableBgColor1Class">
						<TD>
							<asp:ListBox id="lb_ftpfiles" runat="server" Visible="False" Width="270px" Height="100%"></asp:ListBox></TD>
						<TD vAlign="top">
							<asp:Label id="lblOut" Runat="server"></asp:Label></TD>
					</TR>
				</TABLE>
			</asp:panel><asp:textbox id="txtProgress" style="VISIBILITY: hidden" runat="server">none</asp:textbox></form>
		<script language="javascript">
		
		if (window.document.forms[0].txtProgress.value != 'none')
		{

	 window.document.forms[0].submit();
		}
		
		</script>
	</body>
</HTML>
