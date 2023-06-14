'*********************************************************************************
'* Michal Gabrukiewicz - 09.03.2004
'* W Y E T H 
'*********************************************************************************
Public Class ProductAdd
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents labelMessage As System.Web.UI.WebControls.Label
    Protected WithEvents dropdownProducts As System.Web.UI.WebControls.DropDownList
    Protected WithEvents buttonSave As System.Web.UI.WebControls.Button
    Protected WithEvents labelProductInfo As System.Web.UI.WebControls.Label
    Protected WithEvents dropdownPresentation As System.Web.UI.WebControls.DropDownList
    Protected WithEvents showObsolete As System.Web.UI.WebControls.CheckBox

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
        If Not Page.IsPostBack Then
            fillPresentationDropdown()
            fillProductDropdown()
            showDummyProduct()
        End If
      
    End Sub

    '*********************************************************************************************************
    '* fillPresentationDropdown 
    '*********************************************************************************************************
    Private Sub fillPresentationDropdown()
        Dim pr As New WyethProduct
        pr.ProdCtryId = Session.Item("country_id")

        With dropdownPresentation
            .DataSource = pr.GetLines()
            .DataTextField = "line_description"
            .DataValueField = "line_id"
            .DataBind()
        End With
    End Sub

    '*********************************************************************************************************
    '* showDummyProduct 
    '*********************************************************************************************************
    Private Sub showDummyProduct()
        labelProductInfo.Text = New AProduct().HTMLdescription
    End Sub

    '*********************************************************************************************************
    '* fillProductDropdown 
    '*********************************************************************************************************
    Private Sub fillProductDropdown()
        With dropdownProducts
            .DataSource = productsDataAccess.getUnassignedProductsForLine(dropdownPresentation.SelectedValue, showObsolete.Checked)
            .DataValueField = "prod_id"
            .DataTextField = "product"
            .DataBind()
            .Items.Insert(0, New ListItem("---- Please select a product ----", 0))
        End With
        buttonSave.Visible = False
    End Sub

    '*********************************************************************************************************
    '* dropdownProducts_SelectedIndexChanged 
    '*********************************************************************************************************
    Private Sub dropdownProducts_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dropdownProducts.SelectedIndexChanged
        labelMessage.Text = ""
        If Not dropdownProducts.SelectedValue = 0 Then
            Dim currentProduct As DataView
            Dim Product As New AProduct

            currentProduct = productsDataAccess.getProductByID(CInt(dropdownProducts.SelectedValue))

            With Product
                .id = currentProduct.Item(0).Item(0)
                .nr = currentProduct.Item(0).Item(1)
                .name = currentProduct.Item(0).Item(3)
                .packsize = currentProduct.Item(0).Item(4)
                .strength = currentProduct.Item(0).Item(5)
                labelProductInfo.Text = .HTMLdescription()
            End With
            buttonSave.Visible = True
        Else
            buttonSave.Visible = False
            showDummyProduct()
        End If
    End Sub

    '*********************************************************************************************************
    '* buttonSave_Click 
    '*********************************************************************************************************
    Private Sub buttonSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonSave.Click
        If dropdownProducts.SelectedValue <> 0 Then
            Dim product As New AProduct
            With product
                .id = CInt(dropdownProducts.SelectedValue)
                If .addProductToTargetProductGroup(CInt(Request.QueryString.Item("id")), CInt(Session.Item("user_id"))) Then
                    PartitioningHelp.closePopupAndRefreshParent(Page)
                Else
                    labelMessage.Text = "Product adding failed. Contact the admin."
                End If
            End With
            fillProductDropdown()
        End If
    End Sub

    '*********************************************************************************************************
    '* dropdownPresentation_SelectedIndexChanged 
    '*********************************************************************************************************
    Private Sub dropdownPresentation_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dropdownPresentation.SelectedIndexChanged
        fillProductDropdown()
        showDummyProduct()
    End Sub

    '*********************************************************************************************************
    '* dropdownPresentation_SelectedIndexChanged 
    '*********************************************************************************************************
    Private Sub showObsolete_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles showObsolete.CheckedChanged
        fillProductDropdown()
        showDummyProduct()
    End Sub
End Class


'*********************************************************************************
'* Michal Gabrukiewicz - 09.03.2004
'* W Y E T H 
'*********************************************************************************
Public Class AProduct
    Public id As Integer
    Public nr As String
    Public name As String
    Public packsize As String
    Public strength As String

    Public Sub New()
        id = 0
        nr = ""
        name = ""
        packsize = ""
        strength = ""
    End Sub

    Public Function HTMLdescription() As String
        Return "<b>Product-number:</b> " & returnMinusIfEmptyString(Me.nr) & "<br>" & _
                "<b>Presentation:</b> " & returnMinusIfEmptyString(Me.name) & "<br>" & _
                "<b>Packsize:</b> " & returnMinusIfEmptyString(Me.packsize) & "<br>" & _
                "<b>Strength:</b> " & returnMinusIfEmptyString(Me.strength)
    End Function

    Private Function returnMinusIfEmptyString(ByVal str As String) As String
        If str.Trim = "" Then
            Return "-"
        Else
            Return str
        End If
    End Function

    Public Function addProductToTargetProductGroup(ByVal targetProductGroupID As Integer, ByVal userID As Integer) As Boolean
        Dim productDataAccess As New TargetProductGroup
        Dim failed As Boolean = False

        With productDataAccess
            .targetProductGroupID = targetProductGroupID
            Try
                .addProduct(id)
            Catch ex As Exception
                failed = True
            End Try
        End With

        Return Not failed
    End Function
End Class