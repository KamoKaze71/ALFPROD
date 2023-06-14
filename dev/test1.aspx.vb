Imports Wyeth.Utilities
Imports System.Threading


Public Class test1
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents TextBox1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents Table1 As System.Web.UI.WebControls.Table

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

      
        Dim MyMail As New WyethJmail
        Dim sSubject As String = "ImportError"
        Dim sMessage, logMessage, templatefilenamePath As String
        Dim lastIndex As Integer


        Dim MyTemplate As New Wyeth.Utilities.textTemplate



        templatefilenamePath = Settings.ALFApplicationRootPath & "emailTemplates/AlfserviceImportError.html"
        MyTemplate.filename = templatefilenamePath
        sMessage = MyTemplate.returnString()


        logMessage = "ALF could not import Sanova Data!" & vbNewLine
        logMessage += "Please Check ALF Logs In The Admin Section for Import Errors, Ftp Errors and Application Exceptions." & vbNewLine
        logMessage += "Make a Import under Admin Import Distributor or Contact Sanova for misssing FTP Files" & vbNewLine


        ' Make log entry into db
      
        '  MyMail.SendEMail(MyCollection("SMTPHost"), "ALFService", MyCollection("AdminEmail"), sSubject, sMessage)
        MyMail.SendEMail("10.248.57.232", "ALFService", "kamitzp@wyeth.com", sSubject, sMessage)

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim startdate, enddate As Date

        startdate = Now()

        System.Threading.Thread.Sleep(6000)

        enddate = Now()

        Response.Write(DateDiff(DateInterval.Minute, startdate, enddate))
    End Sub
End Class
