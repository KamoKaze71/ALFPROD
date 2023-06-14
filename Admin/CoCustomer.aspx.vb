Imports Wyeth.Alf.CssStyles
Imports Wyeth.Alf.WyethDropdown


Public Class Customercop
	Inherits System.Web.UI.Page

	Dim MyCustomer As New WyethCustomer

#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

	Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents txtCustName As System.Web.UI.WebControls.TextBox
	Protected WithEvents txtsearch As System.Web.UI.WebControls.TextBox
	Protected WithEvents txtCustDepartment As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCustAddress As System.Web.UI.WebControls.TextBox
	Protected WithEvents txtCustZip As System.Web.UI.WebControls.TextBox
	Protected WithEvents txtCustCity As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCustID As System.Web.UI.WebControls.TextBox
    Protected WithEvents GridPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents EditPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents Button_Insert As System.Web.UI.WebControls.Button
	Protected WithEvents Button_Update As System.Web.UI.WebControls.Button
	Protected WithEvents Button_Cancel As System.Web.UI.WebControls.Button
	Protected WithEvents MyGridDist As C1.Web.C1WebGrid.C1WebGrid
	Protected WithEvents inpID As System.Web.UI.HtmlControls.HtmlInputHidden
	Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents ShowCustomer As System.Web.UI.WebControls.Button
    Protected WithEvents ddDistribSelect As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtCudiNr As System.Web.UI.WebControls.TextBox
    Protected WithEvents FilterPanel As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents lbDownloadArea As System.Web.UI.WebControls.Label
    Protected WithEvents ddCustGroup As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtwyethName As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddCustGroupFilter As System.Web.UI.WebControls.DropDownList


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
        MyCustomer.Cust_User_Id = Session("user_id")
        MyCustomer.Cust_Country_Id = Session("country_id")

        txtsearch.Attributes.Add("onkeydown", "return submitButton(ShowCustomer);")

        If Page.IsPostBack Then
            FilterPanel.Visible = True
        Else
            ' set all styles
            SetGridStyles(MyGrid)

            lblPageTitle.Text = Request.QueryString("pageTitle")
            GetDistribSelectDD(ddDistribSelect, Session("country_id"))
            GetCustomerGroupSelect(ddCustGroupFilter, Session("country_id"))
            GetCustomerGroupSelect(ddCustGroup, Session("country_id"))
            Dim li As New ListItem
            li.Value = 0
            li.Text = "-- All --"
            ddCustGroupFilter.Items.Insert(0, li)

            MyGrid.Visible = False


        End If

	End Sub
	Private Sub BindData()

      
        MyCustomer.Cust_Dist_Id = ddDistribSelect.SelectedValue
        MyCustomer.Cust_Name = txtsearch.Text
        MyCustomer.Cust_Group_Id = ddCustGroupFilter.SelectedValue
        MyGrid.DataSource = MyCustomer.GetCustomer()
        MyGrid.DataBind()
        MyGrid.Visible = True
        lbDownloadArea.Visible = False

	End Sub
	Private Sub Button_Insert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Insert.Click

		setvalues("insert")


		If (MyCustomer.insert()) Then
			EditPanel.Visible = False
			GridPanel.Visible = True
			FilterPanel.Visible = True

		Else
			Dim strScript As String
			strScript = "<script language =javascript >"
            strScript += "window.open('../error.aspx?ErrorId=2','Error','width=300,height=250,left=270,top=180');"
			strScript += "</script>"

			RegisterClientScriptBlock("anything", strScript)
		End If
	End Sub
    Private Sub Button_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        EditPanel.Visible = True
        FilterPanel.Visible = False
        GridPanel.Visible = False
        Button_Insert.Visible = True
        Button_Update.Visible = False

        txtCudiNr.Text = ""
        txtCustID.Text = ""
        txtCustName.Text = ""
        txtCustDepartment.Text = ""
        txtCustAddress.Text = ""
        txtCustZip.Text = ""
        txtCustCity.Text = ""
   

    End Sub
    Public Sub MyGrid_ItemCommand(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1CommandEventArgs) Handles MyGrid.ItemCommand

        If e.CommandName = "Delete" Then
            MyCustomer.Cust_Id = e.Item.Cells(0).Text

            If (MyCustomer.DeleteCustomer()) Then
                BindData()
            Else
                Dim strScript As String
                strScript = "<script language =javascript >"
                strScript += "window.open('../error.aspx?ErrorId=1','Error','width=300,height=250,left=270,top=180');"
                strScript += "</script>"

                RegisterClientScriptBlock("anything", strScript)
            End If

        ElseIf e.CommandName = "Row" Then

            EditPanel.Visible = True
            GridPanel.Visible = False
            FilterPanel.Visible = False
            Button_Insert.Visible = False
            Button_Update.Visible = True

            txtCustID.Text = e.Item.Cells(0).Text
            txtCudiNr.Text = e.Item.Cells(1).Text
            txtCustName.Text = e.Item.Cells(2).Text.Replace("&nbsp;", String.Empty)
            txtCustDepartment.Text = e.Item.Cells(3).Text.Replace("&nbsp;", String.Empty)
            txtCustAddress.Text = e.Item.Cells(4).Text.Replace("&nbsp;", String.Empty)
            txtCustZip.Text = e.Item.Cells(5).Text.Replace("&nbsp;", String.Empty)
            txtCustCity.Text = e.Item.Cells(6).Text.Replace("&nbsp;", String.Empty)
            ddCustGroup.SelectedValue = e.Item.Cells(7).Text
            txtwyethName.Text = e.Item.Cells(8).Text.Replace("&nbsp;", String.Empty)
        Else

        End If
    End Sub

    Private Sub Button_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Cancel.Click

        GridPanel.Visible = True
        EditPanel.Visible = False
        FilterPanel.Visible = True

    End Sub
    Private Sub Button_Update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Update.Click

        setvalues("update")

        If MyCustomer.update() Then

            GridPanel.Visible = True
            FilterPanel.Visible = True
            EditPanel.Visible = False

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
            MyCustomer.Cust_Id = CType(txtCustID.Text, Integer)
        End If

        MyCustomer.Cust_Name = Trim(txtCustName.Text.ToString)
        MyCustomer.Cust_Department = Trim(txtCustDepartment.Text.ToString)
        MyCustomer.Cust_Street = Trim(txtCustAddress.Text.ToString)
        MyCustomer.Cust_Zip = Trim(txtCustZip.Text.ToString)
        MyCustomer.Cust_City = Trim(txtCustCity.Text.ToString)
        MyCustomer.Cust_Country_Id = Session("country_id")
        MyCustomer.Cust_Group_Id = ddCustGroup.SelectedValue
        MyCustomer.Cust_WyethName = Trim(txtwyethName.Text)


    End Sub


    Private Sub MyGrid_ItemDatabound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound


        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Then
            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor1Class';")
          
        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then

            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor2Class';")
         
        End If

         e.Item.Attributes.Add("onclick", "javascript:__doPostBack('" & "MyGrid$" & "R" & (e.Item.ItemIndex) & "$_ctl0','');")

    End Sub

    Private Sub C1WebGrid1_SortCommand(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1SortCommandEventArgs) Handles MyGrid.SortCommand
        bindData()
    End Sub
    Private Sub ShowCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowCustomer.Click
        BindData()
    End Sub
End Class
