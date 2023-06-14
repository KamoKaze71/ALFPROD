Imports Wyeth.Alf.CssStyles
Imports Wyeth.Utilities.Helper
Imports Wyeth.Utilities.DateHandling


Public Class _IN
	Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
	Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
	Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
	Protected WithEvents lblPresentation As System.Web.UI.WebControls.Label
    Protected WithEvents lblDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblphznr As System.Web.UI.WebControls.Label
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents reportPanel As System.Web.UI.WebControls.Panel
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
	Dim prod_id, dist_id As Integer
    Dim ed, sd, phznr, presentation As String
	Dim sum1, sum2, sum3 As Double

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		'Put user code to initialize the page here

        If Page.IsPostBack Then
            ed = Me.ViewState.Item("ed")
            sd = Me.ViewState.Item("sd")
            prod_id = Me.ViewState.Item("prod_id")
            dist_id = Me.ViewState.Item("dist_id")

        Else
            phznr = Request.QueryString("phznr")
            presentation = Request.QueryString("presentation")
            lblPageTitle.Text = Request.QueryString("pagetitle")
            prod_id = Request.QueryString("id")
            dist_id = Request.QueryString("dist_id")
            ed = Request.QueryString("ed")
            sd = Request.QueryString("sd")

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
        repData.addLine("Prod.Nr", phznr, True, True)

		MyStock.StockStartDate = sd
		MyStock.StockEndDate = ed
        MyStock.StockProdID = prod_id
		MyStock.StockDistID = dist_id
		MyGrid.DataSource = MyStock.GetStockWE()
		MyGrid.DataBind()
		Setgridstyles(MyGrid)

	End Sub

	Private Sub MyGrid_Databound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound

		Dim d As Double
		If e.Item.Cells(2).Text <> "&nbsp;" Then
			d = CDbl(e.Item.Cells(2).Text)
			sum1 = sum1 + d
			e.Item.Cells(2).Text = d.ToString(NUMBER_FORMAT_STRING)
		End If

		If e.Item.Cells(4).Text <> "&nbsp;" Then
			d = CDbl(e.Item.Cells(4).Text)
			sum2 = sum2 + d
			e.Item.Cells(4).Text = d.ToString(NUMBER_FORMAT_STRING_EXACT)

		End If

	End Sub
	Private Sub Item_created(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated
		Dim i As Integer = 0
		Dim d As Double


		If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.GroupFooter Then


			For Each cell As TableCell In e.Item.Cells()
				cell.Font.Bold = True
			Next



		ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Header Then
			e.Item.CssClass = "head"

		ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Footer Then

            e.Item.Cells(0).Text = "TOTAL"
			e.Item.Cells(2).Text = sum1.ToString(NUMBER_FORMAT_STRING)
			e.Item.Cells(4).Text = sum2.ToString(NUMBER_FORMAT_STRING_EXACT)


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
        exp.addDataGrid(MyGrid)
        exp.export()
    End Sub

End Class
