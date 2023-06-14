Imports C1.Web.C1WebGrid
Imports Wyeth.Utilities.Helper
Imports Wyeth.Utilities.DateHandling

Public Class JDEProcessingConfirmation
    Inherits Wyeth.Alf.AlfPage


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btn_process_jde As System.Web.UI.WebControls.Button
    Protected WithEvents lblProsessSuccess As System.Web.UI.WebControls.Label
    Protected WithEvents panelSuccess As System.Web.UI.WebControls.Panel
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents lblLastProcessing As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim myGrid As New C1.Web.C1WebGrid.C1WebGrid
    Dim MyJs As New JSPopUp(Me)
    Dim MyMep As New MEPData
    Dim MyInfoView As New DataView

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        myGrid = Session("JDEGrid")

        If MyMep.CheckForJDEProcessing(Convert.ToDateTime(Session("LastMonthApproved")), Session("country_id")) = True Then


        Else

            MyInfoView = MyMep.GetV_PM(Convert.ToDateTime(Session("LastMonthApproved")), Session("country_id"))
            lblLastProcessing.Text = "Last JDE Export File Download by:<bR>" & MyInfoView(0).Item("USER_NAME_JDE_PROCESSED") & " at: " & MyInfoView(0).Item("PM_DATE_JDE_PROCESSED")
            lblLastProcessing.CssClass = "nosuccess"


        End If

        MyJs.ConfirmMessage = "This will Process the Month for JDE Download"
        MyJs.AddGetConfirm(btn_process_jde)

      

    End Sub
    Private Sub btn_process_jde_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_process_jde.Click

        Try
            Dim filename, str As String

            str = "<html><body><table>"
          
            For Each Griditem As C1.Web.C1WebGrid.C1GridItem In myGrid.Items

                Dim MyJdeFile As New JDEFile

                MyJdeFile.CostCenter = Trim(Griditem.Cells(myGrid.Columns.IndexOf(myGrid.Columns.ColumnByName("acre_debit_costcenter"))).Text)
                MyJdeFile.Department = Trim(Griditem.Cells(myGrid.Columns.IndexOf(myGrid.Columns.ColumnByName("acre_debit_department"))).Text)
                MyJdeFile.BuisinessUnit = Trim(Griditem.Cells(myGrid.Columns.IndexOf(myGrid.Columns.ColumnByName("debit_country_company"))).Text)
                MyJdeFile.ObjectAccount = Griditem.Cells(myGrid.Columns.IndexOf(myGrid.Columns.ColumnByName("acre_debit_account"))).Text
                MyJdeFile.Subsidiary = Griditem.Cells(myGrid.Columns.IndexOf(myGrid.Columns.ColumnByName("acre_debit_subsidiary"))).Text
                MyJdeFile.Amount = Griditem.Cells(myGrid.Columns.IndexOf(myGrid.Columns.ColumnByName("amount_debit"))).Text
                MyJdeFile.ExplainationAlpha = Griditem.Cells(myGrid.Columns.IndexOf(myGrid.Columns.ColumnByName("acre_name"))).Text
                MyJdeFile.ExplainationRemark = "ALF" & (Session("LastMonthApproved"))
                MyJdeFile.GLDateDay = Day(CDate(Session("LastMonthApproved")))
                MyJdeFile.GLDateMonth = Month(CDate(Session("LastMonthApproved")))
                MyJdeFile.GLDateYear = Year(CDate(Session("LastMonthApproved"))).ToString.Substring(2, 2)
                str += MyJdeFile.GetLineDebit() & vbNewLine


                MyJdeFile.CostCenter = Trim(Griditem.Cells(myGrid.Columns.IndexOf(myGrid.Columns.ColumnByName("acre_credit_costcenter"))).Text)
                MyJdeFile.Department = Trim(Griditem.Cells(myGrid.Columns.IndexOf(myGrid.Columns.ColumnByName("acre_credit_department"))).Text)
                MyJdeFile.ObjectAccount = Griditem.Cells(myGrid.Columns.IndexOf(myGrid.Columns.ColumnByName("acre_credit_account"))).Text
                MyJdeFile.BuisinessUnit = Trim(Griditem.Cells(myGrid.Columns.IndexOf(myGrid.Columns.ColumnByName("credit_country_company"))).Text)
                MyJdeFile.Subsidiary = Griditem.Cells(myGrid.Columns.IndexOf(myGrid.Columns.ColumnByName("acre_credit_subsidiary"))).Text
                MyJdeFile.Amount = Griditem.Cells(myGrid.Columns.IndexOf(myGrid.Columns.ColumnByName("amount_credit"))).Text
                MyJdeFile.ExplainationAlpha = Griditem.Cells(myGrid.Columns.IndexOf(myGrid.Columns.ColumnByName("acre_name"))).Text
                MyJdeFile.ExplainationRemark = "ALF" & (Session("LastMonthApproved"))
                MyJdeFile.GLDateDay = Day(CDate(Session("LastMonthApproved")))
                MyJdeFile.GLDateMonth = Month(CDate(Session("LastMonthApproved")))
                MyJdeFile.GLDateYear = Year(CDate(Session("LastMonthApproved"))).ToString.Substring(2, 2)
                str += MyJdeFile.GetLineCredit & vbNewLine
            Next

            str = str & "<Table></body></html>"

            Dim myMep As New MEPData
            myMep.SetJDEProcessed(Convert.ToDateTime(Session("LastMonthApproved")), Session("user_id"), Session("country_id"))
            lblProsessSuccess.CssClass = "success"
            lblProsessSuccess.Text = "JDE Export File successfully downloaded!"


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
            lblProsessSuccess.CssClass = "nosuccess"
            lblProsessSuccess.Text = "Error while building JDE Export File"
        Finally
            '  Session("JDEGrid") = Nothing
        End Try


    End Sub


End Class
