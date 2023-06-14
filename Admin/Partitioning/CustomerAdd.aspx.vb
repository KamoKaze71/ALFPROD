Public Class CustomerAdd
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents labelMessage As System.Web.UI.WebControls.Label
    Protected WithEvents dropdownCustomers As System.Web.UI.WebControls.DropDownList
    Protected WithEvents buttonSave As System.Web.UI.WebControls.Button
    Protected WithEvents labelCustomerInfo As System.Web.UI.WebControls.Label
    Protected WithEvents datalistSalesReps As System.Web.UI.WebControls.DataList
    Protected WithEvents step2 As System.Web.UI.WebControls.Panel
    Protected WithEvents firstTitle As System.Web.UI.WebControls.Label
    Protected WithEvents secondTitle As System.Web.UI.WebControls.Label
    Protected WithEvents keyword As System.Web.UI.WebControls.TextBox
    Protected WithEvents searchBar As System.Web.UI.WebControls.Panel
    Protected WithEvents searchBtn As System.Web.UI.HtmlControls.HtmlInputButton

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private productsDataAccess As New TargetProductGroup
    Private Const windowWidth As Integer = 640

    Private ReadOnly Property currentCustomerID() As Integer
        Get
            Dim tmp As String = Request.QueryString.Item("c")

            If tmp <> "" Then
                Return CInt(tmp)
            Else
                Return 0
            End If
        End Get
    End Property

    Private ReadOnly Property isNew() As Boolean
        Get
            If Me.currentCustomerID > 0 Then
                Return False
            Else
                Return True
            End If
        End Get
    End Property

    '*********************************************************************************************************
    '* Page_Load 
    '*********************************************************************************************************
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        productsDataAccess.targetProductGroupID = CInt(Request.QueryString.Item("id"))
        productsDataAccess.userID = CInt(Session.Item("user_id"))

        keyword.Attributes.Add("onkeydown", "return submitButton(searchBtn);")

        If Not Page.IsPostBack Then
            If Me.isNew Then
                searchBar.Visible = True
                PartitioningHelp.setWindowSize(240, windowWidth, Me)
            Else
                searchBar.Visible = False
            End If

            step2.Visible = False
            setTitles()
            showDummyProduct()
            fillProductDropdown()
        End If
    End Sub

    '*********************************************************************************************************
    '* setTitles 
    '*********************************************************************************************************
    Private Sub setTitles()
        If isNew Then
            firstTitle.Text = "1. Choose Customer"
            secondTitle.Text = "2. Assign Sales Rep"
        Else
            firstTitle.Text = "Update Customer"
            secondTitle.Text = "Change percentage"
        End If
    End Sub

    '*********************************************************************************************************
    '* setAllForEdititng 
    '*********************************************************************************************************
    Private Sub setAllForEdititng()
        dropdownCustomers.SelectedValue = Me.currentCustomerID
        dropdownCustomers.Enabled = False
    End Sub

    '*********************************************************************************************************
    '* showDummyProduct 
    '*********************************************************************************************************
    Private Sub showDummyProduct()
        labelCustomerInfo.Text = returnCustomerDetails("", "", "", "", "")
    End Sub

    '*********************************************************************************************************
    '* fillProductDropdown 
    '*********************************************************************************************************
    Private Sub fillProductDropdown()
        Dim dv As DataView

        With dropdownCustomers
            If isNew Then
                dv = productsDataAccess.getUnassignedCustomers(keyword.Text)
                .DataSource = dv
                .DataValueField = "cust_id"
                .DataTextField = "customer"
            Else
                Dim dataCustomer As New WyethCustomer
                dv = dataCustomer.GetCustomerByID(Me.currentCustomerID)
                .DataSource = dv
                .SelectedValue = Me.currentCustomerID
                .DataValueField = "cust_id"
                .DataTextField = "cust_name"
                .Enabled = False
            End If
            .DataBind()

            If keyword.Text <> "" Then
                'if there was only one customer found then select him
                If .Items.Count = 1 Then
                    .SelectedValue = .Items(0).Value
                    customerSelected()
                Else
                    setDefaultView()
                End If

                .Items.Insert(0, New ListItem("---- Please select a customer (" & .Items.Count.ToString & " found.) ----", 0))
            Else
                buttonSave.Visible = False
                .Items.Insert(0, New ListItem("---- Please select a customer or use search above ----", 0))
            End If

        End With

        If Not isNew Then
            customerSelected()
        End If
    End Sub

    '*********************************************************************************************************
    '* dropdownProducts_SelectedIndexChanged 
    '*********************************************************************************************************
    Private Sub dropdownCustomers_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dropdownCustomers.SelectedIndexChanged
        customerSelected()
    End Sub

    Private Sub customerSelected()
        labelMessage.Text = ""
        If Not dropdownCustomers.SelectedValue = 0 Then
            Dim dataCustomer As New WyethCustomer
            Dim dv As DataView = dataCustomer.GetCustomerByID(dropdownCustomers.SelectedValue)

            With dv
                Try
                    Dim name As String = .Table.Rows(0).Item("cust_name")
                    Dim detail As String = .Table.Rows(0).Item("cust_department")
                    Dim zip As String = .Table.Rows(0).Item("cust_zip")
                    Dim city As String = .Table.Rows(0).Item("cust_city")
                    Dim street As String = .Table.Rows(0).Item("cust_street")
                    labelCustomerInfo.Text = returnCustomerDetails(name, detail, zip, city, street)
                Catch ex As Exception
                End Try
            End With

            buttonSave.Visible = True
            step2.Visible = True
            fillSalesReps()
        Else
            setDefaultView()
        End If
    End Sub

    '*********************************************************************************************************
    '* setDefaultView 
    '*********************************************************************************************************
    Private Sub setDefaultView()
        PartitioningHelp.setWindowSize(240, windowWidth, Me)
        step2.Visible = False
        datalistSalesReps.Visible = False
        buttonSave.Visible = False
        showDummyProduct()
    End Sub

    '*********************************************************************************************************
    '* fillSalesReps 
    '*********************************************************************************************************
    Private Sub fillSalesReps()
        Dim dv As DataView

        If isNew Then
            dv = productsDataAccess.getSalesReps()
        Else
            dv = productsDataAccess.getSalesrepsForCustomerAndTGP(Me.currentCustomerID)
        End If
        datalistSalesReps.DataSource = dv
        datalistSalesReps.DataBind()

        PartitioningHelp.setWindowSize(340 + (dv.Count * 25), windowWidth, Me)

        datalistSalesReps.Visible = True
    End Sub

    '*********************************************************************************************************
    '* returnCustomerDetails 
    '*********************************************************************************************************
    Private Function returnCustomerDetails(ByVal name As String, ByVal detail As String, ByVal zip As String, ByVal city As String, ByVal street As String) As String
        If Not city = "" Then
            city = " " & city
        End If

        Return String.Format("<b>Name: </b>{0}<br><b>Department: </b>{1}<br><b>City: </b>{2}<br><b>Street: </b>{3}", _
                            checkIfEmpty(name), checkIfEmpty(detail), checkIfEmpty(zip & city), checkIfEmpty(street))
    End Function

    '*********************************************************************************************************
    '* checkIfEmpty 
    '*********************************************************************************************************
    Private Function checkIfEmpty(ByVal str As String) As String
        If str = "" Then
            Return "-"
        Else
            Return str
        End If
    End Function

    '*********************************************************************************************************
    '* buttonSave_Click 
    '*********************************************************************************************************
    Private Sub buttonSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonSave.Click
        If dropdownCustomers.SelectedValue <> 0 Then
            If totalPercent() <> 100 Then
                labelMessage.Text = "Total percentage must be 100% (currently: " & totalPercent() & "%)"
            Else
                labelMessage.Text = ""
                storeSalesReps()
            End If
        End If
    End Sub

    '*********************************************************************************************************
    '* totalPercent 
    '*********************************************************************************************************
    Private Sub storeSalesReps()
        Dim percentageTextbox As TextBox
        Dim sareIDTextbox As TextBox
        Dim errorHappend As Boolean = False
        Dim sareID As Integer
        Dim percent As Double
        Dim ex As Exception

        For i As Integer = 0 To datalistSalesReps.Items.Count - 1
            With datalistSalesReps
                percentageTextbox = .Items(i).FindControl("salesRepPercentage")
                sareIDTextbox = .Items(i).FindControl("salesRepID")
                sareID = CInt(sareIDTextbox.Text)
                percent = PartitioningHelp.decimalPercentage(percentageTextbox.Text)

                Try
                    productsDataAccess.setPercentageForSalesRep(sareID, CInt(dropdownCustomers.SelectedValue), percent)
                Catch ex
                    errorHappend = True
                End Try
            End With
        Next

        If errorHappend Then
            labelMessage.Text = "Could not save values to Database. Contact admin!"
        Else
            PartitioningHelp.closePopupAndRefreshParent(Me)
        End If
    End Sub

    '*********************************************************************************************************
    '* totalPercent 
    '*********************************************************************************************************
    Private Function totalPercent() As Double
        Dim percentageTextbox As TextBox
        Dim sum As Double = 0

        For i As Integer = 0 To datalistSalesReps.Items.Count - 1
            With datalistSalesReps
                percentageTextbox = .Items(i).FindControl("salesRepPercentage")
                sum += PartitioningHelp.decimalPercentage(percentageTextbox.Text)
            End With
        Next

        Return sum
    End Function

    '*********************************************************************************************************
    '* datalistSalesReps_ItemDataBound 
    '*********************************************************************************************************
    Private Sub datalistSalesReps_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles datalistSalesReps.ItemDataBound
        Dim sareTextbox As TextBox = e.Item.FindControl("salesRepID")
        sareTextbox.Text = e.Item.DataItem("sare_id")

        Dim percentageTextbox As TextBox = e.Item.FindControl("salesRepPercentage")
        With percentageTextbox
            If Not isNew Then
                percentageTextbox.Text = e.Item.DataItem("cusr_percent")
            End If
            .Attributes.Add("onchange", "return checkPercentageField(this, assigned, freeAssign);")
            .Attributes.Add("onkeydown", "allowOnlyNumbers();")
        End With
    End Sub

    '*********************************************************************************************************
    '* searchBtn_ServerClick 
    '*********************************************************************************************************
    Private Sub searchBtn_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles searchBtn.ServerClick
        fillProductDropdown()
    End Sub
End Class
