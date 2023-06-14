Imports Wyeth.Utilities.NumberFormat
Imports Wyeth.Utilities.DateHandling
Imports Wyeth.Utilities.Helper
Imports Wyeth.Alf.CssStyles
Imports Wyeth.Alf.WyethDropdown




Public Class FinanceMonthEndReport
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents ddmonth As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnGenRep As System.Web.UI.WebControls.Button
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents btnPrint As System.Web.UI.WebControls.Button
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents GridPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents MyGridInvoices As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents ddlineselect As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddDistribSelect As System.Web.UI.WebControls.DropDownList
    Protected WithEvents repData As reportData
    Protected WithEvents btn_lvl1 As System.Web.UI.WebControls.Button
    Protected WithEvents btn_lvl2 As System.Web.UI.WebControls.Button
    Protected WithEvents btn_lvl3 As System.Web.UI.WebControls.Button
    Protected WithEvents prtControl As Wyeth.Alf.printReportCtl
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


    Dim MyMep As New MEPData
    Dim sumInvoiceValue, sumAccruedValue, sumDiffToGIT, sumDiffToAccrued As Double
    Dim sumUnits As Integer
    Dim MyReport As New Report
    Dim sum_total_cogs As Double
    Dim sum_units As Double


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Page.IsPostBack = True Then
        Else
            lblPageTitle.Text = Request.QueryString("pagetitle")
            SetGridStylesGroup(MyGrid)
            SetGridStylesGroup(MyGridInvoices)
            GetLineSelectDD(ddlineselect, Session("country_id"))
            GetDistribSelectDD(ddDistribSelect, Session("country_id"))
            GetMonthSelectToProcessMonth(ddmonth, CDate(Session("CurrentProcessMonth")))
            ddmonth.SelectedValue = Convert.ToDateTime(Session("FinalJDEMonth")).ToString("MMM-yyyy", GetMyDTFI())
            BindData()
        End If
        repData.lastOrderDate = LastOfThisMonth(CDate(ddmonth.SelectedValue)).ToString(DATEFORMAT_STRING_REPORT, GetMyDTFI())
    End Sub


    Private Sub BindData()

        MyReport.StartDate = FirstOfThisMonth(CDate(ddmonth.SelectedValue))
        MyReport.EndDate = LastOfThisMonth(CDate(ddmonth.SelectedValue))
        MyReport.LineID = ddlineselect.SelectedValue
        MyReport.DistID = ddDistribSelect.SelectedValue

        MyGrid.DataSource = MyReport.GetStockCogsProduct()
        SetGridStylesGroup(MyGrid)
        MyGrid.AlternatingItemStyle.CssClass = "tableBGColor2Class"
        MyGrid.ItemStyle.CssClass = "tableBGColor2Class"
        MyGrid.DataBind()

        If ddlineselect.SelectedItem.ToString.ToUpper.StartsWith("ACT") Then
            MyGridInvoices.Visible = True
            MyGridInvoices.DataSource = MyMep.GetProcessedData(LastOfThisMonth(ddmonth.SelectedValue.ToString(CType(Application("MyDTFI"), IFormatProvider))), Session("country_id"))
            MyGridInvoices.DataBind()
        End If

       End Sub
    Private Sub MyGridInvoices_Databound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGridInvoices.ItemDataBound
        sumInvoiceValue = sumInvoiceValue + e.Item.Cells(MyGridInvoices.Columns.IndexOf(MyGridInvoices.Columns.ColumnByName("stoc_invoice_value"))).Text
        sumAccruedValue = sumAccruedValue + e.Item.Cells(MyGridInvoices.Columns.IndexOf(MyGridInvoices.Columns.ColumnByName("stoc_accrued_value"))).Text
        sumDiffToGIT = sumDiffToGIT + e.Item.Cells(MyGridInvoices.Columns.IndexOf(MyGridInvoices.Columns.ColumnByName("stoc_diff_value"))).Text
        sumDiffToAccrued = sumDiffToAccrued + e.Item.Cells(MyGridInvoices.Columns.IndexOf(MyGridInvoices.Columns.ColumnByName("stoc_diff_value_accrued"))).Text
        sumUnits = sumUnits + e.Item.Cells(MyGridInvoices.Columns.IndexOf(MyGridInvoices.Columns.ColumnByName("stoc_unit"))).Text

        MyNumberFormat(e.Item.Cells(MyGridInvoices.Columns.IndexOf(MyGridInvoices.Columns.ColumnByName("stoc_unit"))), 0)
        MyNumberFormat(e.Item.Cells(MyGridInvoices.Columns.IndexOf(MyGridInvoices.Columns.ColumnByName("stoc_invoice_value"))), 2)
        MyNumberFormat(e.Item.Cells(MyGridInvoices.Columns.IndexOf(MyGridInvoices.Columns.ColumnByName("stoc_accrued_value"))), 2)
        MyNumberFormat(e.Item.Cells(MyGridInvoices.Columns.IndexOf(MyGridInvoices.Columns.ColumnByName("stoc_diff_value"))), 2)
        MyNumberFormat(e.Item.Cells(MyGridInvoices.Columns.IndexOf(MyGridInvoices.Columns.ColumnByName("stoc_diff_value_accrued"))), 2)
    End Sub

    Private Sub MyGridInvoices_ItemCreated(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGridInvoices.ItemCreated

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.GroupHeader Or e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.GroupFooter Then
            MyNumberFormat(e.Item.Cells(MyGridInvoices.Columns.IndexOf(MyGridInvoices.Columns.ColumnByName("stoc_unit")) - 9), 0)
            MyNumberFormat(e.Item.Cells(MyGridInvoices.Columns.IndexOf(MyGridInvoices.Columns.ColumnByName("stoc_invoice_value")) - 9), 2)
            MyNumberFormat(e.Item.Cells(MyGridInvoices.Columns.IndexOf(MyGridInvoices.Columns.ColumnByName("stoc_accrued_value")) - 9), 2)
            MyNumberFormat(e.Item.Cells(MyGridInvoices.Columns.IndexOf(MyGridInvoices.Columns.ColumnByName("stoc_diff_value")) - 9), 2)
            MyNumberFormat(e.Item.Cells(MyGridInvoices.Columns.IndexOf(MyGridInvoices.Columns.ColumnByName("stoc_diff_value_accrued")) - 9), 2)
        End If

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Footer Then
            e.Item.HorizontalAlign = HorizontalAlign.Right
            e.Item.CssClass = "reportTotal"
            e.Item.Cells(MyGridInvoices.Columns.IndexOf(MyGridInvoices.Columns.ColumnByName("stoc_invoice_value"))).Text = MyNumberFormat(sumInvoiceValue, 2)
            e.Item.Cells(MyGridInvoices.Columns.IndexOf(MyGridInvoices.Columns.ColumnByName("stoc_accrued_value"))).Text = MyNumberFormat(sumAccruedValue, 2)
            e.Item.Cells(MyGridInvoices.Columns.IndexOf(MyGridInvoices.Columns.ColumnByName("stoc_diff_value"))).Text = MyNumberFormat(sumDiffToGIT, 2)
            e.Item.Cells(MyGridInvoices.Columns.IndexOf(MyGridInvoices.Columns.ColumnByName("stoc_diff_value_accrued"))).Text = MyNumberFormat(sumDiffToAccrued, 2)
            e.Item.Cells(MyGridInvoices.Columns.IndexOf(MyGridInvoices.Columns.ColumnByName("stoc_unit"))).Text = MyNumberFormat(sumUnits, 0)
        End If

    End Sub
    Private Sub C1WebGrid1_SortCommand(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1SortCommandEventArgs) Handles MyGrid.SortCommand
        BindData()
    End Sub

    Private Sub Item_created(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated
        Dim myGridtmp As C1.Web.C1WebGrid.C1WebGrid
        myGridtmp = sender
        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.GroupHeader Then
            MyNumberFormat(e.Item.Cells(1), 0)
            MyNumberFormat(e.Item.Cells(2), 2)
        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Footer Then
            e.Item.Cells(MyGrid.Columns.IndexOf(myGridtmp.Columns.ColumnByName("units"))).Text = MyNumberFormat(sum_units, 0)
            e.Item.Cells(MyGrid.Columns.IndexOf(myGridtmp.Columns.ColumnByName("total_cogs"))).Text = MyNumberFormat(sum_total_cogs, 2)
            e.Item.CssClass = "reportTotal"
        End If

    End Sub


    Private Sub Item_Databound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound
        Dim myGridtmp As C1.Web.C1WebGrid.C1WebGrid
        myGridtmp = sender

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Or e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then

            sum_units = sum_units + e.Item.Cells(myGridtmp.Columns.IndexOf(myGridtmp.Columns.ColumnByName("units"))).Text
            sum_total_cogs = sum_total_cogs + e.Item.Cells(myGridtmp.Columns.IndexOf(myGridtmp.Columns.ColumnByName("total_cogs"))).Text

            If e.Item.Cells(0).Text.StartsWith("FA") Or e.Item.Cells(0).Text.StartsWith("FG") Then 'it is a FA
                e.Item.Cells(myGridtmp.Columns.IndexOf(MyGrid.Columns.ColumnByName("cc"))).Attributes.Add("onclick", "javascript:OpenPopUp('STOCKCOGSPRODUCTDETAIL.aspx?cc_id=" & (e.Item.Cells(1).Text) & "&bewegkz_desc=" & (e.Item.Cells(2).Text) & "&cc_desc=" & (e.Item.Cells(4).Text) & "&code_id_bewegkz=-1" & "&sd=" & MyReport.StartDate & "&ed=" & MyReport.EndDate & "&dist_id=" & ddDistribSelect.SelectedValue & "&line_id=" & ddlineselect.SelectedValue & " ','StockCogs');")
            Else  '  wenn es eine Lagebewegung ist
                e.Item.Cells(myGridtmp.Columns.IndexOf(MyGrid.Columns.ColumnByName("cc"))).Attributes.Add("onclick", "javascript:OpenPopUp('STOCKCOGSPRODUCTDETAIL.aspx?cc_id=" & (e.Item.Cells(1).Text) & "&bewegkz_desc=" & (e.Item.Cells(2).Text) & "&cc_desc=" & (e.Item.Cells(4).Text) & "&code_id_bewegkz=" & e.Item.Cells(0).Text & "&sd=" & MyReport.StartDate & "&ed=" & MyReport.EndDate & "&dist_id=" & ddDistribSelect.SelectedValue & "&line_id=" & ddlineselect.SelectedValue & "','StockCogs');")
            End If

            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor2Class';")

            MyNumberFormat(e.Item.Cells(myGridtmp.Columns.IndexOf(MyGrid.Columns.ColumnByName("units"))), 0)
            MyNumberFormat(e.Item.Cells(myGridtmp.Columns.IndexOf(MyGrid.Columns.ColumnByName("total_cogs"))), 2)
            MyNumberFormat(e.Item.Cells(myGridtmp.Columns.IndexOf(MyGrid.Columns.ColumnByName("value"))), 2)

        End If

    End Sub

    Private Sub btnGenRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRep.Click
        Page.Validate()
        If Page.IsValid Then
            BindData()
        End If
    End Sub

    Private Sub ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportExcel.Click
        BindData()
       

        For Each col As C1.Web.C1WebGrid.C1Column In MyGrid.Columns()
            col.Visible = True
            col.GroupInfo.Position = C1.Web.C1WebGrid.GroupPositionEnum.None
            col.ItemStyle.Wrap = False
        Next

        MyGrid.Columns(0).Visible = False
        MyGrid.Columns(1).Visible = False

        For Each col As C1.Web.C1WebGrid.C1Column In MyGridInvoices.Columns()
            col.Visible = True
            col.ItemStyle.Wrap = False
        Next

        Dim exp As exportToExcel = New exportToExcel(Page)
        exp.title = lblPageTitle.Text
        exp.showReportData = repData
        exp.formatColumnAsString(5)
        exp.addDataGrid(MyGrid)
        exp.addDataGrid(MyGridInvoices)
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
        MyGrid.Columns(4).GroupInfo.OutlineMode = C1.Web.C1WebGrid.OutlineModeEnum.StartExpanded

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


    
        preview.PageTitle = Me.lblPageTitle.Text
        preview.AddReportHeader(repdata)
        preview.PageSizeLandscape = 31
        preview.DefaultOrientation = Orientation.Landscape
        preview.AddWebGrid(Me.MyGrid)

        Return preview

    End Function

    Private Sub btn_lvl4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_lvl4.Click
        BindData()
        MyGrid.Columns(2).GroupInfo.OutlineMode = C1.Web.C1WebGrid.OutlineModeEnum.StartExpanded
        MyGrid.Columns(3).GroupInfo.OutlineMode = C1.Web.C1WebGrid.OutlineModeEnum.StartExpanded
        MyGrid.Columns(4).GroupInfo.OutlineMode = C1.Web.C1WebGrid.OutlineModeEnum.StartExpanded
        MyGrid.Columns(5).GroupInfo.OutlineMode = C1.Web.C1WebGrid.OutlineModeEnum.StartExpanded

    End Sub
End Class




