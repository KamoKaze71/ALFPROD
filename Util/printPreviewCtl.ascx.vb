Public Class printPreviewCtl
    Inherits System.Web.UI.UserControl

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnPrint As System.Web.UI.WebControls.Button
    Protected WithEvents btnPreview As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Public Delegate Sub delegateEvent(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event Preview_Click As delegateEvent
    Public Event Print_Click As delegateEvent

    Sub New()
        MyBase.New()
    End Sub

    '************************************************************************************************
    '* Preview Button Event
    '************************************************************************************************
    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        RaiseEvent Preview_Click(sender, e)
    End Sub

    '************************************************************************************************
    '* Print Button Event
    '************************************************************************************************
    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        RaiseEvent Print_Click(sender, e)
    End Sub
End Class
