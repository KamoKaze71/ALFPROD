
Imports System.Object
Imports System.Web.UI.Control
Imports System.Web.UI.Page
Imports System.Web.UI.TemplateControl

Imports Oracle.DataAccess.Client

Imports Wyeth.Utilities
Imports Wyeth.Alf.CssStyles

Imports C1.Web.C1WebGrid


Public Class SalesRepTaPrGr
	Inherits System.Web.UI.Page
	Dim MySalesRep As New WyethSalesRep


#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

	End Sub
	Protected WithEvents Button1 As System.Web.UI.WebControls.Button
	Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
	Protected WithEvents Button_save As System.Web.UI.WebControls.Button
	Protected WithEvents EditPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents TargetRepeater As System.Web.UI.WebControls.Repeater
	Protected WithEvents Cancel As System.Web.UI.WebControls.Button
	Protected WithEvents txtQ4 As System.Web.UI.WebControls.TextBox
	Protected WithEvents txtQ3 As System.Web.UI.WebControls.TextBox
	Protected WithEvents txtQ2 As System.Web.UI.WebControls.TextBox
	Protected WithEvents txtQ1 As System.Web.UI.WebControls.TextBox
	Protected WithEvents GridPanel As System.Web.UI.WebControls.Panel
	Protected WithEvents Button_save_editpanel As System.Web.UI.WebControls.Button
	Protected WithEvents Button_save_grid As System.Web.UI.WebControls.Button
	Protected WithEvents lblTapg_descr As System.Web.UI.WebControls.Label
	Protected WithEvents lbltapgid As System.Web.UI.WebControls.Label
	Protected WithEvents lblSum1 As System.Web.UI.WebControls.Label
	Protected WithEvents lblSum2 As System.Web.UI.WebControls.Label
	Protected WithEvents lblSum3 As System.Web.UI.WebControls.Label
	Protected WithEvents lblSum4 As System.Web.UI.WebControls.Label
	Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label


	'NOTE: The following placeholder declaration is required by the Web Form Designer.
	'Do not delete or move it.
	Private designerPlaceholderDeclaration As System.Object

	Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
		'CODEGEN: This method call is required by the Web Form Designer
		'Do not modify it using the code editor.
		InitializeComponent()
	End Sub

#End Region


	Dim MyDataView As New DataView
	Public sum1, sum2, sum3, sum4 As Double

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		'Put user code to initialize the page here
		If Page.IsPostBack = True Then
			BindData()
		Else
			lblPageTitle.Text = Request.QueryString("pageTitle")
			BindData()
		End If

	End Sub
	Public Sub BindData()


		Dim MyCol As New DataColumn

		Dim myCold_edit As New C1ButtonColumn
		Dim MyCol_TaPg_id As New C1BoundColumn
		Dim MyCol_TaPg_desc As New C1BoundColumn

		Dim MyItemTemplate As System.web.UI.ITemplate

		MyItemTemplate = (Page.LoadTemplate("../controls/saretapg.ascx"))

		MyCol_TaPg_id.Visible = False
		MyCol_TaPg_id.DataField = "tapg_id"
		MyGrid.Columns.Add(MyCol_TaPg_id)

		myCold_edit.CommandName = "MyGrid_Edit"
		myCold_edit.Text = "Edit"
		myCold_edit.HeaderText = "Edit"
		myCold_edit.ButtonType = ButtonColumnType.PushButton
		MyGrid.Columns.Add(myCold_edit)

		MyCol_TaPg_desc.DataField = "tapg_description"
		MyCol_TaPg_desc.HeaderText = "Target Group Description"
		MyGrid.Columns.Add(MyCol_TaPg_desc)

		MyDataView = GetSalesRepsTAPG()

		For Each MyCol In MyDataView.Table.Columns
			If MyCol.Ordinal > 2 Then
				Dim GridColumn As New C1.Web.C1WebGrid.C1TemplateColumn

				GridColumn.HeaderText = MyCol.ColumnName
				GridColumn.ItemTemplate = MyItemTemplate

				MyGrid.Columns.Add(GridColumn)
			End If

		Next


		SetGridStyles(MyGrid)
		MyGrid.DataSource = MyDataView
		MyGrid.DataBind()

	End Sub
	Public Function GetSalesRepsTAPG() As DataView

		Dim MyAdapter As New OracleDataAdapter
		Dim MyCmd As New OracleCommand
		Dim MyConn As New MyConnection
		Dim MyDs As New DataSet
		Try

			MyCmd.Connection = MyConn.Open()
			MyCmd.Parameters.Clear()
			MyCmd.Parameters.Add("SalesRep", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
			MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = Session("country_id")
			MyCmd.CommandText = "PKG_APPLICATION.GetSalesRepIDs"
			MyCmd.CommandType = CommandType.StoredProcedure
			MyAdapter.SelectCommand = MyCmd
			MyAdapter.Fill(MyDs, "sare_id")

			MyCmd.Parameters.Clear()
			MyCmd.Parameters.Add("TargetSalesRep", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
			MyCmd.CommandText = "PKG_APPLICATION.GetTargetSalesRep"
			MyCmd.CommandType = CommandType.StoredProcedure
			MyAdapter.Fill(MyDs, "sare")
			Dim myrow As DataRow = MyDs.Tables(1).NewRow()

			Dim x As Integer = 2


			MyDs.Tables(1).Rows.InsertAt(myrow, 0)

			For Each myrow In MyDs.Tables(0).Rows

				MyDs.Tables(1).Rows(0).Item(x) = myrow.Item(0)

				x = x + 1
			Next

			MyDataView = MyDs.Tables(1).DefaultView
			Return MyDataView


		Catch ex As Exception
			ExceptionInfo.Show(ex)
		Finally
			MyConn.Close()

		End Try

	End Function


	Public Sub MyGrid_ItemBoundCommand(ByVal sender As System.Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound

		Dim x, i As Integer
		Dim mycheck As CheckBox
		Dim mytxtBox As TextBox

		i = e.Item.Controls.Count() - 1

		If e.Item.ItemIndex > 0 Then

			For x = 3 To i
                If (MyDataView.Item(e.Item.ItemIndex).Item(x) = 0) Then

                Else
                    mycheck = CType(e.Item.Cells(x).Controls(0).Controls(0), CheckBox)
                    mycheck.Checked = True
                End If

            Next
		Else
			MyGrid.Items(e.Item.ItemIndex).Visible = False
		End If

	End Sub

    Private Sub MyGrid_ItemCreated(ByVal sender As System.Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated

        Try


            If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Then
                e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
                e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor1Class';")

                CType(e.Item.Cells(1).Controls(0), WebControl).CssClass = "button_common"

            ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then

                e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
                e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor2Class';")

                CType(e.Item.Cells(1).Controls(0), WebControl).CssClass = "button_common"
            End If

        Catch ex As Exception
            Exit Sub
        End Try
    End Sub
    Private Sub TargetRepeater_ItemCreated(ByVal sender As System.Object, ByVal e As RepeaterItemEventArgs) Handles TargetRepeater.ItemDataBound
        Try


            sum1 = sum1 + CDbl(e.Item.DataItem(4))
            sum2 = sum2 + CDbl(e.Item.DataItem(5))
            sum3 = sum3 + CDbl(e.Item.DataItem(6))
            sum4 = sum4 + CDbl(e.Item.DataItem(7))
            lblSum1.DataBind()
            lblSum2.DataBind()
            lblSum3.DataBind()
            lblSum4.DataBind()
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        End Try
    End Sub
    Private Sub Button_save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_save_grid.Click

        Dim myDataGridItem As C1.Web.C1WebGrid.C1GridItem
        Dim chkSelected As New System.Web.UI.WebControls.CheckBox
        Dim str_sql_insert, count As String
        Dim sare_id, tapg_id, i, x As Integer
        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        MyCmd.Connection = Myconn.Open

        Try  ' first delete all entries i the table
            'MyCmd.CommandType = CommandType.Text
            'MyCmd.CommandText = "update t_target_pg_sr set "
            'MyCmd.Connection = Myconn.Open()
            'MyCmd.ExecuteNonQuery()


            For Each myDataGridItem In MyGrid.Items
                '
                i = myDataGridItem.Controls.Count() - 1

                If myDataGridItem.ItemIndex > 0 Then

                    For x = 3 To i     ' fistr two columns do not contain checkboxes


                        Dim myCheckbox As CheckBox
                        myCheckbox = CType(myDataGridItem.Cells(x).Controls(0).Controls(0), CheckBox)

                        sare_id = MyDataView.Item(0).Item(x)
                        tapg_id = myDataGridItem.Cells(0).Text

                        If myCheckbox.Checked Then


                            str_sql_insert = "update T_TARGET_PG_SR set TPGS_STATUS=1,tpgs_user_id=" & Session("user_id") & " where TAPG_ID=" & tapg_id & "  AND SARE_ID=" & sare_id


                        Else       'set deleted

                            str_sql_insert = "update T_TARGET_PG_SR set TPGS_STATUS=0,tpgs_user_id=" & Session("user_id") & " where TAPG_ID=" & tapg_id & "  AND SARE_ID=" & sare_id

                        End If
                        Try

                            MyCmd.CommandType = CommandType.Text
                            MyCmd.CommandText = str_sql_insert

                            MyCmd.ExecuteNonQuery()

                        Catch ex As Exception

                        End Try
                    Next
                End If
            Next



        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Myconn.Close()
        End Try

    End Sub
    Public Sub MyGrid_ItemCommand(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1CommandEventArgs) Handles MyGrid.ItemCommand

        Dim MyCmd As New OracleCommand
        Dim MyConn As New MyConnection
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDs As New DataSet

        If e.CommandName = "MyGrid_Edit" Then

            GridPanel.Visible = False
            EditPanel.Visible = True
            Try


                Me.lblTapg_descr.Text = "Targets for:" & e.Item.Cells(2).Text & "  " & Year(Now)
                Me.lbltapgid.Text = e.Item.Cells(0).Text
                Dim strsql As String

                sum1 = 0
                sum2 = 0
                sum3 = 0
                sum4 = 0

                lblSum1.DataBind()
                lblSum2.DataBind()
                lblSum3.DataBind()
                lblSum4.DataBind()



                MyCmd.Parameters.Clear()
                MyCmd.Connection = MyConn.Open
                MyCmd.CommandText = "PKG_APPLICATION.GetTargets"
                MyCmd.CommandType = CommandType.StoredProcedure
                MyCmd.Parameters.Add("SalesRep", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                MyCmd.Parameters.Add("v_tapg_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = e.Item.Cells(0).Text
                MyAdapter.SelectCommand = MyCmd
                MyAdapter.Fill(MyDs, "Targets")
                TargetRepeater.DataSource = MyDs.Tables("Targets")
                TargetRepeater.DataBind()

            Catch ex As Exception
            Finally
                MyConn.Close()
            End Try
        End If

    End Sub
    Private Sub q1_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQ1.Load, txtQ2.Load, txtQ3.Load, txtQ4.Load

        For Each textbox As textbox In EditPanel.Controls

            If textbox.ID.EndsWith("Q1") Then
                sum1 = sum1 + CDbl(textbox.Text)
            End If
            lblSum1.DataBind()

        Next
    End Sub
    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        GridPanel.Visible = True
        EditPanel.Visible = False

    End Sub
    Private Sub Button_update_grid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_save_editpanel.Click

        Dim str_id As String
        Dim str_sql As String
        Dim target_date As String
        Dim tapg_id, sare_id As Integer
        Dim target_value As Double
        Dim MytxtBox As TextBox
        Dim insert_bol As Boolean


        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection

        Try
            MyCmd.CommandType = CommandType.Text
            MyCmd.CommandText = "Delete from t_target where tapg_id=" & CInt(Me.lbltapgid.Text)
            MyCmd.Connection = Myconn.Open()
            MyCmd.ExecuteNonQuery()



            For Each Control As Control In TargetRepeater.Controls

                'str_id = Control.Controls.Count
                insert_bol = False

                For Each childcontrol As Control In Control.Controls

                    str_id = childcontrol.ID
                    If str_id = Nothing Then
                        str_id = "xxx"
                    End If

                    If str_id = "txtsare_id" Then
                        sare_id = CInt(CType(childcontrol, TextBox).Text)
                    End If

                    If str_id = "txttapg_id" Then
                        MytxtBox = CType(childcontrol, TextBox)
                        If MytxtBox.Text.Length = 0 Then

                        Else
                            tapg_id = MytxtBox.Text
                        End If
                    End If

                    If str_id.EndsWith("txtQ1") Then
                        target_date = "1-01-" & Year(Now)
                        target_value = 0
                        MytxtBox = CType(childcontrol, TextBox)
                        If MytxtBox.Text.Length = 0 Then
                            target_value = 0
                        Else
                            target_value = MytxtBox.Text
                            insert_bol = True
                        End If

                    ElseIf str_id.EndsWith("txtQ2") Then
                        target_date = "1-04-" & Year(Now)
                        MytxtBox = CType(childcontrol, TextBox)
                        If MytxtBox.Text.Length = 0 Then
                            target_value = 0
                        Else
                            target_value = MytxtBox.Text
                            insert_bol = True
                        End If
                    ElseIf str_id.EndsWith("txtQ3") Then
                        target_date = "1-07-" & Year(Now)
                        MytxtBox = CType(childcontrol, TextBox)
                        If MytxtBox.Text.Length = 0 Then
                            target_value = 0
                        Else
                            target_value = MytxtBox.Text
                            insert_bol = True
                        End If
                    ElseIf str_id.EndsWith("txtQ4") Then
                        target_date = "1-10-" & Year(Now)
                        MytxtBox = CType(childcontrol, TextBox)
                        If MytxtBox.Text.Length = 0 Then
                            target_value = 0
                        Else
                            target_value = MytxtBox.Text
                            insert_bol = True
                        End If
                    End If

                    If insert_bol = True Then

                        MyCmd.CommandType = CommandType.Text

                        str_sql = "INSERT INTO T_TARGET (TAPG_ID, SARE_ID, TARG_DATE, TARG_VALUE, TARG_USER_ID)"
                        str_sql += "VALUES (" & tapg_id & " ," & sare_id & " ,'" & target_date & "', " & target_value & " ," & Session("user_id") & ")"

                        MyCmd.CommandText = str_sql
                        MyCmd.ExecuteNonQuery()

                        insert_bol = False

                    End If

                Next

            Next
            EditPanel.Visible = False
            GridPanel.Visible = True



        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Myconn.Close()
        End Try


    End Sub

End Class
