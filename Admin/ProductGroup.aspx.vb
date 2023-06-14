Imports Wyeth.Alf.CssStyles

Public Class ProductGroup
	Inherits System.Web.UI.Page

	Dim MyProduct As New WyethProduct

#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
	Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents GridPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents EditPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents txtPrGrId As System.Web.UI.WebControls.TextBox
	Protected WithEvents txtPrGrCode As System.Web.UI.WebControls.TextBox
	Protected WithEvents txtPrGrDescription As System.Web.UI.WebControls.TextBox
	Protected WithEvents Button_Add As System.Web.UI.WebControls.Button
	Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
	Protected WithEvents Button_Insert As System.Web.UI.WebControls.Button
	Protected WithEvents Button_update As System.Web.UI.WebControls.Button
	Protected WithEvents Button_cancel As System.Web.UI.WebControls.Button
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
		If Page.IsPostBack Then
			'FilterPanel.Visible = True
		Else
			lblPageTitle.Text = Request.QueryString("pageTitle")

			BindData()
		End If
	End Sub


	Private Sub BindData()
		SetGridStyles(MyGrid)
		MyProduct.ProdCtryId = Session("country_id")
		MyGrid.DataSource = MyProduct.GetProductGroups()
		MyGrid.DataBind()
	End Sub

	Sub MyGrid_ItemCommand(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1CommandEventArgs) Handles MyGrid.ItemCommand

		If e.CommandName = "Delete" Then

			MyProduct.ProdID = e.Item.Cells(0).Text
			If (MyProduct.DeleteProductGroup()) Then
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
			GridPanel.Visible = False
			FilterPanel.Visible = False
			Button_Insert.Visible = False
			Button_update.Visible = True

			txtPrGrId.Text = e.Item.Cells(0).Text
			txtPrGrCode.Text = e.Item.Cells(1).Text
			txtPrGrDescription.Text = e.Item.Cells(2).Text


		End If

	


	End Sub

	Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_cancel.Click
		GridPanel.Visible = True
		EditPanel.Visible = False
		FilterPanel.Visible = True
	End Sub

	Private Sub Button_update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_update.Click
		setvalues("update")
		If MyProduct.UpdateProductGroup() Then
			BindData()
			EditPanel.Visible = False
			GridPanel.Visible = True
			FilterPanel.Visible = True


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
			MyProduct.ProdProductGroupID = txtPrGrId.Text
		End If
		MyProduct.ProdUserID = Session("user_id")
		MyProduct.ProdCtryId = Session("country_id")
		MyProduct.ProdGroupCode = txtPrGrCode.Text
		MyProduct.ProdGroupDescription = txtPrGrDescription.Text


	End Sub

	Private Sub Button_Insert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Insert.Click
		setvalues("insert")


		If (MyProduct.InsertProductGroup()) Then
			EditPanel.Visible = False
			FilterPanel.Visible = True
			GridPanel.Visible = True
			BindData()

		Else
			Dim strScript As String
			strScript = "<script language =javascript >"
			strScript += "window.open('../error.aspx?ErrorId=1','Error','width=300,height=250,left=270,top=180');"
			strScript += "</script>"

			RegisterClientScriptBlock("anything", strScript)
		End If
	End Sub



	Private Sub Button_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Add.Click

		txtPrGrCode.Text = ""
		txtPrGrDescription.Text = ""
		txtPrGrId.Text = ""

		EditPanel.Visible = True
		FilterPanel.Visible = False
		GridPanel.Visible = False
		Button_update.Visible = False
		Button_Insert.Visible = True

	End Sub
	Private Sub MyGrid_ItemCreated(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated

		If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Then

			e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
			e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor1Class';")

			CType(e.Item.Cells(4).Controls(0), WebControl).CssClass = "button_common"
			CType(e.Item.Cells(4).Controls(0), WebControl).Attributes.Add("onclick", "javascript:return getconfirm();")

		ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then

			e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
			e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor2Class';")

			CType(e.Item.Cells(4).Controls(0), WebControl).CssClass = "button_common"
			CType(e.Item.Cells(4).Controls(0), WebControl).Attributes.Add("onclick", "javascript:return getconfirm();")

		End If

        ' e.Item.Attributes.Add("onclick", "javascript:__doPostBack('" & "MyGrid$" & "R" & (e.Item.ItemIndex) & "$_ctl0','');")




	End Sub
End Class
