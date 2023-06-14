Imports Wyeth.Utilities

Imports Oracle.DataAccess.Client
Imports System.Xml

Public Class KarmaSoftMenu

	Public Sub New()
		MyBase.New()
	End Sub

	Public Function CreateMenu() As String
		Dim doc As XmlDocument = New XmlDocument
		Dim menu, group, item As XmlElement


		' Create the Menu node (Root)
		menu = doc.CreateElement("Menu")
		menu.SetAttribute("ScriptPath", "/Scripts")
		menu.SetAttribute("CssFile", "HorizontalFrame.css")
		menu.SetAttribute("ImagePath", "/Images/Images/ultimatemenu")
		menu.SetAttribute("Layout", "Horizontal")
		menu.SetAttribute("MenuItemWidth", "120")
		menu.SetAttribute("SourceFRame", "header")
		menu.SetAttribute("TargetFrame", "main")
		menu.SetAttribute("UseArrowForNoOnClick", "False")
		menu.SetAttribute("MenuCssClass", "HorizontalFrameMenu")
		menu.SetAttribute("DefaultGroupCssClass", "HorizontalFrameGroup")
		menu.SetAttribute("MenuItemCssClass", "HorizontalFrameMenuItem")
		menu.SetAttribute("MenuItemOverCssClass", "HorizontalFrameMenuItemOver")

		menu.SetAttribute("DefaultItemCssClass", "HorizontalFrameItem")
		menu.SetAttribute("DefaultItemOverCssClass", "HorizontalFrameItemOver")

		menu.SetAttribute("DefaultScrollArrowCssClass", "HorizontalFrameItem")
		menu.SetAttribute("DefaultScrollArrowOverCssClass", "HorizontalFrameMenuItemOver")

		menu.SetAttribute("DefaultDisabledScrollArrowCssClass", "HorizontalFrameDisabledItem")
		menu.SetAttribute("DefaultDisabledItemCssClass", "HorizontalFrameDisabledItem")

		menu.SetAttribute("MenuItemWidth", "70")
		menu.SetAttribute("DefaultItemHeight", "18")

		menu.SetAttribute("MenuItemClickCssClass", "HorizontalFrameItemClick")
		menu.SetAttribute("DefaultDisabledItemCssClass", "HorizontalFrameDisabledItem")
		menu.SetAttribute("MenuItemWidth", "70")
		doc.AppendChild(menu)


		' Create the top Group node
		group = doc.CreateElement("MenuGroup")
		doc.ChildNodes(0).AppendChild(group)

		' Get topmenu items
		Dim connTop As New MyConnection
		Dim cmdText As String = "select menu_id as ItemID, menu_label  as ItemName from t_Menu where menu_id_Parent=0"
		Dim cmdTop As New OracleCommand(cmdText, connTop.Open)
		Dim readerTop As OracleDataReader
		readerTop = cmdTop.ExecuteReader()

		' Trace topmenu items and its submenus
		Dim i As Integer = 0
		While readerTop.Read()
			item = doc.CreateElement("Item")
			item.SetAttribute("ID", "Item" & readerTop("ItemID").ToString())
			item.SetAttribute("Caption", readerTop("ItemName").ToString())
			item.SetAttribute("Filter", "Shadow")
			item.SetAttribute("Filter_Shadow_Strength", "3")

			doc.ChildNodes(0).ChildNodes(0).AppendChild(item)

			group = doc.CreateElement("Group")
			group.SetAttribute("Layout", "Vertical")
			doc.ChildNodes(0).ChildNodes(0).ChildNodes(i).AppendChild(group)
			Dim connItem As New MyConnection
			connItem.Open()
			cmdText = "select menu_id as ItemID,menu_label ItemName, menu_link as URL from t_Menu where menu_id_Parent=" & readerTop("ItemID").ToString() & ""
			Dim cmdItem = New OracleCommand(cmdText, connItem.Open())
			Dim readerItem As OracleDataReader
			readerItem = cmdItem.ExecuteReader()
			While readerItem.Read()
				item = doc.CreateElement("Item")
				item.SetAttribute("ID", "Item" + readerItem("ItemID").ToString())
				item.SetAttribute("Caption", readerItem("ItemName").ToString())
				item.SetAttribute("URL", readerItem("URL").ToString())
				item.SetAttribute("Filter", "Shadow")
				item.SetAttribute("Filter_Shadow_Strength", "3")

				doc.ChildNodes(0).ChildNodes(0).ChildNodes(i).ChildNodes(0).AppendChild(item)
			End While
			readerItem.Close()
			connItem.Close()
			i = i + 1
		End While

		readerTop.Close()
		connTop.Close()

		doc.Save("C:\ALFPROD\Menu.xml")

		Return doc.OuterXml
	End Function
End Class
