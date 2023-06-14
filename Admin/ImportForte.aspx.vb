Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities

Public Class ImportForte
	Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

	End Sub
	Protected WithEvents button_forte_all As System.Web.UI.WebControls.Button
	Protected WithEvents Button_forte_products As System.Web.UI.WebControls.Button
	Protected WithEvents Button_forte_countries As System.Web.UI.WebControls.Button
	Protected WithEvents Button_forte_budget As System.Web.UI.WebControls.Button
	Protected WithEvents Button_forte_tcogs As System.Web.UI.WebControls.Button
	Protected WithEvents Button_forte_fx_rate As System.Web.UI.WebControls.Button
	Protected WithEvents Button_forte_currencies As System.Web.UI.WebControls.Button
	Protected WithEvents Label1 As System.Web.UI.WebControls.Label
	Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
	Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents Button2 As System.Web.UI.WebControls.Button
	Protected WithEvents LabelOUT As System.Web.UI.WebControls.Label
	Protected WithEvents EditPanel As System.Web.UI.WebControls.Panel

	'NOTE: The following placeholder declaration is required by the Web Form Designer.
	'Do not delete or move it.
	Private designerPlaceholderDeclaration As System.Object

	Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
		'CODEGEN: This method call is required by the Web Form Designer
		'Do not modify it using the code editor.
		InitializeComponent()
	End Sub

#End Region

	Dim myCmd As New OracleCommand

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		'Put user code to initialize the page here
		lblPageTitle.Text = Request.QueryString("pageTitle")
	End Sub

	Private Sub Button_forte_products_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_forte_products.Click
		Dim MyConn As New MyConnection
		Dim val As Object
		Try
			myCmd.Parameters.Clear()
		myCmd.CommandType = CommandType.StoredProcedure
		myCmd.Parameters.Add("v_result", OracleDbType.Varchar2, 2000, val, ParameterDirection.ReturnValue)
		myCmd.Connection = MyConn.Open
			myCmd.CommandText = "PKG_IMPORT_FORTE.F_PRODUCTS"
			val = myCmd.ExecuteScalar()
			val = myCmd.Parameters(0).Value
            LabelOUT.Text = LabelOUT.Text & val.ToString & "<BR>"

        Catch ex As Exception
            LabelOUT.Text = LabelOUT.Text & ex.Message.ToString & "<br>"
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try
	End Sub

	Private Sub Button_forte_countries_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_forte_countries.Click
		Dim MyConn As New MyConnection
		Dim val As Object
		Try

		
		myCmd.Parameters.Clear()
		myCmd.CommandType = CommandType.StoredProcedure
		myCmd.Parameters.Add("v_result", OracleDbType.Varchar2, 2000, val, ParameterDirection.ReturnValue)
		myCmd.Connection = MyConn.Open
			myCmd.CommandText = "PKG_IMPORT_FORTE.F_COUNTRY"
			val = myCmd.ExecuteScalar()
			val = myCmd.Parameters(0).Value
            LabelOUT.Text = LabelOUT.Text & val.ToString & "<BR>"
        Catch ex As Exception
            LabelOUT.Text = LabelOUT.Text & ex.Message.ToString & "<br>"
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try
	End Sub

	Private Sub Button_forte_budget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_forte_budget.Click
		Dim MyConn As New MyConnection
		Dim val As Object
		Try
			myCmd.Parameters.Clear()
		myCmd.CommandType = CommandType.StoredProcedure
		myCmd.Parameters.Add("v_result", OracleDbType.Varchar2, 2000, val, ParameterDirection.ReturnValue)
		myCmd.Connection = MyConn.Open
		myCmd.CommandText = "PKG_IMPORT_FORTE.F_Budget"
			val = myCmd.ExecuteScalar()
			val = myCmd.Parameters(0).Value
            LabelOUT.Text = LabelOUT.Text & val.ToString & "<BR>"
        Catch ex As Exception
            LabelOUT.Text = LabelOUT.Text & ex.Message.ToString & "<br>"
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try


	End Sub

	Private Sub Button_forte_tcogs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_forte_tcogs.Click
		Dim MyConn As New MyConnection
		Dim val As Object
		Try
			myCmd.Parameters.Clear()
			myCmd.CommandType = CommandType.StoredProcedure
			myCmd.Parameters.Add("v_result", OracleDbType.Varchar2, 2000, val, ParameterDirection.ReturnValue)
			myCmd.Connection = MyConn.Open
			myCmd.CommandText = "PKG_IMPORT_FORTE.F_COGS"
			val = myCmd.ExecuteScalar()
			val = myCmd.Parameters(0).Value
            LabelOUT.Text = LabelOUT.Text & val.ToString & "<BR>"
        Catch ex As Exception
            LabelOUT.Text = LabelOUT.Text & ex.Message.ToString & "<br>"
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try
	End Sub

	Private Sub Button_forte_fx_rate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_forte_fx_rate.Click
		Dim MyConn As New MyConnection
		Dim val As Object
		Try
			myCmd.Parameters.Clear()
			myCmd.CommandType = CommandType.StoredProcedure
			myCmd.Parameters.Add("v_result", OracleDbType.Varchar2, 2000, val, ParameterDirection.ReturnValue)
			myCmd.Connection = MyConn.Open
			myCmd.CommandText = "PKG_IMPORT_FORTE.F_FX_RATE"
			val = myCmd.ExecuteScalar()
			val = myCmd.Parameters(0).Value
            LabelOUT.Text = LabelOUT.Text & val.ToString & "<BR>"
        Catch ex As Exception
            LabelOUT.Text = LabelOUT.Text & ex.Message.ToString & "<br>"
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try
	End Sub

	Private Sub Button_forte_currencies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_forte_currencies.Click
		Dim MyConn As New MyConnection
		Dim val As Object
		Try
			myCmd.Parameters.Clear()
			myCmd.CommandType = CommandType.StoredProcedure
			myCmd.Parameters.Add("v_result", OracleDbType.Varchar2, 2000, val, ParameterDirection.ReturnValue)
			myCmd.Connection = MyConn.Open
			myCmd.CommandText = "PKG_IMPORT_FORTE.F_CURRENCY"
			val = myCmd.ExecuteScalar()
			val = myCmd.Parameters(0).Value
            LabelOUT.Text = LabelOUT.Text & val.ToString & "<BR>"
        Catch ex As Exception
            LabelOUT.Text = LabelOUT.Text & ex.Message.ToString & "<br>"
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try
	End Sub

	Private Sub button_forte_all_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button_forte_all.Click

		Dim MyConn As New MyConnection
		Dim mycmd As New OracleCommand

		Dim val As String
		Try

			mycmd.Parameters.Clear()
			mycmd.CommandType = CommandType.StoredProcedure
			mycmd.CommandText = "p_start_forte_imports"
			mycmd.CommandType = CommandType.StoredProcedure
			mycmd.Connection = MyConn.Open
			mycmd.ExecuteNonQuery()
        Catch ex As Exception
            LabelOUT.Text = LabelOUT.Text & ex.Message.ToString & "<br>"
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try
	End Sub
End Class
