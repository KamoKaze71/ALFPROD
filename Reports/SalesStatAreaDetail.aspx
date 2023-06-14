<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SalesStatAreaDetail.aspx.vb" Inherits="Wyeth.Alf.SalesStatAreaDetail"%>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
    <title>Sales Statistics Detail</title>
<meta content="Microsoft Visual Studio .NET 7.1" name=GENERATOR>
<meta content="Visual Basic .NET 7.1" name=CODE_LANGUAGE>
<meta content=JavaScript name=vs_defaultClientScript>
<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema><LINK href="../Styles.css" type=text/css rel=stylesheet >
  </HEAD>
<body MS_POSITIONING="GridLayout">
<FORM ID="Form2" METHOD="post" RUNAT="server">
			<DIV CLASS="HL">
				<ASP:LABEL ID="lblPageTitle" RUNAT="server"></ASP:LABEL>
			</DIV>
			<DIV CLASS="reportsButtonBar">
					<TABLE>
						<TR>
							<TD NOWRAP><REP:REPORTDATA ID="repData" RUNAT="server" /></TD>
							<TD WIDTH="100%" ALIGN="center">
								<DIV CLASS="noprint">
									<ASP:BUTTON ID="ExportExcel" RUNAT="server" CSSCLASS="button" TEXT="Export to Excel" VISIBLE="true"></ASP:BUTTON>&nbsp;
									<BUTTON CLASS="button" ONCLICK="javascript:window.print();" TYPE="button">Print</BUTTON>&nbsp;
									<BUTTON CLASS="button" ONCLICK="javascript:window.close();" TYPE="button">Close 
										Window</BUTTON>
								</DIV> 
							</TD>
						</TR>
					</TABLE>
				</DIV>
			<table WIDTH="100%" cellspacing=6 BORDER=0><TR><TD> 
			<DIV CLASS="HL2">
				<ASP:LABEL ID="lblCustomer" RUNAT="server"></ASP:LABEL>
			</DIV>
			</TD></TR></table>
			<ASP:panel id=reportPanel CSSCLASS=reportpanel RUNAT=server>
<P></P><C1WEBGRID:C1WEBGRID id=MyGrid RUNAT="server" DEFAULTROWHEIGHT="22px" DEFAULTCOLUMNWIDTH="120px" AUTOGENERATECOLUMNS="False" ShowFooter="True" EnableViewState="False">
<COLUMNS>
<c1webgrid:C1BoundColumn DataField="tran_date" HeaderText="Date">
<GROUPINFO OUTLINEMODE="None">
</GroupInfo>
</c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="orhe_order_number" HeaderText="Order No.">
<GROUPINFO OUTLINEMODE="None">
</GroupInfo>
</c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="prod_presentation" Visible="False">
<GROUPINFO POSITION="Header" HEADERTEXT="Total {0}" OUTLINEMODE="None">
</GroupInfo>
</c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="PERCENTAGE_UNITS" Aggregate="Sum" HeaderText="Units">
<ITEMSTYLE HORIZONTALALIGN="Right">
</ItemStyle>

<FOOTERSTYLE HORIZONTALALIGN="Right">
</FooterStyle>

<GROUPINFO OUTLINEMODE="None">
</GroupInfo>
</c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="PERCENTAGE_Value" Aggregate="Sum" HeaderText="Value">
<ITEMSTYLE HORIZONTALALIGN="Right">
</ItemStyle>

<FOOTERSTYLE HORIZONTALALIGN="Right">
</FooterStyle>

<GROUPINFO OUTLINEMODE="None">
</GroupInfo>
</c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="PERCENTAGE_FGUnits" Aggregate="Sum" HeaderText="FG Units">
<ITEMSTYLE HORIZONTALALIGN="Right">
</ItemStyle>

<FOOTERSTYLE HORIZONTALALIGN="Right">
</FooterStyle>

<GROUPINFO OUTLINEMODE="None">
</GroupInfo>
</c1webgrid:C1BoundColumn>
<c1webgrid:C1BoundColumn DataField="PERCENTAGE_FGValue" Aggregate="Sum" HeaderText="FG Value">
<ITEMSTYLE HORIZONTALALIGN="Right">
</ItemStyle>

<FOOTERSTYLE HORIZONTALALIGN="Right">
</FooterStyle>

<GROUPINFO OUTLINEMODE="None">
</GroupInfo>
</c1webgrid:C1BoundColumn>
</Columns>
</C1WEBGRID:C1WEBGRID></ASP:panel></FORM>
<P></P>

  </body>
</HTML>
