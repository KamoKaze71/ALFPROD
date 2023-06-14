'Imports Oracle.DataAccess.Client
'Imports Wyeth.Utilities
'Imports Wyeth.Alf.CssStyles
'Imports C1.Web.C1WebGrid
'Imports System.IO
'Imports System.ComponentModel
'Imports Microsoft.VisualBasic
'Imports System
'Imports System.Reflection
'Imports System.Runtime.InteropServices
'Imports Wyeth.Utilities.FileHandling
'Imports System.Data


'Imports System.Data.OleDb

'Public Class testform2
'    Inherits System.Web.UI.Page

'#Region " Web Form Designer Generated Code "

'    'This call is required by the Web Form Designer.
'    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

'    End Sub
'    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
'    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
'    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
'    Protected WithEvents TextBox1 As System.Web.UI.WebControls.TextBox

'    'NOTE: The following placeholder declaration is required by the Web Form Designer.
'    'Do not delete or move it.
'    Private designerPlaceholderDeclaration As System.Object

'    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
'        'CODEGEN: This method call is required by the Web Form Designer
'        'Do not modify it using the code editor.
'        InitializeComponent()
'    End Sub

'#End Region


'    Dim MyService As New Wyeth.Utilities.ServiceControl

'    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load




'        'Dim sr As StreamReader = File.OpenText(sender)
'        Dim input As String = ""
'        'input = ""
'        'If File.Exists(sender) Then
'        '    input = sr.ReadLine()

'        '    While Not sr.ReadLine Is Nothing
'        '        input = input & sr.ReadLine() & vbNewLine
'        '    End While
'        '    sr.Close()


'        If input.IndexOf("failed") > 0 Or input.IndexOf("failed") > 0 Or input.IndexOf("nicht erfolgreich") > 0 Then
'            input = "<Font color=red>" & input & "</font>"
'        End If

'        '     m_strLog += input
'        '   End If

'        Response.Write(MyService.GetStatus)

'    End Sub






'    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
'        Dim myfile As String
'        Dim myfilehandlind As New Wyeth.Utilities.FileHandling
'        Dim myds As New DataSet

'        myfile = myfilehandlind.ReadFileToEnd("c:\WYETH_KD.DAT")

'        '   myds = openCSV("c:/", "WYETH_KD.XLS")

'        Dim ex As New Excel.Application
'        '   e()

'        Me.Response.Cache.SetCacheability(HttpCacheability.NoCache)
'        Me.Response.Clear()
'        Me.Response.Charset = ""
'        Me.Response.ContentEncoding = System.Text.Encoding.GetEncoding(1252)
'        'Me.repo()
'        Me.Response.ContentType = "application/vnd.ms-excel"
'        'Me.Response.ContentType = "application/NOTEPAD"
'        'Me.Response.AddHeader("Content-Disposition", "attachment")
'        Me.Response.AddHeader("Content-Disposition", "inline;filename=WebExcel.xls")

'        Me.Response.AddHeader("Content-Disposition", "attachment; filename=List.csv")
'        Me.Response.Write(myfile)

'    End Sub


'    Private Function openCSV(ByVal pathName As String, ByVal fileName As String)

'        pathName = pathName + fileName
'        Dim ExcelConnection As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathName + ";Extended Properties=Text;")
'        Dim ExcelCommand As New OleDbCommand("SELECT * FROM 2005-02-01_WYETH_KD", ExcelConnection)
'        Dim ExcelAdapter As New OleDbDataAdapter(ExcelCommand)
'        Dim ExcelDataSet As New DataSet
'        ExcelConnection.Open()
'        'dim exapp as New '
'        ExcelAdapter.Fill(ExcelDataSet)

'        ExcelConnection.Close()
'        Return ExcelDataSet

'    End Function

'    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

'    End Sub
'End Class
