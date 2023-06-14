Imports Wyeth.Utilities.Helper
Imports Wyeth.Alf.CssStyles

Public Class WebForm1
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btn_process_invoices As System.Web.UI.WebControls.Button
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents lblProsessSuccess As System.Web.UI.WebControls.Label
    Protected WithEvents btn_print As System.Web.UI.WebControls.Button
    Protected WithEvents panelSuccess As System.Web.UI.WebControls.Panel
    Protected WithEvents MyGrid As C1.Web.C1WebGrid.C1WebGrid
    Protected WithEvents lblTcogs As System.Web.UI.WebControls.Label
    Protected WithEvents prtControl As Wyeth.Alf.printReportCtl

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim MyMep As New MEPData
    Dim MyStock As New Stock

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        Dim strScript As String
        strScript = "<script language =javascript >"
        strScript += "self.focus();"
        strScript += "</script>"
        RegisterClientScriptBlock("anything", strScript)

        btn_process_invoices.Attributes.Add("OnClick", "javascript:return getconfirmMEC();")
        lblPageTitle.Text = "Month End Processing Confirmation"

        BindData()

    End Sub
    Private Sub BindData()
        If MyMep.CheckForFinanceApproval(Convert.ToDateTime(Session("LastProcessMonth")), Session("country_id")) = True Then
            panelSuccess.Visible = True
            lblProsessSuccess.CssClass = "nosuccess"
            lblProsessSuccess.Text = "Month " & Convert.ToString(Session("CurrentProcessMonth")).Substring(0, 7) & " cannot be processed. Previous month has not yet been approved by Finance."
            btn_process_invoices.Visible = False
            MyGrid.Visible = False
        Else

            SetGridStylesGroup(MyGrid)
            MyGrid.DataSource = MyMep.CheckTCOGSForProcessing(Session("CurrentProcessMonth"), Session("country_id"))
            MyGrid.DataBind()
            If MyGrid.Items.Count = 0 Then
                MyGrid.Visible = False
                lblTcogs.Visible = False
            Else
                MyGrid.Visible = True
                lblTcogs.Visible = True
                lblTcogs.Text = "The following GITs and WEs do not match with their TCogs Prices"
                lblTcogs.CssClass = "nosuccess"
            End If
        End If

    End Sub
    Private Sub btn_process_invoices_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_process_invoices.Click

        If MyMep.ProcessInvoices(Session("CurrentProcessMonth"), Session("country_id")) = True Then
            panelSuccess.Visible = True
            lblProsessSuccess.CssClass = "success"
            lblProsessSuccess.Text = "Month <Font Color=red>" & Convert.ToString(Session("CurrentProcessMonth")).Substring(0, 7) & "</font> successfully completed"
            MyMep.SetLogisticsClosingUserID(Session("CurrentProcessMonth"), Session("user_id"), Session("country_id"))
            MyMep.setProcessMonth()
        Else
            panelSuccess.Visible = True
            lblProsessSuccess.CssClass = "nosuccess"
            lblProsessSuccess.Text = "The Month <Font Color=red>" & Convert.ToString(Session("CurrentProcessMonth")).Substring(0, 7) & " </font>has been processed - but Errors occured while Processsing. "
            MyMep.setProcessMonth()
        End If

        Dim strScript As String
        strScript = "<script language =javascript >"
        strScript += " window.opener.Form1.btn_process_invoices.visible=false;"
        strScript += "</script>"
        RegisterClientScriptBlock("anything", strScript)

    End Sub
End Class
