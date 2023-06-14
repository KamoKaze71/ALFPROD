Imports Wyeth.Alf.WyethDropdown
Imports Wyeth.Utilities.DateHandling
Imports Wyeth.Utilities.Helper
Imports Wyeth.Alf.CssStyles
Imports Wyeth.Alf.WyethAppllication

Public Class StockProduct
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents GridPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents MYGRid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents repData As Wyeth.Alf.reportData
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button

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
    Dim MyApplication As New WyethAppllication
    Dim minTran, phznr, presentation, ed, sd As String
    Dim sum0, sum1, sum2, sum3, sum4, sum5, dist_id, prod_id, sumUM, previous_inventory_lvl As Integer

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here


        If Page.IsPostBack = True Then

            ed = Me.ViewState.Item("ed")
            sd = Me.ViewState.Item("sd")
            prod_id = Me.ViewState.Item("prod_id")
            dist_id = Me.ViewState.Item("dist_id")

            'resret sum values
            sum0 = 0
            sum1 = 0
            sum2 = 0
            sum3 = 0
            sum4 = 0
            sum5 = 0
            sumUM = 0
            previous_inventory_lvl = -1
        Else
            previous_inventory_lvl = -1


            sd = Request.QueryString("sd")
            ed = Request.QueryString("ed")
            prod_id = Request.QueryString("id")
            dist_id = Request.QueryString("dist_id")
            phznr = Request.QueryString("phznr")
            presentation = Request.QueryString("presentation")
            lblPageTitle.Text = Request.QueryString("pagetitle")


            Me.ViewState.Add("sd", sd.ToString)
            Me.ViewState.Add("ed", ed.ToString)
            Me.ViewState.Add("prod_id", prod_id)
            Me.ViewState.Add("dist_id", dist_id)

            BindData()


        End If
    End Sub

    Private Sub BindData()

      
        MyReport.StartDate = sd
        MyReport.EndDate = ed
        MyReport.ProductID = prod_id
        MyReport.DistID = dist_id

        minTran = MyApplication.GetValidTranDate(MyReport.StartDate).ToString(CType(Application("MyDTFI"), IFormatProvider))
        MYGRid.DataSource = MyReport.GetStockStatProduct()
        MYGRid.DataBind()
        fillReportData()
        SetGridStyles(MYGRid)

    End Sub

    Private Sub fillReportData()
        repData.addLine("Report-date from", FormatDate(MyReport.StartDate), True, True)
        repData.addLine("Report-date to", FormatDate(MyReport.EndDate), True, True)
        repData.addLine("Presentation", presentation, True, True)
        repData.addLine("Product No.", phznr, True, True)
    End Sub

    Private Sub MyGrid_ItemCreated(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MYGRid.ItemCreated
        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Then
            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor1Class';")
        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor2Class';")
        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Footer Then
            '   e.Item.Cells(2).Text = sum0
            e.Item.Cells(3).Text = sum1
            e.Item.Cells(4).Text = sum2
            e.Item.Cells(5).Text = sum3
            e.Item.Cells(6).Text = sum4
            e.Item.Cells(7).Text = sumUM



        End If
    End Sub

    Private Sub item_Databound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MYGRid.ItemDataBound
        Dim korr, out, out_fg, we, startbalance, endbalance, UM As Integer

        ' immer den EndBestand vom Inventory Balance vom Vortag = Anfangsbestand vom darauffolgenden Tag
        If e.Item.Cells(0).Text = minTran Then
            e.Item.Visible = False
            e.Item.Cells(2).Text = e.Item.Cells(8).Text
            previous_inventory_lvl = e.Item.Cells(8).Text
        Else
            e.Item.Cells(2).Text = e.Item.Cells(8).Text
        End If

        If previous_inventory_lvl <> -1 Then
            e.Item.Cells(2).Text = previous_inventory_lvl
        End If

        If e.Item.Visible = True Then  '1st item must not be added to the sum - since its only for the correct startbalance of the previous day
            startbalance = e.Item.Cells(2).Text
            sum0 = sum0 + startbalance

            we = e.Item.Cells(3).Text()
            sum1 = sum1 + we

            out = e.Item.Cells(4).Text()
            sum2 = sum2 + out

            out_fg = e.Item.Cells(5).Text()
            sum3 = sum3 + out_fg

            korr = e.Item.Cells(6).Text()
            sum4 = sum4 + korr

            UM = e.Item.Cells(7).Text()
            sumUM = sumUM + UM

            endbalance = e.Item.Cells(8).Text


            previous_inventory_lvl = e.Item.Cells(8).Text

            e.Item.Cells(0).Attributes.Add("style", "cursor:default;")
            e.Item.Cells(1).Attributes.Add("style", "cursor:default;")
            e.Item.Cells(2).Attributes.Add("style", "cursor:default;")
            e.Item.Cells(7).Attributes.Add("style", "cursor:default;")

            setTDMouseover(e.Item.Cells(3))
            setTDMouseover(e.Item.Cells(4))
            setTDMouseover(e.Item.Cells(5))
            setTDMouseover(e.Item.Cells(6))

            e.Item.Cells(3).Attributes.Add("onclick", "javascript:OpenPopUp('IN.aspx?id=" & (e.Item.Cells(1).Text) & "&sd=" & (e.Item.Cells(0).Text) & "&ed=" & (e.Item.Cells(0).Text) & "&dist_id=" & dist_id & "&phznr=" & phznr & "&presentation=" & presentation & "&pagetitle=Stock Wareneingang" & "  ','WE');")
            e.Item.Cells(4).Attributes.Add("onclick", "javascript:OpenPopUp('OUT.aspx?id=" & (e.Item.Cells(1).Text) & "&sd=" & (e.Item.Cells(0).Text) & "&ed=" & (e.Item.Cells(0).Text) & "&dist_id=" & dist_id & "&phznr=" & phznr & "&presentation=" & presentation & "&pagetitle=Stock Wareneingang" & " ','Sales');")
            e.Item.Cells(5).Attributes.Add("onclick", "javascript:OpenPopUp('OUT_FG.aspx?id=" & (e.Item.Cells(1).Text) & "&sd=" & (e.Item.Cells(0).Text) & "&ed=" & (e.Item.Cells(0).Text) & "&dist_id=" & dist_id & "&phznr=" & phznr & "&presentation=" & presentation & "&pagetitle=Stock Wareneingang" & " ','FreeGoods');")
            e.Item.Cells(6).Attributes.Add("onclick", "javascript:OpenPopUp('KORR.aspx?id=" & (e.Item.Cells(1).Text) & "&sd=" & (e.Item.Cells(0).Text) & "&ed=" & (e.Item.Cells(0).Text) & "&dist_id=" & dist_id & "&phznr=" & phznr & "&presentation=" & presentation & "&pagetitle=Stock Wareneingang" & " ','Corrections');")

            If (startbalance + we - out - out_fg + korr + UM) <> endbalance Then

                Dim cell As TableCell
                For Each cell In e.Item.Cells
                    cell.Attributes.Add("style", "border-top:solid 2px #FF0000; border-bottom:solid 2px #FF0000; color:#ff0000;")
                Next

                e.Item.Cells(0).Text = "<strong>WARNING!</strong><BR>" & e.Item.Cells(0).Text
                e.Item.Cells(0).Attributes.Add("style", "border-top:solid 2px #FF0000; border-bottom:solid 2px #FF0000;  border-left:solid 2px #FF0000; color:#ff0000;")
                e.Item.Cells(e.Item.Cells.Count - 1).Attributes.Add("style", "border-top:solid 2px #FF0000; border-bottom:solid 2px #FF0000;  border-right:solid 2px #FF0000; color:#ff0000;")

                'e.Item.Cells(8).Text = "Sanova:" & e.Item.Cells(8).Text & "<br>" & "Wyeth:" & startbalance + we - out - out_fg + korr
            End If
        End If

    End Sub

    Private Sub ddDistribSelect_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        BindData()
    End Sub

    Private Sub ddlineselect_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        BindData()
    End Sub

    Private Sub txtStartDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Page.Validate()
        If Page.IsValid Then
            BindData()
        End If

    End Sub

    Private Sub btn_refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Page.Validate()
        If Page.IsValid Then
            BindData()
        End If

    End Sub

    Private Sub ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportExcel.Click
        BindData()

        Dim exp As exportToExcel = New exportToExcel(Page)
        exp.title = lblPageTitle.Text
        exp.showReportData = repData
        exp.formatColumnAsString(1)
        exp.addDataGrid(MYGRid)
        exp.export()
    End Sub
End Class
