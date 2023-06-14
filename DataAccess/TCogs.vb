Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities
Imports Wyeth.Utilities.DateHandling

'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
''' <summary><para>Provides DataAccess for T_COGS</para></summary>
Public Class TCogs


    Public Function GetTCogs(ByVal v_stock_date_stock As Date, ByVal v_prod_id As Integer, ByVal v_curr_id_from As Integer) As Double
        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Dim MyTCogs As Double

        Try
            MyCmd.CommandText = "PKG_APPLICATION.GetTCogs"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("invoice", OracleDbType.Double, MyTCogs, 20, ParameterDirection.ReturnValue)
            MyCmd.Parameters.Add("v_stock_date_stock", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = v_stock_date_stock
            MyCmd.Parameters.Add("v_prod_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = v_prod_id
            MyCmd.Parameters.Add("v_curr_id_from", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = v_curr_id_from

            MyCmd.Connection = Myconn.Open()

            MyCmd.ExecuteScalar()
            MyTCogs = MyCmd.Parameters(0).Value

            Return MyTCogs
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Myconn.Close()

        End Try
    End Function

    Public Function GetProductsWithNoTCOGS(ByVal v_ctry_id As Integer) As DataView
        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Dim MyDataView As New DataView
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDS As New DataSet


        Try
            MyCmd.CommandText = "PKG_APPLICATION.GetProductsWithNoTCOGS"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("products", OracleDbType.RefCursor, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = v_ctry_id

            MyCmd.Connection = Myconn.Open()

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDS, "StockData")
            MyDataView = MyDS.Tables("StockData").DefaultView


            Return MyDataView

            Return MyDataView
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Myconn.Close()

        End Try
    End Function

    Public Function GetProductsWithNoTCOGSForMonth(ByVal v_month As Date, ByVal v_ctry_id As Integer) As DataView

        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Dim MyDataView As New DataView
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDS As New DataSet

        Try
            MyCmd.CommandText = "PKG_APPLICATION.GetProductsWithNoTCOGSForMonth"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("products", OracleDbType.RefCursor, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_month", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = v_month
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = v_ctry_id

            MyCmd.Connection = Myconn.Open()

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDS, "StockData")
            MyDataView = MyDS.Tables("StockData").DefaultView

            Return MyDataView
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Myconn.Close()

        End Try
    End Function

    Public Function GetTCogsHistoryByProductID(ByVal prod_id As Integer) As DataView
        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Dim MyDataView As New DataView
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDS As New DataSet

        Try
            MyCmd.CommandText = "PKG_APPLICATION.GetTCOGSHistoryByProduct"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("TCogsHistory", OracleDbType.RefCursor, 20, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_prod_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = prod_id

            MyCmd.Connection = Myconn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDS, "TCogsHistory")
            MyDataView = MyDS.Tables("TCogsHistory").DefaultView

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Myconn.Close()

        End Try
    End Function

End Class
