Imports Oracle.DataAccess.Client
Imports System.Web
Imports Wyeth.Utilities.Helper
Imports System
Imports System.IO


Public Class Log
    Private m_strDescription As String
    Private m_strSource As String
	Private m_iUserID As Integer
	Private m_iCountryCode As String
	Private m_iCodeCode As String

	Sub New()

		MyBase.New()
		Try
			'  If IsError(HttpContext.Current.Session("User_id")) Then
			m_iUserID = 280
			m_iCountryCode = "AT"
			m_iCodeCode = "FTP"


            m_iUserID = HttpContext.Current.Session("User_id")
			

		Catch ex As Exception
			m_iUserID = 280
			m_iCountryCode = "AT"
		Finally
			m_strDescription = "not set"
			m_strSource = "not set"
		End Try

	End Sub

	Public Property Description() As String
		Get
			Return m_strDescription
		End Get
		Set(ByVal Value As String)
			m_strDescription = Value
		End Set
	End Property
	Public Property Source() As String
		Get
			Return m_strSource
		End Get
		Set(ByVal Value As String)
			m_strSource = Value
		End Set
	End Property
	Public Property UserID() As Integer
		Get
			Return m_iUserID
		End Get
		Set(ByVal Value As Integer)
			m_iUserID = Value
		End Set
	End Property
	Public Property CountryCode() As String
		Get
			Return m_iCountryCode
		End Get
		Set(ByVal Value As String)
			m_iCountryCode = Value
		End Set
	End Property
	Public Property CodeCode() As String
		Get
			Return m_iCodeCode
		End Get
		Set(ByVal Value As String)
			m_iCodeCode = Value
		End Set
	End Property
	
    Public Function DeleteLogs(ByVal ctry_id As Integer, ByVal LogType As Integer, ByVal startDate As Date, ByVal endDate As Date) As Boolean

        Dim MyCmd As New OracleCommand
        Dim conn As New MyConnection
        Try

            MyCmd.CommandText = "PKG_APPLICATION.DeleteLogs"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = ctry_id
            MyCmd.Parameters.Add("v_log_type", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = LogType
            MyCmd.Parameters.Add("v_startDate", OracleDbType.Date, ParameterDirection.Input).Value = startDate
            MyCmd.Parameters.Add("v_endDate", OracleDbType.Date, ParameterDirection.Input).Value = endDate

            MyCmd.Connection = conn.Open()
            MyCmd.ExecuteNonQuery()

            MyCmd.Dispose()
            conn.Close()
        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Return False
        End Try
        Return True

    End Function
    Public Function insert(Optional ByVal connstr = "") As Boolean
        Dim MyCmd As New OracleCommand
        Dim MyConnection As New MyConnection
        Try
            If connstr = "" Or connstr Is Nothing Then
            Else
                MyConnection.ConnectionString = connstr
            End If
            MyCmd.CommandText = "P_Logs_Ins_Proc"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Add("v_Logs_Source", OracleDbType.Varchar2, ParameterDirection.Input).Value = Source
            MyCmd.Parameters.Add("v_Logs_Description", OracleDbType.Varchar2, ParameterDirection.Input).Value = Description
            MyCmd.Parameters.Add("v_Logs_User_ID", OracleDbType.Int16, ParameterDirection.Input).Value = UserID
            MyCmd.Parameters.Add("v_Ctry_Code", OracleDbType.Varchar2, ParameterDirection.Input).Value = CountryCode
            MyCmd.Parameters.Add("v_Code_Code", OracleDbType.Varchar2, ParameterDirection.Input).Value = CodeCode


            MyCmd.Connection = MyConnection.Open()
            MyCmd.ExecuteNonQuery()

            Return True
        Catch ex As Exception
            Return False
        Finally
            MyConnection.Close()
            MyCmd.Dispose()
        End Try

    End Function
    Public Function ViewLogs(ByVal code_id As Integer, ByVal startDate As Date, ByVal endDate As Date) As DataView

        Dim MyConnection As New MyConnection
        Dim MyCmd As New OracleCommand
        Dim MyDataView As New DataView
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDs As New DataSet

        Try
            MyCmd.CommandText = "PKG_APPLICATION.GetLogs"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Add("Logs", OracleDbType.RefCursor, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_code_id", OracleDbType.Int32, ParameterDirection.Input).Value = code_id
            MyCmd.Parameters.Add("v_startDate", OracleDbType.Date, ParameterDirection.Input).Value = startDate
            MyCmd.Parameters.Add("v_endDate", OracleDbType.Date, ParameterDirection.Input).Value = endDate

            MyCmd.Connection = MyConnection.Open()

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Logs")

            MyDataView = MyDs.Tables("Logs").DefaultView

            MyCmd.Dispose()
            MyAdapter.Dispose()
            MyConnection.Close()

            Return MyDataView

        Catch ex As Exception
            Exit Function

            ExceptionInfo.Show(ex)

        End Try

    End Function

End Class
