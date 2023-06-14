Imports Wyeth.Alf
Imports Wyeth.Alf.CssStyles
Imports Wyeth.Alf.WyethCodes
Imports Wyeth.Alf.JSPopUp
Imports Wyeth.Alf.WyethDropdown

Public Class SalesRep
	Inherits System.Web.UI.Page

    Dim MySalesRep As New WyethSalesRep
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents ddIntranetUser As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtShortName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtIntraUser As System.Web.UI.WebControls.TextBox
    Dim myJs As New JSPopUp(Me)

#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
	Protected WithEvents GridPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents Button_Insert As System.Web.UI.WebControls.Button
	Protected WithEvents Button_Update As System.Web.UI.WebControls.Button
	Protected WithEvents Button_Cancel As System.Web.UI.WebControls.Button
	Protected WithEvents Button_Add As System.Web.UI.WebControls.Button
	Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
	Protected WithEvents txtsare_id As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtsarefirstname As System.Web.UI.WebControls.TextBox
	Protected WithEvents txtsarelastname As System.Web.UI.WebControls.TextBox
	Protected WithEvents ddsarecodetype_id As System.Web.UI.WebControls.DropDownList
	Protected WithEvents EditPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents inpID As System.Web.UI.HtmlControls.HtmlInputHidden
	Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents FilterPanel As System.Web.UI.HtmlControls.HtmlGenericControl

	'NOTE: The following placeholder declaration is required by the Web Form Designer.
	'Do not delete or move it.
	Private designerPlaceholderDeclaration As System.Object

	Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
		'CODEGEN: This method call is required by the Web Form Designer
		'Do not modify it using the code editor.
		InitializeComponent()
	End Sub

#End Region

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		'Put user code to initialize the page here

        If Page.IsPostBack Then

        Else
            lblPageTitle.Text = Request.QueryString("pageTitle")

            ddsarecodetype_id.DataSource = GetCodesByCat("Sales Reps", Session("country_id"))
            ddsarecodetype_id.DataValueField = "code_id"
            ddsarecodetype_id.DataTextField = "code_description"
            ddsarecodetype_id.DataBind()


            GetIntranetUsersDD(ddIntranetUser, Session("country_id"))

        End If

        BindData()



        myJs.ConfirmMessage = "This will delete the Sales Rep and all of its Assignments to Target Product Groups and Targets! Do you really want to delete this SalesRep?"

        txtShortName.ToolTip = "This field is required! It is used to match Pharmosan sales to the Sales Rep"
    End Sub

    Private Sub page_unload(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
        Me.inpID.Value = ""
    End Sub
    Private Sub BindData()
        SetGridStyles(MyGrid)  'Apply default Css Style Settings


        MySalesRep.SalesRepCtryId = Session("country_id")
        MyGrid.EnableViewState = True
        MyGrid.DataSource = MySalesRep.GetSalesReps()
        MyGrid.DataBind()
    End Sub


    Public Sub MyGrid_ItemCommand(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1CommandEventArgs) Handles MyGrid.ItemCommand
        If e.CommandName = "Delete" Then

            MySalesRep.SalesRepId = e.Item.Cells(0).Text

            If (MySalesRep.delete()) Then
                BindData()
            Else
                Dim strScript As String
                strScript = "<script language =javascript >"
                strScript += "window.open('../error.aspx?ErrorId=1','Error','width=300,height=250,left=270,top=180');"
                strScript += "</script>"

                RegisterClientScriptBlock("anything", strScript)
            End If

        ElseIf Me.inpID.Value = "" Then
            EditPanel.Visible = True
            Button_Insert.Visible = False
            Button_Update.Visible = True
            GridPanel.Visible = False
            FilterPanel.Visible = False

            txtsare_id.Text = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sare_id"))).Text
            txtsarefirstname.Text = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sare_first_name"))).Text
            txtsarelastname.Text = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sare_last_name"))).Text
            If e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sare_short_name"))).Text <> "&nbsp;" Then
                txtShortName.Text = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sare_short_name"))).Text
            Else
                txtShortName.Text = ""
            End If

            ddsarecodetype_id.SelectedValue = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("code_id_sales_rep_type"))).Text

            Dim temp As String = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sare_number"))).Text
            If IsNothing(ddIntranetUser.Items.FindByValue(temp)) = False Then
                ddIntranetUser.SelectedValue = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sare_number"))).Text
            Else
                ddIntranetUser.SelectedValue = 0
            End If




        End If

    End Sub

    Private Sub Button_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Add.Click

        EditPanel.Visible = True
        Button_Insert.Visible = True

        GridPanel.Visible = False
        FilterPanel.Visible = False
        Button_Update.Visible = False

        txtsare_id.Text = ""
        txtsarefirstname.Text = ""
        txtsarelastname.Text = ""
        txtShortName.Text = ""
        ddsarecodetype_id.SelectedIndex = 0


    End Sub
    Private Sub Button_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Cancel.Click
        GridPanel.Visible = True
        EditPanel.Visible = False
        FilterPanel.Visible = True
    End Sub
    Private Sub Button_Update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Update.Click
        setvalues("update")
        If MySalesRep.Update() Then
            EditPanel.Visible = False
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
    Private Sub Button_Insert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Insert.Click

        setvalues("insert")


        If (MySalesRep.insert()) Then
            EditPanel.Visible = False
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
    Private Sub setvalues(ByVal type As String)
        If type = "update" Then
            MySalesRep.SalesRepId = txtsare_id.Text
        End If

        MySalesRep.SalesRepFirstName = txtsarefirstname.Text
        MySalesRep.SalesRepLastName = txtsarelastname.Text
        MySalesRep.SalesRepUserId = Session("user_id")
        MySalesRep.SalesRepCtryId = Session("country_id")
        MySalesRep.SalesRepCodeTypeId = ddsarecodetype_id.SelectedValue
        MySalesRep.SalesRepNo = ddIntranetUser.SelectedValue
        MySalesRep.SalesRepShortName = txtShortName.Text
    End Sub
    Private Sub MyGrid_ItemDatabound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Then
            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor1Class';")
            CType(e.Item.Cells(8).Controls(0), WebControl).CssClass = "button_common"
            CType(e.Item.Cells(8).Controls(0), WebControl).Attributes.Add("onclick", "javascript:return getconfirm_sare();")
            e.Item.Attributes.Add("onclick", "javascript:__doPostBack('" & "MyGrid$" & "R" & (e.Item.ItemIndex) & "$_ctl0','');")

            ' myJs.AddGetConfirm(CType(e.Item.Cells(6).Controls(0), WebControl))
        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor2Class';")
            CType(e.Item.Cells(8).Controls(0), WebControl).CssClass = "button_common"
            CType(e.Item.Cells(8).Controls(0), WebControl).Attributes.Add("onclick", "javascript:return getconfirm_sare();")
            e.Item.Attributes.Add("onclick", "javascript:__doPostBack('" & "MyGrid$" & "R" & (e.Item.ItemIndex) & "$_ctl0','');")

            ' myJs.AddGetConfirm(CType(e.Item.Cells(6).Controls(0), WebControl))
        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.SelectedItem Then
            e.Item.Attributes.Add("onclick", "javascript:__doPostBack('" & "MyGrid$" & "R" & (e.Item.ItemIndex) & "$_ctl0','');")
        End If

        e.Item.Attributes.Add("onclick", "javascript:__doPostBack('" & "MyGrid$" & "R" & (e.Item.ItemIndex) & "$_ctl0','');")

    End Sub

End Class
