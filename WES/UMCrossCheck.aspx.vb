Imports Wyeth.Utilities.Helper
Imports Wyeth.Utilities.DateHandling
Imports Wyeth.Alf.WyethDropdown
Imports Wyeth.Alf.CssStyles


Public Class UMCrossCheck
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents ddDistribSelect As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnGenRep As System.Web.UI.WebControls.Button
    Protected WithEvents gridpanel As System.Web.UI.WebControls.Panel
    Protected WithEvents txtStartDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents txtEndDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents repData As reportData
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents prtControl As printReportCtl
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents StartImage As System.Web.UI.WebControls.Image
    Protected WithEvents EndImage As System.Web.UI.WebControls.Image

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim MyStock As New Stock
    Dim Type As String = "UM"
    Dim MyDatePopUp As New JSPopUp(Me.page)

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Page.IsPostBack = True Then

        Else
            GetDistribSelectDD(ddDistribSelect, Session("country_id"))
            Me.lblPageTitle.Text = Request.QueryString("PageTitle")
            SetGridStyles(MyGrid)

            txtStartDate.Text = FirstOfThisMonth(repData.lastOrderDate).ToString(DATEFORMAT_STRING_REPORT)
            txtEndDate.Text = LastOfThisMonth(repData.lastOrderDate).ToString(DATEFORMAT_STRING_REPORT)

            MyDatePopUp.AddDatePopupToControl(txtStartDate, StartImage)
            MyDatePopUp.AddDatePopupToControl(txtEndDate, EndImage)
            BindData()
        End If


    End Sub

    Private Sub fillReportData()
        repData.lastOrderDate = txtEndDate.Text
        repData.addLine("Report-date from", txtStartDate.Text, True, False)
        repData.addLine("Report-date to", txtEndDate.Text, True, False)
        repData.addLine("Selected Distributot", ddDistribSelect.SelectedItem.Text.ToString, True, False)
    End Sub


    Private Sub BindData()
        fillReportData()
        MyStock.StockDistID = ddDistribSelect.SelectedValue
        MyStock.StockEndDate = txtEndDate.Text
        MyStock.StockStartDate = txtStartDate.Text
        MyGrid.DataSource = MyStock.GetStockCrossCheckUM()
        MyGrid.DataBind()
    End Sub

    Private Sub C1WebGrid1_SortCommand(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1SortCommandEventArgs) Handles MyGrid.SortCommand
        BindData()
    End Sub

    Private Sub btnGenRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRep.Click
        Page.Validate()
        If Page.IsValid Then
            BindData()
        End If
    End Sub
    Private Sub item_Databound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound

        If e.Item.Cells(2).Text <> "&nbsp;" Then
            e.Item.Cells(2).Attributes.Add("onclick", "javascript:OpenPopUp('KORR.aspx?id=" & (e.Item.Cells(0).Text) & "&type=" & Type & "&sd=" & MyStock.StockStartDate & "&ed=" & MyStock.StockEndDate & "&dist_id=" & ddDistribSelect.SelectedValue & "&phznr=" & e.Item.Cells(2).Text & "&presentation=" & e.Item.Cells(3).Text & "&pagetitle=Actuals to Sample" & "  ','KORR');")
            e.Item.Cells(3).Attributes.Add("onclick", "javascript:OpenPopUp('KORR.aspx?id=" & (e.Item.Cells(0).Text) & "&type=" & Type & "&sd=" & MyStock.StockStartDate & "&ed=" & MyStock.StockEndDate & "&dist_id=" & ddDistribSelect.SelectedValue & "&phznr=" & e.Item.Cells(2).Text & "&presentation=" & e.Item.Cells(3).Text & "&pagetitle=Actuals to Sample" & "  ','KORR');")
            e.Item.Cells(4).Attributes.Add("onclick", "javascript:OpenPopUp('KORR.aspx?id=" & (e.Item.Cells(0).Text) & "&type=" & Type & "&sd=" & MyStock.StockStartDate & "&ed=" & MyStock.StockEndDate & "&dist_id=" & ddDistribSelect.SelectedValue & "&phznr=" & e.Item.Cells(2).Text & "&presentation=" & e.Item.Cells(3).Text & "&pagetitle=Actuals to Sample" & "  ','KORR');")
        End If

        If e.Item.Cells(5).Text <> "&nbsp;" Then
            e.Item.Cells(5).Attributes.Add("onclick", "javascript:OpenPopUp('KORR.aspx?id=" & (e.Item.Cells(1).Text) & "&type=" & Type & "&sd=" & MyStock.StockStartDate & "&ed=" & MyStock.StockEndDate & "&dist_id=" & ddDistribSelect.SelectedValue & "&phznr=" & e.Item.Cells(6).Text & "&presentation=" & e.Item.Cells(7).Text & "&pagetitle=Actuals to Sample" & " ','KORR');")
            e.Item.Cells(6).Attributes.Add("onclick", "javascript:OpenPopUp('KORR.aspx?id=" & (e.Item.Cells(1).Text) & "&type=" & Type & "&sd=" & MyStock.StockStartDate & "&ed=" & MyStock.StockEndDate & "&dist_id=" & ddDistribSelect.SelectedValue & "&phznr=" & e.Item.Cells(6).Text & "&presentation=" & e.Item.Cells(7).Text & "&pagetitle=Actuals to Sample" & " ','KORR');")
            e.Item.Cells(7).Attributes.Add("onclick", "javascript:OpenPopUp('KORR.aspx?id=" & (e.Item.Cells(1).Text) & "&type=" & Type & "&sd=" & MyStock.StockStartDate & "&ed=" & MyStock.StockEndDate & "&dist_id=" & ddDistribSelect.SelectedValue & "&phznr=" & e.Item.Cells(6).Text & "&presentation=" & e.Item.Cells(7).Text & "&pagetitle=Actuals to Sample" & " ','KORR');")
        End If


        If (CInt(e.Item.Cells(4).Text) + CInt(e.Item.Cells(8).Text)) <> 0 Then

            Dim cell As TableCell
            For Each cell In e.Item.Cells
                cell.Attributes.Add("style", "border-top:solid 2px #FF0000; border-bottom:solid 2px #FF0000; color:#ff0000;")
            Next
            e.Item.Cells(2).Text = "<strong>WARNING!</strong><BR>" & e.Item.Cells(2).Text
            e.Item.Cells(1).Attributes.Add("style", "border-top:solid 2px #FF0000; border-bottom:solid 2px #FF0000;  border-left:solid 2px #FF0000; color:#ff0000;")
            e.Item.Cells(e.Item.Cells.Count - 1).Attributes.Add("style", "border-top:solid 2px #FF0000; border-bottom:solid 2px #FF0000;  border-right:solid 2px #FF0000; color:#ff0000;")
        End If
    End Sub
    Private Sub MyGrid_ItemCreated(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated
        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Then
            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor1Class';")
        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor2Class';")
        End If
    End Sub

    '**************************************************************************************************************
    '* For Printing
    '**************************************************************************************************************
    Sub btnPrint_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Print_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Autoprint Or PopupWindowStyle.NoToolbar)
        BindData()
    End Sub

    Sub btnPreview_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Preview_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Fullscreen)
        BindData()
    End Sub

    Private Function PrePrint() As printReportUtil
        BindData()

        Dim preview As New printReportUtil

        preview.PageTitle = "UM Cross Check"
        preview.PageSize = 47

        preview.AddReportHeader(repData)
        preview.AddWebGrid(Me.MyGrid)

        Return preview

    End Function
    '**************************************************************************************************************
    '* End Printing
    '**************************************************************************************************************
End Class
