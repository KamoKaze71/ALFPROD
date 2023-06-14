
Imports Oracle.DataAccess.Client

Imports Wyeth.Utilities

'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
'''         <revision>23</revision>
''' <summary><para>Provides DataAccess for T_Country</para></summary>
Public Class WyethCountry

	Private m_str_CountryDescription As String
	Private m_str_CountryCode As String
	Private m_i_ctry_id As Integer
	Private m_i_ctry_curr_id As Integer
    Private m_i_ctry_user_id As Integer
    Private m_i_CountryBS_CODE As String
    Private m_i_CountryPL_CODE As String
    Private m_i_CountryCurr_CODE As String

	
	

	Public Sub New()
		MyBase.New()
	
		
	End Sub
	Public Function InsertCountry() As Boolean
		Dim MyCmd As New OracleCommand
		Dim MyConn As New MyConnection
	

		Try

			MyCmd.CommandText = "P_Country_INS_PROC"
			MyCmd.CommandType = CommandType.StoredProcedure
			MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_Ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = 0
            MyCmd.Parameters.Add("v_Ctry_Code", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = CountryCode
            MyCmd.Parameters.Add("v_Ctry_Description", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = CountryDescription
			MyCmd.Parameters.Add("v_Ctry_User_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = CountryUserID
            MyCmd.Parameters.Add("v_Curr_code ", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = CountryCurr_CODE
            MyCmd.Parameters.Add("v_Ctry_bs_code", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = CountryBS_CODE
            MyCmd.Parameters.Add("v_Curr_pl_code ", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = CountryPL_CODE

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

	Public Function UpdateCountry() As Boolean
		Dim MyCmd As New OracleCommand
		Dim MyConn As New MyConnection
	
		Try

			MyCmd.CommandText = "P_Country_Upd_Proc"
			MyCmd.CommandType = CommandType.StoredProcedure
			MyCmd.Parameters.Clear()
			MyCmd.Parameters.Add("v_Ctry_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = CountryID
			MyCmd.Parameters.Add("v_Ctry_Code", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = CountryCode
			MyCmd.Parameters.Add("v_Ctry_Description", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = CountryDescription
			MyCmd.Parameters.Add("v_Ctry_User_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = CountryUserID
			MyCmd.Parameters.Add("v_Curr_ID ", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = CountryCurrencyID
            MyCmd.Parameters.Add("v_Ctry_bs_code", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = CountryBS_CODE
            MyCmd.Parameters.Add("v_Curr_pl_code ", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = CountryPL_CODE


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

	Public Function DeleteCountry() As Boolean
		Dim MyCmd As New OracleCommand
		Dim MyConn As New MyConnection
	

		Try

			MyCmd.CommandText = "P_Country_DEL_PROC"
			MyCmd.CommandType = CommandType.StoredProcedure

			MyCmd.Parameters.Clear()
			MyCmd.Parameters.Add("v_country_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = CountryID

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

	Public Function GetCountries() As DataView
		Try
			Dim MyCmd As New OracleCommand
			Dim MyConn As New MyConnection
			Dim MyAdapter As New OracleDataAdapter
			Dim MyDataView As New DataView
			Dim MyDs As New DataSet

			MyCmd.CommandText = "PKG_APPLICATION.GetCountries"
			MyCmd.CommandType = CommandType.StoredProcedure

			'MyCmd.Parameters.Clear()
			MyCmd.Parameters.Add("Country", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)


			MyCmd.Connection = MyConn.Open()
			MyAdapter.SelectCommand = MyCmd
			MyAdapter.Fill(MyDs, "Countries")
			MyDataView = MyDs.Tables("Countries").DefaultView
			MyConn.Close()
			Return MyDataView
		Catch ex As Exception
			ExceptionInfo.Show(ex)

		End Try
    End Function

   

    Public Function GetCurrencies() As DataView
        Dim MyCmd As New OracleCommand
        Dim MyConn As New MyConnection
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyDs As New DataSet
        Try

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

    Public Property CountryDescription() As String
        Get
            m_str_CountryDescription = m_str_CountryDescription.ToUpper
            Return m_str_CountryDescription
        End Get
        Set(ByVal Value As String)
            m_str_CountryDescription = Value.ToUpper
        End Set
    End Property

    Public Property CountryCode() As String
        Get
            m_str_CountryCode = m_str_CountryCode.ToUpper
            Return m_str_CountryCode
        End Get
        Set(ByVal Value As String)
            m_str_CountryCode = Value.ToUpper
        End Set
    End Property

    Public Property CountryID() As Integer
        Get
            Return m_i_ctry_id
        End Get
        Set(ByVal Value As Integer)
            m_i_ctry_id = Value
        End Set
    End Property

    Public Property CountryCurrencyID() As Integer
        Get
            Return m_i_ctry_curr_id
        End Get
        Set(ByVal Value As Integer)
            m_i_ctry_curr_id = Value
        End Set
    End Property
    Public Property CountryUserID() As Integer
        Get
            Return m_i_ctry_user_id
        End Get
        Set(ByVal Value As Integer)
            m_i_ctry_user_id = Value
        End Set
    End Property

    Public Property CountryPL_CODE() As String
        Get
            Return m_i_CountryPL_CODE
        End Get
        Set(ByVal Value As String)
            m_i_CountryPL_CODE = Value
        End Set
    End Property

    Public Property CountryBS_CODE() As String
        Get
            Return m_i_CountryBS_CODE
        End Get
        Set(ByVal value As String)
            m_i_CountryBS_CODE = value
        End Set
    End Property



    Public Property CountryCurr_CODE() As String
        Get
            Return m_i_CountryCurr_CODE
        End Get
        Set(ByVal value As String)
            m_i_CountryCurr_CODE = value
        End Set
    End Property
End Class

