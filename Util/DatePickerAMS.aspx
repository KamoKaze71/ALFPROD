<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DatePickerAMS.aspx.vb" Inherits="Wyeth.Alf.DatePickerAMS" Culture="en-US"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Pick a Date</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellpadding="2">
				<tr>
					<td align="center">
						<asp:DropDownList Runat="server" AutoPostBack="True" ID="ddYear" Width="100%" /><br>
						<asp:DropDownList Runat="server" AutoPostBack="True" ID="ddMonth" Width="100%" />
					</td>
				</tr>
				<tr>
					<td valign="middle" align="center">
						<asp:Calendar Height="150" BorderWidth="2" BorderColor="#999999" Width="150" id="Calendar" OnSelectionChanged="Change_Date"
							runat="server" ShowGridLines="false" BackColor="#CCCCCC" DayNameFormat="FirstLetter" NextPrevFormat="CustomText"
							NextMonthText=">>" PrevMonthText="<<" CellPadding="1" CellSpacing="0" FirstDayOfWeek="Monday"
							SelectedDate="1995-12-01" VisibleDate="1995-12-01">
							<TodayDayStyle BorderWidth="2px" ForeColor="White" BorderColor="Red" BackColor="#BBBBBB"></TodayDayStyle>
							<DayStyle BackColor="White"></DayStyle>
							<DayHeaderStyle Font-Size="7pt"></DayHeaderStyle>
							<TitleStyle ForeColor="White" BackColor="#999999"></TitleStyle>
							<WeekendDayStyle ForeColor="Red" BackColor="White"></WeekendDayStyle>
							<OtherMonthDayStyle ForeColor="#BBBBBB" BackColor="White"></OtherMonthDayStyle>
						</asp:Calendar>
						<button onclick="javascript:window.close();" class="button" style="WIDTH:100%" type="button">
							Cancel</button>
					</td>
				</tr>
			</table>
			<asp:TextBox id="Control" runat="server" Visible="False" />
		</form>
	</body>
</HTML>
