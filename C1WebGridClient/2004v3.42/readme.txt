======================================================================
C1WebGrid Build Number 1.1.20043.42   Build Date: Monday, June 7, 2004
======================================================================

 Note: Now uses v1_9 of the jscript files.
       Assembly version has been updated.

Enhancements/Documentation/Behavior Changes
-------------------------------------------


Corrections
-----------

 ** Fixed columns scroll off the view while editing a row in the grid.  (ANGRD000028)


========================================================================
C1WebGrid Build Number 1.1.20042.41   Build Date: Friday, April 30, 2004
========================================================================

Enhancements/Documentation/Behavior Changes
-------------------------------------------

 * Note: Now uses v1_8 of jscript files.

 * Scroll position (for a scrollable grid) is now maintained on postback.

Corrections
-----------

 ** Note: ClientDir has been upgraded to V1_8.

 ** Dragging a column into the grouping area then clicking an Edit command
    was causing the header text to not render.
    
 ** Group footer was still visible when group it belonged too was collapsed.

 ** Help is not opening on clicking the "Help" button on the property
    builder page of the grid.  (ANGRD000030)

 ** Footer of inner level group is visible even though outer level
    group is collapsed in case of nested grouping.  (ANGRD000029)

 ** Documentation specify that setting the DefaultColumnGrid to 0
    will specify an autosize with content. But doing so makes the scroll 
    bar invisible.  (ANGRD000025)

 ** Vertical scroll bar height was not ajusted if the group body was
    expanded, thus hiding the very last rows of the grid. (ANGRD000025)

=======================================================================
C1WebGrid Build Number 1.1.20042.39   Build Date: Monday, March 8, 2004
=======================================================================

Enhancements/Documentation/Behavior Changes
-------------------------------------------

 * Note: Now uses v1_7 of jscript files.

 * You can now save the grid's layout either at design-time or run-time.
   
   To save/load the grid's layout you can right click the control at
   design-time and select either Save/Load layout.

   Note: Template columns are not supported.

   - New enum: LayoutTypeFlags

     This enum used in conjunction with the LoadLayout() methods can be used to
     specify which objects to deserialize.  Since this enum has the Flags attribute, 
     you can OR these together to load different categories of settings. 
     

     LayoutTypeFlags.Styles - Specifies the loading of the grid's styles surfaced by the
                              following properties:

      AlternatingItemStyle
      ItemStyle
      EditItemStyle
      FooterStyle
      HeaderStyle
      GroupingStyle
      PagerSyle
      SelectedItemStyle


     LayoutTypeFlags.Columns - Specifies the loading of the grid's column information
                               except for template columns.
     
     LayoutTypeFlags.Apperance - Specifies the loading of appearance settings.
      BackColor           
      BorderColor         
      BorderWidth         
      BorderStyle         
      CellPadding         
      CellSpacing         
      CssClass            
      Font                
      ForeColor           
      GroupByCaption      
      GroupIndent         
      BackImageUrl
      HorizontalAlign
      ImageGroup
      ImageSortAscending
      ImageSortDescending
      ImageUngroup
      ShowFooter
      ShowHeader
      HScrollBarStyle
      VScrollBarStyle

     LayoutTypeFlags.Behavior - Specifies the loading of behaviors.
	  AllowCustomPaging
	  AllowPaging
	  AllowAutoSort
	  AllowColMoving
	  AllowColSizing
	  AllowGrouping
	  AllowSorting
	  AutoGenerateColumns

     LayoutTypeFlags.Sizes - Specifies the loading of sizes.
	  DefaultColumnWidth
	  DefaultRowHeight
	  PageSize
	  Height
	  Width


   - New methods: 
   
     public void SaveLayout(string filename)
     public void SaveLayout(Stream stream)

     Used to save the current layouts of the grid to either an xml file or stream.

   - New methods:

     public void LoadLayout(string filename)
     public void LoadLayout(Stream stream)
     public void LoadLayout(string url, LayoutTypeFlags layoutTypes)
     public void LoadLayout(Stream stream, LayoutTypeFlags layoutTypes)

     Used to load a previously saved layout file.


=======================================================================
C1WebGrid Build Number 1.1.20041.37   Build Date: Monday, March 1, 2004
=======================================================================

Enhancements/Documentation/Behavior Changes
-------------------------------------------

 * Note: Now uses v1_7 of jscript files.

 * C1WebGrid now supports right-to-left rendering under Internet Explorer and
   Netscape web browsers. This includes correct handling of column moving,
   grouping and other visual effects.

   To use this functionality make sure you add the dir="rtl" to the c1webgrid tag.

Corrections
-----------

 ** Fixed exception when the Page property was null as design time.  This occured when
    the grid was contained within another control.


==============================================================================
C1WebGrid Build Number 1.1.20041.36   Build Date: Wednesday, December 31, 2003
==============================================================================

Enhancements/Documentation/Behavior Changes
-------------------------------------------

 * C1WebGrid now tracks the expanded/collapsed state of grouped rows on postback.

 * New method: C1WebGrid.DataBind(bool keepGroups) method.

   This override can be used to maintain the expanded/collapsed state of grouped
   rows when the grid is bound to its datasource.  Passing in true to this method
   keeps the current expanded/collapsed state of the grouped rows.  False (the same
   as calling DataBind()) uses the initial state as set in the GroupInfo.OutlineMode 
   property.


Corrections
-----------

 ** Having the PagerStyle.Position set to Top or TopAndBottom was throwing an
    exception. (ANGRD000001)

 ** Grid wasn't rendering grouped rows correctly when GroupInfo.GroupSingleRow property
    was set to false. (ANGRD000002)

 ** When more than one column is grouped, clicking on a column header was throwing an
    exception. (ANGRD000004)


==============================================================================
C1WebGrid Build Number 1.1.20041.34   Build Date: Wednesday, December 03, 2003
==============================================================================

Enhancements/Documentation/Behavior Changes
-------------------------------------------

 * New C1Column.AllowGroup property.

   This property can be used to disallow the client side grouping of a column
   in a grouped grid.

   When this property is set to true (it's default value), you can drag and drop
   columns to the grouping area.

Corrections
-----------

 ** Having a client scrollable grid was not correctly restoring viewstate for
    child controls on postback.

 ** Client functionality wasn't working properly when the grid was embedded inside
    of a naming container (e.g., usercontrol).

 ** A client scrollable grid wasn't raising the ItemCommand event.


===============================================
C1WebGrid Build 1.1.20034.32  15 Sep 2003
===============================================

Enhancements/Documentation Changes
----------------------------------

  Client side scrolling.

  This build allows one to create a client side grid that supports both Horizontal
  and Vertical scrolling.  

  * New enum C1WebGrid.C1ScrollBarStyle
	
	o C1ScrollBarStyle.None - No scrollbar.
	o C1ScrollBarStyle.Always - Always show the scrollbar.
	o C1ScrollBarStyle.Automatic - Show the scrollbar if the content of the webgrid is larger than the control.


    Use this enumeration to specify the visibility of scrollbars.

  * New C1WebGrid.HScrollBarStyle property.

    Specifies the horizontal scrollbar style.  Default value is C1ScrollBarStyle.None.

  * New C1WebGrid.VScrollBarStyle property.

    Specifies the vertical scrollbar style.  Default value is C1ScrollBarStyle.None.

  * New C1WebGrid.DefaultColumnWidth property.

    For a scrollable grid, this property specifies the width for columns in which no width has
    been specified.  Default value is 120 pixels.

  * New C1WebGrid.DefaultRowHeight property.

    For a scrollable grid, this property specifies the height for rows.  Default value is 22 pixels.

  * New C1Column.Fixed property.

    This property can be used to fix a column so that it doesn't scroll.  Default value is false.

    When this property is set to true on a column, all columns to the left are also fixed.  This property
    is usually used with the HScrollbarStyle property.

    Note:  This property is ignored under group mode.

  * New C1GridItem.Fixed property.

    This property can be used to fix a row to the top of the grid when you scroll vertically.  Default
    value is false.

    For a scrollable grid the C1ListItemType.Header will have a default value of true.  This property
    can be set in either the ItemCreated() or ItemDataBound() event.

    Note:  When the grid is in grouping mode, the entire group that contains a fixed row will be moved
    to the top of the grid.


===============================================
C1WebGrid Build 1.1.20033.25  06 Jun 2003
===============================================

Corrected Problems
------------------

-  Default value of PageCount should of been 1 not 0.

-  The Pager tab in the Property Builder was resetting the PagerStyle.

-  Setting the BackImageUrl property was showing the image in each cell.

-  Fixed setting ImageUrl properties in the Property Builder.

-  Fixed rendering when using RowMergeEnum.Restricted.

===============================================
C1WebGrid Build 1.1.20033.24  06 Jun 2003
===============================================

Corrected Problems
------------------

-  Fixed problem when setting AllowPaging = False and PagerStyle = TopAndBottom.

-  Dragging a column to the grouping area after using the GroupedColumn.Clear() method was
   still showing the previous columns as grouped.

-  Exporting to Excell with Office XP was correctly exporting the footer row.

===============================================
C1WebGrid Build 1.1.20033.22  04 Jun 2003
===============================================

Corrected Problems
------------------

 - It is not allowed to set the PageSize property to the negative values and zero
   anymore. 

 - Fixed problem with the "HorizontalAlign" and "VerticalAlign" properties in
   GroupingStyle. They had no effect.

 - The Property Builder dialog is now used as the editor for the C1WebGrid.Columns
   property instead of the default collection editor.

 - Fixed several bugs in the Property Builder.

 - Fixed problem with column headers, they always appeared bold.

 - Fixed Font problems in the Format Tab in the Property Builder (Bold, Italic, Underline,
   Strikeout and Overline could not be reset).

 - Fixed exception when calling C1PagerStyle.MergeWith method.

 - Fixed incorrect behavior that causes incorrect export of the C1WebGrid content
   from IE to Excel when using Office XP.


===============================================
C1WebGrid Build 1.1.20032.19  09 Apr 2003
===============================================

Enhancements/Documentation Changes
----------------------------------

 - None.

Corrected Problems
------------------

 - Fixed exception when using Select button.

 - Fixed problem when the grid was in a user control and using client side column moving
   or grouping.

 - Setting the SortDirection property at design time was throwing an exception.

 - Fixed bug that caused incorrect sorting of non-C1BoundColumn
   columns in AllowAutoSort mode.

 - Fixed incorrect grid rendering that sometimes happens when grid columns are moved.

 - Fixed row merging when the column being merged was a C1TemplateColumn.

===============================================
C1WebGrid Build 1.1.20031.16  11 Feb 2003
===============================================

Note:  This is a drop in replacement for Build 1.1.20031.10.  You will
       need to install Build 1.1.20031.10 prior to using this build.

Enhancements/Documentation Changes
----------------------------------


Corrected Problems
------------------

 - Fixed client side moving of columns in the grid area when columns were also
   being grouped.

===============================================
C1WebGrid Build 1.1.20031.15  29 Jan 2003
===============================================

Note:  This is a drop in replacement for Build 1.1.20031.10.  You will
       need to install Build 1.1.20031.10 prior to using this build.

Enhancements/Documentation Changes
----------------------------------


Corrected Problems
------------------

 - Fixed problem when adding template columns using the property builder.  The UpdateBindings 
   property was being set to an incorrect value.

 - Fixed rendering of Grouped rows with columns that were not visible.

 - Fixed licensing problem when more than one licensing resource was in an application.

===============================================
C1WebGrid Build 1.1.20031.12  08 Jan 2003
===============================================

Note:  This is a drop in replacement for Build 1.1.20031.10.  You will
       need to install Build 1.1.20031.10 prior to using this build.

Enhancements/Documentation Changes
----------------------------------


Corrected Problems
------------------

 - Fixed Field not found exception when using a bound C1HyperLinkColumn.

 - Fixed order of columns when using both AutoGenerated columns and manually created columns.


===============================================
C1WebGrid Build 1.1.20031.11  03 Jan 2003
===============================================

Note:  This is a drop in replacement for Build 1.1.20031.10.  You will
       need to install Build 1.1.20031.10 prior to using this build.

Enhancements/Documentation Changes
----------------------------------


Corrected Problems
------------------

 - Fixed pager position when set to Top.

 - Fixed numeric pager when you had more pages than the PagerStyle.PageButtonCount.
   NewPageIndex was incorrect when passed into the PageIndexChanged() event.

 - Leaving SortExpression blank was still rendering the column headers as a link when
   AllowSort was true.  Having AllowAutoSort = true still renders the header as a link
   regardless of the Value of SortExpression.

 - Fixed exception when binding a C1ButtonColumn at runtime.

===============================================
C1WebGrid Build 1.1.20031.10  12 Dec 2002
===============================================

Enhancements/Documentation Changes
----------------------------------

Added two events to allow customization of a grouped row.

GroupText(object source, C1GroupTextEventArgs e)

Raised when the GroupInfo.HeaderText or GroupInfo.FooterText has been
set to "Custom".  This allows one to customize the text displayed for
the group header or footer.

GroupAggregate(object source, C1GroupTextEventArgs e)

Raised when C1Column.Aggregate has been set to AggregateEnum.Custom.  This
allows the customization of aggregate cell contents for text displayed in the
group header or footer.

Corrected Problems
------------------


===============================================
C1WebGrid Build 1.1.20024.9  30 Oct 2002
===============================================


INSTALLATION
============

(This is intended for troubleshooting only, as the installer should
have completed all the steps outlined below automatically.)

The text below assumes that the installation directory for the 
C1 Studio for .NET is "C:\Program Files\ComponentOne Studio.NET".

C1WebGrid ASP component is contained in the following dll:

C1.Web.C1WebGrid.dll,

The default installation folder is "C:\Program Files\ComponentOne Studio.NET\bin".

The sample applications will be placed in 
"C:\Program Files\ComponentOne Studio.NET\C1WebGrid\Samples\cs" and
"C:\Program Files\ComponentOne Studio.NET\C1WebGrid\Samples\vb" 

Two virtual directories are needed: c1webgrid_samples and c1webgrid_client.

c1webgrid_samples should point to "C:\Program Files\ComponentOne Studio.NET\C1WebGrid\Samples"
c1webgrid_client should point to "C:\Program Files\ComponentOne Studio.NET\C1WebGrid\client"

To run the samples you need to elevate the security settings for the ASPNET user. This account
should be an administrator on the local machine in order to access local databases.


Object changes since the beta
-----------------------------

The C1GridItem.Itemtype property is now of type C1ListItemType instead of ListItemType.

