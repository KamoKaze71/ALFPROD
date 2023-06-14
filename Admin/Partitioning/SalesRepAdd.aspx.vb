Public Class SalesRepAdd
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents labelMessage As System.Web.UI.WebControls.Label
    Protected WithEvents dropdownProducts As System.Web.UI.WebControls.DropDownList
    Protected WithEvents labelProductInfo As System.Web.UI.WebControls.Label
    Protected WithEvents buttonSave As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private productsDataAccess As New TargetProductGroup

    '*********************************************************************************************************
    '* Page_Load 
    '*********************************************************************************************************
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        productsDataAccess.targetProductGroupID = CInt(Request.QueryString.Item("id"))
        productsDataAccess.userID = CInt(Session.Item("user_id"))

        If Not Page.IsPostBack Then
            fillProductDropdown()
            showDummyProduct()
        End If
    End Sub

    '*********************************************************************************************************
    '* showDummyProduct 
    '*********************************************************************************************************
    Private Sub showDummyProduct()
        labelProductInfo.Text = New MySalesRep().HTMLdescription
    End Sub

    '*********************************************************************************************************
    '* fillProductDropdown 
    '*********************************************************************************************************
    Private Sub fillProductDropdown()
        With dropdownProducts
            .DataSource = productsDataAccess.getAllSalesReps
            .DataValueField = "sare_id"
            .DataTextField = "sare_name"
            .DataBind()
            .Items.Insert(0, New ListItem("---- Please select a sales-rep ----", 0))
        End With
        buttonSave.Visible = False
    End Sub

    '*********************************************************************************************************
    '* dropdownProducts_SelectedIndexChanged 
    '*********************************************************************************************************
    Private Sub dropdownProducts_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dropdownProducts.SelectedIndexChanged
        labelMessage.Text = ""
        If Not dropdownProducts.SelectedValue = 0 Then
            Dim currentSare As DataView
            Dim SaRep As New MySalesRep

            currentSare = productsDataAccess.GetAllSalesRepByID(CInt(dropdownProducts.SelectedValue))


            With SaRep
                .id = tryConvert(currentSare.Item(0).Item(0))
                .nr = tryConvert(currentSare.Item(0).Item(1))
                .name = tryConvert(currentSare.Item(0).Item(4))
                .country = tryConvert(currentSare.Item(0).Item(8))
                .type = tryConvert(currentSare.Item(0).Item(9))
                labelProductInfo.Text = .HTMLdescription()
            End With
            buttonSave.Visible = True
        Else
            buttonSave.Visible = False
            showDummyProduct()
        End If
    End Sub

    '*********************************************************************************************************
    '* setField 
    '*********************************************************************************************************
    Function tryConvert(ByVal value As Object) As String
        Try
            Return value.ToString
        Catch ex As Exception
            Return "-"
        End Try
    End Function

    '*********************************************************************************************************
    '* buttonSave_Click 
    '*********************************************************************************************************
    Private Sub buttonSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonSave.Click
        If dropdownProducts.SelectedValue <> 0 Then
            Dim SaRep As New MySalesRep
            With SaRep
                .id = CInt(dropdownProducts.SelectedValue)
                If .addSalesRepToTargetProductGroup(CInt(Request.QueryString.Item("id")), CInt(Session.Item("user_id"))) Then
                    fillProductDropdown()
                    PartitioningHelp.closePopupAndRefreshParent(Me)
                Else
                    labelMessage.Visible = True
                    labelMessage.Text = "Sales-Rep adding failed. Contact the admin."
                End If
            End With
        End If
    End Sub

End Class


'*********************************************************************************
'* Michal Gabrukiewicz - 09.03.2004
'* W Y E T H 
'*********************************************************************************
Public Class MySalesRep
    Public id As Integer
    Public nr As String
    Public name As String
    Public country As String
    Public type As String

    Public Sub New()
        id = 0
        nr = ""
        name = ""
        country = ""
        type = ""
    End Sub

    Public Function HTMLdescription() As String
        Return "<b>Name:</b> " & returnMinusIfEmptyString(Me.name) & "<br>" & _
                "<b>Country:</b> " & returnMinusIfEmptyString(Me.country) & "<br>" & _
                "<b>Type:</b> " & returnMinusIfEmptyString(Me.type)
    End Function

    Private Function returnMinusIfEmptyString(ByVal str As String) As String
        If str.Trim = "" Then
            Return "-"
        Else
            Return str
        End If
    End Function

    Public Function addSalesRepToTargetProductGroup(ByVal targetProductGroupID As Integer, ByVal creatorID As Integer) As Boolean
        Dim productDataAccess As New TargetProductGroup
        Dim failed As Boolean = False

        With productDataAccess
            .targetProductGroupID = targetProductGroupID
            .userID = creatorID
            Try
                .addSalesRep(id)
            Catch ex As Exception
                failed = True
            End Try
        End With

        Return Not failed
    End Function
End Class