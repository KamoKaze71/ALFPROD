Imports System
Imports System.Drawing


'/// <summary>
'/// Summary description for SystemTrayNotificationEventType.
'/// </summary>

Public Enum SystemTrayNotificationEventType
    Hiding
    Showing
    StartingAnimation
    StopingAnimation
    IconChanged
    Disposing
End Enum
'/// <summary>
'/// Summary description for SystemTrayNotificationEventArgs.
'/// </summary>
Public Class SystemTrayNotificationEventArgs
    Inherits EventArgs

    Private state As SystemTrayNotificationEventType



    'Public Property CodeID() As Integer
    '    Get
    '        Return m_i_code_id

    '    End Get
    '    Set(ByVal Value As Integer)
    '        m_i_code_id = Value
    '    End Set
    'End Property

    Public ReadOnly Property SystemTrayNotificationEventArgs(ByVal state As SystemTrayNotificationEventType)
        'implements base
         Get
			me.state = state
          End Get

     end  property

    Public ReadOnly Property State() As SystemTrayNotificationEventType
        Get
            Return State
        End Get

    End Property


End Class


'/// <summary>
'/// Summary description for SystemTrayNotifyIcon.
'/// </summary>
Public Class SystemTrayNotifyIcon

    Private notifyIcon As New System.Windows.Forms.NotifyIcon  '= new NotifyIcon();	// NotifyIcon object
    Private BaseMenu As System.Windows.Forms.ContextMenu
    Private mainForm As System.Windows.Forms.Form
    Private iconArray() As System.Drawing.Icon
    Private mainIcon As System.Drawing.Icon
    Private iconTimer As System.Windows.Forms.Timer
    Private timerInterval As Integer = 200
    Private XiconCounter As Integer = 0
    Private totalAnimations As Integer = 0
    Private animationCounter As Integer = 0
    Private iconsLoaded As Boolean = False
    Private keepAnimationAlive As Boolean = False

    '// Declaring Event 
    Public Delegate Sub StatusChanged(ByVal sender As Object, ByVal e As SystemTrayNotificationEventArgs)
    Public Event StatusChanged As OnStatusChanged

    '/// <summary>
    '/// Property for showing and hiding icon
    '/// </summary>
    Public Property Visibility() As Boolean

			get

            Return notifyIcon.Visible
        End Get

        Set(ByVal Value As Boolean)
			{
            Switch(value)
				{
					case true
						if (notifyIcon.Visible == false)
                OnStatusChanged(this, New SystemTrayNotificationEventArgs(SystemTrayNotificationEventType.Showing))
                break()
					case false
						if (notifyIcon.Visible == true)
                    OnStatusChanged(this, New SystemTrayNotificationEventArgs(SystemTrayNotificationEventType.Hiding))
                    break()
				}
				notifyIcon.Visible = value;
			}
		}

                    '/// <summary>
                    '/// Property
                    '/// </summary>
		public bool KeepAnimationAlive

			get

            Return keepAnimationAlive
        End Get
        Set(ByVal Value As Boolean)

            Select Case (Value)

            Case True
                    If (keepAnimationAlive = False) Then
                        OnStatusChanged(Me, New SystemTrayNotificationEventArgs(SystemTrayNotificationEventType.StartingAnimation))
                        break()
                    End If

                Case False
                    If (keepAnimationAlive = True) Then
                        OnStatusChanged(Me, New SystemTrayNotificationEventArgs(SystemTrayNotificationEventType.StopingAnimation))
                        break()
                    End If

                    keepAnimationAlive = Value
        End Set

    End Property

                    '/// <summary>
                    '/// Overloaded Constructor -- 1 --
                    '/// Icon = Application Icon (Default),
                    '/// Tooltip = Application Name (Default),
                    '/// Visibility = Programmer must provide,
                    '/// ContextMenu = SystemTrayNotifyIcon class generated menu (Default).
                    '/// </summary>
    Public Sub SystemTrayNotifyIcon(ByVal form As System.Windows.Forms.Form, ByVal visible As Boolean)

        OnStatusChanged += New SystemTrayNotifyIcon.StatusChanged(SystemTrayNotificationHandler) '// Setting Event Handler

        mainForm = form
        notifyIcon.Visible = visible
        notifyIcon.Icon = mainForm.Icon
        notifyIcon.Text = mainForm.Text
        notifyIcon.ContextMenu = LoadDefaultContextMenu()
        iconTimer as New Timer
			iconTimer.Interval = timerInterval;
			iconTimer.Tick += new EventHandler(TimerProc);
    End Sub

    '/// <summary>
    '/// Overloaded Constructor -- 2 --
    '/// Icon = Application Icon (Default),
    '/// Tooltip = Programmer must provide,
    '/// Visibility = Programmer must provide,
    '/// ContextMenu = SystemTrayNotifyIcon class generated menu (Default).
    '/// </summary>

    Public Sub SystemTrayNotifyIcon(ByVal form As System.Windows.Forms.Form, ByVal visible As Boolean, ByVal tooltip As String)

			OnStatusChanged += new SystemTrayNotifyIcon.StatusChanged(SystemTrayNotificationHandler);	// Setting Event Handler
        mainForm = form
        notifyIcon.Visible = visible
        notifyIcon.Icon = mainForm.Icon
        notifyIcon.Text = tooltip
        notifyIcon.ContextMenu = LoadDefaultContextMenu()
        iconTimer = New Timer
        iconTimer.Interval = timerInterval
        iconTimer.Tick += New EventHandler(TimerProc)
    End Sub

    '/// <summary>
    '/// Overloaded Constructor -- 3 --
    '/// Icon = Programmer must provide,
    '/// Tooltip = Programmer must provide,
    '/// Visibility = Programmer must provide,
    '/// ContextMenu = SystemTrayNotifyIcon class generated menu (Default).
    '/// </summary>
    Public Sub SystemTrayNotifyIcon(ByVal form As System.Windows.Forms.Form, ByVal visible As Boolean, ByVal toolTip As String, ByVal icon As Icon)

        OnStatusChanged += New SystemTrayNotifyIcon.StatusChanged(SystemTrayNotificationHandler) '// Setting Event Handler
        mainForm = form
        notifyIcon.Visible = visible
			if (icon.Size.Height > 16 || icon.Size.Width > 16)
            notifyIcon.Icon = mainForm.Icon
        Else
            notifyIcon.Icon = icon
            notifyIcon.Text = toolTip
            notifyIcon.ContextMenu = LoadDefaultContextMenu()
            iconTimer = New Timer
            iconTimer.Interval = timerInterval
            iconTimer.Tick += New EventHandler(TimerProc)
    End Sub

    '/// <summary>
    '/// Overloaded Constructor -- 4 --
    '/// Icon = Application Icon (Default),
    '/// Tooltip = Programmer must provide,
    '/// Visibility = Programmer must provide,
    '/// ContextMenu = Programmer must provide.
    '/// </summary>
    Public Sub SystemTrayNotifyIcon(ByVal form As System.Windows.Forms.Form, ByVal visible As Boolean, ByVal toolTip As String, ByVal contextMenu As ContextMenu)

			OnStatusChanged += new SystemTrayNotifyIcon.StatusChanged(SystemTrayNotificationHandler);	// Setting Event Handler
        mainForm = form
        notifyIcon.Visible = visible
        notifyIcon.Icon = mainForm.Icon
        notifyIcon.Text = toolTip
        notifyIcon.ContextMenu = contextMenu
        iconTimer = New Timer
        iconTimer.Interval = timerInterval
        iconTimer.Tick += New EventHandler(TimerProc)
    End Sub

    '/// <summary>
    '/// Overloaded Constructor -- 5 --
    '/// Icon = Programmer must provide,
    '/// Tooltip = Programmer must provide,
    '/// Visibility = Programmer must provide,
    '/// ContextMenu = Programmer must provide.
    '/// </summary>
		public SystemTrayNotifyIcon(ByVal form As System.Windows.Forms.Form, ByVal visible As Boolean, ByVal toolTip As String, ByVal contextMenu As ContextMenu)
			OnStatusChanged += new SystemTrayNotifyIcon.StatusChanged(SystemTrayNotificationHandler)	'// Setting Event Handler
			mainForm = form
			notifyIcon.Visible = visible
			if (icon.Size.Height > 16 || icon.Size.Width > 16)
				notifyIcon.Icon = mainForm.Icon
            Else
				notifyIcon.Icon = icon
			notifyIcon.Text = toolTip
			notifyIcon.ContextMenu = contextMenu
			iconTimer = new Timer()
			iconTimer.Interval = timerInterval
			iconTimer.Tick += new EventHandler(TimerProc)
		end sub

    Public Sub Dispose()

        OnStatusChanged(this, New SystemTrayNotificationEventArgs(SystemTrayNotificationEventType.Disposing))
        iconTimer.Tick -= New EventHandler(TimerProc)
			if (BaseMenu != null)
            BaseMenu.Dispose()
            notifyIcon.Dispose()
    End Sub
    '/// <summary>
    '/// Destructor
    '/// </summary>
    '~SystemTrayNotifyIcon()
    '{
    '}

    '/// <summary>
    '/// Loads a Default ContextMenu
    '/// </summary>
    Private Function LoadDefaultContextMenu() As ContextMenu

        BaseMenu = New System.Windows.Forms.ContextMenu

        '// Adding menu items
        BaseMenu.MenuItems.Add(New MenuItem("Hide Icon", New System.EventHandler(DefaultMenuHandler)))
        BaseMenu.MenuItems.Add(New MenuItem("-", New System.EventHandler(DefaultMenuHandler)))
        BaseMenu.MenuItems.Add(New MenuItem("Exit Application", New System.EventHandler(DefaultMenuHandler)))

        Return BaseMenu
    End Function

    '/// <summary>
    '/// Default menu's Event Handler
    '/// </summary>
		private sub DefaultMenuHandler(sender as ,e as System.EventArgs) 

        Try

				select case (((MenuItem)sender).Text)

                Case "Animate"
                    Animate(-1, 100)
                    break()
                Case "Stop Animation"
						OnStatusChanged(me, new SystemTrayNotificationEventArgs(SystemTrayNotificationEventType.StopingAnimation));
                    keepAnimationAlive = False
                    break()
                Case "Hide Icon"
						OnStatusChanged(me, new SystemTrayNotificationEventArgs(SystemTrayNotificationEventType.Hiding));
                    notifyIcon.Visible = False
                    break()
                Case "Exit Application"
                    notifyIcon.Visible = False
                    mainForm.Close()
                    break()


			catch (Exception err)

            MessageBox.Show(Err.Message, "Error")

        End Try

        '/// <summary>
        '/// Animation Initializer for showing animation in System Tray -- 
        '/// nTimes = Programmer must provide,
        '/// timeInterval = 1000 equals 1 Second (Default: 200).
        '/// </summary>
		public sub Animate( nTimes as integer)

        If (!iconsLoaded) Then

            MessageBox.Show("LoadIcons() must be called before Animate().", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

			if ((nTimes == -1) || (nTimes > 0))

				if (BaseMenu != null)

                BaseMenu.MenuItems.RemoveAt(0)
                BaseMenu.MenuItems.Add(0, New MenuItem("Stop Animation", New System.EventHandler(DefaultMenuHandler)))
            End If
            totalAnimations = nTimes
            OnStatusChanged(Me, New SystemTrayNotificationEventArgs(SystemTrayNotificationEventType.StartingAnimation))
            keepAnimationAlive = True
            iconTimer.Start()
        End If
    End Sub

    '/// <summary>
    '/// Overloaded Animation Initializer for showing animation in System Tray -- 
    '/// nTimes = Programmer must provide,
    '/// timeInterval = Programmer must provide (1000 equals 1 Second) (Limit 50 - 50000).
    '/// </summary>
    Public Sub Animate(ByVal nTimes As Integer, ByVal timerinterval As Integer)

			timerInterval = (timerinterval>50000 || timerinterval<50)?200:timerinterval
        If (!iconsLoaded) Then

            MessageBox.Show("LoadIcons() must be called before Animate().", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
				return;
        End If

			if ((nTimes == -1) || (nTimes > 0))

				if (BaseMenu != null)

                BaseMenu.MenuItems.RemoveAt(0)
                BaseMenu.MenuItems.Add(0, New MenuItem("Stop Animation", New System.EventHandler(DefaultMenuHandler)))
            End If
            totalAnimations = nTimes
            iconTimer.Interval = timerinterval
            OnStatusChanged(this, New SystemTrayNotificationEventArgs(SystemTrayNotificationEventType.StartingAnimation))
            keepAnimationAlive = True
            iconTimer.Start()
        End If
    End Sub

    '/// <summary>
    '/// Loads default icons of size 16x16 for animation,
    '/// </summary>
    Public Sub LoadIcons(ByVal iconarray() As System.Drawing.Icon)

        iconsLoaded = True
        iconCounter = 0
        totalAnimations = 0
        mainIcon = notifyIcon.Icon
        iconarray = iconarray
			if (BaseMenu != null)
            BaseMenu.MenuItems.Add(0, New MenuItem("Animate", New System.EventHandler(DefaultMenuHandler)))

			for (int cnt = 1; cnt < iconArray.Length+1; cnt++)

                Try

					if (iconArray[cnt-1].Size.Height > 16 || iconArray[cnt-1].Size.Width > 16)
						iconArray[cnt-1] = mainForm.Icon
                    End If
				catch (Exception e)

                    MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Next
    End Sub


    '/// <summary>
    '/// Timer proc for showing animation effect in System Tray
    '/// </summary>
    Private Sub TimerProc(ByVal sender As Object, ByVal e As EventArgs)

			if (keepAnimationAlive == false)

            iconTimer.Stop()
            iconCounter = 0
            animationCounter = 0
            notifyIcon.Icon = mainIcon
				if (BaseMenu != null)

                BaseMenu.MenuItems.RemoveAt(0)
					BaseMenu.MenuItems.Add(0, new MenuItem("Animate", new System.EventHandler(DefaultMenuHandler)));
            End If

        Else

				OnStatusChanged(this, new SystemTrayNotificationEventArgs(SystemTrayNotificationEventType.IconChanged));
				notifyIcon.Icon = iconArray[iconCounter++]
				if (iconCounter == iconArray.Length)

                iconCounter = 0
					animationCounter++
            End If

				if ((animationCounter == totalAnimations) && (totalAnimations != -1))

                animationCounter = 0
                totalAnimations = 0
					OnStatusChanged(this, new SystemTrayNotificationEventArgs(SystemTrayNotificationEventType.StopingAnimation));
                keepAnimationAlive = False
            End If
        End If
    End Sub


    '/// <summary>
    '/// Default Event Handler
    '/// </summary>
    '/// <param name="sender"></param>
    '/// <param name="e"></param>
    Protected Sub SystemTrayNotificationHandler(ByVal sender As Object, ByVal e As SystemTrayNotificationEventArgs)

			// This event handler is not required here exactly
			// It just facilitates the programmer to save him/her from
			// exceptions if he/she don't provides his/her own EventHandler
			// function
    End Sub

End Class

