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


Public Class SalesFlatFile
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents txtStartDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents txtEndDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents ddLine As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnGenRep As System.Web.UI.WebControls.Button
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents ReportPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents repData As reportData
    Protected WithEvents lbDownloadArea As System.Web.UI.WebControls.Label
    Protected WithEvents Lblsuccess As System.Web.UI.WebControls.Label
    Protected WithEvents ddProductSelect As System.Web.UI.WebControls.DropDownList

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
    Dim sumValue, sumFGVAlue, SumTotalCogs As Double
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Page.IsPostBack Then
        Else

            Me.lblPageTitle.Text = Request.QueryString("PageTitle")
             GetLineSelectDD(ddLine, Session("country_id"))
            '(ddProductSelect, ddLine.SelectedValue, Session("country_id"), "NONOBS")
            GetProductDescriptionSelectDD(ddProductSelect, ddLine.SelectedValue, Session("country_id"), "ALL")
            txtStartDate.Text = FirstOfThisMonth(repData.lastOrderDate).ToString(DATEFORMAT_STRING_REPORT)
            txtEndDate.Text = LastOfThisMonth(repData.lastOrderDate).ToString(DATEFORMAT_STRING_REPORT)
           
            '  bindData()
        End If

    End Sub

    Private Sub fillReportData()
        repData.lastOrderDate = txtEndDate.Text
        repData.addLine("Report-date from", txtStartDate.Text, True, False)
        repData.addLine("Report-date to", txtEndDate.Text, True, False)
        repData.addLine("Selected Line", ddLine.SelectedItem.Text.ToString, True, False)

    End Sub


    Private Sub bindData()
        fillReportData()
        Dim MyDataView As New DataView
        myReport.StartDate = txtStartDate.Text
        myReport.EndDate = txtEndDate.Text
        myReport.LineID = ddLine.SelectedValue
        myReport.CtryID = Session("country_id")
        myReport.ProdDesc = ddProductSelect.SelectedValue

        SetGridStylesGroup(MyGrid)

        MyDataView = myReport.GetSalesOrders()

        With MyGrid
            .DataSource = MyDataView
            .GridLines = GridLines.None
            .ShowFooter = True
            .AllowSorting = False
            .AllowAutoSort = True
            .AlternatingItemStyle.CssClass = "tableBGColor2Class"
            .ItemStyle.CssClass = "tableBGColor2Class"
            .DataBind()
        End With


    End Sub


    Private Sub Item_created(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated
        setColumnToolTips(e.Item, MyGrid)

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Or e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor2Class';")
        End If

        If e.Item.ItemType = C1ListItemType.Footer Then
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Units"))).Text = MyNumberFormat(sumUnits, 0)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("FGUnits"))).Text = MyNumberFormat(sumFGUnits, 0)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Value"))).Text = MyNumberFormat(sumValue, 2)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("FGValue"))).Text = MyNumberFormat(sumFGVAlue, 2)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("TOTAL_COGS"))).Text = MyNumberFormat(SumTotalCogs, 2)
            e.Item.CssClass = "reportTotal"
            e.Item.Cells(0).Text = "TOTAL AUSTRIA"
            e.Item.Cells(0).HorizontalAlign = HorizontalAlign.Left
        End If

    End Sub

    Private Sub btnGenRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRep.Click

        Page.Validate()

        If Page.IsValid Then
            bindData()
        End If

    End Sub

    Private Sub Item_DataBound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound
        If e.Item.ItemType = C1ListItemType.AlternatingItem Or e.Item.ItemType = C1ListItemType.Item Then

            sumUnits = sumUnits + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Units"))).Text
            sumFGUnits = sumFGUnits + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("FGUnits"))).Text
            sumValue = sumValue + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Value"))).Text
            sumFGVAlue = sumFGVAlue + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("FGValue"))).Text
            SumTotalCogs = SumTotalCogs + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("total_cogs"))).Text

            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("ORPO_VALUE"))), 2)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("ORPO_FGVALUE"))), 2)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("ORPO_UNITS"))), 0)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("ORPO_FGUNITS"))), 0)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("STD_COGS"))), 2)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("TOTAL_COGS"))), 2)
        End If
    End Sub
    Private Sub ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportExcel.Click
        Page.Validate()

        If Page.IsValid Then
            bindData()

        Dim exp As exportToExcel = New exportToExcel(Page)
        exp.title = lblPageTitle.Text
        exp.formatColumnAsString(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("prod_phznr")))
        exp.formatColumnAsString(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("order_no")))
        exp.formatColumnAsString(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("CUDI_CUSTOMER_NR")))
        exp.formatColumnAsString(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Tran_date")))
        exp.formatColumnAsString(4)

        exp.showReportData = repData
        exp.addDataGrid(MyGrid)
        exp.export()
            MyGrid.Visible = False
        End If

    End Sub

    Private Sub ddLine_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddLine.SelectedIndexChanged
        'GetProduct(ddProductSelect, ddLine.SelectedValue, Session("country_id"), "NONOBS")
        GetProductDescriptionSelectDD(ddProductSelect, ddLine.SelectedValue, Session("country_id"), "ALL")

        Dim li As New ListItem
        li.Value = 0
        li.Text = "-- All Products --"
        ddProductSelect.Items.Insert(0, li)
    End Sub
End Class
