Imports System
Imports System.IO
Imports System.data
Imports System.Web
Imports System.Web.UI
Imports System.Security.Permissions
Imports Microsoft.Win32

Public Class FileHandling

	Public Shared Function Delete(ByVal sourcePath As String) As Boolean

		Try
			' Ensure that the target does not exist.
			File.Delete(sourcePath)

			Return True

		Catch ex As Exception
			ExceptionInfo.Show(ex)
			Return False

		End Try
	End Function
    Public Shared Function FileCopy(ByVal sourcePath As String, ByVal destinationPath As String) As Boolean

        Try
            ' Ensure that the target does not exist.
            If File.Exists(destinationPath) Then
                File.Delete(destinationPath)
            End If

            If File.Exists(sourcePath) Then
                ' Copy the file.
                File.Copy(sourcePath, destinationPath)
            End If
            Return True

        Catch ex As Exception
            ExceptionInfo.Show(ex)

        End Try
    End Function
    Public Shared Function FileMove(ByVal sourcePath As String, ByVal destinationPath As String) As Boolean

        Try
            ' Ensure that the target does not exist.
            If File.Exists(destinationPath) Then
                Dim extension As String
                extension = destinationPath.Substring(destinationPath.Length - 4)
                destinationPath = destinationPath & "_" & Hour(Now) & "_" & Minute(Now) & "_" & Second(Now) & "_" & Now.Millisecond & "_" & extension
            End If

            If File.Exists(sourcePath) Then
                ' Move the file.
                File.Move(sourcePath, destinationPath)
            End If
            Return True

        Catch ex As Exception
            ExceptionInfo.Show(ex)

        End Try
    End Function
    Public Shared Function FileSize(ByVal sourcePath As String) As Long
        Dim attrib As New FileItem(sourcePath, False)

        Try

            Return attrib.FileSize


        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Return -1
        End Try
    End Function
    Public Function ReadFileToEnd(ByVal strFilename As String) As String
        Try
            Dim myFile As File
            Dim sr As StreamReader = myFile.OpenText(strFilename)
            Dim input As String

            If myFile.Exists(strFilename) Then
                input = sr.ReadToEnd
                sr.Close()
            End If
            Return input

        Catch ex As Exception
        Finally

        End Try
    End Function
    Public Shared Function FileUpload(ByVal File_Save_Location As String, ByVal Control_ID As System.Web.UI.HtmlControls.HtmlInputFile) As String

        'Get File Upload Control------------
        Dim FileLoader As System.Web.HttpPostedFile = Control_ID.PostedFile
        '-----------------------------------

        'Check to see if file has content---------------
        If FileLoader.ContentLength > 0 Then

            Dim File_Path As String = FileLoader.FileName
            Dim DotLocation1 As String = InStrRev(File_Path, ".", , 0)
            Dim BarLocation1 As String = InStrRev(File_Path, "\", , 0)
            Dim File_Ext As String = Mid(File_Path, DotLocation1, Len(File_Path))
            Dim File_Name As String = Mid(File_Path, BarLocation1 + 1, Len(File_Path))
            Dim Content_Type As String = FileLoader.ContentType
            Dim New_File_Path As String = File_Save_Location & File_Name

            Try

                'Save Upload File------------------------
                FileLoader.SaveAs(New_File_Path)
                '----------------------------------------

            Catch ex As Exception

                'Return Error---------
                Return ex.Message
                Exit Function
                '---------------------

            End Try

            'Get File Size------------------
            Dim File_Size_Actual As String = FileLen(New_File_Path)
            Dim File_Size As String = Size(File_Size_Actual)
            '-------------------------------

            'Return File Completed----------
            Return "File Upload Complete" & vbNewLine _
                 & "ClientFilePath: " & File_Path & vbNewLine _
                & "ServerFilePath: " & New_File_Path & vbNewLine _
                 & "FileSize: " & File_Size
            '   & "FileSizeActual: " & File_Size_Actual
            '-------------------------------

        Else

            'Return error message that file must have something----
            Return "File must be larger than 0 kbs"
            Exit Function
            '------------------------------------------------------

        End If
        '-----------------------------------------------

    End Function
    Private Shared Function Size(ByVal Bytes As Long) As String

        'Make Variables-------------------
        Dim NewData As Long
        '---------------------------------

        Select Case Bytes

            Case 0 To 1023                     'Bytes
                Return (Bytes & " B")
                Exit Function

            Case 1024 To 1048575               'KBytes
                Return (Format(Bytes / 1024, ".##") & " KB")
                Exit Function

            Case 1048576 To 1073741824         'MBytes
                Return (Format(Bytes / 1048576, ".##") & " MB")
                Exit Function

            Case Is > 1073741824               'GBytes
                Return (Format(Bytes / 1073741824, ".##") & " GB")
                Exit Function

        End Select

    End Function
    Public Shared Function DirListing(ByVal dirpath As String) As FileInfo()

        Dim MyDirInfo As New DirectoryInfo(dirpath)
        Dim name As String

        Try
            name = MyDirInfo.Name()
            Return MyDirInfo.GetFiles()

        Catch ex As Exception

        End Try
    End Function

    ''' <summary>For downloading files from server to client via browser. File_Name is the file name and extension. File_Path is path to the downloadable file on the server. BlockSize is the size of data you want to send before a flush.</summary>
    'Public Function DownloadFile(ByVal File_Name As String, ByVal File_Path As String, ByVal BlockSize As Integer) As String

    '    'Make Correct Path----------------------------------
    '    Dim Complete_Path As String = File_Path & File_Name
    '    '---------------------------------------------------

    '    'Check to see if file exists-----
    '    If System.IO.File.Exists(Complete_Path) = False Then
    '        Context.Response.Write("File does not exist")
    '        Return "File does not exist"
    '        Exit Function
    '    End If
    '    '--------------------------------

    '    'Make Variables--------------
    '    Dim BlockOf As Integer = BlockSize
    '    Dim CountOf As Integer = 0
    '    Dim fso As New FileSystemObject
    '    Dim fileObject As Scripting.File = fso.GetFile(Complete_Path)
    '    Dim SizeOf As Long = fileObject.Size
    '    '----------------------------

    '    'Header and Cache---------------------------------
    '    Context.Response.Expires = 0
    '    Context.Response.Buffer = True
    '    Context.Response.Clear()
    '    Context.Response.ContentType = "application/octet-stream"
    '    Context.Response.AddHeader("content-disposition", "attachment; filename=" & File_Name)
    '    Context.Response.AddHeader("Content-Length", SizeOf)
    '    Context.Response.Charset = "UTF-8"
    '    Context.Response.Flush()
    '    '-------------------------------------------------

    '    'Open Adodb stream to get chunks of file----------
    '    Dim BinaryStream As New ADODB.Stream
    '    BinaryStream.Open()
    '    BinaryStream.Type = 1
    '    BinaryStream.LoadFromFile(Complete_Path)
    '    '-------------------------------------------------

    '    'Loop through blocks of data and send-------------
    '    While SizeOf > BlockOf + CountOf
    '        CountOf = CountOf + BlockOf
    '        Context.Response.BinaryWrite(BinaryStream.Read(BlockOf))
    '        context.Response.Flush()
    '    End While
    '    '-------------------------------------------------

    '    'Read Last Block of bytes and send----------------
    '    Context.Response.BinaryWrite(BinaryStream.Read(SizeOf - CountOf))
    '    Context.Response.Flush()
    '    Context.Response.End()
    '    '-------------------------------------------------

    '    'Set to Nothing--------------
    '    BinaryStream = Nothing
    '    fso = Nothing
    '    '----------------------------

    'End Function

End Class
