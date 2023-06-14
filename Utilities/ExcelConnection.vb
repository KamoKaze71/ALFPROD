Imports System.Data
Imports System
Imports System.Data.OleDb
'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
''' <summary><para>Provides Connection to Excel Sheets</para></summary>
Public Class ExcelConnection

    Private sConnectionString As String
    Public Sub New(ByVal filename As String)
        m_filename = filename
        sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & m_filename & ";Extended Properties=Excel 8.0"
    End Sub

    Public Function GetData(ByVal excelSheetName As String) As DataSet

        Dim objConn As New OleDbConnection(sConnectionString)

        Try

            '// Open connection with the database.
            objConn.Open()

            '			// The code to follow uses a SQL SELECT command to display the data from the worksheet.

            '	
            ''			// Create new OleDbCommand to return data from worksheet.



            Dim objCmdSelect As New OleDbCommand

            If m_sqlCommand = String.Empty Then
                objCmdSelect.CommandText = "select * from " & excelSheetName
            Else
                objCmdSelect.CommandText = m_sqlCommand
            End If
            objCmdSelect.Connection = objConn

            '				// Create new OleDbDataAdapter that is used to build a DataSet
            '				// based on the preceding SQL SELECT statement.
            Dim objAdapter As OleDbDataAdapter = New OleDbDataAdapter

            '				// Pass the Select command to the adapter.
            objAdapter.SelectCommand = objCmdSelect

            '				// Create new DataSet to hold information from the worksheet.
            Dim objDataset As DataSet = New DataSet

            '				// Fill the DataSet with the information from the worksheet.
            objAdapter.Fill(objDataset, excelSheetName)

            Return objDataset

        Catch ex As Exception
            ExceptionInfo.Show(ex)

        Finally
            objConn.Close()
        End Try
    End Function



    Private m_filename As String

    Public Property FileName() As String
        Get
            Return m_filename
        End Get
        Set(ByVal Value As String)
            m_filename = Value
        End Set
    End Property


    Private m_sqlCommand As String
    Public Property sqlCommand() As String
        Get
            Return m_sqlCommand
        End Get
        Set(ByVal Value As String)
            m_sqlCommand = Value
        End Set
    End Property

End Class
