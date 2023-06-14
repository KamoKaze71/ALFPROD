Option Explicit On 
Option Strict On
Imports System
Imports System.Globalization
Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities.Helper


Public Class DateHandling

    Public Shared Function FirstOfThisQ(ByVal dat As Date) As Date
        Try

            Dim intYear As Integer = Year(dat)
            Dim intMth As Integer = Month(dat)
            Dim strMthEnd As String

            ' Get the current quarter end date.
            '
            Select Case intMth
                Case 1, 2, 3
                    strMthEnd = intYear & "-01-01"

                Case 4, 5, 6
                    strMthEnd = intYear & "-04-01"

                Case 7, 8, 9
                    strMthEnd = intYear & "-07-01"

                Case 10, 11, 12
                    strMthEnd = intYear & "-10-01"
            End Select

            Return Convert.ToDateTime(strMthEnd, GetMyDTFI())

        Catch ex As Exception
            ExceptionInfo.Show(ex)

        End Try
    End Function

    Public Shared Function LastOfThisQ(ByVal dat As Date) As Date
        Try

            '
        ' Convert a date in "yyyymmdd" format to
        ' the closest quarter end date.
        '
            Dim intYear As Integer = Year(dat)
            Dim intMth As Integer = Month(dat)
            Dim strMthEnd As String


            Select Case intMth
                Case 1, 2, 3
                    strMthEnd = intYear & "-03-31"

                Case 4, 5, 6
                    strMthEnd = intYear & "-06-30"

                Case 7, 8, 9
                    strMthEnd = intYear & "-09-30"

                Case 10, 11, 12
                    strMthEnd = intYear & "-12-31"
            End Select

            Return Convert.ToDateTime(strMthEnd, GetMyDTFI())
        Catch ex As Exception
            ExceptionInfo.Show(ex)

        End Try
    End Function

    Public Shared Function FirstOfThisMonth(ByVal dat As Date) As Date
        Try
            Dim first As Date

            first = DateSerial(Year(dat), Month(dat), 1)
            Return first

        Catch ex As Exception
            ExceptionInfo.Show(ex)

        End Try
    End Function

    Public Shared Function LastOfThisMonth(ByVal dat As Date) As Date
        Try
            Dim last As Date
            Dim str_last As String
            last = DateSerial(Year(dat), Month(dat) + 1, 0)
            Return last


        Catch ex As Exception
            ExceptionInfo.Show(ex)

        End Try
    End Function

    Public Shared Function IsWorkDay(ByVal dat As Date) As Boolean
        Try
            Dim i As Integer

            i = Weekday(dat, FirstDayOfWeek.Monday)
            If i = 7 Or i = 6 Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            ExceptionInfo.Show(ex)

        End Try
    End Function

    Public Shared Function GetWorkDaysForMonth(ByVal dat As Date, ByVal ctry_id As Integer) As Integer
        Try
            Dim MyCmd As New OracleCommand
            Dim MyConn As New MyConnection
            Dim i As Long
            Dim holidays, result As Integer
            Dim d1, d2 As Date
            Dim val, test As String
            Dim obj As Object

            ' get number of holidays within the month
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_APPLICATION.GetWorkdaysForMonth"
            MyCmd.Parameters.Add("v_result", OracleDbType.Int32, ParameterDirection.ReturnValue)
            MyCmd.Parameters.Add("v_month", OracleDbType.Date, ParameterDirection.Input).Value = dat
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, ParameterDirection.Input).Value = ctry_id
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Connection = MyConn.Open
            holidays = CInt(MyCmd.ExecuteScalar())
            obj = MyCmd.Parameters(0).Value
            holidays = CInt(obj)

            MyConn.Close()

            d1 = FirstOfThisMonth(dat)

            d2 = LastOfThisMonth(dat)

            i = DateDiff(DateInterval.Day, d1, d2)

            result = CInt(i) - holidays + 1
            Return result
        Catch ex As Exception
            ExceptionInfo.Show(ex)

        End Try
    End Function

    Public Shared Function GetWorkDaysForMonthToday(ByVal dat As Date, ByVal ctry_id As Integer) As Integer
        Try
            Dim MyCmd As New OracleCommand
            Dim MyConn As New MyConnection
            Dim i As Long
            Dim holidays, result As Integer
            Dim d1, d2 As Date
            Dim obj As Object

            ' get number of holidays within the month
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_APPLICATION.GetWorkdaysForMonthToday"
            MyCmd.Parameters.Add("v_result", OracleDbType.Int32, ParameterDirection.ReturnValue)
            MyCmd.Parameters.Add("v_month", OracleDbType.Date, ParameterDirection.Input).Value = dat
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, ParameterDirection.Input).Value = ctry_id
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Connection = MyConn.Open
            holidays = CInt(MyCmd.ExecuteScalar())
            obj = MyCmd.Parameters(0).Value
            holidays = CInt(obj)

            MyConn.Close()

            d1 = FirstOfThisMonth(dat)
            d2 = dat

            i = DateDiff(DateInterval.Day, d1, d2)

            result = CInt(i) - holidays + 1
            Return result
        Catch ex As Exception
            ExceptionInfo.Show(ex)

        End Try
    End Function

    Public Shared Function GetWorkDaysYear(ByVal dat As Date, ByVal ctry_id As Integer) As Integer
        Try
            Dim MyCmd As New OracleCommand
            Dim MyConn As New MyConnection
            Dim i As Long
            Dim holidays, result As Integer
            Dim d1, d2 As Date
            Dim obj As Object

            ' get number of holidays within the month
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_APPLICATION.GetWorkDaysYear"
            MyCmd.Parameters.Add("v_result", OracleDbType.Int32, ParameterDirection.ReturnValue)
            MyCmd.Parameters.Add("v_month", OracleDbType.Date, ParameterDirection.Input).Value = dat
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, ParameterDirection.Input).Value = ctry_id
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Connection = MyConn.Open
            holidays = CInt(MyCmd.ExecuteScalar())
            obj = MyCmd.Parameters(0).Value
            holidays = CInt(obj)

            MyConn.Close()

            d1 = DateSerial(Year(dat), 1, 1)
            d2 = DateSerial(Year(dat), 12, 31)

            i = DateDiff(DateInterval.Day, d1, d2)

            result = CInt(i) - holidays
            Return result
        Catch ex As Exception
            ExceptionInfo.Show(ex)

        End Try
    End Function

    Public Shared Function GetWorkDaysYearToDate(ByVal dat As Date, ByVal ctry_id As Integer) As Integer
        Try
            Dim MyCmd As New OracleCommand
            Dim MyConn As New MyConnection
            Dim i As Long
            Dim holidays, result As Integer
            Dim d1, d2 As Date
            Dim obj As Object

            ' get number of holidays within the month
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_APPLICATION.GetWorkDaysYearToDate"
            MyCmd.Parameters.Add("v_result", OracleDbType.Int32, ParameterDirection.ReturnValue)
            MyCmd.Parameters.Add("v_month", OracleDbType.Date, ParameterDirection.Input).Value = dat
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, ParameterDirection.Input).Value = ctry_id
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Connection = MyConn.Open
            holidays = CInt(MyCmd.ExecuteScalar())
            obj = MyCmd.Parameters(0).Value
            holidays = CInt(obj)

            MyConn.Close()

            d1 = DateSerial(Year(dat), 1, 1)
            d2 = dat

            i = DateDiff(DateInterval.Day, d1, d2)

            result = CInt(i) - holidays
            Return result
        Catch ex As Exception
            ExceptionInfo.Show(ex)

        End Try
    End Function

    Public Shared Function FormatDate(ByVal dat As Date, Optional ByVal ctry_id As Integer = 56) As String

        Dim MYDTFI As New DateTimeFormatInfo
        Dim ret_val As String
        If ctry_id = -1 Then
            MYDTFI.DateSeparator = ""
        Else

            MYDTFI.DateSeparator = "-"
        End If
        MYDTFI.ShortDatePattern = DATEFORMAT_STRING_REPORT
        MYDTFI.LongDatePattern = DATEFORMAT_STRING_REPORT
        ret_val = dat.ToString(DATEFORMAT_STRING_REPORT, MYDTFI)
        Return ret_val

    End Function

    Public Shared Function FormatDateTime(ByVal dat As Date, Optional ByVal ctry_id As Integer = 56) As String


        Dim ret_val As String
        ret_val = dat.ToString("yyyy_MM_dd_HH:mm", GetMyDTFI)
        Return ret_val

    End Function

    Public Shared Function GetMyDTFI(Optional ByVal ctry_id As Integer = 56) As DateTimeFormatInfo
        Dim MonthNames() As String = {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec", ""}
        Dim MYDTFI As New DateTimeFormatInfo
        Dim ret_val As String
        MYDTFI.AbbreviatedMonthNames = MonthNames
        MYDTFI.DateSeparator = "-"
        MYDTFI.ShortDatePattern = DATEFORMAT_STRING_REPORT
        MYDTFI.LongDatePattern = DATEFORMAT_STRING_REPORT


        Return MYDTFI

    End Function


End Class
