Imports Wyeth.Alf.CssStyles
Imports Wyeth.Utilities
Imports Wyeth.Utilities.Helper
Imports System.Globalization

Public Class Holidays
	Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Button_Add As System.Web.UI.WebControls.Button
    Protected WithEvents GridPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents EditPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents Button_Insert As System.Web.UI.WebControls.Button
    Protected WithEvents Button_Update As System.Web.UI.WebControls.Button
    Protected WithEvents txtholidayID As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtHolidayDay As System.Web.UI.WebControls.TextBox
    Protected WithEvents Button_Cancel As System.Web.UI.WebControls.Button
    Protected WithEvents inpID As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents FilterPanel As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents kalenderImage As System.Web.UI.HtmlControls.HtmlImage
    Protected WithEvents txtDayOfWeek As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim MyHoliday As New Holiday
    Dim myJSpopUp As New JSPopUp(Me)


	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		'Put user code to initialize the page here
		If Page.IsPostBack Then
		Else
			lblPageTitle.Text = Request.QueryString("pageTitle")
            myJSpopUp.AddDatePopupToControl(txtHolidayDay, kalenderImage)
            BindData()
		End If


	End Sub
	Private Sub BindData()
		SetGridStyles(MyGrid)		'Apply default Css Style Settings


		MyHoliday.HoliDayCountryID = Session("country_id")
		MyGrid.DataSource = MyHoliday.GetHoliDays()
		MyGrid.DataBind()
	End Sub


	Public Sub MyGrid_ItemCommand(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1CommandEventArgs) Handles MyGrid.ItemCommand


		If e.CommandName = "Delete" Then

			MyHoliday.HoliDayID = e.Item.Cells(0).Text

			If (MyHoliday.delete()) Then
				BindData()
			Else
				Dim strScript As String
				strScript = "<script language =javascript >"
				strScript += "window.open('../error.aspx?ErrorId=1','Error','width=300,height=250,left=270,top=180');"
				strScript += "</script>"

				RegisterClientScriptBlock("anything", strScript)
			End If

		ElseIf Me.inpID.Value = "" Then
			EditPanel.Visible = True
			Button_Insert.Visible = False
			Button_Update.Visible = True
			GridPanel.Visible = False
			FilterPanel.Visible = False

			txtholidayID.Text = e.Item.Cells(0).Text
            txtHolidayDay.Text = e.Item.Cells(1).Text
            txtDayOfWeek.Text = e.Item.Cells(2).Text
		End If
		


	End Sub
	
	Private Sub Button_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Add.Click

		EditPanel.Visible = True
		Button_Update.Visible = False
		Button_Insert.Visible = True
		GridPanel.Visible = False
		FilterPanel.Visible = False

		txtHolidayDay.Text = ""
        txtholidayID.Text = ""
        txtDayOfWeek.Text = ""

	End Sub
	Private Sub Button_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Cancel.Click
		GridPanel.Visible = True
		EditPanel.Visible = False
		FilterPanel.Visible = True
	End Sub
	Private Sub Button_Update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Update.Click
		setvalues("update")
		If MyHoliday.Update() Then

			EditPanel.Visible = False
			GridPanel.Visible = True
			FilterPanel.Visible = True
			BindData()
		Else
			Dim strScript As String
			strScript = "<script language =javascript >"
			strScript += "window.open('../error.aspx?ErrorId=1','Error','width=300,height=250,left=270,top=180');"
			strScript += "</script>"

			RegisterClientScriptBlock("anything", strScript)
		End If

	End Sub
	Private Sub Button_Insert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Insert.Click
		setvalues("insert")

		If (MyHoliday.insert()) Then
			EditPanel.Visible = False
			GridPanel.Visible = True
			FilterPanel.Visible = True
			BindData()
		Else
			Dim strScript As String
			strScript = "<script language =javascript >"
			strScript += "window.open('../error.aspx?ErrorId=1','Error','width=300,height=250,left=270,top=180');"
			strScript += "</script>"

			RegisterClientScriptBlock("anything", strScript)
		End If
	End Sub
	Private Sub setvalues(ByVal type As String)
		If type = "update" Then
			MyHoliday.HoliDayID = txtholidayID.Text
		End If
		MyHoliday.HoliDayCountryID = Session("country_id")
		MyHoliday.HoliDayUserID = Session("user_id")
		Dim MyDTFI As DateTimeFormatInfo = New CultureInfo("en-US", False).DateTimeFormat
		MyDTFI.DateSeparator = "-"
		MyDTFI.ShortDatePattern = Utilities.Helper.DATEFORMAT_STRING_REPORT
		MyDTFI.FullDateTimePattern = Utilities.Helper.DATEFORMAT_STRING_REPORT
		MyDTFI.YearMonthPattern = "MM-yyyy"
		MyHoliday.HoliDayDay = Date.ParseExact(txtHolidayDay.Text, Utilities.Helper.DATEFORMAT_STRING_REPORT, MyDTFI)



	End Sub

	Private Sub MyGrid_ItemCreated(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated

		If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Then

            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
			e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor1Class';")
			CType(e.Item.Cells(3).Controls(0), WebControl).CssClass = "button_common"
			CType(e.Item.Cells(3).Controls(0), WebControl).Attributes.Add("onclick", "javascript:return getconfirm();")
            e.Item.Attributes.Add("onclick", "javascript:__doPostBack('" & "MyGrid$" & "R" & (e.Item.ItemIndex) & "$_ctl0','');")

        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then

            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor2Class';")
            CType(e.Item.Cells(3).Controls(0), WebControl).CssClass = "button_common"
            CType(e.Item.Cells(3).Controls(0), WebControl).Attributes.Add("onclick", "javascript:return getconfirm();")
            e.Item.Attributes.Add("onclick", "javascript:__doPostBack('" & "MyGrid$" & "R" & (e.Item.ItemIndex) & "$_ctl0','');")

        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.SelectedItem Then

            e.Item.Attributes.Add("onclick", "javascript:__doPostBack('" & "MyGrid$" & "R" & (e.Item.ItemIndex) & "$_ctl0','');")

        End If
       



	End Sub
End Class
