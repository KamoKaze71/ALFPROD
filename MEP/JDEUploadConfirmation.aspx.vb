Public Class JDEUploadConfirmation
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btn_confirm_upload_jde As System.Web.UI.WebControls.Button
    Protected WithEvents panelSuccess As System.Web.UI.WebControls.Panel
    Protected WithEvents lblProcessSuccess As System.Web.UI.WebControls.Label
    Protected WithEvents lblProcessMonth As System.Web.UI.WebControls.Label
    Protected WithEvents btn_closeWindows As System.Web.UI.WebControls.Button
    Protected WithEvents Table1 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents lblInfo As System.Web.UI.WebControls.Label
    Protected WithEvents Literal1 As System.Web.UI.WebControls.Literal

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim myMep As New MEPData
    Dim MyInfoView As New DataView
    Dim myJs As New JSPopUp(Me)

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        If myMep.CheckForJDEFinalApproval(Convert.ToDateTime(Session("LastMonthApproved")), Session("country_id")) = True Then

            lblProcessMonth.Text = "This will confirm the JDE upload for <font color=red>" & Convert.ToString(Session("LastMonthApproved")).Substring(0, 7) & "</font>"
            myJs.ConfirmMessage = "This will approve that JDE export data was sucessfully imported into JDE System! Are you sure that you want to confirm this approval?"
            myJs.AddGetConfirm(btn_confirm_upload_jde)

            If myMep.CheckForJDEProcessing(Convert.ToDateTime(Session("LastMonthApproved")), Session("country_id")) = True Then
                lblProcessMonth.Text = "It' not possible to confirm the JDE upload for the month <font color=red>" & Convert.ToString(Session("LastMonthApproved")).Substring(0, 7) & "</font>, because the JDE Export was not done"
                btn_confirm_upload_jde.Visible = False
            End If
        Else

            MyInfoView = myMep.GetV_PM(Convert.ToDateTime(Session("LastMonthApproved")), Session("country_id"))
            lblProcessMonth.Text = "The Month <font color=red>" & Convert.ToString(Session("LastMonthApproved")).Substring(0, 7) & "</font> is already approved!<br>A Rollback is not possible!"
            lblInfo.Text = "Last Final JDE approval by: " & MyInfoView(0).Item("USER_NAME_JDE_APPROVAL") & " at: " & MyInfoView(0).Item("PM_DATE_JDE_APPROVAL")
            lblInfo.CssClass = "nosuccess"
            btn_confirm_upload_jde.Visible = False
        End If

    End Sub

    Private Sub btn_confirm_upload_jde_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_confirm_upload_jde.Click
        Try

            If myMep.SetJDEFinalApproval(Convert.ToDateTime(Session("LastMonthApproved")), Session("user_id"), Session("country_id")) = True Then
                lblProcessSuccess.Visible = True
                lblProcessSuccess.CssClass = "success"
                lblProcessSuccess.Text = "Final JDE Export Approval  for: " & Convert.ToString(Session("LastMonthApproved")).Substring(0, 7) & " sucesfully completed!"
            Else
                lblProcessSuccess.Visible = True
                lblProcessSuccess.CssClass = "nosuccess"
                lblProcessSuccess.Text = "Error while approving"
            End If

        Catch ex As Exception
            lblProcessSuccess.Visible = True
            lblProcessSuccess.CssClass = "nosuccess"
            lblProcessSuccess.Text = "Error while approving the Month " & Convert.ToString(Session("LastMonthApproved")).Substring(0, 7)
        End Try

    End Sub
End Class
