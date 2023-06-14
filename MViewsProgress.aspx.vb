Public Class MViewsProgress
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents lblProgress As System.Web.UI.WebControls.Label
    Protected WithEvents progress As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblpercent As System.Web.UI.WebControls.Label
    Protected WithEvents inprogress As System.Web.UI.HtmlControls.HtmlTableCell

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

        lblProgress.CssClass = "success"
        lblProgress.Text = "ALF Data is being updated, please stand by.<bR>Progress:" & CStr(Application("MVIEW")) & "%"
        lblpercent.Text = CStr(Application("MVIEW")) & "%"
        lblpercent.Width = New Unit(CInt(Application("MVIEW")), UnitType.Percentage)
        '  inprogress.Width = CStr(Application("MVIEW")) & "%"

        If CStr(Application("MVIEW")) = "100" Then
            Server.Transfer("Main.aspx", True)
        End If
    End Sub

End Class
