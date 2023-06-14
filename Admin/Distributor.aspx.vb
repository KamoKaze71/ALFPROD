Imports Wyeth.Alf.CssStyles

Public Class Distributor
	Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents GridPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents EditPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents Button_Insert As System.Web.UI.WebControls.Button
	Protected WithEvents Button_update As System.Web.UI.WebControls.Button
	Protected WithEvents Button_Cancel As System.Web.UI.WebControls.Button
	Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
	Protected WithEvents Button_add As System.Web.UI.WebControls.Button
	Protected WithEvents txtDistNo As System.Web.UI.WebControls.TextBox
	Protected WithEvents txtDistName As System.Web.UI.WebControls.TextBox
	Protected WithEvents txtDistDescription As System.Web.UI.WebControls.TextBox
	Protected WithEvents txtxDistID As System.Web.UI.WebControls.TextBox
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

	Dim MyDistributor As New WyethDistributor

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		'Put user code to initialize the page here

		If Page.IsPostBack Then
		Else
			lblPageTitle.Text = Request.QueryString("pageTitle")
			BindData()
		End If


	End Sub
	Private Sub BindData()
		SetGridStyles(MyGrid)		'Apply default Css Style Settings

		MyDistributor.DistCtryID = Session("country_id")
		MyGrid.DataSource = MyDistributor.GetDistributors()
		MyGrid.DataBind()
	End Sub


	Public Sub MyGrid_ItemCommand(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1CommandEventArgs) Handles MyGrid.ItemCommand
		If e.CommandName = "Delete" Then
			MyDistributor.DistID = e.Item.Cells(0).Text

			If (MyDistributor.DeleteDistributors()) Then
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

			txtxDistID.Text = e.Item.Cells(0).Text
			txtDistNo.Text = e.Item.Cells(1).Text
			txtDistName.Text = e.Item.Cells(2).Text
			txtDistDescription.Text = e.Item.Cells(3).Text

		End If

	End Sub

	Private Sub Button_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_add.Click

		EditPanel.Visible = True
		Button_update.Visible = False
		Button_Insert.Visible = True
		GridPanel.Visible = False
		FilterPanel.Visible = False

		txtDistDescription.Text = ""
		txtDistName.Text = ""
		txtDistNo.Text = ""

	End Sub
	Private Sub Button_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Cancel.Click
		GridPanel.Visible = True
		EditPanel.Visible = False
		FilterPanel.Visible = True
	End Sub
	Private Sub Button_Update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_update.Click
		setvalues("update")
		If MyDistributor.UpdateDistributor() Then
			BindData()
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
		End If

	End Sub
	Private Sub Button_Insert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Insert.Click
		setvalues("insert")

		If (MyDistributor.InsertDistributor()) Then
			EditPanel.Visible = False
			GridPanel.Visible = True
			FilterPanel.Visible = True
			BindData()
		Else
			Dim strScript As String
			strScript = "<script language =javascript >"
			strScript += "window.open('../error.aspx?ErrorId=3','Error','width=300,height=250,left=270,top=180');"
			strScript += "</script>"

			RegisterClientScriptBlock("anything", strScript)
		End If
	End Sub
	Private Sub setvalues(ByVal type As String)
		If type = "update" Then
			MyDistributor.DistID = txtxDistID.Text
		End If
		MyDistributor.DistNumber = txtDistNo.Text
		MyDistributor.DistCtryID = Session("country_id")
		MyDistributor.DistUserID = Session("user_id")
		MyDistributor.DistName = txtDistName.Text
		MyDistributor.DistDescription = txtDistDescription.Text
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
            e.Item.Attributes.Add("onclick", "javascript:__doPostBack('" & "MyGrid$" & "R" & (e.Item.ItemIndex) & "$_ctl0','');")
            CType(e.Item.Cells(5).Controls(0), WebControl).CssClass = "button_common"
            CType(e.Item.Cells(5).Controls(0), WebControl).Attributes.Add("onclick", "javascript:return getconfirm();")

        End If
		
	End Sub
End Class
