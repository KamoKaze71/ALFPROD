
Imports System
Imports System.Net
Imports System.Net.Sockets
'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
''' <summary><para>A Ping Utility Class</para></summary>
Public Class netPing

    Dim mIpAddr As IpAddress
    Dim mTimeOut As Integer
    Dim mResponseTime As Integer
    Dim strLastErr As String
    Dim mHostName As String

    Public Sub New()
        mTimeOut = 24000000
    End Sub

    Public Sub New(ByVal TimeOutSec As Integer)
        mTimeOut = TimeOutSec * 1000000
    End Sub

    Property TimeOutSec() As Integer
        Get
            TimeOutSec = mTimeOut \ 1000000
        End Get
        Set(ByVal Value As Integer)
            mTimeOut = Value * 1000000
        End Set
    End Property

    Public ReadOnly Property ResponseTime() As Integer
        Get
            ResponseTime = mResponseTime
        End Get
    End Property

    Public ReadOnly Property LastError() As String
        Get
            LastError = strLastErr
        End Get
    End Property

    Public ReadOnly Property IpAddress() As String
        Get
            IpAddress = mIpAddr.ToString()
        End Get
    End Property

    Public ReadOnly Property HostName() As String
        Get
            HostName = mHostName
        End Get
    End Property

    Public Function CheckByName(ByVal HName As String) As Boolean
        Try
            strLastErr = ""
            mIpAddr = Dns.Resolve(HName).AddressList(0)
            mHostName = Dns.GetHostByAddress(mIpAddr).HostName
        Catch ex As System.Net.Sockets.SocketException
            strLastErr = ex.Message
            Return False
        End Try
        Return VBPing()
    End Function

    Public Function CheckByIpAddr(ByVal ipAddr As String) As Boolean
        strLastErr = ""
        Try
            mIpAddr = System.Net.IPAddress.Parse(ipAddr)
            Try
                mHostName = Dns.GetHostByAddress(mIpAddr).HostName
                Return VBPing()
            Catch SEx As SocketException
                strLastErr = SEx.Message
            End Try
        Catch Ex As Exception
            strLastErr = Ex.Message
        End Try
        Return False

    End Function

    Private Function VBPing() As Boolean
        Const RcvSize As Integer = 240
        Const ECHO As Integer = 7
        Dim S As Socket
        Dim BytesRcvd As Integer
        Dim BytesSent As Integer

        Dim T1 As Long
        Dim T2 As Long

        Dim ChkRd As ArrayList

        Dim IpEP As System.Net.IPEndPoint
        Dim RcvBuffer(RcvSize) As Byte
        Dim SendBuffer() As Byte = {8, 0, &HF7, &HFF, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}

        Try
            mResponseTime = 0
            strLastErr = ""
            ChkRd = New ArrayList
            IpEP = New System.Net.IPEndPoint(mIpAddr, ECHO)
            S = New Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Icmp)
            ChkRd.Add(S)

            BytesSent = S.SendTo(SendBuffer, SendBuffer.GetLength(0), SocketFlags.None, IpEP)
            T1 = Now.Ticks
            Socket.Select(ChkRd, Nothing, Nothing, mTimeOut)

            T2 = Now.Ticks
            If ChkRd.Count > 0 Then
                BytesRcvd = S.Receive(RcvBuffer, RcvSize, SocketFlags.None)
                mResponseTime = CType((T2 - T1) / 10000, Integer)
                Return True
            Else
                strLastErr = "Time Out"
                Return False
            End If

        Catch SEx As SocketException
            strLastErr = SEx.Message
            Return False
        End Try

    End Function
End Class
