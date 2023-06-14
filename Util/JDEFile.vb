Imports C1.Web.C1WebGrid
'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
'''         <revision>1</revision>
''' <summary><para>Helper Class for JDE Export, defines the layout of the export file </para></summary>
'''     
''' <todo><para>Simplyfy!!</para></todo>
Public Class JDEFile
    Inherits JDEProcessing

    Public Sub New()
        MyBase.New()

    End Sub
    Public Function GetLineDebit() As String
        Dim Str As String

        Str = "<tr><td Style='mso-number-format:\@'>" & "00300" & "</td><td Style='mso-number-format:\@'>" & Me.GLDateDay & Me.GLDateMonth & Me.GLDateYear & "</td><td Style='mso-number-format:\@'>" & Me.Col3 & "</td>"
        Str += "<td Style='mso-number-format:\@'>ALF" & Me.ExplainationAlpha & "</td><td></td>"
        Str += "<td Style='mso-number-format:Fixed'>" & Me.Amount + "</td><td></td></tr>"

        Return Str
    End Function
    Public Function GetLineCredit() As String
        Dim Str As String

        Str = "<tr><td Style='mso-number-format:\@'>" & "00300" & "</td><td Style='mso-number-format:\@'>" & Me.GLDateDay & Me.GLDateMonth & Me.GLDateYear & "</td><td Style='mso-number-format:\@'>" & Me.Col3 & "</td>"
        Str += "<td Style='mso-number-format:\@'>ALF" & Me.ExplainationAlpha & "</td><td></td>"
        Str += "<td></td><td Style='mso-number-format:Fixed'>" & CDbl(Me.Amount) * (-1) & "</td></tr>"

        Return Str

    End Function
    Public Property BookingFileLine() As String
        Get
            Return strBookingFileLine
        End Get
        Set(ByVal Value As String)
            BookingFileLine = Value
        End Set
    End Property
    Public Property Col3() As String
        Get
            Dim tmp As String
            tmp = Me.BuisinessUnit & Me.CostCenter & Me.Department & "," & Me.ObjectAccount & "," & Me.Subsidiary
            tmp = tmp.Trim()
            tmp = tmp.TrimEnd(",")
            tmp = tmp.TrimStart(",")

            Return tmp
        End Get
        Set(ByVal Value As String)
            m_strCol3 = Value
        End Set

    End Property

    Public Property BuisinessUnit() As String
        Get
            Dim tmp As String
            tmp = strBuisinessUnit
            Return tmp


        End Get
        Set(ByVal Value As String)
            strBuisinessUnit = Value
        End Set

    End Property
    Public Property Department() As String
        Get
            Return strDepartment.Replace("&nbsp;", String.Empty)

        End Get
        Set(ByVal Value As String)
            strDepartment = Value
        End Set
    End Property
    Public Property CostCenter() As String
        Get
            Return strCostCenter.Replace("&nbsp;", String.Empty)
        End Get
        Set(ByVal Value As String)
            strCostCenter = Value
        End Set
    End Property
    Public Property ObjectAccount() As String
        Get
            Return strObjectAccount.PadRight(4)

        End Get
        Set(ByVal Value As String)
            strObjectAccount = Value
        End Set
    End Property
    Public Property Subsidiary() As String
        Get
            If strSubsidiary = "" Then
                Return String.Empty
            Else
                Return strSubsidiary.Replace("&nbsp;", String.Empty).PadRight(8)

            End If
        End Get
        Set(ByVal Value As String)
            strSubsidiary = Value
        End Set
    End Property

    Public Property ShortAccountID() As String
        Get
            If strShortAccountID = "" Then
                Return String.Empty
            Else
                Return strShortAccountID.PadRight(8)
            End If

        End Get
        Set(ByVal Value As String)
            strShortAccountID = Value
        End Set
    End Property

    Public Property SubLedger() As String
        Get
            If strSubLedger = "" Then
                Return String.Empty
            Else
                Return strSubLedger.PadRight(8)

            End If

        End Get
        Set(ByVal Value As String)
            strSubLedger = Value
        End Set
    End Property
    Public Property SubLedgerType() As String
        Get
            If strSubLedgerType = "" Then
                Return String.Empty
            Else
                Return strSubLedgerType.PadRight(1)
            End If


        End Get
        Set(ByVal Value As String)
            strSubLedgerType = Value
        End Set
    End Property

    Public Property DocumentType() As String
        Get
            If strDocumentType = "" Then
                Return String.Empty

            Else
                Return strDocumentType.PadRight(2)

            End If
        End Get

        Set(ByVal Value As String)
            strDocumentType = Value
        End Set
    End Property

    Public Property DocumentNumber() As String
        Get
            If strDocumentNumber = "" Then
                Return String.Empty
            Else
                Return strDocumentNumber.PadRight(8)
            End If
        End Get

        Set(ByVal Value As String)
            strDocumentNumber = Value
        End Set
    End Property

    Public Property DocumentCompany() As String
        Get
            If strDocumentCompany = "" Then
                Return String.Empty
            Else
                Return strDocumentCompany.PadRight(8)
            End If
        End Get

        Set(ByVal Value As String)
            strDocumentCompany = Value
        End Set
    End Property


    Public Property InvoiceNumber() As String
        Get
            If strInvoiceNumber = "" Then
                Return String.Empty
            Else
                Return strInvoiceNumber.PadRight(25)
            End If
        End Get

        Set(ByVal Value As String)
            strInvoiceNumber = Value
        End Set
    End Property

    Public Property PayMentNumber() As String
        Get
            If strPayMentNumber = "" Then
                Return String.Empty
            Else
                Return strPayMentNumber.PadRight(8)
            End If
        End Get

        Set(ByVal Value As String)
            strPayMentNumber = Value
        End Set
    End Property

    Public Property PayItem() As String
        Get
            If strPayItem = "" Then
                Return String.Empty
            Else
                Return strPayItem.PadRight(3)
            End If
        End Get

        Set(ByVal Value As String)
            strPayItem = Value
        End Set
    End Property
    Public Property GLOffset() As String
        Get
            If strGLOffset = "" Then
                Return String.Empty
            Else
                Return strGLOffset.PadRight(4)
            End If
        End Get
        Set(ByVal Value As String)
            strGLOffset = Value
        End Set
    End Property
    Public Property Amount() As String
        Get
            If strAmount = "" Then
                Return String.Empty
            Else
                Return strAmount
            End If
        End Get
        Set(ByVal Value As String)
            strAmount = Value
        End Set
    End Property
    Public Property UnitAmount() As String
        Get
            If strUnitAmount = "" Then
                Return String.Empty
            Else
                Return strUnitAmount.PadRight(15)
            End If
        End Get

        Set(ByVal Value As String)
            strUnitAmount = Value
        End Set
    End Property
    Public Property UnitOfMeasure() As String
        Get
            If strUnitOfMeasure = "" Then
                Return String.Empty
            Else
                Return strUnitOfMeasure.PadRight(2)
            End If
        End Get
        Set(ByVal Value As String)
            strUnitOfMeasure = Value
        End Set
    End Property
    Public Property Reference1() As String
        Get
            If strReference1 = "" Then
                Return String.Empty
            Else
                Return strReference1.PadRight(8)
            End If
        End Get

        Set(ByVal Value As String)
            strReference1 = Value
        End Set
    End Property
    Public Property Reference2() As String
        Get
            If strReference2 = "" Then
                Return String.Empty
            Else
                Return strReference2.PadRight(8)
            End If
        End Get

        Set(ByVal Value As String)
            strReference2 = Value
        End Set
    End Property
    Public Property ExplainationAlpha() As String
        Get
            If strExplainationAlpha = "" Then
                Return String.Empty
            Else
                Return strExplainationAlpha
            End If
        End Get

        Set(ByVal Value As String)
            strExplainationAlpha = Value
        End Set
    End Property
    Public Property ExplainationRemark() As String
        Get
            If strExplainationRemark = "" Then
                Return String.Empty
            Else
                Return strExplainationRemark.PadRight(30)
            End If
        End Get

        Set(ByVal Value As String)
            strExplainationRemark = Value
        End Set
    End Property

    Public Property GLPeriodNumber() As String
        Get
            If strGLPeriodNumber = "" Then
                Return String.Empty
            Else
                Return strGLPeriodNumber.PadRight(2)
            End If
        End Get

        Set(ByVal Value As String)
            strGLPeriodNumber = Value
        End Set
    End Property

    Public Property FiscalYear() As String
        Get
            If strFiscalYear = "" Then
                Return String.Empty
            Else

                Return strFiscalYear.PadRight(2)
            End If

        End Get
        Set(ByVal Value As String)
            strFiscalYear = Value
        End Set
    End Property
    Public Property GLDateDay() As String
        Get
            If strGLDateDay = "" Then
                Return String.Empty
            Else
                If strGLDateDay.Length = 1 Then
                    strGLDateDay = "0" & strGLDateDay
                End If
                Return strGLDateDay.PadRight(2)
            End If
        End Get
        Set(ByVal Value As String)
            strGLDateDay = Value
        End Set
    End Property
    Public Property GLDateMonth() As String
        Get
            If strGLDateMonth = "" Then
                Return String.Empty
            Else
                If strGLDateMonth.Length = 1 Then
                    strGLDateMonth = "0" & strGLDateMonth
                End If
                Return strGLDateMonth.PadRight(2)
            End If
        End Get

        Set(ByVal Value As String)
            strGLDateMonth = Value
        End Set
    End Property

    Public Property GLDateYear() As String
        Get
            If strGLDateYear = "" Then
                Return String.Empty
            Else
                Return strGLDateYear.PadRight(2)
            End If


        End Get
        Set(ByVal Value As String)
            strGLDateYear = Value
        End Set
    End Property


    Public Property ServTaxDateDay() As String
        Get
            If strServTaxDateDay = "" Then
                Return String.Empty
            Else
                Return strServTaxDateDay.PadRight(2)
            End If
        End Get
        Set(ByVal Value As String)
            strServTaxDateDay = Value
        End Set
    End Property


    Public Property ServTaxDateMonth() As String
        Get
            If strServTaxDateMonth = "" Then
                Return String.Empty
            Else
                Return strServTaxDateMonth.PadRight(2)
            End If
        End Get
        Set(ByVal Value As String)
            strServTaxDateMonth = Value
        End Set
    End Property

    Public Property ServTaxDateYear() As String
        Get
            If strServTaxDateYear = "" Then
                Return String.Empty
            Else
                Return strServTaxDateYear.PadRight(2)
            End If
        End Get
        Set(ByVal Value As String)
            strServTaxDateYear = Value
        End Set
    End Property

    Public Property CheckDateDay() As String
        Get
            If strCheckDateDay = "" Then
                Return String.Empty
            Else
                Return strCheckDateDay.PadRight(2)
            End If
        End Get
        Set(ByVal Value As String)
            strCheckDateDay = Value
        End Set
    End Property
    Public Property CheckDateMonth() As String
        Get
            If strCheckDateMonth = "" Then
                Return String.Empty
            Else
                Return strCheckDateMonth.PadRight(2)
            End If
        End Get
        Set(ByVal Value As String)
            strCheckDateMonth = Value
        End Set
    End Property
    Public Property CheckDateYear() As String
        Get
            If strCheckDateYear = "" Then
                Return String.Empty
            Else
                Return strCheckDateYear.PadRight(2)
            End If
        End Get
        Set(ByVal Value As String)
            strCheckDateYear = Value
        End Set
    End Property
    Public Property CurrencyCode() As String
        Get
            If strCurrencyCode = "" Then
                Return String.Empty
            Else
                Return strCurrencyCode.Replace("&nbsp;", String.Empty).PadRight(3)
            End If
        End Get
        Set(ByVal Value As String)
            strCurrencyCode = Value
        End Set
    End Property
    Public Property LedgerType() As String
        Get
            If strLedgerType = "" Then
                Return String.Empty
            Else
                Return strLedgerType.Replace("&nbsp;", String.Empty).PadRight(2)
            End If

        End Get
        Set(ByVal Value As String)
            strLedgerType = Value
        End Set
    End Property
    Private strBuisinessUnit As String = ""
    Private strObjectAccount As String = ""
    Private strSubsidiary As String = ""
    Private strShortAccountID As String = ""
    Private strSubLedger As String = ""
    Private strSubLedgerType As String = ""
    Private strDocumentType As String = ""
    Private strDocumentNumber As String = ""
    Private strDocumentCompany As String = ""
    Private strInvoiceNumber As String = ""
    Private strPayMentNumber As String = ""
    Private strPayItem As String = ""
    Private strGLOffset As String = ""
    Private strAmount As String = ""
    Private strUnitAmount As String = ""
    Private strUnitOfMeasure As String = ""
    Private strReference1 As String = ""
    Private strReference2 As String = ""
    Private strExplainationAlpha As String = ""
    Private strExplainationRemark As String = ""
    Private strGLPeriodNumber As String = ""
    Private strFiscalYear As String = ""
    Private strGLDateDay As String = ""
    Private strGLDateMonth As String = ""
    Private strGLDateYear As String = ""
    Private strServTaxDateDay As String = ""
    Private strServTaxDateMonth As String = ""
    Private strServTaxDateYear As String = ""
    Private strCheckDateDay As String = ""
    Private strCheckDateMonth As String = ""
    Private strCheckDateYear As String = ""
    Private strCurrencyCode As String = ""
    Private strLedgerType As String = ""
    Private strDepartment As String = ""
    Private strCostCenter As String = ""
    Private strBookingFileLine As String = ""
    Private m_strCol3 As String

End Class
