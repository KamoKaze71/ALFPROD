Imports System.Globalization

Imports Oracle.DataAccess.Client

Imports Wyeth.Utilities.Helper
Imports Wyeth.Utilities.NumberFormat
Imports Wyeth.Utilities.DateHandling

Imports Wyeth.Alf.CssStyles

Public Class PremarinReport
	Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
	Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
	Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents txtStartDate As Wyeth.Utilities.WyethTextBox
	Protected WithEvents txtEndDate As Wyeth.Utilities.WyethTextBox
	Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
	Protected WithEvents ReportPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents lblReportDateFrom As System.Web.UI.WebControls.Label
	Protected WithEvents lblReportDateTO As System.Web.UI.WebControls.Label
	Protected WithEvents lblLastOrderEntry As System.Web.UI.WebControls.Label
	Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents ddLineSelect As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnGenRep As System.Web.UI.WebControls.Button
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents repData As Wyeth.Alf.reportData
    Protected WithEvents prtControl As printReportCtl
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents StartImage As System.Web.UI.WebControls.Image
    Protected WithEvents EndImage As System.Web.UI.WebControls.Image


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
    Dim MyDatePopUp As New JSPopUp(Me.page)


	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		'Put user code to initialize the page here

        If Not Page.IsPostBack Then
            Me.lblPageTitle.Text = Request.QueryString("PageTitle")
            txtStartDate.Text = FirstOfThisQ(Today().AddMonths(-3)).ToString(DATEFORMAT_STRING_REPORT)
            txtEndDate.Text = LastOfThisQ(Today().AddMonths(-3)).ToString(DATEFORMAT_STRING_REPORT)
            MyDatePopUp.AddDatePopupToControl(txtStartDate, StartImage)
            MyDatePopUp.AddDatePopupToControl(txtEndDate, EndImage)
            BindData()
        End If
	End Sub
	Private Sub BindData()
		
        MyReport.CtryID = CInt(Session("country_id"))
        repData.lastOrderDate = txtEndDate.Text
        MyReport.EndDate = Convert.ToDateTime(txtEndDate.Text) ' Date.ParseExact(txtEndDate.Text, Wyeth.Utilities.Helper.DATEFORMAT_STRING, MyDTFI)
        MyReport.StartDate = Convert.ToDateTime(txtStartDate.Text) ' Date.ParseExact(txtStartDate.Text, Wyeth.Utilities.Helper.DATEFORMAT_STRING, MyDTFI)
		SetGridStyles(MyGrid)
		MyGrid.DataSource = MyReport.GetPremarinReport()
		MyGrid.DataBind()
		
	End Sub
    Private Sub ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportExcel.Click
        BindData()

        Dim exp As exportToExcel = New exportToExcel(Page)
        exp.title = lblPageTitle.Text
        exp.showReportData = repData
        exp.addDataGrid(MyGrid)
        exp.export()
    End Sub
    Private Sub Item_DataBound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("total_mggrams_stock"))), 0)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("TOTAL_number_PACKAGES_SOLD"))), 0)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("TOTAL_number_tablets_sold"))), 0)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("total_mggrams_sold"))), 0)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("INVL_UNITS"))), 0)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("total_number_tablets_stock"))), 0)
    End Sub

    Private Sub btnGenRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRep.Click
        Page.Validate()
        If Page.IsValid = True Then
            BindData()
        End If
    End Sub

    '**************************************************************************************************************
    '* For Printing
    '**************************************************************************************************************
    Private Sub btnPrint_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Print_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Autoprint Or PopupWindowStyle.NoToolbar)
        BindData()
    End Sub

    Private Sub btnPreview_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Preview_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Fullscreen)
        BindData()
    End Sub

    Private Function PrePrint() As printReportUtil
        BindData()

        Dim preview As New printReportUtil

        preview.PageTitle = "Premarin Report"
        preview.DefaultOrientation = Orientation.Landscape

        preview.AddReportHeader(repData)
        preview.AddWebGrid(Me.MyGrid)

        Return preview

    End Function
    '**************************************************************************************************************
    '* End Printing
    '**************************************************************************************************************
End Class
