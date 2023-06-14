Public Class saretapg
	Inherits System.Web.UI.UserControl

#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

	End Sub
	Protected WithEvents txtbox As System.Web.UI.WebControls.TextBox
	Protected WithEvents checkbox As System.Web.UI.WebControls.CheckBox
	Protected WithEvents TextBox1 As System.Web.UI.WebControls.TextBox

	'NOTE: The following placeholder declaration is required by the Web Form Designer.
	'Do not delete or move it.
	Private designerPlaceholderDeclaration As System.Object

	Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
		'CODEGEN: This method call is required by the Web Form Designer
		'Do not modify it using the code editor.
		InitializeComponent()
	End Sub

#End Region
	Private m_str_value As String

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		'Put user code to initialize the page here



	End Sub
	Public Property Value() As String
		Get
			Return m_str_value
		End Get
		Set(ByVal Value As String)
			m_str_value = Value
		End Set
	End Property
End Class
