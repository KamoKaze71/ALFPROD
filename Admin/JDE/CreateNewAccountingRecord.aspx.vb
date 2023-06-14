Public Class CreateNewAccountingRecord
    Inherits Wyeth.Alf.AlfPage


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents buttonSave As System.Web.UI.WebControls.Button
    Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
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
    Dim MyJde As New JDE
    Dim MyDataView As New DataView
    Dim acre_id As Integer = 0
    Dim myJs As New JSPopUp(Me)

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        If Page.IsPostBack = True Then
            acre_id = Me.ViewState("acre_id")
        Else
            acre_id = Request.QueryString("acre_id")
            Me.ViewState.Add("acre_id", acre_id)


    

        If acre_id = 0 Then
            Me.ALFPageTitle = "Create New Accounting Record"
        Else
                Me.ALFPageTitle = "Modify Accounting Record"

                MyDataView = MyJde.GetAccRecord(acre_id)
                txtName.Text = MyDataView.Item(0).Item("acre_name")
                txtDescription.Text = MyDataView.Item(0).Item("acre_description")

            End If
        End If
        '  buttonSave.Attributes.Add("OnClick", "javascript:window.opener.location.href='Accounting.aspx?acre_id=" & acre_id & "';self.close();")


    End Sub

    Private Sub buttonSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonSave.Click
        If acre_id = 0 Then
            acre_id = MyJde.AddAccountingRecord(Trim(txtName.Text), Trim(txtDescription.Text), Session("country_id"))
        Else
            MyJde.ModifyAccountingRecord(acre_id, Trim(txtName.Text), Trim(txtDescription.Text))
        End If
        Dim strScript As String
        strScript += "<script language =javascript >"
        strScript += "javascript:window.opener.location.href='Accounting.aspx?acre_id=" & acre_id & "';self.close()"
        strScript += "</script>"
        Response.Write(strScript)
    End Sub
End Class
