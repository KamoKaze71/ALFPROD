Imports Wyeth.Utilities.DateHandling
Imports Wyeth.Alf.WyethDropdown
Imports Wyeth.Alf.CssStyles
Imports Wyeth.Utilities.NumberFormat
Imports Wyeth.Utilities.Helper

Public Class JDEHistory
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents ddmonth As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnGenRep As System.Web.UI.WebControls.Button
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents GridPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
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

    Dim MyReport As New Report

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Page.IsPostBack = True Then
        Else
            GetMonthSelectToProcessMonth(ddmonth, CDate(Session("FinalJDEMonth")))
            ddmonth.SelectedValue = Convert.ToDateTime(Session("FinalJDEMonth")).ToString("MMM-yyyy", GetMyDTFI())
            BindData()
        End If

    End Sub


    Private Sub BindData()

        SetGridStylesGroup(MyGrid)
        MyReport.CtryID = Session("country_id")
        MyReport.StartDate = FirstOfThisMonth(CDate(ddmonth.SelectedValue))
        MyGrid.DataSource = MyReport.GetJDEReport()
        MyGrid.DataBind()

    End Sub



    Private Sub btnGenRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRep.Click
        BindData()
    End Sub


    Private Sub MyGrid_Databound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound

        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("AMOUNT_DEBIT"))), 2)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("AMOUNT_CREDIT"))), 2)

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

        Dim reportHeaderString As String = "<tr align=center><td align=left class=currency style='border:0px;'>Currency: {0}</td><td class=head colspan=5 style='border:0px;'><b>Month to Date</b></td><td class=head colspan=5 style='border:0px;'><b>Year to Date</b></td></tr>"
        Dim preview As New printReportUtil

        preview.PageTitle = "JDE Export"
        preview.PageSize = 48
        preview.PageSizeLandscape = 28
        'preview.DefaultOrientation = Orientation.Landscape

        preview.AddReportHeader(repData)
        preview.AddWebGrid(Me.MyGrid)


        Return preview
    End Function
    '**************************************************************************************************************
    '* End Printing
    '**************************************************************************************************************

    Private Sub ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportExcel.Click
        Try
            Dim filename, str As String

            str = "<html><body><table>"

            For Each Griditem As C1.Web.C1WebGrid.C1GridItem In MyGrid.Items

                Dim MyJdeFile As New JDEFile

                MyJdeFile.CostCenter = Trim(Griditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("acre_debit_costcenter"))).Text)
                MyJdeFile.Department = Trim(Griditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("acre_debit_department"))).Text)
                MyJdeFile.BuisinessUnit = Trim(Griditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("debit_country_company"))).Text)
                MyJdeFile.ObjectAccount = Griditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("acre_debit_account"))).Text
                MyJdeFile.Subsidiary = Griditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("acre_debit_subsidiary"))).Text
                MyJdeFile.Amount = Griditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("amount_debit"))).Text
                MyJdeFile.ExplainationAlpha = Griditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("acre_name"))).Text
                MyJdeFile.ExplainationRemark = "ALF" & (Session("LastMonthApproved"))
                MyJdeFile.GLDateDay = Day(CDate(Session("LastMonthApproved")))
                MyJdeFile.GLDateMonth = Month(CDate(Session("LastMonthApproved")))
                MyJdeFile.GLDateYear = Year(CDate(Session("LastMonthApproved"))).ToString.Substring(2, 2)
                str += MyJdeFile.GetLineDebit() & vbNewLine


                MyJdeFile.CostCenter = Trim(Griditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("acre_credit_costcenter"))).Text)
                MyJdeFile.Department = Trim(Griditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("acre_credit_department"))).Text)
                MyJdeFile.ObjectAccount = Griditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("acre_credit_account"))).Text
                MyJdeFile.BuisinessUnit = Trim(Griditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("credit_country_company"))).Text)
                MyJdeFile.Subsidiary = Griditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("acre_credit_subsidiary"))).Text
                MyJdeFile.Amount = Griditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("amount_credit"))).Text
                MyJdeFile.ExplainationAlpha = Griditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("acre_name"))).Text
                MyJdeFile.ExplainationRemark = "ALF" & (Session("LastMonthApproved"))
                MyJdeFile.GLDateDay = Day(CDate(Session("LastMonthApproved")))
                MyJdeFile.GLDateMonth = Month(CDate(Session("LastMonthApproved")))
                MyJdeFile.GLDateYear = Year(CDate(Session("LastMonthApproved"))).ToString.Substring(2, 2)
                str += MyJdeFile.GetLineCredit & vbNewLine
            Next

            str = str & "<Table></body></html>"

            Dim myMep As New MEPData
            myMep.SetJDEProcessed(Convert.ToDateTime(Session("LastMonthApproved")), Session("user_id"), Session("country_id"))
          

            'filename = "JDE_EXPORT_" & Convert.ToDateTime(Session("LastMonthApproved")).ToString(DATEFORMAT_STRING, GetMyDTFI()) & ".prn"
            'Response.Clear()
            'Response.AddHeader("Content-Disposition", "attachment; filename=" & filename)
            'Response.ContentType = "Text/plain"
            'Response.Write(str)

            Response.Clear()
            filename = "JDE_EXPORT_" & LastOfThisMonth(Convert.ToDateTime(Session("LastMonthApproved")).ToString(DATEFORMAT_STRING, GetMyDTFI())) & ".xls"
            Response.AddHeader("Content-Disposition", "attachment; filename=" & filename)
            Response.ContentType = "Text/xls"
            Response.Buffer = True
            Response.Write(str)
            Response.Flush()
            Response.End()


        Catch ex As Exception
          Finally
            '  Session("JDEGrid") = Nothing
        End Try

     
    End Sub
End Class
