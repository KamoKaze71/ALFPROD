Imports System.IO
Imports System.Runtime.InteropServices

Imports C1.Web.C1WebGrid

Public Class printReport
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents litPrint As System.Web.UI.WebControls.Literal
    Protected WithEvents ddLines As System.Web.UI.WebControls.DropDownList
    Protected WithEvents pnlToolbar As System.Web.UI.WebControls.Panel
    Protected WithEvents Dropdownlist1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents btnGenerate As System.Web.UI.WebControls.Button
    Protected WithEvents ddlOrientation As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblOrientation As System.Web.UI.WebControls.Label
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Public Shared SessionName As String = "dg_print_sid"
    Public Shared ShowPageCount As Boolean = True

    Private p_dgPrint As String
    Private p_TableHeader As String
    Private p_CustomTableHeader As String
    Private p_reportData As String = String.Empty
    Private p_pageTitle As String = String.Empty
    Private p_toolbar As String = String.Empty
    Private p_autoprint As String = String.Empty
    Private p_defaultOrientation As String = String.Empty
    Private p_pageSize As Integer = 30
    Private p_pageSizeL As Integer = 30

    '************************************************************************************************
    '* Page_Load
    '************************************************************************************************
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim strTemp As String, jsShowPopup As String = String.Empty
        Dim webGrid As ArrayList
        Dim intCountGrids As Integer = 0
        litPrint.Text = ""

        Me.getQueryString()

        'Fenster verstecken wenn automatisches Drucken aktiv
        If p_autoprint = "yes" And Not Page.IsPostBack Then
            Me.registerJS("Repositionx", "window.moveTo(-1000, -1000);")
        End If

        If Not Page.IsPostBack Then
            Me.setOrientation()
            Me.fillDropDown()
        Else
            If ddlOrientation.SelectedValue = 2 And ddLines.SelectedValue > 32 Then
                Me.p_pageSize = p_pageSizeL
                Me.fillDropDown()
            Else
                Me.p_pageSize = ddLines.SelectedValue
            End If
        End If

        webGrid = Session.Item(Me.SessionName)

        For Each item As Object In webGrid
            If TypeOf (item) Is WebGrids Then
                intCountGrids += 1
                If intCountGrids > 1 Then
                    litPrint.Text += "<div style=""page-break-before:always""></div>"
                End If
                litPrint.Text += Me.splitReport(item)
            ElseIf TypeOf (item) Is String Then
                Me.p_reportData = item
            End If
        Next

        'Toolbar ausblenden, falls eingestellt
        If p_toolbar = "no" Then
            lblNumber.Visible = False
            lblOrientation.Visible = False
            ddLines.Visible = False
            ddlOrientation.Visible = False
        End If

        'Falls automatisches Drucken -> drucken
        If p_autoprint = "yes" And Not Page.IsPostBack Then
            Me.registerJS("Autoprintx", "printAuto();")
        End If
    End Sub

    '************************************************************************************************
    '* registerJS - Javascript Funktion einbinden, die gleich ausgeführt werden soll
    '************************************************************************************************
    Private Sub registerJS(ByVal name As String, ByVal func As String)
        Dim strJavaScript As String = "<Script language=javascript>" & vbNewLine & func & vbNewLine & "</script>"
        Me.RegisterStartupScript(name, strJavaScript)
    End Sub

    '************************************************************************************************
    '* getQueryString - den Querystring auslesen und zuweisen
    '************************************************************************************************
    Private Sub getQueryString()
        p_pageTitle = Request.QueryString("Pagetitle")
        p_pageSize = Request.QueryString("Pagesize")
        p_pageSizeL = Request.QueryString("PagesizeL")
        p_toolbar = Request.QueryString("ShowToolbar")
        p_autoprint = Request.QueryString("AutoPrint")
        p_defaultOrientation = Request.QueryString("Default")
    End Sub

    '************************************************************************************************
    '* setOrientation - die Standardausrichtung der Seite festlegen
    '************************************************************************************************
    Private Sub setOrientation()
        If Not p_defaultOrientation = String.Empty Then
            ddlOrientation.SelectedValue = p_defaultOrientation
            If p_defaultOrientation = "2" Then
                Me.p_pageSize = Me.p_pageSizeL
            End If
        End If
    End Sub

    '************************************************************************************************
    '* fillDropDown - die Dropdownlist mit Zahlen auffüllen
    '************************************************************************************************
    Private Sub fillDropDown()
        Dim lstItem As ListItem

        ddLines.Items.Clear()

        For i As Integer = 10 To 50
            lstItem = New ListItem(i.ToString(), i.ToString())
            If i = Me.p_pageSize Then
                lstItem.Selected = True
            End If

            ddLines.Items.Add(lstItem)
        Next
    End Sub

    '************************************************************************************************
    '* Page_Disposed - Destructor
    '************************************************************************************************
    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Unload
        'Session.Remove(Me.SessionName)
    End Sub

    '************************************************************************************************
    '* getTableHeader - Header aus dem Webgrid auslesen
    '************************************************************************************************
    Private Function getTableHeader(ByVal str As String) As String
        Dim intLength As Integer
        Dim strThead As String = "</THEAD>"
        Dim strTemp As String = String.Empty

        strTemp = UCase(str)
        intLength = strTemp.IndexOf(strThead)
        If intLength > 1 Then
            Return (str.Substring(0, intLength + Len(strThead)))
        Else
            Return String.Empty
        End If
    End Function

    '************************************************************************************************
    '* splitReport - die Seiten aufsplitten 
    '************************************************************************************************
    Private Function splitReport(ByVal grid As WebGrids) As String

        Dim strWebgrid As String = Me.RemoveTableHeader(grid.Webgrid)
        Me.p_TableHeader = Me.getTableHeader(grid.Webgrid)
        Me.p_CustomTableHeader = grid.CustomHeader

        Dim strHelper As String, strSplittedPage As String = String.Empty
        Dim count As Integer = 0
        Dim blnFlag As Boolean = False
        Dim intPageCount As Integer = 0
        Dim intCurrentIndex As Integer
        Dim intStartIndex As Integer = 0
        Dim intSubstringStartPosition As Integer = 0

        While True
            intCurrentIndex = strWebgrid.IndexOf("</tr>", intStartIndex)
            intStartIndex = intCurrentIndex + 1

            If (intPageCount = 0) And (count = 0) Then ' wegem Header auf der ersten Seite weniger Zeilen
                count += 5
            End If

            If intCurrentIndex > 1 Then
                count += 1
            Else
                blnFlag = True
            End If

            If count = Me.p_pageSize Then 'PageBreak
                strHelper = strWebgrid.Substring(intSubstringStartPosition, intCurrentIndex - intSubstringStartPosition + 5) & "</table>"
                strSplittedPage += Me.CreateNewSite(strHelper, intPageCount)
                intSubstringStartPosition = intCurrentIndex + 5
                intPageCount += 1
                count = 0
            End If

            If blnFlag Then 'last Page
                strHelper = strWebgrid.Substring(intSubstringStartPosition)
                strSplittedPage += Me.CreateNewSite(strHelper, intPageCount)
                Exit While
            End If

        End While

        Return strSplittedPage
    End Function

    '************************************************************************************************
    '* InserTableHeader - Tabellenkopf/köpfe in Seite einfügen
    '************************************************************************************************
    Private Function InserTableHeader() As String
        If Me.p_CustomTableHeader = String.Empty Then Return Me.p_TableHeader

        Dim strHeader As String = String.Empty
        Dim intHeadPosition As Integer = 0

        strHeader = UCase(Me.p_TableHeader)
        intHeadPosition = strHeader.IndexOf("<THEAD>")

        strHeader = Me.p_TableHeader.Insert(intHeadPosition + 7, Me.p_CustomTableHeader)
        Return strHeader
    End Function

    '************************************************************************************************
    '* RemoveTableHeader - Tabellenkopf zu beginn aus Seite entfernen
    '************************************************************************************************
    Private Function RemoveTableHeader(ByVal str As String) As String
        If str = String.Empty Then Return String.Empty

        Dim strHeader As String = String.Empty
        Dim intHeadPostition As Integer = 0

        intHeadPostition = Len(Me.getTableHeader(str))
        strHeader = str.Remove(0, intHeadPostition)

        Return strHeader
    End Function

    '************************************************************************************************
    '* CreateNewSite - die Seite rendern
    '************************************************************************************************
    Private Function CreateNewSite(ByVal str As String, ByVal pageCount As Integer) As String
        Dim stringWrite As New StringWriter
        Dim htmlWrite As New System.Web.UI.HtmlTextWriter(stringWrite)
        Dim pagePanel As New Panel, blackPanel As New Panel, whitePanel As New Panel
        Dim myPanel As New Panel
        Dim myLabel As New Label

        If Not pageCount = 0 Then
            str = Me.InserTableHeader() + str
            pagePanel.Style.Add("page-break-before", "always")
        Else
            str = Me.p_reportData & Me.InserTableHeader() + str
        End If

        myPanel.CssClass = "rect" & ddlOrientation.SelectedValue
        whitePanel.CssClass = "printWhite" & ddlOrientation.SelectedValue
        blackPanel.CssClass = "printBlack" & ddlOrientation.SelectedValue
        pagePanel.CssClass = "page" & ddlOrientation.SelectedValue

        myPanel.ID = "panel" & pageCount
        myLabel.ID = "label" & pageCount
        myLabel.Text = str

        myPanel.Controls.Add(myLabel)

        If ShowPageCount Then
            Dim div As New HtmlGenericControl("div")
            div.Attributes.Add("align", "center")
            div.Attributes.Add("class", "field")
            div.InnerHtml = "<BR>[ " & pageCount + 1 & " ]"

            myPanel.Controls.Add(div)
        End If

        whitePanel.Controls.Add(myPanel)
        blackPanel.Controls.Add(whitePanel)
        pagePanel.Controls.Add(blackPanel)

        pagePanel.RenderControl(htmlWrite)

        Return "<BR>" & stringWrite.ToString()
    End Function

End Class


Public Class printReportUtil

    Private p_setSession As Boolean = True
    'Private p_pageLink As String = "../util/printReport.aspx"  'Print/Preview Ansicht
    Private p_pageLink As String = Wyeth.Utilities.Settings.applicationUrl & "util/printTest.aspx"      'PDF Ansicht
    Private p_pageTitle As String
    Private p_datagrids As New ArrayList
    Private p_defaultOrientation As Orientation = Orientation.Portrait
    Private p_pageSize As Integer = 48
    Private p_pageSizeL As Integer = 30

    '*************************************************************************
    '* Anzahl der Zeilen pro Seite
    '*************************************************************************
    Public Property PageSize() As Integer
        Get
            Return p_pageSize
        End Get
        Set(ByVal Value As Integer)
            p_pageSize = Value
        End Set
    End Property

    Public Property PageSizeLandscape() As Integer
        Get
            Return p_pageSizeL
        End Get
        Set(ByVal Value As Integer)
            p_pageSizeL = Value
        End Set
    End Property

    '*************************************************************************
    '* Standardausrichtung beim Aufruf der Seite
    '*************************************************************************
    Public Property DefaultOrientation()
        Get
            Return p_defaultOrientation
        End Get
        Set(ByVal Value)
            p_defaultOrientation = Value
        End Set
    End Property

    '*************************************************************************
    '* Titel der Seite
    '*************************************************************************
    Public Property PageTitle() As String
        Get
            Return p_pageTitle
        End Get
        Set(ByVal Value As String)
            p_pageTitle = Value
        End Set
    End Property

    '*************************************************************************
    '* Automatisch ein Session Objekt anlegen ?
    '*************************************************************************
    Public Property GenerateSessionObject() As Boolean
        Get
            Return p_setSession
        End Get
        Set(ByVal Value As Boolean)
            p_setSession = Value
        End Set
    End Property

    '*************************************************************************
    '* Enthält den Link für die util/printReport Seite
    '*************************************************************************
    Public Property PrintReportLink() As String
        Get
            Return p_pageLink
        End Get
        Set(ByVal Value As String)
            p_pageLink = Value
        End Set
    End Property

    '*************************************************************************
    '* Datagrid hinzufügen
    '*************************************************************************
    Public Sub AddWebGrid(ByVal grid As Object)
        Me.AddWebGrid(String.Empty, grid)
    End Sub

    Public Sub AddWebGrid(ByVal customHeader As String, ByVal grid As Object)
        Dim webgrid As New WebGrids

        If TypeOf (grid) Is DataGrid Then
            webgrid.GridType = WebGridType.Datagrid
        ElseIf TypeOf (grid) Is C1.Web.C1WebGrid.C1WebGrid Then
            webgrid.GridType = WebGridType.C1Webgrid
        Else
            webgrid.GridType = WebGridType.ReportHeader
        End If

        webgrid.CustomHeader = customHeader
        webgrid.Webgrid = renderWebgrid(grid)

        p_datagrids.Add(webgrid)
    End Sub

    '*************************************************************************
    '* Reportheader hinzufügen
    '*************************************************************************
    Public Sub AddReportHeader(ByVal rep As reportData)
        p_datagrids.Add(renderReportData(rep))
    End Sub

    '*************************************************************************
    '* Printpreview Seite anzeigen 
    '*************************************************************************
    Public Sub print(ByRef refPage As Page)
        print(refPage, PopupWindowStyle.Normal)
    End Sub

    Public Sub print(ByRef refPage As Page, ByVal popupStyle As PopupWindowStyle)
        Dim strJavascript As String
        Dim strShowTool As String
        Dim strStyle As String = String.Empty
        Dim blnFlag As Boolean = False

        If Me.GenerateSessionObject Then
            refPage.Session.Add(printReport.SessionName, p_datagrids)
        End If

        'If popupStyle And PopupWindowStyle.Fullscreen Then strStyle += "fullscreen=yes,"
        If popupStyle And PopupWindowStyle.Dependend Then strStyle += "dependend=yes,"
        If popupStyle And PopupWindowStyle.Hotkeys Then strStyle += "hotkeys=yes,"
        If popupStyle And PopupWindowStyle.Location Then strStyle += "location=yes,"
        If popupStyle And PopupWindowStyle.Menubar Then strStyle += "menubar=yes,"
        If popupStyle And PopupWindowStyle.Scrollbars Then strStyle += "scrollbars=yes,"
        If popupStyle And PopupWindowStyle.NoScrollbars Then strStyle += "scrollbars=no,"
        If popupStyle And PopupWindowStyle.Status Then strStyle += "status=yes,"
        If popupStyle And PopupWindowStyle.Resizeable Then strStyle += "resizable=yes,"
        'If popupStyle And PopupWindowStyle.Autoprint Then strStyle += "width=1, height=1,"

        If popupStyle And PopupWindowStyle.NoToolbar Then strShowTool += "&ShowToolbar=no"
        If popupStyle And PopupWindowStyle.Autoprint Then strShowTool += "&AutoPrint=yes"

        If (popupStyle And PopupWindowStyle.Autoprint) And (Me.DefaultOrientation = Orientation.Landscape) Then
            blnFlag = True
        End If

        ' letzten teil von strStyle den "," entfernen
        If Not strStyle = String.Empty Then
            strStyle = strStyle.Substring(0, strStyle.Length - 1)
        End If

        'vbNewLine & "if('" & LCase(blnFlag) & "' == 'true') alert('This Page has been optimized to be printed in ""Landscape Format"". \nPlease choose the Orientation ""Landscape"" in the Printer dialog');" & _
        strJavascript = "<Script language=javascript>" & _
                        vbNewLine & "window.open('" & _
                        p_pageLink & "?Pagetitle=" & Me.PageTitle & "&Default=" & Me.DefaultOrientation & "&PagesizeL=" & Me.PageSizeLandscape & "&Pagesize=" & Me.PageSize & strShowTool & "', 'PrintDialog','" & strStyle & "');" & _
                        vbNewLine & "</Script>"

        'vbNewLine & "this.location.href='" & refPage.Request.ServerVariables.Item("URL") & "?pageTitle=" & Me.PageTitle & "';" & _
        refPage.RegisterStartupScript("Startup", strJavascript)
    End Sub

    '************************************************************************************************
    '* drawReportData - Report Header zeichnen 
    '************************************************************************************************
    Private Function renderReportData(ByVal rep As reportData) As String
        Dim strHTML As String = String.Empty
        Dim stringWrite As New System.IO.StringWriter
        Dim htmlWrite As New System.Web.UI.HtmlTextWriter(stringWrite)

        rep.RenderControl(htmlWrite)

        strHTML += "<div align=left><SPAN Class=""TableHeader"">" & Me.p_pageTitle & "</SPAN><BR><BR>" & _
                    stringWrite.ToString() & "<BR>"

        Return strHTML
    End Function

    '*************************************************************************
    '* Rendered das Datagrid in einen String und liefert diesen zurück
    '*************************************************************************
    Private Function renderWebgrid(ByVal dg As Object) As String
        Dim stringWrite As New StringWriter
        Dim htmlWrite As New System.Web.UI.HtmlTextWriter(stringWrite)

        If TypeOf (dg) Is C1.Web.C1WebGrid.C1WebGrid Then
            dg = webGridModifications(dg)
            Me.removeControls(dg)
        End If

        dg.RenderControl(htmlWrite)
        Return stringWrite.ToString()
    End Function

    '*************************************************************************
    '* Entfernt "Sortier"-Header und Checkboxen
    '*************************************************************************
    Private Sub removeControls(ByRef cont As Control)
        Dim i As Integer = cont.Controls.Count - 1
        Dim x As String = String.Empty
        Dim a As Control = cont.Controls(0)
        For Each b As Control In a.Controls
            For Each c As Control In b.Controls
                For asdf As Integer = 0 To c.Controls.Count - 1
                    Dim d As Control = c.Controls(asdf)
                    If Not d.GetType().GetProperty("Text") Is Nothing Then
                        Dim y As New Literal
                        y.Text = d.GetType().GetProperty("Text").GetValue(d, Nothing)
                        d.Parent.Controls.Add(y)
                        d.Visible = False
                    End If
                    If Not d.GetType().GetProperty("Checked") Is Nothing Then
                        Dim y As New Literal
                        If Convert.ToBoolean(d.GetType().GetProperty("Checked").GetValue(d, Nothing)) Then
                            y.Text = "X"
                        Else
                            y.Text = "&nbsp;"
                        End If
                        d.Parent.Controls.Add(y)
                        d.Visible = False
                    End If
                Next
            Next
        Next
    End Sub


    '************************************************************************************************
    '* webGridModifications  - alle gruppierungen werden geöffnet vorm export (c1.webgrid)
    '************************************************************************************************
    Private Function webGridModifications(ByVal dg As C1.Web.C1WebGrid.C1WebGrid) As C1.Web.C1WebGrid.C1WebGrid
        For Each col As C1.Web.C1WebGrid.C1Column In dg.Columns
            col.GroupInfo.OutlineMode = C1.Web.C1WebGrid.OutlineModeEnum.None
        Next

        Return dg
    End Function
End Class
'************************************************************************************************
'************************************************************************************************
'************************************************************************************************
'* Structure WebGrids  - enthält dann die gerenderten Datagrids
'************************************************************************************************
Structure WebGrids
    Dim CustomHeader As String
    Dim Webgrid As String
    Dim GridType As WebGridType
End Structure

'************************************************************************************************
'* Enum WebGridType  - definiert den Typ der Datagrids
'************************************************************************************************
Public Enum WebGridType
    Datagrid
    C1Webgrid
    ReportHeader
End Enum

'************************************************************************************************
'* Enum PopupWindowStyle  - definiert den Typ für das neue Fenster
'************************************************************************************************
Public Enum PopupWindowStyle
    Normal = 1
    Fullscreen = 2
    NoToolbar = 4
    Autoprint = 8
    Dependend = 16
    Hotkeys = 32
    Location = 64
    Menubar = 128
    Scrollbars = 256
    NoScrollbars = 512
    Status = 1024
    Resizeable = 2048
    Hidden = 4096
End Enum

Public Enum Orientation
    Portrait = 1
    Landscape = 2
End Enum