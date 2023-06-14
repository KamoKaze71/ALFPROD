Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities
Imports Wyeth.Utilities.Helper
Imports System.Data



Public Class testform
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents C1WebGrid1 As C1.Web.C1WebGrid.C1WebGrid

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

        'singleton.getInstance.increase()
        'Response.Write(singleton.getInstance.getValue())


        Dim MyDataview As New DataView
        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyReader As OracleDataReader

        Dim strsql As String
        ' Dim conn As New MyConnection

        Dim conn As New OracleConnection
        conn.ConnectionString = CONNECTION_STRING_TEST_SERVER
        conn.Open()

        Dim MyCmd2 As New OracleCommand
        MyCmd2.CommandType = CommandType.Text
        MyCmd2.CommandText = "ALTER SESSION SET NLS_LANGUAGE='AMERICAN'"
        MyCmd2.Connection = conn
        MyCmd2.ExecuteNonQuery()
        MyCmd2.CommandText = "ALTER SESSION SET NLS_TERRITORY='AMERICA'"
        MyCmd2.ExecuteNonQuery()
        MyCmd2.CommandText = "ALTER SESSION SET NLS_DATE_FORMAT='yyyy/mm/dd'"
        MyCmd2.ExecuteNonQuery()
        MyCmd2.CommandText = "ALTER SESSION SET NLS_NUMERIC_CHARACTERS='.,'"
        MyCmd2.ExecuteNonQuery()


        'Dim sessionGlob As OracleGlobalization
        'sessionGlob = conn.GetSessionInfo()
        'sessionGlob.Territory = "AMERICA"
        'sessionGlob.Language = "AMERICAN"
        'sessionGlob.DateFormat = "yyyy/mm/dd"
        'sessionGlob.NumericCharacters = ".,"
        'conn.SetSessionInfo(sessionGlob)
        Dim day As String

        ' day = Today().DayOfWeek.Saturday


        'Dim msrs As New ms
        MyCmd2.CommandText = "select * from t_oarsch"
        MyCmd2.CommandType = CommandType.Text
        MyAdapter.SelectCommand = MyCmd2
        MyAdapter.Fill(MyDs, "DailySales")
        MyDataview = MyDs.Tables("DailySales").DefaultView
        MyAdapter.Dispose()
        MyGrid.DataSource = MyDataview
        MyGrid.DataBind()


        MyReader = MyCmd2.ExecuteReader()

        While MyReader.Read

            Label1.Text = MyReader(0).ToString()
            Label1.Text = Label1.Text & MyReader(1).ToString()

        End While


    End Sub

End Class
'Public Class singleton
'    Private Shared count As counter

'    Private Sub New()
'    End Sub

'    Public Shared Function getInstance() As counter
'        If count Is Nothing Then
'            count = New counter
'        End If
'        Return count
'    End Function

'    Public Shared Sub reload()
'        count = Nothing
'        count = New counter
'    End Sub
'End Class

'Public Class counter
'    Private count As Integer

'    Sub New()
'        count = 0
'    End Sub

'    Public Sub increase()
'        count += 1
'    End Sub

'    Public Function getValue() As Integer
'        Return count
'    End Function
'End Class
