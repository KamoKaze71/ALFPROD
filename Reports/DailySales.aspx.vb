Imports System
Imports System.Data
Imports System.Math
Imports System.Globalization
Imports Wyeth.Utilities.Helper


Imports Wyeth.Utilities
Imports Wyeth.Utilities.NumberFormat
Imports Wyeth.Utilities.DateHandling

Imports Wyeth.Alf.CssStyles

Imports Wyeth.Alf.WyethDropdown

Public Class DailySails
    Inherits System.Web.UI.Page
#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
	Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents txtStartDate As Wyeth.Utilities.WyethTextBox
	Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid

	Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel

	Protected WithEvents ddBudget As System.Web.UI.WebControls.DropDownList
	Protected WithEvents DailySalesTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents rbl_ROUND As System.Web.UI.HtmlControls.HtmlInputRadioButton
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents btnGenRep As System.Web.UI.WebControls.Button
    Protected WithEvents rbl_EXACT As System.Web.UI.HtmlControls.HtmlInputRadioButton
    Protected WithEvents repData As reportData
    Protected WithEvents prtControl As printReportCtl
    Protected WithEvents StartImage As System.Web.UI.WebControls.Image
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal

	'NOTE: The following placeholder declaration is required by the Web Form Designer.
	'Do not delete or move it.
	Private designerPlaceholderDeclaration As System.Object

	Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
		'CODEGEN: This method call is required by the Web Form Designer
		'Do not modify it using the code editor.
		InitializeComponent()
	End Sub

#End Region

    Dim myPopup As New JSPopUp(Me.page)

	Dim bol_round As Boolean = True
    Dim MyNFI As New NumberFormatInfo
    Dim HeaderRow() As String = {"", "", "", "Day", "Day", "Day", "Month to Date", "Month to Date Daily", "Month to Date", "Month", "Month", "Month", "Year to Date", "Year to Date Daily", "Year to Date", "Year", "Year", "Year"}
    Dim d, d1, d2, d3, sum1, sum2, sum3, sum4, sum5, sum6, sum7, sum8, sum9, sum10, sum11, sum12, sum13, sum14, sum15, sum16 As Double


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' Populates Date Selector Listbox for quick selection of the transaction month


            Me.MyGrid.ID = "MyGrid"

            ResetSum()

            If Page.IsPostBack Then

            Else
                Me.lblPageTitle.Text = Request.QueryString("PageTitle")
                Me.txtStartDate.Attributes.Add("onkeydown", "return submitButton(btnGenRep);")
                ddBudget.Items.Clear()
                GetBudgetDD(ddBudget)
                ddBudget.SelectedValue = "BE"
                BindData()
                myPopup.AddDatePopupToControl(txtStartDate, StartImage)

            End If

            If rbl_EXACT.Checked Then
                repData.addLine("Numbers", "Decimals", True, False)
            Else
                repData.addLine("Numbers", "1000", True, False)
            End If

        
    End Sub


    Private Sub BindData()

        Dim myReport As New Report
        Dim dat As Date = Date.Today






        MyNFI = CType(Application("MyNFI"), NumberFormatInfo)

        If Me.txtStartDate.Text = "" Then
            Me.txtStartDate.Text = CDate(Application("LastOrderEntry")).ToString("yyyy-MM-dd", GetMyDTFI())
        End If
        repData.EnableViewState = True
        repData.lastOrderDate = txtStartDate.Text
        repData.addLine("Report for Day", txtStartDate.Text, True, False)
        repData.addLine("Report Type", ddBudget.SelectedItem.ToString, True, False)

        If ddBudget.SelectedValue = "BU" Then
            MyGrid.Columns(4).HeaderText = "Budget"
            MyGrid.Columns(5).HeaderText = "%vs BU"
            MyGrid.Columns(7).HeaderText = "Budget"
            MyGrid.Columns(8).HeaderText = "% vs BU"
            MyGrid.Columns(10).HeaderText = "Budget"
            MyGrid.Columns(11).HeaderText = "% vs BU"
            MyGrid.Columns(13).HeaderText = "Budget"
            MyGrid.Columns(14).HeaderText = "% vs BU"
            MyGrid.Columns(16).HeaderText = "Budget"
            MyGrid.Columns(17).HeaderText = "% vs BU"


        Else
            MyGrid.Columns(4).HeaderText = "PIA"
            MyGrid.Columns(5).HeaderText = "% vs PIA"
            MyGrid.Columns(7).HeaderText = "PIA"
            MyGrid.Columns(8).HeaderText = "% vs PIA"
            'MyGrid.Columns(9).HeaderText = "Best Estimate"
            MyGrid.Columns(10).HeaderText = "PIA"
            MyGrid.Columns(11).HeaderText = "% vs PIA"
            MyGrid.Columns(13).HeaderText = "PIA"
            MyGrid.Columns(14).HeaderText = "% vs PIA"
            MyGrid.Columns(16).HeaderText = "PIA"
            MyGrid.Columns(17).HeaderText = "% vs PIA"
        End If

        If rbl_ROUND.Checked Then
            bol_round = True
            MyNFI.NumberDecimalDigits = 0
        Else
            bol_round = False
            MyNFI.NumberDecimalDigits = 2
        End If

        'fill the datagrid with the data 
        'Dim MyDTFI As DateTimeFormatInfo = New CultureInfo("de-AT", False).DateTimeFormat
        myReport.StartDate = Convert.ToDateTime(txtStartDate.Text)
        myReport.BudgetType = ddBudget.SelectedValue
        myReport.WorkDaysMonth = CInt(GetWorkDaysForMonth(Convert.ToDateTime(Me.txtStartDate.Text), CInt(Session("country_id"))).ToString())
        myReport.WorkDaysToday = CInt(GetWorkDaysForMonthToday(Convert.ToDateTime(Me.txtStartDate.Text), CInt(Session("country_id"))).ToString())

        SetGridStylesGroup(MyGrid)


        MyGrid.Columns(1).GroupInfo.OutlineMode = C1.Web.C1WebGrid.OutlineModeEnum.StartCollapsed
        MyGrid.Columns(1).Visible = False
        MyGrid.Columns(1).GroupInfo.Position = C1.Web.C1WebGrid.GroupPositionEnum.Footer
        MyGrid.Columns(1).GroupInfo.FooterText = "TOTAL {0}"
        Response.Write("test")
        Me.MyGrid.EnableViewState = True
        Me.MyGrid.DataSource = myReport.GetDailySales
        Response.Write(myReport.GetDailySales.Count)
        Me.MyGrid.DataBind()
     

    End Sub

    Private Sub Item_created(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated

        setColumnToolTips(e.Item, MyGrid, 2, HeaderRow)
        Dim i As Integer = 0

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.GroupFooter Then

            For Each cell As TableCell In e.Item.Cells()
                cell.Font.Bold = True
            Next



            d = ((MyNumberFormat(e.Item.Cells(1).Text, 2) / MyNumberFormat(e.Item.Cells(2).Text, 2) - 1) * 100)
            e.Item.Cells(3).HorizontalAlign = HorizontalAlign.Right
            e.Item.Cells(3).Text = MyNumberFormat(d, 1, "%")

            d = ((MyNumberFormat(e.Item.Cells(4).Text, 2) / MyNumberFormat(e.Item.Cells(5).Text, 2) - 1) * 100)
            e.Item.Cells(6).HorizontalAlign = HorizontalAlign.Right
            e.Item.Cells(6).Text = MyNumberFormat(d, 1, "%")

            d = ((MyNumberFormat(e.Item.Cells(7).Text, 2) / MyNumberFormat(e.Item.Cells(8).Text, 2) - 1) * 100)
            e.Item.Cells(9).HorizontalAlign = HorizontalAlign.Right
            e.Item.Cells(9).Text = MyNumberFormat(d, 1, "%")

            d = ((MyNumberFormat(e.Item.Cells(10).Text, 2) / MyNumberFormat(e.Item.Cells(11).Text, 2) - 1) * 100)
            e.Item.Cells(12).HorizontalAlign = HorizontalAlign.Right
            e.Item.Cells(12).Text = MyNumberFormat(d, 1, "%")

            d = ((MyNumberFormat(e.Item.Cells(13).Text, 2) / MyNumberFormat(e.Item.Cells(14).Text, 2) - 1) * 100)
            e.Item.Cells(15).HorizontalAlign = HorizontalAlign.Right
            e.Item.Cells(15).Text = MyNumberFormat(d, 1, "%")


            roundColumn(e.Item.Cells(1))
            roundColumn(e.Item.Cells(2))

            roundColumn(e.Item.Cells(4))
            roundColumn(e.Item.Cells(5))

            roundColumn(e.Item.Cells(7))
            roundColumn(e.Item.Cells(8))

            roundColumn(e.Item.Cells(10))
            roundColumn(e.Item.Cells(11))

            roundColumn(e.Item.Cells(13))
            roundColumn(e.Item.Cells(14))

            ' roundColumn(e.Item.Cells(16))
            'roundColumn(e.Item.Cells(17))

        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Header Then
            e.Item.CssClass = "head"

        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Footer Then
            e.Item.Cells(2).Text = "TOTAL AUSTRIA"
            e.Item.CssClass = "reportTotal"



            If bol_round Then

                sum3 = Round((sum3 / 1000), 0)
                sum4 = Round((sum4 / 1000), 0)

                sum6 = Round((sum6 / 1000), 0)
                sum7 = Round((sum7 / 1000), 0)

                sum9 = Round((sum9 / 1000), 0)
                sum10 = Round((sum10 / 1000), 0)

                sum12 = Round((sum12 / 1000), 0)
                sum13 = Round((sum13 / 1000), 0)

                sum15 = Round((sum15 / 1000), 0)
                sum16 = Round((sum16 / 1000), 0)

            End If

            e.Item.Cells(3).Text = sum3.ToString(NUMBER_FORMAT_STRING_EXACT, MyNFI)
            e.Item.Cells(4).Text = sum4.ToString(NUMBER_FORMAT_STRING_EXACT, MyNFI)

            e.Item.Cells(6).Text = sum6.ToString(NUMBER_FORMAT_STRING_EXACT, MyNFI)
            e.Item.Cells(7).Text = sum7.ToString(NUMBER_FORMAT_STRING_EXACT, MyNFI)

            e.Item.Cells(9).Text = sum9.ToString(NUMBER_FORMAT_STRING_EXACT, MyNFI)
            e.Item.Cells(10).Text = sum10.ToString(NUMBER_FORMAT_STRING_EXACT, MyNFI)

            e.Item.Cells(12).Text = sum12.ToString(NUMBER_FORMAT_STRING_EXACT, MyNFI)
            e.Item.Cells(13).Text = sum13.ToString(NUMBER_FORMAT_STRING_EXACT, MyNFI)

            e.Item.Cells(15).Text = sum15.ToString(NUMBER_FORMAT_STRING_EXACT, MyNFI)
            e.Item.Cells(16).Text = sum16.ToString(NUMBER_FORMAT_STRING_EXACT, MyNFI)

            d = ((sum3 / sum4) - 1) * 100
            e.Item.Cells(5).Text = MyNumberFormat(d, 1, "%")

            d = ((sum6 / sum7) - 1) * 100
            e.Item.Cells(8).Text = MyNumberFormat(d, 1, "%")

            d = ((sum9 / sum10) - 1) * 100
            e.Item.Cells(11).Text = MyNumberFormat(d, 1, "%")

            d = ((sum12 / sum13) - 1) * 100
            e.Item.Cells(14).Text = MyNumberFormat(d, 1, "%")

            d = ((sum15 / sum16) - 1) * 100
            e.Item.Cells(17).Text = MyNumberFormat(d, 1, "%")
        End If

    End Sub

    'Private Sub txtStartDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtStartDate.TextChanged
    '    Validate()
    '    If Page.IsValid() Then
    '        BindData()
    '    End If
    'End Sub

    Private Function roundColumn(ByVal dataGridRowCell As TableCell) As Double
        Dim d, d1 As Double
        d = CDbl(dataGridRowCell.Text)
        If bol_round Then
            d1 = Round((d / 1000), 0)
            dataGridRowCell.Text = d1.ToString(NUMBER_FORMAT_STRING_EXACT, MyNFI)

        Else
            dataGridRowCell.Text = d.ToString(NUMBER_FORMAT_STRING_EXACT, MyNFI)
        End If

        Return d
    End Function

    Private Sub Item_DataBound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound
        ' Day
        sum3 += roundColumn(e.Item.Cells(3))
        sum4 += roundColumn(e.Item.Cells(4))

        'MTD
        sum6 += roundColumn(e.Item.Cells(6))
        sum7 += roundColumn(e.Item.Cells(7))

        'ProMonth
        sum9 += roundColumn(e.Item.Cells(9))
        sum10 += roundColumn(e.Item.Cells(10))


        sum12 += roundColumn(e.Item.Cells(12))
        sum13 += roundColumn(e.Item.Cells(13))


        sum15 += roundColumn(e.Item.Cells(15))
        sum16 += roundColumn(e.Item.Cells(16))

        MyNumberFormat(e.Item.Cells(5), 1, "%")
        MyNumberFormat(e.Item.Cells(8), 1, "%")
        MyNumberFormat(e.Item.Cells(11), 1, "%")
        MyNumberFormat(e.Item.Cells(14), 1, "%")
        MyNumberFormat(e.Item.Cells(17), 1, "%")

    End Sub

    Private Sub ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportExcel.Click
        BindData()

        Dim reportHeaderString As String = "<table><tr align=center><td align=left>{0}</td><td colspan=2><b>Day</b></td><td colspan=2><b>Month to Date</b></td><td colspan=2><b>Month</b></td></tr></table>"

        Dim exp As exportToExcel = New exportToExcel(Page)
        exp.title = lblPageTitle.Text
        exp.showReportData = repData
        exp.addLine(String.Format(reportHeaderString, "EUR"))
        exp.addDataGrid(MyGrid)
        exp.export()
    End Sub

    Private Sub btnGenRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRep.Click
        Validate()
        If Page.IsValid = True Then
            MyGrid.Visible = True
            BindData()
        Else
            MyGrid.Visible = False
        End If

    End Sub

    Private Sub ResetSum()
        sum1 = 0 : sum2 = 0 : sum3 = 0 : sum4 = 0 : sum5 = 0 : sum6 = 0 : sum7 = 0 : sum8 = 0 : sum9 = 0
        sum10 = 0 : sum11 = 0 : sum12 = 0 : sum13 = 0 : sum14 = 0 : sum15 = 0 : sum16 = 0
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

        Dim reportHeaderString As String = "<tr align=center><td align=left class=currency style='border:0px;'>Currency: EUR</td>" & _
                                           "<td colspan=3 class=head style='border:0px;'><b>Day</b></td>" & _
                                           "<td colspan=3 class=head style='border:0px;'><b>Month to Date</b></td>" & _
                                           "<td colspan=3 class=head style='border:0px;'><b>Month</b></td>" & _
                                           "<td colspan=3 class=head style='border:0px;'><b>Year to Date</b></td>" & _
                                           "<td colspan=3 class=head style='border:0px;'><b>Projected Year</b></td></tr>"
        Dim preview As New printReportUtil

        preview.PageTitle = "Daily Sales Report"
        preview.PageSizeLandscape = 29
        preview.DefaultOrientation = Orientation.Landscape

        preview.AddReportHeader(repData)
        preview.AddWebGrid(reportHeaderString, Me.MyGrid)

        Return preview

    End Function
    '**************************************************************************************************************
    '* End Printing
    '**************************************************************************************************************

End Class

