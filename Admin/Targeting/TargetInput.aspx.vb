Imports Wyeth.Alf.WyethDropdown
Imports Wyeth.Utilities
Imports Wyeth.Utilities.NumberFormat


Public Class TargetInput
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddTPG As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddVersion As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddSare As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblTapg_descr As System.Web.UI.WebControls.Label
    Protected WithEvents lbltapgid As System.Web.UI.WebControls.Label
    Protected WithEvents TargetRepeater As System.Web.UI.WebControls.Repeater
    Protected WithEvents lblSumQ1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblSumQ2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblSumQ3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblSumQ4 As System.Web.UI.WebControls.Label
    Protected WithEvents lblSumTotal As System.Web.UI.WebControls.Label
    Protected WithEvents Button_save_editpanel As System.Web.UI.WebControls.Button
    Protected WithEvents EditPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents btn_show_targets As System.Web.UI.WebControls.Button
    Protected WithEvents btn_new_version As System.Web.UI.WebControls.Button
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents lblsuccess As System.Web.UI.WebControls.Label
    Protected WithEvents lblCurrency As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblSare As System.Web.UI.WebControls.Label
    Protected WithEvents prtControl As printReportCtl
    Protected WithEvents lblTargetType As System.Web.UI.WebControls.Label
    Protected WithEvents lblTargeType As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim MyTargeting As New Targeting
    Dim MyTargetType As String
    Dim x As Integer
    Public SumQ1, SumQ2, SumQ3, SumQ4, SumTotal, SumQ As Double
    Dim MyTraget As New Targeting
    Dim MyJs As New JSPopUp(Me)
    Dim my As New Wyeth.Utilities.WyethTextBox


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Page.IsPostBack = True Then
         
        Else
            GetTPGDD(ddTPG, Session("country_id"))
            GetYearDD4(ddYear, 2004, 1)
            GetSaReForTPGDD(ddSare, ddTPG.SelectedValue)
            GetVersionDD(ddVersion, ddSare.SelectedValue, ddYear.SelectedValue, ddTPG.SelectedValue)

            If Request.QueryString("sare_id") <> "" Then
                GetSaReForTPGDD(ddSare, Request.QueryString("tpg_id"))
                ddYear.SelectedValue = Request.QueryString("year")
                ddTPG.SelectedValue = Request.QueryString("tpg_id")
                ddSare.SelectedValue = Request.QueryString("sare_id")
                GetVersionDD(ddVersion, ddSare.SelectedValue, ddYear.SelectedValue, ddTPG.SelectedValue)
                BindData()
            End If
        End If

        MyJs.Height = 500
        MyJs.Width = 400
        MyJs.PageURL = "NewTargetVersion.aspx?sare_id=" & ddSare.SelectedValue & "&year=" & ddYear.SelectedValue & "&tpg_id=" & ddTPG.SelectedValue
        MyJs.AddPopupToControl(btn_new_version)
        settargetType()
        setButtons()

    End Sub
    Private Sub settargetType()
        MyTargetType = MyTargeting.GetTargetType(CInt(ddTPG.SelectedValue))
        If MyTargetType = "UNIT" Then
            x = 0
        ElseIf MyTargetType = "VALUE" Then
            x = 2
        End If

        Me.lblCurrency.Text = "Target Type: UNIT"

    End Sub

    Private Sub Page_PreRender(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
        If x = 2 Then
            lblCurrency.Visible = True
            lblTargetType.Visible = True
            lblTargetType.Text = "Targets in:<font color=red>Values<red>"
            lblTargetType.CssClass = "currency"
            lblCurrency.CssClass = "currency"
            lblCurrency.Text = "Currency: <font color=red>" & CStr(Session("currency_code")) & "</font>"

        ElseIf x = 0 Then
            lblCurrency.Visible = False
            lblTargetType.Visible = True
            lblTargetType.Text = "Targets in:<font color=red> Units</font>"
            lblTargetType.CssClass = "currency"
        End If

        lblSare.Text = ddSare.SelectedItem.ToString
        Me.lblSumQ1.Text = MyNumberFormat(SumQ1, x)
        Me.lblSumQ2.Text = MyNumberFormat(SumQ2, x)
        Me.lblSumQ3.Text = MyNumberFormat(SumQ3, x)
        Me.lblSumQ4.Text = MyNumberFormat(SumQ4, x)
        Me.lblSumTotal.Text = MyNumberFormat(SumTotal, x)

    End Sub

    Private Sub BindData()
        TargetRepeater.DataSource = MyTargeting.GetTaregtsForInput(ddTPG.SelectedValue, ddSare.SelectedValue, ddYear.SelectedValue, ddVersion.SelectedValue)
        TargetRepeater.DataBind()
    End Sub
    Private Sub Item_databound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles TargetRepeater.ItemDataBound

        Dim myTextBox As New TextBox
        Dim MyLabel As New Label


        SumQ = SumQ + Convert.ToDouble(CType(e.Item.FindControl("txtQ1"), TextBox).Text)
        SumQ = SumQ + Convert.ToDouble(CType(e.Item.FindControl("txtQ2"), TextBox).Text)
        SumQ = SumQ + Convert.ToDouble(CType(e.Item.FindControl("txtQ3"), TextBox).Text)
        SumQ = SumQ + Convert.ToDouble(CType(e.Item.FindControl("txtQ4"), TextBox).Text)

        MyLabel = e.Item.FindControl("lblSumQ")
        MyLabel.Text = MyNumberFormat(SumQ, x)


        SumQ1 = SumQ1 + Convert.ToDouble(CType(e.Item.FindControl("txtQ1"), TextBox).Text)
        SumQ2 = SumQ2 + Convert.ToDouble(CType(e.Item.FindControl("txtQ2"), TextBox).Text)
        SumQ3 = SumQ3 + Convert.ToDouble(CType(e.Item.FindControl("txtQ3"), TextBox).Text)
        SumQ4 = SumQ4 + Convert.ToDouble(CType(e.Item.FindControl("txtQ4"), TextBox).Text)

        SumTotal = SumTotal + SumQ

        SumQ = 0
    End Sub

    Private Sub ddTPG_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddTPG.SelectedIndexChanged
        GetSaReForTPGDD(ddSare, ddTPG.SelectedValue)
        GetVersionDD(ddVersion, ddSare.SelectedValue, ddYear.SelectedValue, ddTPG.SelectedValue)


        MyJs.PageURL = "NewTargetVersion.aspx?sare_id=" & ddSare.SelectedValue & "&year=" & ddYear.SelectedValue & "&tpg_id=" & ddTPG.SelectedValue
        MyJs.AddPopupToControl(btn_new_version)

        setButtons()
    End Sub

    Private Sub ddYear_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddYear.SelectedIndexChanged
        GetVersionDD(ddVersion, ddYear.SelectedValue, ddSare.SelectedValue, ddTPG.SelectedValue)
        setButtons()
    End Sub

    Private Sub btn_show_targets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_show_targets.Click
        EditPanel.Visible = True
        lblsuccess.Visible = False
        BindData()


    End Sub

    Private Sub ddSare_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddSare.SelectedIndexChanged
        GetVersionDD(ddVersion, ddSare.SelectedValue, ddYear.SelectedValue, ddTPG.SelectedValue)

        MyJs.Height = 400
        MyJs.Width = 400
        MyJs.PageURL = "NewTargetVersion.aspx?sare_id=" & ddSare.SelectedValue & "&year=" & ddYear.SelectedValue & "&tpg_id=" & ddTPG.SelectedValue
        MyJs.AddPopupToControl(btn_new_version)
        setButtons()

    End Sub
    Private Sub btn_Update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_save_editpanel.Click
        Try
            EditPanel.Visible = True
            Me.Validate()
            If Page.IsValid = True Then

                Dim q1, q2, q3, q4 As Double
                Dim str_id As String
                Dim str_sql As String
                Dim target_date As String
                Dim tapg_id, sare_id, prod_cc_id As Integer
                Dim target_value As Double
                Dim MytxtBox As WyethTextBox
                Dim insert_bol As Boolean
                Dim MyControl As TextBox
                Dim MyObject As System.Web.UI.HtmlControls.HtmlInputHidden
                Dim i As Integer = 0
                tapg_id = ddTPG.SelectedValue

                MyTraget.DeleteTargetVersion(ddYear.SelectedValue, ddSare.SelectedValue, ddVersion.SelectedValue, ddTPG.SelectedValue)

                For Each Control As Control In TargetRepeater.Controls


                    MyControl = CType(Control.FindControl("txtQ1"), WyethTextBox)
                    MyControl.TabIndex = CInt("10" & i)
                    q1 = MyControl.Text


                    MyControl = CType(Control.FindControl("txtQ2"), WyethTextBox)
                    MyControl.TabIndex = CInt("20" & i)
                    q2 = MyControl.Text


                    MyControl = CType(Control.FindControl("txtQ3"), WyethTextBox)
                    MyControl.TabIndex = CInt("30" & i)
                    q3 = MyControl.Text

                    MyControl = CType(Control.FindControl("txtQ4"), WyethTextBox)
                    MyControl.TabIndex = CInt("40" & i)
                    q4 = MyControl.Text

                    MyObject = CType(Control.FindControl("txtCCid"), HtmlControls.HtmlInputHidden)
                    prod_cc_id = MyObject.Value


                    MyTargeting.InsertTargetVersion(ddYear.SelectedValue, prod_cc_id, ddSare.SelectedValue, ddVersion.SelectedValue, q1, q2, q3, q4, Session("user_id"), tapg_id)


                    i = i + 1

                Next

                lblsuccess.Visible = True
                lblsuccess.CssClass = "success"
                lblsuccess.Text = "targets sucessfully saved"
                BindData()
            Else
                lblsuccess.Visible = True
                lblsuccess.CssClass = "nosuccess"
                lblsuccess.Text = "Error while saving targets"
            End If

        Catch ex As Exception
            ExceptionInfo.Show(ex)
            lblsuccess.CssClass = "nosuccess"
            lblsuccess.Text = "Error while saving targets"
        Finally

        End Try


    End Sub


    Private Sub ddVersion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddVersion.SelectedIndexChanged
        setButtons()
    End Sub

    Private Sub setButtons()
        If ddSare.SelectedValue = 0 Then
            btn_new_version.Visible = False
            btn_show_targets.Visible = False
        ElseIf ddVersion.SelectedValue = 0 Then
            btn_new_version.Visible = True
            btn_show_targets.Visible = False
        ElseIf ddVersion.SelectedItem.ToString.EndsWith("not approved") = True Then
            btn_new_version.Visible = False
            btn_show_targets.Visible = True
        ElseIf ddVersion.SelectedItem.ToString.EndsWith("no versions") = True Then
            btn_new_version.Visible = True
            btn_show_targets.Visible = False
        ElseIf checkForVersion() = False Then
            btn_new_version.Visible = False
            btn_show_targets.Visible = True
        Else
            btn_new_version.Visible = True
            btn_show_targets.Enabled = True
        End If

        If ddVersion.SelectedItem.Text.EndsWith("not approved") Then
            Button_save_editpanel.Visible = True
        Else
            Button_save_editpanel.Visible = False
        End If

        Me.EditPanel.Visible = False

    End Sub
    Private Function checkForVersion() As Boolean
        Dim ret_val As Boolean = True


        For Each item As ListItem In ddVersion.Items()
            If item.Text.EndsWith("not approved") Then
                ret_val = False
            End If

        Next
        Return ret_val

    End Function

    Private Sub MyRepeater_ItemDatabound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles TargetRepeater.ItemDataBound
        Dim myText As WyethTextBox
        myText = CType(e.Item.FindControl("txtQ1"), Wyeth.Utilities.WyethTextBox)
        myText.Text = MyNumberFormat(CDbl(myText.Text), x)
        myText.Attributes.Add("onkeydown", "return submitButton(Button_save_editpanel);")

        myText = CType(e.Item.FindControl("txtQ2"), Wyeth.Utilities.WyethTextBox)
        myText.Text = MyNumberFormat(CDbl(myText.Text), x)
        myText.Attributes.Add("onkeydown", "return submitButton(Button_save_editpanel);")


        myText = CType(e.Item.FindControl("txtQ3"), Wyeth.Utilities.WyethTextBox)
        myText.Text = MyNumberFormat(CDbl(myText.Text), x)
        myText.Attributes.Add("onkeydown", "return submitButton(Button_save_editpanel);")


        myText = CType(e.Item.FindControl("txtQ4"), Wyeth.Utilities.WyethTextBox)
        myText.Text = MyNumberFormat(CDbl(myText.Text), x)
        myText.Attributes.Add("onkeydown", "return submitButton(Button_save_editpanel);")

        Dim mylabel As Label
        mylabel = CType(e.Item.FindControl("lblSumQ"), Label)
        '  mylabel.Text = MyNumberFormat(CDbl(mylabel.Text), 2)

    End Sub



    '**************************************************************************************************************
    '* For Printing
    '**************************************************************************************************************
    Private Sub btnPrint_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Print_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Autoprint Or PopupWindowStyle.NoToolbar)

        BindData()
    End Sub

    Private Sub btnPreview_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Preview_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Fullscreen)

        BindData()
    End Sub

    Private Function PrePrint() As printReportUtil
        BindData()

        Dim preview As New printReportUtil
        '  Dim repData As New reportData


        ' repData.addLine("Sales Rep:", ddSare.SelectedItem.ToString, True, False)
        ' repData.addLine("Year:", ddYear.SelectedItem.ToString, True, False)

        preview.PageTitle = Me.ALFPageTitle
        ' preview.AddReportHeader(repdata)
        preview.PageSizeLandscape = 31
        preview.DefaultOrientation = Orientation.Landscape
        preview.AddWebGrid(Me.TargetRepeater)

        Return preview

    End Function
    '**************************************************************************************************************
    '* End Printing
    '**************************************************************************************************************

    'Private Sub ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportExcel.Click
    '    bindData()
    '    Dim exp As exportToExcel = New exportToExcel(Page)
    '    exp.title = Me.ALFPageTitle
    '    exp.addLine("Targets for" & ddSare.SelectedItem.ToString & " for " & ddYear.SelectedItem.ToString)
    '    ' exp.addDataGrid()
    '    exp.export()
    'End Sub


End Class
