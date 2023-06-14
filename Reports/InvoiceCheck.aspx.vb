Imports Oracle.DataAccess.Client

Imports wyeth.Alf
Imports Wyeth.Alf.CssStyles
Imports Wyeth.Alf.FxRate
Imports wyeth.Alf.WyethDropdown

Imports Wyeth.Utilities
Imports Wyeth.Utilities.NumberFormat
Imports Wyeth.Utilities.Helper
Imports Wyeth.Utilities.DateHandling

Public Class InvoiceCheck
	Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
	Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
	Protected WithEvents EditPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents txtPresentation As System.Web.UI.WebControls.TextBox
	Protected WithEvents txtStockDateStock As System.Web.UI.WebControls.TextBox
	Protected WithEvents txtInvoiceNumber As System.Web.UI.WebControls.TextBox
	Protected WithEvents txtInvoiceValue As System.Web.UI.WebControls.TextBox
	Protected WithEvents txtValueAccrued As System.Web.UI.WebControls.TextBox
	Protected WithEvents txtComment As System.Web.UI.WebControls.TextBox
	Protected WithEvents Button_Update As System.Web.UI.WebControls.Button
	Protected WithEvents Button_Cancel As System.Web.UI.WebControls.Button
    Protected WithEvents GridPanel As System.Web.UI.WebControls.Panel

    Protected WithEvents txtOrderNo As Wyeth.Utilities.WyethTextBox
    Protected WithEvents txtInvoiceUnits As Wyeth.Utilities.WyethTextBox
    Protected WithEvents txtstockdateGIT As Wyeth.Utilities.WyethTextBox
    Protected WithEvents txtunits_git As Wyeth.Utilities.WyethTextBox
    Protected WithEvents txtInvoiceValue_git As Wyeth.Utilities.WyethTextBox

    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents ddInvoiceCurrencyid As System.Web.UI.WebControls.DropDownList
	Protected WithEvents ddAccruedCurrencyID As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtprod_id As System.Web.UI.WebControls.TextBox
	Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents txtStartDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents txtEndDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents ddInvoiceSelect As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddDistribSelect As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtphznr As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddLineSelect As System.Web.UI.WebControls.DropDownList

    Protected WithEvents txtDiff As System.Web.UI.WebControls.TextBox
    Protected WithEvents Dropdownlist1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtstock_id As System.Web.UI.WebControls.TextBox
    Protected WithEvents BTN_GIT_ASSIGN As System.Web.UI.WebControls.Button
    Protected WithEvents btn_insert_git As System.Web.UI.WebControls.Button

    Protected WithEvents EditPanelGIT As System.Web.UI.WebControls.Panel
    Protected WithEvents ddProductSelect As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btn_update_git As System.Web.UI.WebControls.Button
    Protected WithEvents btn_cancel_git As System.Web.UI.WebControls.Button
    Protected WithEvents txtwyethInvoiceNo_git As Wyeth.Utilities.WyethTextBox

    Protected WithEvents txtComment_git As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtstock_id_git As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtOrderNo_git As Wyeth.Utilities.WyethTextBox
    Protected WithEvents ddInvoiceCurrencyIDGIT As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDiffValueAccrued As System.Web.UI.WebControls.TextBox
    Protected WithEvents btn_cancel_git_assignment As System.Web.UI.WebControls.Button
    Protected WithEvents txt_stock_id_git As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDiffGIT As System.Web.UI.WebControls.Label
    Protected WithEvents lblDiff As System.Web.UI.WebControls.Label
    Protected WithEvents btn_delete_git As System.Web.UI.WebControls.Button
    Protected WithEvents lblEditGIT As System.Web.UI.WebControls.Label
    Protected WithEvents lblEditWE As System.Web.UI.WebControls.Label
    Protected WithEvents txtdiffhidden As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtInvoiceValueHidden As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents startDate3 As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents startDate1 As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents startDate2 As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents EndDate1 As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents EndDate2 As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents EndDate3 As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents lblstdcogs As System.Web.UI.WebControls.Label
    Protected WithEvents lblSTdCogsWE As System.Web.UI.WebControls.Label
    Protected WithEvents btnGenRep As System.Web.UI.WebControls.Button
    Protected WithEvents btn_add_git As System.Web.UI.WebControls.Button
    Protected WithEvents GITASSIGNFRAME As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button

	'NOTE: The following placeholder declaration is required by the Web Form Designer.
	'Do not delete or move it.
	Private designerPlaceholderDeclaration As System.Object

	Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
		'CODEGEN: This method call is required by the Web Form Designer
		'Do not modify it using the code editor.
		InitializeComponent()
	End Sub

#End Region

	Dim MyStock As New Stock
    Dim MyTCogs As New TCogs
    Dim bol_finished As Boolean

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btn_update_git.Attributes.Add("onkeydown", "return submitButton(btn_update_git);")
        btn_insert_git.Attributes.Add("onkeydown", "return submitButton(btn_insert_git);")


        If Page.IsPostBack = True Then

        Else

            GetLineSelectDD(ddLineSelect, Session("country_id"))
            GetCurrencySelectDD(ddInvoiceCurrencyid)
            GetCurrencySelectDD(ddAccruedCurrencyID)
            GetCurrencySelectDD(ddInvoiceCurrencyIDGIT)
            GetDistribSelectDD(ddDistribSelect, Session("country_id"))

            GetProductSelectDD(ddProductSelect, ddLineSelect.SelectedValue, Session("country_id"), "NOBS")
            Dim li As New ListItem
            li.Value = 0
            li.Text = "-- Select a Product --"
            ddProductSelect.Items.Insert(0, li)

            ddInvoiceSelect.Items.Add("Open Invoices")
            ddInvoiceSelect.Items.Add("Closed Invoices")
            ddInvoiceSelect.Items.Add("All Invoices")
            ddInvoiceSelect.DataBind()
            Me.lblPageTitle.Text = Request.QueryString("PageTitle")


            startDate3.Visible = False
            startDate2.Visible = False
            startDate1.Visible = False

            EndDate1.Visible = False
            EndDate2.Visible = False
            EndDate3.Visible = False

            txtStartDate.Text = FirstOfThisMonth(Session("CurrentProcessMonth")).ToString(DATEFORMAT_STRING_REPORT)
            txtEndDate.Text = LastOfThisMonth(Session("CurrentProcessMonth")).ToString(DATEFORMAT_STRING_REPORT)




            BindData()
        End If
         btn_cancel_git_assignment.Attributes.Add("onclick", "javascript:return getconfirmGIT();")
        Button_Update.Attributes.Add("onclick", "javascript:return getconfirmGITComment();")

        SetGridStyles(MyGrid)  'Apply default Css Style Settings
    End Sub

    Private Sub BindData()
        MyGrid.AllowPaging = False
        MyGrid.EnableViewState = True

        MyStock.StockEndDate = txtEndDate.Text
        MyStock.StockStartDate = txtStartDate.Text
        MyStock.StockCtryID = Session("country_id")
        MyStock.StockDistID = ddDistribSelect.SelectedValue
        MyStock.StockLineID = ddLineSelect.SelectedValue
        MyGrid.DataSource = MyStock.GetInvoiceData(ddInvoiceSelect.SelectedIndex)
        MyGrid.DataBind()
    End Sub

    Private Sub Button_Update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Update.Click
        Validate()
        If Page.IsValid Then

          

            MyStock.StockID = txtstock_id.Text
            MyStock.StockCurrID = ddInvoiceCurrencyid.SelectedValue
            MyStock.StockCurrIDAccrued = ddAccruedCurrencyID.SelectedValue
            MyStock.StockInvoiceNo = txtInvoiceNumber.Text
            MyStock.StockInvoiceValue = MyNumberFormat(txtInvoiceValueHidden.Value, 2) 'txtInvoiceValue.Text
            MyStock.StockUserID = Session("user_id")
            MyStock.StockComment = txtComment.Text
            MyStock.StockInvoiceAccruedValue = MyNumberFormat(txtValueAccrued.Text, 2)
            MyStock.StockUnits = MyNumberFormat(txtInvoiceUnits.Text, 0)
            MyStock.StockOrderNumber = txtOrderNo.Text
            MyStock.StockCodeIDBewegKZ = MyStock.GetCodeIDBewegKZ("WE")
            MyStock.StockInvoiceDiffValue = MyNumberFormat(txtdiffhidden.Value, 2) ' txtDiff.Text
            MyStock.StockInvoiceDiffValueAccrued = MyNumberFormat(txtDiffValueAccrued.Text, 2)

            If MyStock.UpdateInvoiceData() Then
                EditPanel.Visible = False
                GridPanel.Visible = True
                FilterPanel.Visible = True

                BindData()
                
            Else
                Dim strScript As String
                strScript = "<script language =javascript >"
                strScript += "window.open('../error.aspx?ErrorId=2','Error','width=300,height=250,left=270,top=180');"
                strScript += "</script>"
                RegisterClientScriptBlock("anything", strScript)
                Response.Write(strScript)

            End If
        End If


    End Sub
    Private Sub Button_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Cancel.Click
        BindData()

        EditPanel.Visible = False
        EditPanelGIT.Visible = False
        GridPanel.Visible = True
        FilterPanel.Visible = True

    End Sub

    Sub MyGrid_ItemCommand(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1CommandEventArgs) Handles MyGrid.ItemCommand

        Dim stdcogs As Double

        If e.Item.Cells(2).Text = "WE" Then


            EditPanel.Visible = True
            GridPanel.Visible = False
            FilterPanel.Visible = False


            txtstock_id.Text = e.Item.Cells(0).Text
            txtprod_id.Text = e.Item.Cells(1).Text
            txtphznr.Text = e.Item.Cells(3).Text
            txtPresentation.Text = e.Item.Cells(4).Text
            txtStockDateStock.Text = e.Item.Cells(5).Text

            stdcogs = MyTCogs.GetTCogs(Convert.ToDateTime(txtStockDateStock.Text), txtprod_id.Text, Session("currency_id"))
            lblSTdCogsWE.Text = MyNumberFormat(stdcogs, 2)

            If e.Item.Cells(6).Text = "&nbsp;" Then
                txtOrderNo.Text = ""
            Else
                txtOrderNo.Text = e.Item.Cells(6).Text
            End If

            txtInvoiceNumber.Text = Trim(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("STOC_INVOICE_NUMBER"))).Text)
            txtInvoiceUnits.Text = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("STOC_UNIT"))).Text
            txtInvoiceValue.Text = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("STOC_INVOICE_VALUE"))).Text
            txtValueAccrued.Text = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("STOC_ACCRUED_VALUE"))).Text
            txtComment.Text = Trim(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("STOC_COMMENT"))).Text)
            txtDiff.Text = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("STOC_DIFF_VALUE"))).Text
            txtDiffValueAccrued.Text = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("STOC_DIFF_VALUE_ACCRUED"))).Text

            txtdiffhidden.Value = txtDiff.Text
            txtInvoiceValueHidden.Value = txtInvoiceValue.Text

            If e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("CURR_ID_INVOICE"))).Text = "0" Then
                ddInvoiceCurrencyid.SelectedValue = Session("currency_id")
            Else
                ddInvoiceCurrencyid.SelectedValue = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("CURR_ID_INVOICE"))).Text
            End If

            If e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("CURR_ID_ACCRUED"))).Text = "0" Then
                ddAccruedCurrencyID.SelectedValue = Session("currency_id")
            Else
                ddAccruedCurrencyID.SelectedValue = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("CURR_ID_ACCRUED"))).Text
            End If

            BTN_GIT_ASSIGN.Attributes.Add("OnClick", "javascript:OpenPopUp('AssignGIT.aspx?prod_id=" & (e.Item.Cells(1).Text) & "&dist_id=" & ddDistribSelect.SelectedValue & "&prod_presentation=" & txtPresentation.Text & "&prod_phznr=" & txtphznr.Text & "&stock_we_id=" & txtstock_id.Text & "' ,'Corrections');")

            If e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("GIT ID"))).Text = "0" Then
                ' wenn we keine git zugeordnet
                lblDiff.Text = "Diff WE to Accrual"
                BTN_GIT_ASSIGN.Visible = True
                btn_cancel_git_assignment.Visible = False
                txtDiffValueAccrued.Visible = True
                txtDiff.Visible = False
            Else         'es existiert eine GIT eintrag zu diesem we

                btn_cancel_git_assignment.Visible = True
                BTN_GIT_ASSIGN.Visible = False

                txtDiffValueAccrued.Visible = False
                txtDiff.Visible = True
                lblDiff.Text = "Diff WE to GIT"

                ' es existiert ein GIT eintrag, aber der record dart nicht mehr editiert werden da breits processed
                If e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_date_correct"))).Text = "&nbsp;" Then

                    btn_cancel_git_assignment.Enabled = True
                    BTN_GIT_ASSIGN.Enabled = True
                    Button_Update.Visible = True

                Else
                    btn_cancel_git_assignment.Enabled = False
                    BTN_GIT_ASSIGN.Enabled = False
                    Button_Update.Visible = False
                End If



            End If


        ElseIf e.Item.Cells(2).Text = "GIT" Then ' wenn es ein Git eintrag ist

            EditPanel.Visible = False
            GridPanel.Visible = False
            FilterPanel.Visible = False
            EditPanelGIT.Visible = True
            btn_update_git.Visible = True
            btn_insert_git.Visible = False
            btn_delete_git.Visible = True


            txtstock_id_git.Text = e.Item.Cells(0).Text
            ddProductSelect.SelectedValue = e.Item.Cells(1).Text
            txtunits_git.Text = e.Item.Cells(8).Text
            txtInvoiceValue_git.Text = e.Item.Cells(10).Text

            If e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("CURR_ID_INVOICE"))).Text = 0 Then
                ddInvoiceCurrencyIDGIT.SelectedValue = Session("currency_id")
            Else
                ddInvoiceCurrencyIDGIT.SelectedValue = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("CURR_ID_INVOICE"))).Text

            End If

            If e.Item.Cells(6).Text = "&nbsp;" Then
                txtComment_git.Text = ""
            Else
                txtComment_git.Text = Trim(e.Item.Cells(13).Text)
            End If



            If e.Item.Cells(7).Text = "&nbsp;" Then
                txtwyethInvoiceNo_git.Text = ""
            Else
                txtwyethInvoiceNo_git.Text = Trim(e.Item.Cells(7).Text)
            End If

            txtunits_git.Text = e.Item.Cells(8).Text
            txtstockdateGIT.Text = e.Item.Cells(5).Text

            If e.Item.Cells(6).Text = "&nbsp;" Then
                txtOrderNo_git.Text = ""
            Else
                txtOrderNo_git.Text = e.Item.Cells(6).Text
            End If


            ' Get The Tcogs Value for this product and show it  in the label
            stdcogs = MyTCogs.GetTCogs(Convert.ToDateTime(txtstockdateGIT.Text), ddProductSelect.SelectedValue, Session("currency_id"))
            lblstdcogs.Text = MyNumberFormat(stdcogs, 2)


            If e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_ID_GIT"))).Text <> "0" Or e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_date_correct"))).Text <> "&nbsp;" Or Convert.ToDateTime(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("stoc_date_stock"))).Text) < Convert.ToDateTime(Session("LastProcessMonth")) Then
                btn_delete_git.Visible = False
                btn_update_git.Visible = False
            Else
                btn_delete_git.Attributes.Add("OnClick", "javascript:return getconfirmDelete();")
            End If

        End If



    End Sub
    Sub MyGrid_ItemCreated(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated


        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Then

            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor1Class';")
            '    e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("GIT"))).Text = ""
            setPostback(e)
        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then

            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor2Class';")
            '     e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("GIT"))).Text = ""
            setPostback(e)
        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.SelectedItem Then

            setPostback(e)
        End If


      

    End Sub
    Private Sub setPostback(ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs)
        Dim i, x As Integer
        x = e.Item.Cells.Count
        x = x - 2
        For i = 0 To x
            e.Item.Cells(i).Attributes.Add("onclick", "javascript:__doPostBack('MyGrid$R" & (e.Item.ItemIndex) & "$_ctl0','');")
        Next
    End Sub
    Sub MyGrid_ItemDatabound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound
        Dim str As String

        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("STOC_UNIT"))), 0)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("STOC_INVOICE_VALUE"))), 2)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("STOC_ACCRUED_VALUE"))), 2)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("STOC_DIFF_VALUE"))), 2)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("STOC_DIFF_VALUE_ACCRUED"))), 2)

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Or e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Or e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.SelectedItem Then
            If e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("STOC_ID_GIT"))).Text <> "0" Then
                Dim img As New System.Web.UI.HtmlControls.HtmlImage
                Dim strJS As String

                img.Src = "../images/info.gif"
                strJS = "javascript:OpenPopUp('ViewGIT.aspx?stock_id=" & e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("STOC_ID_GIT"))).Text & "&stock_id_git=" & e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("STOC_ID"))).Text & "');"
                img.Attributes.Add("onClick", strJS)
                e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("GIT"))).Controls.Add(img)


                'str = "<img src='../images/info.gif' onClick=javascript:OpenPopUp('ViewGIT.aspx?stock_id=" & e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("STOC_ID_GIT"))).Text & "&stock_id_git=" & e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("STOC_ID"))).Text & "'); >"
                'e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("GIT"))).Text = str


            Else
                e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("GIT"))).Text = ""

            End If
        End If
    End Sub


    Private Sub CalculateAccruedValue()
        Dim curr_from, curr_to, prod_id As Integer
        Dim stock_date_stock As Date
        Dim MyRate, MyTCogs_value As Double
        Dim MyReader As OracleDataReader

        prod_id = txtprod_id.Text
        curr_from = ddInvoiceCurrencyid.SelectedValue
        curr_to = ddAccruedCurrencyID.SelectedValue

        If curr_from = curr_to Then
            MyRate = 1
        Else

            MyStock.StockCtryID = Session("country_id")
            MyStock.StockCurrID = curr_from
            MyStock.StockCurrIDAccrued = curr_to
            MyStock.StockDateStock = txtStockDateStock.Text

            MyRate = MyStock.GetFXRate()

        End If
        MyTCogs_value = MyTCogs.GetTCogs(Convert.ToDateTime(txtStockDateStock.Text), prod_id, curr_from)

        txtInvoiceValue.Text = MyNumberFormat(((CInt(txtInvoiceUnits.Text) * MyTCogs_value) * MyRate), 2)
        txtInvoiceValueHidden.Value = MyNumberFormat(((CInt(txtInvoiceUnits.Text) * MyTCogs_value) * MyRate), 2)
    End Sub
    'Private Sub ddInvoiceCurrencyid_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddInvoiceCurrencyid.SelectedIndexChanged
    '    CalculateAccruedValue()
    'End Sub
    'Private Sub ddAccruedCurrencyID_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddAccruedCurrencyID.SelectedIndexChanged
    '    CalculateAccruedValue()
    'End Sub

    Private Sub btnGenRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRep.Click
        Validate()
        If Page.IsValid = True Then
            BindData()
        End If
    End Sub
    Private Sub btn_add_git_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_add_git.Click
        EditPanel.Visible = False
        EditPanelGIT.Visible = True
        GridPanel.Visible = False
        FilterPanel.Visible = False
        btn_insert_git.Visible = True
        btn_update_git.Visible = False
        btn_delete_git.Visible = False


        'MyStock.StockDistID = ddDistribSelect.SelectedValue
        '  MyStock.StockTranID = MyStock.GetMaxTranID()
        '  MyStock.StockCodeIDBewegKZ = MyStock.GetCodeIDBewegKZ("GIT")
        ' MyStock.StockProdID = ddProductSelect.SelectedValue
        txtComment_git.Text = ""
        txtInvoiceValue_git.Text = ""
        txtunits_git.Text = ""
        txtwyethInvoiceNo_git.Text = ""
        txtOrderNo_git.Text = ""


        ' MyStock.StockDistID = ddDistribSelect.SelectedValue
        txtstockdateGIT.Text = Session("CurrentProcessMonth")
        ddInvoiceCurrencyIDGIT.SelectedValue = Session("currency_id")

    End Sub
    Private Sub btn_insert_git_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_insert_git.Click

        Validate()
        If Page.IsValid Then

            If txtunits_git.ISValid = True And txtunits_git.ISValid = True Then
                MyStock.StockCtryID = Session("country_id")
                MyStock.StockDistID = ddDistribSelect.SelectedValue
                MyStock.StockTranID = MyStock.InsertDummyTransmission()
                MyStock.StockCodeIDBewegKZ = MyStock.GetCodeIDBewegKZ("GIT")
                MyStock.StockProdID = ddProductSelect.SelectedValue
                MyStock.StockComment = txtComment_git.Text()
                MyStock.StockInvoiceValue = MyNumberFormat(txtInvoiceValue_git.Text, 2)
                MyStock.StockDateStock = txtstockdateGIT.Text()
                MyStock.StockUnits = MyNumberFormat(txtunits_git.Text, 0)
                MyStock.StockInvoiceNo = txtwyethInvoiceNo_git.Text()
                MyStock.StockUserID = Session("user_id")
                MyStock.StockOrderNumber = txtOrderNo_git.Text
                MyStock.StockCurrID = ddInvoiceCurrencyIDGIT.SelectedValue

                If MyStock.InsertGIT() Then

                    EditPanel.Visible = False
                    EditPanelGIT.Visible = False
                    GridPanel.Visible = True
                    FilterPanel.Visible = True
                    BindData()
                Else

                    Dim strScript As String
                    strScript = "<script language =javascript >"
                    strScript += "window.open('../error.aspx?ErrorId=3','Error','width=300,height=250,left=270,top=180');"
                    strScript += "</script>"
                    RegisterClientScriptBlock("anything", strScript)
                    Response.Write(strScript)
                End If
            End If
        End If


    End Sub
    Private Sub btn_update_git_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_update_git.Click
        Validate()
        If Page.IsValid Then


            MyStock.StockUserID = Session("user_id")
            MyStock.StockID = txtstock_id_git.Text
            MyStock.StockProdID = ddProductSelect.SelectedValue
            MyStock.StockComment = txtComment_git.Text
            MyStock.StockInvoiceValue = MyNumberFormat(txtInvoiceValue_git.Text, 2)
            MyStock.StockDateStock = txtstockdateGIT.Text
            MyStock.StockUnits = MyNumberFormat(txtunits_git.Text, 0)
            MyStock.StockInvoiceNo = txtwyethInvoiceNo_git.Text
            MyStock.StockOrderNumber = txtOrderNo_git.Text
            MyStock.StockCurrID = ddInvoiceCurrencyIDGIT.SelectedValue

            If MyStock.UpdateInvoiceGITData() Then
                BindData()
                EditPanel.Visible = False
                EditPanelGIT.Visible = False
                GridPanel.Visible = True
                FilterPanel.Visible = True

            Else

                Dim strScript As String
                strScript = "<script language =javascript >"
                strScript += "window.open('../error.aspx?ErrorId=2','Error','width=300,height=250,left=270,top=180');"
                strScript += "</script>"

                RegisterClientScriptBlock("anything", strScript)


            End If
        End If



    End Sub

    Private Sub txtunits_git_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtunits_git.TextChanged
        getGitValue()
    End Sub

    Private Sub btn_cancel_git_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cancel_git.Click
        EditPanel.Visible = False
        EditPanelGIT.Visible = False
        GridPanel.Visible = True
        FilterPanel.Visible = True
        BindData()
    End Sub
    Private Sub txtInvoiceValue_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInvoiceValue.TextChanged
        'CalculateAccruedValue()
    End Sub


    Private Sub ddProductSelect_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddProductSelect.SelectedIndexChanged
        If txtunits_git.ISValid = True Then
            getGitValue()
        End If
    End Sub

    Private Sub getGitValue()
        'txtunits_git.Validate()
        'If txtunits_git.ISValid Then
        Try

            Dim tcogs, stdcogs As Double
            Dim units As Integer

            stdcogs = MyTCogs.GetTCogs(Convert.ToDateTime(txtstockdateGIT.Text), ddProductSelect.SelectedValue, Session("currency_id"))
            If txtunits_git.Text = "" Then
                units = 0
            Else
                units = txtunits_git.Text
            End If
            tcogs = units * stdcogs
            txtInvoiceValue_git.Text = MyNumberFormat(tcogs, 2)
            lblstdcogs.Text = MyNumberFormat(stdcogs, 2)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddInvoiceSelect_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddInvoiceSelect.SelectedIndexChanged
        If ddInvoiceSelect.SelectedIndex > 0 Then

            EndDate1.Visible = True
            EndDate2.Visible = True
            EndDate3.Visible = True

            startDate3.Visible = True
            startDate2.Visible = True
            startDate1.Visible = True

        Else
            startDate3.Visible = False
            startDate2.Visible = False
            startDate1.Visible = False

            EndDate1.Visible = False
            EndDate2.Visible = False
            EndDate3.Visible = False
        End If
    End Sub
    Private Sub btn_cancel_git_assignment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cancel_git_assignment.Click
        MyStock.StockID = txtstock_id.Text
        If MyStock.RemoveGITAssignment Then
            CalculateAccruedValue()

            btn_cancel_git_assignment.Visible = False
            BTN_GIT_ASSIGN.Visible = True
            txtdiffhidden.Value = "0,00"
            txtDiff.Text = "0,00"
            txtDiff.Visible = False
            txtDiffValueAccrued.Visible = True
            lblDiff.Text = "Differnce Accrued"
        End If
    End Sub
    Private Sub BTN_GIT_ASSIGN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTN_GIT_ASSIGN.Click
        Dim strpage As String

        btn_cancel_git_assignment.Visible = True
        txtDiff.Visible = True
        txtDiffValueAccrued.Visible = False
        BTN_GIT_ASSIGN.Visible = False
        lblDiff.Text = "Differnce GIT"
        'strpage = "assignGIT.aspx?prod_id=" & txtprod_id.Text & "&dist_id=" & ddDistribSelect.SelectedValue & "&prod_presentation=" & txtPresentation.Text & "&prod_phznr=" & txtphznr.Text & "&stock_we_id=" & txtstock_id.Text
        'Server.Transfer(strpage, True)

    End Sub
    Private Sub btn_delete_git_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_delete_git.Click
        MyStock.StockID = txtstock_id_git.Text
        If MyStock.DeleteStockEntry() Then

            EditPanel.Visible = False
            EditPanelGIT.Visible = False
            GridPanel.Visible = True
            FilterPanel.Visible = True
            BindData()
        Else
            Dim strScript As String
            strScript = "<script language =javascript >"
            strScript += "window.open('../error.aspx?ErrorId=1','Error','width=300,height=250,left=270,top=180');"
            strScript += "</script>"

            RegisterClientScriptBlock("anything", strScript)

        End If
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        CalculateAccruedValue()
    End Sub
End Class
