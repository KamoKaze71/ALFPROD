Imports Oracle.DataAccess.Client

Public Class Helper
    Public Const CONNECTION_STRING_LIVE_SERVER As String = "User Id=alf2;Password=alf2;Data Source=intranet;Pooling=False; Connection Lifetime=20; Connection Timeout=10; Max Pool Size=3; Min Pool Size=2; Incr Pool Size=2; Decr Pool Size=2" '"ConfigurationSettings.AppSettings("ConnectionString").ToString()
    'Public Const CONNECTION_STRING_TEST_SERVER As String = "User Id=alf2;Password=alf2;Data Source=oratest;Pooling=False; Connection Lifetime=20; Connection Timeout=10; Max Pool Size=3; Min Pool Size=2; Incr Pool Size=2; Decr Pool Size=2" '"ConfigurationSettings.AppSettings("ConnectionString").ToString() 
    Public Const CONNECTION_STRING_TEST_SERVER As String = "User Id=alf2;Password=alf2;Data Source=dintra;Pooling=False; Connection Lifetime=20; Connection Timeout=10; Max Pool Size=3; Min Pool Size=2; Incr Pool Size=2; Decr Pool Size=2" '"ConfigurationSettings.AppSettings("ConnectionString").ToString() 

    Public Const DATEFORMAT_STRING As String = "yyyy-MM-dd"
    Public Const DATEFORMAT_STRING_REPORT As String = "yyyy-MM-dd"
    Public Const DATEFORMAT_STRING_REPORT_MONTH As String = "yyyy-MM"
    Public Const NUMBER_FORMAT_STRING As String = "####"
    Public Const NUMBER_FORMAT_STRING_EXACT As String = "N"
    Public Const NUMBER_FORMAT_STRING_PERCENT As String = "P"
End Class
