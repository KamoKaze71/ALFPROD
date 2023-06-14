Imports System.Data
Imports Oracle.DataAccess.Client
Imports Wyeth.Utilities.Helper
Imports Wyeth.Utilities
Imports Wyeth.Utilities.DateHandling
Imports System.Web.UI.WebControls

'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
'''         <revision>1</revision>
''' <summary><para>Helper Class that returns various filled Dropdownboxes used in ALF </para></summary>
'''<seealso cref="Utilities.FTP">[label]</seealso> 
Public Class WyethDropdown


    Public Shared Function GetBudgetDD(ByRef MyDropdown As DropDownList) As DropDownList


        Dim MyddItemBU As New ListItem
        Dim MyddItemBE As New ListItem
        Dim unit As New Unit

        MyddItemBU.Value = "BU"
        MyddItemBU.Text = "Budget"

        MyDropdown.Items.Add(MyddItemBU)

        MyddItemBE.Value = "BE"
        MyddItemBE.Text = "PIA"

        MyDropdown.Items.Add(MyddItemBE)
        MyDropdown.DataBind()

        MyDropdown.SelectedValue = "BU"
        MyDropdown.CssClass = "formfield"
        MyDropdown.Width = unit.Pixel(100)
        Return MyDropdown

    End Function

    Public Shared Function GetMonthSelectDD(ByRef MyDropdown As DropDownList) As DropDownList


        Dim MyddItem As New ListItem
        Dim unit As New Unit
        Dim MyMonth As String
        Dim dat As Date = Date.Today


        dat = DateAdd(DateInterval.Month, -24, dat)

        For i As Integer = 1 To 24
            Dim li As New ListItem

            dat = DateAdd(DateInterval.Month, 1, dat)
            If (dat >= CDate("2003-01-01")) Then
                li.Text = dat.ToString("MMM-yyyy", GetMyDTFI())
                li.Value = dat.ToString("MMM-yyyy", GetMyDTFI())

                MyDropdown.Items.Add(li)
                'MyDropdown.Items.Add(dat.ToString("MMM-yyyy", GetMyDTFI()))
            End If
        Next
        MyDropdown.DataBind()
        MyDropdown.SelectedValue = dat.ToString("MMM-yyyy", GetMyDTFI())
        MyDropdown.CssClass = "formfield"
        MyDropdown.Width = unit.Pixel(150)
        Return MyDropdown

    End Function

    Public Shared Function GetMonthSelectToProcessMonth(ByRef MyDropdown As DropDownList, ByVal maxMonth As Date) As DropDownList
        Dim MyddItem As New ListItem
        Dim unit As New unit
        Dim MyMonth As String
        Dim dat As Date = maxMonth


        dat = DateAdd(DateInterval.Month, -24, dat)

        For i As Integer = 1 To 24
            Dim li As New ListItem

            dat = DateAdd(DateInterval.Month, 1, dat)
            If (dat >= CDate("2003-01-01")) Then
                li.Text = dat.ToString("MMM-yyyy", GetMyDTFI())
                li.Value = dat.ToString("MMM-yyyy", GetMyDTFI())

                MyDropdown.Items.Add(li)
                'MyDropdown.Items.Add(dat.ToString("MMM-yyyy", GetMyDTFI()))
            End If
        Next
        MyDropdown.DataBind()
        MyDropdown.SelectedValue = dat.ToString("MMM-yyyy", GetMyDTFI())
        MyDropdown.CssClass = "formfield"
        MyDropdown.Width = unit.Pixel(150)
        Return MyDropdown
    End Function

    Public Shared Function GetMonthSelectDDAMS(ByRef MyDropdown As DropDownList) As DropDownList


        Dim MyddItem As New ListItem
        Dim unit As New unit
        Dim MyMonth As String
        Dim dat As Date = "1995-11-01"


        For i As Integer = 1 To 85
            Dim li As New ListItem

            dat = DateAdd(DateInterval.Month, 1, dat)

            li.Text = dat.ToString("MMM-yyyy", GetMyDTFI())
            li.Value = dat.ToString("MMM-yyyy", GetMyDTFI())

            MyDropdown.Items.Add(li)

        Next
        MyDropdown.CssClass = "formfield"
        MyDropdown.Width = unit.Pixel(150)
        Return MyDropdown

    End Function

    Public Shared Function GetLineSelectDD(ByRef MyDropdown As DropDownList, ByVal v_ctry_id As Integer) As DropDownList


        Dim MyddItem As New ListItem
        Dim unit As New unit
        Dim MyProduct As New WyethProduct
        MyProduct.ProdCtryId = v_ctry_id
        MyDropdown.DataSource = MyProduct.GetLines()
        MyDropdown.DataTextField = "line_description"
        MyDropdown.DataValueField = "line_id"

        MyDropdown.DataBind()

        MyDropdown.CssClass = "formfield"
        MyDropdown.Width = unit.Pixel(110)
        Return MyDropdown

    End Function

    Public Shared Function GetBewegKZSelectDD(ByRef MyDropdown As DropDownList, ByVal v_ctry_id As Integer) As DropDownList


        Dim MyddItem As New ListItem
        Dim unit As New unit
        Dim MyJDE As New JDE

        MyDropdown.DataSource = MyJDE.GetAllBewegKZs(v_ctry_id)
        MyDropdown.DataTextField = "code_name"
        MyDropdown.DataValueField = "code_id"

        MyDropdown.DataBind()

        MyDropdown.CssClass = "formfield"
        MyDropdown.Width = unit.Pixel(100)
        Return MyDropdown

    End Function

    Public Shared Function GetDistribSelectDD(ByRef MyDropdown As DropDownList, ByVal v_ctry_id As Integer) As DropDownList


        Dim MyddItem As New ListItem
        Dim unit As New unit
        Dim MyDistributor As New WyethDistributor

        MyDistributor.DistCtryID = v_ctry_id

        MyDropdown.DataSource = MyDistributor.GetDistributors
        MyDropdown.DataTextField = "dist_name"
        MyDropdown.DataValueField = "dist_id"

        MyDropdown.DataBind()

        MyDropdown.CssClass = "formfield"
        MyDropdown.Width = unit.Pixel(100)
        Return MyDropdown

    End Function

    Public Shared Function GetCurrencySelectDD(ByRef MyDropdown As DropDownList) As DropDownList
        Dim MyStock As New Stock
        MyDropdown.DataSource = MyStock.GetCurrencies()
        MyDropdown.DataValueField = "curr_id"
        MyDropdown.DataTextField = "curr_code"
        MyDropdown.DataBind()
    End Function

    Public Shared Function GetProductSelectDD(ByRef MyDropdown As DropDownList, ByVal line_id As Integer, ByVal ctry_id As Integer, Optional ByVal obs_code As String = "ALL") As DropDownList
        Dim MyProduct As New WyethProduct

        MyProduct.ProdCtryId = ctry_id
        MyProduct.ProdLineId = line_id
        MyProduct.ObsCode = obs_code
        MyDropdown.Width = Unit.Pixel(200)
        MyDropdown.Items.Insert(0, "Please Select a Product")
        MyDropdown.DataSource = MyProduct.GetProductList
        MyDropdown.DataValueField = "prod_id"
        MyDropdown.DataTextField = "product"
        MyDropdown.DataBind()


    End Function

    Public Shared Function GetProductDescriptionSelectDD(ByRef MyDropdown As DropDownList, ByVal line_id As Integer, ByVal ctry_id As Integer, ByVal obs_code As String) As DropDownList
        Dim MyProduct As New WyethProduct

        MyProduct.ProdCtryId = ctry_id
        MyProduct.ProdLineId = line_id
        MyDropdown.Width = Unit.Pixel(300)
        MyDropdown.Items.Insert(0, "Please Select a Product")
        MyDropdown.DataSource = MyProduct.GetProductDescriptions(ctry_id, line_id, obs_code)
        MyDropdown.DataValueField = "prod_description"
        MyDropdown.DataTextField = "prod_description"
        MyDropdown.DataBind()

        Dim li As New ListItem
        li.Value = 0
        li.Text = "-- All Products --"
        MyDropdown.Items.Insert(0, li)


    End Function

    Public Shared Function GetProcessedMonthSelectDD(ByRef MyDropdown As DropDownList, ByVal ctry_id As Integer)
        Dim MyProduct As New WyethProduct
        Dim MyMep As New MEPData

        MyDropdown.DataSource = MyMep.GetProcessedMonth(ctry_id)
        MyDropdown.DataValueField = "pm_date"
        MyDropdown.DataTextField = "pm_date_show"
        MyDropdown.DataBind()


    End Function

    Public Shared Function GetCountrySelectDD(ByRef MyDropdown As DropDownList)
        Dim MyProduct As New WyethProduct
        Dim MyMep As New MEPData

        MyDropdown.DataSource = MyProduct.GetCountries()
        MyDropdown.DataValueField = "ctry_id"
        MyDropdown.DataTextField = "ctry_description"
        MyDropdown.DataBind()

    End Function

    Public Shared Function GetProductGroupSelectDD(ByRef MyDropdown As DropDownList, ByVal v_ctry_id As Integer)

        Dim MyPrGrDataView As DataView
        Dim MyProduct As New WyethProduct

        MyProduct.ProdCtryId = v_ctry_id
        MyPrGrDataView = MyProduct.GetProductGroups

        MyDropdown.DataSource = MyPrGrDataView
        MyDropdown.DataTextField = "prgr_description"
        MyDropdown.DataValueField = "prgr_id"
        MyDropdown.DataBind()

    End Function

    Public Shared Function GetTargetProductGroupSelectDD(ByRef MyDropdown As DropDownList, ByVal v_ctry_id As Integer)
        Dim MyProduct As New WyethProduct

        MyProduct.ProdCtryId = v_ctry_id
        MyDropdown.DataSource = MyProduct.GetTargetPrGr()
        MyDropdown.DataValueField = "tapg_id"
        MyDropdown.DataTextField = "tapg_description"
        MyDropdown.DataBind()


    End Function

    Public Shared Sub fillDDwithDT(ByVal MyDt As DataTable, ByRef MyLb As DropDownList, ByVal strZeroIndex As String)
        Dim MyDv As New DataView
        MyDv.Table = MyDt
        MyDv.Sort = MyDv.Table.Columns(1).ToString & " ASC"

        MyLb.DataSource = MyDv
        MyLb.DataValueField = MyDv.Table.Columns(0).ToString
        MyLb.DataTextField = MyDv.Table.Columns(1).ToString
        MyLb.DataBind()

        Dim li0 As New ListItem
        li0.Value = "0"
        li0.Text = strZeroIndex
        MyLb.Items.Insert(0, li0)


    End Sub

    Public Shared Sub GetNotAssignedSampleProducts(ByVal ctry_id As Integer, ByRef MyDD As DropDownList)
        Dim MyProduct As New WyethProduct
        MyProduct.ProdCtryId = ctry_id
        MyDD.DataSource = MyProduct.GetNotAssignedSampleProducts()
        MyDD.DataValueField = "prod_id"
        MyDD.DataTextField = "Prod_presentation"
        MyDD.DataBind()

    End Sub

    Public Shared Sub GetCustomerGroupSelect(ByRef MyDD As DropDownList, ByVal ctry_id As Integer)
        Dim MyCustomer As New WyethCustomer
        MyCustomer.Cust_Country_Id = ctry_id
        MyDD.DataSource = MyCustomer.GetCustomerGroups
        MyDD.DataValueField = "cugr_id"
        MyDD.DataTextField = "cugr_name"
        MyDD.DataBind()
    End Sub

    Public Shared Sub GetTPGDD(ByRef MyDD As DropDownList, ByVal ctry_id As Integer)
        Dim mytapg As New TargetProductGroup
        mytapg.countryID = ctry_id
        With MyDD
            .DataSource = mytapg.getList()
            .DataTextField = "tapg_description"
            .DataValueField = "tapg_id"
            .DataBind()
        End With
    End Sub

    Public Shared Sub GetYearDD(ByRef MyDD As DropDownList, ByVal sy As Integer, Optional ByVal futurYears As Integer = 0)
        Dim y As Integer = Year(Now())
        sy = sy + futurYears

        For i As Integer = y To sy

            Dim li As New ListItem
            li.Text = y
            li.Value = y

            y = y + 1
            MyDD.Items.Add(li)
        Next
        MyDD.SelectedValue = Year(Now())
        MyDD.DataBind()
    End Sub

    Public Shared Sub GetYearDD3(ByRef MyDD As DropDownList, ByVal sy As Integer, Optional ByVal futurYears As Integer = 0)
        Dim y As Integer = sy '= Year(Now())
        'sy = sy + futurYears

        For i As Integer = sy To sy + futurYears
            Dim li As New ListItem
            li.Text = y
            li.Value = y

            y = y + 1
            MyDD.Items.Add(li)
        Next
        MyDD.SelectedValue = Year(Now())
        MyDD.DataBind()
    End Sub

    Public Shared Sub GetYearDD2(ByVal MyDD As DropDownList, ByVal sy As Integer, Optional ByVal futurYears As Integer = 0)

        If futurYears = 0 Then
            futurYears = Year(Now())
        End If

        For i As Integer = sy To futurYears

            Dim li As New ListItem
            li.Text = sy
            li.Value = sy

            sy = sy + 1
            MyDD.Items.Add(li)
        Next
        MyDD.SelectedValue = Year(Now())
        MyDD.DataBind()
    End Sub
    Public Shared Sub GetYearDD4(ByVal MyDD As DropDownList, ByVal sy As Integer, Optional ByVal futurYears As Integer = 0)

        'If futurYears = 0 Then
        '    futurYears = Year(Now())
        'End If

        For i As Integer = sy To Year(Now()) + futurYears

            Dim li As New ListItem
            li.Text = sy
            li.Value = sy

            sy = sy + 1
            MyDD.Items.Add(li)
        Next
        MyDD.SelectedValue = Year(Now())
        MyDD.DataBind()
    End Sub

    Public Shared Sub GetAllSaReDD(ByRef MyDD As DropDownList, ByVal ctry_id As Integer)
        Dim mysare As New WyethSalesRep
        mysare.SalesRepCtryId = ctry_id
        With MyDD
            .DataSource = mysare.GetSalesReps
            .DataTextField = "sare_name"
            .DataValueField = "sare_id"
            .DataBind()
        End With
    End Sub

    Public Shared Sub GetSaReForTPGDD(ByRef MyDD As DropDownList, ByVal tpg_id As Integer)
        Dim mytapg As New TargetProductGroup
        mytapg.targetProductGroupID = tpg_id
        With MyDD
            .DataSource = mytapg.getAllSalesRepsForTPG
            .DataTextField = "fullname"
            .DataValueField = "sare_id"
            .DataBind()
        End With
        If MyDD.Items.Count = 0 Then
            MyDD.DataSource = Nothing
            Dim li As New ListItem
            li.Value = 0
            li.Text = "no Sales Reps "
            MyDD.Items.Add(li)
        End If

    End Sub

    Public Shared Sub GetVersionDD(ByVal MyDD As DropDownList, ByVal sare_id As Integer, ByVal year As Integer, ByVal tapg_id As Integer)

        Dim MyTargeting As New Targeting
        Dim myDataview As New DataView
        myDataview = MyTargeting.GetTargetVersions(year, sare_id, tapg_id)
        MyDD.Items.Clear()

        If myDataview.Count > 0 Then
            MyDD.DataSource = myDataview
            MyDD.DataTextField = "Targ_version_approval"
            MyDD.DataValueField = "Targ_version"
            MyDD.DataBind()
        Else
            MyDD.DataSource = Nothing
            Dim li As New ListItem
            li.Value = 0
            li.Text = "no versions "
            MyDD.Items.Add(li)
        End If

    End Sub

    Public Shared Sub GetIntranetUsersDD(ByVal MyDD As DropDownList, ByVal ctry_id As Integer)
        Dim MySalesRep As New WyethSalesRep
        MyDD.DataSource = MySalesRep.GetIntranetUsers(ctry_id)
        MyDD.DataTextField = "Name"
        MyDD.DataValueField = "user_id"
        MyDD.DataBind()
        Dim li As New ListItem
        li.Value = 0
        li.Text = "please assign a intranet user "
        MyDD.Items.Add(li)
    End Sub

    Public Shared Sub GetOBSProductDD(ByVal MyDD As DropDownList)

        Dim li As New ListItem
        li.Value = "ALL"
        li.Text = "All Products"
        MyDD.Items.Add(li)
        Dim li2 As New ListItem
        li2.Value = "NOOBS"
        li2.Text = "Non-OBS Products"
        MyDD.Items.Add(li2)

        Dim li3 As New ListItem
        li3.Value = "OBS"
        li3.Text = "OBS Products"
        MyDD.Items.Add(li3)
        MyDD.DataBind()

    End Sub

    Public Shared Sub GetDDTPGTypes(ByRef MyDD As DropDownList, ByVal country_id As Integer)
        Dim myCodes As New WyethCodes
        With MyDD
            .DataSource = myCodes.GetCodesByCat("TPG Type", country_id)
            .DataTextField = "code_code"
            .DataValueField = "code_id"
            .DataBind()
        End With
    End Sub
End Class
