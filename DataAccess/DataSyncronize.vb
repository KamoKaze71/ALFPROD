Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities
Imports Wyeth.Alf.DataAccessBaseClass
'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
'''         <revision>23</revision>
''' <summary><para>Provides DataAccess for Syncronizing LIVE <-> DEV Server</para></summary>
Public Class DataSyncronize


    Public Function Gettables() As DataView

        Dim MyData As New DataAccessBaseClass

        Try
            Dim parameters(0) As OracleParameter
            parameters(0) = New OracleParameter("Tables", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            Return MyData.executeStoredProcedure("PKG_APPLICATION.GetDataSyncronizeTables", parameters)

        Catch

        End Try


    End Function

End Class
