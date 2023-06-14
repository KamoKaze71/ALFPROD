Imports Wyeth.Alf.CssStyles

Public Class FxRate
	Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents GridPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents ddcurr_id_from As System.Web.UI.WebControls.DropDownList
	Protected WithEvents ddcurr_id_to As System.Web.UI.WebControls.DropDownList
	Protected WithEvents Mygrid As C1.Web.C1WebGrid.C1WebGrid
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
	Dim MyFxRate As New WyethFXRate

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		If Page.IsPostBack Then
			

			'	BindData()
		Else
            lblPageTitle.Text = Request.QueryString("pageTitle")
            ddcurr_id_from.DataSource = MyFxRate.GetCurrencies
            ddcurr_id_from.DataValueField = "curr_id"
            ddcurr_id_from.DataTextField = "curr_description"

            ddcurr_id_from.DataBind()

            ddcurr_id_to.DataSource = MyFxRate.GetCurrencies
            ddcurr_id_to.DataValueField = "curr_id"
            ddcurr_id_to.DataTextField = "curr_description"

            ddcurr_id_to.DataBind()
			ddcurr_id_from.SelectedValue = 15
			ddcurr_id_to.SelectedValue = 18

            BindData()
		End If


	End Sub
	Private Sub BindData()
        'Apply default Css Style Settings
        SetGridStyles(Mygrid)
		MyFxRate.FXRateCurrIDFrom = ddcurr_id_from.SelectedValue		  'ddcurr_id_from.SelectedValue
		MyFxRate.FXRateCurrIDTo = ddcurr_id_to.SelectedValue		  ' ddcurr_id_to.SelectedValue
		MyFxRate.FXRateID = Session("country_id")
		Mygrid.DataSource = MyFxRate.GetFXRates
		Mygrid.DataBind()
	End Sub

	Private Sub ddcurr_id_from_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddcurr_id_from.SelectedIndexChanged
	BindData()

	End Sub

	Private Sub ddcurr_id_to_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddcurr_id_to.SelectedIndexChanged
		BindData()
	End Sub
	Private Sub MyGrid_ItemCreated(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles Mygrid.ItemCreated

		If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Then
            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';")
			e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor1Class';")
        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor2Class';")
        End If
     



	End Sub
End Class
