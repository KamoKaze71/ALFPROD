Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities
Imports System
Imports System.IO
Imports System.Collections



Public Class import
	Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

	End Sub
	Protected WithEvents Button2 As System.Web.UI.WebControls.Button
	Protected WithEvents ddTransmissions As System.Web.UI.WebControls.DropDownList
	Protected WithEvents ddimportDay As System.Web.UI.WebControls.DropDownList
	Protected WithEvents dddistribSelect As System.Web.UI.WebControls.DropDownList
	Protected WithEvents File1 As System.Web.UI.HtmlControls.HtmlInputFile
	Protected WithEvents txtPath As System.Web.UI.WebControls.TextBox
	Protected WithEvents Button_Import_Sanova As System.Web.UI.WebControls.Button
	Protected WithEvents rbl_AutomaticUpdate As System.Web.UI.WebControls.RadioButtonList
	Protected WithEvents LabelOUT As System.Web.UI.WebControls.Label
	Protected WithEvents Button_KD As System.Web.UI.WebControls.Button
	Protected WithEvents Button_ART As System.Web.UI.WebControls.Button
	Protected WithEvents Button_BW As System.Web.UI.WebControls.Button
	Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
	Protected WithEvents Label1 As System.Web.UI.WebControls.Label

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
	Dim MyDs As New DataSet
	Dim MyCmd As New OracleCommand
	Dim MyAdapter As New OracleDataAdapter
	Dim MyDataView As New DataView
	Dim conn As New MyConnection
	Dim MyCollection As New Hashtable
	Dim myReader As OracleDataReader


	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

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
			myReader.Close()
			myReader.Dispose()
			populatelistbox()
		Else
			BindData()
		End If
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
		MyCmd.Parameters.Clear()
		MyCmd.CommandType = CommandType.StoredProcedure
		MyCmd.CommandText = "PKG_APPLICATION.GetAppVars"
		MyCmd.Connection = conn.Open()
		MyCmd.Parameters.Add("AppVars", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
		myReader = MyCmd.ExecuteReader()

		While myReader.Read
			MyCollection.Add(myReader("apse_variable"), myReader("apse_value"))
		End While

		myReader.Close()
		myReader.Dispose()
		populatelistbox()

	End Sub
	Private Sub populatelistbox()


		Dim fileEntries As String()
		Dim MyDir As Directory
		Dim path As String
		If txtPath.Text = "" Then
			path = CStr(MyCollection("SanovaImportFileArchivePath"))
		Else
			path = txtPath.Text
		End If

		fileEntries = MyDir.GetFiles(path)
		' Process the list of files found in the directory.
		Dim fileName As String
		ddimportDay.Items.Add("Select SQL-Loader log file to view")
		For Each fileName In fileEntries

			If fileName.EndsWith("log") Or fileName.EndsWith("bad") Then
				ddimportDay.Items.Add(fileName)
			End If
		Next fileName

	End Sub


	Private Function GetTransmissions() As DataView
		Try

			MyCmd.CommandText = "PKG_APPLICATION.GetTransmissions"
			MyCmd.CommandType = CommandType.StoredProcedure

			MyCmd.Parameters.Add("transmissions", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
			MyCmd.Parameters.Add("v_dist_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = dddistribSelect.SelectedValue
			MyCmd.Connection = conn.Open()

			MyAdapter.SelectCommand = MyCmd
			MyAdapter.Fill(MyDs, "Codes")
			MyDataView = MyDs.Tables("codes").DefaultView
			conn.Close()
			Return MyDataView
		Catch ex As Exception
			ExceptionInfo.Show(ex)

		End Try
	End Function

	Private Function SqlLoader() As Boolean
		Dim temp, cmd As String
		Try

			Dim fileEntries As String()
			Dim MyDir As Directory
			Dim path As String
			path = txtPath.Text


			' takes the filenames from the folder specified in the input box
			fileEntries = MyDir.GetFiles(path)

			Dim fileName As String
			For Each fileName In fileEntries

				' Alle Files die die entweder "KD" "BE" opder "ART" enthalten und mit .dat enden werden importiert

				If InStr((fileName.ToUpper), "KD") <> 0 And fileName.ToUpper.EndsWith(".DAT") Then
					LabelOUT.Text = LabelOUT.Text & "Importing....   "
					cmd = "SQLLDR.EXE " & MyCollection("OracleUserName") & "/" & MyCollection("OracleUserPassword") & "@" & MyCollection("OracleServiceName") _
					  & " control=" & MyCollection("SanovaImportControlFilePath") & "wyeth_kd.ctl" & " data=" & fileName & " log=" & fileName & ".log bad=" & fileName & ".bad"
					System.CodeDom.Compiler.Executor.ExecWait(cmd, New System.CodeDom.Compiler.TempFileCollection)
					LabelOUT.Text = LabelOUT.Text & fileName & " .. imported <BR>"
				ElseIf InStr(fileName.ToUpper, "ART") <> 0 And fileName.ToUpper.EndsWith(".DAT") Then
					LabelOUT.Text = LabelOUT.Text & "Importing....   "
					cmd = "SQLLDR.EXE " & MyCollection("OracleUserName") & "/" & MyCollection("OracleUserPassword") & "@" & MyCollection("OracleServiceName") _
	   & " control=" & MyCollection("SanovaImportControlFilePath") & "wyeth_art.ctl" & " data=" & fileName & " log=" & fileName & ".log bad=" & fileName & ".bad"
					System.CodeDom.Compiler.Executor.ExecWait(cmd, New System.CodeDom.Compiler.TempFileCollection)
					LabelOUT.Text = LabelOUT.Text & fileName & " .. imported <BR>"
				ElseIf InStr(fileName.ToUpper, "BW") <> 0 And fileName.ToUpper.EndsWith(".DAT") Then
					LabelOUT.Text = LabelOUT.Text & "Importing....   "
					cmd = "SQLLDR.EXE " & MyCollection("OracleUserName") & "/" & MyCollection("OracleUserPassword") & "@" & MyCollection("OracleServiceName") _
					  & " control=" & MyCollection("SanovaImportControlFilePath") & "wyeth_bw.ctl" & " data=" & fileName & " log=" & fileName & ".log bad=" & fileName & ".bad"
					System.CodeDom.Compiler.Executor.ExecWait(cmd, New System.CodeDom.Compiler.TempFileCollection)
					LabelOUT.Text = LabelOUT.Text & fileName & " .. imported <BR>"
				End If

			Next fileName

			Return True
		Catch ex As Exception
			ExceptionInfo.Show(ex)
			Return False
		End Try
		Return True
	End Function

	Private Sub Button_Import_Sanova_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Import_Sanova.Click
		Dim MyCmd As New OracleCommand
		Dim MyConn As New MyConnection
		Dim trans As OracleTransaction
		Dim test As String
		Dim val As String
		' first Load Data with the sql loader into temp tables
		LabelOUT.Text = ""
		If SqlLoader() Then

			If rbl_AutomaticUpdate.SelectedValue = "automatic" Then

				' update MasterTables in ALF with upadte & insert Stored Procedures
				MyCmd.CommandType = CommandType.StoredProcedure
				MyCmd.CommandText = "PKG_IMPORT_SANOVA.F_Customers"
				MyCmd.Parameters.Add("v_result", OracleDbType.Varchar2, 2000, val, ParameterDirection.ReturnValue)
				MyCmd.CommandType = CommandType.StoredProcedure
				MyCmd.Connection = MyConn.Open
				val = CStr(MyCmd.ExecuteScalar())
				MyCmd.CommandText = "PKG_IMPORT_SANOVA.F_Products"
				val = CStr(MyCmd.ExecuteScalar())
				MyCmd.CommandText = "PKG_IMPORT_SANOVA.F_BW"
				val = CStr(MyCmd.ExecuteScalar())
				MyConn.Close()
				CheckForNotImported()
			End If

		End If

	End Sub


	
	Private Sub Button_KD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_KD.Click
		Dim MyConn As New MyConnection
		Dim val As String
		MyCmd.Parameters.Clear()
		MyCmd.CommandType = CommandType.StoredProcedure
		MyCmd.CommandText = "PKG_IMPORT_SANOVA.F_Customers"
		MyCmd.Parameters.Add("v_result", OracleDbType.Varchar2, 2000, Val, ParameterDirection.ReturnValue)
		MyCmd.CommandType = CommandType.StoredProcedure
		MyCmd.Connection = MyConn.Open
		Val = CStr(MyCmd.ExecuteScalar())

	End Sub

	Private Sub Button_ART_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_ART.Click
		Dim MyConn As New MyConnection
		Dim val As String
		MyCmd.Parameters.Clear()
		MyCmd.CommandType = CommandType.StoredProcedure
		MyCmd.Parameters.Add("v_result", OracleDbType.Varchar2, 2000, val, ParameterDirection.ReturnValue)
		MyCmd.Connection = MyConn.Open
		MyCmd.CommandText = "PKG_IMPORT_SANOVA.F_Products"
		Val = CStr(MyCmd.ExecuteScalar())
	End Sub

	Private Sub Button_BW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_BW.Click
		' update MasterTables in ALF with upadte & insert Stored Procedures
		Dim MyConn As New MyConnection
		Dim val As String
		MyCmd.Parameters.Clear()
		MyCmd.CommandType = CommandType.StoredProcedure
		MyCmd.Parameters.Add("v_result", OracleDbType.Varchar2, 2000, Val, ParameterDirection.ReturnValue)
		MyCmd.Connection = MyConn.Open
		MyCmd.CommandText = "PKG_IMPORT_SANOVA.F_BW"
		Val = CStr(MyCmd.ExecuteScalar())
		MyConn.Close()
	End Sub

	Private Sub rbl_AutomaticUpdate_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbl_AutomaticUpdate.SelectedIndexChanged
		If rbl_AutomaticUpdate.SelectedValue = "Manual" Then
			Button_KD.Enabled = True
			Button_ART.Enabled = True
			Button_BW.Enabled = True
		Else
			Button_KD.Enabled = False
			Button_ART.Enabled = False
			Button_BW.Enabled = False
		End If
	End Sub

	Private Sub ddimportDay_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddimportDay.SelectedIndexChanged
		Dim strScript As String

		Dim Str As String = ddimportDay.SelectedValue
		Dim writer As New StringWriter
		Server.UrlEncode(Str, writer)
		Dim EncodedString As String = writer.ToString()

		strScript = "<script language =javascript >"
		strScript += "window.open('SqlLoaderLog.aspx?file=" & EncodedString & "','Error','width=500,height=650,left=170,top=80','scrollbars=yes','resizable=yes');"
		strScript += "</script>"

		RegisterClientScriptBlock("anything", strScript)
	End Sub

	Private Sub txtPath_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPath.TextChanged
		ddimportDay.Items.Clear()
		populatelistbox()
	End Sub

	Private Sub CheckForNotImported()
		Dim strScript As String
		strScript = "<script language =javascript >"
		strScript += "window.open('NotImported.aspx','Error','width=500,height=650,left=170,top=80','scrollbars=yes','resizable=yes');"
		strScript += "</script>"
		RegisterClientScriptBlock("anything", strScript)
	End Sub
End Class
