Imports System
Imports System.Globalization

Imports System.Web.UI.WebControls
Imports System.Math
Imports Wyeth.Utilities.Helper
'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
''' <summary><para>Soem Helper Functions for Number Formats</para></summary>
Public Class NumberFormat

    Public Shared Function GetMyNFI(Optional ByVal Precision As Integer = 10) As NumberFormatInfo
        Try
            Dim MyNFI As New NumberFormatInfo
            Dim NumberGroupSize As Integer() = {3, 3, 3}
            Dim PercentGroupSize As Integer() = {3, 3, 3}

            MyNFI.NumberDecimalSeparator = ","
            MyNFI.NumberGroupSeparator = "."
            MyNFI.NumberGroupSizes = NumberGroupSize
            MyNFI.NumberDecimalDigits = Precision
            MyNFI.PercentSymbol = "%"
            MyNFI.CurrencyDecimalSeparator = ","
            MyNFI.CurrencyGroupSeparator = "."
            MyNFI.PercentDecimalDigits = 1
            MyNFI.PercentGroupSizes = PercentGroupSize
            MyNFI.CurrencyDecimalDigits = 2
            MyNFI.CurrencyDecimalSeparator = ","
            MyNFI.NegativeSign = "-"
            MyNFI.NaNSymbol = "-"

            Return MyNFI
        Catch ex As Exception

        End Try
    End Function
    Public Overloads Shared Sub MyNumberFormat(ByRef cell As TableCell, ByVal precision As Integer, Optional ByVal sign As String = "")
        Try

            If cell.Text = "-" Then

            Else

                Dim d As Double
                d = Convert.ToDouble(cell.Text, GetMyNFI(precision))
                cell.Text = d.ToString(NUMBER_FORMAT_STRING_EXACT, GetMyNFI(precision)) & sign
            End If

Catch ex As Exception
            cell.Text = "-"
        End Try
    End Sub

    Public Overloads Shared Function MyNumberFormat(ByVal str As String, ByVal precision As Integer) As Double
        Try
            Dim d As Double
            d = Convert.ToDouble(str, GetMyNFI(precision))
            Return d
        Catch ex As Exception

        End Try
    End Function
    Public Overloads Shared Function MyNumberFormat(ByVal value As Double, ByVal precision As Integer, Optional ByVal sign As String = "") As String
        Dim ret_val As String
        Try
            value = Round(value, precision)
            ret_val = value.ToString(NUMBER_FORMAT_STRING_EXACT, GetMyNFI(precision)) & sign
            If ret_val.StartsWith("-Infinity") Or ret_val.StartsWith("-%") Or ret_val.StartsWith("Infinity") Then
                ret_val = "-"
            End If
            Return ret_val

        Catch ex As Exception
            ret_val = "-"
            Return ret_val
        End Try
    End Function

    Public Overloads Shared Function MyNumberFormat(ByVal value As Integer, ByVal precision As Integer) As String
        Dim ret_val As String
        Try
            ret_val = value.ToString(GetMyNFI(precision))
            Return ret_val
        Catch ex As Exception
            ret_val = "-"
            Return ret_val
        End Try
    End Function

End Class
