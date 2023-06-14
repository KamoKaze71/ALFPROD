Imports Wyeth.Alf.CssStyles

Public Class ImportErrors
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents reportPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents repData As Wyeth.Alf.reportData

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim MyStatus As New AlfStatus
    Dim dist_id, dist_id_muenster As Integer

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        If Page.IsPostBack = True Then

        Else
            dist_id = Request.QueryString("dist_id")
            dist_id_muenster = Request.QueryString("dist_id_muenster")
            lblPageTitle.Text = "Import Errors"
            SetGridStyles(MyGrid)


            BindData()

        End If
    End Sub
    Private Sub BindData()
        If dist_id_muenster <> 0 Then
            MyStatus.CtryID = "999"
        Else
            MyStatus.CtryID = Session("country_id")
        End If

        MyStatus.LogsSource = "PKG_IMPORT_SANOVA.F_Bewegungs_Daten"
        MyGrid.DataSource = MyStatus.GetLatestImportError()
        MyGrid.DataBind()
    End Sub

    Private Sub ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportExcel.Click
        BindData()

        Dim exp As exportToExcel = New exportToExcel(Page)
        exp.title = lblPageTitle.Text
        exp.showReportData = repData
        exp.formatColumnAsString(1)
        exp.addDataGrid(MyGrid)
        exp.testWithHTML = True
        exp.export()
    End Sub
End Class
