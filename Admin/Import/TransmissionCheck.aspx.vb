Imports Wyeth.Alf.CssStyles
Imports Wyeth.Alf.WyethDropdown

Public Class TransmissionCheck
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddDistributor As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddyear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents myGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents GenButton As System.Web.UI.WebControls.Button
    Protected WithEvents MyPanel As System.Web.UI.WebControls.Panel

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.

        InitializeComponent()

        ddDistributor = New DropDownList
        ddyear = New DropDownList
        MyPanel = New System.Web.UI.WebControls.Panel

        MyPanel.Width = New System.Web.UI.WebControls.Unit("100%")
        MyPanel.Height = New System.Web.UI.WebControls.Unit(20)

        'AddHandler ddyear.SelectedIndexChanged, AddressOf ddyear_SelIndexChange


        myGrid = New C1.Web.C1WebGrid.C1WebGrid
        myGrid.AutoGenerateColumns = False
        myGrid.ID = "myGrid"
        AddHandler myGrid.ItemDataBound, AddressOf Item_DataBound

        Dim thisSysTranDate As New C1.Web.C1WebGrid.C1BoundColumn
        thisSysTranDate.DataField = "tran_date"
        thisSysTranDate.HeaderText = "Date"

        Dim thisSysTranID As New C1.Web.C1WebGrid.C1BoundColumn
        thisSysTranID.DataField = "tran_id"
        thisSysTranID.HeaderText = "Tran ID"

        Dim thisSysTranNumber As New C1.Web.C1WebGrid.C1BoundColumn
        thisSysTranNumber.DataField = "tran_number"
        thisSysTranNumber.HeaderText = "Tran No."

        Dim thisSysTranCheck As New C1.Web.C1WebGrid.C1BoundColumn
        thisSysTranCheck.DataField = "tran_check"
        thisSysTranCheck.HeaderText = "Tran Check"

        Dim emptyCol As New C1.Web.C1WebGrid.C1Column
        emptyCol.HeaderStyle.CssClass = "headlineSeperatorBoth"
        emptyCol.ItemStyle.BackColor = Color.LightGreen


        Dim OtherSysTranDate As New C1.Web.C1WebGrid.C1BoundColumn
        OtherSysTranDate.DataField = "tran_date_1"
        OtherSysTranDate.HeaderText = "Date"

        Dim OtherSysTranID As New C1.Web.C1WebGrid.C1BoundColumn
        OtherSysTranID.DataField = "tran_id_1"
        OtherSysTranID.HeaderText = "Tran ID"

        Dim OtherSysTranNumber As New C1.Web.C1WebGrid.C1BoundColumn
        OtherSysTranNumber.DataField = "tran_number_1"
        OtherSysTranNumber.HeaderText = "Tran No."

        Dim OtherSysTranCheck As New C1.Web.C1WebGrid.C1BoundColumn
        OtherSysTranCheck.DataField = "tran_check_1"
        OtherSysTranCheck.HeaderText = "Tran Check"


        If Wyeth.Utilities.Settings.isLiveServer = True Then
            myGrid.Columns.Add(thisSysTranDate)
            myGrid.Columns.Add(thisSysTranID)
            myGrid.Columns.Add(thisSysTranNumber)
            myGrid.Columns.Add(thisSysTranCheck)

            myGrid.Columns.Add(emptyCol)

            myGrid.Columns.Add(OtherSysTranDate)
            myGrid.Columns.Add(OtherSysTranID)
            myGrid.Columns.Add(OtherSysTranNumber)
            myGrid.Columns.Add(OtherSysTranCheck)

        Else

            myGrid.Columns.Add(OtherSysTranDate)
            myGrid.Columns.Add(OtherSysTranID)
            myGrid.Columns.Add(OtherSysTranNumber)
            myGrid.Columns.Add(OtherSysTranCheck)

            myGrid.Columns.Add(emptyCol)

            myGrid.Columns.Add(thisSysTranDate)
            myGrid.Columns.Add(thisSysTranID)
            myGrid.Columns.Add(thisSysTranNumber)
            myGrid.Columns.Add(thisSysTranCheck)


        End If

        Dim lbl As New Label
        lbl.Text = "Distributor:"
        lbl.BorderWidth = New System.Web.UI.WebControls.Unit(5)
        lbl.BorderColor = Color.White
        MyPanel.Controls.Add(lbl)
        MyPanel.Controls.Add(ddDistributor)

        Dim lbl2 As New Label
        lbl2.Text = "Year:"
        lbl2.BorderColor = Color.White
        lbl2.BorderWidth = New System.Web.UI.WebControls.Unit(5)
        MyPanel.Controls.Add(lbl2)
        MyPanel.Controls.Add(ddyear)

        Dim spacer As New System.Web.UI.WebControls.Label
        spacer.Width = New System.Web.UI.WebControls.Unit(10)
        MyPanel.Controls.Add(spacer)

        Dim GenButton As New System.Web.UI.WebControls.Button
        GenButton.CssClass = "button"
        GenButton.Text = "Generate Report"
        GenButton.BorderWidth = New System.Web.UI.WebControls.Unit
        MyPanel.Controls.Add(GenButton)
        AddHandler GenButton.Click, AddressOf GenButton_Click


        Me.FindControl("Form1").Controls.Add(MyPanel)
        Me.FindControl("Form1").Controls.Add(myGrid)


    End Sub

#End Region

    Dim MyAlfStatus As New Alf.AlfStatus

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        If Page.IsPostBack = True Then

        Else
            GetYearDD4(ddyear, 2002, 0)
            BindData()
        End If

    End Sub



    Private Sub BindData()



        Wyeth.Alf.WyethDropdown.GetDistribSelectDD(ddDistributor, Session("country_id"))

        myGrid.DataSource = MyAlfStatus.GetTransmissions(ddDistributor.SelectedValue, ddyear.SelectedValue)
        myGrid.DataBind()

        SetGridStyles(myGrid)


    End Sub

    Private Sub Item_DataBound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles myGrid.ItemDataBound
        For Each cell As TableCell In e.Item.Cells
            If cell.Text.StartsWith("Missing!") Then
                cell.ForeColor = Color.Red
            End If
        Next

    End Sub

    Private Sub GenButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GenButton.Click
        BindData()
    End Sub

    'Private Sub ddyear_SelIndexChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddyear.SelectedIndexChanged
    '    BindData()
    'End Sub

End Class
