Imports Wyeth.Alf.WyethDropdown
Imports Wyeth.Alf.CssStyles
Imports Wyeth.Utilities.NumberFormat
Imports Wyeth.Utilities.DateHandling
Imports Wyeth.Utilities.Helper



Public Class MEC
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents btn_process_invoices As System.Web.UI.HtmlControls.HtmlButton
    Protected WithEvents lblProcessMonth As System.Web.UI.WebControls.Label
    Protected WithEvents lblstartProcessing As System.Web.UI.WebControls.Label
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents lblItems As System.Web.UI.WebControls.Label
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
    Dim sumInvoiceValue, sumAccruedValue, sumDiffToGIT, sumDiffToAccrued As Double
    Dim sumUnits As Integer
    Dim unit As New FontUnit



    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        MyMEP.setProcessMonth()
        If Page.IsPostBack Then

        Else

            lblPageTitle.Text = Request.QueryString("pagetitle")
            lblProcessMonth.Text = "Processing Month:<Font color=red> " & Convert.ToString(Session("CurrentProcessMonth")).Substring(0, 7) & "</Font>"
            SetGridStylesGroup(MyGrid)

            If MyMEP.CheckForFinanceApproval(CDate(Session("CurrentProcessMonth")).AddMonths(-1), Session("country_id")) = False Then
                lblstartProcessing.Text = "Start Processing for:<Font color=red> " & Convert.ToString(Session("CurrentProcessMonth")).Substring(0, 7) & "</Font>"
            Else

                lblstartProcessing.Text = "Start Processing for<Font color=red> " & Convert.ToString(Session("CurrentProcessMonth")).Substring(0, 7) & "</Font> is not possible, because <BR>"
                lblstartProcessing.Text += "<font color=red>" & Convert.ToDateTime(Session("CurrentProcessMonth")).AddMonths(-1).ToString(DATEFORMAT_STRING_REPORT).Substring(0, 7).ToString & "</font> was not approved by Finance!"

                btn_process_invoices.Visible = False

            End If


            BindData()
        End If
    End Sub
    Private Sub BindData()
        MyMEP.ProcessInvoices(Session("CurrentProcessMonth"), Session("country_id"))
        MyGrid.DataSource = MyMEP.GetProcessedData(Session("CurrentProcessMonth"), Session("country_id"))
        MyMEP.RollBackInvoices(Session("CurrentProcessMonth"), Session("country_id"))
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
