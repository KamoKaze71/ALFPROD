Imports Wyeth.Alf.CssStyles

Public Class Currency
	Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
	Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents Button_Add As System.Web.UI.WebControls.Button
	Protected WithEvents GridPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
	Protected WithEvents EditPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents Button_Insert As System.Web.UI.WebControls.Button
	Protected WithEvents Button_update As System.Web.UI.WebControls.Button
	Protected WithEvents Button_Cancel As System.Web.UI.WebControls.Button
	Protected WithEvents txtCurrId As System.Web.UI.WebControls.TextBox
	Protected WithEvents txtCurrCode As System.Web.UI.WebControls.TextBox
	Protected WithEvents txtCurrDescription As System.Web.UI.WebControls.TextBox
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


	Dim MyCurrency As New WyethCurrency
	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		If Page.IsPostBack Then
		Else
			lblPageTitle.Text = Request.QueryString("pageTitle")
			BindData()
		End If
	End Sub
	Private Sub BindData()
		SetGridStyles(MyGrid)		'Apply default Css Style Settings
		MyGrid.DataSource = MyCurrency.GetCurrencies()
		MyGrid.DataBind()
	End Sub

	Public Sub MyGrid_ItemCommand(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1CommandEventArgs) Handles MyGrid.ItemCommand


		If e.CommandName = "Delete" Then		'Delete this Item

			MyCurrency.CurrID = e.Item.Cells(0).Text

			If (MyCurrency.DeleteCurrency()) Then
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
			Button_update.Visible = True
			GridPanel.Visible = False
			FilterPanel.Visible = False

			txtCurrId.Text = e.Item.Cells(0).Text
			txtCurrCode.Text = e.Item.Cells(1).Text
			txtCurrDescription.Text = e.Item.Cells(2).Text

		End If

	End Sub
	Private Sub Button_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Add.Click

		EditPanel.Visible = True
		Button_update.Visible = False
		Button_Insert.Visible = True
		GridPanel.Visible = False
		FilterPanel.Visible = False

		txtCurrCode.Text = ""
		txtCurrDescription.Text = ""
	End Sub
	Private Sub Button_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Cancel.Click
		GridPanel.Visible = True
		EditPanel.Visible = False
		FilterPanel.Visible = True
	End Sub
	Private Sub Button_Update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_update.Click
		setvalues("update")
		If MyCurrency.UpdateCurrency() Then
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

		If (MyCurrency.InsertCurency()) Then
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
			MyCurrency.CurrID = txtCurrId.Text
		End If
		MyCurrency.CurrUserID = Session("user_id")
		MyCurrency.CurrCode = txtCurrCode.Text
		MyCurrency.CurrDescription = txtCurrDescription.Text
	End Sub

	Private Sub MyGrid_ItemCreated(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated

		If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Then
			e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
			e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor1Class';")

		ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then

			e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
			e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor2Class';")

		End If
		e.Item.Attributes.Add("onclick", "javascript:__doPostBack('" & "MyGrid:" & "_ctl" & (e.Item.ItemIndex + 1) & ":_ctl0','');")

	End Sub
End Class
