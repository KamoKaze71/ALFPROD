Public Class JSMsgBox
    Private _stringMsg As String
    Private _type As String

    Public Property Type() As String
        Get
            Return _type
        End Get
        Set(ByVal Value As String)
            _type = UCase(Value)
        End Set
    End Property
    Public Property StringMsg() As String
        Get
            Return _stringMsg
        End Get
        Set(ByVal Value As String)
            _stringMsg = Value
        End Set
    End Property


End Class
