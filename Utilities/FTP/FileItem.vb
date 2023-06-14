'**********************************************************************************
' $Archive: $ 
' $Author: $ 
' $Date: $  $Revision: $
' Description : Classe Permettant de gérer les infos sur les fichiers
' *********************************************************************************

Option Strict On
Option Explicit

'
'   FileItem class
'     written in VB.NET                          Version: 1.0
'     by The KPD-Team                            Date: 2002/02/06
'     Copyright © 2002                           Comments to: KPDTeam@allapi.net
'                                                URL: http://www.allapi.net/
'
'
'  You are free to use this class file in your own applications,
'  but you are expressly forbidden from selling or otherwise
'  distributing this code as source without prior written consent.
'  This includes both posting samples on a web site or otherwise
'  reproducing it in text or html format.
'
'  Although much care has gone into the programming of this class
'  file, The KPD-Team does not accept any responsibility for damage
'  caused by possible errors in this class and/or by misuse of this
'  class.
'

Imports System
Imports System.IO

'/// <summary>Represents a file from the local or a remote system.</summary>
Public Class FileItem
Implements IComparable

	' Private Variables
	Private m_FileTitle As String = ""
	Private m_FileSize As Long = 0
	Private m_FileOwner As String = ""
	Private m_FileGroup As String = ""
	Private m_FilePermissions As String = ""
	Private m_FileDate As DateTime
	Private m_IsDirectory As Boolean
	Private m_FilePath As String = ""

	'/// <summary>Constructs a new FileItem object.</summary>
	Public Sub New()
		'Do Nothing
	End Sub
	'/// <summary>Constructs a new FileItem object.</summary>
	'/// <param name="ObjectName">Specifies the full path of the object in question.</param>
	'/// <param name="IsDirectory">Specifies whether the specified object is a directory or a file.</param>
	'/// <exceptions cref="ArgumentNullException">Thrown when the specifed Filename is Nothing (C#, VC++: null)</exceptions>
	'/// <exceptions cref="ArgumentException">Thrown when there was an error querying the information of the specified object.</exceptions>
	Public Sub New(ObjectName as String, IsDirectory as Boolean)
		If ObjectName Is Nothing Then Throw New ArgumentNullException()
		m_IsDirectory = IsDirectory
		If m_IsDirectory Then
			Try
				Dim di as New DirectoryInfo(ObjectName)
				m_FileTitle = di.Name
				m_FileSize = 0
				m_FilePath = di.FullName
				m_FilePath = m_FilePath.Substring(0, m_FilePath.Length - m_FileTitle.Length)
				m_FileDate = di.LastWriteTime
			Catch
				Throw New ArgumentException()
			End try
		Else
			Try
				Dim fi as New FileInfo(ObjectName)
				m_FileTitle = fi.Name
				m_FileSize = fi.Length
				m_FilePath = fi.DirectoryName
				m_FileDate = fi.LastWriteTime
			Catch
				Throw New ArgumentException()
			End Try
		End If
	End Sub
	'/// <summary>Specifies the path of the object.</summary>
	'/// <value>A String that specifies the path of the object.</value>
	'/// <exceptions cref="ArgumentNullException">Thrown when the specified value is Nothing (C#, VC++: null).</exceptions>
	Public Property FilePath as String
		Get
			Return m_FilePath
		End Get
		Set(value as String)
			If value Is Nothing Then Throw New ArgumentNullException()
			m_FilePath = value
		End set
	End Property
	'/// <summary>Specifies whether the object is directory or not.</summary>
	'/// <value>A Boolean that specifies whether the object is a directory or not.</value>
	Public Property IsDirectory as Boolean
		Get
			Return m_IsDirectory
		End Get
		Set(value as Boolean)
			m_IsDirectory = value
		End set
	End Property
	'/// <summary>Specifies the date of the object.</summary>
	'/// <value>A DateTime object that specifies the date of the object.</value>
	Public Property FileDate as DateTime
		Get
			Return m_FileDate
		End Get
		Set(value as DateTime)
			m_FileDate = value
		End set
	End Property
	'/// <summary>Specifies the permissions of the file.</summary>
	'/// <value>A String that specifies the permissions for the file.</value>
	'/// <exceptions cref="ArgumentNullException">Thrown when the specified value is Nothing (C#, VC++: null).</exceptions>
	Public Property FilePermissions as String
		Get
			Return m_FilePermissions
		End Get
		Set(value as String)
			If value Is Nothing Then Throw New ArgumentNullException()
			m_FilePermissions = value
		End set
	End Property
	'/// <summary>Specifies the group of users the file belongs to.</summary>
	'/// <value>A String that specifies the group of users the file belongs to.</value>
	'/// <exceptions cref="ArgumentNullException">Thrown when the specified value is Nothing (C#, VC++: null).</exceptions>
	Public Property FileGroup as String
		Get
			Return m_FileGroup
		End Get
		Set(value as String)
			If value Is Nothing Then Throw New ArgumentNullException()
			m_FileGroup = value
		End set
	End Property
	'/// <summary>Specifies the owner of the file.</summary>
	'/// <value>A String that specifies the owner of the file.</value>
	'/// <exceptions cref="ArgumentNullException">Thrown when the specified value is Nothing (C#, VC++: null).</exceptions>
	Public Property FileOwner as String
		Get
			Return m_FileOwner
		End Get
		Set(value as String)
			If value Is Nothing Then Throw New ArgumentNullException()
			m_FileOwner = value
		End set
	End Property
	'/// <summary>Specifies the size of the file.</summary>
	'/// <value>A Long that specifies the size of the file.</value>
	Public Property FileSize as Long
		Get
			Return m_FileSize
		End Get
		Set(value as Long)
			m_FileSize = value
		End set
	End Property
	'/// <summary>Specifies the title of the file.</summary>
	'/// <value>A String that specifies the title of the file.</value>
	'/// <exceptions cref="ArgumentNullException">Thrown when the specified value is Nothing (C#, VC++: null).</exceptions>
	Public Property FileTitle as String
		Get
			Return m_FileTitle
		End Get
		Set(value as String)
			If value Is Nothing Then Throw New ArgumentNullException()
			m_FileTitle = value
		End set
	End Property
	'/// <summary>Compares this object to another FileItem object.</summary>
	'/// <returns>Returns 1 if the passed FileItem object should be placed above this FileItem, -1 if the passed FileItem should be placed below this FileItem and 0 if it is the same.</returns>
	Public Overridable Function CompareTo(obj as Object) as Integer Implements IComparable.CompareTo
		If obj Is Nothing Then Return -1
		Dim ct as FileItem = CType(obj, FileItem)
		If m_IsDirectory AndAlso Not(ct.IsDirectory) Then
			Return -1
		ElseIf Not(m_IsDirectory) AndAlso ct.IsDirectory Then
			Return 1
		ElseIf m_FileTitle.ToLower > ct.FileTitle.ToLower Then
			Return 1
		ElseIf m_FileTitle.ToLower < ct.FileTitle.ToLower Then
			Return -1
		Else
			Return 0
		End If
	End Function
	
End Class
