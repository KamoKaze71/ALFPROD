Imports Wyeth.Utilities
Imports System.IO

Public Class WyethExport
    Private MyCollection As Hashtable
    Private myExpReports As DataView
    Private myLog As New Wyeth.Utilities.Log


    Public Sub New()
        MyCollection = Wyeth.Alf.WyethImportHelper.readAppVars()
        myExpReports = Wyeth.Alf.WyethImportHelper.getExportReports()
    End Sub
    Public Function ftpUploadFiles(ByVal bmanual As Boolean) As Integer

        Dim FtpSession As New FtpConnection
        Dim countUpload As Integer = 0
        Dim debug As Boolean = Settings.AlfServiceModeDebug

        Try

            Dim MyFiles As FileInfo() = Wyeth.Utilities.FileHandling.DirListing(MyCollection("ExportDir"))

            If MyFiles.Length > 0 Then

                FtpSession.Hostname = MyCollection("FTPHostAPOExport")
                FtpSession.Port = 21
                FtpSession.Passive = False
                FtpSession.Username = MyCollection("FTPHostAPOExportUser")
                FtpSession.Password = MyCollection("FTPHostAPOExportPass")

                FtpSession.Connect()

                If FtpSession.IsConnected = True Then

                    For Each fi As FileInfo In MyFiles
                        countUpload = countUpload + 1
                        myExpReports.RowFilter = "rpt_expfilename='" & fi.Name & "'"
                        FtpSession.ChangeWorkDir(myExpReports.Item(0).Item("rpt_exportdironserver"))

                        If FtpSession.UploadFile(fi.FullName, fi.Name) = True Then

                            myLog.CodeCode = "Export"
                            myLog.CountryCode = "AT"
                            myLog.Description = fi.FullName.ToString()
                            If bmanual = True Then
                                myLog.Source = "ExportFile_Manual:" & Convert.ToString(fi.FullName)
                            ElseIf bmanual = False Then
                                myLog.Source = "ExportFile:" & Convert.ToString(fi.FullName)
                            End If

                            myLog.insert()

                            FtpSession.UploadFile("", fi.Name.Remove(fi.Name.Length - 3, 3) + "trg")
                            countUpload = countUpload + 1
                            FileHandling.FileMove(fi.FullName, MyCollection("APOExportArchiveDir") + fi.Name, True)

                            ' m_strLog += FtpSession.strLog & vbNewLine
                            _strLog += "Uploading " & fi.FullName & " -> " & MyCollection("FTPHostAPOExport") & (myExpReports.Item(0).Item("rpt_exportdironserver")) & fi.Name & vbNewLine
                            _strLog += "... Ok!" & vbNewLine & vbNewLine
                        Else
                            ' m_strLog += FtpSession.strLog & vbNewLine
                            _strLog += "<Font color=red> Error while Uploading " & fi.FullName & " -> " & MyCollection("SanovaImportFilePath") & fi.Name & "</font>" & vbNewLine
                            _strLog += vbNewLine
                        End If

                    Next
                End If
            End If 'end  files

            Return countUpload

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            FtpSession.Disconnect()
            Me.Log = vbNewLine & vbNewLine & FtpSession.strLog & vbNewLine & vbNewLine
        End Try

    End Function
    Private _strLog As String

    Public Sub getNumberExpectedExports()


    End Sub


    Public Property Log() As String
        Get
            Return _strLog
        End Get
        Set(ByVal Value As String)
            _strLog = _strLog & Value
        End Set
    End Property

End Class
