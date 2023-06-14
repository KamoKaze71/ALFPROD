Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities
Imports Wyeth.Utilities.DateHandling
Imports Wyeth.Utilities.Helper
Imports Wyeth.Alf.CssStyles
Imports Wyeth.Alf.FxRate
Imports wyeth.Alf
'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
''' <summary><para>Provides DataAccess for ALF Stcok Handling Invoices Accrual GITs</para></summary>
Public Class Stock

    Public Function getAllStock() As DataView
        '***********************************************************************************************
        'Holt alle Bewegungsdaten aus der T_STOCK mittels View V_STOCK.
        'Wird in erster Linie bei Admin/Orders eingesetzt.
        '***********************************************************************************************

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyConn As New MyConnection
        Try
            MyCmd.CommandText = "PKG_STOCK.GetAllStock"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Stock", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_startDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StockStartDate
            MyCmd.Parameters.Add("v_EndDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StockEndDate
            MyCmd.Parameters.Add("v_line_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockLineID
            MyCmd.Parameters.Add("v_dist_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = DistributorID
            MyCmd.Connection = MyConn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "StockData")
            MyDataView = MyDs.Tables("StockData").DefaultView
            MyConn.Close()
            Return MyDataView
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        End Try
    End Function

    Public Function GetStockWE() As DataView
        '***********************************************************************************************
        'Holt alle Wareneingange aus der T_STOCK 
        'Wird in bei Stock Statistik Detail Ansicht eingesetzt.
        '***********************************************************************************************

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyConn As New MyConnection
        Try

            MyCmd.CommandText = "PKG_STOCK.GetStockWE"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Stock", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_startDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StockStartDate
            MyCmd.Parameters.Add("v_EndDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StockEndDate
            MyCmd.Parameters.Add("v_prod_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockProdID
            MyCmd.Parameters.Add("v_dist_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockDistID

            MyCmd.Connection = MyConn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Stock")
            MyDataView = MyDs.Tables("Stock").DefaultView
            MyConn.Close()
            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)

        End Try

    End Function
    Public Function GetStockOUT() As DataView
        '***********************************************************************************************
        'Holt alle Sales aus der MV_SALES
        'Wird in bei Stock Statistik Detail OUT Ansicht eingesetzt.
        '***********************************************************************************************

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyConn As New MyConnection
        Try

            MyCmd.CommandText = "PKG_STOCK.GetStockOUT"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Stock", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_startDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StockStartDate
            MyCmd.Parameters.Add("v_EndDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StockEndDate
            MyCmd.Parameters.Add("v_prod_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockProdID
            MyCmd.Parameters.Add("v_dist_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockDistID
            MyCmd.Connection = MyConn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Stock")
            MyDataView = MyDs.Tables("Stock").DefaultView
            MyConn.Close()
            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)

        End Try

    End Function
    Public Function GetStockOUT_FG() As DataView
        '***********************************************************************************************
        'Holt alle FG Sales  Einträge aus der MV_SALES Tabelle
        'Wird in bei Stock Statistik Detail  FG_OUT Ansicht eingesetzt.
        '***********************************************************************************************

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyConn As New MyConnection
        Try

            MyCmd.CommandText = "PKG_STOCK.GetStockOUT_FG"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Stock", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_startDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StockStartDate
            MyCmd.Parameters.Add("v_EndDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StockEndDate
            MyCmd.Parameters.Add("v_prod_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockProdID
            MyCmd.Parameters.Add("v_dist_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockDistID
            MyCmd.Connection = MyConn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Stock")
            MyDataView = MyDs.Tables("Stock").DefaultView
            MyConn.Close()
            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)

        End Try

    End Function
    Public Function GetStockKORR() As DataView
        '***********************************************************************************************
        'Holt alle Krrektur Einträge aus der T_STOCK Tabelle
        'Wird in bei Stock Statistik Detail  KORR Ansicht eingesetzt.
        '***********************************************************************************************

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyConn As New MyConnection
        Try

            MyCmd.CommandText = "PKG_STOCK.GetStockKORR"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Stock", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_startDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StockStartDate
            MyCmd.Parameters.Add("v_EndDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StockEndDate
            MyCmd.Parameters.Add("v_prod_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockProdID
            MyCmd.Parameters.Add("v_dist_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockDistID
            MyCmd.Connection = MyConn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Stock")
            MyDataView = MyDs.Tables("Stock").DefaultView
            MyConn.Close()
            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)

        End Try

    End Function
    Public Function GetStockKORRByCodeIDBewegKZ() As DataView
        '***********************************************************************************************
        'Holt alle Krrektur Einträge aus der T_STOCK Tabelle
        'Wird in bei Stock Statistik Detail  KORR Ansicht eingesetzt.
        '***********************************************************************************************

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyConn As New MyConnection
        Try

            MyCmd.CommandText = "PKG_STOCK. GetStockKORRByCodeIDBewegKZ"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Stock", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_startDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StockStartDate
            MyCmd.Parameters.Add("v_EndDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StockEndDate
            MyCmd.Parameters.Add("v_prod_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockProdID
            MyCmd.Parameters.Add("v_dist_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockDistID
            MyCmd.Parameters.Add("v_code_id_bewegkz", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockCodeIDBewegKZ
            MyCmd.Connection = MyConn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Stock")
            MyDataView = MyDs.Tables("Stock").DefaultView
            MyConn.Close()
            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)

        End Try

    End Function

    Public Function GetStockCrossCheckUM() As DataView
        '***********************************************************************************************
        'Vergleicht alle Alle Abgänge aus dem Verkauflage mit zugangen im Musterlager BEWEGKZ='UM'
        'Wird in bei CrossCheckReport eingesetzt.
        '***********************************************************************************************

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyConn As New MyConnection
        Try

            MyCmd.CommandText = "PKG_STOCK.GetStockCrossCheckUM"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Stock", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_startDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StockStartDate
            MyCmd.Parameters.Add("v_EndDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StockEndDate
            MyCmd.Parameters.Add("v_dist_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockDistID
            MyCmd.Connection = MyConn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Stock")
            MyDataView = MyDs.Tables("Stock").DefaultView
            MyConn.Close()
            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)

        End Try

    End Function

    Public Function GetStockProductDetail() As DataView
        '***********************************************************************************************
        'Holt alle detail Einträge aus der MV_STOCK Tabelle
        'Wird in bei Stock Statistik Detail  PRESENTATION Ansicht eingesetzt.
        '***********************************************************************************************

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyConn As New MyConnection
        Try

            MyCmd.CommandText = "PKG_STOCK.GetStockProductDetail"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Stock", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_startDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StockStartDate
            MyCmd.Parameters.Add("v_EndDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StockEndDate
            MyCmd.Parameters.Add("v_prod_phznr", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = StockPhznr
            MyCmd.Parameters.Add("v_code_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockCodeIDBewegKZ
            MyCmd.Parameters.Add("v_line_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockLineID
            MyCmd.Parameters.Add("v_dist_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockDistID
            MyCmd.Connection = MyConn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Stock")
            MyDataView = MyDs.Tables("Stock").DefaultView
            MyConn.Close()
            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)

        End Try

    End Function
    Public Function GetFAProductDetail() As DataView
        '***********************************************************************************************
        'Holt alle detail Einträge aus der V_STOCK Tabelle
        'Wird in bei Stock Statistik Detail  KORR Ansicht eingesetzt.
        '***********************************************************************************************

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyConn As New MyConnection
        Try

            MyCmd.CommandText = "PKG_STOCK. GetFAProductDetail"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Stock", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_startDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StockStartDate
            MyCmd.Parameters.Add("v_EndDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StockEndDate
            MyCmd.Parameters.Add("v_prod_phznr", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = StockPhznr
            MyCmd.Parameters.Add("v_line_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockLineID
            MyCmd.Parameters.Add("v_dist_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockDistID
            MyCmd.Connection = MyConn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Stock")
            MyDataView = MyDs.Tables("Stock").DefaultView
            MyConn.Close()
            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)

        End Try

    End Function
    Public Function GetFGProductDetail() As DataView
        '***********************************************************************************************
        'Holt alle detail Einträge aus der MV_SALES Tabelle
        'Wird in bei Stock & Cogs Report von der Detail Ansicht (StockCogsProductDetail.aspx) aufgerufen.
        '***********************************************************************************************

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyConn As New MyConnection
        Try

            MyCmd.CommandText = "PKG_STOCK. GetFGProductDetail"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Stock", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_startDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StockStartDate
            MyCmd.Parameters.Add("v_EndDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StockEndDate
            MyCmd.Parameters.Add("v_cc_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockCCID
            MyCmd.Parameters.Add("v_line_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockLineID
            MyCmd.Parameters.Add("v_dist_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockDistID
            MyCmd.Connection = MyConn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Stock")
            MyDataView = MyDs.Tables("Stock").DefaultView
            MyConn.Close()
            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)

        End Try

    End Function

    Public Function GetInvoiceData(ByVal InvoiceSelect As Integer) As DataView

        '***********************************************************************************************
        'Holt alle Wareneingänge und GIT aus der V_stock 
        'Wird in beim Invoice Check aufgerufen.
        '***********************************************************************************************


        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim Myconn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_STOCK.GetInvoiceCheckData"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Invoice", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_startDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StockStartDate
            MyCmd.Parameters.Add("v_EndDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StockEndDate
            MyCmd.Parameters.Add("InvoiceSelect", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = InvoiceSelect
            MyCmd.Parameters.Add("v_dist_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockDistID
            MyCmd.Parameters.Add("v_line_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockLineID
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockCtryID

            MyCmd.Connection = Myconn.Open()

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Invoice")
            MyDataView = MyDs.Tables("Invoice").DefaultView

            Return MyDataView
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Myconn.Close()

        End Try
    End Function

    Public Function GetGITViewData() As OracleDataReader

        '***********************************************************************************************
        'Holt alle WE und dazuzgehörige GITs in die Detailansicht aus der V_stock 
        'Wird in beim Invoice Check aufgerufen.
        '***********************************************************************************************


        Dim MyCmd As New OracleCommand
        Dim MyReader As OracleDataReader
        Dim Myconn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_STOCK.GetGITViewData"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Invoice", OracleDbType.RefCursor, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_stock_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockID
            MyCmd.Parameters.Add("v_stock_we_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockWEID

            MyCmd.Connection = Myconn.Open()
            MyReader = MyCmd.ExecuteReader(CommandBehavior.CloseConnection)


            Return MyReader
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            'Myconn.Close()

        End Try
    End Function


    Public Function GetMaxTranID() As Integer

        '***********************************************************************************************
        'Holt max TranID from distributor
        '
        '***********************************************************************************************



        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Dim val As Integer

        Try

            MyCmd.CommandText = "PKG_APPLICATION.GetMaxTranID"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("tran_id", OracleDbType.Int32, val, ParameterDirection.ReturnValue)
            MyCmd.Parameters.Add("dist_id", OracleDbType.Int32, ParameterDirection.Input).Value = StockDistID

            MyCmd.Connection = Myconn.Open()
            MyCmd.ExecuteScalar()
            val = Convert.ToInt32(MyCmd.Parameters(0).Value)


            Return val
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Myconn.Close()

        End Try
    End Function
    Public Function GetCodeIDBewegKZ(ByVal code_code As String) As Integer

        '***********************************************************************************************
        'Holt alldie passende ID aus t_codes zum string
        '
        '***********************************************************************************************



        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Dim val As Integer

        Try

            MyCmd.CommandText = "PKG_APPLICATION.GetCodeID"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("code_id", OracleDbType.Int32, val, ParameterDirection.ReturnValue)
            MyCmd.Parameters.Add("v_code_code", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = code_code

            MyCmd.Connection = Myconn.Open()
            MyCmd.ExecuteScalar()
            val = Convert.ToInt32(MyCmd.Parameters(0).Value)


            Return val
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Myconn.Close()

        End Try
    End Function
    Public Function GetInvoiceGIT() As DataView

        '***********************************************************************************************
        'Holt alle und GIT aus der V_stock 
        'Wird in beim Invoice Check aufgerufen.
        '***********************************************************************************************


        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim Myconn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_STOCK.GetInvoiceGIT"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Invoice", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_prod_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockProdID
            MyCmd.Parameters.Add("v_dist_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockDistID
            MyCmd.Connection = Myconn.Open()

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Invoice")
            MyDataView = MyDs.Tables("Invoice").DefaultView

            Return MyDataView
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Myconn.Close()

        End Try
    End Function

    Public Function GetLastProcessedMonth() As Date
        '***********************************************************************************************
        'Holt alle Wareneingänge und GIT aus der V_stock 
        'Wird in beim Invoice Check aufgerufen.
        '***********************************************************************************************

        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Dim dat As Date
        Dim val As String

        Try

            MyCmd.CommandText = "PKG_APPLICATION.GetLastProcessedMonth"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_month", OracleDbType.Varchar2, 20, val, ParameterDirection.ReturnValue)
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockCtryID
            MyCmd.Connection = Myconn.Open()
            MyCmd.ExecuteScalar()
            val = Convert.ToString((MyCmd.Parameters(0).Value), GetMyDTFI())
            dat = Convert.ToDateTime(val, GetMyDTFI())
            dat = LastOfThisMonth(dat)
            Return dat
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Myconn.Close()

        End Try
    End Function
    Public Function GetLastMonthApproved() As Date
        '***********************************************************************************************
        'Holt alle Wareneingänge und GIT aus der V_stock 
        'Wird in beim Invoice Check aufgerufen.
        '***********************************************************************************************

        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Dim dat As Date
        Dim val As String

        Try

            MyCmd.CommandText = "PKG_APPLICATION.GetLastMonthApproved"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_month", OracleDbType.Varchar2, 20, val, ParameterDirection.ReturnValue)
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockCtryID
            MyCmd.Connection = Myconn.Open()
            MyCmd.ExecuteScalar()
            val = Convert.ToString((MyCmd.Parameters(0).Value), GetMyDTFI())
            dat = Convert.ToDateTime(val, GetMyDTFI())
            dat = LastOfThisMonth(dat)
            Return dat
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Myconn.Close()

        End Try
    End Function
    Public Function GetCurrentProcessMonth() As Date
        '***********************************************************************************************
        'Holt alle Wareneingänge und GIT aus der V_stock 
        'Wird in beim Invoice Check aufgerufen.
        '***********************************************************************************************

        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Dim dat As Date
        Dim val As String

        Try

            MyCmd.CommandText = "PKG_APPLICATION.GetCurrentProcessMonth"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_month", OracleDbType.Varchar2, 20, val, ParameterDirection.ReturnValue)
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockCtryID
            MyCmd.Connection = Myconn.Open()
            MyCmd.ExecuteScalar()
            val = Convert.ToString((MyCmd.Parameters(0).Value), GetMyDTFI())
            dat = Convert.ToDateTime(val, GetMyDTFI())
            dat = LastOfThisMonth(dat)
            Return dat
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Myconn.Close()

        End Try
    End Function
    Public Function GetLastFinalJDEMonth() As Date
        '***********************************************************************************************
        'Holt alle Wareneingänge und GIT aus der V_stock 
        'Wird in beim Invoice Check aufgerufen.
        '***********************************************************************************************

        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Dim dat As Date
        Dim val As String

        Try

            MyCmd.CommandText = "PKG_APPLICATION.GetLastFinalJDEMonth"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_month", OracleDbType.Varchar2, 20, val, ParameterDirection.ReturnValue)
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockCtryID
            MyCmd.Connection = Myconn.Open()
            MyCmd.ExecuteScalar()
            val = Convert.ToString((MyCmd.Parameters(0).Value), GetMyDTFI())
            dat = Convert.ToDateTime(val, GetMyDTFI())
            dat = LastOfThisMonth(dat)
            Return dat
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Myconn.Close()

        End Try
    End Function
    Public Function InsertGIT() As Boolean

        Dim MyCmd As New OracleCommand

        Dim MyConn As New MyConnection
        Try
            MyCmd.CommandText = "P_STOCK_INS_PROC"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_Stoc_Unit", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockUnits
            MyCmd.Parameters.Add("v_Stoc_Accrued_Value", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = DBNull.Value
            MyCmd.Parameters.Add("v_Stoc_Invoice_Number", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = StockInvoiceNo
            MyCmd.Parameters.Add("v_Stoc_Invoice_Value", OracleDbType.Double, DBNull.Value, ParameterDirection.Input).Value = StockInvoiceValue
            MyCmd.Parameters.Add("v_Stoc_Invoice_Date", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = DBNull.Value
            MyCmd.Parameters.Add("v_Stoc_Date_Stock ", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StockDateStock
            MyCmd.Parameters.Add("v_Stoc_Comment", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = StockComment
            MyCmd.Parameters.Add("v_Stoc_Order_Number", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = StockOrderNumber
            MyCmd.Parameters.Add("v_Stoc_Date_Open", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StockDateStock
            MyCmd.Parameters.Add("v_Stoc_Date_Accrued", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = DBNull.Value
            MyCmd.Parameters.Add("v_Stoc_Date_Correct", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = DBNull.Value
            MyCmd.Parameters.Add("v_Stoc_User_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockUserID
            MyCmd.Parameters.Add("v_prod_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockProdID
            MyCmd.Parameters.Add("v_dist_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockDistID
            MyCmd.Parameters.Add("v_tran_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockTranID
            MyCmd.Parameters.Add("v_code_ID_quarantine", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = DBNull.Value
            MyCmd.Parameters.Add("v_code_ID_status", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = DBNull.Value
            MyCmd.Parameters.Add("v_code_ID_bewegkz", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockCodeIDBewegKZ
            MyCmd.Parameters.Add("v_curr_ID_accrued", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = DBNull.Value
            MyCmd.Parameters.Add("v_curr_ID_invoice", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockCurrID
            MyCmd.Parameters.Add("v_cust_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = DBNull.Value
            MyCmd.Parameters.Add("v_Batch_Number", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = DBNull.Value
            MyCmd.Parameters.Add("v_stc_grp_date", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = DBNull.Value
            MyCmd.Parameters.Add("v_stc_grp_number", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = DBNull.Value
            MyCmd.Parameters.Add("v_stc_exp_date", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = DBNull.Value

            MyCmd.Connection = MyConn.Open()
            MyCmd.ExecuteNonQuery()


            Return True

        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Return False
        Finally
            MyConn.Close()
        End Try

    End Function

    Public Function RemoveGITAssignment() As Boolean
        '***********************************************************************************************
        'Löscht die GIT zuweisung zum WE
        'Wird in beim Invoice Check aufgerufen.
        '***********************************************************************************************

        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Try

            MyCmd.CommandText = "PKG_STOCK.RemoveGITAssignment"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_stock_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockID
            MyCmd.Connection = Myconn.Open()
            MyCmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Myconn.Close()

        End Try
    End Function


    Public Function DeleteStockEntry() As Boolean
        '***********************************************************************************************
        'Löscht die einen datensatz aus t_stock
        'Wird in beim Invoice Check aufgerufen.
        '***********************************************************************************************

        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Try

            MyCmd.CommandText = "P_STOCK_DEL_PROC"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_stock_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockID
            MyCmd.Connection = Myconn.Open()
            MyCmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
            ExceptionInfo.Show(ex)
        Finally
            Myconn.Close()

        End Try
    End Function

    Public Function SetGITStockDateCorrect() As Boolean

        Dim MyCmd As New OracleCommand
        Dim MyConn As New MyConnection
        Try
            MyCmd.CommandText = "PKG_STOCK.SetGITStockDateCorrect"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_Stoc_ID", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = StockID
            MyCmd.Parameters.Add("v_Stoc_we_ID", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = StockWEID
            MyCmd.Parameters.Add("v_dist_ID", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = StockDistID
            MyCmd.Parameters.Add("v_ctry_ID", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = StockCtryID

            MyCmd.Connection = MyConn.Open()
            MyCmd.ExecuteNonQuery()

            Return True

        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Return False
        Finally
            MyConn.Close()
        End Try

    End Function
    Public Function UpdateInvoiceGITData() As Boolean

        Dim MyCmd As New OracleCommand
        Dim MyConn As New MyConnection
        Try
            MyCmd.CommandText = "PKG_STOCK.UpdateGIT"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_Stoc_ID", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = StockID
            MyCmd.Parameters.Add("v_Stoc_Unit", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockUnits
            'MyCmd.Parameters.Add("v_Stoc_Accrued_Value", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = DBNull.Value
            MyCmd.Parameters.Add("v_Stoc_Invoice_Number", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = StockInvoiceNo
            MyCmd.Parameters.Add("v_Stoc_Invoice_Value", OracleDbType.Double, DBNull.Value, ParameterDirection.Input).Value = StockInvoiceValue
            'MyCmd.Parameters.Add("v_Stoc_Invoice_Date", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = DBNull.Value
            'MyCmd.Parameters.Add("v_Stoc_Date_Stock ", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StockDateStock
            MyCmd.Parameters.Add("v_Stoc_Comment", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = StockComment
            MyCmd.Parameters.Add("v_Stoc_Order_Number", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = StockOrderNumber
            'MyCmd.Parameters.Add("v_Stoc_Date_Open", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StockDateStock
            'MyCmd.Parameters.Add("v_Stoc_Date_Accrued", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = DBNull.Value
            'MyCmd.Parameters.Add("v_Stoc_Date_Correct", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = DBNull.Value
            MyCmd.Parameters.Add("v_Stoc_User_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockUserID
            MyCmd.Parameters.Add("v_prod_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockProdID
            'MyCmd.Parameters.Add("v_dist_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockDistID
            ' MyCmd.Parameters.Add("v_tran_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockTranID
            ' MyCmd.Parameters.Add("v_code_ID_quarantine", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = DBNull.Value
            ' MyCmd.Parameters.Add("v_code_ID_status", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = DBNull.Value
            ' MyCmd.Parameters.Add("v_code_ID_bewegkz", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockCodeIDBewegKZ
            ' MyCmd.Parameters.Add("v_curr_ID_accrued", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = DBNull.Value
            MyCmd.Parameters.Add("v_curr_ID_invoice", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockCurrID
            ' MyCmd.Parameters.Add("v_cust_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = DBNull.Value


            MyCmd.Connection = MyConn.Open()
            MyCmd.ExecuteNonQuery()

            Return True

        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Return False
        Finally
            MyConn.Close()
        End Try

    End Function
    Public Function UpdateInvoiceData() As Boolean
        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyConn As New MyConnection
        Try
            MyCmd.CommandText = "PKG_STOCK.UpdateWE"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_Stoc_ID", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = StockID
            MyCmd.Parameters.Add("v_Stoc_Unit", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockUnits
            MyCmd.Parameters.Add("v_Stoc_Accrued_Value", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = StockInvoiceAccruedValue
            MyCmd.Parameters.Add("v_Stoc_Invoice_Number", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = StockInvoiceNo
            MyCmd.Parameters.Add("v_Stoc_Invoice_Value", OracleDbType.Double, DBNull.Value, ParameterDirection.Input).Value = StockInvoiceValue
            'MyCmd.Parameters.Add("v_Stoc_Invoice_Date", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = DBNull.Value
            ' MyCmd.Parameters.Add("v_Stoc_Date_Stock ", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StockDateStock
            MyCmd.Parameters.Add("v_Stoc_Comment", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = StockComment
            MyCmd.Parameters.Add("v_Stoc_Order_Number", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = StockOrderNumber
            ' MyCmd.Parameters.Add("v_Stoc_Date_Open", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StockDateStock
            'MyCmd.Parameters.Add("v_Stoc_Date_Accrued", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = DBNull.Value
            'MyCmd.Parameters.Add("v_Stoc_Date_Correct", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = DBNull.Value
            MyCmd.Parameters.Add("v_Stoc_User_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockUserID
            'MyCmd.Parameters.Add("v_prod_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockProdID
            'MyCmd.Parameters.Add("v_dist_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockDistID
            'MyCmd.Parameters.Add("v_tran_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockTranID
            'MyCmd.Parameters.Add("v_code_ID_quarantine", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = DBNull.Value
            'MyCmd.Parameters.Add("v_code_ID_status", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = DBNull.Value
            'MyCmd.Parameters.Add("v_code_ID_bewegkz", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockCodeIDBewegKZ
            MyCmd.Parameters.Add("v_curr_ID_accrued", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockCurrIDAccrued
            MyCmd.Parameters.Add("v_curr_ID_invoice", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockCurrID
            '   MyCmd.Parameters.Add("v_cust_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = DBNull.Value
            MyCmd.Parameters.Add("v_Stoc_Invoice_Diff_Value", OracleDbType.Double, DBNull.Value, ParameterDirection.Input).Value = StockInvoiceDiffValue
            MyCmd.Parameters.Add("v_Stoc_Invoice_Diff_Value_ACCRUED", OracleDbType.Double, DBNull.Value, ParameterDirection.Input).Value = StockInvoiceDiffValueAccrued



            MyCmd.Connection = MyConn.Open()
            MyCmd.ExecuteNonQuery()

            Return True

        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Return False

        Finally
            MyConn.Close()
        End Try

    End Function

    Public Function GetFXRate() As Double
        Try

            Dim MyCmd As New OracleCommand
            Dim Myconn As New MyConnection
            Dim MyReader As OracleDataReader
            Dim MyFxRate As Double

            MyCmd.CommandText = "PKG_APPLICATION.CalculateAccruedInvoiceValue"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("invoice", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_curr_id_from", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockCurrID
            MyCmd.Parameters.Add("v_curr_id_to", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockCurrIDAccrued
            MyCmd.Parameters.Add("v_stock_date_stock", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StockDateStock


            MyCmd.Connection = Myconn.Open()

            MyReader = MyCmd.ExecuteReader
            MyReader.Read()
            MyFxRate = MyReader("FXRT_RATE")
            Myconn.Close()
            Return MyFxRate
        Catch ex As Exception
            ExceptionInfo.Show(ex)

        End Try
    End Function
    Public Function GetCurrencies() As DataView
        Try
            Dim MyDs As New DataSet
            Dim MyCmd As New OracleCommand
            Dim MyAdapter As New OracleDataAdapter
            Dim MyDataView As New DataView
            Dim MyConn As New MyConnection

            MyCmd.CommandText = "PKG_APPLICATION.GetCurrencies"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Currency", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Connection = MyConn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Currency")
            MyDataView = MyDs.Tables("Currency").DefaultView
            MyConn.Close()
            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)

        End Try

    End Function
    Public Function GetDisributors() As DataView
        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyConn As New MyConnection
        Try

            MyCmd.CommandText = "PKG_APPLICATION.GetDistributors"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Distributors", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockCtryID
            MyCmd.Connection = MyConn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Distributors")
            MyDataView = MyDs.Tables("Distributors").DefaultView
            MyConn.Close()
            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)

        End Try

    End Function
    Public Function InsertDummyTransmission() As Integer

        Dim MyCmd As New OracleCommand
        Dim MyConn As New MyConnection
        Dim Value As Integer
        Try
            MyCmd.CommandText = "PKG_STOCK.InsertDummyTransmission"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_ret_val", OracleDbType.Int32, Value, ParameterDirection.ReturnValue)
            MyCmd.Parameters.Add("v_dist_ID", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = StockDistID
            MyCmd.Parameters.Add("v_ctry_ID", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = StockCtryID

            MyCmd.Connection = MyConn.Open()
            MyCmd.ExecuteScalar()
            Value = Convert.ToInt32(MyCmd.Parameters(0).Value())

            Return Value

        Catch ex As Exception
            ExceptionInfo.Show(ex)

        Finally
            MyConn.Close()
        End Try

    End Function








    Public Property DistributorID() As Integer
        Get
            Return m_i_dist_id
        End Get
        Set(ByVal Value As Integer)
            m_i_dist_id = Value
        End Set
    End Property
    Public Property StockID() As Integer
        Get
            Return m_i_stock_id

        End Get
        Set(ByVal Value As Integer)
            m_i_stock_id = Value
        End Set
    End Property
    Public Property StockCurrID() As Integer
        Get
            Return m_i_stock_curr_id

        End Get
        Set(ByVal Value As Integer)
            m_i_stock_curr_id = Value
        End Set
    End Property
    Public Property StockCurrIDAccrued() As Integer
        Get
            Return m_i_stock_curr_id_accrued

        End Get
        Set(ByVal Value As Integer)
            m_i_stock_curr_id_accrued = Value
        End Set
    End Property
    Public Property StockInvoiceNo() As String
        Get
            Return m_str_stock_invoice_no

        End Get
        Set(ByVal Value As String)
            m_str_stock_invoice_no = Value
        End Set
    End Property
    Public Property StockComment() As String
        Get
            Return m_str_stock_comment

        End Get
        Set(ByVal Value As String)
            m_str_stock_comment = Value
        End Set
    End Property
    Public Property StockCtryID() As Integer
        Get
            Return m_i_stock_ctry_id
        End Get
        Set(ByVal Value As Integer)
            m_i_stock_ctry_id = Value
        End Set
    End Property
    Public Property StockUnits() As Integer
        Get
            Return m_i_invoice_units
        End Get
        Set(ByVal Value As Integer)
            m_i_invoice_units = Value
        End Set
    End Property
    Public Property StockUserID() As Integer
        Get
            Return m_i_stock_user_id
        End Get
        Set(ByVal Value As Integer)
            m_i_stock_user_id = Value
        End Set
    End Property
    Public Property StockInvoiceDiffValue() As Double
        Get
            Return m_dbl_invoice_diff_value
        End Get
        Set(ByVal Value As Double)
            m_dbl_invoice_diff_value = Value
        End Set
    End Property
    Public Property StockInvoiceDiffValueAccrued() As Double
        Get
            Return m_dbl_invoice_diff_value_accrued
        End Get
        Set(ByVal Value As Double)
            m_dbl_invoice_diff_value_accrued = Value
        End Set
    End Property
    Public Property StockInvoiceValue() As Double
        Get
            Return m_dbl_invoice_value
        End Get
        Set(ByVal Value As Double)
            m_dbl_invoice_value = Value
        End Set
    End Property
    Public Property StockInvoiceAccruedValue() As Double
        Get
            Return m_dbl_invoice_accrued_value
        End Get
        Set(ByVal Value As Double)
            m_dbl_invoice_accrued_value = Value
        End Set
    End Property
    Public Property StockDateStock() As Date
        Get
            Return m_date_stock_date_stock
        End Get
        Set(ByVal Value As Date)
            m_date_stock_date_stock = Value
        End Set
    End Property
    Public Property StockDateInvoiceDate() As Date
        Get
            Return m_date_stock_invoice_date
        End Get
        Set(ByVal Value As Date)
            m_date_stock_invoice_date = Value
        End Set
    End Property
    Public Property StockStartDate() As Date
        Get
            Return m_stock_startDate
        End Get
        Set(ByVal Value As Date)
            m_stock_startDate = Value
        End Set
    End Property
    Public Property StockEndDate() As Date
        Get
            Return m_stock_EndDate
        End Get
        Set(ByVal Value As Date)
            m_stock_EndDate = Value
        End Set
    End Property
    Public Property StockProdID() As Integer
        Get
            Return m_i_stock_prod_id
        End Get
        Set(ByVal Value As Integer)
            m_i_stock_prod_id = Value
        End Set
    End Property
    Public Property StockLineID() As Integer
        Get
            Return m_i_stock_line_id
        End Get
        Set(ByVal Value As Integer)
            m_i_stock_line_id = Value
        End Set
    End Property
    Public Property StockDistID() As Integer
        Get
            Return m_i_stock_dist_id
        End Get
        Set(ByVal Value As Integer)
            m_i_stock_dist_id = Value
        End Set
    End Property
    Public Property StockCodeIDBewegKZ() As Integer
        Get
            Return m_i_code_id_beweg_kz
        End Get
        Set(ByVal Value As Integer)
            m_i_code_id_beweg_kz = Value
        End Set
    End Property
    Public Property StockCCID() As Integer
        Get
            Return m_i_cc_id
        End Get
        Set(ByVal Value As Integer)
            m_i_cc_id = Value
        End Set
    End Property
    Public Property StockOrderNumber() As String
        Get
            Return m_i_stock_order_number
        End Get
        Set(ByVal Value As String)
            m_i_stock_order_number = Value
        End Set
    End Property
    Public Property StockTranID() As Integer
        Get
            Return m_i_stock_tran_id
        End Get
        Set(ByVal Value As Integer)
            m_i_stock_tran_id = Value
        End Set
    End Property
    Public Property StockWEID() As Integer
        Get
            Return m_i_stock_we_id
        End Get
        Set(ByVal Value As Integer)
            m_i_stock_we_id = Value
        End Set
    End Property

    Public Property StockPhznr() As String
        Get
            Return m_str_phznr
        End Get
        Set(ByVal Value As String)
            m_str_phznr = Value
        End Set
    End Property
    Private m_i_stock_id As Integer
    Private m_i_stock_curr_id As Integer
    Private m_i_stock_curr_id_accrued As Integer
    Private m_i_stock_user_id As Integer
    Private m_i_stock_ctry_id As Integer
    Private m_dbl_invoice_value As Double
    Private m_dbl_invoice_diff_value As Double
    Private m_dbl_invoice_accrued_value As Double
    Private m_str_stock_comment As String
    Private m_str_stock_invoice_no As String
    Private m_date_stock_date_stock As Date
    Private m_date_stock_invoice_date As Date
    Private m_i_invoice_units As Integer
    Private m_i_stock_prod_id As Integer
    Private m_i_stock_line_id As Integer
    Private m_stock_startDate As Date
    Private m_stock_EndDate As Date
    Private m_i_stock_dist_id As Integer
    Private m_i_dist_id As Integer
    Private m_i_code_id_beweg_kz As Integer
    Private m_i_cc_id As Integer
    Private m_i_stock_order_number As String
    Private m_i_stock_tran_id As Integer
    Private m_dbl_invoice_diff_value_accrued As Double
    Private m_i_stock_we_id As Integer
    Private m_str_phznr As String



    'Public Function UpdateInvoiceData(ByVal InvoiceSelect As Integer) As Boolean

    '    Dim MyDs As New DataSet
    '    Dim MyCmd As New OracleCommand
    '    Dim MyAdapter As New OracleDataAdapter
    '    Dim MyDataView As New DataView
    '    Dim Myconn As New MyConnection

    '    Try


    '        MyCmd.CommandText = "PKG_APPLICATION.GetInvoiceCheckData"
    '        MyCmd.CommandType = CommandType.StoredProcedure

    '        MyCmd.Parameters.Clear()
    '        MyCmd.Parameters.Add("v_startDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = FirstOfThisMonth(Today())
    '        MyCmd.Parameters.Add("v_EndDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = LastOfThisMonth(Today())
    '        MyCmd.Parameters.Add("InvoiceSelect", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = InvoiceSelect
    '        MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = StockCtryID

    '        MyCmd.Connection = Myconn.Open()

    '        MyAdapter.SelectCommand = MyCmd
    '        MyAdapter.Fill(MyDs, "Invoice")
    '        MyDataView = MyDs.Tables("Invoice").DefaultView
    '        Myconn.Close()
    '        Return True
    '    Catch ex As Exception
    '        ExceptionInfo.Show(ex)
    '        Return False
    '    End Try
    'End Function

End Class
