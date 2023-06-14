Option Strict On
Option Explicit On 

Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities

'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
''' <summary><para>Provides DataAccess for T_product</para></summary>
Public Class WyethProduct
	
	Dim MyCmd As New OracleCommand
	

	


	Private Sub initialize(ByVal type As String)
		If type = "upd" Then
			MyCmd.Parameters.Add("v_Prod_ID", OracleDbType.Int32, ParameterDirection.Input).Value = m_i_prod_id		  'v_Prod_ID                     IN NUMBER,
		End If

		MyCmd.Parameters.Add("v_Prod_PHZNr", OracleDbType.Varchar2, ParameterDirection.Input).Value = m_str_PhzNr	   'v_Prod_PHZNr                  IN VARCHAR2,
		MyCmd.Parameters.Add("v_Prod_Description", OracleDbType.Varchar2, ParameterDirection.Input).Value = m_str_Description	   'v_Prod_Description            IN VARCHAR2,
		MyCmd.Parameters.Add("v_Prod_Presentation", OracleDbType.Varchar2, ParameterDirection.Input).Value = m_str_Presentation	   'v_Prod_Presentation           IN VARCHAR2,
		MyCmd.Parameters.Add("v_Prod_Packsize", OracleDbType.Varchar2, ParameterDirection.Input).Value = m_str_Packsize	  'v_Prod_Packsize               IN VARCHAR2,
        MyCmd.Parameters.Add("v_Prod_Strength", OracleDbType.Varchar2, ParameterDirection.Input).Value = m_str_Strength  'v_Prod_Strength               IN VARCHAR2,
        MyCmd.Parameters.Add("v_Prod_Units_Measure", OracleDbType.Varchar2, ParameterDirection.Input).Value = m_i_UnitMeasure 'v_Prod_Units_Measure IN NUMBER,
        MyCmd.Parameters.Add("v_Prod_Bus_Unit ", OracleDbType.Varchar2, ParameterDirection.Input).Value = m_str_BusUnit    'v_Prod_Bus_Unit               IN VARCHAR2,
		MyCmd.Parameters.Add("v_Prod_Segment", OracleDbType.Varchar2, ParameterDirection.Input).Value = m_str_Segment		'v_Prod_Segment                IN VARCHAR2,
		MyCmd.Parameters.Add("v_Prod_SubSegment", OracleDbType.Varchar2, ParameterDirection.Input).Value = m_str_SubSegment	   'v_Prod_Sub_Segment            IN VARCHAR2,
		MyCmd.Parameters.Add("v_Prod_CC_ID ", OracleDbType.Int32, ParameterDirection.Input).Value = m_i_CCID	   'v_Prod_CC_ID                  IN NUMBER,
		MyCmd.Parameters.Add("v_Prod_CC_Description", OracleDbType.Varchar2, ParameterDirection.Input).Value = m_str_CCDescription	   'v_Prod_CC_Description         IN VARCHAR2,
		MyCmd.Parameters.Add("v_Prod_FAP", OracleDbType.Double, ParameterDirection.Input).Value = m_str_FAP	  'v_Prod_FAP                    IN NUMBER,
		MyCmd.Parameters.Add("v_Prod_WWS_Item_Code", OracleDbType.Varchar2, ParameterDirection.Input).Value = m_str_WWSitem_code	  'v_Prod_WWS_Item_Code          IN VARCHAR2,
		MyCmd.Parameters.Add("v_Prod_FSD", OracleDbType.Varchar2, ParameterDirection.Input).Value = m_str_FSD	  'v_Prod_FSD                    IN VARCHAR2,
		MyCmd.Parameters.Add("v_Prod_Plant_Item_Number", OracleDbType.Varchar2, ParameterDirection.Input).Value = m_str_plant_item_number	   'v_Prod_Plant_Item_Number      IN NUMBER,
		MyCmd.Parameters.Add("v_Prod_Manufacturer", OracleDbType.Varchar2, ParameterDirection.Input).Value = m_str_manufacturer	  'v_Prod_Manufacturer           IN VARCHAR2,
		MyCmd.Parameters.Add("v_Prod_MFGLocation", OracleDbType.Varchar2, ParameterDirection.Input).Value = m_str_mfglocation	   'v_Prod_MFGLocation            IN VARCHAR2,
		MyCmd.Parameters.Add("v_Prod_Packer_Code", OracleDbType.Varchar2, ParameterDirection.Input).Value = m_str_packer_code	   'v_Prod_Packer_Code            IN VARCHAR2,
		MyCmd.Parameters.Add("v_Prod_Invoicer_Code", OracleDbType.Varchar2, ParameterDirection.Input).Value = m_str_invoicer_code	  'v_Prod_Invoicer_Code          IN VARCHAR2,
		MyCmd.Parameters.Add("v_Prod_Routing", OracleDbType.Varchar2, ParameterDirection.Input).Value = m_str_routing	   'v_Prod_Routing                IN VARCHAR2,
		MyCmd.Parameters.Add("v_Prod_Info", OracleDbType.Varchar2, ParameterDirection.Input).Value = m_str_info	   'v_Prod_Info                   IN VARCHAR2,
		MyCmd.Parameters.Add("v_Prod_Forte_Product_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = m_i_forte_product_id	   'v_Prod_Forte_Product_ID       IN VARCHAR2,
		MyCmd.Parameters.Add("v_Prod_ForteTimestamp", OracleDbType.Varchar2, ParameterDirection.Input).Value = DBNull.Value		 'v_Prod_Forte_Timestamp        IN DATE,
		MyCmd.Parameters.Add("v_Prod_Status", OracleDbType.Varchar2, ParameterDirection.Input).Value = m_str_status	  'v_Prod_Status                 IN VARCHAR2,
		MyCmd.Parameters.Add("v_Prod_Obs_Code", OracleDbType.Varchar2, ParameterDirection.Input).Value = m_str_obs_code		'v_Prod_Obs_Code               IN VARCHAR2,
		MyCmd.Parameters.Add("v_Prod_User_ID", OracleDbType.Int32, ParameterDirection.Input).Value = m_i_user_id	   'v_Prod_User_ID                IN NUMBER,
		MyCmd.Parameters.Add("v_PrGr_ID", OracleDbType.Int32, ParameterDirection.Input).Value = m_i_prgr_id	   'v_PrGr_ID                     IN NUMBER,
        If ProdProdIDSample <> 0 Then
            MyCmd.Parameters.Add("v_Prod_ID_Sample_Product", OracleDbType.Int32, ParameterDirection.Input).Value = ProdProdIDSample   'v_Prod_ID_Sample_Product      IN NUMBER,
        Else
            MyCmd.Parameters.Add("v_Prod_ID_Sample_Product", OracleDbType.Int32, ParameterDirection.Input).Value = DBNull.Value    'v_Prod_ID_Sample_Product      IN NUMBER,

        End If
        MyCmd.Parameters.Add("v_Ctry_ID", OracleDbType.Int32, ParameterDirection.Input).Value = ProdCtryId   'v_Ctry_ID                     IN NUMBER,
        MyCmd.Parameters.Add("v_Curr_ID", OracleDbType.Int32, ParameterDirection.Input).Value = ProdCurrId      'v_Curr_ID                     IN NUMBER,
        MyCmd.Parameters.Add("v_Line_ID", OracleDbType.Int32, ParameterDirection.Input).Value = ProdLineId    'v_Line_ID                     IN NUMBER,
        If ProdTargetProductGroupId = 0 Then
            MyCmd.Parameters.Add("v_TaPG_ID", OracleDbType.Int32, ParameterDirection.Input).Value = DBNull.Value   'v_TaPG_ID                     IN NUMBER
        Else
            MyCmd.Parameters.Add("v_TaPG_ID", OracleDbType.Int32, ParameterDirection.Input).Value = ProdTargetProductGroupId   'v_TaPG_ID                     IN NUMBER
        End If



    End Sub


    Public Function insert() As Boolean
        Dim Conn As New MyConnection

        Try


            MyCmd.CommandText = "P_PRODUCT_INS_PROC"
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
    Public Function update() As Boolean

        Dim Conn As New MyConnection

        Try
            initialize("upd")

            MyCmd.CommandText = "P_PRODUCT_UPD_PROC"
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
    Public Function GetProducts(ByVal str_obs_code As String) As DataView

        Dim Conn As New MyConnection
        Dim MyDataView As DataView
        Dim MyDs As New DataSet
        Dim MyAdapter As New OracleDataAdapter

        Try


            MyCmd.CommandText = "PKG_PRODUCT.GetProducts"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Products", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_Ctry_ID", OracleDbType.Int32, ParameterDirection.Input).Value = ProdCtryId
            MyCmd.Parameters.Add("v_searchstring", OracleDbType.Varchar2, ParameterDirection.Input).Value = ProdSearchString
            MyCmd.Parameters.Add("v_line_id", OracleDbType.Int32, ParameterDirection.Input).Value = ProdLineId
            MyCmd.Parameters.Add("v_tapg_id", OracleDbType.Int32, ParameterDirection.Input).Value = ProdTargetProductGroupId
            MyCmd.Parameters.Add("v_obs_code", OracleDbType.Varchar2, ParameterDirection.Input).Value = str_obs_code

            MyCmd.Connection = Conn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Product")
            MyDataView = MyDs.Tables("Product").DefaultView
            Conn.Close()
            Return MyDataView
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Conn.Close()
        End Try

    End Function


    Public Function GetProductByID(ByVal prod_id As Integer) As OracleDataReader

        Dim Conn As New MyConnection
        Dim MyReader As OracleDataReader
        Try
            MyCmd.CommandText = "PKG_PRODUCT.GetProductByID"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Products", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_prod_ID", OracleDbType.RefCursor, ParameterDirection.Input).Value = prod_id
            MyCmd.Connection = Conn.Open()
            MyReader = MyCmd.ExecuteReader()
            Return MyReader
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally

        End Try
    End Function
    Public Function GetProductList() As DataView

        Dim Conn As New MyConnection
        Dim MyDataView As DataView
        Dim MyDs As New DataSet
        Dim MyAdapter As New OracleDataAdapter

        Try


            MyCmd.CommandText = "PKG_PRODUCT.GetProductList"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Products", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_line_ID", OracleDbType.Int32, ParameterDirection.Input).Value = ProdLineId
            MyCmd.Parameters.Add("v_Ctry_ID", OracleDbType.Int32, ParameterDirection.Input).Value = ProdCtryId
            If ObsCode <> "" Then
                MyCmd.Parameters.Add("v_obs_code", OracleDbType.Varchar2, ParameterDirection.Input).Value = ObsCode
            End If
            'MyCmd.Parameters.Add("v_obs_code", OracleDbType.Varchar2, ParameterDirection.Input).Value = DBNull.Value



            MyCmd.Connection = Conn.Open()

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Product")
            MyDataView = MyDs.Tables("Product").DefaultView

            Return MyDataView
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Conn.Close()
        End Try

    End Function
    Public Function GetNotAssignedSampleProducts() As DataView

        Dim Conn As New MyConnection
        Dim MyDataView As DataView
        Dim MyDs As New DataSet
        Dim MyAdapter As New OracleDataAdapter

        Try


            MyCmd.CommandText = "PKG_PRODUCT.GetNotAssignedSampleProducts"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Products", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_Ctry_ID", OracleDbType.Int32, ParameterDirection.Input).Value = ProdCtryId
            MyCmd.Connection = Conn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Product")
            MyDataView = MyDs.Tables("Product").DefaultView

            Return MyDataView
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Conn.Close()
        End Try

    End Function
    Public Function GetProductGroups() As DataView

        Dim Conn As New MyConnection
        Dim MyDataView As DataView
        Dim MyDs As New DataSet
        Dim MyAdapter As New OracleDataAdapter

        Try
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("ProductGroups", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, ParameterDirection.Input).Value = ProdCtryId
            MyCmd.CommandText = "PKG_PRODUCT.GetProductGroups"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Connection = Conn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "ProductGroups")
            MyDataView = MyDs.Tables("ProductGroups").DefaultView

            Return MyDataView
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Conn.Close()
        End Try
    End Function
    Public Function GetProductDescriptions(ByVal ctry_id As Integer, ByVal line_id As Integer, ByVal obs_code As String) As DataView

        Dim Conn As New MyConnection
        Dim MyDataView As DataView
        Dim MyDs As New DataSet
        Dim MyAdapter As New OracleDataAdapter

        Try
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("ProductGroups", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, ParameterDirection.Input).Value = ctry_id
            MyCmd.Parameters.Add("v_line_id", OracleDbType.Int32, ParameterDirection.Input).Value = line_id
            MyCmd.Parameters.Add("v_obs_code", OracleDbType.Varchar2, ParameterDirection.Input).Value = obs_code
            MyCmd.CommandText = "PKG_PRODUCT.GetProductDescriptions"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Connection = Conn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "ProductDescriptions")
            MyDataView = MyDs.Tables("ProductDescriptions").DefaultView

            Return MyDataView
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Conn.Close()
        End Try
    End Function

    Public Function GetLines() As DataView

        Dim Conn As New MyConnection
        Dim MyDataView As DataView
        Dim MyDs As New DataSet
        Dim MyAdapter As New OracleDataAdapter

        Try

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Product", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_Ctry_ID", OracleDbType.Int32, ParameterDirection.Input).Value = m_i_ctry_id
            MyCmd.CommandText = "PKG_PRODUCT.GetLines"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Connection = Conn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Product")

            MyDataView = MyDs.Tables("Product").DefaultView
            Conn.Close()
            Return MyDataView
        Catch ex As Exception
            ExceptionInfo.Show(ex)

        End Try
    End Function
    Public Function GetCountries() As DataView
        Dim Conn As New MyConnection
        Dim MyDataView As DataView
        Dim MyDs As New DataSet
        Dim MyAdapter As New OracleDataAdapter
        Try

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Product", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.CommandText = "PKG_PRODUCT.GetCountries"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Connection = Conn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Product")
            MyDataView = MyDs.Tables("Product").DefaultView
            Conn.Close()
            Return MyDataView
        Catch ex As Exception
            ExceptionInfo.Show(ex)

        End Try
    End Function
    Public Function GetCurrencies() As DataView
        Dim Conn As New MyConnection
        Dim MyDataView As DataView
        Dim MyDs As New DataSet
        Dim MyAdapter As New OracleDataAdapter
        Try

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Product", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.CommandText = "PKG_PRODUCT.GetCurrencies"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Connection = Conn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Product")
            MyDataView = MyDs.Tables("Product").DefaultView

            Return MyDataView
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Conn.Close()
        End Try
    End Function
    Public Function GetTargetPrGr() As DataView

        Dim Conn As New MyConnection
        Dim MyDataView As DataView
        Dim MyDs As New DataSet
        Dim MyAdapter As New OracleDataAdapter

        Try

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Product", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_Ctry_ID", OracleDbType.Int32, ParameterDirection.Input).Value = m_i_ctry_id
            MyCmd.CommandText = "PKG_PRODUCT.GetTargetPrGr"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Connection = Conn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Product")
            MyDataView = MyDs.Tables("Product").DefaultView

            Return MyDataView
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Conn.Close()

        End Try
    End Function
    Public Function GetInvoicer() As DataView

        Dim Conn As New MyConnection
        Dim MyDataView As DataView
        Dim MyDs As New DataSet
        Dim MyAdapter As New OracleDataAdapter

        Try

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("Invoicer", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_Ctry_ID", OracleDbType.Int32, ParameterDirection.Input).Value = ProdCtryId
            MyCmd.CommandText = "PKG_APPLICATION.GetInvoicer"
            MyCmd.CommandType = CommandType.StoredProcedure
            MyCmd.Connection = Conn.Open()
            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDs, "Invoicer")
            MyDataView = MyDs.Tables("Invoicer").DefaultView

            Return MyDataView
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Conn.Close()
        End Try
    End Function
    Public Function DeleteProduct() As Boolean
        Dim Conn As New MyConnection

        Try
            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_product_id", OracleDbType.Int32, ParameterDirection.Input).Value = m_i_prod_id
            MyCmd.CommandText = "PKG_PRODUCT.DeleteProduct"
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
    Public Function DeleteProductGroup() As Boolean
        Dim Conn As New MyConnection

        Try


            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_prgr_id", OracleDbType.Int32, ParameterDirection.Input).Value = m_i_prod_id
            MyCmd.CommandText = "P_Product_Group_Del_Proc"
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
    Public Function UpdateProductGroup() As Boolean
        Dim Conn As New MyConnection
        Dim MyDataView As DataView
        Dim MyDs As New DataSet
        Dim MyAdapter As New OracleDataAdapter

        Try


            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("v_PrGr_id", OracleDbType.Int32, ParameterDirection.Input).Value = ProdProductGroupID
            MyCmd.Parameters.Add("v_PrGr_Code", OracleDbType.Varchar2, ParameterDirection.Input).Value = ProdGroupCode
            MyCmd.Parameters.Add("v_PrGr_Description", OracleDbType.Varchar2, ParameterDirection.Input).Value = ProdGroupDescription
            MyCmd.Parameters.Add("v_PrGr_User_ID", OracleDbType.Int32, ParameterDirection.Input).Value = ProdUserID
            MyCmd.Parameters.Add("v_Ctry_ID", OracleDbType.Int32, ParameterDirection.Input).Value = ProdCtryId


            MyCmd.CommandText = "P_Product_Group_Upd_Proc"
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
    Public Function InsertProductGroup() As Boolean

        Dim Conn As New MyConnection

        Try

            MyCmd.Parameters.Clear()

            MyCmd.Parameters.Add("v_PrGr_Code", OracleDbType.Varchar2, ParameterDirection.Input).Value = ProdGroupCode
            MyCmd.Parameters.Add("v_PrGr_Description", OracleDbType.Varchar2, ParameterDirection.Input).Value = ProdGroupDescription
            MyCmd.Parameters.Add("v_PrGr_User_ID", OracleDbType.Int32, ParameterDirection.Input).Value = ProdUserID
            MyCmd.Parameters.Add("v_Ctry_ID", OracleDbType.Int32, ParameterDirection.Input).Value = ProdCtryId

            MyCmd.CommandText = "P_Product_Group_Ins_Proc"
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

    Public Function GetSamProductsWithActProductForMonth(ByVal v_month As Date, ByVal v_ctry_id As Integer) As DataView

        Dim MyCmd As New OracleCommand
        Dim Myconn As New MyConnection
        Dim MyDataView As New DataView
        Dim MyAdapter As New OracleDataAdapter
        Dim MyDS As New DataSet

        Try
            MyCmd.CommandText = "PKG_APPLICATION.GetSamProductsWithNoActual"
            MyCmd.CommandType = CommandType.StoredProcedure

            MyCmd.Parameters.Clear()
            MyCmd.Parameters.Add("products", OracleDbType.RefCursor, ParameterDirection.Output)
            MyCmd.Parameters.Add("v_month", OracleDbType.Date, DBNull.Value, ParameterDirection.Input).Value = v_month
            MyCmd.Parameters.Add("v_ctry_id", OracleDbType.Int32, DBNull.Value, ParameterDirection.Input).Value = v_ctry_id

            MyCmd.Connection = Myconn.Open()

            MyAdapter.SelectCommand = MyCmd
            MyAdapter.Fill(MyDS, "StockData")
            MyDataView = MyDS.Tables("StockData").DefaultView

            Return MyDataView
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Myconn.Close()

        End Try
    End Function


    Public Property ProdTargetProductGroupId() As Integer
        Get
            Return m_i_tapg_id
        End Get
        Set(ByVal Value As Integer)
            m_i_tapg_id = Value
        End Set
    End Property
    Public Property ProdUnitMeasure() As Integer
        Get
            Return m_i_UnitMeasure
        End Get
        Set(ByVal Value As Integer)
            m_i_UnitMeasure = Value
        End Set
    End Property
    Public Property ProdLineId() As Integer
        Get
            Return m_i_line_id
        End Get
        Set(ByVal Value As Integer)
            m_i_line_id = Value
        End Set
    End Property
    Public Property ProdCurrId() As Integer
        Get
            Return m_i_curr_id
        End Get
        Set(ByVal Value As Integer)
            m_i_curr_id = Value
        End Set
    End Property
    Public Property ProdCtryId() As Integer
        Get
            Return m_i_ctry_id
        End Get
        Set(ByVal Value As Integer)
            m_i_ctry_id = Value
        End Set
    End Property
    Public Property ProdProdIDSample() As Integer
        Get
            Return m_i_prod_id_sample_product
        End Get
        Set(ByVal Value As Integer)
            If Value = 0 Then Value = Nothing
            m_i_prod_id_sample_product = Value
        End Set
    End Property
    Public Property ProdProductGroupID() As Integer
        Get
            Return m_i_prgr_id
        End Get
        Set(ByVal Value As Integer)
            m_i_prgr_id = Value
        End Set
    End Property
    Public Property ProdUserID() As Integer
        Get
            Return m_i_user_id
        End Get
        Set(ByVal Value As Integer)
            m_i_user_id = Value
        End Set
    End Property
    Public Property ObsCode() As String
        Get
            Return m_str_obs_code
        End Get
        Set(ByVal Value As String)
            m_str_obs_code = Value
        End Set
    End Property
    Public Property ObsCodeID() As Integer
        Get
            Return m_i_obs_code_id
        End Get
        Set(ByVal Value As Integer)
            m_i_obs_code_id = Value
        End Set
    End Property
    Public Property ProdStatus() As String
        Get
            Return m_str_status
        End Get
        Set(ByVal Value As String)
            m_str_status = Value
        End Set
    End Property
    Public Property ProdSearchString() As String
        Get
            Return m_str_search
        End Get
        Set(ByVal Value As String)
            m_str_search = Value
        End Set
    End Property
    Public Property ProdForteProdID() As String
        Get
            Return m_i_forte_product_id
        End Get
        Set(ByVal Value As String)
            m_i_forte_product_id = Value
        End Set
    End Property
    Public Property ProdInfo() As String
        Get
            Return m_str_info
        End Get
        Set(ByVal Value As String)
            m_str_info = Value
        End Set
    End Property
    Public Property ProdRouting() As String
        Get
            Return m_str_routing
        End Get
        Set(ByVal Value As String)
            m_str_routing = Value
        End Set
    End Property
    Public Property ProdInvoicerCode() As String
        Get
            Return m_str_invoicer_code
        End Get
        Set(ByVal Value As String)
            m_str_invoicer_code = Value
        End Set
    End Property
    Public Property ProdPackerCode() As String
        Get
            Return m_str_packer_code
        End Get
        Set(ByVal Value As String)
            m_str_packer_code = Value
        End Set
    End Property
    Public Property ProdMFGLocation() As String
        Get
            Return m_str_mfglocation
        End Get
        Set(ByVal Value As String)
            m_str_mfglocation = Value
        End Set
    End Property
    Public Property ProdManufacturer() As String
        Get
            Return m_str_manufacturer
        End Get
        Set(ByVal Value As String)
            m_str_manufacturer = Value
        End Set
    End Property
    Public Property ProdPlantItemNo() As String
        Get
            Return m_str_plant_item_number
        End Get
        Set(ByVal Value As String)
            m_str_plant_item_number = Value
        End Set
    End Property
    Public Property ProdmFSD() As String
        Get
            Return m_str_FSD
        End Get
        Set(ByVal Value As String)
            m_str_FSD = Value
        End Set
    End Property
    Public Property ProdItemCode() As String
        Get
            Return m_str_WWSitem_code
        End Get
        Set(ByVal Value As String)
            m_str_WWSitem_code = Value
        End Set
    End Property
    Public Property ProdFAP() As Double
        Get
            Return m_str_FAP
        End Get
        Set(ByVal Value As Double)
            m_str_FAP = Value
        End Set
    End Property
    Public Property ProdCCDescription() As String
        Get
            Return m_str_CCDescription
        End Get
        Set(ByVal Value As String)
            m_str_CCDescription = Value
        End Set
    End Property
    Public Property ProdCCID() As Integer
        Get
            Return m_i_CCID
        End Get
        Set(ByVal Value As Integer)
            m_i_CCID = Value
        End Set
    End Property
    Public Property ProdSubSegment() As String
        Get
            Return m_str_SubSegment
        End Get
        Set(ByVal Value As String)
            m_str_SubSegment = Value
        End Set
    End Property
    Public Property ProdSegment() As String
        Get
            Return m_str_Segment
        End Get
        Set(ByVal Value As String)
            m_str_Segment = Value
        End Set
    End Property
    Public Property ProdBusUnit() As String
        Get
            Return m_str_BusUnit
        End Get
        Set(ByVal Value As String)
            m_str_BusUnit = Value
        End Set
    End Property
    Public Property ProdID() As Integer
        Get
            Return m_i_prod_id
        End Get
        Set(ByVal Value As Integer)
            m_i_prod_id = Value
        End Set
    End Property
    Public Property PhzNr() As String
        Get
            Return m_str_PhzNr
        End Get
        Set(ByVal Value As String)
            m_str_PhzNr = Value
        End Set
    End Property
    Public Property ProdDescription() As String
        Get
            Return m_str_Description
        End Get
        Set(ByVal Value As String)
            m_str_Description = Value
        End Set
    End Property
    Public Property ProdPresentation() As String
        Get
            Return m_str_Presentation
        End Get
        Set(ByVal Value As String)
            m_str_Presentation = Value
        End Set
    End Property
    Public Property ProdPackSize() As String
        Get
            Return m_str_Packsize
        End Get
        Set(ByVal Value As String)
            m_str_Packsize = Value
        End Set
    End Property
    Public Property ProdStrength() As String
        Get
            Return m_str_Strength
        End Get
        Set(ByVal Value As String)
            m_str_Strength = Value
        End Set
    End Property
    Public Property ProdGroupCode() As String
        Get
            Return m_str_prod_group_code
        End Get
        Set(ByVal Value As String)
            m_str_prod_group_code = Value
        End Set
    End Property
    Public Property ProdGroupDescription() As String
        Get
            Return m_str_prod_group_description
        End Get
        Set(ByVal Value As String)
            m_str_prod_group_description = Value
        End Set
    End Property


    Private m_i_prod_id As Integer
    Private m_str_PhzNr As String
    Private m_str_Description As String
    Private m_str_Presentation As String
    Private m_str_Packsize As String
    Private m_str_Strength As String
    Private m_i_UnitMeasure As Integer
    Private m_str_BusUnit As String
    Private m_str_Segment As String
    Private m_str_SubSegment As String
    Private m_i_CCID As Integer
    Private m_str_CCDescription As String
    Private m_str_FAP As Double
    Private m_str_WWSitem_code As String
    Private m_str_FSD As String
    Private m_str_plant_item_number As String
    Private m_str_manufacturer As String
    Private m_str_mfglocation As String
    Private m_str_packer_code As String
    Private m_str_invoicer_code As String
    Private m_str_routing As String
    Private m_str_info As String
    Private m_i_forte_product_id As String
    Private m_str_status As String
    Private m_str_obs_code As String
    Private m_i_user_id As Integer
    Private m_i_prgr_id As Integer
    Private m_i_prod_id_sample_product As Integer
    Private m_i_ctry_id As Integer
    Private m_i_curr_id As Integer
    Private m_i_line_id As Integer
    Private m_i_tapg_id As Integer
    Private m_str_prod_group_description As String
    Private m_str_prod_group_code As String
    Private m_str_search As String
    Private m_i_obs_code_id As Integer

End Class
