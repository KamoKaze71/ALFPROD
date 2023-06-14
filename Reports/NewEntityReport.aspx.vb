Imports Wyeth.Alf.CssStyles
Imports Wyeth.Alf.WyethDropdown

Public Class NewEntityReport
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents GridPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents reportPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents ddDistribSelect As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnGenRep As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim MyReport As New Report
    Dim TAPG As New TargetProductGroup
    Dim ddtapg As New DropDownList

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        If Page.IsPostBack = True Then
        Else
            GetDistribSelectDD(ddDistribSelect, Session("country_id"))
            bindData()
            SetGridStyles(MyGrid)

         
            If Me.ALFPageAccessRights > AlfPage.Rights.Read Then
                ddTAPG.Enabled = True
            Else
                ddtapg.Enabled = False
            End If
        End If
    End Sub


    Private Sub bindData()

        MyReport.CtryID = Session("country_id")
        MyReport.DistID = ddDistribSelect.SelectedValue
        MyGrid.DataSource = MyReport.GetNewEntitiesReportCustomers()
        MyGrid.EnableViewState = True
        MyGrid.DataBind()

    End Sub
    Public Sub MyGrid_ItemCommand(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1CommandEventArgs) Handles MyGrid.ItemCommand
        If e.CommandName.StartsWith("Sort") Then
        Else
            Dim cust_id, TAPG As Integer
            cust_id = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("cust_id"))).Text
            TAPG = CType(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("tpg"))).FindControl("ddtapg2"), DropDownList).SelectedValue
            PartitioningHelp.addCustomerToTPG(cust_id, TAPG, Me)

        End If
         End Sub

    Public Sub MyGrid_ItemCreated(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated
        'e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("tapg"))).Controls.AddAt(0, ddtapg)
    End Sub

    Public Sub MyGrid_ItemDataBound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound
        fillDropDown(CType(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("tpg"))).FindControl("ddtapg2"), DropDownList))
        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Then
            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor1Class';")

        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then

            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor2Class';")

        End If
        '  e.Item.Attributes.Add("onclick", "javascript:__doPostBack('" & "MyGrid$" & "R" & (e.Item.ItemIndex) & "$_ctl0','');")
    End Sub

    Private Sub fillDropDown(ByRef ddtapg As DropDownList)

        TAPG.countryID = Session("country_id")
        ddtapg.DataSource = TAPG.getList
        ddtapg.DataValueField = "tapg_id"
        ddtapg.DataTextField = "tapg_description"
        ddtapg.DataBind()
        ddtapg.Items.Insert(0, New ListItem("--- Assign to Target Product Group ---", 0))

    End Sub

    Private Sub C1WebGrid1_SortCommand(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1SortCommandEventArgs) Handles MyGrid.SortCommand
        bindData()
    End Sub


    Private Sub btnGenRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRep.Click
        bindData()
    End Sub
End Class
