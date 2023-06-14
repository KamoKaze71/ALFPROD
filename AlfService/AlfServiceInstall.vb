Imports System.ComponentModel
Imports System.Configuration.Install
Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities
Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

<RunInstaller(True)> Public Class ProjectInstaller
	Inherits System.Configuration.Install.Installer

    
#Region " Vom Component Designer generierter Code "

    Dim myCollection As New Hashtable

	Public Sub New()

        
        

        ' Dieser Aufruf ist für den Komponenten-Designer erforderlich.
        InitializeComponent()
        myCollection = Wyeth.Alf.WyethImportHelper.readAppVars()

        ' Initialisierungen nach dem Aufruf InitializeComponent() hinzufügen

    End Sub

    'Installer überschreibt den Löschvorgang zum Bereinigen der Komponentenliste.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    ' Für Komponenten-Designer erforderlich
    Private components As System.ComponentModel.IContainer

    'HINWEIS: Die folgende Prozedur ist für den Komponenten-Designer erforderlich
    'Sie kann mit dem Komponenten-Designer modifiziert werden.
    'Verwenden Sie nicht den Code-Editor zur Bearbeitung.
    Friend WithEvents ServiceProcessInstallerALF As System.ServiceProcess.ServiceProcessInstaller
    Friend WithEvents ServiceInstallerALF As System.ServiceProcess.ServiceInstaller
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        Me.ServiceProcessInstallerALF = New System.ServiceProcess.ServiceProcessInstaller
        Me.ServiceInstallerALF = New System.ServiceProcess.ServiceInstaller
        '
        'ServiceProcessInstallerALF
        '
        'Me.ServiceProcessInstallerALF.Account = System.ServiceProcess.ServiceAccount.LocalService.LocalSystem
        'Me.ServiceProcessInstallerALF.Account = ServiceProcess.ServiceAccount.User

        If Settings.isLiveServer = True Then

            Me.ServiceProcessInstallerALF.Account = ServiceProcess.ServiceAccount.User
            Me.ServiceProcessInstallerALF.Password = myCollection("AlfServiceUserPassword")
            Me.ServiceProcessInstallerALF.Username = Settings.getMachineName() & "\" & myCollection("AlfServiceUserName")

        ElseIf Settings.isDevelopmentServer = True Then

            Me.ServiceProcessInstallerALF.Account = ServiceProcess.ServiceAccount.User
            Me.ServiceProcessInstallerALF.Password = myCollection("AlfServiceUserPassword")
            Me.ServiceProcessInstallerALF.Username = Settings.getMachineName() & "\" & myCollection("AlfServiceUserName")
        Else
            Me.ServiceProcessInstallerALF.Account = ServiceProcess.ServiceAccount.User
            Me.ServiceProcessInstallerALF.Password = "kp12345"
            Me.ServiceProcessInstallerALF.Username = Settings.getMachineName() & "\kamitzp"

        End If

        '
        Me.ServiceInstallerALF.DisplayName = "ALFService"
        Me.ServiceInstallerALF.ServiceName = "ALFService"
        Me.ServiceInstallerALF.StartType = ServiceProcess.ServiceStartMode.Automatic

        '
        'ProjectInstaller
        '
        Me.Installers.AddRange(New System.Configuration.Install.Installer() {Me.ServiceProcessInstallerALF, Me.ServiceInstallerALF})

    End Sub


#End Region

End Class

