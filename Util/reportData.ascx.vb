Imports System
Imports System.Globalization
Imports Wyeth.Utilities
Imports Wyeth.Utilities.Helper
Imports Wyeth.Utilities.DateHandling

Public Class reportData
    Inherits System.Web.UI.UserControl

    '****************************************************
    ' KLASSEN-VARS
    '****************************************************
    Private p_lastOrderDate As Date
    Private usersCountryID As Integer
    Private MyNFI As New NumberFormatInfo
    Private forReportHtmlCode As String
    Private forPrintHtmlCode As String
    Public dateForLastOrder As Date
    Public printDateCaption As String   'soll die "print-date" - bezeichnung anders lauten?

    '****************************************************
    ' PROPERTIES 
    '****************************************************
    Public Property lastOrderDate() As String
        Get
            Return p_lastOrderDate.ToString(DATEFORMAT_STRING_REPORT)
        End Get
        Set(ByVal Value As String)
            Dim myReport As WyethAppllication = New WyethAppllication
            p_lastOrderDate = myReport.getLastOrderEntry(CDate(Value))
        End Set
    End Property

    Public ReadOnly Property CountryID() As Integer
        Get
            Dim page As System.Web.UI.Page = New page
            Return page.Session.Item("country_id")
        End Get
    End Property

    Public ReadOnly Property dateToday() As String
        Get
            Return Date.Today.ToString(DATEFORMAT_STRING_REPORT, CType(Application("MyDTFI"), IFormatProvider))
        End Get
    End Property

    Public ReadOnly Property workDaysOfMonth() As Integer
        Get
            Return GetWorkDaysForMonthToday(Me.lastOrderDate, usersCountryID)
        End Get
    End Property

    Public ReadOnly Property workDaysOfMonthTotal() As Integer
        Get
            Return GetWorkDaysForMonth(Me.lastOrderDate, usersCountryID)
        End Get
    End Property

    '****************************************************
    ' KONSTRUKTOR 
    '****************************************************
    Sub New()
        MyBase.New()
        Me.usersCountryID = CountryID
        Me.lastOrderDate = Date.Today.ToString
        Me.printDateCaption = ""

    End Sub

#Region " Web Form Designer Generated Code "

    Protected WithEvents forReport As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents rep As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents lblWorkdays As System.Web.UI.WebControls.Label
    Protected WithEvents lblReportLastEntry As System.Web.UI.WebControls.Label
    Protected WithEvents lblPrintDate As System.Web.UI.WebControls.Label
    Protected WithEvents forPrint As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents lblPrintDateCaption As System.Web.UI.HtmlControls.HtmlGenericControl

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    '****************************************************
    ' BEIM SEITE LADEN 
    '****************************************************
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
        fillLastOrderEntry()
        fillWorkdays()
        fillPrintDate()
        fillPreliminarySalesData()
    End Sub

    '****************************************************
    ' WORKDAYS BEFÜLLEN. LOS! 
    '****************************************************
    Private Sub fillWorkdays()
        Dim workDays As Integer = Me.workDaysOfMonth
        Dim allWorkDays As Integer = Me.workDaysOfMonthTotal
        lblWorkdays.Text = String.Format("{0} of {1}", workDays, allWorkDays)
    End Sub

    '****************************************************
    ' LASTORDERDATE AUSFÜLLEN. 
    '****************************************************
    Private Sub fillLastOrderEntry()
        lblReportLastEntry.Text = Me.lastOrderDate
    End Sub

    '****************************************************
    ' PRINTDATE UND ZEIT REINSCHREIBEN. 
    '****************************************************
    Private Sub fillPrintDate()
        Dim hours As Integer = Date.Now.TimeOfDay.Hours
        Dim minutes As Integer = Date.Now.TimeOfDay.Minutes
        Dim timeNow As String = hours.ToString("00") & ":" & minutes.ToString("00")
        lblPrintDate.Text = Me.dateToday & " " & timeNow
        If Not Me.printDateCaption = "" Then
            lblPrintDateCaption.InnerText = Me.printDateCaption
        End If
    End Sub
    Private Sub fillPreliminarySalesData()
        If CInt(Application("Status")) = 1 Then
            Me.addLine("<Font color=red><strong>Sanova Sales data missing!</strong></font>", True, True)
        ElseIf CInt(Application("Status")) = 2 Then
            '   Me.addLine("<Font color=red><strong>Münster Sales data missing!</strong></font>", True, True)
        ElseIf CInt(Application("Status")) = 3 Then
            Me.addLine("<Font color=red><strong>Sanova Sales data missing!</strong></font>", True, True)
            '  Me.addLine("<Font color=red><strong>Münster Sales data missing!</strong></font>", True, True)
        End If

    End Sub

    '**************************************************************************
    ' ZEICHNET ALLE ZUSÄTZLICHEN LINIEN WELCHE MIT ADDLINE GEADDED WURDEN. 
    '**************************************************************************
    Private Sub fillAdditionalLine(ByVal value As String, ByVal print As Boolean)
        If print Then
            Me.forPrintHtmlCode &= value
            forPrint.InnerHtml = Me.forPrintHtmlCode
        Else
            Me.forReportHtmlCode &= value
            forReport.InnerHtml = Me.forReportHtmlCode
        End If
    End Sub

    '****************************************************
    ' FÜGT EINE NEUE LINIE ZUR REPORTDATA HINZU 
    '****************************************************
    Public Overloads Function addLine(ByVal caption As String, ByVal value As String, ByVal showPrint As Boolean, ByVal showReport As Boolean)
        Dim htmlCode As String
        htmlCode = "<SPAN class=reportSummaryField>" & caption & ": </SPAN>" & value & "<BR>"

        If Not showReport And showPrint Then
            Me.fillAdditionalLine(htmlCode, True)
        ElseIf showReport And Not showPrint Then
            htmlCode = "<span class=noprint>" & htmlCode & "</span>"
            Me.fillAdditionalLine(htmlCode, False)
        ElseIf showReport Then
            Me.fillAdditionalLine(htmlCode, False)
        End If
    End Function

    Public Overloads Function addLine(ByVal caption As String, ByVal showPrint As Boolean, ByVal showReport As Boolean)
        Dim htmlCode As String
        htmlCode = "<SPAN class=reportSummaryField>" & caption & "</SPAN><BR>"

        If Not showReport And showPrint Then
            Me.fillAdditionalLine(htmlCode, True)
        ElseIf showReport And Not showPrint Then
            htmlCode = "<span class=noprint>" & htmlCode & "</span>"
            Me.fillAdditionalLine(htmlCode, False)
        ElseIf showReport Then
            Me.fillAdditionalLine(htmlCode, False)
        End If
    End Function

End Class