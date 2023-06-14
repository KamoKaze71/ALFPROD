Imports Oracle.DataAccess.Client
Imports Wyeth.Alf
Imports Wyeth.Utilities
Imports Wyeth.Alf.WyethAppllication
Imports Wyeth.Utilities.NumberFormat
Imports Wyeth.Utilities.Helper
Imports System.Globalization
Imports Wyeth.Alf.JSPopUp
Imports Wyeth.Utilities.DateHandling


'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
'''         <revision>1</revision>
''' <summary><para>Start Page for ALF Application Shows the ALF Distributor Import Status and Import Errors and Sales value Summary</para></summary>
'''<seealso cref="Main.aspx">[label]</seealso> 

Public Class Main
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
	Protected WithEvents Form1 As System.Web.UI.HtmlControls.HtmlForm
	Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
	Protected WithEvents Label1 As System.Web.UI.WebControls.Label
	Protected WithEvents Label2 As System.Web.UI.WebControls.Label
	Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents repData As reportData
    Protected WithEvents austriaSales As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents sales As System.Web.UI.WebControls.DataGrid
    Protected WithEvents myGrid As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblImportErrors As System.Web.UI.WebControls.Label
    Protected WithEvents pnlErrors As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents phErrors As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents phstockerrors As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents MyGridYear As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblTranDate As System.Web.UI.WebControls.Label
    Protected WithEvents phNewProducts As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents phNewCustomers As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents statusTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents phNotImportedTransmisisons As System.Web.UI.WebControls.PlaceHolder

    Protected WithEvents phNotImportedTransmisisonsMuenster As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents statusTableMuenster As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents phErrorsMuenster As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents phstockerrorsMuenster As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents lblImportErrorsMuenster As System.Web.UI.WebControls.Label
    Protected WithEvents lblTranDateMuenster As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorPanelMuenster As System.Web.UI.WebControls.Panel
    Protected WithEvents PlaceHolder1 As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents PlaceHolder2 As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents PLACEHOLDER3 As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents exportStatusPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents phLastApoExport As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents phAPOServiceStatus As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents exportStatusTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lnkAbout As System.Web.UI.HtmlControls.HtmlAnchor
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
	'Do not delete or move it.
	Private designerPlaceholderDeclaration As System.Object

	Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
		'CODEGEN: This method call is required by the Web Form Designer
		'Do not modify it using the code editor.
		InitializeComponent()
	End Sub

#End Region


    Dim MyImport As New WyethImport
    Dim myService As New ServiceControl
    Dim MyNFI As New NumberFormatInfo
    Dim MyUser As New UserAccess
    Dim MyCustomerPopUp As New JSPopUp(Me)
    Dim MyProductPopUp As New JSPopUp(Me)
    Dim MyAboutPopUp As New JSPopUp(Me)
    Dim div As HtmlGenericControl

    Public Tran_date, Tran_date_muenster, import_date_muenster, import_date, lastExportDate As String
    Public tran_id, tran_id_muenster As String



    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MyNFI = getmynfi(2)
        Me.ALFPageTitle = "Austria Sales"
        If Not Page.IsPostBack Then
            MyProductPopUp.Width = 900
            MyProductPopUp.Height = 550
            MyCustomerPopUp.Height = 350
            MyCustomerPopUp.Width = 750
            MyAboutPopUp.Width = 400
            MyAboutPopUp.Height = 200
            MyAboutPopUp.PageURL = "About.aspx"
            MyAboutPopUp.AddPopupToControl(Me.lnkAbout)

            BindData()
        End If
    End Sub
    Private Sub roundColumn(ByVal dataGridRowCell As TableCell)
        If Not dataGridRowCell.Text = "" Then
            Dim d As Double
            d = CDbl(dataGridRowCell.Text)
            dataGridRowCell.Text = "€ " & d.ToString(NUMBER_FORMAT_STRING_EXACT, MyNFI)
        End If
    End Sub
    Private Sub myGrid_ItemCreated(ByVal source As Object, ByVal e As DataGridItemEventArgs) Handles myGrid.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            Dim i As Integer = 0
            For Each cell As TableCell In e.Item.Cells
                If i = 2 Or i = 5 Or i = 8 Then 'prozent-spalten
                    MyNumberFormat(cell, 1, "%")
                Else
                    roundColumn(cell)
                End If
                cell.Wrap = False
                cell.Width = Unit.Percentage(11)
                i += 1
            Next
        End If
    End Sub
    Private Sub myGridYear_ItemCreated(ByVal source As Object, ByVal e As DataGridItemEventArgs) Handles MyGridYear.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            Dim i As Integer = 0
            For Each cell As TableCell In e.Item.Cells
                If i = 2 Or i = 5 Then  'prozent-spalten
                    MyNumberFormat(cell, 1, "%")
                Else
                    roundColumn(cell)
                End If
                cell.Wrap = False
                cell.Width = Unit.Percentage(11)
                i += 1
            Next

        End If
    End Sub

    Private Sub BindData()
        Dim myDataView As New DataView
        Dim myReport As Report = New Report
        myReport.StartDate = repData.lastOrderDate
        myReport.WorkDaysMonth = repData.workDaysOfMonthTotal
        myReport.WorkDaysToday = repData.workDaysOfMonth
        myReport.BudgetType = "BU"
        myDataView = myReport.GetDailySalesTotal
        With myGrid
            Dim cel As DataGridColumn
            For Each cel In .Columns
                cel.ItemStyle.Width = Unit.Percentage(16)
                cel.ItemStyle.HorizontalAlign = HorizontalAlign.Right
                cel.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                cel.ItemStyle.BackColor = Color.White
                cel.ItemStyle.BorderColor = Color.Gray
                cel.ItemStyle.BorderWidth = Unit.Pixel(1)
            Next

            .CellPadding = 2
            .CellSpacing = 0
            .Width = Unit.Percentage(100)
            .HeaderStyle.CssClass = "headCommon"
            .DataSource = myDataView
            .DataBind()
        End With

        With MyGridYear
            Dim cel As DataGridColumn
            For Each cel In .Columns
                cel.ItemStyle.Width = Unit.Percentage(16)
                cel.ItemStyle.HorizontalAlign = HorizontalAlign.Right
                cel.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                cel.ItemStyle.BackColor = Color.White
                cel.ItemStyle.BorderColor = Color.Gray
                cel.ItemStyle.BorderWidth = Unit.Pixel(1)
            Next

            .CellPadding = 2
            .CellSpacing = 0
            .Width = Unit.Percentage(100)
            .HeaderStyle.CssClass = "headCommon"
            .DataSource = myDataView
            .DataBind()
        End With

        showImportStatus()
        'Change Request 07-AT-0005 
        '    showMunsterImport()

        DataBind()
    End Sub

    Private Sub showImportStatus()
        Dim MyStatus As New AlfStatus
        Dim MyDataView, MyLatestImportView, MyMissingTransmissions As DataView
        Dim str As String
        Dim Errdesc, Errdesc_muenster As String
        Dim Users As Collection
        Dim numberOfOccuredErrors As Integer = 0
        Dim numberOfStockOccuredErrors As Integer = 0
        Dim numberOfImportNewProducts As Integer = 0
        Dim numberOfImportNewCustomers As Integer = 0
        Dim dist_id, dist_id_muenster As Integer

        Dim numberOfOccuredErrorsMuenster As Integer = 0
        Dim numberOfStockOccuredErrorsMuenster As Integer = 0



        Try

            MyStatus.CtryID = Session("country_id")
            MyStatus.LogsSource = "PKG_IMPORT_SANOVA.F_Bewegungs_Daten"

          
            ' start to retrieve Sanova Import Errors
            dist_id = MyImport.GetDistributorID("Sanova")
            MyStatus.CtryID = MyStatus.GetCountrID("AT")
            numberOfOccuredErrors = MyStatus.GetNumberOfImportErrors()
            numberOfImportNewProducts = MyStatus.GetNumberOfImportNewProducts
            numberOfImportNewCustomers = MyStatus.GetNumberOfImportNewCustomers
            MyLatestImportView = MyStatus.GetLatestImport()
            MyDataView = MyStatus.GetLatestImportError()

            Tran_date = Convert.ToDateTime(MyLatestImportView.Item(0).Item(2)).ToString(DATEFORMAT_STRING_REPORT)
            tran_id = MyLatestImportView.Item(0).Item(3)
            import_date = MyDataView.Item(0).Item(1)
            Errdesc = MyDataView.Item(0).Item(0)


            MyStatus.LogsSource = "Stock Check"
            numberOfStockOccuredErrors = MyStatus.GetNumberOfStockErrors

           


            'show alfservice status for admins
            If Me.ALFPageAccessRights = AlfPage.Rights.Admin Then

                Dim myRow As New HtmlTableRow
                Dim MyCellName As New HtmlTableCell
                Dim MyCellValue As New HtmlTableCell
                Dim strStatus, strcolor As String

                strStatus = myService.GetStatus()
                MyCellName.InnerHtml = "Alf-Service Status:"
                MyCellName.Attributes.Add("class", "field")

                myRow.Attributes.Add("class", "tableBGColor1Class")
                myRow.Style.Add("padding", "2px")
                myRow.Style.Add("font-size", "smaller")
                myRow.Cells.Add(MyCellName)
                myRow.Cells.Add(MyCellValue)

                Me.statusTable.Rows.Insert(1, (myRow))
                If strStatus.ToUpper <> "RUNNING" Then
                    strcolor = "red"
                    formatStatusTableRows(1)
                Else
                    strcolor = "green"
                End If
                MyCellValue.InnerHtml = "<font color=" & strcolor & ">" & myService.GetStatus() & "</Font>"

            End If



            ' display Import Errorors for transmission SAnova
            If numberOfOccuredErrors > 0 Then
                Dim ahrefControl As HtmlAnchor = New HtmlAnchor
                With ahrefControl
                    .HRef = "javascript:OpenPopUp('admin/import/importErrors.aspx?dist_id=" & dist_id & "','ImportErrors');"
                    .InnerText = numberOfOccuredErrors & " error(s) occured"
                End With
                phErrors.Controls.Add(ahrefControl)

                If Me.ALFPageAccessRights = AlfPage.Rights.Admin Then
                    formatStatusTableRows(2)
                Else
                    formatStatusTableRows(1)
                End If

            ElseIf numberOfOccuredErrors = 0 Then
                Dim lblErrdesc As New Label
                With lblErrdesc
                    .Text = Errdesc
                End With
                phErrors.Controls.Add(lblErrdesc)
            End If

            Try

                ' display stock errors
                If numberOfStockOccuredErrors > 0 Then
                    Dim stockErrorDescription As String = MyStatus.GetStockErrorsDetails()
                    Dim stockerrLine As String()


                    Dim errD As Literal = New Literal
                    stockerrLine = stockErrorDescription.Split("$")

                    For Each element As String In stockerrLine
                        Dim ahrefControl As HtmlAnchor = New HtmlAnchor
                        Dim strPage, prod_id, line_id As String

                        div = New HtmlGenericControl("div")
                        div.Style.Add("padding", "2px")
                        div.Style.Add("font-size", "smaller")

                        prod_id = element.Substring(element.IndexOf("|") + 1, element.LastIndexOf("|") - element.IndexOf("|") - 1)
                        line_id = element.Substring(element.LastIndexOf("|") + 1)
                        strPage = "wes/StockMovement.aspx?PageTitle=Stock Report&prod_id=" & prod_id & "&line_id=" & line_id & "&dist_id=" & dist_id

                        With ahrefControl
                            .Target = "main"
                            .HRef = strPage
                            .InnerHtml = element.Substring(0, element.IndexOf("|"))
                        End With
                        div.Controls.Add(ahrefControl)
                        phstockerrors.Controls.Add(div)
                    Next

                Else
                    Dim lblErrdesc2 As New Label
                    With lblErrdesc2
                        .Text = "0 error(s) occured"
                    End With
                    phstockerrors.Controls.Add(lblErrdesc2)
                End If


            Catch ex As Exception
                ExceptionInfo.Show(ex)
            End Try

            ' display new products
            If numberOfImportNewProducts > 0 Then


                MyDataView = MyStatus.GetLatestImportNewProducts

                For i As Integer = 0 To MyDataView.Count - 1
                    div = New HtmlGenericControl("div")
                    div.Style.Add("padding", "2px")
                    div.Style.Add("font-size", "smaller")
                    Dim ahrefControl As HtmlAnchor = New HtmlAnchor
                    Dim txt, phznr, strPage As String
                    txt = MyDataView(i).Item(0)
                    phznr = txt.Substring((txt.IndexOf("ProductNo:") + 10), 6)
                    strPage = "admin/ProductById.aspx?phznr=" & phznr
                    MyProductPopUp.PageURL = strPage

                    With ahrefControl
                        .HRef = MyProductPopUp.GetJSFunctionCall()
                        .InnerText = txt
                    End With

                    div.Controls.Add(ahrefControl)
                    phNewProducts.Controls.Add(div)
                Next

            ElseIf numberOfImportNewProducts = 0 Then

                Dim lblErrdesc As New Label
                With lblErrdesc
                    .Text = "No new Products have been inserted"
                End With
                phNewProducts.Controls.Add(lblErrdesc)



            End If


            'display new customers
            If numberOfImportNewCustomers > 0 Then

                MyDataView = MyStatus.GetLatestImportNewCustomers

                For i As Integer = 0 To MyDataView.Count - 1
                    Dim ahrefControl As HtmlAnchor = New HtmlAnchor
                    Dim txt, cudi_nr, strPage As String
                    div = New HtmlGenericControl("div")
                    div.Style.Add("padding", "2px")
                    div.Style.Add("font-size", "smaller")
                    txt = MyDataView(i).Item(0)
                    cudi_nr = txt.Substring(txt.IndexOf("CustNo") + 7, 5)
                    strPage = "admin/ViewCustomerByID.aspx?cudi_nr=" & cudi_nr
                    MyCustomerPopUp.PageURL = strPage
                    MyCustomerPopUp.Title = "Customer Details"
                    With ahrefControl
                        .HRef = MyCustomerPopUp.GetJSFunctionCall()
                        .InnerText = txt
                    End With
                    div.Controls.Add(ahrefControl)
                    phNewCustomers.Controls.Add(div)
                Next

            ElseIf numberOfImportNewCustomers = 0 Then

                Dim lblErrdesc As New Label
                With lblErrdesc
                    .Text = "No new Customers have been inserted"
                End With
                phNewCustomers.Controls.Add(lblErrdesc)

            End If
            'display not Imported transmissions Muenster
            'display Not Imported Transmisions
            MyMissingTransmissions = MyStatus.NotImportedTransmissions()

            div = New HtmlGenericControl("div")
            div.Style.Add("padding", "2px")
            div.Style.Add("font-size", "smaller")

            If MyMissingTransmissions.Count > 0 Then
                For Each row As DataRow In MyMissingTransmissions.Table.Rows
                    div.InnerHtml = div.InnerHtml & row.Item(0) & ", "
                    div.Style.Add("color", "red")
                Next
                div.InnerHtml = div.InnerHtml.Remove(div.InnerHtml.Length - 2, 1)
                div.InnerHtml = "<Blink>" & div.InnerHtml & "</Blink>"

                If Me.ALFPageAccessRights = AlfPage.Rights.Admin Then
                    formatStatusTableRows(3)
                Else
                    formatStatusTableRows(2)
                End If

            Else
                div.InnerHtml = "All Transmissions where imported successfully!"
            End If
            phNotImportedTransmisisons.Controls.Add(div)

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        End Try

        showAPOExportStatus()

    End Sub

    Private Sub showAPOExportStatus()


        Dim myStatus As New AlfStatus
        Dim NoOfexpectedExports, NoOfExportsDone As Integer

        lastExportDate = myStatus.getLastExportDate(Today())

        NoOfexpectedExports = myStatus.getNumberofExpectedExports(Convert.ToDateTime(lastExportDate).DayOfWeek)
        NoOfExportsDone = myStatus.getNumberofExportsDone(Convert.ToDateTime(lastExportDate))

        If NoOfexpectedExports <> NoOfExportsDone Then
            lastExportDate = lastExportDate & "<font color = red>" & "  " & NoOfExportsDone & " of " & NoOfexpectedExports & " files exported </font>"
        Else
            lastExportDate = lastExportDate & "<font color = green>" & "  " & NoOfExportsDone & " of " & NoOfexpectedExports & " files exported</font>"
        End If

        If Me.ALFPageAccessRights = AlfPage.Rights.Admin Then

            Dim myServicestatus As New ServiceControl("ALFExportservice")
            Dim myRow As New HtmlTableRow
            Dim MyCellName As New HtmlTableCell
            Dim MyCellValue As New HtmlTableCell
            Dim strStatus, strcolor As String

            strStatus = myServicestatus.GetStatus()
            MyCellName.InnerHtml = "APO Export Service Status:"
            MyCellName.Attributes.Add("class", "field")

            myRow.Attributes.Add("class", "tableBGColor1Class")
            myRow.Style.Add("padding", "2px")
            myRow.Style.Add("font-size", "smaller")
            myRow.Cells.Add(MyCellName)
            myRow.Cells.Add(MyCellValue)

            Me.exportStatusTable.Rows.Insert(1, (myRow))
            If strStatus.ToUpper <> "RUNNING" Then
                strcolor = "red"
                formatExportStatusTableRows(1)
            Else
                strcolor = "green"
            End If
            MyCellValue.InnerHtml = "<font color=" & strcolor & ">" & myServicestatus.GetStatus() & "</Font>"

        End If
    End Sub

    Private Sub showMunsterImport()

        Dim MyStatus As New AlfStatus
        Dim MyDataView, MyLatestImportView, MyMissingTransmissions As DataView
        Dim str As String
        Dim Errdesc, Errdesc_muenster As String
        Dim dist_id_muenster As Integer

        Dim numberOfOccuredErrorsMuenster As Integer = 0
        Dim numberOfStockOccuredErrorsMuenster As Integer = 0

        ' start to retrieve Münster Import Errors
        MyStatus.LogsSource = "PKG_IMPORT_SANOVA.F_Bewegungs_Daten"
        MyStatus.CtryID = MyStatus.GetCountrID("MUE")
        dist_id_muenster = MyImport.GetDistributorID("Münster")
        MyLatestImportView = MyStatus.GetLatestImport()
        MyDataView = MyStatus.GetLatestImportError()
        numberOfOccuredErrorsMuenster = MyStatus.GetNumberOfImportErrors()

        Try
            Tran_date_muenster = Convert.ToDateTime(MyLatestImportView.Item(0).Item(2)).ToString(DATEFORMAT_STRING_REPORT)
            tran_id_muenster = MyLatestImportView.Item(0).Item(3)
            import_date_muenster = MyDataView.Item(0).Item(1)


            Errdesc_muenster = MyDataView.Item(0).Item(0)


        Catch ex As Exception

        End Try


        ' display Import Errorors for transmission Muenster
        If numberOfOccuredErrorsMuenster > 0 Then
            Dim ahrefControl As HtmlAnchor = New HtmlAnchor
            With ahrefControl
                .HRef = "javascript:OpenPopUp('admin/import/importErrors.aspx?dist_id_muenster=" & dist_id_muenster & "','ImportErrors');"
                .InnerText = numberOfOccuredErrorsMuenster & " error(s) occured"
            End With
            phErrorsMuenster.Controls.Add(ahrefControl)

            'If Me.ALFPageAccessRights = AlfPage.Rights.Admin Then
            '    formatStatusTableRowsMue(1)
            'Else
            '    ' formatStatusTableRowsMue(0)
            'End If

        ElseIf numberOfOccuredErrorsMuenster = 0 Then
            Dim lblErrdesc As New Label
            With lblErrdesc
                .Text = Errdesc_muenster
            End With
            phErrorsMuenster.Controls.Add(lblErrdesc)
        End If

        MyStatus.LogsSource = "Stock Check"
        numberOfStockOccuredErrorsMuenster = MyStatus.GetNumberOfStockErrors
        ' display stock errors Muenster
        If numberOfStockOccuredErrorsMuenster > 0 Then
            Dim stockErrorDescription As String = MyStatus.GetStockErrorsDetails()
            Dim stockerrLine As String()


            Dim errD As Literal = New Literal
            stockerrLine = stockErrorDescription.Split("$")

            For Each element As String In stockerrLine
                Dim ahrefControl As HtmlAnchor = New HtmlAnchor
                Dim strPage, prod_id, line_id As String

                div = New HtmlGenericControl("div")
                div.Style.Add("padding", "2px")
                div.Style.Add("font-size", "smaller")

                prod_id = element.Substring(element.IndexOf("|") + 1, element.LastIndexOf("|") - element.IndexOf("|") - 1)
                line_id = element.Substring(element.LastIndexOf("|") + 1)
                strPage = "wes/StockMovement.aspx?PageTitle=Stock Report&prod_id=" & prod_id & "&line_id=" & line_id & "&dist_id=" & dist_id_muenster

                With ahrefControl
                    .Target = "main"
                    .HRef = strPage
                    .InnerHtml = element.Substring(0, element.IndexOf("|"))
                End With
                div.Controls.Add(ahrefControl)
                phstockerrorsMuenster.Controls.Add(div)
            Next

        Else
            Dim lblErrdesc2 As New Label
            With lblErrdesc2
                .Text = "0 error(s) occured"
            End With
            phstockerrorsMuenster.Controls.Add(lblErrdesc2)
        End If


        MyMissingTransmissions = MyStatus.NotImportedTransmissionsMue

        div = New HtmlGenericControl("div")
        div.Style.Add("padding", "2px")
        div.Style.Add("font-size", "smaller")

        If MyMissingTransmissions.Count > 0 Then
            For Each row As DataRow In MyMissingTransmissions.Table.Rows
                div.InnerHtml = div.InnerHtml & row.Item(0) & ", "
                div.Style.Add("color", "red")
            Next
            div.InnerHtml = div.InnerHtml.Remove(div.InnerHtml.Length - 2, 1)
            div.InnerHtml = "<Blink>" & div.InnerHtml & "</Blink>"

            'If Me.ALFPageAccessRights = AlfPage.Rights.Admin Then
            '    formatStatusTableRowsMue(3)
            'Else
            '    formatStatusTableRowsMue(2)
            'End If

        Else
            div.InnerHtml = "All Transmissions where imported successfully!"
        End If
        phNotImportedTransmisisonsMuenster.Controls.Add(div)
    End Sub

    Private Sub formatStatusTableRows(ByVal rownum As Integer)
        Try
            For Each cell As HtmlTableCell In Me.statusTable.Rows(rownum).Cells()
                cell.Attributes.Add("style", "border-top:solid 2px #FF0000; border-bottom:solid 2px #FF0000; color:#ff0000;")
            Next

            '   Me.statusTable.Rows(rownum).Cells(1).InnerHtml = "<strong>WARNING!</strong>" & mycontrol.Text
            Me.statusTable.Rows(rownum).Cells(0).Attributes.Add("style", "border-top:solid 2px #FF0000; border-bottom:solid 2px #FF0000;  border-left:solid 2px #FF0000; color:#ff0000;")
            Me.statusTable.Rows(rownum).Cells(1).Attributes.Add("style", "border-top:solid 2px #FF0000; border-bottom:solid 2px #FF0000;  border-right:solid 2px #FF0000; color:#ff0000;")
        Catch ex As Exception


        End Try

    End Sub

    Private Sub formatExportStatusTableRows(ByVal rownum As Integer)
        Try
            For Each cell As HtmlTableCell In Me.exportStatusTable.Rows(rownum).Cells()
                cell.Attributes.Add("style", "border-top:solid 2px #FF0000; border-bottom:solid 2px #FF0000; color:#ff0000;")
            Next

            Me.statusTable.Rows(rownum).Cells(0).Attributes.Add("style", "border-top:solid 2px #FF0000; border-bottom:solid 2px #FF0000;  border-left:solid 2px #FF0000; color:#ff0000;")
            Me.statusTable.Rows(rownum).Cells(1).Attributes.Add("style", "border-top:solid 2px #FF0000; border-bottom:solid 2px #FF0000;  border-right:solid 2px #FF0000; color:#ff0000;")

        Catch ex As Exception

        End Try

    End Sub

    Private Sub formatStatusTableRowsMue(ByVal rownum As Integer)
        Try
            For Each cell As HtmlTableCell In Me.statusTableMuenster.Rows(rownum).Cells()
                cell.Attributes.Add("style", "border-top:solid 2px #FF0000; border-bottom:solid 2px #FF0000; color:#ff0000;")
            Next

            '   Me.statusTable.Rows(rownum).Cells(1).InnerHtml = "<strong>WARNING!</strong>" & mycontrol.Text
            Me.statusTableMuenster.Rows(rownum).Cells(0).Attributes.Add("style", "border-top:solid 2px #FF0000; border-bottom:solid 2px #FF0000;  border-left:solid 2px #FF0000; color:#ff0000;")
            Me.statusTableMuenster.Rows(rownum).Cells(1).Attributes.Add("style", "border-top:solid 2px #FF0000; border-bottom:solid 2px #FF0000;  border-right:solid 2px #FF0000; color:#ff0000;")
        Catch ex As Exception


        End Try

    End Sub

    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Unload
        Dim MyStatus As New AlfStatus
        Application.Lock()
        Application("Status") = MyStatus.GetALFStatus()
        ' Holt datum des Letzetn Order Entry
        Application("LastOrderEntry") = Convert.ToDateTime(getLastOrderEntry(Today()))
        Application.Lock()
    End Sub
End Class
