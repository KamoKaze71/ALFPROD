Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities
Imports Wyeth.Alf.DataAccessBaseClass


'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
'''         <revision>23</revision>
''' <summary><para>Provides DataAccess for verifying ImportStatus</para></summary>
Public Class AlfStatus

	Private m_str_logs_source As String
    Private m_i_ctry_id As Integer
    Private dataBase As New DataAccessBaseClass
    Public Function GetCountrID(ByVal ctry_code As String) As Integer
        Try
            Dim MyCmd As New OracleCommand
            Dim MyConn As New MyConnection
            Dim ret_val As Integer

            MyCmd.CommandText = "PKG_APPLICATION.GetCountryId"
            MyCmd.CommandType = CommandType.StoredProcedure

            'MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Country", OracleDbType.Int16, DBNull.Value, ParameterDirection.ReturnValue)
            MyCmd.Parameters.Add("Country", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = ctry_code

            MyCmd.Connection = MyConn.Open()
            MyCmd.ExecuteScalar()
            ret_val = CInt(MyCmd.Parameters(0).Value())
            MyConn.Close()

            Return ret_val
        Catch ex As Exception
            ExceptionInfo.Show(ex)

        End Try
    End Function
    Public Function NotImportedTransmissions() As DataView
        Dim MyConn As New MyConnection
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyDs As New DataSet
        Try
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_IMPORT_ERRORS.NotImportedTransmissions"
            MyCmd.Parameters.Add("Transmissions", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)

            MyCmd.Connection = MyConn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Transmissions")
            MyDataView = MyDs.Tables("Transmissions").DefaultView
            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try
    End Function

    Public Function NotImportedTransmissionsMue() As DataView
        Dim MyConn As New MyConnection
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyDs As New DataSet
        Try
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_IMPORT_ERRORS.NotImportedTransmissions_MUE"
            MyCmd.Parameters.Add("Transmissions", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)

            MyCmd.Connection = MyConn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Transmissions")
            MyDataView = MyDs.Tables("Transmissions").DefaultView
            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try
    End Function
    Public Function GetALFStatus() As Integer
        Dim MyConn As New MyConnection
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        'Dim MyDataViewmue As New DataView
        Dim MyDs As New DataSet
        Dim numberOfImportErrors As Integer
        Dim ret_val As Integer
        Try
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_IMPORT_ERRORS.NotImportedTransmissions"
            MyCmd.Parameters.Add("Transmissions", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Connection = MyConn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Transmissions")
            MyDataView = MyDs.Tables("Transmissions").DefaultView

            'MyCmd.Parameters.Clear()
            'MyCmd.CommandType = CommandType.StoredProcedure
            'MyCmd.CommandText = "PKG_IMPORT_ERRORS.NotImportedTransmissions_MUE"
            'MyCmd.Parameters.Add("Transmissions", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            'MyCmd.Connection = MyConn.Open()
            'MyAdapter.SelectCommand = MyCmd
            'MyAdapter.Fill(MyDs, "Transmissions")
            'MyDataViewmue = MyDs.Tables("Transmissions").DefaultView


            MyCmd.Parameters.Clear()
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_IMPORT_ERRORS.GetNumberTempTableSalesRecords"
            MyCmd.Parameters.Add("tmprec", OracleDbType.Int16, DBNull.Value, ParameterDirection.ReturnValue)
            MyCmd.Connection = MyConn.Open()
            MyCmd.ExecuteScalar()
            numberOfImportErrors = MyCmd.Parameters(0).Value()
            If numberOfImportErrors = 0 And MyDataView.Count = 0 Then
                ret_val = 0
            ElseIf MyDataView.Count > 0 Then
                ret_val = 1
            End If

            Return ret_val
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try
    End Function

    Public Function GetNumberOfImportErrors() As Integer
        Dim MyConn As New MyConnection
        Dim MyCmd As New OracleCommand
        Dim numberOfImportErrors As Integer
        Try
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_IMPORT_ERRORS.GetNumberOfImportError"
            MyCmd.Parameters.Add("importError", OracleDbType.Int16, DBNull.Value, ParameterDirection.ReturnValue)
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = CtryID
            MyCmd.Parameters.Add("v_logs_source", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = LogsSource

            MyCmd.Connection = MyConn.Open()
            MyCmd.ExecuteScalar()
            numberOfImportErrors = MyCmd.Parameters(0).Value()
            Return numberOfImportErrors

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try
    End Function

    Public Function GetNumberOfImportNewProducts() As Integer
        Dim MyConn As New MyConnection
        Dim MyCmd As New OracleCommand
        Dim numberOfImportNews As Integer
        Try
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_IMPORT_ERRORS.GetNumberOfImportNewProducts"
            MyCmd.Parameters.Add("importError", OracleDbType.Int16, DBNull.Value, ParameterDirection.ReturnValue)
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = CtryID
            MyCmd.Connection = MyConn.Open()
            MyCmd.ExecuteScalar()
            numberOfImportNews = MyCmd.Parameters(0).Value()
            Return numberOfImportNews

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try
    End Function
    Public Function GetLatestImportNewProducts() As DataView
        Dim MyConn As New MyConnection
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyDs As New DataSet
        Try
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_IMPORT_ERRORS.GetLatestImportNewProducts"
            MyCmd.Parameters.Add("importError", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = CtryID
            MyCmd.Connection = MyConn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Currency")
            MyDataView = MyDs.Tables("Currency").DefaultView
            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try
    End Function

    Public Function GetLatestImportNewCustomers() As DataView
        Dim MyConn As New MyConnection
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyDs As New DataSet
        Try
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_IMPORT_ERRORS.GetLatestImportNewCustomers"
            MyCmd.Parameters.Add("importError", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int16, DBNull.Value, ParameterDirection.Input).Value = CtryID
            MyCmd.Connection = MyConn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Currency")
            MyDataView = MyDs.Tables("Currency").DefaultView
            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try
    End Function
    Public Function GetNumberOfImportNewCustomers() As Integer
        Dim MyConn As New MyConnection
        Dim MyCmd As New OracleCommand
        Dim numberOfImportNews As Integer
        Try
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_IMPORT_ERRORS.GetNumberOfImportNewCustomers"
            MyCmd.Parameters.Add("importError", OracleDbType.Int16, DBNull.Value, ParameterDirection.ReturnValue)
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = CtryID
            MyCmd.Connection = MyConn.Open()
            MyCmd.ExecuteScalar()
            numberOfImportNews = MyCmd.Parameters(0).Value()
            Return numberOfImportNews

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try
    End Function

    Public Function GetNumberOfStockErrors() As Integer
        Dim MyConn As New MyConnection
        Dim MyCmd As New OracleCommand
        Dim numberOfStockErrors As Integer
        Try
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_IMPORT_ERRORS.GetNumberOfStockError"
            MyCmd.Parameters.Add("stockError", OracleDbType.Int16, DBNull.Value, ParameterDirection.ReturnValue)
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = CtryID
            MyCmd.Parameters.Add("v_logs_source", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = LogsSource
            MyCmd.Connection = MyConn.Open()
            MyCmd.ExecuteScalar()
            numberOfStockErrors = MyCmd.Parameters(0).Value()
            Return numberOfStockErrors
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try
    End Function
    Public Function GetStockErrorsDetails() As String
        Dim MyConn As New MyConnection
        Dim MyCmd As New OracleCommand
        Dim StockErrorsDetail As String
        Dim val As String
        Try
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_IMPORT_ERRORS.GetStockErrorDetails"
            MyCmd.Parameters.Add("stockError", OracleDbType.Varchar2, 20000, val, ParameterDirection.ReturnValue)
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = CtryID
            MyCmd.Parameters.Add("v_logs_source", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = LogsSource
            MyCmd.Connection = MyConn.Open()
            val = MyCmd.ExecuteScalar()
            StockErrorsDetail = Convert.ToString(MyCmd.Parameters(0).Value())
            Return StockErrorsDetail
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try
    End Function

    Public Function GetLatestImportError() As DataView

        Dim MyConn As New MyConnection
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyDs As New DataSet

        Try
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_IMPORT_ERRORS.GetLatestImportError"
            MyCmd.Parameters.Add("importError", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = CtryID
            MyCmd.Parameters.Add("v_logs_source", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = LogsSource

            MyCmd.Connection = MyConn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Currency")
            MyDataView = MyDs.Tables("Currency").DefaultView
            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)

        Finally
            MyConn.Close()
        End Try


    End Function
    Public Function GetDetailImportErrors() As DataView

        Dim MyConn As New MyConnection
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyDs As New DataSet

        Try
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_IMPORT_ERRORS.GetDetailImportError"
            MyCmd.Parameters.Add("importError", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = CtryID
            MyCmd.Parameters.Add("v_logs_source", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = LogsSource

            MyCmd.Connection = MyConn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Currency")
            MyDataView = MyDs.Tables("Currency").DefaultView
            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)

        Finally
            MyConn.Close()
        End Try


    End Function
    Public Function GetLatestImport() As DataView

        Dim MyConn As New MyConnection
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyDs As New DataSet

        Try
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_IMPORT_ERRORS.GetLatestImport"
            MyCmd.Parameters.Add("importError", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = CtryID
            MyCmd.Parameters.Add("v_logs_source", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = LogsSource

            MyCmd.Connection = MyConn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Currency")
            MyDataView = MyDs.Tables("Currency").DefaultView
            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)

        Finally
            MyConn.Close()
        End Try


    End Function


    Public Function getImportNews() As DataView
        Dim parameters(0) As OracleParameter
        parameters(0) = New OracleParameter("News", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        Return dataBase.executeStoredProcedure("PKG_APPLICATION.GetunassignedProducts", parameters)
    End Function

    Public Function GetNumberTempTableRecords() As Integer
        Dim MyConn As New MyConnection
        Dim MyCmd As New OracleCommand
        Dim numberOfImportErrors As Integer
        Try
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_IMPORT_ERRORS.GetNumberTempTableRecords"
            MyCmd.Parameters.Add("tmprec", OracleDbType.Int16, DBNull.Value, ParameterDirection.ReturnValue)
            MyCmd.Connection = MyConn.Open()
            MyCmd.ExecuteScalar()
            numberOfImportErrors = MyCmd.Parameters(0).Value()
            Return numberOfImportErrors

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try
    End Function

    Public Function GetNumberTempTableSAlesRecords() As Integer
        Dim MyConn As New MyConnection
        Dim MyCmd As New OracleCommand
        Dim numberOfImportErrors As Integer
        Try
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_IMPORT_ERRORS.GetNumberTempTableSalesRecords"
            MyCmd.Parameters.Add("tmprec", OracleDbType.Int16, DBNull.Value, ParameterDirection.ReturnValue)
            MyCmd.Connection = MyConn.Open()
            MyCmd.ExecuteScalar()
            numberOfImportErrors = MyCmd.Parameters(0).Value()
            Return numberOfImportErrors

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try
    End Function


    Public Function GetNumberTempTableRecordsART() As Integer
        Dim MyConn As New MyConnection
        Dim MyCmd As New OracleCommand
        Dim numberOfImportErrors As Integer
        Try
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_IMPORT_ERRORS.GetNumberTempTableRecordsART"
            MyCmd.Parameters.Add("tmprec", OracleDbType.Int16, DBNull.Value, ParameterDirection.ReturnValue)
            MyCmd.Connection = MyConn.Open()
            MyCmd.ExecuteScalar()
            numberOfImportErrors = MyCmd.Parameters(0).Value()
            Return numberOfImportErrors

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try
    End Function
    Public Function GetNumberTempTableRecordsKD() As Integer
        Dim MyConn As New MyConnection
        Dim MyCmd As New OracleCommand
        Dim numberOfImportErrors As Integer
        Try
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_IMPORT_ERRORS.GetNumberTempTableRecordsKD"
            MyCmd.Parameters.Add("tmprec", OracleDbType.Int16, 2000, ParameterDirection.ReturnValue)
            MyCmd.Connection = MyConn.Open()
            MyCmd.ExecuteScalar()
            numberOfImportErrors = MyCmd.Parameters(0).Value()
            Return numberOfImportErrors

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try
    End Function
    Public Function IsHoliday(ByVal v_date As Date, ByVal v_ctry_code As String) As Boolean
        Dim MyConn As New MyConnection
        Dim MyCmd As New OracleCommand
        Dim holiday As Integer
        Try
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_Application.IsHoliDay"
            MyCmd.Parameters.Add("ret_val", OracleDbType.Int16, DBNull.Value, ParameterDirection.ReturnValue)
            MyCmd.Parameters.Add("v_date", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = v_date
            MyCmd.Parameters.Add("ctry_code", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = v_ctry_code

            MyCmd.Connection = MyConn.Open()
            MyCmd.ExecuteScalar()
            holiday = MyCmd.Parameters(0).Value()

            If holiday = 1 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
            MyCmd.Dispose()
            MyConn = Nothing
            MyCmd = Nothing
            holiday = Nothing

        End Try
    End Function


    Public Function getLastExportDate(ByVal v_date As Date) As String

        Dim MyConn As New MyConnection
        Dim MyCmd As New OracleCommand
        Dim retVal As String

        Try
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_Application.GetLastExportDate"
            MyCmd.Parameters.Add("ret_val", OracleDbType.Varchar2, 2000, DBNull.Value, ParameterDirection.ReturnValue)
            MyCmd.Parameters.Add("v_date", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = v_date

            MyCmd.Connection = MyConn.Open()
            MyCmd.ExecuteScalar()
            retVal = Convert.ToString(MyCmd.Parameters(0).Value())

            Return retVal

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
            MyCmd.Dispose()
            MyConn = Nothing
            MyCmd = Nothing


        End Try
    End Function

    Public Function getNumberofExpectedExports(ByVal v_day As Integer) As Integer

        Dim MyConn As New MyConnection
        Dim MyCmd As New OracleCommand
        Dim retVal As String

        Try
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_Application.getNumberofExpectedExports"
            MyCmd.Parameters.Add("ret_val", OracleDbType.Int32, 2000, DBNull.Value, ParameterDirection.ReturnValue)
            MyCmd.Parameters.Add("v_day", OracleDbType.Int16, DBNull.Value, ParameterDirection.Input).Value = v_day

            MyCmd.Connection = MyConn.Open()
            MyCmd.ExecuteScalar()
            retVal = Convert.ToInt32(MyCmd.Parameters(0).Value())

            Return retVal

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
            MyCmd.Dispose()
            MyConn = Nothing
            MyCmd = Nothing


        End Try
    End Function

    Public Function getNumberofExportsDone(ByVal v_date As Date) As Integer

        Dim MyConn As New MyConnection
        Dim MyCmd As New OracleCommand
        Dim retVal As Integer
        v_date = v_date.Date
        Try
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_Application.GetNumberOfExportsDone"
            MyCmd.Parameters.Add("ret_val", OracleDbType.Int32, 2000, DBNull.Value, ParameterDirection.ReturnValue)
            MyCmd.Parameters.Add("v_date", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = v_date

            MyCmd.Connection = MyConn.Open()
            MyCmd.ExecuteScalar()
            retVal = Convert.ToInt32(MyCmd.Parameters(0).Value())

            Return retVal

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
            MyCmd.Dispose()
            MyConn = Nothing
            MyCmd = Nothing


        End Try
    End Function

    Public Function checkLastExportDate(ByVal v_date As Date) As Integer

        Dim MyConn As New MyConnection
        Dim MyCmd As New OracleCommand
        Dim retVal As Integer
        v_date = v_date.Date
        Try
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_Application.GetNumberOfExportsDone"
            MyCmd.Parameters.Add("ret_val", OracleDbType.Int32, 2000, DBNull.Value, ParameterDirection.ReturnValue)
            MyCmd.Parameters.Add("v_date", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = v_date

            MyCmd.Connection = MyConn.Open()
            MyCmd.ExecuteScalar()
            retVal = Convert.ToInt32(MyCmd.Parameters(0).Value())

            Return retVal

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
            MyCmd.Dispose()
            MyConn = Nothing
            MyCmd = Nothing


        End Try
    End Function

    Public Function GetTransmissions(ByVal dist_id As Integer, ByVal year As Integer) As DataView
        Dim MyConn As New MyConnection
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyDs As New DataSet
        Try
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_APPLICATION.CompareTransmissions"
            MyCmd.Parameters.Add("importError", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_dist_id", OracleDbType.Int16, DBNull.Value, ParameterDirection.Input).Value = dist_id
            MyCmd.Parameters.Add("v_year", OracleDbType.Int16, DBNull.Value, ParameterDirection.Input).Value = year
            MyCmd.Connection = MyConn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Transmission")
            MyDataView = MyDs.Tables("Transmission").DefaultView
            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try
    End Function

    Public Property LogsSource() As String
        Get
            Return m_str_logs_source
        End Get
        Set(ByVal Value As String)
            m_str_logs_source = Value
        End Set
    End Property
    Public Property CtryID() As Integer
        Get
            Return m_i_ctry_id
        End Get
        Set(ByVal Value As Integer)
            m_i_ctry_id = Value
        End Set
    End Property


End Class
