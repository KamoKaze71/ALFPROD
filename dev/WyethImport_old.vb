Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities
Imports Wyeth.Utilities.DateHandling
Imports Wyeth.Utilities.FileHandling
Imports System.IO
Imports Wyeth.Utilities.FtpConnection
Imports Wyeth.Utilities.Log
Imports Wyeth.Utilities.Helper
Imports System.Collections
Imports System.Data

Public Class WyethImport_old

    Dim myLog As New Log
    Private MyCollection As New Hashtable
    Private m_strLog As String
    Private sqlldr_err As String
    Private sqlldr_out As String


    Public Sub New()
        readAppVars()
    End Sub
    Public Function ImportForteAll(Optional ByVal connstr As String = "") As String

        Dim MyConn As New MyConnection
        Dim mycmd As New OracleCommand
        Dim ret_val As String
        Dim val As String
        Try
            If connstr = "" Then
            Else
                MyConn.ConnectionString = connstr
            End If

            mycmd.CommandType = CommandType.StoredProcedure
            mycmd.Connection = MyConn.Open

            mycmd.Parameters.Clear()
            mycmd.Parameters.Add("v_result", OracleDbType.Varchar2, 2000, val, ParameterDirection.ReturnValue)
            mycmd.CommandText = "pkg_import_forte.F_Budget"
            val = mycmd.ExecuteScalar()
            val = Convert.ToString(mycmd.Parameters(0).Value)
            ret_val = ret_val & vbNewLine & val
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        End Try

        Try
            mycmd.Parameters.Clear()
            mycmd.Parameters.Add("v_result", OracleDbType.Varchar2, 2000, val, ParameterDirection.ReturnValue)
            mycmd.CommandText = "pkg_import_forte.F_COGS"
            val = mycmd.ExecuteScalar()
            val = Convert.ToString(mycmd.Parameters(0).Value)
            ret_val = ret_val & vbNewLine & val

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        End Try

        Try
            mycmd.Parameters.Clear()
            mycmd.Parameters.Add("v_result", OracleDbType.Varchar2, 2000, val, ParameterDirection.ReturnValue)
            mycmd.CommandText = "pkg_import_forte.F_Country"
            val = mycmd.ExecuteScalar()
            val = Convert.ToString(mycmd.Parameters(0).Value)
            ret_val = ret_val & vbNewLine & val

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        End Try

        Try
            mycmd.Parameters.Clear()
            mycmd.Parameters.Add("v_result", OracleDbType.Varchar2, 2000, val, ParameterDirection.ReturnValue)
            mycmd.CommandText = "pkg_import_forte.F_Currency"
            val = mycmd.ExecuteScalar()
            val = Convert.ToString(mycmd.Parameters(0).Value)
            ret_val = ret_val & vbNewLine & val
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        End Try

        Try
            mycmd.Parameters.Clear()
            mycmd.Parameters.Add("v_result", OracleDbType.Varchar2, 2000, val, ParameterDirection.ReturnValue)
            mycmd.CommandText = "pkg_import_forte.F_FX_Rate"
            val = mycmd.ExecuteScalar()
            val = Convert.ToString(mycmd.Parameters(0).Value)
            ret_val = ret_val & vbNewLine & val
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        End Try

        Try
            mycmd.Parameters.Clear()
            mycmd.Parameters.Add("v_result", OracleDbType.Varchar2, 2000, val, ParameterDirection.ReturnValue)
            mycmd.CommandText = "pkg_import_forte.F_Product_Group"
            val = mycmd.ExecuteScalar()
            val = Convert.ToString(mycmd.Parameters(0).Value)
            ret_val = ret_val & vbNewLine & val
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        End Try

        Try
            mycmd.Parameters.Clear()
            mycmd.Parameters.Add("v_result", OracleDbType.Varchar2, 2000, val, ParameterDirection.ReturnValue)
            mycmd.CommandText = "pkg_import_forte.F_Products"
            val = mycmd.ExecuteScalar()
            val = Convert.ToString(mycmd.Parameters(0).Value)
            ret_val = ret_val & vbNewLine & val
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        End Try

        Try
            mycmd.Parameters.Clear()
            mycmd.Parameters.Add("v_result", OracleDbType.Varchar2, 2000, val, ParameterDirection.ReturnValue)
            mycmd.CommandText = "pkg_import_forte.F_GM_Layout"
            val = mycmd.ExecuteScalar()
            val = Convert.ToString(mycmd.Parameters(0).Value)
            ret_val = ret_val & vbNewLine & val

        Catch ex As Exception
            ExceptionInfo.Show(ex)
            ret_val = "<Font color=red>" & ex.Message.ToString & "</font>"

        Finally
            mycmd.Dispose()
            MyConn.Close()

        End Try
        Return ret_val

    End Function
    Public Function ImportSanovaALL(Optional ByVal connstr As String = "") As String
        Dim MyConn As New MyConnection
        Dim MyCmd As New OracleCommand
        Dim ret_val As String
        Try
            If connstr = "" Then
            Else
                MyConn.ConnectionString = connstr
            End If
            MyCmd.Parameters.Clear()
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Connection = MyConn.Open
            MyCmd.CommandText = "PKG_IMPORT_SANOVA.P_Import"
            MyCmd.ExecuteNonQuery()
            ret_val = "Update with Sanova Data successfully completed"

        Catch ex As Exception
            ret_val = "<Font color=red>" & ex.Message.ToString & "</font>"
            ExceptionInfo.Show(ex)
            Return ret_val
        Finally
            MyConn.Close()
            MyCmd.Dispose()

        End Try
        Return ret_val
    End Function
    Public Shared Function ArchiveFiles(ByVal sourcepath As String, ByVal destinationpath As String) As Boolean

        Dim fileEntries As String()
        Dim fileName, month, mydestinationpath, strfile As String
        Dim MyDir As Directory

        Try

            fileEntries = MyDir.GetFiles(sourcepath)
            ' Process the list of files found in the directory.

            For Each fileName In fileEntries

                month = fileName.Substring(fileName.LastIndexOf("\") + 1, 7) & "\"
                strfile = fileName.Substring(fileName.LastIndexOf("\") + 1)

                If fileName.ToUpper.EndsWith(".LOG") Then
                    mydestinationpath = destinationpath & month & "LOGS\" & strfile
                ElseIf fileName.ToUpper.EndsWith(".BAD") Then
                    mydestinationpath = destinationpath & month & "BAD\" & strfile
                ElseIf fileName.EndsWith(".DAT") Then
                    mydestinationpath = destinationpath & month & strfile
                End If

                If MyDir.Exists(mydestinationpath) Then
                    FileMove(fileName, mydestinationpath)
                Else
                    MyDir.CreateDirectory(mydestinationpath.Substring(0, mydestinationpath.LastIndexOf("\")))
                    FileMove(fileName, mydestinationpath)
                End If

                mydestinationpath = ""

            Next fileName
            Return True

        Catch ex As Exception

            ExceptionInfo.Show(ex)
            Return False
        End Try
    End Function
    Public Shared Function ArchiveManualImportedFiles(ByVal sourcepath As String, ByVal destinationpath As String) As Boolean

        Dim fileEntries As String()
        Dim fileName, month, mydestinationpath, strfile As String
        Dim MyDir As Directory

        Try

            fileEntries = MyDir.GetFiles(sourcepath)
            ' Process the list of files found in the directory.

            For Each fileName In fileEntries

                month = "manually_imported\"
                strfile = fileName.Substring(fileName.LastIndexOf("\") + 1)

                If fileName.ToUpper.EndsWith(".LOG") Then
                    mydestinationpath = destinationpath & month & "LOGS\" & strfile
                ElseIf fileName.ToUpper.EndsWith(".BAD") Then
                    mydestinationpath = destinationpath & month & "BAD\" & strfile
                ElseIf fileName.EndsWith(".DAT") Then
                    mydestinationpath = destinationpath & month & strfile
                End If

                If MyDir.Exists(mydestinationpath) Then
                    FileMove(fileName, mydestinationpath)
                Else
                    MyDir.CreateDirectory(mydestinationpath.Substring(0, mydestinationpath.LastIndexOf("\")))
                    FileMove(fileName, mydestinationpath)
                End If

                mydestinationpath = ""

            Next fileName
            Return True

        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Return False
        End Try
    End Function

    Public Function GetFtpFilesService() As Integer
        Dim FtpSession As New FtpConnection
        Dim x As Integer = 0
        Try
            Dim files As FileItem()
            Dim strDate As String
            Dim strname, cmd_test, cmd_live, strctlFilename, descOut, descErr As String
            Dim strpath, str_output_test, str_error_test, str_output_live, str_error_live As String
            Dim test_i As Integer = 0
            Dim live_i As Integer = 0

            Dim debug As Boolean = False

            If Settings.isLiveServer = True Then
                debug = False
            Else
                debug = True
            End If

            'Initializing FTP Connection
            FtpSession.Hostname = MyCollection("FtpHost")
            FtpSession.Port = 21
            FtpSession.Passive = True
            FtpSession.Username = MyCollection("FtpUser")
            FtpSession.Password = MyCollection("FtpPass")

            FtpSession.Connect()

            If FtpSession.IsConnected = True Then


                ' change into correct dir on the server
                FtpSession.ChangeWorkDir(MyCollection("FtpRemoteDir"))

                ' get a list of all files in this directory
                FtpSession.GetList()
                files = FtpSession.GetDirectoryListing()

                For Each File As FileItem In files
                    strDate = FormatDate(File.FileDate)
                    strpath = File.FilePath
                    strname = File.FileTitle
                    strctlFilename = File.FileTitle

                    myLog.Description = ""
                    myLog.Source = "Alfservice:" & strname
                    myLog.CodeCode = "FTP"
                    myLog.insert()

                    If strname.ToUpper.EndsWith(".DAT") Then
                        strname = strDate & "_" & strname

                        ' First Download the file
                        FtpSession.DownloadFile(File.FileTitle, MyCollection("SanovaImportFilePath") & strname)

                        m_strLog += "Downloading " & File.FileTitle & " -> " & MyCollection("SanovaImportFilePath") & strname & vbNewLine
                        m_strLog += "...Ok " & vbNewLine

                        If debug = False Then ' wenn alfservice am live server läuft - dann  wird auch am Testsever importiert

                            ' then delete file on the server when not in debug mode
                            FtpSession.Delete(File.FileTitle)


                            'then load downloaded file in the temp tables on the Test server
                            Try
                                cmd_test = "SQLLDR.EXE " & MyCollection("OracleUserName") & "/" & MyCollection("OracleUserPassword") & "@" & MyCollection("OracleServiceNameTestServer") _
                                 & " control=" & MyCollection("SanovaImportControlFilePath") & strctlFilename & ".ctl" & " data=" & MyCollection("SanovaImportFilePath") & strname & " log=" & MyCollection("SanovaImportFilePath") & strname & ".testserver.log bad=" & MyCollection("SanovaImportFilePath") & strname & ".testserver.bad"

                                test_i = System.CodeDom.Compiler.Executor.ExecWaitWithCapture(cmd_test, New System.CodeDom.Compiler.TempFileCollection, str_output_test, str_error_test)

                                descErr = read_sqlloader_errors(str_error_test, True)
                                descOut = read_sqlloader_errors(str_output_test, True)


                                ' make Log Entries for SQLLOADER OUTPUT LOG
                                myLog.Description = descOut
                                myLog.Source = " Alfservice SQL-LOADER OUTPUT LOG FOR:" & strname
                                myLog.CodeCode = "FTP"
                                myLog.insert(CONNECTION_STRING_TEST_SERVER)

                                ' make Log Entries for SQLLOADER ERROR LOG
                                myLog.Description = descErr
                                myLog.Source = " Alfservice SQL-LOADER ERROR LOG FOR:" & strname
                                myLog.CodeCode = "FTP"
                                myLog.insert(CONNECTION_STRING_TEST_SERVER)

                                System.IO.File.Delete(str_error_test)
                                System.IO.File.Delete(str_output_test)

                            Catch ex As Exception
                                m_strLog += "A SQL-LOADER Exception occurred while loading Sanova files into the Testserver Database:" & vbNewLine
                                m_strLog += cmd_test & vbNewLine
                                m_strLog += ex.Message.ToString
                            End Try

                        End If


                        'then load downloaded file in the temp tables on the Live server
                        Try
                            cmd_live = "SQLLDR.EXE " & MyCollection("OracleUserName") & "/" & MyCollection("OracleUserPassword") & "@" & MyCollection("OracleServiceName") _
                                & " control=" & MyCollection("SanovaImportControlFilePath") & strctlFilename & ".ctl" & " data=" & MyCollection("SanovaImportFilePath") & strname & " log=" & MyCollection("SanovaImportFilePath") & strname & ".log bad=" & MyCollection("SanovaImportFilePath") & strname & ".bad"

                            live_i = System.CodeDom.Compiler.Executor.ExecWaitWithCapture(cmd_live, New System.CodeDom.Compiler.TempFileCollection, str_output_live, str_error_live)

                            descOut = read_sqlloader_errors(str_error_live, True)
                            descErr = read_sqlloader_errors(str_output_live, True)

                            ' make Log Entries for SQLLOADER OUTPUT LOG
                            myLog.Description = descOut
                            myLog.Source = "Alfservice SQL-LOADER OUTPUT LOG FOR:" & strname
                            myLog.CodeCode = "FTP"
                            myLog.insert()
                            ' make Log Entries for SQLLOADER ERROR LOG
                            myLog.Description = descErr
                            myLog.Source = "Alfservice SQL-LOADER ERROR LOG FOR:" & strname
                            myLog.CodeCode = "FTP"
                            myLog.insert()

                            System.IO.File.Delete(str_error_live)
                            System.IO.File.Delete(str_output_live)

                            If live_i < 0 Then
                                m_strLog += "ALF Was not able to execute the following Command: " & vbNewLine & cmd_live & vbNewLine & vbNewLine
                                m_strLog += "The File " & strname & " was not loaded into the Database!" & vbNewLine
                                m_strLog += "Please check that Oracle Client is correctly installed and SQLLDR.EXE is working correctly" & vbNewLine & vbNewLine
                            Else
                                m_strLog += "The File " & strname & " successfully loaded into the Database!" & vbNewLine
                                x = x + 1
                            End If

                        Catch ex As Exception
                            m_strLog += "A SQL-LOADER Exception occurred while loading Sanova files into the Database:" & vbNewLine
                            m_strLog += cmd_live & vbNewLine
                            m_strLog += ex.Message.ToString

                            myLog.Description = ex.Message.ToString
                            myLog.CodeCode = "ImportError"
                            myLog.Source = "AlfService"
                            myLog.insert()
                        End Try

                    End If

                    m_strLog += vbNewLine + vbNewLine



                Next

                FtpSession.Disconnect()


                ' then move all files into the archive dir
                ArchiveFiles(MyCollection("SanovaImportFilePath"), MyCollection("SanovaImportFileArchivePath"))


                If x > 0 Then
                    If debug = False Then ' wenn live server dann
                        Try

                            'Make Updates on the live server first
                            m_strLog += vbNewLine & "Liveserver:" & ImportForteAll(CONNECTION_STRING_LIVE_SERVER)
                            m_strLog += vbNewLine & "Liveserver:" & ImportSanovaALL(CONNECTION_STRING_LIVE_SERVER)

                            'Make updates on the test server
                            m_strLog += "Testserver:" & ImportForteAll(CONNECTION_STRING_TEST_SERVER)
                            m_strLog += "Testserver:" & ImportSanovaALL(CONNECTION_STRING_TEST_SERVER)

                        Catch ex As Exception
                            myLog.Description = ex.Message.ToString
                            myLog.CodeCode = "ImportError"
                            myLog.Source = "AlfService"
                            myLog.insert()
                        End Try


                    Else ' wenn nicht am live server dann import nur am server auf dem der service läuft!
                        m_strLog += ImportForteAll()
                        m_strLog += ImportSanovaALL()
                    End If

                End If

                myLog.Description = x & "Files were successfully imported"
                myLog.CodeCode = "FTP"
                myLog.Source = "AlfService"
                myLog.insert()


            End If ' IsConnected=true


        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            ' make log entries in db and reset the logs

            myLog.Description = FtpSession.strLog
            myLog.Source = "Alfservice FTP LOG"
            myLog.CodeCode = "FTP"
            myLog.insert()
            FtpSession.strLog = ""


            myLog.Description = Me.strLog
            myLog.Source = "Alfservice"
            myLog.CodeCode = "FTP"
            myLog.insert()
            Me.strLog = ""

        End Try

        Return x

    End Function
    Public Overloads Function GetFtpFiles(ByRef FileLisBox As ListBox) As Integer
        Try
            Dim files As FileItem()
            Dim strDate As String
            Dim strname, cmd_test, cmd_live, strctlFilename As String
            Dim strpath, str_output_test, str_error_test, str_output_live, str_error_live As String
            Dim test_i As Integer = 0
            Dim live_i As Integer = 0
            Dim x As Integer = 0
            Dim FtpSession As New FtpConnection
            Dim MyImport As New WyethImport
            FtpSession.strLog = ""

            'Initializing FTP Connection
            FtpSession.Hostname = MyCollection("FtpHost")
            FtpSession.Port = 21
            FtpSession.Passive = True
            FtpSession.Username = MyCollection("FtpUser")
            FtpSession.Password = MyCollection("FtpPass")

            FtpSession.Connect()

            If FtpSession.IsConnected = True Then


                'm_strLog += FtpSession.strLog & vbNewLine & vbNewLine & vbNewLine

                ' change into correct dir on the server
                FtpSession.ChangeWorkDir(MyCollection("FtpRemoteDir"))
                FtpSession.ChangeWorkDir(MyCollection("FtpRemoteDirArchive"))
                ' get a list of all files in this directory


                For Each item As ListItem In FileLisBox.Items()

                    If item.Text.ToString.ToUpper.EndsWith(".DAT") And item.Selected = True Then

                        ' First Download the file
                        If FtpSession.DownloadFile(item.Value, MyCollection("SanovaImportFilePath") & item.Text) = True Then
                            m_strLog += FtpSession.strLog & vbNewLine
                            m_strLog += "Downloading " & item.Value & " -> " & MyCollection("SanovaImportFilePath") & item.Text & vbNewLine
                            m_strLog += "... Ok!" & vbNewLine & vbNewLine
                        Else
                            m_strLog += FtpSession.strLog & vbNewLine
                            m_strLog += "<Font color=red> Error while downloading " & item.Value & " -> " & MyCollection("SanovaImportFilePath") & item.Text & "</font>" & vbNewLine
                            m_strLog += vbNewLine
                        End If

                        'then load downloaded file in the temp tables on the Live server
                        cmd_live = "SQLLDR.EXE " & MyCollection("OracleUserName") & "/" & MyCollection("OracleUserPassword") & "@" & MyCollection("OracleServiceName") _
                             & " control=" & MyCollection("SanovaImportControlFilePath") & item.Value.ToString.Substring(0, item.Value.ToString.LastIndexOf(".")) & ".dat.ctl" & " data=" & MyCollection("SanovaImportFilePath") & item.Text & " log=" & MyCollection("SanovaImportFilePath") & item.Text & ".log bad=" & MyCollection("SanovaImportFilePath") & item.Text & ".bad"
                        live_i = System.CodeDom.Compiler.Executor.ExecWaitWithCapture(cmd_live, New System.CodeDom.Compiler.TempFileCollection, str_output_live, str_error_live)


                        read_sqlloader_errors(str_error_live, "live_server")
                        read_sqlloader_errors(str_output_live, "live_server")

                        If live_i < 0 Then
                            m_strLog += "ALF Was not able to execute the following Command: " & vbNewLine & cmd_live & vbNewLine & vbNewLine
                            m_strLog += "The File " & item.Text & " was not loaded into the Database!" & vbNewLine
                            m_strLog += "Please check that Oracle Client is correctly installed and SQLLDR.EXE is working correctly" & vbNewLine & vbNewLine
                        Else
                            If m_strLog.IndexOf("failed") > 0 Or m_strLog.IndexOf("failed") > 0 Or m_strLog.IndexOf("nicht erfolgreich") > 0 Then

                            Else
                                m_strLog += "The File " & item.Text & " successfully loaded into the Database!" & vbNewLine
                                x = x + 1
                            End If

                        End If

                        'delete temp files
                        Try
                            System.IO.File.Delete(str_error_live)
                            System.IO.File.Delete(str_output_live)
                        Catch ex As Exception

                        End Try
                        m_strLog += vbNewLine + vbNewLine
                    End If

                    'reset FTP session Log
                    FtpSession.strLog = ""

                Next

                FtpSession.Disconnect()
                m_strLog += FtpSession.strLog & vbNewLine


                ' then move all files into the archive dir

                ArchiveFiles(MyCollection("SanovaImportFilePath"), MyCollection("SanovaImportFileArchivePath"))


                If x > 0 Then
                    m_strLog += MyImport.ImportForteAll() & vbNewLine
                    m_strLog += MyImport.ImportSanovaALL() & vbNewLine
                End If

                myLog.Description = Me.strLog
                myLog.Source = "Alfservice FTP"
                myLog.CodeCode = "FTP"
                myLog.insert()

            End If ' IsConnected=true

            Return x
        Catch ex As Exception
            Throw (ex)
        Finally

        End Try
    End Function
    Public Overloads Function ImportManual(ByVal path As String) As String
        Try
            Dim strname, cmd, strctlFilename As String
            Dim strpath, str_output, str_error As String
            Dim files As File()
            Dim i As Integer = 0
            Dim x As Integer = 0
            Dim f As Integer = 0
            Dim fileEntries As String()
            Dim MyDir As Directory

            ' takes the filenames from the folder specified in the input box
            fileEntries = MyDir.GetFiles(path)

            For Each fileName As String In fileEntries

                ' Alle Files die die entweder "KD" "BE" oder "ART" enthalten und mit .dat enden werden importiert

                If InStr((fileName.ToUpper), "KD") <> 0 And fileName.ToUpper.EndsWith(".DAT") Then
                    f = f + 1
                    cmd = "SQLLDR.EXE " & MyCollection("OracleUserName") & "/" & MyCollection("OracleUserPassword") & "@" & MyCollection("OracleServiceName") _
                      & " control=" & MyCollection("SanovaImportControlFilePath") & "wyeth_kd.dat.ctl" & " data=" & fileName & " log=" & fileName & ".log bad=" & fileName & ".bad"

                    i = System.CodeDom.Compiler.Executor.ExecWaitWithCapture(cmd, New System.CodeDom.Compiler.TempFileCollection, str_output, str_error)

                    read_sqlloader_errors(str_output)
                    read_sqlloader_errors(str_error)

                ElseIf InStr(fileName.ToUpper, "ART") <> 0 And fileName.ToUpper.EndsWith(".DAT") Then
                    f = f + 1
                    cmd = "SQLLDR.EXE " & MyCollection("OracleUserName") & "/" & MyCollection("OracleUserPassword") & "@" & MyCollection("OracleServiceName") _
                      & " control=" & MyCollection("SanovaImportControlFilePath") & "wyeth_art.dat.ctl" & " data=" & fileName & " log=" & fileName & ".log bad=" & fileName & ".bad"

                    i = System.CodeDom.Compiler.Executor.ExecWaitWithCapture(cmd, New System.CodeDom.Compiler.TempFileCollection, str_output, str_error)

                    read_sqlloader_errors(str_output)
                    read_sqlloader_errors(str_error)

                ElseIf InStr(fileName.ToUpper, "BW") <> 0 And fileName.ToUpper.EndsWith(".DAT") Then
                    f = f + 1
                    cmd = "SQLLDR.EXE " & MyCollection("OracleUserName") & "/" & MyCollection("OracleUserPassword") & "@" & MyCollection("OracleServiceName") _
                   & " control=" & MyCollection("SanovaImportControlFilePath") & "wyeth_bw.dat.ctl" & " data=" & fileName & " log=" & fileName & ".log bad=" & fileName & ".bad"

                    i = System.CodeDom.Compiler.Executor.ExecWaitWithCapture(cmd, New System.CodeDom.Compiler.TempFileCollection, str_output, str_error)

                    read_sqlloader_errors(str_output)
                    read_sqlloader_errors(str_error)

                End If



                If i < 0 Then
                    m_strLog += "<Font color=red>ALF Was not able to execute the following Command: " & vbNewLine & cmd & vbNewLine & vbNewLine
                    m_strLog += "The File " & fileName & " was not loaded into the Database!" & vbNewLine
                    m_strLog += "Please check that Oracle Client is correctly installed and SQLLDR.EXE is working correctly </font>" & vbNewLine & vbNewLine
                Else
                    If str_error.IndexOf("failed") > 0 Or str_output.IndexOf("failed") > 0 Then

                    Else
                        m_strLog += "<Font color=red>The File " & fileName & " successfully loaded into the Database!</font>" & vbNewLine & vbNewLine & vbNewLine
                        x = x + 1
                    End If
                End If

                Try
                    System.IO.File.Delete(str_output)
                    System.IO.File.Delete(str_error)
                Catch ex As Exception
                    m_strLog += ex.Message & vbNewLine
                End Try
                i = 0
            Next fileName

            If f = 0 Then
                m_strLog += "<font color=red>ALF was not able to find any valid import files on the server" & vbNewLine
                m_strLog += "The files have to contain either 'KD', 'BE' or 'ART' in their names and a ending with '.DAT' to be recognized as valid import files " & vbNewLine
                m_strLog += "Please check the path of the import files and their names</font>" & vbNewLine & vbNewLine
            Else
                ' then move all files into the archive dir
                ArchiveManualImportedFiles(MyCollection("SanovaImportFilePath"), MyCollection("SanovaImportFileArchivePath"))
            End If

            If x > 0 Then
                Try
                    m_strLog += ImportForteAll()
                    m_strLog += ImportSanovaALL()
                Catch ex As Exception
                    myLog.Description = ex.Message.ToString
                    myLog.CodeCode = "FTP"
                    myLog.Source = "Manual Import"
                    myLog.insert()
                End Try

            End If

            Return Me.strLog

        Catch ex As Exception
            m_strLog += "<font color=red>" & ex.Message & "</font>" & vbNewLine
            ExceptionInfo.Show(ex)
        Finally
            myLog.Description = Me.strLog
            myLog.Source = "Manual Import"
            myLog.CodeCode = "Import"
            myLog.insert()
        End Try
    End Function

    Public Function RefreshMVs() As Boolean
        Dim MyCmd As New OracleCommand
        Dim MyConn As New MyConnection
        Dim val As Object

        Try
            MyCmd.Parameters.Clear()
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_IMPORT_SANOVA.F_REFRESH_MVS"
            MyCmd.Parameters.Add("v_result", OracleDbType.Varchar2, ParameterDirection.ReturnValue)
            MyCmd.Connection = MyConn.Open
            val = MyCmd.ExecuteScalar()
            val = MyCmd.Parameters(0).Value
            m_strLog += "MViews sucessfully refreshed at!!"

            Return True
        Catch ex As Exception
            m_strLog += "<Font color=red>Error while refreshing Mviews!</font><BR>"
            m_strLog += ex.Message
            ExceptionInfo.Show(ex)
            Return False
        Finally
            MyConn.Close()
        End Try
    End Function
    Public Function GetFtpFilesList(Optional ByVal remotePath As String = "") As Wyeth.Utilities.FileItem()
        Dim FTPSession As New FtpConnection
        Try

            Dim files As FileItem()
            Dim x As Integer = 0
            Dim ftpRemoteDir As String

            If remotePath = "" Then
                ftpRemoteDir = MyCollection("FtpRemoteDir") & "\" & MyCollection("FtpRemoteDirArchive")
            Else
                ftpRemoteDir = remotePath
            End If

            'Initializing FTP Connection
            FTPSession.Hostname = MyCollection("FtpHost")
            FTPSession.Port = 21
            FTPSession.Passive = True
            FTPSession.Username = MyCollection("FtpUser")
            FTPSession.Password = MyCollection("FtpPass")


            FTPSession.Connect()

            If (FTPSession.IsConnected = True) Then



                ' change into correct dir on the server
                FTPSession.ChangeWorkDir(ftpRemoteDir)

                ' get a list of all files in this directory
                If FTPSession.GetList() = True Then
                    files = FTPSession.GetDirectoryListing()
                    FTPSession.Disconnect()
                End If


            Else

            End If

            Return files

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            FTPSession.strLog = ""
        End Try
    End Function

    Private Overloads Sub read_sqlloader_errors(ByVal sender As String, Optional ByVal server As String = "")
        Try
            Dim sr As StreamReader = File.OpenText(sender)
            Dim input As String

            If File.Exists(sender) Then
                input = sr.ReadToEnd

                sr.Close()
                If input Is Nothing = False Then
                    If input.IndexOf("failed") > 0 Or input.IndexOf("failed") > 0 Or input.IndexOf("nicht erfolgreich") > 0 Then
                        input = "<Font color=red>" & input & "</font>"
                    End If
                    m_strLog += input
                End If
            End If

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        End Try

    End Sub
    Private Overloads Function read_sqlloader_errors(ByVal sender As String, ByVal dontlog As Boolean) As String
        Try
            Dim sr As StreamReader = File.OpenText(sender)
            Dim input As String = ""

            If File.Exists(sender) Then
                input = sr.ReadToEnd
                sr.Close()
            End If
            Return input
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        End Try

    End Function
    Private Sub readAppVars()

        'Get Vars from DB and Put it into MyCollection

        Dim MyConnection As New MyConnection
        Dim MyReader As OracleDataReader
        Dim MyCmd As New OracleCommand
        Try

            MyCollection.Clear()
            MyCmd.Parameters.Clear()
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_APPLICATION.GetAppVars"
            MyCmd.Connection = MyConnection.Open()
            MyCmd.Parameters.Add("AppVars", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyReader = MyCmd.ExecuteReader(CommandBehavior.CloseConnection)
            Do While MyReader.Read
                MyCollection.Add(MyReader("apse_variable"), MyReader("apse_value"))
            Loop
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConnection.Close()
            MyReader.Close()
            MyReader.Dispose()
            MyCmd.Dispose()
        End Try
    End Sub

    Private Function Import_KD() As String
        Dim MyConn As New MyConnection
        Dim val As Object
        Dim MyCmd As New OracleCommand
        Try
            MyCmd.Parameters.Clear()
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_IMPORT_SANOVA.F_Customers"
            MyCmd.Parameters.Add("v_result", OracleDbType.Varchar2, 2000, val, ParameterDirection.ReturnValue)
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Connection = MyConn.Open
            val = MyCmd.ExecuteScalar()
            val = MyCmd.Parameters(0).Value
            Return CStr(val)
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try

    End Function
    Private Function Import_ART() As String
        Dim MyConn As New MyConnection
        Dim val As Object
        Dim MyCmd As New OracleCommand
        Try
            MyCmd.Parameters.Clear()
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Add("v_result", OracleDbType.Varchar2, 2000, val, ParameterDirection.ReturnValue)
            MyCmd.Connection = MyConn.Open
            MyCmd.CommandText = "PKG_IMPORT_SANOVA.F_Products"
            val = MyCmd.ExecuteScalar()
            val = MyCmd.Parameters(0).Value
            Return CStr(val)
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()

        End Try
    End Function
    Private Function Import_BW() As String
        ' update MasterTables in ALF with upadte & insert Stored Procedures
        Dim MyConn As New MyConnection
        Dim val As Object
        Dim MyCmd As New OracleCommand
        Try
            MyCmd.Parameters.Clear()
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Add("v_result", OracleDbType.Varchar2, 2000, val, ParameterDirection.ReturnValue)
            MyCmd.Connection = MyConn.Open
            MyCmd.CommandText = "PKG_IMPORT_SANOVA.F_BW"
            val = MyCmd.ExecuteScalar()
            val = MyCmd.Parameters(0).Value
            Return CStr(val)
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()

        End Try

    End Function
    Private Function Import_Broken_BW() As String
        ' update MasterTables in ALF with upadte & insert Stored Procedures
        Dim MyConn As New MyConnection
        Dim val As Object
        Dim MyCmd As New OracleCommand
        Try
            MyCmd.Parameters.Clear()
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Add("v_result", OracleDbType.Varchar2, 2000, val, ParameterDirection.ReturnValue)
            MyCmd.Connection = MyConn.Open
            MyCmd.CommandText = "PKG_IMPORT_SANOVA.F_add_broken_BW"
            val = MyCmd.ExecuteScalar()
            val = MyCmd.Parameters(0).Value
            Return CStr(val)
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()

        End Try

    End Function
    Public Property strLog() As String
        Get
            Return m_strLog
        End Get
        Set(ByVal Value As String)
            m_strLog = Value
        End Set
    End Property
    Public Function GetTransmissions(ByVal dist_id As Int16) As DataView
        Dim MyCmd As New OracleCommand
        Dim conn As New MyConnection
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyDS As New DataSet
        Try
            MyCmd.CommandText = "PKG_APPLICATION.GetTransmissions"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Add("transmissions", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_dist_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = dist_id
            MyCmd.Connection = conn.Open()

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDS, "Codes")
            MyDataView = MyDS.Tables("codes").DefaultView

            Return MyDataView
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try
    End Function
    Public Function DeleteTransmission(ByVal tran_id As Integer) As Boolean
        Dim MyCmd As New OracleCommand
        Dim MyConn As New MyConnection
        Dim val As Object
        Try

            MyCmd.Connection = MyConn.Open
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "P_Transmission_Del_Proc"
            MyCmd.Parameters.Add("v_trans_id", OracleDbType.Int32, ParameterDirection.Input).Value = tran_id
            val = MyCmd.ExecuteScalar()
            val = MyCmd.Parameters(0).Value
            Me.strLog = "<Font color=red>Transmission successfully deleted!<br></font>"
            Return True

        Catch ex As Exception
            Me.strLog = "<Font color=red>Transmission could not be deleted! </font><br>"
            Me.strLog = Me.strLog + ex.Message
            Return False
        Finally
            MyConn.Close()

        End Try
    End Function
End Class
