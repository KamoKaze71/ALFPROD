Imports Wyeth.Alf.WyethDropdown
Imports Wyeth.Alf.CssStyles
Imports Wyeth.Utilities.NumberFormat
Imports Wyeth.Utilities.Helper

Public Class FMA
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents lblCompletedAction As System.Web.UI.WebControls.Label
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents lblFAMEApprove As System.Web.UI.WebControls.Label
    Protected WithEvents btn_confirm As System.Web.UI.WebControls.Button
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    'Put user code to initialize the page here
    Dim MyMep As New MEPData
    Dim sumInvoiceValue, sumAccruedValue, sumDiffToGIT, sumDiffToAccrued As Double
    Dim sumUnits As Integer
    Dim MyJs As New JSPopUp(Me)

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Page.IsPostBack = True Then

        Else
            MyJs.Width = 400
            MyJs.Height = 250

            MyJs.PageURL = "FinanceApprovalConfirmation.aspx"
            MyJs.AddPopupToControl(btn_confirm)

            lblPageTitle.Text = Request.QueryString("pagetitle")
            lblCompletedAction.Text = "1.) Logistics has closed the following Month:<Font color=red> " & Convert.ToDateTime(Session("LastProcessMonth")).ToString(DATEFORMAT_STRING_REPORT).Substring(0, 7) & "</font>"
            lblFAMEApprove.Text = "2.) Final Month End Closing (month will be locked):<Font color=red> " & Convert.ToDateTime(Session("LastProcessMonth")).ToString(DATEFORMAT_STRING_REPORT).Substring(0, 7) & "</font>"
            SetGridStylesGroup(MyGrid)
            BindData()

            If Session("LastProcessMonth") = Session("LastMonthApproved") Then
                btn_confirm.Visible = False
                lblCompletedAction.Text = "1.) The Month:<Font color=red> " & Convert.ToDateTime(Session("LastProcessMonth")).ToString(DATEFORMAT_STRING_REPORT).Substring(0, 7) & "</font> is already approved by Finance."
                lblFAMEApprove.Text = "2.) The Month:<Font color=red> " & Convert.ToDateTime(Session("LastProcessMonth")).ToString(DATEFORMAT_STRING_REPORT).Substring(0, 7) & "</font> is already approved by Finance."

                If MyMep.CheckForJDEFinalApproval(Session("LastProcessMonth"), Session("country_id")) = False Then
                    lblFAMEApprove.Text = "2.) The Month:<Font color=red> " & Convert.ToDateTime(Session("LastProcessMonth")).ToString(DATEFORMAT_STRING_REPORT).Substring(0, 7) & "</font> is already approved by Finance.<br>3.) A Rollback is not possible because the final JDE approval is already set!"
                    btn_confirm.Visible = False
                Else
                    btn_confirm.Visible = True
                    btn_confirm.Text = "Rollback Finance approval"
                    MyJs.PageURL = "FinanceApprovalConfirmation.aspx?action=Rollback"
                    MyJs.AddPopupToControl(btn_confirm)
                End If

            End If

        End If 'page.ispostback

    End Sub
    Private Sub BindData()
        MyGrid.DataSource = MyMep.GetProcessedData(Convert.ToDateTime(Session("LastProcessMonth")), Session("country_id"))
        MyGrid.DataBind()
       
    End Sub

    Private Sub MyGrid_Databound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound
        sumInvoiceValue = sumInvoiceValue + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_invoice_value"))).Text
        sumAccruedValue = sumAccruedValue + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_accrued_value"))).Text
        sumDiffToGIT = sumDiffToGIT + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_diff_value"))).Text
        sumDiffToAccrued = sumDiffToAccrued + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_diff_value"))).Text
        sumUnits = sumUnits + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_unit"))).Text

        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_unit"))), 0)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_invoice_value"))), 2)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_accrued_value"))), 2)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_diff_value"))), 2)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_diff_value_accrued"))), 2)
    End Sub

    Private Sub MyGrid_ItemCreated(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated
        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.GroupHeader Or e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.GroupFooter Then
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_unit")) - 7), 0)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_invoice_value")) - 7), 2)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_accrued_value")) - 7), 2)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_diff_value")) - 7), 2)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_diff_value_accrued")) - 7), 2)
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

        Dim reportHeaderString As String = lblCompletedAction.Text

        Dim exp As exportToExcel = New exportToExcel(Page)
        exp.title = lblPageTitle.Text

        exp.addLine(String.Format(reportHeaderString))
        exp.addDataGrid(MyGrid)
        exp.export()
    End Sub
End Class
