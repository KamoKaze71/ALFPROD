Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.Page
Imports Oracle.DataAccess.Client
Imports System.Drawing
Imports Wyeth.Utilities
Imports Wyeth.Alf.CssStyles

Public Class MyForm
	Inherits Control


	Sub New()
		MyBase.New()
	End Sub


	Protected Overloads Sub Render(ByVal writer As HtmlTextWriter, ByRef MyDataView As DataView)

		Dim strForm, strMouseOver As String


		Dim MyCols As DataColumnCollection
		Dim MyCol As DataColumn


		strForm = "<asp:panel id='EditPanel' style='Z-INDEX: 103; LEFT: 8px; POSITION: absolute; TOP: 40px' runat='server' Height='100%' width=100% Visible='False'>"
		strForm += "  <table border=0 cellpadding=2 cellspacing=0 width=100%>"

		'<tr class=tableBgColor2Class onMouseOver="this.className='tableMouseoverColor';" onMouseOut="this.className='tableBgColor2Class';"><td class=field>Name</td><td><img src="images/trans.gif" alt="" width="10" height="1" border="0"></td><td width="100%"><asp:TextBox id="txtMenuName" runat="server" cssClass=formfield ></asp:TextBox></td></tr>
		'<tr class=tableBgColor1Class onMouseOver="this.className='tableMouseoverColor';" onMouseOut="this.className='tableBgColor1Class';"><td class=field>Label</td><td><img src="images/trans.gif" alt="" width="10" height="1" border="0"></td><td width="100%"><asp:TextBox id="txtMenuLabel" runat="server" cssClass=formfield></asp:TextBox></td></tr>
		'<tr class=tableBgColor2Class onMouseOver="this.className='tableMouseoverColor';" onMouseOut="this.className='tableBgColor2Class';"><td class=field>Link</td><td><img src="images/trans.gif" alt="" width="10" height="1" border="0"></td><td width="100%"><asp:TextBox id="txtMenuLink" runat="server" cssClass=formfield></asp:TextBox></td></tr>
		'<tr class=tableBgColor1Class onMouseOver="this.className='tableMouseoverColor';" onMouseOut="this.className='tableBgColor1Class';"><td class=field>Target</td><td><img src="images/trans.gif" alt="" width="10" height="1" border="0"></td><td width="100%"><asp:TextBox id="txtMenuTarget" runat="server" cssClass=formfield></asp:TextBox></td></tr>
		'<tr class=tableBgColor2Class onMouseOver="this.className='tableMouseoverColor';" onMouseOut="this.className='tableBgColor2Class';"><td class=field>Display No.</td><td><img src="images/trans.gif" alt="" width="10" height="1" border="0"></td><td width="100%"><asp:TextBox id="txtMenuDisplayNo" runat="server" cssClass=formfield></asp:TextBox></td></tr>
		'<tr class=tableBgColor1Class onMouseOver="this.className='tableMouseoverColor';" onMouseOut="this.className='tableBgColor1Class';"><td class=field>Category</td><td><img src="images/trans.gif" alt="" width="10" height="1" border="0"></td><td width="100%"><asp:TextBox id="txtMenuCategory" runat="server" cssClass=formfield></asp:TextBox></td></tr>
		'<tr class=tableBgColor2Class onMouseOver="this.className='tableMouseoverColor';" onMouseOut="this.className='tableBgColor2Class';"><td class=field>Parent ID</td><td><img src="images/trans.gif" alt="" width="10" height="1" border="0"></td><td width="100%"><asp:TextBox id="txtMenuIDParent" runat="server" cssClass=formfield></asp:TextBox></td></tr>
		'<tr class=tableBgColor1Class onMouseOver="this.className='tableMouseoverColor';" onMouseOut="this.className='tableBgColor1Class';"><td colspan=3 align=right><asp:Button id="Button_Update" runat="server" Text="Update" cssClass=button_common></asp:Button>

		'strMouseOver= "class=tableBgColor2Class onMouseOver="this.className='tableMouseoverColor';" onMouseOut="this.className='tableBgColor2Class';"
		strMouseOver = "class=tableBgColor2Class onMouseOver='this.className='tableMouseoverColor';' onMouseOut='this.className='tableBgColor2Class';'"

		MyCols = MyDataView.Table.Columns()

		For Each MyCol In MyCols



			strForm += "<tr><td class=field>Name</td><td><img src='images/trans.gif' alt='' width=10 height=1 border=0></td><td width='100%'>"
			strForm += "<asp:TextBox id='txt" & MyCol.ColumnName.ToString & " runat='server' cssClass=formfield ></asp:TextBox></td></tr>"


		Next

		strForm += "<asp:Button id='Button_Insert' runat='server' Text='Insert' cssClass=button_common></asp:Button>"
		strForm += "<asp:Button id='Button_Cancel' runat='server' Text='Cancel' cssClass=button_common></asp:Button></td></tr>"
		strForm += "</table>"
		strForm += "</asp:panel>"








		writer.Write(strForm)

	End Sub
End Class
