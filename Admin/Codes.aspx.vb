Imports Wyeth.Alf.CssStyles

Public Class Codes
	Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents FilterPanel As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents GridPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents EditPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents Button_insert As System.Web.UI.WebControls.Button
	Protected WithEvents Button_update As System.Web.UI.WebControls.Button
	Protected WithEvents Button_cancel As System.Web.UI.WebControls.Button
	Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
	Protected WithEvents Button_Add As System.Web.UI.WebControls.Button
	Protected WithEvents txtCodeID As Wyeth.Utilities.WyethTextBox
	Protected WithEvents txtCodeCode As Wyeth.Utilities.WyethTextBox
	Protected WithEvents txtCodeDescription As System.Web.UI.WebControls.TextBox
	Protected WithEvents txtCodeCategory As System.Web.UI.WebControls.TextBox
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
	Dim MyCode As New WyethCodes

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		If Page.IsPostBack Then
		Else
			lblPageTitle.Text = Request.QueryString("pageTitle")
			BindData()
		End If


	End Sub
	Private Sub BindData()
		SetGridStyles(MyGrid)		'Apply default Css Style Settings

		MyGrid.DataSource = MyCode.GetCodesByCat("", Session("country_id"))
		MyGrid.DataBind()
	End Sub


	Public Sub MyGrid_ItemCommand(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1CommandEventArgs) Handles MyGrid.ItemCommand
		If e.CommandName = "Delete" Then
			MyCode.CodeID = e.Item.Cells(0).Text

			If (MyCode.DeleteCode()) Then
				BindData()
			Else
				Dim strScript As String
				strScript = "<script language =javascript >"
				strScript += "window.open('../error.aspx?ErrorId=1','Error','width=300,height=250,left=270,top=180');"
				strScript += "</script>"

                RegisterClientScriptBlock("anything", strScript)

			End If

        ElseIf Me.inpID.Value = "" And e.CommandName.StartsWith("Sort") = False Then
            EditPanel.Visible = True
            Button_insert.Visible = False
            Button_update.Visible = True
            GridPanel.Visible = False
            FilterPanel.Visible = False

            txtCodeID.Text = e.Item.Cells(0).Text
            txtCodeCode.Text = e.Item.Cells(1).Text
            txtCodeDescription.Text = e.Item.Cells(2).Text
            txtCodeCategory.Text = e.Item.Cells(3).Text

        ElseIf e.CommandName.StartsWith("Sort") = True Then

        End If




	End Sub

	Private Sub Button_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Add.Click

		EditPanel.Visible = True
		Button_update.Visible = False
		Button_insert.Visible = True
		GridPanel.Visible = False
		FilterPanel.Visible = False

		txtCodeCategory.Text = ""
		txtCodeCode.Text = ""
		txtCodeDescription.Text = ""

	End Sub
	Private Sub Button_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_cancel.Click
		GridPanel.Visible = True
		EditPanel.Visible = False
		FilterPanel.Visible = True
	End Sub
	Private Sub Button_Update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_update.Click
		setvalues("update")
		If MyCode.UpdateCode() Then

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
	Private Sub Button_Insert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_insert.Click
		setvalues("insert")

		If (MyCode.InsertCode()) Then
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
			MyCode.CodeID = txtCodeID.Text
		End If
		MyCode.CodeCtryID = Session("country_id")
		MyCode.CodeUserID = Session("user_id")
		MyCode.CodeCode = txtCodeCode.Text
		MyCode.CodeDescription = txtCodeDescription.Text
		MyCode.CodeCategory = txtCodeCategory.Text




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

		End If
       
    End Sub
    Private Sub C1WebGrid1_SortCommand(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1SortCommandEventArgs) Handles MyGrid.SortCommand
        bindData()
    End Sub

End Class
