Imports Wyeth.Alf.WyethDropdown

Public Class Accounting
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btn_select_type As System.Web.UI.WebControls.Button
    Protected WithEvents btn_Add_bewegkz As System.Web.UI.WebControls.Button
    Protected WithEvents chk_active As System.Web.UI.WebControls.CheckBox
    Protected WithEvents ddInvertSelect As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dlBewegKZs As System.Web.UI.WebControls.DataList
    Protected WithEvents btn_modify As System.Web.UI.WebControls.Button
    Protected WithEvents ddAccountingRecordName As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblAcReName As System.Web.UI.WebControls.Label
    Protected WithEvents btnDeleteAcre As System.Web.UI.WebControls.Button
    Protected WithEvents tblacrename As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents btn_CreateNewAccRe As System.Web.UI.WebControls.Button
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents BUTTON1 As System.Web.UI.HtmlControls.HtmlButton
    Protected WithEvents tblmain As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblbuttons As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblDatalist As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents txtCtry_debit As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDepartment_debit As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtAccount_debit As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSubsidiary_debit As System.Web.UI.WebControls.TextBox
    Protected WithEvents rbl_pl_debit As System.Web.UI.WebControls.RadioButtonList
    Protected WithEvents rbl_pl_credit As System.Web.UI.WebControls.RadioButtonList
    Protected WithEvents ddCC_debit As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddCC_credit As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDepartment_credit As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSubsidiary_credit As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCtry_Credit As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtAccount_credit As System.Web.UI.WebControls.TextBox
    Protected WithEvents buttonSave As System.Web.UI.WebControls.Button
    Protected WithEvents tblSave As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents ddLineSelect As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim MyJDE As New JDE
    Dim MyAcReDataView As New DataView
    Dim myJS As New JSPopUp(Me)
    Dim acre_id As Integer = 0


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        myJS.Height = 150
        myJS.Width = 300
        myJS.Left = 100
        myJS.ConfirmMessage = "This will delete the Accounting Record"
        myJS.AddGetConfirm(btnDeleteAcre)


        If Page.IsPostBack = True Then
            myJS.Title = "Modify Accounting Record"
            myJS.PageURL = "CreateNewAccountingRecord.aspx?acre_id=" & ddAccountingRecordName.SelectedValue
            myJS.AddPopupToControl(btn_modify)
            'acre_id = Me.ViewState.Item("acre_id")
            'If acre_id <> 0 Then
            '    btn_select_type_Click(sender, e)
            'End If
          

        Else
           

            myJS.Title = "Create New Accounting Record"
            myJS.PageURL = "CreateNewAccountingRecord.aspx"
            myJS.AddPopupToControl(btn_CreateNewAccRe)

            tblmain.Visible = False
            tblbuttons.Visible = False
            tblDatalist.Visible = False
            tblacrename.Visible = False
            tblSave.Visible = False

            BindData()

            acre_id = ddAccountingRecordName.SelectedValue
            ' Me.ViewState.Add("acre_id", acre_id)



            If Request.QueryString("acre_id") <> "" Then
                BindData()

                acre_id = Request.QueryString("acre_id")
                ddAccountingRecordName.SelectedValue = acre_id

                Dim str As String
                str = "<script>" & vbNewLine & _
                          "document.location.href = 'Accounting.aspx';</script>"
                'Response.Write(str)


                btn_select_type_Click(sender, e)

            End If

        End If


       

      
       

    End Sub


    Private Sub BindData()
        fillDropdown()
    End Sub

    Private Sub fillDropdown()
        GetLineSelectDD(ddLineSelect, Session("Country_id"))
        With ddAccountingRecordName
            .DataSource = MyJDE.GetAccountNamesList(Session("country_id"))
            .DataTextField = "name"
            .DataValueField = "acre_id"
            .DataBind()
        End With

    End Sub

    Private Sub btn_select_type_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_select_type.Click

        tblmain.Visible = True
        tblbuttons.Visible = True
        tblDatalist.Visible = True
        tblacrename.Visible = True
        tblSave.Visible = True

        acre_id = ddAccountingRecordName.SelectedValue

        MyAcReDataView = MyJDE.GetAccRecord(ddAccountingRecordName.SelectedValue)

        lblAcReName.Text = MyAcReDataView.Item(0).Item("acre_name") & "-" & MyAcReDataView.Item(0).Item("acre_description")

        myJS.PageURL = "AddBewegKZ.aspx?acre_id=" & MyAcReDataView.Item(0).Item("acre_id")
        myJS.AddPopupToControl(btn_Add_bewegkz)

        txtCtry_debit.Text = MyAcReDataView.Item(0).Item("ctry_pl_country")
        txtCtry_debit.ToolTip = "Debit Country Code"

        txtAccount_debit.Text = Convert.ToString(MyAcReDataView.Item(0).Item("acre_debit_account"))
        txtAccount_debit.ToolTip = "Debit Account No."

        txtSubsidiary_debit.Text = Convert.ToString(MyAcReDataView.Item(0).Item("acre_debit_subsidiary"))
        txtSubsidiary_debit.ToolTip = "Debit Subsidiary"

        txtDepartment_debit.Text = Convert.ToString(MyAcReDataView.Item(0).Item("acre_debit_department"))
        txtDepartment_debit.ToolTip = "Debit Department Code"

        ddCC_debit.SelectedValue = Convert.ToString(MyAcReDataView.Item(0).Item("acre_debit_costcenter"))
        ddCC_debit.ToolTip = "Debit CostCenter"

        rbl_pl_debit.SelectedValue = Convert.ToString(MyAcReDataView.Item(0).Item("acre_debit_type"))
        rbl_pl_debit.ToolTip = "Debit Type"

        If rbl_pl_debit.SelectedValue = "PL" Then
            txtCtry_debit.Text = MyAcReDataView.Item(0).Item("ctry_pl_country")
            txtCtry_debit.ToolTip = "Debit Country Code"
        Else
            txtCtry_debit.Text = MyAcReDataView.Item(0).Item("ctry_bs_country")
            txtCtry_debit.ToolTip = "Debit Country Code"
        End If

        ddInvertSelect.SelectedValue = Convert.ToString(MyAcReDataView.Item(0).Item("acre_invert"))
        ddInvertSelect.ToolTip = "Invert Amount"
        'chk_active.SelectedValue = Convert.ToString(MyAcReDataView.Item(0).Item("acre_active"))

        If Convert.ToString(MyAcReDataView.Item(0).Item("acre_active")) = 1 Then
            chk_active.Checked = True
        Else
            chk_active.Checked = False
        End If

        ddLineSelect.SelectedValue = MyAcReDataView.Item(0).Item("line_id")

        txtAccount_credit.Text = Convert.ToString(MyAcReDataView.Item(0).Item("acre_credit_account"))
        txtAccount_credit.ToolTip = "Credit Account No."

        txtDepartment_credit.Text = Convert.ToString(MyAcReDataView.Item(0).Item("acre_credit_department"))
        txtDepartment_credit.ToolTip = "Credit Department Code"

        ddCC_credit.SelectedValue = Convert.ToString(MyAcReDataView.Item(0).Item("acre_credit_costcenter"))
        ddCC_credit.ToolTip = "Credit CostCenter"

        rbl_pl_credit.SelectedValue = Convert.ToString(MyAcReDataView.Item(0).Item("acre_credit_type"))
        rbl_pl_credit.ToolTip = "Credit Type"


        If rbl_pl_credit.SelectedValue = "PL" Then
            txtCtry_Credit.Text = Convert.ToString(MyAcReDataView.Item(0).Item("ctry_pl_country"))
            txtCtry_Credit.ToolTip = "Debit Country Code"
        Else
            txtCtry_Credit.Text = Convert.ToString(MyAcReDataView.Item(0).Item("ctry_bs_country"))
            txtCtry_Credit.ToolTip = "Debit Country Code"
        End If


        txtSubsidiary_credit.Text = Convert.ToString(MyAcReDataView.Item(0).Item("acre_credit_subsidiary"))
        txtSubsidiary_credit.ToolTip = "Credit Subsidiary"

        ' display BewegKZ associated with this accounting record
        MyAcReDataView = MyJDE.GetBewegKZsForAcRe(acre_id)
        dlBewegKZs.DataSource = MyAcReDataView
        dlBewegKZs.DataBind()

    End Sub

    Private Sub btnDeleteAcre_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteAcre.Click
        MyJDE.DeleteAccountingRecord(CInt(ddAccountingRecordName.SelectedValue))
        BindData()
        tblmain.Visible = False
        tblbuttons.Visible = False
        tblDatalist.Visible = False
        tblacrename.Visible = False
        tblSave.Visible = False
    End Sub

    Private Sub dlProducts_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlBewegKZs.ItemCommand

        Dim affectedDatalist As DataList = source

        If e.CommandName = "delete" Then
            MyJDE.DeleteBewegKZForAcRe(ddAccountingRecordName.SelectedValue, e.CommandArgument)
        End If
        dlBewegKZs.DataSource = MyJDE.GetBewegKZsForAcRe(ddAccountingRecordName.SelectedValue)
        affectedDatalist.DataBind()
    End Sub

    Private Sub buttonSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonSave.Click
        saveform()
    End Sub

    Private Sub dlBewegKz_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlBewegKZs.ItemDataBound
        Dim delButton As LinkButton = e.Item.FindControl("Linkbutton2")
        myJS.ConfirmMessage = "Are you sure you want to remove " & e.Item.DataItem("code_code") & "\nfrom the selected Accounting Record?"
        myJS.AddGetConfirm(delButton)
    End Sub

    Private Sub rbl_pl_debit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbl_pl_debit.SelectedIndexChanged
        saveform()
        btn_select_type_Click(sender, e)
    End Sub

    Private Sub rbl_pl_credit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbl_pl_credit.SelectedIndexChanged
        saveform()
        btn_select_type_Click(sender, e)
    End Sub
    Private Sub saveform()
        Dim i_chk_active As Integer
        If chk_active.Checked = True Then
            i_chk_active = 1
        Else
            i_chk_active = 0
        End If
        MyJDE.UpdateAccountingRecord(CInt(ddAccountingRecordName.SelectedValue), rbl_pl_debit.SelectedValue.ToString, ddCC_debit.SelectedValue.ToString, Trim(txtDepartment_debit.Text), Trim(txtAccount_debit.Text), Trim(txtSubsidiary_debit.Text), rbl_pl_credit.SelectedValue.ToString, ddCC_credit.SelectedValue.ToString, Trim(txtDepartment_credit.Text), Trim(txtAccount_credit.Text), Trim(txtSubsidiary_credit.Text), i_chk_active, CDbl(ddInvertSelect.SelectedValue), Session("user_id"), CInt(ddLineSelect.SelectedValue))

    End Sub

    Private Sub ddAccountingRecordName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddAccountingRecordName.SelectedIndexChanged
        acre_id = ddAccountingRecordName.SelectedValue
        Me.ViewState.Add("acre_id", acre_id)
    End Sub
End Class
