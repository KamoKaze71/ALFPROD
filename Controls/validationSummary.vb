'************************************************************************************************
'* Klasse um die validation-summary einheitliche zu gestalten. 
'************************************************************************************************

Public Class validationSummary
    Inherits System.Web.UI.WebControls.ValidationSummary

    Public Sub New()
        Me.CssClass = "nosuccess"
        Me.HeaderText = "The following error(s) occured:"
        Me.ToolTip = "The following error(s) occured ..."
        Me.DisplayMode = ValidationSummaryDisplayMode.BulletList
    End Sub

End Class