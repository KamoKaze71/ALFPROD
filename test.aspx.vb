Imports Wyeth.Utilities
Imports Wyeth.Utilities.MyConnection
Public Class ddtest
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ID = "ddtest"

    End Sub
    Protected WithEvents myGrid As Wyeth.Utilities.wyethDataGrid
    'Protected WithEvents myGrid As DataGrid
    Protected WithEvents btn As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim MyCountry As New WyethCountry

    Private Sub Page_LoadViewState(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Init

        'Dim myconn As New myconn
        'myconn.setGlobalizationParams()


        Dim col As BoundColumn
        With myGrid
            col = New BoundColumn
            col.DataField = "CTRY_ID"
            col.HeaderText = "Country"
            .Columns.Add(col)

            col = New BoundColumn
            col.DataField = "CTRY_DESCRIPTION"
            col.HeaderText = "Description"
            .Columns.Add(col)

            Dim filter As New Wyeth_FilterColumn
            filter.DatafieldNameOfColumnToBound = "ctry_id"
            filter.filterType = filterColumnTypes.dropdown
            .Wyeth_FilterColumns.Add(filter)

        End With
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MyCountry.CountryID = Session("country_id")
        myGrid.DataSource = MyCountry.GetCountries
        If Not Page.IsPostBack Then
            myGrid.DataBind()
        End If
    End Sub

    Private Sub btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn.Click
        btn.Width = Unit.Pixel(199)
    End Sub
End Class
