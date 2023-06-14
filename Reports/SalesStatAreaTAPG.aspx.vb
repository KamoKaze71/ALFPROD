Imports Wyeth.Alf.WyethDropdown
Imports Wyeth.Utilities.DateHandling
Imports Wyeth.alf.exportToExcel
Imports Wyeth.Alf.CssStyles
Imports C1.Web.C1WebGrid
Imports Wyeth.Utilities.Helper
Imports Wyeth.Utilities.NumberFormat


Public Class SalesStatAreaTAPG
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents txtStartDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents txtEndDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents ddLine As System.Web.UI.WebControls.DropDownList
    Protected WithEvents groupBy As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnGenRep As System.Web.UI.WebControls.Button
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents btnLevel1 As System.Web.UI.WebControls.Button
    Protected WithEvents btnLevel2 As System.Web.UI.WebControls.Button
    Protected WithEvents btnLevel3 As System.Web.UI.WebControls.Button
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents ReportPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents repData As reportData
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
    Dim myReport As New Report
    Dim sumUnits, sumFGUnits, i As Double
    Dim sumValue, sumFGValue As Double
    Dim line_id As Integer

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        ResetSum()

        If Not Page.IsPostBack Then
            Me.lblPageTitle.Text = Request.QueryString("PageTitle")
            GetLineSelectDD(ddLine, Session("country_id"))
            fillGroupByDropdown()

            txtStartDate.Text = FirstOfThisMonth(repData.lastOrderDate).ToString(DATEFORMAT_STRING)
            txtEndDate.Text = LastOfThisMonth(repData.lastOrderDate).ToString(DATEFORMAT_STRING)


            bindData()

        End If

        i = 3
    End Sub

    Private Sub fillReportData()

        repData.lastOrderDate = txtEndDate.Text
        repData.addLine("Report-date from", txtStartDate.Text, True, False)
        repData.addLine("Report-date to", txtEndDate.Text, True, False)
        repData.addLine("Selected Line", ddLine.SelectedItem.Text.ToString, True, False)
        repData.addLine("Grouped by", groupBy.SelectedItem.ToString, True, False)
    End Sub

    'Wie befüllen unser "grouped by" dropdown. Der value sind gleichzeitig die ID's
    'der spalten nach denen gruppiert wird
    Private Sub fillGroupByDropdown()


        Dim it As ListItem = New ListItem
        it.Value = 2
        it.Text = "TPGroup & Rep. & Prod. & Cust."
        groupBy.Items.Add(it)


        Dim it3 As ListItem = New ListItem
        it3.Value = 1
        it3.Text = "TPGroup & Prod. & Rep. & Cust."
        groupBy.Items.Add(it3)

      

        Dim it2 As ListItem = New ListItem
        it2.Value = 3
        it2.Text = "TPGroup & Rep. & Cust. & Prod."
        groupBy.Items.Add(it2)

        Dim it4 As ListItem = New ListItem
        it4.Value = 4
        it4.Text = "Prod. & Cust. & Rep."
        groupBy.Items.Add(it4)

        groupBy.DataBind()
    End Sub

    Private Sub bindData()

        Dim MyDataView As New DataView
        myReport.StartDate = txtStartDate.Text
        myReport.EndDate = txtEndDate.Text
        myReport.DistID = 8
        myReport.LineID = ddLine.SelectedValue
        fillReportData()

        MyDataView = myReport.GetSalesAreaStatTAPG()

        With MyGrid
            .DataSource = MyDataView
            .GridLines = GridLines.None
            .ShowFooter = True
            .AllowSorting = False
            .AllowAutoSort = True

            'um richtig zu gruppieren müssen die columns zuerst gemoved werden.
            'fürs moven müssen wir die cols holen und removen und dann inserten
            Dim tapg_descriptionColumn As New C1BoundColumn
            tapg_descriptionColumn = .Columns.ColumnByName("tapg_description")
            .Columns.RemoveAt(.Columns.IndexOf(tapg_descriptionColumn))

            Dim customerColumn As New C1BoundColumn
            customerColumn = .Columns.ColumnByName("Customer_Name")
            .Columns.RemoveAt(.Columns.IndexOf(customerColumn))

            Dim sareColumn As New C1BoundColumn
            sareColumn = .Columns.ColumnByName("sare_name")
            .Columns.RemoveAt(.Columns.IndexOf(sareColumn))

            Dim prod_descriptionColumn As New C1BoundColumn
            prod_descriptionColumn = .Columns.ColumnByName("prod_description")
            .Columns.RemoveAt(.Columns.IndexOf(prod_descriptionColumn))

            'cols wieder an anderer stelle inserten
            If groupBy.SelectedValue = "1" Then
                .Columns.Insert(0, tapg_descriptionColumn)
                .Columns.Insert(1, prod_descriptionColumn)
                .Columns.Insert(2, sareColumn)
                .Columns.Insert(3, customerColumn)

                .Columns(0).Visible = False
                .Columns(1).Visible = False
                .Columns(2).Visible = False
                .Columns(3).Visible = True

                .Columns(3).GroupInfo.Position = GroupPositionEnum.None
                .Columns(3).GroupInfo.OutlineMode = OutlineModeEnum.None

            ElseIf groupBy.SelectedValue = "2" Then
                .Columns.Insert(0, tapg_descriptionColumn)
                .Columns.Insert(1, sareColumn)
                .Columns.Insert(2, prod_descriptionColumn)
                .Columns.Insert(3, customerColumn)

                .Columns(0).Visible = False
                .Columns(1).Visible = False
                .Columns(2).Visible = False
                .Columns(3).Visible = True

                .Columns(3).GroupInfo.Position = GroupPositionEnum.None
                .Columns(3).GroupInfo.OutlineMode = OutlineModeEnum.None

            ElseIf groupBy.SelectedValue = "3" Then
                .Columns.Insert(0, tapg_descriptionColumn)
                .Columns.Insert(1, sareColumn)
                .Columns.Insert(2, customerColumn)
                .Columns.Insert(3, prod_descriptionColumn)

                .Columns(0).Visible = False
                .Columns(1).Visible = False
                .Columns(2).Visible = False
                .Columns(3).Visible = True

                .Columns(3).GroupInfo.Position = GroupPositionEnum.None
                .Columns(3).GroupInfo.OutlineMode = OutlineModeEnum.None

            ElseIf groupBy.SelectedValue = "4" Then
                .Columns.Insert(0, prod_descriptionColumn)
                .Columns.Insert(1, customerColumn)
                .Columns.Insert(2, sareColumn)


                .Columns(0).Visible = False
                .Columns(1).Visible = False
                .Columns(2).Visible = True

                .Columns(0).GroupInfo.Position = GroupPositionEnum.Header
                .Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed

                .Columns(1).GroupInfo.Position = GroupPositionEnum.Header
                .Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed

                .Columns(2).GroupInfo.Position = GroupPositionEnum.None
                .Columns(2).GroupInfo.OutlineMode = OutlineModeEnum.None

                i = 2
            End If




            .Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed
            .Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed
            .Columns(2).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed


            '.Columns(1).GroupInfo.Position = GroupPositionEnum.Header
            '.Columns(1).GroupInfo.HeaderText = "Total {0}"
            ''.Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded

            '.Columns(2).GroupInfo.Position = GroupPositionEnum.Header
            '.Columns(2).GroupInfo.HeaderText = "Total {0}"
            ''.Columns(2).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded


            ''.Columns(3).GroupInfo.Position = GroupPositionEnum.None
            ''.Columns(3).GroupInfo.HeaderText = "Total {0}"
            ''.Columns(3).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded

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
        setColumnToolTips(e.Item, MyGrid, 3)

        If e.Item.ItemType = C1ListItemType.GroupFooter Or e.Item.ItemType = C1ListItemType.GroupHeader Then
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_VALUE")) - i), 2)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_FGVALUE")) - i), 2)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_UNITS")) - i), 2)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_FGUNITS")) - i), 2)
        End If

        'If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Or e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then
        '    e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
        '    e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor2Class';")
        'End If

        If e.Item.ItemType = C1ListItemType.Footer Then
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Units"))).Text = MyNumberFormat(sumUnits, 2)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("FGUnits"))).Text = MyNumberFormat(sumFGUnits, 2)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Value"))).Text = MyNumberFormat(sumValue, 2)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("FGValue"))).Text = MyNumberFormat(sumFGValue, 2)

            e.Item.CssClass = "reportTotal"
            e.Item.Cells(2).Text = "TOTAL AUSTRIA"
            e.Item.Cells(2).HorizontalAlign = HorizontalAlign.Left



        End If
    End Sub

    Private Sub btnGenRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRep.Click
        'MyGrid.Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
        'MyGrid.Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
        'MyGrid.Columns(2).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
        Page.Validate()
        If Page.IsValid = True Then

            bindData()
        Else
            MyGrid.Visible = False
        End If


    End Sub

    Private Sub Item_DataBound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound

        If e.Item.ItemType = C1ListItemType.AlternatingItem Or e.Item.ItemType = C1ListItemType.Item Then

            sumUnits = sumUnits + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Units"))).Text
            sumFGUnits = sumFGUnits + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("FGUnits"))).Text
            sumValue = sumValue + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Value"))).Text
            sumFGValue = sumFGValue + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("FGValue"))).Text


            '  Dim JS_AreaDetail, prod, cust_no As String
            ' Dim sare_id, cust_name As String
            'sare_id = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sare_id"))).Text

            'prod = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("tapg_description"))).Text
            'cust_no = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("CUDI_CUSTOMER_NR"))).Text
            'ust_name = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("CUSTOMER_NAME"))).Text

            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_VALUE"))), 2)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_FGVALUE"))), 2)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_UNITS"))), 2)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_FGUNITS"))), 2)

            ' JS_AreaDetail = "javascript:OpenPopUp('SalesStatAreaDetail.aspx?sare_id=" & sare_id & "&sare_name=" & e.Item.Cells(0).Text & "&customer_name=" & cust_name & "&cust_no=" & cust_no & "&prod_desc=" & prod & "&sd=" & myReport.StartDate & "&ed=" & myReport.EndDate & "&line_id=" & myReport.LineID & "&pagetitle=Sales Stat Area Detail" & "  ','AreaSales');"

            ' e.Item.Attributes.Add("onclick", JS_AreaDetail)

        End If
    End Sub


    Private Sub ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportExcel.Click
        bindData()

        MyGrid.Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.None
        MyGrid.Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.None
        MyGrid.Columns(2).GroupInfo.OutlineMode = OutlineModeEnum.None

        Dim exp As exportToExcel = New exportToExcel(Page)
        exp.title = lblPageTitle.Text
        exp.showReportData = repData
        exp.addDataGrid(MyGrid)
        exp.export()
    End Sub

    Private Sub btnLevel3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLevel3.Click
        'Dim col As New C1Column
        'col = MyGrid.Columns.ColumnByName("Customer Name")
        'MyGrid.Columns(MyGrid.Columns.IndexOf(col)).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded


        'Dim col2 As New C1Column
        'col2 = MyGrid.Columns.ColumnByName("SARE_NAME")
        'MyGrid.Columns(MyGrid.Columns.IndexOf(col2)).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
        bindData()
        MyGrid.Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
        MyGrid.Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
        MyGrid.Columns(2).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded

    End Sub

    Private Sub btnLevel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLevel1.Click
        'Dim col As New C1Column
        'col = MyGrid.Columns.ColumnByName("Customer Name")
        'MyGrid.Columns(MyGrid.Columns.IndexOf(col)).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed

        bindData()
        MyGrid.Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed
        MyGrid.Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed
        MyGrid.Columns(2).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed


    End Sub

    Private Sub btnLevel2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLevel2.Click
        'Dim col As New C1Column
        'col = MyGrid.Columns.ColumnByName("SARE_NAME")
        'MyGrid.Columns(MyGrid.Columns.IndexOf(col)).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded

        'Dim col2 As New C1Column
        'col2 = MyGrid.Columns.ColumnByName("Customer Name")
        'MyGrid.Columns(MyGrid.Columns.IndexOf(col)).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed
        bindData()
        MyGrid.Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
        MyGrid.Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed
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

        preview.AddReportHeader(repData)
        preview.AddWebGrid(Me.MyGrid)

        Return preview

    End Function
    '**************************************************************************************************************
    '* End Printing
    '**************************************************************************************************************
End Class
