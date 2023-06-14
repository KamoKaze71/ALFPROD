Imports Wyeth.Alf.CssStyles




Public Class CustSare
	Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

	End Sub
	Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
	Protected WithEvents dd_sare As System.Web.UI.WebControls.DropDownList
	Protected WithEvents GridPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents EditPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents dd_cust As System.Web.UI.WebControls.DropDownList
	Protected WithEvents Button_insert As System.Web.UI.WebControls.Button
	Protected WithEvents Button_update As System.Web.UI.WebControls.Button
	Protected WithEvents Button_cancel As System.Web.UI.WebControls.Button
	Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents txtpercent As System.Web.UI.WebControls.TextBox
	Protected WithEvents dd_edit_sare As System.Web.UI.WebControls.DropDownList
	Protected WithEvents Button_Add As System.Web.UI.WebControls.Button
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
	Dim MySare As New WyethSalesRep
	Dim MyCustomer As New WyethCustomer

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		If Page.IsPostBack Then
		Else
			lblPageTitle.Text = Request.QueryString("pageTitle")
			BindData()
		End If


	End Sub
	Private Sub BindData()
		SetGridStyles(MyGrid)		'Apply default Css Style Settings
		MySare.SalesRepCtryId = Session("country_id")

		dd_sare.DataSource = MySare.GetSalesReps()
		dd_sare.DataTextField = "sare_name"
		dd_sare.DataValueField = "sare_id"
		dd_sare.SelectedIndex = 0
		dd_sare.DataBind()

		dd_edit_sare.DataSource = MySare.GetSalesReps()
		dd_edit_sare.DataTextField = "sare_name"
		dd_edit_sare.DataValueField = "sare_id"
		dd_edit_sare.SelectedIndex = 0
		dd_edit_sare.DataBind()

		MyCustomer.Cust_Country_Id = Session("country_id")
		dd_cust.DataSource = MyCustomer.GetCustomerList()
		dd_cust.DataTextField = "Cust_name"
		dd_cust.DataValueField = "cust_id"
		dd_cust.DataBind()


		MySare.SalesRepId = dd_sare.SelectedValue
		SetGridStyles(MyGrid)
		MyGrid.DataSource = MySare.GetCustSare()
		MyGrid.DataBind()
	End Sub


	Public Sub MyGrid_ItemCommand(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1CommandEventArgs) Handles MyGrid.ItemCommand
		If e.CommandName = "Delete" Then
			MySare.SalesRepId = e.Item.Cells(1).Text
			MySare.SalesRepCust = e.Item.Cells(0).Text
			If (MySare.deleteCusSare()) Then
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
			Button_insert.Visible = False
			Button_update.Visible = True
			GridPanel.Visible = False
			FilterPanel.Visible = False

			txtpercent.Text = e.Item.Cells(3).Text
			dd_cust.SelectedValue = e.Item.Cells(0).Text
			dd_edit_sare.SelectedValue = e.Item.Cells(1).Text
		End If




	End Sub

	Private Sub Button_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Add.Click

		EditPanel.Visible = True
		Button_update.Visible = False
		Button_insert.Visible = True
		GridPanel.Visible = False
		FilterPanel.Visible = False

		txtpercent.Text = ""
		dd_cust.SelectedIndex = 0
		dd_edit_sare.SelectedValue = dd_sare.SelectedValue

	End Sub
	Private Sub Button_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_cancel.Click
		GridPanel.Visible = True
		EditPanel.Visible = False
		FilterPanel.Visible = True
	End Sub
	Private Sub Button_Update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_update.Click
		setvalues("update")
		If MySare.UpdateCustSare() Then
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
	Private Sub Button_Insert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_insert.Click
		setvalues("insert")

		If (MySare.InsertCustSare()) Then
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
			MySare.SalesRepId = dd_sare.SelectedValue
			MySare.SalesRepCust = dd_cust.SelectedValue
		End If
		MySare.SalesRepCtryId = Session("country_id")
		MySare.SalesRepUserId = Session("user_id")
		MySare.SalesRepId = dd_edit_sare.SelectedValue
		MySare.SalesRepCust = dd_cust.SelectedValue
        'If CDbl(txtpercent.Text) > 1 Then
        '	MySare.SalesRepCustPercent = txtpercent.Text / 100
        'Else
        MySare.SalesRepCustPercent = txtpercent.Text
        'End If

	End Sub
	Private Sub MyGrid_ItemCreated(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated

		If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Then
			e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
			e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor1Class';")
			CType(e.Item.Cells(5).Controls(0), WebControl).CssClass = "button_common"
			CType(e.Item.Cells(5).Controls(0), WebControl).Attributes.Add("onclick", "javascript:return getconfirm();")

		ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then

			e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
			e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor2Class';")

			CType(e.Item.Cells(5).Controls(0), WebControl).CssClass = "button_common"
			CType(e.Item.Cells(5).Controls(0), WebControl).Attributes.Add("onclick", "javascript:return getconfirm();")

		End If
		 e.Item.Attributes.Add("onclick", "javascript:__doPostBack('" & "MyGrid$" & "R" & (e.Item.ItemIndex) & "$_ctl0','');")

	End Sub


	Private Sub dd_sare_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dd_sare.SelectedIndexChanged
		MySare.SalesRepId = dd_sare.SelectedValue
		MyGrid.DataSource = MySare.GetCustSare()
		MyGrid.DataBind()
	End Sub
End Class
