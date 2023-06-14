Imports Wyeth.Alf.WyethDropdown

Public Class MER
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents ddMonthEnd As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btn_Invoices As System.Web.UI.WebControls.Button
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim MyMEP As New MEPData

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        If Page.IsPostBack Then

        Else
            GetMonthSelectDD(ddMonthEnd)
            BindData()
        End If
    End Sub
    Private Sub BindData()
    End Sub

    Private Sub btn_Invoices_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Invoices.Click
        MyMEP.RollBackInvoices(ddMonthEnd.SelectedValue, Session("country_id"))
    End Sub
End Class
