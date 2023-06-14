Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities
'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
'''         <revision>23</revision>
''' <summary><para>Provides DataAccess for T_Codes</para></summary>
Public Class WyethCodes

	Private m_i_code_id As Integer
	Private m_str_code_code As String
	Private m_str_code_description As String
	Private m_i_code_user_id As Integer
	Private m_i_code_ctry_id As Integer
	Private m_str_code_category As String

	
    Public Shared Function GetCodesByCat(ByVal category As String, ByVal ctry_id As Integer) As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection
        Try


            MyCmd.CommandText = "PKG_APPLICATION.GetCodesByCat"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Add("Codes", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_category", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = category
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = 56
            MyCmd.Connection = conn.Open()

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Codes")
            MyDataView = MyDs.Tables("codes").DefaultView

            Return MyDataView
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try
    End Function

    Public Function DeleteCode() As Boolean
        Dim MyCmd As New OracleCommand
        Dim MyConn As New MyConnection

        Try

            MyCmd.CommandText = "P_Code_del_Proc"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_code_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = CodeID

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

    Public Function InsertCode() As Boolean
        Dim MyCmd As New OracleCommand
        Dim MyConn As New MyConnection

        Try

            MyCmd.CommandText = "P_Code_Ins_Proc"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_code_Code", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = CodeCode
            MyCmd.Parameters.Add("v_code_description", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = CodeDescription
            MyCmd.Parameters.Add("v_Code_Category", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = CodeCategory
            MyCmd.Parameters.Add("v_code_user_ID ", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = CodeUserID
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = CodeCtryID

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

    Public Function UpdateCode() As Boolean
        Dim MyCmd As New OracleCommand
        Dim MyConn As New MyConnection

        Try


            MyCmd.CommandText = "P_Code_Upd_Proc"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_code_id", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = CodeID
            MyCmd.Parameters.Add("v_code_Code", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = CodeCode
            MyCmd.Parameters.Add("v_code_description", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = CodeDescription
            MyCmd.Parameters.Add("v_Code_Category", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = CodeCategory
            MyCmd.Parameters.Add("v_code_user_ID ", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = CodeUserID
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = CodeCtryID



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

    Public Property CodeID() As Integer
        Get
            Return m_i_code_id

        End Get
        Set(ByVal Value As Integer)
            m_i_code_id = Value
        End Set
    End Property
    Public Property CodeCtryID() As Integer
        Get
            Return m_i_code_ctry_id

        End Get
        Set(ByVal Value As Integer)
            m_i_code_ctry_id = Value
        End Set
    End Property

    Public Property CodeUserID() As Integer
        Get
            Return m_i_code_user_id

        End Get
        Set(ByVal Value As Integer)
            m_i_code_user_id = Value
        End Set
    End Property
    Public Property CodeCode() As String
        Get
            Return m_str_code_code

        End Get
        Set(ByVal Value As String)
            m_str_code_code = Value
        End Set
    End Property
    Public Property CodeDescription() As String
        Get
            Return m_str_code_description

        End Get
        Set(ByVal Value As String)
            m_str_code_description = Value
        End Set
    End Property
    Public Property CodeCategory() As String
        Get
            Return m_str_code_category

        End Get
        Set(ByVal Value As String)
            m_str_code_category = Value
        End Set
    End Property

End Class
