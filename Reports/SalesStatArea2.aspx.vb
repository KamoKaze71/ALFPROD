Imports Wyeth.Alf.WyethDropdown
Imports Wyeth.Utilities.DateHandling
Imports Wyeth.alf.exportToExcel
Imports Wyeth.Alf.CssStyles
Imports C1.Web.C1WebGrid
Imports Wyeth.Utilities.Helper
Imports Wyeth.Utilities.NumberFormat



Public Class SalesStatArea2
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

    Dim myReport As New Report
    Dim sumUnits, sumFGUnits As Integer
    Dim sumValue, sumFGValue As Double
    Dim line_id As Integer


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Page.IsPostBack Then


        Else
            Me.lblPageTitle.Text = Request.QueryString("PageTitle")
            txtStartDate.Text = FirstOfThisMonth(Today()).ToString(Wyeth.Utilities.Helper.DATEFORMAT_STRING_REPORT, CType(Application("MyDTFI"), IFormatProvider))
            txtEndDate.Text = LastOfThisMonth(Today()).ToString(Wyeth.Utilities.Helper.DATEFORMAT_STRING_REPORT, CType(Application("MyDTFI"), IFormatProvider))
            GetLineSelectDD(ddLine, Session("country_id"))
            fillGroupByDropdown()
            bindData()
        End If

        fillReportData()
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
        it.Value = 25
        it.Text = "Rep& Product & Customer"
        groupBy.Items.Add(it)

        Dim it2 As ListItem = New ListItem
        it2.Value = 52
        it2.Text = "Rep & Customer & Product"
        groupBy.Items.Add(it2)

        groupBy.DataBind()
    End Sub

    Private Sub bindData()

        Page.Validate()
        If Page.IsValid Then
            Dim MyDataView As New DataView
            myReport.StartDate = txtStartDate.Text
            myReport.EndDate = txtEndDate.Text
            myReport.DistID = 8
            myReport.LineID = ddLine.SelectedValue

            SetGridStylesGroup(MyGrid)
            MyDataView = myReport.GetSalesAreaStat()

            With MyGrid
                .DataSource = MyDataView
                .GridLines = GridLines.None
                .ShowFooter = True
                .AllowSorting = False
                .AllowAutoSort = True

                'um richtig zu gruppieren müssen die columns zuerst gemoved werden.
                'fürs moven müssen wir die cols holen und removen und dann inserten
                Dim descriptionColumn As New C1BoundColumn
                descriptionColumn = .Columns.ColumnByName("Description")
                .Columns.RemoveAt(.Columns.IndexOf(descriptionColumn))

                Dim customerColumn As New C1BoundColumn
                customerColumn = .Columns.ColumnByName("Customer_Name")
                .Columns.RemoveAt(.Columns.IndexOf(customerColumn))

                'Dim sareColumn As New C1BoundColumn
                'sareColumn = .Columns.ColumnByName("sare_name")
                '.Columns.RemoveAt(.Columns.IndexOf(sareColumn))

                'cols wieder an anderer stelle inserten
                If groupBy.SelectedValue = "25" Then
                    '.Columns.Insert(0, sareColumn)
                    .Columns.Insert(1, descriptionColumn)
                    .Columns.Insert(2, customerColumn)
                Else
                    '.Columns.Insert(0, sareColumn)
                    .Columns.Insert(1, customerColumn)
                    .Columns.Insert(2, descriptionColumn)
                End If


                .Columns(0).Visible = False
                .Columns(1).Visible = False
                .Columns(2).Visible = True

                .Columns(0).GroupInfo.Position = GroupPositionEnum.Header
                .Columns(0).GroupInfo.HeaderText = "Total {0}"
                '.Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded

                .Columns(1).GroupInfo.Position = GroupPositionEnum.Header
                .Columns(1).GroupInfo.HeaderText = "Total {0}"
                '.Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded

                .Columns(2).GroupInfo.Position = GroupPositionEnum.None


                .Columns(0).SortDirection = C1SortDirection.Ascending
                .Columns(1).SortDirection = C1SortDirection.Ascending
                .Columns(2).SortDirection = C1SortDirection.Ascending

                .DataBind()
            End With
        End If

    End Sub

    Private Sub C1WebGrid1_SortCommand(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1SortCommandEventArgs) Handles MyGrid.SortCommand
        bindData()
    End Sub 'C1WebGrid1_SortCommand
    Private Sub Item_created(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated
        setColumnToolTips(e.Item, MyGrid)

        If e.Item.ItemType = C1ListItemType.GroupFooter Or e.Item.ItemType = C1ListItemType.GroupHeader Then
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_VALUE")) - 2), 2)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_FGVALUE")) - 2), 2)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_UNITS")) - 2), 0)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_FGUNITS")) - 2), 0)
        End If

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Or e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor2Class';")
        End If

        If e.Item.ItemType = C1ListItemType.Footer Then
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Units"))).Text = MyNumberFormat(sumUnits, 0)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("FGUnits"))).Text = MyNumberFormat(sumFGUnits, 0)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Value"))).Text = MyNumberFormat(sumValue, 2)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("FGValue"))).Text = MyNumberFormat(sumFGValue, 2)

            e.Item.CssClass = "reportTotal"
            e.Item.Cells(2).Text = "TOTAL AUSTRIA"
            e.Item.Cells(2).HorizontalAlign = HorizontalAlign.Left
           


        End If
    End Sub

    Private Sub btnGenRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRep.Click
        MyGrid.Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
        MyGrid.Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
        MyGrid.Columns(2).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded

        bindData()
    End Sub

    Private Sub Item_DataBound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound

        If e.Item.ItemType = C1ListItemType.AlternatingItem Or e.Item.ItemType = C1ListItemType.Item Then

            sumUnits = sumUnits + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Units"))).Text
            sumFGUnits = sumFGUnits + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("FGUnits"))).Text
            sumValue = sumValue + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Value"))).Text
            sumFGValue = sumFGValue + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("FGValue"))).Text


            Dim JS_AreaDetail, prod, cust_no As String
            Dim sare_id, cust_name As String
            sare_id = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sare_id"))).Text

            prod = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("prod_description"))).Text
            cust_no = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("CUDI_CUSTOMER_NR"))).Text
            cust_name = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("CUSTOMER_NAME"))).Text

            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_VALUE"))), 2)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_FGVALUE"))), 2)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_UNITS"))), 0)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_FGUNITS"))), 0)

            JS_AreaDetail = "javascript:OpenPopUp('SalesStatAreaDetail.aspx?sare_id=" & sare_id & "&sare_name=" & e.Item.Cells(0).Text & "&customer_name=" & cust_name & "&cust_no=" & cust_no & "&prod_desc=" & prod & "&sd=" & myReport.StartDate & "&ed=" & myReport.EndDate & "&line_id=" & myReport.LineID & "&pagetitle=Sales Stat Area Detail" & "  ','AreaSales');"

            e.Item.Attributes.Add("onclick", JS_AreaDetail)

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
        MyGrid.Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
        MyGrid.Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
        MyGrid.Columns(2).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
        bindData()
    End Sub

    Private Sub btnLevel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLevel1.Click
        'Dim col As New C1Column
        'col = MyGrid.Columns.ColumnByName("Customer Name")
        'MyGrid.Columns(MyGrid.Columns.IndexOf(col)).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed


        MyGrid.Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed
        MyGrid.Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed
        MyGrid.Columns(2).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed

        bindData()
    End Sub

    Private Sub btnLevel2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLevel2.Click
        'Dim col As New C1Column
        'col = MyGrid.Columns.ColumnByName("SARE_NAME")
        'MyGrid.Columns(MyGrid.Columns.IndexOf(col)).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded

        'Dim col2 As New C1Column
        'col2 = MyGrid.Columns.ColumnByName("Customer Name")
        'MyGrid.Columns(MyGrid.Columns.IndexOf(col)).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed
        MyGrid.Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
        MyGrid.Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed
        MyGrid.Columns(2).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed
        bindData()
    End Sub

    '**************************************************************************************************************
    '* For Printing
    '**************************************************************************************************************
    Sub btnPrint_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Print_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Autoprint Or PopupWindowStyle.NoToolbar)
    End Sub

    Sub btnPreview_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Preview_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Fullscreen)
    End Sub

    Private Function PrePrint() As printReportUtil
        BindData()

        Dim preview As New printReportUtil

        preview.PageTitle = "Area Sales Statistics"
        preview.PageSizeLandscape = 31
        preview.DefaultOrientation = Orientation.Landscape

        preview.AddReportHeader(repData)
        preview.AddWebGrid(Me.MyGrid)

        Return preview

    End Function
    '**************************************************************************************************************
    '* End Printing
    '**************************************************************************************************************
End Class
