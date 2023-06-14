Imports System
Imports System.Data
Imports System.Math
Imports System.Globalization
Imports Wyeth.Utilities.Helper
Imports Wyeth.Utilities
Imports Wyeth.Utilities.NumberFormat
Imports Wyeth.Utilities.DateHandling
Imports Wyeth.Alf.CssStyles
Imports Wyeth.Alf.WyethDropdown

Public Class APO_BCountry1
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents txtStartDate As Wyeth.Utilities.WyethTextBox
    Protected WithEvents StartImage As System.Web.UI.WebControls.Image
    Protected WithEvents btnGenRep As System.Web.UI.WebControls.Button
    Protected WithEvents ExportExcel As System.Web.UI.WebControls.Button
    Protected WithEvents FilterPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents repData As Wyeth.Alf.reportData
    Protected WithEvents prtControl As printReportCtl


    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Private myPopup As New JSPopUp(Me.page)
    Protected WithEvents BUTTON1 As System.Web.UI.WebControls.Button
    Dim ZFCST_QTY_CDF, ZFCST_QTY_SAMPLES, ZFCST_QTY_ARID, ZFCST_QTY_ADHOC, ZFCST_QTY_UPSIDE As Integer


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Page.IsPostBack Then
        Else
            '  Me.lblPageTitle.Text = Request.QueryString("PageTitle")
            Me.txtStartDate.Attributes.Add("onkeydown", "return submitButton(btnGenRep);")

            BindData()



        End If

    End Sub

    Private Sub BindData()

        Dim myReport As New Report
        Dim dat As Date = Date.Today
        myPopup.PageURL = "../../util/datepicker.aspx"
        myPopup.AddDatePopupToControl(txtStartDate, StartImage)
        If Me.txtStartDate.Text = "" Then
            Me.txtStartDate.Text = CDate(Application("LastOrderEntry")).ToString("yyyy-MM-dd", GetMyDTFI())
        End If
        repData.EnableViewState = True
        repData.lastOrderDate = txtStartDate.Text
        repData.addLine("Report for Day", txtStartDate.Text, True, False)


        'fill the datagrid with the data 
        'Dim MyDTFI As DateTimeFormatInfo = New CultureInfo("de-AT", False).DateTimeFormat
        myReport.StartDate = Convert.ToDateTime(txtStartDate.Text)

        SetGridStyles(MyGrid)

        Me.MyGrid.EnableViewState = True
        Me.MyGrid.DataSource = myReport.GetAPOB_Countries()
        Me.MyGrid.DataBind()


    End Sub
    Private Sub Item_DataBound(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemDataBound
        ZFCST_QTY_CDF = ZFCST_QTY_CDF + CInt(e.Item.Cells(4).Text)
        ZFCST_QTY_SAMPLES = ZFCST_QTY_SAMPLES + CInt(e.Item.Cells(6).Text)
        ZFCST_QTY_ARID = ZFCST_QTY_ARID + CInt(e.Item.Cells(7).Text)
        ZFCST_QTY_ADHOC = ZFCST_QTY_ADHOC + CInt(e.Item.Cells(8).Text)
        ZFCST_QTY_UPSIDE = ZFCST_QTY_UPSIDE + CInt(e.Item.Cells(9).Text)

    End Sub
    Private Sub Item_Created(ByVal sender As Object, ByVal e As C1.Web.C1WebGrid.C1ItemEventArgs) Handles MyGrid.ItemCreated
        If e.Item.ItemType = C1.Web.C1WebGrid.C1ListItemType.Footer Then
            e.Item.Cells(4).Text = ZFCST_QTY_CDF
            e.Item.Cells(6).Text = ZFCST_QTY_SAMPLES
            e.Item.Cells(7).Text = ZFCST_QTY_ARID
            e.Item.Cells(8).Text = ZFCST_QTY_ADHOC
            e.Item.Cells(9).Text = ZFCST_QTY_UPSIDE

            ZFCST_QTY_CDF = 0
            ZFCST_QTY_SAMPLES = 0
            ZFCST_QTY_ARID = 0
            ZFCST_QTY_ADHOC = 0
            ZFCST_QTY_UPSIDE = 0

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

    Private Sub BUTTON1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUTTON1.Click

        Validate()
        If Page.IsValid = True Then
            Dim filename, strout As String
            Dim myExport As New Exporter("APO B Country")
            myExport.StartDate = Me.txtStartDate.Text
            filename = myExport.ExportFileName
            strout = myExport.DownloadFile()
            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" & filename)
            Response.ContentType = "Text/xls"
            Response.Buffer = True
            Response.Write(strout)
            Response.Flush()
            Response.End()

        End If

    End Sub
End Class