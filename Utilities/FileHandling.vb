Imports System
Imports System.IO
Imports System.data
Imports System.Web
Imports System.Web.UI
Imports System.Security.Permissions
Imports Microsoft.Win32
'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
''' <summary><para>Some Helper Functions for FileHandling</para></summary>
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
    Public Overloads Shared Function FileMove(ByVal sourcePath As String, ByVal destinationPath As String) As Boolean

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
    Public Overloads Shared Function FileMove(ByVal sourcePath As String, ByVal destinationPath As String, ByVal wDate As Boolean) As Boolean

        Try
            Dim extension, filename, path As String
            extension = destinationPath.Substring(destinationPath.Length - 4)
            filename = destinationPath.Substring(destinationPath.LastIndexOf("\") + 1)
            path = destinationPath.Substring(0, destinationPath.LastIndexOf("\") + 1)
            If wDate = True Then

                Dim sday As String = Day(Today())
                Dim syear As String = Year(Today())
                Dim smonth As String = Month(Today())
                filename = syear & "_" & smonth & "_" & sday & "_" & filename
            End If
            destinationPath = path + filename

            ' Ensure that the target does not exist.
            If File.Exists(destinationPath) Then
                filename = filename & "_" & Hour(Now) & "_" & Minute(Now) & "_" & Second(Now) & "_" & Now.Millisecond & "_" & extension
            End If
            destinationPath = path + filename
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
    Public Shared Function createTextFile(ByVal strFileName As String, ByVal strData As String) As Boolean
        Dim myFile As File
        Try
           
            Dim fs As New FileStream(strFileName, FileMode.Create, FileAccess.Write)
            Dim s As New StreamWriter(fs)
            s.Write(strData)
            s.Close()
            Return True

        Catch ex As Exception
            Return False
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
        Try
            Dim MyDirInfo As New DirectoryInfo(dirpath)
            Dim name As String
            name = MyDirInfo.Name()

            Dim myLog As New Wyeth.Utilities.Log

            Return MyDirInfo.GetFiles()

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        End Try

    End Function
    Public Shared Function DirListing(ByVal dirpath As String, ByVal searchPattern As String) As FileInfo()
        Try
            Dim MyDirInfo As New DirectoryInfo(dirpath)
            Dim name As String
            name = MyDirInfo.Name()

            Dim myLog As New Wyeth.Utilities.Log

            Return MyDirInfo.GetFiles(searchPattern)

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        End Try
    End Function
End Class