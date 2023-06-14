'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
'''         <revision>23</revision>
''' <summary><para>Provides some Helper functions for DataAccess</para></summary>
Public Class DataAccessHelper
    Public Shared Function SelectDistinct(ByVal TableName As String, ByVal SourceTable As DataTable, ByVal KeyFieldName As String, ByVal ValueFieldName As String) As DataTable
        Dim dt As New DataTable(TableName)
        dt.Columns.Add(KeyFieldName, SourceTable.Columns(KeyFieldName).DataType)
        dt.Columns.Add(ValueFieldName, SourceTable.Columns(ValueFieldName).DataType)
        Dim dr As DataRow, LastValue, LastValueValue As Object
        For Each dr In SourceTable.Select("", KeyFieldName)
            If LastValue Is Nothing OrElse Not ColumnEqual(LastValue, dr(KeyFieldName)) Then
                LastValue = dr(KeyFieldName)
                LastValueValue = dr(ValueFieldName)
                dt.Rows.Add(New Object() {LastValue, LastValueValue})
            End If
        Next
        'If Not ds Is Nothing Then ds.Tables.Add(dt)
        Return dt
    End Function
    Private Shared Function ColumnEqual(ByVal A As Object, ByVal B As Object) As Boolean
        '
        ' Compares two values to determine if they are equal. Also compares DBNULL.Value.
        '
        ' NOTE: If your DataTable contains object fields, you must extend this
        ' function to handle the fields in a meaningful way if you intend to group on them.
        '
        If A Is DBNull.Value And B Is DBNull.Value Then Return True ' Both are DBNull.Value.
        If A Is DBNull.Value Or B Is DBNull.Value Then Return False ' Only one is DBNull.Value.
        Return A = B                                                ' Value type standard comparison
    End Function

End Class
