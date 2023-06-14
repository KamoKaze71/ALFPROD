Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities


Public Class UserAccess

    Public Function getAccess(ByVal access_id As Integer, Optional ByVal user_id As Integer = Nothing, Optional ByVal group_id() As Integer = (Nothing)) As Integer


        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Dim MyReader As OracleDataReader
        Dim sqluser, sqlGroup, sqlGroup2, strWhere As String
        Dim i, MyAccess As Integer

        MyCmd.CommandType = CommandType.Text
        MyCmd.Connection = Myconn.Open()

        Try


            For i = 0 To UBound(group_id)
                strWhere = strWhere & group_id(i) & ","
            Next

            strWhere = "id_group IN (" & Left(strWhere, Len(strWhere) - 1) & ") "
            sqluser = "SELECT permission FROM intranetuser.links_users_access WHERE id_access=" & access_id & " AND id_user=" & user_id & " ORDER BY permission ASC"
            sqlGroup = "SELECT count(*) as count FROM intranetuser.links_groups_access WHERE id_access=" & access_id & " AND " & strWhere
            sqlGroup2 = "SELECT permission FROM intranetuser.links_groups_access WHERE id_access=" & access_id & " AND " & strWhere & " ORDER BY permission ASC"

            ' User Rechte überprüfen
            MyCmd.CommandText = sqluser
            MyReader = MyCmd.ExecuteReader

            While MyReader.Read
                MyAccess = MyReader("Permission")
            End While


            'Grupen Rechte überprüfen
            MyCmd.CommandText = sqlGroup
            MyReader = MyCmd.ExecuteReader
            While MyReader.Read
                If MyReader("count") > 0 Then
                    MyCmd.CommandText = sqlGroup2
                    MyReader = MyCmd.ExecuteReader(CommandBehavior.CloseConnection)
                    While MyReader.Read
                        MyAccess = MyReader("permission")
                    End While

                End If
            End While

            Return MyAccess
        Catch ex As OracleException
            If ex.Number <> 1403 Then
                ExceptionInfo.Show(ex, "UserID:" & user_id)
            End If
        Finally

            Myconn.Close()

        End Try
    End Function

    Public Function FillUserGroup(ByVal user_id) As Array

        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Dim MyReader As OracleDataReader

        Try

            Dim sql As String

            sql = "SELECT groups.id_group, users.id_user " & _
             "FROM Intranetuser.groups, v_users users, Intranetuser.links_group_user " & _
             "WHERE	users.id_user=links_group_user.id_user AND " & _
             "groups.id_group=links_group_user.id_group AND " & _
             "users.id_user= " & user_id


            Dim id_groups() As Integer
            Dim i As Integer
            MyCmd.CommandText = sql
            MyCmd.CommandType = CommandType.Text
            MyCmd.Connection = Myconn.Open
            MyReader = MyCmd.ExecuteReader()

            i = 0
            Do While MyReader.Read
                ReDim Preserve id_groups(i)
                id_groups(i) = MyReader("id_group")
                i = i + 1
            Loop


            If i = 0 Then
                ReDim Preserve id_groups(i)
                id_groups(0) = 0
            End If

            Return id_groups

        Catch ex As Exception
        Finally
            Myconn.Close()
        End Try

    End Function
    Public Function GetMenuModuleID(ByVal str_script_name As String) As Integer
        '********************************************************************************************************************************
        '* Holt die entsprechende Modul (Security) ID aus t_menu
        '********************************************************************************************************************************
        '

        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Dim MyMenuModulID As Integer

        Try
            MyCmd.CommandText = "PKG_APPLICATION.GetMenuModuleID"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_menu_module_id", OracleDbType.Int32, ParameterDirection.ReturnValue)
            MyCmd.Parameters.Add("v_script_name", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = str_script_name
            MyCmd.Connection = Myconn.Open()
            MyCmd.ExecuteScalar()
            MyMenuModulID = MyCmd.Parameters(0).Value()

            Return MyMenuModulID
        Catch ex As Exception
            Return -1
        Finally
            Myconn.Close()
        End Try

        Return True

    End Function
    Public Function GetUserName(ByVal i_user_id As Integer) As String
        '********************************************************************************************************************************
        '* Holt die entsprechende Modul (Security) ID aus t_menu
        '********************************************************************************************************************************
        '

        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Dim UserName As String
        Dim val As Object


        Try
            MyCmd.CommandText = "PKG_APPLICATION.GetUserName"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_user_name", OracleDbType.Varchar2, 2000, val, ParameterDirection.ReturnValue)
            MyCmd.Parameters.Add("v_user_id", OracleDbType.Int32, ParameterDirection.Input).Value = i_user_id
            MyCmd.Connection = Myconn.Open()
            val = MyCmd.ExecuteScalar()
            val = MyCmd.Parameters(0).Value()
            UserName = Convert.ToString(val)

            Return UserName

        Catch ex As Exception

            ExceptionInfo.Show(ex, "UserID:" & i_user_id)
        Finally
            Myconn.Close()
        End Try

    End Function

    Public Function GetUserForAcessRight(ByVal v_group_Name) As DataView
        '********************************************************************************************************************************
        '* Holt die entsprechende Modul (Security) ID aus t_menu
        '********************************************************************************************************************************
        '

        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyDs As New DataSet

        Try
            MyCmd.CommandText = "PKG_APPLICATION.GetUserForAcessRight"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_retval", OracleDbType.RefCursor, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_group_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = v_group_Name

            MyCmd.Connection = Myconn.Open()
            MyCmd.Connection = Myconn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "User")
            MyDataView = MyDs.Tables("User").DefaultView
            Myconn.Close()
            Return MyDataView

        Catch ex As Exception


        Finally
            Myconn.Close()
        End Try

    End Function


    Public Function GetUserIDsForAcessRight(ByVal v_group_Name) As Collection
        '********************************************************************************************************************************
        '* Holt die entsprechende Modul (Security) ID aus t_menu
        '********************************************************************************************************************************
        '

        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim MyDs As New DataSet
        Dim Users As New Collection
        Dim i As Integer = 0


        Try
            MyCmd.CommandText = "PKG_APPLICATION.GetUserForAcessRight"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_retval", OracleDbType.RefCursor, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_group_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = v_group_Name

            MyCmd.Connection = Myconn.Open()
            MyCmd.Connection = Myconn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "User")
            MyDataView = MyDs.Tables("User").DefaultView
            Myconn.Close()

            For Each row As DataRow In MyDataView.Table.Rows
                Users.Add(row.Item("id_User"), row.Item("id_User"))
            Next

            Return Users

        Catch ex As Exception


        Finally
            Myconn.Close()
        End Try

    End Function

End Class
