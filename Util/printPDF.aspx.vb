Imports WebSupergoo.ABCpdf4

Public Class printPDF
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
        Response.Expires = -1000
        Dim theDoc As Doc = CType(Session("doc"), Doc)
        Dim theData As Byte() = theDoc.GetData()
        'Response.Write(theDoc.License)

        Response.ContentType = "application/pdf"
        Response.AddHeader("content-length", theData.Length)
        'If Not Request.QueryString("attachment") = Nothing Then
        Response.AddHeader("content-disposition", "attachment; filename=MyPDF.PDF")
        'Else
        '    Response.AddHeader("content-disposition", "inline; filename=MyPDF.PDF")
        'End If
        Response.BinaryWrite(theData)

        Session("doc") = New Doc
    End Sub

End Class
