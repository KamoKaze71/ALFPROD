

Imports Oracle.DataAccess.Client
Imports Wyeth.Alf.JSPopUp
Imports Wyeth.Utilities
Imports Wyeth.Alf.UserAccess

'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
'''         <revision>23</revision>
''' <summary><para>Base Class for ALF Pages implements  Access Right, automatic
''' PageTitle Generartion from t_menu and Referrer</para></summary>
'''
'''    <todo>Improve exception handling</todo>
Public Class AlfPage
    Inherits System.Web.UI.Page

    Dim MyUserAccess As New UserAccess
    Dim MyConnection As New MyConnection
    Dim lblPageTitle As New Label

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Me.Page.IsPostBack Then

            ALFPageReferrer = Me.ViewState.Item("referrer")
            Me.ViewState.Item("referrer") = ALFPageReferrer

        Else
            SetReferrer()
        End If
    End Sub


    Private Sub SetReferrer()

        If IsNothing(Request.UrlReferrer) = False Then
            ALFPageReferrer = Request.UrlReferrer.LocalPath
            Me.ViewState.Add("referrer", ALFPageReferrer)
        Else
            ALFPageReferrer = Nothing
        End If
    End Sub

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Init
        Dim div As HtmlGenericControl = New HtmlGenericControl("div")
        div.Attributes.Add("class", "hl")
        lblPageTitle.Text = Me.ALFPageTitle
        div.Controls.Add(lblPageTitle)
        Me.Controls.AddAt(1, div)
    End Sub

    Private Sub BuildPage()
        Dim MyLiteral As New LiteralControl
        Dim str As String
        Me.Controls.AddAt(0, MyLiteral)

        str += "<html>"
        str += "<head>"
        str += "<title>" + ALFPageTitle + "</title>"
        str += "</head>"
        MyLiteral.Text = str
    End Sub

    Public Enum Rights As Integer
        AccessDenied = 0
        Read = 1
        Write = 2
        Admin = 3
    End Enum

    Public Property ALFPageAccessRights() As Rights
        Get
            m_i_module_id = MyUserAccess.GetMenuModuleID(Me.Request.Url.LocalPath.ToString.Substring(Me.Request.Url.LocalPath.ToString.IndexOf("/", 2) + 1))
            m_i_access_right = MyUserAccess.getAccess(m_i_module_id, Session("user_id"), Session("user_groups"))
            Return m_i_access_right
        End Get
        Set(ByVal Value As Rights)
            m_i_access_right = Value
        End Set
    End Property

    Public Property ALFPageTitle() As String
        Get

            If m_str_page_title = "" Then
                m_str_page_title = Me.Request.QueryString("pageTitle")
            End If

            If m_str_page_title = "" Then
                m_str_page_title = getpageTitle(Me.Request.ServerVariables("script_name").ToString.Substring(Me.Request.ServerVariables("script_name").ToString.IndexOf("/", 2) + 1))
            End If

            Return m_str_page_title
        End Get
        Set(ByVal Value As String)
            lblPageTitle.Text = Value
            m_str_page_title = Value
        End Set
    End Property
    Public Property ALFPageReferrer() As String
        Get
            Return m_str_referrer
        End Get
        Set(ByVal Value As String)

            m_str_referrer = Value
        End Set
    End Property

    Private m_i_access_right As Integer
    Private m_i_module_id As Integer
    Private m_str_page_title As String
    Private m_str_referrer As String

    Private Function getpageTitle(ByVal filename As String) As String
        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Dim PageTitle As String
        Dim val As Object

        Try
            MyCmd.CommandText = "PKG_APPLICATION.GetPageTitle"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("pageTitle", OracleDbType.Varchar2, 500, PageTitle, ParameterDirection.ReturnValue)
            MyCmd.Parameters.Add("v_filename", OracleDbType.Varchar2, ParameterDirection.Input).Value = filename
            MyCmd.Connection = Myconn.Open()
            MyCmd.ExecuteScalar()
            PageTitle = Convert.ToString(MyCmd.Parameters(0).Value)
            Return PageTitle
        Catch ex As OracleException
            Dim additionalInfo As String
            additionalInfo = filename
            ExceptionInfo.Show(ex, additionalInfo)
        Finally
            Myconn.Close()

        End Try
    End Function

End Class
