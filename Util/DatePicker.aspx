<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DatePicker.aspx.vb" Inherits="Wyeth.Alf.DatePicker" Culture="en-US"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Pick a Date</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table cellPadding="2">
				<tr>
					<td align="center"><asp:dropdownlist id="ddYear" Width="100%" AutoPostBack="True" Runat="server"></asp:dropdownlist><br>
						<asp:dropdownlist id="ddMonth" Width="100%" AutoPostBack="True" Runat="server"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td vAlign="middle" align="center"><asp:calendar id="Calendar" runat="server" Width="150" FirstDayOfWeek="Monday" CellSpacing="0"
							CellPadding="1" PrevMonthText="<<" NextMonthText=">>" NextPrevFormat="CustomText" DayNameFormat="FirstLetter" BackColor="#CCCCCC"
							ShowGridLines="false" OnSelectionChanged="Change_Date" BorderColor="#999999" BorderWidth="2" Height="150">
							<TitleStyle BackColor="#999999" ForeColor="#FFFFFF" />
							<NextPrevStyle />
							<TodayDayStyle ForeColor="#FFFFFF" BackColor="#BBBBBB" BorderColor="#FF0000" BorderWidth="2" />
							<DayHeaderStyle Font-Size="7" />
							<DayStyle BackColor="#FFFFFF" />
							<WeekendDayStyle ForeColor="#FF0000" BackColor="#FFFFFF" />
							<OtherMonthDayStyle ForeColor="#BBBBBB" BackColor="#FFFFFF" />
						</asp:calendar><button class="button" style="WIDTH: 100%" onclick="javascript:window.close();" type="button">Cancel</button>
					</td>
				</tr>
			</table>
			<asp:textbox id="Control" runat="server" Visible="False"></asp:textbox></form>
	</body>
</HTML>
