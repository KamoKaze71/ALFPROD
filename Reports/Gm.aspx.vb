Option Explicit On 
Option Strict On

Imports System
Imports System.Data
Imports System.Math
Imports System.Globalization

Imports Wyeth.Utilities
Imports Wyeth.Utilities.Helper
Imports Wyeth.Utilities.DateHandling
Imports Wyeth.Utilities.NumberFormat


Imports Wyeth.Alf.CssStyles
Imports Wyeth.Alf.WyethDropdown
Imports Wyeth.Alf.WyethAppllication
Imports Wyeth.Alf.reportData



Public Class GM
	Inherits System.Web.UI.Page


#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
	Protected WithEvents txtStartDate As System.Web.UI.WebControls.TextBox
	Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
	Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
	Protected WithEvents ddBudget As System.Web.UI.WebControls.DropDownList
	Protected WithEvents ddMonth As System.Web.UI.WebControls.DropDownList
	Protected WithEvents Button1 As System.Web.UI.WebControls.Button
	Protected WithEvents MyGrid_US As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents ReportPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents genReport As System.Web.UI.WebControls.Button
    Protected WithEvents rbl_ROUND As System.Web.UI.HtmlControls.HtmlInputRadioButton
    Protected WithEvents rbl_EXACT As System.Web.UI.HtmlControls.HtmlInputRadioButton
    Protected WithEvents repData As Wyeth.Alf.reportData
    Protected WithEvents Table_gm As System.Web.UI.HtmlControls.HtmlTable
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
    Dim HeaderRow() As String = {"", "Month to Date", "Month to Date monthly", "Month to Date monthly", "Month to Date", "Month to Date", "Month to Date", "Month to Date", "Year to Date", "Year to Date  monthly", "Year to Date  monthly", "Year to Date", "Year to Date", "Year to Date", "Year to Date", "", ""}


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not Page.IsPostBack = True Then
            Me.lblPageTitle.Text = Request.QueryString("PageTitle")

            GetBudgetDD(ddBudget)
            GetMonthSelectDD(ddMonth)


            ddMonth.SelectedValue = Convert.ToDateTime(Application("LastOrderEntry")).ToString("MMM-yyyy", GetMyDTFI())
            bindData()
        End If

        repData.lastOrderDate = LastOfThisMonth(CDate(ddMonth.SelectedValue)).ToString(DATEFORMAT_STRING_REPORT, GetMyDTFI())
        repData.addLine("Report Month", ddMonth.SelectedItem.ToString, True, False)
        repData.addLine("Report Type", ddBudget.SelectedItem.ToString, True, False)
        If rbl_EXACT.Checked Then
            repData.addLine("Numbers", "Decimals", True, False)
        Else
            repData.addLine("Numbers", "1000", True, False)
        End If
    End Sub

    Private Sub bindData()
        Dim str As String
        str = "<style>"
        str += " .blef {border-left:1px solid #000;}"
        str += "</style>"
        RegisterClientScriptBlock("anything", str)

        ' Set default values  for GM Report
        SetGridStyles(MyGrid)
        SetGridStyles(MyGrid_US)

        MyReport.BudgetType = ddBudget.SelectedValue

        MyReport.StartDate = FirstOfThisMonth(CDate(ddMonth.SelectedValue))
        MyReport.EndDate = LastOfThisMonth(CDate(ddMonth.SelectedValue))

       

        MyNFI = CType(Application("MyNFI"), NumberFormatInfo)

        If rbl_ROUND.Checked Then
            MyNFI.NumberDecimalDigits = 0
        Else
            MyNFI.NumberDecimalDigits = 2
        End If


        MyGrid.DataSource = MyReport.GetGMReport
        MyGrid.DataBind()

        MyGrid_US.DataSource = MyReport.GetGMReport
        MyGrid_US.DataBind()

        showGridCols()    ' set columns for BU or BE visible

    End Sub

    Private Sub roundColumn(ByVal dataGridRowCell As TableCell)
        Dim d As Double
        Dim text As String = dataGridRowCell.Text
        If Not text = "&nbsp;" Then
            d = CDbl(text)
            If rbl_ROUND.Checked Then
                d = Round((d / 1000), 0)
            End If
            dataGridRowCell.Text = d.ToString(NUMBER_FORMAT_STRING_EXACT, MyNFI)
        Else
            dataGridRowCell.Text = "-"
        End If
    End Sub

    Private Sub MyGrid_ItemDataBound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound, MyGrid_US.ItemDataBound

        setColumnToolTips(e.Item, MyGrid, HeaderRow)

        '**************** SummenLinie herausheben indem wir eine eigene CSS-Klasse hinzufügen
        If e.Item.Cells(0).Text = "TOTAL" Then
            e.Item.CssClass = "reportTotal"
        End If

        If e.Item.Cells(0).Text = "" Or e.Item.Cells(0).Text = "&nbsp;" Then
            For Each cell As TableCell In e.Item.Cells()
                cell.Text = "&nbsp;"
            Next
        Else

            ' Format Percent Columns
            MyNumberFormat(e.Item.Cells(5), 1, "%")
            MyNumberFormat(e.Item.Cells(6), 1, "%")
            MyNumberFormat(e.Item.Cells(7), 1, "%")
            MyNumberFormat(e.Item.Cells(12), 1, "%")
            MyNumberFormat(e.Item.Cells(13), 1, "%")
            MyNumberFormat(e.Item.Cells(14), 1, "%")



            roundColumn(e.Item.Cells(1))
            roundColumn(e.Item.Cells(2))
            roundColumn(e.Item.Cells(3))
            roundColumn(e.Item.Cells(4))
            roundColumn(e.Item.Cells(8))
            roundColumn(e.Item.Cells(9))
            roundColumn(e.Item.Cells(10))
            roundColumn(e.Item.Cells(11))


        End If

    End Sub

    Private Sub ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportExcel.Click
        bindData()

        Dim tod As String = Date.Today.Day & "-" & Date.Today.Month & "-" & Right(Date.Today.Year.ToString, 2)
        Dim reportHeaderString As String = "<table><tr align=center style='border-bottom:1px solid #000;border-left:1px solid #000'><td align=left style='border:1px solid #fff'><b>{0}</b></td><td colspan=3><b>Month</b></td><td colspan=2><b>%Versus</b></td><td colspan=3><b>Y-T-D</b></td><td colspan=2><b>%Versus</b></td></tr></table>"

        Dim exp As exportToExcel = New exportToExcel(Page)
        'exp.testWithHTML = True
        exp.addLine("<table border=0>")

        exp.addLine("<tr><td align=center><b>GENERAL MANAGER MONTHLY REPORT</b></td></tr>")
        exp.addLine("<tr><td align=center><b>Net Sales Evolution</b></td></tr>")
        exp.addLine("<tr><td align=center style=""mso-number-format:'\@';""><b>(000)</b></td></tr>")
        exp.addLine("<tr><td><br><br></td></tr>")
        exp.addLine("<tr><td><b>Affiliate: Wyeth-Lederle Pharma, Austria</b></td></tr>")
        exp.addLine("<tr><td style=""mso-number-format:'\@';""><b>Month/Year:</b> " & ddMonth.SelectedItem.ToString & "</td></tr>")
        exp.addLine("<tr><td style=""mso-number-format:'\@';""><b>Date Prepared:</b> " & tod & "</td></tr>")
        exp.addLine("<tr><td align=right><b>Schedule B</b></td></tr>")
        exp.addLine("<tr><td><br></td></tr>")

        exp.addLine("<tr><td>")
        exp.addLine("<table border=1 style='border:1px double #000;'><tr><td>")
        exp.addLine(String.Format(reportHeaderString, "EUR"))
        prepareDGforExcel(MyGrid)
        exp.addDataGrid(MyGrid)
        exp.addLine("</td></tr></table>")
        exp.addLine("</td></tr>")

        exp.addLine("<tr><td><br></td></tr>")

        exp.addLine("<tr><td>")
        exp.addLine("<table border=1 style='border:1px double #000;'><tr><td>")
        exp.addLine(String.Format(reportHeaderString, "$US"))
        prepareDGforExcel(MyGrid_US)
        exp.addDataGrid(MyGrid_US)
        exp.addLine("</td></tr></table>")
        exp.addLine("</td></tr>")

        exp.addLine("</table>")
        exp.export()
    End Sub

    '*******************************************************************************************
    '* verändert das Datagrid so, dass es im excel dann so aussieht wie es die kollegen
    '* in den usa haben wollen. rahmen, etc.
    '*******************************************************************************************
    Private Sub prepareDGforExcel(ByRef dg As C1.Web.C1WebGrid.C1WebGrid)

        With dg
            .Columns.Item(1).HeaderText = "Act."
            .Columns.Item(2).HeaderText = "Bud."
            .Columns.Item(4).HeaderText = "LY"

            .Columns.Item(5).HeaderText = "Bud."
            .Columns.Item(7).HeaderText = "LY"

            .Columns.Item(8).HeaderText = "Act."
            .Columns.Item(10).HeaderText = "Bud."
            .Columns.Item(11).HeaderText = "LY"

            .Columns.Item(12).HeaderText = "Bud."
            .Columns.Item(14).HeaderText = "LY"

            Dim i As Integer = 0
            For Each col As C1.Web.C1WebGrid.C1Column In .Columns
                If Not i = 0 Then
                    col.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    col.HeaderStyle.BorderWidth = Unit.Pixel(1)
                    col.HeaderStyle.BorderColor = Color.Black
                End If
                i += 1
            Next

            .DataBind()

            .Columns(5).ItemStyle.CssClass = "blef"
            .Columns(8).ItemStyle.CssClass = "blef"
            .Columns(12).ItemStyle.CssClass = "blef"

            'Totals formatierung
            .Items(.Items.Count - 1).Font.Bold = True
            .Items(.Items.Count - 1).Style.Add("border-top", "1px solid #000")

            Dim c As String
            For Each item As C1.Web.C1WebGrid.C1GridItem In dg.Items
                c = item.Cells(0).Text.ToUpper
                If c = "LOCAL PRODUCTS" Or c = "ALL OTHERS" Or c = "GLOBAL PRODUCTS" Then
                    item.Font.Bold = True
                    item.Cells(0).Font.Underline = True
                End If

                item.Cells(0).Style.Add("border-right", "1px solid #000")
                item.Cells(4).Style.Add("border-right", "1px solid #000")
                item.Cells(7).Style.Add("border-right", "1px solid #000")
                item.Cells(11).Style.Add("border-right", "1px solid #000")
            Next

            .Items(0).Style.Add("border-top", "1px solid #000")
        End With
    End Sub

    Private Sub showGridCols()

        If ddBudget.SelectedValue = "BU" Then

            MyGrid.Columns(2).Visible = True    ' Set Budget Column visible
            MyGrid.Columns(5).Visible = True    ' Set Budget % Column visible
            MyGrid.Columns(10).Visible = True
            MyGrid.Columns(12).Visible = True

            MyGrid.Columns(3).Visible = False
            MyGrid.Columns(6).Visible = False
            MyGrid.Columns(9).Visible = False
            MyGrid.Columns(13).Visible = False


            MyGrid_US.Columns(2).Visible = True    ' Set Budget Column visible
            MyGrid_US.Columns(5).Visible = True    ' Set Budget % Column visible
            MyGrid_US.Columns(10).Visible = True
            MyGrid_US.Columns(12).Visible = True

            MyGrid_US.Columns(3).Visible = False
            MyGrid_US.Columns(6).Visible = False
            MyGrid_US.Columns(9).Visible = False
            MyGrid_US.Columns(13).Visible = False

        Else  ' BE is selected

            MyGrid.Columns(2).Visible = False    ' hide BU Columns 
            MyGrid.Columns(5).Visible = False    '  hide BU % Columns  
            MyGrid.Columns(10).Visible = False
            MyGrid.Columns(12).Visible = False

            MyGrid.Columns(3).Visible = True    'show BE Columns
            MyGrid.Columns(6).Visible = True    'show BE Columns
            MyGrid.Columns(9).Visible = True    'show BE Columns
            MyGrid.Columns(13).Visible = True


            MyGrid_US.Columns(2).Visible = False   ' hide BU Columns 
            MyGrid_US.Columns(5).Visible = False   ' hide BU% Columns 
            MyGrid_US.Columns(10).Visible = False
            MyGrid_US.Columns(12).Visible = False

            MyGrid_US.Columns(3).Visible = True     'show BE Columns for US Grid
            MyGrid_US.Columns(6).Visible = True     'show BE Columns for US Grid
            MyGrid_US.Columns(9).Visible = True     'show BE Columns for US Grid
            MyGrid_US.Columns(13).Visible = True
        End If
    End Sub

    Private Sub genReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles genReport.Click
        bindData()
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

        Dim reportHeaderString As String = "<tr align=center><td align=left class=currency style='border:0px;'>Currency: {0}</td><td class=head colspan=5 style='border:0px;'><b>Month to Date</b></td><td class=head colspan=5 style='border:0px;'><b>Year to Date</b></td></tr>"
        Dim preview As New printReportUtil

        preview.PageTitle = "GM Report"
        preview.PageSize = 48
        preview.PageSizeLandscape = 28
        'preview.DefaultOrientation = Orientation.Landscape

        preview.AddReportHeader(repData)
        preview.AddWebGrid(String.Format(reportHeaderString, "EUR"), Me.MyGrid)
        preview.AddWebGrid(String.Format(reportHeaderString, "USD"), Me.MyGrid_US)

        Return preview
    End Function
    '**************************************************************************************************************
    '* End Printing
    '**************************************************************************************************************

End Class


