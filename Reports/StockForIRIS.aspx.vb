Imports Wyeth.Alf.CssStyles
Imports Wyeth.Alf.WyethDropdown
Imports Wyeth.Utilities.NumberFormat
Imports C1.Web.C1WebGrid


Public Class StockForIRIS
	Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
	Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
	Protected WithEvents ddLineSelect As System.Web.UI.WebControls.DropDownList
	Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents ReportPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents genReport As System.Web.UI.WebControls.Button
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents btnGenRep As System.Web.UI.WebControls.Button
    Protected WithEvents txtStartDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents DROPDOWNLIST1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddDistribselect As System.Web.UI.WebControls.DropDownList
    Protected WithEvents repData As Wyeth.Alf.reportData
    Protected WithEvents prtControl As printReportCtl
    Protected WithEvents StartImage As System.Web.UI.WebControls.Image
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents ddproduct As System.Web.UI.WebControls.DropDownList

	'NOTE: The following placeholder declaration is required by the Web Form Designer.
	'Do not delete or move it.
	Private designerPlaceholderDeclaration As System.Object

	Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
		'CODEGEN: This method call is required by the Web Form Designer
		'Do not modify it using the code editor.
		InitializeComponent()
	End Sub

#End Region


	Dim MyStock As New Stock
	Dim MyProduct As New WyethProduct
    Dim MyReport As New Report
    Dim MyDatePopup As New JSPopUp(Me.Page)



	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		'Put user code to initialize the page here
		If Page.IsPostBack Then
		Else
			Me.lblPageTitle.Text = Request.QueryString("PageTitle")
			'lists all available Distributors
			GetDistribSelectDD(ddDistribSelect, Session("country_id"))
            'Lists all available Lines

            GetLineSelectDD(ddLineSelect, Session("country_id"))
            txtStartDate.Text = repData.lastOrderDate
            MyDatePopup.AddDatePopupToControl(txtStartDate, StartImage)
            bindData()
        End If


	End Sub
    Private Sub bindData()
        repData.lastOrderDate = txtStartDate.Text
        SetGridStylesGroup(MyGrid)
        MyReport.StartDate = txtStartDate.Text
        If Page.IsPostBack = True Then
            MyReport.ProductID = ddproduct.SelectedValue
        Else
            MyReport.ProductID = 0
        End If

        MyReport.DistID = ddDistribselect.SelectedValue
        MyReport.LineID = ddLineSelect.SelectedValue
        MyGrid.DataSource = MyReport.GetStockForIRIS(ddproduct, Me)
        MyGrid.DataBind()
    End Sub

    Private Sub ddLineSelect_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddLineSelect.SelectedIndexChanged
        ddproduct.SelectedValue = 0
        bindData()
    End Sub

    Private Sub ddDistribSelect_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddDistribselect.SelectedIndexChanged
        ddproduct.SelectedValue = 0
        bindData()
    End Sub

    Private Sub MyGrid_ItemDataBound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound
        Dim myCheckbox As CheckBox = CType(e.Item.Cells(5).FindControl("CheckQuarantine"), CheckBox)

        myCheckbox.ToolTip = "Quarantine:" & Replace(e.Item.Cells(6).Text, "nbsp;", "") & vbCrLf & "Description:" & Replace(e.Item.Cells(7).Text, "nbsp;", "")
        myCheckbox.Enabled = False

        If e.Item.Cells(6).Text <> "S" And e.Item.Cells(6).Text <> "R" Then
            myCheckbox.Checked = True
        End If


        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("units"))), 0)

    End Sub

    Private Sub MyGrid_ItemCreated(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated
        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.GroupHeader Then
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("units")) - 1), 0)
        End If
        setColumnToolTips(e.Item, MyGrid, 1)
    End Sub

    Private Sub C1WebGrid1_SortCommand(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1SortCommandEventArgs) Handles MyGrid.SortCommand
        bindData()
    End Sub

    Private Sub ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportExcel.Click

        Dim expGrid As New DataGrid
        expGrid.AutoGenerateColumns = False

        Dim col As New BoundColumn
        Dim col1 As New BoundColumn
        Dim col2 As New BoundColumn
        Dim col3 As New BoundColumn
        Dim col4 As New BoundColumn
        Dim col5 As New BoundColumn
        Dim col6 As New BoundColumn
        col.DataField = "prod_phznr"
        col1.DataField = "prod_presentation"
        col2.DataField = "invl_units"
        col3.DataField = "INVL_DATE_EXP"
        col4.DataField = "code_code"
        col5.DataField = "code_description"
        col6.DataField = "INVL_BATCH_NUMBER"

        expGrid.Columns.Add(col)
        expGrid.Columns.Add(col1)
        expGrid.Columns.Add(col2)
        expGrid.Columns.Add(col3)
        expGrid.Columns.Add(col6)
        expGrid.Columns.Add(col4)
        expGrid.Columns.Add(col5)

        ' SetGridStylesGroup(expGrid)
        MyReport.StartDate = txtStartDate.Text
        If Page.IsPostBack = True Then
            MyReport.ProductID = ddproduct.SelectedValue
        Else
            MyReport.ProductID = 0
        End If

        MyReport.DistID = ddDistribselect.SelectedValue
        MyReport.LineID = ddLineSelect.SelectedValue
        expGrid.DataSource = MyReport.GetStockForIRIS(ddproduct, Me)
        expGrid.DataBind()

        expGrid.ShowHeader = False
        expGrid.AllowSorting = False

        Dim exp As exportToExcel = New exportToExcel(Page)
        exp.ContainsControls = False
        exp.title = lblPageTitle.Text
        exp.formatColumnAsString(0)
        exp.formatColumnAsString(3)
        exp.formatColumnAsString(4)
        exp.showReportData = repData
        exp.addDataGrid(expGrid)
        exp.testWithHTML = True
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

        preview.PageTitle = "Stock for IRIS"
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
