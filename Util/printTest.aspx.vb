Imports WebSupergoo.ABCpdf4

Public Class printTest1
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

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

    Private p_reportData As String = String.Empty
    Private p_TableHeader As String = String.Empty
    Private p_CustomTableHeader As String = String.Empty
    Private p_Styles As String = String.Empty
    Private p_htmlParts As New ArrayList
    Private p_pageTitle As String
    Private p_pageSize As Integer = 28
    'Private p_pageSizeL As Integer = 30

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim webGrid As ArrayList
        Dim intCountGrids As Integer = 0

        Me.getQueryString()

        webGrid = Session.Item(Me.SessionName)

        Me.readStyleFile()
        Me.setDefaultPageSize()

        For Each item As Object In webGrid
            If TypeOf (item) Is WebGrids Then
                Me.splitReport(item)
            ElseIf TypeOf (item) Is String Then
                Me.p_reportData = item
            End If
        Next

        'Dim x As Integer = 32, y As Integer = 28, w As Integer = 548, h As Integer = 724
        Dim theID As Integer = 0
        Dim theDoc As New Doc
        Session("doc") = theDoc

        Dim w As Double = theDoc.MediaBox.Width
        Dim h As Double = theDoc.MediaBox.Height
        Dim l As Double = theDoc.MediaBox.Left
        Dim b As Double = theDoc.MediaBox.Bottom

        theDoc.Transform.Rotate(90, l, b)   'Rotate the Text
        theDoc.Transform.Translate(w, 0)    'and move it a little bit :)

        theDoc.Rect.Width = h
        theDoc.Rect.Height = w

        'theDoc.Rect.Inset(10, 20)   'with these values you can change the position of the text

        'theDoc.Rect.SetRect(x, y, w, h)

        For i As Integer = 0 To p_htmlParts.Count - 1
            'left bottom right top'
            'theDoc.Rect.String = "10 20 770 600"
            theDoc.Rect.String = "0 0 790 612"
            theID = theDoc.AddImageHtml(p_htmlParts(i), True, 0, True)  'Add the Text into the PDF Page
            theID = theDoc.GetInfoInt(theDoc.Root, "Pages")             'Get the ID of the Root Page
            theDoc.SetInfo(theID, "/Rotate", "90")                      'and rotate it about 90 Degrees

            theDoc.Rect.String = "375 30 440 40"
            theDoc.AddText(i + 1 & " of " & p_htmlParts.Count)

            'theDoc.Rect.String = "20 30 100 40"
            'theDoc.AddText("pagesize: " & PageSize)

            'While theDoc.GetInfo(theID, "Truncated") = "1"
            '    theDoc.Page = theDoc.AddPage()
            '    theID = theDoc.AddImageToChain(theID)
            'End While

            'theDoc.Rect.Inset(10, 50)
            'theDoc.AddText("Test")

            If i < (p_htmlParts.Count - 1) Then                         'If there is still a page left
                theDoc.Page = theDoc.AddPage()                          'add a new page
            End If
        Next

        'Response.Write(p_htmlParts(0))
        'Response.End()

        theDoc.PageNumber = 1
        Response.Redirect("printPDF.aspx")
    End Sub

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
                count += 7
            End If

            If intCurrentIndex > 1 Then
                count += 1
            Else
                blnFlag = True
            End If

            If count = Me.p_pageSize Then 'PageBreak
                strHelper = strWebgrid.Substring(intSubstringStartPosition, intCurrentIndex - intSubstringStartPosition + 5) & "</table>"
                'strSplittedPage += Me.CreateNewSite(strHelper, intPageCount)

                Me.saveHtmlParts(strHelper, intPageCount)

                intSubstringStartPosition = intCurrentIndex + 5
                intPageCount += 1
                count = 0
            End If

            If blnFlag Then 'last Page
                strHelper = strWebgrid.Substring(intSubstringStartPosition)
                'strSplittedPage += Me.CreateNewSite(strHelper, intPageCount)

                Me.saveHtmlParts(strHelper, intPageCount)

                Exit While
            End If

        End While

        strSplittedPage = String.Empty
        Return strSplittedPage
    End Function

    'Private Function ReLen(ByVal str As String) As Integer
    '    Dim strNew As String = String.Empty
    '    Dim tmp As String() = Split(str)

    '    For i As Integer = 0 To UBound(tmp)
    '        If Len(tmp(i)) > 0 Then
    '            strNew += tmp(i) & " "
    '        End If
    '    Next

    '    Return Len(Trim(strNew))
    'End Function

    Private Sub saveHtmlParts(ByVal str As String, ByVal pageCount As Integer)

        If pageCount = 0 Then
            p_htmlParts.Add(p_Styles & Me.p_reportData & Me.InsertTableHeader() + str)
        Else
            p_htmlParts.Add(p_Styles & Me.InsertTableHeader() + str)
        End If
    End Sub

    Private Sub readStyleFile()
        Dim sr As New System.IO.StreamReader(Server.MapPath(Request.ApplicationPath & "/printpdf.css"))
        Dim temp As String = "<style type=""text/css"">" & sr.ReadToEnd() & "</style>"
        sr.Close()

        p_Styles = temp
    End Sub

    '************************************************************************************************
    '* InsertTableHeader - Tabellenkopf/köpfe in Seite einfügen
    '************************************************************************************************
    Private Function InsertTableHeader() As String
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
    '* getQueryString - den Querystring auslesen und zuweisen
    '************************************************************************************************
    Private Sub getQueryString()
        p_pageTitle = Request.QueryString("Pagetitle")
        'p_pageSize = Request.QueryString("PagesizeL")
        'p_pageSizeL = Request.QueryString("PagesizeL")
        'p_toolbar = Request.QueryString("ShowToolbar")
        'p_autoprint = Request.QueryString("AutoPrint")
        'p_defaultOrientation = Request.QueryString("Default")
    End Sub

    Private Sub setDefaultPageSize()
        Select Case UCase(p_pageTitle)
            Case PageTitles.S_Daily
                PageSize = 35
            Case PageTitles.S_GM
                PageSize = 32
            Case PageTitles.S_Net
                PageSize = 34
            Case PageTitles.S_Sales
                PageSize = 35
            Case PageTitles.S_Area
                PageSize = 28
            Case PageTitles.S_TPG
                PageSize = 28
            Case PageTitles.L_RUII
                PageSize = 35
            Case PageTitles.L_StockIris
                PageSize = 35
            Case Else
                PageSize = 30
        End Select
    End Sub

    Private Property PageSize() As Integer
        Get
            Return p_pageSize
        End Get
        Set(ByVal Value As Integer)
            p_pageSize = Value
        End Set
    End Property
End Class

Public Class PageTitles

    Public Const S_Daily As String = "DAILY SALES REPORT"
    Public Const S_GM As String = "GM REPORT"
    Public Const S_Net As String = "NET SALES REPORT"
    Public Const S_Sales As String = "SALES STATISTICS"
    Public Const S_Area As String = "AREA SALES STATISTICS"
    Public Const S_TPG As String = "TPG QUICK STATISTICS"
    Public Const L_RUII As String = "RUII REPORT"
    Public Const L_StockIris As String = "STOCK FOR IRIS"



End Class