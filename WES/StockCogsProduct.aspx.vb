Imports Wyeth.Alf.WyethDropdown
Imports Wyeth.Utilities.DateHandling
Imports Wyeth.Utilities.NumberFormat
Imports Wyeth.Utilities.Helper
Imports Wyeth.Alf.CssStyles
Imports System.Globalization

Public Class StockCogsProduct
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents txtStartDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents btn_refresh As System.Web.UI.WebControls.Button
    Protected WithEvents ddlineselect As System.Web.UI.WebControls.DropDownList
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
            GetLineSelectDD(ddlineselect, Session("country_id"))
            GetDistribSelectDD(ddDistribSelect, Session("country_id"))

            BindData()
        End If
        repData.lastOrderDate = txtEndDate.Text
    End Sub

    Private Sub BindData()

        MyReport.StartDate = txtStartDate.Text
        MyReport.EndDate = txtEndDate.Text
        MyReport.LineID = ddlineselect.SelectedValue
        MyReport.DistID = ddDistribSelect.SelectedValue


        MyGrid.DataSource = MyReport.GetStockCogsProduct()
        SetGridStylesGroup(MyGrid)
        MyGrid.AlternatingItemStyle.CssClass = "tableBGColor2Class"
        MyGrid.ItemStyle.CssClass = "tableBGColor2Class"

        MyGrid.DataBind()


    End Sub

    Private Sub C1WebGrid1_SortCommand(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1SortCommandEventArgs) Handles MyGrid.SortCommand
        binddata()
    End Sub 'C1WebGrid1_SortCommand

    Private Sub Item_created(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated
        Dim myGridtmp As C1.Web.C1WebGrid.C1WebGrid


        myGridtmp = sender

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.GroupHeader Then
            MyNumberFormat(e.Item.Cells(1), 0)
            MyNumberFormat(e.Item.Cells(2), 2)
            MyNumberFormat(e.Item.Cells(3), 2)
        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Footer Then
            e.Item.Cells(MyGrid.Columns.IndexOf(myGridtmp.Columns.ColumnByName("units"))).Text = MyNumberFormat(sum_units, 0)
            e.Item.Cells(MyGrid.Columns.IndexOf(myGridtmp.Columns.ColumnByName("total_cogs"))).Text = MyNumberFormat(sum_total_cogs, 2)
            e.Item.Cells(MyGrid.Columns.IndexOf(myGridtmp.Columns.ColumnByName("value"))).Text = MyNumberFormat(sum_value, 2)
            e.Item.CssClass = "reportTotal"
        End If

    End Sub
    Private Sub Item_Databound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound
        Dim myGridtmp As C1.Web.C1WebGrid.C1WebGrid
        myGridtmp = sender

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Or e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then

            sum_units = sum_units + e.Item.Cells(myGridtmp.Columns.IndexOf(myGridtmp.Columns.ColumnByName("units"))).Text
            sum_total_cogs = sum_total_cogs + e.Item.Cells(myGridtmp.Columns.IndexOf(myGridtmp.Columns.ColumnByName("total_cogs"))).Text
            sum_value = sum_value + e.Item.Cells(myGridtmp.Columns.IndexOf(myGridtmp.Columns.ColumnByName("value"))).Text

            If e.Item.Cells(0).Text = "-1" Then 'it is a FA
                e.Item.Cells(myGridtmp.Columns.IndexOf(myGridtmp.Columns.ColumnByName("prod_phznr"))).Attributes.Add("onclick", "javascript:OpenPopUp('STOCKCOGSPRODUCTDETAIL.aspx?cc_id=" & (e.Item.Cells(1).Text) & "&bewegkz_desc=" & (e.Item.Cells(2).Text) & "&cc_desc=" & (e.Item.Cells(4).Text) & "&code_id_bewegkz=-1" & "&sd=" & MyReport.StartDate & "&ed=" & MyReport.EndDate & "&dist_id=" & ddDistribSelect.SelectedValue & "&line_id=" & ddlineselect.SelectedValue & " ','StockCogs');")
            ElseIf e.Item.Cells(0).Text = "-2" Then 'it is FA (FG)
                e.Item.Cells(myGridtmp.Columns.IndexOf(myGridtmp.Columns.ColumnByName("prod_phznr"))).Attributes.Add("onclick", "javascript:OpenPopUp('STOCKCOGSPRODUCTDETAIL.aspx?cc_id=" & (e.Item.Cells(1).Text) & "&bewegkz_desc=" & (e.Item.Cells(2).Text) & "&cc_desc=" & (e.Item.Cells(4).Text) & "&code_id_bewegkz=-2" & "&sd=" & MyReport.StartDate & "&ed=" & MyReport.EndDate & "&dist_id=" & ddDistribSelect.SelectedValue & "&line_id=" & ddlineselect.SelectedValue & " ','StockCogs');")
            Else  '  wenn es eine Lagebewegung ist
                'e.Item.Cells(myGridtmp.Columns.IndexOf(myGridtmp.Columns.ColumnByName("cc"))).Attributes.Add("onclick", "javascript:OpenPopUp('STOCKCOGSPRODUCTDETAIL.aspx?cc_id=" & (e.Item.Cells(1).Text) & "&bewegkz_desc=" & (e.Item.Cells(2).Text) & "&cc_desc=" & (e.Item.Cells(4).Text) & "&code_id_bewegkz=" & e.Item.Cells(0).Text & "&sd=" & MyReport.StartDate & "&ed=" & MyReport.EndDate & "&dist_id=" & ddDistribSelect.SelectedValue & "&line_id=" & ddlineselect.SelectedValue & "','StockCogs');")
                e.Item.Cells(myGridtmp.Columns.IndexOf(myGridtmp.Columns.ColumnByName("prod_phznr"))).Attributes.Add("onclick", "javascript:OpenPopUp('STOCKCOGSPRODUCTDETAIL.aspx?phznr=" & e.Item.Cells(myGridtmp.Columns.IndexOf(myGridtmp.Columns.ColumnByName("prod_phznr"))).Text & "&bewegkz_desc=" & (e.Item.Cells(2).Text) & "&cc_desc=" & (e.Item.Cells(4).Text) & "&code_id_bewegkz=" & e.Item.Cells(0).Text & "&sd=" & MyReport.StartDate & "&ed=" & MyReport.EndDate & "&dist_id=" & ddDistribSelect.SelectedValue & "&line_id=" & ddlineselect.SelectedValue & "','StockCogs');")

            End If

            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor2Class';")

            MyNumberFormat(e.Item.Cells(myGridtmp.Columns.IndexOf(myGridtmp.Columns.ColumnByName("units"))), 0)
            MyNumberFormat(e.Item.Cells(myGridtmp.Columns.IndexOf(myGridtmp.Columns.ColumnByName("total_cogs"))), 2)
            MyNumberFormat(e.Item.Cells(myGridtmp.Columns.IndexOf(myGridtmp.Columns.ColumnByName("value"))), 2)

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
        exp.title = lblPageTitle.Text
        exp.showReportData = repData
        exp.formatColumnAsString(5)
        exp.addDataGrid(MyGrid)
        exp.export()
    End Sub

    Private Sub btn_lvl1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_lvl1.Click
        BindData()
        MyGrid.Columns(2).GroupInfo.OutlineMode = C1.Web.C1WebGrid.OutlineModeEnum.StartCollapsed
        MyGrid.Columns(3).GroupInfo.OutlineMode = C1.Web.C1WebGrid.OutlineModeEnum.StartCollapsed
        MyGrid.Columns(4).GroupInfo.OutlineMode = C1.Web.C1WebGrid.OutlineModeEnum.StartCollapsed

    End Sub

    Private Sub btn_lvl2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_lvl2.Click
        BindData()
        MyGrid.Columns(2).GroupInfo.OutlineMode = C1.Web.C1WebGrid.OutlineModeEnum.StartExpanded
        MyGrid.Columns(3).GroupInfo.OutlineMode = C1.Web.C1WebGrid.OutlineModeEnum.StartCollapsed
        MyGrid.Columns(4).GroupInfo.OutlineMode = C1.Web.C1WebGrid.OutlineModeEnum.StartCollapsed

    End Sub

    Private Sub btn_lvl3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_lvl3.Click
        BindData()
        MyGrid.Columns(2).GroupInfo.OutlineMode = C1.Web.C1WebGrid.OutlineModeEnum.StartExpanded
        MyGrid.Columns(3).GroupInfo.OutlineMode = C1.Web.C1WebGrid.OutlineModeEnum.StartExpanded
        MyGrid.Columns(4).GroupInfo.OutlineMode = C1.Web.C1WebGrid.OutlineModeEnum.StartCollapsed

    End Sub

    Private Sub btn_lvl4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_lvl4.Click
        BindData()
        MyGrid.Columns(2).GroupInfo.OutlineMode = C1.Web.C1WebGrid.OutlineModeEnum.StartExpanded
        MyGrid.Columns(3).GroupInfo.OutlineMode = C1.Web.C1WebGrid.OutlineModeEnum.StartExpanded
        MyGrid.Columns(4).GroupInfo.OutlineMode = C1.Web.C1WebGrid.OutlineModeEnum.StartExpanded

    End Sub
End Class
