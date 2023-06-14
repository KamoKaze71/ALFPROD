Imports Wyeth.Alf.WyethDropdown
Imports Wyeth.Utilities.DateHandling
Imports Wyeth.Utilities.NumberFormat
Imports Wyeth.Utilities.Helper
Imports Wyeth.Alf.CssStyles
Imports System.Globalization

Public Class ForteDownloadME
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents txtStartDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents btn_refresh As System.Web.UI.WebControls.Button
    Protected WithEvents ddDistribSelect As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblReportDateFrom As System.Web.UI.WebControls.Label
    Protected WithEvents lblReportDateTO As System.Web.UI.WebControls.Label
    Protected WithEvents lblLastOrderEntry As System.Web.UI.WebControls.Label
    Protected WithEvents txtEndDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents GridPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents repData As Wyeth.Alf.reportData
    Protected WithEvents btnGenRep As System.Web.UI.WebControls.Button
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents btn_lvl1 As System.Web.UI.WebControls.Button
    Protected WithEvents btn_lvl2 As System.Web.UI.WebControls.Button
    Protected WithEvents btn_lvl3 As System.Web.UI.WebControls.Button
    Protected WithEvents btn_lvl4 As System.Web.UI.WebControls.Button

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
    Dim sum_total_cogs, sum_value As Double
    Dim sum_units As Double
    Dim sumUnits As Integer


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        If Page.IsPostBack = True Then
            sum_total_cogs = 0
            sum_units = 0
        Else
            txtStartDate.Text = FirstOfThisMonth(Session("FinalJDEMonth")).ToString(DATEFORMAT_STRING)
            txtEndDate.Text = LastOfThisMonth(Session("FinalJDEMonth")).ToString(DATEFORMAT_STRING)
            lblPageTitle.Text = Request.QueryString("pagetitle")
            GetDistribSelectDD(ddDistribSelect, Session("country_id"))

            BindData()
        End If
        repData.lastOrderDate = txtEndDate.Text
    End Sub

    Private Sub BindData()

        Me.Validate()

        If Me.IsValid() = True Then
            MyGrid.DataSource = MyReport.GetForteDownload(txtStartDate.Text, txtEndDate.Text, ddDistribSelect.SelectedValue)
            SetGridStylesGroup(MyGrid)

            MyGrid.AlternatingItemStyle.CssClass = "tableBGColor2Class"
            MyGrid.ItemStyle.CssClass = "tableBGColor2Class"

            MyGrid.DataBind()
        End If


    End Sub

    Private Sub C1WebGrid1_SortCommand(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1SortCommandEventArgs) Handles MyGrid.SortCommand
        BindData()
    End Sub

    Private Sub Item_created(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated


        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.GroupHeader Then
            MyNumberFormat(e.Item.Cells(1), 0)
            MyNumberFormat(e.Item.Cells(2), 2)
            MyNumberFormat(e.Item.Cells(3), 2)
        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Footer Then
            e.Item.Cells(0).Text = "Total:"
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("units"))).Text = MyNumberFormat(sum_units, 0)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("tcogs"))).Text = MyNumberFormat(sum_total_cogs, 2)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("value"))).Text = MyNumberFormat(sum_value, 2)
            e.Item.CssClass = "reportTotal"
        End If

    End Sub
    Private Sub Item_Databound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Or e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then

            sum_units = sum_units + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("units"))).Text
            sum_total_cogs = sum_total_cogs + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("tcogs"))).Text
            sum_value = sum_value + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("value"))).Text

            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("units"))), 0)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("tcogs"))), 2)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("value"))), 2)

        End If

    End Sub



    Private Sub btnGenRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRep.Click
        Page.Validate()
        If Page.IsValid Then
            BindData()
        Else
            MyGrid.Visible = False
        End If
    End Sub
    Private Sub ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportExcel.Click


        BindData()

        Dim exp As exportToExcel = New exportToExcel(Page)
        exp.ExportGroupHeaders = False
        exp.ExportHeaders = False
        exp.formatColumnAsString(2)
        exp.addDataGrid(MyGrid)
        exp.export()
    End Sub

    Private Sub btn_lvl1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_lvl1.Click
        BindData()
        MyGrid.Columns(6).GroupInfo.OutlineMode = C1.Web.C1WebGrid.OutlineModeEnum.StartCollapsed


    End Sub

    Private Sub btn_lvl2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_lvl2.Click
        BindData()
        MyGrid.Columns(6).GroupInfo.OutlineMode = C1.Web.C1WebGrid.OutlineModeEnum.StartExpanded


    End Sub

End Class
