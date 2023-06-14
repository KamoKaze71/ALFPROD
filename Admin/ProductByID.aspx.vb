Imports Oracle.DataAccess.Client
Imports Wyeth.Alf.WyethDropdown
Imports Wyeth.Utilities

Public Class ProductByID
    Inherits Wyeth.Alf.AlfPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Cancel As System.Web.UI.WebControls.Button
    Protected WithEvents Button_update As System.Web.UI.WebControls.Button
    Protected WithEvents txtUnitMeasure As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddProdTaPG As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btn_TcogsHistory As System.Web.UI.WebControls.Button
    Protected WithEvents txtProdFAP As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddLineId As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddProdCurrId As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddProdCtryId As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddProdIdSampleProd As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddProdGroup As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtProdObsCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtProdInfo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtProdMfGLocation As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtProdManufacturer As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtProdRouting As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtProdInvoicerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtProdPackerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtProdPlantItemNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtProdFSD As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtProdWWSItemCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtProdCCDesc As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtProdCCId As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtProdStrength As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtProdPacksize As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtProdDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtProdPresentation As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtProdSegment As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtProdBusUnit As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtProd_PhZnr As System.Web.UI.WebControls.TextBox
    Protected WithEvents EditPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents txtStdCogs As System.Web.UI.WebControls.TextBox
    Protected WithEvents btn_goto_sample As System.Web.UI.WebControls.Button
    Protected btn_cancel As System.Web.UI.WebControls.Button
    Protected WithEvents button_cancel As System.Web.UI.HtmlControls.HtmlButton
    Protected WithEvents _clientScript As System.Web.UI.WebControls.Literal
    Protected WithEvents txtProdStatus As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtProdForteProductId As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtProd_id As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtProdSubsegment As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtprodsampleproduct As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtprod_id_sample_prod As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnObsCode As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim MyProduct As New WyethProduct
    Dim MyJs As New JSPopUp(Me)

    Dim Prod_id, i As Integer
    Dim phznr As String
    Dim referrer, keywords, obs_code As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        keywords = Request.QueryString("keywords")
        obs_code = Request.QueryString("obs_code")

        If Page.IsPostBack = True Then
            Dim strreferr As String
            strreferr = Me.ALFPageReferrer & "?line_id=" & ddLineId.SelectedValue & "&tapg_id=" & ddProdTaPG.SelectedValue & "&obs=" & obs_code & "&keywords=" & keywords
            button_cancel.Attributes.Add("OnClick", "javascript:location.reload('" & strreferr & "');")
            Prod_id = Me.ViewState("prod_id")
        Else
            'Fill DropdownBoxes
            GetProductGroupSelectDD(ddProdGroup, Session("country_id"))
            GetLineSelectDD(ddLineId, Session("country_id"))
            GetCountrySelectDD(ddProdCtryId)
            GetCurrencySelectDD(ddProdCurrId)
            GetNotAssignedSampleProducts(Session("country_id"), ddProdIdSampleProd)
            GetTargetProductGroupSelectDD(ddProdTaPG, Session("country_id"))

            Dim li As New ListItem
            li.Text = "Select a Sample Product"
            li.Value = 0
            ddProdIdSampleProd.Items.Insert(0, li)

            Dim li2 As New ListItem
            li2.Text = "Select a Target Product Group"
            li2.Value = 0
            ddProdTaPG.Items.Insert(0, li2)

            Prod_id = Request.QueryString("prod_id")
            phznr = Request.QueryString("phznr")

            BindData()
            SetPage()

        End If

    End Sub
    Private Sub SetPage()

        If Me.ALFPageAccessRights < AlfPage.Rights.Write Then
            Button_update.Visible = False
        End If

        If IsNothing(Request.UrlReferrer) = False Then
            'referrer = Request.UrlReferrer.LocalPath
            Dim referrer As String
            referrer = Request.UrlReferrer.LocalPath & "?line_id=" & ddLineId.SelectedValue & "&tapg_id=" & ddProdTaPG.SelectedValue & "&obs=" & obs_code & "&keywords=" & keywords

            button_cancel.Attributes.Add("OnClick", "javascript:location.reload('" & referrer & "');")
        Else
            button_cancel.Attributes.Add("OnClick", "javascript:self.close();")
        End If


    End Sub
    Private Sub BindData()

        Dim Conn As New MyConnection
        Dim MyReader As OracleDataReader
        Dim MyCmd As New OracleCommand

        Try
            If Prod_id <> 0 Then
                MyCmd.CommandText = "PKG_PRODUCT.GetProductByID"
                MyCmd.CommandType = CommandType.StoredProcedure
                MyCmd.Parameters.Clear()
                MyCmd.Parameters.Add("Products", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                MyCmd.Parameters.Add("v_prod_ID", OracleDbType.Int32, ParameterDirection.Input).Value = Prod_id
            ElseIf phznr <> "" Then
                MyCmd.CommandText = "PKG_PRODUCT.GetProductByPhznr"
                MyCmd.CommandType = CommandType.StoredProcedure
                MyCmd.Parameters.Clear()
                MyCmd.Parameters.Add("Products", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                MyCmd.Parameters.Add("v_phznr", OracleDbType.Varchar2, ParameterDirection.Input).Value = phznr
            End If


            MyCmd.Connection = Conn.Open()
            MyReader = MyCmd.ExecuteReader()

            While MyReader.Read

                btn_TcogsHistory.Attributes.Add("OnClick", "javascript:OpenPopUp('ProductTCogsHistory.aspx?prod_id=" & MyReader("prod_id").ToString & "&presentation=" & MyReader("prod_presentation").ToString & "');")
                Me.ViewState.Add("prod_id", MyReader("prod_id").ToString)
                txtProd_id.Text = MyReader("prod_id").ToString    ' PROD_ID, 
                txtProd_PhZnr.Text = MyReader("prod_phznr").ToString    ' PHZNR
                txtProdDescription.Text = MyReader("prod_description").ToString    ' DESCRIPTION
                txtProdPresentation.Text = MyReader("prod_presentation").ToString  ' PRESENTATION
                txtProdPacksize.Text = MyReader("prod_packsize").ToString    ' PACKSIZE
                txtProdStrength.Text = MyReader("prod_strength").ToString  ' STRENGTH
                txtProdBusUnit.Text = MyReader("prod_bus_unit").ToString   ' BUSUNIT
                txtProdSegment.Text = MyReader("prod_segment").ToString   ' SEGMENT
                txtProdSubsegment.Text = MyReader("prod_sub_segment").ToString   ' SUBSEGMENT
                txtProdCCId.Text = MyReader("prod_cc_id").ToString  ' CCID
                txtProdCCDesc.Text = MyReader("prod_cc_description").ToString
                txtProdFAP.Text = MyReader("prod_fap").ToString
                txtProdWWSItemCode.Text = MyReader("prod_wws_item_code").ToString
                txtProdFSD.Text = MyReader("prod_fsd").ToString
                txtProdPlantItemNo.Text = MyReader("prod_plant_item_number").ToString
                txtProdManufacturer.Text = MyReader("prod_manufacturer").ToString
                txtProdMfGLocation.Text = MyReader("prod_mfglocation").ToString
                txtProdPackerCode.Text = MyReader("prod_packer_code").ToString
                txtProdInvoicerCode.Text = MyReader("prod_invoicer_code").ToString
                txtProdRouting.Text = MyReader("prod_routing").ToString
                txtProdInfo.Text = MyReader("prod_info").ToString
                txtProdForteProductId.Text = MyReader("prod_forte_product_id").ToString
                txtProdStatus.Text = MyReader("prod_status").ToString
                txtProdObsCode.Text = MyReader("prod_obs_code").ToString
                txtUnitMeasure.Text = MyReader("prod_units_measure").ToString
                txtStdCogs.Text = MyReader("std_cogs").ToString
                ddProdGroup.SelectedValue = MyReader("prgr_id").ToString


                ddProdCtryId.SelectedValue = MyReader("ctry_id").ToString
                ddProdCurrId.SelectedValue = MyReader("curr_id").ToString
                ddLineId.SelectedValue = MyReader("line_id").ToString


                If MyReader("prod_id_sample_product").ToString <> 0 Then
                    ddProdIdSampleProd.SelectedValue = 0
                    btn_goto_sample.Visible = True
                    btn_goto_sample.Text = "Go To Sample Product"
                    txtprodsampleproduct.Text = MyReader("prod_sample_presentation").ToString
                    txtprod_id_sample_prod.Text = MyReader("prod_id_sample_product").ToString
                Else
                    btn_goto_sample.Visible = False

                End If


                If MyReader("tapg_id").ToString = 0 Then
                    ddProdTaPG.SelectedIndex = MyReader("tapg_id").ToString
                Else
                    ddProdTaPG.SelectedValue = MyReader("tapg_id").ToString
                End If


                If ddLineId.SelectedItem.Text.ToUpper = "ACTUALS" Or ddLineId.SelectedItem.Text.ToUpper = "SAMPLES" Then
                    btnObsCode.Visible = False

                Else
                    btnObsCode.Visible = True


                    If txtProdObsCode.Text.ToUpper <> "OBS" Then
                        btnObsCode.Text = "Click here to set product obsolete"
                    Else
                        btnObsCode.Text = "Click here to set product active"
                    End If

                End If
                If ddLineId.SelectedItem.Text.ToUpper = "ACTUALS" Then
                    ddProdIdSampleProd.Enabled = True
                    ddProdIdSampleProd.Visible = True
                Else
                    ddProdIdSampleProd.Visible = False
                    txtprod_id_sample_prod.Enabled = False
                End If
            End While

        Catch ex As Exception
            ExceptionInfo.Show(ex)
        Finally
            Conn.Close()
        End Try
    End Sub

    Private Sub Button_update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_update.Click
        setvalues()



        If MyProduct.update() Then
            If Me.ALFPageReferrer <> "" Then
                ' Response.Redirect(Me.ALFPageReferrer, True)
                Dim strReferr As String
                strReferr = Me.ALFPageReferrer & "?line_id=" & ddLineId.SelectedValue & "&tapg_id=" & ddProdTaPG.SelectedValue & "&obs=" & obs_code & "&keywords=" & keywords
                ' Server.Transfer(strReferr, False)
                Response.Redirect(strReferr, True)
            Else
                MyJs.ClosePopUp()
            End If

        Else
            Dim strScript As String
            strScript = "<script language =javascript >"
            strScript += "window.open('../error.aspx?ErrorId=2','Error','width=300,height=250,left=270,top=180');"
            strScript += "</script>"
            RegisterClientScriptBlock("anything", strScript)
        End If
    End Sub
    Private Sub setvalues()
        MyProduct.ProdID = txtProd_id.Text.Trim()
        MyProduct.PhzNr = txtProd_PhZnr.Text.Trim()
        MyProduct.ProdDescription = txtProdDescription.Text.Trim()
        MyProduct.ProdPresentation = txtProdPresentation.Text.Trim()
        MyProduct.ProdPackSize = txtProdPacksize.Text.Trim()
        MyProduct.ProdStrength = txtProdStrength.Text.Trim()
        MyProduct.ProdBusUnit = txtProdBusUnit.Text.Trim()
        MyProduct.ProdSegment = txtProdSegment.Text.Trim()
        MyProduct.ProdSubSegment = txtProdSubsegment.Text.Trim()
        MyProduct.ProdCCID = txtProdCCId.Text.Trim()
        MyProduct.ProdCCDescription = txtProdCCDesc.Text.Trim()
        MyProduct.ProdFAP = CDbl(txtProdFAP.Text.Trim())
        MyProduct.ProdItemCode = txtProdWWSItemCode.Text.Trim()
        MyProduct.ProdmFSD = txtProdFSD.Text.Trim()
        MyProduct.ProdPlantItemNo = txtProdPlantItemNo.Text.Trim()
        MyProduct.ProdPackerCode = txtProdPackerCode.Text.Trim()
        MyProduct.ProdInvoicerCode = txtProdInvoicerCode.Text.Trim()
        MyProduct.ProdUnitMeasure = txtUnitMeasure.Text.Trim()
        MyProduct.ProdInfo = txtProdInfo.Text.Trim()
        MyProduct.ProdForteProdID = txtProdForteProductId.Text.Trim()
        MyProduct.ProdStatus = txtProdStatus.Text.Trim()
        MyProduct.ObsCode = txtProdObsCode.Text.Trim()
        MyProduct.ProdProdIDSample = ddProdIdSampleProd.SelectedValue

        MyProduct.ProdProductGroupID = ddProdGroup.SelectedValue
        MyProduct.ProdCtryId = ddProdCtryId.SelectedValue()
        MyProduct.ProdCurrId = ddProdCurrId.SelectedValue()
        MyProduct.ProdLineId = ddLineId.SelectedValue()
        MyProduct.ProdTargetProductGroupId = ddProdTaPG.SelectedValue()

    End Sub
    Private Sub btn_goto_sample_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_goto_sample.Click
        Server.Transfer("ProductByID.aspx?prod_id=" & txtprod_id_sample_prod.Text, True)
    End Sub

    Private Sub btnObsCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnObsCode.Click
        If txtProdObsCode.Text.ToUpper <> "OBS" Then
            txtProdObsCode.Text = "OBS"
            btnObsCode.Text = "Click here to set product active"
        Else
            btnObsCode.Text = "Click here to set product obsolete"
            txtProdObsCode.Text = ""
        End If
    End Sub
End Class
