Imports Wyeth.Alf.WyethDropdown
Imports Wyeth.Alf.CssStyles

Public Class ProcessingStatus
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents ddyear As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim mymep As New MEPData

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        binddata()
    End Sub

    Private Sub binddata()
        GetYearDD4(ddyear, 2004, 0)
        ddyear.SelectedValue = getYear()
        'MyGrid.DataSource = mymep.GetV_PMForYear(ddyear.SelectedValue, Session("country_id"))
        MyGrid.DataSource = mymep.GetV_PMForYear(getYear(), Session("country_id"))
        MyGrid.DataBind()
        SetGridStyles(MyGrid)
    End Sub

    Private Function getYear() As String
        Dim yearQS As String = Request.QueryString("year")
        If yearQS <> "" Then
            Return yearQS
        Else
            Return Year(Now())
        End If
    End Function

    Private Sub MyGrid_Databound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound
        Dim myimage As New System.Web.UI.WebControls.Image

        For Each cell As TableCell In e.Item.Cells
            If cell.Text = "1" Then
                cell.ToolTip = "Processed"
                cell.Text = "<Img src='../images/image_true.gif'>"
            ElseIf cell.Text = "0" Then
                cell.ToolTip = "Not Processed"
                cell.Text = "<Img src='../images/image_false.gif'>"
            ElseIf cell.Text.Trim.StartsWith("at") Then
                cell.Text = ""
            End If
        Next

    End Sub

    Private Sub ddyear_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddyear.PreRender
        ddyear.Attributes.Add("onchange", "location.href='processingStatus.aspx?year=' + this.value;")
    End Sub
End Class
