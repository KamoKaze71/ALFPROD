Public Class CegedimDeatil
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents txtCustID As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCustName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCustDepartment As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCustWyethName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCustAddress As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCustCity As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCustZip As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddCustGroup As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddCustStatType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents EditPanel As System.Web.UI.WebControls.Panel

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim cust_id As Integer
    Dim MyCustomer As New WyethCustomer
    Dim MyDataView As New DataView

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Put user code to initialize the page here

        Dim strScript As String
        strScript = "<script language =javascript >"
        strScript += "self.focus();"
        strScript += "</script>"
        RegisterClientScriptBlock("anything", strScript)

        If Page.IsPostBack = True Then
        Else
            lblPageTitle.Text = "Cegedim Customer Details"
            cust_id = Request.QueryString("cust_id")

            MyCustomer.Cust_Country_Id = Session("country_id")

            ddCustGroup.DataSource = MyCustomer.GetCustomerGroups()
            ddCustGroup.DataValueField = "cugr_id"
            ddCustGroup.DataTextField = "cugr_description"
            ddCustGroup.DataBind()

            ddCustStatType.DataSource = MyCustomer.GetCustomerCodes()
            ddCustStatType.DataValueField = "code_id"
            ddCustStatType.DataTextField = "code_code"
            ddCustStatType.DataBind()

            BindData()
        End If


    End Sub

    Private Sub BindData()
        MyDataView = MyCustomer.GetCustomerByID(cust_id)
        txtCustID.Text = MyDataView(0).Item("Cudi_customer_nr")
        txtCustAddress.Text = MyDataView(0).Item("Cust_street")
        txtCustCity.Text = MyDataView(0).Item("Cust_city")
        txtCustDepartment.Text = MyDataView(0).Item("Cust_department")
        txtCustName.Text = MyDataView(0).Item("Cust_name")
        '  txtCustWyethName.Text = MyDataView(0).Item("Cust_wyethname")
        txtCustZip.Text = MyDataView(0).Item("Cust_zip")
        ddCustGroup.SelectedValue = MyDataView(0).Item("cugr_id")
        ddCustStatType.SelectedValue = MyDataView(0).Item("code_id_soku")
       


    End Sub

End Class
