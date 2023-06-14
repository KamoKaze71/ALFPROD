Imports System
Imports System.IO
Imports System.data
Imports System.Web
Imports System.Web.UI
Imports Microsoft.Win32
'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
''' <summary><para>Template Class for Wyeth Email Templates</para></summary>
Public Class textTemplate
    Const DEFAULT_PLACEHOLDER_BEGIN = "<<< "
    Const DEFAULT_PLACEHOLDER_END = " >>>"

    Private version As String
    Private DICTvariables As New Hashtable
    Private m_fileName As String    ''The filename of your template.
    Private placeHolderBegin As String  ''If you want to use your own placeholder characters. this is the beginning. e.g. <<<
    Private placeHolderEnd As String  ''If you want to use your own placeholder characters. this is the ending. e.g. >>>
    Private customLineBreak As String  ''You need a custom line-break? e.g. chr(13) or vbcrlf, etc.
   
    Public DEFAULT_LINEBREAK As Char = Chr(13)

    'Construktor => set the default values
    Public Sub New()
        MyBase.New()
        version = "TextTemplate Class - ver 0.1"
        filename = String.Empty
        placeHolderBegin = DEFAULT_PLACEHOLDER_BEGIN
        placeHolderEnd = DEFAULT_PLACEHOLDER_END
        customLineBreak = DEFAULT_LINEBREAK
    End Sub

    '******************************************************************************************************************
    '' @SDESCRIPTION:	Adds a variable which should be replaced
    '' @DESCRIPTION:	Adds a variable. All placeholders in the template using this Variable will be replaced by the
    ''					value of the variable.
    '' @PARAM:			- varName: The name of you variable (Important: every name just once!)
    '' @PARAM:			- varValue: The value you want to put instead the varName
    '******************************************************************************************************************
    Public Sub addVariable(ByVal varName As String, ByVal varValue As String)
        DICTvariables.Add(varName, varValue)
    End Sub

    '******************************************************************************************************************
    '' @SDESCRIPTION:	Returns a string where the template and the placeholders are merged
    '******************************************************************************************************************
    Public Function returnString() As String

        Dim fileToOPen As String
        Dim sr As StreamReader = File.OpenText(filename)
        Dim input As String

        If File.Exists(filename) Then
            input = sr.ReadToEnd
            sr.Close()
            input = replace_PlaceHolders(input)
        Else 'file does not exist
            input = "Error: Template-file does not exist."
        End If

        Return input

    End Function

    '******************************************************************************************************************
    '* replace_PlaceHolders 
    '******************************************************************************************************************

    Public Function replace_PlaceHolders(ByVal input As String) As String
        Dim tmp As String = input
        Dim toreplace, replaceby As String
        For Each Key As String In DICTvariables.Keys
            toreplace = UCase(placeHolderBegin & Key & placeHolderEnd)
            replaceby = DICTvariables.Item(Key)
            tmp = Replace(tmp, UCase(toreplace), checkIfNull(replaceby))
        Next

        'return the finished string
        Return tmp
    End Function

    '******************************************************************************************************************
    '* checkIfNull 
    '******************************************************************************************************************
    Private Function checkIfNull(ByVal val)
        If IsNothing(val) Then
            checkIfNull = ""
        Else
            checkIfNull = val
        End If

    End Function

    Public Property filename() As String
        Get
            Return m_fileName
        End Get
        Set(ByVal Value As String)
            m_fileName = Value
        End Set
    End Property

End Class

