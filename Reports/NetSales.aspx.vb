Imports Wyeth.Utilities
Imports Wyeth.Utilities.Helper
Imports Wyeth.Alf.CssStyles
Imports Wyeth.Alf.WyethDropdown
Imports System.Globalization
Imports Wyeth.Utilities.DateHandling
Imports System.Math
Imports Wyeth.Utilities.NumberFormat


Public Class NetSales
	Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
	Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
	Protected WithEvents ddmonth As System.Web.UI.WebControls.DropDownList
	Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
	Protected WithEvents ReportPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents ddBudget As System.Web.UI.WebControls.DropDownList
    Protected WithEvents rbl_ROUND As System.Web.UI.HtmlControls.HtmlInputRadioButton
    Protected WithEvents rbl_EXACT As System.Web.UI.HtmlControls.HtmlInputRadioButton
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
	Protected WithEvents NetSalesTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents btnGenRep As System.Web.UI.WebControls.Button
    Protected WithEvents repData As reportData
    Protected WithEvents btnPrint As System.Web.UI.WebControls.Button
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
	Dim MyNFI As New NumberFormatInfo
	Dim MyDTFI As DateTimeFormatInfo
    Dim bol_round As Boolean = True
    Dim HeaderRow() As String = {"", "", "", "Month to Date", "Month to Date total Monthly", "Month to Date", "Month to Date", "Month to Date", "Year to Date", "Year to Date total Monthly", "Year to Date", "Year to Date", "Year to Date"}
    Dim sum1, sum2, sum3, sum4, sum5, sum6, sum7, sum8, sum9, sum10, sum11 As Double


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' ResetSum()
        If Page.IsPostBack = False Then
            GetBudgetDD(ddBudget)
            ddBudget.SelectedValue = "BE"

            GetMonthSelectDD(ddmonth)
            SetGridStylesGroup(MyGrid)
            Me.lblPageTitle.Text = Request.QueryString("PageTitle")
            ddmonth.SelectedValue = Convert.ToDateTime(Application("LastOrderEntry")).ToString("MMM-yyyy", GetMyDTFI())
            BindData()
        End If

        repData.lastOrderDate = LastOfThisMonth(CDate(ddmonth.SelectedValue))
        repData.addLine("Report Month", ddmonth.SelectedItem.ToString, True, False)
        repData.addLine("Report Type", ddBudget.SelectedItem.ToString, True, False)
        If rbl_EXACT.Checked Then
            repData.addLine("Numbers", "Decimals", True, False)
        Else
            repData.addLine("Numbers", "1000", True, False)
        End If

    End Sub

    Private Sub BindData()
        MyNFI = CType(Application("MyNFI"), NumberFormatInfo)
        MyDTFI = CType(Application("MyDTI"), DateTimeFormatInfo)

        If ddBudget.SelectedValue = "BU" Then
            MyGrid.Columns(4).HeaderText = "Budget"
            MyGrid.Columns(5).HeaderText = "% vs BU"

            MyGrid.Columns(9).HeaderText = "Budget"
            MyGrid.Columns(10).HeaderText = "% vs BU"

        Else
            MyGrid.Columns(4).HeaderText = "PIA"
            MyGrid.Columns(5).HeaderText = "% vs PIA"

            MyGrid.Columns(9).HeaderText = "PIA"
            MyGrid.Columns(10).HeaderText = "% vs PIA"
        End If

        If rbl_ROUND.Checked Then
            bol_round = True
        Else
            bol_round = False
        End If

        If bol_round = True Then
            MyNFI.NumberDecimalDigits = 0
        Else
            MyNFI.NumberDecimalDigits = 2
        End If
        MyGrid.Columns(1).GroupInfo.OutlineMode = C1.Web.C1WebGrid.OutlineModeEnum.StartCollapsed
        MyGrid.Columns(1).Visible = False
        MyGrid.Columns(1).GroupInfo.Position = C1.Web.C1WebGrid.GroupPositionEnum.Footer
        MyGrid.Columns(1).GroupInfo.FooterText = "TOTAL {0}"

        MyReport.BudgetType = ddBudget.SelectedValue
        MyReport.StartDate = ddmonth.SelectedValue
        MyGrid.EnableViewState = True
        MyGrid.DataSource = MyReport.GetNetSales()
        MyGrid.DataBind()

    End Sub

    Private Function roundColumn(ByVal dataGridRowCell As TableCell) As Double
        Dim d As Double
        d = CDbl(dataGridRowCell.Text)
        If bol_round Then
            d = Round((d / 1000), 2)
        End If
        dataGridRowCell.Text = d.ToString(NUMBER_FORMAT_STRING_EXACT, MyNFI)
        Return d
    End Function

    Private Sub FormatTOPercent(ByVal dataGridRowCell As TableCell)
        Dim d As Double = CDbl(dataGridRowCell.Text)
        d = d / 100
        dataGridRowCell.Text = d.ToString(NUMBER_FORMAT_STRING_PERCENT, MyNFI)
    End Sub

    Private Sub MyGrid_Databound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound
        sum3 += roundColumn(e.Item.Cells(3))
        sum4 += roundColumn(e.Item.Cells(4))
        sum6 += roundColumn(e.Item.Cells(6))
        sum8 += roundColumn(e.Item.Cells(8))
        sum9 += roundColumn(e.Item.Cells(9))
        sum11 += roundColumn(e.Item.Cells(11))

        'Percent Columns
        MyNumberFormat(e.Item.Cells(5), 1, "%")
        MyNumberFormat(e.Item.Cells(7), 1, "%")
        MyNumberFormat(e.Item.Cells(10), 1, "%")
        MyNumberFormat(e.Item.Cells(12), 1, "%")
    End Sub

    Private Overloads Sub divideMe(ByVal toCell As TableCell, ByVal div1 As TableCell, ByVal div2 As TableCell)

        Dim d1 As Double = MyNumberFormat(div1.Text, 2)
        Dim d2 As Double = MyNumberFormat(div2.Text, 2)

        'wir müssen die 0er abfangen da sonst einen division durch 0 entsteht oder ein unendlich wert
        'rauskommt. somit abfangen!
        If Not d1 = 0 And Not d2 = 0 Then
            Dim tot As Double = -1 + (d1 / d2)
            toCell.Text = tot.ToString(NUMBER_FORMAT_STRING_PERCENT, MyNFI)
        Else
            toCell.Text = "-"
        End If
    End Sub
    Private Overloads Sub divideMe(ByVal toCell As TableCell, ByVal div1 As Double, ByVal div2 As Double)

        Dim d1 As Double = div1
        Dim d2 As Double = div2

        'wir müssen die 0er abfangen da sonst einen division durch 0 entsteht oder ein unendlich wert
        'rauskommt. somit abfangen!
        If Not d1 = 0 And Not d2 = 0 Then
            Dim tot As Double = -1 + (d1 / d2)
            toCell.Text = tot.ToString(NUMBER_FORMAT_STRING_PERCENT, MyNFI)
        Else
            toCell.Text = "-"
        End If
    End Sub

    Private Sub Item_created(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated
        Dim i As Integer = 0
        Dim d As Double

        setColumnToolTips(e.Item, MyGrid, 2, HeaderRow)
        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.GroupFooter Then

            'Prozente berechnen. Die aufsummierungen werden  automatisch vom Grid übernommen.
            'Die Quer-berechnung (%) in der Gruppen-Zeile muss jedoch währen der laufzeit getätigt werden.
            e.Item.HorizontalAlign = HorizontalAlign.Right
            divideMe(e.Item.Cells(5), e.Item.Cells(1), e.Item.Cells(4))
            divideMe(e.Item.Cells(3), e.Item.Cells(1), e.Item.Cells(2))
            divideMe(e.Item.Cells(8), e.Item.Cells(6), e.Item.Cells(7))
            divideMe(e.Item.Cells(10), e.Item.Cells(6), e.Item.Cells(9))

            For Each cell As TableCell In e.Item.Cells()
                If i > 0 Then

                    If e.Item.Cells(i).Text <> "&nbsp;" And e.Item.Cells(i).Text <> "" And Not e.Item.Cells(i).Text.EndsWith("%") And Not e.Item.Cells(i).Text = "-" Then
                        d = CDbl(e.Item.Cells(i).Text)
                        If bol_round Then
                            d = Round((d / 1000), 0)
                        End If
                        e.Item.Cells(i).Text = d.ToString(NUMBER_FORMAT_STRING_EXACT, MyNFI)
                    End If
                End If
                i = i + 1
            Next

        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Separator Then
            i = 0

        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Footer Then
            'percent(totals)

            e.Item.CssClass = "reportTotal"
            e.Item.Cells(2).Text = "TOTAL AUSTRIA"
            e.Item.Cells(2).HorizontalAlign = HorizontalAlign.Left
            e.Item.Cells(3).Text = sum3.ToString(NUMBER_FORMAT_STRING_EXACT, MyNFI)
            e.Item.Cells(4).Text = sum4.ToString(NUMBER_FORMAT_STRING_EXACT, MyNFI)
            e.Item.Cells(6).Text = sum6.ToString(NUMBER_FORMAT_STRING_EXACT, MyNFI)
            e.Item.Cells(8).Text = sum8.ToString(NUMBER_FORMAT_STRING_EXACT, MyNFI)
            e.Item.Cells(9).Text = sum9.ToString(NUMBER_FORMAT_STRING_EXACT, MyNFI)
            e.Item.Cells(11).Text = sum11.ToString(NUMBER_FORMAT_STRING_EXACT, MyNFI)

            divideMe(e.Item.Cells(5), sum3, sum4)
            divideMe(e.Item.Cells(7), sum3, sum6)
            divideMe(e.Item.Cells(10), sum8, sum9)
            divideMe(e.Item.Cells(12), sum8, sum11)

        End If
    End Sub


    Private Sub ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportExcel.Click

        BindData()

        Dim reportHeaderString As String = "<table><tr align=center><td align=left>{0}</td><td colspan=5><b>Month to Date</b></td><td colspan=5><b>Year to Date</b></td></tr></table>"

        Dim exp As exportToExcel = New exportToExcel(Page)
        exp.title = lblPageTitle.Text
        exp.showReportData = repData
        exp.addLine(String.Format(reportHeaderString, "EUR"))
        exp.addDataGrid(MyGrid)
        exp.export()
    End Sub

    Private Sub btnGenRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRep.Click
        BindData()
    End Sub

    Private Sub ResetSum()
        sum1 = 0 : sum2 = 0 : sum3 = 0 : sum4 = 0 : sum5 = 0 : sum6 = 0 : sum7 = 0 : sum8 = 0 : sum9 = 0
        sum10 = 0 : sum11 = 0
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

        Dim reportHeaderString As String = "<tr align=center><td align=left class=currency style='border: 0px;'>Currency: EUR</td><td class=head colspan=5 style='border: 0px;'><b>Month to Date</b></td><td class=head colspan=5 style='border: 0px;'><b>Year to Date</b></td></tr>"
        Dim preview As New printReportUtil

        preview.PageTitle = "Net Sales Report"
        preview.PageSize = 46

        preview.AddReportHeader(repData)
        preview.AddWebGrid(reportHeaderString, Me.MyGrid)

        Return preview

    End Function
    '**************************************************************************************************************
    '* End Printing
    '**************************************************************************************************************
End Class
