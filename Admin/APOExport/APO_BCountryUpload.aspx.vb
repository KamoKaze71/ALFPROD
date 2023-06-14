
Imports Wyeth.Utilities

Public Class APO_BCountryUpload
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents chkReports As System.Web.UI.WebControls.CheckBoxList
    Protected WithEvents lblOut As System.Web.UI.WebControls.Label
    Protected WithEvents txtProgress As System.Web.UI.WebControls.TextBox
    Protected WithEvents btn_upload As System.Web.UI.WebControls.Button
    Protected WithEvents MyFile As System.Web.UI.HtmlControls.HtmlInputFile

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim MyCollection As Hashtable = WyethImportHelper.readAppVars()

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
    End Sub

    Private Sub btn_upload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_upload.Click
        Dim rowCount As Integer = 0
        Dim filename As String
        Dim importResult As String
        Try


            Wyeth.Utilities.FileHandling.FileUpload(MyCollection("SanovaImportFilePath") & MyCollection("APOBCountryFileArchivePath") & "\", MyFile)

            filename = MyCollection("SanovaImportFilePath") & MyCollection("APOBCountryFileArchivePath") & "\" & MyFile.Value.Substring(MyFile.Value.LastIndexOf("\") + 1)
            Dim éxcelData As New Wyeth.Utilities.ExcelConnection(filename)

            Dim myDS As DataTable = éxcelData.GetData(MyCollection("APOBCountryExcelSheetRange")).Tables(0)

            Dim bc As Wyeth.Alf.APO_BCountry
            Try

                bc = New Wyeth.Alf.APO_BCountry

                For Each r As DataRow In myDS.Rows()
                    rowCount = rowCount + 1
                    bc.InsertItem(r.ItemArray())
                Next

                importResult = bc.APOB_Country_SartImportFromTempTables()

            Catch ex As Exception
                ExceptionInfo.Show(ex)
                Me.lblOut.Text = "<font color=red>An error ocurred while uploading the Data <BR>"
                Me.lblOut.Text = "Uploaded " & rowCount & " rows sucessfully<BR>"
                Me.lblOut.Text = ex.Message + "<\font>"
            Finally

            End Try


            Me.lblOut.Text = "Uploaded " & rowCount & " rows sucessfully<BR>" & importResult

        Catch ex As Exception
            Me.lblOut.Text = "Please specify a file! <br>"
            Me.lblOut.Text = filename + "<br>"
            Me.lblOut.Text = ex.Source.ToString() + "<BR>"
            Me.lblOut.Text = ex.Message.ToString()
        End Try

    End Sub
End Class
