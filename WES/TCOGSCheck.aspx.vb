Imports Wyeth.Alf.CssStyles

Public Class TCOGSCheck
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents Form1 As System.Web.UI.HtmlControls.HtmlForm
    Protected WithEvents reportpanel As System.Web.UI.WebControls.Panel
    Protected WithEvents BUTTON1 As System.Web.UI.HtmlControls.HtmlButton
    Protected WithEvents repData As Wyeth.Alf.reportData
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents prtControl As printReportCtl
    Protected WithEvents lblTcogs As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim MyTcogs As New TCogs
    Dim MyPopUp As New JSPopUp(Me.page)

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Put user code to initialize the page here

        SetGridStyles(MyGrid)

        If Not Page.IsPostBack Then

            MyPopUp.Height = 550
            MyPopUp.Width = 900
            BindData()
        End If

    End Sub

    Private Sub BindData()

        MyGrid.DataSource = MyTcogs.GetProductsWithNoTCOGS(Session("country_id"))
        MyGrid.DataBind()


        If MyGrid.Items.Count = 0 Then
            MyGrid.Visible = False
            lblTcogs.Visible = True
            lblTcogs.Text = "All products that had a movement in the last 12 months have also TCogs values"
            lblTcogs.CssClass = "success"
        Else
            MyGrid.Visible = True
            lblTcogs.Visible = True
            lblTcogs.Text = "These products had a movement in the last 12 months, but do no have TCogs values"
            lblTcogs.CssClass = "nosuccess"
        End If


    End Sub

    Private Sub BUTTON1_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUTTON1.ServerClick
        BindData()
        Dim exp As exportToExcel = New exportToExcel(Page)
        exp.showReportData = repData
        exp.title = Me.ALFPageTitle
        exp.formatColumnAsString(0)
        MyGrid.Visible = True

        exp.addDataGrid(MyGrid)
        exp.export()
        MyGrid.Visible = False
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

    Private Sub MyGrid_ItemDataBound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound

        MyPopUp.PageURL = "../admin/ProductByID.aspx?prod_id=" & e.Item.Cells(0).Text
        MyPopUp.AddPopupToControl(e.Item)
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

        preview.PageTitle = "Products with no TCOGS"
        preview.PageSize = 47

        preview.AddReportHeader(repData)
        preview.AddWebGrid(Me.MyGrid)

        Return preview

    End Function
    '**************************************************************************************************************
    '* End Printing
    '**************************************************************************************************************
End Class
