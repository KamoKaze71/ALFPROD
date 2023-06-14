Imports Wyeth.Utilities
Imports System.Threading

Public Class SerViceControll
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MyButton As System.Web.UI.WebControls.Button
    Protected WithEvents MyLabel As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim strStatus As String
    Dim myService As New ServiceControl("DailySales")

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckStatus()
        'Put user code to initialize the page here
    End Sub

    Private Sub CheckStatus()
        strStatus = myService.GetStatus.ToUpper

        MyLabel.Text = strStatus
        If strStatus = "STOPPED" Then

            MyLabel.BackColor = Color.Red
            MyButton.Text = "Start Service"

        ElseIf strStatus = "RUNNING" Then

            MyLabel.BackColor = Color.Green
            MyButton.Text = "Stop Service"

        Else
            MyButton.Enabled = False
        End If
    End Sub


    Private Sub MyButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyButton.Click


        CheckStatus()
        If strStatus = "STOPPED" Then
            myService.StartService()
        ElseIf strStatus = "RUNNING" Then
            myService.StopService()
        End If

        CheckStatus()
    End Sub
End Class

