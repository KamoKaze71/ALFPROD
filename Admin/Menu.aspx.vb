Imports Wyeth.Alf.CssStyles
Imports Wyeth.Alf.WyethDropdown


Imports Oracle.DataAccess.Client
Imports wyeth.Utilities.MyConnection
Imports wyeth.Utilities


Public Class Menu1
	Inherits System.Web.UI.Page

	Dim MyMenu As New Menu
	Dim MyReader As OracleDataReader
    Protected WithEvents ddMenuModulID As System.Web.UI.WebControls.DropDownList
    Dim MyReaderSub As OracleDataReader
    Protected WithEvents chk_display As System.Web.UI.WebControls.CheckBox
    Dim int_display As Integer


#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents FilterPanel As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents GridPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents EditPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents Button_Add As System.Web.UI.WebControls.Button
	Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
	Protected WithEvents Button_Update As System.Web.UI.WebControls.Button
	Protected WithEvents Button_Insert As System.Web.UI.WebControls.Button
	Protected WithEvents Button_Cancel As System.Web.UI.WebControls.Button
	Protected WithEvents txtMenuID As System.Web.UI.WebControls.TextBox
	Protected WithEvents txtMenuName As System.Web.UI.WebControls.TextBox
	Protected WithEvents txtMenuLabel As System.Web.UI.WebControls.TextBox
	Protected WithEvents txtMenuLink As System.Web.UI.WebControls.TextBox
	Protected WithEvents txtMenuTarget As System.Web.UI.WebControls.TextBox
	Protected WithEvents txtMenuDisplayNo As System.Web.UI.WebControls.TextBox
	Protected WithEvents txtMenuCategory As System.Web.UI.WebControls.TextBox
	Protected WithEvents ddMenuIDParent As System.Web.UI.WebControls.DropDownList
	Protected WithEvents inpID As System.Web.UI.HtmlControls.HtmlInputHidden
	Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label

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
			MyMenu.MenuCtryID = Session("country_id")
			MyMenu.MenuIDParent = 0
			MyMenu.Category = "leftnavi"
			MyMenu.GetMenuIDParent(ddMenuIDParent)

            Dim li As New ListItem
            li.Text = "Root"
            li.Value = "0"
            ddMenuIDParent.Items.Insert(0, li)
            ddMenuIDParent.DataBind()

           
            ddMenuModulID.DataSource = MyMenu.GetModulesIDs
            ddMenuModulID.DataValueField = "id_access"
            ddMenuModulID.DataTextField = "task"
            ddMenuModulID.DataBind()
            Dim liAcc As New ListItem
            liAcc.Text = "Select Module Section"
            liAcc.Value = "0"
            ddMenuModulID.Items.Insert(0, liAcc)

			BindData()
		End If


	End Sub
	Private Sub BindData()
		SetGridStyles(MyGrid)		'Apply default Css Style Settings
        MyGrid.DataSource = MyMenu.GetMenu()
		MyGrid.DataBind()
	End Sub


	Public Sub MyGrid_ItemCommand(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1CommandEventArgs) Handles MyGrid.ItemCommand
		If e.CommandName = "Delete" Then

            MyMenu.MenuID = e.Item.Cells(0).Text

            If (MyMenu.DeleteMenuEntry()) Then
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

        ElseIf Me.inpID.Value = "" Then

			EditPanel.Visible = True
			Button_Insert.Visible = False
			Button_Update.Visible = True
			GridPanel.Visible = False
			FilterPanel.Visible = False

            txtMenuID.Text = e.Item.Cells(0).Text.Replace("&nbsp;", "")
            txtMenuName.Text = e.Item.Cells(1).Text.Replace("&nbsp;", "")
            txtMenuLabel.Text = e.Item.Cells(2).Text.Replace("&nbsp;", "")
            txtMenuLink.Text = e.Item.Cells(3).Text.Replace("&nbsp;", "")
            txtMenuTarget.Text = e.Item.Cells(4).Text.Replace("&nbsp;", "")
            txtMenuDisplayNo.Text = e.Item.Cells(5).Text.Replace("&nbsp;", "")
            txtMenuCategory.Text = e.Item.Cells(6).Text.Replace("&nbsp;", "")

            ddMenuIDParent.SelectedValue = e.Item.Cells(7).Text
            ddMenuModulID.SelectedValue = e.Item.Cells(10).Text

            If e.Item.Cells(11).Text = 1 Then
                Me.chk_display.Checked = True
            Else
                Me.chk_display.Checked = False
            End If



            End If

	End Sub
	
	Private Sub Button_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Add.Click

		EditPanel.Visible = True
		Button_Update.Visible = False
		Button_Insert.Visible = True
		GridPanel.Visible = False
		FilterPanel.Visible = False

		txtMenuID.Text = ""
		txtMenuDisplayNo.Text = ""
		txtMenuLabel.Text = ""
		txtMenuLink.Text = ""
		ddMenuIDParent.SelectedIndex = 0
		txtMenuName.Text = ""
        txtMenuCategory.Text = "leftnavi"
        txtMenuTarget.Text = ""
        chk_display.Checked = True
	End Sub
	Private Sub Button_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Cancel.Click
		GridPanel.Visible = True
		EditPanel.Visible = False
		FilterPanel.Visible = True
	End Sub
	Private Sub Button_Update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Update.Click
		setvalues("update")
		If MyMenu.UpdateMenuEntry() Then
			BindData()
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

		If (MyMenu.InsertMenuEntry()) Then
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
			MyMenu.MenuID = txtMenuID.Text
		End If
		MyMenu.MenuCtryID = Session("country_id")
		MyMenu.MenuUserID = Session("user_id")
		MyMenu.MenuDisplayNo = txtMenuDisplayNo.Text
		MyMenu.MenuLabel = txtMenuLabel.Text
		MyMenu.MenuLink = txtMenuLink.Text
		MyMenu.MenuIDParent = ddMenuIDParent.SelectedValue
		MyMenu.MenuName = txtMenuName.Text
		MyMenu.MenuTarget = txtMenuTarget.Text
        MyMenu.Category = txtMenuCategory.Text
        MyMenu.MenuAccessId = ddMenuModulID.SelectedValue
        If chk_display.Checked Then
            int_display = 1
        Else
            int_display = 0
        End If
        MyMenu.MenuDisplay = int_display

	End Sub
	Private Sub MyGrid_ItemCreated(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated

		If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Then
			e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
			e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor1Class';")
            CType(e.Item.Cells(9).Controls(0), WebControl).CssClass = "button_common"
            CType(e.Item.Cells(9).Controls(0), WebControl).Attributes.Add("onclick", "javascript:return getconfirm();")
            e.Item.Attributes.Add("onclick", "javascript:__doPostBack('" & "MyGrid$" & "R" & (e.Item.ItemIndex) & "$_ctl0','');")

		ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then

			e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
			e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor2Class';")
            CType(e.Item.Cells(9).Controls(0), WebControl).CssClass = "button_common"
            CType(e.Item.Cells(9).Controls(0), WebControl).Attributes.Add("onclick", "javascript:return getconfirm();")
            e.Item.Attributes.Add("onclick", "javascript:__doPostBack('" & "MyGrid$" & "R" & (e.Item.ItemIndex) & "$_ctl0','');")

        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.SelectedItem Then
            CType(e.Item.Cells(9).Controls(0), WebControl).CssClass = "button_common"
            CType(e.Item.Cells(9).Controls(0), WebControl).Attributes.Add("onclick", "javascript:return getconfirm();")
            e.Item.Attributes.Add("onclick", "javascript:__doPostBack('" & "MyGrid$" & "R" & (e.Item.ItemIndex) & "$_ctl0','');")

        End If
        '    e.Item.Attributes.Add("onclick", "javascript:__doPostBack('" & "MyGrid$" & "R" & (e.Item.ItemIndex) & "_ctl0','');")
      
	End Sub
End Class
