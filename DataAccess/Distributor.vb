Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities

'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
''' <summary><para>Provides DataAccess for T_Distributor</para></summary>
Public Class WyethDistributor

	Private m_i_dist_id As Integer
	Private m_i_ctry_id As Integer
	Private m_i_dist_number As String
	Private m_str_dist_name As String
	Private m_str_description As String
	Private m_i_dist_user_id As Integer

	Public Function GetDistributors() As DataView

		Dim MyCmd As New OracleCommand
		Dim Conn As New MyConnection
		Dim MyDataView As New DataView
		Dim MyDs As New DataSet
		Dim MyAdapter As New OracleDataAdapter

		Try
			MyCmd.CommandText = "PKG_APPLICATION.GetDistributors"

			MyCmd.CommandType = CommandType.StoredProcedure
			MyCmd.Parameters.Clear()
			MyCmd.Parameters.Add("Distributors", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
			MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = DistCtryID
			MyCmd.Connection = Conn.Open()
			MyAdapter.SelectCommand = MyCmd
			MyAdapter.Fill(MyDs, "Distributors")
			MyDataView = MyDs.Tables("Distributors").DefaultView

			Return MyDataView

		Catch ex As Exception
			ExceptionInfo.Show(ex)
		Finally
			Conn.Close()

		End Try
	End Function

	Public Function DeleteDistributors() As Boolean

		Dim MyCmd As New OracleCommand
		Dim Conn As New MyConnection


		Try
			MyCmd.CommandText = "P_Distributor_Del_Proc"
			MyCmd.CommandType = CommandType.StoredProcedure
			MyCmd.Parameters.Clear()
			MyCmd.Parameters.Add("v_Dist_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = DistID
			MyCmd.Connection = Conn.Open()

			MyCmd.ExecuteNonQuery()

			Return True

		Catch ex As Exception
			ExceptionInfo.Show(ex)
			Return False
		Finally
			Conn.Close()
		End Try
	End Function
	Public Function InsertDistributor() As Boolean

		Dim MyCmd As New OracleCommand
		Dim Conn As New MyConnection

		Try
			MyCmd.CommandText = "P_Distributor_Ins_Proc"
			MyCmd.CommandType = CommandType.StoredProcedure
			MyCmd.Parameters.Clear()
			MyCmd.Parameters.Add("v_Dist_Number", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = DistNumber
			MyCmd.Parameters.Add("v_Dist_Name", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = DistName
			MyCmd.Parameters.Add("v_Dist_Description", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = DistDescription
			MyCmd.Parameters.Add("v_Dist_User_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = DistUserID
			MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = DistCtryID
			MyCmd.Connection = Conn.Open()
			MyCmd.ExecuteNonQuery()

			Return True

		Catch ex As Exception
			ExceptionInfo.Show(ex)
			Return False
		Finally
			Conn.Close()
		End Try
	End Function
	Public Function UpdateDistributor() As Boolean

		Dim MyCmd As New OracleCommand
		Dim Conn As New MyConnection

		Try

			MyCmd.CommandText = "P_Distributor_Upd_Proc"
			MyCmd.CommandType = CommandType.StoredProcedure
			MyCmd.Parameters.Clear()

			MyCmd.Parameters.Add("v_Dist_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = DistID
			MyCmd.Parameters.Add("v_Dist_Number", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = DistNumber
			MyCmd.Parameters.Add("v_Dist_Name", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = DistName
			MyCmd.Parameters.Add("v_Dist_Description", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = DistDescription
			MyCmd.Parameters.Add("v_Dist_User_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = DistUserID
			MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = DistCtryID
			MyCmd.Connection = Conn.Open()
			MyCmd.ExecuteNonQuery()

			Return True

		Catch ex As Exception
			ExceptionInfo.Show(ex)
			Return False
		Finally
			Conn.Close()
		End Try
	End Function

	Public Property DistID() As Integer
		Get
			Return m_i_dist_id
		End Get
		Set(ByVal Value As Integer)
			m_i_dist_id = Value
		End Set
	End Property
	Public Property DistCtryID() As Integer
		Get
			Return m_i_ctry_id
		End Get
		Set(ByVal Value As Integer)
			m_i_ctry_id = Value
		End Set
	End Property
	Public Property DistUserID() As Integer
		Get
			Return m_i_dist_user_id
		End Get
		Set(ByVal Value As Integer)
			m_i_dist_user_id = Value
		End Set
	End Property

	Public Property DistNumber() As Integer
		Get
			Return m_i_dist_number
		End Get
		Set(ByVal Value As Integer)
			m_i_dist_number = Value
		End Set
	End Property
	Public Property DistDescription() As String
		Get
			Return m_str_description
		End Get
		Set(ByVal Value As String)
			m_str_description = Value
		End Set
	End Property

	Public Property DistName() As String
		Get
			Return m_str_dist_name
		End Get
		Set(ByVal Value As String)
			m_str_dist_name = Value
		End Set
	End Property



End Class
