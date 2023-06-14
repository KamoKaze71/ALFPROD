Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities
Imports Wyeth.Alf.CssStyles

Public Class NotImportedKD

	Inherits System.Web.UI.Page

	Dim MyDs As New DataSet
	Dim MyCmd As New OracleCommand
	Dim MyAdapter As New OracleDataAdapter
	Dim MyDataView As New DataView
	Protected WithEvents MyGridART As C1.Web.C1WebGrid.C1WebGrid
	Protected WithEvents MyGridBW As C1.Web.C1WebGrid.C1WebGrid
	Protected WithEvents MyGridKD As C1.Web.C1WebGrid.C1WebGrid
	Dim conn As New MyConnection

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

		If Page.IsPostBack Then

		Else
			BindData()

		End If

	End Sub
	Private Sub BindData()

		Try

			SetGridStyles(MyGridKD)
			SetGridStyles(MyGridBW)
			SetGridStyles(MyGridART)

			MyCmd.Connection = conn.Open()

			MyCmd.CommandType = CommandType.StoredProcedure
			MyCmd.CommandText = "PKG_APPLICATION.GetNotImportedKD"
			MyCmd.Parameters.Add("imported", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)

			MyAdapter.SelectCommand = MyCmd
			MyAdapter.Fill(MyDs, "ImportedKD")
			MyDataView = MyDs.Tables("ImportedKD").DefaultView
			MyGridKD.DataSource = MyDataView
			MyGridKD.DataBind()

			MyCmd.CommandText = "PKG_APPLICATION.GetNotImportedART"
			MyAdapter.Fill(MyDs, "ImportedART")
			MyDataView = MyDs.Tables("ImportedART").DefaultView
			MyGridART.DataSource = MyDataView
			MyGridART.DataBind()

			MyCmd.CommandText = "PKG_APPLICATION.GetNotImportedBW"
			MyAdapter.Fill(MyDs, "ImportedART")
			MyDataView = MyDs.Tables("ImportedART").DefaultView
			MyGridBW.DataSource = MyDataView
			MyGridBW.DataBind()

			conn.Close()

		Catch ex As Exception
			ExceptionInfo.Show(ex)

		End Try

	End Sub
End Class
