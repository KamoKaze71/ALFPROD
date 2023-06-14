Imports Wyeth.Alf.CssStyles
Imports Wyeth.Utilities.NumberFormat
Imports Wyeth.Utilities.DateHandling
Imports C1.Web.C1WebGrid
Imports Wyeth.Alf.WyethDropdown



Public Class AMS_SalesStat
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddLine As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnGenRep As System.Web.UI.WebControls.Button
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents btnLevel1 As System.Web.UI.WebControls.Button
    Protected WithEvents btnLevel2 As System.Web.UI.WebControls.Button
    Protected WithEvents btnLevel3 As System.Web.UI.WebControls.Button
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents repData As Wyeth.Alf.reportData
    Protected WithEvents groupByDD As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btn_lvl1 As System.Web.UI.WebControls.Button
    Protected WithEvents btn_lvl2 As System.Web.UI.WebControls.Button
    Protected WithEvents btn_lvl3 As System.Web.UI.WebControls.Button
    Protected WithEvents ddstartDate As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddEndDate As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnPrint As System.Web.UI.WebControls.Button
    Protected WithEvents prtControl As printReportCtl
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim myamsreport As New AMS
    Dim startDate, endDate As String
    Dim sumUnits, sumFGUnits, sumValue, sumFGValue As Double
    Dim grplvl As Integer = 5



    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        If Page.IsPostBack = True Then
            ResetSum()
        Else
            GetMonthSelectDDAMS(ddstartDate)
            GetMonthSelectDDAMS(ddEndDate)

            SetGridStylesGroup(MyGrid)
            fillGroupByDropdown()
            filllinedd()


            BindData()
        End If
    End Sub
 
    Private Sub filllinedd()
        Dim li As New ListItem
        li.Value = "WYL"
        li.Text = "Actuals"
        ddLine.Items.Insert(0, li)
        Dim li2 As New ListItem
        li2.Value = "MWL"
        li2.Text = "Samples"
        ddLine.Items.Insert(1, li2)
    End Sub

    Private Sub fillGroupByDropdown()

        Dim it3 As ListItem = New ListItem
        it3.Value = 53
        it3.Text = "Prod. & Present. & Cust."
        groupByDD.Items.Add(it3)

        Dim it As ListItem = New ListItem
        it.Value = 25
        it.Text = "Prod. & Cust. & Present."
        groupByDD.Items.Add(it)

        Dim it2 As ListItem = New ListItem
        it2.Value = 52
        it2.Text = "Cust. & Prod. & Present."
        groupByDD.Items.Add(it2)



        groupByDD.DataBind()
    End Sub



    Private Sub BindData()

        With MyGrid

            MyGrid.DataSource = myamsreport.GetAMSSalesStat(ddstartDate.SelectedValue, ddEndDate.SelectedValue, ddLine.SelectedValue)


            Dim descriptionColumn As New C1BoundColumn
            descriptionColumn = .Columns.ColumnByName("kurzbez")
            .Columns.RemoveAt(.Columns.IndexOf(descriptionColumn))

            Dim customerColumn As New C1BoundColumn
            customerColumn = .Columns.ColumnByName("NAME")
            .Columns.RemoveAt(.Columns.IndexOf(customerColumn))

            Dim presentationColumn As New C1BoundColumn
            presentationColumn = .Columns.ColumnByName("bez")
            .Columns.RemoveAt(.Columns.IndexOf(presentationColumn))

            'cols wieder an anderer stelle inserten
            If groupByDD.SelectedValue = "25" Then
                .Columns.Insert(0, descriptionColumn)
                .Columns.Insert(1, customerColumn)
                .Columns.Insert(2, presentationColumn)

                .Columns(0).SortDirection = C1SortDirection.Ascending
                .Columns(1).SortDirection = C1SortDirection.Ascending
                .Columns(2).SortDirection = C1SortDirection.Ascending
                .Columns(3).SortDirection = C1SortDirection.Ascending

            ElseIf groupByDD.SelectedValue = "52" Then

                Dim phznrColumn As New C1BoundColumn
                phznrColumn = .Columns.ColumnByName("phznr")
                .Columns.RemoveAt(.Columns.IndexOf(phznrColumn))

                Dim kdnrColumn As New C1BoundColumn
                kdnrColumn = .Columns.ColumnByName("kdnr")
                .Columns.RemoveAt(.Columns.IndexOf(kdnrColumn))


                .Columns.Insert(0, customerColumn)
                .Columns.Insert(1, descriptionColumn)
                .Columns.Insert(2, presentationColumn)
                .Columns.Insert(3, kdnrColumn)
                .Columns.Insert(4, phznrColumn)

                .Columns(0).SortDirection = C1SortDirection.Ascending
                .Columns(1).SortDirection = C1SortDirection.Ascending
                .Columns(2).SortDirection = C1SortDirection.Ascending
                .Columns(3).SortDirection = C1SortDirection.Ascending
                .Columns(4).SortDirection = C1SortDirection.Ascending


            ElseIf groupByDD.SelectedValue = "53" Then
                .Columns.Insert(0, descriptionColumn)
                .Columns.Insert(1, presentationColumn)
                .Columns.Insert(2, customerColumn)

                .Columns(0).SortDirection = C1SortDirection.Ascending
                .Columns(1).SortDirection = C1SortDirection.Ascending
                .Columns(2).SortDirection = C1SortDirection.Ascending
                .Columns(3).SortDirection = C1SortDirection.Ascending

            End If


            .Columns(0).Visible = False
            .Columns(1).Visible = False
            .Columns(2).Visible = True


            .Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed
            .Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed
            .Columns(2).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed


            .Columns(1).GroupInfo.Position = GroupPositionEnum.Header
            .Columns(1).GroupInfo.HeaderText = "Total {0}"
            .Columns(0).GroupInfo.Position = GroupPositionEnum.Header
            .Columns(0).GroupInfo.HeaderText = "Total {0}"
            .Columns(2).GroupInfo.Position = GroupPositionEnum.None


            SetGridStylesGroup(MyGrid)
            .AllowAutoSort = True
            .AllowSorting = False
            .AlternatingItemStyle.CssClass = "tableBGColor2Class"
            .ItemStyle.CssClass = "tableBGColor2Class"
            .DataBind()
        End With



    End Sub

    Private Sub btnGenRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRep.Click
        Page.Validate()
        If Page.IsValid = True Then
            BindData()
        End If
    End Sub
    Private Sub Item_created(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated
        setColumnToolTips(e.Item, MyGrid)

        'If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Or e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then
        '    e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
        '    e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor2Class';")
        'End If

        If e.Item.ItemType = C1ListItemType.GroupHeader Then
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("VALUE")) - grplvl), 2)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("UNITS")) - grplvl), 0)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("FGUNITS")) - grplvl), 0)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("FGVALUE")) - grplvl), 2)
        End If


        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Footer Then
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Units"))).Text = MyNumberFormat(sumUnits, 0)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("FGUnits"))).Text = MyNumberFormat(sumFGUnits, 0)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Value"))).Text = MyNumberFormat(sumValue, 2)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("FGValue"))).Text = MyNumberFormat(sumFGValue, 2)
            e.Item.CssClass = "reportTotal"
            e.Item.Cells(0).Text = "TOTAL AUSTRIA"
            e.Item.Cells(0).HorizontalAlign = HorizontalAlign.Left
        End If

    End Sub



    Private Sub Item_DataBound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound
        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Or e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Then

            sumUnits = sumUnits + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Units"))).Text
            sumFGUnits = sumFGUnits + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("FGUnits"))).Text
            sumValue = sumValue + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Value"))).Text
            sumFGValue = sumFGValue + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("FGValue"))).Text

            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("VALUE"))), 2)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("UNITS"))), 0)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("FGUNITS"))), 0)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("FGVALUE"))), 2)

        End If
    End Sub
    Private Sub btnLevel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_lvl1.Click
        BindData()
        MyGrid.Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed
        MyGrid.Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed

    End Sub

    Private Sub btnLevel2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_lvl2.Click
        BindData()
        MyGrid.Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
        MyGrid.Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed

    End Sub


    Private Sub btn_lvl3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_lvl3.Click
        BindData()
        MyGrid.Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
        MyGrid.Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded

    End Sub

    Private Sub ResetSum()
        sumUnits = 0 : sumFGUnits = 0 : sumValue = 0 : sumFGValue = 0
    End Sub



    Private Sub ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportExcel.Click
        Page.Validate()

        If Page.IsValid Then
            BindData()


            Dim exp As New exportToExcel(Page)

            exp.title = Me.ALFPageTitle
            exp.formatColumnAsString(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("phznr")))
            exp.formatColumnAsString(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("kdnr")))
            exp.formatColumnAsString(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("cod_kdkenn")))
            exp.addLine("first historic order entry:</STRONG>&nbsp; 1995-12-01 <br>")
            exp.addLine("last historic order entry:</STRONG> &nbsp; 2002-12-31 <br><br><br>")

            exp.addLine("Report Date from: " & ddstartDate.SelectedValue.ToString & " to: " & ddEndDate.SelectedValue.ToString & "<bR>")
            exp.addLine("Print Date: " & FormatDate(Today()))
            exp.addDataGrid(MyGrid)
            exp.export()
        End If
    End Sub
    '**************************************************************************************************************
    '* For Printing
    '**************************************************************************************************************
    Sub btnPrint_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Print_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Autoprint Or PopupWindowStyle.NoToolbar)
        ResetSum()
        BindData()
    End Sub

    Sub btnPreview_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Preview_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Fullscreen)
        ResetSum()
        BindData()
    End Sub

    Private Function PrePrint() As printReportUtil
        BindData()
     
        Dim reportHeaderString As String = "first historic order entry:</STRONG>&nbsp; 1995-12-01 <br>last historic order entry:</STRONG> &nbsp; 2002-12-31 <br><br><br> Report Date from: " & ddstartDate.SelectedValue.ToString & " to: " & ddEndDate.SelectedValue.ToString & "<bR>Print Date: " & FormatDate(Today())



        Dim preview As New printReportUtil

        preview.PageTitle = Me.ALFPageTitle
        preview.PageSize = 46
        preview.AddWebGrid(reportHeaderString, Me.MyGrid)

        Return preview

    End Function
    '**************************************************************************************************************
    '* End Printing
    '**************************************************************************************************************

End Class
