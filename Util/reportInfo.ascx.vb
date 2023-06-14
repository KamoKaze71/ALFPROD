Public Class reportInfo
    Inherits System.Web.UI.UserControl

    'Public showReportDate As Boolean
    'Public showWorkdays As Boolean
    'Public showLastOrderEntry As Boolean
    'Public showPrintDate As Boolean

    'Sub New()
    '    MyBase.New()
    '    showReportDate = True
    '    showWorkdays = True
    '    showLastOrderEntry = True
    '    showPrintDate = True
    'End Sub

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

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
        'fillAllData()
    End Sub

    Private Sub fillAllData()
        'fillReportDate()
        'fillWorkdays()
        'fillLastOrderEntry()
        'fillPrintDate()
    End Sub

    'Private Sub fillReportDate()
    '    lblReportDate.Text = "ss"
    'End Sub

End Class
