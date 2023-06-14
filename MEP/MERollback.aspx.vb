Imports Wyeth.Alf.WyethDropdown
Imports Wyeth.Utilities.Helper
Imports Wyeth.Utilities.NumberFormat
Imports Wyeth.Alf.CssStyles

Public Class MER
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents ddMonthEnd As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btn_Invoices As System.Web.UI.WebControls.Button
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents lblProcessMonth As System.Web.UI.WebControls.Label
    Protected WithEvents lblstartProcessing As System.Web.UI.WebControls.Label
    Protected WithEvents btn_process_invoices As System.Web.UI.WebControls.Button
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents tblRollback As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents prtControl As Wyeth.Alf.printReportCtl

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim MyMEP As New MEPData
    Dim MyStock As New Stock
    Dim sumInvoiceValue, sumAccruedValue, sumDiffToGIT, sumDiffToAccrued As Double
    Dim sumUnits As Integer

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        MyMEP.setProcessMonth()

        If Page.IsPostBack Then
        Else
            If MyMEP.CheckForFinanceApproval(Convert.ToDateTime(Session("LastProcessMonth")), Session("country_id")) = True Then

                Me.lblProcessMonth.Text = "ALF will Rollback the following Items for:<Font color=red> " & Convert.ToDateTime(Session("LastProcessMonth")).ToString(DATEFORMAT_STRING_REPORT).Substring(0, 7) & "</Font>"
                Me.lblstartProcessing.Text = "Start Rollback for:<Font color=red> " & Convert.ToDateTime(Session("LastProcessMonth")).ToString(DATEFORMAT_STRING_REPORT).Substring(0, 7) & "</Font>"
            Else   ' Month has already been approved
                Me.lblProcessMonth.Text = "A Rollback for the Month:<Font color=red> " & Convert.ToDateTime(Session("LastProcessMonth")).ToString(DATEFORMAT_STRING_REPORT).Substring(0, 7) & "</font> is not possible, because it has already been locked by Finance"
                Me.lblstartProcessing.Text = "Rollback for:<Font color=red> " & Convert.ToDateTime(Session("LastProcessMonth")).ToString(DATEFORMAT_STRING_REPORT).Substring(0, 7) & "</font> is not Possible, because it has already been locked by Finance"
                Me.tblRollback.Visible = False
            End If
            Me.lblPageTitle.Text = Request.QueryString("pagetitle")
            SetGridStylesGroup(MyGrid)
            BindData()
        End If
    End Sub
    Private Sub BindData()
        MyGrid.DataSource = MyMEP.GetProcessedData(Convert.ToDateTime(Session("LastProcessMonth")), Session("country_id"))
        MyGrid.DataBind()
    End Sub
    Private Sub MyGrid_Databound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound
        sumInvoiceValue = sumInvoiceValue + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_invoice_value"))).Text
        sumAccruedValue = sumAccruedValue + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_accrued_value"))).Text
        sumDiffToGIT = sumDiffToGIT + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_diff_value"))).Text
        sumDiffToAccrued = sumDiffToAccrued + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_diff_value_accrued"))).Text
        sumUnits = sumUnits + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_unit"))).Text

        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_unit"))), 0)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_invoice_value"))), 2)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_accrued_value"))), 2)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_diff_value"))), 2)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_diff_value_accrued"))), 2)
    End Sub
    Private Sub MyGrid_ItemCreated(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.GroupHeader Or e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.GroupFooter Then
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_unit")) - 8), 0)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_invoice_value")) - 8), 2)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_accrued_value")) - 8), 2)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_diff_value")) - 8), 2)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_diff_value_accrued")) - 8), 2)
        End If

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Footer Then
            e.Item.HorizontalAlign = HorizontalAlign.Right
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_invoice_value"))).Text = MyNumberFormat(sumInvoiceValue, 2)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_accrued_value"))).Text = MyNumberFormat(sumAccruedValue, 2)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_diff_value"))).Text = MyNumberFormat(sumDiffToGIT, 2)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_diff_value_accrued"))).Text = MyNumberFormat(sumDiffToAccrued, 2)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_unit"))).Text = MyNumberFormat(sumUnits, 0)
        End If

    End Sub
    Private Sub ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportExcel.Click
        BindData()

        Dim reportHeaderString As String = lblProcessMonth.Text

        Dim exp As exportToExcel = New exportToExcel(Page)
        exp.title = lblPageTitle.Text
        exp.addLine(String.Format(reportHeaderString))
        exp.addDataGrid(MyGrid)
        exp.export()
    End Sub




    '**************************************************************************************************************
    '* For Printing
    '**************************************************************************************************************
    Private Sub btnPrint_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Print_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Autoprint Or PopupWindowStyle.NoToolbar)

        bindData()
    End Sub

    Private Sub btnPreview_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Preview_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Fullscreen)

        bindData()
    End Sub

    Private Function PrePrint() As printReportUtil
        bindData()

        Dim preview As New printReportUtil

        preview.PageTitle = lblPageTitle.Text
        preview.PageSizeLandscape = 31
        preview.DefaultOrientation = Orientation.Landscape
        preview.AddWebGrid(Me.MyGrid)

        Return preview

    End Function
End Class
