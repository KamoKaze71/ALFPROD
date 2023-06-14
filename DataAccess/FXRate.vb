Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities
'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
''' <summary><para>Provides DataAccess for T_FXRATE</para></summary>
Public Class WyethFXRate

	Private m_i_fxrate_id As Integer
	Private m_i_fxrare_rate As Double
	Private m_d_fxrate_date_from As Date
	Private m_d_fxrate_date_to As Date
	Private m_i_fxrate_user_id As Integer
	Private m_i_fxrate_curr_id_from As Integer
	Private m_i_fxrate_curr_id_to As Integer

	Public Function GetCurrencies() As DataView


		Dim MyCmd As New OracleCommand
		Dim Conn As New MyConnection
		Dim MyDataView As New DataView
		Dim MyDs As New DataSet
		Dim MyAdapter As New OracleDataAdapter

		Try

			MyCmd.CommandText = "PKG_APPLICATION.GetCurrencies"
			MyCmd.CommandType = CommandType.StoredProcedure
			MyCmd.Parameters.Clear()
			MyCmd.Parameters.Add("Currency", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
			MyCmd.Connection = Conn.Open()
			MyAdapter.SelectCommand = MyCmd
			MyAdapter.Fill(MyDs, "Currency")
			MyDataView = MyDs.Tables("Currency").DefaultView

			Return MyDataView

		Catch ex As Exception
			ExceptionInfo.Show(ex)
		Finally

			Conn.Close()

		End Try

	End Function
	Public Function GetFXRates() As DataView


		Dim MyCmd As New OracleCommand
		Dim Conn As New MyConnection
		Dim MyDataView As New DataView
		Dim MyDs As New DataSet
		Dim MyAdapter As New OracleDataAdapter

		Try




			MyCmd.CommandText = "PKG_APPLICATION.GetFxRates"
			MyCmd.CommandType = CommandType.StoredProcedure
			MyCmd.Parameters.Clear()
			MyCmd.Parameters.Add("Currency", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
			MyCmd.Parameters.Add("v_curr_id_from", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = FXRateCurrIDFrom
			MyCmd.Parameters.Add("v_curr_id_to", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = FXRateCurrIDTo

			MyCmd.Connection = Conn.Open()
			MyAdapter.SelectCommand = MyCmd
			MyAdapter.Fill(MyDs, "FXRate")
			MyDataView = MyDs.Tables("FxRate").DefaultView

			Return MyDataView
		Catch ex As Exception
			ExceptionInfo.Show(ex)
		Finally
			Conn.Close()

		End Try

	End Function

	'Public Function insert() As Boolean
	'	Try
	'		MyCmd.CommandText = "P_Holiday_Ins_Proc"
	'		MyCmd.CommandType = CommandType.StoredProcedure


	'		MyCmd.Parameters.Add("v_Holi_Day", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = HoliDayDay
	'		MyCmd.Parameters.Add("v_Holi_User_ID", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = HoliDayUserID
	'		MyCmd.Parameters.Add("v_Ctry_ID", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = HoliDayCountryID



	'		MyCmd.Connection = Conn.Open()
	'		MyCmd.ExecuteNonQuery()


	'		Conn.Close()
	'	Catch ex As Exception
	'		ExceptionInfo.Show(ex)
	'		Return False
	'	End Try
	'	Return True
	'End Function

	'Public Function delete() As Boolean
	'	Try
	'		MyCmd.CommandText = "P_Holiday_Del_Proc"
	'		MyCmd.CommandType = CommandType.StoredProcedure

	'		MyCmd.Parameters.Add("v_holi_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = HoliDayID
	'		MyCmd.Connection = Conn.Open()
	'		MyCmd.ExecuteNonQuery()
	'		Conn.Close()
	'	Catch ex As Exception
	'		ExceptionInfo.Show(ex)
	'		Return False
	'	End Try
	'	Return True
	'End Function
	'Public Function Update() As Boolean
	'	Try
	'		MyCmd.CommandText = "P_Holiday_Upd_Proc"
	'		MyCmd.CommandType = CommandType.StoredProcedure

	'		MyCmd.Parameters.Clear()
	'		MyCmd.Parameters.Add("v_holi_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = HoliDayID
	'		MyCmd.Parameters.Add("v_Holi_Day", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = HoliDayDay.ToString(Wyeth.Utilities.Helper.DATEFORMAT_STRING, HttpContext.Current.Application("MyDTFI"))
	'		MyCmd.Parameters.Add("v_Holi_User_ID", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = HoliDayUserID
	'		MyCmd.Parameters.Add("v_Ctry_ID", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = HoliDayCountryID


	'		MyCmd.Connection = Conn.Open()
	'		MyCmd.ExecuteNonQuery()


	'		Conn.Close()
	'	Catch ex As Exception
	'		ExceptionInfo.Show(ex)
	'		Return False
	'	End Try
	'	Return True
	'End Function


	Public Property FXRateID() As Integer
		Get
			Return m_i_fxrate_id
		End Get
		Set(ByVal Value As Integer)
			m_i_fxrate_id = Value
		End Set
	End Property
	Public Property FXRateRate() As Double
		Get
			Return m_i_fxrare_rate
		End Get
		Set(ByVal Value As Double)
			m_i_fxrare_rate = Value
		End Set
	End Property
	Public Property FXRateCurrIDFrom() As Integer
		Get
			Return m_i_fxrate_curr_id_from

		End Get
		Set(ByVal Value As Integer)
			m_i_fxrate_curr_id_from = Value
		End Set
	End Property
	Public Property FXRateCurrIDTo() As Integer
		Get
			Return m_i_fxrate_curr_id_to
		End Get
		Set(ByVal Value As Integer)
			m_i_fxrate_curr_id_to = Value
		End Set
	End Property
End Class
