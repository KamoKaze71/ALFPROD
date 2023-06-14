Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities

'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
'''         <revision>23</revision>
''' <summary><para>Provides DataAccess for T_Currrency</para></summary>
Public Class WyethCurrency

	Private m_str_curr_description As String
	Private m_str_Curr_Code As String
	Private m_i_curr_id As Integer
	Private m_i_curr_user_id As Integer

	Public Sub New()
		MyBase.New()
	End Sub

	Public Function InsertCurency() As Boolean
		Try
			Dim MyCmd As New OracleCommand
			Dim MyConn As New MyConnection

			MyCmd.CommandText = "P_Currency_Ins_Proc"
			MyCmd.CommandType = CommandType.StoredProcedure
			MyCmd.Parameters.Clear()
			MyCmd.Parameters.Add("v_Curr_Code", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = CurrCode
			MyCmd.Parameters.Add("v_Curr_Description", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = CurrDescription
			MyCmd.Parameters.Add(" v_Curr_User_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = CurrUserID

			MyCmd.Connection = MyConn.Open()
			MyCmd.ExecuteNonQuery()

			MyCmd.Dispose()
			MyConn.Close()
		Catch ex As Exception
			ExceptionInfo.Show(ex)
			Return False
		End Try
		Return True

	End Function

	Public Function UpdateCurrency() As Boolean

		Try
			Dim MyCmd As New OracleCommand
			Dim MyConn As New MyConnection

			MyCmd.CommandText = "P_Currency_Upd_Proc"
			MyCmd.CommandType = CommandType.StoredProcedure
			MyCmd.Parameters.Clear()
			MyCmd.Parameters.Add("v_Curr_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = CurrID
			MyCmd.Parameters.Add("v_Curr_Code", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = CurrCode
			MyCmd.Parameters.Add("v_Curr_Description", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = CurrDescription
			MyCmd.Parameters.Add(" v_Curr_User_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = CurrUserID


			MyCmd.Connection = MyConn.Open()
			MyCmd.ExecuteNonQuery()

			MyCmd.Dispose()
			MyConn.Close()
		Catch ex As Exception
			ExceptionInfo.Show(ex)
			Return False
		End Try
		Return True

	End Function

	Public Function DeleteCurrency() As Boolean
		Try
			Dim MyCmd As New OracleCommand
			Dim MyConn As New MyConnection

			MyCmd.CommandText = "P_Currency_Del_Proc"
			MyCmd.CommandType = CommandType.StoredProcedure

			MyCmd.Parameters.Clear()
			MyCmd.Parameters.Add("v_curr_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = CurrID

			MyCmd.Connection = MyConn.Open()
			MyCmd.ExecuteNonQuery()

			MyCmd.Dispose()
			MyConn.Close()
		Catch ex As Exception
			ExceptionInfo.Show(ex)
			Return False
		End Try
		Return True

	End Function


	Public Function GetCurrencies() As DataView
		Try
			Dim MyConn As New MyConnection
			Dim MyCmd As New OracleCommand
			Dim MyAdapter As New OracleDataAdapter
			Dim MyDataView As New DataView
			Dim MyDs As New DataSet

			MyCmd.CommandText = "PKG_APPLICATION.GetCurrencies"
			MyCmd.CommandType = CommandType.StoredProcedure
			MyCmd.Parameters.Clear()
			MyCmd.Parameters.Add("Currency", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
			MyCmd.Connection = MyConn.Open()
			MyAdapter.SelectCommand = MyCmd
			MyAdapter.Fill(MyDs, "Currency")
			MyDataView = MyDs.Tables("Currency").DefaultView
			MyConn.Close()
			Return MyDataView

		Catch ex As Exception
			ExceptionInfo.Show(ex)

		End Try

	End Function

	Public Property CurrDescription() As String
		Get

			Return m_str_curr_description
		End Get
		Set(ByVal Value As String)
			m_str_curr_description = Value.ToUpper
		End Set
	End Property

	Public Property CurrCode() As String
		Get

			Return m_str_Curr_Code
		End Get
		Set(ByVal Value As String)
			m_str_Curr_Code = Value.ToUpper
		End Set
	End Property

	Public Property CurrID() As Integer
		Get
			Return m_i_curr_id
		End Get
		Set(ByVal Value As Integer)
			m_i_curr_id = Value
		End Set
	End Property


	Public Property CurrUserID() As Integer
		Get
			Return m_i_curr_user_id
		End Get
		Set(ByVal Value As Integer)
			m_i_curr_user_id = Value
		End Set
	End Property
End Class
