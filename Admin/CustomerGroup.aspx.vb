Imports Wyeth.Utilities
Imports Wyeth.Alf.CssStyles


Public Class CustomerGroup
	Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
	Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents GridPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents EditPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents Button_Add As System.Web.UI.WebControls.Button
	Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
	Protected WithEvents Button_Insert As System.Web.UI.WebControls.Button
	Protected WithEvents Button_update As System.Web.UI.WebControls.Button
	Protected WithEvents Button_Cancel As System.Web.UI.WebControls.Button
	Protected WithEvents txtCustomerGrID As System.Web.UI.WebControls.TextBox
	Protected WithEvents txtCustomerGrCode As System.Web.UI.WebControls.TextBox
	Protected WithEvents txtCustomerGrDescription As System.Web.UI.WebControls.TextBox
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
	Dim MyCustomerGr As New WyethCustomer
	Dim MyDataView As DataView
	Dim strForm, strMouseOver, strResult As String

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


		MyCustomerGr.Cust_Country_Id = Session("country_id")
		MyGrid.DataSource = MyCustomerGr.GetCustomerGroups()
		MyGrid.DataBind()
	End Sub


	Public Sub MyGrid_ItemCommand(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1CommandEventArgs) Handles MyGrid.ItemCommand

		If e.CommandName = "Delete" Then


			MyCustomerGr.Cust_Group_Id = e.Item.Cells(0).Text

			If (MyCustomerGr.DeleteCustomerGroup) Then
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

			txtCustomerGrID.Text = e.Item.Cells(0).Text
			txtCustomerGrCode.Text = e.Item.Cells(1).Text
			txtCustomerGrDescription.Text = e.Item.Cells(2).Text

		End If
		



	End Sub
	
	Private Sub Button_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Add.Click

		EditPanel.Visible = True
		Button_update.Visible = False
		Button_Insert.Visible = True
		GridPanel.Visible = False
		FilterPanel.Visible = False

		txtCustomerGrID.Text = ""
		txtCustomerGrCode.Text = ""
		txtCustomerGrDescription.Text = ""
	End Sub
	Private Sub Button_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Cancel.Click
		GridPanel.Visible = True
		EditPanel.Visible = False
		FilterPanel.Visible = True
	End Sub
	Private Sub Button_Update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_update.Click
		setvalues("update")
		If MyCustomerGr.UpdateCustomerGroup() Then

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




		If (MyCustomerGr.InsertCustomerGroup()) Then
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
			MyCustomerGr.Cust_Group_Id = CType(txtCustomerGrID.Text.ToString, Integer)
		End If
		MyCustomerGr.CustomerGroupCode = txtCustomerGrCode.Text
		MyCustomerGr.CustomerGroupDescription = txtCustomerGrDescription.Text
		MyCustomerGr.Cust_Country_Id = Session("country_id")
		MyCustomerGr.Cust_User_Id = Session("user_id")


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
		 e.Item.Attributes.Add("onclick", "javascript:__doPostBack('" & "MyGrid$" & "R" & (e.Item.ItemIndex) & "$_ctl0','');")




	End Sub
End Class
