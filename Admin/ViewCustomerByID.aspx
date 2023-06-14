<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ViewCustomerByID.aspx.vb" Inherits="Wyeth.Alf.CegedimDeatil"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Customer Details</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<ASP:PANEL ID="FilterPanel" CSSCLASS="FilterPanel" RUNAT="server">
				<DIV class="reportsButtonBar">
					<TABLE>
						<TR>
							<TD noWrap>
								<REP:REPORTDATA id="repData" RUNAT="server"></REP:REPORTDATA></TD>
							<TD align="center" width="100%">
								<DIV class="noprint">&nbsp;<BUTTON class="button" id="close" onclick="javascript:window.close();" type="button">Close 
										Window</BUTTON>&nbsp;<BUTTON class="button" onclick="javascript:window.print();" type="button">Print 
										Report</BUTTON>
								</DIV>
							</TD>
						</TR>
					</TABLE>
				</DIV>
			</ASP:PANEL>
			<ASP:PANEL ID="EditPanel" RUNAT="server" VISIBLE="True" CSSCLASS="EditPanel" Height="224px">
				<TABLE cellSpacing="2" cellPadding="2" width="100%" border="0">
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field" width="120">Customer No.</TD>
						<TD colSpan="3">
							<ASP:TEXTBOX id="txtCustID" RUNAT="server" CSSCLASS="formField" Width="532px" ENABLED="False"></ASP:TEXTBOX></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field">Name</TD>
						<TD colSpan="3">
							<ASP:TEXTBOX id="txtCustName" RUNAT="server" CSSCLASS="formField" Width="531px" Enabled="False"></ASP:TEXTBOX></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field">Department</TD>
						<TD colSpan="3">
							<ASP:TEXTBOX id="txtCustDepartment" RUNAT="server" CSSCLASS="formField" Width="531px" Enabled="False"></ASP:TEXTBOX></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field">Wyeth Name</TD>
						<TD colSpan="3">
							<ASP:TEXTBOX id="txtCustWyethName" RUNAT="server" CSSCLASS="formField" Width="531px" Enabled="False"></ASP:TEXTBOX></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field">Address</TD>
						<TD colSpan="3">
							<ASP:TEXTBOX id="txtCustAddress" RUNAT="server" CSSCLASS="formField" Width="531px" Enabled="False"></ASP:TEXTBOX></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field">City</TD>
						<TD style="WIDTH: 284px">
							<ASP:TEXTBOX id="txtCustCity" RUNAT="server" CSSCLASS="formField" Width="245px" Enabled="False"></ASP:TEXTBOX></TD>
						<TD class="field" style="WIDTH: 28px" width="28">ZIP</TD>
						<TD>
							<ASP:TEXTBOX id="txtCustZip" RUNAT="server" CSSCLASS="formField" Width="207px" Enabled="False"></ASP:TEXTBOX></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field" style="HEIGHT: 18px">Group</TD>
						<TD style="HEIGHT: 18px" colSpan="3">
							<asp:DropDownList id="ddCustGroup" runat="server" Width="352px"></asp:DropDownList>&nbsp;
						</TD>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field" style="HEIGHT: 18px">Assign to TPGroup</TD>
						<TD style="HEIGHT: 18px" colSpan="3">
							<asp:DropDownList id="ddTAPG" runat="server" Width="352px"></asp:DropDownList>&nbsp;
							<asp:Button id="btn_assign" runat="server" CssClass="button" Text="Assign to TPGroup"></asp:Button></TD>
					</TR>
				</TABLE>
			</ASP:PANEL>
		</form>
	</body>
</HTML>
