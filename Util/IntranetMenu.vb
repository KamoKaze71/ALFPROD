Imports System
Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities
Imports Wyeth.Alf
'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
'''         <revision>1</revision>
''' <summary><para>Class for rendering tha ALF Menu </para></summary>
Public Class IntranetMenu
	Inherits WebControl
	Private m_i_menu_ctry_id As Integer
	Private m_bol_menuhead As Boolean
    Private m_i_menu_id_Parent As Integer
    Private m_i_modul_id As Integer

    Dim i As Integer = 0
    Dim x As Integer = 10
    Dim empty As String = ""
    Dim Myuseraccess As New UserAccess


	Public Sub New()
		MyBase.New()
	End Sub

	Protected Overrides Sub Render(ByVal writer As HtmlTextWriter)



        Dim strOut, strOutSub As String
        Dim menu_category As String = "leftnavi"
		Dim MyReader As OracleDataReader
		Dim MyReaderSUB As OracleDataReader
		Dim MyCmd As New OracleCommand
		Dim Myconn As New MyConnection
        Dim user_id As Integer = HttpContext.Current.Session("User_id")
        Dim group_id() As Integer = HttpContext.Current.Session("user_groups")
		Try

	
		MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_APPLICATION.FillLeftNaviMenu"
		MyCmd.Parameters.Add("Menu", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
		MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = MenuCtryID
		MyCmd.Parameters.Add("v_menu_id_parent", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = MenuIDParent
		MyCmd.Parameters.Add("v_menu_category", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = menu_category

		MyCmd.Connection = Myconn.Open()
		MyReader = MyCmd.ExecuteReader()

		If MenuHeader = True Then		' Render Menu Bar for the Header frame


				strOut = "<div id='alphanav2'>"
				strOut += "<table id='tablenav2' border='0' cellpadding='0' cellspacing='0'>"
				strOut += "<tr halign='bottom'>"


                While MyReader.Read
                    If Myuseraccess.getAccess(MyReader("Menu_access_id"), user_id, group_id) > 0 Then

                        If MyReader("Menu_label").ToString.ToUpper = "HOME" Then
                            strOut += "<td id='td" & i & "' class='topnav navbackground' nowrap>"
                            strOut += "<a class='navlink' href='" & MyReader("menu_link") & "?id=" & MyReader("menu_id") & "?pageTitle=" & MyReader("Menu_label") & "' target='" & MyReader("menu_target") & "' onclick=""select(td" & i & ");parent.frames(1).location.href='leftnavi.aspx';""  title='" & MyReader("Menu_label") & "'>" & MyReader("menu_label") & "</a>"
                            strOut += "</td>"
                        Else
                            strOut += "<td id='td" & i & "' class='topnav navbackground' nowrap>"
                            strOut += "<a class='navlink' href='" & MyReader("menu_link") & "?id=" & MyReader("menu_id") & "' target='" & MyReader("menu_target") & "' onclick='select(td" & i & ");' title='" & MyReader("Menu_Name") & "'>" & MyReader("menu_Name") & "</a>"
                            strOut += "</td>"
                        End If
                        i = i + 1
                    End If


                End While

                strOut += "</tr></table></div>"
                i = 0

            Else    ' render menu for leftnavi frame



                    strOut = "<div id='mItems5' class='leftnavbar' style='position:absolute;top:0;'><span id='mItems5Lvl1Span5'>"
                    While MyReader.Read
                    'If Myuseraccess.getAccess(MyReader("Menu_access_id"), user_id, group_id) > 0 Then


                    If MyReader("menu_link") Is DBNull.Value Then
                        If Myuseraccess.getAccess(MyReader("Menu_access_id"), user_id, group_id) > 0 Then
                            strOut += renderOnClick("000" & MyReader("menu_link"), MyReader("Menu_label"), MyReader("Menu_label"), "000" & MyReader("Menu_id"), "level1")
                        End If

                    Else
                        If Myuseraccess.getAccess(MyReader("Menu_access_id"), user_id, group_id) > 0 Then
                            strOut += renderLink(MyReader("menu_link"), MyReader("Menu_label"), MyReader("Menu_label"), "000" & MyReader("Menu_id") & i, "level1", "main", empty)

                        End If

                    End If

                    MyCmd.Parameters.Clear()
                    MyCmd.Parameters.Add("Menu", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                    MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = MenuCtryID
                    MyCmd.Parameters.Add("v_menu_id_parent", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = MyReader("menu_id")
                    MyCmd.Parameters.Add("v_menu_category", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = menu_category
                    MyReaderSUB = MyCmd.ExecuteReader()


                    strOut += "<span id='grp_" & "000" & MyReader("menu_id") & "' style='display:none'>"

                    Do While MyReaderSUB.Read
                        strOutSub += renderLink(MyReaderSUB("menu_link"), MyReaderSUB("Menu_label"), MyReaderSUB("Menu_name"), "000" & MyReader("Menu_id") & x, "level2", "main", empty)
                        x = x + 1
                    Loop
                    strOut += strOutSub & "</span>"
                    strOutSub = ""


                    MyReaderSUB.Close()

                    i = i + 1
                    'End If
                End While

                strOut += "</span></div>"
                If i = 0 Then
                    strOut = ""
                End If
                End If

          
            writer.Write(strOut)




        Catch ex As Exception
		Finally
			Myconn.Close()
		End Try
	End Sub
	'********************************************************************************************************************************
	'* Will render a common menu-item
	'********************************************************************************************************************************

	Public Function renderLink(ByVal linkUrl, ByVal linkName, ByVal linkTitle, ByVal linkId, ByVal linkClass, ByVal linkTarget, ByVal onclick) As String
		Dim strMenu As String
		If (Len(linkTitle) = 0) Then linkTitle = linkName

		strMenu = (vbCrLf & "<!-- " & linkTitle & " -->" & vbCrLf)
		strMenu += ("<div id='nav_" & linkId & "' class='" & linkClass & "'>")
		strMenu += ("<img id='img_" & linkId & "' src='images/nav.gif' alt='open' width='12' height='7'>")
		strMenu += ("<a id='link_" & linkId & "' href='" & linkUrl & "?pageTitle=" & linkTitle & "' target='" & linkTarget & "' onclick=""expander('" & linkId & "');" & onclick & """ title='" & linkTitle & "' class='navlink'>" & linkName & " </a>")
		strMenu += ("</div>" & vbCrLf)

		Return strMenu

	End Function

	'********************************************************************************************************************************
	'* Will render a menu-item for expanding
	'********************************************************************************************************************************
	Public Function renderOnClick(ByVal onClick, ByVal linkName, ByVal linkTitle, ByVal linkId, ByVal linkClass) As String
		Dim strMenu As String
		If (Len(linkTitle) = 0) Then linkTitle = linkName

		strMenu += (vbCrLf & "<!-- " & linkTitle & " -->" & vbCrLf)
		strMenu += ("<div id='nav_" & linkId & "' class='" & linkClass & "'>")
		strMenu += ("<img id='img_" & linkId & "' src='images/navClosed.gif' alt='open' width='12' height='7'>")
		strMenu += ("<a id='link_" & linkId & "' href='#' onclick=""expander('" & linkId & "','" & linkId & "');" & onClick & """ title='" & linkTitle & "' class='navlink'>" & linkName & " </a>")
		strMenu += ("</div>" & vbCrLf)
		Return strMenu
	End Function

   
    Public Property MenuCtryID() As Integer
        Get
            Return m_i_menu_ctry_id
        End Get
        Set(ByVal Value As Integer)
            m_i_menu_ctry_id = Value
        End Set
    End Property

    Public Property MenuHeader() As Boolean
        Get
            Return m_bol_menuhead
        End Get
        Set(ByVal Value As Boolean)
            m_bol_menuhead = Value
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

    Public Property MenuModulID() As Integer
        Get
            Return m_i_modul_id
        End Get
        Set(ByVal Value As Integer)
            m_i_modul_id = Value
        End Set
    End Property


End Class
