Option Explicit On 
Option Strict On

Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports Wyeth.Utilities
Imports wyeth.Utilities.DateHandling
Imports System.Globalization
Imports Wyeth.Utilities.Helper
'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
''' <summary><para>Provides DataAccess for ALF Month End Processing</para></summary>
Public Class MEPData

    Public Function ProcessInvoices(ByVal v_month As Date, ByVal v_ctry_id As Integer) As Boolean

        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Try
            MyCmd.CommandText = "PKG_MONTH_END.P_Invoice_Processing"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_month", OracleDbType.Date, ParameterDirection.Input).Value = v_month
            MyCmd.Connection = Myconn.Open()
            MyCmd.ExecuteNonQuery()

            Return True

        Catch ex As Exception

            ExceptionInfo.Show(ex)
            Return False

        Finally
            Myconn.Close()
        End Try
    End Function

    Public Function SetFinaceApproval(ByVal v_month As Date, ByVal v_user_id As Integer, ByVal v_ctry_id As Integer) As Boolean

        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Try
            MyCmd.CommandText = "PKG_MONTH_END.P_Set_FinanceApproval"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_month", OracleDbType.Date, ParameterDirection.Input).Value = v_month
            MyCmd.Parameters.Add("v_user_id", OracleDbType.Int16, ParameterDirection.Input).Value = v_user_id
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int16, ParameterDirection.Input).Value = v_ctry_id

            MyCmd.Connection = Myconn.Open()
            MyCmd.ExecuteNonQuery()

            Return True

        Catch ex As Exception

            ExceptionInfo.Show(ex)
            Return False

        Finally
            Myconn.Close()
        End Try
    End Function
    Public Function RollbackFinaceApproval(ByVal v_month As Date, ByVal v_user_id As Integer, ByVal v_ctry_id As Integer) As Boolean

        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Try
            MyCmd.CommandText = "PKG_MONTH_END.P_Rollback_FinanceApproval"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_month", OracleDbType.Date, ParameterDirection.Input).Value = v_month
            MyCmd.Parameters.Add("v_user_id", OracleDbType.Int16, ParameterDirection.Input).Value = v_user_id
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int16, ParameterDirection.Input).Value = v_ctry_id

            MyCmd.Connection = Myconn.Open()
            MyCmd.ExecuteNonQuery()

            Return True

        Catch ex As Exception

            ExceptionInfo.Show(ex)
            Return False

        Finally
            Myconn.Close()
        End Try
    End Function
    Public Function SetJDEProcessed(ByVal v_month As Date, ByVal v_user_id As Integer, ByVal v_ctry_id As Integer) As Boolean

        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Try
            MyCmd.CommandText = "PKG_MONTH_END.P_Set_JDE_Processed"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_month", OracleDbType.Date, ParameterDirection.Input).Value = v_month
            MyCmd.Parameters.Add("v_user_id", OracleDbType.Int16, ParameterDirection.Input).Value = v_user_id
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int16, ParameterDirection.Input).Value = v_ctry_id

            MyCmd.Connection = Myconn.Open()
            MyCmd.ExecuteNonQuery()

            Return True

        Catch ex As Exception

            ExceptionInfo.Show(ex)
            Return False

        Finally
            Myconn.Close()
        End Try
    End Function
    Public Function SetJDEFinalApproval(ByVal v_month As Date, ByVal v_user_id As Integer, ByVal v_ctry_id As Integer) As Boolean

        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Try
            MyCmd.CommandText = "PKG_MONTH_END.P_Set_JDE_FinalApproval"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_month", OracleDbType.Date, ParameterDirection.Input).Value = v_month
            MyCmd.Parameters.Add("v_user_id", OracleDbType.Int16, ParameterDirection.Input).Value = v_user_id
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int16, ParameterDirection.Input).Value = v_ctry_id

            MyCmd.Connection = Myconn.Open()
            MyCmd.ExecuteNonQuery()

            Return True

        Catch ex As Exception

            ExceptionInfo.Show(ex)
            Return False

        Finally
            Myconn.Close()
        End Try
    End Function
    Public Function RollbackJDEFinalApproval(ByVal v_month As Date, ByVal v_user_id As Integer, ByVal v_ctry_id As Integer) As Boolean

        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Try
            MyCmd.CommandText = "PKG_MONTH_END.P_Rollback_JDE_FinalApproval"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_month", OracleDbType.Date, ParameterDirection.Input).Value = v_month
            MyCmd.Parameters.Add("v_user_id", OracleDbType.Int16, ParameterDirection.Input).Value = v_user_id
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int16, ParameterDirection.Input).Value = v_ctry_id

            MyCmd.Connection = Myconn.Open()
            MyCmd.ExecuteNonQuery()

            Return True

        Catch ex As Exception

            ExceptionInfo.Show(ex)
            Return False

        Finally
            Myconn.Close()
        End Try
    End Function
    Public Function SetLogisticsClosingUserID(ByVal v_month As Date, ByVal v_user_id As Integer, ByVal v_ctry_id As Integer) As Boolean

        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Try
            MyCmd.CommandText = "PKG_MONTH_END.P_SetLogisticsClosingUserID"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_month", OracleDbType.Date, ParameterDirection.Input).Value = v_month
            MyCmd.Parameters.Add("v_user_id", OracleDbType.Int16, ParameterDirection.Input).Value = v_user_id
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int16, ParameterDirection.Input).Value = v_ctry_id

            MyCmd.Connection = Myconn.Open()
            MyCmd.ExecuteNonQuery()

            Return True

        Catch ex As Exception

            ExceptionInfo.Show(ex)
            Return False

        Finally
            Myconn.Close()
        End Try
    End Function


    Public Function GetProcessedData(ByVal v_month As Date, ByVal v_ctry_id As Integer) As DataView

        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Dim MyDs As New DataSet
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView

        Try

            MyCmd.CommandText = "PKG_REPORTS.GetMonthEndProcessedData"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("MonthData", OracleDbType.RefCursor, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_month", OracleDbType.Date, ParameterDirection.Input).Value = v_month
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, ParameterDirection.Input).Value = v_ctry_id

            MyCmd.Connection = Myconn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Invoices")
            MyDataView = MyDs.Tables("Invoices").DefaultView

            Return MyDataView


        Catch ex As Exception

            ExceptionInfo.Show(ex)
        Finally
            Myconn.Close()

        End Try

    End Function
    Public Function RollBackInvoices(ByVal v_month As Date, ByVal v_ctry_id As Integer) As Boolean

        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Dim MyDs As New DataSet
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView

        Try

            MyCmd.CommandText = "PKG_MONTH_END.P_Invoice_Rollback"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_month", OracleDbType.Date, ParameterDirection.Input).Value = v_month

            MyCmd.Connection = Myconn.Open()
            MyCmd.ExecuteNonQuery()

            MyCmd.CommandText = "PKG_MONTH_END.P_GIT_Rollback"
            MyCmd.ExecuteNonQuery()

            Return True
        Catch ex As Exception

            ExceptionInfo.Show(ex)
            Return False

        Finally
            Myconn.Close()

        End Try

    End Function
    Public Function GetProcessedMonth(ByVal v_ctry_id As Integer) As DataView

        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Dim MyDs As New DataSet
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView

        Try

            MyCmd.CommandText = "PKG_MONTH_END.P_Get_ProcessedMonth"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("MonthData", OracleDbType.RefCursor, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, ParameterDirection.Input).Value = v_ctry_id

            MyCmd.Connection = Myconn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Invoices")
            MyDataView = MyDs.Tables("Invoices").DefaultView

            Return MyDataView


        Catch ex As Exception

            ExceptionInfo.Show(ex)
        Finally
            Myconn.Close()

        End Try

    End Function
    Public Function GetMonthEndHistoryLogs(ByVal v_month As String, ByVal v_ctry_id As Integer) As DataView

        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Dim MyDs As New DataSet
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView

        Try

            MyCmd.CommandText = "PKG_MONTH_END.P_Get_MonthEndHistoryLogs"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("MonthData", OracleDbType.RefCursor, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_month", OracleDbType.Varchar2, ParameterDirection.Input).Value = v_month
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, ParameterDirection.Input).Value = v_ctry_id

            MyCmd.Connection = Myconn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Invoices")
            MyDataView = MyDs.Tables("Invoices").DefaultView

            Return MyDataView


        Catch ex As Exception

            ExceptionInfo.Show(ex)
        Finally
            Myconn.Close()

        End Try

    End Function
    Public Function CheckForFinanceApproval(ByVal v_month As Date, ByVal v_ctry_id As Integer) As Boolean

        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Dim MyDs As New DataSet
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim tmp As String

        Try

            MyCmd.CommandText = "PKG_MONTH_END.F_Check_For_FinanceApproval"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("ret_val", OracleDbType.Int16, ParameterDirection.ReturnValue)
            MyCmd.Parameters.Add("v_month", OracleDbType.Date, ParameterDirection.Input).Value = v_month
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, ParameterDirection.Input).Value = v_ctry_id

            MyCmd.Connection = Myconn.Open()
            MyCmd.ExecuteScalar()
            tmp = MyCmd.Parameters(0).Value.ToString()

            If tmp = "0" Then ' Moth is not locked (approved)
                Return True

            Else     'Month is locked
                Return False
            End If


        Catch ex As Exception

            ExceptionInfo.Show(ex)
            Return False

        Finally
            Myconn.Close()

        End Try

    End Function
    Public Function CheckForJDEProcessing(ByVal v_month As Date, ByVal v_ctry_id As Integer) As Boolean

        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Dim MyDs As New DataSet
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim tmp As String

        Try

            MyCmd.CommandText = "PKG_MONTH_END.F_Check_For_JDEProcessing"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("ret_val", OracleDbType.Int16, ParameterDirection.ReturnValue)
            MyCmd.Parameters.Add("v_month", OracleDbType.Date, ParameterDirection.Input).Value = v_month
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, ParameterDirection.Input).Value = v_ctry_id

            MyCmd.Connection = Myconn.Open()
            MyCmd.ExecuteScalar()
            tmp = MyCmd.Parameters(0).Value.ToString()

            If tmp = "0" Then ' Moth is not locked (approved)
                Return True

            Else     'Month is locked
                Return False
            End If


        Catch ex As Exception

            ExceptionInfo.Show(ex)
            Return False

        Finally
            Myconn.Close()

        End Try

    End Function

    Public Function CheckForJDEFinalApproval(ByVal v_month As Date, ByVal v_ctry_id As Integer) As Boolean

        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Dim MyDs As New DataSet
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim tmp As String

        Try

            MyCmd.CommandText = "PKG_MONTH_END.F_Check_For_Jde_finalApproval"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("ret_val", OracleDbType.Int16, ParameterDirection.ReturnValue)
            MyCmd.Parameters.Add("v_month", OracleDbType.Date, ParameterDirection.Input).Value = v_month
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, ParameterDirection.Input).Value = v_ctry_id

            MyCmd.Connection = Myconn.Open()
            MyCmd.ExecuteScalar()
            tmp = MyCmd.Parameters(0).Value.ToString()

            If tmp = "0" Then ' Moth is not approved
                Return True

            Else     'Month is approved
                Return False
            End If


        Catch ex As Exception

            ExceptionInfo.Show(ex)
            Return False

        Finally
            Myconn.Close()

        End Try

    End Function
    Public Function CheckTCOGSForProcessing(ByVal v_month As Date, ByVal v_ctry_id As Integer) As DataView

        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Dim MyDs As New DataSet
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim tmp As String
        Dim val As Object

        Try


            MyCmd.CommandText = "PKG_MONTH_END.CheckTGCOGSForProcessing"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("invoice", OracleDbType.Varchar2, 2000, val, ParameterDirection.ReturnValue)
            MyCmd.Parameters.Add("v_month", OracleDbType.Date, ParameterDirection.Input).Value = v_month
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, ParameterDirection.Input).Value = v_ctry_id

            MyCmd.Connection = Myconn.Open()
            MyCmd.ExecuteScalar()
            tmp = MyCmd.Parameters(0).Value.ToString

            ' MyAdapter.SelectCommand = MyCmd
            'MyAdapter.Fill(MyDs, "Invoices")
            'MyDataView = MyDs.Tables("Invoices").DefaultView

            ' MyDataView = GetProcessedData(v_month, v_ctry_id)
            If tmp = "" Then
                tmp = "0"
            End If
            ProcessInvoices(v_month, v_ctry_id)
            MyDataView = GetProcessedData(v_month, v_ctry_id)
            MyDataView.RowFilter = "stoc_id in(" & tmp & ")"
            RollBackInvoices(v_month, v_ctry_id)

            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Myconn.Close()

        End Try

    End Function

    Public Function GetV_PM(ByVal v_month As Date, ByVal v_ctry_id As Integer) As DataView

        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Dim MyDs As New DataSet
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView

        Try

            MyCmd.CommandText = "PKG_MONTH_END.P_GetV_PM"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("MonthData", OracleDbType.RefCursor, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_month", OracleDbType.Date, ParameterDirection.Input).Value = v_month
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, ParameterDirection.Input).Value = v_ctry_id

            MyCmd.Connection = Myconn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "V_PM")
            MyDataView = MyDs.Tables("V_PM").DefaultView

            Return MyDataView


        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Myconn.Close()

        End Try

    End Function
    Public Function GetV_PMForYear(ByVal v_year As Integer, ByVal v_ctry_id As Integer) As DataView

        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Dim MyDs As New DataSet
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView

        Try
            MyCmd.CommandText = "PKG_MONTH_END.P_GetV_PMForYear"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("MonthData", OracleDbType.RefCursor, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_year", OracleDbType.Int32, ParameterDirection.Input).Value = v_year
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, ParameterDirection.Input).Value = v_ctry_id

            MyCmd.Connection = Myconn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "V_PM")
            MyDataView = MyDs.Tables("V_PM").DefaultView

            Return MyDataView
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Myconn.Close()
        End Try

    End Function

    Public Sub setProcessMonth()
        Dim mystock As New Stock
        mystock.StockCtryID = CInt(HttpContext.Current.Session("country_id"))
        ' Holt das max processed Month aus t_pm
        HttpContext.Current.Session("LastProcessMonth") = Convert.ToDateTime(mystock.GetLastProcessedMonth()).ToString(DATEFORMAT_STRING_REPORT)
        ' Holt das max processed + finance approved Month aus t_pm
        HttpContext.Current.Session("LastMonthApproved") = Convert.ToDateTime(mystock.GetLastMonthApproved()).ToString(DATEFORMAT_STRING_REPORT)
        ' Holt das laufende processe Month aus t_pm
        HttpContext.Current.Session("CurrentProcessMonth") = Convert.ToDateTime(mystock.GetCurrentProcessMonth).ToString(DATEFORMAT_STRING_REPORT)

        HttpContext.Current.Session("FinalJDEMonth") = Convert.ToDateTime(mystock.GetLastFinalJDEMonth).ToString(DATEFORMAT_STRING_REPORT)
    End Sub

End Class
