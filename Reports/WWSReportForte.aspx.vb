Option Explicit On 
Option Strict On

Imports System
Imports System.IO
Imports System.Data

Imports Wyeth.Utilities
Imports Wyeth.Utilities.Helper
Imports Wyeth.Utilities.NumberFormat
Imports Wyeth.Utilities.DateHandling
Imports Wyeth.Alf.WyethDropdown

Imports Wyeth.Alf.CssStyles

Public Class WWSReportForte
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtStartDate As System.Web.UI.WebControls.TextBox
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents ddMonth As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents ReportPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents lblTransactionMonth As System.Web.UI.WebControls.Label
    Protected WithEvents btn_wwsdownload As System.Web.UI.WebControls.Button
    Protected WithEvents genReport As System.Web.UI.WebControls.Button
    Protected WithEvents repData As Wyeth.Alf.reportData
    Protected WithEvents prtControl As printReportCtl

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region


    Dim MyReport As New Report
    Dim TransActionMonth As Date
    Dim sumValue, sumTCogs As Double
    Dim sumUnits As Integer

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ResetSum()

        If Not Page.IsPostBack Then
            Me.lblPageTitle.Text = Request.QueryString("PageTitle")
            GetMonthSelectToProcessMonth(ddMonth, CDate(Session("LastMonthApproved")))
            ddMonth.SelectedValue = Convert.ToDateTime(Session("LastMonthApproved")).ToString("MMM-yyyy", GetMyDTFI())
            repData.addLine("Report Month", ddMonth.SelectedItem.Text, True, False)
            bindData()
            SetGridStyles(MyGrid)
        End If
        repData.lastOrderDate = LastOfThisMonth(CDate(ddMonth.SelectedValue)).ToString(DATEFORMAT_STRING_REPORT, GetMyDTFI())
    End Sub
    Private Sub bindData()
        MyReport.CtryID = CInt(Session("country_id"))
        MyReport.StartDate = Convert.ToDateTime((ddMonth.SelectedValue), GetMyDTFI())
        MyGrid.DataSource = MyReport.GetWWSReportForte
        MyGrid.DataBind()
    End Sub

    Private Sub btn_wwsdownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_wwsdownload.Click
        bindData()
        Dim exp As exportToExcel = New exportToExcel(Page)
        exp.title = lblPageTitle.Text
        exp.showReportData = repData
        exp.formatColumnAsString(3)
        exp.addDataGrid(MyGrid)
        exp.export()
    End Sub


    Private Sub Item_created(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated
        setColumnToolTips(e.Item, MyGrid)

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Footer Then
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("value"))).Text = MyNumberFormat(sumValue, 2)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("total_cogs"))).Text = MyNumberFormat(sumTCogs, 2)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("units"))).Text = MyNumberFormat(sumUnits, 0)
            e.Item.Cells(2).Text = "TOTAL AUSTRIA"
            e.Item.CssClass = "reportTotal"
        End If
    End Sub

    Private Sub Item_databound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound
        sumValue = sumValue + MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("value"))).Text, 2)
        sumTCogs = sumTCogs + MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("total_cogs"))).Text, 2)
        sumUnits = sumUnits + CInt(MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("units"))).Text, 0))


    End Sub

    Private Sub genReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles genReport.Click
        bindData()
    End Sub

    Private Sub ResetSum()
        sumValue = 0 : sumTCogs = 0 : sumUnits = 0
    End Sub

    '**************************************************************************************************************
    '* For Printing
    '**************************************************************************************************************
    Sub btnPrint_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Print_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Autoprint Or PopupWindowStyle.NoToolbar)
        ResetSum()
        bindData()
    End Sub

    Sub btnPreview_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Preview_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Fullscreen)
        ResetSum()
        bindData()
    End Sub

    Private Function PrePrint() As printReportUtil
        bindData()

        Dim preview As New printReportUtil

        preview.PageTitle = "WWS Report"
        preview.PageSize = 43
        preview.PageSizeLandscape = 29

        preview.AddReportHeader(repData)
        preview.AddWebGrid(Me.MyGrid)

        Return preview

    End Function
    '**************************************************************************************************************
    '* End Printing
    '**************************************************************************************************************
End Class
