Imports System.ServiceProcess
Imports Oracle.DataAccess.Client
Imports Wyeth.Alf
Imports Wyeth.Alf.WyethImport
Imports Wyeth.Utilities
Imports Wyeth.Utilities.DateHandling
Imports System
Imports System.IO
Imports System.Collections
Imports System.Drawing
Imports System.Globalization
Imports System.Web.Mail
'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
''' <summary><para>Provides DataAccess for T_product</para></summary>
Public Class AlfService
    Inherits System.ServiceProcess.ServiceBase

    Private d_day As Date = Today()
    Private tmrAction As System.Timers.Timer
    'Private tmrMuenster As System.Timers.Timer
    Private MyLog As New Log
    Private MyCollection As New Hashtable
    Private debug As Boolean = False   ' set true if you want to debug the service -> does not delete ftp files on server
    Private m_bol_ImportSuccessFull As Boolean = False
    Private makeCheck As Boolean = False
    Private RunOnce As Boolean = False
    Private dist_id_sanova, dist_id_pharmosan, dist_id_muenster As Integer


#Region " Vom Component Designer generierter Code "

    Public Sub New()
        MyBase.New()

        ' Dieser Aufruf wird vom Komponenten-Designer benötigt.
        InitializeComponent()

        ' Fügen Sie Initialisierungscode hinter dem InitializeComponent()-Aufruf ein

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

    ' Der Haupteinstiegspunkt für den Vorgang
    <MTAThread()> _
    Shared Sub Main()
        Dim ServicesToRun() As System.ServiceProcess.ServiceBase

        ' Innerhalb desselben Prozesses können mehrere NT-Dienste ausgeführt werden. Um einen
        ' weiteren Dienst zum Prozess hinzuzufügen, änderen Sie die folgende Zeile,
        ' um ein zweites Dienstprojekt zu erstellen. Z.B.,
        '
        '   ServicesToRun = New System.ServiceProcess.ServiceBase () {New Service1, New MySecondUserService}
        '
        ServicesToRun = New System.ServiceProcess.ServiceBase() {New AlfService}

        System.ServiceProcess.ServiceBase.Run(ServicesToRun)
    End Sub

    ' Für Komponenten-Designer erforderlich
    Private components As System.ComponentModel.IContainer

    ' HINWEIS: Die folgende Prozedur wird vom Komponenten-Designer benötigt.
    ' Sie kann mit dem Komponenten-Designer modifiziert werden. Verwenden Sie nicht
    ' den Code-Editor zur Bearbeitung.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        '
        'Service1
        '
        Me.ServiceName = "AlfService"

    End Sub

#End Region

    Protected Overrides Sub OnStart(ByVal args() As String)

        Try
            Me.AutoLog = True

            ''    tmrMuenster = New System.Timers.Timer
            tmrAction = New System.Timers.Timer

            AddHandler tmrAction.Elapsed, AddressOf tmrAction_Tick
            '' AddHandler tmrMuenster.Elapsed, AddressOf tmrMuenster_Tick

            debug = Settings.AlfServiceModeDebug

            Dim icount As Integer

            While MyCollection.Count < 1
                Try


                    MyCollection = Wyeth.Alf.WyethImportHelper.readAppVars()
                    Dim myImport As New WyethImport
                    dist_id_sanova = myImport.GetDistributorID("Sanova")
                    dist_id_pharmosan = myImport.GetDistributorID("Pharmosan")
                    '    dist_id_muenster = myImport.GetDistributorID("Munster")


                Catch ex As Exception
                    '   Me.EventLog.WriteEntry(ex.message + ex.StackTrace + ex.Source, EventLogEntryType.Error)
                    Me.EventLog.WriteEntry("Could not get DB Connection - trying again in 60 seconds", EventLogEntryType.Error)
                    System.Threading.Thread.Sleep(3000000) ' 5 min
                    icount = icount + 1
                    If icount > 5 Then
                        Dim sc As New ServiceController("Alfservice")
                        sc.Stop()
                    End If
                Finally
                   

                End Try

            End While


            '' Make a Log Entry
            MyLog.Description = "ALFservice has started successfully! Debug mode: debug=" & debug
            MyLog.CodeCode = "FTP"
            MyLog.CountryCode = "AT"
            MyLog.Source = "AlfService on Start"
            MyLog.insert()

        Catch ex As Exception
            Me.EventLog.WriteEntry(ex.message, EventLogEntryType.Error)
            ExceptionInfo.Show(ex)
        Finally

        End Try

        Try

            Me.EventLog.WriteEntry("Alfservice has started successfully")
            Me.EventLog.WriteEntry("Debug mode: debug=" & debug)

        Catch ex As Exception

        Finally
            tmrAction.Interval = 300000                  '5 Min
            '  tmrMuenster.Interval = 300000

            ' tmrMuenster.Start()
            tmrAction.Start()
        End Try

    End Sub
    Protected Overrides Sub OnStop()
        Try

            tmrAction.Stop()
            tmrAction = Nothing

            '   tmrMuenster.Stop()
            '  tmrMuenster = Nothing

            MyLog.Description = "ALFservice has stopped successfully!"
            MyLog.CodeCode = "FTP"
            MyLog.Source = "AlfService on Stop"
            MyLog.insert()

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        End Try

    End Sub
    Protected Overrides Sub OnPause()
        Try

            tmrAction.Stop()
            '   tmrMuenster.Stop()

            MyLog.Description = "ALFservice has paused successfully!"
            MyLog.CodeCode = "FTP"
            MyLog.Source = "AlfService on Pause"
            MyLog.insert()

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        End Try

    End Sub
    Protected Overrides Sub OnContinue()
        Try

            tmrAction.Start()
            ' tmrMuenster.Start()

            MyLog.Description = "ALFservice has continued successfully!"
            MyLog.CodeCode = "FTP"
            MyLog.Source = "AlfService on Continue"
            MyLog.insert()

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        End Try

    End Sub
    Protected Overrides Sub OnShutDown()
        Try

            tmrAction.Stop()
            tmrAction = Nothing

            'tmrMuenster.Stop()
            'tmrMuenster = Nothing

            MyLog.Description = "ALFservice has ShutDown successfully!"
            MyLog.CodeCode = "FTP"
            MyLog.Source = "AlfService on ShutDown"
            MyLog.insert()

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        End Try
    End Sub

    'Private Sub tmrMuenster_Tick(ByVal source As Object, ByVal e As System.Timers.ElapsedEventArgs)
    '    Try
    '        Dim x As Integer
    '        tmrMuenster.Stop()
    '        tmrMuenster.Interval = 300000

    '        If checkTimeMuenster() Then
    '            If debug = True Then Me.EventLog.WriteEntry("AlfService: Starting Münster Import procedure ....")
    '            Dim MyImport As New WyethImport
    '            x = MyImport.ImportMuensterFiles()
    '            If debug = True Then Me.EventLog.WriteEntry("AlfService has downloaded: " & x & " Münster files")

    '            If x > 0 Then
    '                tmrMuenster.Interval = 61200000 '17 h
    '            End If

    '            MyLog.CountryCode = "MUE"
    '            MyLog.CodeCode = "FTP"
    '            MyLog.Source = "ALFService Münster Import Progress"
    '            MyLog.Description = MyImport.strLog()
    '            MyLog.insert()
    '        End If

    '    Catch ex As Exception
    '        Me.EventLog.WriteEntry(ex.Message.ToString, EventLogEntryType.Error)
    '        ExceptionInfo.Show(ex)
    '    Finally
    '        d_day = Today()
    '        RunOnce = False
    '        tmrMuenster.Start()

    '    End Try
    'End Sub
    Private Sub tmrAction_Tick(ByVal source As Object, ByVal e As System.Timers.ElapsedEventArgs)
        Dim MyImport As New WyethImport
        Try
            Dim x As Integer = 0

            tmrAction.Stop()
            tmrAction.Interval = 300000

            If CheckTime() = True Then

                If debug = True Then Me.EventLog.WriteEntry("AlfService: Starting Sanova Import procedure ....")
                x = MyImport.GetFtpFilesService()
                If debug = True Then Me.EventLog.WriteEntry("AlfService has downloaded: " & x & " Sanova files")
                If x >= 3 Then
                    tmrAction.Interval = 61200000 '17 h
                    makeCheck = False
                Else
                    makeCheck = True
                End If
                GC.Collect()
                GC.WaitForPendingFinalizers()
            End If

            If checkTimeForNoImport() = True Or debug = True Then
                'log entry for debugging ....
                If debug = True Then
                    Me.EventLog.WriteEntry("Alfservice: Its Time to check if the import was correct")
                    MyLog.CodeCode = "AppEx"
                    MyLog.Description = "checkTimeForNoImport() = True"
                    MyLog.Source = "ALFService"
                    MyLog.insert()
                End If

                CheckForNoImport(MyImport.GetDistributorID("Sanova"))
                makeCheck = False
            End If

        Catch ex As Exception
            Me.EventLog.WriteEntry(ex.Message.ToString, EventLogEntryType.Error)
            ExceptionInfo.Show(ex)
        Finally
            d_day = Today()
            tmrAction.Start()
            MyImport = Nothing
        End Try

    End Sub
  
    Private Function checkTimeForNoImport() As Boolean
        Dim v_date As Date = Today()

        Dim h As Integer = Hour(Now())
        Dim m As Integer = Minute(Now())

        Dim starttime As String() = CStr(MyCollection("FTPSTARTCHECK")).Split(":")
        Dim starth As Integer = CInt(starttime(0))
        Dim startm As Integer = CInt(starttime(1))

        Dim stoptime As String() = CStr(MyCollection("FTPSTOPCHECK")).Split(":")
        Dim stoph As Integer = CInt(stoptime(0))
        Dim stopm As Integer = CInt(stoptime(1))

        Dim MyStatus As New AlfStatus
        Try
            If ((MyStatus.IsHoliday(v_date.AddDays(-1), "AT") = False) And (h >= starth) And (h > stoph) And makeCheck = True) And (Today.DayOfWeek <> DayOfWeek.Saturday) Then
                Return True
            Else ' Yesterday was either a holiday or time is not between startcheck and stopcheck in t_application_settings
                Return False
            End If

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally

            MyStatus = Nothing
            h = Nothing
            v_date = Nothing
        End Try
    End Function
 
    Private Sub CheckForNoImport(ByVal dist_id)
        Try
            Dim myImportStatus As New AlfStatus
            Dim myNotImportedTransmissions As New DataView


            myNotImportedTransmissions = myImportStatus.NotImportedTransmissions()

            If myNotImportedTransmissions.Count > 0 Then

                tmrAction.Interval = 61200000 ' sleep for 17 h

                Dim MyMail As New WyethJmail
                Dim sSubject As String = "ALF ImportError on Server:" & Settings.getMachineName()
                Dim logMessage, sMessage, templatefilenamePath As String
                Dim lastIndex As Integer


                Dim MyTemplate As New Wyeth.Utilities.textTemplate

                templatefilenamePath = Settings.ALFApplicationRootPath & "emailTemplates/AlfserviceImportError.html"
                MyTemplate.filename = templatefilenamePath
                sMessage = MyTemplate.returnString()


                logMessage = "ALF could not import Sanova Data!" & vbNewLine
                logMessage += "Please Check ALF Logs In The Admin Section for Import Errors, Ftp Errors and Application Exceptions." & vbNewLine
                logMessage += "Make a Import under Admin Import Distributor or Contact Sanova for misssing FTP Files" & vbNewLine


                ' Make log entry into db
                MyLog.CodeCode = "ImportErr"
                MyLog.Description = logMessage
                MyLog.Source = "ALFService"
                MyLog.insert()
                Me.EventLog.WriteEntry("Impport Error - sending mail")
                If Settings.isLiveServer = True Then
                    MyMail.SendEMail(MyCollection("SMTPHost"), "ALFService", MyCollection("AdminEmail"), sSubject, sMessage)
                Else
                    MyMail.SendEMail(MyCollection("SMTPHost"), "ALFService", "kamitzp@wyeth.com", sSubject, sMessage)
                End If

            ElseIf myNotImportedTransmissions.Count = 0 Then

            End If


        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            tmrAction.Interval = 60000
            makeCheck = False
        End Try

    End Sub
    Private Function CheckTime() As Boolean
        Dim v_date As Date = Today()

        Dim h As Integer = Hour(Now())
        Dim m As Integer = Minute(Now())

        Dim starttime As String() = CStr(MyCollection("FTPSTARTCHECK")).Split(":")
        Dim starth As Integer = CInt(starttime(0))
        Dim startm As Integer = CInt(starttime(1))

        Dim stoptime As String() = CStr(MyCollection("FTPSTOPCHECK")).Split(":")
        Dim stoph As Integer = CInt(stoptime(0))
        Dim stopm As Integer = CInt(stoptime(1))

        Dim MyStatus As New AlfStatus

        Try
            If (MyStatus.IsHoliday(v_date.AddDays(-1), "AT") = False) And ((h >= starth) And (h <= stoph)) And (m >= startm) Then
                Me.EventLog.WriteEntry("AlfService its time to check for new files on the FtpServer")
                Return True

            ElseIf Today().DayOfWeek = DayOfWeek.Monday And MyStatus.IsHoliday(Today().AddDays(-3), "AT") = False And ((h >= starth) And (h <= stoph) And (m >= startm)) Then                 ' wenn Montag ist und der Freitag kein Feiertag war - weil Sanova oft erst am Montag in der Früh Daten liefert
                Dim myImportStatus As New AlfStatus
                Dim myNotImportedTransmissions As New DataView
                myNotImportedTransmissions = myImportStatus.NotImportedTransmissions()

                If myNotImportedTransmissions.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyStatus = Nothing
            h = Nothing
            v_date = Nothing
        End Try
    End Function

    'Private Function checkTimeMuenster() As Boolean

    '    Dim h As Integer = Hour(Now())
    '    Dim m As Integer = Minute(Now())

    '    Dim starttime As String() = CStr(MyCollection("FtpHostMuensterStartCheck")).Split(":")
    '    Dim starth As Integer = CInt(starttime(0))
    '    Dim startm As Integer = CInt(starttime(1))

    '    Dim stoptime As String() = CStr(MyCollection("FtpHostMuensterStopCheck")).Split(":")
    '    Dim stoph As Integer = CInt(stoptime(0))
    '    Dim stopm As Integer = CInt(stoptime(1))

    '    Try
    '        If Today.DayOfWeek <> DayOfWeek.Monday Then
    '            If ((h >= starth) And (h <= stoph)) And ((m >= startm)) Then
    '                If debug = True Then
    '                    Me.EventLog.WriteEntry("AlfService Muenster its time to check for new files on the Server")
    '                End If
    '                Return True
    '            End If
    '        End If
    '    Catch ex As Exception
    '        ExceptionInfo.Show(ex)
    '    End Try

    'End Function
    Public Property ImportSuccessFull() As Boolean
        Get
            Return m_bol_ImportSuccessFull
        End Get
        Set(ByVal Value As Boolean)
            m_bol_ImportSuccessFull = Value
        End Set
    End Property
End Class