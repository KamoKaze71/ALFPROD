Imports Wyeth.Utilities.DateHandling
Imports System.IO

Public Class printtest
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgPrint As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnPrint As System.Web.UI.WebControls.Button
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents repData As reportData
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim MyCountry As New WyethCountry
    Dim MyReport As New Report

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim startDate As String = "2004-02-26"
        MyCountry.CountryID = Session("country_id")

        MyReport.StartDate = Convert.ToDateTime(startDate)
        MyReport.BudgetType = "BU"
        MyReport.WorkDaysMonth = CInt(GetWorkDaysForMonth(Convert.ToDateTime(startDate), CInt(Session("country_id"))).ToString())
        MyReport.WorkDaysToday = CInt(GetWorkDaysForMonthToday(Convert.ToDateTime(startDate), CInt(Session("country_id"))).ToString())

        BindData()

    End Sub

    Sub btnPrint_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'Dim test As New printReport

        'With test
        '    .Orientation = .PageOrientation.Portrait
        '    .PageSize = 30
        '    .ReportHeaderValue = repData
        '    .addDataGrid(Me.dgPrint)
        '    .addDataGrid(Me.MyGrid)
        '    .RenderDataGrid()
        'End With

        'Session.Add("dg_print", test.getRenderedReport)

        'Response.Redirect("util/printReport.aspx")
        'Response.Write("<Script language=javascript>")
        'Response.Write("window.open('util/printReport.aspx')")
        'Response.Write("</Script>")

        'Dim test As New renderPrintReport

        'test.PageSize = 20
        'Session("dg_print") = test.renderReport(Me.MyGrid)
        'Response.Redirect("util/printreport.aspx")
    End Sub

    Private Sub BindData()
        Me.dgPrint.DataSource = MyCountry.GetCountries
        Me.dgPrint.DataBind()

        Me.MyGrid.DataSource = MyReport.GetDailySales
        Me.MyGrid.DataBind()
    End Sub

End Class
