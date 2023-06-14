Public Class Import_SQLLoader
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
    Dim myImport As New WyethImport
    Dim MyCollection As New Collection
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        MyCollection = Application("AppSetting")

        If Request.QueryString("manual").ToUpper = "FALSE" Then
            Response.Write(myImport.SQLLoader(CType(Session("ImportLB"), ListBox)))
        ElseIf Request.QueryString("manual").ToUpper = "TRUE" Then
            Response.Write(myImport.SQLLoader(MyCollection("SanovaImportFilePath").ToString))
        ElseIf Request.QueryString("manual") = "PHARMOSAN" Then
            Dim MyPharmosanImport As New WyethImportPharmosan
            Response.Write(MyPharmosanImport.sqlLoader(Session("user_id")))
        End If

    End Sub

End Class
