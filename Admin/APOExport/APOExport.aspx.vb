Imports Wyeth.Utilities
' Change Request 07-AT-0005 
Public Class APOExport
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents chkReports As System.Web.UI.WebControls.CheckBoxList
    Protected WithEvents btn_upload As System.Web.UI.WebControls.Button
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents StartImage As System.Web.UI.WebControls.Image
    Protected WithEvents txtStartDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents lblOut As System.Web.UI.WebControls.Label
    Protected WithEvents txtProgress As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region


    Dim myPopUp As New JSPopUp(Me)
    Dim expView As New DataView
    Dim strStep As String
    Dim stringWriteLog = New System.IO.StringWriter
    Dim htmlWriteLog = New System.Web.UI.HtmlTextWriter(stringWriteLog)
    Dim MyCollection As New Hashtable

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        myPopUp.PageURL = "../../util/datepicker.aspx"
        myPopUp.AddDatePopupToControl(txtStartDate, StartImage)
        expView = Wyeth.Alf.WyethImportHelper.getExportReports()


        If Page.IsPostBack = False Then
            MyCollection = Wyeth.Alf.WyethImportHelper.readAppVars()

            For Each dr As DataRow In expView.Table.Rows
                Dim li As New ListItem
                li.Value = dr.Item("rpt_queryName")
                li.Text = dr.Item("rpt_name")
                chkReports.Items.Add(li)
            Next
        Else
            strStep = Request.Form("txtProgress").ToString
            ImportProgress()
        End If

    End Sub

    Private Sub btn_upload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_upload.Click
        Validate()
        If Me.IsValid Then
            Wyeth.Alf.WyethImportHelper.ClearDir(myCollection("ExportDir"))
            Me.txtProgress.Text = "step1"
            Me.lblOut.Text = "Start building export File(s)...."
        End If

    End Sub

    Private Sub logOut(ByVal strlog As String)
        strlog = strlog.Replace(vbNewLine, "<BR>")
        Me.lblOut.Text = Me.lblOut.Text & strlog

    End Sub

    Private Sub ImportProgress()
        Try
            Select Case strStep
                Case "step1"
                    For Each it As ListItem In chkReports.Items
                        If it.Selected = True Then
                            Server.Execute("APOExportBuildFiles.aspx?startdate=" & Me.txtStartDate.Text & "&rpt_queryname=" & it.Value, stringWriteLog)
                        End If
                    Next
                    Me.txtProgress.Text = "step2"
                    logOut(stringWriteLog.ToString)

                Case "step2"

                    Server.Execute("APOExportUploadFiles.aspx", stringWriteLog)
                    logOut(stringWriteLog.ToString)
                    Me.txtProgress.Text = "none"

            End Select
        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Me.txtProgress.Text = "none"
            Me.lblOut.Text = Me.lblOut.Text & ex.Message.ToString
        Finally
        End Try
    End Sub
End Class
