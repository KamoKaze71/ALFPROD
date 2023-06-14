Imports Wyeth.Alf.CssStyles
Imports Wyeth.Utilities.NumberFormat

Public Class SamplesAssignment
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents Button2 As System.Web.UI.HtmlControls.HtmlButton
    Protected WithEvents ddobsCode As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Button3 As System.Web.UI.HtmlControls.HtmlButton
    Protected WithEvents btn_generate As System.Web.UI.WebControls.Button
    Protected WithEvents repData As reportData
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents prtControl As printReportCtl

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim MyReport As New Report
    Dim MyPopUp As New JSPopUp(Me)

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Page.IsPostBack = True Then
            SetGridStyles(MyGrid)
        Else
           
            fillGroupByDropdown()
            lblPageTitle.Text = Request.QueryString("pagetitle")
            SetGridStyles(MyGrid)
            BindData()

        End If
        MyPopUp.Width = 900
        MyPopUp.Height = 550
    End Sub

    Private Sub BindData()

        MyReport.OBSCode = ddobsCode.SelectedValue
        MyReport.CtryID = Session("country_id")
        MyGrid.DataSource = MyReport.GetActAndSamProducts()
        MyGrid.DataBind()
    End Sub



    Private Sub fillGroupByDropdown()
        Dim it As ListItem = New ListItem
        it.Value = 0
        it.Text = "Active Products"
        ddobsCode.Items.Add(it)

        Dim it2 As ListItem = New ListItem
        it2.Value = 1
        it2.Text = "Obsolete Products"
        ddobsCode.Items.Add(it2)
        ddobsCode.DataBind()
    End Sub
    Private Sub MyGrid_ItemDAtabound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound
        Dim act_cogs As Double
        Dim sam_cogs As Double

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Then
            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor1Class';")
        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor2Class';")

        End If

        If e.Item.Cells(0).Text <> "&nbsp;" Then
            MyPopUp.Width = 900
            MyPopUp.Height = 550
            MyPopUp.PageURL = "../admin/ProductByID.aspx?prod_id=" & e.Item.Cells(0).Text
            MyPopUp.AddPopupToControl(e.Item.Cells(1))
            setTDMouseover(e.Item.Cells(1))
        End If



        'setTDMouseover(e.Item.Cells(2))
        'setTDMouseover(e.Item.Cells(3))
        'setTDMouseover(e.Item.Cells(4))

        'setTDMouseover(e.Item.Cells(6))
        'setTDMouseover(e.Item.Cells(7))
        'setTDMouseover(e.Item.Cells(8))
        'setTDMouseover(e.Item.Cells(9))


        If e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("SAM_STDCOGS"))).Text() <> "&nbsp;" And e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("ACT_STDCOGS"))).Text() <> "&nbsp;" Then
            act_cogs = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("ACT_STDCOGS"))).Text()
            sam_cogs = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("SAM_STDCOGS"))).Text()
            If act_cogs <> sam_cogs Then
                For Each cell As TableCell In e.Item.Cells
                    cell.Attributes.Add("style", "border-top:solid 2px #FF0000; border-bottom:solid 2px #FF0000; color:#ff0000;")
                Next
            End If
        End If
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("SAM_STDCOGS"))), 2)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("ACT_STDCOGS"))), 2)

    End Sub

    Private Sub btn_generate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_generate.Click
        BindData()
    End Sub

    Private Sub Button3_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.ServerClick
        BindData()

        Dim reportHeaderString As String = lblPageTitle.Text

        Dim exp As exportToExcel = New exportToExcel(Page)
        exp.title = lblPageTitle.Text
        exp.showReportData = repData
        exp.addLine(String.Format(reportHeaderString, "EUR"))
        exp.formatColumnAsString(1)
        exp.formatColumnAsString(7)
        exp.addDataGrid(MyGrid)
        exp.export()
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

        Dim reportHeaderString As String = "<tr align=center><td align=center class=head style='border: 0px;' colspan=4><b>Actuals</b></td><td style='border:0px;'>&nbsp;</td><td class=head colspan=4 style='border: 0px;'><b>Samples</b></td></tr>"
        Dim preview As New printReportUtil

        preview.PageTitle = "Sample Product Check"
        preview.PageSizeLandscape = 28
        preview.DefaultOrientation = Orientation.Landscape

        preview.AddReportHeader(repData)
        preview.AddWebGrid(reportHeaderString, Me.MyGrid)

        Return preview

    End Function
    '**************************************************************************************************************
    '* End Printing
    '**************************************************************************************************************
End Class

