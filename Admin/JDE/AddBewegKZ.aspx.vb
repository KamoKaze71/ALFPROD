Imports Wyeth.Alf.WyethDropdown

Public Class AddBewegKZ
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents buttonSave As System.Web.UI.WebControls.Button
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddBewegKZ As System.Web.UI.WebControls.DropDownList
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

    Dim MyJDE As New JDE
    Dim acre_id, code_id As Integer
    Dim MyJS As New JSPopUp(Me)

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Me.ALFPageTitle = "Add BewegKz"

        If Page.IsPostBack = True Then
            acre_id = Me.ViewState("acre_id")
            code_id = CInt(ddBewegKZ.SelectedValue.ToString)
                   Else
            GetBewegKZSelectDD(ddBewegKZ, Session("country_id"))
            acre_id = Request.QueryString("acre_id")
            Me.ViewState.Add("acre_id", acre_id)
        End If

        '  buttonSave.Attributes.Add("OnClick", "javascript:window.opener.location.href='Accounting.aspx?acre_id=" & acre_id & "';self.close()")



    End Sub

    Private Sub ddBewegKZ_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddBewegKZ.SelectedIndexChanged
        Dim tmp() As String
        tmp = ddBewegKZ.SelectedItem.ToString.Split("-")
        txtDescription.Text = ""
        txtName.Text = Trim(tmp(0))

        For Each str As String In tmp
            txtDescription.Text = txtDescription.Text & Trim(str) & "-"
        Next
        txtDescription.Text = txtDescription.Text.TrimEnd("-")

    End Sub

    Private Sub buttonSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonSave.Click
        MyJDE.AddBewegKZForAcRE(acre_id, code_id)
        'MyJS.ClosePopUpAndRefreshOpener("?acre_id=" & acre_id)
        Dim strScript As String
        strScript += "<script language =javascript >"
        strScript += "javascript:window.opener.location.href='Accounting.aspx?acre_id=" & acre_id & "';self.close()"
        'strScript += "javascript:window.opener.location.href=window.opener.location.href;self.close()"

        strScript += "</script>"
        Response.Write(strScript)
    End Sub
End Class
