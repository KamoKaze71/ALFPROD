Imports Wyeth.Alf.CssStyles
Imports Wyeth.Alf.WyethDropdown
Imports Wyeth.Utilities.Helper
Imports Wyeth.Utilities.NumberFormat

Public Class ruii
	Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
	Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
	Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
	Protected WithEvents ReportPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents txtStartDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents ddLineSelect As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnGenRep As System.Web.UI.WebControls.Button
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents repData As Wyeth.Alf.reportData
    Protected WithEvents prtControl As printReportCtl
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents StartImage As System.Web.UI.WebControls.Image
    Protected WithEvents ddProduct As System.Web.UI.WebControls.DropDownList

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
    Dim sumUnits, sumhistUse As Integer
    Dim sumTcogs As Double

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		'Put user code to initialize the page here

        If Page.IsPostBack = True Then
            ResetSum()
        Else
            Me.lblPageTitle.Text = Request.QueryString("PageTitle")
            txtStartDate.Text = repData.lastOrderDate
            GetLineSelectDD(ddLineSelect, Session("country_id"))
            GetProductSelectDD(ddProduct, ddLineSelect.SelectedValue, Session("country_id"), "ALL")
            Dim li As New ListItem
            li.Text = "--All Products--"
            li.Value = "0"
            ddProduct.Items.Insert(0, li)

            MyDatePopUp.AddDatePopupToControl(txtStartDate, StartImage)
            BindData()

        End If

	End Sub
	Private Sub BindData()
        repData.lastOrderDate = txtStartDate.Text
        SetGridStylesGroup(MyGrid)
        MyReport.StartDate = txtStartDate.Text
        MyReport.LineID = ddLineSelect.SelectedValue
        MyReport.ProductID = ddProduct.SelectedValue
		MyGrid.DataSource = MyReport.GetRuii()
		MyGrid.DataBind()

	End Sub

	Private Sub Item_created(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated
        Dim i As Integer

	If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.GroupFooter Then
            For Each cell As TableCell In e.Item.Cells()
                cell.Font.Bold = True
            Next
           
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("HIST_RG_DIVIDENT")) - 1).Text = (CDbl(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("invl_units")) - 1).Text) / CDbl(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("orpo_units")) - 1).Text))

            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("invl_units")) - 1), 0)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("total_cogs")) - 1), 2)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("orpo_units")) - 1), 0)


            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("HIST_RG_DIVIDENT")) - 1), 2)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("HIST_RG_DIVIDENT")) - 1).Text = mulitply(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("HIST_RG_DIVIDENT")) - 1))
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("HIST_RG_DIVIDENT")) - 1).HorizontalAlign = HorizontalAlign.Right
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("HIST_RG_DIVIDENT")) - 1), 2)

        End If

      


    End Sub

    Private Function mulitply(ByVal cell As TableCell) As String

        Dim ret_val As String

        Try
            Dim dbl As Double = cell.Text()
            ret_val = dbl * 12

        Return ret_val
        Catch
            ret_val = "-"
            Return ret_val
        End Try

    End Function
    Private Sub Item_DataBound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound

        sumUnits = sumUnits + MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("invl_units"))).Text, 0)
        sumTcogs = sumTcogs + MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("total_cogs"))).Text, 2)
        sumhistUse = sumhistUse + MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("orpo_units"))).Text, 0)

        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("invl_units"))), 0)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("total_cogs"))), 2)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("orpo_units"))), 0)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("HIST_RG_DIVIDENT"))), 2)



        'If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.GroupFooter Then
        '    Dim i, j As Integer
        '    Dim d As Double

        '    i = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("invl_units"))).Text
        '    j = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("orpo_units"))).Text

        '    d = i / j * 12
        '    e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("hist_rg_divident"))).Text = d


        'End If




    End Sub

    Private Sub btnGenRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRep.Click
        Validate()
        If Page.IsValid Then
            BindData()
        End If
    End Sub
    Private Sub ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportExcel.Click
        BindData()

        Dim exp As exportToExcel = New exportToExcel(Page)
        exp.title = lblPageTitle.Text
        exp.formatColumnAsString(0)
        exp.formatColumnAsString(1)
        exp.showReportData = repData
        exp.addDataGrid(MyGrid)
        exp.export()
    End Sub

    Private Sub ResetSum()
        sumUnits = 0 : sumhistUse = 0 : sumTcogs = 0
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

        preview.PageTitle = "RUII Report"
        preview.PageSizeLandscape = 32
        preview.DefaultOrientation = Orientation.Landscape

        preview.AddReportHeader(repData)
        preview.AddWebGrid(Me.MyGrid)

        Return preview

    End Function
    '**************************************************************************************************************
    '* End Printing
    '**************************************************************************************************************

    Private Sub txtStartDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtStartDate.TextChanged

    End Sub

    Private Sub ddLineSelect_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddLineSelect.SelectedIndexChanged
        GetProductSelectDD(ddProduct, ddLineSelect.SelectedValue, Session("country_id"), "ALL")
    End Sub
End Class
