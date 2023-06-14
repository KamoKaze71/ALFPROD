Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities
'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
'''         <revision>23</revision>
''' <summary><para>BaseClass for OracleDataAccess</para></summary>
Public Class DataAccessBaseClass

    Private dbConnection As MyConnection

    '****************************************************************************************************
    '* makeDBConnection 
    '****************************************************************************************************
    Private Function makeDBConnection() As OracleConnection
        Me.dbConnection = New MyConnection
        Return Me.dbConnection.Open()
    End Function

    '****************************************************************************************************
    '* closeDBConnection 
    '****************************************************************************************************
    Private Sub closeDBConnection()
        Me.dbConnection.Close()
    End Sub

    '****************************************************************************************************
    '* executes a Stored procedure and returns the result as a DATAVIEW 
    '****************************************************************************************************
    Public Function executeStoredProcedure(ByVal storedProcedureName As String, ByVal parameterArray As OracleParameter()) As DataView
        Dim MyCmd As New OracleCommand
        Dim MyDataView As New DataView
        Dim MyDs As New DataSet
        Dim MyAdapter As New OracleDataAdapter
        Try
            MyCmd.Parameters.Clear()
            MyCmd.CommandText = storedProcedureName
            MyCmd.CommandType = CommandType.StoredProcedure

            For i As Integer = 0 To parameterArray.Length - 1
                MyCmd.Parameters.Add(parameterArray(i))
            Next

            MyCmd.Connection = Me.makeDBConnection()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Table")
            MyDataView = MyDs.Tables("Table").DefaultView

            Return MyDataView
        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Throw ex
        Finally
            Me.closeDBConnection()
        End Try
    End Function

    '****************************************************************************************************
    '* executes a Stored procedure as a nonquery. for updates, deletes, etc. 
    '****************************************************************************************************
    Public Sub executeNonQuery(ByVal storedProcedureName As String, ByVal parameterArray As OracleParameter())
        Dim MyCmd As New OracleCommand
        Dim MyDataView As New DataView
        Dim MyDs As New DataSet
        Dim MyAdapter As New OracleDataAdapter
        Try
            MyCmd.Parameters.Clear()
            MyCmd.CommandText = storedProcedureName
            MyCmd.CommandType = CommandType.StoredProcedure

            For i As Integer = 0 To parameterArray.Length - 1
                MyCmd.Parameters.Add(parameterArray(i))
            Next

            MyCmd.Connection = Me.makeDBConnection()
            MyCmd.ExecuteNonQuery()
        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Throw ex
        Finally
            Me.closeDBConnection()
        End Try
    End Sub

    '****************************************************************************************************
    '* executes a Stored procedure and returns a value of it. returns the first parameter
    '****************************************************************************************************
    Public Function executeScalar(ByVal storedProcedureName As String, ByVal parameterArray As OracleParameter()) As String
        Dim MyCmd As New OracleCommand
        Try
            MyCmd.Parameters.Clear()
            MyCmd.CommandText = storedProcedureName
            MyCmd.CommandType = CommandType.StoredProcedure

            For i As Integer = 0 To parameterArray.Length - 1
                MyCmd.Parameters.Add(parameterArray(i))
            Next

            MyCmd.Connection = Me.makeDBConnection()
            MyCmd.ExecuteScalar()

            
            Return Convert.ToString((MyCmd.Parameters(0).Value.ToString))

        Catch ex As Exception
            Throw ex
        Finally
            Me.closeDBConnection()
        End Try
    End Function

End Class
