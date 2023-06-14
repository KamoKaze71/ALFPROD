Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing

Imports System.Web
Imports System.Web.SessionState
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls

Imports C1.Web.C1WebGrid
Imports Microsoft.Win32

Imports Wyeth.Utilities.DateHandling
Imports Wyeth.Alf.CssStyles
Imports Wyeth.Alf.WyethDropdown
Imports Wyeth.Utilities.Helper
Imports Wyeth.Utilities.NumberFormat

Public Class SalesStatCustomer3
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
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents ReportPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents repData As reportData
    Protected WithEvents btn_lvl3 As System.Web.UI.WebControls.Button
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
    Dim sumValue, sumFGVAlue As Double
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

        Dim it3 As ListItem = New ListItem
        it3.Value = 53
        it3.Text = "Prod. & Present. & Cust."
        groupBy.Items.Add(it3)

        Dim it As ListItem = New ListItem
        it.Value = 25
        it.Text = "Prod. & Cust. & Present."
        groupBy.Items.Add(it)

        Dim it2 As ListItem = New ListItem
        it2.Value = 52
        it2.Text = "Cust. & Prod. & Present."
        groupBy.Items.Add(it2)

      

        groupBy.DataBind()
    End Sub

    Private Sub bindData()
        Dim MyDataView As New DataView
        myReport.StartDate = txtStartDate.Text
        myReport.EndDate = txtEndDate.Text
        myReport.LineID = ddLine.SelectedValue
        myReport.CtryID = Session("country_id")
        fillReportData()
        MyDataView = myReport.GetSales()

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
            customerColumn = .Columns.ColumnByName("Customer Name")
            .Columns.RemoveAt(.Columns.IndexOf(customerColumn))

            Dim presentationColumn As New C1BoundColumn
            presentationColumn = .Columns.ColumnByName("presentation")
            .Columns.RemoveAt(.Columns.IndexOf(presentationColumn))

            'cols wieder an anderer stelle inserten
            If groupBy.SelectedValue = "25" Then
                .Columns.Insert(0, descriptionColumn)
                .Columns.Insert(1, customerColumn)
                .Columns.Insert(2, presentationColumn)

            ElseIf groupBy.SelectedValue = "52" Then
                .Columns.Insert(0, customerColumn)
                .Columns.Insert(1, descriptionColumn)
                .Columns.Insert(2, presentationColumn)
              
            ElseIf groupBy.SelectedValue = "53" Then
                .Columns.Insert(0, descriptionColumn)
                .Columns.Insert(1, presentationColumn)
                .Columns.Insert(2, customerColumn)

            End If


            .Columns(0).Visible = False
            .Columns(1).Visible = False
            .Columns(2).Visible = True

            .Columns(1).GroupInfo.Position = GroupPositionEnum.Header
            .Columns(1).GroupInfo.HeaderText = "Total {0}"
            .Columns(0).GroupInfo.Position = GroupPositionEnum.Header
            .Columns(0).GroupInfo.HeaderText = "Total {0}"
            .Columns(2).GroupInfo.Position = GroupPositionEnum.None


            .Columns(0).SortDirection = C1SortDirection.Ascending
            .Columns(1).SortDirection = C1SortDirection.Ascending
            .Columns(2).SortDirection = C1SortDirection.Ascending
            .Columns(3).SortDirection = C1SortDirection.Ascending

            SetGridStylesGroup(MyGrid)
            .EnableViewState = True
            .AlternatingItemStyle.CssClass = "tableBGColor2Class"
            .ItemStyle.CssClass = "tableBGColor2Class"
            .DataBind()
        End With


    End Sub


    Private Sub Item_created(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated
        setColumnToolTips(e.Item, MyGrid, 2)

        If e.Item.ItemType = C1ListItemType.GroupFooter Or e.Item.ItemType = C1ListItemType.GroupHeader Then
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("ORPO_VALUE")) - 2), 2)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("ORPO_FGVALUE")) - 2), 2)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("ORPO_UNITS")) - 2), 0)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("ORPO_FGUNITS")) - 2), 0)
        End If

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Or e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor2Class';")
        End If

        If e.Item.ItemType = C1ListItemType.Footer Then
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Units"))).Text = MyNumberFormat(sumUnits, 0)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("FGUnits"))).Text = MyNumberFormat(sumFGUnits, 0)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Value"))).Text = MyNumberFormat(sumValue, 2)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("FGValue"))).Text = MyNumberFormat(sumFGVAlue, 2)

            e.Item.CssClass = "reportTotal"
            e.Item.Cells(1).Text = "TOTAL AUSTRIA"
            e.Item.Cells(1).HorizontalAlign = HorizontalAlign.Left
        End If

    End Sub

    Private Sub btnGenRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRep.Click

        Page.Validate()

        If Page.IsValid Then
            MyGrid.Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed
            MyGrid.Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed
            bindData()
        Else
            MyGrid.Visible = False
        End If

    End Sub

    Private Sub Item_DataBound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound
        If e.Item.ItemType = C1ListItemType.AlternatingItem Or e.Item.ItemType = C1ListItemType.Item Then

            Dim JS_AreaDetail, prod, cust_no, prod_id As String
            Dim cust_name As String


            sumUnits = sumUnits + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Units"))).Text
            sumFGUnits = sumFGUnits + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("FGUnits"))).Text
            sumValue = sumValue + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Value"))).Text
            sumFGVAlue = sumFGVAlue + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("FGValue"))).Text


            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("ORPO_VALUE"))), 2)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("ORPO_FGVALUE"))), 2)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("ORPO_UNITS"))), 0)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("ORPO_FGUNITS"))), 0)



            prod_id = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("prod_id"))).Text
            prod = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("prod_description"))).Text
            cust_no = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("CUDI_CUSTOMER_NR"))).Text
            cust_name = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("CUST_NAME"))).Text


            JS_AreaDetail = "javascript:OpenPopUp('SalesStatAreaDetail.aspx?customer_name=" & cust_name & "&cust_no=" & cust_no & "&prod_desc=" & prod & "&prod_id=" & prod_id & "&sd=" & myReport.StartDate & "&ed=" & myReport.EndDate & "&line_id=" & myReport.LineID & "&pagetitle=Detail Sales Statistics" & "  ','DetailSalesStatistics');"

            e.Item.Attributes.Add("onclick", JS_AreaDetail)


        End If
    End Sub
    Private Sub ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportExcel.Click
        bindData()
        Dim exp As exportToExcel = New exportToExcel(Page)
        exp.title = lblPageTitle.Text
        exp.showReportData = repData
        exp.addDataGrid(MyGrid)
        exp.export()
    End Sub

    Private Sub btnLevel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLevel1.Click
        'Dim col As New C1Column
        'Dim myMode As Integer
        'col = MyGrid.Columns.ColumnByName("Customer Name")
        'MyGrid.Columns(MyGrid.Columns.IndexOf(col)).GroupInfo.OutlineMode = myMode
        bindData()
        MyGrid.Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed
        MyGrid.Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed

    End Sub

    Private Sub btnLevel2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLevel2.Click
        'Dim col As New C1Column
        'Dim myMode As Integer
        'col = MyGrid.Columns.ColumnByName("Description")
        'MyGrid.Columns(MyGrid.Columns.IndexOf(col)).GroupInfo.OutlineMode = myMode
        bindData()
        MyGrid.Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
        MyGrid.Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed

    End Sub


    Private Sub btn_lvl3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_lvl3.Click
        bindData()
        MyGrid.Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
        MyGrid.Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded

    End Sub

    Private Sub ResetSum()
        sumUnits = 0 : sumFGUnits = 0 : sumValue = 0 : sumFGVAlue = 0
    End Sub

    '**************************************************************************************************************
    '* For Printing
    '**************************************************************************************************************
    Private Sub btnPrint_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Print_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Autoprint Or PopupWindowStyle.NoToolbar)
        ResetSum()
        bindData()
    End Sub

    Private Sub btnPreview_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Preview_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Fullscreen)
        ResetSum()
        bindData()
    End Sub

    Private Function PrePrint() As printReportUtil
        bindData()

        Dim preview As New printReportUtil

        preview.PageTitle = "Sales Statistics (new)"
        preview.PageSizeLandscape = 31
        preview.DefaultOrientation = Orientation.Landscape

        preview.AddReportHeader(Me.repData)
        preview.AddWebGrid(Me.MyGrid)

        Return preview

    End Function
    '**************************************************************************************************************
    '* End Printing
    '**************************************************************************************************************

End Class