Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities
Imports Wyeth.Utilities.DateHandling

'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
'''         <revision>23</revision>
''' <summary><para>Provides DataAccess for the Reports of old AMS Data from the pre ALF area</para></summary>
Public Class AMS

    Public Function GetAMSSalesStat(ByVal v_StartDate As Date, ByVal v_EndDate As Date, ByVal v_line_code As String) As DataView

        Dim MyCmd As New OracleCommand
        Dim MyConn As New MyConnection
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyDs As New DataSet

        Try

            MyCmd.CommandText = "PKG_REports.GetAMSSalesStat"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("AMS", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_StartDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = FirstOfThisMonth(v_StartDate)
            MyCmd.Parameters.Add("v_EndDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = LastOfThisMonth(v_EndDate)
            MyCmd.Parameters.Add("v_LineType", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = v_line_code

            MyCmd.Connection = MyConn.Open()

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "AMS")
            MyDataView = MyDs.Tables("AMS").DefaultView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try
        Return MyDataView
    End Function



    Public Function GetAMSOrderDetails(ByVal v_StartDate As Date, ByVal v_EndDate As Date, ByVal v_line_code As String, ByVal v_product As String) As DataView

        Dim MyCmd As New OracleCommand
        Dim MyConn As New MyConnection
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyDs As New DataSet

        Try

            MyCmd.CommandText = "PKG_Reports.GetAMSOrderDetails"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("AMS", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_StartDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = v_StartDate
            MyCmd.Parameters.Add("v_EndDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = v_EndDate
            MyCmd.Parameters.Add("v_LineType", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = v_line_code
            If v_product = "0" Then
                MyCmd.Parameters.Add("v_product", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = System.DBNull.Value
            Else
                MyCmd.Parameters.Add("v_product", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = v_product
            End If

            MyCmd.Connection = MyConn.Open()

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "AMS")
            MyDataView = MyDs.Tables("AMS").DefaultView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try
        Return MyDataView
    End Function


    Public Function GetAMSProdKurzBez(ByVal v_line_code As String) As DataView

        Dim MyCmd As New OracleCommand
        Dim MyConn As New MyConnection
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyDs As New DataSet

        Try

            MyCmd.CommandText = "PKG_Reports.GetAMSProdKurzBez"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("AMS", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)

            If v_line_code = "0" Then
                MyCmd.Parameters.Add("v_LineType", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = System.DBNull.Value
            Else
                MyCmd.Parameters.Add("v_LineType", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = v_line_code
            End If

            MyCmd.Connection = MyConn.Open()

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "AMS")
            MyDataView = MyDs.Tables("AMS").DefaultView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try
        Return MyDataView
    End Function



End Class
