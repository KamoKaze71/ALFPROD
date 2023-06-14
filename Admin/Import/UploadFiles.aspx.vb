Imports Wyeth.Utilities.DateHandling
Imports Wyeth.Alf.WyethDropdown
Imports Wyeth.Utilities
Imports Wyeth.Utilities.FileHandling
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls

Public Class UploadFiles
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btn_upload As System.Web.UI.WebControls.Button
    Protected WithEvents MyFile As HtmlInputFile
    Protected WithEvents lblOut_manual As Label
    Protected WithEvents btn_cancel As System.Web.UI.HtmlControls.HtmlButton
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region


    Dim MyCollection As New Collection
    Dim dist_name As String = "SANOVA"
    Dim myJs As New JSPopUp(Me)
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        MyCollection = Application("AppSetting")
        Me.ALFPageTitle = "Upload Import Files"

        If Request.QueryString("dist_name") <> String.Empty Then
            dist_name = Request.QueryString("dist_name")
        End If
      

        WyethImport.displayImportFiles(dist_name, Me.lblOut_manual, MyCollection("SanovaImportFilePath"))

    End Sub

    Private Sub btn_upload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_upload.Click

        Select Case dist_name.ToUpper

            Case "PHARMOSAN"
                If MyFile.PostedFile.FileName.ToString.EndsWith("txt") = False Then
                    Me.lblOut_manual.Text = "<font color=red>" & MyFile.PostedFile.FileName & " is not a valid File!<bR>"
                    lblOut_manual.Text = lblOut_manual.Text & "<Font color=red><br> There are no files in the Server Directory which will be imported<br>"
                    lblOut_manual.Text = lblOut_manual.Text & "Notes:<br> "
                    lblOut_manual.Text = lblOut_manual.Text & "--> ALF will only import valid " & dist_name & " import Files:<br> "
                    lblOut_manual.Text = lblOut_manual.Text & "--> The File type must be TAB deltimited TextFile -> Save as Text (Tab delimited) in Excel! "
                    lblOut_manual.Text = lblOut_manual.Text & "--> The file extension must be '.txt'!</font><BR>"

                ElseIf WyethImport.CountImportFilesOnServer(dist_name, MyCollection("SanovaImportFilePath")) > 0 Then
                    Me.lblOut_manual.Text = " Only one file per Import Session is allowed!"
                Else
                    Wyeth.Utilities.FileHandling.FileUpload(MyCollection("SanovaImportFilePath"), MyFile)
                    WyethImport.displayImportFiles(dist_name, Me.lblOut_manual, MyCollection("SanovaImportFilePath"))
                End If

            Case "SANOVA"
                If MyFile.PostedFile.FileName.ToString.ToUpper.EndsWith("DAT") = False Then
                    Me.lblOut_manual.Text = MyFile.PostedFile.FileName & " is not a valid File!<bR>"
                Else
                    Wyeth.Utilities.FileHandling.FileUpload(MyCollection("SanovaImportFilePath"), MyFile)
                    WyethImport.displayImportFiles(dist_name, Me.lblOut_manual, MyCollection("SanovaImportFilePath"))
                End If

        End Select

      

        MyCollection = Application("AppSetting")



    End Sub

    'Private Sub displayImportFiles(ByVal filetype As String)
    '    Try
    '        If filetype = "SANOVA" Then
    '            Dim icount As Integer = 0
    '            lblOut_manual.Text = "Files that will be imported:<Font color=green><BR>" & vbNewLine
    '            For Each MyInfo As System.IO.FileInfo In DirListing(MyCollection("SanovaImportFilePath"))
    '                If MyInfo.FullName.EndsWith(".dat") And (MyInfo.FullName.IndexOf("KD") > 0 Or MyInfo.FullName.IndexOf("ART") > 0 Or MyInfo.FullName.IndexOf("BW") > 0) Then
    '                    lblOut_manual.Text = lblOut_manual.Text & MyInfo.FullName & "<br>"
    '                    icount = icount + 1
    '                End If
    '            Next
    '            lblOut_manual.Text = lblOut_manual.Text & "</Font>"

    '            If icount = 0 Then
    '                lblOut_manual.Text = lblOut_manual.Text & "<Font color=red><br> There are no files in the directory at the server that will be imported<br>"
    '                lblOut_manual.Text = lblOut_manual.Text & "ALF will only import valid Sanova import Files:<br> "
    '                lblOut_manual.Text = lblOut_manual.Text & "ALF will only recognize Sanova Import file when the filename contains a 'BW' OR' KD' OR 'ART'. The file extension must be '.dat'!</font>"
    '            End If
    '        ElseIf filetype = "PHARMOSAN" Then

    '            Dim icount As Integer = 0
    '            lblOut_manual.Text = "Files that will be imported:<Font color=green><BR>" & vbNewLine
    '            For Each MyInfo As System.IO.FileInfo In DirListing(MyCollection("SanovaImportFilePath"))
    '                If MyInfo.FullName.EndsWith(".csv") Then
    '                    lblOut_manual.Text = lblOut_manual.Text & MyInfo.FullName & "<br>"
    '                    icount = icount + 1
    '                End If
    '            Next
    '            lblOut_manual.Text = lblOut_manual.Text & "</Font>"

    '            If icount = 0 Then
    '                lblOut_manual.Text = lblOut_manual.Text & "<Font color=red><br> There are no files in the directory at the server that will be imported<br>"
    '                lblOut_manual.Text = lblOut_manual.Text & "ALF will only import Files:<br> "
    '                lblOut_manual.Text = lblOut_manual.Text & "ALF will only recognize Pharmosan Import file when the '.csv'!</font>"
    '            End If

    '        End If

    '    Catch ex As Exception
    '        lblOut_manual.Text = lblOut_manual.Text & vbNewLine & "No files found in " & MyCollection("SanovaImportFilePath")
    '    End Try
    'End Sub

    Private Sub btn_cancel_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cancel.ServerClick
        myJs.ClosePopUpAndRefreshOpener()
    End Sub
End Class
