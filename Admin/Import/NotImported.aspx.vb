Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities
Imports Wyeth.Alf.CssStyles
Imports C1.Web.C1WebGrid

Public Class NotImportedKD
    Inherits Wyeth.Alf.AlfPage

    Dim myData As New TempTables
    Dim myJsPopup As New JSPopUp(Me)
    Protected WithEvents MYgRID As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents Button_delete As System.Web.UI.WebControls.Button
    Protected WithEvents FilterPanel As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents btn_cancel As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents DFGDSF As C1.Web.C1WebGrid.C1WebGrid


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

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		'Put user code to initialize the page here

		If Page.IsPostBack Then

        Else
            Dim file_type As String = Request.QueryString("file_type")
           

            Me.ViewState.Add("file_type", file_type)

            BindData(file_type)

        End If

	End Sub
    Private Sub BindData(ByVal file_type As String)

        Try

            Dim MyDataView As New DataView

           

            Select Case file_type
                Case "KD"
                    Me.ALFPageTitle = "ALF Customer temp Table"
                    MyDataView = myData.GetKD()
                Case "BW"
                    Me.ALFPageTitle = "ALF Orders temp Table"
                    MyDataView = myData.GetBW()
                Case "ART"
                    Me.ALFPageTitle = "ALF Products temp Table"
                    MyDataView = myData.GetART()
            End Select

            If MyDataView.Count > 0 Then
                Me.Button_delete.Text = "Delete " & Me.ALFPageTitle.ToString
                myJsPopup.ConfirmMessage = "This will delete all records in " & Me.ALFPageTitle.ToString & "! Are you sure you want to do that?"
                myJsPopup.AddGetConfirm(Button_delete)
                SetGridStyles(MYgRID)
                MYgRID.Visible = True
                MYgRID.AutoGenerateColumns = True
                MYgRID.ShowHeader = True
                MYgRID.DataSource = MyDataView
                MYgRID.DataBind()
                Me.Controls.Add(MYgRID)
            Else
                MYgRID.Visible = False
                Dim mylblOut As New Label
                Me.Button_delete.Visible = False
                mylblOut.Text = "No records found in " & Me.ALFPageTitle.ToString
                mylblOut.CssClass = "success"
                Me.Controls.Add(mylblOut)
            End If


        Catch ex As Exception
            ExceptionInfo.Show(ex)

        End Try

    End Sub

    Private Sub Button_delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_delete.Click
        Dim file_type As String = Me.ViewState("file_type")
        Dim ret_val As Boolean
        Dim lblOut As New Label
        lblOut.CssClass = "success"

        Select Case file_type
            Case "KD"
                ret_val = myData.DeleteKD()
            Case "BW"
                ret_val = myData.DeleteBW()
            Case "ART"
                ret_val = myData.DeleteART()
        End Select

        If ret_val Then
            lblOut.Text = "sucessfully deleted all records"
        Else
            lblOut.Text = "Error ocurred while deleting the records"
        End If
        Me.Controls.Add(lblOut)

    End Sub

    Private Sub btn_cancel_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cancel.ServerClick
        myJsPopup.ClosePopUp()
    End Sub
End Class
