Imports Wyeth.Utilities.Helper
Imports Wyeth.Utilities.DateHandling
Imports Wyeth.Alf.CssStyles


Public Class OUT_FG
	Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
	Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
	Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents Gridpanel As System.Web.UI.WebControls.Panel
    Protected WithEvents repData As Wyeth.Alf.reportData
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button

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
    Dim ed, sd, phznr, presentation As String
	Dim prod_id, dist_id As Integer
    Dim sum5, sum6, sum7 As Double


	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


		If Page.IsPostBack = True Then

            ed = Me.ViewState.Item("ed")
            sd = Me.ViewState.Item("sd")
            prod_id = Me.ViewState.Item("prod_id")
            dist_id = Me.ViewState.Item("dist_id")
		Else
			prod_id = Request.QueryString("id")
			ed = Request.QueryString("ed")
			sd = Request.QueryString("sd")
            dist_id = Request.QueryString("dist_id")
            phznr = Request.QueryString("phznr")
            presentation = Request.QueryString("presentation")
            lblPageTitle.Text = Request.QueryString("pagetitle")

            Me.ViewState.Add("sd", sd.ToString)
            Me.ViewState.Add("ed", ed.ToString)
            Me.ViewState.Add("prod_id", prod_id)
            Me.ViewState.Add("dist_id", dist_id)

            BindData()
		End If
	End Sub
	Private Sub BindData()

        repData.addLine("Start Date", FormatDate(sd), True, True)
        repData.addLine("End Date", FormatDate(ed), True, True)
        repData.addLine("Presentation", presentation, True, True)
        repData.addLine("Product No.", phznr, True, True)

        MyStock.StockStartDate = sd
		MyStock.StockEndDate = ed
        MyStock.StockProdID = prod_id
        MyStock.StockDistID = dist_id

		MyGrid.DataSource = MyStock.GetStockOUT_FG()
		MyGrid.DataBind()
        SetGridStyles(MyGrid)

	End Sub



	Private Sub MyGrid_Databound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound

		Dim d As Double

        If e.Item.Cells(5).Text <> "&nbsp;" Then
            d = CDbl(e.Item.Cells(5).Text)
            sum5 = sum5 + d
            e.Item.Cells(5).Text = d.ToString(NUMBER_FORMAT_STRING)
        End If

        If e.Item.Cells(6).Text <> "&nbsp;" Then
            d = CDbl(e.Item.Cells(6).Text)
            e.Item.Cells(6).Text = d.ToString(NUMBER_FORMAT_STRING_EXACT)
        End If


        If e.Item.Cells(7).Text <> "&nbsp;" Then
            d = CDbl(e.Item.Cells(7).Text)
            sum7 = sum7 + d
            e.Item.Cells(7).Text = d.ToString(NUMBER_FORMAT_STRING_EXACT)
        End If

	End Sub


	Private Sub Item_created(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated

        Dim d As Double

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Footer Then

            e.Item.Cells(0).Text = "TOTAL"
            e.Item.Cells(5).Text = sum5.ToString(NUMBER_FORMAT_STRING)
            'e.Item.Cells(6).Text = sum6.ToString(NUMBER_FORMAT_STRING_EXACT)
            e.Item.Cells(7).Text = sum7.ToString(NUMBER_FORMAT_STRING_EXACT)

            For Each cell As TableCell In e.Item.Cells()
                cell.Font.Bold = True
            Next
        End If
	End Sub

    Private Sub ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportExcel.Click
        BindData()

        Dim exp As exportToExcel = New exportToExcel(Page)
        exp.title = lblPageTitle.Text
        exp.showReportData = repData
        exp.formatColumnAsString(1)
        exp.addDataGrid(MYGRid)
        exp.export()
    End Sub


End Class


