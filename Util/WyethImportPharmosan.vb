Imports Wyeth.Utilities
Imports Oracle.DataAccess.Client
Imports System.IO
Imports Wyeth.Alf.DataAccessBaseClass
'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
'''         <revision>1</revision>
''' <summary><para>DataAccess For Pharmosan Import</para></summary>
'''<seealso cref="WyethImport">[label]</seealso> 
Public Class WyethImportPharmosan

    Dim myLog As New Log
    Private MyCollection As New Hashtable
    Private m_strLog As String
    Private m_strError As String

    Public Sub New()
        MyCollection = Wyeth.Alf.WyethImportHelper.readAppVars
    End Sub
    Public Function sqlLoader(ByVal user_id As Integer)
        Try
            Dim MyCommand As New CommandLauncher
            Dim MyPharmosanImport As New WyethImportPharmosan

            Dim dir As New DirectoryInfo(MyCollection("SanovaImportFilePath"))
            Dim Files() As FileInfo
            Dim strExec As String
            Dim strImportDir As String = MyCollection("SanovaImportFilePath")

            Files = dir.GetFiles("*.txt")

            Dim x As Integer = 0
            For Each File As FileInfo In Files

                Try

                    Dim i As Integer

                    strExec = "SQLLDR.EXE " & MyCollection("OracleUserName") & "/" & MyCollection("OracleUserPassword") & "@" & MyCollection("OracleServiceName") _
                                          & " control=" & MyCollection("SanovaImportControlFilePath") & "Pharmosan.ctl" & " data=" & File.FullName & " log=" & File.FullName & ".log bad=" & File.FullName & ".bad"

                    Dim str_out, str_err As String
                    i = System.CodeDom.Compiler.Executor.ExecWaitWithCapture(strExec, New System.CodeDom.Compiler.TempFileCollection, str_out, str_err)
                    m_strError += read_sqlloader_errors(str_err)
                    m_strError += read_sqlloader_errors(str_out)

                Catch ex As Exception
                    m_strError += ex.Message.ToString
                Finally
                    m_strError += SetTranmission(user_id)
                End Try

            Next

        Catch ex As Exception
            m_strError += ex.Message.ToString
            Return m_strError
        Finally

        End Try
        Return m_strError
    End Function



    Public Function UpdateT_Prescription() As String

        '****************************************************************************************************
        '* updates T_PREXSCRPTIONS with temp table ´data 
        '****************************************************************************************************

        Dim parameters(0) As OracleParameter
        Dim base As New DataAccessBaseClass
        Dim val As Object
        parameters(0) = New OracleParameter("v_ret_val", OracleDbType.Varchar2, 2000, val, ParameterDirection.ReturnValue)

        Dim ret As String
        ret = base.executeScalar("PKG_IMPORT_PHARMOSAN.F_Prescriptions", parameters)

        Return ret

    End Function
    Public Function SetTranmission(ByVal user_id As Integer) As String

        '****************************************************************************************************
        '* Set Tranmission ID for Pharmosan Import
        '****************************************************************************************************

        Dim parameters(1) As OracleParameter
        Dim base As New DataAccessBaseClass
        Dim val As Object
        parameters(0) = New OracleParameter("v_ret_val", OracleDbType.Varchar2, 2000, val, ParameterDirection.ReturnValue)

        Dim param1 As New OracleParameter("v_user_id", OracleDbType.Int32, ParameterDirection.Input)
        param1.Value = user_id
        parameters(1) = param1

        Dim ret As String
        ret = base.executeScalar("PKG_IMPORT_PHARMOSAN.F_SetTransmission", parameters)

        Return ret

    End Function
    Private Function read_sqlloader_errors(ByVal sender As String) As String
        Try
            Dim sr As StreamReader = File.OpenText(sender)
            Dim input As String = ""

            If File.Exists(sender) Then
                input = sr.ReadToEnd
                sr.Close()
            End If
            Return input
        Catch ex As Exception
            ExceptionInfo.Show(ex)
        End Try

    End Function
    Public Property strError() As String
        Get
            Return m_strError
        End Get
        Set(ByVal Value As String)
            m_strError = Value
        End Set
    End Property

End Class
