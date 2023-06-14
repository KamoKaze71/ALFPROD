Imports Wyeth.Utilities.DateHandling
Imports Wyeth.Alf.WyethDropdown
Imports Wyeth.Alf.CssStyles
Imports Wyeth.Utilities.NumberFormat
Imports Wyeth.Utilities.Helper
Public Class JDEProcessing
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents btnGenRep As System.Web.UI.WebControls.Button
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents GridPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents repData As reportData
    Protected WithEvents prtControl As printReportCtl
    Protected WithEvents lblProcessMonth As System.Web.UI.WebControls.Label
    Protected WithEvents lblItems As System.Web.UI.WebControls.Label
    Protected WithEvents lblstartProcessing As System.Web.UI.WebControls.Label
    Protected WithEvents btn_process_jde As System.Web.UI.WebControls.Button
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

    Dim MyReport As New Report
    Dim MyMep As New MEPData
    Dim MyJs As New JSPopUp(Me)


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MyJs.Height = 300
        MyJs.Width = 350
        MyJs.PageURL = "JDEProcessingConfirmation.aspx"
        MyJs.AddPopupToControl(btn_process_jde)

        If Page.IsPostBack = True Then
        Else
            MyMep.setProcessMonth()
            If MyMep.CheckForFinanceApproval(Convert.ToDateTime(Session("LastProcessMonth")), Session("country_id")) = False Then
                lblProcessMonth.Text = "Processing Month:<Font color=red> " & Convert.ToString(Session("LastProcessMonth")).Substring(0, 7) & "</Font>"
                lblstartProcessing.Text = "Start JDE Export Processing for:<Font color=red> " & Convert.ToString(Session("LastProcessMonth")).Substring(0, 7) & "</Font>"

            Else
                lblProcessMonth.Text = "The Month:<Font color=red> " & Convert.ToString(Session("LastProcessMonth")).Substring(0, 7) & "</Font> has not yet been approved by Finance"
                lblstartProcessing.Text = "JDE Export for:<Font color=red> " & Convert.ToString(Session("LastProcessMonth")).Substring(0, 7) & "</Font> is not possible"
                btn_process_jde.Visible = False

            End If
            BindData()
        End If

    End Sub


    Private Sub BindData()

        SetGridStylesGroup(MyGrid)
        MyReport.CtryID = Session("country_id")
        MyReport.StartDate = FirstOfThisMonth(Session("LastMonthApproved"))
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

        ' preview.AddReportHeader(repData)
        preview.AddWebGrid(Me.MyGrid)


        Return preview
    End Function
    '**************************************************************************************************************
    '* End Printing
    '**************************************************************************************************************

    'Private Sub ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportExcel.Click
    '    Dim filename, str As String
    '    Dim MyJde As New JDEFile
    '    For Each Griditem As C1.Web.C1WebGrid.C1GridItem In MyGrid.Items

    '        Dim MyJdeFile As New JDEFile

    '        MyJdeFile.CostCenter = Trim(Griditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("acre_debit_costcenter"))).Text)
    '        MyJdeFile.Department = Trim(Griditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("acre_debit_department"))).Text)
    '        MyJdeFile.BuisinessUnit = Trim(Griditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("debit_country_company"))).Text)
    '        MyJdeFile.ObjectAccount = Griditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("acre_debit_account"))).Text
    '        MyJdeFile.Subsidiary = Griditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("acre_debit_subsidiary"))).Text
    '        MyJdeFile.Amount = Griditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("amount_debit"))).Text
    '        MyJdeFile.ExplainationAlpha = Griditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("acre_name"))).Text
    '        MyJdeFile.ExplainationRemark = "ALF" & (Session("LastMonthApproved"))
    '        MyJdeFile.GLDateDay = Day(CDate(Session("LastMonthApproved")))
    '        MyJdeFile.GLDateMonth = Month(CDate(Session("LastMonthApproved")))
    '        MyJdeFile.GLDateYear = Year(CDate(Session("LastMonthApproved"))).ToString.Substring(2, 2)
    '        str += MyJdeFile.GetLine() & vbNewLine


    '        MyJdeFile.CostCenter = Trim(Griditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("acre_credit_costcenter"))).Text)
    '        MyJdeFile.Department = Trim(Griditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("acre_credit_department"))).Text)
    '        MyJdeFile.ObjectAccount = Griditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("acre_credit_account"))).Text
    '        MyJdeFile.BuisinessUnit = Trim(Griditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("credit_country_company"))).Text)
    '        MyJdeFile.Subsidiary = Griditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("acre_credit_subsidiary"))).Text
    '        MyJdeFile.Amount = Griditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("amount_credit"))).Text
    '        MyJdeFile.ExplainationAlpha = Griditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("acre_name"))).Text
    '        MyJdeFile.ExplainationRemark = "ALF" & (Session("LastMonthApproved"))
    '        MyJdeFile.GLDateDay = Day(CDate(Session("LastMonthApproved")))
    '        MyJdeFile.GLDateMonth = Month(CDate(Session("LastMonthApproved")))
    '        MyJdeFile.GLDateYear = Year(CDate(Session("LastMonthApproved"))).ToString.Substring(2, 2)
    '        str += MyJdeFile.GetLine() & vbNewLine
    '    Next

    '    filename = "JDE_EXPORT_" & Convert.ToDateTime(Session("LastMonthApproved")).ToString(DATEFORMAT_STRING, GetMyDTFI()) & ".prn"
    '    Response.Clear()
    '    Response.AddHeader("Content-Disposition", "attachment; filename=" & filename)
    '    Response.ContentType = "Text/plain"
    '    Response.Buffer = True
    '    Response.Write(str)
    '    Response.Flush()
    '    Response.End()
    'End Sub

    Private Sub btn_process_jde_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_process_jde.Click
        BindData()
        Session("JDEGrid") = MyGrid

    End Sub
End Class