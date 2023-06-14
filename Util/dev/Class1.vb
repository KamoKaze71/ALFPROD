Imports System
Imports System.Web.UI.WebControls
Imports System.Web
Imports System.Web.UI
Imports System.IO
Imports System.Web.Util



	' <summary>
	' Serves as the base class that defines the methods, properties and events common 
	' to all datagrid exporters in the Web.Generic.DataGridTools
	' </summary>
Public MustInherit Class DataGridExporterBase

	' <summary>
	' Holds a reference to the datagrid being exported
	' </summary>
	Private MyDataGrid As DataGrid

	' <summary>
	' Holds a reference to the page where the datagrid locates
	' </summary>
	Protected CurrentPage As Page

	' <summary>
	' Overloaded. Initializes a new instance of the DataGridExporterBase class.
	' </summary>
	' <param name="dg">The datagrid to be exported</param>
	' <param name="pg">The page to which the datagrid is to be exported</param>
		public DataGridExporterBase(dg as DataGrid ,pg as page)

			MyDataGrid = dg
			CurrentPage = pg


	' <summary>
	' Overloaded. Initializes a new instance of the DataGridExporterBase class.
	' </summary>
	' <param name="dg">The datagrid to be exported</param>
		public DataGridExporterBase(dg as DataGrid )
			inherits me(dg, dg.Page)



	' <summary>
	' Exports the current datagrid
	' </summary>
	Public Sub MustInherit Export()
	End Sub

End Class

' <summary>
' Exports a datagrid to a excel file. 
' </summary>
' <requirements>Microsoft Excel 97 or above should be installed on the client machine in order to make 
' this function work
' </requirements>
Public Class DataGridExcelExporter
	Inherits DataGridExporterBase


	' <summary>
	' CSS file for decoration, se it if any or dont use it
	' </summary>
	Private Const MY_CSS_FILE As String = "./css/MDF.css"

	' <summary>
	' Overloaded. Initializes a new instance of the DataGridExcelExporter class.
	' </summary>
	' <param name="dg">The datagrid to be exported</param>
	' <param name="pg">The page to which the datagrid is to be exported</param>
		public DataGridExcelExporter(dg as DataGrid , Page pg) IHERHITS base(dg, pg)


	' <summary>
	' Overloaded. Initializes a new instance of the DataGridExcelExporter class.
	' </summary>
	' <param name="dg">The datagrid to be exported</param>
		public DataGridExcelExporter(dg as DataGrid )
			inherits base(dg)


	' <summary>
	' Overloaded. Exports a datagrid to an excel file, the title of which is empty
	' </summary>
		public sub override Export()

		Export(String.Empty)

	End Sub


	' <summary>
	' Renders the html text before the datagrid.
	' </summary>
	' <param name="writer">A HtmlTextWriter to write html to output stream</param>
		protected  virtual sub FrontDecorator(writer as HtmlTextWriter)

		writer.WriteFullBeginTag("HTML")
		writer.WriteFullBeginTag("Head")
		writer.RenderBeginTag(HtmlTextWriterTag.Style)
		writer.Write("<!--")

	Dim sr As StreamReader = File.OpenText(CurrentPage.MapPath(MY_CSS_FILE))
	Dim input As String

	While ((input = sr.ReadLine()) <> null)

			writer.WriteLine(input)
		End While

		sr.Close()
		writer.Write("-->")
		writer.RenderEndTag()
		writer.WriteEndTag("Head")
		writer.WriteFullBeginTag("Body")
	End Sub

	' <summary>
	' Renders the html text after the datagrid.
	' </summary>
	' <param name="writer">A HtmlTextWriter to write html to output stream</param>
		protected virtual sub  RearDecorator( writer as HtmlTextWriter)
			writer.WriteEndTag("Body")
			writer.WriteEndTag("HTML")
		end sub

	' <summary>
	' Exports the datagrid to an Excel file with the name of the datasheet provided by the passed in parameter
	' </summary>
	' <param name="reportName">Name of the datasheet.
	' </param>
		public sub virtual Export(reportName as string)

		ClearChildControls(MyDataGrid)
		MyDataGrid.EnableViewState = False		  'Gets rid of the viewstate of the control. The viewstate may make an excel file unreadable.


		CurrentPage.Response.Clear()
		CurrentPage.Response.Buffer = True

		'This will make the browser interpret the output as an Excel file
		CurrentPage.Response.AddHeader("Content-Disposition", "filename=" + reportName)
		CurrentPage.Response.ContentType = "application/vnd.ms-excel"

		'Prepares the html and write it into a StringWriter
		Dim StringWriter As StringWriter = New StringWriter
		Dim htmlWriter As HtmlTextWriter = New HtmlTextWriter(StringWriter)
		FrontDecorator(htmlWriter)
		MyDataGrid.RenderControl(htmlWriter)
		RearDecorator(htmlWriter)

		'Write the content to the web browser
		CurrentPage.Response.Write(StringWriter.ToString())
		CurrentPage.Response.End()
	End Sub

	' <summary>
	' Iterates a control and its children controls, ensuring they are all LiteralControls
	' <remarks>
	' Only LiteralControl can call RenderControl(System.Web.UI.HTMLTextWriter htmlWriter) method. Otherwise 
	' a runtime error will occur. This is the reason why this method exists.
	' </remarks>
	' </summary>
	' <param name="control">The control to be cleared and verified</param>
	Private Sub RecursiveClear(ByVal control As Control)

		'Clears children controls
			for (i as integer =control.Controls.Count -1 i>=0; i--)

			RecursiveClear(control.Controls(i))
		Next

		'
		'If it is a LinkButton, convert it to a LiteralControl
		'
		If (control Is LinkButton) Then

			Dim literal As LiteralControl = New LiteralControl
			control.Parent.Controls.Add(literal)
				literal.Text = ((LinkButton)control).Text
			control.Parent.Controls.Remove(control)

			' We don't need a button in the excel sheet, so simply delete it
		ElseIf (control Is Button) Then

			control.Parent.Controls.Remove(control)


			'If it is a ListControl, copy the text to a new LiteralControl
		ElseIf (control Is ListControl) Then

			LiteralControl(literal = New LiteralControl)
			control.Parent.Controls.Add(literal)
			Try

					literal.Text = ((ListControl)control).SelectedItem.Text

			Catch


				control.Parent.Controls.Remove(control)

			End Try

			'You may add more conditions when necessary

			Return True
	End Sub

	' <summary>
	' Clears the child controls of a Datagrid to make sure all controls are LiteralControls
	' </summary>
	' <param name="dg">Datagrid to be cleared and verified</param>
	Protected Sub ClearChildControls(ByVal dg As DataGrid)


			for( i as integer = dg.Columns.Count -1 ; i>=0; i--)

			Dim column As DataGridColumn = dg.Columns(i)
			If (column Is ButtonColumn) Then

				dg.Columns.Remove(column)



				this.RecursiveClear(dg)



			End If
		Next
		' <summary>
		' HTML Encodes an entire DataGrid. 
		' It iterates through each cell in the TableRow, ensuring that all 
		' the text being displayed is HTML Encoded, irrespective of whether 
		' they are just plain text, buttons, hyperlinks, multiple controls etc..
		' </summary>
		Public Class CellFormater

		' <summary>
		' Constructs an instance of the CellFormater class.
		' </summary>
		Public CellFormater()


		' <summary>
		' Method that HTML Encodes an entire DataGrid. 
		' It iterates through each cell in the TableRow, ensuring that all 
		' the text being displayed is HTML Encoded, irrespective of whether 
		' they are just plain text, buttons, hyperlinks, multiple controls etc..
		' <seealso cref="System.Web.UI.WebControls.DataGrid.ItemDataBound">DataGrid.ItemDataBound Event</seealso>
		' </summary>
		' <param name="item">
		' The DataGridItem that is currently being bound in the calling Web 
		' Page's DataGrid.ItemDataBound Event.
		' </param>
		' <remarks>
		' This method should be called from the 
		' <c>DataGrid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)</c> 
		' event in the respective Web View Codebehind.
		' </remarks>
		' <example>
		'          We want to HTMLEncode a complete DataGrid (all columns and all 
		'          rows that may/do contain characters that will require encoding 
		'          for display in HTML) called dgIssues.
		'          Use the following code for the ItemDataBound Event:
		'          <code>
		'               private void dgIssues_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		'               {
		'                    WebMethod wm = new WebMethod();
		'                    wm.DataGrid_ItemDataBound_HTMLEncode((DataGridItem) e.Item);
		'               }//dgIssues_ItemDataBound
		'          </code>
		' </example>



		public sub AdHocHTMLEncode(item as System.Web.UI.WebControls.DataGridItem )

		bool(doHTMLEncode = False)
		Switch(item.ItemType)


		'The following case statements are in ascending TableItemStyle order.
		'See ms-help://MS.VSCC/MS.MSDNVS/cpref/html/frlrfsystemwebuiwebcontrolsdatagridclassitemstyletopic.htm for details.
				case System.Web.UI.WebControls.ListItemType.Item

		doHTMLEncode = True
		break()
		'ListItemType.Item
				case System.Web.UI.WebControls.ListItemType.AlternatingItem

		doHTMLEncode = True
		break()
		'ListItemType.AlternatingItem
				case System.Web.UI.WebControls.ListItemType.SelectedItem

		doHTMLEncode = True
		break()
		'ListItemType.SelectedItem					
				case System.Web.UI.WebControls.ListItemType.EditItem

		'These should not be prone to this as TextBoxes aren't.
		doHTMLEncode = False
		break()
		'ListItemType.EditItem

				case System.Web.UI.WebControls.ListItemType.Header

		'We might have specified Headers like "<ID>".
		doHTMLEncode = True
		break()
		'ListItemType.Header
				case System.Web.UI.WebControls.ListItemType.Footer

		'Similarly for the Footer as with the Header.
		doHTMLEncode = True

		break()
		'ListItemType.Footer
				case System.Web.UI.WebControls.ListItemType.Pager

		'With just numbers or buttons, none is required.
		'However, for buttons, this is not strictly true as you 
		'need to specify the text on the buttons. But the Property 
		'Builder for the DataGrid hints in its defaults that these 
		'need to be HTMLencoded anyway.
		doHTMLEncode = False
		break()
		ListItemType.Pager()
				case System.Web.UI.WebControls.ListItemType.Separator

		doHTMLEncode = False
		break()
		'ListItemType.Separator

				default

		'This will never be executed as all ItemTypes are listed above.
		break()
		'default
		'switch

		If (doHTMLEncode) Then

			'Encode the cells dependent on the type of content 
			'within (e.g. BoundColumn, Hyperlink), taking into account 
			'that there may be more than one (or even zero) control in 
			'each cell.
				dim cells as System.Web.UI.WebControls.TableCellCollection  = (System.Web.UI.WebControls.TableCellCollection)item.Cells;
				foreach (cell as System.Web.UI.WebControls.TableCell in cells)

			If (cell.Controls.Count <> 0) Then

						foreach ( ctrl as System.Web.UI.Control in cell.Controls)

				If (ctrl Is Button) Then

								dim btn as Button= (Button) ctrl
					btn.Text = HttpUtility.HtmlEncode(btn.Text)
				End If
			ElseIf (ctrl Is HyperLink) Then

								dim hyp as HyperLink = (HyperLink) ctrl
				hyp.Text = HttpUtility.HtmlEncode(hyp.Text)
				'//hyp.NavigateUrl = HttpUtility.UrlEncode(hyp.NavigateUrl);

			ElseIf (ctrl Is LinkButton) Then

								Dim lb = (LinkButton) ctrl
				lb.Text = HttpUtility.HtmlEncode(lb.Text)

				' this check is for to change the forecolor of REJECTED activities to red
			ElseIf (ctrl Is Label) Then

								dim  objL as Label = (Label)ctrl
				If (objL.Text = "REJECTED") Then
					objL.ForeColor = System.Drawing.Color.Red

				Else

					'The cell is a BoundColumn.
					If (cell.Text.ToLower().Trim() <> "&nbsp;") Then
						cell.Text = HttpUtility.HtmlEncode(cell.Text)
					End If

				End If

			End If
		End If

	End Sub

End Class

