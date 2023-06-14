Public Class sms
    Inherits System.Web.UI.Page

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




        Dim mysms As New SendSMS
        Dim strResult As String
        Dim sms_text, mobile As String
        sms_text = "test"
        ' sms_text = Request.QueryString("sms_text")
        mobile = Request.QueryString("Mobile")
        mysms.MobileNumber = "+4369981615297"
        mysms.PWD = "wye3010"
        mysms.UID = "wyeth_at_intern"
        mysms.SMSMessage = sms_text

        strResult = mysms.SendSms()
        strResult = strResult + "1"


        Response.Write(strResult)
    End Sub

End Class
