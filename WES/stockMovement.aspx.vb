Imports Wyeth.Alf.WyethDropdown
Imports Wyeth.Utilities.DateHandling
Imports Wyeth.Utilities.NumberFormat
Imports Wyeth.Utilities.Helper
Imports Wyeth.Alf.CssStyles


Public Class stockMovement
	Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
	Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents ddlineselect As System.Web.UI.WebControls.DropDownList
	Protected WithEvents txtStartDate As Wyeth.Utilities.WyethTextBox
	Protected WithEvents txtEndDate As Wyeth.Utilities.WyethTextBox
	Protected WithEvents MYGRid As C1.Web.C1WebGrid.C1WebGrid
	Protected WithEvents GridPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents ddDistribSelect As System.Web.UI.WebControls.DropDownList
	Protected WithEvents btn_refresh As System.Web.UI.WebControls.Button
    Protected WithEvents btnGenRep As System.Web.UI.WebControls.Button
    Protected WithEvents repData As reportData
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents prtControl As printReportCtl
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents StartImage As System.Web.UI.WebControls.Image
    Protected WithEvents EndImage As System.Web.UI.WebControls.Image
    Protected WithEvents MyGridValues As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents ddValuesSelect As System.Web.UI.WebControls.DropDownList
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
    Dim myDatePopUp As New JSPopUp(Me.page)
    Dim sum0, sum1, sum2, sum3, sum4, sum5, sumUM As Double
    Dim prod_id, line_id, dist_id As String

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		'Put user code to initialize the page here

        If Not Page.IsPostBack = True Then
            prod_id = Request.QueryString("prod_id")
            line_id = Request.QueryString("line_id")
            dist_id = Request.QueryString("dist_id")
            lblPageTitle.Text = Request.QueryString.Item("pageTitle")
           
            GetLineSelectDD(ddlineselect, Session("country_id"))
          
            If line_id Is Nothing Then
            Else
                ddlineselect.SelectedValue = line_id
            End If

            GetDistribSelectDD(ddDistribSelect, Session("country_id"))
            If dist_id <> "" Then
                ddDistribSelect.SelectedValue = dist_id
            End If
            myDatePopUp.AddDatePopupToControl(txtStartDate, StartImage)
            myDatePopUp.AddDatePopupToControl(txtEndDate, EndImage)

            txtStartDate.Text = FirstOfThisMonth(repData.lastOrderDate).ToString(DATEFORMAT_STRING_REPORT)
            txtEndDate.Text = LastOfThisMonth(repData.lastOrderDate).ToString(DATEFORMAT_STRING_REPORT)


            Dim li As New ListItem
            li.Text = "Units"
            li.Value = 0
            ddValuesSelect.Items.Add(li)
            Dim li2 As New ListItem
            li2.Text = "Values"
            li2.Value = 2
            ddValuesSelect.Items.Add(li2)
            ddValuesSelect.SelectedValue = 0
            ddValuesSelect.DataBind()

            BindData()
        End If


	End Sub

    Private Sub fillReportData()
        repData.lastOrderDate = txtEndDate.Text
        repData.addLine("Report-date from", txtStartDate.Text, True, False)
        repData.addLine("Report-date to", txtEndDate.Text, True, False)
        repData.addLine("Selected Line", ddlineselect.SelectedItem.Text.ToString, True, False)
    End Sub

    Private Sub BindData()
        If Page.IsPostBack = True Then
            MyReport.ProductID = ddProduct.SelectedValue
        Else
            If prod_id Is Nothing Then
                MyReport.ProductID = 0
            Else
                MyReport.ProductID = prod_id
            End If


            End If
            fillReportData()
            MyReport.StartDate = txtStartDate.Text
            MyReport.EndDate = txtEndDate.Text
            MyReport.LineID = ddlineselect.SelectedValue
            MyReport.DistID = ddDistribSelect.SelectedValue
            If ddValuesSelect.SelectedValue = 0 Then
                MYGRid.Columns.Item(3).SortDirection = C1.Web.C1WebGrid.C1SortDirection.Ascending
                MYGRid.DataSource = MyReport.GetStockStat(ddProduct, Me)
                MYGRid.DataBind()
            ElseIf ddValuesSelect.SelectedValue = 2 Then
                MyGridValues.Columns.Item(3).SortDirection = C1.Web.C1WebGrid.C1SortDirection.Ascending
                MyGridValues.DataSource = MyReport.GetStockStatValues(ddProduct, Me)
                MyGridValues.DataBind()
            End If

            SetGridStyles(MYGRid)
            SetGridStyles(MyGridValues)
    End Sub



    Private Sub MyGrid_ItemCreated(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MYGRid.ItemCreated, MyGridValues.ItemCreated

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Then
            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor1Class';")
        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor2Class';")
        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Footer Then
            e.Item.Cells(3).Text = "TOTAL:"
            e.Item.Cells(3).HorizontalAlign = HorizontalAlign.Right

            ' write sum values in the grid footer
            e.Item.Cells(4).Text = MyNumberFormat(sum0, CInt(ddValuesSelect.SelectedValue))
            e.Item.Cells(5).Text = MyNumberFormat(sum1, CInt(ddValuesSelect.SelectedValue))
            e.Item.Cells(6).Text = MyNumberFormat(sum2, CInt(ddValuesSelect.SelectedValue))
            e.Item.Cells(7).Text = MyNumberFormat(sum3, CInt(ddValuesSelect.SelectedValue))
            e.Item.Cells(8).Text = MyNumberFormat(sum4, CInt(ddValuesSelect.SelectedValue))
            e.Item.Cells(9).Text = MyNumberFormat(sumUM, CInt(ddValuesSelect.SelectedValue))
            e.Item.Cells(10).Text = MyNumberFormat(sum5, CInt(ddValuesSelect.SelectedValue))

            'reset sum values
            ResetSum()

        End If

    End Sub

    Private Sub item_Databound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MYGRid.ItemDataBound, MyGridValues.ItemDataBound

        setColumnToolTips(e.Item, MYGRid)

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Or e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Then

            Dim korr, out, out_fg, we, startbalance, endbalance, um As Double
            Dim JS_byProduct As String = "javascript:OpenPopUp('StockProduct.aspx?id=" & (e.Item.Cells(0).Text) & "&sd=" & MyReport.StartDate & "&ed=" & MyReport.EndDate & "&dist_id=" & ddDistribSelect.SelectedValue & "&phznr=" & e.Item.Cells(2).Text & "&presentation=" & e.Item.Cells(3).Text & "&pagetitle=Stock by Product" & "  ','StockbyProduct');"

            setTDMouseover(e.Item.Cells(3))
            setTDMouseover(e.Item.Cells(4))
            setTDMouseover(e.Item.Cells(5))
            setTDMouseover(e.Item.Cells(6))
            setTDMouseover(e.Item.Cells(7))
            setTDMouseover(e.Item.Cells(8))
            setTDMouseover(e.Item.Cells(9))

            e.Item.Cells(1).Attributes.Add("style", "cursor:default;")
            e.Item.Cells(10).Attributes.Add("style", "cursor:default;")

            e.Item.Cells(3).Attributes.Add("onclick", JS_byProduct)
            e.Item.Cells(4).Attributes.Add("onclick", JS_byProduct)
            e.Item.Cells(5).Attributes.Add("onclick", "javascript:OpenPopUp('IN.aspx?id=" & (e.Item.Cells(0).Text) & "&sd=" & MyReport.StartDate & "&ed=" & MyReport.EndDate & "&dist_id=" & ddDistribSelect.SelectedValue & "&phznr=" & e.Item.Cells(2).Text & "&presentation=" & e.Item.Cells(3).Text & "&pagetitle=Stock Wareneingang" & "  ','WE');")
            e.Item.Cells(6).Attributes.Add("onclick", "javascript:OpenPopUp('OUT.aspx?id=" & (e.Item.Cells(0).Text) & "&sd=" & MyReport.StartDate & "&ed=" & MyReport.EndDate & "&dist_id=" & ddDistribSelect.SelectedValue & "&phznr=" & e.Item.Cells(2).Text & "&presentation=" & e.Item.Cells(3).Text & "&pagetitle=Stock Sales" & " ','Sales');")
            e.Item.Cells(7).Attributes.Add("onclick", "javascript:OpenPopUp('OUT_FG.aspx?id=" & (e.Item.Cells(0).Text) & "&sd=" & MyReport.StartDate & "&ed=" & MyReport.EndDate & "&dist_id=" & ddDistribSelect.SelectedValue & "&phznr=" & e.Item.Cells(2).Text & "&presentation=" & e.Item.Cells(3).Text & "&pagetitle=Stock Free Goods" & " ','FreeGoods');")
            e.Item.Cells(8).Attributes.Add("onclick", "javascript:OpenPopUp('KORR.aspx?id=" & (e.Item.Cells(0).Text) & "&sd=" & MyReport.StartDate & "&ed=" & MyReport.EndDate & "&dist_id=" & ddDistribSelect.SelectedValue & "&phznr=" & e.Item.Cells(2).Text & "&presentation=" & e.Item.Cells(3).Text & "&pagetitle=Stock Corrections" & " ','Corrections');")
            e.Item.Cells(9).Attributes.Add("onclick", "javascript:OpenPopUp('KORR.aspx?type=um&id=" & (e.Item.Cells(0).Text) & "&sd=" & MyReport.StartDate & "&ed=" & MyReport.EndDate & "&dist_id=" & ddDistribSelect.SelectedValue & "&phznr=" & e.Item.Cells(2).Text & "&presentation=" & e.Item.Cells(3).Text & "&pagetitle=Stock Corrections" & " ','Corrections');")


            startbalance = e.Item.Cells(4).Text
            sum0 = sum0 + startbalance


            we = e.Item.Cells(5).Text()
            sum1 = sum1 + we

            out = e.Item.Cells(6).Text()
            sum2 = sum2 + out

            out_fg = e.Item.Cells(7).Text()
            sum3 = sum3 + out_fg

            korr = e.Item.Cells(8).Text()
            sum4 = sum4 + korr

            um = e.Item.Cells(9).Text()
            sumUM = sumUM + UM

            endbalance = e.Item.Cells(10).Text
            sum5 = sum5 + endbalance



            e.Item.Cells(4).Text = MyNumberFormat(startbalance, CInt(ddValuesSelect.SelectedValue))
            e.Item.Cells(5).Text = MyNumberFormat(we, CInt(ddValuesSelect.SelectedValue))
            e.Item.Cells(6).Text = MyNumberFormat(out, CInt(ddValuesSelect.SelectedValue))
            e.Item.Cells(7).Text = MyNumberFormat(out_fg, CInt(ddValuesSelect.SelectedValue))
            e.Item.Cells(8).Text = MyNumberFormat(korr, CInt(ddValuesSelect.SelectedValue))
            e.Item.Cells(9).Text = MyNumberFormat(um, CInt(ddValuesSelect.SelectedValue))
            e.Item.Cells(10).Text = MyNumberFormat(endbalance, CInt(ddValuesSelect.SelectedValue))
            If MyNumberFormat((startbalance + we - out - out_fg + korr + um), CInt(ddValuesSelect.SelectedValue)) <> MyNumberFormat(endbalance, CInt(ddValuesSelect.SelectedValue)) Then

                Dim cell As TableCell
                For Each cell In e.Item.Cells
                    cell.Attributes.Add("style", "border-top:solid 2px #FF0000; border-bottom:solid 2px #FF0000; color:#ff0000;")
                Next

                e.Item.Cells(1).Text = "<strong>WARNING!</strong><BR>" & e.Item.Cells(1).Text
                e.Item.Cells(1).Attributes.Add("style", "border-top:solid 2px #FF0000; border-bottom:solid 2px #FF0000;  border-left:solid 2px #FF0000; color:#ff0000;")
                e.Item.Cells(e.Item.Cells.Count - 1).Attributes.Add("style", "border-top:solid 2px #FF0000; border-bottom:solid 2px #FF0000;  border-right:solid 2px #FF0000; color:#ff0000;")

                e.Item.Cells(10).Text = ddDistribSelect.SelectedItem.Text & ":" & e.Item.Cells(10).Text & "<br>" & "Wyeth:" & MyNumberFormat(startbalance + we - out - out_fg + korr + um, CInt(ddValuesSelect.SelectedValue))

            End If
        End If


    End Sub

    Private Sub C1WebGrid1_SortCommand(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1SortCommandEventArgs) Handles MYGRid.SortCommand
        BindData()
    End Sub

    Private Sub btnGenRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRep.Click
        Page.Validate()
        If Page.IsValid Then
            BindData()
        End If
    End Sub

    Private Sub ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportExcel.Click
        BindData()

        Dim exp As exportToExcel = New exportToExcel(Page)
        exp.title = lblPageTitle.Text
        exp.showReportData = repData
        exp.formatColumnAsString(2)
        If CInt(ddValuesSelect.SelectedValue) = 0 Then
            exp.addDataGrid(MYGRid)
        ElseIf CInt(ddValuesSelect.SelectedValue) = 2 Then
            exp.addDataGrid(MyGridValues)
        End If

        exp.export()
    End Sub

    Private Sub ResetSum()
        sum0 = 0 : sum1 = 0 : sum2 = 0 : sum3 = 0
        sum4 = 0 : sum5 = 0
    End Sub

    '**************************************************************************************************************
    '* For Printing
    '**************************************************************************************************************
    Sub btnPrint_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Print_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Autoprint Or PopupWindowStyle.NoToolbar)
        ResetSum()
        BindData()
    End Sub

    Sub btnPreview_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Preview_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Fullscreen)
        ResetSum()
        BindData()
    End Sub

    Private Function PrePrint() As printReportUtil
        BindData()

        Dim preview As New printReportUtil

        preview.PageTitle = "Stock Report"
        preview.PageSize = 45
        preview.PageSizeLandscape = 28

        preview.AddReportHeader(repData)
        If ddValuesSelect.SelectedValue = 0 Then
            preview.AddWebGrid(Me.MYGRid)
        ElseIf ddValuesSelect.SelectedValue = 2 Then
            preview.AddWebGrid(Me.MyGridValues)
        End If

        Return preview

    End Function
    '**************************************************************************************************************
    '* End Printing
    '**************************************************************************************************************

    Private Sub ddlineselect_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlineselect.SelectedIndexChanged
        ddProduct.SelectedValue = 0
    End Sub
End Class
