Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities

Public Class leftnavi
	Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

	End Sub
	Protected WithEvents Menu1 As Wyeth.Alf.IntranetMenu

	'NOTE: The following placeholder declaration is required by the Web Form Designer.
	'Do not delete or move it.
	Private designerPlaceholderDeclaration As System.Object

	Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
		'CODEGEN: This method call is required by the Web Form Designer
		'Do not modify it using the code editor.
		Menu1.MenuIDParent = 31
		InitializeComponent()
	End Sub

#End Region
	Dim myMenu As New IntranetMenu

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		'Put user code to initialize the page here
		Dim id_menu As Integer


		id_menu = CInt(Request.QueryString("id"))
		If id_menu = 0 Then
            id_menu = 63
		End If
        Menu1.MenuIDParent = id_menu
        Session("module_id") = Menu1.MenuModulID

	End Sub

End Class
