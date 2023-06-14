Imports Wyeth.Alf.WyethDropdown
Imports Wyeth.Utilities.DateHandling
Imports Wyeth.Alf.CssStyles
Imports Wyeth.Utilities.NumberFormat
Imports Wyeth.Utilities.Helper



Public Class AMS_SalesStatDetail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents txtStartDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents txtEndDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents ddProductSelect As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddLine As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents Lblsuccess As System.Web.UI.WebControls.Label
    Protected WithEvents lbDownloadArea As System.Web.UI.WebControls.Label
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents ReportPanel As System.Web.UI.WebControls.Panel

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim myAMS As New AMS
    Dim sumUnits, sumFGUnits As Integer
    Dim sumValue, sumFGVAlue, SumTotalCogs As Double
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Page.IsPostBack Then
        Else

            Me.lblPageTitle.Text = Request.QueryString("PageTitle")
            filllineDD()
            fillAMSProductDD()


            txtStartDate.Text = "1995-12-01"
            txtEndDate.Text = "1995-12-31"
            SetGridStylesGroup(MyGrid)
          
        End If

    End Sub

    Private Sub fillamsProductDD()

        ddProductSelect.DataSource = myAMS.GetAMSProdKurzBez(ddLine.SelectedValue)
        ddProductSelect.DataTextField = "KURZBEZ"
        ddProductSelect.DataValueField = "KURZBEZ"
        ddProductSelect.DataBind()
        Dim li As New ListItem
        li.Value = 0
        li.Text = "-- All Products --"
        ddProductSelect.Items.Insert(0, li)
    End Sub

    Private Sub filllineDD()
        Dim li As New ListItem
        li.Value = "WYL"
        li.Text = "Actuals"
        ddLine.Items.Insert(0, li)
        Dim li2 As New ListItem
        li2.Value = "MWL"
        li2.Text = "Samples"
        ddLine.Items.Insert(1, li2)
    End Sub
 


    Private Sub bindData()

        Dim MyDataView As New DataView


        SetGridStylesGroup(MyGrid)

        MyDataView = myAMS.GetAMSOrderDetails(txtStartDate.Text, txtEndDate.Text, ddLine.SelectedValue, ddProductSelect.SelectedValue)


        With MyGrid
            .DataSource = MyDataView
            .GridLines = GridLines.None
            .ShowFooter = True
            .AllowSorting = False
            .AllowAutoSort = True
            .AlternatingItemStyle.CssClass = "tableBGColor2Class"
            .ItemStyle.CssClass = "tableBGColor2Class"
            .DataBind()
        End With


    End Sub


    Private Sub Item_created(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated
        setColumnToolTips(e.Item, MyGrid)

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Or e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor2Class';")
        End If

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Footer Then
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Units"))).Text = MyNumberFormat(sumUnits, 0)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("FGUnits"))).Text = MyNumberFormat(sumFGUnits, 0)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Value"))).Text = MyNumberFormat(sumValue, 2)
            e.Item.CssClass = "reportTotal"
            e.Item.Cells(0).Text = "TOTAL AUSTRIA"
            e.Item.Cells(0).HorizontalAlign = HorizontalAlign.Left
        End If

    End Sub

    Private Sub btnGenRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportExcel.Click

        Page.Validate()

        If Page.IsValid Then
            bindData()
        End If

    End Sub

    Private Sub Item_DataBound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound
        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Or e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Then

            sumUnits = sumUnits + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Units"))).Text
            sumFGUnits = sumFGUnits + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("FGUnits"))).Text
            sumValue = sumValue + e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("Value"))).Text


            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("VALUE"))), 2)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("UNITS"))), 0)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("FGUNITS"))), 0)
        End If
    End Sub
    Private Sub ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportExcel.Click
        Page.Validate()

        If Page.IsValid Then
            bindData()
            MyGrid.Visible = True

            Dim exp As New exportToExcel(Page)

            exp.title = lblPageTitle.Text
            exp.addLine("first historic order entry:</STRONG>&nbsp; 1995-12-01 <br>")
            exp.addLine("last historic order entry:</STRONG> &nbsp; 2002-12-31 <br><br><br>")
            exp.addLine("Report Date from: " & txtStartDate.Text & "<bR>Report End to: " & txtEndDate.Text)

            exp.formatColumnAsString(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("phznr")))
            exp.formatColumnAsString(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("kdnr")))
            exp.formatColumnAsString(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("KonzNr")))

            exp.addDataGrid(MyGrid)
            exp.export()
            MyGrid.Visible = False
        End If

    End Sub

    Private Sub ddLine_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddLine.SelectedIndexChanged
        ddProductSelect.DataSource = myAMS.GetAMSProdKurzBez(ddLine.SelectedValue)
        ddProductSelect.DataTextField = "KURZBEZ"
        ddProductSelect.DataValueField = "KURZBEZ"
        ddProductSelect.DataBind()
        Dim li As New ListItem
        li.Value = 0
        li.Text = "-- All Products --"
        ddProductSelect.Items.Insert(0, li)
    End Sub

    Private Sub fillgroupdd()
        Dim li As New ListItem
        li.Value = "WYL"
        li.Text = "ACT"
        ddLine.Items.Insert(0, li)
        Dim li2 As New ListItem
        li2.Value = "MWL"
        li2.Text = "SAM"
        ddLine.Items.Insert(1, li2)
    End Sub
End Class


