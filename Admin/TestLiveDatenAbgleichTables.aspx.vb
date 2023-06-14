Imports Wyeth.Alf.CssStyles
Imports Wyeth.Alf.DataAccessBaseClass
Imports Oracle.DataAccess.Client

Public Class TestLiveDatenAbgleichTables
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents btn_save As System.Web.UI.WebControls.Button

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

      
            BindData()
            Me.ALFPageTitle = "Data Syncronize Table"


    End Sub

    Private Sub BindData()
        SetGridStyles(MyGrid)
        Dim myData As New DataSyncronize
        MyGrid.DataSource = myData.Gettables()
        MyGrid.DataBind(True)
    End Sub

    Private Sub OnItemdataBound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound

        Dim MyDropdown As New DropDownList
        MyDropdown.ID = "DropDown"
        Dim li As New ListItem
        li.Value = "0"
        li.Text = "don't import"
        MyDropdown.Items.Add(li)

        Dim li2 As New ListItem
        li2.Value = "1"
        li2.Text = "import"
        MyDropdown.Items.Add(li2)
        e.Item.Cells(3).Controls.Add(MyDropdown)
        MyDropdown.SelectedValue = e.Item.Cells(3).Text()
        MyDropdown.DataBind()




    End Sub

    Private Sub btn_save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_save.Click
        For Each item As C1.Web.C1WebGrid.C1GridItem In MyGrid.Items()

            If item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Or item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Then
                Dim sql As String
                sql = "UPDATE T_IMPORT_CONTROL SET IMCT_NUMBER=" & item.Cells(0).Text & " ,IMCT_NAME='" & item.Cells(1).Text & "', IMCT_TYPE='" & item.Cells(2).Text & "',IMCT_STATUS=" & CType(item.Cells(3).FindControl("DropDown"), DropDownList).SelectedValue.ToString & " where upper(imct_name)='" & item.Cells(1).Text & "'"
                Dim MyConn As New Wyeth.Utilities.MyConnection
                Try
                    Dim MyCommand As New OracleCommand

                    MyCommand.CommandText = sql
                    MyCommand.CommandType = CommandType.Text
                    MyCommand.Connection = MyConn.Open()
                    MyCommand.ExecuteNonQuery()

                Catch ex As Exception
                Finally
                    MyConn.Close()
                End Try
            End If

        Next
    End Sub
End Class
