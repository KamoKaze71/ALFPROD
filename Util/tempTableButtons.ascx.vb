Public Class tempTableButtons
    Inherits System.Web.UI.UserControl

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btn_tmpTable As System.Web.UI.WebControls.Button
    Protected WithEvents btn_tableart As System.Web.UI.WebControls.Button
    Protected WithEvents btn_tableKD As System.Web.UI.WebControls.Button
    Protected WithEvents btn_tableBW As System.Web.UI.WebControls.Button

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
        'Put user code to initialize the page here

        Dim myjsPopup As New JSPopUp(Parent.Page)
        myjsPopup.Width = 1000
        myjsPopup.Height = 700
        myjsPopup.ScrollBars = True

        myjsPopup.Title = "ViewProducts"
        myjsPopup.PageURL = "notimported.aspx?file_type=ART"
        myjsPopup.AddPopupToControl(btn_tableart)

        myjsPopup.Title = "ViewCustomers"
        myjsPopup.PageURL = "notimported.aspx?file_type=KD"
        myjsPopup.AddPopupToControl(btn_tableKD)

        myjsPopup.Title = "ViewOrders"
        myjsPopup.PageURL = "notimported.aspx?file_type=BW"
        myjsPopup.AddPopupToControl(btn_tableBW)


    End Sub


    'Public Delegate Sub delegateEvent(ByVal sender As Object, ByVal e As System.EventArgs)
    'Public Event btnBW_Click As delegateEvent
    'Public Event btnART_Click As delegateEvent

    Sub New()
        MyBase.New()
    End Sub

    ''************************************************************************************************
    ''* Preview Button Event
    ''************************************************************************************************
    'Private Sub btnBW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_tableBW.Click
    '    RaiseEvent Preview_Click(sender, e)
    'End Sub

    ''************************************************************************************************
    ''* Print Button Event
    ''************************************************************************************************
    'Private Sub btnART_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_tableart.Click
    '    RaiseEvent Print_Click(sender, e)
    'End Sub

    Private Sub btn_tableart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_tableart.Click

    End Sub

    Private Sub btn_tableKD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_tableKD.Click

    End Sub

    Private Sub btn_tableBW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_tableBW.Click

    End Sub
End Class
