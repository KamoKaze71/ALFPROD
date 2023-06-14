Option Explicit On 
Option Strict On

Imports System
Imports System.IO

Public Class SqlLoaderLog
	Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

	End Sub
	Protected WithEvents LabelOUT As System.Web.UI.WebControls.Label
	Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel

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

		Dim filename As String

		filename = Request.QueryString("file")
		Dim sr As StreamReader = File.OpenText(filename)
		Dim input As String
		Try

		

		If File.Exists(filename) Then
				input = sr.ReadLine()



				While Not input Is Nothing
					LabelOUT.Text = LabelOUT.Text & input & "<br>"
					input = sr.ReadLine()
				End While
				sr.Close()
			End If
		Catch ex As Exception

		End Try
	End Sub

End Class


