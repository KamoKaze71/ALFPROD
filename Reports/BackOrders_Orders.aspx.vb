Imports Wyeth.Alf.CssStyles
Imports Wyeth.Alf.WyethDropdown
Imports Wyeth.Utilities.NumberFormat
Imports Wyeth.Utilities.DateHandling
Imports Wyeth.Utilities.Helper


Imports C1.Web.C1WebGrid

    Public Class BackOrders_Orders


        Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
        Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
        Protected WithEvents txtStartDate As Wyeth.Utilities.WyethTextBox
        Protected WithEvents StartImage As System.Web.UI.WebControls.Image
        Protected WithEvents ddproduct As System.Web.UI.WebControls.DropDownList
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
    Dim Orders_uncomplete, Orders_ordered As Double
    Dim grpLvl As Integer = 2

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If Page.IsPostBack Then
            Else

                'lists all available Distributors
                GetDistribSelectDD(ddDistribselect, Session("country_id"))
                'Lists all available Lines

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


        MyGrid.DataSource = MyReport.GetBackOrdersOrders(ddDistribselect.SelectedValue, txtStartDate.Text, txtEndDate.Text)
            MyGrid.ShowFooter = True
            MyGrid.DataBind()
        End Sub

   

    Private Sub ddDistribSelect_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddDistribselect.SelectedIndexChanged

        bindData()
    End Sub

    Private Sub MyGrid_ItemDataBound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound

        Orders_uncomplete = Orders_uncomplete + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Orders_uncomplete"))).Text
        Orders_ordered = Orders_ordered + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Orders_ordered"))).Text

        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Orders_uncomplete"))), 0)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Orders_ordered"))), 0)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("percentage_complete"))), 1)

    End Sub

    Private Sub MyGrid_ItemCreated(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated
     
        Dim percentage_complete As Double
        Dim tmpOrders_uncomplete, tmpOrders_ordered As Integer

        If e.Item.ItemType = C1ListItemType.GroupHeader Then
            Dim headerstring As String() = {"Date", "Orders ordered", "Orders incomplete", "%", "", "", "", ""}
            setColumnToolTips(e.Item, MyGrid, 0, headerstring, True)

            tmpOrders_ordered = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Orders_ordered")) - grpLvl).Text()
            tmpOrders_uncomplete = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Orders_uncomplete")) - grpLvl).Text()
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("percentage_complete")) - grpLvl).Text = MyNumberFormat((((tmpOrders_ordered - tmpOrders_uncomplete) / tmpOrders_ordered) * 100), 1)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Orders_uncomplete")) - grpLvl), 0)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Orders_ordered")) - grpLvl), 0)

        ElseIf e.Item.ItemType = C1ListItemType.Footer Then

            Dim headerstring As String() = {"Date", "Customer", "Order No.", "Orders ordered", "Orders incomplete", "%", "", "", "", ""}
            setColumnToolTips(e.Item, MyGrid, 0, headerstring, True)


            e.Item.CssClass = "reportTotal"
            e.Item.Cells(1).Text = "TOTAL"
            e.Item.Cells(1).HorizontalAlign = HorizontalAlign.Left

            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Orders_uncomplete"))).Text = MyNumberFormat(Orders_uncomplete, 0)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Orders_ordered"))).Text = MyNumberFormat(Orders_ordered, 0)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("percentage_complete"))).Text = MyNumberFormat((((Orders_ordered - Orders_uncomplete) / Orders_ordered) * 100), 1)

        ElseIf e.Item.ItemType = C1ListItemType.Item Or e.Item.ItemType = C1ListItemType.AlternatingItem Then

            Dim headerstring As String() = {"Date", "Customer", "Order No.", "Orders ordered", "Orders incomplete", "%", "", "", "", ""}
            setColumnToolTips(e.Item, MyGrid, 0, headerstring, True)

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
        exp.formatColumnAsString(2)
        exp.addLine("Date from:" & txtStartDate.Text)
        exp.addLine("Date to:" & txtEndDate.Text)
        exp.title = Me.ALFPageTitle
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

End Class

