<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid, Version=1.1.20034.32, Culture=neutral, PublicKeyToken=589f1fc067ff4031" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SalesRepTaPrGr.aspx.vb" Inherits="Wyeth.Alf.SalesRepTaPrGr"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>SalesRepTaPrGr</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
	<script language="JavaScript" src="/JS/ClientScript.js"> </script>
	
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="FormTargets" method="post" runat="server">
				  	<asp:Label id="lblPageTitle" runat="server" Width="100%" CssClass="lblPageTitle"></asp:Label>
						
			<asp:panel  id="EditPanel" runat="server" Visible=false CssClass="EditPanel">
				<asp:label id="lblTapg_descr" runat="server" CssClass=head Width=100% Font-Size=larger></asp:label>
				<asp:label id="lbltapgid" runat="server" visible=False ></asp:label>
				
				<table width="100%">
							<tr height="20px" class="tableBgColor2Class">
								<td width="200px"></td>
								<td align=center class="tableBgColor2Class">Q1</td>
								<td align=center class="tableBgColor2Class">Q2</td>
								<td align=center class="tableBgColor2Class">Q3</td>
								<td align=center class="tableBgColor2Class">Q4</td>
							</tr>
				<asp:Repeater id="TargetRepeater" runat="server" EnableViewState="True">
					<ItemTemplate>
						
							
							<tr class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
								<td class="tableBgColor2Class>
									<asp:TextBox visible=False runat=server id="txtsare_id" text='<%# DataBinder.Eval(Container.DataItem, "SARE_ID")%>' >
									</asp:TextBox>
									<asp:TextBox visible=False runat=server id="txttapg_id" text='<%# DataBinder.Eval(Container.DataItem, "tapg_ID")%>' >
									</asp:TextBox>
									<asp:label id=SalesrepName CssClass="field" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SARE_LAST_NAME")& "  " & DataBinder.Eval(Container.DataItem, "SARE_FIRST_NAME") %>'>
									</asp:label>
								</td>
														
								<td align=right>
									<asp:TextBox id="txtQ1"  runat="server" text='<%# DataBinder.Eval(Container.DataItem, "q1") %>' EnableViewState=false CssClass="formfield">
									</asp:TextBox>
								</td>
							
								<td align=right>
									<asp:TextBox id="txtQ2"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"q2") %>' EnableViewState=false CssClass="formfield">
									</asp:TextBox>
								</td>
								<td align=right>
									<asp:TextBox id="txtQ3"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"q3") %>' EnableViewState=false CssClass="formfield">
									</asp:TextBox>
								</td>
								<td align=right>
									<asp:TextBox id="txtQ4"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"q4") %>' EnableViewState=false CssClass="formfield">
									</asp:TextBox>
								</td>
							</tr>
						
					</ItemTemplate>
					
				</asp:Repeater>
				<tr height="20px" class="headline">
								<td width="200px"></td>
								<td align=right><asp:label id="lblSum1" CssClass="field" runat="server" Text='<%#Sum1 %>' ></asp:label></td>
								<td align=right><asp:label id="lblSum2" CssClass="field" runat="server" Text='<%#Sum2 %>'></asp:label></td>
								<td align=right><asp:label id="lblSum3" CssClass="field" runat="server" Text='<%#Sum3 %>'></asp:label></td>
								<td align=right><asp:label id="lblSum4" CssClass="field" runat="server" Text='<%#Sum4 %>'></asp:label></td>
							</tr>
				</table>
					  <table cellspacing=2 cellpadding=2 border=0><tr><td>
				<asp:Button id="Button_save_editpanel" runat="server" Width="110px" CssClass="Button_common" Text="Update"></asp:Button>
				<asp:Button id="Cancel" runat="server" Width="110px" CssClass="Button_common" Text="Cancel"></asp:Button>
			</td></tr></table>
			</asp:panel>
			
			<asp:panel id="GridPanel"  cssclass="GridPanel" Runat=server>
				<c1webgrid:c1webgrid id="MyGrid" runat="server" AutoGenerateColumns="False"></c1webgrid:c1webgrid>
				<asp:button id="Button_save_grid" runat="server" Width="110px" CssClass="Button_common" Text="Update"></asp:button>
			</asp:panel></form>
	</body>
</HTML>
