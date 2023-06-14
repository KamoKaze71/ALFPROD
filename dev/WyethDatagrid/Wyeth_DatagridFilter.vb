Imports System.Web.UI.WebControls

Public Enum filterColumnTypes
    dropdown = 1
    textfield = 2
End Enum

Public Class Wyeth_DatagridFilter
    Public filterType As filterColumnTypes
    Public selectedValue As String

    Private Const TXT_PLEASE_SELECT_FILTER As String = "- Select filter -"
    Private p_dropdownValues As Array

    Public Sub New(ByVal filterType As filterColumnTypes)
        Me.filterType = filterType
    End Sub

    Public Sub New(ByVal filterType As filterColumnTypes, ByVal defaultSelectedValue As String)
        Me.New(filterType)
        Me.selectedValue = defaultSelectedValue
    End Sub

    Public WriteOnly Property dropdownValues() As Array
        Set(ByVal Value As Array)
            Me.p_dropdownValues = Value
        End Set
    End Property

    '**************************************************************************************************
    '* draw 
    '**************************************************************************************************
    Public Function draw() As WebControl
        Select Case Me.filterType
            Case filterColumnTypes.textfield
                Dim lbl As TextBox = New TextBox
                With lbl
                    .Text = Me.selectedValue
                End With
                Return lbl

            Case filterColumnTypes.dropdown
                Dim dd As DropDownList = New DropDownList
                With dd
                    'add the item "please select filter"
                    .Items.Add(New ListItem(Me.TXT_PLEASE_SELECT_FILTER, "0"))
                    'and now add all available values for this dropdown
                    If Not Me.p_dropdownValues Is Nothing Then
                        For i As Integer = 0 To Me.p_dropdownValues.Length - 1
                            .Items.Add(New ListItem(Me.p_dropdownValues(i), Me.p_dropdownValues(i)))
                        Next
                    End If
                    .AutoPostBack = True
                End With
                Return dd
        End Select
    End Function
End Class