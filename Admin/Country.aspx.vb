Imports Wyeth.Alf.CssStyles

Public Class Country
	Inherits System.Web.UI.Page
	Dim MyCountry As New WyethCountry

#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
	Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents GridPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents EditPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents Button_Add As System.Web.UI.WebControls.Button
	Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
	Protected WithEvents Button_insert As System.Web.UI.WebControls.Button
	Protected WithEvents Button_update As System.Web.UI.WebControls.Button
	Protected WithEvents txtCtry_id As System.Web.UI.WebControls.TextBox
	Protected WithEvents txtCtry_description As System.Web.UI.WebControls.TextBox
	Protected WithEvents txtCtry_code As System.Web.UI.WebControls.TextBox
	Protected WithEvents button_cancel As System.Web.UI.WebControls.Button
	Protected WithEvents ddCurrencyId As System.Web.UI.WebControls.DropDownList
	Protected WithEvents inpID As System.Web.UI.HtmlControls.HtmlInputHidden
	Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents txtplCountry As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtbsCountry As System.Web.UI.WebControls.TextBox

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
			BindData()
		End If


	End Sub
	Private Sub BindData()
		SetGridStyles(MyGrid)		'Apply default Css Style Settings


		MyCountry.CountryID = Session("country_id")

		ddCurrencyId.DataSource = MyCountry.GetCurrencies()
		ddCurrencyId.DataValueField = "curr_id"
		ddCurrencyId.DataTextField = "curr_code"
		ddCurrencyId.DataBind()

		MyGrid.DataSource = MyCountry.GetCountries()
		MyGrid.DataBind()
	End Sub


	Public Sub MyGrid_ItemCommand(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1CommandEventArgs) Handles MyGrid.ItemCommand

		If e.CommandName = "Delete" Then


			MyCountry.CountryID = e.Item.Cells(0).Text

			If (MyCountry.DeleteCountry()) Then
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

            txtCtry_id.Text = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("ctry_id"))).Text
            txtCtry_code.Text = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("ctry_code"))).Text
            txtCtry_description.Text = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("ctry_description"))).Text
            ddCurrencyId.SelectedValue = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("curr_id"))).Text

            If e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("CTRY_bs_COUNTRY"))).Text <> "&nbsp;" Then
                txtbsCountry.Text = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("CTRY_bs_COUNTRY"))).Text
            End If

            If e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("CTRY_pl_COUNTRY"))).Text <> "&nbsp;" Then
                txtplCountry.Text = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("CTRY_pl_COUNTRY"))).Text
            End If

            End If



	End Sub
	
	Private Sub Button_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Add.Click

		EditPanel.Visible = True
		Button_update.Visible = False
		Button_insert.Visible = True
		GridPanel.Visible = False
		FilterPanel.Visible = False

		txtCtry_code.Text = ""
		txtCtry_description.Text = ""
        txtCtry_id.Text = ""
        txtbsCountry.Text = ""
        txtplCountry.Text = ""
	End Sub
	Private Sub Button_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button_cancel.Click
		GridPanel.Visible = True
		EditPanel.Visible = False
		FilterPanel.Visible = True
	End Sub
	Private Sub Button_Update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_update.Click
		setvalues("update")
		If MyCountry.UpdateCountry() Then
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
	Private Sub Button_Insert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_insert.Click
		setvalues("insert")

		If (MyCountry.InsertCountry()) Then
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
			MyCountry.CountryID = txtCtry_id.Text
		End If
		MyCountry.CountryUserID = Session("user_id")
		MyCountry.CountryCode = txtCtry_code.Text
		MyCountry.CountryDescription = txtCtry_description.Text
        MyCountry.CountryCurrencyID = ddCurrencyId.SelectedValue
        MyCountry.CountryPL_CODE = txtplCountry.Text
        MyCountry.CountryBS_CODE = txtbsCountry.Text
        MyCountry.CountryCurr_CODE = ddCurrencyId.SelectedItem.ToString

	End Sub
	Private Sub MyGrid_ItemCreated(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated

		If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Then
			e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
			e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor1Class';")
            CType(e.Item.Cells(8).Controls(0), WebControl).CssClass = "button_common"
            CType(e.Item.Cells(8).Controls(0), WebControl).Attributes.Add("onclick", "javascript:return getconfirm();")
            e.Item.Attributes.Add("onclick", "javascript:__doPostBack('" & "MyGrid$" & "R" & (e.Item.ItemIndex) & "$_ctl0','');")


		ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then

			e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
			e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor2Class';")
            CType(e.Item.Cells(8).Controls(0), WebControl).CssClass = "button_common"
            CType(e.Item.Cells(8).Controls(0), WebControl).Attributes.Add("onclick", "javascript:return getconfirm();")
            e.Item.Attributes.Add("onclick", "javascript:__doPostBack('" & "MyGrid$" & "R" & (e.Item.ItemIndex) & "$_ctl0','');")

        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.SelectedItem Then
            e.Item.Attributes.Add("onclick", "javascript:__doPostBack('" & "MyGrid$" & "R" & (e.Item.ItemIndex) & "$_ctl0','');")

        End If
		


	End Sub
End Class
