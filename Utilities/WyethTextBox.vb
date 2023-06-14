Imports System
Imports System.web
Imports System.Drawing
Imports System.Web.UI.WebControls
Imports System.Web.UI
Imports System.Web.UI.Page
Imports System.Globalization
Imports Wyeth.Utilities.NumberFormat
'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
''' <summary><para>Self Validating TextBox for Wyeth</para></summary>
Public Class WyethTextBox
    Inherits System.Web.UI.WebControls.TextBox
    Implements IValidator

	Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
		MyBase.OnInit(e)

		Dim unit As New System.web.UI.WebControls.Unit

        If Me.FieldType = "DATE" Then
            If CssClass = String.Empty Then
                Me.CssClass = "formfield_enter"
            End If
        End If
        Page.Validators.Add(Me)

	End Sub

	Protected Overrides Sub OnUnload(ByVal e As System.EventArgs)
		If (Page Is Nothing) Then
			Page.Validators.Remove(Me)
			MyBase.OnUnload(e)
		End If
	End Sub

    Public Overridable Sub Validate() Implements IValidator.Validate
        If Me.Visible = True Then


            Me.ISValid = True

            ' Checks if blank is allowed
            If (Me.AllowBlank = False) Then
                Dim isBlank As Boolean = (Me.Text.Trim() = "")

                If (isBlank = True) Then
                    Me.ErrorMessage = String.Format("'{0}' cannot be blank.", Me.FriendlyName)
                    Me.ISValid = False
                End If

            End If

            If FieldType = "INTEGER" Then
                Try
                    _valueInt = Int32.Parse(Me.Text, System.Globalization.NumberStyles.Any, GetMyNFI())
                    If (_valueInt < Me.MinValueInt) Then
                        Me.ErrorMessage = String.Format("'{0}' cannot be less than {1}", Me.FriendlyName, Me.MinValueInt)
                        Me.ISValid = False
                    End If

                    If (_valueInt > Me.MaxValueInt) Then
                        Me.ErrorMessage = String.Format("'{0}' cannot be more than {1}", Me.FriendlyName, Me.MaxValueInt)
                        Me.ISValid = False
                    End If

                Catch
                    Me.ErrorMessage = String.Format("'{0}' is not a valid integer.", Me.FriendlyName)
                    Me.ISValid = False
                    'WriteErrors()
                End Try

            ElseIf FieldType = "DOUBLE" Then
                Try
                    _valueDouble = Double.Parse(Me.Text, System.Globalization.NumberStyles.Any, GetMyNFI())
                    If (_valueDouble < Me.MinValueDouble) Then
                        Me.ErrorMessage = String.Format("'{0}' cannot be less than {1}", Me.FriendlyName, Me.MinValueDouble)
                        Me.ISValid = False
                    End If

                    If (_valueDouble > Me.MaxValueDouble) Then
                        Me.ErrorMessage = String.Format("'{0}' cannot be more than {1}", Me.FriendlyName, Me.MaxValueDouble)
                        Me.ISValid = False
                    End If

                Catch
                    Me.ErrorMessage = String.Format("'{0}' is not a valid Number.", Me.FriendlyName)
                    Me.ISValid = False

                End Try

            ElseIf FieldType = "DATE" Then

                Dim MyDTFI As DateTimeFormatInfo = New CultureInfo("en-US", False).DateTimeFormat
                Try
                    MyDTFI.ShortDatePattern = Helper.DATEFORMAT_STRING_REPORT

                    _valueDate = Date.ParseExact(Me.Text, Helper.DATEFORMAT_STRING_REPORT, MyDTFI)

                    If (_valueDate < Me.MinValueDate) Then
                        Me.ErrorMessage = String.Format("'{0}' cannot be less than {1}", Me.FriendlyName, Me.MinValueDate)
                        Me.ISValid = False
                    End If

                    If (_valueDate > Me.MaxValueDate) Then
                        Me.ErrorMessage = String.Format("'{0}' cannot be more than {1}", Me.FriendlyName, Me.MaxValueDate)
                        Me.ISValid = False
                    End If


                Catch ex As Exception
                    Me.ErrorMessage = String.Format("'{0}' is not a valid Date.", Me.FriendlyName)
                    Me.ISValid = False


                End Try

            End If
        End If

    End Sub

    Private _valid As Boolean = True
    Private _errorMessage As String = ""
    Private _blankAllowed As Boolean = False
    Private _friendlyName As String = ""
    Private _type As String = "TEXT"
    Private _maxValueInt As Integer = Integer.MaxValue
    Private _minValueInt As Integer = Integer.MinValue
    Private _minValueDouble As Double = Double.MinValue
    Private _maxValueDouble As Double = Double.MaxValue
    Private _minValueDate As Date = Date.MinValue
    Private _maxValueDate As Date = Date.MaxValue
    Private _valueDouble As Double
    Private _valueDate As Date
    Private _valueInt As Integer
    Private _storedBorderColor As Color = Me.BorderColor

    Public Overridable Property ISValid() As Boolean Implements System.Web.UI.IValidator.IsValid
        Get
            Return _valid
        End Get
        Set(ByVal Value As Boolean)
            _valid = Value
            If (_valid = False) Then
                Me.BorderColor = Color.Red
            Else
                Me.BorderColor = Me._storedBorderColor
            End If

        End Set
    End Property
    Public Overridable Property ErrorMessage() As String Implements System.Web.UI.IValidator.ErrorMessage
        Get
            Return _errorMessage
        End Get
        Set(ByVal Value As String)
            _errorMessage = Value
        End Set
    End Property
    Public Overridable Property FieldType() As String
        Get
            Return _type
        End Get
        Set(ByVal Value As String)
            _type = UCase(Value)
        End Set
    End Property
    Public Property AllowBlank() As Boolean
        Get
            Return _blankAllowed
        End Get
        Set(ByVal Value As Boolean)
            _blankAllowed = Value
        End Set
    End Property
    Public Property FriendlyName() As String
        Get
            Return _friendlyName
        End Get
        Set(ByVal Value As String)
            _friendlyName = Value
        End Set
    End Property
    Public Property MinValueInt() As Integer
        Get
            Return _minValueInt
        End Get
        Set(ByVal Value As Integer)
            _minValueInt = Value

            If (_minValueInt > _maxValueInt) Then
                Dim swap As Integer = _minValueInt
                _minValueInt = _maxValueInt
                _maxValueInt = swap
            End If

        End Set
    End Property
    Public Property MaxValueInt() As Integer
        Get
            Return _maxValueInt
        End Get
        Set(ByVal Value As Integer)
            _maxValueInt = Value

            If (_maxValueDate > _maxValueDate) Then
                Dim swap As Integer = _minValueInt
                _maxValueInt = _maxValueInt
                _maxValueInt = swap
            End If

        End Set
    End Property
    Public Property MinValueDate() As Date
        Get
            Return _minValueDate
        End Get
        Set(ByVal Value As Date)
            _minValueDate = Value

            If (_minValueDate > _maxValueDate) Then
                Dim swap As Date = _minValueDate
                _minValueDate = _maxValueDate
                _maxValueDate = swap
            End If

        End Set
    End Property
    Public Property MaxValueDate() As Date
        Get
            Return _maxValueDate
        End Get
        Set(ByVal Value As Date)
            _maxValueDate = Value

            If (_maxValueInt > _maxValueInt) Then
                Dim swap As Date = _minValueDate
                _maxValueDate = _maxValueDate
                _maxValueDate = swap
            End If

        End Set
    End Property
    Public Property MinValueDouble() As Double
        Get
            Return _minValueDouble
        End Get
        Set(ByVal Value As Double)
            _minValueDouble = Value

            If (_minValueDouble > _maxValueDouble) Then
                Dim swap As Double = _minValueDouble
                _minValueDouble = _maxValueDouble
                _maxValueDouble = swap
            End If

        End Set
    End Property
    Public Property MaxValueDouble() As Double
        Get
            Return _maxValueDouble
        End Get
        Set(ByVal Value As Double)
            _maxValueDouble = Value

            If (_maxValueDouble > _minValueDouble) Then
                Dim swap As Double = _minValueDouble
                _maxValueDouble = _minValueDouble
                _maxValueDouble = swap
            End If

        End Set
    End Property

End Class
