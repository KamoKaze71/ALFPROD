Imports Wyeth.Alf.CssStyles
Imports Wyeth.Alf.Stock

Public Class Orders
    Inherits System.Web.UI.Page

    Private myDataAccess As New Stock
    Private myDataView As DataView

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents dataGridOrders As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Form1 As System.Web.UI.HtmlControls.HtmlForm
    Protected WithEvents btnDateEnter As System.Web.UI.HtmlControls.HtmlButton
    Protected WithEvents dropdownDistributor As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dropdownLine As System.Web.UI.WebControls.DropDownList
    Protected WithEvents inputStartDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents inputEndDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents valSummary As Wyeth.Alf.validationSummary
    Protected WithEvents CompareValidator1 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents viewData As System.Web.UI.WebControls.Panel
    Protected WithEvents viewUpdate As System.Web.UI.WebControls.Panel
    Protected WithEvents back As System.Web.UI.HtmlControls.HtmlButton
    Protected WithEvents stoc_unit As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        setDataGridStyles(dataGridOrders)

        If Not Page.IsPostBack Then
            display("LIST")

            'wir schreiben den titel ins titel-label
            lblPageTitle.Text = Request.QueryString("pageTitle")
            'heutiges Datum in die datums-felder. Start-datum ist von gestern damit man gleich
            'eine aktuelle view bekommt.
            inputStartDate.Text = Date.Today.AddDays(-1)
            inputEndDate.Text = Date.Today
            'zeichnen der Dropdowns
            WyethDropdown.GetDistribSelectDD(dropdownDistributor, Session.Item("country_id"))
            WyethDropdown.GetLineSelectDD(dropdownLine, Session.Item("country_id"))
            'Das Line Dropdown benötigt noch ein "All" Item
            dropdownLine.Items.Insert(0, "All")
            dropdownLine.Items.Item(0).Value = 0
        End If
    End Sub

    Private Sub display(ByVal what As String)
        Select Case UCase(what)
            Case "LIST"
                viewUpdate.Visible = False
                viewData.Visible = True
            Case "EDIT"
                viewUpdate.Visible = True
                viewData.Visible = False
        End Select
    End Sub

    Private Sub drawDataGrid(ByVal param As String)
        myDataAccess.StockStartDate = Date.Parse(inputStartDate.Text)
        myDataAccess.StockEndDate = Date.Parse(inputEndDate.Text)
        myDataAccess.DistributorID = dropdownDistributor.SelectedValue
        myDataAccess.StockLineID = dropdownLine.SelectedValue
        Me.myDataView = myDataAccess.getAllStock()
        Me.myDataView.Sort = param
        With dataGridOrders
            .DataSource = myDataView
            .DataBind()
        End With

        Dim dgItem As DataGridItem
        Dim dgCell As TableCell
        Dim intColCounter As Int16
        dgItem = New DataGridItem(2, 0, ListItemType.Header)

        For intColCounter = 0 To dataGridOrders.Columns.Count - 1
            If Not intColCounter = 3 Then
                dgCell = New TableCell
            Else
                dgCell.ColumnSpan = 2
            End If
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "Value for the column"
        Next

        dataGridOrders.Controls(0).Controls.AddAt(0, dgItem)
    End Sub

    Private Sub btnDateEnter_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDateEnter.ServerClick
        Page.Validate()
        If Page.IsValid Then
            drawDataGrid("code_code")
        Else
            With valSummary
                .ShowSummary = True
                .Enabled = True
            End With
        End If
    End Sub

    Private Sub dataGridOrders_EditCommand(ByVal sender As Object, ByVal e As DataGridCommandEventArgs) Handles dataGridOrders.EditCommand
        display("EDIT")
        fillEditForm(e.Item.ItemIndex)
    End Sub

    Private Sub fillEditForm(ByVal itemIndex As Integer)
        stoc_unit.Value = Me.myDataView.Item(itemIndex).Item("stoc_id")
    End Sub

    Private Sub dataGridOrders_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As DataGridSortCommandEventArgs) Handles dataGridOrders.SortCommand
        drawDataGrid(e.SortExpression)
    End Sub

    Private Sub back_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles back.ServerClick
        display("LIST")
    End Sub
End Class
