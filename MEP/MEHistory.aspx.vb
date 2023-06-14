Imports Wyeth.Alf.WyethDropdown
Imports Wyeth.Alf.CssStyles
Imports Wyeth.Utilities.Helper
Imports Wyeth.Utilities.NumberFormat
Imports Wyeth.Utilities.DateHandling


Public Class MEP
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddMonthEnd As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btn_Invoices As System.Web.UI.WebControls.Button
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents MyGridLogs As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents lblCompletedAction As System.Web.UI.WebControls.Label
    Protected WithEvents repData As Wyeth.Alf.reportData
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


    Dim MyMep As New MEPData
    Dim sumInvoiceValue, sumAccruedValue, sumDiffToGIT, sumDiffToAccrued As Double
    Dim sumUnits As Integer

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        MyMep.setProcessMonth()

        If Page.IsPostBack = True Then

        Else
            lblPageTitle.Text = Request.QueryString("pagetitle")
            SetGridStylesGroup(MyGrid)
            SetGridStylesGroup(MyGridLogs)
            GetMonthSelectToProcessMonth(ddMonthEnd, CDate(Session("LastProcessMonth")))
            ddMonthEnd.SelectedValue = Convert.ToDateTime(Session("LastProcessMonth")).ToString("MMM-yyyy", GetMyDTFI())
            BindData()


        End If
        repData.lastOrderDate = LastOfThisMonth(CDate(ddMonthEnd.SelectedValue)).ToString(DATEFORMAT_STRING_REPORT, GetMyDTFI())
    End Sub
    Private Sub BindData()
        MyGrid.DataSource = MyMep.GetProcessedData(LastOfThisMonth(ddMonthEnd.SelectedValue.ToString(CType(Application("MyDTFI"), IFormatProvider))), Session("country_id"))
        MyGridLogs.DataSource = MyMep.GetMonthEndHistoryLogs(ddMonthEnd.SelectedItem.ToString.Substring(0, 7), Session("country_id"))
        MyGridLogs.Visible = False
        MyGrid.DataBind()
        MyGridLogs.ShowFooter = False
        MyGridLogs.DataBind()
    End Sub
    Private Sub btn_Invoices_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Invoices.Click
        BindData()
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
        Dim reportHeaderString As String = ddMonthEnd.SelectedValue.ToString(CType(Application("MyDTFI"), IFormatProvider)).Substring(0, 7)
        Dim exp As exportToExcel = New exportToExcel(Page)
        exp.title = lblPageTitle.Text
        exp.addLine(String.Format(reportHeaderString))
        exp.addDataGrid(MyGridLogs)
        exp.addDataGrid(MyGrid)
        exp.export()
    End Sub



    '**************************************************************************************************************
    '* For Printing
    '**************************************************************************************************************
    Private Sub btnPrint_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Print_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Autoprint Or PopupWindowStyle.NoToolbar)

        BindData()
    End Sub

    Private Sub btnPreview_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Preview_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Fullscreen)

        BindData()
    End Sub

    Private Function PrePrint() As printReportUtil
        BindData()

        Dim preview As New printReportUtil


        preview.PageTitle = lblPageTitle.Text
        preview.PageSizeLandscape = 31
        preview.DefaultOrientation = Orientation.Landscape
        preview.AddWebGrid(Me.MyGrid)

        Return preview

    End Function
End Class
