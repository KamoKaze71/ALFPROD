Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports Wyeth.Utilities
'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
'''   ''' <summary><para>Provides DataAccess for T_Customer & T_CUSTOMER_GROUP</para></summary>
Public Class WyethCustomer

	Private m_cust_id As Integer
	Private m_cust_name As String
	Private m_cust_department As String
	Private m_cust_wyeth_name As String
	Private m_cust_zip As String
	Private m_cust_city As String
	Private m_cust_street As String
	Private m_cust_gr_id As Integer
	Private m_cust_ctry_id As Integer
	Private m_cust_stat_id As Integer
	Private m_cust_user_id As Integer
	Private m_cust_date_changes As Date
	Private m_str__customer_group_description As String
    Private m_str_customer_group_code As String
    Private m_cust_dist_id As Integer

	Private MyCmd As New OracleCommand
	Sub New()
		MyBase.New()
	End Sub
	Public Function initalizeParams(ByVal type As String) As Boolean

		If type = "upd" Then
			MyCmd.Parameters.Add("v_cust_ID", OracleDbType.Int32, ParameterDirection.Input).Value = m_cust_id
		End If
		MyCmd.Parameters.Add("v_Cust_Name", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = m_cust_name
		MyCmd.Parameters.Add("v_Cust_Department", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = m_cust_department
		MyCmd.Parameters.Add("v_Cust_Wyeth_Name", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = m_cust_wyeth_name
		MyCmd.Parameters.Add("v_Cust_Zip", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = m_cust_zip
		MyCmd.Parameters.Add("v_Cust_City", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = m_cust_city
		MyCmd.Parameters.Add("v_Cust_Street ", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = m_cust_street
		MyCmd.Parameters.Add("v_Cust_User_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = m_cust_user_id
        MyCmd.Parameters.Add("v_CuGr_ID ", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = m_cust_gr_id
		MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = m_cust_ctry_id
        'MyCmd.Parameters.Add("v_Stat_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = m_cust_stat_id

	End Function


	Public Function insert() As Boolean

		Dim conn As New MyConnection
		Try
			initalizeParams("ins")

			MyCmd.CommandText = "P_Customer_Ins_Proc"
			MyCmd.CommandType = CommandType.StoredProcedure
			MyCmd.Connection = conn.Open()
			MyCmd.ExecuteNonQuery()


		Catch ex As Exception
			ExceptionInfo.Show(ex)
			Return False
		Finally
			conn.Close()
		End Try
		Return True
	End Function
	Public Function InsertCustomerGroup() As Boolean

		Dim conn As New MyConnection
		Try
 
			MyCmd.CommandText = "P_Customer_Group_Ins_Proc"
			MyCmd.CommandType = CommandType.StoredProcedure

			MyCmd.Parameters.Add("v_CuGr_Code", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = CustomerGroupCode
			MyCmd.Parameters.Add("v_CuGr_Description", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = CustomerGroupDescription
			MyCmd.Parameters.Add("v_CuGr_User_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = Cust_User_Id
			MyCmd.Parameters.Add("v_Ctry_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = Cust_Country_Id

			MyCmd.Connection = conn.Open()
			MyCmd.ExecuteNonQuery()


		Catch ex As Exception
			ExceptionInfo.Show(ex)
			Return False
		Finally
			conn.Close()
		End Try
		Return True
	End Function
	Public Function update() As Boolean

		Dim conn As New MyConnection
		Try
			initalizeParams("upd")

			MyCmd.CommandText = "P_Customer_Upd_Proc"
			MyCmd.CommandType = CommandType.StoredProcedure
			MyCmd.Connection = conn.Open()
			MyCmd.ExecuteNonQuery()


		Catch ex As Exception
			ExceptionInfo.Show(ex)
			Return False
		Finally
			conn.Close()

		End Try
		Return True

	End Function
	Public Function UpdateCustomerGroup() As Boolean

		Dim conn As New MyConnection
		Try


			MyCmd.Parameters.Add("v_CuGr_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = Cust_Group_Id
			MyCmd.Parameters.Add("v_CuGr_Code", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = CustomerGroupCode
			MyCmd.Parameters.Add("v_CuGr_Description", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = CustomerGroupDescription
			MyCmd.Parameters.Add("v_CuGr_User_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = Cust_User_Id
			MyCmd.Parameters.Add("v_Ctry_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = Cust_Country_Id
			MyCmd.CommandText = "P_Customer_Group_Upd_Proc"
			MyCmd.CommandType = CommandType.StoredProcedure
			MyCmd.Connection = conn.Open()
			MyCmd.ExecuteNonQuery()

		Catch ex As Exception
			ExceptionInfo.Show(ex)
			Return False
		Finally
			conn.Close()

		End Try
		Return True

	End Function
	Public Function GetCustomerList() As DataView

		Dim MyDs As New DataSet
		Dim MyCmd As New OracleCommand
		Dim MyAdapter As New OracleDataAdapter
		Dim MyDataView As New DataView
		Dim conn As New MyConnection

		Try

		MyCmd.CommandText = "PKG_CUSTOMER.GetCustomerList"
		MyCmd.CommandType = CommandType.StoredProcedure

		MyCmd.Parameters.Add("CustomerGroupList", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
		MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = Cust_Country_Id
		MyCmd.Connection = conn.Open()

		MyAdapter.SelectCommand = MyCmd
		MyAdapter.Fill(MyDs, "CustomerList")
		MyDataView = MyDs.Tables("CustomerList").DefaultView

		MyCmd.Dispose()
		MyAdapter.Dispose()


			Return MyDataView


		Catch ex As Exception
			ExceptionInfo.Show(ex)
		Finally
			conn.Close()
		End Try
	End Function


	Public Function GetCustomerPlz() As DataView

		Dim MyDs As New DataSet
		Dim MyCmd As New OracleCommand
		Dim MyAdapter As New OracleDataAdapter
		Dim MyDataView As New DataView
		Dim conn As New MyConnection

		Try

			MyCmd.CommandText = "PKG_CUSTOMER.GetCustomerPlz"
			MyCmd.CommandType = CommandType.StoredProcedure

			MyCmd.Parameters.Add("CustomerPlz", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
			MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = Cust_Country_Id
			MyCmd.Connection = conn.Open()

			MyAdapter.SelectCommand = MyCmd
			MyAdapter.Fill(MyDs, "CustomerPlz")
			MyDataView = MyDs.Tables("CustomerPlz").DefaultView

			MyCmd.Dispose()
			MyAdapter.Dispose()


			Return MyDataView


		Catch ex As Exception
			ExceptionInfo.Show(ex)
		Finally
			conn.Close()
		End Try
	End Function




	Public Function GetCustomerGroups() As DataView
		Dim MyDs As New DataSet
		Dim MyCmd As New OracleCommand
		Dim MyAdapter As New OracleDataAdapter
		Dim MyDataView As New DataView
		Dim conn As New MyConnection

		Try

			MyCmd.CommandText = "PKG_CUSTOMER.GetCustomerGroups"
			MyCmd.CommandType = CommandType.StoredProcedure

			MyCmd.Parameters.Add("CustomerGroups", OracleDbType.RefCursor, ParameterDirection.Output)
			MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = m_cust_ctry_id
			MyCmd.Connection = conn.Open()

			MyAdapter.SelectCommand = MyCmd
			MyAdapter.Fill(MyDs, "CustomerGroups")
			MyDataView = MyDs.Tables("CustomerGroups").DefaultView

			MyCmd.Dispose()
			MyAdapter.Dispose()


			Return MyDataView

		Catch ex As Exception
			ExceptionInfo.Show(ex)
		Finally
			conn.Close()
		End Try
	End Function

	Public Function GetCustomer() As DataView

		Dim MyDs As New DataSet
		Dim MyCmd As New OracleCommand
		Dim MyAdapter As New OracleDataAdapter
		Dim MyDataView As New DataView
		Dim conn As New MyConnection

		Try

            MyCmd.CommandText = "PKG_CUSTOMER.GetCustomer"
			MyCmd.CommandType = CommandType.StoredProcedure

			MyCmd.Parameters.Add("Customer", OracleDbType.RefCursor, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_searchsttring", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = Cust_Name
            MyCmd.Parameters.Add("v_dist_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = Cust_Dist_Id
            MyCmd.Parameters.Add("v_cust_group_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = Cust_Group_Id


			MyCmd.Connection = conn.Open()

			MyAdapter.SelectCommand = MyCmd
			MyAdapter.Fill(MyDs, "Customer")

			MyDataView = MyDs.Tables("Customer").DefaultView

			MyCmd.Dispose()
			MyAdapter.Dispose()


			Return MyDataView

		Catch ex As Exception
			ExceptionInfo.Show(ex)
		Finally
			conn.Close()
		End Try
    End Function

    Public Function GetCustomerByID(ByVal cust_id As Integer) As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_CUSTOMER.GetCustomerByID"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Add("Customer", OracleDbType.RefCursor, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_cust_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = Cust_Id

            MyCmd.Connection = conn.Open()

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Customer")

            MyDataView = MyDs.Tables("Customer").DefaultView

            MyCmd.Dispose()
            MyAdapter.Dispose()


            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try
    End Function

    Public Function GetCustomerByCudiNr(ByVal cudi_nr As String) As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_CUSTOMER.GetCustomerByCudiNr"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Add("Customer", OracleDbType.RefCursor, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_cudi_nr", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = cudi_nr

            MyCmd.Connection = conn.Open()

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Customer")

            MyDataView = MyDs.Tables("Customer").DefaultView

            MyCmd.Dispose()
            MyAdapter.Dispose()


            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try
    End Function


    Public Function DeleteCustomer() As Boolean

        Dim MyCmd As New OracleCommand
        Dim conn As New MyConnection
        Try

            MyCmd.CommandText = "P_Customer_Group_Del_Proc"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Add("v_cust_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = m_cust_id
            MyCmd.Connection = conn.Open()
            MyCmd.ExecuteNonQuery()

            MyCmd.Dispose()

        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Return False
        Finally
            conn.Close()
        End Try
        Return True

    End Function
    Public Function DeleteCustomerGroup() As Boolean

        Dim MyCmd As New OracleCommand
        Dim conn As New MyConnection
        Try

            MyCmd.CommandText = "P_Customer_Group_Del_Proc"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Add("v_CuGr_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = Cust_Group_Id
            MyCmd.Connection = conn.Open()
            MyCmd.ExecuteNonQuery()

            MyCmd.Dispose()
            conn.Close()
        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Return False
        Finally
            conn.Close()
        End Try
        Return True

    End Function

    Public Function GetCustomerStatType() As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_CUSTOMER.GetCustomerStatType"
            MyCmd.CommandType = CommandType.StoredProcedure


            MyCmd.Parameters.Add("CustomerStatType", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = m_cust_ctry_id
            MyCmd.Connection = conn.Open()

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "CustomerStatType")

            MyDataView = MyDs.Tables("CustomerStatType").DefaultView

            MyCmd.Dispose()
            MyAdapter.Dispose()


            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try
    End Function
    Public Function GetCustomerCodes() As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try

            MyCmd.CommandText = "PKG_CUSTOMER.GetCustomerCodes"
            MyCmd.CommandType = CommandType.StoredProcedure


            MyCmd.Parameters.Add("CustomerStatType", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = m_cust_ctry_id
            MyCmd.Connection = conn.Open()

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "CustomerCodes")

            MyDataView = MyDs.Tables("CustomerCodes").DefaultView

            MyCmd.Dispose()
            MyAdapter.Dispose()


            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            conn.Close()
        End Try
    End Function
    Public Function GetCustomerDist() As DataView

        Dim MyDs As New DataSet
        Dim MyCmd As New OracleCommand

        Dim MyAdapter As New OracleDataAdapter
        Dim MyDataView As New DataView
        Dim conn As New MyConnection

        Try
            MyCmd.CommandText = "PKG_CUSTOMER.GetCustomerDist"
            MyCmd.CommandType = CommandType.StoredProcedure


            MyCmd.Parameters.Add("CustomerStatType", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_cust_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = m_cust_id
            MyCmd.Connection = conn.Open()

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "CustomerDist")

            MyDataView = MyDs.Tables("CustomerDist").DefaultView

            MyCmd.Dispose()
            MyAdapter.Dispose()


            Return MyDataView

        Catch ex As Exception
            ExceptionInfo.Show(ex)

        Finally
            conn.Close()
        End Try
    End Function

    Public Property Cust_Id() As Integer
        Get
            Return m_cust_id
        End Get
        Set(ByVal Value As Integer)
            m_cust_id = Value
        End Set
    End Property
    Public Property Cust_Name() As String
        Get
            Return m_cust_name
        End Get
        Set(ByVal Value As String)
            m_cust_name = Value
        End Set
    End Property
    Public Property Cust_Department() As String
        Get
            Return m_cust_department
        End Get
        Set(ByVal Value As String)
            m_cust_department = Value
        End Set
    End Property
    Public Property Cust_WyethName() As String
        Get
            Return m_cust_wyeth_name
        End Get
        Set(ByVal Value As String)
            m_cust_wyeth_name = Value
        End Set
    End Property
    Public Property Cust_Zip() As String
        Get
            Return m_cust_zip
        End Get
        Set(ByVal Value As String)
            m_cust_zip = Value
        End Set
    End Property
    Public Property Cust_City() As String
        Get
            Return m_cust_city
        End Get
        Set(ByVal Value As String)
            m_cust_city = Value
        End Set
    End Property
    Public Property Cust_Street() As String
        Get
            Return m_cust_street
        End Get
        Set(ByVal Value As String)
            m_cust_street = Value
        End Set
    End Property
    Public Property Cust_Country_Id() As Integer
        Get
            Return m_cust_ctry_id
        End Get
        Set(ByVal Value As Integer)
            m_cust_ctry_id = Value
        End Set
    End Property
    Public Property Cust_Group_Id() As Integer
        Get
            Return m_cust_gr_id
        End Get
        Set(ByVal Value As Integer)
            m_cust_gr_id = Value
        End Set
    End Property
    Public Property Cust_Stat_Id() As Integer
        Get
            Return m_cust_stat_id
        End Get
        Set(ByVal Value As Integer)
            m_cust_stat_id = Value
        End Set
    End Property
    Public Property Cust_User_Id() As Integer
        Get
            Return m_cust_user_id
        End Get
        Set(ByVal Value As Integer)
            m_cust_user_id = Value
        End Set
    End Property
    Public Property Cust_Dist_Id() As Integer
        Get
            Return m_cust_dist_id
        End Get
        Set(ByVal Value As Integer)
            m_cust_dist_id = Value
        End Set
    End Property
    Public Property Cust_Date_Changes() As Date
        Get
            Return m_cust_date_changes
        End Get
        Set(ByVal Value As Date)
            m_cust_date_changes = Value
        End Set
    End Property
    Public Property CustomerGroupDescription() As String
        Get
            Return m_str__customer_group_description
        End Get
        Set(ByVal Value As String)
            m_str__customer_group_description = Value
        End Set
    End Property
    Public Property CustomerGroupCode() As String
        Get
            Return m_str_customer_group_code
        End Get
        Set(ByVal Value As String)
            m_str_customer_group_code = Value
        End Set
    End Property

End Class

