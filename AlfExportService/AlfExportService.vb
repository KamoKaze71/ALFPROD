Imports System.ServiceProcess
Imports Wyeth
Imports Wyeth.Alf
Imports Wyeth.Alf.WyethImport
Imports Wyeth.Utilities
Imports Wyeth.Utilities.Helper
Imports Wyeth.Utilities.DateHandling
Imports Microsoft.Win32
Imports System
Imports System.IO
Imports System.Collections
Imports System.Globalization
Imports System.Web
Imports System.Diagnostics
Imports Oracle.DataAccess.Client
'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
''' <summary><para>Executes Exports of ALF Data</para></summary>
Public Class AlfExportService
    Inherits System.ServiceProcess.ServiceBase

    Private d_day As Date = Today()
    Protected WithEvents tmrAction As System.Timers.Timer
    Private MyLog As New Log
    Private MyCollection As New Hashtable
    Private debug As Boolean = False   ' set true if you want to debug the service -> does not delete ftp files on server
    Private expView As New DataView
    Public Sub New()
        MyBase.New()

        ' This call is required by the Component Designer.
        InitializeComponent()
    End Sub

    ' The main entry point for the process
    <MTAThread()> _
    Shared Sub Main()
        Dim ServicesToRun() As System.ServiceProcess.ServiceBase

        ' More than one NT Service may run within the same process. To add
        ' another service to this process, change the following line to
        ' create a second service object. For example,
        '
        '   ServicesToRun = New System.ServiceProcess.ServiceBase () {New Service1, New MySecondUserService}
        '
        ServicesToRun = New System.ServiceProcess.ServiceBase() {New AlfExportService}

        System.ServiceProcess.ServiceBase.Run(ServicesToRun)
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    ' NOTE: The following procedure is required by the Component Designer
    ' It can be modified using the Component Designer.  
    ' Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container
        Me.ServiceName = "AlfExportService"
    End Sub


    'UserService überschreibt den Löschvorgang zum Bereinigen der Komponentenliste.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub


    Protected Overrides Sub OnStart(ByVal args() As String)

        Try

            tmrAction = New System.Timers.Timer

            AddHandler tmrAction.Elapsed, AddressOf tmrAction_Tick
                debug = Settings.AlfServiceModeDebug

            If debug = False Then
                tmrAction.Interval = 600000                  '10 Min
            Else
                tmrAction.Interval = 60000                  '1 Min
            End If


            While MyCollection.Count = 0
                Try
                    MyCollection = Wyeth.Alf.WyethImportHelper.readAppVars()
                    expView = Wyeth.Alf.WyethImportHelper.getExportReports()

                Catch ex As Exception
                    Me.EventLog.WriteEntry(ex.message, EventLogEntryType.Error)


                Finally

                    If MyCollection.Count = 0 Then
                        System.Threading.Thread.Sleep(20000) ' 20 sec
                    End If

                End Try
            End While

            '' Make a Log Entry
            MyLog.Description = "ALFExportService has started successfully! Debug mode: debug=" & debug
            MyLog.CodeCode = "Export"
            MyLog.CountryCode = "AT"
            MyLog.Source = "AlfExportService on Start"
            MyLog.insert()

        Catch ex As Exception
            Me.EventLog.WriteEntry(ex.message, EventLogEntryType.Error)
            ExceptionInfo.Show(ex)
        Finally

        End Try

        Try
            Me.EventLog.WriteEntry("AlfExportService has started successfully")
        Catch ex As Exception
        Finally
            tmrAction.Start()
        End Try

    End Sub

    Private Sub tmrAction_Tick(ByVal source As Object, ByVal e As System.Timers.ElapsedEventArgs)

        tmrAction.Interval = 600000
        tmrAction.Stop()
        Try


            If checkTimeForExport() = True Or debug = True Then
                Wyeth.Alf.WyethImportHelper.ClearDir(MyCollection("ExportDir"))
                makeExport()

                If transferFiles() > 0 Then
                    tmrAction.Interval = 12000000 ' sleep for 3,3 h
                    Wyeth.Alf.WyethImport.ArchiveFilesExport(MyCollection("ExportDir"), MyCollection("APOExportArchiveDir"))
                End If

                Wyeth.Alf.WyethImportHelper.ClearDir(MyCollection("ExportDir"))

                MyLog.CountryCode = "AT"
                MyLog.Description = Me.Log
                MyLog.CodeCode = "EXPORT"
                MyLog.Source = "AlfExportService Export Log"
                MyLog.insert()
                _strLog = String.Empty

            End If
        Catch ex As Exception
            Me.EventLog.WriteEntry(ex.Message, EventLogEntryType.Error)
        Finally

            tmrAction.Start()
        End Try
    End Sub
    Private Function makeExport()

        MyCollection = Wyeth.Alf.WyethImportHelper.readAppVars()
        expView = Wyeth.Alf.WyethImportHelper.getExportReports()

        Select Case (Today().DayOfWeek)
            Case DayOfWeek.Monday
                expView.RowFilter = "rpt_export_intervall='" & DayOfWeek.Monday & "' OR rpt_export_intervall='-1'"
            Case DayOfWeek.Tuesday
                expView.RowFilter = "rpt_export_intervall='" & DayOfWeek.Tuesday & "' OR rpt_export_intervall='-1'"
            Case DayOfWeek.Wednesday
                expView.RowFilter = "rpt_export_intervall='" & DayOfWeek.Wednesday & "' OR rpt_export_intervall='-1'"
            Case DayOfWeek.Thursday
                expView.RowFilter = "rpt_export_intervall='" & DayOfWeek.Thursday & "' OR rpt_export_intervall='-1'"
            Case DayOfWeek.Friday
                expView.RowFilter = "rpt_export_intervall='" & DayOfWeek.Friday & "' OR rpt_export_intervall='-1'"
            Case DayOfWeek.Saturday
                expView.RowFilter = "rpt_export_intervall='" & DayOfWeek.Saturday & "' OR rpt_export_intervall='-1'"
            Case DayOfWeek.Sunday
                expView.RowFilter = "rpt_export_intervall='" & DayOfWeek.Sunday & "' OR rpt_export_intervall='-1'"
        End Select

        Me.EventLog.WriteEntry("ALF Export: Number of reports to be exported:" & expView.Count, EventLogEntryType.Information)
        If Settings.AlfServiceModeDebug Then

            MyLog.Description = "Normal ALFExportREports:" & expView.Count
            MyLog.CodeCode = "Export"
            MyLog.Source = "AlfExportService debug Info:"
            MyLog.insert()

            expView.RowFilter = ""

            MyLog.Description = "Normal ALFExportREports:" & expView.Count
            MyLog.CodeCode = "Export"
            MyLog.Source = "AlfExportService debug Info:"
            MyLog.insert()

        End If

        For Each dr As DataRowView In expView

            If debug = True Then Me.EventLog.WriteEntry("AlfExportService:  exporting" & Convert.ToString(dr.Item("rpt_queryName")))

            Dim MyExport As New Exporter
            MyExport.Delimiter = Convert.ToString(dr.Item("rpt_delimiter"))
            MyExport.QueryName = Convert.ToString(dr.Item("rpt_queryName"))
            MyExport.ExportFileName = Convert.ToString(dr.Item("rpt_expfilename"))
            MyExport.RemoteFTPDir = Convert.ToString(dr.Item("rpt_exportDironserver"))

            If (Convert.ToString(dr.Item("rpt_name")).ToUpper = "Gross Sales".ToUpper()) Then
                MyExport.StartDate = Today().AddDays(-1)
            Else
                MyExport.StartDate = Today()
            End If

            MyExport.Execute()
            Me.Log = MyExport.Log
        Next

        ' Changes were made due to Change Request 07-AT-0005 
        If (Today().DayOfWeek = DayOfWeek.Saturday) Then
            sendmail()
        End If

        expView.RowFilter = ""

    End Function
    Private Sub sendmail()

        Try

            Dim MyMail As New WyethJmail
        Dim sSubject As String = "ALF Export Log: " & Settings.getMachineName()
        Dim logMessage, sMessage, templatefilenamePath As String
        Dim lastIndex As Integer


        Dim MyTemplate As New Wyeth.Utilities.textTemplate

        templatefilenamePath = Settings.ALFApplicationRootPath & "emailTemplates/AlfExportServiceError.html"
        MyTemplate.filename = templatefilenamePath
        MyTemplate.addVariable("MESSAGE", Me.Log)
        sMessage = MyTemplate.returnString()


        logMessage = Me.Log



        ' Make log entry into db
        MyLog.CodeCode = "Export"
        MyLog.Description = logMessage
        MyLog.Source = "ALFExportService"
        MyLog.insert()
        Me.EventLog.WriteEntry("ALFExportService - sending mail")
        If Settings.isLiveServer = True Then
            MyMail.SendEMail(MyCollection("SMTPHost"), "ALFExport", MyCollection("AdminEmail"), sSubject, sMessage)
        Else
            MyMail.SendEMail(MyCollection("SMTPHost"), "ALFExport", MyCollection("APOEmail"), sSubject, sMessage)
        End If

         Catch  ex as Exception
            Me.EventLog.WriteEntry(ex.Message, EventLogEntryType.Error)

        End Try




    End Sub

    Private Function transferFiles() As Integer
        Dim MyWyethExport As New WyethExport
        Try
            Dim uploadCounter As Integer
            uploadCounter = MyWyethExport.ftpUploadFiles(False)
            Me.Log = Me.Log + MyWyethExport.Log
            Return uploadCounter
        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Me.Log = ex.Message
        Finally
            Me.Log = MyWyethExport.Log()
        End Try
    End Function
    Private Function checkTimeForExport() As Boolean
        Try
            Dim h As Integer = Hour(Now())
            Dim m As Integer = Minute(Now())

            Dim starttime As String() = CStr(MyCollection("FTPStartAPOExport")).Split(":")
            Dim starth As Integer = CInt(starttime(0))
            Dim startm As Integer = CInt(starttime(1))

            Dim stoptime As String() = CStr(MyCollection("FTPStopAPOExport")).Split(":")
            Dim stoph As Integer = CInt(stoptime(0))
            Dim stopm As Integer = CInt(stoptime(1))

            If (h >= starth) And (h <= stoph) And (m >= startm) Then
                If debug = True Then Me.EventLog.WriteEntry("AlfExportService:  its time for export")
                Return True
            End If

        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Me.EventLog.WriteEntry(ex.Message, EventLogEntryType.Error)
        End Try
    End Function
    Protected Overrides Sub OnStop()
        Try

            tmrAction.Stop()
            tmrAction = Nothing

            MyLog.Description = "ALFExportservice has stopped successfully!"
            MyLog.CodeCode = "Export"
            MyLog.Source = "AlfExportService on Stop"
            MyLog.insert()

        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Me.EventLog.WriteEntry(ex.Message, EventLogEntryType.Error)
        End Try

    End Sub
    Protected Overrides Sub OnPause()
        Try

            tmrAction.Stop()

            MyLog.Description = "ALFExportservice has paused successfully!"
            MyLog.CodeCode = "Export"
            MyLog.Source = "AlfExportService on Pause"
            MyLog.insert()

        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Me.EventLog.WriteEntry(ex.Message, EventLogEntryType.Error)
        End Try

    End Sub
    Protected Overrides Sub OnContinue()
        Try

            tmrAction.Start()

            MyLog.Description = "ALFExportservice has continued successfully!"
            MyLog.CodeCode = "Export"
            MyLog.Source = "AlfExportService on Continue"
            MyLog.insert()

        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Me.EventLog.WriteEntry(ex.Message, EventLogEntryType.Error)
        End Try

    End Sub
    Protected Overrides Sub OnShutDown()
        Try

            tmrAction.Stop()
            tmrAction = Nothing

            MyLog.Description = "ALFExportservice has ShutDown successfully!"
            MyLog.CodeCode = "Export"
            MyLog.Source = "AlfExportService on ShutDown"
            MyLog.insert()

        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Me.EventLog.WriteEntry(ex.Message, EventLogEntryType.Error)
        End Try
    End Sub
    Private _strLog As String
    Public Property Log() As String
        Get
            Return _strLog
        End Get
        Set(ByVal Value As String)
            _strLog = _strLog + Value + vbNewLine
        End Set
    End Property
End Class
