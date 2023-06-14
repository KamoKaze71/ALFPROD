Imports System
Imports System.IO
Imports System.Web
Imports System.Web.Mail
Imports System.Web.UI.Page
Imports jmail.SMTPMailClass


Public Class wyethNetMail

    Dim mm As New Mail.MailMessage
    Dim MyTemplate As New textTemplate
    Private strFooter, strHeader, imgid As String

    Public Sub New()
        MyBase.new()
        MyTemplate.addVariable("COMPANY_NAME", Settings.CompanyName)
        MyTemplate.addVariable("IMAGE_ID", Settings.applicationUrl & "/images/logo.gif")
        MyTemplate.addVariable("TIMESENT", Now())
        MyTemplate.addVariable("DOMAIN_NAME_WITHOUT_HTTP", Settings.DomainWithoutHTTP)
    End Sub

    Public Function SendEMail(ByVal sSMTPServer As String, _
    ByVal sFrom As String, _
    ByVal sTo As String, _
    ByVal sSubject As String, _
    ByVal sMessageText As String) As Boolean


        Try
            With mm

                .From = sFrom
                .To = sTo
                .Subject = sSubject
                .Body = HeaderTemplate & sMessageText & FooterTemplate
                .BodyFormat = MailFormat.Html
                .UrlContentBase = Settings.applicationUrl
                .UrlContentLocation = Settings.applicationUrl
                .Headers.Remove("content-type")
                .Headers.Add("content-type", "text/html")

            End With



            SmtpMail.SmtpServer = sSMTPServer
            SmtpMail.Send(mm)

            Return True

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        End Try

    End Function

    Public Function AddAttachment(ByVal filename As String) As String
        Try
            Dim retval As String
            Dim myAttachment As New MailAttachment(filename)

            retval = mm.Attachments.Add(myAttachment)
            Return retval

        Catch ex As Exception

        End Try
    End Function

    Private Function GetEmailHeader() As String
        Dim input As String

        Try

            Dim fileName As String = Settings.ALFApplicationRootPath & Settings.emailHeaderTemplate

            If File.Exists(fileName) Then
                Dim sr As StreamReader = File.OpenText(fileName)
                input = sr.ReadToEnd
                sr.Close()
            Else 'file does not exist
                input = "Error: Template-file does not exist."
            End If

            Return MyTemplate.replace_PlaceHolders(input)

        Catch ex As Exception
            Return input
        End Try

    End Function

    Private Function GetEmailFooter() As String
        Dim input As String
        Try

            Dim fileName As String = Settings.ALFApplicationRootPath & Settings.emailFooterTemplate


            If File.Exists(fileName) Then
                Dim sr As StreamReader = File.OpenText(fileName)
                input = sr.ReadToEnd()
                sr.Close()
            Else 'file does not exist
                input = "Error: Template-file does not exist."
            End If

            Return MyTemplate.replace_PlaceHolders(input)
        Catch ex As Exception
            Return input
        Finally

        End Try
    End Function

    Public Property HeaderTemplate() As String
        Get
            Return GetEmailHeader()
        End Get
        Set(ByVal Value As String)
            strHeader = Value
        End Set
    End Property

    Public Property FooterTemplate() As String
        Get
            Return GetEmailFooter()
        End Get
        Set(ByVal Value As String)
            strFooter = Value
        End Set
    End Property
End Class


Public Class WyethJmail
    Inherits wyethNetMail

    Dim msg As New jmail.SMTPMailClass
    Dim MyTemplate As New textTemplate


    Public Sub New()
        MyBase.new()

        Try
            MyTemplate.addVariable("COMPANY_NAME", Settings.CompanyName)
            MyTemplate.addVariable("IMAGE_ID", Settings.applicationUrl & "/images/logo.gif")
            MyTemplate.addVariable("TIMESENT", Now())
            MyTemplate.addVariable("DOMAIN_NAME_WITHOUT_HTTP", Settings.DomainWithoutHTTP)
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        End Try
    End Sub

    Public Shadows Function SendEMail(ByVal sSMTPServer As String, _
    ByVal sFrom As String, _
    ByVal sTo As String, _
    ByVal sSubject As String, _
    ByVal sMessageText As String) As Boolean
        Try


            Dim test As String

            With msg

                .AddRecipient(sTo)
                .Sender = Settings.emailSendersName
                .SenderName = sFrom
                .Subject = sSubject
                .HTMLBody = HeaderTemplate & sMessageText & FooterTemplate
                .ServerAddress = sSMTPServer
                .ISOEncodeHeaders = False
                .Silent = True
                .Execute()
            End With

            '        Response.Write("sended test: " & msg.Execute())
        Catch ex As Exception
            ExceptionInfo.Show(ex)

        End Try
    End Function

    Public Shadows Function AddAttachment(ByVal filename As String) As String
        Try
            Dim retval As String

            msg.AddAttachment(filename, True)

            Return retval

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        End Try
    End Function
End Class
