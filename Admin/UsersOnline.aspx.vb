Public Class UsersOnline
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lbOnlineUsers As System.Web.UI.WebControls.ListBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim OnlineUsers As New Hashtable

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        OnlineUsers = CType(Application("OnlineUsers"), Hashtable)

        For Each s As String In OnlineUsers.Keys
            Dim li As New ListItem
            li.Value = s
            li.Text = s + " online since: " + OnlineUsers.Item(s)
            lbOnlineUsers.Items.Add(li)
        Next



    End Sub

End Class
