Imports Wyeth.Utilities
Imports Wyeth.Utilities.Helper
Imports Wyeth.Utilities.NumberFormat
Imports Wyeth.Alf.CssStyles
Imports Wyeth.Utilities.DateHandling
Imports Wyeth.Alf.WyethDropdown
Imports C1.Web.C1WebGrid


Public Class ReturnsCheckReport
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddlineselect As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddDistribSelect As System.Web.UI.WebControls.DropDownList

    Protected WithEvents btnGenRep As System.Web.UI.WebControls.Button
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel

    Protected WithEvents btn_lvl1 As System.Web.UI.WebControls.Button
    Protected WithEvents btn_lvl2 As System.Web.UI.WebControls.Button
    Protected WithEvents btn_lvl3 As System.Web.UI.WebControls.Button
    Protected WithEvents btn_lvl4 As System.Web.UI.WebControls.Button

    Protected WithEvents GridPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents txtStartDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents txtEndDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents repdata As reportData
    Protected WithEvents groupby As System.Web.UI.WebControls.DropDownList
    Protected WithEvents prtControl As printPreviewCtl
   
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim sum_total_cogs, sum_value As Double
    '  Dim sum_units As Double
    Dim sumUnits As Integer
    Dim sumFGUnits, sum_UnitsKH, sum_UnitsGH, i As Double
    Dim sumValue, sumFGValue As Double
    Dim BwegKZ As String
    Dim nodelevel As Integer


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        ResetSum()

        If Not Page.IsPostBack Then
            Me.ALFPageTitle = Request.QueryString("PageTitle")
            GetLineSelectDD(ddlineselect, Session("country_id"))
            GetDistribSelectDD(ddDistribSelect, Session("country_id"))
            fillGroupByDropdown()

            txtStartDate.Text = FirstOfThisMonth(repdata.lastOrderDate).ToString(DATEFORMAT_STRING)
            txtEndDate.Text = LastOfThisMonth(repdata.lastOrderDate).ToString(DATEFORMAT_STRING)



        End If
        bindData()
        i = 7
    End Sub

    Private Sub fillReportData()

        repdata.lastOrderDate = txtEndDate.Text
        repdata.addLine("Report-date from", txtStartDate.Text, True, False)
        repdata.addLine("Report-date to", txtEndDate.Text, True, False)
        repdata.addLine("Selected Line", ddlineselect.SelectedItem.Text.ToString, True, False)
        repdata.addLine("Grouped by", groupby.SelectedItem.ToString, True, False)
    End Sub

    'Wie befüllen unser "grouped by" dropdown. Der value sind gleichzeitig die ID's
    'der spalten nach denen gruppiert wird
    Private Sub fillGroupByDropdown()
       
        Dim it1 As ListItem = New ListItem
        it1.Value = 1
        it1.Text = "Product Group & BewegKZ"
        groupby.Items.Add(it1)

        Dim it2 As ListItem = New ListItem
        it2.Value = 2
        it2.Text = "BewegKZ & Product Group"
        groupby.Items.Add(it2)

      


        groupby.DataBind()
    End Sub

    Private Sub bindData()

        Dim myData As New DataView
        Dim myReport As New Report
        fillReportData()

        myData = myReport.GetReturnCheck(ddDistribSelect.SelectedValue, ddlineselect.SelectedValue, CDate(txtStartDate.Text), CDate(txtEndDate.Text))

        With MyGrid
            .DataSource = myData
            .GridLines = GridLines.None
            .ShowFooter = True
            .AllowSorting = False
            .AllowAutoSort = True

            'um richtig zu gruppieren müssen die columns zuerst gemoved werden.
            'fürs moven müssen wir die cols holen und removen und dann inserten

            Dim BEWEGKZ_Column As New C1BoundColumn
            BEWEGKZ_Column = .Columns.ColumnByName("BEWEGKZ")
            .Columns.RemoveAt(.Columns.IndexOf(BEWEGKZ_Column))

            Dim PRGR_CODEColumn As New C1BoundColumn
            PRGR_CODEColumn = .Columns.ColumnByName("PRGR_CODE")
            .Columns.RemoveAt(.Columns.IndexOf(PRGR_CODEColumn))

            Dim PROD_DESCRIPTIONColumn As New C1BoundColumn
            PROD_DESCRIPTIONColumn = .Columns.ColumnByName("PROD_DESCRIPTION")
            .Columns.RemoveAt(.Columns.IndexOf(PROD_DESCRIPTIONColumn))

            If groupby.SelectedValue = "2" Then
                .Columns.Insert(1, BEWEGKZ_Column)
                .Columns.Insert(2, PRGR_CODEColumn)
                .Columns.Insert(3, PROD_DESCRIPTIONColumn)


                .Columns(0).Visible = False
                .Columns(1).Visible = False
                .Columns(2).Visible = False
                .Columns(3).Visible = False

            ElseIf groupby.SelectedValue = "1" Then
                .Columns.Insert(1, PRGR_CODEColumn)
                .Columns.Insert(2, PROD_DESCRIPTIONColumn)
                .Columns.Insert(3, BEWEGKZ_Column)


                .Columns(0).Visible = False
                .Columns(1).Visible = False
                .Columns(2).Visible = False
                .Columns(3).Visible = False
            End If

            .Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
            .Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed
            .Columns(2).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed

            .Columns(0).SortDirection = C1SortDirection.Ascending
            .Columns(1).SortDirection = C1SortDirection.Ascending
            .Columns(2).SortDirection = C1SortDirection.Ascending

            .DataBind()
        End With
        SetGridStylesGroup(MyGrid)
    End Sub

    Private Sub C1WebGrid1_SortCommand(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1SortCommandEventArgs) Handles MyGrid.SortCommand
        bindData()
    End Sub 'C1WebGrid1_SortCommand
    Private Sub Item_created(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated
        setColumnToolTips(e.Item, MyGrid, 7)

        If e.Item.ItemType = C1ListItemType.GroupFooter Or e.Item.ItemType = C1ListItemType.GroupHeader Then
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("ORPO_UNITS")) - i), 0)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("ORPO_FGUNITS")) - i), 0)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("ORPO_VALUE")) - i), 2)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("ORPO_FGVALUE")) - i), 2)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("t_cogs")) - i), 2)
            nodelevel = e.Item.Attributes.Item("nodelevel")
            If nodelevel > 0 And MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("ORPO_UNITS")) - i).Text, 0) <> 0 Then
                For Each cell As TableCell In e.Item.Cells
                    cell.ForeColor = Color.Red
                Next
            End If
        End If


        If e.Item.ItemType = C1ListItemType.Footer Then
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("ORPO_UNITS"))).Text = MyNumberFormat(sumUnits, 0)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("ORPO_FGUNITS"))).Text = MyNumberFormat(sumFGUnits, 0)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("ORPO_VALUE"))).Text = MyNumberFormat(sumValue, 2)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("ORPO_FGVALUE"))).Text = MyNumberFormat(sumFGValue, 2)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("t_cogs"))).Text = MyNumberFormat(sum_total_cogs, 2)

            e.Item.CssClass = "reportTotal"
            e.Item.Cells(4).Text = "TOTAL AUSTRIA"
            e.Item.Cells(4).HorizontalAlign = HorizontalAlign.Left
        End If


    End Sub

    'Private Sub MyGrid_ItemGrouptext(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1GroupTextEventArgs) Handles MyGrid.GroupText

    '    If e.GroupText.StartsWith("Fakturen") Then
    '        e.Text = "Total " & e.GroupText & ":"

    '    ElseIf e.GroupText.StartsWith("Sonstige") Then
    '        e.Text = "Total " & e.GroupText & ":"
    '    Else
    '        If e.StartItemIndex > 0 Then
    '            e.Text = e.GroupText
    '            formatHeader(e)
    '        End If
    '    End If
    'End Sub


    Private Sub MyGrid_ItemGroupAgg(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1GroupTextEventArgs) Handles MyGrid.GroupText
        If e.GroupText.StartsWith("Fakturen") Then
            e.Text = "Total " & e.GroupText & ":"

        ElseIf e.GroupText.StartsWith("Sonstige") Then
            e.Text = "Total " & e.GroupText & ":"

        Else
            e.Text = e.GroupText
            If e.StartItemIndex > 0 Then
                e.Text = e.GroupText
                ' formatHeader(e)
            End If
        End If

    End Sub



    'Private Sub formatHeader(ByVal e As C1.Web.C1WebGrid.C1GroupTextEventArgs)
    '    Dim g As C1GridItem


    '    If nodelevel = 1 And groupby.SelectedValue = 2 Then
    '        g = MyGrid.Items(e.StartItemIndex)
    '        Dim stext = e.GroupText()

    '        If sumUnits <> 0 Then

    '            e.Text = "<Font color=red><strong>WARNING! </strong>" & stext & "</font>"
    '        End If

    '    ElseIf nodelevel = 3 And groupby.SelectedValue = 1 Then


    '        Dim stext = e.GroupText()

    '        If sumUnits <> 0 Then
    '            e.Text = "<Font color=red><strong>WARNING! </strong>" & stext & "</font>"
    '        End If
    '    End If
    '    nodelevel = 0

    'End Sub


    Private Sub btnGenRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRep.Click

        Page.Validate()
        If Page.IsValid = True Then

            bindData()
        Else
            MyGrid.Visible = False
        End If


    End Sub

    Private Sub Item_DataBound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound

        If e.Item.ItemType = C1ListItemType.AlternatingItem Or e.Item.ItemType = C1ListItemType.Item Then
            sumUnits = sumUnits + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("ORPO_UNITS"))).Text
            sumFGUnits = sumFGUnits + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("ORPO_FGUNITS"))).Text
            sumValue = sumValue + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("ORPO_VAlUE"))).Text
            sumFGValue = sumFGValue + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("ORPO_FGVALUE"))).Text
            sum_total_cogs = sum_total_cogs + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("t_cogs"))).Text
        End If
    End Sub


    Private Sub ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportExcel.Click
        bindData()

        MyGrid.Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.None
        MyGrid.Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.None
        MyGrid.Columns(2).GroupInfo.OutlineMode = OutlineModeEnum.None

        Dim exp As exportToExcel = New exportToExcel(Page)
        exp.title = Me.ALFPageTitle
        exp.showReportData = repdata
        exp.addDataGrid(MyGrid)
        exp.export()
    End Sub

    Private Sub btnLevel3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_lvl3.Click
        bindData()
        MyGrid.Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
        MyGrid.Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
        MyGrid.Columns(2).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
    End Sub

    Private Sub btnLevel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_lvl1.Click
        bindData()
        MyGrid.Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
        MyGrid.Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed
        MyGrid.Columns(2).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed
    End Sub

    Private Sub btnLevel2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_lvl2.Click
        bindData()
        MyGrid.Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
        MyGrid.Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
        MyGrid.Columns(2).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed
    End Sub

    Private Sub ResetSum()
        sumUnits = 0 : sumFGUnits = 0 : sumValue = 0 : sumFGValue = 0
    End Sub

    '**************************************************************************************************************
    '* For Printing
    '**************************************************************************************************************
    Sub btnPrint_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Print_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Autoprint Or PopupWindowStyle.NoToolbar)
        ResetSum()
        bindData()
    End Sub

    Sub btnPreview_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Preview_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Fullscreen)
        ResetSum()
        bindData()
    End Sub

    Private Function PrePrint() As printReportUtil
        bindData()

        Dim preview As New printReportUtil

        preview.PrintReportLink = "../util/printReport.aspx"
        preview.PageTitle = "Sales Area"
        preview.PageSize = 47
        preview.PageSizeLandscape = 30
        preview.DefaultOrientation = Orientation.Landscape

        preview.AddReportHeader(repdata)
        preview.AddWebGrid(Me.MyGrid)

        Return preview

    End Function
    '**************************************************************************************************************
    '* End Printing
    '**************************************************************************************************************

    Private Sub btn_lvl4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_lvl4.Click
        bindData()
        MyGrid.Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
        MyGrid.Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
        MyGrid.Columns(2).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
        MyGrid.Columns(3).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
    End Sub

    Private Sub groupby_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles groupby.SelectedIndexChanged

    End Sub
End Class
