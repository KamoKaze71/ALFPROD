Imports System.Diagnostics
Imports System.Threading

Public Class CommandLauncher

    Private _OutputThread As Thread
    Private _ErrorThread As Thread
    ' Private _TextToAdd As String
    Private _myProcess As Process

    Public Sub LaunchCommand(ByVal strExceFilename As String, ByVal strExecArguments As String, ByVal strWorkingDir As String, ByVal windowStyle As ProcessWindowStyle)
        Try
            _myProcess = New Process
         

            With _myProcess
                Dim i As Integer
                .StartInfo.WindowStyle = windowStyle
                .StartInfo.ErrorDialog = True
                .StartInfo.UseShellExecute = False
                .StartInfo.RedirectStandardOutput = True
                .StartInfo.RedirectStandardError = True
                .StartInfo.CreateNoWindow = False
                .StartInfo.FileName = strExceFilename
                .StartInfo.Arguments = strExecArguments
                .StartInfo.WorkingDirectory = strWorkingDir
                .Start()
                _strProcessName = .ProcessName()
            End With

            ' Ändern der Streams, sodass wir bei Änderungen etwas mitbekommen.
            Dim ts1 As ThreadStart = New ThreadStart(AddressOf StreamOutput)
            _OutputThread = New Thread(ts1)
            _OutputThread.IsBackground = True
            _OutputThread.Start()

            Dim ts2 As ThreadStart = New ThreadStart(AddressOf StreamError)
            _ErrorThread = New Thread(ts2)
            _ErrorThread.IsBackground = True
            _ErrorThread.Start()

        Catch ex As Exception
            _strErr = _strErr & ex.Message.ToString
            Throw ex
        End Try
    End Sub

    Private Sub StreamOutput()
        Try
            _strOut = _myProcess.StandardOutput.ReadToEnd
        Catch
        End Try
    End Sub

    ''' <summary>
    '''   Liest vom Fehlerstream und gibt die gelesenen Informationen aus.
    ''' </summary>
    Private Sub StreamError()
        Try
            _strErr = _myProcess.StandardError.ReadToEnd
        Catch
        End Try
    End Sub


    Private _strErr As String
    Private _strOut As String
    Private _strProcessName As String

    Public ReadOnly Property strErr() As String
        Get
            Return _strErr
        End Get
    End Property
    Public ReadOnly Property strOut() As String
        Get
            Return _strOut
        End Get
    End Property
    Public ReadOnly Property strProcessName() As String
        Get
            Return _strProcessName
        End Get
    End Property
End Class
