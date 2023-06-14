Imports C1.Web.C1WebGrid
Imports Wyeth.Alf
Imports Wyeth.Alf.WyethCodes
Imports Wyeth.Alf.CssStyles
Imports Wyeth.Alf.JSPopUp


Imports Wyeth.Utilities

Public Class ViewLogs
    Inherits Wyeth.Alf.AlfPage

    Dim MyLog As New Log
    Dim MyJs As New JSPopUp(Me)
	Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents detailPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents detailLogText As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtStartDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents txtEndDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents Button_showLogs As System.Web.UI.WebControls.Button
    Protected WithEvents button_deleteLogs As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents imgstartkal As System.Web.UI.HtmlControls.HtmlImage
    Protected WithEvents imgendkal As System.Web.UI.HtmlControls.HtmlImage
    Dim MyDataView As New DataView

#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
	Protected WithEvents MyDropDown As System.Web.UI.WebControls.DropDownList


	'NOTE: The following placeholder declaration is required by the Web Form Designer.
	'Do not delete or move it.
	Private designerPlaceholderDeclaration As System.Object

	Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
		'CODEGEN: This method call is required by the Web Form Designer
		'Do not modify it using the code editor.
		InitializeComponent()
	End Sub

#End Region

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		If Page.IsPostBack Then

        Else
            
            MyJs.PageURL = "../../util/datepicker.aspx"
            MyJs.AddDatePopupToControl(txtStartDate, imgstartkal)
            MyJs.AddDatePopupToControl(txtEndDate, imgendkal)

            txtStartDate.Text = DateAdd(DateInterval.Day, -7, Today()).ToString(Wyeth.Utilities.Helper.DATEFORMAT_STRING_REPORT, CType(Application("MyDTFI"), IFormatProvider))
            txtEndDate.Text = Today().ToString(Wyeth.Utilities.Helper.DATEFORMAT_STRING_REPORT, CType(Application("MyDTFI"), IFormatProvider))

            MyDropDown.EnableViewState = True
            MyDropDown.DataValueField = "code_id"
            MyDropDown.DataTextField = "code_description"
            MyDropDown.DataSource = GetCodesByCat("Log Table", Session("country_id"))
            MyDropDown.SelectedIndex = 0
            MyDropDown.DataBind()


           

            BindData()

        End If
        MyJs.Height = 200
        MyJs.Width = 400
        MyJs.PageURL = "DeleteLogs.aspx?logs_code_id=" & MyDropDown.SelectedValue & "&logs_code=" & Me.MyDropDown.SelectedItem.Text
        MyJs.AddPopupToControl(button_deleteLogs)

        button_deleteLogs.Value = "Delete " & MyDropDown.SelectedItem.ToString & " Log"

	End Sub
	Private Sub BindData()
		setGridStyles(MyGrid)
        MyDataView = MyLog.ViewLogs(CInt(MyDropDown.SelectedValue), txtStartDate.Text, txtEndDate.Text)
		MyGrid.DataSource = MyDataView
        MyGrid.DataBind()

	End Sub
	
    Private Sub MyDropDown_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyDropDown.SelectedIndexChanged
        button_deleteLogs.Value = "Delete " & MyDropDown.SelectedItem.ToString & " Log"
        MyGrid.DataSource = MyDataView
        MyGrid.DataBind()
    End Sub
    'Private Sub Button_DeleteLogs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_DeleteLogs.Click
    '       MyLog.DeleteLogs(Session("country_id"), MyDropDown.SelectedValue)
    '	BindData()
    '   End Sub

    Private Sub MyGrid_ItemDataBound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound
        e.Item.ToolTip = e.Item.Cells(3).Text
        '   e.Item.Attributes.Add("onclick", "javascript:__doPostBack('" & "MyGrid$" & "R" & (e.Item.ItemIndex) & "$_ctl0','');")

    End Sub


    Private Sub MyGrid_ItemCreated(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Then
            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor1Class';")

        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then

            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor2Class';")

        End If
        e.Item.Attributes.Add("onclick", "javascript:__doPostBack('" & "MyGrid$" & "R" & (e.Item.ItemIndex) & "$_ctl0','');")




    End Sub

    Public Sub MyGrid_ItemCommand(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1CommandEventArgs) Handles MyGrid.ItemCommand
        Me.MyGrid.Visible = False
        Me.detailPanel.Visible = True
        Me.detailLogText.Text = e.Item.Cells(3).Text
    End Sub

    Private Sub Button_showLogs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_showLogs.Click
        Page.Validate()
        If Me.IsValid = True Then
            BindData()
        End If

    End Sub
End Class
