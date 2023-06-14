<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CustomerAdd.aspx.vb" Inherits="Wyeth.Alf.CustomerAdd"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>Add customer</title>
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
					<td valign="top"><div class="HL"><asp:Label ID="firstTitle" Runat="server"></asp:Label></div>
					</td>
				</tr>
				<tr>
					<td valign="top" align="center">
						<table cellpadding="5" width="100%">
							<tr>
								<td>
									<asp:Panel ID="searchBar" Runat="server">
            <TABLE cellSpacing=0 width="100%">
              <TR>
                <TD class=field noWrap>Keyword:</TD>
                <TD width="100%">
<asp:TextBox id=keyword Runat="server" CssClass="formfield"></asp:TextBox></TD>
                <TD><INPUT class=button id=searchBtn type=button value=Search runat="server"></TD></TR></TABLE>
									</asp:Panel>
								</td>
							</tr>
							<tr>
								<td align="center"><asp:DropDownList Width="100%" ID="dropdownCustomers" Runat="server" AutoPostBack="True"></asp:DropDownList></td>
							</tr>
							<tr>
								<td class="field">
									<asp:Label ID="labelCustomerInfo" Runat="server" CssClass="field"></asp:Label>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td><div class="HL"><asp:Label ID="secondTitle" Runat="server"></asp:Label></div>
					</td>
				</tr>
				<tr>
					<td valign="top">
						<asp:Panel ID="step2" Runat="server">
      <TABLE cellSpacing=0 cellPadding=0 width="100%" border=0>
        <TR>
          <TD class=HL2 align=center>
            <TABLE cellPadding=3>
              <TR>
                <TD>Used:</TD>
                <TD>
                  <DIV id=assigned>0%</DIV></TD><!--<TD>
													<TABLE height="10" cellSpacing="0" cellPadding="0" width="100">
														<TR>
															<TD bgColor="green">
																<DIV id="barUsed"></DIV>
															</TD>
															<TD bgColor="red">
																<DIV id="barLeft"></DIV>
															</TD>
														</TR>
													</TABLE>
												</TD>-->
                <TD>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Available:</TD>
                <TD>
                  <DIV id=freeAssign>100%</DIV></TD></TR></TABLE></TD></TR>
        <TR>
          <TD align=center>
<asp:Label id=labelMessage Runat="server" CssClass="nosuccess"></asp:Label></TD></TR>
        <TR>
          <TD 
          style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px" 
          align=center>
<asp:DataList id=datalistSalesReps Runat="server" Visible="False" RepeatDirection="Vertical" RepeatColumns="1" RepeatLayout="Table">
											<ItemTemplate>
												<table width="100%" cellpadding="2">
													<tr>
														<td width="100%" valign="middle" class="field"><%# container.dataitem("fullname") %></td>
														<td valign="middle" nowrap>&nbsp;&nbsp;&nbsp;&nbsp;
															<asp:TextBox Visible="false" ID="salesRepID" Runat="server"></asp:TextBox>
															<asp:TextBox MaxLength="5" ID="salesRepPercentage" CssClass="formField" Runat="server" Width="40">0</asp:TextBox>
														</td>
													</tr>
												</table>
											</ItemTemplate>
										</asp:DataList></TD></TR></TABLE>
      <SCRIPT language=javascript>
							<!--
							calculatePercent(assigned, freeAssign);
							//-->
							</SCRIPT>
						</asp:Panel>
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
