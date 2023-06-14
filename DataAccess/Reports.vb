Option Explicit On 
Option Strict On
Imports System
Imports System.Data
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports Wyeth.Utilities
Imports wyeth.Utilities.DateHandling
Imports System.Globalization
Imports Wyeth.Utilities.Helper
Imports Wyeth.Alf.WyethDropdown
'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
''' <summary><para>Provides DataAccess for all ALf Reports</para></summary>
Public Class Report

	Public Sub New()
		MyBase.New()

		StartDate = FirstOfThisMonth(Today())
		EndDate = LastOfThisMonth(Today())
		BudgetType = "BU"

	End Sub

    Public Function GetStockStat(ByRef mylb As DropDownList, ByRef pg As Page) As DataView
        '***********************************************************************************************
        'Holt Stock Statistik aus der MV_STOCK
        ' 
        '***********************************************************************************************

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyConn As New MyConnection
        Dim myDt As DataTable


        Try
            MyCmd.CommandText = "PKG_STock.GetStockStat"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Stockstat", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_StartDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StartDate
            MyCmd.Parameters.Add("v_EndDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = EndDate
            MyCmd.Parameters.Add("v_line_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = LineID
            MyCmd.Parameters.Add("v_dist_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = DistID
            MyCmd.Parameters.Add("v_prod_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = ProductID

            MyCmd.Connection = MyConn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Stockstat")
            MyDataView = MyDs.Tables("Stockstat").DefaultView

            If ProductID = 0 Then
                myDt = DataAccessHelper.SelectDistinct("mydt", MyDs.Tables("Stockstat"), "prod_id", "prod_presentation")
                fillDDwithDT(myDt, mylb, "All Products")
            Else
                myDt = DataAccessHelper.SelectDistinct("mydt", MyDs.Tables("Stockstat"), "prod_id", "prod_presentation")
                fillDDwithDT(myDt, mylb, "All Products")
                mylb.SelectedValue = CStr(ProductID)
            End If

            MyConn.Close()
            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try

    End Function

    Public Function GetStockStatValues(ByRef mylb As DropDownList, ByVal pg As Page) As DataView
        '***********************************************************************************************
        'Holt Stock Statistik aus der MV_STOCK
        ' 
        '***********************************************************************************************

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyConn As New MyConnection
        Dim myDt As New DataTable


        Try
            MyCmd.CommandText = "PKG_STock.GetStockStatValues"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Stockstat", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_StartDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StartDate
            MyCmd.Parameters.Add("v_EndDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = EndDate
            MyCmd.Parameters.Add("v_line_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = LineID
            MyCmd.Parameters.Add("v_dist_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = DistID
            MyCmd.Parameters.Add("v_prod_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = ProductID
            MyCmd.Connection = MyConn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Stockstat")
            MyDataView = MyDs.Tables("Stockstat").DefaultView


            If ProductID = 0 Then
                myDt = DataAccessHelper.SelectDistinct("mydt", MyDs.Tables("Stockstat"), "prod_id", "prod_presentation")
                fillDDwithDT(myDt, mylb, "All Products")
            End If

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try

    End Function

    Public Function GetStockCogs() As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyConn As New MyConnection


        Try
            MyCmd.CommandText = "PKG_Stock.GetStockCogs"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Stockstat", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_StartDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StartDate
            MyCmd.Parameters.Add("v_EndDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = EndDate
            MyCmd.Parameters.Add("v_line_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = LineID
            MyCmd.Parameters.Add("v_dist_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = DistID
            MyCmd.Connection = MyConn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Stockstat")
            MyDataView = MyDs.Tables("Stockstat").DefaultView
            MyConn.Close()
            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try

    End Function

    Public Function GetStockCogsProduct() As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyConn As New MyConnection


        Try
            MyCmd.CommandText = "PKG_Stock.GetStockCogs"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Stockstat", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_StartDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StartDate
            MyCmd.Parameters.Add("v_EndDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = EndDate
            MyCmd.Parameters.Add("v_line_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = LineID
            MyCmd.Parameters.Add("v_dist_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = DistID
            MyCmd.Connection = MyConn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Stockstat")
            MyDataView = MyDs.Tables("Stockstat").DefaultView
            MyConn.Close()
            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try

    End Function


    Public Function GetAPOB_Countries() As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyConn As New MyConnection

        Try
            MyCmd.CommandText = "PKG_Reports.GetAPO_b_countries"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Stockstat", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_StartDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StartDate
            MyCmd.Connection = MyConn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "GetAPO_b_countries")
            MyDataView = MyDs.Tables("GetAPO_b_countries").DefaultView
            MyConn.Close()
            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try

    End Function


    Public Function GetStockStatProduct() As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyConn As New MyConnection


        Try
            MyCmd.CommandText = "PKG_Stock.GetStockStatProduct"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Stockstat", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_StartDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StartDate
            MyCmd.Parameters.Add("v_EndDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = EndDate
            MyCmd.Parameters.Add("v_prod_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = ProductID
            MyCmd.Parameters.Add("v_dist_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = DistID
            MyCmd.Connection = MyConn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Stockstat")
            MyDataView = MyDs.Tables("Stockstat").DefaultView
            MyConn.Close()
            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try

    End Function

    Public Function GetStockForIRIS(ByRef MyLb As DropDownList, ByRef pg As Page) As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection
        Dim mydt As New DataTable

        Try

            MyCmd.CommandText = "PKG_REPORTS.GetStockForIRIS"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Add("StockIRIS", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_startDate", OracleDbType.Date, ParameterDirection.Input).Value = StartDate
            MyCmd.Parameters.Add("dist_id", OracleDbType.Int32, ParameterDirection.Input).Value = DistID
            MyCmd.Parameters.Add("line_id", OracleDbType.Int32, ParameterDirection.Input).Value = LineID
            MyCmd.Parameters.Add("product_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = ProductID

            MyCmd.Connection = conn.Open

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "StockIRIS")
            MyDataView = MyDs.Tables("StockIRIS").DefaultView

            If ProductID = 0 Then
                mydt = DataAccessHelper.SelectDistinct("mydt", MyDs.Tables("StockIRIS"), "prod_id", "prod_presentation")
                fillDDwithDT(mydt, MyLb, "All Products")
            End If

            MyCmd.Dispose()
            MyAdapter.Dispose()

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try

    End Function

    Public Function GetInvlBatchReport(ByRef MyLb As DropDownList, ByRef pg As Page) As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection
        Dim mydt As New DataTable

        Try

            MyCmd.CommandText = "PKG_REPORTS.GetInvlBatchReport"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Add("StockIRIS", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_startDate", OracleDbType.Date, ParameterDirection.Input).Value = StartDate
            MyCmd.Parameters.Add("v_endDate", OracleDbType.Date, ParameterDirection.Input).Value = EndDate
            MyCmd.Parameters.Add("dist_id", OracleDbType.Int32, ParameterDirection.Input).Value = DistID
            MyCmd.Parameters.Add("line_id", OracleDbType.Int32, ParameterDirection.Input).Value = LineID
            MyCmd.Parameters.Add("product_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = ProductID

            MyCmd.Connection = conn.Open

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "InvlBatchReport")
            MyDataView = MyDs.Tables("InvlBatchReport").DefaultView

            If ProductID = 0 Then
                mydt = DataAccessHelper.SelectDistinct("mydt", MyDs.Tables("InvlBatchReport"), "prod_id", "prod_presentation")
                fillDDwithDT(mydt, MyLb, "All Products")
            End If

            MyCmd.Dispose()
            MyAdapter.Dispose()

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try

    End Function

    Public Function GetWWSReport() As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_REPORTS.GetWWSReport"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Add("WWS", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, ParameterDirection.Input).Value = CtryID
            MyCmd.Parameters.Add("StartDate", OracleDbType.Date, ParameterDirection.Input).Value = StartDate

            MyCmd.Connection = conn.Open

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "WWS")
            MyDataView = MyDs.Tables("WWS").DefaultView

            MyCmd.Dispose()
            MyAdapter.Dispose()


            Return MyDataView
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try
    End Function

    Public Function GetWWSReportForte() As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_REPORTS.GetWWSForteReport"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Add("WWS", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("StartDate", OracleDbType.Date, ParameterDirection.Input).Value = StartDate
            MyCmd.Connection = conn.Open
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "WWS")
            MyDataView = MyDs.Tables("WWS").DefaultView

            MyCmd.Dispose()
            MyAdapter.Dispose()


            Return MyDataView
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try
    End Function

    Public Function GetPremarinReport() As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_REPORTS.GetPremarinReport"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Add("Premarin", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)

            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = Me.CtryID
            MyCmd.Parameters.Add("startDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = Me.StartDate
            MyCmd.Parameters.Add("endDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = Me.EndDate
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

    Public Function GetRoyalityReportValue() As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim strsql As String
        Dim conn As New MyConnection
        Dim yr As Integer

        Try

            yr = Year(CDate(StartDate))


            MyCmd.Parameters.Add("Royality", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_year", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = yr
            MyCmd.Parameters.Add("v_invoicer_code", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = Invoicer
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = CtryID
            MyCmd.Connection = conn.Open()

            MyCmd.CommandText = "PKG_REPORTS.GetRoyalityReportValue"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Connection = conn.Open
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Royality")

            MyDataView = MyDs.Tables("Royality").DefaultView

            MyCmd.Dispose()
            MyAdapter.Dispose()

            Return MyDataView
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try
    End Function

    Public Function GetRoyalityReportUnit() As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim strsql As String
        Dim conn As New MyConnection
        Dim yr As Integer

        Try

            yr = Year(CDate(StartDate))


            MyCmd.Parameters.Add("Royality", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_year", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = yr
            MyCmd.Parameters.Add("v_invoicer_code", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = Invoicer
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = CtryID
            MyCmd.Connection = conn.Open()

            MyCmd.CommandText = "PKG_REPORTS.GetRoyalityReportUnit"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Connection = conn.Open
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Royality")

            MyDataView = MyDs.Tables("Royality").DefaultView

            MyCmd.Dispose()
            MyAdapter.Dispose()

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try

    End Function

    Public Function GetSales() As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_REPORTS.GetSales"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Add("Sales", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)

            MyCmd.Parameters.Add("v_startDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StartDate
            MyCmd.Parameters.Add("v_endDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = EndDate
            MyCmd.Parameters.Add("v_line_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = LineID
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = CtryID

            MyCmd.Connection = conn.Open()

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Sales")
            MyDataView = MyDs.Tables("Sales").DefaultView
            conn.Close()

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try

    End Function

    Public Function GetSalesOrders() As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_REPORTS.GetSalesOrders"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Add("Sales", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)

            MyCmd.Parameters.Add("v_startDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StartDate
            MyCmd.Parameters.Add("v_endDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = EndDate
            MyCmd.Parameters.Add("v_line_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = LineID
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = CtryID
            MyCmd.Parameters.Add("v_product_desc", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = ProdDesc

            MyCmd.Connection = conn.Open()

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Sales")
            MyDataView = MyDs.Tables("Sales").DefaultView
            conn.Close()

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try

    End Function

    Public Function GetSalesStatCustomer() As DataView
        '***********************************************************************************************
        'Holt Daten für den CustomerSalesReport 
        '
        '***********************************************************************************************

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_REPORTS.GetCustomerStatistik"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Add("Sales", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)

            MyCmd.Parameters.Add("startDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StartDate
            MyCmd.Parameters.Add("endDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = EndDate
            MyCmd.Parameters.Add("v_line_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = LineID
            MyCmd.Connection = conn.Open()

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Sales")
            MyDataView = MyDs.Tables("Sales").DefaultView

            conn.Close()

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try

    End Function

    Public Function GetSalesStatCustomer2() As DataView
        '***********************************************************************************************
        'Holt Daten für den CustomerSalesReport 
        '
        '***********************************************************************************************

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_REPORTS.GetSalesStatCustomer2"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Add("Sales", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)

            MyCmd.Parameters.Add("startDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StartDate
            MyCmd.Parameters.Add("endDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = EndDate
            MyCmd.Parameters.Add("v_line_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = LineID
            MyCmd.Connection = conn.Open()

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Sales")
            MyDataView = MyDs.Tables("Sales").DefaultView

            conn.Close()

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try

    End Function

    Public Function GetSalesAreaStat() As DataView
        '***********************************************************************************************
        'Holt Daten für den SalesAreaStatitistik Report
        '
        '***********************************************************************************************

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_REPORTS.GetSalesAreaStat"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Add("Sales", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)

            MyCmd.Parameters.Add("startDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StartDate
            MyCmd.Parameters.Add("endDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = EndDate
            MyCmd.Parameters.Add("v_line_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = LineID
            MyCmd.Parameters.Add("v_dist_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = DistID
            MyCmd.Connection = conn.Open()

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Sales")
            MyDataView = MyDs.Tables("Sales").DefaultView
            conn.Close()

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try

    End Function

    Public Function GetSalesAreaStatTAPG() As DataView
        '***********************************************************************************************
        'Holt Daten für den SalesAreaStatitistik Report
        '
        '***********************************************************************************************

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_REPORTS.GetSalesAreaStatTAPG"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Add("Sales", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)

            MyCmd.Parameters.Add("startDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StartDate
            MyCmd.Parameters.Add("endDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = EndDate
            MyCmd.Parameters.Add("v_line_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = LineID
            '  MyCmd.Parameters.Add("v_dist_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = DistID
            MyCmd.Connection = conn.Open()

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Sales")
            MyDataView = MyDs.Tables("Sales").DefaultView
            conn.Close()

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try

    End Function
    Public Function GetSalesAreaStatSMS() As DataView
        '***********************************************************************************************
        'Holt Daten für den SalesAreaStatitistik Report
        '
        '***********************************************************************************************

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_REPORTS.GetSalesAreaStatSMS"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Add("Sales", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)

            MyCmd.Parameters.Add("startDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = Me.StartDate
            MyCmd.Parameters.Add("endDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = Me.EndDate
            ' MyCmd.Parameters.Add("v_line_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = Me.LineID
            MyCmd.Parameters.Add("v_tapg_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = Me.TargetPG

            MyCmd.Connection = conn.Open()

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Sales")
            MyDataView = MyDs.Tables("Sales").DefaultView
            conn.Close()

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try

    End Function

    Public Function GetSalesAreaStatDetail() As DataView
        '***********************************************************************************************
        'Holt Daten für den SalesAreaStatitistik Report
        '
        '***********************************************************************************************

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_REPORTS.GetSalesAreaStatDetail"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Add("Sales", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)

            MyCmd.Parameters.Add("startDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = Me.StartDate
            MyCmd.Parameters.Add("endDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = Me.EndDate
            MyCmd.Parameters.Add("v_line_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = Me.LineID
            MyCmd.Parameters.Add("v_prod", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = Me.ProdDesc
            MyCmd.Parameters.Add("v_cust_no", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = Me.CustomerNo
            MyCmd.Parameters.Add("v_sare_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = Me.SaReID
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = Me.CtryID

            MyCmd.Connection = conn.Open()

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Sales")
            MyDataView = MyDs.Tables("Sales").DefaultView
            conn.Close()

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try

    End Function

    Public Function GetNewEntitiesReportCustomers() As DataView
        '***********************************************************************************************
        'Holt alle importierten enuen customer
        '
        '***********************************************************************************************

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_REPORTS.GetNewEntitiesReportCustomers"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Add("Sales", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = CtryID
            MyCmd.Parameters.Add("v_dist_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = DistID

            MyCmd.Connection = conn.Open()

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Sales")
            MyDataView = MyDs.Tables("Sales").DefaultView
            conn.Close()

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try

    End Function

    Public Function GetSalesCustomerStatDetail() As DataView
        '***********************************************************************************************
        'Holt Daten für den SalesAreaStatitistik Report
        '
        '***********************************************************************************************

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_REPORTS.GetSalesCustomerStatDetail"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Add("Sales", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)

            MyCmd.Parameters.Add("startDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StartDate
            MyCmd.Parameters.Add("endDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = EndDate
            MyCmd.Parameters.Add("v_line_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = LineID
            MyCmd.Parameters.Add("v_prod", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = ProductID
            MyCmd.Parameters.Add("v_cust_no", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = CustomerNo
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = CtryID

            MyCmd.Connection = conn.Open()

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Sales")
            MyDataView = MyDs.Tables("Sales").DefaultView
            conn.Close()

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try

    End Function

    Public Function GetSalesStatistic() As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_REPORTS.GetSalesStatistic"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Add("Sales", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)

            MyCmd.Parameters.Add("v_month", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StartDate
            MyCmd.Parameters.Add("v_line_code", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = LineCode
            MyCmd.Connection = conn.Open()

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Sales")
            MyDataView = MyDs.Tables("Sales").DefaultView
            conn.Close()

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try

    End Function

    Public Function GetDailySalesTotal() As DataView
        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim strsql As String
        Dim conn As New MyConnection

        Try
            MyCmd.CommandText = "PKG_REPORTS.GetDailySalesTotal"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Add("DailySales", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_month", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StartDate
            MyCmd.Parameters.Add("v_workdays", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = WorkDaysMonth
            MyCmd.Parameters.Add("v_workdaysUntilToday", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = WorkDaysToday
            MyCmd.Parameters.Add("v_budget", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = BudgetType

            MyCmd.Connection = conn.Open
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "DailySales")

            MyDataView = MyDs.Tables("DailySales").DefaultView

            MyCmd.Dispose()
            MyAdapter.Dispose()
            conn.Close()
            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try
    End Function

    Public Function GetDailySales() As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim strsql As String
        Dim conn As New MyConnection


        Try


            MyCmd.CommandText = "PKG_REPORTS.GetDailySales"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Add("DailySales", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_month", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StartDate
            MyCmd.Parameters.Add("v_workdays", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = WorkDaysMonth
            MyCmd.Parameters.Add("v_workdaysUntilToday", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = WorkDaysToday
            MyCmd.Parameters.Add("v_budget", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = BudgetType

            MyCmd.Connection = conn.Open
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "DailySales")

            MyDataView = MyDs.Tables("DailySales").DefaultView

            MyCmd.Dispose()
            MyAdapter.Dispose()
            conn.Close()

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try

    End Function

    Public Function GetNetSales() As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_REPORTS.GetNetSales"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Add("NetSales", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_month", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StartDate
            MyCmd.Parameters.Add("v_budget", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = BudgetType

            MyCmd.Connection = conn.Open
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "NetSales")

            MyDataView = MyDs.Tables("NetSales").DefaultView

            MyCmd.Dispose()
            MyAdapter.Dispose()
            conn.Close()

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try

    End Function

    Public Function GetCegedim() As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_REPORTS.GetCegedim"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Add("Cegedim", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_Startdate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StartDate
            MyCmd.Parameters.Add("v_Enddate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = EndDate
            MyCmd.Parameters.Add("v_line_id", OracleDbType.Int16, DBNull.Value, ParameterDirection.Input).Value = LineID


            MyCmd.Connection = conn.Open
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Cegedim")

            MyDataView = MyDs.Tables("Cegedim").DefaultView

            MyCmd.Dispose()
            MyAdapter.Dispose()
            conn.Close()

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try

    End Function

    Public Function GetFcstAccuracy() As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_REPORTS.GetSalesForeCastAccuracy"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Add("Sales", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = CtryID
            MyCmd.Parameters.Add("v_start_date", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StartDate
            MyCmd.Parameters.Add("v_end_date", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = EndDate
            MyCmd.Parameters.Add("v_line_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = LineID

            MyCmd.Connection = conn.Open
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "FcstAccuracy")

            MyDataView = MyDs.Tables("FcstAccuracy").DefaultView

            MyCmd.Dispose()
            MyAdapter.Dispose()
            conn.Close()

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try

    End Function

    Public Function GetRuii() As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_REPORTS.GetRuii"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Add("ruii", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_StartDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StartDate
            MyCmd.Parameters.Add("v_line_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = LineID
            MyCmd.Parameters.Add("v_product_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = ProductID

            MyCmd.Connection = conn.Open
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "ruii")

            MyDataView = MyDs.Tables("ruii").DefaultView

            MyCmd.Dispose()
            MyAdapter.Dispose()
            conn.Close()

            Return MyDataView
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try

    End Function

    Public Function GetGMReport() As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try

            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_REPORTS.GetGmReport"
            MyCmd.Parameters.Add("GM", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_date", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StartDate

            MyCmd.Connection = conn.Open

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "GM")

            MyDataView = MyDs.Tables("GM").DefaultView

            MyCmd.Dispose()
            MyAdapter.Dispose()

            Return MyDataView
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try

    End Function

    Public Function GetActAndSamProducts() As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_REPORTS.GetActandSamProducts"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Add("SamProducts", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = CtryID
            MyCmd.Parameters.Add("v_obs_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = OBSCode

            MyCmd.Connection = conn.Open
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Cegedim")

            MyDataView = MyDs.Tables("Cegedim").DefaultView

            MyCmd.Dispose()
            MyAdapter.Dispose()


            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try

    End Function

    Public Function GetJDEReport() As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_JDE.GetAccountingData"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Add("AccountingData", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_month", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StartDate
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = CtryID

            MyCmd.Connection = conn.Open
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "AccountingData")

            MyDataView = MyDs.Tables("AccountingData").DefaultView

            MyCmd.Dispose()
            MyAdapter.Dispose()
            conn.Close()

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try

    End Function
    ' Public Function GetBackOrdersLines(ByVal prod_desc As String, ByVal dist_id As Integer, ByVal line_id As Integer, ByVal startDate As Date, ByVal endDate As Date) As DataView
    Public Function GetBackOrdersLines(ByVal prod_desc As String, ByVal startDate As Date, ByVal endDate As Date, ByVal dist_id As Integer, ByVal line_id As Integer) As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_Reports.GetBackOrdersLines"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Add("BackOrders", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_startDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = startDate
            MyCmd.Parameters.Add("v_endDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = endDate
            MyCmd.Parameters.Add("v_dist_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = dist_id
            MyCmd.Parameters.Add("v_line_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = line_id
            MyCmd.Parameters.Add("v_prod_desc", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = prod_desc

            MyCmd.Connection = conn.Open
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "BackOrders")

            MyDataView = MyDs.Tables("BackOrders").DefaultView

            MyCmd.Dispose()
            MyAdapter.Dispose()
            conn.Close()

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try

    End Function

    Public Function GetBackOrdersOrders(ByVal dist_id As Integer, ByVal startDate As Date, ByVal endDate As Date) As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_Reports.GetBackOrdersOrders"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Add("BackOrders", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_startDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = startDate
            MyCmd.Parameters.Add("v_endDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = endDate
            MyCmd.Parameters.Add("v_dist_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = dist_id

            MyCmd.Connection = conn.Open
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "BackOrders")

            MyDataView = MyDs.Tables("BackOrders").DefaultView

            MyCmd.Dispose()
            MyAdapter.Dispose()
            conn.Close()

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try

    End Function

    Public Function GetReturnCheck(ByVal dist_id As Integer, ByVal line_id As Integer, ByVal startDate As Date, ByVal endDate As Date) As DataView
        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_Reports.GetReturnCheck"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Add("ReturnCheck", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_startDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = startDate
            MyCmd.Parameters.Add("v_endDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = endDate
            MyCmd.Parameters.Add("v_dist_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = dist_id
            MyCmd.Parameters.Add("v_line_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = line_id

            MyCmd.Connection = conn.Open
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "ReturnCheck")

            MyDataView = MyDs.Tables("ReturnCheck").DefaultView

            MyCmd.Dispose()
            MyAdapter.Dispose()
            conn.Close()

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try
    End Function

    Public Function GetForteDownload(ByVal v_startDate As Date, ByVal v_EndDate As Date, ByVal v_dist_id As Integer) As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyConn As New MyConnection


        Try
            MyCmd.CommandText = "PKG_REPORTS.GetForteDownload"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("download ", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_StartDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = v_startDate
            MyCmd.Parameters.Add("v_EndDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = v_EndDate
            MyCmd.Parameters.Add("v_dist_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = v_dist_id
            MyCmd.Connection = MyConn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "download ")
            MyDataView = MyDs.Tables("download ").DefaultView
            MyConn.Close()
            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try

    End Function

    Public Function GetAPO_Inventory() As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_REPORTS.GetAPO_Inventory"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Add("GetAPO_Inventory", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_date", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StartDate

            MyCmd.Connection = conn.Open
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "GetAPO_Inventory")

            MyDataView = MyDs.Tables("GetAPO_Inventory").DefaultView

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyCmd.Dispose()
            MyAdapter.Dispose()
            conn.Close()
        End Try

    End Function
    Public Function GetAPO_goodsissues() As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_REPORTS.GetAPO_goodsissues"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Add("GetAPO_goodsissues", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_date", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StartDate

            MyCmd.Connection = conn.Open
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "GetAPO_goodsissues")

            MyDataView = MyDs.Tables("GetAPO_goodsissues").DefaultView

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyCmd.Dispose()
            MyAdapter.Dispose()
            conn.Close()
        End Try

    End Function
    Public Function GetAPO_goodsreceipt() As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_REPORTS.GetAPO_goodsreceipt"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Add("GetAPO_goodsreceipt", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_date", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StartDate

            MyCmd.Connection = conn.Open
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "GetAPO_goodsreceipt")

            MyDataView = MyDs.Tables("GetAPO_goodsreceipt").DefaultView

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyCmd.Dispose()
            MyAdapter.Dispose()
            conn.Close()
        End Try

    End Function
    Public Function GetAPO_grosssales() As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_REPORTS.GetAPO_grosssales"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Add("GetAPO_goodsreceipt", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_date", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StartDate

            MyCmd.Connection = conn.Open
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "GetAPO_grosssales")

            MyDataView = MyDs.Tables("GetAPO_grosssales").DefaultView

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyCmd.Dispose()
            MyAdapter.Dispose()
            conn.Close()
        End Try

    End Function

    Public Function GetBatchMovement() As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_REPORTS.GetBatchMovement"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Add("GetBatchMovement", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_start_date", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = Me.StartDate
            MyCmd.Parameters.Add("v_end_date", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = Me.EndDate
            MyCmd.Parameters.Add("v_line_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = Me.LineID
            MyCmd.Parameters.Add("v_batchNr", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = Me.BachtNr

            MyCmd.Connection = conn.Open
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "GetBatchMovement")

            MyDataView = MyDs.Tables("GetBatchMovement").DefaultView

            MyCmd.Dispose()
            MyAdapter.Dispose()

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try

    End Function

    Public Function GetBatchIssued() As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_REPORTS.GetBatchIssued"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Add("GetBatchIssued", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_start_date", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = Me.StartDate
            MyCmd.Parameters.Add("v_end_date", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = Me.EndDate
            MyCmd.Parameters.Add("v_line_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = Me.LineID
            MyCmd.Parameters.Add("v_batchNr", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = Me.BachtNr


            MyCmd.Connection = conn.Open
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "GetBatchIssued")

            MyDataView = MyDs.Tables("GetBatchIssued").DefaultView

            MyCmd.Dispose()
            MyAdapter.Dispose()


            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try

    End Function
    Public Function GetBatchInventoryReport(ByRef MyLb As DropDownList, ByRef pg As Page) As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection
        Dim mydt As New DataTable

        Try

            MyCmd.CommandText = "PKG_REPORTS.GetBatchInventoryReport"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Add("StockIRIS", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_startDate", OracleDbType.Date, ParameterDirection.Input).Value = StartDate
            MyCmd.Parameters.Add("dist_id", OracleDbType.Int32, ParameterDirection.Input).Value = DistID
            MyCmd.Parameters.Add("line_id", OracleDbType.Int32, ParameterDirection.Input).Value = LineID
            MyCmd.Parameters.Add("product_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = ProductID
            MyCmd.Parameters.Add("v_batchNr", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = Me.BachtNr

            MyCmd.Connection = conn.Open

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "StockIRIS")
            MyDataView = MyDs.Tables("StockIRIS").DefaultView

            If ProductID = 0 Then
                mydt = DataAccessHelper.SelectDistinct("mydt", MyDs.Tables("StockIRIS"), "prod_id", "prod_presentation")
                fillDDwithDT(mydt, MyLb, "All Products")
            End If

            MyCmd.Dispose()
            MyAdapter.Dispose()

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try

    End Function
    Public Function GetProductAvalabilityReport() As DataView
        '***********************************************************************************************
        'Holt Stock Statistik aus der MV_STOCK
        ' 
        '***********************************************************************************************

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyConn As New MyConnection
        Dim myDt As DataTable


        Try
            MyCmd.CommandText = "PKG_Reports.GetProductAvailability"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Product", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_StartDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StartDate
            MyCmd.Parameters.Add("v_EndDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = EndDate
            MyCmd.Parameters.Add("v_line_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = LineID
          
            MyCmd.Connection = MyConn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Stockstat")
            MyDataView = MyDs.Tables("Stockstat").DefaultView
            MyConn.Close()

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try

    End Function
    Public Function GetStockForGDWReport() As DataView
        '***********************************************************************************************
        'Holt Stock Statistik aus der MV_STOCK
        ' 
        '***********************************************************************************************

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyConn As New MyConnection
        Dim myDt As DataTable


        Try
            MyCmd.CommandText = "PKG_Reports.GetStockForGDW"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("StockForGDW", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_StartDate", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = StartDate
            MyCmd.Parameters.Add("v_dist_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = DistID

            MyCmd.Connection = MyConn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "StockForGDW")
            MyDataView = MyDs.Tables("StockForGDW").DefaultView
            MyConn.Close()

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try

    End Function
    Private m_str_StartDate As Date
    Private m_str_EndDate As Date
    Private m_strReportName As String
    Private m_strBudgetType As String
    Private m_i_Currency As String
    Private m_i_lineID As Integer
    Private m_i_distID As Integer
    Private m_i_ctryID As Integer
    Private m_strInvoicer As String
    Private m_i_WorkDaysMonth As Integer
    Private m_i_WorkDaysToday As Integer
    Private m_str_lineCode As String
    Private m_i_Product_id As Integer
    Private m_str_prod_desc As String
    Private m_str_cust_no As String
    Private m_i_sare_id As Integer
    Private m_i_obs_code As String
    Private m_i_targetPG As Integer
    Private m_str_bachtNr As String


    Public Property StartDate() As Date ' String
        Get
            Return CDate(m_str_StartDate)
        End Get
        Set(ByVal Value As Date)    ' String)
            m_str_StartDate = Value
        End Set
    End Property
    Public Property EndDate() As Date 'String
        Get
            Return CDate(m_str_EndDate)    ' FormatDateForReport(CDate(m_str_EndDate))
        End Get
        Set(ByVal Value As Date)    ' String)
            m_str_EndDate = Value
        End Set
    End Property
    Public Property ReportName() As String
        Get
            Return m_strReportName
        End Get
        Set(ByVal Value As String)
            m_strReportName = Value
        End Set
    End Property
    Public Property Invoicer() As String
        Get
            Return m_strInvoicer
        End Get
        Set(ByVal Value As String)
            m_strInvoicer = Value
        End Set
    End Property
    Public Property BudgetType() As String
        Get
            Return m_strBudgetType
        End Get
        Set(ByVal Value As String)
            m_strBudgetType = Value
        End Set
    End Property
    Public Property LineID() As Integer
        Get
            Return m_i_lineID
        End Get
        Set(ByVal Value As Integer)
            m_i_lineID = Value
        End Set
    End Property
    Public Property LineCode() As String
        Get
            Return m_str_lineCode
        End Get
        Set(ByVal Value As String)
            m_str_lineCode = Value
        End Set
    End Property
    Public Property DistID() As Integer
        Get
            Return m_i_distID
        End Get
        Set(ByVal Value As Integer)
            m_i_distID = Value
        End Set
    End Property
    Public Property CtryID() As Integer
        Get
            Return m_i_ctryID
        End Get
        Set(ByVal Value As Integer)
            m_i_ctryID = Value
        End Set
    End Property
    Public Property WorkDaysMonth() As Integer
        Get
            Return m_i_WorkDaysMonth
        End Get
        Set(ByVal Value As Integer)
            m_i_WorkDaysMonth = Value
        End Set
    End Property
    Public Property WorkDaysToday() As Integer
        Get
            Return m_i_WorkDaysToday
        End Get
        Set(ByVal Value As Integer)
            m_i_WorkDaysToday = Value
        End Set
    End Property
    Public Property ProductID() As Integer
        Get
            Return m_i_Product_id
        End Get
        Set(ByVal Value As Integer)
            m_i_Product_id = Value
        End Set
    End Property
    Public Property ProdDesc() As String
        Get
            Return m_str_prod_desc
        End Get
        Set(ByVal Value As String)
            m_str_prod_desc = Value
        End Set
    End Property
    Public Property CustomerNo() As String
        Get
            Return m_str_cust_no
        End Get
        Set(ByVal Value As String)
            m_str_cust_no = Value
        End Set
    End Property
    Public Property SaReID() As Integer
        Get
            Return m_i_sare_id
        End Get
        Set(ByVal Value As Integer)
            m_i_sare_id = Value
        End Set
    End Property
    Public Property TargetPG() As Integer
        Get
            Return m_i_targetPG
        End Get
        Set(ByVal Value As Integer)
            m_i_targetPG = Value
        End Set
    End Property
    Public Property OBSCode() As String
        Get
            Return m_i_obs_code
        End Get
        Set(ByVal Value As String)
            m_i_obs_code = Value
        End Set
    End Property
    Public Property BachtNr() As String
        Get
            Return m_str_bachtNr
        End Get
        Set(ByVal Value As String)
            m_str_bachtNr = Value
        End Set
    End Property
End Class

