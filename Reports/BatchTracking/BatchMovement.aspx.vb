Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing

Imports System.Web
Imports System.Web.SessionState
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls

Imports C1.Web.C1WebGrid
Imports Microsoft.Win32

Imports Wyeth.Utilities.DateHandling
Imports Wyeth.Alf.CssStyles
Imports Wyeth.Alf.WyethDropdown
Imports Wyeth.Utilities.Helper
Imports Wyeth.Utilities.NumberFormat

Public Class BatchMovement
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents txtStartDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents txtEndDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents ddLine As System.Web.UI.WebControls.DropDownList
    Protected WithEvents groupBy As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnGenRep As System.Web.UI.WebControls.Button
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents btnLevel1 As System.Web.UI.WebControls.Button
    Protected WithEvents btnLevel2 As System.Web.UI.WebControls.Button
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents ReportPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents repData As reportData
    Protected WithEvents btn_lvl3 As System.Web.UI.WebControls.Button
    Protected WithEvents prtControl As printReportCtl
    Protected WithEvents txtBatchNr As Wyeth.Utilities.WyethTextBox
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents StartImage As System.Web.UI.WebControls.Image
    Protected WithEvents EndImage As System.Web.UI.WebControls.Image

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim myReport As New Report
    Dim sumUnits, sumFGUnits As Integer
    Dim sumValue, sumFGVAlue As Double
    Dim myPopup = New JSPopUp(Me)
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ResetSum()

        If Not Page.IsPostBack Then

            GetLineSelectDD(ddLine, Session("country_id"))

            txtStartDate.Text = FirstOfThisMonth(repData.lastOrderDate).ToString(DATEFORMAT_STRING)
            txtEndDate.Text = LastOfThisMonth(repData.lastOrderDate).ToString(DATEFORMAT_STRING)
            myPopup.PageURL = "../../util/datepicker.aspx"
            myPopup.AddDatePopupToControl(txtStartDate, StartImage)
            myPopup.AddDatePopupToControl(txtEndDate, EndImage)
            bindData()
        End If
    End Sub

    Private Sub fillReportData()
        repData.lastOrderDate = txtEndDate.Text
        repData.addLine("Report-date from", txtStartDate.Text, True, False)
        repData.addLine("Report-date to", txtEndDate.Text, True, False)
        repData.addLine("Selected Line", ddLine.SelectedItem.Text.ToString, True, False)
    End Sub

    Private Sub bindData()

        myReport.StartDate = txtStartDate.Text
        myReport.EndDate = txtEndDate.Text
        myReport.LineID = ddLine.SelectedValue
        myReport.BachtNr = Me.txtBatchNr.Text.Trim()
        fillReportData()


        With MyGrid
            .AutoGenerateColumns = False
            .DataSource = myReport.GetBatchMovement()
            .Columns(0).Visible = True
            .Columns(0).GroupInfo.Position = GroupPositionEnum.Header
            .Columns(0).GroupInfo.HeaderText = "Batch No.: {0}"
            .Columns(0).SortDirection = C1SortDirection.Ascending
            .AlternatingItemStyle.CssClass = "tableBGColor2Class"
            .ItemStyle.CssClass = "tableBGColor2Class"
            .DataBind()
        End With
        SetGridStylesGroup(MyGrid)
    End Sub


    Private Sub Item_created(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated

        If e.Item.ItemType = C1ListItemType.GroupFooter Or e.Item.ItemType = C1ListItemType.GroupHeader Then
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("UNITS"))), 0)
        End If

        If e.Item.ItemType = C1ListItemType.Footer Then
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Units"))).Text = MyNumberFormat(sumUnits, 0)
            e.Item.CssClass = "reportTotal"
            e.Item.Cells(0).Text = "TOTAL AUSTRIA"
            e.Item.Cells(0).HorizontalAlign = HorizontalAlign.Left
        End If

    End Sub

    Private Sub btnGenRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRep.Click

        Page.Validate()

        If Page.IsValid Then
            MyGrid.Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed
            bindData()
        Else
            MyGrid.Visible = False
        End If

    End Sub

    Private Sub ItemCustomAgg2(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1GroupTextEventArgs) Handles MyGrid.GroupAggregate
        Dim i As Integer = MyGrid.Columns.IndexOf(e.Col)
        If i < 4 Then
            e.Text = MyGrid.Items(e.StartItemIndex).Cells(i).Text
        End If

    End Sub
    Private Sub Item_DataBound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound

        If e.Item.ItemType = C1ListItemType.AlternatingItem Or e.Item.ItemType = C1ListItemType.Item Then
            sumUnits = sumUnits + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Units"))).Text
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("UNITS"))), 0)
        End If

    End Sub
    Private Sub ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportExcel.Click
        bindData()
        Dim exp As exportToExcel = New exportToExcel(Page)
        exp.title = Me.ALFPageTitle
        exp.showReportData = repData
        exp.addDataGrid(MyGrid)
        exp.export()
    End Sub

    Private Sub btnLevel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLevel1.Click
        bindData()
        MyGrid.Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed
    End Sub

    Private Sub btnLevel2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLevel2.Click
        bindData()
        MyGrid.Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
    End Sub


    Private Sub ResetSum()
        sumUnits = 0
    End Sub

    '**************************************************************************************************************
    '* For Printing
    '**************************************************************************************************************
    Private Sub btnPrint_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Print_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Autoprint Or PopupWindowStyle.NoToolbar)
        ResetSum()
        bindData()
    End Sub

    Private Sub btnPreview_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Preview_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Fullscreen)
        ResetSum()
        bindData()
    End Sub

    Private Function PrePrint() As printReportUtil
        bindData()

        Dim preview As New printReportUtil

        preview.PageTitle = "Sales Statistics (new)"
        preview.PageSizeLandscape = 31
        preview.DefaultOrientation = Orientation.Landscape

        preview.AddReportHeader(Me.repData)
        preview.AddWebGrid(Me.MyGrid)

        Return preview

    End Function
    '**************************************************************************************************************
    '* End Printing
    '**************************************************************************************************************

End Class