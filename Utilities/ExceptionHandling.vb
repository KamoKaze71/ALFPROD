Imports System
Imports System.web
Imports System.Diagnostics
Imports System.Reflection
'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
''' <summary><para>Provides Basic Exception Handling for ALF</para></summary>
Public Class ExceptionInfo
    Public Sub New()
        MyBase.New()
    End Sub

    Public Shared Sub Show(ByVal ex As Exception, Optional ByVal additionalInfo As String = "")
        ' traceString hold all the Information of the Exception

        Dim MyLog As New Log
        Dim traceString, methodParams, s, user_id As String

        ' Stack Trace aus Exception erstellen
        Dim StackTrace As New StackTrace(ex, True)

        'Stack Frame aus StackTRace ermitteln
        Dim Frame As New StackFrame
        ' Klassen und Funktionsnamen aus Stacktrace ermitteln
        Dim Method As System.Reflection.MethodBase
        ' MethodenParameter ermitteln
        Dim paramInfos As ParameterInfo()
        Try
            Frame = StackTrace.GetFrame(0)
            Method = Frame.GetMethod
            traceString += " Error in: "
            traceString += Method.DeclaringType.ToString()
            traceString += "." + Method.Name + "("

            paramInfos = Method.GetParameters()
            methodParams = ""

            traceString += ") in Line "
            traceString += Frame.GetFileLineNumber().ToString()
            traceString += ", Column"
            traceString += Frame.GetFileColumnNumber().ToString()
            traceString += ", ILOffset:"
            traceString += Frame.GetILOffset().ToString()
            traceString += ", Datei:"
            traceString += Frame.GetFileName
            ' TraceString mit Exception Infos Fehlerzeile ausgeben
            user_id = HttpContext.Current.Session("user_id")

            MyLog.Description = ex.StackTrace & vbNewLine & traceString & vbNewLine & additionalInfo
            MyLog.CodeCode = "AppEx"
            MyLog.Source = ex.Message
            MyLog.insert()

        Catch
        Finally
        End Try

    End Sub

End Class

