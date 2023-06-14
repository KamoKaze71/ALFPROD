Imports System.Web.UI.WebControls
Imports System.Web.UI

Public Class DataGridTemplate

	Implements ITemplate
	Dim templateType As ListItemType
	Dim columnName As String

	Sub New(ByVal type As ListItemType, ByVal ColName As String)
		templateType = type
		columnName = ColName
	End Sub

	Sub InstantiateIn(ByVal container As Control) Implements ITemplate.InstantiateIn
		Dim lc As New Literal
		Select Case templateType
			Case ListItemType.Header
				lc.Text = "<B>" & columnName & "</B>"
				container.Controls.Add(lc)
			Case ListItemType.Item
				lc.Text = "Item " & columnName
				container.Controls.Add(lc)
			Case ListItemType.EditItem
				Dim tb As New TextBox
				tb.Text = ""
				container.Controls.Add(tb)
			Case ListItemType.Footer
				lc.Text = "<I>Footer</I>"
				container.Controls.Add(lc)
		End Select
	End Sub


End Class
