Imports Wyeth.Alf.CssStyles
Imports Wyeth.Utilities.DateHandling
Imports Wyeth.Alf.WyethDropdown
Imports Wyeth.Utilities.NumberFormat




Public Class MonthEndCheck
    Inherits Wyeth.Alf.AlfPage


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MyGridLogistics As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents lblLogistics As System.Web.UI.WebControls.Label
    Protected WithEvents MyGridNoTCogs As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents MyGridUM As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents lblNoTCogs As System.Web.UI.WebControls.Label
    Protected WithEvents lblUM As System.Web.UI.WebControls.Label
    Protected WithEvents ddmonth As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnGenRep As System.Web.UI.WebControls.Button
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents MYGRidStockUnits As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents MyGridStockValues As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents lblStockValues As System.Web.UI.WebControls.Label
    Protected WithEvents lblStockUnits As System.Web.UI.WebControls.Label
    Protected WithEvents ddDistribSelect As System.Web.UI.WebControls.DropDownList
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents ddProduct As System.Web.UI.WebControls.DropDownList
    Protected WithEvents LblSamProdNoAct As System.Web.UI.WebControls.Label
    Protected WithEvents MyGridSamNoAct As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents ddlineselect As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region


    Dim myMep As New MEPData
    Dim MyStock As New Stock
    Dim MyTCogs As New TCogs
    Dim MyProduct As New WyethProduct

    Dim mydate As Date
    Dim UmErrCount As Integer = 0
    Dim sumInvoiceValue, sumAccruedValue, sumDiffToGIT, sumDiffToAccrued As Double
    Dim sumUnits As Integer
    Dim MyReport As New Report
    Dim myDatePopUp As New JSPopUp(Me.page)
    Dim sum0, sum1, sum2, sum3, sum4, sum5 As Double
    Dim StockValuesErrCount As Integer = 0
    Dim StockUnitsErrCount As Integer = 0



    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Page.IsPostBack = True Then
        Else

         

            GetMonthSelectToProcessMonth(ddmonth, CDate(Session("FinalJDEMonth")))
            GetDistribSelectDD(ddDistribSelect, Session("country_id"))
            GetLineSelectDD(ddlineselect, Session("country_id"))

            ddmonth.SelectedValue = Convert.ToDateTime(Session("FinalJDEMonth")).ToString("MMM-yyyy", GetMyDTFI())
            BindData()

        End If
        myDatePopUp.Height = 550
        myDatePopUp.Width = 900
    End Sub


    Private Sub BindData()

        SetGridStylesGroup(MyGridLogistics)
        SetGridStylesGroup(MyGridUM)
        SetGridStyles(MyGridNoTCogs)
        SetGridStylesGroup(MyGridStockValues)
        SetGridStylesGroup(MYGRidStockUnits)
        SetGridStyles(MyGridSamNoAct)


        mydate = Convert.ToDateTime(ddmonth.SelectedValue, GetMyDTFI())


        MyStock.StockDistID = ddDistribSelect.SelectedValue
        MyStock.StockEndDate = LastOfThisMonth(mydate)
        MyStock.StockStartDate = FirstOfThisMonth(mydate)
        MyReport.EndDate = LastOfThisMonth(mydate)
        MyReport.StartDate = FirstOfThisMonth(mydate)
        MyReport.ProductID = 0
        MyReport.DistID = ddDistribSelect.SelectedValue
        MyReport.LineID = ddlineselect.SelectedValue


        MyGridUM.DataSource = MyStock.GetStockCrossCheckUM()
        MyGridNoTCogs.DataSource = MyTCogs.GetProductsWithNoTCOGSForMonth(LastOfThisMonth(mydate), Session("country_id"))
        MyGridStockValues.DataSource = MyReport.GetStockStatValues(ddProduct, Me)
        MYGRidStockUnits.DataSource = MyReport.GetStockStat(ddProduct, Me)
        MyGridLogistics.DataSource = myMep.CheckTCOGSForProcessing(Session("CurrentProcessMonth"), Session("country_id"))
        MyGridSamNoAct.DataSource = MyProduct.GetSamProductsWithActProductForMonth(FirstOfThisMonth(mydate), Session("country_id"))




        MyGridUM.DataBind()
        MyGridNoTCogs.DataBind()
        MyGridStockValues.DataBind()
        MYGRidStockUnits.DataBind()
        MyGridLogistics.DataBind()
        MyGridSamNoAct.DataBind()


        MyGridUM.ShowFooter = False
        MyGridNoTCogs.ShowFooter = False
        MyGridStockValues.ShowFooter = False
        MYGRidStockUnits.ShowFooter = False
        MyGridLogistics.ShowFooter = False
        MyGridSamNoAct.ShowFooter = False


        If MyGridNoTCogs.Items.Count = 0 Then
            MyGridNoTCogs.Visible = False
            lblNoTCogs.Visible = True
            lblNoTCogs.Text = "1.) All products that had a movement in this month have also their TCogs"
            lblNoTCogs.CssClass = "success"
        Else
            MyGridNoTCogs.Visible = True
            lblNoTCogs.Visible = True
            lblNoTCogs.Text = "1.) These products had a movement in this month but do no have TCogs"
            lblNoTCogs.CssClass = "nosuccess"
        End If

        If MyGridSamNoAct.Items.Count = 0 Then
            MyGridSamNoAct.Visible = False
            LblSamProdNoAct.Visible = True
            LblSamProdNoAct.Text = "2.) All sample products that had a movement in this month are assigned to an actual product"
            LblSamProdNoAct.CssClass = "success"
        Else
            MyGridSamNoAct.Visible = True
            LblSamProdNoAct.Visible = True
            LblSamProdNoAct.Text = "2.) The following sample products had a movement in this month but are not assigned to an actual product"
            LblSamProdNoAct.CssClass = "nosuccess"
        End If


        If UmErrCount = 0 Then
            MyGridUM.Visible = False
            lblUM.Visible = False
            lblUM.Visible = True
            lblUM.Text = "3.) All 'Musterumwandlungen' for this month are correct"
            lblUM.CssClass = "success"
        Else
            MyGridUM.Visible = True
            lblUM.Visible = True
            lblUM.Text = "3.) These 'Musterumwandlungen' for this month are not correct"
            lblUM.CssClass = "nosuccess"
        End If

        If MyGridLogistics.Items.Count = 0 Then
            MyGridLogistics.Visible = False
            lblLogistics.Visible = True
            lblLogistics.Text = "4.) All GIT and WEs for this month have been checked and seem to be correct"
            lblLogistics.CssClass = "success"
        Else
            MyGridLogistics.Visible = True
            lblLogistics.Visible = True
            lblLogistics.Text = "4.) The following GITs and WEs do not match with their TCogs prices"
            lblLogistics.CssClass = "nosuccess"
        End If


        If StockValuesErrCount = 0 Then
            MyGridStockValues.Visible = False
            lblStockValues.Visible = True
            lblStockValues.Text = "5.) All values for the Stock have been checked and seem to be correct"
            lblStockValues.CssClass = "success"
        Else
            MyGridStockValues.Visible = True
            lblStockValues.Visible = True
            lblStockValues.Text = "5.) The Stock values for these products seem to be incorrect"
            lblStockValues.CssClass = "nosuccess"
        End If


        If StockUnitsErrCount = 0 Then
            MYGRidStockUnits.Visible = False
            lblStockUnits.Visible = True
            lblStockUnits.Text = "6.) All units for the Stock have been checked and seem to be correct"
            lblStockUnits.CssClass = "success"
        Else
            MYGRidStockUnits.Visible = True
            lblStockUnits.Visible = True
            lblStockUnits.Text = "6.) The Stock units for these products seem to be incorrect"
            lblStockUnits.CssClass = "nosuccess"
        End If

      

    End Sub

    Private Sub MYGridUM_Databound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGridUM.ItemDataBound
        If (CInt(e.Item.Cells(4).Text) + CInt(e.Item.Cells(8).Text)) <> 0 Then
            Dim cell As TableCell
            For Each cell In e.Item.Cells
                cell.Attributes.Add("style", "border-top:solid 2px #FF0000; border-bottom:solid 2px #FF0000; color:#ff0000;")
            Next
            e.Item.Cells(2).Text = "<strong>WARNING!</strong><BR>" & e.Item.Cells(2).Text
            e.Item.Cells(1).Attributes.Add("style", "border-top:solid 2px #FF0000; border-bottom:solid 2px #FF0000;  border-left:solid 2px #FF0000; color:#ff0000;")
            e.Item.Cells(e.Item.Cells.Count - 1).Attributes.Add("style", "border-top:solid 2px #FF0000; border-bottom:solid 2px #FF0000;  border-right:solid 2px #FF0000; color:#ff0000;")
            UmErrCount = UmErrCount + 1
        Else
            e.Item.Visible = False
        End If
    End Sub


    Private Sub MyGridLogistics_Databound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGridLogistics.ItemDataBound
        sumInvoiceValue = sumInvoiceValue + e.Item.Cells(MyGridLogistics.Columns.IndexOf(MyGridLogistics.Columns.ColumnByName("stoc_invoice_value"))).Text
        sumAccruedValue = sumAccruedValue + e.Item.Cells(MyGridLogistics.Columns.IndexOf(MyGridLogistics.Columns.ColumnByName("stoc_accrued_value"))).Text
        sumDiffToGIT = sumDiffToGIT + e.Item.Cells(MyGridLogistics.Columns.IndexOf(MyGridLogistics.Columns.ColumnByName("stoc_diff_value"))).Text
        sumDiffToAccrued = sumDiffToAccrued + e.Item.Cells(MyGridLogistics.Columns.IndexOf(MyGridLogistics.Columns.ColumnByName("stoc_diff_value_accrued"))).Text
        sumUnits = sumUnits + e.Item.Cells(MyGridLogistics.Columns.IndexOf(MyGridLogistics.Columns.ColumnByName("stoc_unit"))).Text

        MyNumberFormat(e.Item.Cells(MyGridLogistics.Columns.IndexOf(MyGridLogistics.Columns.ColumnByName("stoc_unit"))), 0)
        MyNumberFormat(e.Item.Cells(MyGridLogistics.Columns.IndexOf(MyGridLogistics.Columns.ColumnByName("stoc_invoice_value"))), 2)
        MyNumberFormat(e.Item.Cells(MyGridLogistics.Columns.IndexOf(MyGridLogistics.Columns.ColumnByName("stoc_accrued_value"))), 2)
        MyNumberFormat(e.Item.Cells(MyGridLogistics.Columns.IndexOf(MyGridLogistics.Columns.ColumnByName("stoc_diff_value"))), 2)
        MyNumberFormat(e.Item.Cells(MyGridLogistics.Columns.IndexOf(MyGridLogistics.Columns.ColumnByName("stoc_diff_value_accrued"))), 2)
    End Sub

    Private Sub MyGridLogistics_ItemCreated(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGridLogistics.ItemCreated

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.GroupHeader Or e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.GroupFooter Then
            MyNumberFormat(e.Item.Cells(MyGridLogistics.Columns.IndexOf(MyGridLogistics.Columns.ColumnByName("stoc_unit")) - 8), 0)
            MyNumberFormat(e.Item.Cells(MyGridLogistics.Columns.IndexOf(MyGridLogistics.Columns.ColumnByName("stoc_invoice_value")) - 8), 2)
            MyNumberFormat(e.Item.Cells(MyGridLogistics.Columns.IndexOf(MyGridLogistics.Columns.ColumnByName("stoc_accrued_value")) - 8), 2)
            MyNumberFormat(e.Item.Cells(MyGridLogistics.Columns.IndexOf(MyGridLogistics.Columns.ColumnByName("stoc_diff_value")) - 8), 2)
            MyNumberFormat(e.Item.Cells(MyGridLogistics.Columns.IndexOf(MyGridLogistics.Columns.ColumnByName("stoc_diff_value_accrued")) - 8), 2)
        End If

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Footer Then
            e.Item.HorizontalAlign = HorizontalAlign.Right
            e.Item.Cells(MyGridLogistics.Columns.IndexOf(MyGridLogistics.Columns.ColumnByName("stoc_invoice_value"))).Text = MyNumberFormat(sumInvoiceValue, 2)
            e.Item.Cells(MyGridLogistics.Columns.IndexOf(MyGridLogistics.Columns.ColumnByName("stoc_accrued_value"))).Text = MyNumberFormat(sumAccruedValue, 2)
            e.Item.Cells(MyGridLogistics.Columns.IndexOf(MyGridLogistics.Columns.ColumnByName("stoc_diff_value"))).Text = MyNumberFormat(sumDiffToGIT, 2)
            e.Item.Cells(MyGridLogistics.Columns.IndexOf(MyGridLogistics.Columns.ColumnByName("stoc_diff_value_accrued"))).Text = MyNumberFormat(sumDiffToAccrued, 2)
            e.Item.Cells(MyGridLogistics.Columns.IndexOf(MyGridLogistics.Columns.ColumnByName("stoc_unit"))).Text = MyNumberFormat(sumUnits, 0)
        End If

    End Sub

    Private Sub MyGridStockValues_ItemCreated(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGridStockValues.ItemCreated

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Then
            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor1Class';")
        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor2Class';")
        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Footer Then
            e.Item.Cells(3).Text = "TOTAL:"
            e.Item.Cells(3).HorizontalAlign = HorizontalAlign.Right

            ' write sum values in the grid footer
            e.Item.Cells(4).Text = MyNumberFormat(sum0, CInt(2))
            e.Item.Cells(5).Text = MyNumberFormat(sum1, CInt(2))
            e.Item.Cells(6).Text = MyNumberFormat(sum2, CInt(2))
            e.Item.Cells(7).Text = MyNumberFormat(sum3, CInt(2))
            e.Item.Cells(8).Text = MyNumberFormat(sum4, CInt(2))
            e.Item.Cells(9).Text = MyNumberFormat(sum5, CInt(2))

            'reset sum values
            ResetSum()

        End If

    End Sub

    Private Sub MyGridStockValuesItem_Databound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGridStockValues.ItemDataBound

        ' setColumnToolTips(e.Item, sender.GetType.)

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Or e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Then

            Dim korr, out, out_fg, we, startbalance, endbalance As Double
            Dim JS_byProduct As String = "javascript:OpenPopUp('StockProduct.aspx?id=" & (e.Item.Cells(0).Text) & "&sd=" & MyReport.StartDate & "&ed=" & MyReport.EndDate & "&dist_id=" & ddDistribSelect.SelectedValue & "&phznr=" & e.Item.Cells(2).Text & "&presentation=" & e.Item.Cells(3).Text & "&pagetitle=Stock by Product" & "  ','StockbyProduct');"

            setTDMouseover(e.Item.Cells(3))
            setTDMouseover(e.Item.Cells(4))
            setTDMouseover(e.Item.Cells(5))
            setTDMouseover(e.Item.Cells(6))
            setTDMouseover(e.Item.Cells(7))
            setTDMouseover(e.Item.Cells(8))

            e.Item.Cells(1).Attributes.Add("style", "cursor:default;")
            e.Item.Cells(9).Attributes.Add("style", "cursor:default;")

            e.Item.Cells(3).Attributes.Add("onclick", JS_byProduct)
            e.Item.Cells(4).Attributes.Add("onclick", JS_byProduct)
            e.Item.Cells(5).Attributes.Add("onclick", "javascript:OpenPopUp('IN.aspx?id=" & (e.Item.Cells(0).Text) & "&sd=" & MyReport.StartDate & "&ed=" & MyReport.EndDate & "&dist_id=" & ddDistribSelect.SelectedValue & "&phznr=" & e.Item.Cells(2).Text & "&presentation=" & e.Item.Cells(3).Text & "&pagetitle=Stock Wareneingang" & "  ','WE');")
            e.Item.Cells(6).Attributes.Add("onclick", "javascript:OpenPopUp('OUT.aspx?id=" & (e.Item.Cells(0).Text) & "&sd=" & MyReport.StartDate & "&ed=" & MyReport.EndDate & "&dist_id=" & ddDistribSelect.SelectedValue & "&phznr=" & e.Item.Cells(2).Text & "&presentation=" & e.Item.Cells(3).Text & "&pagetitle=Stock Sales" & " ','Sales');")
            e.Item.Cells(7).Attributes.Add("onclick", "javascript:OpenPopUp('OUT_FG.aspx?id=" & (e.Item.Cells(0).Text) & "&sd=" & MyReport.StartDate & "&ed=" & MyReport.EndDate & "&dist_id=" & ddDistribSelect.SelectedValue & "&phznr=" & e.Item.Cells(2).Text & "&presentation=" & e.Item.Cells(3).Text & "&pagetitle=Stock Free Goods" & " ','FreeGoods');")
            e.Item.Cells(8).Attributes.Add("onclick", "javascript:OpenPopUp('KORR.aspx?id=" & (e.Item.Cells(0).Text) & "&sd=" & MyReport.StartDate & "&ed=" & MyReport.EndDate & "&dist_id=" & ddDistribSelect.SelectedValue & "&phznr=" & e.Item.Cells(2).Text & "&presentation=" & e.Item.Cells(3).Text & "&pagetitle=Stock Corrections" & " ','Corrections');")


            startbalance = e.Item.Cells(4).Text
            sum0 = sum0 + startbalance


            we = e.Item.Cells(5).Text()
            sum1 = sum1 + we

            out = e.Item.Cells(6).Text()
            sum2 = sum2 + out

            out_fg = e.Item.Cells(7).Text()
            sum3 = sum3 + out_fg

            korr = e.Item.Cells(8).Text()
            sum4 = sum4 + korr

            endbalance = e.Item.Cells(9).Text
            sum5 = sum5 + endbalance



            e.Item.Cells(4).Text = MyNumberFormat(startbalance, CInt(2))
            e.Item.Cells(5).Text = MyNumberFormat(we, CInt(2))
            e.Item.Cells(6).Text = MyNumberFormat(out, CInt(2))
            e.Item.Cells(7).Text = MyNumberFormat(out_fg, CInt(2))
            e.Item.Cells(8).Text = MyNumberFormat(korr, CInt(2))
            e.Item.Cells(9).Text = MyNumberFormat(endbalance, CInt(2))

            If MyNumberFormat((startbalance + we - out - out_fg + korr), CInt(2)) <> MyNumberFormat(endbalance, CInt(2)) Then

                Dim cell As TableCell
                For Each cell In e.Item.Cells
                    cell.Attributes.Add("style", "border-top:solid 2px #FF0000; border-bottom:solid 2px #FF0000; color:#ff0000;")
                Next

                e.Item.Cells(1).Text = "<strong>WARNING!</strong><BR>" & e.Item.Cells(1).Text
                e.Item.Cells(1).Attributes.Add("style", "border-top:solid 2px #FF0000; border-bottom:solid 2px #FF0000;  border-left:solid 2px #FF0000; color:#ff0000;")
                e.Item.Cells(e.Item.Cells.Count - 1).Attributes.Add("style", "border-top:solid 2px #FF0000; border-bottom:solid 2px #FF0000;  border-right:solid 2px #FF0000; color:#ff0000;")

                e.Item.Cells(9).Text = "Sanova:" & e.Item.Cells(9).Text & "<br>" & "Wyeth:" & startbalance + we - out - out_fg + korr
                StockValuesErrCount = StockValuesErrCount + 1
            Else
                e.Item.Visible = False

            End If
        End If

    End Sub
    Private Sub MyGridStockUnits_ItemCreated(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MYGRidStockUnits.ItemCreated

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Then
            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor1Class';")
        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor2Class';")
        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Footer Then
            e.Item.Cells(3).Text = "TOTAL:"
            e.Item.Cells(3).HorizontalAlign = HorizontalAlign.Right

            ' write sum values in the grid footer
            e.Item.Cells(4).Text = MyNumberFormat(sum0, CInt(0))
            e.Item.Cells(5).Text = MyNumberFormat(sum1, CInt(0))
            e.Item.Cells(6).Text = MyNumberFormat(sum2, CInt(0))
            e.Item.Cells(7).Text = MyNumberFormat(sum3, CInt(0))
            e.Item.Cells(8).Text = MyNumberFormat(sum4, CInt(0))
            e.Item.Cells(9).Text = MyNumberFormat(sum5, CInt(0))

            'reset sum values
            ResetSum()

        End If

    End Sub

    Private Sub MyGridStockUnitsItem_Databound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MYGRidStockUnits.ItemDataBound

        ' setColumnToolTips(e.Item, sender.GetType.)

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Or e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Then

            Dim korr, out, out_fg, we, startbalance, endbalance As Double
            Dim JS_byProduct As String = "javascript:OpenPopUp('StockProduct.aspx?id=" & (e.Item.Cells(0).Text) & "&sd=" & MyReport.StartDate & "&ed=" & MyReport.EndDate & "&dist_id=" & ddDistribSelect.SelectedValue & "&phznr=" & e.Item.Cells(2).Text & "&presentation=" & e.Item.Cells(3).Text & "&pagetitle=Stock by Product" & "  ','StockbyProduct');"

            setTDMouseover(e.Item.Cells(3))
            setTDMouseover(e.Item.Cells(4))
            setTDMouseover(e.Item.Cells(5))
            setTDMouseover(e.Item.Cells(6))
            setTDMouseover(e.Item.Cells(7))
            setTDMouseover(e.Item.Cells(8))

            e.Item.Cells(1).Attributes.Add("style", "cursor:default;")
            e.Item.Cells(9).Attributes.Add("style", "cursor:default;")

            e.Item.Cells(3).Attributes.Add("onclick", JS_byProduct)
            e.Item.Cells(4).Attributes.Add("onclick", JS_byProduct)
            e.Item.Cells(5).Attributes.Add("onclick", "javascript:OpenPopUp('IN.aspx?id=" & (e.Item.Cells(0).Text) & "&sd=" & MyReport.StartDate & "&ed=" & MyReport.EndDate & "&dist_id=" & ddDistribSelect.SelectedValue & "&phznr=" & e.Item.Cells(2).Text & "&presentation=" & e.Item.Cells(3).Text & "&pagetitle=Stock Wareneingang" & "  ','WE');")
            e.Item.Cells(6).Attributes.Add("onclick", "javascript:OpenPopUp('OUT.aspx?id=" & (e.Item.Cells(0).Text) & "&sd=" & MyReport.StartDate & "&ed=" & MyReport.EndDate & "&dist_id=" & ddDistribSelect.SelectedValue & "&phznr=" & e.Item.Cells(2).Text & "&presentation=" & e.Item.Cells(3).Text & "&pagetitle=Stock Sales" & " ','Sales');")
            e.Item.Cells(7).Attributes.Add("onclick", "javascript:OpenPopUp('OUT_FG.aspx?id=" & (e.Item.Cells(0).Text) & "&sd=" & MyReport.StartDate & "&ed=" & MyReport.EndDate & "&dist_id=" & ddDistribSelect.SelectedValue & "&phznr=" & e.Item.Cells(2).Text & "&presentation=" & e.Item.Cells(3).Text & "&pagetitle=Stock Free Goods" & " ','FreeGoods');")
            e.Item.Cells(8).Attributes.Add("onclick", "javascript:OpenPopUp('KORR.aspx?id=" & (e.Item.Cells(0).Text) & "&sd=" & MyReport.StartDate & "&ed=" & MyReport.EndDate & "&dist_id=" & ddDistribSelect.SelectedValue & "&phznr=" & e.Item.Cells(2).Text & "&presentation=" & e.Item.Cells(3).Text & "&pagetitle=Stock Corrections" & " ','Corrections');")


            startbalance = e.Item.Cells(4).Text
            sum0 = sum0 + startbalance


            we = e.Item.Cells(5).Text()
            sum1 = sum1 + we

            out = e.Item.Cells(6).Text()
            sum2 = sum2 + out

            out_fg = e.Item.Cells(7).Text()
            sum3 = sum3 + out_fg

            korr = e.Item.Cells(8).Text()
            sum4 = sum4 + korr

            endbalance = e.Item.Cells(9).Text
            sum5 = sum5 + endbalance



            e.Item.Cells(4).Text = MyNumberFormat(startbalance, CInt(0))
            e.Item.Cells(5).Text = MyNumberFormat(we, CInt(0))
            e.Item.Cells(6).Text = MyNumberFormat(out, CInt(0))
            e.Item.Cells(7).Text = MyNumberFormat(out_fg, CInt(0))
            e.Item.Cells(8).Text = MyNumberFormat(korr, CInt(0))
            e.Item.Cells(9).Text = MyNumberFormat(endbalance, CInt(0))

            If MyNumberFormat((startbalance + we - out - out_fg + korr), CInt(0)) <> MyNumberFormat(endbalance, CInt(0)) Then

                Dim cell As TableCell
                For Each cell In e.Item.Cells
                    cell.Attributes.Add("style", "border-top:solid 2px #FF0000; border-bottom:solid 2px #FF0000; color:#ff0000;")
                Next

                e.Item.Cells(1).Text = "<strong>WARNING!</strong><BR>" & e.Item.Cells(1).Text
                e.Item.Cells(1).Attributes.Add("style", "border-top:solid 2px #FF0000; border-bottom:solid 2px #FF0000;  border-left:solid 2px #FF0000; color:#ff0000;")
                e.Item.Cells(e.Item.Cells.Count - 1).Attributes.Add("style", "border-top:solid 2px #FF0000; border-bottom:solid 2px #FF0000;  border-right:solid 2px #FF0000; color:#ff0000;")

                e.Item.Cells(9).Text = "Sanova:" & e.Item.Cells(9).Text & "<br>" & "Wyeth:" & startbalance + we - out - out_fg + korr
                StockUnitsErrCount = StockUnitsErrCount + 1
            Else
                e.Item.Visible = False
            End If
        End If

    End Sub



    Private Sub MyGrid_ItemCreated(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGridNoTCogs.ItemCreated, MyGridSamNoAct.ItemCreated

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Item Then
            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor1Class';")
        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.className='tableMouseoverColor';this.style.cursor='hand'")
            e.Item.Attributes.Add("onmouseOut", "this.className='tableBgColor2Class';")
        End If
        ' e.Item.Attributes.Add("onclick", "javascript:__doPostBack('" & "MyGrid$" & "R" & (e.Item.ItemIndex) & "$_ctl0','');")

    End Sub

    Private Sub MyGrid_ItemDataBound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGridNoTCogs.ItemDataBound, MyGridSamNoAct.ItemDataBound

        myDatePopUp.PageURL = "../admin/ProductByID.aspx?prod_id=" & e.Item.Cells(0).Text
        myDatePopUp.AddPopupToControl(e.Item)
    End Sub


    Private Sub btnGenRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRep.Click
        BindData()
    End Sub

    Private Sub ResetSum()
        sum0 = 0 : sum1 = 0 : sum2 = 0 : sum3 = 0
        sum4 = 0 : sum5 = 0
    End Sub

    Private Sub ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportExcel.Click
        BindData()

      
        Dim exp As exportToExcel = New exportToExcel(Page)

        exp.addDataGrid(MyGridUM)
        exp.addDataGrid(MyGridNoTCogs)
        exp.addDataGrid(MyGridStockValues)
        exp.addDataGrid(MYGRidStockUnits)
        exp.addDataGrid(MyGridLogistics)
        exp.addDataGrid(MyGridSamNoAct)
        exp.export()
    End Sub
End Class
