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
    Dim dist_id As Integer


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Dim MyImport As New WyethImport
        ' list all available transmissions for the selected Distributor
        If Page.IsPostBack = True Then
            dist_id = Me.ViewState("dist_id")
        Else
            dist_id = Request.QueryString("dist_id")
            Me.ViewState.Add("dist_id", dist_id)



            ddTransmissions.DataSource = MyImport.GetTransmissions(dist_id)
            ddTransmissions.DataValueField = "tran_id"
            ddTransmissions.DataTextField = "tran_date"
            ddTransmissions.DataBind()

            MyJs.ConfirmMessage = "This will delete the transmisison of the selected day! Are you sure you want to delete this transmission"
            MyJs.AddGetConfirm(Button_delete_transmission)



        End If
       


    End Sub
    Private Sub Button_delete_transmission_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_delete_transmission.Click
        MyImport.DeleteTransmission(CInt(ddTransmissions.SelectedValue), dist_id)
        lblOut.Text = MyImport.strLog()
        ddTransmissions.DataSource = MyImport.GetTransmissions(dist_id)
        ddTransmissions.DataValueField = "tran_id"
        ddTransmissions.DataTextField = "tran_date"
        ddTransmissions.DataBind()
    End Sub

End Class
