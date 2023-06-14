Imports Wyeth.Alf.JSPopUp
Imports Wyeth.Alf
Imports Wyeth.Utilities
Imports Oracle.DataAccess.Client




Public Class DeleteLogs
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Button_DeleteLogs As System.Web.UI.WebControls.Button
    Protected WithEvents txtStartDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents txtEndDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents lblOut As System.Web.UI.WebControls.Label
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents MyDropDown As System.Web.UI.WebControls.DropDownList
    Protected WithEvents imgstartkal As System.Web.UI.HtmlControls.HtmlImage
    Protected WithEvents imgendkal As System.Web.UI.HtmlControls.HtmlImage

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim myData As New Wyeth.Alf.DataAccessBaseClass
    Dim MyLog As New Log
    Dim logs_code_id As Integer
    Dim logs_code, min_log_date As String
    Dim MyJS As New JSPopUp(Me)


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        If Page.IsPostBack = True Then
            logs_code_id = Me.ViewState("logs_code_id")
            logs_code = Me.ViewState("logs_code")
        Else
            MyJS.Title = "DatePicker"
            MyJS.PageURL = "../../util/datepicker.aspx"
            MyJS.AddDatePopupToControl(txtStartDate, imgstartkal)
            MyJS.AddDatePopupToControl(txtEndDate, imgendkal)

            Me.ALFPageTitle = "Delete Logs"
            logs_code_id = Request.QueryString("logs_code_id")
            logs_code = Request.QueryString("logs_code")
            Me.ViewState.Add("logs_code_id", logs_code_id)
            Me.ViewState.Add("logs_code", logs_code)
            Dim myParam(2) As OracleParameter

            Dim param0 As New OracleParameter
            param0.Direction = ParameterDirection.ReturnValue
            param0.OracleDbType = OracleDbType.Varchar2
            param0.Size = 2000
            myParam(0) = param0

            Dim param1 As New OracleParameter
            param1.Direction = ParameterDirection.Input
            param1.OracleDbType = OracleDbType.Int32
            param1.Value = logs_code_id
            myParam(1) = param1

            Dim param2 As New OracleParameter
            param2.Direction = ParameterDirection.Input
            param2.OracleDbType = OracleDbType.Int32
            param2.Value = Session("country_id")
            myParam(2) = param2
            min_log_date = myData.executeScalar("PKG_APPLICATION.GetMinLogEntry", myParam)
            If min_log_date = "" Then
                Me.lblOut.Text = "No " & logs_code & "Log in ALF"
                Me.lblOut.CssClass = "success"
                min_log_date = "2003-01-01"
            End If

            txtStartDate.Text = min_log_date
            txtEndDate.Text = Today().ToString(Wyeth.Utilities.Helper.DATEFORMAT_STRING_REPORT, CType(Application("MyDTFI"), IFormatProvider))

        End If


        Me.Button_DeleteLogs.Text = "Delete " & logs_code & " Log"
    End Sub

    Private Sub Button_DeleteLogs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_DeleteLogs.Click
        Page.Validate()
        If Page.IsValid = True Then
            If MyLog.DeleteLogs(Session("country_id"), logs_code_id, txtStartDate.Text, txtEndDate.Text) Then
                lblOut.Text = "sucessfully deleted " & logs_code & " Log"
                lblOut.CssClass = "success"
            Else
                lblOut.Text = "Error: could not delete " & logs_code & " Log"
                lblOut.CssClass = "success"
        End If
        End If

    End Sub

End Class
