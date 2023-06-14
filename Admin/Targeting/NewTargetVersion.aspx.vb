Imports Wyeth.Alf.WyethDropdown
Imports Wyeth.Utilities
Imports Wyeth.Utilities.Settings

Public Class NewTargetVersion
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents chk_box_take_values As System.Web.UI.WebControls.CheckBox
    Protected WithEvents btn_save As System.Web.UI.WebControls.Button
    Protected WithEvents ddTPG As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddyear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddSare As System.Web.UI.WebControls.DropDownList
    Protected WithEvents tblCopy As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents ddSare_old As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddVersion_old As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddyear_old As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblsuccess As System.Web.UI.WebControls.Label
    Protected WithEvents lblVersion As System.Web.UI.WebControls.Label
    Protected WithEvents btnclose As System.Web.UI.HtmlControls.HtmlButton

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region


    Public new_version As String
    Dim sare_id, year, tpg_id, version As Integer
    Dim tpg_type As String
    Dim MyTargeting As New Targeting


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        Me.ALFPageTitle = "Create new Traget Version"

        If Page.IsPostBack = True Then

            sare_id = Me.ViewState.Item("sare_id")
            year = Me.ViewState.Item("year")
            tpg_id = Me.ViewState.Item("tpg_id")
            version = Me.ViewState.Item("version")
            tpg_type = Me.ViewState.Item("tpg_type")
        Else
            tpg_id = Request.QueryString("tpg_id")
            sare_id = Request.QueryString("sare_id")
            year = Request.QueryString("year")
            tpg_type = MyTargeting.GetTargetType(tpg_id)

            Me.ViewState.Add("year", year)
            Me.ViewState.Add("sare_id", sare_id)
            Me.ViewState.Add("tpg_id", tpg_id)
            Me.ViewState.Add("tpg_type", tpg_type)
            BindData()

        End If

        Me.btnclose.InnerText = "Cancel"


    End Sub

    Private Sub BindData()
       
        GetYearDD4(ddyear, 2004, 1)
        ddyear.SelectedValue = year

        GetTPGDD(ddTPG, Session("country_id"))
        ddTPG.SelectedValue = tpg_id

        GetSaReForTPGDD(ddSare, ddTPG.SelectedValue)
        ddSare.SelectedValue = sare_id

        GetYearDD4(ddyear_old, 2004, 1)
        ddyear_old.SelectedValue = year

        GetSaReForTPGDD(ddSare_old, ddTPG.SelectedValue)
        ddSare_old.SelectedValue = sare_id

        GetVersionDD(ddVersion_old, ddSare_old.SelectedValue, ddyear_old.SelectedValue, ddTPG.SelectedValue)

        btnclose.Attributes.Add("onClick", "window.close();window.opener.location.href='TargetInput.aspx?tpg_id=" & tpg_id & "&sare_id=" & sare_id & "&year=" & year & "';")

        If chk_box_take_values.Checked = True Then
            tblCopy.Visible = True
        Else
            tblCopy.Visible = False
        End If

        new_version = 1 + MyTargeting.GetLatestVersionForSaReYear(ddyear.SelectedValue, ddSare.SelectedValue, ddTPG.SelectedValue)

        If MyTargeting.CheckForApproval(ddyear.SelectedValue, ddSare.SelectedValue, ddTPG.SelectedValue, new_version - 1) = 0 Then
            btn_save.Enabled = False
            lblsuccess.Text = "The previous Version has to be approved, before a new version can be created!"
            lblsuccess.CssClass = "nosuccess"
        Else
            btn_save.Enabled = True
        End If

        lblVersion.DataBind()
    End Sub

    Private Sub btn_save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Dim old_version As String

        new_version = 1 + MyTargeting.GetLatestVersionForSaReYear(ddyear.SelectedValue, ddSare.SelectedValue, ddTPG.SelectedValue)
        old_version = ddVersion_old.SelectedValue
        If chk_box_take_values.Checked = False Then
            old_version = "0"
        End If

        If MyTargeting.CreateNewTargetVersionFromOld(ddyear_old.SelectedValue, old_version, ddSare_old.SelectedValue, ddTPG.SelectedValue, ddSare.SelectedValue, ddyear.SelectedValue, new_version, Session("user_id")) = True Then
            BindData()
            lblsuccess.Text = "new target version sucessfully created"
            lblsuccess.CssClass = "success"
            Me.btnclose.InnerText = "Close Window"
            Try


                Dim sSMTPServer, sFrom, sTo, sSubject, sMessage, strLink As String
                Dim MyCollection As New Collection
                Dim myuser As New UserAccess
                Dim MyDataView As New DataView

                Dim MyMail As New Wyeth.Utilities.WyethJmail

              
                MyDataView = myuser.GetUserForAcessRight("ALF Target Approver")


            MyCollection = Application("AppSetting")
            sSMTPServer = MyCollection("SMTPHost")
                sFrom = "ALF"



                For Each item As DataRow In MyDataView.Table.Rows
                    Dim MyTemplate As New Wyeth.Utilities.textTemplate
                    MyTemplate.filename = Server.MapPath("../../emailTemplates\") & "TargetApproval.html"

                    sSubject = "Sales target approval for " & ddSare.SelectedItem.Text
                    sTo = item.Item("EMAIL")
                    strLink = "Admin/Targeting/TargetApproval.aspx?tapg_id=" & ddTPG.SelectedValue & "&sare_id=" & ddSare.SelectedValue & "&year=" & ddyear.SelectedValue & "&approval=YES"
                    MyTemplate.addVariable("MAIL_NAME_APPROVER", item.Item("FirstName") & " " & item.Item("LastName"))
                    MyTemplate.addVariable("USER_ENTER_DATA", myuser.GetUserName(Session("user_id")))
                    MyTemplate.addVariable("URL", applicationUrl & strLink)
                    MyTemplate.addVariable("SALES_REP", ddSare.SelectedItem.ToString)
                    sMessage = MyTemplate.returnString()

                    MyMail.SendEMail(sSMTPServer, sFrom, sTo, sSubject, sMessage)
                Next
            Catch ex As Exception
                ExceptionInfo.show(ex)
            End Try
        Else
            lblsuccess.Text = "could not create new target version"
            lblsuccess.CssClass = "nosuccess"
        End If

    End Sub

    Private Sub chk_box_take_values_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_box_take_values.CheckedChanged
        If chk_box_take_values.Checked = True Then
            tblCopy.Visible = True
        Else
            tblCopy.Visible = False
        End If
    End Sub

    Private Sub ddTPG_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddTPG.SelectedIndexChanged

        GetSaReForTPGDD(ddSare, ddTPG.SelectedValue)
        ' ddSare.SelectedValue = sare_id

        GetSaReForTPGDD(ddSare_old, ddTPG.SelectedValue)
        ' ddSare_old.SelectedValue = sare_id

        GetVersionDD(ddVersion_old, ddSare_old.SelectedValue, ddyear_old.SelectedValue, ddTPG.SelectedValue)

        new_version = 1 + MyTargeting.GetLatestVersionForSaReYear(ddyear.SelectedValue, ddSare.SelectedValue, ddTPG.SelectedValue)

        If MyTargeting.CheckForApproval(ddyear.SelectedValue, ddSare.SelectedValue, ddTPG.SelectedValue, new_version - 1) = 0 Then
            btn_save.Enabled = False
            lblsuccess.Text = "The previous Version has to be approved, before a new version can be created!"
            lblsuccess.CssClass = "nosuccess"
        Else
            btn_save.Enabled = True
        End If
        lblVersion.DataBind()
    End Sub

    Private Sub ddSare_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddSare.SelectedIndexChanged
        new_version = 1 + MyTargeting.GetLatestVersionForSaReYear(ddyear.SelectedValue, ddSare.SelectedValue, ddTPG.SelectedValue)
        If MyTargeting.CheckForApproval(ddyear.SelectedValue, ddSare.SelectedValue, ddTPG.SelectedValue, new_version - 1) = 0 Then
            btn_save.Enabled = False
            lblsuccess.Text = "The previous Version has to be approved, before a new version can be created!"
            lblsuccess.CssClass = "nosuccess"
        Else
            btn_save.Enabled = True
        End If

        lblVersion.DataBind()
    End Sub

    Private Sub ddSare_old_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddSare_old.SelectedIndexChanged

        GetVersionDD(ddVersion_old, ddSare_old.SelectedValue, ddyear_old.SelectedValue, ddTPG.SelectedValue)

    End Sub
End Class
