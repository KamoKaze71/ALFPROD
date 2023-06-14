Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities
'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
'''         <revision>1</revision>
''' <summary><para>Helper Class that returns various filled Dropdownboxes used in ALF </para></summary>
'''<seealso cref="Utilities.FTP">[label]</seealso> 
Public Class Exporter

    Private expDataView As New DataView
    Private myReport As New DataView
    Private strOut As String
    Private rowcount As Integer
    Private myCollection As Hashtable

    Public Sub New()
        myCollection = Wyeth.Alf.WyethImportHelper.readAppVars()
    End Sub

    Public Sub New(ByVal strReportname As String)
        myCollection = Wyeth.Alf.WyethImportHelper.readAppVars()
        myReport = Wyeth.Alf.WyethImportHelper.getExportReports()
        myReport.RowFilter = "rpt_name='" & strReportname & "'"

        Me._exportFileName = Convert.ToString(myReport.Item(0).Item("rpt_expfilename"))
        Me._delimiter = Convert.ToString(myReport.Item(0).Item("rpt_delimiter"))
        Me._queryName = Convert.ToString(myReport.Item(0).Item("rpt_queryName"))

    End Sub

    Public Sub Execute()
        Try
            expDataView = getExportData(QueryName, StartDate)
            generateExportFile()
            writeExportFile(ExportFileName)

            If Me.GenerateTriggerFile = True Then
                strOut = ""
                writeExportFile(ExportFileName.Remove(ExportFileName.Length - 3, 3) + "trg")
            End If

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally

        End Try
    End Sub

    Public Function DownloadFile() As String
        expDataView = getExportData(QueryName, StartDate)
        Return generateExportFile()
    End Function
    Private Function generateExportFile() As String

        Dim out As String
        Try
            Dim sb As New System.Text.StringBuilder



            For Each dr As DataRow In expDataView.Table.Rows
                rowcount = rowcount + 1
                For Each o As Object In dr.ItemArray
                    sb.Append(Convert.ToString(o))
                    If Delimiter.Length > 0 Then
                        sb.Append(Delimiter.ToString())
                    Else

                    End If
                Next
                If Delimiter.Length > 0 Then
                    'strOut = strOut.TrimEnd(Delimiter.ToString())
                    'strOut = strOut.TrimEnd(Delimiter.ToString())
                Else

                End If

                sb.Append(vbNewLine)
            Next
            Me.Log = "Building ExportFile " & rowcount.ToString & " Lines....  done! "
            strOut = sb.ToString()
            Return sb.ToString

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        End Try
    End Function
    Private Sub writeExportFile(ByVal strFileName As String)
        Try
            Me.Log = "Writing Export File " + strFileName & vbNewLine
            If Wyeth.Utilities.FileHandling.createTextFile(myCollection("ExportDir") + strFileName, strOut) = False Then
                Me.Log = "Error while creating " & myCollection("ExportDir") & strFileName
            End If
        Catch ex As Exception
            Me.Log = ex.Message & vbNewLine & vbNewLine
            ExceptionInfo.Show(ex)
        Finally
            strOut = String.Empty
        End Try
    End Sub

    Private Function getExportData(ByVal queryName As String, ByVal startDate As Date) As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection
        Try
            MyCmd.CommandText = queryName
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Add(queryName, OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_date", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = startDate

            MyCmd.Connection = conn.Open
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, queryName)

            MyDataView = MyDs.Tables(queryName).DefaultView
            Me.Log = "Exceuting " + queryName + " ..."
            Me.Log = "Retrieved " + MyDataView.Count.ToString + " records"
            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Me.Log = ex.Message
        Finally

            MyCmd.Dispose()
            MyAdapter.Dispose()
            conn.Close()
        End Try

    End Function

    Private _strLog As String
    Private _queryName As String
    Private _startDate As Date
    Private _exportFileName As String
    Private _delimiter As String
    Private _remoteFTPDir As String
    Private _genTrigger As Boolean = False

    Public Property Log() As String
        Get
            Return _strLog
        End Get
        Set(ByVal Value As String)
            _strLog = _strLog + Value + vbNewLine
        End Set
    End Property
    Public Property ExportFileName() As String
        Get
            Return _exportFileName
        End Get
        Set(ByVal Value As String)
            _exportFileName = Value
        End Set
    End Property
    Public Property QueryName() As String
        Get
            Return _queryName
        End Get
        Set(ByVal Value As String)
            _queryName = Value
        End Set
    End Property
    Public Property Delimiter() As String
        Get

            Return _delimiter
        End Get
        Set(ByVal Value As String)
            _delimiter = Value
        End Set
    End Property
    Public Property StartDate() As Date
        Get
            Return _startDate
        End Get
        Set(ByVal Value As Date)
            _startDate = Value
        End Set
    End Property
    Public Property RemoteFTPDir() As String
        Get
            Return _remoteFTPDir
        End Get
        Set(ByVal Value As String)
            _remoteFTPDir = Value
        End Set
    End Property

    Public Property GenerateTriggerFile() As Boolean
        Get
            Return _genTrigger
        End Get
        Set(ByVal Value As Boolean)
            _genTrigger = Value
        End Set
    End Property
End Class
