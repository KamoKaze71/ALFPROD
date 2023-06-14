Public Class DatePickerAMS
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents Calendar As System.Web.UI.WebControls.Calendar
    Protected WithEvents Control As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddMonth As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Control.Text = Request.QueryString("textbox").ToString()

        If Not Page.IsPostBack Then
            Dim tmp As String

            fillMonths()
            fillYears()

        End If
    End Sub

    Private Sub fillMonths()
        Dim it As ListItem
        For i As Integer = 1 To 12
            it = New ListItem
            it.Value = i.ToString
            it.Text = MonthName(i)
            ddMonth.Items.Add(it)
        Next
        ddMonth.SelectedValue = 12
        ddMonth.DataBind()
    End Sub

    Private Sub fillYears()
        Dim it As ListItem
        '  For i As Integer = (Date.Today.Year - 5) To (Date.Today.Year + 5)
        For i As Integer = (1995) To (2002)

            it = New ListItem
            it.Value = i.ToString
            it.Text = i.ToString
            ddYear.Items.Add(it)
        Next
        ddYear.SelectedValue = 1995
        ddYear.DataBind()
    End Sub
   

    Protected Sub Change_Date(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim strScript As String
        strScript = "<script language =javascript >"
        strScript += "opener." & Control.Text & ".value='" & Calendar.SelectedDate.ToString("yyyy-MM-dd") & "';"
        strScript += " opener.focus();self.close();"
        strScript += "</script>"

        RegisterClientScriptBlock("anything", strScript)
    End Sub

    Private Sub calendars(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.MonthChangedEventArgs) Handles Calendar.VisibleMonthChanged
        ddMonth.SelectedValue = Calendar.VisibleDate.Month.ToString
        ddYear.SelectedValue = Calendar.VisibleDate.Year.ToString
    End Sub

    Private Sub ddMonth_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddMonth.SelectedIndexChanged, ddYear.SelectedIndexChanged
        Dim calYear As Integer = Convert.ToInt64(ddYear.SelectedValue)
        Calendar.VisibleDate = New Date(calYear, Convert.ToInt16(ddMonth.SelectedValue), 1)
        Calendar.DataBind()
    End Sub
End Class
