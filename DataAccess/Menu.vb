Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.Page
Imports System.Web.UI.WebControls
Imports System.Collections
Imports Oracle.DataAccess.Client
Imports System.Drawing
Imports Wyeth.Utilities
Imports Wyeth.Alf.CssStyles

'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
''' <summary><para>Provides DataAccess for ALF Menu (T_Menu)</para></summary>
Public Class Menu
	Inherits WebControl


    Private m_strTabImage As String
	Private m_strMenu_Category As String
	Private m_strMenu_Type As String
	Private m_strCssStyle As String
	Private m_strCssStyleHover As String
	Private m_i_menu_id As Integer
	Private m_str_menu_name As String
	Private m_i_menu_id_Parent As Integer
	Private m_i_menu_ctry_id As Integer
	Private m_i_menu_UserID As Integer
	Private m_i_menu_displayno As Integer
	Private m_str_menu_Target As String
	Private m_str_menu_link As String
	Private m_str_menu_label As String
    Private m_str_menu_width As String
    Private m_i_menu_access_id As Integer
    Private m_i_display As Integer


    Public Sub New()
        MyBase.New()
    End Sub

    Public Function GetModulesIDs() As DataView


        Dim MyReader As OracleDataReader
        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection

        Try

            Dim MyAdapter As New OracleDataAdapter
            Dim MyDataView As New DataView
            Dim MyDs As New DataSet

            MyCmd.CommandText = "PKG_APPLICATION.GetMenuModuleIds"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Menu", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_module_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = 2000

            MyCmd.Connection = Myconn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Menu")
            MyDataView = MyDs.Tables("Menu").DefaultView

            Return MyDataView
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Myconn.Close()
        End Try
    End Function


    Public Function GetMenu() As DataView


        Dim MyReader As OracleDataReader
        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection

        Try

            Dim MyAdapter As New OracleDataAdapter
            Dim MyDataView As New DataView
            Dim MyDs As New DataSet

            MyCmd.CommandText = "PKG_APPLICATION.GetMenu"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Menu", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = MenuCtryID

            MyCmd.Connection = Myconn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Menu")
            MyDataView = MyDs.Tables("Menu").DefaultView

            Return MyDataView
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Myconn.Close()
        End Try
    End Function

    Public Sub GetMenuIDParent(ByRef MyDropdown As DropDownList)

        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Dim i As Integer
        Dim item As New ListItem


        Try


            Dim MyReader As OracleDataReader
            Dim MyReaderSub As OracleDataReader

            MyCmd.CommandText = "PKG_APPLICATION.GetMenuIDparent"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Menu", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = MenuCtryID
            MyCmd.Parameters.Add("v_men_id_parent", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = MenuIDParent
            MyCmd.Parameters.Add("v_menu_category", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = Category
            MyCmd.Connection = Myconn.Open()
            MyReader = MyCmd.ExecuteReader()


            i = 0



            While MyReader.Read


                Dim itemtest As New ListItem
                i = i + 1
                MyCmd.Parameters.Clear()
                MyCmd.Parameters.Add("Menu", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = MenuCtryID
                MyCmd.Parameters.Add("v_men_id_parent", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = CStr(MyReader("menu_id"))
                MyCmd.Parameters.Add("v_menu_category", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = Category
                MyCmd.Connection = Myconn.Open()
                MyReaderSub = MyCmd.ExecuteReader()

                itemtest.Value = CStr(MyReader("menu_id"))
                itemtest.Text = CStr(MyReader("menu_Name"))
                MyDropdown.Items.Add(itemtest)


                While MyReaderSub.Read
                    If MyReaderSub("menu_link") Is DBNull.Value Then
                        Dim itemsub As New ListItem
                        i = i + 1
                        itemsub.Value = CStr(MyReaderSub("menu_id"))
                        itemsub.Text = "--" & CStr(MyReaderSub("menu_Name"))
                        MyDropdown.Items.Add(itemsub)
                    End If

                End While


            End While

            MyDropdown.DataBind()

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Myconn.Close()

        End Try
    End Sub
    Public Function DeleteMenuEntry() As Boolean
        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Try

            MyCmd.CommandText = "P_Menu_DEL_PROC"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_menu_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = MenuID

            MyCmd.Connection = Myconn.Open()
            MyCmd.ExecuteNonQuery()



        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Return False
        Finally
            Myconn.Close()
        End Try
        Return True
    End Function
    Public Function InsertMenuEntry() As Boolean
        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Try

            MyCmd.CommandText = "P_Menu_Ins_Proc"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_Menu_Name", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = MenuName
            MyCmd.Parameters.Add("v_Menu_Label", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = MenuLabel
            MyCmd.Parameters.Add("v_Menu_Link", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = MenuLink
            MyCmd.Parameters.Add("v_Menu_Target", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = MenuTarget
            MyCmd.Parameters.Add("v_Menu_Display_Number", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = MenuDisplayNo
            MyCmd.Parameters.Add("v_Menu_Category", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = Category
            MyCmd.Parameters.Add("v_Menu_User_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = MenuUserID
            MyCmd.Parameters.Add("v_Ctry_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = MenuCtryID
            MyCmd.Parameters.Add("v_Menu_ID_Parent", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = MenuIDParent
            MyCmd.Parameters.Add("v_Menu_access_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = MenuAccessId
            MyCmd.Parameters.Add("v_Menu_display", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = MenuDisplay

            MyCmd.Connection = Myconn.Open()
            MyCmd.ExecuteNonQuery()


        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Return False

        Finally
            Myconn.Close()

        End Try
        Return True

    End Function
    Public Function UpdateMenuEntry() As Boolean


        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Try
            MyCmd.CommandText = "P_Menu_Upd_Proc"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_Menu_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = MenuID
            MyCmd.Parameters.Add("v_Menu_Name", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = MenuName
            MyCmd.Parameters.Add("v_Menu_Label", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = MenuLabel
            MyCmd.Parameters.Add("v_Menu_Link", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = MenuLink
            MyCmd.Parameters.Add("v_Menu_Target", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = MenuTarget
            MyCmd.Parameters.Add("v_Menu_Display_Number", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = MenuDisplayNo
            MyCmd.Parameters.Add("v_Menu_Category", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = Category
            MyCmd.Parameters.Add("v_Menu_User_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = MenuUserID
            MyCmd.Parameters.Add("v_Ctry_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = MenuCtryID
            MyCmd.Parameters.Add("v_Menu_ID_Parent", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = MenuIDParent
            MyCmd.Parameters.Add("v_Menu_access_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = MenuAccessId
            MyCmd.Parameters.Add("v_Menu_display", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = MenuDisplay


            MyCmd.Connection = Myconn.Open()
            MyCmd.ExecuteNonQuery()



        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Return False
        Finally
            Myconn.Close()
        End Try

        Return True

    End Function




    Public Property TabImage() As String
        Get
            Return m_strTabImage
        End Get
        Set(ByVal Value As String)
            m_strTabImage = Value
        End Set
    End Property
    Public Property Category() As String
        Get
            Return m_strMenu_Category
        End Get
        Set(ByVal Value As String)
            m_strMenu_Category = Value
        End Set
    End Property
    Public Property Type() As String
        Get
            Return m_strMenu_Type
        End Get
        Set(ByVal Value As String)
            m_strMenu_Type = Value
        End Set
    End Property
    Public Property CssStyle() As String
        Get
            Return m_strCssStyle
        End Get
        Set(ByVal Value As String)
            m_strCssStyle = Value
        End Set
    End Property
    Public Property CssStyleHover() As String
        Get
            Return m_strCssStyleHover
        End Get
        Set(ByVal Value As String)
            m_strCssStyleHover = Value
        End Set
    End Property
    Public Property MenuID() As Integer
        Get
            Return m_i_menu_id
        End Get
        Set(ByVal Value As Integer)
            m_i_menu_id = Value
        End Set
    End Property
    Public Property MenuName() As String
        Get
            Return m_str_menu_name
        End Get
        Set(ByVal Value As String)
            m_str_menu_name = Value
        End Set
    End Property
    Public Property MenuLabel() As String
        Get
            Return m_str_menu_label
        End Get
        Set(ByVal Value As String)
            m_str_menu_label = Value
        End Set
    End Property
    Public Property MenuLink() As String
        Get
            Return m_str_menu_link
        End Get
        Set(ByVal Value As String)
            m_str_menu_link = Value
        End Set
    End Property
    Public Property MenuTarget() As String
        Get
            Return m_str_menu_Target
        End Get
        Set(ByVal Value As String)
            m_str_menu_Target = Value
        End Set
    End Property
    Public Property MenuDisplayNo() As Integer
        Get
            Return m_i_menu_displayno
        End Get
        Set(ByVal Value As Integer)
            m_i_menu_displayno = Value
        End Set
    End Property
    Public Property MenuUserID() As Integer
        Get
            Return m_i_menu_UserID
        End Get
        Set(ByVal Value As Integer)
            m_i_menu_UserID = Value
        End Set
    End Property
    Public Property MenuCtryID() As Integer
        Get
            Return m_i_menu_ctry_id
        End Get
        Set(ByVal Value As Integer)
            m_i_menu_ctry_id = Value
        End Set
    End Property
    Public Property MenuIDParent() As Integer
        Get
            Return m_i_menu_id_Parent
        End Get
        Set(ByVal Value As Integer)
            m_i_menu_id_Parent = Value

        End Set
    End Property
    Public Property MenuAccessId() As Integer
        Get
            Return m_i_menu_access_id
        End Get
        Set(ByVal Value As Integer)
            m_i_menu_access_id = Value
        End Set
    End Property

    Public Property MenuDisplay() As Integer
        Get
            Return m_i_display
        End Get
        Set(ByVal Value As Integer)
            m_i_display = Value
        End Set
    End Property
    Public Property MenuWidth() As String
        Get
            Return m_str_menu_width
        End Get
        Set(ByVal Value As String)
            m_str_menu_width = Value
        End Set
    End Property

End Class





