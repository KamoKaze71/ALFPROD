Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities
'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
''' <summary><para>Provides DataAccess for SlesReps Targeting</para></summary>

Public Class Targeting
    Private dataBase As New DataAccessBaseClass

    Public Function GetTargetVersions(ByVal year As Integer, ByVal sare_id As Integer, ByVal tapg_id As Integer) As DataView
        Dim parameters(3) As OracleParameter

        parameters(0) = New OracleParameter("Targeting", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)

        Dim param As New OracleParameter("v_year", OracleDbType.Int32, ParameterDirection.Input)
        param.Value = year
        parameters(1) = param

        Dim param2 As New OracleParameter("v_sare_id", OracleDbType.Int32, ParameterDirection.Input)
        param2.Value = sare_id
        parameters(2) = param2


        Dim param3 As New OracleParameter("v_tapg_id", OracleDbType.Int32, ParameterDirection.Input)
        param3.Value = tapg_id
        parameters(3) = param3

        Return dataBase.executeStoredProcedure("PKG_TARGETING.GetTargetVersions", parameters)

    End Function

    Public Function GetTaregtsForInput(ByVal tpg_id As Integer, ByVal sare_id As Integer, ByVal year As Integer, ByVal version As String)

        Dim parameters(4) As OracleParameter

        parameters(0) = New OracleParameter("Targeting", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)

        Dim param1 As New OracleParameter("v_tpg_id", OracleDbType.Int32, ParameterDirection.Input)
        param1.Value = tpg_id
        parameters(1) = param1

        Dim param2 As New OracleParameter("v_sare_id", OracleDbType.Int32, ParameterDirection.Input)
        param2.Value = sare_id
        parameters(2) = param2


        Dim param3 As New OracleParameter("v_year", OracleDbType.Int32, ParameterDirection.Input)
        param3.Value = year
        parameters(3) = param3


        Dim param4 As New OracleParameter("v_version", OracleDbType.Varchar2, ParameterDirection.Input)
        param4.Value = version
        parameters(4) = param4

        Return dataBase.executeStoredProcedure("PKG_TARGETING.GetTargets", parameters)

    End Function

    Public Function DeleteTargetVersion(ByVal year As Integer, ByVal sare_id As Integer, ByVal version As String, ByVal tapg_id As Integer) As DataView
        Dim parameters(3) As OracleParameter

        Dim param As New OracleParameter("v_year", OracleDbType.Int32, ParameterDirection.Input)
        param.Value = year
        parameters(0) = param

        Dim param1 As New OracleParameter("v_sare_id", OracleDbType.Int32, ParameterDirection.Input)
        param1.Value = sare_id
        parameters(1) = param1

        Dim param2 As New OracleParameter("v_version", OracleDbType.Varchar2, ParameterDirection.Input)
        param2.Value = version
        parameters(2) = param2

        Dim param3 As New OracleParameter("v_tapg_id", OracleDbType.Varchar2, ParameterDirection.Input)
        param3.Value = tapg_id
        parameters(3) = param3

        dataBase.executeNonQuery("PKG_TARGETING.DeleteTargetVersion", parameters)
    End Function

    Public Function InsertTargetVersion(ByVal year As Integer, ByVal prod_cc_id As Integer, ByVal sare_id As Integer, ByVal version As String, ByVal q1 As Double, ByVal q2 As Double, ByVal q3 As Double, ByVal q4 As Double, ByVal user_id As Integer, ByVal v_tapg_id As Integer) As DataView
        Dim parameters(9) As OracleParameter


        Dim param As New OracleParameter("v_sare_id", OracleDbType.Int32, ParameterDirection.Input)
        param.Value = sare_id
        parameters(0) = param

        Dim param1 As New OracleParameter("v_prod_cc_id", OracleDbType.Int32, ParameterDirection.Input)
        param1.Value = prod_cc_id
        parameters(1) = param1

        Dim param2 As New OracleParameter("v_year", OracleDbType.Int32, ParameterDirection.Input)
        param2.Value = year
        parameters(2) = param2


        Dim param3 As New OracleParameter("v_version", OracleDbType.Varchar2, ParameterDirection.Input)
        param3.Value = version
        parameters(3) = param3


        Dim param4 As New OracleParameter("v_q1", OracleDbType.Double, ParameterDirection.Input)
        param4.Value = q1
        parameters(4) = param4


        Dim param5 As New OracleParameter("v_q2", OracleDbType.Double, ParameterDirection.Input)
        param5.Value = q2
        parameters(5) = param5


        Dim param6 As New OracleParameter("v_q3", OracleDbType.Double, ParameterDirection.Input)
        param6.Value = q3
        parameters(6) = param6


        Dim param7 As New OracleParameter("v_q4", OracleDbType.Double, ParameterDirection.Input)
        param7.Value = q4
        parameters(7) = param7

        Dim param8 As New OracleParameter("v_user_id", OracleDbType.Int32, ParameterDirection.Input)
        param8.Value = user_id
        parameters(8) = param8


        Dim param9 As New OracleParameter("v_tapg_id", OracleDbType.Int32, ParameterDirection.Input)
        param9.Value = v_tapg_id
        parameters(9) = param9

        dataBase.executeNonQuery("PKG_TARGETING.InsertTargetVersion", parameters)

    End Function

    Public Function CreateNewTargetVersionFromOld(ByVal v_year_old As Integer, ByVal v_version_old As Integer, ByVal v_sare_id_old As Integer, ByVal v_tapg_id As Integer, ByVal v_sare_id_new As Integer, ByVal v_year_new As Integer, ByVal v_version_new As Integer, ByVal v_user_id As Integer) As Boolean

        Try

            Dim parameters(7) As OracleParameter

            Dim param As New OracleParameter("v_year_old", OracleDbType.Int32, ParameterDirection.Input)
            param.Value = v_year_old
            parameters(0) = param

            Dim param1 As New OracleParameter("v_version_old", OracleDbType.Int32, ParameterDirection.Input)
            param1.Value = v_version_old
            parameters(1) = param1


            Dim param2 As New OracleParameter("v_sare_id", OracleDbType.Int32, ParameterDirection.Input)
            param2.Value = v_sare_id_old
            parameters(2) = param2


            Dim param3 As New OracleParameter("v_tapg_id", OracleDbType.Int32, ParameterDirection.Input)
            param3.Value = v_tapg_id
            parameters(3) = param3


            Dim param4 As New OracleParameter("v_sare_id_new", OracleDbType.Int32, ParameterDirection.Input)
            param4.Value = v_sare_id_new
            parameters(4) = param4


            Dim param5 As New OracleParameter("v_year_new", OracleDbType.Int32, ParameterDirection.Input)
            param5.Value = v_year_new
            parameters(5) = param5



            Dim param6 As New OracleParameter("v_version_new", OracleDbType.Int32, ParameterDirection.Input)
            param6.Value = v_version_new
            parameters(6) = param6


            Dim param7 As New OracleParameter("v_user_id", OracleDbType.Int32, ParameterDirection.Input)
            param7.Value = v_user_id
            parameters(7) = param7

            dataBase.executeNonQuery("PKG_TARGETING.CreateNewVersionFromOld", parameters)

            Return True

        Catch ex As Exception

            ExceptionInfo.Show(ex)
            Return False

        End Try

    End Function

    Public Function GetLatestVersionForSaReYear(ByVal v_year As Integer, ByVal v_sare_id As Integer, ByVal v_tapg_id As Integer) As Integer
        Dim ret_val As String

        Dim parameters(3) As OracleParameter

        Dim param As New OracleParameter("v_ret_val", OracleDbType.Int32, ParameterDirection.ReturnValue)
        parameters(0) = param

        Dim param1 As New OracleParameter("v_year_old", OracleDbType.Int32, ParameterDirection.Input)
        param1.Value = v_year
        parameters(1) = param1

        Dim param2 As New OracleParameter("v_sare_id", OracleDbType.Int32, ParameterDirection.Input)
        param2.Value = v_sare_id
        parameters(2) = param2

        Dim param3 As New OracleParameter("v_tapg_id", OracleDbType.Int32, ParameterDirection.Input)
        param3.Value = v_tapg_id
        parameters(3) = param3
        ret_val = dataBase.executeScalar("PKG_TARGETING.GetLatestVersionForSaReYear", parameters)
        If ret_val = "" Then
            ret_val = 0
        End If

        Return ret_val
    End Function

    Public Function CheckForApproval(ByVal v_year As Integer, ByVal v_sare_id As Integer, ByVal v_tapg_id As Integer, ByVal v_version As Integer) As Integer
        Dim ret_val As String

        Dim parameters(4) As OracleParameter

        Dim param As New OracleParameter("v_ret_val", OracleDbType.Int32, ParameterDirection.ReturnValue)
        parameters(0) = param

        Dim param1 As New OracleParameter("v_year_old", OracleDbType.Int32, ParameterDirection.Input)
        param1.Value = v_year
        parameters(1) = param1

        Dim param2 As New OracleParameter("v_sare_id", OracleDbType.Int32, ParameterDirection.Input)
        param2.Value = v_sare_id
        parameters(2) = param2

        Dim param3 As New OracleParameter("v_version", OracleDbType.Int32, ParameterDirection.Input)
        param3.Value = v_version
        parameters(3) = param3

        Dim param4 As New OracleParameter("v_tapg_id", OracleDbType.Int32, ParameterDirection.Input)
        param4.Value = v_tapg_id
        parameters(4) = param4

        ret_val = dataBase.executeScalar("PKG_TARGETING.CheckForApproval", parameters)
        If ret_val = "" Then
            ret_val = 1
        End If

        Return ret_val
    End Function

    Public Function GetTaregtsForReport(ByVal tpg_id As Integer, ByVal year As Integer, ByVal sare_id As Integer)

        Dim parameters(3) As OracleParameter

        parameters(0) = New OracleParameter("Targeting", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)

        Dim param1 As New OracleParameter("v_tpg_id", OracleDbType.Int32, ParameterDirection.Input)
        param1.Value = tpg_id
        parameters(1) = param1

        Dim param2 As New OracleParameter("v_year", OracleDbType.Int32, ParameterDirection.Input)
        param2.Value = year
        parameters(2) = param2

        Dim param3 As New OracleParameter("v_sare_id", OracleDbType.Int32, ParameterDirection.Input)
        param3.Value = sare_id
        parameters(3) = param3

        Return dataBase.executeStoredProcedure("PKG_TARGETING.GetTargetsForReport", parameters)

    End Function

    Public Function GetSareID(ByVal user_id As Integer)

        Dim parameters(1) As OracleParameter

        parameters(0) = New OracleParameter("Targeting", OracleDbType.Int64, DBNull.Value, ParameterDirection.ReturnValue)

        Dim param1 As New OracleParameter("v_tpg_id", OracleDbType.Int32, ParameterDirection.Input)
        param1.Value = user_id
        parameters(1) = param1

      

        Return dataBase.executeScalar("PKG_APPLICATION.GetSareID", parameters)

    End Function



    Public Function GetTaregtsForReportUnits(ByVal tpg_id As Integer, ByVal year As Integer)

        Dim parameters(2) As OracleParameter

        parameters(0) = New OracleParameter("Targeting", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)

        Dim param1 As New OracleParameter("v_tpg_id", OracleDbType.Int32, ParameterDirection.Input)
        param1.Value = tpg_id
        parameters(1) = param1

        Dim param2 As New OracleParameter("v_year", OracleDbType.Int32, ParameterDirection.Input)
        param2.Value = year
        parameters(2) = param2

        Return dataBase.executeStoredProcedure("PKG_TARGETING.GetTargetsForReportUnits", parameters)

    End Function

    Public Function GetTaregtsForApproval(ByVal year As Integer, ByVal sare_id As Integer, ByVal tapg_id As Integer)

        Dim parameters(3) As OracleParameter

        parameters(0) = New OracleParameter("Targeting", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)

        Dim param1 As New OracleParameter("v_sare_id", OracleDbType.Int32, ParameterDirection.Input)
        param1.Value = sare_id
        parameters(1) = param1

        Dim param2 As New OracleParameter("v_sare_id", OracleDbType.Int32, ParameterDirection.Input)
        param2.Value = tapg_id
        parameters(2) = param2

        Dim param3 As New OracleParameter("v_year", OracleDbType.Int32, ParameterDirection.Input)
        param3.Value = year
        parameters(3) = param3

        Return dataBase.executeStoredProcedure("PKG_TARGETING.GetTargetsForApproval", parameters)

    End Function


    Public Function ApproveTaregtsVersion(ByVal year As Integer, ByVal sare_id As Integer, ByVal version As String, ByVal tapg_id As Integer, ByVal v_user_id As Integer) As Boolean

        Try


            Dim parameters(4) As OracleParameter

            Dim param As New OracleParameter("v_year", OracleDbType.Int32, ParameterDirection.Input)
            param.Value = year
            parameters(0) = param

            Dim param1 As New OracleParameter("v_sare_id", OracleDbType.Int32, ParameterDirection.Input)
            param1.Value = sare_id
            parameters(1) = param1

            Dim param2 As New OracleParameter("v_version", OracleDbType.Int32, ParameterDirection.Input)
            param2.Value = version
            parameters(2) = param2

            Dim param3 As New OracleParameter("v_tapg_id", OracleDbType.Int32, ParameterDirection.Input)
            param3.Value = tapg_id
            parameters(3) = param3

            Dim param4 As New OracleParameter("v_user_id", OracleDbType.Varchar2, ParameterDirection.Input)
            param4.Value = v_user_id
            parameters(4) = param4

            dataBase.executeNonQuery("PKG_TARGETING.ApproveTargetVersion", parameters)
            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function


    Public Function GetTPForSalesRep(ByVal user_id As Integer, ByVal ctry_id As Integer) As String
        Dim v_ret_val As String

        Try

            Dim parameters(2) As OracleParameter

            parameters(0) = New OracleParameter("Targeting", OracleDbType.Int64, DBNull.Value, ParameterDirection.ReturnValue)

            Dim param1 As New OracleParameter("v_ctry", OracleDbType.Int32, ParameterDirection.Input)
            param1.Value = ctry_id
            parameters(1) = param1


            Dim param2 As New OracleParameter("v_user_id", OracleDbType.Int32, ParameterDirection.Input)
            param2.Value = user_id
            parameters(2) = param2


            v_ret_val = (dataBase.executeScalar("PKG_TARGET_PRODUCTGROUP.GetTPGForSalesRep", parameters))
        Catch ex As OracleException
            If ex.Number = "1403" Then
                Return Nothing
            End If
        End Try

        Return v_ret_val
    End Function


    Public Function GetTargetType(ByVal tpg_id As Integer) As String
        Dim parameters(1) As OracleParameter

        parameters(0) = New OracleParameter("Targeting", OracleDbType.Varchar2, 2000, DBNull.Value, ParameterDirection.ReturnValue)

        Dim param1 As New OracleParameter("v_tpg_id", OracleDbType.Int32, ParameterDirection.Input)
        param1.Value = tpg_id
        parameters(1) = param1

        GetTargetType = (dataBase.executeScalar("PKG_TARGET_PRODUCTGROUP.GetTypeForTPG", parameters))
    End Function

End Class
