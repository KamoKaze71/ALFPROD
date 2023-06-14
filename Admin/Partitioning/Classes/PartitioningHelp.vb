Imports System.Web.UI
Imports Wyeth.Utilities

Public Class PartitioningHelp

    '***************************************************************************************************
    '* closePopupAndRefreshParent 
    '***************************************************************************************************
    Public Shared Sub closePopupAndRefreshParent(ByVal currentPage As Page)
        Dim str As String = "<script language=Javascript>" & vbNewLine & "closePopup(window);" & vbNewLine & "</script>"
        currentPage.RegisterClientScriptBlock("anything", str)
    End Sub

    '***************************************************************************************************
    '* closePopupAndLoadUrlInParent 
    '***************************************************************************************************
    Public Shared Sub closePopupAndLoadUrlInParent(ByVal url As String, ByVal currentPage As Page)
        Dim str As String = "<script language=Javascript>" & vbNewLine & "closePopupAndRedirect('" & url & "');" & vbNewLine & "</script>"
        currentPage.RegisterClientScriptBlock("anything", str)
    End Sub

    '***************************************************************************************************
    '* setWindowSize 
    '***************************************************************************************************
    Public Shared Sub setWindowSize(ByVal height As Integer, ByVal width As Integer, ByVal currentPage As Page)
        Dim str As String = "<script language=Javascript>" & vbNewLine & "setWindowSize(window, " & height & ", " & width & ");" & vbNewLine & "</script>"
        currentPage.RegisterClientScriptBlock("anything", str)
    End Sub

    '***************************************************************************************************
    '* decimalPercentage 
    '***************************************************************************************************
    Public Shared Function decimalPercentage(ByVal value As String) As Double
        Return CDbl(Replace(value, ".", ","))
    End Function

    '***************************************************************************************************
    '* addCustomerToTPG 
    '***************************************************************************************************
    Public Shared Sub addCustomerToTPGFromPopup(ByVal customerID As Integer, ByVal tpgID As Integer, ByRef currentPage As Page)
        Dim partUrl As String
        partUrl = Settings.applicationUrl & "admin/Partitioning/Partitioning.aspx?pageTitle=Target Product Groups&id=" & tpgID & "&cust=" & customerID

        Dim str As String = "<script language=Javascript>" & vbNewLine & _
                            "   window.opener.location.href = '" & partUrl & "';" & vbNewLine & _
                            "</script>"

        currentPage.Response.Write(str)
    End Sub
    Public Shared Sub addCustomerToTPG(ByVal customerID As Integer, ByVal tpgID As Integer, ByRef currentPage As Page)
        Dim partUrl As String
        partUrl = Settings.applicationUrl & "admin/Partitioning/Partitioning.aspx?pageTitle=Target Product Groups&id=" & tpgID & "&cust=" & customerID

        Dim str As String = "<script language=Javascript>" & vbNewLine & _
                            "   window.location.href = '" & partUrl & "';" & vbNewLine & _
                            "</script>"

        currentPage.Response.Write(str)
    End Sub

    '***************************************************************************************************
    '* generatePercentBar 
    '***************************************************************************************************
    Public Shared Function generatePercentBar(ByVal value As Integer, ByVal itemIndex As Integer) As Table
        If value > 99 Then
            value = 100
        End If

        Dim table As New table
        Dim row As New TableRow
        Dim cellLeft As New TableCell
        Dim cellRight As New TableCell
        Dim cellLast As New TableCell

        table.Width = Unit.Pixel(180)
        table.Height = Unit.Pixel(10)
        table.ID = "percentBar_" & itemIndex
        table.CellPadding = 0
        table.CellSpacing = 0
        cellLeft.Width = Unit.Pixel(value)
        cellLeft.Style.Add("background-color", "#EB2C0A")
        cellLeft.ToolTip = value.ToString & "% used"
        cellRight.Width = Unit.Pixel(100 - value)
        cellRight.Style.Add("background-color", "#A6D672")
        cellRight.ToolTip = 100 - value.ToString & "% available"
        cellLast.Text = "&nbsp;&nbsp;" & 100 - value & " % free"

        row.Cells.Add(cellLeft)
        row.Cells.Add(cellRight)
        row.Cells.Add(cellLast)
        table.Rows.Add(row)

        Return table
    End Function

End Class
