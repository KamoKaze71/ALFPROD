''' <summary>Provides Basic JavaScript Functions for ALF Page Class</summary>
Public Class JSPopUp
    'Inherits System.Web.UI.Page

    Private m_i_width As Integer = 300
    Private m_i_height As Integer = 400
    Private m_i_left As Integer = 80
    Private m_i_top As Integer = 70
    Private m_str_title As String = "new_window"
    Private m_str_url As String = "../util/datepicker.aspx"
    Private m_bol_autofocus As Boolean = False
    Private m_bol_scrollbars As Boolean = False
    Private m_bol_statusbar As Boolean = False
    Private strFunctionCall As String
    Private str_scrollbar As String = "no"
    Private str_statusbar As String = "no"
    Private WithEvents my_page As Page
    Private strConfirmMessage As String

    Public Sub New(ByRef m_page As Page)
        ' MyBase.New()
        my_page = m_page

    End Sub

    Public Function GetJSFunctionCall() As String
        strFunctionCall = "javascript:ShowPopUp('" & PageURL & "'," & Width & "," & Height & "," & Left & "," & Top & ",'" & str_scrollbar & "','" & StatusBar & "');"
        Return strFunctionCall
    End Function

    Public Overloads Sub AddPopupToControl(ByRef MyControl As WebControl)
        strFunctionCall = "ShowPopUp('" & PageURL & "'," & Width & "," & Height & "," & Left & "," & Top & ",'" & str_scrollbar & "','" & StatusBar & "');"
        MyControl.Attributes.Add("OnClick", strFunctionCall)
    End Sub

    Public Overloads Sub AddPopupToControl(ByRef MyControl As HtmlControl)
        strFunctionCall = "ShowPopUp('" & PageURL & "'," & Width & "," & Height & "," & Left & "," & Top & ",'" & str_scrollbar & "','" & str_statusbar & "');"
        MyControl.Attributes.Add("OnClick", strFunctionCall)
    End Sub

    Public Overloads Sub AddDatePopupToControl(ByRef MyControlTextBox As WebControl)
        Width = 158
        Height = 222
        Left = 300
        Top = 150
        strFunctionCall = "ShowDatePopUp('" & PageURL & "'," & Width & "," & Height & "," & Left & "," & Top & ",'" & str_scrollbar & "','" & str_statusbar & "','Form1." & MyControlTextBox.ClientID.ToString & "','" & Me.Title & "');"
        MyControlTextBox.Attributes.Add("OnClick", strFunctionCall)
    End Sub

    Public Overloads Sub AddDatePopupToControl(ByRef MyControlTextBox As WebControl, ByRef MyControlToClick As WebControl)
        Width = 158
        Height = 222
        Left = 300
        Top = 150
        strFunctionCall = "ShowDatePopUp('" & PageURL & "'," & Width & "," & Height & "," & Left & "," & Top & ",'" & str_scrollbar & "','" & str_statusbar & "','Form1." & MyControlTextBox.ClientID.ToString & "','" & Me.Title & "');"
        MyControlToClick.Attributes.Add("OnClick", strFunctionCall)
        MyControlToClick.Attributes.Add("OnMouseOver", "this.style.cursor='hand'")
    End Sub
    Public Overloads Sub AddDatePopupToControl(ByRef MyControlTextBox As WebControl, ByRef MyImageToClick As HtmlImage)
        Width = 158
        Height = 222
        Left = 300
        Top = 150
        strFunctionCall = "ShowDatePopUp('" & PageURL & "'," & Width & "," & Height & "," & Left & "," & Top & ",'" & str_scrollbar & "','" & str_statusbar & "','Form1." & MyControlTextBox.ClientID.ToString & "','" & Me.Title & "');"
        MyImageToClick.Attributes.Add("OnClick", strFunctionCall)
        MyImageToClick.Attributes.Add("OnMouseOver", "this.style.cursor='hand'")
    End Sub
    Public Sub AddGetConfirm(ByRef MyControl As WebControl)
        strFunctionCall = "javascript:return GetConfirmDel('" & ConfirmMessage & "');"
        MyControl.Attributes.Add("onclick", strFunctionCall)
        MyControl.Attributes.Add("OnMouseOver", "this.style.cursor='hand'")
    End Sub

    Public Sub ClosePopUp()
        Dim strJScript As String
        strJScript += "<script language =javascript >" & vbNewLine
        strJScript += "self.close();"
        strJScript += "</script>"
        my_page.Response.Write(strJScript)
    End Sub

    Public Overloads Sub ClosePopUpAndRefreshOpener()
        Dim strJScript As String
        strJScript += "<script language =javascript >"
        strJScript += "window.opener.location.href = window.opener.location.href;"
        strJScript += "self.close();"
        strJScript += "</script>"
        my_page.Response.Write(strJScript)
    End Sub

    Public Overloads Sub ClosePopUpAndRefreshOpener(ByVal qrystr As String)

        Dim strJScript As String
        strJScript += "<script language =javascript >"
        strJScript += "window.opener.location.href = window.opener.location.href +'" & qrystr & "';"
        strJScript += "self.close();"
        strJScript += "</script>"
        my_page.Response.Write(strJScript)

    End Sub

    Private Sub RegisterJScripts()
        Dim scripLiteral As Literal
        scripLiteral = my_page.FindControl("_clientScript")

        Dim strScriptHeader As String
        Dim strScript As String
        Dim strScriptFooter As String

        If AutoFocus = True Then
            strScript += "<script language =javascript >"
            strScript += "self.focus();"
            strScript += "</script>"
            my_page.RegisterStartupScript("Autofocus", strScript)
        End If

        strScriptHeader = "<script language =javascript >" & vbNewLine

        strScript += "function ShowPopUp(pageUrl, w, h, l, t, scroll, status) {" & vbNewLine
        strScript += "var newWin = window.open(pageUrl, 'newWindow', 'width=' + w + ',height=' + h + ',left=' + l + ',top=' + t + ',scrollbars=' + scroll + ',status=' + status );" & vbNewLine
        strScript += "newWin.focus();" & vbNewLine
        strScript += "}"

        strScript += vbNewLine

        'strScript += "function ShowDatePopUp(pageUrl, w, h, l, t, scroll, status,ret_field) {" & vbNewLine
        'strScript += "pageUrl='../Util/Datepicker.aspx?textbox='+ ret_field;"
        'strScript += "var newWin = window.open(pageUrl, 'newWindow', 'width=' + w + ',height=' + h + ',left=' + l + ',top=' + t + ',scrollbars=' + scroll + ',status=' + status );" & vbNewLine
        'strScript += "newWin.focus();" & vbNewLine
        'strScript += "}"


        strScript += "function ShowDatePopUp(pageUrl, w, h, l, t, scroll, status,ret_field,newWindow) {" & vbNewLine
        strScript += "pageUrl=pageUrl + '?textbox='+ ret_field;"
        strScript += "var newWin = window.open(pageUrl, newWindow, 'width=' + w + ',height=' + h + ',left=' + l + ',top=' + t + ',scrollbars=' + scroll + ',status=' + status );" & vbNewLine
        strScript += "newWin.focus();" & vbNewLine
        strScript += "}"

        strScript += vbNewLine

        strScript += "function GetConfirmDel(msg)" & vbNewLine
        strScript += "{" & vbNewLine
        strScript += "if (confirm(msg)==true)" & vbNewLine
        strScript += "return true;" & vbNewLine
        strScript += "else" & vbNewLine
        strScript += "{" & vbNewLine
        strScript += "return false;" & vbNewLine
        strScript += "}" & vbNewLine
        strScript += "}" & vbNewLine



        strScript += "function submitButton(button, evt) {"
        strScript += "evt = (evt) ? evt : window.event;"
        strScript += "if ((evt.which && evt.which == 13) || (evt.keyCode && evt.keyCode == 13)) {"
        strScript += "button.click();"
        strScript += "return false;"
        strScript += "} else {"
        strScript += "return true;"
        strScript += "}"
        strScript += "}"


        strScript += "function ClosePopUpAndRefreshOpener(qrystr)" & vbNewLine
        strScript += "{ " & vbNewLine
        strScript += "window.opener.location.href = window.opener.location.href + qrystr;" & vbNewLine
        strScript += "self.close();"
        strScript += "}"




        strScriptFooter += vbNewLine
        strScriptFooter += "</script>"
        strScriptFooter += vbNewLine

        scripLiteral.Text = strScriptHeader & strScript & strScriptFooter


    End Sub


    Public Property Width() As Integer
        Get
            Return m_i_width

        End Get
        Set(ByVal Value As Integer)
            m_i_width = Value
        End Set
    End Property
    Public Property Height() As Integer
        Get
            Return m_i_height

        End Get
        Set(ByVal Value As Integer)
            m_i_height = Value
        End Set
    End Property

    Public Property Top() As Integer
        Get
            Return m_i_top

        End Get
        Set(ByVal Value As Integer)
            m_i_top = Value
        End Set
    End Property
    Public Property Left() As Integer
        Get
            Return m_i_left

        End Get
        Set(ByVal Value As Integer)
            m_i_left = Value
        End Set
    End Property

    Public Property PageURL() As String
        Get
            Return m_str_url

        End Get
        Set(ByVal Value As String)
            m_str_url = Value
        End Set
    End Property

    Public Property Title() As String
        Get
            Return m_str_title

        End Get
        Set(ByVal Value As String)
            m_str_title = Value
        End Set
    End Property

    Public Property ConfirmMessage() As String
        Get
            Return strConfirmMessage

        End Get
        Set(ByVal Value As String)
            strConfirmMessage = Value
        End Set
    End Property

    Public Property AutoFocus() As Boolean
        Get
            Return m_bol_autofocus

        End Get
        Set(ByVal Value As Boolean)
            m_bol_autofocus = Value
        End Set
    End Property

    Public Property ScrollBars() As Boolean
        Get
            Return m_bol_scrollbars
        End Get
        Set(ByVal Value As Boolean)
            m_bol_scrollbars = Value

            If m_bol_scrollbars = True Then
                str_scrollbar = "yes"
            Else
                str_scrollbar = "no"
            End If
        End Set
    End Property


    Public Property StatusBar() As Boolean
        Get
            If m_bol_statusbar = True Then
                str_statusbar = "yes"
            Else
                str_statusbar = "no"
            End If

            Return m_bol_statusbar
        End Get
        Set(ByVal Value As Boolean)
            m_bol_statusbar = Value
        End Set
    End Property

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles my_page.PreRender
        RegisterJScripts()
    End Sub
End Class
