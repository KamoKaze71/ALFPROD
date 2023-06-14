Imports Wyeth.Alf.CssStyles
Imports Wyeth.Utilities.Helper
Imports Wyeth.Utilities.DateHandling
Imports Wyeth.Alf.WyethDropdown



Public Class ForcastIris
	Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
	Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
	Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
	Protected WithEvents txtStartDate As Wyeth.Utilities.WyethTextBox
	Protected WithEvents txtEndDate As Wyeth.Utilities.WyethTextBox
	Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents ReportPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents lblReportStartDate As System.Web.UI.WebControls.Label
	Protected WithEvents lblLastEndDate As System.Web.UI.WebControls.Label
	Protected WithEvents lblLastOrderEntry As System.Web.UI.WebControls.Label
	Protected WithEvents btn_export As System.Web.UI.WebControls.Button
    Protected WithEvents ddlineselect As System.Web.UI.WebControls.DropDownList
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

		If Page.IsPostBack = True Then


        Else
            GetLineSelectDD(ddlineselect, Session("country_id"))
            Me.lblPageTitle.Text = Request.QueryString("PageTitle")
            txtStartDate.Text = FirstOfThisMonth(Session("LastMonthApproved")).ToString(DATEFORMAT_STRING)
            txtEndDate.Text = LastOfThisMonth(Session("LastMonthApproved")).ToString(DATEFORMAT_STRING)
            MyDatePopUp.AddDatePopupToControl(txtStartDate, StartImage)
            MyDatePopUp.AddDatePopupToControl(txtEndDate, EndImage)

            BindData()

        End If

	End Sub
	Private Sub BindData()
		
		' Set default values  for GM Report
		SetGridStyles(MyGrid)
        repData.lastOrderDate = txtEndDate.Text
		MyReport.CtryID = Session("country_id")
		MyReport.StartDate = txtStartDate.Text
        MyReport.EndDate = txtEndDate.Text
        MyReport.LineID = ddlineselect.SelectedValue
		MyGrid.DataSource = MyReport.GetFcstAccuracy()
		MyGrid.DataBind()
		

	End Sub

    'Private Sub btn_export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_export.Click

    '    'Dim strwws, filename As String
    '    'filename = "IRIS_SALES_" & MyReport.StartDate.ToString(DATEFORMAT_STRING_REPORT) & "_TO_" & MyReport.EndDate.ToString(DATEFORMAT_STRING_REPORT) & ".txt"

    '    'For Each Griditem As C1.Web.C1WebGrid.C1GridItem In MyGrid.Items


    '    '    strwws += Griditem.Cells(0).Text
    '    '    strwws += Griditem.Cells(1).Text
    '    '    strwws += Griditem.Cells(2).Text
    '    '    strwws += Griditem.Cells(3).Text
    '    '    strwws += Griditem.Cells(4).Text
    '    '    strwws += Griditem.Cells(5).Text
    '    '    strwws += Griditem.Cells(6).Text
    '    '    strwws += Griditem.Cells(7).Text
    '    '    'strwws += Griditem.Cells(8).Text.PadLeft(8, CType(vbNullChar, Char))
    '    '    'strwws += Griditem.Cells(9).Text.PadLeft(8, CType(vbNullChar, Char))
    '    '    'strwws += Griditem.Cells(10).Text.PadLeft(8, CType(vbNullChar, Char))
    '    '    'strwws += Griditem.Cells(11).Text.PadLeft(8, CType(vbNullChar, Char))
    '    '    'strwws += Griditem.Cells(12).Text.PadLeft(8, CType(vbNullChar, Char))
    '    '    strwws += ";" & vbNewLine
    '    'Next


    '    'Response.Clear()
    '    'Response.AddHeader("Content-Disposition", "attachment; filename=" & filename)
    '    'Response.ContentType = "Text/plain"
    '    ''Response.WriteFile(filename)
    '    'Response.Write(strwws)
    '    'Response.End()

    'End Sub

    Private Sub ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportExcel.Click
        BindData()
        Dim exp As exportToExcel = New exportToExcel(Page)
        exp.title = lblPageTitle.Text
        exp.showReportData = repData
        exp.addDataGrid(MyGrid)
        exp.export()
    End Sub


    Private Sub btnGenRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRep.Click

        Validate()
        If Page.IsValid = True Then
            BindData()
        End If
    End Sub

    '**************************************************************************************************************
    '* For Printing
    '**************************************************************************************************************
    Sub btnPrint_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Print_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Autoprint Or PopupWindowStyle.NoToolbar)
        BindData()
    End Sub

    Sub btnPreview_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Preview_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Fullscreen)
        BindData()
    End Sub

    Private Function PrePrint() As printReportUtil
        BindData()

        Dim preview As New printReportUtil

        preview.PageTitle = "Sales for IRIS "
        preview.PageSize = 43
        preview.PageSizeLandscape = 29
        preview.DefaultOrientation = Orientation.Landscape

        preview.AddReportHeader(repData)
        preview.AddWebGrid(Me.MyGrid)

        Return preview

    End Function
    '**************************************************************************************************************
    '* End Printing
    '**************************************************************************************************************
End Class
