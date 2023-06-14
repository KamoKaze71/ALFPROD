Imports Wyeth.Alf.CssStyles
Imports Wyeth.Utilities.NumberFormat


Public Class ProductTCogsHistory
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents lblNoTCogs As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim Prod_id As Integer
    Dim Presentation As String
    Dim MyTCogs As New TCogs

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        If Page.IsPostBack = True Then


        Else

            Dim strScript As String
            strScript = "<script language =javascript >"
            strScript += "self.focus();"
            strScript += "</script>"
            RegisterClientScriptBlock("anything", strScript)

            Prod_id = Request.QueryString("prod_id")
            Presentation = Request.QueryString("presentation")
            lblPageTitle.Text = "TCogs History for " & Presentation
            SetGridStylesGroup(MyGrid)
            BindData()
        End If


    End Sub


    Private Sub BindData()
        MyGrid.DataSource = MyTCogs.GetTCogsHistoryByProductID(Prod_id)
        MyGrid.DataBind()
        If MyGrid.Items.Count = 0 Then
            MyGrid.Visible = False
            lblNoTCogs.Text = "No TCogs For this Product available"
            lblNoTCogs.Visible = True
        Else
            MyGrid.Visible = True
            lblNoTCogs.Visible = False
        End If
    End Sub




    Private Sub MyGrid_ItemCreated(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.GroupHeader Or e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.GroupFooter Then
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("cogs_std_cogs"))), 2)
           End If

    End Sub



End Class
