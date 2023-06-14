Public Class Partitioning
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents ddTargetGroup As System.Web.UI.WebControls.DropDownList
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents lblGroupName As System.Web.UI.WebControls.Label
    Protected WithEvents btnNewGroup As System.Web.UI.WebControls.Button
    Protected WithEvents btnEditGroup As System.Web.UI.WebControls.Button
    Protected WithEvents btnDeleteGroup As System.Web.UI.WebControls.Button
    Protected WithEvents pnlGroupData As System.Web.UI.WebControls.Panel
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents Div1 As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents btnGenerate As System.Web.UI.WebControls.Button
    Protected WithEvents linkCustomers As System.Web.UI.WebControls.LinkButton
    Protected WithEvents dgCustomers As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dlProducts As System.Web.UI.WebControls.DataList
    Protected WithEvents dlSalesReps As System.Web.UI.WebControls.DataList
    Protected WithEvents lblnoProducts As System.Web.UI.WebControls.Label
    Protected WithEvents lblnoSalesReps As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private TargetProductGroups As TargetProductGroup

    Public Sub New()
        Me.TargetProductGroups = New TargetProductGroup
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.TargetProductGroups.countryID = Session("country_id")
        lblPageTitle.Text = Request.QueryString("pageTitle").ToString

        If Not Page.IsPostBack Then
            pnlGroupData.Visible = False
            fillGroupsDropdown()
        End If
    End Sub

    Private Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        fillGroupName()
        fillProducts()
        fillSalesReps()
        pnlGroupData.Visible = True
    End Sub

    Private Sub linkCustomers_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkCustomers.Click
        If linkCustomers.Text = "Hide customers" Then
            linkCustomers.Text = "View customers"
            dgCustomers.Visible = False
        Else
            linkCustomers.Text = "Hide customers"
            dgCustomers.Visible = True
            fillCustomers()
        End If
    End Sub

    '***************************************************************************************************
    '* fillGroupsDropdown 
    '***************************************************************************************************
    Private Sub fillGroupsDropdown()
        With Me.ddTargetGroup
            .DataSource = Me.TargetProductGroups.getList()
            .DataTextField = "tapg_description"
            .DataValueField = "tapg_id"

            If Not Page.IsPostBack Then
                .DataBind()
            End If
        End With
    End Sub

    '***************************************************************************************************
    '* fillGroupName 
    '***************************************************************************************************
    Private Sub fillGroupName()
        lblGroupName.Text = ddTargetGroup.SelectedItem.Text.ToString
    End Sub

    '***************************************************************************************************
    '* fillProducts 
    '***************************************************************************************************
    Private Sub fillProducts()



        Dim MyCountry As New WyethCountry
        MyCountry.CountryID = Session("country_id")
        Dim dv As DataView = MyCountry.GetCountries
        dlProducts.DataSource = dv

        Me.checkIfItemsAvailable(dv, lblnoProducts)
        dlProducts.DataBind()
    End Sub

    '***************************************************************************************************
    '* fillSalesReps 
    '***************************************************************************************************
    Private Sub fillSalesReps()
        Dim MyCountry As New WyethCountry
        MyCountry.CountryID = 23
        Dim dv As DataView = MyCountry.GetCurrencies
        dv.RowFilter = "curr_id=20"
        dlSalesReps.DataSource = dv

        Me.checkIfItemsAvailable(dv, lblnoSalesReps)
        dlSalesReps.DataBind()
    End Sub

    '***************************************************************************************************
    '* checkIfItemsAvailable 
    '***************************************************************************************************
    Private Function checkIfItemsAvailable(ByRef dv As DataView, ByVal noLabel As Label) As Boolean
        If dv.Count > 0 Then
            noLabel.Visible = False
            Return True
        Else
            noLabel.Visible = True
            Return False
        End If
    End Function

    '***************************************************************************************************
    '* fillCustomers 
    '***************************************************************************************************
    Private Sub fillCustomers()
        Dim MyCountry As New WyethCountry
        MyCountry.CountryID = Session("country_id")
        dgCustomers.DataSource = MyCountry.GetCountries

        dgCustomers.DataBind()
    End Sub

    '***************************************************************************************************
    '* dlProducts_DeleteCommand 
    '***************************************************************************************************
    Private Sub dlProducts_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlProducts.ItemCommand, dlSalesReps.ItemCommand
        If e.CommandName = "delete" Then
            'e.CommandArgument 
            source.Items(e.Item.ItemIndex).BackColor = Color.Red
        End If
    End Sub

    '***************************************************************************************************
    '* dgCustomers_ItemDataBound 
    '***************************************************************************************************
    Private Sub dgCustomers_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgCustomers.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim text As String = e.Item.Cells(1).Text
            Dim value As Integer = CInt(text)
            Dim table As table = Me.generatePercentBar(value)
            Dim salesreps As DataList
            Dim arr As String() = {"Hannes", "Tester", "Michal"}

            e.Item.Cells(1).Controls.Add(table)

            salesreps = e.Item.Cells(2).FindControl("dlDatagridSalesReps")
            salesreps.DataSource = arr
            salesreps.DataBind()
        End If
    End Sub

    '***************************************************************************************************
    '* generatePercentBar 
    '***************************************************************************************************
    Private Function generatePercentBar(ByVal value As Integer) As Table
        If value > 99 Then
            value = 100
        End If

        Dim table As New table
        Dim row As New TableRow
        Dim cellLeft As New TableCell
        Dim cellRight As New TableCell
        Dim cellLast As New TableCell

        table.Width = Unit.Pixel(180)
        table.Height = Unit.Pixel(10)
        table.CellPadding = 0
        table.CellSpacing = 0
        cellLeft.Width = Unit.Pixel(value)
        cellLeft.Style.Add("background-color", "#EB2C0A")
        cellLeft.ToolTip = value.ToString & "% used"
        cellRight.Width = Unit.Pixel(100 - value)
        cellRight.Style.Add("background-color", "#A6D672")
        cellRight.ToolTip = 100 - value.ToString & "% available"
        cellLast.Text = "&nbsp;&nbsp;" & 100 - value & " % free"

        row.Cells.Add(cellLeft)
        row.Cells.Add(cellRight)
        row.Cells.Add(cellLast)
        table.Rows.Add(row)

        Return table
    End Function
End Class
