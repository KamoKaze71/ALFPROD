Imports Wyeth.Alf.DataAccessBaseClass
Imports Oracle.DataAccess.Client
'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
''' <summary><para>Provides DataAccess for JDE Accounting</para></summary>
Public Class JDE

    Dim dataBase As New DataAccessBaseClass

    Public Function GetAccRecord(ByVal acre_id As Integer) As DataView
        Dim parameters(1) As OracleParameter
        parameters(0) = New OracleParameter("JDE", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        parameters(1) = New OracleParameter("v_acre_id", OracleDbType.Int32, ParameterDirection.Input)
        parameters(1).Value = acre_id
        Return dataBase.executeStoredProcedure("PKG_JDE.GetAccountingRecord", parameters)
    End Function

    Public Function GetBewegKZsForAcRe(ByVal acre_id As Integer) As DataView
        Dim parameters(1) As OracleParameter
        parameters(0) = New OracleParameter("JDE", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        parameters(1) = New OracleParameter("v_acre_id", OracleDbType.Int32, ParameterDirection.Input)
        parameters(1).Value = acre_id
        Return dataBase.executeStoredProcedure("PKG_JDE.GetBewegKZsForAcRe", parameters)
    End Function

    Public Function GetAllBewegKZs(ByVal ctry_id As Integer) As DataView
        Dim parameters(1) As OracleParameter
        parameters(0) = New OracleParameter("JDE", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        parameters(1) = New OracleParameter("v_ctry_id", OracleDbType.Int32, ParameterDirection.Input)
        parameters(1).Value = ctry_id
        Return dataBase.executeStoredProcedure("PKG_JDE.GetAllBewegKZs", parameters)
    End Function

    Public Function GetAccountNamesList(ByVal ctry_id As Integer) As DataView
        Dim parameters(1) As OracleParameter
        parameters(0) = New OracleParameter("JDE", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        parameters(1) = New OracleParameter("v_ctry_id", OracleDbType.Int32, ParameterDirection.Input)
        parameters(1).Value = ctry_id
        Return dataBase.executeStoredProcedure("PKG_JDE.GetAccountNamesList", parameters)
    End Function

    Public Function DeleteAccountingRecord(ByVal acre_id As Integer) As DataView
        Dim parameters(0) As OracleParameter
        parameters(0) = New OracleParameter("v_acre_id", OracleDbType.Int32, ParameterDirection.Input)
        parameters(0).Value = acre_id
        dataBase.executeNonQuery("PKG_JDE.DeleteAccountingRecord", parameters)
    End Function

    Public Function AddAccountingRecord(ByVal acre_name As String, ByVal acre_description As String, ByVal ctry_id As Integer) As Integer
        Dim parameters(3) As OracleParameter
        parameters(0) = New OracleParameter("v_acre_id", OracleDbType.Int32, ParameterDirection.ReturnValue)

        parameters(1) = New OracleParameter("v_acre_name", OracleDbType.Varchar2, ParameterDirection.Input)
        parameters(1).Value = acre_name
        parameters(2) = New OracleParameter("v_acre_description", OracleDbType.Varchar2, ParameterDirection.Input)
        parameters(2).Value = acre_description
        parameters(3) = New OracleParameter("v_ctry_id", OracleDbType.Int32, ParameterDirection.Input)
        parameters(3).Value = ctry_id
        Return dataBase.executeScalar("PKG_JDE.AddAccountingRecord", parameters)
    End Function

    Public Function AddBewegKZForAcRE(ByVal acre_id As String, ByVal code_id As String)
        Dim parameters(1) As OracleParameter
        parameters(0) = New OracleParameter("v_code_id", OracleDbType.Varchar2, ParameterDirection.Input)
        parameters(0).Value = code_id
        parameters(1) = New OracleParameter("v_acre_id", OracleDbType.Varchar2, ParameterDirection.Input)
        parameters(1).Value = acre_id
        dataBase.executeNonQuery("PKG_JDE.AddBewegKZsForAcRe", parameters)
    End Function

    Public Function ModifyAccountingRecord(ByVal acre_id As Integer, ByVal acre_name As String, ByVal acre_description As String) As DataView
        Dim parameters(2) As OracleParameter
        parameters(0) = New OracleParameter("v_acre_id", OracleDbType.Int32, ParameterDirection.Input)
        parameters(0).Value = acre_id
        parameters(1) = New OracleParameter("v_acre_name", OracleDbType.Varchar2, ParameterDirection.Input)
        parameters(1).Value = acre_name
        parameters(2) = New OracleParameter("v_acre_description", OracleDbType.Varchar2, ParameterDirection.Input)
        parameters(2).Value = acre_description
        dataBase.executeNonQuery("PKG_JDE.ModifyAccountingRecord", parameters)
    End Function

    Public Function UpdateAccountingRecord(ByVal acre_id As Integer, ByVal acre_debit_type As String, ByVal acre_acre_debit_cc As String, ByVal acre_debit_departmnet As String, ByVal acre_debit_account As String, ByVal acre_debit_subsidiary As String, ByVal credit_type As String, ByVal acre_credit_cc As String, ByVal acre_credit_department As String, ByVal acre_cerdit_account As String, ByVal acre_credit_subsidiary As String, ByVal acre_active As Integer, ByVal acre_invert As Double, ByVal user_id As Integer, ByVal v_line_id As Integer) As DataView
        Dim parameters(14) As OracleParameter
        parameters(0) = New OracleParameter("v_acre_id", OracleDbType.Int32, ParameterDirection.Input)
        parameters(0).Value = acre_id
        parameters(1) = New OracleParameter("v_acre_debit_type", OracleDbType.Varchar2, ParameterDirection.Input)
        parameters(1).Value = acre_debit_type
        parameters(2) = New OracleParameter("v_acre_debit_cc", OracleDbType.Varchar2, ParameterDirection.Input)
        parameters(2).Value = acre_acre_debit_cc
        parameters(3) = New OracleParameter("v_acre_debit_department", OracleDbType.Varchar2, ParameterDirection.Input)
        parameters(3).Value = acre_debit_departmnet
        parameters(4) = New OracleParameter("v_acre_debit_account", OracleDbType.Varchar2, ParameterDirection.Input)
        parameters(4).Value = acre_debit_account
        parameters(5) = New OracleParameter("v_acre_debit_subsidiary", OracleDbType.Varchar2, ParameterDirection.Input)
        parameters(5).Value = acre_debit_subsidiary
        parameters(6) = New OracleParameter("v_acre_credit_type", OracleDbType.Varchar2, ParameterDirection.Input)
        parameters(6).Value = credit_type
        parameters(7) = New OracleParameter("v_acre_credit_cc", OracleDbType.Varchar2, ParameterDirection.Input)
        parameters(7).Value = acre_credit_cc
        parameters(8) = New OracleParameter("v_acre_credit_department", OracleDbType.Varchar2, ParameterDirection.Input)
        parameters(8).Value = acre_credit_department
        parameters(9) = New OracleParameter("v_acre_cerdit_account", OracleDbType.Varchar2, ParameterDirection.Input)
        parameters(9).Value = acre_cerdit_account
        parameters(10) = New OracleParameter("v_acre_credit_subsidiary", OracleDbType.Varchar2, ParameterDirection.Input)
        parameters(10).Value = acre_credit_subsidiary
        parameters(11) = New OracleParameter("v_acre_active", OracleDbType.Int32, ParameterDirection.Input)
        parameters(11).Value = acre_active
        parameters(12) = New OracleParameter("v_acre_invert", OracleDbType.Double, ParameterDirection.Input)
        parameters(12).Value = acre_invert
        parameters(13) = New OracleParameter("v_user_id", OracleDbType.Int32, ParameterDirection.Input)
        parameters(13).Value = user_id
        parameters(14) = New OracleParameter("v_line_id", OracleDbType.Int32, ParameterDirection.Input)
        parameters(14).Value = v_line_id
        dataBase.executeNonQuery("PKG_JDE.UpdateAccountingRecord", parameters)
    End Function

    Public Function DeleteBewegKZForAcRe(ByVal acre_id As Integer, ByVal code_id As Integer)
        Dim parameters(1) As OracleParameter
        parameters(0) = New OracleParameter("v_acre_id", OracleDbType.Int32, ParameterDirection.Input)
        parameters(0).Value = acre_id
        parameters(1) = New OracleParameter("v_code_id", OracleDbType.Int32, ParameterDirection.Input)
        parameters(1).Value = code_id
        dataBase.executeNonQuery("PKG_JDE.DelBewegKZsForAcRe", parameters)
    End Function


End Class
