Public Class ImportDBUpdate_BW
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

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

        If Request.QueryString("manual") = String.Empty Then
            Dim myImport As New WyethImport
            Response.Write(myImport.Import_BW())
        ElseIf Request.QueryString("manual") = "PHARMOSAN" Then
            Dim ret_val As String
            Try

                Dim myPharmosanImport As New WyethImportPharmosan
                ret_val = myPharmosanImport.UpdateT_Prescription()
            Catch ex As Exception
                ret_val = ret_val & "<br>" & ex.Message
            Finally
                Response.Write(ret_val)
            End Try

        End If
    End Sub

End Class
