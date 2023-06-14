Imports C1.Web.C1WebGrid
Imports Wyeth.Alf.WyethDropdown
Imports Wyeth.Alf.CssStyles
Imports Wyeth.Utilities.NumberFormat
Imports Wyeth.Alf.JSPopUp


Public Class TargetApproval

    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btn_show_targets As System.Web.UI.WebControls.Button
    Protected WithEvents ddSare As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents reportpanel As System.Web.UI.WebControls.Panel
    Protected WithEvents mybutton As Button
    Protected WithEvents btnGenRep As System.Web.UI.WebControls.Button
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents prtControl As Wyeth.Alf.printReportCtl
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents lblCurrency As System.Web.UI.WebControls.Label
    Protected WithEvents repdata As reportData
    Protected WithEvents ddtapg As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblTargetType As System.Web.UI.WebControls.Label

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
    Dim tapg_id, version, user_name_approval, sare_id, sare_name, showAll As String
    Dim MyJs As New JSPopUp(Me)
    Dim sumYear As Double
    Dim x As Integer
    Public strFrame, requestedPage As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        strFrame = Request.ServerVariables("SCRIPT_NAME") & Request.Url.Query
        requestedPage = Request.QueryString("requestedURl")
        Dim strScript As String
        Dim year, sare_id, version, tapg_id As String

        strScript = "<script language=javascript>" & vbNewLine
        strScript += "var correct_frame = 0 + (parent.header ? 1 : 0);" & vbNewLine
        strScript += "if (self == top || !correct_frame) {" & vbNewLine
        strScript += "top.location.href = '" & Utilities.Settings.applicationUrl & "default.aspx?requestedpage=" & strFrame & "';" & vbNewLine & "}" & vbNewLine & "</script>"
      
        RegisterStartupScript("anything", strScript)
        Response.Write(strScript)
        'MyGrid.Visible = False

        If Page.IsPostBack = True Then


        Else
            GetYearDD4(ddYear, 2004, 1)

            GetTargetProductGroupSelectDD(ddtapg, Session("country_id"))
            GetSaReForTPGDD(ddSare, ddtapg.SelectedValue)
            AddTPGAll()
            MyGrid.Visible = False

            If Request.QueryString("sare_id") <> "" And Request.QueryString("approval") = "" Then

                year = Request.QueryString("year")
                sare_id = Request.QueryString("sare_id")
                version = Request.QueryString("version")
                tapg_id = Request.QueryString("tapg_id")
                showAll = Request.QueryString("showAll")
               
                Mytrageting.ApproveTaregtsVersion(year, sare_id, version, tapg_id, Session("user_id"))

                ddtapg.SelectedValue = tapg_id
                GetSaReForTPGDD(ddSare, ddtapg.SelectedValue)
                AddTPGAll()

                ddSare.SelectedValue = showAll
            
                ddYear.SelectedValue = year

                MyGrid.Visible = True
                bindData()
            End If

            If Request.QueryString("approval") = "YES" Then
                ' User comes from a HREF Link in a mail that is being sent 
                ' when a new target has been entered !!(NewTragetVersion.aspx)
                Dim MyTarget As New Wyeth.Alf.Targeting

                year = Request.QueryString("year")
                sare_id = Request.QueryString("sare_id")
                tapg_id = Request.QueryString("tapg_id")
                '  ddtapg.SelectedValue = CInt(MyTarget.GetTPForSalesRep(sare_id, Session("country_id")))

                ddtapg.SelectedValue = tapg_id
                GetSaReForTPGDD(ddSare, ddtapg.SelectedValue)
                AddTPGAll()
                ddSare.SelectedValue = sare_id
                ddYear.SelectedValue = year

                MyGrid.Visible = True
                bindData()
            End If
        End If
        settargetType()
    End Sub
    Private Sub settargetType()
        Dim mytargetType As String

        mytargetType = Mytrageting.GetTargetType(CInt(ddtapg.SelectedValue))
        If mytargetType = "UNIT" Then
            x = 0
        ElseIf mytargetType = "VALUE" Then
            x = 2
        End If

        Me.lblCurrency.Text = "Target Type: UNIT"

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
        Dim myDataView As New DataView
        MyGrid.ShowFooter = False

        SetGridStylesGroup(MyGrid)
        myDataView = Mytrageting.GetTaregtsForApproval(ddYear.SelectedValue, ddSare.SelectedValue, ddtapg.SelectedValue)

        If ddSare.SelectedValue <> 0 Then
            myDataView.RowFilter = "sare_id=" & ddSare.SelectedValue
        End If
        MyGrid.DataSource = myDataView
        MyGrid.DataBind()


    End Sub

    Private Sub btnGenRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_show_targets.Click
        MyGrid.Visible = True
        bindData()
    End Sub

    Private Sub MyGrid_ItemCreated(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated

        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.GroupHeader And e.Item.Attributes.Item("nodelevel") = "2" Then
            'e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q4_value"))).Text = MyNumberFormat(sumYear, 2)
            'sumYear = 0
            Dim sum As Double
            Dim targ1, targ2, targ3, targ4 As String

            targ1 = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q1_value")) - 2).Text
            targ2 = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q2_value")) - 2).Text
            targ3 = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q3_value")) - 2).Text
            targ4 = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q4_value")) - 2).Text

            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q1_value")) - 2), x)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q2_value")) - 2), x)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q3_value")) - 2), x)
            MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q4_value")) - 2), x)
            'sum = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q1_value")) - 2).Text
            'sum += e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q2_value")) - 2).Text
            'sum += e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q3_value")) - 2).Text
            'sum += e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q4_value")) - 2).Text

            If targ1 = String.Empty Then
                targ1 = 0
            End If
            If targ2 = String.Empty Then
                targ2 = 0
            End If
            If targ3 = String.Empty Then
                targ3 = 0
            End If
            If targ4 = String.Empty Then
                targ4 = 0
            End If

            sum = CDbl(targ1) + CDbl(targ2) + CDbl(targ3) + CDbl(targ4)
            e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q4_value")) - 1).Text = MyNumberFormat(sum, x)

            If e.Item.Attributes.Item("nodelevel") = "2" Then
                   If user_name_approval = "" Then
                    Dim mylink As New HyperLink

                    '   sare_id = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sare_id")) - 2).Text

                    mylink.Text = "Approve"
                    ' mylink.NavigateUrl = "TargetApproval.aspx?version=" & version & "&sare_id=" & ddSare.SelectedValue & "&year=" & ddYear.SelectedValue & "&tapg_id=" & tapg_id
                    mylink.NavigateUrl = "TargetApproval.aspx?version=" & version & "&sare_id=" & sare_id & "&year=" & ddYear.SelectedValue & "&tapg_id=" & tapg_id & "&showAll=" & ddSare.SelectedValue

                    mylink.CssClass = "Button"
                    mylink.Width = New Unit().Pixel(100)
                    MyJs.ConfirmMessage = "Are you really sure that you want to approve Target version v_" & version & " for " & sare_name & " ?"
                    MyJs.AddGetConfirm(mylink)

                    Dim s As New Style
                    s.ForeColor = Color.White
                    mylink.MergeStyle(s)

                    e.Item.Cells(6).Controls.Add(mylink)

                    version = ""
                    tapg_id = ""

                Else
                    e.Item.Cells(6).Text = "approved"

                End If

            End If


        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Footer Then
            e.Item.Visible = False
        ElseIf e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.GroupHeader And e.Item.Attributes.Item("nodelevel") = "1" Then
            Dim i As Integer = 0
            For Each cell As TableCell In e.Item.Cells
                If i > 0 Then
                    cell.Text = ""
                End If
                i = i + 1
            Next

        End If

    End Sub

    Private Sub MyGrid_ItemDatabound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound
        Dim sum As Double

        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q1_value"))), x)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q2_value"))), x)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q3_value"))), x)
        MyNumberFormat(e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q4_value"))), x)

        sum = e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q1_value"))).Text
        sum += e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q2_value"))).Text
        sum += e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q3_value"))).Text
        sum += e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q4_value"))).Text
        e.Item.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_q4_value")) + 1).Text = MyNumberFormat(sum, x)

        sumYear = sumYear + sum
    End Sub

    Private Sub MyGrid_ItemGrouptext(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1GroupTextEventArgs) Handles MyGrid.GroupAggregate

        Dim startGriditem As C1GridItem
        startGriditem = MyGrid.Items(e.StartItemIndex)
        sare_id = startGriditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sare_id"))).Text
        sare_name = startGriditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("sare_name"))).Text
        tapg_id = startGriditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("tapg_id"))).Text
        version = startGriditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("targ_version"))).Text
        user_name_approval = startGriditem.Cells(MyGrid.Columns.IndexOf(MyGrid.Columns.ColumnByName("user_name_approval"))).Text.Trim
    End Sub

    '**************************************************************************************************************
    '* For Printing
    '**************************************************************************************************************
    Private Sub btnPrint_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Print_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Autoprint Or PopupWindowStyle.NoToolbar)
        MyGrid.Visible = True
        bindData()
    End Sub

    Private Sub btnPreview_onClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prtControl.Preview_Click
        PrePrint.print(Me.Page, PopupWindowStyle.Fullscreen)

        bindData()
    End Sub

    Private Function PrePrint() As printReportUtil
        bindData()

        Dim preview As New printReportUtil


        repdata.addLine("Year", ddYear.SelectedItem.ToString, True, False)
        repdata.addLine("Sales Rep", ddSare.SelectedItem.ToString, True, False)

        preview.PageTitle = Me.ALFPageTitle
        preview.AddReportHeader(repdata)
        preview.PageSizeLandscape = 31
        preview.DefaultOrientation = Orientation.Landscape
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
        exp.addLine("Targets for" & ddSare.SelectedItem.ToString & " for " & ddYear.SelectedItem.ToString)
        exp.addDataGrid(MyGrid)
        exp.export()
    End Sub


    Private Sub ddtapg_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddtapg.SelectedIndexChanged
        MyGrid.Visible = False
        GetSaReForTPGDD(ddSare, ddtapg.SelectedValue)
        AddTPGAll()

    End Sub
    Private Sub AddTPGAll()
        Dim li As New ListItem
        li.Text = "---All---"
        li.Value = 0
        ddSare.Items.Insert(0, li)
    End Sub
End Class
