Imports System
Imports System.ServiceProcess

Public Class ServiceControl

    Public arrServices() As ServiceController
    Public iIndex As Integer = -1

    'An array to store service status
    Public arrServiceStatus() As String = {"Continue Pending", "Paused", "Pause Pending", "Running", "Start Pending", "Stopped", "Stop Pending"}



    Sub New(Optional ByVal ServiceDisplayName As String = "AlfService", Optional ByVal MachineName As String = "")
        Dim iCounter As Integer

        If MachineName = "" Then
            MachineName = Wyeth.Utilities.Settings.getMachineName()
        End If
        arrServices = ServiceController.GetServices(MachineName)

        For iCounter = 0 To arrServices.Length - 1

            If arrServices(iCounter).DisplayName.ToUpper = ServiceDisplayName.ToUpper Then
                iIndex = iCounter
                Exit Sub
            End If

        Next

    End Sub

    Public Function GetStatus() As String
        If iIndex = -1 Then
            Return "Service Not installed"
        Else

            Return arrServices(iIndex).Status.ToString
        End If
    End Function

    'Public Function GetDetailedStatus() As String
    '    Return arrServiceStatus(arrServices(iIndex).
    'End Function


    Public Function StartService() As String



        arrServices(iIndex).Start()
    End Function


    Public Function StopService() As String
        arrServices(iIndex).Stop()
    End Function


    Public Function ReStartService() As String
        arrServices(iIndex).Stop()
        arrServices(iIndex).Start()
    End Function


    Public Function PauseService() As String
        arrServices(iIndex).Pause()
    End Function


    Public Function ContinueService() As String
        arrServices(iIndex).Continue()
    End Function


End Class
