Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities
'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
''' <summary><para>Provides DataAccess for Wyeth SalesRep/para></summary>
Public Class WyethSalesRep

	Private m_i_sare_id As Integer
	Private m_str_sare_no As String
	Private m_str_sare_first_name As String
	Private m_str_sare_last_name As String
	Private m_str_user_id As Integer
	Private m_i_code_id_sales_rep_type As Integer
	Private m_i_ctry_id As Integer
	Private m_str_tapg_description As String
	Private m_i_tapg_id As Integer
	Private m_i_cust_id As Integer
    Private m_d_cust_percent As Double
    Private m_sare_short_name As String

	Public Function insert() As Boolean

		Dim MyCmd As New OracleCommand
		Dim Conn As New MyConnection

		Try
			MyCmd.CommandText = "P_Sales_Rep_Ins_proc"
			MyCmd.CommandType = CommandType.StoredProcedure


			MyCmd.Parameters.Add("v_sare_number", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = SalesRepNo
			MyCmd.Parameters.Add("v_sare_first_name", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = SalesRepFirstName
			MyCmd.Parameters.Add("v_sare_last_name", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = SalesRepLastName
			MyCmd.Parameters.Add("v_sare_user_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = SalesRepUserId
			MyCmd.Parameters.Add("v_Code_ID_Sales_Rep_Type", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = SalesRepCodeTypeId
			MyCmd.Parameters.Add("v_Ctry_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = SalesRepCtryId
            MyCmd.Parameters.Add("v_short_name", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = SalesRepShortName


			MyCmd.Connection = Conn.Open()
			MyCmd.ExecuteNonQuery()


		Catch ex As Exception
			ExceptionInfo.Show(ex)
			Return False
		Finally
			Conn.Close()
		End Try
		Return True
	End Function

	Public Function delete() As Boolean

		Dim MyCmd As New OracleCommand
		Dim Conn As New MyConnection


		Try
			MyCmd.CommandText = "P_Sales_Rep_del_proc"
			MyCmd.CommandType = CommandType.StoredProcedure

			MyCmd.Parameters.Add("v_sare_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = SalesRepId
			MyCmd.Connection = Conn.Open()
			MyCmd.ExecuteNonQuery()

		Catch ex As Exception
			ExceptionInfo.Show(ex)
			Return False
		Finally
			Conn.Close()
		End Try
		Return True
	End Function
	Public Function Update() As Boolean

		Dim MyCmd As New OracleCommand
		Dim Conn As New MyConnection

		Try
			MyCmd.CommandText = "P_Sales_Rep_Upd_proc"
			MyCmd.CommandType = CommandType.StoredProcedure

			MyCmd.Parameters.Add("v_sare_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = SalesRepId
			MyCmd.Parameters.Add("v_sare_number", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = SalesRepNo
			MyCmd.Parameters.Add("v_sare_first_name", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = SalesRepFirstName
			MyCmd.Parameters.Add("v_sare_last_name", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = SalesRepLastName
			MyCmd.Parameters.Add("v_sare_user_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = SalesRepUserId
			MyCmd.Parameters.Add("v_Code_ID_Sales_Rep_Type", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = SalesRepCodeTypeId
			MyCmd.Parameters.Add("v_Ctry_ID", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = SalesRepCtryId
            MyCmd.Parameters.Add("v_short_name", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = SalesRepShortName


			MyCmd.Connection = Conn.Open()
			MyCmd.ExecuteNonQuery()



		Catch ex As Exception
			ExceptionInfo.Show(ex)
			Return False
		Finally
			Conn.Close()
		End Try
		Return True
	End Function

	Public Function GetSalesReps() As DataView

		Dim MyCmd As New OracleCommand
		Dim Conn As New MyConnection
		Dim MyDataView As New DataView
		Dim MyDs As New DataSet
		Dim MyAdapter As New OracleDataAdapter

		Try
			MyCmd.Parameters.Clear()
			MyCmd.Parameters.Add("SalesReps", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
			MyCmd.Parameters.Add("v_Ctry_ID", OracleDbType.Int32, ParameterDirection.Input).Value = m_i_ctry_id
			MyCmd.CommandText = "PKG_APPLICATION.GetSalesReps"
			MyCmd.CommandType = CommandType.StoredProcedure
			MyCmd.Connection = Conn.Open()
			MyAdapter.SelectCommand = MyCmd
			MyAdapter.Fill(MyDs, "SaRe")
			MyDataView = MyDs.Tables("SaRe").DefaultView

			Return MyDataView
		Catch ex As Exception
			ExceptionInfo.Show(ex)
		Finally
			Conn.Close()
		End Try

	End Function

	Public Function GetTargetProductGroups() As DataView

		Dim MyCmd As New OracleCommand
		Dim Conn As New MyConnection
		Dim MyDataView As New DataView
		Dim MyDs As New DataSet
		Dim MyAdapter As New OracleDataAdapter
		Try

			MyCmd.Parameters.Clear()
            MyCmd.CommandText = "PKG_PRODUCT.GetTargetPrGr"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Add("TargetProductGroups", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
			MyCmd.Parameters.Add("v_Ctry_ID", OracleDbType.Int32, ParameterDirection.Input).Value = m_i_ctry_id
			MyCmd.Connection = Conn.Open()
			MyAdapter.SelectCommand = MyCmd
			MyAdapter.Fill(MyDs, "TaPg")
			MyDataView = MyDs.Tables("TaPg").DefaultView

			Return MyDataView
		Catch ex As Exception
			ExceptionInfo.Show(ex)
		Finally
			Conn.Close()

		End Try
	End Function
    Public Function GetIntranetUsers(ByVal v_ctry_id As Integer) As DataView

        Dim MyCmd As New OracleCommand
        Dim Conn As New MyConnection
        Dim MyDataView As New DataView
        Dim MyDs As New DataSet
        Dim MyAdapter As New OracleDataAdapter
        Try

            MyCmd.Parameters.Clear()
            MyCmd.CommandText = "PKG_Application.GetIntranetUsers"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Add("IntranetUsers", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_Ctry_ID", OracleDbType.Int32, ParameterDirection.Input).Value = v_ctry_id
            MyCmd.Connection = Conn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "IntranetUsers")
            MyDataView = MyDs.Tables("IntranetUsers").DefaultView

            Return MyDataView
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Conn.Close()

        End Try
    End Function

    Public Function UpdateTaPg() As Boolean
        Dim MyCmd As New OracleCommand
        Dim Conn As New MyConnection

        Try
            MyCmd.CommandText = "P_Target_PG_Upd_Proc"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_tapg_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = TaPgId
            MyCmd.Parameters.Add("v_tapg_description", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = TaPgDescription
            MyCmd.Parameters.Add("v_tapg_user_id", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = SalesRepUserId


            MyCmd.Connection = Conn.Open()
            MyCmd.ExecuteNonQuery()


            Conn.Close()
        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Return False
        End Try
        Return True
    End Function
    Public Function insertTaPg() As Boolean

        Dim MyCmd As New OracleCommand
        Dim Conn As New MyConnection


        Try
            MyCmd.CommandText = "P_Target_PG_Ins_Proc"
            MyCmd.CommandType = CommandType.StoredProcedure


            MyCmd.Parameters.Add("v_tapg_description", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = TaPgDescription
            MyCmd.Parameters.Add("v_tapg_user_id", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = SalesRepUserId
            MyCmd.Parameters.Add("ctry_id", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Input).Value = SalesRepCtryId
            MyCmd.Connection = Conn.Open()
            MyCmd.ExecuteNonQuery()



        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Return False
        Finally
            Conn.Close()
        End Try

        Return True

    End Function
    Public Function deleteTaPg() As Boolean

        Dim MyCmd As New OracleCommand
        Dim Conn As New MyConnection

        Try
            MyCmd.CommandText = "P_Target_PG_del_Proc"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Add("v_tapg_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = TaPgId
            MyCmd.Connection = Conn.Open()
            MyCmd.ExecuteNonQuery()

        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Return False
        Finally
            Conn.Close()
        End Try

        Return True
    End Function

    Public Function GetCustSare() As DataView

        Dim MyCmd As New OracleCommand
        Dim Conn As New MyConnection
        Dim MyDataView As New DataView
        Dim MyDs As New DataSet
        Dim MyAdapter As New OracleDataAdapter

        Try
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("cusr", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_sare_ID", OracleDbType.Int32, ParameterDirection.Input).Value = SalesRepId
            MyCmd.CommandText = "PKG_APPLICATION.GetCuSr"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Connection = Conn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "cusr")
            MyDataView = MyDs.Tables("cusr").DefaultView
            Conn.Close()

            Return MyDataView
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Conn.Close()

        End Try
    End Function
    Public Function InsertCustSare() As Boolean
        Dim MyCmd As New OracleCommand
        Dim Conn As New MyConnection

        Try
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_sare_ID", OracleDbType.Int32, ParameterDirection.Input).Value = SalesRepId
            MyCmd.Parameters.Add("v_cust_ID", OracleDbType.Int32, ParameterDirection.Input).Value = SalesRepCust
            MyCmd.Parameters.Add("v_cust_percent_ID", OracleDbType.Double, ParameterDirection.Input).Value = SalesRepCustPercent
            MyCmd.Parameters.Add("v_user_ID", OracleDbType.Int32, ParameterDirection.Input).Value = SalesRepUserId

            MyCmd.CommandText = "P_Cust_Sales_Rep_Ins_Proc"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Connection = Conn.Open()

            MyCmd.ExecuteNonQuery()
            Conn.Close()
            Return True
        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Return False
        Finally
            Conn.Close()

        End Try
    End Function
    Public Function UpdateCustSare() As Boolean

        Dim MyCmd As New OracleCommand
        Dim Conn As New MyConnection

        Try
            MyCmd.Parameters.Clear()

            MyCmd.Parameters.Add("v_cust_ID", OracleDbType.Int32, ParameterDirection.Input).Value = SalesRepCust
            MyCmd.Parameters.Add("v_sare_ID", OracleDbType.Int32, ParameterDirection.Input).Value = SalesRepId
            MyCmd.Parameters.Add("v_cust_percent_ID", OracleDbType.Double, ParameterDirection.Input).Value = CDbl(SalesRepCustPercent)
            MyCmd.Parameters.Add("v_user_ID", OracleDbType.Int32, ParameterDirection.Input).Value = SalesRepUserId

            MyCmd.CommandText = "P_Cust_Sales_Rep_Upd_Proc"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Connection = Conn.Open()

            MyCmd.ExecuteNonQuery()

            Return True
        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Return False
        Finally
            Conn.Close()
        End Try
    End Function
    Public Function deleteCusSare() As Boolean
        Dim MyCmd As New OracleCommand
        Dim Conn As New MyConnection

        Try
            MyCmd.CommandText = "P_Cust_Sales_Rep_Del_Proc"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Add("v_sare_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = SalesRepId
            MyCmd.Parameters.Add("v_cust_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = SalesRepCust
            MyCmd.Connection = Conn.Open()
            MyCmd.ExecuteNonQuery()

            Return True
        Catch ex As Exception
            ExceptionInfo.Show(ex)
            Return False
        Finally
            Conn.Close()
        End Try
        Return True
    End Function

    Public Property SalesRepCtryId() As Integer
        Get
            Return m_i_ctry_id
        End Get
        Set(ByVal Value As Integer)
            m_i_ctry_id = Value
        End Set
    End Property
    Public Property SalesRepCustPercent() As Double
        Get
            Return m_d_cust_percent
        End Get
        Set(ByVal Value As Double)
            m_d_cust_percent = Value
        End Set
    End Property
    Public Property SalesRepCust() As Integer
        Get
            Return m_i_cust_id
        End Get
        Set(ByVal Value As Integer)
            m_i_cust_id = Value
        End Set
    End Property
    Public Property SalesRepCodeTypeId() As Integer
        Get
            Return m_i_code_id_sales_rep_type
        End Get
        Set(ByVal Value As Integer)
            m_i_code_id_sales_rep_type = Value
        End Set
    End Property
    Public Property SalesRepUserId() As Integer
        Get
            Return m_str_user_id
        End Get
        Set(ByVal Value As Integer)
            m_str_user_id = Value
        End Set
    End Property
    Public Property SalesRepId() As Integer
        Get
            Return m_i_sare_id
        End Get
        Set(ByVal Value As Integer)
            m_i_sare_id = Value
        End Set
    End Property
    Public Property SalesRepLastName() As String
        Get
            Return m_str_sare_last_name
        End Get
        Set(ByVal Value As String)
            m_str_sare_last_name = Value
        End Set
    End Property

    Public Property SalesRepShortName() As String
        Get
            Return m_sare_short_name
        End Get
        Set(ByVal Value As String)
            m_sare_short_name = Value
        End Set
    End Property
    Public Property SalesRepFirstName() As String
        Get
            Return m_str_sare_first_name
        End Get
        Set(ByVal Value As String)
            m_str_sare_first_name = Value
        End Set
    End Property
    Public Property SalesRepNo() As String
        Get
            Return m_str_sare_no
        End Get
        Set(ByVal Value As String)
            m_str_sare_no = Value
        End Set
    End Property
    Public Property TaPgDescription() As String
        Get
            Return m_str_tapg_description
        End Get
        Set(ByVal Value As String)
            m_str_tapg_description = Value
        End Set
    End Property
    Public Property TaPgId() As Integer
        Get
            Return m_i_tapg_id
        End Get
        Set(ByVal Value As Integer)
            m_i_tapg_id = Value
        End Set
    End Property
End Class
