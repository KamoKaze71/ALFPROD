Imports System.Globalization
Imports Wyeth.Utilities.Helper
Imports Wyeth.Utilities
Imports Wyeth.Utilities.NumberFormat
Imports Wyeth.Utilities.DateHandling
Imports Wyeth.Alf.CssStyles
Imports Wyeth.Alf.WyethDropdown

Public Class ProductAvalability
    Inherits Wyeth.Alf.AlfPage


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents txtStartDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents StartImage As System.Web.UI.WebControls.Image
    Protected WithEvents txtEnddate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents EndImage As System.Web.UI.WebControls.Image
    Protected WithEvents btnGenRep As System.Web.UI.WebControls.Button
    Protected WithEvents BUTTON1 As System.Web.UI.WebControls.Button
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents repData As Wyeth.Alf.reportData
    Protected WithEvents prtControl As printReportCtl
    Protected WithEvents ddLine As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim linesOrdered, linesCompleted, unitsOrdered, UnitsCompleted As Integer
    Dim myPopupStart As New JSPopUp(Me)
    Dim myPopupEnd As New JSPopUp(Me)


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Page.IsPostBack Then
        Else

            Me.txtStartDate.Attributes.Add("onkeydown", "return submitButton(btnGenRep);")
            GetLineSelectDD(Me.ddLine, Session("country_id"))

            BindData()

        End If


    End Sub

    Private Sub BindData()

        Dim myReport As New Report
        Dim dat As Date = Date.Today
        myPopupStart.PageURL = "../util/datepicker.aspx"
        myPopupStart.AddDatePopupToControl(txtStartDate, StartImage)
        If Me.txtStartDate.Text = "" Then
            txtStartDate.Text = FirstOfThisMonth(Today().AddMonths(-1)).ToString("yyyy-MM-dd", GetMyDTFI())
            txtEnddate.Text = LastOfThisMonth(Today().AddMonths(-1)).ToString("yyyy-MM-dd", GetMyDTFI())


        End If


        Dim datend As Date = Date.Today
        myPopupEnd.PageURL = "../util/datepicker.aspx"
        myPopupEnd.AddDatePopupToControl(txtEnddate, EndImage)
   
        repData.EnableViewState = True
        repData.lastOrderDate = txtStartDate.Text
        repData.addLine("Report for Day", txtStartDate.Text, True, False)

        myReport.StartDate = Convert.ToDateTime(txtStartDate.Text)
        myReport.EndDate = Convert.ToDateTime(txtEnddate.Text)
        myReport.LineID = ddLine.SelectedValue

        SetGridStyles(MyGrid)

        Me.MyGrid.EnableViewState = True
        Me.MyGrid.DataSource = myReport.GetProductAvalabilityReport()
        Me.MyGrid.DataBind()

    End Sub
    Private Sub Item_DataBound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound
        Me.linesCompleted = linesCompleted + CInt(e.Item.Cells(2).Text)
        Me.linesOrdered = Me.linesOrdered + CInt(e.Item.Cells(1).Text)
        Me.UnitsCompleted = Me.UnitsCompleted + CInt(e.Item.Cells(4).Text)
        Me.unitsOrdered = Me.unitsOrdered + CInt(e.Item.Cells(3).Text)

    End Sub
    Private Sub Item_Created(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated
        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Footer Then
            e.Item.Cells(0).Text = "Total:"

            e.Item.Cells(3).Text = Me.unitsOrdered
            e.Item.Cells(4).Text = Me.UnitsCompleted
            e.Item.Cells(1).Text = Me.linesOrdered
            e.Item.Cells(2).Text = Me.linesCompleted

            Me.unitsOrdered = 0
            Me.UnitsCompleted = 0
            Me.linesOrdered = 0
            Me.linesCompleted = 0

        End If
    End Sub


    Private Sub btnGenRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRep.Click
        Validate()
        If Page.IsValid = True Then
            MyGrid.Visible = True
            BindData()
        Else
            MyGrid.Visible = False
        End If
    End Sub


    Private Sub ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportExcel.Click
        BindData()

        Dim exp As exportToExcel = New exportToExcel(Page)
        exp.title = Me.ALFPageTitle
        exp.showReportData = repData
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

        Dim preview As New printReportUtil

        preview.PageTitle = Me.ALFPageTitle
        preview.AddReportHeader(repData)
        preview.AddWebGrid(MyGrid)

        Return preview
    End Function
    '**************************************************************************************************************
    '* End Printing
    '**************************************************************************************************************

End Class
