Imports System.Web.UI.WebControls
Imports System.Web.UI

'***************************************************************************************************************
'* WYETHDATAGRID - 19.01.2004 - MG
'* soll eine tabelle erstellen, welche automatisch einige features enthält. excel export, etc.
'***************************************************************************************************************

Public Class wyethDataGrid
    Inherits DataGrid

    Public Wyeth_SortByFirstColumn As Boolean    'Soll zu beginn nach der ersten SICHTBAREN spalte sortiert werden
    Public Wyeth_FilterColumns As New FilterColumns

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
    '* placeFilters 
    '***************************************************************************************************************
    Private Sub drawFilterBar()
        Dim dgItem As New DataGridItem(0, 0, ListItemType.Header)

        Dim dgCell As TableCell
        For Each col As Wyeth_FilterColumn In Me.Wyeth_FilterColumns
            dgCell = New TableCell
            dgCell.Text = col.DatafieldNameOfColumnToBound
            dgCell.ColumnSpan = 2
            dgItem.Cells.Add(dgCell)
        Next

        Dim table As New table
        Dim row As New TableRow
        Dim cell As New TableCell
        cell.Text = "TEST"
        row.Cells.Add(cell)
        table.Rows.Add(row)



        Me.Controls(0).Controls.AddAt(0, dgItem)

        'Dim dgItem As DataGridItem
        'Dim dgCell As TableCell
        'dgItem = New DataGridItem(0, 0, ListItemType.Header)
        'dgCell = New TableCell
        'dgCell.ColumnSpan = 2
        'dgItem.Cells.Add(dgCell)
        'dgCell.Text = "List of Products"
        'Me.Controls(0).Controls.AddAt(0, dgItem)

    End Sub


    '===============
    '[ E V E N T S ]
    '===============


    '***************************************************************************************************************
    '* onLoad 
    '***************************************************************************************************************
    Protected Overrides Sub onLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)

        'wir setzen alle sort_expressions automatisch und sortieren
        'wenn gewünscht nach der ersten Spalte.
        If Not Page.IsPostBack Then
            Dim seted As Boolean = False
            For Each col As BoundColumn In Me.Columns
                If col.Visible = True Then
                    col.SortExpression = col.DataField.ToString

                    If Me.Wyeth_SortByFirstColumn And Not seted Then
                        Me.sortColumn(col.SortExpression.ToString)
                        seted = True
                    End If
                End If
            Next
            Me.drawFilterBar()
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
End Class



Public Class FilterColumns
    Inherits CollectionBase

    Default Public Property Item(ByVal Index As Integer) As Wyeth_FilterColumn
        Get
            Return CType(Me.List.Item(Index), Wyeth_FilterColumn)
        End Get
        Set(ByVal Value As Wyeth_FilterColumn)
            Me.List.Item(Index) = Value
        End Set
    End Property

    Public Function Add(ByVal Item As Wyeth_FilterColumn) As Integer
        Return Me.List.Add(Item)
    End Function
End Class


Public Enum filterColumnTypes
    dropdown = 1
End Enum

Public Class Wyeth_FilterColumn
    Public filterType As filterColumnTypes
    Public DatafieldNameOfColumnToBound As String
End Class
