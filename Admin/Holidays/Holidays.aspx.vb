Public Class Holidays1
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents newHolidayButton As System.Web.UI.WebControls.Button
    Protected WithEvents switchViewButton As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents lastHolidayEntry As System.Web.UI.WebControls.Label
    Protected WithEvents holidayCalendar As System.Web.UI.WebControls.Calendar
    Protected WithEvents calendarView As System.Web.UI.WebControls.Panel
    Protected WithEvents tableView As System.Web.UI.WebControls.Panel

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private currentMonthInclHolidays() As String
    Private dayIteration As Integer
    Private holidayDataAccess As Holiday

    Public Sub New()
        holidayDataAccess = New Holiday
    End Sub

    Public ReadOnly Property isCalendarView() As Boolean
        Get
            If Request.QueryString("view") <> "" Then
                Return False
            Else
                Return True
            End If
        End Get
    End Property

    Public ReadOnly Property pageURL() As String
        Get
            Return Request.ServerVariables("SCRIPT_NAME") & "?pageTitle" & Request.QueryString("pageTitle")
        End Get
    End Property

    '************************************************************************************************************
    '* Page_Load 
    '************************************************************************************************************
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dayIteration = 0
        holidayDataAccess.HoliDayCountryID = Page.Session.Item("country_id")

        If Not Page.IsPostBack Then
            initForm()
            'setLatestHolidayEntry()
        End If
    End Sub

    '************************************************************************************************************
    '* setLatestHolidayEntry 
    '************************************************************************************************************
    Private Sub setLatestHolidayEntry()
        Dim latestEntry As Date = holidayDataAccess.getLatestHolidayEntry
        lastHolidayEntry.Text = latestEntry.ToString & " (" & latestEntry.DayOfWeek.ToString & ")"
    End Sub

    '************************************************************************************************************
    '* isWeekend 
    '************************************************************************************************************
    Private Function isWeekend(ByVal dat As Date) As Boolean
        If dat.DayOfWeek = DayOfWeek.Saturday Or dat.DayOfWeek = DayOfWeek.Sunday Then
            Return True
        Else
            Return False
        End If
    End Function

    '************************************************************************************************************
    '* fillData 
    '************************************************************************************************************
    Private Sub fillData(ByVal month As Integer, ByVal year As Integer)
        Dim holidayData As DataView
        holidayData = holidayDataAccess.GetHolidaysForMonth(month, year)

        'resize our array to the size of this months day-amount
        ReDim Preserve currentMonthInclHolidays(Date.DaysInMonth(year, month))

        If holidayData.Table.Rows.Count > 0 Then
            For i As Integer = 0 To holidayData.Table.Rows.Count - 1
                currentMonthInclHolidays(CDate(holidayData.Table.Rows(i).Item(1)).Day) = 1
            Next
        End If
    End Sub

    '************************************************************************************************************
    '* initForm 
    '************************************************************************************************************
    Private Sub initForm()
        'set title
        lblPageTitle.Text = Request.QueryString("pageTitle")

        'prepare switch-view button
        With switchViewButton
            Dim qs As String = ""
            If isCalendarView Then
                calendarView.Visible = True
                .Value = "Siwtch to Listview"
                qs = "&view=1"
            Else
                tableView.Visible = True
                .Value = "Siwtch to Calendarview"
            End If
            .Attributes.Add("onclick", "location.href='" & Me.pageURL & qs & "';")
        End With
    End Sub

    '************************************************************************************************************
    '* holidayCalendar_DayRender 
    '************************************************************************************************************
    Private Sub holidayCalendar_DayRender(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DayRenderEventArgs) Handles holidayCalendar.DayRender
        If Not e.Day.IsOtherMonth Then
            If Me.dayIteration = 0 Then
                fillData(e.Day.Date.Month, e.Day.Date.Year)
            End If

            If Me.currentMonthInclHolidays(e.Day.Date.Day) = 1 Then
                drawHolidayDay(e.Cell, e.Day.Date)
            End If

            dayIteration += 1
        End If
    End Sub

    '************************************************************************************************************
    '* drawHolidayDay 
    '************************************************************************************************************
    Private Sub drawHolidayDay(ByVal targetCell As TableCell, ByVal currentDate As Date)
        Dim txt As String
        Dim textLink As New LinkButton

        If isWeekend(currentDate) Then
            txt = "Weekend"
            targetCell.BackColor = Color.LightYellow
        Else
            txt = "Holiday"
            targetCell.BackColor = Color.FromArgb(&HFEE1C2)
        End If

        textLink.Text = " (" & txt & ")"
        targetCell.Controls.Add(textLink)
    End Sub
End Class
