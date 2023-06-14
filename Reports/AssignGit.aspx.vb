Imports Wyeth.Alf.CssStyles
Imports Wyeth.Utilities.NumberFormat


Public Class AssignGit
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents MyMessage As System.Web.UI.WebControls.Panel
    Protected WithEvents GridPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents repData As Wyeth.Alf.reportData
    Protected WithEvents lblNotFound As System.Web.UI.WebControls.Label
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents txtstock_id_we As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim MyStock As New Stock
    Dim prod_id, dist_id, stock_we_id As Integer
    Dim MyDataView As DataView
    Dim Presentation, phznr As String



    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        If Page.IsPostBack = True Then

        Else

            lblPageTitle.Text = "GIT Assignment"
            prod_id = Request.QueryString("prod_id")
            dist_id = Request.QueryString("dist_id")
            txtstock_id_we.Text = Request.QueryString("stock_we_id")
            Presentation = Request.QueryString("prod_presentation")
            phznr = Request.QueryString("prod_phznr")

            BindData()
            repData.addLine("PhzNr", phznr, True, True)
            repData.addLine("Presentation", Presentation, True, True)
        End If
    End Sub

    Private Sub BindData()

        MyStock.StockProdID = prod_id
        MyStock.StockDistID = dist_id
        MyDataView = MyStock.GetInvoiceGIT()
        MyGrid.DataSource = MyDataView
        SetGridStyles(MyGrid)
        MyGrid.DataBind()

        If MyDataView.Count = 0 Then
            MyGrid.Visible = False
            lblNotFound.Visible = True
            lblNotFound.Text = " No GIT Entries found for " & Presentation

        End If
    End Sub
    Private Sub MyGrid_ItemCreated(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Or e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then
            CType(e.Item.Cells(9).Controls(0), WebControl).CssClass = "button"
        End If

    End Sub

    Private Sub MyGrid_ItemDatabound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound
        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Or e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then
            Dim invoice_value As String
            MyNumberFormat(e.Item.Cells(6), 2)
            MyNumberFormat(e.Item.Cells(5), 0)
            invoice_value = e.Item.Cells(6).Text
            invoice_value = Replace(invoice_value, ".", "")
            invoice_value = Replace(invoice_value, ",", ".")

            CType(e.Item.Cells(9).Controls(0), WebControl).Attributes.Add("onclick", "javascript:TakeGITValues('" & e.Item.Cells(3).Text & "','" & e.Item.Cells(4).Text & "','" & e.Item.Cells(8).Text & "','" & e.Item.Cells(10).Text & "','" & invoice_value & "');")
           
        End If

    End Sub


    Sub MyGrid_ItemCommand(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1CommandEventArgs) Handles MyGrid.ItemCommand
        Dim stock_id, dist_id As Integer
        Dim strPage As String

        dist_id = e.Item.Cells(1).Text
        stock_id = e.Item.Cells(0).Text
        MyStock.StockDistID = dist_id
        MyStock.StockID = stock_id
        MyStock.StockWEID = txtstock_id_we.Text
        MyStock.StockCtryID = Session("country_id")

        MyStock.SetGITStockDateCorrect()
      

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub Page_Unload(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Disposed


    End Sub
End Class
