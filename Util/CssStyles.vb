Imports C1.Web.C1WebGrid
'''     <author>Peter Kamitz</author>
'''         <version>1.0</version>
'''         <revision>1</revision>
''' <summary><para>Provides basic styles used by the c1webgrid and also sets the tooltips</para></summary>

Public Class CssStyles
    Const pathToImage As String = "/c1webgrid_client/v_13/"
    Const SortAscImg As String = "c1grid_SortAsc.gif"
    Const SortDescImg As String = "c1grid_SortDsc.gif"
    Const GroupImg As String = "c1grid_group.gif"
    Const UnGroupImg As String = "c1grid_UnGroup.gif"


    Dim test As String

    '*****************************************************************************************
    '* Erstellt einen mouseover für die einzelne Table-datas. Funktioniert auch
    '* zusätzlich zum showMouseover-effekt. Man kann somit auch einzelne Zellen
    '* mit mouseover versehen
    '*****************************************************************************************
    Public Shared Sub setTDMouseover(ByVal cell As TableCell)
        Dim JS_mouseover As String = "col=this.style.backgroundColor;this.style.backgroundColor='#FACF27';this.style.cursor='hand'"
        Dim JS_mouseout As String = "this.style.backgroundColor=col;"
        cell.Attributes.Add("onMouseOver", JS_mouseover)
        cell.Attributes.Add("onMouseOut", JS_mouseout)
    End Sub

    '*****************************************************************************************
    '* Die Methode hinterlegt für jede Spalte eines C1-Webgrids Tooltips
    '* Der Tooltip kommt von der Spaltenüberschrift.
    '* Um die methode zu verwenden muss man sie im item.databound ereignis aufrufen.
    '*****************************************************************************************
    Public Overloads Shared Sub setColumnToolTips(ByRef dataGridRow As C1.Web.C1WebGrid.C1GridItem, ByRef webGrid As C1.Web.C1WebGrid.C1WebGrid)
        Dim cell2 As TableCell
        Dim counter As Integer = 0
        For Each cell2 In dataGridRow.Cells

            cell2.Attributes.Add("title", webGrid.Columns(counter).HeaderText.ToString)
            counter += 1

        Next
    End Sub
    Public Overloads Shared Sub setColumnToolTips(ByRef dataGridRow As C1.Web.C1WebGrid.C1GridItem, ByRef webGrid As C1.Web.C1WebGrid.C1WebGrid, ByVal grouplevel As Integer)
        Dim cell2 As TableCell
        Dim counter As Integer = 0
        Dim strTooltip As String = ""


        For Each cell2 In dataGridRow.Cells
            If dataGridRow.ItemType = C1.Web.C1WebGrid.C1ListItemType.GroupFooter Or dataGridRow.ItemType = C1.Web.C1WebGrid.C1ListItemType.GroupHeader Then
                'strTooltip = webGrid.Items.Item(1).Cells(counter).Text.ToString
                strTooltip = webGrid.Columns((counter) + grouplevel).HeaderText.ToString
                cell2.Attributes.Add("title", strTooltip)
                counter += 1

            Else
                'strTooltip = webGrid.Items.Item(0).Cells(counter).Text.ToString
                strTooltip = webGrid.Columns(counter).HeaderText.ToString
                cell2.Attributes.Add("title", strTooltip)
                counter += 1
            End If

        Next



    End Sub

    Public Overloads Shared Sub setColumnToolTips(ByRef dataGridRow As C1.Web.C1WebGrid.C1GridItem, ByRef webGrid As C1.Web.C1WebGrid.C1WebGrid, ByVal HeaderRow As String())
        Dim cell2 As TableCell
        Dim x As Integer = 0
        Dim strTooltip As String = ""


        For Each cell2 In dataGridRow.Cells
      
            strTooltip = HeaderRow(x).ToString & " " & webGrid.Columns(x).HeaderText.ToString
                cell2.Attributes.Add("title", strTooltip)

            x = x + 1

        Next
    End Sub



    Public Overloads Shared Sub setColumnToolTips(ByRef dataGridRow As C1.Web.C1WebGrid.C1GridItem, ByRef webGrid As C1.Web.C1WebGrid.C1WebGrid, ByVal grouplevel As Integer, ByVal HeaderRow As String())
        Dim cell2 As TableCell
        Dim counter As Integer = 0
        Dim strTooltip As String = ""

        For Each cell2 In dataGridRow.Cells
            If dataGridRow.ItemType = C1.Web.C1WebGrid.C1ListItemType.GroupFooter Or dataGridRow.ItemType = C1.Web.C1WebGrid.C1ListItemType.GroupHeader Then
                strTooltip = HeaderRow(counter + grouplevel).ToString & " " & webGrid.Columns((counter) + grouplevel).HeaderText.ToString
                cell2.Attributes.Add("title", strTooltip)
                counter += 1

            Else
                strTooltip = HeaderRow(counter).ToString & " " & webGrid.Columns(counter).HeaderText.ToString
                cell2.Attributes.Add("title", strTooltip)
                counter += 1
            End If

        Next



    End Sub

    Public Overloads Shared Sub setColumnToolTips(ByRef dataGridRow As C1.Web.C1WebGrid.C1GridItem, ByRef webGrid As C1.Web.C1WebGrid.C1WebGrid, ByVal grouplevel As Integer, ByVal HeaderRow As String(), ByVal TwoLineHeader As Boolean)
        Dim cell2 As TableCell
        Dim counter As Integer = 0
        Dim strTooltip As String = ""
        grouplevel = CInt(dataGridRow.Attributes.Item("nodeLevel")) - 1
        For Each cell2 In dataGridRow.Cells
            If dataGridRow.ItemType = C1.Web.C1WebGrid.C1ListItemType.GroupFooter Or dataGridRow.ItemType = C1.Web.C1WebGrid.C1ListItemType.GroupHeader Then
                strTooltip = HeaderRow(counter + grouplevel).ToString
                cell2.Attributes.Add("title", strTooltip)
                counter += 1

            Else
                strTooltip = HeaderRow(counter).ToString
                cell2.Attributes.Add("title", strTooltip)
                counter += 1
            End If

        Next



    End Sub

    Public Shared Sub setDataGridStyles(ByRef dg As DataGrid)
        dg.HeaderStyle.CssClass = "head"
        dg.FooterStyle.CssClass = "tableFooterBG"
        dg.ItemStyle.CssClass = "tableBGColor1Class"
        dg.SelectedItemStyle.CssClass = "tableBGColor2Class"
        dg.AlternatingItemStyle.CssClass = "tableBGColor2Class"
        dg.EditItemStyle.CssClass = "dgEditItem"
        dg.AutoGenerateColumns = False

        dg.Width = Unit.Percentage(100)
        dg.HorizontalAlign = HorizontalAlign.Center
        dg.ShowFooter = False
        dg.ShowHeader = True

        dg.BorderStyle = BorderStyle.Solid
        dg.BorderWidth = Unit.Pixel(1)
        dg.BorderColor = Color.Gray
        dg.CellSpacing = 1


        dg.GridLines = GridLines.None
        dg.BackColor = Color.GhostWhite
        dg.CellPadding = 4
        dg.GridLines = GridLines.None
    End Sub

    Public Shared Sub SetGridStyles(ByRef dg As C1.Web.C1WebGrid.C1WebGrid)

        dg.CssClass = "dg"

        dg.HeaderStyle.CssClass = "head"
        dg.FooterStyle.CssClass = "reportTotal"
        dg.ItemStyle.CssClass = "tableBGColor1Class"
        dg.SelectedItemStyle.CssClass = "tableBGColor2Class"
        dg.AlternatingItemStyle.CssClass = "tableBGColor2Class"
        dg.EditItemStyle.CssClass = "dgEditItem"
        dg.GroupingStyle.CssClass = "reportGroupFooter"
        'dg.PagerStyle.CssClass = "dgPager"

        'dg.AllowSorting = True
        'dg.AllowColMoving = True
        'dg.AllowColSizing = True
        dg.AutoGenerateColumns = False

        dg.Width = Unit.Percentage(100)
        'dg.Height.Pixel(400)
        dg.GroupIndent = Unit.Pixel(10)
        dg.HorizontalAlign = HorizontalAlign.Center
        'dg.ShowFooter = True
        dg.ShowHeader = True


        'dg.BorderStyle = BorderStyle.Solid
        'dg.BorderWidth = Unit.Pixel(1)
        'dg.BorderColor = Color.Gray
        'dg.CellSpacing = 1


        'dg.GridLines = GridLines.Horizontal
        'dg.BackColor = Color.FromArgb(&H78DDDDDD)

        dg.CellPadding = 4

        'dg.ItemStyle.BackColor = Color.GhostWhite
        'dg.ItemStyle.ForeColor = Color.Black

        dg.ImageSortAscending = pathToImage & SortAscImg
        dg.ImageSortDescending = pathToImage & SortDescImg
        dg.ImageGroup = pathToImage & GroupImg
        dg.ImageUngroup = pathToImage & UnGroupImg
        dg.GridLines = GridLines.None
    End Sub

    Public Shared Sub SetGridStylesGroup(ByRef dg As C1.Web.C1WebGrid.C1WebGrid)

        dg.CssClass = "dg"

        'hier werden für die grouped-cols die styles gesetzt.
        'jeder erste grouped column wird mit einer bestimmten klassen hinterlegt und alle
        'anderen mit einer anderen css-klasse.
        Dim col As C1.Web.C1WebGrid.C1Column
        Dim counter As Integer = 0

        For Each col In dg.Columns
            If Not col.GroupInfo.FooterText = "" Or Not col.GroupInfo.HeaderText = "" Then
                If counter = 0 Then
                    col.GroupInfo.FooterStyle.CssClass = "reportGroupFooter1st"
                    col.GroupInfo.HeaderStyle.CssClass = "reportGroupFooter1st"
                Else
                    col.GroupInfo.FooterStyle.CssClass = "reportGroupFooter"
                    col.GroupInfo.HeaderStyle.CssClass = "reportGroupFooter"
                End If
                counter += 1
            End If
        Next

        dg.HeaderStyle.CssClass = "head"
        dg.FooterStyle.CssClass = "reportGroupFooter"

        dg.ItemStyle.CssClass = "tableBGColor2Class"
        'dg.SelectedItemStyle.CssClass = "tableBGColor2Class"
        dg.AlternatingItemStyle.CssClass = "tableBGColor2Class"
        'dg.EditItemStyle.CssClass = "dgEditItem"

        'dg.PagerStyle.CssClass = "dgPager"

        'dg.AllowSorting = True
        'dg.AllowColMoving = True
        'dg.AllowColSizing = True
        dg.AutoGenerateColumns = False
        dg.CellPadding = 3

        'Das benötigt man um einen darstellungsfehler (BUG) das C1.Webgrid zu bereinigen.
        'Der fehler kommt beim gruppieren und zeigt wild gridlines an
        dg.BorderStyle = BorderStyle.Solid
        dg.BorderWidth = Unit.Pixel(0)
        'dg.BorderColor = Color.Transparent
        'dg.CellSpacing = 1
        dg.GridLines = GridLines.None
        'dg.ItemStyle.BackColor = Color.Transparent
        'dg.ItemStyle.ForeColor = Color.Black
        'dg.al

        dg.Width = Unit.Percentage(100)
        'dg.Height.Pixel(400)
        dg.GroupIndent = Unit.Pixel(25)
        dg.HorizontalAlign = HorizontalAlign.Center
        dg.ShowFooter = True
        dg.ShowHeader = True


        dg.ImageSortAscending = pathToImage & SortAscImg
        dg.ImageSortDescending = pathToImage & SortDescImg
        dg.ImageGroup = pathToImage & GroupImg
        dg.ImageUngroup = pathToImage & UnGroupImg

    End Sub


    Public Shared Sub SetGridStylesGrouptest(ByRef dg As C1.Web.C1WebGrid.C1WebGrid)

        dg.CssClass = "dg"

        'hier werden für die grouped-cols die styles gesetzt.
        'jeder erste grouped column wird mit einer bestimmten klassen hinterlegt und alle
        'anderen mit einer anderen css-klasse.
        Dim col As C1.Web.C1WebGrid.C1Column
        Dim counter As Integer = 0

        For Each col In dg.Columns
            If Not col.GroupInfo.FooterText = "" Or Not col.GroupInfo.HeaderText = "" Then
                If counter = 0 Then
                    col.GroupInfo.FooterStyle.CssClass = "reportGroupFooter1st"
                    col.GroupInfo.HeaderStyle.CssClass = "reportGroupFooter1st"
                Else
                    col.GroupInfo.FooterStyle.CssClass = "reportGroupFooter"
                    col.GroupInfo.HeaderStyle.CssClass = "reportGroupFooter"
                End If
                counter += 1
            End If
        Next

        dg.HeaderStyle.CssClass = "head"
        dg.FooterStyle.CssClass = "reportGroupFooter"

        dg.ItemStyle.CssClass = "tableBGColor2Class"
        'dg.SelectedItemStyle.CssClass = "tableBGColor2Class"
        dg.AlternatingItemStyle.CssClass = "tableBGColor2Class"
        'dg.EditItemStyle.CssClass = "dgEditItem"

        'dg.PagerStyle.CssClass = "dgPager"

        'dg.AllowSorting = True
        'dg.AllowColMoving = True
        'dg.AllowColSizing = True
        dg.AutoGenerateColumns = False
        dg.CellPadding = 3


        dg.BorderStyle = BorderStyle.Inset
        dg.BorderWidth = Unit.Pixel(1)
        dg.CellSpacing = 1
        dg.GridLines = GridLines.None
        dg.ItemStyle.BackColor = Color.White
        dg.ItemStyle.ForeColor = Color.Black
        dg.AlternatingItemStyle.ForeColor = Color.Black
        dg.AlternatingItemStyle.BackColor = Color.White
        dg.BackColor = Color.Gray


        'Das benötigt man um einen darstellungsfehler (BUG) das C1.Webgrid zu bereinigen.
        'Der fehler kommt beim gruppieren und zeigt wild gridlines an
        'dg.BorderStyle = BorderStyle.Solid
        'dg.BorderWidth = Unit.Pixel(0)
        'dg.BorderColor = Color.Transparent
        'dg.CellSpacing = 1
        'dg.GridLines = GridLines.None
        'dg.ItemStyle.BackColor = Color.Transparent
        'dg.ItemStyle.ForeColor = Color.Black
        'dg.al

        dg.Width = Unit.Percentage(100)
        'dg.Height.Pixel(400)
        dg.GroupIndent = Unit.Pixel(25)
        dg.HorizontalAlign = HorizontalAlign.Center
        dg.ShowFooter = True
        dg.ShowHeader = True


        dg.ImageSortAscending = pathToImage & SortAscImg
        dg.ImageSortDescending = pathToImage & SortDescImg
        dg.ImageGroup = pathToImage & GroupImg
        dg.ImageUngroup = pathToImage & UnGroupImg

    End Sub





    '******************************************************************************************************************
    '' @DESCRIPTION: for table use. it generates 2 seperated colors and javacript for cool mouseover line highlighting
    ''				 Use it in TR-Tags
    '' @PARAM:		- number: the color depends on this number. Odd and even. e.g. counter-var
    '' @PARAM:		- hovereffect: true/false - turn the hovereffect on or off
    '' @RETURN:		string with HTML-code for your TR-Tag
    '******************************************************************************************************************
    Public Shared Function showMouseover(ByVal hovereffect As String)
        showMouseover = " onMouseOver=""this.className='hovereffect';"""
    End Function
End Class
