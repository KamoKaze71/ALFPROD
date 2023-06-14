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
Imports aejw.Network
'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
'''         <revision>1</revision>
''' <summary><para>provides all the functions for DataImport from Distributors</para></summary>
'''<seealso cref="Utilities.FTP">Utilities.FTP</seealso> 
Public Class WyethImport

    Private myLog As New Log
    Private MyCollection As New Hashtable
    Private m_strLog As String
    Private sqlldr_err As String
    Private sqlldr_out As String
    Private FtpSession As New FtpConnection
    Private foundFilesOnServer As Boolean = False
   

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
    Public Sub ImportMuensterALL(Optional ByVal connstr As String = "")
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
            MyCmd.CommandText = "PKG_IMPORT_SANOVA.P_Import_MUE"
            MyCmd.ExecuteNonQuery()
            ret_val = "Update with Münster Data successfully completed"

        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Me.strLog = Me.strLog & vbNewLine & ex.Message
        Finally
            MyConn.Close()
            MyCmd.Dispose()

        End Try
    End Sub
    Public Shared Function ArchiveFilesExport(ByVal sourcepath As String, ByVal destinationpath As String) As String

        Dim fileEntries As FileInfo()
        Dim fileName As FileInfo
        Dim month, mydestinationpath, strfile As String
        Dim MyDir As New DirectoryInfo(sourcepath)
        Dim ret_val As String

        Try

            fileEntries = MyDir.GetFileSystemInfos(sourcepath)


            For Each fileName In fileEntries

                month = Today.Date.ToString()
               
                File.Move(fileName.FullName, destinationpath + month + fileName.Name)

            Next fileName
            Return ret_val

        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Return "<font color=red>" & ex.Message.ToString & "</font> "
        End Try

    End Function
    Public Shared Function ArchiveFiles(ByVal sourcepath As String, ByVal destinationpath As String) As String

        Dim fileEntries As String()
        Dim fileName, month, mydestinationpath, strfile As String
        Dim MyDir As Directory
        Dim ret_val As String

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
                ret_val = ret_val & "Moving " & fileName & " to " & mydestinationpath & "<BR>"

                mydestinationpath = ""

            Next fileName
            Return ret_val

        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Return "<font color=red>" & ex.Message.ToString & "</font> "
        End Try
    End Function
    Public Shared Function ArchiveManualImportedFiles(ByVal sourcepath As String, ByVal destinationpath As String, Optional ByVal dist_name As String = "SANOVA") As String

        Dim fileEntries As String()
        Dim fileName, month, mydestinationpath, strfile, fileext As String
        Dim MyDir As Directory
        Dim ret_val As String


        Try

            fileEntries = MyDir.GetFiles(sourcepath)
            ' Process the list of files found in the directory.

            For Each fileName In fileEntries

                If dist_name = "SANOVA" Then
                    month = "manually_imported\"
                    fileext = ".DAT"
                ElseIf dist_name = "PHARMOSAN" Then
                    month = "pharmosan_file_archive\"
                    fileext = ".TXT"
                End If
                strfile = fileName.Substring(fileName.LastIndexOf("\") + 1)

                If fileName.ToUpper.EndsWith(".LOG") Then
                    mydestinationpath = destinationpath & month & "LOGS\" & strfile
                ElseIf fileName.ToUpper.EndsWith(".BAD") Then
                    mydestinationpath = destinationpath & month & "BAD\" & strfile
                ElseIf fileName.ToUpper.EndsWith(fileext) Then
                    mydestinationpath = destinationpath & month & strfile
                End If
                If IsNothing(mydestinationpath) = False Then
                    If MyDir.Exists(mydestinationpath) Then
                        FileMove(fileName, mydestinationpath)
                    Else
                        MyDir.CreateDirectory(mydestinationpath.Substring(0, mydestinationpath.LastIndexOf("\")))
                        FileMove(fileName, mydestinationpath)
                    End If
                    ret_val = ret_val & "Moving " & fileName & " to " & mydestinationpath & "<BR>"
                End If


                mydestinationpath = Nothing

            Next fileName

            Return ret_val

        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Return "<font color=red>" & ex.Message.ToString & "</font> "
        End Try
    End Function


    Public Function GetFtpFilesService() As Integer
        Dim FtpSession As New FtpConnection
        Dim x As Integer = 0


        Dim files As FileItem()
        Dim strDate As String
        Dim strname, cmd_test, cmd_live, strctlFilename, descOut, descErr As String
        Dim strpath, str_output_test, str_error_test, str_output_live, str_error_live, strGroup, strCtlFile As String
        Dim test_i As Integer = 0
        Dim live_i As Integer = 0

        Dim debug As Boolean = Settings.AlfServiceModeDebug

        'If Settings.isLiveServer = True Then
        '    debug = False
        'Else
        '    debug = True
        'End If

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

            'check for duplicate files
            files = checkSanovaFiles(files)

            If foundFilesOnServer = True Then

                For Each File As FileItem In files
                    strDate = FormatDate(File.FileDate)
                    strpath = File.FilePath
                    strname = strDate & "_" & File.FileTitle
                    strctlFilename = File.FileOwner
                    strGroup = File.FileGroup



                    'check Sanova File set File.group property to dont_import for more than one bw file
                    'download file, but dont import it!

                    If strGroup.ToUpper.StartsWith("DONT_IMPORT") And File.IsDirectory = False Then

                        FtpSession.DownloadFile(File.FileTitle, MyCollection("SanovaImportFileArchivePath") & strDate & "_" & File.FileGroup & "_" & File.FileTitle)

                        If Settings.isLiveServer = True Then    ' Nur wenn Service am LiveServer läuft werden Files vom Server gelöscht
                            FtpSession.Delete(File.FileTitle)
                        End If

                    ElseIf strGroup.ToUpper.StartsWith("IMPORT") And File.IsDirectory = False Then

                        ' First Download the file
                        FtpSession.DownloadFile(File.FileTitle, MyCollection("SanovaImportFilePath") & strname)

                        m_strLog += "Downloading " & File.FileTitle & " -> " & MyCollection("SanovaImportFilePath") & strname & vbNewLine
                        m_strLog += "...Ok " & vbNewLine

                        If Settings.isLiveServer = True Then 'Nur wenn Service am LiveServer läuft werden Files vom Server gelöscht
                            FtpSession.Delete(File.FileTitle)
                        End If


                        'then load downloaded file in the temp tables on the Live server
                        Try
                            cmd_live = "SQLLDR.EXE " & MyCollection("OracleUserName") & "/" & MyCollection("OracleUserPassword") & "@" & MyCollection("OracleServiceName") _
                                & " control=" & MyCollection("SanovaImportControlFilePath") & strctlFilename & " data=" & MyCollection("SanovaImportFilePath") & strname & " log=" & MyCollection("SanovaImportFilePath") & strname & ".log bad=" & MyCollection("SanovaImportFilePath") & strname & ".bad"

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
                            myLog.CodeCode = "ImportErr"
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
                    '    If debug = False Then ' wenn live server dann
                    Try

                        'Make Updates on the live server first
                        m_strLog += vbNewLine & Settings.getMachineName & ":" & ImportForteAll() & vbNewLine 'CONNECTION_STRING_LIVE_SERVER)
                        m_strLog += vbNewLine & Settings.getMachineName & ":" & ImportSanovaALL() & vbNewLine 'CONNECTION_STRING_LIVE_SERVER)

                        ''Make updates on the test server
                        'm_strLog += "Testserver:" & ImportForteAll(Settings.connectionStringDev) & vbNewLine 'CONNECTION_STRING_TEST_SERVER)
                        'm_strLog += "Testserver:" & ImportSanovaALL(Settings.connectionStringDev) & vbNewLine

                    Catch ex As Exception
                        myLog.Description = ex.Message.ToString
                        myLog.CodeCode = "ImportErr"
                        myLog.Source = "AlfService"
                        myLog.insert()
                    End Try


                    'Else ' wenn nicht am live server dann import nur am server auf dem der service läuft!
                    '    m_strLog += ImportForteAll()
                    '    m_strLog += ImportSanovaALL()
                    'End If

                End If

                myLog.Description = x & " Files were successfully imported"
                myLog.CodeCode = "FTP"
                myLog.Source = "AlfService: " & x & " Files were successfully imported"
                myLog.insert()

            ElseIf FtpSession.IsConnected = False Then

                myLog.Description = "Could not connect to FTP Server"
                myLog.Source = "Could not conect to FTP Server"
                myLog.CodeCode = "FTP"
                myLog.insert()
            End If ' IsConnected=true


            ' make log entries in db and reset the logs

            myLog.Description = FtpSession.strLog
            myLog.Source = "Alfservice FTP LOG"
            myLog.CodeCode = "FTP"
            myLog.insert()
            FtpSession.strLog = ""


            myLog.Description = Me.strLog
            myLog.Source = "Alfservice: Import Process"
            myLog.CodeCode = "FTP"
            myLog.insert()
            Me.strLog = ""

        End If ' foundFilesOnserver=true
        Return x

    End Function
    Private Function checkSanovaFiles(ByVal files As FileItem()) As FileItem()
        ' Achtung File Attrributes Owner + Group werden missbraucht um Import Flags zu speichern

        Dim retfiles As FileItem() = files
        Dim i_bw_file_count As Integer = 0
        Dim i_art_file_count As Integer = 0
        Dim i_kd_file_count As Integer = 0


        For Each File As FileItem In retfiles
            Try

                If File.IsDirectory = False Then

                    Dim value As String
                    Dim filename As String = File.FileTitle


                    If filename.ToUpper.IndexOf("BW") > 0 Then
                        value = "BW"
                    ElseIf filename.ToUpper.IndexOf("ART") > 0 Then
                        value = "ART"
                    ElseIf filename.ToUpper.IndexOf("KD") > 0 Then
                        value = "KD"
                    Else
                        value = "UNKOWN"
                    End If

                    Select Case value

                        Case "BW"  ' bewegungsdaten
                            i_bw_file_count = i_bw_file_count + 1

                            If i_bw_file_count > 1 Then
                                File.FileGroup = "DONT_IMPORT" & File.FileTitle

                                Dim MyLOg As New Log
                                Dim strMessage As String

                                strMessage = "ALF detected more than one BW File on Sanova's FTP server!" & vbNewLine
                                strMessage += "ALF will only import one BW File!" & vbNewLine
                                strMessage += "Not imported BW Files will only be downloaded and can be found in " & MyCollection("SanovaImportFileArchivePath")
                                strMessage += " on the Intranet Server"
                                MyLOg.CodeCode = "FTP"
                                MyLOg.Description = strMessage
                                MyLOg.Source = "AlfService: More Than on BW file"
                                MyLOg.insert()

                            ElseIf i_bw_file_count = 1 Then
                                File.FileOwner = "WYETH_BW.dat.ctl"
                                File.FileGroup = "IMPORT"
                            End If

                        Case "ART" ' Artikeldaten
                            i_art_file_count = i_art_file_count + 1

                            File.FileOwner = "WYETH_ART.dat.ctl"
                            File.FileGroup = "IMPORT"

                        Case "KD"  ' KundenDaten
                            i_kd_file_count = i_kd_file_count + 1

                            File.FileOwner = "WYETH_KD.dat.ctl"
                            File.FileGroup = "IMPORT"

                        Case Else
                            File.FileOwner = "UNKNOWN"
                            File.FileGroup = "DONT_IMPORT"
                    End Select

                End If

                If i_bw_file_count > 0 Or i_art_file_count > 0 Or i_kd_file_count > 0 Then
                    foundFilesOnServer = True
                Else
                    foundFilesOnServer = False
                End If

            Catch ex As Exception
                ExceptionInfo.Show(ex)
            End Try
        Next

        Return retfiles

    End Function
    Public Function DownloadFtpFiles(ByVal FileLisBox As ListBox) As String

        Try
            Dim files As FileItem()
            Dim strDate As String
            Dim strname, strctlFilename As String
            Dim strpath, str_output_test, str_error_test, str_output_live, str_error_live As String
            Dim test_i As Integer = 0
            Dim live_i As Integer = 0
            Dim x As Integer = 0

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

                    item.Text = item.Text.ToString.Replace(":", "_")

                    If item.Text.ToString.ToUpper.EndsWith(".DAT") And item.Selected = True Then

                        ' First Download the file
                        If FtpSession.DownloadFile(item.Value, MyCollection("SanovaImportFilePath") & item.Text) = True Then
                            ' m_strLog += FtpSession.strLog & vbNewLine
                            m_strLog += "Downloading " & item.Value & " -> " & MyCollection("SanovaImportFilePath") & item.Text & vbNewLine
                            m_strLog += "... Ok!" & vbNewLine & vbNewLine
                        Else
                            ' m_strLog += FtpSession.strLog & vbNewLine
                            m_strLog += "<Font color=red> Error while downloading " & item.Value & " -> " & MyCollection("SanovaImportFilePath") & item.Text & "</font>" & vbNewLine
                            m_strLog += vbNewLine
                        End If

                    End If

                Next
                FtpSession.Disconnect()

            End If
            Return m_strLog & vbNewLine & FtpSession.strLog
        Catch ex As Exception

        Finally

        End Try
    End Function
    Public Overloads Function SQLLoader(ByVal path As String) As String

        Dim cmd_live, dir, live_i, str_error_live, str_output_live, ctl_file As String
        Dim x, f, i As Integer
        Dim fileEntries As String()
        Dim MyDir As Directory

        Try

            fileEntries = MyDir.GetFiles(path)

            For Each fileName As String In fileEntries
                ctl_file = ""
                If InStr((fileName.ToUpper), "KD") <> 0 And fileName.ToUpper.EndsWith(".DAT") Then
                    ctl_file = "wyeth_kd.dat.ctl"
                    f = f + 1
                ElseIf InStr(fileName.ToUpper, "ART") <> 0 And fileName.ToUpper.EndsWith(".DAT") Then
                    ctl_file = "wyeth_art.dat.ctl"
                    f = f + 1
                ElseIf InStr(fileName.ToUpper, "BW") <> 0 And fileName.ToUpper.EndsWith(".DAT") Then
                    ctl_file = "wyeth_bw.dat.ctl"
                    f = f + 1
                Else
                    File.Delete(fileName)
                End If

                If ctl_file <> "" Then
                    cmd_live = "SQLLDR.EXE " & MyCollection("OracleUserName") & "/" & MyCollection("OracleUserPassword") & "@" & MyCollection("OracleServiceName") _
                      & " control=" & MyCollection("SanovaImportControlFilePath") & ctl_file & " data=" & fileName & " log=" & fileName & ".log bad=" & fileName & ".bad"

                    i = System.CodeDom.Compiler.Executor.ExecWaitWithCapture(cmd_live, New System.CodeDom.Compiler.TempFileCollection, str_output_live, str_error_live)

                    read_sqlloader_errors(str_error_live, "live_server")
                    read_sqlloader_errors(str_output_live, "live_server")

                End If

                If i < 0 Then
                    m_strLog += "<Font color=red>ALF Was not able to execute the following Command: " & vbNewLine & cmd_live & vbNewLine & vbNewLine
                    m_strLog += "The File " & fileName & " was not loaded into the Database!" & vbNewLine
                    m_strLog += "Please check that Oracle Client is correctly installed and SQLLDR.EXE is working correctly </font>" & vbNewLine & vbNewLine
                Else
                    If m_strLog.IndexOf("failed") > 0 Or m_strLog.IndexOf("failed") > 0 Or m_strLog.IndexOf("nicht") > 0 Then
                        m_strLog += "<br><font color=red>The File " & fileName & " could not be loaded into the Database!</font>" & vbNewLine
                    Else
                        m_strLog += "<br><Font color=green>The File " & fileName & " successfully loaded into the Database!</font>" & vbNewLine & vbNewLine & vbNewLine
                        x = x + 1
                    End If
                End If

                Try
                    System.IO.File.Delete(str_error_live)
                    System.IO.File.Delete(str_output_live)
                Catch ex As Exception

                End Try

                i = 0
            Next fileName

            If f = 0 Then
                m_strLog += "<font color=red>ALF was not able to find any valid import files on the server <bR>"
                m_strLog += "The files have to contain either 'KD', 'BE' or 'ART' in their names and a ending with '.DAT' to be recognized as valid import files <br>"
                m_strLog += "Please check the path of the import files and their names</font><bR>"
            End If

        Catch ex As Exception
            m_strLog = m_strLog & "<font color=red>" & ex.message & "<bR>" & ex.Source.ToString & "</font>"
            Return m_strLog
        End Try
        Return m_strLog
    End Function
    Public Overloads Function SQLLoader(ByRef FileLisBox As ListBox) As String

        Dim cmd_live, cmd_test, dir, live_i, str_error_live, str_output_live As String
        Dim mydir As Directory
        Dim x As Integer

        Try


            For Each item As ListItem In FileLisBox.Items()
                If item.Selected = True Then
                    item.Text = item.Text.ToString.Replace(":", "_")

                    'then load downloaded file in the temp tables on the Live server
                    cmd_live = "SQLLDR.EXE " & MyCollection("OracleUserName") & "/" & MyCollection("OracleUserPassword") & "@" & MyCollection("OracleServiceName") _
                         & " control=" & MyCollection("SanovaImportControlFilePath") & item.Value.ToString.Substring(0, item.Value.ToString.LastIndexOf(".")) & ".dat.ctl" & " data=" & MyCollection("SanovaImportFilePath") & item.Text & " log=" & MyCollection("SanovaImportFilePath") & item.Text & ".log bad=" & MyCollection("SanovaImportFilePath") & item.Text & ".bad"
                    live_i = System.CodeDom.Compiler.Executor.ExecWaitWithCapture(cmd_live, New System.CodeDom.Compiler.TempFileCollection, str_output_live, str_error_live)


                    read_sqlloader_errors(str_error_live, "live_server")
                    read_sqlloader_errors(str_output_live, "live_server")

                    If live_i < 0 Then
                        m_strLog += "<font color=red>ALF Was not able to execute the following Command: " & vbNewLine & cmd_live & vbNewLine & vbNewLine
                        m_strLog += "The File " & item.Text & " was not loaded into the Database!" & vbNewLine
                        m_strLog += "Please check that Oracle Client is correctly installed and SQLLDR.EXE is working correctly</font>" & vbNewLine & vbNewLine
                    Else
                        If m_strLog.IndexOf("failed") > 0 Or m_strLog.IndexOf("failed") > 0 Or m_strLog.IndexOf("nicht") > 0 Then
                            m_strLog += "<font color=red>The File " & item.Text & " could not be loaded into the Database!</font>" & vbNewLine
                        Else
                            m_strLog += "The File " & item.Text & " successfully loaded into the Database!" & vbNewLine
                            x = x + 1
                        End If

                    End If
                End If

                'finally delete temp files
                Try
                    System.IO.File.Delete(str_error_live)
                    System.IO.File.Delete(str_output_live)
                Catch ex As Exception

                End Try

            Next
        Catch ex As Exception
            m_strLog = m_strLog & "<font color=red>" & ex.message & "<bR>" & ex.Source.ToString & "</font>"
            Return m_strLog
        End Try
        Return m_strLog
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
    Public Function DeleteTransmission(ByVal tran_id As Integer, ByVal dist_id As Integer) As Boolean
        Dim MyCmd As New OracleCommand
        Dim MyConn As New MyConnection
        Dim val As Object
        Dim ret_val As String
        Dim dist_name As String = GetDistributorName(dist_id)
        Try


            If dist_name <> "PHARMOSAN" Then
                MyCmd.Connection = MyConn.Open
                MyCmd.CommandType = CommandType.StoredProcedure
                MyCmd.CommandText = "P_Transmission_Del_Proc"
                MyCmd.Parameters.Add("v_trans_id", OracleDbType.Int32, ParameterDirection.Input).Value = tran_id
                val = MyCmd.ExecuteScalar()
                val = MyCmd.Parameters(0).Value
                Me.strLog = "<Font color=red>Transission successfully deleted!</font><br>"
                Return True
            Else
                MyCmd.Connection = MyConn.Open
                MyCmd.CommandType = CommandType.StoredProcedure
                MyCmd.CommandText = "PKG_IMPORT_PHARMOSAN.F_DeleteTransmission"
                MyCmd.Parameters.Add("v_ret_val", OracleDbType.Varchar2, 2000, val, ParameterDirection.ReturnValue)
                MyCmd.Parameters.Add("v_trans_id", OracleDbType.Int32, ParameterDirection.Input).Value = tran_id
                val = MyCmd.ExecuteScalar()
                ret_val = Convert.ToString(MyCmd.Parameters(0).Value.ToString)
                Me.strLog = "<Font color=red>" & ret_val & "</font><br>"
                Return True

            End If

        Catch ex As Exception
            Me.strLog = "<Font color=red>Transmission could not be deleted! </font><br>"
            Me.strLog = Me.strLog + ex.Message
            Return False
        Finally
            MyConn.Close()

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

            'If Settings.AlfServiceModeDebug = True Then
            '    MyCollection("FtpHostMuensterTmpNetworkDrive") = MyCollection("FtpHostMuensterTmpNetworkDrive") + "Alf_Test\"
            '    MyCollection("FtpHostMuensterArchive") = "history"
            'End If


        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConnection.Close()
            MyReader.Close()
            MyReader.Dispose()
            MyCmd.Dispose()
        End Try
    End Sub
    Public Function Import_KD() As String
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

            Return val.ToString

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try

    End Function
    Public Function Import_ART() As String
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
            Return val.ToString
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()

        End Try
    End Function
    Public Function Import_BW() As String
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
            Return val.ToString
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()

        End Try

    End Function
    Public Function Import_BrokenBW() As String
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
            Return val.ToString
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try

    End Function
    Public Function Import_Broken_BW() As String
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
            Return val.ToString
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()

        End Try

    End Function
    Public Function RefreshMVs() As String
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
            Return val.ToString

        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Return "<font color=red>" & ex.Message & " </font>"
        Finally
            MyConn.Close()
        End Try
    End Function
    Public Function Import_StockCheck() As String

        Dim MyConn As New MyConnection
        Dim MyCmd As New OracleCommand

        Try
            MyCmd.Parameters.Clear()
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Connection = MyConn.Open
            MyCmd.CommandText = "PKG_IMPORT_SANOVA.P_Check_Transmissions"
            MyCmd.ExecuteNonQuery()

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()

        End Try
        Return "Transmission has been checked for stock inconsistency "
    End Function
    Public Property strLog() As String
        Get
            Return m_strLog
        End Get
        Set(ByVal Value As String)
            m_strLog = Value
        End Set
    End Property
    Public Function GetTransmissions(ByVal dist_id As Integer) As DataView
        Dim MyCmd As New OracleCommand
        Dim conn As New MyConnection
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyDS As New DataSet


        Dim dist_name As String = GetDistributorName(dist_id)
        Try
            If dist_name = "PHARMOSAN" Then

                MyCmd.CommandText = "PKG_IMPORT_PHARMOSAN.GetTransmissionsPharmosan"
                MyCmd.CommandType = CommandType.StoredProcedure

                MyCmd.Parameters.Add("transmissions", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                MyCmd.Connection = conn.Open()

                MyAdapter.SelectCommand = MyCmd
                MyAdapter.Fill(MyDS, "Transmissions")
                MyDataView = MyDS.Tables("Transmissions").DefaultView

            ElseIf dist_name = "SANOVA" Then

                MyCmd.CommandText = "PKG_APPLICATION.GetTransmissions"
                MyCmd.CommandType = CommandType.StoredProcedure

                MyCmd.Parameters.Add("transmissions", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                MyCmd.Parameters.Add("v_dist_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = dist_id
                MyCmd.Connection = conn.Open()

                MyAdapter.SelectCommand = MyCmd
                MyAdapter.Fill(MyDS, "Codes")
                MyDataView = MyDS.Tables("codes").DefaultView
            ElseIf dist_name = "MÜNSTER" Then

                MyCmd.CommandText = "PKG_APPLICATION.GetTransmissions"
                MyCmd.CommandType = CommandType.StoredProcedure

                MyCmd.Parameters.Add("transmissions", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                MyCmd.Parameters.Add("v_dist_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = dist_id
                MyCmd.Connection = conn.Open()

                MyAdapter.SelectCommand = MyCmd
                MyAdapter.Fill(MyDS, "Codes")
                MyDataView = MyDS.Tables("codes").DefaultView
            End If

            Return MyDataView
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try
    End Function
    Public Shared Function displayImportFiles(ByVal dist_name As String, ByRef lblOUT As Label, ByVal importfilePath As String) As Integer

        Try
            Dim icount As Integer = 0
            lblOUT.Text = ""
            lblOUT.Text = lblOUT.Text & "Files that will be imported:<br><Font color=green>" & vbNewLine

            Select Case dist_name.ToUpper

                Case Is = "SANOVA"
                    For Each MyInfo As System.IO.FileInfo In DirListing(importfilePath)
                        If (MyInfo.FullName.ToUpper.EndsWith(".DAT")) And ((MyInfo.FullName.IndexOf("KD") > 0) Or (MyInfo.FullName.IndexOf("ART") > 0) Or (MyInfo.FullName.IndexOf("BW") > 0)) Then
                            lblOUT.Text = lblOUT.Text & MyInfo.FullName & "<br>"
                            icount = icount + 1
                        End If
                    Next
                    lblOUT.Text = lblOUT.Text & "</Font>"

                    If icount = 0 Then
                        lblOUT.Text = lblOUT.Text & "<Font color=red><br> There are no files in the Server Directory which will be imported<br>"
                        lblOUT.Text = lblOUT.Text & "Notes:<br> "
                        lblOUT.Text = lblOUT.Text & "--> ALF will only import valid Sanova import Files:<br> "
                        lblOUT.Text = lblOUT.Text & "--> ALF will only recognize Sanova Import file when the filename contains either 'BW' or' KD' or 'ART'. <br>"
                        lblOUT.Text = lblOUT.Text & "--> The file extension must be '.dat'!</font><BR>"
                    End If

                Case Is = "PHARMOSAN"
                    For Each MyInfo As System.IO.FileInfo In DirListing(importfilePath)
                        If (MyInfo.FullName.ToUpper.EndsWith(".TXT")) Then
                            lblOUT.Text = lblOUT.Text & MyInfo.FullName & "<br>"
                            icount = icount + 1
                        End If


                    Next
                    lblOUT.Text = lblOUT.Text & "</Font>"


                    If icount = 0 Then
                        lblOUT.Text = lblOUT.Text & "<Font color=red><br> There are no files in the Server Directory which will be imported<br>"
                        lblOUT.Text = lblOUT.Text & "Notes:<br> "
                        lblOUT.Text = lblOUT.Text & "--> ALF will only import valid " & dist_name & " import Files:<br> "
                        lblOUT.Text = lblOUT.Text & "--> The File type must be TAB deltimited TextFile -> Save as Text (Tab delimited) in Excel! "
                        lblOUT.Text = lblOUT.Text & "--> The file extension must be '.txt'!</font><BR>"
                    End If
            End Select
            Return icount
        Catch ex As Exception
            lblOUT.Text = lblOUT.Text & vbNewLine & "No files found in " & Trim(importfilePath)
        End Try
    End Function
    Public Shared Function CountImportFilesOnServer(ByVal dist_name As String, ByVal importfilePath As String) As Integer
        Dim x As Integer
        Try
            If dist_name.ToUpper = "SANOVA" Then
                Dim icount As Integer = 0
                For Each MyInfo As System.IO.FileInfo In DirListing(importfilePath)
                    If MyInfo.FullName.ToUpper.EndsWith(".DAT") Or MyInfo.FullName.IndexOf("KD") > 0 Or MyInfo.FullName.IndexOf("ART") > 0 Or MyInfo.FullName.IndexOf("BW") > 0 Then
                        icount = icount + 1
                    End If
                Next
                Return icount
            ElseIf dist_name.ToUpper = "PHARMOSAN" Then
                Dim icount As Integer = 0
                For Each MyInfo As System.IO.FileInfo In DirListing(importfilePath)
                    If MyInfo.FullName.ToUpper.EndsWith(".TXT") Then
                        icount = icount + 1
                    End If
                Next
                Return icount
            End If
        Catch ex As Exception
            ExceptionInfo.Show(ex)
            x = 0
        End Try
        Return x
    End Function
    Public Function GetDistributorName(ByVal dist_id As Integer) As String
        Dim MyConn As New MyConnection
        Dim MyCmd As New OracleCommand
        Dim dist_name As String
        Dim val As Object
        Try
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_APPLICATION.GetDistributorName"
            MyCmd.Parameters.Add("dist_name", OracleDbType.Varchar2, 2000, val, ParameterDirection.ReturnValue)
            MyCmd.Parameters.Add("v_dist_id", OracleDbType.Int16, DBNull.Value, ParameterDirection.Input).Value = dist_id
            MyCmd.Connection = MyConn.Open()
            MyCmd.ExecuteScalar()
            dist_name = Convert.ToString(MyCmd.Parameters(0).Value.ToString)
            Return dist_name

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try
    End Function
    Public Function GetDistributorID(ByVal dist_name As String) As Integer
        Dim MyConn As New MyConnection
        Dim MyCmd As New OracleCommand
        Dim dist_id As Integer

        Dim val As Object
        Try
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_APPLICATION.GetDistributorID"
            MyCmd.Parameters.Add("dist_name", OracleDbType.Int16, 2000, val, ParameterDirection.ReturnValue)
            MyCmd.Parameters.Add("v_dist_id", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = dist_name
            MyCmd.Connection = MyConn.Open()
            MyCmd.ExecuteScalar()
            dist_id = Convert.ToString(MyCmd.Parameters(0).Value.ToString)
            Return dist_id

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try
    End Function
    Public Sub MapDrive(ByVal username As String, ByVal password As String, ByVal force As Boolean, ByVal persitent As Boolean, ByVal localDrive As String, ByVal ShareName As String)
        Try
            Dim oNetDrive As New NetworkDrive
            oNetDrive.Force = force
            oNetDrive.Persistent = persitent
            oNetDrive.LocalDrive = localDrive
            oNetDrive.PromptForCredentials = False
            oNetDrive.ShareName = ShareName
            oNetDrive.SaveCredentials = False
            oNetDrive.MapDrive(username, password)

        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Me.strLog = Me.strLog & "Failed to Connect to Muenster Network Drive" & vbNewLine & ex.Message
        End Try
    End Sub
    Public Sub UnMapDrive(ByVal localDrive As String, ByVal force As Boolean)
        Try
            Dim oNetDrive As New NetworkDrive
            oNetDrive.Force = force
            oNetDrive.LocalDrive = localDrive
            oNetDrive.PromptForCredentials = False
            oNetDrive.SaveCredentials = False
            oNetDrive.UnMapDrive()

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        End Try
    End Sub
    Public Function ImportMuensterFiles() As Integer
        Try

            'Connnect to Network Drive
            MapDrive(MyCollection("FtpHostMuensterUser"), MyCollection("FtpHostMuensterPass"), True, False, MyCollection("FtpHostMuensterTmpNetworkDrive"), MyCollection("FtpHostMuenster"))

            'Check If File Exists
            Dim importFilesData, importFilesCustomer As Collection
            Dim x As Integer

            importFilesData = newFiles()
            importFilesCustomer = newFilesCustomer()

            If importFilesCustomer.Count > 0 Then
                x = SQLLoaderMunesterImport(importFilesCustomer)
            End If

            If importFilesData.Count > 0 Then
                x = SQLLoaderMunesterImport(importFilesData)
                x = x + x
                myLog.CountryCode = "MUE"
                myLog.CodeCode = "FTP"
                myLog.Source = "ALFService Münster"
                myLog.Description = "Loaded " & x & " files from Münser Server"
                myLog.insert()

                If x > 0 Then
                    ImportMuensterALL()
                End If

            Else
                myLog.CountryCode = "MUE"
                myLog.CodeCode = "FTP"
                myLog.Source = "ALFService Münster"
                myLog.Description = "No ALF files found on Muenster Server "
                myLog.insert()
            End If


            Return x
        Catch ex As Exception
            Throw ex
        Finally
            ' Disconnect Network Drive
            UnMapDrive(MyCollection("FtpHostMuensterTmpNetworkDrive"), True)
        End Try



    End Function
    Public Function SQLLoaderMunesterImport(ByVal importFiles As Collection) As Integer

        Dim cmd_live, dir, live_i, descOut, descErr, str_error_live, str_output_live, ctl_file As String
        Dim i, count_test As Integer
        Dim count As Integer = 0

        myLog.CountryCode = "MUE"
        Try

            For Each filename As String In importFiles
                Dim name As String

                name = filename.Substring(filename.LastIndexOf("\") + 1)

                If name.StartsWith("ALF_JDE_KD") Or name.StartsWith("JDE_ALF_KD") Then
                    ctl_file = "WYETH_KD.dat.ctl"
                Else
                    ctl_file = "WYETH_BW.ctl"
                End If
                Try

                    cmd_live = "SQLLDR.EXE " & MyCollection("OracleUserName") & "/" & MyCollection("OracleUserPassword") & "@" & MyCollection("OracleServiceName") _
                                  & " control=" & MyCollection("SanovaImportControlFilePath") & ctl_file & " data=" & filename & " log=" & filename & ".log bad=" & filename & ".bad"

                    i = System.CodeDom.Compiler.Executor.ExecWaitWithCapture(cmd_live, New System.CodeDom.Compiler.TempFileCollection, str_output_live, str_error_live)

                    If i >= 0 Then
                        count = count + 1
                    End If
                    descOut = read_sqlloader_errors(str_error_live, True)
                    descErr = read_sqlloader_errors(str_output_live, True)

                    ' make Log Entries for SQLLOADER OUTPUT LOG

                    myLog.Description = descOut
                    myLog.Source = "Alfservice Münster SQL-LOADER OUTPUT LOG FOR:" & filename
                    myLog.CodeCode = "FTP"
                    myLog.insert(Settings.connectionString)

                    ' make Log Entries for SQLLOADER ERROR LOG
                    myLog.Description = descErr
                    myLog.Source = "Alfservice Münster SQL-LOADER ERROR LOG FOR:" & filename
                    myLog.CodeCode = "FTP"
                    myLog.insert(Settings.connectionString)

                    System.IO.File.Delete(str_error_live)
                    System.IO.File.Delete(str_error_live)

                Catch ex As Exception
                    ExceptionInfo.Show(ex)
                End Try

            Next

            Return count
            Me.strLog = Me.strLog & vbNewLine & descOut + vbNewLine + descErr
        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Me.strLog = Me.strLog & "<BR>Error while Loading data files with SqlLoader<br>" & ex.Message
        End Try

    End Function
    Private Function newFiles() As Collection

        Dim filename As String
        Dim retval As New Collection
        Dim strDir = MyCollection("FtpHostMuensterTmpNetworkDrive")

        If Settings.isLiveServer = False Then
            strDir = strDir + "\ALF_Test\"
        End If
        Dim counti = 0

        Try

            For Each MyInfo As System.IO.FileInfo In DirListing(strDir)
                If MyInfo.Name.StartsWith("JDE_ALF_") And MyInfo.Name.Length > 11 Then
                    counti = counti + 1
                    'copy File To Muenster Archive
                    File.Copy(MyInfo.FullName, MyInfo.DirectoryName & "\" & MyCollection("FtpHostMuensterArchive") & "\" & MyInfo.Name, True)

                    'copy File To Local ALF archive
                    File.Copy(MyInfo.FullName, MyCollection("SanovaImportFilePath") & MyCollection("FtpHostMuensterLocalArchive") & "\" & MyInfo.Name, True)
                    If Settings.isLiveServer Then
                        File.Delete(MyInfo.FullName)
                    End If

                    retval.Add(MyCollection("SanovaImportFilePath") & MyCollection("FtpHostMuensterLocalArchive") & "\" & MyInfo.Name)
                End If
            Next

            If Settings.AlfServiceModeDebug = True Then
                myLog.CountryCode = "MUE"
                myLog.CodeCode = "FTP"
                myLog.Source = "ALFService Münster"
                myLog.Description = "Number of files on Server" & counti
                myLog.insert()
            End If

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        End Try
        Return retval
    End Function
    Private Function newFilesCustomer() As Collection

        Dim filename As String
        Dim retval As New Collection
        Dim counti = 0
        Dim strDir = MyCollection("FtpHostMuensterTmpNetworkDrive")

        'If Settings.isLiveServer = False Then
        '    strDir = strDir + "\ALF_Test\"
        'End If
        Try

            For Each MyInfo As System.IO.FileInfo In DirListing(strDir)

                If MyInfo.Name.StartsWith("ALF_JDE_KD") And MyInfo.Name.Length > 14 Then
                    counti = counti + 1

                    'copy File To Muenster Archive
                    File.Copy(MyInfo.FullName, MyInfo.DirectoryName & "\" & MyCollection("FtpHostMuensterArchive") & "\" & MyInfo.Name, True)

                    'copy File To Local ALF archive
                    File.Copy(MyInfo.FullName, MyCollection("SanovaImportFilePath") & MyCollection("FtpHostMuensterLocalArchive") & "\" & MyInfo.Name, True)

                    'delete Files on Muenster Server

                    If Settings.isLiveServer Then
                        File.Delete(MyInfo.FullName)
                    End If


                    retval.Add(MyCollection("SanovaImportFilePath") & MyCollection("FtpHostMuensterLocalArchive") & "\" & MyInfo.Name)

                End If


            Next

            If Settings.AlfServiceModeDebug = True Then
                myLog.CountryCode = "MUE"
                myLog.CodeCode = "FTP"
                myLog.Source = "ALFService Münster"
                myLog.Description = "Number of files on Server" & counti
                myLog.insert()
            End If

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        End Try

        Return retval
    End Function
    Public Function getMuensterFiles(ByVal lbfiles As ListBox) As Collection

        Dim filename As String
        Dim retval As New Collection
        Dim counti = 0
        Try
            For Each item As ListItem In lbfiles.Items()

                If item.Selected = True Then
                    counti = counti + 1

                    File.Copy(item.Value, MyCollection("SanovaImportFilePath") & MyCollection("FtpHostMuensterLocalArchive") & "\" & item.Value.Substring(item.Value.LastIndexOf("\") + 1), True)
                    retval.Add(MyCollection("SanovaImportFilePath") & MyCollection("FtpHostMuensterLocalArchive") & "\" & item.Value.Substring(item.Value.LastIndexOf("\") + 1))
                End If

            Next
        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Me.strLog = Me.strLog & vbNewLine & ex.Message
        End Try

        Return retval
    End Function

End Class
