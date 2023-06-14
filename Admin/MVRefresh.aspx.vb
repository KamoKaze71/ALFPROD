Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities




Public Class MVRefresh
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btn_mv_refresh As System.Web.UI.WebControls.Button
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents lblOut As System.Web.UI.WebControls.Label
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
    End Sub

    Private Sub btn_mv_refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_mv_refresh.Click
        Dim MyCmd As New OracleCommand
        Dim MyConn As New MyConnection
        Dim val As Object

        Try
            MyCmd.Parameters.Clear()
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_IMPORT_SANOVA.F_REFRESH_MVS"
            MyCmd.Parameters.Add("v_result", OracleDbType.Varchar2, 2000, val, ParameterDirection.ReturnValue)
            MyCmd.Connection = MyConn.Open
            val = MyCmd.ExecuteScalar()
            val = MyCmd.Parameters(0).Value
            lblOut.Text = lblOut.Text & val.ToString & vbCrLf


        Catch ex As Exception
            lblOut.Text = ex.Message
        Finally
            MyConn.Close()

        End Try
    End Sub
End Class
