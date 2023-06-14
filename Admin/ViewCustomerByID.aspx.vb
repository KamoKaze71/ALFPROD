Imports Wyeth.Alf.TargetProductGroup

Public Class CegedimDeatil
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents txtCustID As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCustName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCustDepartment As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCustWyethName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCustAddress As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCustCity As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCustZip As System.Web.UI.WebControls.TextBox
    Protected WithEvents EditPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents ddTAPG As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btn_assign As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Shared cust_id As Integer = 0
    Dim cudi_nr As String
    Dim MyCustomer As New WyethCustomer
    Dim MyDataView As New DataView
    Dim TAPG As New TargetProductGroup

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Put user code to initialize the page here

        Dim strScript As String
        strScript = "<script language =javascript >"
        strScript += "self.focus();"
        strScript += "</script>"
        RegisterClientScriptBlock("anything", strScript)
        Me.ALFPageTitle = "Customer Details"

        If Page.IsPostBack = True Then
        Else
            cust_id = Request.QueryString("cust_id")
            cudi_nr = Request.QueryString("cudi_nr")

            MyCustomer.Cust_Country_Id = Session("country_id")

            TAPG.countryID = Session("country_id")
            ddTAPG.DataSource = TAPG.getList
            ddTAPG.DataValueField = "tapg_id"
            ddTAPG.DataTextField = "tapg_description"
            ddTAPG.DataBind()

            ddTAPG.Items.Insert(0, New ListItem("--- Assign to Target Product Group ---", 0))
            If Me.ALFPageAccessRights > AlfPage.Rights.Read Then
                ddTAPG.Enabled = True
            Else
                ddTAPG.Enabled = False
            End If
            BindData()
        End If
    End Sub

    Private Sub BindData()
        If cust_id <> 0 Then
            MyDataView = MyCustomer.GetCustomerByID(cust_id)
        ElseIf cudi_nr <> "" Then
            MyDataView = MyCustomer.GetCustomerByCudiNr(cudi_nr)
        End If
        cust_id = MyDataView(0).Item("cust_id")
        txtCustID.Text = Convert.ToString(MyDataView(0).Item("Cudi_customer_nr"))
        txtCustAddress.Text = Convert.ToString(MyDataView(0).Item("Cust_street"))
        txtCustCity.Text = Convert.ToString(MyDataView(0).Item("Cust_city"))
        txtCustDepartment.Text = Convert.ToString(MyDataView(0).Item("Cust_department"))
        txtCustName.Text = Convert.ToString(MyDataView(0).Item("Cust_name"))
        '  txtCustWyethName.Text = MyDataView(0).Item("Cust_wyethname")
        txtCustZip.Text = Convert.ToString(MyDataView(0).Item("Cust_zip"))
        '   ddCustGroup.SelectedValue = MyDataView(0).Item("cugr_id")
        'ddCustStatType.SelectedValue = MyDataView(0).Item("code_id_soku")

    End Sub
    Private Sub btn_assign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_assign.Click
        If Not ddTAPG.SelectedValue = 0 Then
            PartitioningHelp.addCustomerToTPGFromPopup(cust_id, ddTAPG.SelectedValue, Me)
        End If
    End Sub
End Class
