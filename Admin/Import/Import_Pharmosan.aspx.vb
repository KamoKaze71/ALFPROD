Public Class Import_EnbrelTargets
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btn_upload As System.Web.UI.WebControls.Button
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents btnImport As System.Web.UI.WebControls.Button
    Protected WithEvents lblOut As System.Web.UI.WebControls.Label
    Protected WithEvents btnDelete As System.Web.UI.WebControls.Button
    Protected WithEvents txtProgress As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Page.IsPostBack = True Then

        End If
        Dim myPop As New JSPopUp(Me)
        myPop.PageURL = "UploadFiles.aspx?filetype=Pharmosan"
        myPop.Width = 450
        myPop.Height = 250
        myPop.AddPopupToControl(Me.btn_upload)

        myPop.PageURL = "DeleteTransmission.aspx?dist_id=9"
        myPop.Width = 450
        myPop.Height = 250
        myPop.AddPopupToControl(Me.btnDelete)

    End Sub

End Class
