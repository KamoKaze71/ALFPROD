Imports System.Web.UI.WebControls
Imports System.Web.UI

'***************************************************************************************************************
'* WYETHDATAGRID - 19.01.2004 - MG
'* soll eine tabelle erstellen, welche automatisch einige features enthält. excel export, etc.
'***************************************************************************************************************

Public Class WyethDatagrid
    Inherits DataGrid

    Public Wyeth_SortByFirstColumn As Boolean    'Soll zu beginn nach der ersten SICHTBAREN spalte sortiert werden
    Public Wyeth_ShowFilterBar As Boolean

    '=========================
    '[ C O N S T R U C T O R ]
    '=========================

    Public Sub New()
        MyBase.New()
        Me.EnableViewState = True
        Me.AllowSorting = True
        Me.AutoGenerateColumns = False
        Me.currentSort = ""
        Me.Wyeth_SortByFirstColumn = True
        Me.Wyeth_ShowFilterBar = True
    End Sub


    '=======================
    '[ P R O P E R T I E S ]
    '=======================

    Public ReadOnly Property Wyeth_Source() As DataView
        Get
            Return Me.DataSource()
        End Get
    End Property


    'nach welchen feldern ist gerade sortiert.
    Private Property currentSort() As String
        Get
            Return Me.Attributes.Item("currentSort").ToString
        End Get
        Set(ByVal Value As String)
            Me.Attributes.Add("currentSort", Value)
        End Set
    End Property


    '=================
    '[ M E T H O D S ]
    '=================

    '***************************************************************************************************************
    '* sortColumn 
    '***************************************************************************************************************
    Private Sub sortColumn(ByVal sortExpression As String)
        If Me.AllowSorting Then
            'wenn schon nach der spalte sortiert wurde dann in die andere richtung sortieren
            If Me.currentSort = sortExpression Then

                Me.Wyeth_Source.Sort = sortExpression & " DESC"
            Else
                Me.Wyeth_Source.Sort = sortExpression
            End If
            Me.currentSort = Me.Wyeth_Source.Sort.ToString
        End If
    End Sub

    '***************************************************************************************************************
    '* drawFilterBar 
    '***************************************************************************************************************
    Private Sub drawFilterBar()
        If Me.Wyeth_ShowFilterBar Then
            Dim dgitem As New DataGridItem(1, 1, ListItemType.Header)
            Dim dgcell As TableCell
            Dim numberOfFilters As Integer = 0

            'we loop through all needed filters
            For Each col As Wyeth_BoundColumn In Me.Columns
                dgcell = New TableCell
                If Not col.Wyeth_Filter Is Nothing Then
                    Dim ctrl As WebControl = col.Wyeth_Filter.draw()
                    If Not ctrl Is Nothing Then
                        dgcell.Controls.Add(ctrl)
                    End If
                    numberOfFilters += 1
                End If
                dgitem.Cells.Add(dgcell)
            Next

            'add the filter-bar to the grid
            Me.Controls(0).Controls.AddAt(1, dgitem)
        End If
    End Sub

    '***************************************************************************************************************
    '* onLoad 
    '***************************************************************************************************************
    Protected Overrides Sub onLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)

        'wir setzen alle sort_expressions automatisch und sortieren
        'wenn gewünscht nach der ersten Spalte.
        If Not Page.IsPostBack Then
            Dim seted As Boolean = False
            For Each col As Wyeth_BoundColumn In Me.Columns
                If col.Visible = True Then
                    col.SortExpression = col.DataField.ToString

                    If Me.Wyeth_SortByFirstColumn And Not seted Then
                        Me.sortColumn(col.SortExpression.ToString)
                        seted = True
                    End If
                End If
            Next

            Me.DataBind()
        End If
    End Sub

    '***************************************************************************************************************
    '*  sort-event 
    '***************************************************************************************************************
    Private Sub me_sortCommand(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs) Handles MyBase.SortCommand
        Me.sortColumn(e.SortExpression.ToString)
        Me.DataBind()
    End Sub

    '***************************************************************************************************************
    '* OnItemCreated 
    '***************************************************************************************************************
    Protected Overrides Sub OnItemCreated(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        MyBase.OnItemCreated(e)

        If e.Item.ItemType = ListItemType.Footer Then
            drawFilterBar()
        End If
    End Sub

    Protected Overrides Sub OnDataBinding(ByVal e As System.EventArgs)
        MyBase.OnDataBinding(e)

        Dim dv As DataView
        dv = Me.Wyeth_Source

        For Each col As Wyeth_BoundColumn In Me.Columns
            If Not col.Wyeth_Filter Is Nothing AndAlso col.Wyeth_Filter.selectedValue <> "" Then
                'dv.RowFilter = col.DataField.ToString & "='" & col.Wyeth_Filter.selectedValue & "'"
            End If
        Next

        dv.RowFilter = "ctry_id='68'"
    End Sub
End Class