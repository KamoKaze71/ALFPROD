'********************************************************************************
'* Michal Gabrukiewicz - 05.04.2004
'********************************************************************************
Imports System.Web
Imports system
Imports system.Configuration
Imports System.Web.Caching, System.Xml, Microsoft.VisualBasic

Public Class Settings

    Private Shared machineNameLiveServer As String = ConstantsManager.getConstant("MACHINE_NAME_LIVE")
    Private Shared machineNameDevelopmentServer As String = ConstantsManager.getConstant("MACHINE_NAME_DEV")

    Public Shared connectionStringLive As String = ConstantsManager.getConstant("DB_CONNECTION_STRING_LIVE")
    Public Shared connectionStringDev As String = ConstantsManager.getConstant("DB_CONNECTION_STRING_DEV")
    Public Shared emailSendersName As String = ConstantsManager.getConstant("EMAIL_SENDERS_NAME")

    Private Shared urlLive As String = ConstantsManager.getConstant("APPLICATION_URL_LIVE")
    Private Shared urlDev As String = ConstantsManager.getConstant("APPLICATION_URL_DEV")
    Private Shared urlLocal As String = ConstantsManager.getConstant("APPLICATION_URL_LOCALHOST")

    Public Shared CompanyName As String = ConstantsManager.getConstant("COMPANY")
    Public Shared PathToDailySalesExe As String = ConstantsManager.getConstant("PathToDailySalesExe")
    Public Const emailHeaderTemplate As String = "emailTemplates/emailHeader.html"
    Public Const emailFooterTemplate As String = "emailTemplates/emailFooter.html"
    Public Shared SMSGatewayURL As String = ConstantsManager.getConstant("SMS_GATEWAY")

    '*****************************************************************************************
    '* returns the CONNECTIONSTRING 
    '*****************************************************************************************
    Public Shared ReadOnly Property connectionString() As String
        Get
            If Settings.isLiveServer Then
                Return Settings.connectionStringLive
            Else
                Return Settings.connectionStringDev
            End If
        End Get
    End Property

    '*****************************************************************************************
    '* returns the APPLICATION URL 
    '*****************************************************************************************
    Public Shared ReadOnly Property applicationUrl() As String
        Get
            If Settings.isLiveServer Then
                Return urlLive
            ElseIf Settings.isDevelopmentServer Then
                Return urlDev
            Else
                Return urlLocal
            End If
        End Get
    End Property

    '*****************************************************************************************
    '* returns weather the application is on development server or not 
    '*****************************************************************************************
    Public Shared ReadOnly Property isDevelopmentServer() As Boolean
        Get
            If Settings.getMachineName = Settings.machineNameDevelopmentServer.ToUpper Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property
    Public Shared ReadOnly Property isLocalMachine() As Boolean
        Get
            If Settings.getMachineName = Settings.machineNameDevelopmentServer.ToUpper = False And Settings.getMachineName = Settings.machineNameLiveServer = False Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    '*****************************************************************************************
    '* returns weather the root path of Intranet application  
    '*****************************************************************************************
    Public Shared ReadOnly Property IntranetApplicationRootPath() As String
        Get
            Return HttpContext.Current.Server.MapPath("\")

        End Get
    End Property

    '*****************************************************************************************
    '* returns weather the root path of Intranet application  
    '*****************************************************************************************
    Public Shared ReadOnly Property ALFApplicationRootPath() As String
        Get
            If System.Reflection.Assembly.GetExecutingAssembly.Location.ToString.IndexOf("alfservice") > 0 Then
                Return AppDomain.CurrentDomain.BaseDirectory.ToString.Substring(0, Len(AppDomain.CurrentDomain.BaseDirectory.ToString) - 15)
            Else
                Return HttpContext.Current.Request.PhysicalApplicationPath
            End If

        End Get
    End Property

    '*****************************************************************************************
    '* returns weather the Application root  - no hhtp context 
    '*****************************************************************************************
    Public Shared ReadOnly Property ApplicationRootPath() As String
        Get
            Return AppDomain.CurrentDomain.BaseDirectory.ToString
        End Get
    End Property

    '*****************************************************************************************
    '* returns the name of the machine where the application is running on 
    '*****************************************************************************************
    Public Shared Function getMachineName() As String
        'Return HttpContext.Current.ApplicationInstance.Server.MachineName.ToUpper
        Return Environment.MachineName()
    End Function

    Public Shared ReadOnly Property isLiveServer() As Boolean
        Get
            If Settings.getMachineName = Settings.machineNameLiveServer.ToUpper Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property


    Public Shared ReadOnly Property DomainWithoutHTTP() As String
        Get
            Dim Trimchars As Char() = "http:"
            If Settings.isLiveServer Then
                Return urlLive.TrimStart(Trimchars)
            ElseIf Settings.isDevelopmentServer Then
                Return urlDev.TrimStart(Trimchars)
            Else
                Return urlLocal.TrimStart(Trimchars)
            End If
        End Get
    End Property


End Class

Public Class ConstantsManager

    ' Cache object that will be used to store and retrieve items from
    ' the cache and constants used within this object

    Protected Friend Shared myCache As System.Web.Caching.Cache = System.Web.HttpRuntime.Cache()
    Private Shared applicationConstantsFile As String = "ApplicationConstantsFile"
    Public Shared applicationConstantsFileName As String = AppDomain.CurrentDomain.BaseDirectory.ToString.Replace("/", "\") & "ALF.config"
   
    Private Shared xmlFile As New XmlDocument
    Private Shared constantIdentifier As String = "constant"
    Private Shared constantKey As String = "cacheDependencyKey"


    Public Shared Function getConstant(ByRef key As String) As Object

        Dim tmpObj As Object
        If Not (myCache(constantIdentifier & key) Is Nothing) Then
            tmpObj = CType(myCache(constantIdentifier & key), Object)
        Else
            tmpObj = pullConstantFromFile(key)

            'Create the cache dependencies and insert the object into the cache
            If Not IsNothing(tmpObj) Then
                If myCache(constantKey) Is Nothing Then
                    myCache.Insert(constantKey, Now)
                End If
                myCache.Insert(constantIdentifier & key, tmpObj, _
                            New CacheDependency(applicationConstantsFileName))
            End If
        End If
        Return tmpObj
    End Function

    Private Shared Function pullConstantFromFile(ByRef key As String) As Object
        Dim obj As Object = 0
        If myCache(applicationConstantsFile) Is Nothing Then
            PopulateCache()
        End If

        'Attempt to find the element given the "key" for that tag
        Dim elementList As XmlNodeList = xmlFile.GetElementsByTagName(key)

        'If the key is found, the element list will have a count greater than
        'zero and we retrieve the value of the tag...
        If elementList.Count > 0 Then

            'Gets the node for the first element in the list of elements with
            'this tag name.  There should only be 1 so we return the first and
            'ignore the others.  If the node has a value, we retrieve the text        
            Dim node As XmlNode = elementList.Item(0)
            If Not (node Is Nothing) Then
                obj = node.InnerText()
            End If

            'If the value is a numeric, convert it to a number; otherwise
            'convert it to a string (we don't store values other than strings
            'and numbers).
            If IsNumeric(obj) Then
                obj = CType(obj, Integer)
            Else
                obj = CType(obj, String)
            End If
        End If
        Return obj
    End Function

    Private Shared Sub PopulateCache()
        'With a try around the entire event, the object attempts to load the XML
        'file and store it in the cache with a dependency on the XML file itself.
        'This means that any time the XML file changes, it is removed from the 
        'cache.  When the "getConstant" method is called again, the XML file won't
        'exist in memory and the PopulateCache will be re-called.
        Try
            xmlFile.Load(applicationConstantsFileName)
            myCache.Insert(applicationConstantsFile, xmlFile, _
                            New CacheDependency(applicationConstantsFileName))
        Catch e As Exception
            ExceptionInfo.Show(e)
        End Try
    End Sub

End Class

