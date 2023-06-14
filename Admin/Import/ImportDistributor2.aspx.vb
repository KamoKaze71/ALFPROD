Imports Wyeth.Utilities.DateHandling
Imports Wyeth.Alf.WyethDropdown
Imports Wyeth.Utilities
Imports Wyeth.Utilities.FileHandling
Imports Wyeth.Utilities.NumberFormat
Imports Wyeth.Alf.WyethAppllication
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls

Public Class ImportDistributor2
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lb_ftpfiles As System.Web.UI.WebControls.ListBox
    Protected WithEvents btn_Import As System.Web.UI.WebControls.Button
    Protected WithEvents lblOut As System.Web.UI.WebControls.Label
    Protected WithEvents auto_panel As System.Web.UI.WebControls.Panel
    Protected WithEvents btn_manual_import As System.Web.UI.WebControls.Button
    Protected WithEvents dddistribSelect As System.Web.UI.WebControls.DropDownList
    Protected WithEvents FilterPanel As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents rbl_AutomaticUpdate As System.Web.UI.WebControls.RadioButtonList
    Protected WithEvents btn_connect As System.Web.UI.WebControls.Button
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents filMyFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents txtProgress As System.Web.UI.WebControls.TextBox
    Protected WithEvents btn_add_broken_bw As System.Web.UI.WebControls.Button
    Protected WithEvents btn_upload As System.Web.UI.WebControls.Button
    Protected WithEvents tbl_import As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents btn_delete_transmission As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents btn_load_into_tempTable As System.Web.UI.WebControls.Button
    Protected WithEvents btn_view_ftp_Files As System.Web.UI.WebControls.Button
    Protected WithEvents TempTableButtons1 As System.Web.UI.UserControl
    Protected WithEvents btn_import_pharmosan As System.Web.UI.WebControls.Button
    Protected WithEvents btnConnectMuenster As System.Web.UI.WebControls.Button
    Protected WithEvents btn_importmuenster As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim myStatus As New AlfStatus
    Dim MyImport As New WyethImport
    Dim MyDelTranPop As New JSPopUp(Me)
    Dim stringWriteLog = New System.IO.StringWriter
    Dim htmlWriteLog = New System.Web.UI.HtmlTextWriter(stringWriteLog)
    Dim strStep As String = "none"
    Dim timestart, timeEnd As Date
    Dim manualImport As Boolean = False
    Dim LoadIntoTempTables As Boolean = False
    Dim brokenBW As Boolean = False
    Dim MyCollection As New Collection
    Dim importFilesOnServer As Integer = 0
    Dim isConnected As Boolean = False
    Dim dist_name As String = "SANOVA"
    Dim dist_id As Integer
    Dim importHitoricMuensterData As Boolean = False


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        MyCollection = Application("AppSetting")

        If Session("Import") = "manual" Then
            manualImport = "True"
            dist_name = "Sanova"
        ElseIf Session("Import") = "automatic" Then
            manualImport = "False"
            dist_name = "Sanova"
        ElseIf Session("import") = "PHARMOSAN" Then
            dist_name = "Pharmosan"
        ElseIf Session("import") = "MUENSTER" Then
            dist_name = "MÜNSTER"
        Else
            dist_name = "Sanova"
            Session("Import") = "automatic"
        End If

        dist_id = MyImport.GetDistributorID(dist_name)
        Try
            If Page.IsPostBack Then


                LoadIntoTempTables = Me.ViewState("LoadInTempTables")
                brokenBW = Me.ViewState("BrokenBW")
                isConnected = Me.ViewState("isConnected")
                '   dist_name = Me.ViewState.Item("dist_name")
                '  dist_id = Me.ViewState.Item("dist_id")

                strStep = Request.Form("txtProgress").ToString


                If LoadIntoTempTables = True Then
                    ImportProgressTempTable()
                ElseIf brokenBW = True Then
                    ImportProgressBrokenBW()
                ElseIf brokenBW = False And LoadIntoTempTables = False And Session("import") = "PHARMOSAN" Then
                    ImportProgressPharmosan()
                Else
                    ImportProgress()
                End If

            Else
                Dim strMessage As String
                strMessage = "This will import all valid import files in the server directory! this will include update with Forte Data and MV-refresh"
                MyDelTranPop.ConfirmMessage = strMessage
                MyDelTranPop.AddGetConfirm(Me.btn_manual_import)


                strMessage = "This will import all valid import files in the server directory!"
                MyDelTranPop.ConfirmMessage = strMessage
                MyDelTranPop.AddGetConfirm(Me.btn_import_pharmosan)

                strMessage = "This will load all valid import files in the server directory into ALFs temp tables. No MV-Refresh and no updatde with sales data!"
                MyDelTranPop.ConfirmMessage = strMessage
                MyDelTranPop.AddGetConfirm(Me.btn_load_into_tempTable)

                If myStatus.GetNumberTempTableRecords() > 0 And Session("import") <> "PHARMOSAN" Then
                    lblOut.Text = "<Font color=red>ALF has detected sales data in the temp table! This might be due to a missing customer or product!<br>Please check the 'Import Logs' for errors  - import the missing customer or product data and then press the 'Add broken BW' Button to append the data to the transmission!"
                    btn_add_broken_bw.Visible = True
                End If

                fillDisribDD()
                MyImport.displayImportFiles(dist_name, Me.lblOut, MyCollection("SanovaImportFilePath"))

            End If



        Catch ex As Exception
            lblOut.Text = ex.Message.ToString
        End Try

    End Sub
    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
        setLastOrderEntry()
        If Session("import") = "manual" Then
            setManualUpdateLayout()
        ElseIf Session("import") = "automatic" Then
            setAutomaticUpdateLayout()
        ElseIf Session("import") = "PHARMOSAN" Or dist_name.ToUpper = "PHARMOSAN" Then
            setPharmosanLayout()
        ElseIf Session("import") = "MUENSTER" Then
            setmuensterLayout()
        End If

        setPopUps()

    End Sub

   
    Private Sub btn_load_into_tempTable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_load_into_tempTable.Click
        Try
            Me.txtProgress.Text = "step2"

            outputformat("2.) Now starting SQL Loader  ..... </font><BR>")
            Me.ViewState.Add("timestart", Now())
            Me.ViewState.Add("ManualImport", "TRUE")
            Me.ViewState.Add("LoadInTempTables", "TRUE")

        Catch ex As Exception
            lblOut.Text = lblOut.Text & "<BR>" & ex.Message.ToString
        Finally

        End Try
    End Sub
    Private Sub btn_add_broken_bw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_add_broken_bw.Click
        Try
            lblOut.Text = ""
            Me.txtProgress.Text = "step2.2"
            outputformat("1.) Now fetching Forte data ......")
            Me.ViewState.Add("timestart", Now())
            Me.ViewState.Add("manualImport", "true")
            Me.ViewState.Add("LoadInTempTables", "false")
            Me.ViewState.Add("BrokenBW", "True")

        Catch ex As Exception
            lblOut.Text = lblOut.Text & "<BR>" & ex.Message.ToString
        Finally

        End Try
    End Sub
    Private Sub btn_import_pharmosan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_import_pharmosan.Click
        Try
            Me.txtProgress.Text = "step2"
            Me.lblOut.Text = ""


            Session("import") = "PHARMOSAN"
            dist_name = "Pharmosan"
            viewstate.Add("dist_name", dist_name)
            outputformat("1.) Now starting SQL Loader  ..... </font><BR>")
            Me.ViewState.Add("timestart", Now())
            Me.ViewState.Add("ManualImport", "false")
            Me.ViewState.Add("LoadInTempTables", "false")

        Catch ex As Exception
            lblOut.Text = lblOut.Text & "<BR>" & ex.Message.ToString
            txtProgress.Text = "none"
        Finally

        End Try

    End Sub
    Private Sub btn_Import_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Import.Click
        Try

            Me.txtProgress.Text = "step0"
            ' lblOut.Text = "<font color=green size=larger> 1.) Now downloading selected FTP Files  ..... </font><BR> <X> "


            Session("ImportLB") = lb_ftpfiles
            Me.ViewState.Add("timestart", Now())
            'Me.ViewState.Add("manualImport", "false")
            Session("import") = "automatic"

            Dim myrow As System.Web.UI.HtmlControls.HtmlTableRow
            Dim mycell As System.Web.UI.HtmlControls.HtmlTableCell
            myrow = Me.tbl_import.Rows(1)
            mycell = myrow.Cells(0)
            mycell.Visible = False
            mycell = myrow.Cells(1)
            mycell.ColSpan = 2

            Wyeth.Alf.WyethImportHelper.ClearDir(MyCollection("SanovaImportFilePath"))


        Catch ex As Exception
            lblOut.Text = lblOut.Text & vbNewLine & vbNewLine & vbNewLine & ex.Message.ToString
        Finally

        End Try


    End Sub
    Private Sub btn_manual_import_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_manual_import.Click
        Try
            Me.txtProgress.Text = "step2"

            lblOut.Text = ""
            outputformat("2.) Now starting SQL Loader  ..... </font><BR>")
            Me.ViewState.Add("timestart", Now())

        Catch ex As Exception
            lblOut.Text = lblOut.Text & "<BR>" & ex.Message.ToString
        Finally

        End Try
    End Sub
    Private Sub btn_view_ftp_files_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_view_ftp_Files.Click
        Try
            Dim myImport As New WyethImport
            Dim myfile As New Wyeth.Utilities.FileHandling

            Session("ImportLB") = lb_ftpfiles
            myImport.DownloadFtpFiles(CType(Session("ImportLB"), ListBox))


            For Each MyInfo As System.IO.FileInfo In DirListing(MyCollection("SanovaImportFilePath"))

                Me.Response.Clear()
                Me.Response.Charset = ""
                Me.Response.ContentEncoding = System.Text.Encoding.GetEncoding(1252)
                Me.Response.ContentType = "text/plain"
                Me.Response.AddHeader("Content-Disposition", "attachment; filename=" & myinfo.name)
                Me.Response.Write(myfile.ReadFileToEnd(myinfo.FullName))
                Me.Response.Buffer = True
                Me.Response.BufferOutput = True
                Me.Response.End()


            Next


        Catch ex As Exception
        Finally
            'delete files 
            For Each MyInfo As System.IO.FileInfo In DirListing(MyCollection("SanovaImportFilePath"))
                File.Delete(MyInfo.FullName)
            Next
        End Try

    End Sub
    Private Sub btn_connect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_connect.Click

        Dim myrow As System.Web.UI.HtmlControls.HtmlTableRow
        Dim mycell As System.Web.UI.HtmlControls.HtmlTableCell
        myrow = Me.tbl_import.Rows(1)
        mycell = myrow.Cells(0)
        mycell.Visible = True
        mycell = myrow.Cells(1)
        mycell.ColSpan = 1
        MyImport.strLog = ""
        Me.lb_ftpfiles.Visible = True
        Me.lb_ftpfiles.Items.Clear()
        fillFilesLB()
        Me.btn_view_ftp_Files.Visible = True
        Me.btn_Import.Visible = True
        Me.lblOut.Text = MyImport.strLog
        lblOut.Text = lblOut.Text.Replace(vbNewLine, "<BR>")
        MyImport.strLog = ""
        isConnected = True
        Me.ViewState.Add("isConnected", "TRUE")
    End Sub


    Private Sub setLastOrderEntry()
        Application.Lock()
        ' Holt datum des Letzetn Order Entry
        Application("LastOrderEntry") = Convert.ToDateTime(WyethAppllication.getLastOrderEntry(Today()))
        Application.Lock()
    End Sub
    Private Sub setManualUpdateLayout()

        Dim myrow As System.Web.UI.HtmlControls.HtmlTableRow
        Dim mycell As System.Web.UI.HtmlControls.HtmlTableCell
        myrow = Me.tbl_import.Rows(1)
        mycell = myrow.Cells(0)
        mycell.Visible = False
        mycell = myrow.Cells(1)
        mycell.ColSpan = 2

        Me.rbl_AutomaticUpdate.SelectedValue = "manual"

        Me.btn_upload.Visible = True
        Me.btn_manual_import.Visible = True
        Me.btn_load_into_tempTable.Visible = True
        Me.btn_Import.Visible = False
        Me.btn_connect.Visible = False
        Me.lb_ftpfiles.Visible = False
        Me.btn_view_ftp_Files.Visible = False
        Me.btn_import_pharmosan.Visible = False
        Me.rbl_AutomaticUpdate.Visible = True
    
        If Wyeth.Alf.WyethImport.CountImportFilesOnServer(dist_name, MyCollection("SanovaImportFilePath")) > 0 And Session("import") <> "PHARMOSAN" Then
            Me.btn_load_into_tempTable.Visible = True
            Me.btn_manual_import.Visible = True
        ElseIf Wyeth.Alf.WyethImport.CountImportFilesOnServer(dist_name, MyCollection("SanovaImportFilePath")) = 0 And Session("import") <> "PHARMOSAN" Then
            Me.btn_load_into_tempTable.Visible = False
            Me.btn_manual_import.Visible = False
        ElseIf Session("import") = "PHARMOSAN" Then
            Me.btn_load_into_tempTable.Visible = False
            Me.btn_manual_import.Visible = False


            Me.btn_add_broken_bw.Visible = False
            Me.btn_connect.Visible = False
            Me.btn_import_pharmosan.Visible = True
            Me.btn_load_into_tempTable.Visible = False
            Me.btn_manual_import.Visible = False
            Me.TempTableButtons1.Visible = False
            Me.rbl_AutomaticUpdate.Visible = False
        End If
    End Sub
    Private Sub setAutomaticUpdateLayout()
        If strStep = "none" Then
            Dim myrow As System.Web.UI.HtmlControls.HtmlTableRow
            Dim mycell As System.Web.UI.HtmlControls.HtmlTableCell
            myrow = Me.tbl_import.Rows(1)
            mycell = myrow.Cells(0)
            mycell.Visible = True
            mycell = myrow.Cells(1)
            mycell.ColSpan = 1
        Else
            Dim myrow As System.Web.UI.HtmlControls.HtmlTableRow
            Dim mycell As System.Web.UI.HtmlControls.HtmlTableCell
            myrow = Me.tbl_import.Rows(1)
            mycell = myrow.Cells(0)
            mycell.Visible = False
            mycell = myrow.Cells(1)
            mycell.ColSpan = 2
        End If

        Me.auto_panel.Visible = True
        If isConnected = False Then
            Me.lb_ftpfiles.Visible = False
        Else
            Me.lb_ftpfiles.Visible = True
        End If
        Me.btn_importmuenster.Visible = False
        Me.btnConnectMuenster.Visible = False
        Me.btn_importmuenster.Visible = False
        Me.btn_connect.Visible = True
        Me.btn_upload.Visible = False
        Me.btn_manual_import.Visible = False
        Me.btn_load_into_tempTable.Visible = False
        Me.rbl_AutomaticUpdate.SelectedValue = "automatic"
        Me.rbl_AutomaticUpdate.Visible = True
        Me.btn_import_pharmosan.Visible = False
        Me.TempTableButtons1.Visible = True
        Me.ViewState.Add("ManualImport", "FALSE")

    End Sub
    Private Sub setmuensterLayout()
        If strStep = "none" Then
            Dim myrow As System.Web.UI.HtmlControls.HtmlTableRow
            Dim mycell As System.Web.UI.HtmlControls.HtmlTableCell
            myrow = Me.tbl_import.Rows(1)
            mycell = myrow.Cells(0)
            mycell.Visible = True
            mycell = myrow.Cells(1)
            mycell.ColSpan = 1
        Else
            Dim myrow As System.Web.UI.HtmlControls.HtmlTableRow
            Dim mycell As System.Web.UI.HtmlControls.HtmlTableCell
            myrow = Me.tbl_import.Rows(1)
            mycell = myrow.Cells(0)
            mycell.Visible = False
            mycell = myrow.Cells(1)
            mycell.ColSpan = 2
        End If

        Me.lb_ftpfiles.SelectionMode = ListSelectionMode.Multiple
        Me.btn_Import.Visible = False
        Me.lb_ftpfiles.Visible = True
        Me.btn_Import.Visible = False
        Me.btn_upload.Visible = False
        Me.btn_manual_import.Visible = False
        Me.btn_importmuenster.Visible = True
        Me.btn_connect.Visible = False
        Me.btn_upload.Visible = False
        Me.btn_Import.Visible = False
        Me.btn_load_into_tempTable.Visible = False
        Me.rbl_AutomaticUpdate.SelectedValue = "automatic"
        Me.rbl_AutomaticUpdate.Visible = False
        Me.btn_import_pharmosan.Visible = False
        Me.TempTableButtons1.Visible = False
        Me.btnConnectMuenster.Visible = True
        Me.ViewState.Add("ManualImport", "FALSE")
    End Sub
    Private Sub setPharmosanLayout()

        Session("import") = "PHARMOSAN"

        Dim myrow As System.Web.UI.HtmlControls.HtmlTableRow
        Dim mycell As System.Web.UI.HtmlControls.HtmlTableCell
        myrow = Me.tbl_import.Rows(1)
        mycell = myrow.Cells(0)
        mycell.Visible = False
        mycell = myrow.Cells(1)
        mycell.ColSpan = 2

        Me.btn_importmuenster.Visible = False
        Me.btnConnectMuenster.Visible = False
        Me.btn_add_broken_bw.Visible = False
        Me.btn_connect.Visible = False
        Me.btn_import_pharmosan.Visible = True
        Me.btn_load_into_tempTable.Visible = False
        Me.btn_manual_import.Visible = False
        Me.TempTableButtons1.Visible = False
        Me.rbl_AutomaticUpdate.Visible = False
        Me.btn_upload.Visible = True


        Me.dddistribSelect.SelectedIndex = 1
        Me.dddistribSelect.SelectedItem.Value = dist_id


    End Sub

    Private Sub setPopUps()

        If dist_name.ToUpper = "PHARMOSAN" Or Session("import") = "PHARMOSAN" Then
            MyDelTranPop.PageURL = "DeleteTransmission.aspx?dist_id=" & dist_id
            MyDelTranPop.Width = 530
            MyDelTranPop.Height = 200
            MyDelTranPop.AddPopupToControl(Me.btn_delete_transmission)
        ElseIf dist_name.ToUpper = "SANOVA" Then
            MyDelTranPop.PageURL = "DeleteTransmission.aspx?dist_id=" & dist_id
            MyDelTranPop.Width = 400
            MyDelTranPop.Height = 200
            MyDelTranPop.AddPopupToControl(Me.btn_delete_transmission)
        ElseIf dist_name.ToUpper = "MÜNSTER" Then
            MyDelTranPop.PageURL = "DeleteTransmission.aspx?dist_id=" & dist_id
            MyDelTranPop.Width = 400
            MyDelTranPop.Height = 200
            MyDelTranPop.AddPopupToControl(Me.btn_delete_transmission)
        End If


        MyDelTranPop.PageURL = "UploadFiles.aspx?dist_name=" & dist_name.ToUpper
        MyDelTranPop.Width = 450
        MyDelTranPop.Height = 250
        MyDelTranPop.AddPopupToControl(Me.btn_upload)
    End Sub
    Private Sub fillFilesLB()
        Dim files() As Wyeth.Utilities.FileItem
        Dim i As Integer = 0
        lb_ftpfiles.SelectionMode = ListSelectionMode.Multiple
        lb_ftpfiles.Width = Unit.Pixel(350)
        files = MyImport.GetFtpFilesList()

        For Each File As Wyeth.Utilities.FileItem In files
            Dim li As New ListItem
            li.Value = File.FileTitle
            li.Text = FormatDateTime(File.FileDate) & "_" & File.FileTitle.ToString.Substring(0, li.Value.ToString.LastIndexOf(".")) & "_" & File.FileSize & "_bytes.DAT"
            lb_ftpfiles.Items.Insert(i, li)
            i = i + 1
        Next
        lb_ftpfiles.DataBind()
        listdescend(lb_ftpfiles)
    End Sub
    Private Sub fillDisribDD()
        GetDistribSelectDD(dddistribSelect, Session("country_id"))

        Try
            Dim tmp As String
            If IsNothing(Request.QueryString("PageTitle").ToUpper) = False Then
                tmp = Request.QueryString("PageTitle").ToUpper
                If tmp.ToUpper = "IMPORT PHARMOSAN" Then
                    dddistribSelect.SelectedItem.Text = "Pharmosan"
                    dddistribSelect.Enabled = False
                    Session("import") = "PHARMOSAN"
                    dist_name = "PHARMOSAN"
                    Me.ViewState.Add("dist_name", dist_name)
                End If
            End If

        Catch ex As Exception
        Finally
            dist_id = MyImport.GetDistributorID(dist_name)

        End Try


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
    Private Sub outputformat(Optional ByVal nextstep As String = "")
        Dim str As String

        If nextstep <> "" Then
            str = "<BR><Font color=green  size=larger>" & nextstep & "</Font><br><br> in progress....<X> "
            Me.lblOut.Text = str & Me.lblOut.Text.Replace(vbNewLine, "<BR>")
        End If

        If stringWriteLog.ToString <> "" Then

            str = stringWriteLog.ToString.Replace(vbNewLine, "<BR>")

            Me.lblOut.Text = Me.lblOut.Text.ToString.Replace("in progress....<X>", str) & "<BR>"
        End If

        If Me.txtProgress.Text = "none" Then
            Me.lblOut.Text = Me.lblOut.Text.ToString.Replace("in progress....<X>", "")
        End If

    End Sub
    Private Sub AddToLog(ByVal str As String)
        Me.lblOut.Text = Me.lblOut.Text.ToString.Replace("<X>", str & "<BR>")
    End Sub


    Private Sub dddistribSelect_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dddistribSelect.SelectedIndexChanged
        dist_name = dddistribSelect.SelectedItem.Text
        dist_id = dddistribSelect.SelectedValue

        Me.ViewState.Add("dist_name", dist_name.ToUpper)
        Me.ViewState.Add("dist_id", dist_id)

        Me.lblOut.Text = ""
        Me.txtProgress.Text = "none"

        If manualImport <> "true" Or Session("import") = "PHARMOSAN" Then
            MyImport.displayImportFiles(dist_name, Me.lblOut, MyCollection("SanovaImportFilePath"))
        End If

        If dist_name.ToUpper = "PHARMOSAN" Then
            Session("import") = "PHARMOSAN"
        ElseIf dist_name.ToUpper = "SANOVA" Then
            Session("import") = "automatic"
        ElseIf dist_name.ToUpper = "MÜNSTER" Then
            Session("import") = "MUENSTER"
        End If

    End Sub
    Private Sub rbl_AutomaticUpdate_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbl_AutomaticUpdate.SelectedIndexChanged
        If Me.rbl_AutomaticUpdate.SelectedValue = "automatic" Then
            Session("Import") = "automatic"
        Else
            Session("Import") = "manual"
            dist_name = "SANOVA"
            Me.ViewState.Add("dist_name", dist_name)
        End If
        Me.lblOut.Text = ""
    End Sub

    Private Sub ImportProgress()
        Try

            Select Case strStep
                Case "step0"

                    outputformat("1.) Now downloading selected FTP Files  ..... </font><BR>")
                    Me.txtProgress.Text = "step1"

                Case "step1"

                    Server.Execute("Import_FTP_Download.aspx", stringWriteLog)
                    outputformat()
                    stringWriteLog = ""
                    Me.txtProgress.Text = "step2"
                    outputformat("2.) Now starting SQL Loader ......")

                Case "step2"

                    Server.Execute("Import_SQLLoader.aspx?manual=" & manualImport, stringWriteLog)
                    stringWriteLog = stringWriteLog.ToString & "<X>"
                    outputformat()
                    stringWriteLog = ""

                    AddToLog(myStatus.GetNumberTempTableRecordsART() & " rows had been inserted into temp table")
                    AddToLog(myStatus.GetNumberTempTableRecordsKD() & " rows had been inserted into temp table")
                    AddToLog(myStatus.GetNumberTempTableRecords() & " rows had been inserted into temp table<br><br>")

                    Me.txtProgress.Text = "step2.1"
                    outputformat("3.) Now archiving files ......")


                Case "step2.1"

                    Server.Execute("Import_Archive_Files.aspx?manual=" & manualImport, stringWriteLog)
                    outputformat()
                    stringWriteLog = ""
                    outputformat("4.) Now fetching Forte data ......")
                    Me.txtProgress.Text = "step2.2"


                Case "step2.2"

                    Server.Execute("Import_DBUpdate_Forte.aspx", stringWriteLog)
                    outputformat()
                    stringWriteLog = ""
                    Me.txtProgress.Text = "step3"
                    outputformat("5.) Now updating Customer data ......")


                Case "step3"

                    Server.Execute("Import_DBUPdate_KD.aspx", stringWriteLog)
                    outputformat()
                    stringWriteLog = ""
                    Me.txtProgress.Text = "step4"
                    outputformat("6.) Now updating Product data ......")

                Case "step4"

                    Server.Execute("Import_DBUPdate_ART.aspx", stringWriteLog)
                    outputformat()
                    stringWriteLog = ""
                    Me.txtProgress.Text = "step5"
                    outputformat("7.) Now updating Sales data ......")


                Case "step5"

                    Server.Execute("Import_DBUPdate_BW.aspx", stringWriteLog)
                    outputformat()
                    stringWriteLog = ""
                    Me.txtProgress.Text = "step6"
                    outputformat("8.) Now refreshing MV's ......")


                Case "step6"

                    Server.Execute("Import_MVRefresh.aspx", stringWriteLog)
                    outputformat()
                    stringWriteLog = ""
                    Me.txtProgress.Text = "step7"
                    outputformat("9.) Now checking the stock for errors ......")

                Case "step7"
                    Dim str As String

                    Server.Execute("Import_StockCheck.aspx", stringWriteLog)
                    outputformat()
                    stringWriteLog = ""

                    Me.txtProgress.Text = "none"

                    If myStatus.GetNumberTempTableRecordsART > 0 Then
                        str = "<Font color=red>ALF could not insert all of the product data<br>" & myStatus.GetNumberTempTableRecordsART & "products are still in the temp table!<br>"
                    ElseIf myStatus.GetNumberTempTableRecordsKD() > 0 Then
                        str = "<Font color=red>ALF could not insert all of the customer data<br>" & myStatus.GetNumberTempTableRecordsKD & " customers are still in the temp table!<br>"
                    ElseIf myStatus.GetNumberTempTableRecords() > 0 Then
                        str = "<Font color=red>ALF could not insert all of the Sales Data! This could be due to a missing customer or product!<br>Please check the 'Import Error' log for errors and then start the 'add broken BW' procedure!<bR>"
                    End If
                    timeEnd = Now()
                    str += ".... Import done in " & MyNumberFormat((CInt(DateDiff(DateInterval.Second, CDate(Me.ViewState("timestart")), timeEnd)) / 60), 2) & " minutes !"
                    outputformat(str)


                    Exit Select
            End Select
        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Me.txtProgress.Text = "none"
            Me.lblOut.Text = Me.lblOut.Text & ex.Message.ToString
        Finally
            setLastOrderEntry()
        End Try
    End Sub
    Private Sub ImportProgressTempTable()
        Try

            Select Case strStep

                Case "step2"

                    Server.Execute("Import_SQLLoader.aspx?manual=" & manualImport, stringWriteLog)
                    stringWriteLog = stringWriteLog.ToString & "<X>"
                    outputformat()
                    stringWriteLog = ""

                    AddToLog(myStatus.GetNumberTempTableRecordsART() & " rows had been inserted into temp table")
                    AddToLog(myStatus.GetNumberTempTableRecordsKD() & " rows had been inserted into temp table")
                    AddToLog(myStatus.GetNumberTempTableRecords() & " rows had been inserted into temp table<br><br>")

                    Me.txtProgress.Text = "step2.1"
                    outputformat("3.) Now archiving files ......")


                Case "step2.1"

                    Server.Execute("Import_Archive_Files.aspx?manual=" & manualImport, stringWriteLog)
                    outputformat()
                    stringWriteLog = ""

                    Me.txtProgress.Text = "none"
                    Dim str As String
                    If myStatus.GetNumberTempTableRecordsART > 0 Then
                        str = "<Font color=red>ALF could not insert all of the product data<br>" & myStatus.GetNumberTempTableRecordsART & "products are still in the temp table!</font><br>"
                    ElseIf myStatus.GetNumberTempTableRecordsKD() > 0 Then
                        str = "<Font color=red>ALF could not insert all of the customer data<br>" & myStatus.GetNumberTempTableRecordsKD & " customers are still in the temp table!</font><br>"
                    ElseIf myStatus.GetNumberTempTableRecords() > 0 Then
                        str = "<Font color=red>ALF could not insert all of the Sales Data! This could be due to a missing customer or product!<br>Please check the 'Import Error' log for errors and then start the 'add broken BW' procedure!</font><bR>"
                    End If
                    timeEnd = Now()
                    str += ".... Import done in " & MyNumberFormat((CInt(DateDiff(DateInterval.Second, CDate(Me.ViewState("timestart")), timeEnd)) / 60), 2) & " minutes !"
                    outputformat(str)
                    Exit Select
            End Select
        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Me.txtProgress.Text = "none"
            Me.lblOut.Text = Me.lblOut.Text & ex.Message.ToString
        Finally
            Me.ViewState.Add("LoadInTempTables", "TRUE")
            setLastOrderEntry()
        End Try
    End Sub
    Private Sub ImportProgressBrokenBW()
        Try

            Select Case strStep

                Case "step2.2"

                    Server.Execute("Import_DBUpdate_Forte.aspx", stringWriteLog)
                    outputformat()
                    stringWriteLog = ""
                    Me.txtProgress.Text = "step3"
                    outputformat("2.) Now updating Customer data ......")


                Case "step3"

                    Server.Execute("Import_DBUPdate_KD.aspx", stringWriteLog)
                    outputformat()
                    stringWriteLog = ""
                    Me.txtProgress.Text = "step4"
                    outputformat("3.) Now updating Product data ......")

                Case "step4"

                    Server.Execute("Import_DBUPdate_ART.aspx", stringWriteLog)
                    outputformat()
                    stringWriteLog = ""
                    Me.txtProgress.Text = "step5"
                    outputformat("4.) Now updating Sales data (broken BW)......")


                Case "step5"

                    Server.Execute("Import_DBUPdate_BrokenBW.aspx", stringWriteLog)
                    outputformat()
                    stringWriteLog = ""
                    Me.txtProgress.Text = "step6"
                    outputformat("5.) Now refreshing MV's ......")


                Case "step6"

                    Server.Execute("Import_MVRefresh.aspx", stringWriteLog)
                    outputformat()
                    stringWriteLog = ""
                    Me.txtProgress.Text = "step7"
                    outputformat("6.) Now checking the stock for errors ......")

                Case "step7"
                    Dim str As String

                    Server.Execute("Import_StockCheck.aspx", stringWriteLog)
                    outputformat()
                    stringWriteLog = ""

                    Me.txtProgress.Text = "none"

                    If myStatus.GetNumberTempTableRecordsART > 0 Then
                        str = "<Font color=red>ALF could not insert all of the product data<br>" & myStatus.GetNumberTempTableRecordsART & "products are still in the temp table!<br></font>"
                    ElseIf myStatus.GetNumberTempTableRecordsKD() > 0 Then
                        str = "<Font color=red>ALF could not insert all of the customer data<br>" & myStatus.GetNumberTempTableRecordsKD & " customers are still in the temp table!<br></font>"
                    ElseIf myStatus.GetNumberTempTableRecords() > 0 Then
                        str = "<Font color=red>ALF could not insert all of the Sales Data! This could be due to a missing customer or product!<br>Please check the 'Import Error' log for errors and then start the 'add broken BW' procedure!<bR></font>"
                    End If
                    timeEnd = Now()
                    str += ".... Import done in " & MyNumberFormat((CInt(DateDiff(DateInterval.Second, CDate(Me.ViewState("timestart")), timeEnd)) / 60), 2) & " minutes !"
                    outputformat(str)


                    Exit Select
            End Select
        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Me.txtProgress.Text = "none"
            Me.lblOut.Text = Me.lblOut.Text & ex.Message.ToString
        Finally
            Me.ViewState.Add("manualImport", "true")
            Me.ViewState.Add("LoadInTempTables", "false")
            Me.ViewState.Add("BrokenBW", "True")
            setLastOrderEntry()
        End Try
    End Sub
    Private Sub ImportProgressPharmosan()
        Try

            Select Case strStep

                Case "step2"

                    Server.Execute("Import_SQLLoader.aspx?manual=" & Session("import"), stringWriteLog)
                    stringWriteLog = stringWriteLog.ToString & "<X>"
                    outputformat()
                    stringWriteLog = ""

                    Me.txtProgress.Text = "step2.01"
                    outputformat("2.) Now updating Data ......")

                Case "step2.01"

                    Server.Execute("Import_DBUpdate_BW.aspx?manual=" & Session("import"), stringWriteLog)
                    outputformat()
                    stringWriteLog = ""

                    Me.txtProgress.Text = "step2.1"
                    outputformat("3.) Now archiving files ......")

                Case "step2.1"

                    Server.Execute("Import_Archive_Files.aspx?manual=" & Session("import"), stringWriteLog)
                    outputformat()
                    stringWriteLog = ""

                    Me.txtProgress.Text = "none"
                    timeEnd = Now()
                    Dim str As String
                    str += ".... Import done in " & MyNumberFormat((CInt(DateDiff(DateInterval.Second, CDate(Me.ViewState("timestart")), timeEnd)) / 60), 2) & " minutes !"
                    outputformat(str)
            End Select


        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Me.txtProgress.Text = "none"
            Me.lblOut.Text = Me.lblOut.Text & ex.Message.ToString
        Finally

        End Try
    End Sub

    Private Sub btnConnectMuenster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnectMuenster.Click

        Dim MyImport As New WyethImport
        Try
            lb_ftpfiles.Items.Clear()
        MyImport.MapDrive(MyCollection("FtpHostMuensterUser"), MyCollection("FtpHostMuensterPass"), True, False, MyCollection("FtpHostMuensterTmpNetworkDrive"), MyCollection("FtpHostMuenster"))
        Dim myfiles As FileInfo()
            If importHitoricMuensterData = True Then
                myfiles = Wyeth.Utilities.FileHandling.DirListing(MyCollection("FtpHostMuensterTmpNetworkDrive") + "\History ALF\")
            ElseIf importHitoricMuensterData = False Then
                myfiles = Wyeth.Utilities.FileHandling.DirListing(MyCollection("FtpHostMuensterTmpNetworkDrive"), "*ALF*")
            End If

            For Each file As FileInfo In myfiles
                Dim li As New ListItem
                li.Value = file.FullName
                li.Text = file.Name
                Me.lb_ftpfiles.Items.Add(li)
            Next
        Catch ex As Exception
            Me.lblOut.Text = Me.lblOut.Text + vbNewLine + ex.Message
            ExceptionInfo.Show(ex)
        Finally
            MyImport.UnMapDrive(MyCollection("FtpHostMuensterTmpNetworkDrive"), True)
        End Try


    End Sub
    Private Sub btn_importmuenster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_importmuenster.Click
        Dim MyImport As New WyethImport
        Dim i As Integer

        Try
            MyImport.MapDrive(MyCollection("FtpHostMuensterUser"), MyCollection("FtpHostMuensterPass"), True, False, MyCollection("FtpHostMuensterTmpNetworkDrive"), MyCollection("FtpHostMuenster"))

            i = MyImport.SQLLoaderMunesterImport(MyImport.getMuensterFiles(Me.lb_ftpfiles))
            If i > 0 Then
                MyImport.ImportMuensterALL()
            End If

            Me.lblOut.Text = MyImport.strLog
        Catch ex As Exception
            Me.lblOut.Text = Me.lblOut.Text + vbNewLine + ex.Message
            ExceptionInfo.Show(ex)
        Finally
            MyImport.UnMapDrive(MyCollection("FtpHostMuensterTmpNetworkDrive"), True)
        End Try
    End Sub

End Class
