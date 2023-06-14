Imports System
Imports System.Data
Imports Wyeth.Utilities
Imports Oracle.DataAccess.Client


'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
'''         <revision>23</revision>
''' <summary><para>Provides DataAccess for APO_BCountry Import/Export</para></summary>
Public Class APO_BCountry
    Implements IDisposable

    Dim conn As New MyConnection
    Dim MyCmd As New OracleCommand


    Public Sub New()
        'MyCmd.CommandText = "P_T_TEMP_APO_BCountry_INS"
        'MyCmd.CommandType = CommandType.StoredProcedure
        MyCmd.CommandText = "P_T_TEMP_APO_BCountry_INS"
        MyCmd.CommandType = CommandType.Text
        MyCmd.Connection = conn.Open()
    End Sub


    Public Function InsertItem(ByVal item As Object()) As Boolean

        Try
            Dim sqlstr As String = "INSERT INTO TMP_T_APO_BCOUNTRY ("
            Dim sqlFields As String
            Dim sqlValues As String = " VALUES("
            Dim count As Integer = 0

            For Each o As Object In item

                count = count + 1
                MyCmd.Parameters.Add("f" & count.ToString, OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = Convert.ToString(o)
                sqlFields = sqlFields & "f" & count.ToString() & ","
                sqlValues = sqlValues & ":f" & count.ToString() & ","

            Next

            sqlValues = sqlValues.TrimEnd(",")
            sqlValues = sqlValues + ")"

            sqlFields = sqlFields.TrimEnd(",")
            sqlFields = sqlFields & ")"

            MyCmd.CommandText = sqlstr + sqlFields + sqlValues
            MyCmd.ExecuteNonQuery()

            count = 0

        Catch ex As Exception
            Throw ex
        Finally
            MyCmd.Parameters.Clear()
        End Try
        Return True
    End Function

    Public Function APOB_Country_SartImportFromTempTables() As String

        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Dim val As String

        Try

            MyCmd.CommandText = "PKG_IMPORT_B_COUNTRIES.F_Import"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("code_id", OracleDbType.Int32, val, ParameterDirection.ReturnValue)
            MyCmd.Connection = Myconn.Open()
            MyCmd.ExecuteScalar()
            val = Convert.ToString(MyCmd.Parameters(0).Value)


            Return val
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Myconn.Close()

        End Try
    End Function

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Overridable Sub Dispose() Implements IDisposable.Dispose
        Try
            GC.SuppressFinalize(Me)
            conn.Close()
        Catch ex As Exception

        End Try


    End Sub

End Class
