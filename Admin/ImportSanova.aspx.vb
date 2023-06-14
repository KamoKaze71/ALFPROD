Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities
Imports System
Imports System.IO
Imports System.Collections
Imports Wyeth.Alf.WyethImport



Public Class ImportSanova
	Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dddistribSelect As System.Web.UI.WebControls.DropDownList
	Protected WithEvents File1 As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents rbl_AutomaticUpdate As System.Web.UI.WebControls.RadioButtonList
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
	Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents rbl_automatic_mview_refresh As System.Web.UI.WebControls.RadioButtonList
    Protected WithEvents Button_KD As System.Web.UI.WebControls.Button
    Protected WithEvents Button_ART As System.Web.UI.WebControls.Button
    Protected WithEvents Button_BW As System.Web.UI.WebControls.Button
    Protected WithEvents Button_MView As System.Web.UI.WebControls.Button
    Protected WithEvents Button_delete_transmission As System.Web.UI.WebControls.Button
    Protected WithEvents ddTransmissions As System.Web.UI.WebControls.DropDownList
    Protected WithEvents LabelOut As System.Web.UI.WebControls.Label
    Protected WithEvents txtPath As System.Web.UI.WebControls.TextBox
    Protected WithEvents btn_manual_import As System.Web.UI.WebControls.Button

	'NOTE: The following placeholder declaration is required by the Web Form Designer.
	'Do not delete or move it.
	Private designerPlaceholderDeclaration As System.Object

	Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
		'CODEGEN: This method call is required by the Web Form Designer
		'Do not modify it using the code editor.
		InitializeComponent()
	End Sub

#End Region
    Dim MyStock As New Stock
    Dim MyImport As New WyethImport
	Dim MyDs As New DataSet
    Dim MyAdapter As New OracleDataAdapter
	Dim MyDataView As New DataView
    Dim MyCollection As New Hashtable
    Dim str_output As String
    Dim str_error As String
    Dim i_result As Integer

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

		Dim myReader As OracleDataReader
		Dim MyCmd As New OracleCommand
		Dim conn As New MyConnection

		Try
            If Page.IsPostBack = True Then

                ' because the Hashtable does not persit its values during postback - read in again the Application Variables
                MyCmd.Parameters.Clear()
                MyCmd.CommandType = CommandType.StoredProcedure
                MyCmd.CommandText = "PKG_APPLICATION.GetAppVars"
                MyCmd.Connection = conn.Open()
                MyCmd.Parameters.Add("AppVars", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                myReader = MyCmd.ExecuteReader()

                While myReader.Read
                    MyCollection.Add(myReader("apse_variable"), myReader("apse_value"))
                End While

                BindData()
            Else
                lblPageTitle.Text = Request.QueryString("pageTitle")
                BindData()
            End If
        Catch ex As Exception

			ExceptionInfo.Show(ex)
		Finally
			conn.Close()

		End Try
	End Sub
	Private Sub BindData()

		If rbl_AutomaticUpdate.SelectedValue = "automatic" Then

			Button_KD.Enabled = False
			Button_ART.Enabled = False
            Button_BW.Enabled = False

		End If
		'Set the Country Id for this Page
		MyStock.StockCtryID = Session("country_id")

		'lists all available Distributors
		dddistribSelect.DataSource = MyStock.GetDisributors()
		dddistribSelect.DataValueField = "dist_id"
		dddistribSelect.DataTextField = "dist_name"
		dddistribSelect.DataBind()

        ' list all available transmissions for the selected Distributor
        ddTransmissions.DataSource = GetTransmissions()
        ddTransmissions.DataValueField = "tran_id"
        ddTransmissions.DataTextField = "tran_date"
        ddTransmissions.DataBind()


		' reads in the Application Variables needed in update process
		Dim myReader As OracleDataReader
		Dim MyCmd As New OracleCommand
		Dim conn As New MyConnection
		Try

		
		MyCmd.Parameters.Clear()
		MyCmd.CommandType = CommandType.StoredProcedure
		MyCmd.CommandText = "PKG_APPLICATION.GetAppVars"
		MyCmd.Connection = conn.Open()
		MyCmd.Parameters.Add("AppVars", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
		myReader = MyCmd.ExecuteReader()

		While myReader.Read
			MyCollection.Add(myReader("apse_variable"), myReader("apse_value"))
		End While
		txtPath.Text = MyCollection("SanovaImportFilePath")
		Catch ex As Exception
		Finally
			conn.Close()
		End Try



	End Sub
    Private Function GetTransmissions() As DataView
        Dim MyCmd As New OracleCommand
        Dim conn As New MyConnection
        Try
            MyCmd.CommandText = "PKG_APPLICATION.GetTransmissions"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Add("transmissions", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_dist_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = dddistribSelect.SelectedValue
            MyCmd.Connection = conn.Open()

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Codes")
            MyDataView = MyDs.Tables("codes").DefaultView

            Return MyDataView
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try
    End Function

    Private Sub Button_Import_Sanova_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim MyCmd As New OracleCommand
        Dim MyConn As New MyConnection
        Dim val As Object
        Dim x As Integer
        Try


            ' first Load Data with the sql loader into temp tables
            LabelOut.Text = ""

            x = MyImport.GetFtpFilesService()

            LabelOut.Text = MyImport.strLog()
            MyImport.strLog = ""

        Catch ex As Exception
            LabelOut.Text = LabelOut.Text & "<bR> Error while updating Alf with Sanova Data"
            ArchiveFiles(MyCollection("SanovaImportFilePath"), MyCollection("SanovaImportFileArchivePath"))
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()
        End Try

    End Sub



    Private Sub Button_KD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_KD.Click
        Dim MyConn As New MyConnection
        Dim val As Object
        Dim MyCmd As New OracleCommand
        Try


            MyCmd.Parameters.Clear()
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.CommandText = "PKG_IMPORT_SANOVA.F_Customers"
            MyCmd.Parameters.Add("v_result", OracleDbType.Varchar2, 2000, val, ParameterDirection.ReturnValue)
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Connection = MyConn.Open
            val = MyCmd.ExecuteScalar()
            val = MyCmd.Parameters(0).Value
            LabelOut.Text = LabelOut.Text & "Updating Customers Table...<bR>"
            LabelOut.Text = LabelOut.Text & val.ToString & "<br>"

        Catch ex As Exception
            LabelOut.Text = LabelOut.Text & ex.Message.ToString
            ExceptionInfo.Show(ex)
        Finally

            MyConn.Close()

        End Try

    End Sub

    Private Sub Button_ART_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_ART.Click
        Dim MyConn As New MyConnection
        Dim val As Object
        Dim MyCmd As New OracleCommand
        Try
            MyCmd.Parameters.Clear()
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Add("v_result", OracleDbType.Varchar2, 2000, val, ParameterDirection.ReturnValue)
            MyCmd.Connection = MyConn.Open
            MyCmd.CommandText = "PKG_IMPORT_SANOVA.F_Products"
            val = MyCmd.ExecuteScalar()
            val = MyCmd.Parameters(0).Value
            LabelOut.Text = LabelOut.Text & "Updating Products Table....<br>"
            LabelOut.Text = LabelOut.Text & val.ToString & "<br>"

        Catch ex As Exception
            LabelOut.Text = LabelOut.Text & ex.Message.ToString
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()

        End Try
    End Sub

    Private Sub Button_BW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_BW.Click
        ' update MasterTables in ALF with upadte & insert Stored Procedures
        Dim MyConn As New MyConnection
        Dim val As Object
        Dim MyCmd As New OracleCommand
        Try
            MyCmd.Parameters.Clear()
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Add("v_result", OracleDbType.Varchar2, 2000, val, ParameterDirection.ReturnValue)
            MyCmd.Connection = MyConn.Open
            MyCmd.CommandText = "PKG_IMPORT_SANOVA.F_BW"
            val = MyCmd.ExecuteScalar()
            val = MyCmd.Parameters(0).Value
            LabelOut.Text = LabelOut.Text & "Updating Orders Stock & Inventory Level Tables.. <bR>"
            LabelOut.Text = LabelOut.Text & val.ToString & "<br>"


        Catch ex As Exception
            LabelOut.Text = LabelOut.Text & ex.Message.ToString
            ExceptionInfo.Show(ex)
        Finally
            MyConn.Close()

        End Try

    End Sub

    Private Sub rbl_AutomaticUpdate_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbl_AutomaticUpdate.SelectedIndexChanged
        If rbl_AutomaticUpdate.SelectedValue.ToUpper = "Manual".ToUpper Then
            Button_KD.Enabled = True
            Button_ART.Enabled = True
            Button_BW.Enabled = True
            Button_MView.Enabled = True
        Else
            Button_KD.Enabled = False
            Button_ART.Enabled = False
            Button_BW.Enabled = False
            Button_MView.Enabled = False
        End If
    End Sub

    Private Sub Button_delete_transmission_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_delete_transmission.Click
        If MyImport.DeleteTransmission(CInt(ddTransmissions.SelectedValue)) Then
            LabelOut.Text = LabelOut.Text & "Transmission successfully deleted!" & "<br>"
        Else
            LabelOut.Text = LabelOut.Text & "Error while deleteting transmission" & "<br>"
        End If
    End Sub

    Private Sub Button_MView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_MView.Click
        If MyImport.RefreshMVs = True Then
            LabelOut.Text = LabelOut.Text + "All MVs have been refreshed"
        Else
            LabelOut.Text = LabelOut.Text + "Error ocurred while rfreshing MVs"
        End If

    End Sub

End Class
