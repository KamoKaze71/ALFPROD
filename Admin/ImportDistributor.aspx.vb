Imports Wyeth.Utilities.DateHandling
Imports Wyeth.Alf.WyethDropdown

Public Class ImportDistributor
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lb_ftpfiles As System.Web.UI.WebControls.ListBox
    Protected WithEvents btn_Import As System.Web.UI.WebControls.Button
    Protected WithEvents lblOut As System.Web.UI.WebControls.Label
    Protected WithEvents auto_panel As System.Web.UI.WebControls.Panel
    Protected WithEvents txtPath As System.Web.UI.WebControls.TextBox
    Protected WithEvents manual_panel As System.Web.UI.WebControls.Panel
    Protected WithEvents btn_manual_import As System.Web.UI.WebControls.Button
    Protected WithEvents dddistribSelect As System.Web.UI.WebControls.DropDownList
    Protected WithEvents FilterPanel As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents rbl_AutomaticUpdate As System.Web.UI.WebControls.RadioButtonList
    Protected WithEvents btn_connect As System.Web.UI.WebControls.Button
    Protected WithEvents lblOut_manual As System.Web.UI.WebControls.Label
    Protected WithEvents btn_delete_transmission As System.Web.UI.WebControls.Button
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
    Dim MyImport As New WyethImport
    Dim MyDelTranPop As New JSPopUp(Me)


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            If Page.IsPostBack Then
                lblOut.Text = ""
                lblOut_manual.Text = ""
            Else

                Me.manual_panel.Visible = False
                fillDisribDD()
            End If
            MyDelTranPop.PageURL = "DeleteTransmission.aspx?dist_id=" & dddistribSelect.SelectedValue
            MyDelTranPop.Width = 350
            MyDelTranPop.Height = 200
            MyDelTranPop.AddPopupToControl(Me.btn_delete_transmission)

        Catch ex As Exception
            lblOut.Text = ex.Message.ToString
        End Try

    End Sub
    Private Sub fillFilesLB()
        Dim files() As Wyeth.Utilities.FileItem
        Dim i As Integer = 0
        lb_ftpfiles.SelectionMode = ListSelectionMode.Multiple
        files = MyImport.GetFtpFilesList()

        For Each File As Wyeth.Utilities.FileItem In files
            Dim li As New ListItem
            li.Value = File.FileTitle
            li.Text = FormatDate(File.FileDate) & "_" & File.FileTitle.ToString.Substring(0, li.Value.ToString.LastIndexOf(".")) & ".DAT"
            lb_ftpfiles.Items.Insert(i, li)
            i = i + 1
        Next
        lb_ftpfiles.DataBind()
        listdescend(lb_ftpfiles)
    End Sub
    Private Sub fillDisribDD()
        GetDistribSelectDD(dddistribSelect, Session("country_id"))
    End Sub
    Private Sub listdescend(ByRef box As ListBox)
        'sorts listbox descending
        Dim array1 As New ArrayList
        Dim loop1 As Integer
        For loop1 = 0 To box.Items.Count - 1
            array1.Add(box.Items(loop1).Text & "|" & box.Items(loop1).Value)
        Next
        array1.Sort()
        box.Items.Clear()
        For loop1 = array1.Count - 1 To 0 Step -1
            Dim li As New ListItem
            Dim tmp As Array
            tmp = CStr(array1(loop1)).Split("|")
            li.Text = tmp(0)
            li.Value = tmp(1)
            box.Items.Add(li)
        Next
    End Sub
    Private Sub btn_Import_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Import.Click
        Try
            MyImport.GetFtpFiles(Me.lb_ftpfiles)
            lblOut.Text = MyImport.strLog
        Catch ex As Exception
            lblOut.Text = lblOut.Text & vbNewLine & vbNewLine & vbNewLine & ex.Message.ToString
        Finally
            lblOut.Text = lblOut.Text.Replace(vbNewLine, "<BR>")
            MyImport.strLog = ""
        End Try

    End Sub

    Private Sub btn_manual_import_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_manual_import.Click
        Try
            MyImport.ImportManual(Me.txtPath.Text.Trim())
        Catch ex As Exception
            lblOut.Text = lblOut.Text & vbNewLine & vbNewLine & vbNewLine & ex.Message.ToString
        Finally
            lblOut.Text = MyImport.strLog
            lblOut.Text = lblOut.Text.Replace(vbNewLine, "<BR>")
            MyImport.strLog = ""
        End Try
    End Sub

    Private Sub rbl_AutomaticUpdate_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbl_AutomaticUpdate.SelectedIndexChanged
        If Me.rbl_AutomaticUpdate.SelectedValue = "automatic" Then
            Me.auto_panel.Visible = True
            Me.manual_panel.Visible = False
            Me.lb_ftpfiles.Visible = False

        Else

            Dim MyCollection As New Collection
            MyCollection = Application("AppSetting")
            Me.txtPath.Text = MyCollection("SanovaImportFilePath")

            Me.auto_panel.Visible = False
            Me.manual_panel.Visible = True
        End If
    End Sub

    Private Sub btn_connect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_connect.Click
        MyImport.strLog = ""
        Me.lb_ftpfiles.Visible = True
        fillFilesLB()
        Me.btn_Import.Visible = True
        Me.lblOut.Text = MyImport.strLog
        lblOut.Text = lblOut.Text.Replace(vbNewLine, "<BR>")
        MyImport.strLog = ""
    End Sub

    Private Sub btn_delete_transmission_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_delete_transmission.Click
       
    End Sub
End Class
