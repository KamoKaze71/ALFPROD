Imports Wyeth.Alf.CssStyles
Imports C1.Web.C1WebGrid
Imports Wyeth.Utilities.Helper
Imports Wyeth.Utilities.NumberFormat





Public Class SalesStatAreaDetail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents repData As Wyeth.Alf.reportData
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents lblCustomer As System.Web.UI.WebControls.Label
    Protected WithEvents reportPanel As System.Web.UI.WebControls.Panel


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
    Dim sare_id, line_id, prod_id As Integer
    Dim cust_no, prod_desc, ed, sd, sare_name, customer_name As String
    Dim sumUnits, sumFGUnits As Integer
    Dim sumValue, sumFGValue As Double


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        If Page.IsPostBack Then

            sd = Me.ViewState("sd")
            ed = Me.ViewState("ed")
            cust_no = Me.ViewState("cust_no")
            prod_desc = Me.ViewState("prod_desc")
            sare_id = Me.ViewState("sare_id")
            line_id = Me.ViewState("line_id")
            sare_name = Me.ViewState("sare_name")
            customer_name = Me.ViewState("customer_name")
            prod_id = Me.ViewState("prod_id")

        Else
            sd = Request.QueryString("sd")
            ed = Request.QueryString("ed")
            cust_no = Request.QueryString("cust_no")
            prod_desc = Request.QueryString("prod_desc")
            sare_id = Request.QueryString("sare_id")
            line_id = Request.QueryString("line_id")
            sare_name = Request.QueryString("sare_name")
            customer_name = Request.QueryString("customer_name")
            prod_id = Request.QueryString("prod_id")


            Me.ViewState.Add("sd", sd)
            Me.ViewState.Add("ed", ed)
            Me.ViewState.Add("cust_no", cust_no)
            Me.ViewState.Add("prod_desc", prod_desc)
            Me.ViewState.Add("sare_id", sare_id)
            Me.ViewState.Add("line_id", line_id)
            Me.ViewState.Add("sare_name", sare_name)
            Me.ViewState.Add("customer_name", customer_name)
            Me.ViewState.Add("prod_id", prod_id)
            lblCustomer.Text = customer_name

            repData.addLine("Product", prod_desc, True, True)
            repData.addLine("Start Date", Convert.ToDateTime(sd).ToString(DATEFORMAT_STRING_REPORT), True, True)
            repData.addLine("End Date", Convert.ToDateTime(ed).ToString(DATEFORMAT_STRING_REPORT), True, True)

            MyGrid.Columns(2).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded

            lblPageTitle.Text = Request.QueryString("pagetitle")
            SetGridStylesGroup(MyGrid)

            If sare_id = Nothing Then

                BindDataCustStat()
            Else
                repData.addLine("ADM", sare_name, True, True)
                BindDataAreaStat()
            End If

        End If




    End Sub

    Private Sub BindDataAreaStat()
        MyReport.SaReID = sare_id
        MyReport.StartDate = sd
        MyReport.EndDate = ed
        MyReport.ProdDesc = prod_desc
        MyReport.CustomerNo = cust_no
        MyReport.LineID = line_id
        MyReport.ProductID = prod_id
        MyReport.CtryID = Session("country_id")
        MyGrid.DataSource = MyReport.GetSalesAreaStatDetail
        MyGrid.DataBind()

    End Sub

    Private Sub BindDataCustStat()

        MyReport.StartDate = sd
        MyReport.EndDate = ed
        MyReport.ProdDesc = prod_desc
        MyReport.CustomerNo = cust_no
        MyReport.LineID = line_id
        MyReport.ProductID = prod_id
        MyReport.CtryID = Session("country_id")
        MyGrid.DataSource = MyReport.GetSalesCustomerStatDetail
        MyGrid.DataBind()

    End Sub

    Private Sub Item_DataBound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound

        If e.Item.ItemType = C1ListItemType.AlternatingItem Or e.Item.ItemType = C1ListItemType.Item Then
            sumUnits = sumUnits + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_Units"))).Text
            sumFGUnits = sumFGUnits + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_FGUnits"))).Text
            sumValue = sumValue + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_Value"))).Text
            sumFGValue = sumFGValue + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_FGValue"))).Text
        End If

        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_Units"))), 0)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_FGUnits"))), 0)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_Value"))), 2)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_FGValue"))), 2)

    End Sub
    Private Sub Item_Created(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated

        If e.Item.ItemType = C1ListItemType.GroupHeader Then
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_Units")) - 2), 0)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_FGUnits")) - 2), 0)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_Value")) - 2), 2)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_FGValue")) - 2), 2)
        End If


        If e.Item.ItemType = C1ListItemType.Footer Then
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_Units"))).Text = MyNumberFormat(sumUnits, 0)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_FGUnits"))).Text = MyNumberFormat(sumFGUnits, 0)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_Value"))).Text = MyNumberFormat(sumValue, 2)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_FGValue"))).Text = MyNumberFormat(sumFGValue, 2)
        End If
    End Sub




    Private Sub ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportExcel.Click
        For Each column As C1Column In MyGrid.Columns
            column.GroupInfo.OutlineMode = OutlineModeEnum.None
            column.GroupInfo.ImageExpanded = ""
        Next

        If sare_id = Nothing Then
            BindDataCustStat()
        Else
            BindDataAreaStat()

        End If

        Dim exp As exportToExcel = New exportToExcel(Page)


        exp.title = lblPageTitle.Text
        exp.showReportData = repData
        exp.addDataGrid(MyGrid)
        exp.export()
    End Sub

End Class