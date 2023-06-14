Imports Wyeth.Alf.WyethDropdown
Imports Wyeth.Alf.CssStyles
Imports C1.Web.C1WebGrid
Imports Wyeth.Utilities.NumberFormat
Imports Wyeth.Utilities.Helper


Public Class TargetStatistics
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents ddTPG As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnGenRep As System.Web.UI.WebControls.Button
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents ddGroupby As System.Web.UI.WebControls.DropDownList
    Protected WithEvents rbl_ROUND As System.Web.UI.HtmlControls.HtmlInputRadioButton
    Protected WithEvents rbl_EXACT As System.Web.UI.HtmlControls.HtmlInputRadioButton
    Protected WithEvents prtControl As Wyeth.Alf.printReportCtl
    Protected WithEvents btnLevel1 As System.Web.UI.WebControls.Button
    Protected WithEvents btnLevel2 As System.Web.UI.WebControls.Button
    Protected WithEvents repData As reportData
    Protected WithEvents lblTargetType As System.Web.UI.WebControls.Label
    Protected WithEvents lblCurrency As System.Web.UI.WebControls.Label
    Protected WithEvents tblRound As System.Web.UI.HtmlControls.HtmlTable

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim Mytrageting As New Targeting
    Dim decdigit, x, sare_id As Integer
    Dim tpg, tmpSareNum As String
    Dim sum_q1_sales_value, sum_q1_target_value, sum_q2_sales_value, sum_q2_target_value, sum_q3_sales_value, sum_q3_target_value, sum_q4_sales_value, sum_q4_target_value, sum_sales_total_year, sum_targ_total_year As Double
    Dim HeaderRow() As String = {"", "", "Q1", "Q1", "Q1", "Q2", "Q2", "Q2", "Q3", "Q3", "Q3", "Q4", "Q4", "Q4", "Year", "Year", "Year", "", "", ""}

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.EnableViewState = True
        checkSalesRepStatus()
        If Page.IsPostBack = True Then

        Else
            GetYearDD4(ddYear, 2004)
            GetTPGDD(ddTPG, Session("country_id"))

            If sare_id <> 0 Then
                bindData()
            ElseIf sare_id = 0 Then
                MyGrid.Visible = False
            End If


            End If
    End Sub
    Private Sub checkSalesRepStatus()
        tpg = Mytrageting.GetTPForSalesRep(Session("user_id"), Session("country_id"))

        If tpg Is Nothing Then ' es is kein sales rep

            sare_id = 0 ' es werden alle sales reps angezeigt

            If Me.ALFPageAccessRights > AlfPage.Rights.Read Then
                ddTPG.Enabled = True

            Else
                ddTPG.Enabled = False
                sare_id = Mytrageting.GetSareID(Session("user_id"))
            End If

        Else ' wenn es sich um einen Sales rep Handelt
            sare_id = Mytrageting.GetSareID(Session("user_id")) 'es werden nur daten für diesen sales rep angezeigt

            If Me.ALFPageAccessRights > AlfPage.Rights.Read Then
                ddTPG.Enabled = True
                sare_id = 0
            Else
                ddTPG.SelectedValue = tpg
                ddTPG.Enabled = False

            End If

        End If

    End Sub

    Private Sub settargetType()

        Dim MyTargetType As String

        MyTargetType = Mytrageting.GetTargetType(CInt(ddTPG.SelectedValue))
        If MyTargetType = "UNIT" Then
            x = 0
            decdigit = 0

            MyGrid.Columns.ColumnByName(("sales_q1_value")).HeaderText = "Sales Units"
            MyGrid.Columns.ColumnByName(("targ_q1_value")).HeaderText = "Target Units"

            MyGrid.Columns.ColumnByName(("sales_q2_value")).HeaderText = "Sales Units"
            MyGrid.Columns.ColumnByName(("targ_q2_value")).HeaderText = "Target Units"

            MyGrid.Columns.ColumnByName(("sales_q3_value")).HeaderText = "Sales Units"
            MyGrid.Columns.ColumnByName(("targ_q3_value")).HeaderText = "Target Units"

            MyGrid.Columns.ColumnByName(("sales_q4_value")).HeaderText = "Sales Units"
            MyGrid.Columns.ColumnByName(("targ_q4_value")).HeaderText = "Target Units"

            rbl_ROUND.Checked = False
            rbl_EXACT.Checked = True

            tblRound.Visible = False

        ElseIf MyTargetType = "VALUE" Then
            x = 2

            MyGrid.Columns.ColumnByName(("sales_q1_value")).HeaderText = "Sales Value"
            MyGrid.Columns.ColumnByName(("targ_q1_value")).HeaderText = "Target Value"

            MyGrid.Columns.ColumnByName(("sales_q2_value")).HeaderText = "Sales Value"
            MyGrid.Columns.ColumnByName(("targ_q2_value")).HeaderText = "Target Value"

            MyGrid.Columns.ColumnByName(("sales_q3_value")).HeaderText = "Sales Value"
            MyGrid.Columns.ColumnByName(("targ_q3_value")).HeaderText = "Target Value"

            MyGrid.Columns.ColumnByName(("sales_q4_value")).HeaderText = "Sales Value"
            MyGrid.Columns.ColumnByName(("targ_q4_value")).HeaderText = "Target Value"

            ' rbl_ROUND.Checked = True
            ' rbl_EXACT.Checked = False


            tblRound.Visible = True

        End If

    End Sub
    Private Sub Page_PreRender(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
        If x = 2 Then
            lblCurrency.Visible = True
            lblTargetType.Visible = True
            lblTargetType.Text = "Targets in: <font color=red>Values<red>"
            lblTargetType.CssClass = "currency"
            lblCurrency.CssClass = "currency"
            lblCurrency.Text = "Currency: <font color=red>" & CStr(Session("currency_code")) & "</font>"
        ElseIf x = 0 Then
            lblCurrency.Visible = False
            lblTargetType.Visible = True
            lblTargetType.Text = "Targets in: <font color=red> Units</font>"
            lblTargetType.CssClass = "currency"
        End If
    End Sub

    Private Sub bindData()
        settargetType()

        If rbl_ROUND.Checked Then
            decdigit = 0
        ElseIf rbl_EXACT.Checked And x = 2 Then
            decdigit = 2
        ElseIf rbl_EXACT.Checked And x = 0 Then ' Its a Units TPG!
            decdigit = 0
        End If

        SetGridStylesGroup(MyGrid)
        ' groupColumns()
        If x = 2 Then
            MyGrid.DataSource = Mytrageting.GetTaregtsForReport(ddTPG.SelectedValue, ddYear.SelectedValue, sare_id)
        ElseIf x = 0 Then
            MyGrid.DataSource = Mytrageting.GetTaregtsForReportUnits(ddTPG.SelectedValue, ddYear.SelectedValue)
        End If
        MyGrid.DataBind()
        MyGrid.Visible = True

    End Sub

    'Private Sub groupColumns()

    '    With MyGrid

    '        Dim SalesValueColumn As New C1BoundColumn
    '        SalesValueColumn = .Columns.ColumnByName("percentage_value")
    '        SalesValueColumn.HeaderText = "Sales Value"
    '        SalesValueColumn.Aggregate = AggregateEnum.Sum
    '        .Columns.RemoveAt(.Columns.IndexOf(SalesValueColumn))

    '        Dim TargetValueColumn As New C1BoundColumn
    '        TargetValueColumn = .Columns.ColumnByName("target_value")
    '        TargetValueColumn.HeaderText = "Traget Value"
    '        TargetValueColumn.Aggregate = AggregateEnum.Sum
    '        .Columns.RemoveAt(.Columns.IndexOf(TargetValueColumn))

    '        Dim qColumn As New C1BoundColumn
    '        qColumn = .Columns.ColumnByName("qType")
    '        qColumn.GroupInfo.HeaderText = "{0}"
    '        qColumn.HeaderText = "Quarter"
    '        .Columns.RemoveAt(.Columns.IndexOf(qColumn))

    '        Dim sareColumn As New C1BoundColumn
    '        sareColumn = .Columns.ColumnByName("sare_name")
    '        sareColumn.HeaderText = "Sales Rep"


    '        sareColumn.GroupInfo.HeaderText = "{0}"
    '        .Columns.RemoveAt(.Columns.IndexOf(sareColumn))

    '        Dim CCColumn As New C1BoundColumn
    '        CCColumn = .Columns.ColumnByName("prod_cc_description")
    '        CCColumn.GroupInfo.HeaderText = "{0}"
    '        CCColumn.HeaderText = "Cost Center"
    '        .Columns.RemoveAt(.Columns.IndexOf(CCColumn))


    '        .Columns.Insert(0, sareColumn)
    '        .Columns.Insert(1, CCColumn)
    '        .Columns.Insert(2, qColumn)
    '        .Columns.Insert(3, TargetValueColumn)
    '        .Columns.Insert(4, SalesValueColumn)

    '        .Columns(0).Visible = False
    '        .Columns(1).Visible = False
    '        .Columns(2).Visible = True
    '        .Columns(3).Visible = True
    '        .Columns(4).Visible = True

    '        .Columns(0).GroupInfo.Position = GroupPositionEnum.Header
    '        .Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed


    '        .Columns(1).GroupInfo.Position = GroupPositionEnum.Header
    '        .Columns(1).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed

    '        .Columns(2).GroupInfo.Position = GroupPositionEnum.None
    '        .Columns(2).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed

    '        .Columns(3).GroupInfo.Position = GroupPositionEnum.None
    '        .Columns(3).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed

    '        .Columns(4).GroupInfo.Position = GroupPositionEnum.None
    '        .Columns(4).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed

    '        sareColumn.GroupInfo.HeaderStyle.Wrap = False

    '    End With

    'End Sub

    Private Sub btnGenRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRep.Click
        bindData()
    End Sub

    Private Sub MyGrid_ItemDatabound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound

        sum_q1_sales_value += e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_q1_value"))).Text
        sum_q1_target_value += e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q1_value"))).Text

        sum_q2_sales_value += e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_q2_value"))).Text
        sum_q2_target_value += e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q2_value"))).Text

        sum_q3_sales_value += e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_q3_value"))).Text
        sum_q3_target_value += e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q3_value"))).Text

        sum_q4_sales_value += e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_q4_value"))).Text
        sum_q4_target_value += e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q4_value"))).Text

        sum_sales_total_year += e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_total_year"))).Text
        sum_targ_total_year += e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_total_year"))).Text

        If rbl_ROUND.Checked = True Then

            divideMe(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_q1_value"))))
            divideMe(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q1_value"))))

            divideMe(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_q2_value"))))
            divideMe(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q2_value"))))

            divideMe(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_q3_value"))))
            divideMe(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q3_value"))))

            divideMe(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_q4_value"))))
            divideMe(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q4_value"))))

            divideMe(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_total_year"))))
            divideMe(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_total_year"))))

        End If

        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q1_value"))), decdigit)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_q1_value"))), decdigit)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("per_q1_value"))), 1, "%")


        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q2_value"))), decdigit)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_q2_value"))), decdigit)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("per_q2_value"))), 1, "%")


        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q3_value"))), decdigit)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_q3_value"))), decdigit)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("per_q3_value"))), 1, "%")


        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q4_value"))), decdigit)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_q4_value"))), decdigit)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("per_q4_value"))), 1, "%")



        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_total_year"))), decdigit)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_total_year"))), decdigit)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("per_year_value"))), 1, "%")
        tmpSareNum = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sare_name"))).Text
        tmpSareNum = tmpSareNum.Substring(tmpSareNum.IndexOf("|") + 1)

        If tmpSareNum = CStr(Session("user_id")) Then
            e.Item.Visible = True
        ElseIf Me.ALFPageAccessRights > AlfPage.Rights.Read Then
            e.Item.Visible = True
        Else
            e.Item.Visible = False
        End If

    End Sub

    'Private Sub MyGrid_ItemAggregate(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1GroupTextEventArgs) Handles MyGrid.GroupAggregate
    '    'Dim startGriditem As C1GridItem
    '    'Dim sare_name As String
    '    'startGriditem = MyGrid.Items(e.StartItemIndex)
    '    'sare_name = startGriditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sare_name"))).Text
    '    'tmpSareNum = sare_name.Substring(sare_name.IndexOf("|") + 1)
    '    'e.Col.GroupInfo.HeaderText = sare_name.Substring(0, sare_name.IndexOf("|"))

    'End Sub

    Private Sub MyGrid_ItemGrouptext(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1GroupTextEventArgs) Handles MyGrid.GroupText
        Dim startGriditem As C1GridItem
        Dim sare_name As String
        startGriditem = MyGrid.Items(e.StartItemIndex)
        sare_name = startGriditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sare_name"))).Text
        tmpSareNum = sare_name.Substring(sare_name.IndexOf("|") + 1)
        'sare_name = startGriditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sare_name"))).Text
        'e.Col.GroupInfo.HeaderText = sare_name.Substring(0, sare_name.IndexOf("|"))
        e.Text = sare_name.Substring(0, sare_name.IndexOf("|"))
    End Sub

    Private Sub MyGrid_ItemCreated(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated
        Dim grouplvl As Integer = 1
        '  setColumnToolTips(e.Item, MyGrid, 2, HeaderRow)

        If e.Item.ItemType = C1ListItemType.GroupHeader Then

            '   MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sare_number")) - 1), 0)

            'If CInt(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sare_number")) - 1).Text) = Session("user_id") Then
            If tmpSareNum = Session("user_id") Then
                e.Item.Visible = True
            ElseIf Me.ALFPageAccessRights > AlfPage.Rights.Read Then
                e.Item.Visible = True
            Else
                e.Item.Visible = False
            End If

            If rbl_ROUND.Checked = True Then
                divideMe(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q1_value")) - grouplvl))
                divideMe(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q2_value")) - grouplvl))
                divideMe(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q3_value")) - grouplvl))
                divideMe(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q4_value")) - grouplvl))
                divideMe(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_total_year")) - grouplvl))

                divideMe(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_q1_value")) - grouplvl))
                divideMe(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_q2_value")) - grouplvl))
                divideMe(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_q3_value")) - grouplvl))
                divideMe(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_q4_value")) - grouplvl))
                divideMe(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_total_year")) - grouplvl))
            End If

            divideMe(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("per_q1_value")) - grouplvl), e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_q1_value")) - grouplvl), e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q1_value")) - grouplvl))
            divideMe(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("per_q2_value")) - grouplvl), e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_q2_value")) - grouplvl), e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q2_value")) - grouplvl))
            divideMe(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("per_q3_value")) - grouplvl), e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_q3_value")) - grouplvl), e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q3_value")) - grouplvl))
            divideMe(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("per_q4_value")) - grouplvl), e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_q4_value")) - grouplvl), e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q4_value")) - grouplvl))
            divideMe(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("per_year_value")) - grouplvl), e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_total_year")) - grouplvl), e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_total_year")) - grouplvl))

            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("per_q1_value")) - grouplvl), 1, "%")
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("per_q2_value")) - grouplvl), 1, "%")
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("per_q3_value")) - grouplvl), 1, "%")
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("per_q4_value")) - grouplvl), 1, "%")
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("per_year_value")) - grouplvl), 1, "%")


            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_q1_value")) - grouplvl), decdigit)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_q2_value")) - grouplvl), decdigit)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_q3_value")) - grouplvl), decdigit)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_q4_value")) - grouplvl), decdigit)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_total_year")) - grouplvl), decdigit)

            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q1_value")) - grouplvl), decdigit)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q2_value")) - grouplvl), decdigit)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q3_value")) - grouplvl), decdigit)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q4_value")) - grouplvl), decdigit)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_total_year")) - grouplvl), decdigit)


        End If

        If e.Item.ItemType = C1ListItemType.Footer Then


            If Me.ALFPageAccessRights > AlfPage.Rights.Read Then
                e.Item.Visible = True
            Else
                e.Item.Visible = False
            End If

            If rbl_ROUND.Checked Then
                sum_q1_sales_value = sum_q1_sales_value / 1000
                sum_q1_target_value = sum_q1_target_value / 1000

                sum_q2_sales_value = sum_q2_sales_value / 1000
                sum_q2_target_value = sum_q2_target_value / 1000

                sum_q3_sales_value = sum_q3_sales_value / 1000
                sum_q3_target_value = sum_q3_target_value / 1000

                sum_q4_sales_value = sum_q4_sales_value / 1000
                sum_q4_target_value = sum_q4_target_value / 1000

                sum_sales_total_year = sum_sales_total_year / 1000
                sum_targ_total_year = sum_targ_total_year / 1000

            End If

            divideMe(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("per_q1_value"))), sum_q1_sales_value, sum_q1_target_value)
            divideMe(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("per_q2_value"))), sum_q2_sales_value, sum_q2_target_value)
            divideMe(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("per_q3_value"))), sum_q3_sales_value, sum_q3_target_value)
            divideMe(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("per_q4_value"))), sum_q4_sales_value, sum_q4_target_value)
            divideMe(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("per_year_value"))), sum_sales_total_year, sum_targ_total_year)

            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("per_q1_value"))), 1, "%")
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("per_q2_value"))), 1, "%")
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("per_q3_value"))), 1, "%")
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("per_q4_value"))), 1, "%")
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("per_year_value"))), 1, "%")

            e.Item.CssClass = "reportTotal"
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("prod_cc_description"))).Text = "TOTAL " & ddTPG.SelectedItem.ToString
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_q1_value"))).Text = MyNumberFormat(sum_q1_sales_value, decdigit)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q1_value"))).Text = MyNumberFormat(sum_q1_target_value, decdigit)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_q2_value"))).Text = MyNumberFormat(sum_q2_sales_value, decdigit)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q2_value"))).Text = MyNumberFormat(sum_q2_target_value, decdigit)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_q3_value"))).Text = MyNumberFormat(sum_q3_sales_value, decdigit)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q3_value"))).Text = MyNumberFormat(sum_q3_target_value, decdigit)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_q4_value"))).Text = MyNumberFormat(sum_q4_sales_value, decdigit)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q4_value"))).Text = MyNumberFormat(sum_q4_target_value, decdigit)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_total_year"))).Text = MyNumberFormat(sum_targ_total_year, decdigit)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sales_total_year"))).Text = MyNumberFormat(sum_sales_total_year, decdigit)
        End If

    End Sub



    Private Overloads Sub divideMe(ByVal toCell As TableCell, ByVal div1 As TableCell, ByVal div2 As TableCell)

        Dim d1 As Double = MyNumberFormat(div1.Text, 2)
        Dim d2 As Double = MyNumberFormat(div2.Text, 2)

        'wir müssen die 0er abfangen da sonst einen division durch 0 entsteht oder ein unendlich wert
        'rauskommt. somit abfangen!
        If d1 = 0 Then
            toCell.Text = "0"
        ElseIf d2 = 0 Then
            toCell.Text = "-"
        Else
            Dim tot As Double = (d1 / d2)
            toCell.Text = tot * 100
        End If
    End Sub
    Private Overloads Sub divideMe(ByVal toCell As TableCell, ByVal div1 As Double, ByVal div2 As Double)

        Dim d1 As Double = div1
        Dim d2 As Double = div2

        'wir müssen die 0er abfangen da sonst einen division durch 0 entsteht oder ein unendlich wert
        'rauskommt. somit abfangen!
        If d1 = 0 Then
            toCell.Text = "0"
        ElseIf d2 = 0 Then
            toCell.Text = "-"
        Else
            Dim tot As Double = (d1 / d2)
            toCell.Text = tot * 100
        End If
    End Sub

    Private Overloads Sub divideMe(ByRef Cell As TableCell)
        Try

            Cell.Text = Cell.Text / 1000
        Catch ex As Exception
            Cell.Text = "-"
        End Try

    End Sub
    '**************************************************************************************************************
    '* For Printing
    '**************************************************************************************************************
    Private Sub btnPrint_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Print_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Autoprint Or PopupWindowStyle.NoToolbar)

        bindData()
    End Sub

    Private Sub btnPreview_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Preview_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Fullscreen)

        bindData()
    End Sub

    Private Function PrePrint() As printReportUtil
        bindData()

        repData.addLine("Year", ddYear.SelectedItem.ToString, True, False)
        repData.addLine("TPG", ddTPG.SelectedItem.ToString, True, False)

        If rbl_EXACT.Checked = True Then
            repData.addLine("Numbers:", "Decimals", True, False)
        ElseIf rbl_ROUND.Checked = True Then
            repData.addLine("Numbers:", "1000", True, False)
        End If

        Dim preview As New printReportUtil

        preview.PageTitle = Me.ALFPageTitle
        preview.PageSizeLandscape = 31
        preview.DefaultOrientation = Orientation.Landscape
        preview.AddReportHeader(repData)
        preview.AddWebGrid(Me.MyGrid)

        Return preview

    End Function
    '**************************************************************************************************************
    '* End Printing
    '**************************************************************************************************************

    Private Sub ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportExcel.Click
        bindData()
        Dim exp As exportToExcel = New exportToExcel(Page)
        exp.title = Me.ALFPageTitle
        exp.addLine("Targets for " & ddTPG.SelectedItem.ToString & " for 2004 for " & ddYear.SelectedItem.ToString)
        exp.addDataGrid(MyGrid)
        exp.export()
    End Sub

    Private Sub btnLevel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLevel1.Click
        bindData()
        MyGrid.Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartCollapsed
    End Sub

    Private Sub btnLevel2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLevel2.Click
        bindData()
        MyGrid.Columns(0).GroupInfo.OutlineMode = OutlineModeEnum.StartExpanded
    End Sub

    Private Sub ddTPG_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddTPG.SelectedIndexChanged
        settargetType()
        MyGrid.Visible = False
    End Sub
End Class
