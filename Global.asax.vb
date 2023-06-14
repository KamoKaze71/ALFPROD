Imports System
Imports System.Web
Imports System.Diagnostics
Imports Wyeth.Utilities.Helper
Imports System.Web.SessionState
Imports System.Globalization
Imports wyeth.Utilities
Imports wyeth.Alf.WyethAppllication
Imports wyeth.Utilities.NumberFormat
Imports wyeth.Utilities.DateHandling
Imports Oracle.DataAccess.Client
Imports System.Web.Mail

Public Class Global
    Inherits HttpApplication

#Region " Component Designer Generated Code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container
    End Sub

#End Region


    Dim MyUserAccess As New UserAccess
    Dim OnlineUsers As New Hashtable

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)

        Dim MyReader As OracleDataReader
        Dim MyConnection As New MyConnection
        Dim MyCmd As New OracleCommand
        Dim MyCollection As New Collection

        MyCmd.CommandType = CommandType.StoredProcedure
        MyCmd.CommandText = "PKG_APPLICATION.GetAppVars"
        MyCmd.Connection = MyConnection.Open()

        MyCmd.Parameters.Add("AppVars", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        MyReader = MyCmd.ExecuteReader()

        While MyReader.Read
            MyCollection.Add(MyReader("apse_value"), MyReader("apse_variable"))
        End While

        MyReader.Close()

        Application("AppSetting") = MyCollection
        Application("MyDTFI") = GetMyDTFI()
        Application("MyNFI") = GetMyNFI(2)


        MyReader.Dispose()
        MyConnection.Close()

        End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started

        Dim MyStock As New Stock
        Dim MyMep As New MEPData
        Dim MyApplication As New WyethAppllication
        Dim UserName As String
        Dim MyStatus As New AlfStatus

        If Not Settings.isDevelopmentServer And Not Settings.isLiveServer Then
            Session("user_id") = 2202
        Else

            Session("user_id") = Request.Form("userID")  ' default User_id
        End If

        Session("country_id") = 56  ' default Country ID
        Session("currency_id") = 15  'default Currency ID
        Session("currency_code") = "EUR"
        Session("Module_id") = 0
        MyStock.StockCtryID = Session("country_id")

        MyMep.setProcessMonth()

       
        Application.Lock()
        ' Application("Status") = MyStatus.GetALFStatus()
        ' Holt datum des Letzetn Order Entry
        Application("LastOrderEntry") = Convert.ToDateTime(getLastOrderEntry(Today()))

        Application.Lock()

        If Session("user_id") = 0 Or Session("user_id") = Nothing Then
            Dim qryStr As String
            qryStr = Request.Url.PathAndQuery

            Response.Redirect("/intranet_gateways/alf.asp" & "?requestedUrl=" & qryStr)
            'Response.Redirect("/intranet_gateways/alf.asp")

        Else
            Session("user_groups") = MyUserAccess.FillUserGroup(Session("user_id"))
        End If

        UserName = MyUserAccess.GetUserName(Session("user_id"))

        Application.Lock()

        If Application("OnlineUsers") Is Nothing = False Then
            OnlineUsers = Application("OnlineUsers")
        Else

        End If


        If OnlineUsers.ContainsKey(UserName) Then
            OnlineUsers.Remove(UserName)
            OnlineUsers.Add(UserName, Now())
        Else
            OnlineUsers.Add(UserName, Now())
        End If

        Application("OnlineUsers") = OnlineUsers
        Application.UnLock()





    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
        'check if mvs are being actualized
        Dim MyConnection As New MyConnection
        Dim MyCmd As New OracleCommand
        Dim val As Oracle.DataAccess.Types.OracleString
        Dim str, calledByPage As String

        Try
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "Pkg_Application.GetMViewsStatus"
            MyCmd.Parameters.Add("v_result", OracleDbType.Varchar2, 20, val, ParameterDirection.ReturnValue)
            MyCmd.Connection = MyConnection.Open()
            val = MyCmd.ExecuteScalar()
            val = MyCmd.Parameters(0).Value
            str = val.ToString
            If str <> "100" Then
                Application.Lock()
                Application("MVIEW") = val.ToString
                Application.UnLock()

                calledByPage = Request.ServerVariables("SCRIPT_NAME")

                If calledByPage.EndsWith("default.aspx") Then

                ElseIf calledByPage.EndsWith("header.aspx") Then

                ElseIf calledByPage.EndsWith("leftnavi.aspx") Then

                ElseIf calledByPage.EndsWith("main.aspx") Then

                ElseIf calledByPage.ToUpper.IndexOf("admin".ToUpper) > 0 Then

                ElseIf calledByPage.EndsWith("MviewsProgress.aspx") Then

                Else
                    Response.Redirect(Settings.applicationUrl & "default.aspx?page=mviews", True)
                End If
            Else

                Application.Lock()
                Application("MVIEW") = val.ToString
                Application.UnLock()

            End If
        Catch ex As Exception


        Finally
            MyConnection.Close()

        End Try

    End Sub

    Sub Application_PreRequestHandlerExecute(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Session("user_id") <> 0 And Session("user_groups") Is Nothing Then
                Session("user_groups") = MyUserAccess.FillUserGroup(Session("user_id"))
            ElseIf Request.Form("userID") <> "" Then
                Session("user_id") = Request.Form("userID")
                Session("user_groups") = MyUserAccess.FillUserGroup(Session("user_id"))
            ElseIf (Session("user_id") = 0 Or Session("user_id") Is Nothing) And Request.Form("userID") = "" Then
                Dim strJScript As String
                strJScript += "<script language =javascript >" & vbNewLine
                strJScript += "self.close();"
                strJScript += "</script>"
                Response.Write(strJScript)
                Session.Clear()
                Session.Abandon()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        Try


            Dim MyCollection As New Collection
            Dim MyUserAccess As New UserAccess
            Dim user_id, user_name As String

            user_id = HttpContext.Current.Session("User_Id")
            user_name = MyUserAccess.GetUserName("user_id")

            MyCollection = Application("AppSetting")

            Dim MyLog As New Log
            MyLog.Description = Server.GetLastError().InnerException.ToString()
            MyLog.Source = "Global.asa - Application Error Handler"
            MyLog.CodeCode = "AppEx"
            MyLog.insert()


            'Fires when an error occurs
            Dim msg As MailMessage = New MailMessage
            Dim Body As String
            Body = "Error Source: " & Server.GetLastError.Source.ToString & vbCrLf
            Body = Body & "Error Message: " & Server.GetLastError.Message.ToString & vbCrLf
            Body = Body & "Error: " & Server.GetLastError().InnerException.ToString() & vbCrLf
            Body = Body & "User id:" & user_id & "UserName:" & user_name & vbCrLf
            Body = Body & Server.MachineName.ToString


            SmtpMail.SmtpServer = MyCollection("SMTPHost")
            msg.Body = Body
            msg.To = MyCollection("AdminEmail")
            msg.From = MyCollection("AdminEmail")
            msg.Subject = "Application Error:" & Server.MachineName.ToString & " " & Request.Url.ToString
            SmtpMail.Send(msg)

        Catch ex As Exception

        End Try

    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        If Session("user_id") = 0 Or Session("user_id") = Nothing Then
        Else
            OnlineUsers.Remove(MyUserAccess.GetUserName(Session("user_id")))
            Application.Lock()
            Application("OnlineUsers") = OnlineUsers
            Application.UnLock()
        End If

    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
    End Sub

End Class
