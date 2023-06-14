Imports Wyeth.Alf.WyethDropdown
Imports Wyeth.Utilities.DateHandling
Imports Wyeth.Utilities.Helper
Imports Wyeth.Alf.CssStyles



Public Class StockCogs
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddLineselect As System.Web.UI.WebControls.DropDownList
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents btn_refresh As System.Web.UI.WebControls.Button
    Protected WithEvents ddDistribSelect As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblReportDateFrom As System.Web.UI.WebControls.Label
    Protected WithEvents lblReportDateTO As System.Web.UI.WebControls.Label
    Protected WithEvents lblLastOrderEntry As System.Web.UI.WebControls.Label
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents txtStartDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents txtEndDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents GridPanel As System.Web.UI.WebControls.Panel

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim MyReport As New Report
    Dim sum_total_cogs As Double
    Dim sum_units As Double

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here



        If Page.IsPostBack = True Then

            sum_total_cogs = 0
            sum_units = 0
        Else
            txtStartDate.Text = FirstOfThisMonth(Today()).ToString(DATEFORMAT_STRING_REPORT)
            txtEndDate.Text = LastOfThisMonth(Today()).ToString(DATEFORMAT_STRING_REPORT)
            GetLineSelectDD(ddLineselect, Session("country_id"))
            GetDistribSelectDD(ddDistribSelect, Session("country_id"))
            SetGridStylesGroup(MyGrid)
            binddata()
        End If
    End Sub

    Private Sub binddata()

        MyReport.StartDate = txtStartDate.Text
        MyReport.EndDate = txtEndDate.Text
        MyReport.LineID = ddLineselect.SelectedValue
        MyReport.DistID = ddDistribSelect.SelectedValue


        MyGrid.DataSource = MyReport.GetStockCogs()
        MyGrid.DataBind()


    End Sub


    Private Sub C1WebGrid1_SortCommand(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1SortCommandEventArgs) Handles MyGrid.SortCommand
        BindData()
    End Sub 'C1WebGrid1_SortCommand

    Private Sub btn_refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_refresh.Click
        Page.Validate()
        If Page.IsValid Then
            BindData()
        End If
    End Sub
    Private Sub Item_created(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated


        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.GroupHeader Then
            e.Item.CssClass = "tableGroupFooterBG"
        End If

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Footer Then
            e.Item.Cells(7).Text = sum_units
            e.Item.Cells(9).Text = sum_total_cogs
        End If
    End Sub

    Private Sub Item_Databound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound


        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Or e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then
            sum_units = sum_units + e.Item.Cells(7).Text
            sum_total_cogs = sum_total_cogs + e.Item.Cells(9).Text
        End If

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Footer Then
            e.Item.Cells(7).Text = sum_units
            e.Item.Cells(9).Text = sum_total_cogs
        End If

    End Sub


End Class
