'Option Strict On
'Option Explicit On 

'Imports Wyeth.Utilities
'Imports Oracle.DataAccess.Client
'Imports C1.Web.C1WebGrid


'Public Class table_tmp

'    Private MyCmd, MyInsCmd, MyUpdCmd, MyDelCmd As New OracleCommand
'    Private MyConn As New MyConnection
'    Private MyAdapter As New OracleDataAdapter
'    Private MyDataset As New DataSet
'    Private MyReader As OracleDataReader
'    Private MyHashtable As New Hashtable
'    Private MyDataView As New DataView
'    Private m_str_tableName As String

'    Public Sub New(ByVal tableName As String)
'        MyBase.New()
'    End Sub

'    Public Function insert(ByVal MyForm As Web.UI.HtmlControls.HtmlForm) As Boolean

'        Dim str_sql As String = "select * from " & TableName
'        Dim str_sql_insert As String = "select * from " & TableName.ToUpper & "';"
'        Dim str_sql_schema As String = "select column_name,data_type from USER_TAB_COLUMNS where table_name='" & TableName & "'"
'        Dim colname, str_OraType As String

'        Dim i As Integer
'        Dim MySchemaTable As DataTable
'        Try

'            MyCmd.CommandType = CommandType.Text
'            MyCmd.CommandText = str_sql_schema
'            MyCmd.Connection = MyConn.Open
'            MyReader = MyCmd.ExecuteReader(CommandBehavior.CloseConnection)


'            While MyReader.Read
'                'str_OraType = "OracleDBType." & CType(MyReader("data_type"), String)
'                str_OraType = "OracleDBType." & CType(MyReader("data_type"), String)
'                MyInsCmd.Parameters.Add("v_" & CType(MyReader("column_name"), String), CType(str_OraType, OracleDbType), ParameterDirection.Input).Value = MyForm.FindControl(CStr((MyReader("column_name"))))
'            End While
'            'MySchemaTable = MyAdapter.FillSchema(tableName, SchemaType.Source)
'            MyConn.Close()
'        Catch ex As Exception
'            ExceptionInfo.Show(ex)
'            Return True

'        End Try
'        Return True

'    End Function

'    Public Sub draw(ByRef MyGrid As C1WebGrid)


'        GetTableSchema()









'        Dim MyColumn As New C1BoundColumn
'        Dim MyEditColumn As New C1EditCommandColumn
'        Dim MyButtonColumn As New C1ButtonColumn

'        For Each MyEntry As DictionaryEntry In MyHashtable

'            MyColumn.DataField = CStr(MyEntry.Key)
'            MyColumn.HeaderText = CStr(MyEntry.Key)
'            MyGrid.Columns.Add(MyColumn)

'        Next
'        'finally add  edit & delete columns

'        MyButtonColumn.ButtonType = ButtonColumnType.PushButton
'        MyButtonColumn.Text = "Delete"
'        MyButtonColumn.CommandName = "Delete"
'        MyGrid.Columns.Add(MyButtonColumn)
'        'MyGrid.DataSource = GetTableData()
'        'MyGrid.Attributes.Add("OnEditCommand", " MyGrid _Edit")
'        'MyGrid.Attributes.Add("OnUpdateCommand", " MyGrid _Update")
'        'MyGrid.Attributes.Add("OnCancelCommand", " MyGrid _Cancel")

'    End Sub
'    Public Sub addColumns()

'    End Sub

'    Public Function GetTableData() As DataView
'        Try


'            MyCmd.CommandType = CommandType.Text
'            MyCmd.CommandText = "select * from " & m_str_tableName
'            MyCmd.Connection = MyConn.Open
'            MyAdapter.SelectCommand = MyCmd
'            MyAdapter.Fill(MyDataset, TableName)
'            MyDataView = MyDataset.Tables(TableName).DefaultView
'            MyConn.Close()
'            Return MyDataView

'        Catch ex As Exception
'            ExceptionInfo.Show(ex)
'        End Try

'    End Function
'    Private Sub GetTableSchema()

'        Try
'            TableName = m_str_tableName
'            Dim str_sql_schema As String = "select column_name,data_type from USER_TAB_COLUMNS where table_name='" & TableName & "'"
'            Dim test As String

'            MyCmd.CommandType = CommandType.Text
'            MyCmd.CommandText = str_sql_schema
'            MyCmd.Connection = MyConn.Open
'            MyReader = MyCmd.ExecuteReader(CommandBehavior.CloseConnection)

'            While MyReader.Read
'                test = CStr(MyReader("column_name"))

'                MyHashtable.Add(CStr(MyReader("column_name")), CStr(MyReader("data_type")))
'            End While

'            MyReader.Close()
'            MyConn.Close()
'        Catch ex As Exception
'            ExceptionInfo.Show(ex)
'        End Try
'    End Sub


'    Public Property TableName() As String
'        Get
'            m_str_tableName = m_str_tableName.ToUpper
'            Return m_str_tableName
'        End Get
'        Set(ByVal Value As String)
'            m_str_tableName = Value.ToUpper
'        End Set
'    End Property

'End Class
