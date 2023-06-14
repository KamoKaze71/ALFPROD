Imports Wyeth.Utilities
Imports System.Net
Imports System.Web
Imports System
Imports System.IO
Imports System.Text
'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
'''         <revision>1</revision>
''' <summary><para>Module used to send SMS to the SMS Gateway of</para></summary>
''' <summary><para>This Module is used by the send SMS report</para></summary>
Public Class SendSMS
    Public Sub New()
        MaxChar = 160
        ipart = 0
        ipartOf = 0
    End Sub

    Public Function SendSms() As String
        Dim length As Integer = strSMSMessage.Replace("|", String.Empty).Length
        Dim strChunk() As String = strSMSMessage.Split("|")
        Dim tmp As Integer


        'checkForSmsChunks()

        If length > MaxChar Then
            strSMSMessage = ""
            For Each str As String In strChunk
                If SMSMessage.Length + str.Length > MaxChar Then
                    strStatus = sendSmsChunks()
                    SMSMessage = ""
                    SMSMessage = SMSMessage + str
                    'tmp = strChunk.GetLowerBound(0)
                    'tmp = strChunk.GetUpperBound(1)
                Else
                    SMSMessage = SMSMessage + str
                End If
            Next
        Else
            SMSMessage = SMSMessage.Replace("|", String.Empty)
            strStatus = sendSmsChunks()
            SMSMessage = ""
        End If
        Return strStatus
    End Function



    Private Sub checkForSmsChunks()
        Dim tmpMessage As String = SMSMessage
        Dim lengthtmp As Integer = strSMSMessage.Replace("|", String.Empty).Length
        Dim strChunktmp() As String = strSMSMessage.Split("|")


        If lengthtmp > MaxChar Then
            For Each str As String In strChunktmp
                If tmpMessage.Length + str.Length > MaxChar Then
                    ipart = ipart + 1
                    tmpMessage = ""
                Else
                    tmpMessage = tmpMessage + str
                    ipartOf = ipartOf + 1
                End If
            Next
        Else
            ipartOf = 1
            ipart = 1
            tmpMessage = ""
        End If

    End Sub


    Private Function sendSmsChunks()
        Dim strRequest, strResult As String
        Dim MyRequest As HttpWebRequest
        Dim newStream As Stream
        Dim encoding As New ASCIIEncoding
        Dim postData As String
        Dim SPM As ServicePointManager

        postData = "Uid=" + UID
        postData += "&Pwd=" + PWD
        postData += "&MsIsdn=" & MobileNumber
        postData += "&Originator=WYETH"
        'postData += "&SmsText=" & Part & "/" & PartOf & SMSMessage
        postData += "&SmsText=" & SMSMessage

        Dim data As Byte() = encoding.GetBytes(postData)

        Try
            strRequest = Settings.SMSGatewayURL & "?"
            SPM.CertificatePolicy = New AcceptAllCertifcate
            MyRequest = WebRequest.Create(strRequest)
            MyRequest.Method = "POST"
            MyRequest.ContentType = "application/x-www-form-urlencoded"
            MyRequest.ContentLength = data.Length

            newStream = MyRequest.GetRequestStream()
            newStream.Write(data, 0, data.Length)
            newStream.Close()


            Dim resp As HttpWebResponse = CType(MyRequest.GetResponse(), HttpWebResponse)
            Dim sr As StreamReader = New StreamReader(resp.GetResponseStream())

            strResult = sr.ReadToEnd()
            sr.Close()

            Return strResult

        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Return ex.Message
        End Try
    End Function
    Private strSMSMessage As String
    Private strMobileNumber As String
    Private strSMSServerUrl As String
    Private strUID As String
    Private strPWD As String
    Private imaxChar As Integer
    Private ipartOf As Integer
    Private ipart As Integer
    Private strStatus As String
    Private strStatusShort As String

    Public Property SMSMessage() As String
        Get
            Return strSMSMessage

        End Get
        Set(ByVal Value As String)
            strSMSMessage = Value
        End Set
    End Property


    Public Property MobileNumber() As String
        Get
            Dim tmp As String
            tmp = strMobileNumber.Replace(" ", "")
            tmp = tmp.Replace("+", "00")
            Return tmp
        End Get
        Set(ByVal Value As String)
            strMobileNumber = Value
        End Set
    End Property

    Public Property SMSServerUrl() As String
        Get
            Return strSMSServerUrl

        End Get
        Set(ByVal Value As String)
            strSMSServerUrl = Value
        End Set
    End Property
    Public Property UID() As String
        Get
            Return strUID

        End Get
        Set(ByVal Value As String)
            strUID = Value
        End Set
    End Property

    Public Property PWD() As String
        Get
            Return strPWD

        End Get
        Set(ByVal Value As String)
            strPWD = Value
        End Set
    End Property

    Public Property MaxChar() As Integer
        Get
            Return imaxChar - 3

        End Get
        Set(ByVal Value As Integer)
            imaxChar = Value
        End Set
    End Property

    Public ReadOnly Property Part() As Integer
        Get
            Return ipart
        End Get
    End Property
    Public ReadOnly Property PartOf() As Integer
        Get
            Return ipartOf
        End Get
    End Property

    Public ReadOnly Property SMSStatus() As String
        Get
            If strStatus.IndexOf("Active=0") > 0 Then
                strStatusShort = "Sms successfully sent"
            Else
                strStatusShort = "Sms could not be sent"
            End If
            Return strStatus
        End Get
    End Property

    Public ReadOnly Property SMSStatusShort() As String
        Get
            Return strStatusShort
        End Get
    End Property
End Class

'*****************************************************************************************
'* class to accept all certificates
'*****************************************************************************************
Public Class AcceptAllCertifcate
    Implements ICertificatePolicy

    Public Sub New()
    End Sub

    Public Function CheckValidationResult(ByVal srvPoint As System.Net.ServicePoint, ByVal certificate As System.Security.Cryptography.X509Certificates.X509Certificate, ByVal request As System.Net.WebRequest, ByVal certificateProblem As Integer) As Boolean Implements System.Net.ICertificatePolicy.CheckValidationResult
        Return True
    End Function
End Class
