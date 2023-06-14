Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities


Public Class _Error
	Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

	End Sub
	Protected WithEvents ErrorPanel As System.Web.UI.HtmlControls.HtmlGenericControl

	'NOTE: The following placeholder declaration is required by the Web Form Designer.
	'Do not delete or move it.
	Private designerPlaceholderDeclaration As System.Object

	Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
		'CODEGEN: This method call is required by the Web Form Designer
		'Do not modify it using the code editor.
		InitializeComponent()
	End Sub

#End Region


    Dim MyAppMessage As New WyethAppllication
	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		'Put user code to initialize the page here

		Dim ErrorID As Integer
        Dim ErrorMessage As String
        Dim MyReader As OracleDataReader

		Try

		
            ErrorID = Request.QueryString("ErrorID")

            MyAppMessage.ApSe_Country_ID = Session("country_id")
            MyAppMessage.AMsgNumber = ErrorID
            ErrorMessage = MyAppMessage.GetApplicationMessage()




            ErrorPanel.InnerHtml = ErrorMessage


		Catch ex As Exception
			ExceptionInfo.Show(ex)
		End Try



	End Sub

End Class
