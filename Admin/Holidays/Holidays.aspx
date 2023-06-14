<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Holidays.aspx.vb" Inherits="Wyeth.Alf.Holidays1"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Holidays</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Styles.css" type="text/css" rel="stylesheet">
		<LINK media="print" href="../../printing.css" type="text/css" rel="stylesheet">
		<LINK href="holidays.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="partitioning.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div class="HL"><asp:label id="lblPageTitle" runat="server" Width="100%"></asp:label></div>
			<asp:panel id="FilterPanel" runat="server">
				<DIV class="reportsButtonBar">
					<TABLE>
						<TR>
							<TD class="reportSummary" noWrap><STRONG>Last Holiday entry:</STRONG><BR>
								<asp:Label id="lastHolidayEntry" Runat="server"></asp:Label></TD>
							<TD align="center" width="100%">
								<DIV class="noprint">
									<asp:Button id="newHolidayButton" CssClass="button" Runat="server" Text="Add new holiday"></asp:Button>&nbsp;
									<INPUT class="button" id="switchViewButton" type="button" Runat="server"></DIV>
							</TD>
						</TR>
					</TABLE>
				</DIV>
			</asp:panel>
			<br>
			<asp:Panel ID="calendarView" Runat="server" Visible="False">
				<asp:Calendar id="holidayCalendar" Width="100%" Runat="server"
				NextMonthText="next month >>" PrevMonthText="<< previous month"
				DayNameFormat=full TitleFormat=MonthYear BorderWidth=0>
					<DayStyle Height="100" BorderColor="LightGrey" BorderWidth=2 VerticalAlign=Top HorizontalAlign=Left />
					<OtherMonthDayStyle BackColor="#eeeeee" />
					<TitleStyle BackColor="#ffffff" CssClass="head" />
					<DayHeaderStyle CssClass="dayHeader" />
					<NextPrevStyle CssClass="nextPrev" />
				</asp:Calendar>
			</asp:Panel>
			<asp:Panel ID="tableView" Runat="server" Visible="False"></asp:Panel></form>
	</body>
</HTML>
