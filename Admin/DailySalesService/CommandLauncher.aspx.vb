Imports Wyeth.Utilities.ServiceControl
Imports System
Imports System.IO
Imports System.Diagnostics
Imports wyeth.Utilities
Imports Wyeth.Utilities.Settings

Public Class WebForm
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MyLabel As System.Web.UI.WebControls.Label
    Protected WithEvents MyButton As System.Web.UI.WebControls.Button
    Protected WithEvents lblOut As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private strProcessName As String = "DailySales"
    Private strPathToExe As String
    Private myCommand As New CommandLauncher
    Private myJs As New JSPopUp(Me)

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        strPathToExe = """" & PathToDailySalesExe & """"
        Me.lblOut.Text = ""
        If Page.IsPostBack = True Then
            Me.lblOut.Text = ""
        Else
            myJs.ConfirmMessage = "This will restart DailySales.Exe and import DailySales Data into Forte! Are you sure you want to do that?"
            myJs.AddGetConfirm(MyButton)
            checkForStatus()
        End If
    End Sub

    Private Sub checkForStatus()
        Try
            Dim localAll As Process() = Process.GetProcesses()

            For Each myproc As Process In localAll
                Try
                    If myproc.ProcessName.ToLower = strProcessName.ToLower Then
                        Me.MyLabel.Text = "DailySales.exe is running"
                        Me.MyLabel.BackColor = Color.Green
                        Exit For
                    Else
                        Me.MyLabel.Text = "DailySales.exe is not running"
                        Me.MyLabel.BackColor = Color.Red
                    End If
                Catch ex As Exception
                End Try
            Next
        Catch ex As Exception
        End Try
    End Sub
    Private Sub MyButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyButton.Click
        Dim localAll As Process() = Process.GetProcessesByName(strProcessName)
       
        For Each myproc As Process In localAll
            Try
                If myproc.ProcessName.ToLower = strProcessName.ToLower Then
                    myproc.Kill()
                End If
                Me.lblOut.Text = "Successfully killed " & strProcessName & " Process <BR>"
            Catch ex As Exception
                Me.lblOut.Text = "Error while Killing " & strProcessName & ":" & ex.Message.ToString & "<BR>"
            End Try
        Next
        Try
            myCommand.LaunchCommand(strPathToExe, "", "", ProcessWindowStyle.Hidden)
            Me.lblOut.Text = Me.lblOut.Text & "starting Process..." & strPathToExe & "<BR>" & myCommand.strErr & "<BR>" & myCommand.strErr
        
        Catch ex As Exception
            Me.lblOut.Text = Me.lblOut.Text & "<bR>Path To DailySales.exe: " & strPathToExe
            Me.lblOut.Text = Me.lblOut.Text & "<br>" & ex.Message.ToString
        End Try
        checkForStatus()
    End Sub

End Class
