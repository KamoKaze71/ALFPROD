'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
'''         <revision>1</revision>
''' <summary>'*************************************************************************************************************
'* exportToExcel-Klasse 
'* ermöglicht den export eines datagrids (wobei auch abgeleitet datagrids verwendet werden können)
'* ins Excel. Exportiert nach wunsch auch report-header daten und unterstützt auch das exportieren
'* mehrerer Datagrids. 
'*************************************************************************************************************
''</summary>

Imports System
Imports System.Net
Imports System.Text
Imports System.IO

Public Class exportToExcel

    Private p_myPage As Page
    Private p_Datagrids As SortedList
    Private p_dg_id As Integer
    Private p_reportData As reportData
    Private p_title As String
    Private p_test As Boolean
    Private p_formatAsTextColumns As ArrayList
    Private p_contain As Boolean = False
    Private p_exportGroupHeaders = True
    Private p_exportHeaders As Boolean = True



    Public Property testWithHTML() As Boolean
        Get
            Return p_test
        End Get
        Set(ByVal Value As Boolean)
            Me.p_test = Value
        End Set
    End Property

    Public Property ContainsControls() As Boolean
        Get
            Return p_contain
        End Get
        Set(ByVal Value As Boolean)
            Me.p_contain = Value
        End Set
    End Property

    Public WriteOnly Property showReportData() As reportData
        Set(ByVal Value As reportData)
            Me.p_reportData = Value
        End Set
    End Property

    Public Property title() As String
        Get
            Return Me.p_title
        End Get
        Set(ByVal Value As String)
            Me.p_title = Value
        End Set
    End Property

    '************************************************************************************************
    '* CONSTRUCTOR 
    '************************************************************************************************
    Public Sub New(ByVal myPage As Page)
        Me.p_formatAsTextColumns = New ArrayList
        Me.testWithHTML = True
        Me.p_Datagrids = New SortedList
        Me.p_myPage = myPage
        Me.p_dg_id = 0
        Me.p_title = ""
    End Sub

    '************************************************************************************************
    '* formatColumnAsString 
    '************************************************************************************************
    Public Sub formatColumnAsString(ByVal columnID As Integer)
        Me.p_formatAsTextColumns.Add(columnID)
    End Sub

    '************************************************************************************************
    '* getUniqueID 
    '************************************************************************************************
    Private Function getUniqueID() As Integer
        Me.p_dg_id += 1
        Return (Me.p_dg_id)
    End Function

    '************************************************************************************************
    '* addDataGrid 
    '************************************************************************************************
    Public Sub addDataGrid(ByVal dg As C1.Web.C1WebGrid.C1WebGrid)
        Me.p_Datagrids.Add(Me.getUniqueID(), dg)
    End Sub

    '************************************************************************************************
    '* addDataGrid 
    '************************************************************************************************
    Public Sub addDataGrid(ByVal dg As DataGrid)
        Me.p_Datagrids.Add(Me.getUniqueID(), dg)
    End Sub

    '************************************************************************************************
    '* addLine 
    '************************************************************************************************
    Public Sub addLine(ByVal str As String)
        Me.p_Datagrids.Add(Me.getUniqueID, str)
    End Sub

    '************************************************************************************************
    '* export 
    '************************************************************************************************
    Public Sub export()
        'den cache ausschalten sonst ist immer das gleich excel drinnen.
        Try

       
        With p_myPage
                '    .Response.Cache.SetCacheability(HttpCacheability.NoCache)
                .Response.Cache.SetAllowResponseInBrowserHistory(True)
                .Response.Cache.SetExpires(Now())
                .Response.Clear()
                .Response.Charset = ""
                .Response.Buffer = True
                .Response.AppendHeader("Pragma", "public")
                .Response.AppendHeader("Cache-control", "must-revalidate")
                .Response.AddHeader("content-disposition", "attachment")
                .Response.ContentEncoding = System.Text.Encoding.GetEncoding(1252)
                .Response.ContentType = "application/vnd.ms-excel"
               

                ' .Response.AddHeader("content-disposition", "attachment; filename=c:\test.csv")
                ' .Response.AddHeader("content-disposition", "attachment; filename=" & FileName)


            'mime type auf excel setzen und angeben das download fester kommt.

        End With

        If Not Me.p_title = "" Then
            Me.print("<strong>" & Me.title & "</strong>")
            Me.print("<br><br><br>")
        End If

        If Not Me.p_reportData Is Nothing Then
            Me.drawReportHeader()
            Me.print("<br>")
        End If

        For Each item As Object In p_Datagrids.Values
            If TypeOf (item) Is DataGrid Or TypeOf (item) Is C1.Web.C1WebGrid.C1WebGrid Then
                If ContainsControls = True Then
                    Me.removeControls(item)
                End If
                Me.forceColumnAsText(item)
                If TypeOf (item) Is C1.Web.C1WebGrid.C1WebGrid Then
                    Me.exportDG(Me.webGridModifications(item))
                Else
                    Me.exportDG(item)
                End If
            Else
                Me.print(item)
            End If
        Next
        Catch ex As Exception
        Finally

            p_myPage.Response.End()

        End Try

    End Sub

    '************************************************************************************************
    '* removeControls - entfernt alle controls aus dem excel und stellt diese nach möglichkeit 
    '* dar. es werden "text"-properties genommen, etc. REKURSIV!
    '************************************************************************************************
    Private Sub removeControls(ByRef control As Control)
        For i As Integer = 0 To control.Controls.Count - 1
            Me.removeControls(control.Controls(i))
        Next

        If Not TypeOf control Is TableCell Then
            If Not control.GetType.GetProperty("SelectedItem") Is Nothing Then
                Dim literal As New LiteralControl
                control.Parent.Controls.Add(literal)
                literal.Text = control.GetType.GetProperty("SelectedItem").GetValue(control, Nothing)
                control.Parent.Controls.Remove(control)
            ElseIf Not control.GetType.GetProperty("Text") Is Nothing Then
                Dim literal As New LiteralControl
                control.Parent.Controls.Add(literal)
                literal.Text = control.GetType.GetProperty("Text").GetValue(control, Nothing)
                control.Parent.Controls.Remove(control)
            ElseIf Not control.GetType.GetProperty("Checked") Is Nothing Then
                Dim literal As New LiteralControl
                control.Parent.Controls.Add(literal)
                literal.Text = control.GetType.GetProperty("Checked").GetValue(control, Nothing)
                control.Parent.Controls.Remove(control)
            End If
        End If
    End Sub


    '************************************************************************************************
    '* removeControls - entfernt alle controls aus dem excel und stellt diese nach möglichkeit 
    '* dar. es werden "text"-properties genommen, etc. REKURSIV!
    '************************************************************************************************
    Private Sub removeGroupHeaders()

    End Sub

    '************************************************************************************************
    '* forceColumnAsText - alle nötigen spalten als text formatieren 
    '************************************************************************************************
    Private Sub forceColumnAsText(ByRef dg As Object)
        For Each row As Object In dg.items
            For i As Integer = 0 To Me.p_formatAsTextColumns.Count - 1
                row.Cells(Me.p_formatAsTextColumns.Item(i)).Style.Add("mso-number-format", "\@")
            Next
        Next
    End Sub

    '************************************************************************************************
    '* webGridModifications  - alle gruppierungen werden geöffnet vorm export (c1.webgrid)
    '************************************************************************************************
    Private Function webGridModifications(ByRef dg As C1.Web.C1WebGrid.C1WebGrid) As C1.Web.C1WebGrid.C1WebGrid

        If Me.ExportHeaders = False Then
            dg.ShowHeader = False
            dg.ShowFooter = False
        End If

        For Each col As C1.Web.C1WebGrid.C1Column In dg.Columns

            col.GroupInfo.OutlineMode = C1.Web.C1WebGrid.OutlineModeEnum.None

            If Me.ExportGroupHeaders = False Then
                col.GroupInfo.Position = C1.Web.C1WebGrid.GroupPositionEnum.None
            End If



        Next
        Return dg
    End Function

    '************************************************************************************************
    '* drawReportHeader 
    '************************************************************************************************
    Private Sub drawReportHeader()
        Dim stringWrite As New System.IO.StringWriter
        Dim htmlWrite As New System.Web.UI.HtmlTextWriter(stringWrite)
        Me.p_reportData.RenderControl(htmlWrite)
        Me.print(stringWrite.ToString)
    End Sub

    '************************************************************************************************
    '* print 
    '************************************************************************************************
    Private Sub print(ByVal str As String)
        Me.p_myPage.Response.Write(str)

        ' Me.p_myPage.Response.Clear()
    End Sub

    '************************************************************************************************
    '* export 
    '************************************************************************************************
    Private Sub exportDG(ByRef dg As Object)
        'das object in ein datagrid-object umwandeln
        Dim dgC1 As C1.Web.C1WebGrid.C1WebGrid


        If TypeOf (dg) Is C1.Web.C1WebGrid.C1WebGrid Then
            dg = CType(dg, C1.Web.C1WebGrid.C1WebGrid)

        Else
            dg = CType(dg, DataGrid)
        End If



        dg.GridLines = GridLines.None
        dg.HeaderStyle.Font.Bold = True


        Dim stringWrite As New System.IO.StringWriter
        Dim htmlWrite As New System.Web.UI.HtmlTextWriter(stringWrite)

        dg.RenderControl(htmlWrite)
        Me.print(stringWrite.ToString())
    End Sub

    Public Property ExportGroupHeaders() As Boolean
        Get
            Return p_exportGroupHeaders
        End Get
        Set(ByVal Value As Boolean)
            p_exportGroupHeaders = Value
        End Set
    End Property

    Public Property ExportHeaders() As Boolean
        Get
            Return p_exportHeaders
        End Get
        Set(ByVal Value As Boolean)
            p_exportHeaders = Value
        End Set
    End Property
End Class