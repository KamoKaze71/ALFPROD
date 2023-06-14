Imports Wyeth.Alf.DataAccessBaseClass
Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities

Public Class TestLiveDatenAbgleich
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents btn_Import As System.Web.UI.WebControls.Button
    Protected WithEvents auto_panel As System.Web.UI.WebControls.Panel
    Protected WithEvents txtProgress As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblOut As System.Web.UI.WebControls.Label
    Protected WithEvents btnEditTables As System.Web.UI.WebControls.Button
    Protected WithEvents Literal1 As System.Web.UI.WebControls.Literal

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim strStep As String = "none"
    Protected WithEvents iidx As System.Web.UI.WebControls.TextBox
    Dim myJs As New JSPopUp(Me)

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Page.IsPostBack = True Then
            strStep = Request.Form("txtProgress")
            '  iidx = Request.Form("txtiidx")
            importProgress(strStep)

        Else
            myJs.ConfirmMessage = "This will syncronize the data between Liveserver and Developmentserver. Are you sure you want to do this?"
            myJs.AddGetConfirm(btn_Import)

            myJs.PageURL = "TestLiveDatenAbgleichTables.aspx"
            myJs.Title = " Edit Tables"
            myJs.Height = "400"
            myJs.Width = "500"
            myJs.ScrollBars = True
            myJs.AddPopupToControl(btnEditTables)
           

            If Settings.isLiveServer = True Then
                lblOut.Text = "<font color=red>Syncronize Data is only possible on the Development Server</font>"
                btn_Import.Enabled = False
            End If
        End If

    End Sub


    Private Sub importProgress(ByVal steps As String)

        Dim tmp As String

        Select Case steps

            Case "step1"

                Dim MyCmd As New OracleCommand
                Dim MyConn As New MyConnection
                Dim MyReader As OracleDataReader
                MyCmd.CommandText = "PKG_APPLICATION.GetDataSynchronizeScript"
                MyCmd.CommandType = CommandType.StoredProcedure
                MyCmd.Parameters.Clear()
                MyCmd.Parameters.Add("statement", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                MyCmd.Connection = MyConn.Open()
                MyReader = MyCmd.ExecuteReader()

                While MyReader.Read
                    Try
                        MyCmd.CommandText = MyReader("stmt")
                        MyCmd.CommandType = CommandType.Text
                        tmp = CommandType.Text
                        MyCmd.ExecuteNonQuery()
                        lblOut.Text = lblOut.Text & "Executing  " & MyCmd.CommandText & "..... OK<BR>"
                    Catch ex As Exception
                        lblOut.Text = lblOut.Text & "Error executing  " & tmp & "..... OK<BR><Font color=red>" & ex.Message.ToString
                    End Try
                End While
                Me.txtProgress.Text = "none"



        End Select



    End Sub

    Private Sub btn_Import_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Import.Click
        Me.txtProgress.Text = "step1"
        Me.lblOut.Text = " Now starting Data Syncronize Script... <BR>This will take about 5 minutes ....<BR>"
    End Sub
End Class
