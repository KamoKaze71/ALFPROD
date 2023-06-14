Option Explicit On 
Option Strict On

Imports System
Imports System.Data
Imports System.Math
Imports System.Globalization
Imports Wyeth.Utilities.Helper


Imports Wyeth.Utilities
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
	Protected WithEvents ReportPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents ddBudget As System.Web.UI.WebControls.DropDownList
	Protected WithEvents DailySalesTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents rbl_ROUND As System.Web.UI.HtmlControls.HtmlInputRadioButton
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents btnGenRep As System.Web.UI.WebControls.Button
    Protected WithEvents rbl_EXACT As System.Web.UI.HtmlControls.HtmlInputRadioButton
    Protected WithEvents repData As reportData


	'NOTE: The following placeholder declaration is required by the Web Form Designer.
	'Do not delete or move it.
	Private designerPlaceholderDeclaration As System.Object

	Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
		'CODEGEN: This method call is required by the Web Form Designer
		'Do not modify it using the code editor.
		InitializeComponent()
	End Sub

#End Region


	Dim myReport As New Report
	Dim bol_round As Boolean = True
	Dim MyNFI As New NumberFormatInfo
	Dim d, sum1, sum2, sum3, sum4, sum5, sum6, sum7, sum8 As Double


	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		' Populates Date Selector Listbox for quick selection of the transaction month

        sum3 = 0
        sum4 = 0
        sum5 = 0
        sum6 = 0
        sum7 = 0
        sum8 = 0

        If Not Page.IsPostBack Then

            Me.lblPageTitle.Text = Request.QueryString("PageTitle")
            GetBudgetDD(ddBudget)
            BindData()
        Else
            repData.lastOrderDate = txtStartDate.Text
        End If

        repData.addLine("Report for Day", txtStartDate.Text, True, False)
        repData.addLine("Report Type", ddBudget.SelectedItem.ToString, True, False)
        If rbl_EXACT.Checked Then
            repData.addLine("Numbers", "Decimals", True, False)
        Else
            repData.addLine("Numbers", "1000", True, False)
        End If
	End Sub


	Private Sub BindData()
		Dim dat As Date = Date.Today

		MyNFI = CType(Application("MyNFI"), NumberFormatInfo)

		If Me.txtStartDate.Text = "" Then
            Me.txtStartDate.Text = repData.lastOrderDate
        End If

        If ddBudget.SelectedValue = "BU" Then
            MyGrid.Columns(4).HeaderText = "Budget"
            MyGrid.Columns(6).HeaderText = "Budget"
            MyGrid.Columns(8).HeaderText = "Budget"
        Else
            MyGrid.Columns(4).HeaderText = "Best Estimate"
            MyGrid.Columns(6).HeaderText = "Best Estimate"
            MyGrid.Columns(8).HeaderText = "Best Estimate"
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

        Me.MyGrid.DataSource = myReport.GetDailySales
        Me.MyGrid.DataBind()


	End Sub

	Private Sub Item_created(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated

        setColumnToolTips(e.Item, MyGrid)
		Dim i As Integer = 0

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.GroupFooter Then

            For Each cell As TableCell In e.Item.Cells()
                cell.Font.Bold = True
            Next


            d = CDbl(e.Item.Cells(1).Text)
            If bol_round Then
                d = Round((d / 1000), 0)
            End If
            e.Item.Cells(1).Text = d.ToString(NUMBER_FORMAT_STRING_EXACT, MyNFI)


            d = CDbl(e.Item.Cells(2).Text)
            If bol_round Then
                d = Round((d / 1000), 0)
            End If
            e.Item.Cells(2).Text = d.ToString(NUMBER_FORMAT_STRING_EXACT, MyNFI)


            d = CDbl(e.Item.Cells(3).Text)
            If bol_round Then
                d = Round((d / 1000), 0)
            End If
            e.Item.Cells(3).Text = d.ToString(NUMBER_FORMAT_STRING_EXACT, MyNFI)


            d = CDbl(e.Item.Cells(4).Text)
            If bol_round Then
                d = Round((d / 1000), 0)
            End If
            e.Item.Cells(4).Text = d.ToString(NUMBER_FORMAT_STRING_EXACT, MyNFI)

            d = CDbl(e.Item.Cells(5).Text)
            If bol_round Then
                d = Round((d / 1000), 0)
            End If
            e.Item.Cells(5).Text = d.ToString(NUMBER_FORMAT_STRING_EXACT, MyNFI)

            d = CDbl(e.Item.Cells(6).Text)
            If bol_round Then
                d = Round((d / 1000), 0)
            End If
            e.Item.Cells(6).Text = d.ToString(NUMBER_FORMAT_STRING_EXACT, MyNFI)


        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Header Then
            e.Item.CssClass = "head"

        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Separator Then
            i = 0
        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Footer Then
            e.Item.Cells(2).Text = "TOTAL AUSTRIA"
            e.Item.CssClass = "reportTotal"

            If bol_round Then



                sum3 = Round((sum3 / 1000), 0)
                sum4 = Round((sum4 / 1000), 0)
                sum5 = Round((sum5 / 1000), 0)
                sum6 = Round((sum6 / 1000), 0)
                sum7 = Round((sum7 / 1000), 0)
                sum8 = Round((sum8 / 1000), 0)


            End If

            e.Item.Cells(3).Text = sum3.ToString(NUMBER_FORMAT_STRING_EXACT, MyNFI)
            e.Item.Cells(4).Text = sum4.ToString(NUMBER_FORMAT_STRING_EXACT, MyNFI)
            e.Item.Cells(5).Text = sum5.ToString(NUMBER_FORMAT_STRING_EXACT, MyNFI)
            e.Item.Cells(6).Text = sum6.ToString(NUMBER_FORMAT_STRING_EXACT, MyNFI)
            e.Item.Cells(7).Text = sum7.ToString(NUMBER_FORMAT_STRING_EXACT, MyNFI)
            e.Item.Cells(8).Text = sum8.ToString(NUMBER_FORMAT_STRING_EXACT, MyNFI)
            End If

	End Sub

	Private Sub txtStartDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtStartDate.TextChanged
		Validate()
		If Page.IsValid() Then
			BindData()
		End If
    End Sub

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
        sum3 += roundColumn(e.Item.Cells(3))
        sum4 += roundColumn(e.Item.Cells(4))
        sum5 += roundColumn(e.Item.Cells(5))
        sum6 += roundColumn(e.Item.Cells(6))
        sum7 += roundColumn(e.Item.Cells(7))
        sum8 += roundColumn(e.Item.Cells(8))

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
        BindData()
    End Sub
End Class

