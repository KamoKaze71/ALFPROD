Public Class TmpBW
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dropdownBelegart As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents lblBelegart As System.Web.UI.WebControls.Label
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid

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
        If Not IsPostBack() Then
            Dim dgItem As C1.Web.C1WebGrid.C1GridItem
            Dim dgCell As TableCell
            Dim intColCounter As Int16
            dgItem = New C1.Web.C1WebGrid.C1GridItem(2, C1.Web.C1WebGrid.C1ListItemType.Item)

            For intColCounter = 0 To MyGrid.Columns.Count - 1
                dgCell = New TableCell
                dgItem.Cells.Add(dgCell)
                dgCell.Text = "Value for the column"
            Next

            MyGrid.Controls(0).Controls.AddAt(0, dgItem)
        End If
    End Sub

End Class
