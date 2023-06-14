Imports Wyeth.Utilities

Public Class WebForm2
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label

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
        Try

        
        Dim fi() As System.IO.FileInfo = Wyeth.Utilities.FileHandling.DirListing("d:\")

        For Each f As System.IO.FileInfo In fi
            Me.Label1.Text = Me.Label1.Text + f.Name + "<br>"
            Next

        Catch ex As Exception
            Me.Label1.Text = ex.Message & ex.InnerException.ToString
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Try

        Dim MyCollection As New Hashtable
        Dim i As Integer = MyCollection.Count
     

        While MyCollection.Count = 0
            Try
                MyCollection = Wyeth.Alf.WyethImportHelper.readAppVars()


            Catch ex As Exception
                System.Threading.Thread.Sleep(600000)
            Finally

            End Try
        End While



        Dim h As Integer = Hour(Now())
        Dim m As Integer = Minute(Now())

        Dim starttime As String() = CStr(MyCollection("FTPSTARTCHECK")).Split(":")
        Dim starth As Integer = CInt(starttime(0))
        Dim startm As Integer = CInt(starttime(1))

        Dim stoptime As String() = CStr(MyCollection("FTPSTOPCHECK")).Split(":")
        Dim stoph As Integer = CInt(stoptime(0))
        Dim stopm As Integer = CInt(stoptime(1))

        Dim expView As New DataView
        expView = Wyeth.Alf.WyethImportHelper.getExportReports()

        Select Case (Today().DayOfWeek)
            Case DayOfWeek.Monday
                expView.RowFilter = "rpt_export_intervall='" & DayOfWeek.Monday & "' OR rpt_export_intervall='-1'"
            Case DayOfWeek.Tuesday
                expView.RowFilter = "rpt_export_intervall='" & DayOfWeek.Tuesday & "' OR rpt_export_intervall='-1'"
            Case DayOfWeek.Wednesday
                expView.RowFilter = "rpt_export_intervall='" & DayOfWeek.Wednesday & "' OR rpt_export_intervall='-1'"
            Case DayOfWeek.Thursday
                expView.RowFilter = "rpt_export_intervall='" & DayOfWeek.Thursday & "' OR rpt_export_intervall='-1'"
            Case DayOfWeek.Friday
                expView.RowFilter = "rpt_export_intervall='" & DayOfWeek.Friday & "' OR rpt_export_intervall='-1'"
            Case DayOfWeek.Saturday
                expView.RowFilter = "rpt_export_intervall='" & DayOfWeek.Saturday & "' OR rpt_export_intervall='-1'"
            Case DayOfWeek.Sunday
                expView.RowFilter = "rpt_export_intervall='" & DayOfWeek.Sunday & "' OR rpt_export_intervall='-1'"
        End Select




        For Each dr As DataRowView In expView
            Dim MyExport As New Exporter
            MyExport.Delimiter = Convert.ToString(dr.Item("rpt_delimiter"))
            MyExport.QueryName = Convert.ToString(dr.Item("rpt_queryName"))
            MyExport.ExportFileName = Convert.ToString(dr.Item("rpt_expfilename"))
            MyExport.RemoteFTPDir = Convert.ToString(dr.Item("rpt_exportDironserver"))
            MyExport.StartDate = Today()
            MyExport.Execute()

        Next
        expView.RowFilter = ""

        '   For i As Integer = 1 To 10
        'Dim mytest As New WyethImport
        '  mytest.MapDrive("ATFORTE01", "q8751436", True, False, "v:", "\\vwaeup02.vw.uk.pri.wyeth.com\root\atfiles")
        ' mytest.MapDrive("ATFORTE01", "q8751436", True, False, "w:", "\\10.245.23.17\atfiles")
        '  i = i + 1
        ' mytest.UnMapDrive("v:", True)

        '  Ne

        'Dim v_date As Date = Today()
        'Dim h As Integer = Hour(Now())
        'Try
        '    If Today.DayOfWeek <> DayOfWeek.Monday Then
        '        If (h >= CInt(MyCollection("FtpHostMuensterStartCheck"))) And (h < CInt(MyCollection("FtpHostMuensterStopCheck"))) Then
        '            '  Me.EventLog.WriteEntry("AlfService Muenster its time to check for new files on the Server")
        '            Dim x As Integer = 1

        '        End If
        '    End If

        'Catch ex As Exception
        '    ExceptionInfo.Show(ex)
        'End Try

        'If Settings.isDevelopmentServer Then
        '    Me.Label1.Text = "isDevekompent=true"
        'End If

        'If Settings.isLocalMachine Then
        '    Me.Label1.Text = Me.Label1.Text = "isLocal=true"
        'End If

        'Catch ex As Exception
        '    Me.Label1.Text = ex.Message

        'Finally

        'End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class
