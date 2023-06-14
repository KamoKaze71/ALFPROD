Imports Wyeth.Alf.CssStyles
Imports Wyeth.Alf.WyethDropdown
Imports Wyeth.Utilities.NumberFormat
Imports C1.Web.C1WebGrid

Public Class InvlBatchReport
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents txtStartDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents StartImage As System.Web.UI.WebControls.Image
    Protected WithEvents ddproduct As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddLineSelect As System.Web.UI.WebControls.DropDownList
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
        If Page.IsPostBack Then
        Else

            'lists all available Distributors
            GetDistribSelectDD(ddDistribselect, Session("country_id"))
            'Lists all available Lines

            GetLineSelectDD(ddLineSelect, Session("country_id"))
            txtStartDate.Text = repData.lastOrderDate
            txtEndDate.Text = repData.lastOrderDate
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

        If Page.IsPostBack = True Then
            MyReport.ProductID = ddproduct.SelectedValue
        Else
            MyReport.ProductID = 0
        End If

        MyReport.DistID = ddDistribselect.SelectedValue
        MyReport.LineID = ddLineSelect.SelectedValue
        MyGrid.DataSource = MyReport.GetInvlBatchReport(ddproduct, Me)
        MyGrid.ShowFooter = False
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

    'Private Sub MyGrid_ItemDataBound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound
    '    Dim myCheckbox As CheckBox = CType(e.Item.Cells(5).FindControl("CheckQuarantine"), CheckBox)

    '    myCheckbox.ToolTip = "Quarantine:" & Replace(e.Item.Cells(6).Text, "nbsp;", "") & vbCrLf & "Description:" & Replace(e.Item.Cells(7).Text, "nbsp;", "")
    '    myCheckbox.Enabled = False

    '    If e.Item.Cells(6).Text <> "S" And e.Item.Cells(6).Text <> "R" Then
    '        myCheckbox.Checked = True
    '    End If


    '    MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("units"))), 0)

    'End Sub

    Private Sub MyGrid_ItemCreated(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated
        'If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.GroupHeader Then
        '    MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("units")) - 1), 0)
        'End If
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
        col6.DataField = "INVL_BATCH_NUMBER"

        expGrid.Columns.Add(col)
        expGrid.Columns.Add(col1)
        expGrid.Columns.Add(col6)
      

        ' SetGridStylesGroup(expGrid)
        MyReport.StartDate = txtStartDate.Text
        If Page.IsPostBack = True Then
            MyReport.ProductID = ddproduct.SelectedValue
        Else
            MyReport.ProductID = 0
        End If

        MyReport.DistID = ddDistribselect.SelectedValue
        MyReport.LineID = ddLineSelect.SelectedValue
        expGrid.DataSource = MyReport.GetInvlBatchReport(ddproduct, Me)
        expGrid.DataBind()

        expGrid.ShowHeader = False
        expGrid.AllowSorting = False

        Dim exp As exportToExcel = New exportToExcel(Page)
        exp.ContainsControls = False
        exp.title = Me.ALFPageTitle
        exp.formatColumnAsString(0)
        exp.formatColumnAsString(1)
        exp.formatColumnAsString(2)
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
End Class

