Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities
Imports Wyeth.Utilities.DateHandling

'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
''' <summary><para>Provides DataAccess for T_Holiday</para></summary>
Public Class Holiday

	Private m_i_holiday_id As Integer
	Private m_i_holiday_user_id As Integer
	Private m_d_holiday_ctry_id As Integer
	Private m_d_holiday_day As Date

	Public Function insert() As Boolean

		Dim MyCmd As New OracleCommand
		Dim Conn As New MyConnection

		Try
			MyCmd.CommandText = "P_Holiday_Ins_Proc"
			MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Add("v_Holi_Day", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = HoliDayDay
			MyCmd.Parameters.Add("v_Holi_User_ID", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = HoliDayUserID
			MyCmd.Parameters.Add("v_Ctry_ID", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = HoliDayCountryID

			MyCmd.Connection = Conn.Open()
			MyCmd.ExecuteNonQuery()

		Catch ex As Exception
			ExceptionInfo.Show(ex)
			Return False
		Finally
			Conn.Close()
		End Try
		Return True

	End Function

	Public Function delete() As Boolean

		Dim MyCmd As New OracleCommand
		Dim Conn As New MyConnection

		Try
			MyCmd.CommandText = "P_Holiday_Del_Proc"
			MyCmd.CommandType = CommandType.StoredProcedure

			MyCmd.Parameters.Add("v_holi_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = HoliDayID

			MyCmd.Connection = Conn.Open()
			MyCmd.ExecuteNonQuery()

		Catch ex As Exception
			ExceptionInfo.Show(ex)
			Return False
		Finally
			Conn.Close()
		End Try
		Return True
	End Function
	Public Function Update() As Boolean
		Dim MyCmd As New OracleCommand
		Dim Conn As New MyConnection
		
		Try
			MyCmd.CommandText = "P_Holiday_Upd_Proc"
			MyCmd.CommandType = CommandType.StoredProcedure

			MyCmd.Parameters.Clear()
			MyCmd.Parameters.Add("v_holi_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = HoliDayID
            MyCmd.Parameters.Add("v_Holi_Day", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = HoliDayDay 'FormatDateForDB(HoliDayDay)
			MyCmd.Parameters.Add("v_Holi_User_ID", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = HoliDayUserID
			MyCmd.Parameters.Add("v_Ctry_ID", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = HoliDayCountryID

			MyCmd.Connection = Conn.Open()
			MyCmd.ExecuteNonQuery()
			Conn.Close()

		Catch ex As Exception
			ExceptionInfo.Show(ex)
			Return False
		Finally
			Conn.Close()
		End Try

		Return True
	End Function

	Public Function GetHoliDays() As DataView
		Dim MyCmd As New OracleCommand
		Dim Conn As New MyConnection
		Dim MyDataView As New DataView
		Dim MyDs As New DataSet
		Dim MyAdapter As New OracleDataAdapter
		Try
			MyCmd.Parameters.Clear()
			MyCmd.Parameters.Add("Holidays", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
			MyCmd.Parameters.Add("v_Ctry_ID", OracleDbType.Int32, ParameterDirection.Input).Value = HoliDayCountryID
			MyCmd.CommandText = "PKG_APPLICATION.GetHolidays"
			MyCmd.CommandType = CommandType.StoredProcedure
			MyCmd.Connection = Conn.Open()

			MyAdapter.SelectCommand = MyCmd
			MyAdapter.Fill(MyDs, "Holidays")

			MyDataView = MyDs.Tables("Holidays").DefaultView

			Return MyDataView

		Catch ex As Exception
			ExceptionInfo.Show(ex)
		Finally
			Conn.Close()

		End Try

    End Function

    Public Function getLatestHolidayEntry() As Date
        Dim dataBase As New DataAccessBaseClass
        Dim parameters(1) As OracleParameter
        parameters(0) = New OracleParameter("Holidays", OracleDbType.Varchar2, ParameterDirection.ReturnValue)

        Dim param1 As New OracleParameter("v_ctry_id", OracleDbType.Int32, ParameterDirection.Input)
        param1.Value = HoliDayCountryID
        parameters(1) = param1

        Dim ret As String
        ret = dataBase.executeScalar("PKG_APPLICATION.GetLastHoliDay", parameters)

        Return CDate(ret)
    End Function

    Public Function GetHolidaysForMonth(ByVal month As Integer, ByVal year As Integer) As DataView
        Dim monthValue As String

        If month < 10 Then
            monthValue = "0" & month
        Else
            monthValue = month
        End If

        Dim MyCmd As New OracleCommand
        Dim Conn As New MyConnection
        Dim MyDataView As New DataView
        Dim MyDs As New DataSet
        Dim MyAdapter As New OracleDataAdapter
        Try
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Holidays", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_Ctry_ID", OracleDbType.Int32, ParameterDirection.Input).Value = HoliDayCountryID
            MyCmd.Parameters.Add("v_month", OracleDbType.Varchar2, ParameterDirection.Input).Value = monthValue
            MyCmd.Parameters.Add("v_year", OracleDbType.Int32, ParameterDirection.Input).Value = year
            MyCmd.CommandText = "PKG_APPLICATION.GetHoliDaysformonth"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Connection = Conn.Open()

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Holidays")

            MyDataView = MyDs.Tables("Holidays").DefaultView

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Conn.Close()

        End Try

    End Function

    Public Property HoliDayID() As Integer
        Get
            Return m_i_holiday_id
        End Get
        Set(ByVal Value As Integer)
            m_i_holiday_id = Value
        End Set
    End Property
    Public Property HoliDayUserID() As Integer
        Get
            Return m_i_holiday_user_id
        End Get
        Set(ByVal Value As Integer)
            m_i_holiday_user_id = Value
        End Set
    End Property


    Public Property HoliDayDay() As Date
        Get

            Return m_d_holiday_day

        End Get
        Set(ByVal Value As Date)
            m_d_holiday_day = Value
        End Set
    End Property

    Public Property HoliDayCountryID() As Integer
        Get
            Return m_d_holiday_ctry_id
        End Get
        Set(ByVal Value As Integer)
            m_d_holiday_ctry_id = Value
        End Set
    End Property
End Class
