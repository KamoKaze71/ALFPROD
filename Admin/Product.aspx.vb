Imports Wyeth.Alf.CssStyles
Imports Wyeth.Alf.WyethDropdown

Public Class Product
    Inherits Wyeth.Alf.AlfPage
    Dim MyProduct As New WyethProduct
    Dim prod_id As Integer
    Dim line_id, tapg_id, obs_code, keywords As String


#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents GridPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents txtSearch As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents btn_show_products As System.Web.UI.WebControls.Button
    Protected WithEvents ddLineSelect As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents ddProductObs As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddTapg As System.Web.UI.WebControls.DropDownList
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


        line_id = Request.QueryString("line_id")
        tapg_id = Request.QueryString("tapg_id")
        obs_code = Request.QueryString("obs")
        keywords = Request.QueryString("keywords")
        
        If Page.IsPostBack = True Then
            SetGridStyles(MyGrid)
            BindData()
        Else
            GetLineSelectDD(ddLineSelect, Session("country_id"))
            GetTargetProductGroupSelectDD(ddTapg, Session("country_id"))
            ddLineSelect.Width = New System.Web.UI.WebControls.Unit(190, UnitType.Pixel)
            ddTapg.Width = New System.Web.UI.WebControls.Unit(190, UnitType.Pixel)


            Dim li As New ListItem
            li.Value = "0"
            li.Text = "All TAPGs"
            ddTapg.Items.Insert(0, li)

            Dim li2 As New ListItem
            li2.Value = "888888"
            li2.Text = ">>UNDEFINED<<"
            ddTapg.Items.Insert(1, li2)

            GetOBSProductDD(ddProductObs)

            If line_id <> "" Then
                ddLineSelect.SelectedValue = line_id
            End If

            If tapg_id <> "" Or tapg_id <> "0" Then
                ddTapg.SelectedValue = tapg_id
            End If
            If obs_code <> "" Then
                Select Case obs_code
                    Case "OBS"
                End Select
                ddProductObs.SelectedValue = obs_code
            End If

            If keywords <> "" Then
                txtSearch.Text = keywords
            End If



            SetGridStyles(MyGrid)
            BindData()
        End If
      

	End Sub

	Private Sub BindData()
	
        MyProduct.ProdLineId = ddLineSelect.SelectedValue
        MyProduct.ProdSearchString = txtSearch.Text
        MyProduct.ProdCtryId = Session("country_id")
        MyProduct.ProdTargetProductGroupId = ddTapg.SelectedValue
        MyGrid.DataSource = MyProduct.GetProducts(ddProductObs.SelectedValue)
		MyGrid.DataBind()

	End Sub
	
	Sub MyGrid_ItemCommand(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1CommandEventArgs) Handles MyGrid.ItemCommand
        If e.CommandName = "" Then
            Server.Transfer("ProductByID.aspx?prod_id=" & e.Item.Cells(0).Text & "&keywords=" & txtSearch.Text & "&obs_code=" & ddProductObs.SelectedValue, True)
        End If

    End Sub

   
    Private Sub MyGrid_ItemCreated(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Then

            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor1Class';")

        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then

            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor2Class';")

        End If

         e.Item.Attributes.Add("onclick", "javascript:__doPostBack('" & "MyGrid$" & "R" & (e.Item.ItemIndex) & "$_ctl0','');")

    End Sub

    Private Sub btn_show_products_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_show_products.Click
        BindData()
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Validate()
        If Page.IsValid() Then
            BindData()
        End If
    End Sub
    Private Sub C1WebGrid1_SortCommand(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1SortCommandEventArgs) Handles MyGrid.SortCommand
        bindData()
    End Sub
    Private Sub ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportExcel.Click
        MyGrid.AllowAutoSort = False
        MyGrid.AllowSorting = False

        BindData()

        Dim exp As exportToExcel = New exportToExcel(Page)
        exp.title = Me.ALFPageTitle
        exp.formatColumnAsString(1)
        exp.addDataGrid(MyGrid)
        exp.export()
    End Sub
End Class
