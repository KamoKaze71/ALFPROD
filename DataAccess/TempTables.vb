Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities
'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
''' <summary><para>Provides DataAccess for the importTempTables used by the ALF Import </para></summary>
Public Class TempTables

    Dim myadapter As New OracleDataAdapter
    Dim myCommand As New OracleCommand
    Dim myconnn As New Wyeth.Utilities.MyConnection
    Dim myDataview As New DataView
    Dim MyDs As New DataSet


    Public Sub New()
        myCommand.Connection = myconnn.Open()
        myCommand.CommandType = CommandType.StoredProcedure
        myCommand.Parameters.Add("imported", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        myadapter.SelectCommand = myCommand
    End Sub



    Public Function GetKD() As DataView
        myCommand.CommandText = "PKG_APPLICATION.GetNotImportedKD"
        myadapter.Fill(MyDs, "Imported")
        myDataview = MyDs.Tables("Imported").DefaultView
        Return myDataview
    End Function


    Public Function GetART() As DataView
        myCommand.CommandText = "PKG_APPLICATION.GetNotImportedART"
        myadapter.Fill(MyDs, "Imported")
        myDataview = MyDs.Tables("Imported").DefaultView
        Return myDataview
    End Function


    Public Function GetBW() As DataView
        myCommand.CommandText = "PKG_APPLICATION.GetNotImportedBW"
        myadapter.Fill(MyDs, "Imported")
        myDataview = MyDs.Tables("Imported").DefaultView
        Return myDataview
    End Function



    Public Function DeleteKD() As Boolean
        Try

        myCommand.Parameters.Clear()
        myCommand.CommandText = "PKG_APPLICATION.DeleteNotImportedKD"
            myCommand.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Return False
        End Try
    End Function


    Public Function DeleteART() As Boolean
        Try
            myCommand.Parameters.Clear()
            myCommand.CommandText = "PKG_APPLICATION.DeleteNotImportedART"
            myCommand.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Return False
        End Try
    End Function


    Public Function DeleteBW() As Boolean
        Try
            myCommand.Parameters.Clear()
            myCommand.CommandText = "PKG_APPLICATION.DeleteNotImportedBW"
            myCommand.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Return False
        End Try
    End Function




End Class
