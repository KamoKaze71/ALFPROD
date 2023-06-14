Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities
'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
''' <summary><para>Provides DataAccess for Salerep Target customer relations </para></summary>
Public Class TargetProductGroup

    Private p_countryID As Integer
    Private p_targetProductGroupID As Integer
    Private p_userID As Integer
    Private dataBase As New DataAccessBaseClass

    '****************************************************************************************************
    '* P R O P E R T I E S 
    '****************************************************************************************************
    Public Property countryID() As Integer
        Get
            Return p_countryID
        End Get
        Set(ByVal Value As Integer)
            p_countryID = Value
        End Set
    End Property

    Public Property userID() As Integer
        Get
            Return p_userID
        End Get
        Set(ByVal Value As Integer)
            p_userID = Value
        End Set
    End Property

    Public Property targetProductGroupID() As Integer
        Get
            Return p_targetProductGroupID
        End Get
        Set(ByVal Value As Integer)
            p_targetProductGroupID = Value
        End Set
    End Property

    '****************************************************************************************************
    '* returns all TARGET PRODUCT GROUPS 
    '****************************************************************************************************
    Public Function getList() As DataView
        Dim parameters(1) As OracleParameter

        parameters(0) = New OracleParameter("TargetProductGroups", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)

        Dim param As New OracleParameter("v_Ctry_ID", OracleDbType.Int32, ParameterDirection.Input)
        param.Value = Me.countryID
        parameters(1) = param

        Return dataBase.executeStoredProcedure("PKG_TARGET_PRODUCTGROUP.GetTargetProductGroup", parameters)
    End Function

    '****************************************************************************************************
    '* returns all PRODUCTS for a specified Target product group 
    '****************************************************************************************************
    Public Function getProducts() As DataView
        Dim parameters(1) As OracleParameter

        parameters(0) = New OracleParameter("Products", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)

        Dim param As New OracleParameter("v_tapg_id", OracleDbType.Int32, ParameterDirection.Input)
        param.Value = Me.targetProductGroupID
        parameters(1) = param

        Return dataBase.executeStoredProcedure("PKG_TARGET_PRODUCTGROUP.GetProductsForTPG", parameters)
    End Function

    '****************************************************************************************************
    '* returns all SALES REPS for a specified Target Product Group (TPG) 
    '****************************************************************************************************
    Public Function getSalesReps() As DataView
        Dim parameters(1) As OracleParameter

        parameters(0) = New OracleParameter("SalesReps", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)

        Dim param As New OracleParameter("v_tapg_id", OracleDbType.Int32, ParameterDirection.Input)
        param.Value = Me.targetProductGroupID
        parameters(1) = param

        Return dataBase.executeStoredProcedure("PKG_TARGET_PRODUCTGROUP.GetSalesRepsForTPG", parameters)
    End Function

    '****************************************************************************************************
    '* returns all CUSTOMERS for a specified TGP 
    '****************************************************************************************************
    Public Function getCustomers() As DataView
        Dim parameters(1) As OracleParameter

        parameters(0) = New OracleParameter("Customers", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)

        Dim param As New OracleParameter("v_tapg_id", OracleDbType.Int32, ParameterDirection.Input)
        param.Value = Me.targetProductGroupID
        parameters(1) = param

        Return dataBase.executeStoredProcedure("PKG_TARGET_PRODUCTGROUP.GetCustomersandSalesRepsForTPG", parameters)
    End Function

    '****************************************************************************************************
    '* returns all UNASSIGNED PRODUCTS  
    '****************************************************************************************************
    Public Function getUnassignedProducts() As DataView
        Dim parameters(0) As OracleParameter
        parameters(0) = New OracleParameter("Products", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        Return dataBase.executeStoredProcedure("PKG_TARGET_PRODUCTGROUP.GetunassignedProducts", parameters)
    End Function

    '****************************************************************************************************
    '* returns all UNASSIGNED PRODUCTS for a specified LINE 
    '****************************************************************************************************
    Public Function getUnassignedProductsForLine(ByVal lineID As Integer, ByVal showAlsoObsoleteProducts As Boolean) As DataView
        Dim parameters(2) As OracleParameter

        parameters(0) = New OracleParameter("Products", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)

        Dim param As New OracleParameter("v_line_id", OracleDbType.Int32, ParameterDirection.Input)
        param.Value = lineID
        parameters(1) = param

        Dim param2 As New OracleParameter("v_obs", OracleDbType.Varchar2, ParameterDirection.Input)
        param2.Value = showAlsoObsoleteProducts.ToString.ToUpper
        parameters(2) = param2

        Return dataBase.executeStoredProcedure("PKG_TARGET_PRODUCTGROUP.GetUnassignedProductsForLine", parameters)
    End Function

    '****************************************************************************************************
    '* returns a specified PRODUCT  
    '****************************************************************************************************
    Public Function getProductByID(ByVal prod_id As Integer) As DataView
        Dim parameters(1) As OracleParameter

        parameters(0) = New OracleParameter("Product", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)

        Dim param As New OracleParameter("v_prod_ID", OracleDbType.Int32, ParameterDirection.Input)
        param.Value = prod_id
        parameters(1) = param

        Return dataBase.executeStoredProcedure("PKG_PRODUCT.GetProductByID", parameters)
    End Function

    '****************************************************************************************************
    '* adds a PRODUCT to specified TPG  
    '****************************************************************************************************
    Public Sub addProduct(ByVal prod_id As Integer)
        Dim parameters(2) As OracleParameter

        Dim param As New OracleParameter("v_tapg_id", OracleDbType.Int32, ParameterDirection.Input)
        param.Value = Me.targetProductGroupID
        parameters(0) = param

        Dim param2 As New OracleParameter("v_prod_id", OracleDbType.Int32, ParameterDirection.Input)
        param2.Value = prod_id
        parameters(1) = param2

        Dim param3 As New OracleParameter("v_user_id", OracleDbType.Int32, ParameterDirection.Input)
        param3.Value = Me.userID
        parameters(2) = param3

        dataBase.executeNonQuery("PKG_TARGET_PRODUCTGROUP.AddProducttoTPG", parameters)
    End Sub

    '****************************************************************************************************
    '* removes a PRODUCT from a TPG  
    '****************************************************************************************************
    Public Sub deleteProduct(ByVal prod_id As Integer)
        Dim parameters(2) As OracleParameter

        Dim param As New OracleParameter("v_tapg_id", OracleDbType.Int32, ParameterDirection.Input)
        param.Value = Me.targetProductGroupID
        parameters(0) = param

        Dim param2 As New OracleParameter("v_prod_id", OracleDbType.Int32, ParameterDirection.Input)
        param2.Value = prod_id
        parameters(1) = param2

        Dim param3 As New OracleParameter("v_user_id", OracleDbType.Int32, ParameterDirection.Input)
        param3.Value = Me.userID
        parameters(2) = param3

        dataBase.executeNonQuery("PKG_TARGET_PRODUCTGROUP.DeleteProductofTPG", parameters)
    End Sub

    '****************************************************************************************************
    '* returns all SalesReps 
    '****************************************************************************************************
    Public Function getAllSalesReps() As DataView
        Dim parameters(1) As OracleParameter

        parameters(0) = New OracleParameter("Product", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)

        Dim param1 As New OracleParameter("v_tapg_id", OracleDbType.Int32, ParameterDirection.Input)
        param1.Value = Me.targetProductGroupID
        parameters(1) = param1

        Return dataBase.executeStoredProcedure("PKG_TARGET_PRODUCTGROUP.GetAllUnasSalesRepsforTPG", parameters)
    End Function

    Public Function getAllSalesRepsForTPG() As DataView
        Dim parameters(1) As OracleParameter

        parameters(0) = New OracleParameter("Product", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)

        Dim param1 As New OracleParameter("v_tapg_id", OracleDbType.Int32, ParameterDirection.Input)
        param1.Value = Me.targetProductGroupID
        parameters(1) = param1

        Return dataBase.executeStoredProcedure("PKG_TARGET_PRODUCTGROUP.GetSalesRepsForTPG", parameters)
    End Function


    '****************************************************************************************************
    '* returns a specified PRODUCT  
    '****************************************************************************************************
    Public Function GetAllSalesRepByID(ByVal sare_id As Integer) As DataView
        Dim parameters(1) As OracleParameter

        parameters(0) = New OracleParameter("Sare", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)

        Dim param As New OracleParameter("v_sare_id", OracleDbType.Int32, ParameterDirection.Input)
        param.Value = sare_id
        parameters(1) = param

        Return dataBase.executeStoredProcedure("PKG_TARGET_PRODUCTGROUP.GetAllSalesRepByID", parameters)
    End Function

    '****************************************************************************************************
    '* adds a SALESREP to specified TPG  
    '****************************************************************************************************
    Public Sub addSalesRep(ByVal sareID As Integer)
        Dim parameters(2) As OracleParameter

        Dim param As New OracleParameter("v_tapg_id", OracleDbType.Int32, ParameterDirection.Input)
        param.Value = Me.targetProductGroupID
        parameters(0) = param

        Dim param2 As New OracleParameter("v_sare_id", OracleDbType.Int32, ParameterDirection.Input)
        param2.Value = sareID
        parameters(1) = param2

        Dim param3 As New OracleParameter("v_user_id", OracleDbType.Int32, ParameterDirection.Input)
        param3.Value = Me.userID
        parameters(2) = param3

        dataBase.executeNonQuery("PKG_TARGET_PRODUCTGROUP.AddSalesReptoTPG", parameters)
    End Sub

    '****************************************************************************************************
    '* removes a SALESREP from TPG  
    '****************************************************************************************************
    Public Sub removeSalesRep(ByVal sare_id As Integer)
        Dim parameters(1) As OracleParameter

        Dim param As New OracleParameter("v_tapg_id", OracleDbType.Int32, ParameterDirection.Input)
        param.Value = Me.targetProductGroupID
        parameters(0) = param

        Dim param2 As New OracleParameter("v_sare_id", OracleDbType.Int32, ParameterDirection.Input)
        param2.Value = sare_id
        parameters(1) = param2

        dataBase.executeNonQuery("PKG_TARGET_PRODUCTGROUP.DeleteSalesRepofTPG", parameters)
    End Sub

    '****************************************************************************************************
    '* gets all data of a specified TPG  
    '****************************************************************************************************
    Public Function getTPGByID(ByVal tpgID As Integer) As DataView
        Dim parameters(1) As OracleParameter

        parameters(0) = New OracleParameter("Tpg", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)

        Dim param As New OracleParameter("v_tapg_id", OracleDbType.Int32, ParameterDirection.Input)
        param.Value = tpgID
        parameters(1) = param

        Return dataBase.executeStoredProcedure("PKG_TARGET_PRODUCTGROUP.GetTPGByID", parameters)
    End Function

    '****************************************************************************************************
    '* renames a TPG  
    '****************************************************************************************************
    Public Sub renameTPG(ByVal newDescription As String, ByVal type_id As Integer)
        Dim parameters(3) As OracleParameter

        Dim param As New OracleParameter("v_tp_name", OracleDbType.Varchar2, ParameterDirection.Input)
        param.Value = newDescription.ToUpper
        parameters(0) = param

        Dim param2 As New OracleParameter("v_tapg_id", OracleDbType.Int32, ParameterDirection.Input)
        param2.Value = Me.targetProductGroupID
        parameters(1) = param2

        Dim param3 As New OracleParameter("v_user_id", OracleDbType.Int32, ParameterDirection.Input)
        param3.Value = Me.userID
        parameters(2) = param3

        Dim param4 As New OracleParameter("v_type_id", OracleDbType.Int32, ParameterDirection.Input)
        param4.Value = type_id
        parameters(3) = param4

        dataBase.executeNonQuery("PKG_TARGET_PRODUCTGROUP.RenameTPG", parameters)
    End Sub

    '****************************************************************************************************
    '* add a new TPG 
    '****************************************************************************************************
    Public Function AddTPG(ByVal description As String, ByVal type As Integer) As Integer
        Dim parameters(3) As OracleParameter

        parameters(0) = New OracleParameter("v_tp_id", OracleDbType.Int32, ParameterDirection.ReturnValue)

        Dim param1 As New OracleParameter("v_tp_name", OracleDbType.Varchar2, ParameterDirection.Input)
        param1.Value = description.ToUpper
        parameters(1) = param1

        Dim param2 As New OracleParameter("v_user_id", OracleDbType.Int32, ParameterDirection.Input)
        param2.Value = Me.userID
        parameters(2) = param2

        Dim param3 As New OracleParameter("v_type_id", OracleDbType.Int32, ParameterDirection.Input)
        param3.Value = type
        parameters(3) = param3

        Dim ret As String
        ret = dataBase.executeScalar("PKG_TARGET_PRODUCTGROUP.AddTPG", parameters)

        If ret <> "" Then
            Return CInt(ret)
        Else
            Return 0
        End If
    End Function

    '****************************************************************************************************
    '* delete TPG 
    '****************************************************************************************************
    Public Sub deleteTPG()
        Dim parameters(0) As OracleParameter

        Dim param As New OracleParameter("v_tapg_id", OracleDbType.Int32, ParameterDirection.Input)
        param.Value = Me.targetProductGroupID
        parameters(0) = param

        dataBase.executeNonQuery("PKG_TARGET_PRODUCTGROUP.DelTPG", parameters)
    End Sub


    '****************************************************************************************************
    '* delete TPG 
    '****************************************************************************************************
    Public Sub addCustomer()
        Dim parameters(0) As OracleParameter

        Dim param As New OracleParameter("v_tapg_id", OracleDbType.Int32, ParameterDirection.Input)
        param.Value = Me.targetProductGroupID
        parameters(0) = param

        dataBase.executeNonQuery("PKG_TARGET_PRODUCTGROUP.DelTPG", parameters)
    End Sub

    '****************************************************************************************************
    '* returns all UNASSIGNED CUSTOMERS for a specified TGP 
    '****************************************************************************************************
    Public Function getUnassignedCustomers(ByVal keyword As String) As DataView
        Dim parameters(2) As OracleParameter

        parameters(0) = New OracleParameter("Customers", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)

        Dim param As New OracleParameter("v_tapg_id", OracleDbType.Int32, ParameterDirection.Input)
        param.Value = Me.targetProductGroupID
        parameters(1) = param

        Dim param2 As New OracleParameter("v_keyword", OracleDbType.Varchar2, ParameterDirection.Input)
        param2.Value = keyword.Trim()
        parameters(2) = param2

        Return dataBase.executeStoredProcedure("PKG_TARGET_PRODUCTGROUP.GetAllUnasCustomersForTPG", parameters)
    End Function

    '****************************************************************************************************
    '* sets a PERCENTAGE value for a SALES-REP and a specified CUSTOMER 
    '****************************************************************************************************
    Public Function setPercentageForSalesRep(ByVal sareID As Integer, ByVal customerID As Integer, ByVal percent As Double)
        Dim parameters(3) As OracleParameter

        Dim param As New OracleParameter("v_cust_id", OracleDbType.Int32, ParameterDirection.Input)
        param.Value = customerID
        parameters(0) = param

        Dim param1 As New OracleParameter("v_sare_id", OracleDbType.Int32, ParameterDirection.Input)
        param1.Value = sareID
        parameters(1) = param1

        Dim param2 As New OracleParameter("v_user_id", OracleDbType.Int32, ParameterDirection.Input)
        param2.Value = Me.userID
        parameters(3) = param2

        Dim param3 As New OracleParameter("v_percent", OracleDbType.Decimal, ParameterDirection.Input)
        param3.Value = percent
        parameters(2) = param3

        dataBase.executeNonQuery("PKG_TARGET_PRODUCTGROUP.SetPercentforCustSaRe", parameters)
    End Function

    '****************************************************************************************************
    '* gets all customers  
    '****************************************************************************************************
    Public Function getSalesrepsForCustomerAndTGP(ByVal customerID As Integer)
        Dim parameters(2) As OracleParameter

        parameters(0) = New OracleParameter("Table", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)

        Dim param As New OracleParameter("v_tapg_id", OracleDbType.Int32, ParameterDirection.Input)
        param.Value = Me.targetProductGroupID
        parameters(1) = param

        Dim param1 As New OracleParameter("v_cust_id", OracleDbType.Int32, ParameterDirection.Input)
        param1.Value = customerID
        parameters(2) = param1

        Return dataBase.executeStoredProcedure("PKG_TARGET_PRODUCTGROUP.GetSalesRepsForCustomerAndTPG", parameters)
    End Function


    '****************************************************************************************************
    '* Get Count of Custpomer for this tpg
    '****************************************************************************************************
    Public Function GetCustomerCount(ByVal tpg_id As Integer) As Integer
        Dim parameters(1) As OracleParameter

        parameters(0) = New OracleParameter("v_tp_id", OracleDbType.Int32, ParameterDirection.ReturnValue)

        Dim param1 As New OracleParameter("v_tpg_id", OracleDbType.Int64, ParameterDirection.Input)
        param1.Value = tpg_id
        parameters(1) = param1

     

        Dim ret As Integer
        ret = dataBase.executeScalar("PKG_TARGET_PRODUCTGROUP.GetCustomerCountForTPG", parameters)

        Return ret

    End Function

End Class
