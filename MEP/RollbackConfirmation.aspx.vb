Imports Wyeth.Utilities.Helper

Public Class RollbackConfirmation
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents btn_process_invoices As System.Web.UI.WebControls.Button
    Protected WithEvents lblProsessSuccess As System.Web.UI.WebControls.Label
    Protected WithEvents panelSuccess As System.Web.UI.WebControls.Panel

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim myStock As New Stock
    Dim MyMep As New MEPData

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        btn_process_invoices.Attributes.Add("OnClick", "javascript:return getconfirmRollback();")
        lblPageTitle.Text = "Month End Rollback Confirmation"

        Dim strScript As String
        strScript = "<script language =javascript >"
        strScript += "self.focus();"
        strScript += "</script>"
        RegisterClientScriptBlock("anything", strScript)

    End Sub

    Private Sub btn_process_invoices_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_process_invoices.Click
        If MyMep.CheckForFinanceApproval(Convert.ToDateTime(Session("LastProcessMonth")), Session("country_id")) = False Then
            panelSuccess.Visible = True
            lblProsessSuccess.CssClass = "nosuccess"
            lblProsessSuccess.Text = "You can't rollback a month that was already approved by Finance"
            MyMep.setProcessMonth()
        Else
            MyMep.RollBackInvoices(Convert.ToDateTime(Session("LastProcessMonth")), Session("country_id"))
            myStock.StockCtryID = Session("country_id")
            '  Session("LastProcessMonth") = Convert.ToDateTime(myStock.GetLastProcessedMonth()).ToString(DATEFORMAT_STRING_REPORT)
            panelSuccess.Visible = True
            lblProsessSuccess.CssClass = "success"
            lblProsessSuccess.Text = "Rollback for <font color=red> " & Convert.ToString(Session("LastProcessMonth")).Substring(0, 7) & "</font> successfully completed"
            MyMep.setProcessMonth()
        End If

    End Sub

End Class
