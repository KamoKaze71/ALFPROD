Imports Wyeth.Alf.CssStyles
Imports Wyeth.Alf.WyethDropdown
Imports Wyeth.Utilities.NumberFormat
Imports Wyeth.Utilities.DateHandling
Imports Wyeth.Utilities.Helper


Imports C1.Web.C1WebGrid

Public Class BackOrders


    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents txtStartDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents StartImage As System.Web.UI.WebControls.Image
    Protected WithEvents ddproduct As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddLineSelect As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddDistribselect As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnGenRep As System.Web.UI.WebControls.Button
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents ReportPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents endImage As System.Web.UI.WebControls.Image
    Protected WithEvents txtEndDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents repData As Wyeth.Alf.reportData
    Protected WithEvents prtControl As printReportCtl
    Protected WithEvents btnLevel1 As System.Web.UI.WebControls.Button
    Protected WithEvents btnLevel2 As System.Web.UI.WebControls.Button
    Protected WithEvents btnLevel3 As System.Web.UI.WebControls.Button

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
    Dim MyProduct As New WyethProduct
    Dim MyDatePopup As New JSPopUp(Me.Page)
    Dim units_ordered, values_ordered, units_uncomplete, values_uncomplete, Lines_shipped_ordered, Lines_shipped_uncomplete As Double
    Dim grpLvl As Integer = 5


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Page.IsPostBack Then
        Else

            'lists all available Distributors
            GetDistribSelectDD(ddDistribselect, Session("country_id"))
            'Lists all available Lines

            GetLineSelectDD(ddLineSelect, Session("country_id"))
            GetProductDescriptionSelectDD(ddproduct, ddLineSelect.SelectedValue, Session("country_id"), "NONOBS")
            txtStartDate.Text = FirstOfThisMonth(Session("CurrentProcessMonth")).ToString(DATEFORMAT_STRING_REPORT)
            txtEndDate.Text = LastOfThisMonth(CDate(Session("CurrentProcessMonth"))).ToString(DATEFORMAT_STRING_REPORT)
            MyDatePopup.AddDatePopupToControl(txtStartDate, StartImage)
            MyDatePopup.AddDatePopupToControl(txtEndDate, endImage)
            bindData()
        End If


    End Sub
    Private Sub bindData()
        repData.lastOrderDate = txtEndDate.Text
        SetGridStylesGroup(MyGrid)

        MyReport.StartDate = txtStartDate.Text
        MyReport.EndDate = txtEndDate.Text
        MyGrid.DataSource = MyReport.GetBackOrdersLines(ddproduct.SelectedValue, txtStartDate.Text, txtEndDate.Text, ddDistribselect.SelectedValue, ddLineSelect.SelectedValue)

        MyGrid.ShowFooter = True
        MyGrid.DataBind()
    End Sub

    Private Sub ddLineSelect_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddLineSelect.SelectedIndexChanged
        GetProductDescriptionSelectDD(ddproduct, ddLineSelect.SelectedValue, Session("country_id"), "NONOBS")
        ddproduct.SelectedValue = 0
        bindData()
    End Sub

    Private Sub ddDistribSelect_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddDistribselect.SelectedIndexChanged
        ddproduct.SelectedValue = 0
        bindData()
    End Sub

    Private Sub MyGrid_ItemDataBound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound

        units_ordered = units_ordered + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("units_ordered"))).Text
        values_ordered = values_ordered + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("values_ordered"))).Text
        units_uncomplete = units_uncomplete + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("units_uncomplete"))).Text
        values_uncomplete = values_uncomplete + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("values_uncomplete"))).Text
        Lines_shipped_ordered = Lines_shipped_ordered + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Lines_shipped_ordered"))).Text
        Lines_shipped_uncomplete = Lines_shipped_uncomplete + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Lines_shipped_uncomplete"))).Text


        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("units_ordered"))), 0)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("values_ordered"))), 2)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("units_uncomplete"))), 0)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("values_uncomplete"))), 2)

        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Lines_shipped_ordered"))), 0)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Lines_shipped_uncomplete"))), 0)


        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("percentage_lines"))), 1)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("percentage_units"))), 1)
        '   MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("percentage_values"))), 1)


    End Sub

    Private Sub MyGrid_ItemCreated(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated
       
        If e.Item.ItemType = C1ListItemType.GroupHeader Then

            Dim grplvl2 As Integer
            grplvl2 = e.Item.Attributes.Item("nodelevel")
            If grplvl2 = 1 Then
                Dim headerstring As String() = {"Product", "Units ordered", "Values ordered", "Units incomplete", "Values incomplete", "Lines ordered ", "Lines incompleted", "% Lines completed", "% Units or Values completed", "", "", "", ""}
                setColumnToolTips(e.Item, MyGrid, 0, headerstring, True)
            ElseIf grplvl2 > 1 Then
                Dim headerstring As String() = {"Product", "Presentation", "Units ordered", "Values ordered", "Units incomplete", "Values incomplete", " Lines ordered ", "Lines incomplete", "% Lines completed", "% Units or Values completed", "", "", "", ""}
                setColumnToolTips(e.Item, MyGrid, 0, headerstring, True)
            End If


            Dim tmpUnitsOrdered, tmpvalues_ordered, tmpunits_uncomplete, tmpvalues_uncomplete, tmpLines_shipped_ordered, tmpLines_shipped_uncomplete As Double

            tmpUnitsOrdered = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("units_ordered")) - grpLvl).Text
            tmpvalues_ordered = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("values_ordered")) - grpLvl).Text
            tmpunits_uncomplete = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("units_uncomplete")) - grpLvl).Text
            tmpvalues_uncomplete = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("values_uncomplete")) - grpLvl).Text()
            tmpLines_shipped_ordered = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Lines_shipped_ordered")) - grpLvl).Text()
            tmpLines_shipped_uncomplete = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Lines_shipped_uncomplete")) - grpLvl).Text()


            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("percentage_lines")) - grpLvl).HorizontalAlign = HorizontalAlign.Right
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("percentage_units")) - grpLvl).HorizontalAlign = HorizontalAlign.Right

            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("percentage_lines")) - grpLvl).Text = MyNumberFormat((((tmpLines_shipped_ordered - tmpLines_shipped_uncomplete) / tmpLines_shipped_ordered) * 100), 1)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("percentage_units")) - grpLvl).Text = MyNumberFormat((((tmpUnitsOrdered - tmpunits_uncomplete) / tmpUnitsOrdered) * 100), 1)

            MyNumberFormat((e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("units_ordered")) - grpLvl)), 0)
            MyNumberFormat((e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("values_ordered")) - grpLvl)), 2)
            MyNumberFormat((e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("units_uncomplete")) - grpLvl)), 0)
            MyNumberFormat((e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("values_uncomplete")) - grpLvl)), 2)
            MyNumberFormat((e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Lines_shipped_ordered")) - grpLvl)), 0)
            MyNumberFormat((e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Lines_shipped_uncomplete")) - grpLvl)), 0)



        ElseIf e.Item.ItemType = C1ListItemType.AlternatingItem Or e.Item.ItemType = C1ListItemType.Item Then

            Dim headerstring As String() = {"Product", "Presentation", "Prod. No.", "Date", "Order No.", "Customer", "Units ordered", "Values ordered", "Units incomplete", "Values incomplete", "Lines ordered ", "Lines incomplete", "% Lines completed", "% Units or Values completed", "", "", "", ""}
            setColumnToolTips(e.Item, MyGrid, 0, headerstring, True)
        End If

        If e.Item.ItemType = C1ListItemType.Footer Then
            e.Item.CssClass = "reportTotal"
            e.Item.Cells(2).Text = "TOTAL"
            e.Item.Cells(2).HorizontalAlign = HorizontalAlign.Left

            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("percentage_lines"))).Text = MyNumberFormat(((Lines_shipped_ordered - Lines_shipped_uncomplete) / Lines_shipped_ordered) * 100, 1)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("percentage_units"))).Text = MyNumberFormat(((units_ordered - units_uncomplete) / units_ordered) * 100, 1)
            '  e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("percentage_values"))).Text = MyNumberFormat(((values_ordered - values_uncomplete) / values_ordered) * 100, 1)

            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("units_ordered"))).Text = MyNumberFormat(units_ordered, 0)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("values_ordered"))).Text = MyNumberFormat(values_ordered, 2)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("units_uncomplete"))).Text = MyNumberFormat(units_uncomplete, 0)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("values_uncomplete"))).Text = MyNumberFormat(values_uncomplete, 2)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Lines_shipped_ordered"))).Text = MyNumberFormat(Lines_shipped_ordered, 0)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Lines_shipped_uncomplete"))).Text = MyNumberFormat(Lines_shipped_uncomplete, 0)


        End If




    End Sub

    Private Sub C1WebGrid1_SortCommand(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1SortCommandEventArgs) Handles MyGrid.SortCommand
        bindData()
    End Sub

    Private Sub ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportExcel.Click


        MyGrid.AllowAutoSort = False
        MyGrid.AllowSorting = False
        bindData()
        Dim exp As exportToExcel = New exportToExcel(Page)

        exp.addLine("Date from:" & txtStartDate.Text)
        exp.addLine("Date to:" & txtEndDate.Text)
        exp.addLine("Product selcted:" & ddproduct.SelectedValue.ToString)
        exp.title = Me.ALFPageTitle
        exp.formatColumnAsString(2)
        exp.formatColumnAsString(4)
        exp.showReportData = repData
        exp.addDataGrid(MyGrid)
        exp.export()
    End Sub

    Private Sub btnGenRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRep.Click
        Validate()
        If Page.IsValid = True Then
            bindData()
        End If
    End Sub

    '**************************************************************************************************************
    '* For Printing
    '**************************************************************************************************************
    Sub btnPrint_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Print_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Autoprint Or PopupWindowStyle.NoToolbar)
        bindData()
    End Sub

    Sub btnPreview_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Preview_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Fullscreen)
        bindData()
    End Sub

    Private Function PrePrint() As printReportUtil
        bindData()

        Dim preview As New printReportUtil

        preview.PageTitle = Me.ALFPageTitle
        preview.PageSizeLandscape = 35
        preview.DefaultOrientation = Orientation.Landscape

        preview.AddReportHeader(repData)
        preview.AddWebGrid(Me.MyGrid)

        Return preview

    End Function
    '**************************************************************************************************************
    '* End Printing
    '**************************************************************************************************************

    Private Sub btnLevel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLevel1.Click
        bindData()
        MyGrid.Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed
        MyGrid.Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed
        MyGrid.Columns(2).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed
    End Sub

    Private Sub btnLevel2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLevel2.Click
        bindData()
        MyGrid.Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
        MyGrid.Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed
        MyGrid.Columns(2).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed
    End Sub

    Private Sub btnLevel3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLevel3.Click
        bindData()
        MyGrid.Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
        MyGrid.Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
        MyGrid.Columns(2).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
    End Sub
End Class

