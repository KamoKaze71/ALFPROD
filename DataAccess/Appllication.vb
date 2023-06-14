Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities
'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
'''         <revision>23</revision>
''' <summary><para>Provides DataAccess for Basic Application Data needed throuout the application</para></summary>
Public Class WyethAppllication

	Private m_str_ApSe_Variable As String
	Private m_str_ApSe_Value As String
	Private m_str_ApSe_Description As String
	Private m_str_ApSe_User_ID As Integer
	Private m_i_Ctry_ID As Integer
	Private m_i_apse_ID As Integer
	Private m_i_amsg_number As Integer
	Private m_str_AMsg_message As String

    Public Function GetValidTranDate(ByVal forDate As Date) As String

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyConn As New MyConnection
        Dim val As String
        forDate = DateAdd(DateInterval.Day, -1, forDate)
        Try

            MyCmd.CommandText = "PKG_APPLICATION.GetMaxTransmission"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_ret_val3", OracleDbType.Varchar2, 20, val, ParameterDirection.ReturnValue)
            MyCmd.Parameters.Add("v_date", OracleDbType.Date, ParameterDirection.Input).Value = forDate
            MyCmd.Connection = MyConn.Open()
            MyCmd.ExecuteScalar()
            val = Convert.ToString((MyCmd.Parameters(0).Value))

            Return val
        Catch ex As Exception

        Finally
            MyConn.Close()
        End Try
    End Function
    Public Shared Function getLastOrderEntry(ByVal forDate As Date) As String
        '*******************************************************************************
        '* Liefert das Datum des letzten Eintrags für einen bestimmten Tag
        '*******************************************************************************

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyConn As New MyConnection
        Dim val As String

        MyCmd.CommandText = "PKG_APPLICATION.GetMaxTransmission"
        MyCmd.CommandType = CommandType.StoredProcedure
        MyCmd.Parameters.Clear()
        MyCmd.Parameters.Add("v_ret_val3", OracleDbType.Varchar2, 20, val, ParameterDirection.ReturnValue)
        MyCmd.Parameters.Add("v_date", OracleDbType.Date, ParameterDirection.Input).Value = forDate
        MyCmd.Connection = MyConn.Open()
        MyCmd.ExecuteScalar()
        val = Convert.ToString((MyCmd.Parameters(0).Value))
        If val = "" Then
            val = "2003-01-01"
        End If
        MyConn.Close()
        Return val
    End Function
    Public Function GetStatus() As DataView
        Dim MyCmd As New OracleCommand
        Dim MyReader As OracleDataReader
        Dim MyConn As New MyConnection
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyDs As New DataSet

        Try
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "GetLogsStatus"
            MyCmd.Connection = MyConn.Open
            MyReader = MyCmd.ExecuteReader()

            'While MyReader.Read
            '	test = CStr(MyReader("column_name"))

            '	MyHashtable.Add(CStr(MyReader("column_name")), CStr(MyReader("data_type")))
            'End While



        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyReader.Close()
            MyConn.Close()
        End Try
    End Function

    Public Function DeleteApplicationSetting() As Boolean
        Dim MyCmd As New OracleCommand
        Dim MyConn As New MyConnection

        Try

            MyCmd.CommandText = "P_Application_Setting_Del_Proc"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_apse_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = ApSe_ID

            MyCmd.Connection = MyConn.Open()
            MyCmd.ExecuteNonQuery()


        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Return False
        Finally
            MyConn.Close()
        End Try
        Return True
    End Function
    Public Function DeleteApplicationMessage() As Boolean
        Dim MyCmd As New OracleCommand
        Dim MyConn As New MyConnection

        Try

            MyCmd.CommandText = "P_ALF_Message_Del_Proc"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_AMsg_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = ApSe_ID

            MyCmd.Connection = MyConn.Open()
            MyCmd.ExecuteNonQuery()

        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Return False
        Finally
            MyConn.Close()
        End Try
        Return True
    End Function
    Public Function InsertApplicationSetting() As Boolean
        Dim MyCmd As New OracleCommand

        Dim MyConn As New MyConnection

        Try

            MyCmd.CommandText = "P_Application_Setting_Ins_Proc"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_ApSe_Variable", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = ApSe_Variable
            MyCmd.Parameters.Add("v_ApSe_Value", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = ApSe_Value
            MyCmd.Parameters.Add("v_ApSe_Description", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = ApSe_Description
            MyCmd.Parameters.Add("v_ApSe_User_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = ApSe_User_ID
            MyCmd.Parameters.Add("v_Ctry_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = ApSe_Country_ID

            MyCmd.Connection = MyConn.Open()
            MyCmd.ExecuteNonQuery()


        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Return False
        Finally
            MyConn.Close()
        End Try
        Return True

    End Function
    Public Function InsertApplicationMessage() As Boolean
        Dim MyCmd As New OracleCommand

        Dim MyConn As New MyConnection

        Try

            MyCmd.CommandText = "P_ALF_Message_Ins_Proc"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_AMsg_Number", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = AMsgNumber
            MyCmd.Parameters.Add("v_AMsg_Message", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = AMsgMessage
            MyCmd.Parameters.Add("v_AMsg_User_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = ApSe_User_ID
            MyCmd.Parameters.Add("v_Ctry_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = ApSe_Country_ID


            MyCmd.Connection = MyConn.Open()
            MyCmd.ExecuteNonQuery()


        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Return False
        Finally
            MyConn.Close()
        End Try
        Return True

    End Function
    Public Function UpdateApplicationSetting() As Boolean
        Dim MyCmd As New OracleCommand

        Dim MyConn As New MyConnection

        Try

            MyCmd.CommandText = "P_Application_Setting_Upd_Proc"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_apse_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = ApSe_ID
            MyCmd.Parameters.Add("v_ApSe_Variable", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = ApSe_Variable
            MyCmd.Parameters.Add("v_ApSe_Value", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = ApSe_Value
            MyCmd.Parameters.Add("v_ApSe_Description", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = ApSe_Description
            MyCmd.Parameters.Add("v_ApSe_User_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = ApSe_User_ID
            MyCmd.Parameters.Add("v_Ctry_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = ApSe_Country_ID

            MyCmd.Connection = MyConn.Open()
            MyCmd.ExecuteNonQuery()


        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Return False
        Finally
            MyConn.Close()
        End Try
        Return True

    End Function

    Public Function UpdateApplicationMessage() As Boolean
        Dim MyCmd As New OracleCommand

        Dim MyConn As New MyConnection

        Try

            MyCmd.CommandText = "P_ALF_Message_Upd_Proc"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_AMsg_Id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = ApSe_ID
            MyCmd.Parameters.Add("v_AMsg_Number", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = AMsgNumber
            MyCmd.Parameters.Add("v_AMsg_Message", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = AMsgMessage
            MyCmd.Parameters.Add("v_AMsg_User_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = ApSe_User_ID
            MyCmd.Parameters.Add("v_Ctry_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = ApSe_Country_ID

            MyCmd.Connection = MyConn.Open()
            MyCmd.ExecuteNonQuery()



        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Return False
        Finally
            MyConn.Close()
        End Try
        Return True

    End Function

    Public Function GetApplicationSettings() As DataView

        Dim MyCmd As New OracleCommand
        Dim MyConn As New MyConnection
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyDs As New DataSet

        Try

            MyCmd.CommandText = "PKG_APPLICATION.GetApplicationSettings"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("ApplicationSettings", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_Ctry_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = ApSe_Country_ID

            MyCmd.Connection = MyConn.Open()

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "ApplicationSettings")
            MyDataView = MyDs.Tables("ApplicationSettings").DefaultView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try
        Return MyDataView
    End Function
    Public Function GetApplicationMessage() As String
        Dim MyCmd As New OracleCommand
        Dim MyConn As New MyConnection
        Dim MyReader As OracleDataReader
        Dim ret_val As String
        Dim val As Object

        Try


            MyCmd.CommandText = "PKG_APPLICATION.GetApplicationMessages"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("GetApplicationMessages", OracleDbType.RefCursor, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_AMSG_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = AMsgNumber
            MyCmd.Parameters.Add("v_Ctry_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = ApSe_Country_ID
            MyCmd.Connection = MyConn.Open()
            MyReader = MyCmd.ExecuteReader
            While MyReader.Read
                ret_val = MyReader("AMSG_MESSAGE")
            End While
            Return ret_val

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyReader.Close()
            MyConn.Close()
        End Try

    End Function

    Public Function GetAllApplicationMessages() As DataView

        Dim MyCmd As New OracleCommand
        Dim MyConn As New MyConnection
        Dim MyReader As OracleDataReader
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyDs As New DataSet

        Try


            MyCmd.CommandText = "PKG_APPLICATION.GetALLApplicationMessages"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("GetApplicationMessages", OracleDbType.RefCursor, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_Ctry_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = ApSe_Country_ID
            MyCmd.Connection = MyConn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "ApplicationSettings")
            MyDataView = MyDs.Tables("ApplicationSettings").DefaultView
            Return MyDataView
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally

            MyConn.Close()
        End Try

    End Function
    Public Property ApSe_Variable() As String
        Get
            Return m_str_ApSe_Variable
        End Get
        Set(ByVal Value As String)
            m_str_ApSe_Variable = Value
        End Set
    End Property
    Public Property ApSe_Value() As String
        Get
            Return m_str_ApSe_Value
        End Get
        Set(ByVal Value As String)
            m_str_ApSe_Value = Value
        End Set
    End Property
    Public Property ApSe_Description() As String
        Get
            Return m_str_ApSe_Description
        End Get
        Set(ByVal Value As String)
            m_str_ApSe_Description = Value
        End Set
    End Property
    Public Property ApSe_User_ID() As Integer
        Get
            Return m_str_ApSe_User_ID
        End Get
        Set(ByVal Value As Integer)
            m_str_ApSe_User_ID = Value
        End Set
    End Property
    Public Property ApSe_Country_ID() As Integer
        Get
            Return m_i_Ctry_ID
        End Get
        Set(ByVal Value As Integer)
            m_i_Ctry_ID = Value
        End Set
    End Property
    Public Property ApSe_ID() As Integer
        Get
            Return m_i_apse_ID
        End Get
        Set(ByVal Value As Integer)
            m_i_apse_ID = Value
        End Set
    End Property
    Public Property AMsgMessage() As String
        Get
            Return m_str_AMsg_message
        End Get
        Set(ByVal Value As String)
            m_str_AMsg_message = Value
        End Set
    End Property
    Public Property AMsgNumber() As Integer
        Get
            Return m_i_amsg_number
        End Get
        Set(ByVal Value As Integer)
            m_i_amsg_number = Value
        End Set
    End Property
End Class
