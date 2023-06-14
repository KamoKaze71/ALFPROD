Imports Wyeth.Alf.CssStyles
Imports Wyeth.Utilities.Helper
Imports Wyeth.Utilities.NumberFormat
Imports Wyeth.Utilities.DateHandling


Public Class StockCogsProductDetail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MyGridStock As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents GridPanelFA As System.Web.UI.WebControls.Panel
    Protected WithEvents GridPanelStock As System.Web.UI.WebControls.Panel
    Protected WithEvents myGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents repData As Wyeth.Alf.reportData
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim sd, ed, cc_desc, bewegkz_desc, phznr As String
    Dim prod_id, dist_id, code_id, cc_id, line_id, sum_units As Integer
    Dim sum_total_cogs As Double

    Dim MyStock As New Stock
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Page.IsPostBack = True Then
           
        Else
            code_id = Request.QueryString("code_id_bewegkz")
            cc_id = Request.QueryString("cc_id")
            ed = Request.QueryString("ed")
            sd = Request.QueryString("sd")
            dist_id = Request.QueryString("dist_id")
            line_id = Request.QueryString("line_id")
            cc_desc = Request.QueryString("cc_desc")
            phznr = Request.QueryString("phznr")
            bewegkz_desc = Request.QueryString("bewegkz_desc")
            bindData()
        End If

        repData.addLine("Type", bewegkz_desc, True, True)
        repData.addLine("Cost Center", cc_desc, True, True)
        repData.addLine("Start Date", FormatDate(sd), True, True)
        repData.addLine("End Date", FormatDate(ed), True, True)

    End Sub
    Private Sub bindData()

        

        MyStock.StockDistID = dist_id
        MyStock.StockStartDate = sd
        MyStock.StockEndDate = ed
        MyStock.StockProdID = prod_id
        MyStock.StockCodeIDBewegKZ = code_id
        MyStock.StockLineID = line_id
        MyStock.StockCCID = cc_id
        MyStock.StockPhznr = phznr


        If bewegkz_desc.StartsWith("FA") Or bewegkz_desc.StartsWith("FG") Then  'it is a faktura
            GridPanelFA.Visible = True
            GridPanelStock.Visible = False

            SetGridStyles(myGrid)
            myGrid.DataSource = MyStock.GetFAProductDetail()
            myGrid.DataBind()
           
        Else
            GridPanelFA.Visible = False
            GridPanelStock.Visible = True
            SetGridStyles(MyGridStock)
            MyGridStock.DataSource = MyStock.GetStockProductDetail
            MyGridStock.DataBind()
        End If

    End Sub
    Private Sub Item_created(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles myGrid.ItemCreated, MyGridStock.ItemCreated

        Dim d As Double
        Dim MyGridtmp As C1.Web.C1WebGrid.C1WebGrid
        MyGridtmp = sender



        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Footer Then
            e.Item.Cells(0).Text = "Total"
            e.Item.Cells(MyGridtmp.Columns.IndexOf(MyGridtmp.Columns.ColumnByName("units"))).Text = MyNumberFormat(sum_units, 0)
            e.Item.Cells(MyGridtmp.Columns.IndexOf(MyGridtmp.Columns.ColumnByName("totalcogs"))).Text = MyNumberFormat(sum_total_cogs, 2)

            For Each cell As TableCell In e.Item.Cells()
                cell.Font.Bold = True
            Next
        End If
    End Sub
    Private Sub Item_Databound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles myGrid.ItemDataBound, MyGridStock.ItemDataBound

        Dim d As Double
        Dim MyGridtmp As C1.Web.C1WebGrid.C1WebGrid
        MyGridtmp = sender

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Or e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Then
            sum_units = sum_units + e.Item.Cells(MyGridtmp.Columns.IndexOf(MyGridtmp.Columns.ColumnByName("units"))).Text
            sum_total_cogs = sum_total_cogs + e.Item.Cells(MyGridtmp.Columns.IndexOf(MyGridtmp.Columns.ColumnByName("totalcogs"))).Text

        End If
        MyNumberFormat(MyGridtmp.Columns.IndexOf(MyGridtmp.Columns.ColumnByName("totalcogs")), 2)
        MyNumberFormat(MyGridtmp.Columns.IndexOf(MyGridtmp.Columns.ColumnByName("units")), 0)
        MyNumberFormat(MyGridtmp.Columns.IndexOf(MyGridtmp.Columns.ColumnByName("stdcogs")), 2)
    End Sub
End Class
