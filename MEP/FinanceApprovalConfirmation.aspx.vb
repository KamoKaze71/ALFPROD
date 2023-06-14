Imports Wyeth.Utilities.Helper

Public Class FinanceApprovalConfirmation
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents lblProsessSuccess As System.Web.UI.WebControls.Label
    Protected WithEvents panelSuccess As System.Web.UI.WebControls.Panel
    Protected WithEvents btn_approve_month As System.Web.UI.WebControls.Button
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents lblInfo As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim MyMep As New MEPData
    Dim MyStock As New Stock
    Dim postbackcount As Integer = 0
    Dim MyJs As New JSPopUp(Me)
    Dim MyInfoView As New DataView


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Me.ViewState.Add("action", Request.QueryString("action"))

        If Me.ViewState("action") = "Rollback" Then
            lblPageTitle.Text = "Finance Month End Approval Rollback"
            btn_approve_month.Text = "Rollback Finance approval"
            MyJs.ConfirmMessage = " This will rollback the finance month end approval! Are you sure you want to do this?"
            MyInfoView = MyMep.GetV_PM(Convert.ToDateTime(Session("LastMonthApproved")), Session("country_id"))
            lblInfo.Text = "Last Finance approval by: " & MyInfoView(0).Item("USER_NAME_JDE_APPROVAL") & " at: " & MyInfoView(0).Item("PM_DATE_JDE_APPROVAL")

        Else
            MyJs.ConfirmMessage = "This will set the Finance Approval for the Month! Are you sure you want to do that?"
            lblPageTitle.Text = "Finance Month End Approval Confirmation"
        End If

        MyJs.AddGetConfirm(btn_approve_month)

        Dim strScript As String
        strScript = "<script language =javascript >"
        strScript += "self.focus();"
        strScript += "</script>"
        RegisterClientScriptBlock("anything", strScript)

    End Sub

    Private Sub btn_approve_month_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_approve_month.Click
        Select Case Me.ViewState("action")

            Case ""
                If MyMep.CheckForFinanceApproval(Convert.ToDateTime(Session("LastProcessMonth")), Session("country_id")) = False Then
                    panelSuccess.Visible = True
                    lblProsessSuccess.CssClass = "nosuccess"
                    lblProsessSuccess.Text = Convert.ToDateTime(Session("LastProcessMonth")).ToString(DATEFORMAT_STRING_REPORT).Substring(0, 7) & "  has already been approved by Finance"
                    MyMep.setProcessMonth()
                Else
                    MyMep.SetFinaceApproval(Convert.ToDateTime(Session("LastProcessMonth")), Session("User_id"), Session("country_id"))
                    panelSuccess.Visible = True
                    lblProsessSuccess.CssClass = "success"
                    lblProsessSuccess.Text = "The month <FONT COLOR=red>" & Convert.ToDateTime(Session("LastProcessMonth")).ToString(DATEFORMAT_STRING_REPORT).Substring(0, 7) & "</font> was successfully approved, and is now locked!"
                    MyMep.setProcessMonth()
                End If
            Case "Rollback"
                If MyMep.RollbackFinaceApproval(Convert.ToDateTime(Session("LastProcessMonth")), Session("User_id"), Session("country_id")) = True Then
                    panelSuccess.Visible = True
                    lblProsessSuccess.CssClass = "success"
                    lblProsessSuccess.Text = "Finance Approval for the month <FONT COLOR=red>" & Convert.ToDateTime(Session("LastProcessMonth")).ToString(DATEFORMAT_STRING_REPORT).Substring(0, 7) & "</font> was successfully rolled back!"
                    MyMep.setProcessMonth()
                Else
                    panelSuccess.Visible = True
                    lblProsessSuccess.CssClass = "nosuccess"
                    lblProsessSuccess.Text = "Could not rollback the Finance Approval for <FONT COLOR=red>" & Convert.ToDateTime(Session("LastProcessMonth")).ToString(DATEFORMAT_STRING_REPORT).Substring(0, 7) & "</font>"
                    MyMep.setProcessMonth()
                End If

        End Select


      
    End Sub
End Class
