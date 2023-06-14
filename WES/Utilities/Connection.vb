Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Client.OracleGlobalization
Imports System
Imports System.Configuration

Public Class MyConnection

    Private conn As New OracleConnection
    Private m_str_connection As String '= Helper.CONNECTION_STRING_LIVE_SERVER         ' apply default connection string here

    Public Sub New()
        'MyBase.New()
        Me.ConnectionString = Settings.connectionString
    End Sub

    Public Function Open() As OracleConnection

        Try

            'check if Connection is already opened
            If conn.State = ConnectionState.Open Then

            Else

                conn.ConnectionString = ConnectionString
                conn.Open()
               
            End If

            'SetNLSParams(conn)
            'SetNLSParams_tmp(conn)
            Return conn
        Catch ex As Exception
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        Finally

        End Try
    End Function

    Public Sub SetNLSParams(ByRef conn As OracleConnection)
        ' this is how it should work - but there is a bug in ODP.NET 9.2.0.4.01
        ' this method works only with certain Regional Options  in Windows Control Panel is 
        ' Oracle says that this bug will be fixed in future releases!!
        Dim sessionGlob As OracleGlobalization
        sessionGlob = conn.GetSessionInfo()
        sessionGlob.Territory = "GERMANY"
        sessionGlob.Language = "GERMAN"
        sessionGlob.DateFormat = "yyyy-mm-dd"
        sessionGlob.NumericCharacters = ".,"
        conn.SetSessionInfo(sessionGlob)

    End Sub

    Public Sub SetNLSParams_tmp(ByRef conn As OracleConnection)

        Dim MyCmd As New OracleCommand
        MyCmd.CommandType = CommandType.Text
        MyCmd.CommandText = "ALTER SESSION SET NLS_LANGUAGE='GERMAN'"
        MyCmd.Connection = conn
        MyCmd.ExecuteNonQuery()
        MyCmd.CommandText = "ALTER SESSION SET NLS_TERRITORY='GERMANY'"
        MyCmd.ExecuteNonQuery()
        MyCmd.CommandText = "ALTER SESSION SET NLS_DATE_FORMAT='yyyy-mm-dd'"
        MyCmd.ExecuteNonQuery()
        MyCmd.CommandText = "ALTER SESSION SET NLS_NUMERIC_CHARACTERS='.,'"
        MyCmd.ExecuteNonQuery()

    End Sub

    Public Function Close()
        Try
            

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
            End If
        End Try

    End Function

    Public Property ConnectionString() As String
        Get
            Return m_str_connection

        End Get
        Set(ByVal Value As String)
            m_str_connection = Value
        End Set
    End Property
End Class

