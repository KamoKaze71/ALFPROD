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
    Protected WithEvents btnDeleteGroup As System.Web.UI.WebControls.Button
    Protected WithEvents pnlGroupData As System.Web.UI.WebControls.Panel
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents Div1 As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents viewCustomersLink As System.Web.UI.HtmlControls.HtmlAnchor
    Protected WithEvents dgCustomers As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dlProducts As System.Web.UI.WebControls.DataList
    Protected WithEvents dlSalesReps As System.Web.UI.WebControls.DataList
    Protected WithEvents lblnoProducts As System.Web.UI.WebControls.Label
    Protected WithEvents lblnoSalesReps As System.Web.UI.WebControls.Label
    Protected WithEvents removeCustomer As System.Web.UI.WebControls.LinkButton
    Protected WithEvents btnRefreshMV As System.Web.UI.WebControls.Button
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
    Private salesReps As New ArrayList(10)
    Private totalPercentPerCustomer As Double = 0
    Private currentTargetProductGroup As Integer
    Private customers As DataView
    Private customerAmount As Integer = 0
    Private MyJs As New JSPopUp(Me)

    Public Sub New()
        Me.TargetProductGroups = New TargetProductGroup
        currentTargetProductGroup = 0
        Me.salesReps.Clear()
    End Sub

    '***************************************************************************************************
    '* Page_Load 
    '***************************************************************************************************
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MyJs.ConfirmMessage = "This will refresh ALF Data! \n->This will take about 10 min.\n->ALF Reports will not show data during this time! \nAre you sure you want to do this?"
        MyJs.AddGetConfirm(btnRefreshMV)
        'first we have to check if the user wants to directly open the customer popup from outside
        If Request.QueryString("cust") <> "" Then
            Dim str As String
            str = "<script>" & vbNewLine & _
                    "showPopup('CustomerAdd.aspx?id=" & Request.QueryString("id") & "&c=" & Request.QueryString("cust") & "', 640, 190);" & vbNewLine & _
                    "document.location.href = 'Partitioning.aspx?id=" & Request.QueryString("id") & "&pageTitle=" & Request.QueryString("pageTitle") & "&showCustomers=1';" & _
                    "</script>"
            RegisterStartupScript("anything", str)
        End If

        Me.TargetProductGroups.countryID = Session("country_id")
        Me.TargetProductGroups.userID = CInt(Session.Item("user_id"))

        lblPageTitle.Text = Request.QueryString("pageTitle").ToString
        fillGroupsDropdown()
        addConfirmJavascript()

        If Request.QueryString.Item("id") <> "" Then
            currentTargetProductGroup = Request.QueryString.Item("id")
            TargetProductGroups.targetProductGroupID = currentTargetProductGroup
            selectCurrentTargetProductgroup()


            fillGroupName()
            fillProducts()
            fillSalesReps()
            pnlGroupData.Visible = True
            checkIfShowCustomers()
            showCustCount()
        Else
            pnlGroupData.Visible = False
        End If
    End Sub
    Private Sub showCustCount()
        If dgCustomers.Visible Then
        Else
            Dim i As Integer
            i = Me.TargetProductGroups.GetCustomerCount(ddTargetGroup.SelectedValue)
            Me.viewCustomersLink.InnerText = Me.viewCustomersLink.InnerText & " (" & i & " customers)"
        End If
       

    End Sub
    '***************************************************************************************************
    '* addConfirmJavascriptsToButtons 
    '***************************************************************************************************
    Private Sub addConfirmJavascript()
        Dim confirm As String = "Attention by deleting this TGP the following actions will happen:\n\n" & _
                                "- Every product will be removed from this group.\n" & _
                                "- All removed products will be available for other TGPs\n" & _
                                "- Every sales-Rep and it values will removed.\n\n" & _
                                "Are you sure you want to delete\n" & _
                                "this Target-Product-Group?"
        btnDeleteGroup.Attributes.Add("onclick", "return showConfirm('" & confirm & "');")
    End Sub

    '***************************************************************************************************
    '* selectCurrentTargetProductgroup 
    '***************************************************************************************************
    Private Sub selectCurrentTargetProductgroup()
        For i As Integer = 0 To ddTargetGroup.Items.Count - 1
            If CInt(ddTargetGroup.Items(i).Value) = currentTargetProductGroup Then
                ddTargetGroup.SelectedIndex = i
            End If
        Next
    End Sub

    '***************************************************************************************************
    '* linkCustomers_Click 
    '***************************************************************************************************
    Private Sub checkIfShowCustomers()
        If Request.QueryString.Item("showCustomers") = "1" Then
            viewCustomersLink.InnerText = "Hide customers"
            dgCustomers.Visible = True
            viewCustomersLink.HRef = "partitioning.aspx?id=" & Me.currentTargetProductGroup & "&pageTitle=" & Request.QueryString("pageTitle") & "&showCustomers=0"
            fillCustomers()
        Else
            viewCustomersLink.HRef = "partitioning.aspx?id=" & Me.currentTargetProductGroup & "&pageTitle=" & Request.QueryString("pageTitle") & "&showCustomers=1"
            viewCustomersLink.InnerText = "View customers"
            dgCustomers.Visible = False
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
        Dim dv As DataView = Me.TargetProductGroups.getProducts
        With dlProducts
            .DataSource = dv
            Me.checkIfItemsAvailable(dv, lblnoProducts)
            .DataBind()
        End With
    End Sub

    '***************************************************************************************************
    '* fillSalesReps 
    '***************************************************************************************************
    Private Sub fillSalesReps()
        Dim dv As DataView = Me.TargetProductGroups.getSalesReps

        Me.salesReps.Clear()
        For i As Integer = 0 To dv.Table.Rows.Count - 1
            Me.salesReps.Add(dv.Table.Rows(i).Item(0))
        Next

        With dlSalesReps
            .DataSource = dv
            Me.checkIfItemsAvailable(dv, Me.lblnoSalesReps)
            .DataBind()
        End With

        Me.salesReps.Clear()
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
        customers = Me.TargetProductGroups.getCustomers
        With dgCustomers
            .DataSource = customers
            .DataBind()
        End With

        'If Not dgCustomers.Items.Count > 0 Then
        '    Dim lbl As Label = New Label
        '    lbl.Text = "No customers added yet."
        '    pnlGroupData.Controls.Add(lbl)
        'End If
    End Sub

    '***************************************************************************************************
    '* refreshWindow 
    '***************************************************************************************************
    Private Sub refreshWindow()
        Response.Redirect("Partitioning.aspx?id=" & currentTargetProductGroup & "&pageTitle=" & lblPageTitle.Text & "&showCustomers=" & Request.QueryString("showCustomers"))
    End Sub

    '***************************************************************************************************
    '* dlProducts_DeleteCommand 
    '***************************************************************************************************
    Private Sub dlProducts_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlProducts.ItemCommand, dlSalesReps.ItemCommand
        Dim affectedDatalist As DataList = source

        If e.CommandName = "delete" Then
            If affectedDatalist.ID = "dlProducts" Then
                TargetProductGroups.deleteProduct(CInt(e.CommandArgument))
            ElseIf affectedDatalist.ID = "dlSalesReps" Then
                TargetProductGroups.removeSalesRep(CInt(e.CommandArgument))
            End If
            refreshWindow()
        End If
    End Sub

    '***************************************************************************************************
    '* dgCustomers_ItemCommand 
    '***************************************************************************************************
    Private Sub dgCustomers_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgCustomers.ItemCommand
        If e.CommandName = "delCustomer" Then
            Dim affectedSalesreps As DataList
            Dim ctrl As HtmlInputHidden

            affectedSalesreps = e.Item.Cells(2).FindControl("dlDatagridSalesReps")

            For i As Integer = 0 To affectedSalesreps.Items.Count - 1
                ctrl = affectedSalesreps.Items(i).FindControl("sareID")
                TargetProductGroups.setPercentageForSalesRep(ctrl.Value, e.CommandArgument, 0)
            Next

            refreshWindow()
        End If
    End Sub

    '***************************************************************************************************
    '* dgCustomers_ItemDataBound 
    '***************************************************************************************************
    Private Sub dgCustomers_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgCustomers.ItemDataBound
        If customerAmount = 0 Then
            setCustomersAmount(0)
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            e.Item.Visible = False
            Dim currentPercentage As Double = CDbl(e.Item.Cells(1).Text)

            totalPercentPerCustomer += currentPercentage
            Me.salesReps.Add(New ASalesRep(e.Item.DataItem("fullname"), _
                                            Math.Round(CDbl(e.Item.DataItem("cusr_percent")), 2), _
                                            CInt(e.Item.DataItem("sare_id"))))

            If Not nextCustomerIsTheSame(e.Item.DataSetIndex) Then
                Dim salesreps As DataList = e.Item.Cells(2).FindControl("dlDatagridSalesReps")
                salesreps.DataSource = Me.salesReps
                salesreps.DataBind()

                e.Item.Cells(1).Text = Math.Round(totalPercentPerCustomer, 2) & "%"

                If totalPercentPerCustomer <> 100 Then
                    e.Item.Cells(0).ForeColor = Color.Red
                    e.Item.Cells(1).ForeColor = Color.Red
                    e.Item.Cells(1).Font.Bold = True
                End If

                e.Item.Visible = True
                Me.salesReps.Clear()
                totalPercentPerCustomer = 0

                Dim confirm As String = "By removing this customer the following " & _
                                        "action(s) will happen:\n\n" & _
                                        "- All percent values of each Sales-rep will be set to zero.\n\n" & _
                                        "Are you sure you want to proceed?"
                Dim removeCustomerControl As LinkButton = e.Item.Cells(3).FindControl("removeCustomer")
                removeCustomerControl.Attributes.Add("onclick", "return showConfirm('" & confirm & "');")
                customerAmount += 1
                setCustomersAmount(customerAmount)
            End If

        End If
    End Sub

    Private Sub setCustomersAmount(ByVal amount As Integer)
        Dim txt As String
        If amount = 1 Then
            txt = "customer"
        Else
            txt = "customers"
        End If
        viewCustomersLink.InnerText = "Hide customers (" & amount & " " & txt & ")"
    End Sub

    Private Function nextCustomerIsTheSame(ByVal index As Integer) As Boolean
        Dim nextIndex As Integer = index + 1
        Dim customerColumn As Integer = 4

        If nextIndex <= customers.Count - 1 Then
            If customers.Item(nextIndex).Item(customerColumn) = customers.Item(index).Item(customerColumn) Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Private Sub btnDeleteGroup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteGroup.Click
        TargetProductGroups.deleteTPG()
        Response.Redirect(Request.ServerVariables.Item("SCRIPT_NAME") & "?pageTitle=" & Request.QueryString("pageTitle"))
    End Sub

    Private Sub dlProducts_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlProducts.ItemDataBound
        Dim delButton As LinkButton = e.Item.FindControl("Linkbutton1")
        Dim confirm As String = "Are you sure you want to remove " & e.Item.DataItem("prod_presentation") & "\nfrom the selected Target-Product-Group?"
        delButton.Attributes.Add("onclick", "return showConfirm('" & confirm & "');")
    End Sub

    Private Sub dlSalesReps_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlSalesReps.ItemDataBound
        Dim delButton As LinkButton = e.Item.FindControl("Linkbutton2")
        Dim confirm As String = "By removing " & e.Item.DataItem("fullname") & "\nfrom the TPG the following actions will happen:\n\n" & _
                                "- The sales-rep will be removed from every customer\n" & _
                                "- All sales-rep - customer values also will be removed\n\n" & _
                                "Are you sure you want to proceed?"
        delButton.Attributes.Add("onclick", "return showConfirm('" & confirm & "');")
    End Sub

    Private Sub On_btnRefresh_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefreshMV.Click
        Dim MyImport As New WyethImport
        MyImport.RefreshMVs()
    End Sub
End Class

Public Class ASalesRep
    Public id As Integer
    Public percentage As Double
    Public name As String

    Public Sub New(ByVal name As String, ByVal percent As Double, ByVal id As Integer)
        Me.name = name
        Me.percentage = percent
        Me.id = id
    End Sub
End Class

