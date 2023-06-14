Public Class DeleteTransmission
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddTransmissions As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Button_delete_transmission As System.Web.UI.WebControls.Button
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents lblOut As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim MyJs As New JSPopUp(Me)
    Dim MyImport As New WyethImport


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        ' list all available transmissions for the selected Distributor
        Dim dist_id = Request.QueryString("dist_id")
        Dim MyImport As New WyethImport

        ddTransmissions.DataSource = MyImport.GetTransmissions(dist_id)
        ddTransmissions.DataValueField = "tran_id"
        ddTransmissions.DataTextField = "tran_date"
        ddTransmissions.DataBind()

        MyJs.ConfirmMessage = "This will delete The tRansmisison of the selected Day! Are you sure sure that you want to delete this transmission"
        MyJs.AddGetConfirm(Button_delete_transmission)


    End Sub
   
    Private Sub Button_delete_transmission_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_delete_transmission.Click
        MyImport.DeleteTransmission(CInt(ddTransmissions.SelectedValue))
        lblOut.Text = MyImport.strLog()
    End Sub
End Class
