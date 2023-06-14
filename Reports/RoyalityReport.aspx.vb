Option Explicit On 
Option Strict On

Imports Wyeth.Alf.CssStyles
Imports Wyeth.Utilities.Helper


Public Class RoyalityReport
	Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
	Protected WithEvents ddInvoicerSelect As System.Web.UI.WebControls.DropDownList
	Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
	Protected WithEvents ddYearSelect As System.Web.UI.WebControls.DropDownList
	Protected WithEvents myGridUnits As C1.Web.C1WebGrid.C1WebGrid
	Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents ReportPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents btnGenRep As System.Web.UI.WebControls.Button
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents repData As reportData
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


	Dim MyProduct As New WyethProduct
	Dim MyReport As New Report
	Dim myDataView As DataView

	Dim sumJan As Double
	Dim sumFeb As Double
	Dim sumMar As Double
	Dim sumApr As Double
	Dim sumMay As Double
	Dim sumJun As Double
	Dim sumJul As Double
	Dim sumAug As Double
	Dim SumSep As Double
	Dim sumOct As Double
	Dim sumNov As Double
	Dim sumDec As Double



	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		'Put user code to initialize the page here
        If Not Page.IsPostBack Then
            Me.lblPageTitle.Text = Request.QueryString("PageTitle")
            MyProduct.ProdCtryId = CInt(Session("country_id"))
            ddYearSelect.Items.Add(CStr(Year(Now)))
            ddYearSelect.Items.Add(CStr(Year(Now) - 1))
            ddYearSelect.Items.Add(CStr(Year(Now) - 2))
            ddYearSelect.SelectedValue = CStr(Year(Now))
            ddYearSelect.DataBind()

            ddInvoicerSelect.DataSource = MyProduct.GetInvoicer()
            ddInvoicerSelect.DataTextField = "Prod_invoicer_code"
            ddInvoicerSelect.DataValueField = "Prod_invoicer_code"


            ddInvoicerSelect.DataBind()

            bindData()
        End If



	End Sub
	Private Sub bindData()
		MyProduct.ProdCtryId = CInt(Session("country_id"))
        MyReport.CtryID = CInt(Session("country_id"))

		
        'If ddInvoicerSelect.SelectedValue = "" Then
        '	ddInvoicerSelect.SelectedIndex = 0
        'Else
        '	ddInvoicerSelect.SelectedValue = ddInvoicerSelect.SelectedValue
        'End If

        MyReport.StartDate = CDate("1.1." & ddYearSelect.SelectedValue)
		MyReport.Invoicer = ddInvoicerSelect.SelectedValue

		SetGridStyles(MyGrid)
		SetGridStyles(myGridUnits)


		myGridUnits.DataSource = MyReport.GetRoyalityReportUnit()
		MyGrid.DataSource = MyReport.GetRoyalityReportValue()

		MyGrid.DataBind()
		myGridUnits.DataBind()
	End Sub

	Private Sub ddYearSelect_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddYearSelect.SelectedIndexChanged
		MyReport.CtryID = CInt(Session("country_id"))
		MyReport.Invoicer = ddInvoicerSelect.SelectedValue
        MyReport.StartDate = CDate("1.1." & ddYearSelect.SelectedValue)
		MyGrid.DataSource = MyReport.GetRoyalityReportValue()
		MyGrid.DataBind()

		myGridUnits.DataSource = MyReport.GetRoyalityReportUnit()
		myGridUnits.DataBind()
		'bindData()
	End Sub

	Private Sub ddInvoicerSelect_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddInvoicerSelect.SelectedIndexChanged
		MyReport.CtryID = CInt(Session("country_id"))
        MyReport.StartDate = CDate("1.1." & ddYearSelect.SelectedValue)
		MyReport.Invoicer = ddInvoicerSelect.SelectedValue
		MyGrid.DataSource = MyReport.GetRoyalityReportValue()
		MyGrid.DataBind()
		myGridUnits.DataSource = MyReport.GetRoyalityReportUnit()
		myGridUnits.DataBind()
		'bindData()
	End Sub

	Private Sub MyGrid_ItemCreated(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated, myGridUnits.ItemCreated


		' add sums to footer row
		If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Footer Then

			For Each cell As TableCell In e.Item.Cells()
				cell.Font.Bold = True
			Next

			e.Item.Cells(4).Text = sumJan.ToString(Wyeth.Utilities.Helper.NUMBER_FORMAT_STRING)
			e.Item.Cells(5).Text = sumFeb.ToString(Wyeth.Utilities.Helper.NUMBER_FORMAT_STRING)
			e.Item.Cells(6).Text = sumMar.ToString(Wyeth.Utilities.Helper.NUMBER_FORMAT_STRING)
			e.Item.Cells(7).Text = sumApr.ToString(Wyeth.Utilities.Helper.NUMBER_FORMAT_STRING)
			e.Item.Cells(8).Text = sumMay.ToString(Wyeth.Utilities.Helper.NUMBER_FORMAT_STRING)
			e.Item.Cells(9).Text = sumJun.ToString(Wyeth.Utilities.Helper.NUMBER_FORMAT_STRING)
			e.Item.Cells(10).Text = sumJul.ToString(Wyeth.Utilities.Helper.NUMBER_FORMAT_STRING)
			e.Item.Cells(11).Text = sumAug.ToString(Wyeth.Utilities.Helper.NUMBER_FORMAT_STRING)
			e.Item.Cells(12).Text = SumSep.ToString(Wyeth.Utilities.Helper.NUMBER_FORMAT_STRING)
			e.Item.Cells(13).Text = sumOct.ToString(Wyeth.Utilities.Helper.NUMBER_FORMAT_STRING)
			e.Item.Cells(14).Text = sumNov.ToString(Wyeth.Utilities.Helper.NUMBER_FORMAT_STRING)
			e.Item.Cells(15).Text = sumDec.ToString(Wyeth.Utilities.Helper.NUMBER_FORMAT_STRING)

            ResetSum()

		End If

	End Sub


	Private Sub MyGrid_ItemDataBound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound, myGridUnits.ItemDataBound


		If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Or e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Then

			sumJan = sumJan + CDbl(e.Item.Cells(4).Text)
			sumFeb = sumFeb + CDbl(e.Item.Cells(5).Text)
			sumMar = sumMar + CDbl(e.Item.Cells(6).Text)
			sumApr = sumApr + CDbl(e.Item.Cells(7).Text)
			sumMay = sumMay + CDbl(e.Item.Cells(8).Text)
			sumJun = sumJun + CDbl(e.Item.Cells(9).Text)
			sumJul = sumJul + CDbl(e.Item.Cells(10).Text)
			sumAug = sumAug + CDbl(e.Item.Cells(11).Text)
			SumSep = SumSep + CDbl(e.Item.Cells(12).Text)
			sumOct = sumOct + CDbl(e.Item.Cells(13).Text)
			sumNov = sumNov + CDbl(e.Item.Cells(14).Text)
			sumDec = sumDec + CDbl(e.Item.Cells(15).Text)

		End If
	End Sub


    Private Sub btnGenRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRep.Click
        bindData()

    End Sub

    Private Sub ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportExcel.Click
           bindData()

        Dim exp As exportToExcel = New exportToExcel(Page)
        exp.title = lblPageTitle.Text
        exp.showReportData = repData
        exp.formatColumnAsString(1)
        exp.addDataGrid(MyGrid)
        exp.export()
    End Sub

    Private Sub ResetSum()
        sumJan = 0 : sumFeb = 0 : sumMar = 0 : sumApr = 0 : sumMay = 0 : sumJun = 0
        sumJul = 0 : sumAug = 0 : SumSep = 0 : sumOct = 0 : sumNov = 0 : sumDec = 0
    End Sub

    '**************************************************************************************************************
    '* For Printing
    '**************************************************************************************************************
    Sub btnPrint_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Print_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Autoprint Or PopupWindowStyle.NoToolbar)
        ResetSum()
        bindData()
    End Sub

    Sub btnPreview_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Preview_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Fullscreen)
        ResetSum()
        bindData()
    End Sub

    Private Function PrePrint() As printReportUtil
        bindData()

        Dim reportHeaderString As String = "<tr align=center><td align=center class=head colspan=16 style='border: 0px;'>{0}</td></tr>"
        Dim preview As New printReportUtil

        preview.PageTitle = "Royality Report "
        preview.PageSizeLandscape = 27
        preview.DefaultOrientation = Orientation.Landscape

        preview.AddReportHeader(repData)
        preview.AddWebGrid(String.Format(reportHeaderString, "Royality Values"), Me.MyGrid)
        preview.AddWebGrid(String.Format(reportHeaderString, "Royality Units"), Me.myGridUnits)

        Return preview

    End Function
    '**************************************************************************************************************
    '* End Printing
    '**************************************************************************************************************
End Class
