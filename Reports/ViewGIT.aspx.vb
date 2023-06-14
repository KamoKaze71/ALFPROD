Imports Oracle.DataAccess.Client

Public Class ViewGIT
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtStockDateStock As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPhznr As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPresentation As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtOrderNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtWyethInvoiceNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtUnits As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtInvoiceValue As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtInvoiceValueAccrued As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtGitDifference As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtComment As System.Web.UI.WebControls.TextBox
    Protected WithEvents WEPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents txtCommentGIT As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtInvoiceValueGIT As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtUnitsGIT As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtWyethInvoiceNoGIT As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtOrderNoGIT As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPresentationGIt As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPhznrGIT As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtStockDateStockGIT As System.Web.UI.WebControls.TextBox
    Protected WithEvents GITPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents txtStockDateAccrued As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtStockDateCorrect As System.Web.UI.WebControls.TextBox
    Protected WithEvents currInvoice As System.Web.UI.WebControls.TextBox
    Protected WithEvents currAccrued As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDifferenceAccrued As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtStockDateCorrectGIT As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCurrGIT As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents txtDiffGIT As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim stock_id, stock_id_git As Integer
    Dim MyStock As New Stock


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Page.IsPostBack Then

        Else
            stock_id = Request.QueryString("stock_id")
            stock_id_git = Request.QueryString("stock_id_git")
            lblPageTitle.Text = "WE - GIT Assignment"
            BindData()
        End If
    End Sub

    Private Sub BindData()
        Dim MyReader As OracleDataReader
        Try

            MyStock.StockID = stock_id
            MyStock.StockWEID = stock_id_git
            MyReader = MyStock.GetGITViewData()

            Do While (MyReader.Read)

                If MyReader("CODE_CODE") = "WE" Then

                    txtStockDateStock.Text = MyReader("STOC_DATE_STOCK")
                    txtPresentation.Text = MyReader("PROD_PRESENTATION")
                    txtPhznr.Text = MyReader("PROD_PHZNR")
                    txtWyethInvoiceNo.Text = MyReader("STOC_INVOICE_NUMBER")
                    txtOrderNo.Text = MyReader("stoc_order_number")
                    txtStockDateAccrued.Text = (MyReader("STOC_DATE_ACCRUED"))
                    txtStockDateCorrect.Text = MyReader("STOC_DATE_CORRECT")
                    txtInvoiceValue.Text = MyReader("STOC_INVOICE_VALUE")
                    txtDifferenceAccrued.Text = MyReader("STOC_DIFF_VALUE_ACCRUED")
                    currAccrued.Text = MyReader("CURRENCY_ACCRUED")
                    currInvoice.Text = MyReader("CURRENCY_INVOICE")
                    txtComment.Text = MyReader("STOC_COMMENT")
                    txtUnits.Text = MyReader("STOC_UNIT")
                    txtDiffGIT.Text = MyReader("STOC_DIFF_VALUE")


                ElseIf MyReader("CODE_CODE") = "GIT" Then

                    txtStockDateStockGIT.Text = MyReader("STOC_DATE_STOCK")
                    txtPresentationGIt.Text = MyReader("PROD_PRESENTATION")
                    txtPhznrGIT.Text = MyReader("PROD_PHZNR")
                    txtStockDateCorrectGIT.Text = MyReader("STOC_DATE_CORRECT")
                    txtInvoiceValueGIT.Text = MyReader("STOC_INVOICE_VALUE")
                    txtOrderNoGIT.Text = MyReader("stoc_order_number")
                    txtWyethInvoiceNoGIT.Text = MyReader("STOC_INVOICE_NUMBER")

                    txtUnitsGIT.Text = MyReader("STOC_UNIT")
                    txtCommentGIT.Text = MyReader("STOC_COMMENT")
                    txtCurrGIT.Text = MyReader("CURRENCY_INVOICE")


                End If

            Loop

        Catch ex As Exception
        Finally
            MyReader.Close()
        End Try
    End Sub

End Class
