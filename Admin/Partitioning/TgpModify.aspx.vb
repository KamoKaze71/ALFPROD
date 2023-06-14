'******************************************************************************************
'* WYETH - Michal Gabrukiewicz 
'* 10.03.2003
'******************************************************************************************
Imports Wyeth.Alf.WyethDropdown

Public Class TgpModify
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents tpgDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents labelDateLastChanged As System.Web.UI.WebControls.Label
    Protected WithEvents buttonSave As System.Web.UI.WebControls.Button
    Protected WithEvents reqValid As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents errorDesc As System.Web.UI.WebControls.Label
    Protected WithEvents pageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents dateLabel As System.Web.UI.WebControls.Label
    Protected WithEvents ddTPGType As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private dataAccessTPG As New TargetProductGroup

    Private ReadOnly Property isNew() As Boolean
        Get
            If Request.QueryString.Item("id") <> "" Then
                Return False
            Else
                Return True
            End If
        End Get
    End Property

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dataAccessTPG.userID = Session.Item("user_id")

        If Not Me.isNew Then
            dataAccessTPG.targetProductGroupID = Request.QueryString.Item("id")
        End If

        If Not Page.IsPostBack Then
            GetDDTPGTypes(ddTPGType, Session("country_id"))
            initPage()
            fillTPGData()
        End If
    End Sub

    Private Sub initPage()
        If Me.isNew Then
            dateLabel.Visible = False
            pageTitle.Text = "Create Target-Product-Group"
        Else
            dateLabel.Visible = True
            pageTitle.Text = "Modify Target-Product-Group"
        End If
    End Sub

    Private Sub fillTPGData()
        If Not Me.isNew Then
            Dim dv As DataView = dataAccessTPG.getTPGByID(CInt(Request.QueryString.Item("id")))
            labelDateLastChanged.Text = dv.Item(0).Item(3)
            tpgDescription.Text = dv.Item(0).Item(1)
            ddTPGType.SelectedValue = dv.Item(0).Item("code_id")
        End If
    End Sub

    Private Sub buttonSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonSave.Click
        If Not tpgDescription.Text = "" Then
            saveData()
        Else
            errorDesc.Text = "Description cannot be empty."
            errorDesc.Visible = True
        End If
    End Sub

    Private Sub saveData()
        Dim err As Boolean = False
        Dim newID As Integer

        Try
            If Me.isNew Then
                newID = dataAccessTPG.AddTPG(tpgDescription.Text, ddTPGType.SelectedValue)
            Else
                dataAccessTPG.renameTPG(tpgDescription.Text, ddTPGType.SelectedValue)
            End If
        Catch ex As Exception
            err = True
            errorDesc.Visible = True
            errorDesc.Text = "Database update failed. Contact Admin!"
        End Try

        If Not err Then
            If isNew Then
                'not the best solution because page-title could be another => titles are stored in Database
                PartitioningHelp.closePopupAndLoadUrlInParent("Partitioning.aspx?pageTitle=Partitioning&id=" & newID, Me)
            Else
                PartitioningHelp.closePopupAndRefreshParent(Me)
            End If
        End If
    End Sub

End Class
