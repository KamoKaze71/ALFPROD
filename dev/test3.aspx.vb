Public Class test3
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


        'Dim a As Integer = 0
        'For i As Integer = 0 To 60000000
        '    a = a + 1
        '    If a Mod 2000000 = 0 Then
        '        Response.Write(" wait ... ")
        '        Response.Flush()
        '    End If
        'Next

        'Response.Write("<br>page3 - " & a)+


        Dim value As String = "wyeth_bw.dat"

        
        Select Case value

            ' bewegungsdaten
        Case ((value.ToUpper.IndexOf("BW")) > 0) = False
                Response.Write(value.ToUpper.IndexOf("BW"))

            Case Else
                Response.Write(value)
        End Select

    End Sub

End Class
