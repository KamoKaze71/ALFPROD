Imports Wyeth.Alf.CssStyles

Public Class ApplicationSettings
	Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents FilterPanel As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents GridPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents EditPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents Button_Insert As System.Web.UI.WebControls.Button
	Protected WithEvents Button_Update As System.Web.UI.WebControls.Button
	Protected WithEvents Button_Cancel As System.Web.UI.WebControls.Button
	Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
	Protected WithEvents Button_add As System.Web.UI.WebControls.Button
	Protected WithEvents txtapseID As System.Web.UI.WebControls.TextBox
	Protected WithEvents txtApseVariable As System.Web.UI.WebControls.TextBox
	Protected WithEvents txtapseValue As System.Web.UI.WebControls.TextBox
	Protected WithEvents txtApSeDescription As System.Web.UI.WebControls.TextBox
	Protected WithEvents inpID As System.Web.UI.HtmlControls.HtmlInputHidden
	Protected WithEvents txtdelete As System.Web.UI.WebControls.TextBox
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

	Dim MyApplicationSetting As New WyethAppllication
	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		If Page.IsPostBack Then
		Else
			lblPageTitle.Text = Request.QueryString("pageTitle")
			BindData()
		End If


	End Sub
	Private Sub BindData()
		Dim strScript As String

		strScript = "<script language =Javascript>"
		strScript += "function getconfirm() "
		strScript += "{ "
		strScript += "if (confirm('Do you want to delete this record?')==true)"
		strScript += "{window.document.form[0].inpID.value='deleted';"
		strScript += "return true;} "
		strScript += "Else "
		strScript += "return false; "
		strScript += "}"
		strScript += "</script>"

		'RegisterClientScriptBlock("Confirm", strScript)

			SetGridStyles(MyGrid)		   'Apply default Css Style Settings
			MyApplicationSetting.ApSe_Country_ID = Session("country_id")
			MyGrid.DataSource = MyApplicationSetting.GetApplicationSettings()
		MyGrid.DataBind()
	End Sub


	Public Sub MyGrid_ItemCommand(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1CommandEventArgs) Handles MyGrid.ItemCommand
		If e.CommandName = "Delete" Then
			MyApplicationSetting.ApSe_ID = e.Item.Cells(0).Text

			If (MyApplicationSetting.DeleteApplicationSetting()) Then
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

			txtapseID.Text = e.Item.Cells(0).Text
			txtApseVariable.Text = e.Item.Cells(1).Text
            txtapseValue.Text = e.Item.Cells(2).Text
            txtApSeDescription.Text = e.Item.Cells(3).Text
		End If

		Me.inpID.Value = ""


	End Sub
	Private Sub Button_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_add.Click

		EditPanel.Visible = True
		Button_Update.Visible = False
		Button_Insert.Visible = True
		GridPanel.Visible = False
		FilterPanel.Visible = False

		txtapseID.Text = ""
		txtApseVariable.Text = ""
		txtapseValue.Text = ""
	End Sub
	Private Sub Button_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Cancel.Click
		GridPanel.Visible = True
		EditPanel.Visible = False
		FilterPanel.Visible = True
	End Sub
	Private Sub Button_Update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Update.Click
		setvalues("update")
		If MyApplicationSetting.UpdateApplicationSetting() Then
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


		If (MyApplicationSetting.InsertApplicationSetting()) Then
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
			MyApplicationSetting.ApSe_ID = txtapseID.Text
		End If

		MyApplicationSetting.ApSe_User_ID = Session("user_id")
		MyApplicationSetting.ApSe_Country_ID = Session("country_id")
		MyApplicationSetting.ApSe_Value = txtapseValue.Text
		MyApplicationSetting.ApSe_Variable = txtApseVariable.Text
		MyApplicationSetting.ApSe_Description = txtApSeDescription.Text


	End Sub
	Private Sub MyGrid_ItemCreated(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated

		If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Then
			e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
			e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor1Class';")
			CType(e.Item.Cells(5).Controls(0), WebControl).CssClass = "button_common"
			CType(e.Item.Cells(5).Controls(0), WebControl).Attributes.Add("onclick", "javascript:return getconfirm();")
            e.Item.Attributes.Add("onclick", "javascript:__doPostBack('" & "MyGrid$" & "R" & (e.Item.ItemIndex) & "$_ctl0','');")

		ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then

			e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
			e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor2Class';")
			CType(e.Item.Cells(5).Controls(0), WebControl).CssClass = "button_common"
			CType(e.Item.Cells(5).Controls(0), WebControl).Attributes.Add("onclick", "javascript:return getconfirm();")
            e.Item.Attributes.Add("onclick", "javascript:__doPostBack('" & "MyGrid$" & "R" & (e.Item.ItemIndex) & "$_ctl0','');")

        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.SelectedItem Then

            CType(e.Item.Cells(5).Controls(0), WebControl).CssClass = "button_common"
            CType(e.Item.Cells(5).Controls(0), WebControl).Attributes.Add("onclick", "javascript:return getconfirm();")
            e.Item.Attributes.Add("onclick", "javascript:__doPostBack('" & "MyGrid$" & "R" & (e.Item.ItemIndex) & "$_ctl0','');")

        End If
		
	End Sub


End Class
