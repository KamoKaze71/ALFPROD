Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities
'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
'''         <revision>1</revision>
''' <summary><para>Helper Class  for DataImport from Distributors</para></summary>
'''<seealso cref="Utilities.FTP">[label]</seealso> 
Public Class WyethImportHelper
    Public Shared Function readAppVars() As Hashtable

        'Get Vars from DB and Put it into MyCollection
        Dim MyConnection As New MyConnection
        Dim MyReader As OracleDataReader
        Dim MyCmd As New OracleCommand
        Dim MyCollection As New Hashtable

        Try

            MyCollection.Clear()
            MyCmd.Parameters.Clear()
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_APPLICATION.GetAppVars"
            MyCmd.Parameters.Add("AppVars", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Connection = MyConnection.Open()
            MyReader = MyCmd.ExecuteReader(CommandBehavior.CloseConnection)
            Do While MyReader.Read
                MyCollection.Add(MyReader("apse_variable"), MyReader("apse_value"))
            Loop

        Catch ex As Exception
            Throw ex

        Finally
            MyConnection.Close()
            MyReader.Close()
            MyReader.Dispose()
            MyCmd.Dispose()
            MyConnection = Nothing
            MyReader = Nothing
            MyReader = Nothing
            MyCmd = Nothing
        End Try

        Return MyCollection

    End Function


    Public Shared Function getExportReports() As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection
        Try
            MyCmd.CommandText = "PKG_APPLICATION.GetExportReports"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Add("GetExportReports", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Connection = conn.Open
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "GetExportReports")

            MyDataView = MyDs.Tables("GetExportReports").DefaultView

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally

            MyCmd.Dispose()
            MyAdapter.Dispose()
            conn.Close()
        End Try

    End Function


    Public Shared Sub ClearDir(ByVal strPath As String)
        Try
            For Each MyInfo As System.IO.FileInfo In Wyeth.Utilities.FileHandling.DirListing(strPath)
                Wyeth.Utilities.FileHandling.Delete(MyInfo.FullName)
            Next
        Catch ex As Exception

        End Try
    End Sub
End Class
