Public Class _Defaultcopy

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
    Public requestedpage As String = "main.aspx"

    Private Sub Page_PreRender(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.PreRender

        'Put user code to initialize the page here

        Dim page As String
        If Request.QueryString("page") <> "" Then
            page = Request.QueryString("page")
        Else
            page = "none"
        End If
        If Request.QueryString("requestedPage") <> "" Then
            requestedpage = Request.QueryString("requestedPage")
        Else
            requestedpage = "Main.aspx"
        End If


        If page.ToUpper = "MVIEWS" Then
            Dim strScript As String
            strScript = "<script language =javascript >"
            strScript += "if (parent.frames['main']!=null) {"
            strScript += "parent.frames['main'].location='MviewsProgress.aspx';"
            strScript += "}</script>"
            RegisterClientScriptBlock("anything", strScript)
            Response.Write(strScript)
        End If

    End Sub

End Class
