Imports Wyeth.Alf.CssStyles
Imports Wyeth.Utilities
Imports Wyeth.Utilities.Helper
Imports Wyeth.Utilities.NumberFormat
Imports Wyeth.Alf.WyethDropdown


Public Class Cegedim
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents txtStartDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents ReportPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents lblReportDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblLastOrderEntry As System.Web.UI.WebControls.Label
	Protected WithEvents Wyethtextbox1 As Wyeth.Utilities.WyethTextBox
	Protected WithEvents txtEndDate As Wyeth.Utilities.WyethTextBox
	Protected WithEvents lblReportEndDate As System.Web.UI.WebControls.Label
	Protected WithEvents btn_export As System.Web.UI.WebControls.Button
    Protected WithEvents ddlineselect As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddDistribSelect As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnGenRep As System.Web.UI.WebControls.Button
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents repData As Wyeth.Alf.reportData
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents StartImage As System.Web.UI.WebControls.Image
    Protected WithEvents EndImage As System.Web.UI.WebControls.Image
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
    Dim MyReport As New Report
    Dim sumFGValue, sumValue As Double
    Dim sumFGUnits, sumUnits As Integer
    Dim MyPopUp As New JSPopUp(Me.Page)
    Dim MyCustomerPopUp As New JSPopUp(Me.Page)
    Dim MyDatePickerPopUp As New JSPopUp(Me.page)


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ResetSum()
        MyPopUp.Width = 900
        MyPopUp.Height = 550
        MyCustomerPopUp.Height = 350
        MyCustomerPopUp.Width = 750

        If Not Page.IsPostBack Then
          

            Me.lblPageTitle.Text = Request.QueryString("PageTitle")
            Me.txtStartDate.Text = Convert.ToDateTime(repData.lastOrderDate).AddDays(-7).ToString(DATEFORMAT_STRING_REPORT)
            Me.txtEndDate.Text = repData.lastOrderDate
            GetLineSelectDD(ddlineselect, Session("country_id"))
            GetDistribSelectDD(ddDistribSelect, Session("country_id"))
            BindData()
            MyDatePickerPopUp.AddDatePopupToControl(txtEndDate, StartImage)
            MyDatePickerPopUp.AddDatePopupToControl(txtStartDate, EndImage)
        End If

    End Sub
    Private Sub BindData()

        SetGridStyles(MyGrid)
        repData.lastOrderDate = txtEndDate.Text
        MyReport.StartDate = txtStartDate.Text
        MyReport.EndDate = txtEndDate.Text
        MyReport.LineID = ddlineselect.SelectedValue
        MyGrid.ShowFooter = True
        MyGrid.DataSource = MyReport.GetCegedim()
        MyGrid.DataBind()



    End Sub

    Private Sub btnGenRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRep.Click
        Validate()
        If Page.IsValid = True Then
            BindData()
        End If
    End Sub


    Private Sub Item_DataBound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound

        MyPopUp.PageURL = "../admin/ProductByID.aspx?prod_id=" & e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("prod_id"))).Text
        MyPopUp.AddPopupToControl(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("prod_phznr"))))

        MyCustomerPopUp.PageURL = "../admin/ViewCustomerByID.aspx?cust_id=" & e.Item.Cells(0).Text
        MyCustomerPopUp.AddPopupToControl(e.Item.Cells(1))

        sumFGUnits = sumFGUnits + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Orpo_FGunits"))).Text
        sumUnits = sumUnits + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Orpo_units"))).Text
        sumValue = sumValue + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Orpo_Value"))).Text

        MyNumberFormat((e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Orpo_units")))), 0)
        MyNumberFormat((e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Orpo_FGUnits")))), 0)

        'e.Item.Cells(1).Attributes.Add("onclick", "OpenPopUp('../admin/ViewCustomerByID.aspx?cust_id=" & e.Item.Cells(0).Text & "','CustomerDetails')")
          
        e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("CUDI_CUSTOMER_NR"))).ToolTip = "View Customer Information"
        e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("prod_phznr"))).ToolTip = "View Product Information"

    End Sub



    Private Sub Item_Created(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated
        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Then
            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor1Class';")

            setTDMouseover(e.Item.Cells(1))
            setTDMouseover(e.Item.Cells(2))

        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor2Class';")

            setTDMouseover(e.Item.Cells(1))
            setTDMouseover(e.Item.Cells(2))

        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Footer Then
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Orpo_FGunits"))).Text = MyNumberFormat(sumFGUnits, 0)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Orpo_units"))).Text = MyNumberFormat(sumUnits, 0)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Orpo_Value"))).Text = MyNumberFormat(sumValue, 2)
        End If


    End Sub


    Private Sub ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportExcel.Click

        BindData()
        Dim strwws, filename As String
        filename = "CEGEDIM_" & MyReport.StartDate.ToString(DATEFORMAT_STRING_REPORT) & "_TO_" & MyReport.EndDate.ToString(DATEFORMAT_STRING_REPORT) & ".txt"

        For Each Griditem As C1.Web.C1WebGrid.C1GridItem In MyGrid.Items
            strwws += Griditem.Cells(1).Text & ";"
            strwws += Griditem.Cells(2).Text & ";"
            strwws += Griditem.Cells(3).Text & ";"
            strwws += Griditem.Cells(4).Text & ";"
            strwws += Griditem.Cells(5).Text & ";"
            strwws += Griditem.Cells(6).Text & ";"
            strwws += vbNewLine
        Next


        Response.Clear()
        Response.AddHeader("Content-Disposition", "attachment; filename=" & filename)
        Response.ContentType = "Text/plain"
        Response.Buffer = True
        Response.Write(strwws)
        Response.Flush()
        Response.End()
    End Sub

    Private Sub ResetSum()
        sumFGValue = 0 : sumValue = 0
        sumFGUnits = 0 : sumUnits = 0
    End Sub

    '**************************************************************************************************************
    '* For Printing
    '**************************************************************************************************************
    Private Sub btnPrint_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Print_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Autoprint Or PopupWindowStyle.NoToolbar)
        ResetSum()
        BindData()
    End Sub

    Private Sub btnPreview_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Preview_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Fullscreen)
        ResetSum()
        BindData()
    End Sub

    Private Function PrePrint() As printReportUtil
        BindData()

        Dim preview As New printReportUtil

        preview.PageTitle = "Cegedim Report"
        preview.PageSize = 44

        preview.AddReportHeader(repData)
        preview.AddWebGrid(Me.MyGrid)

        Return preview

    End Function
    '**************************************************************************************************************
    '* End Printing
    '**************************************************************************************************************
End Class
