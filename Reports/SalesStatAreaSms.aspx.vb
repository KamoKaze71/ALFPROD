Imports Wyeth.Alf.WyethDropdown
Imports Wyeth.Utilities.DateHandling
Imports Wyeth.Utilities.Helper
Imports C1.Web.C1WebGrid
Imports Wyeth.Utilities.NumberFormat
Imports Wyeth.Alf.CssStyles

Public Class SalesStatAreaSms
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents txtStartDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents txtEndDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents btnGenRep As System.Web.UI.WebControls.Button
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents ReportPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents panelOut As System.Web.UI.WebControls.Panel
    Protected WithEvents lblOut As System.Web.UI.WebControls.Label
    Protected WithEvents repData As Wyeth.Alf.reportData
    Protected WithEvents prtControl As printReportCtl
    Protected WithEvents btn_send_sms As System.Web.UI.WebControls.Button
    Protected WithEvents ddTPGSelect As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblSuccess As System.Web.UI.WebControls.Label
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents btn_lvl1 As System.Web.UI.WebControls.Button
    Protected WithEvents btn_lvl2 As System.Web.UI.WebControls.Button
    Protected WithEvents btn_lvl3 As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim myReport As New Report
    Dim MyJS As New JSPopUp(Me)
    Dim sumUnits, sumFGUnits, i As Integer
    Dim sumValue, sumFGValue As Double
    Dim x As Integer = 0
    Dim icount, l As Integer
    Dim line_id As Integer
    Dim send_sms As Boolean = False
    Dim create_new_sms As Boolean = True
    Dim sms_text, sms_text_html, str_result, strmobile, prod_description, percentage_value, sare_name, sare_mobile, sare_mobile_send, sare_id, cust_name As String
    Dim lstrmobile, lprod_description, lpercentage_value, lsare_name, lsare_mobile, lsare_id, lcust_name As String
    Dim textWriter As System.IO.StringWriter
    Dim sms_active, sms_active_now, lsms_active As Integer

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        ResetSum()

        If Not Page.IsPostBack Then
           
            GetTargetProductGroupSelectDD(ddTPGSelect, Session("country_id"))

            If Me.ALFPageAccessRights > AlfPage.Rights.Read Then
                Me.btn_send_sms.Visible = True
            Else
                Me.btn_send_sms.Visible = False
            End If

            Try
                ddTPGSelect.SelectedValue = "92"
            Catch ex As Exception

            End Try
            txtStartDate.Text = repData.lastOrderDate
            txtEndDate.Text = repData.lastOrderDate

            Me.panelOut.Visible = False
            Me.lblOut.Visible = False

            bindData()
        Else
            Me.panelOut.Visible = False
            Me.lblOut.Visible = False
            str_result = Request.QueryString("result")
        End If

        i = 2

        MyJS.ConfirmMessage = "This will send the relevant sales details to all members of " & ddTPGSelect.SelectedItem.Text.ToString & " via SMS. Do you want to continue?"
        MyJS.AddGetConfirm(btn_send_sms)
    End Sub

    Private Sub fillReportData()

        repData.lastOrderDate = txtEndDate.Text
        repData.addLine("Report-date from", txtStartDate.Text, True, False)
        repData.addLine("Report-date to", txtEndDate.Text, True, False)
        repData.addLine("Selected TPG", ddTPGSelect.SelectedItem.Text.ToString, True, False)

    End Sub

    Private Sub bindData()


            Dim MyDataView As New DataView
            myReport.StartDate = txtStartDate.Text
            myReport.EndDate = txtEndDate.Text
            myReport.DistID = 8
            myReport.TargetPG = ddTPGSelect.SelectedValue
            fillReportData()
            MyDataView = myReport.GetSalesAreaStatSMS
            icount = MyDataView.Count

            With MyGrid
                .DataSource = MyDataView
                .GridLines = GridLines.None
                .ShowFooter = True
                .AllowSorting = False
                .AllowAutoSort = True
                .DataBind()
            End With

        SetGridStylesGroup(MyGrid)
        If icount = 0 Then
            MyGrid.Visible = False
            lblSuccess.Visible = True
            lblSuccess.Text = "No Sales for " & ddTPGSelect.SelectedItem.Text.ToString
        Else
            MyGrid.Visible = True
            lblSuccess.Visible = False
            
        End If
    End Sub

    Private Sub C1WebGrid1_SortCommand(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1SortCommandEventArgs) Handles MyGrid.SortCommand
        bindData()
    End Sub 'C1WebGrid1_SortCommand
    Private Sub Item_created(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated
        setColumnToolTips(e.Item, MyGrid, 3)

        If e.Item.ItemType = C1ListItemType.GroupFooter Or e.Item.ItemType = C1ListItemType.GroupHeader Then
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_VALUE")) - i), 2)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_UNITS")) - i), 0)
        End If

        If e.Item.ItemType = C1ListItemType.Footer Then
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Units"))).Text = MyNumberFormat(sumUnits, 0)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Value"))).Text = MyNumberFormat(sumValue, 2)

            e.Item.CssClass = "reportTotal"
            e.Item.Cells(2).Text = "TOTAL AUSTRIA"
            e.Item.Cells(2).HorizontalAlign = HorizontalAlign.Left
        End If
    End Sub

    Private Sub btnGenRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRep.Click
        Page.Validate()
        If Page.IsValid = True Then
            lblSuccess.Visible = False
            bindData()
        Else
            MyGrid.Visible = False
        End If

    End Sub

    Private Sub Item_DataBound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound

        If e.Item.ItemType = C1ListItemType.AlternatingItem Or e.Item.ItemType = C1ListItemType.Item Then
            sumUnits = sumUnits + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Units"))).Text
            sumValue = sumValue + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Value"))).Text
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_VALUE"))), 0)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_UNITS"))), 0)
        End If

        If send_sms = True Then
            l = 0
            sare_id = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sare_id"))).Text()
            sare_name = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sare_name"))).Text()
            sare_mobile = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("mobile"))).Text()
            cust_name = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("cust_wyeth_name"))).Text()
            prod_description = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("prod_description"))).Text()
            percentage_value = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_VALUE"))).Text()
            sms_active = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sms_active"))).Text()

            ' sare_mobile_send = sare_mobile.Replace("+", "00")
            ' sare_mobile_send = sare_mobile.Replace(" ", "")

            If sare_id <> lsare_id And cust_name <> lcust_name And x = 0 Then

                sms_text_html = sms_text_html & "<div class=item>"
                sms_text_html = sms_text_html & "<strong>Sending SMS to: " & sare_name & ", mobile:" & sare_mobile & "</strong><bR>"
                sms_text_html = sms_text_html & "<br><Strong>" & cust_name & ":</STRONG>"
                sms_text_html = sms_text_html & prod_description & ": "
                sms_text_html = sms_text_html & percentage_value & "; "

                sms_text = sms_text & cust_name & ":"
                sms_text = sms_text & prod_description & ":"
                sms_text = sms_text & percentage_value '& ";"
                l = 1
            ElseIf sare_id = lsare_id And cust_name <> lcust_name Then
                sms_text = sms_text & "|" & cust_name & ":"
                sms_text = sms_text & prod_description & ":"
                sms_text = sms_text & percentage_value & " "
                '  sms_text = sms_text & "|"

                sms_text_html = sms_text_html & "<br><Strong>" & cust_name & ":</STRONG>"
                sms_text_html = sms_text_html & prod_description & ":"
                sms_text_html = sms_text_html & percentage_value & ";"
                l = 1
            ElseIf sare_id = lsare_id And cust_name = lcust_name Then
                sms_text = sms_text & prod_description & ": "
                sms_text = sms_text & percentage_value & " "
                ' sms_text = sms_text & "|"

                sms_text_html = sms_text_html & prod_description & ": "
                sms_text_html = sms_text_html & percentage_value & "; "
                l = 1
            ElseIf sare_id <> lsare_id And x > 0 Then

                Dim mysms As New SendSMS
                Dim strResult As String
                If cust_name <> lcust_name Or l = 0 Then
                    mysms.MobileNumber = lsare_mobile
                    sms_active_now = lsms_active
                Else
                    mysms.MobileNumber = sare_mobile
                    sms_active_now = sms_active
                End If
                mysms.PWD = "wye3010"
                mysms.UID = "wyeth_at_intern"
                mysms.SMSMessage = sms_text
                If sms_active_now = 1 Then
                    strResult = mysms.SendSms
                Else
                    'dont send
                    strResult = "User was skipped because sms sending is not activated for this user"
                End If
                'strResult = "Status=0"
                sms_text = ""

                If strResult.IndexOf("Status=0") > 0 Then
                    sms_text_html = sms_text_html & "<Br><Font color=red><Strong>Done ...</strong></Font><Br>"
                Else
                    sms_text_html = sms_text_html & "<Br><Font color=red><Strong>SMS could not be sent!!<br>(" & strResult & ")</strong></font><Br>"
                End If
                sms_text_html = sms_text_html & "</div> <Div class=item><strong>Sending SMS to: " & sare_name & ", mobile: " & sare_mobile & "</strong><BR>"

                sms_text = sms_text & "|" & cust_name & ":"
                sms_text = sms_text & prod_description & ":"
                sms_text = sms_text & percentage_value & " "
                '  sms_text = sms_text & "|"

                sms_text_html = sms_text_html & "<br><Strong>" & cust_name & ":</STRONG>"
                sms_text_html = sms_text_html & prod_description & ":"
                sms_text_html = sms_text_html & percentage_value & ";"

                lblOut.Text = lblOut.Text + sms_text_html


                sms_text_html = ""
                strmobile = ""
                sare_name = ""
                sare_id = ""
                cust_name = ""


                End If

                If x = icount - 1 Then

                    Dim mysms As New SendSMS
                    Dim strResult As String

                    mysms.MobileNumber = sare_mobile
                    mysms.PWD = "wye3010"
                    mysms.UID = "wyeth_at_intern"
                mysms.SMSMessage = sms_text
                If sms_active_now = 1 Then
                    strResult = mysms.SendSms()
                Else
                    strResult = "sms not active for user"
                End If

                'strResult = "Status=0"
                If strResult.IndexOf("Status=0") > 0 Then
                    sms_text_html = sms_text_html & "<Br><Font color=red><Strong>Done ...</strong><Font><Br>"
                Else
                    sms_text_html = sms_text_html & "<Br><Font color=red><Strong>SMS could not be sent!!</strong></font><Br>"
                End If
                sms_text_html = sms_text_html & "</div>"

                lblOut.Text = lblOut.Text + sms_text_html

                sms_text = ""
                sms_text_html = ""
                strmobile = ""
                sare_name = ""
                sare_id = ""
                cust_name = ""

            End If

            lsare_id = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sare_id"))).Text()
            lsare_name = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sare_name"))).Text()
            lsare_mobile = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("mobile"))).Text()
            lcust_name = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("cust_wyeth_name"))).Text()
            lprod_description = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("prod_description"))).Text()
            lpercentage_value = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("PERCENTAGE_VALUE"))).Text()
            lsms_active = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sms_active"))).Text()


        End If
        x = x + 1
    End Sub

    Private Sub ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportExcel.Click
        bindData()

        MyGrid.Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.None
        MyGrid.Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.None
        MyGrid.Columns(2).GroupInfo.OutlineMode = OutlineModeEnum.None

        Dim exp As exportToExcel = New exportToExcel(Page)
        exp.title = Me.ALFPageTitle
        exp.showReportData = repData
        exp.addDataGrid(MyGrid)
        exp.export()
    End Sub
    Private Sub ResetSum()
        sumUnits = 0 : sumFGUnits = 0 : sumValue = 0 : sumFGValue = 0
        cust_name = ""
        sare_name = ""
        x = 0
        sare_id = "0"
        prod_description = ""
        percentage_value = ""
    End Sub

    '**************************************************************************************************************
    '* For Printing
    '**************************************************************************************************************
    Sub btnPrint_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Print_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Autoprint Or PopupWindowStyle.NoToolbar)
        ResetSum()
        bindData()
    End Sub

    Sub btnPreview_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Preview_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Fullscreen)
        ResetSum()
        bindData()
    End Sub

    Private Function PrePrint() As printReportUtil
        bindData()

        Dim preview As New printReportUtil

        preview.PageTitle = "Sales Area"
        preview.PageSize = 47

        preview.AddReportHeader(repData)
        preview.AddWebGrid(Me.MyGrid)

        Return preview

    End Function
    '**************************************************************************************************************
    '* End Printing
    '**************************************************************************************************************

    Private Sub btn_send_sms_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_send_sms.Click
        MyGrid.Visible = False
        lblOut.Visible = True
        lblOut.Text = ""
        sms_text = ""
        sms_text_html = ""
        panelOut.Visible = True
        send_sms = True
        bindData()
        send_sms = False
    End Sub

    Private Sub btn_lvl1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_lvl1.Click
        bindData()
        MyGrid.Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed
        MyGrid.Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed
        MyGrid.Columns(2).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed
    End Sub

    Private Sub btn_lvl2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_lvl2.Click
        bindData()
        MyGrid.Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
        MyGrid.Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed
        MyGrid.Columns(2).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed
    End Sub

    Private Sub btn_lvl3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_lvl3.Click
        bindData()
        MyGrid.Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
        MyGrid.Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
        MyGrid.Columns(2).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
    End Sub
End Class



















