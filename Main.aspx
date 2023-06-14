<%@ Register TagPrefix="rep" TagName="reportData" Src="Util/reportData.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Main.aspx.vb" Inherits="Wyeth.Alf.Main" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Main</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<LINK media="print" href="printing.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="JS/ClientScripts.js" type="text/javascript">	</script>
		<asp:literal id="_clientScript" runat="server"></asp:literal>
		<style>TD {
	FONT-SIZE: 9pt
}
		</style>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:panel id="Panel1" runat="server">
				<rep:reportData id="repData" runat="server"></rep:reportData>
				<P></P>
				<asp:DataGrid id="myGrid" GridLines="None" AutoGenerateColumns="False" Runat="server">
					<COLUMNS>
						<asp:BoundColumn DataField="ACTDAY" HeaderText="Actuals">
							<HEADERSTYLE WRAP="False" CSSCLASS="headlineSeperatorLeft"></HEADERSTYLE>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="BUDDAY" HeaderText="Budget"></asp:BoundColumn>
						<asp:BoundColumn DataField="ACTBUD" HeaderText="%">
							<HEADERSTYLE WRAP="False" CSSCLASS="headlineSeperator"></HEADERSTYLE>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="TOTALCUMDAYACT" HeaderText="Actuals"></asp:BoundColumn>
						<asp:BoundColumn DataField="BUDCUMDAY" HeaderText="Budget"></asp:BoundColumn>
						<asp:BoundColumn DataField="PROBUD" HeaderText="%">
							<HEADERSTYLE WRAP="False" CSSCLASS="headlineSeperator"></HEADERSTYLE>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="PROMONTH" HeaderText="Projected"></asp:BoundColumn>
						<asp:BoundColumn DataField="BUDGET" HeaderText="Budget"></asp:BoundColumn>
						<asp:BoundColumn DataField="ACTBUDCUM" HeaderText="%">
							<HEADERSTYLE WRAP="False" CSSCLASS="headlineSeperator"></HEADERSTYLE>
						</asp:BoundColumn>
					</COLUMNS>
				</asp:DataGrid>
				<P></P>
				<asp:DataGrid id="MyGridYear" GridLines="None" AutoGenerateColumns="False" Runat="server">
					<COLUMNS>
						<asp:BoundColumn DataField="ActYTD" HeaderText="Actuals">
							<HEADERSTYLE WRAP="False" CSSCLASS="headlineSeperatorLeft"></HEADERSTYLE>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="BYTD" HeaderText="Budget"></asp:BoundColumn>
						<asp:BoundColumn DataField="ActBudYTD" HeaderText="%">
							<HEADERSTYLE WRAP="False" CSSCLASS="headlineSeperator"></HEADERSTYLE>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="ActPro" HeaderText="Projected"></asp:BoundColumn>
						<asp:BoundColumn DataField="budyear" HeaderText="Budget"></asp:BoundColumn>
						<asp:BoundColumn DataField="ActProBudYear" HeaderText="%">
							<HEADERSTYLE WRAP="False" CSSCLASS="headlineSeperator"></HEADERSTYLE>
						</asp:BoundColumn>
					</COLUMNS>
				</asp:DataGrid>
			</asp:panel>
			<p><br>
			</p>
			<asp:panel id="ErrorPanel" runat="server">
				<DIV class="HL">System Status Sanova</DIV>
				<DIV class="reportSummary">
					<TABLE id="statusTable" cellPadding="3" width="100%" runat="server">
						<TR>
							<TD colSpan="4"><STRONG>Transmission:</STRONG><%# tran_id %><BR>
								<STRONG>Order Entry of: </STRONG>
								<%# tran_date %>
								<BR>
								<STRONG>Imported at: </STRONG>
								<%# import_date %>
								<BR>
							</TD>
						</TR>
						</SPAN>
						<TR class="tableBGColor1Class">
							<TD class="field" style="WIDTH: 156px">Last Sanova Import:</TD>
							<TD colSpan="3">
								<ASP:PLACEHOLDER id="phErrors" RUNAT="server"></ASP:PLACEHOLDER></TD>
						</TR>
						<TR class="tableBGColor1Class">
							<TD class="field" style="WIDTH: 156px">Transmisions missing:</TD>
							<TD colSpan="3">
								<asp:PlaceHolder id="phNotImportedTransmisisons" Runat="server"></asp:PlaceHolder></TD>
						</TR>
						<TR class="tableBGColor1Class">
							<TD class="field" style="WIDTH: 156px; HEIGHT: 20px">Stock&nbsp; Check:</SPAN></TD>
							<TD style="HEIGHT: 20px" colSpan="3">
								<asp:PlaceHolder id="phstockerrors" Runat="server"></asp:PlaceHolder></TD>
						</TR>
						<TR class="tableBGColor1Class">
							<TD class="field" style="WIDTH: 156px; HEIGHT: 19px">New Products:</TD>
							<TD style="HEIGHT: 19px" colSpan="3">
								<asp:PlaceHolder id="phNewProducts" Runat="server"></asp:PlaceHolder></TD>
						</TR>
						<TR class="tableBGColor1Class">
							<TD class="field" style="WIDTH: 156px">New&nbsp;Customers:</TD>
							<TD colSpan="3">
								<asp:PlaceHolder id="phNewCustomers" Runat="server"></asp:PlaceHolder></TD>
						</TR>
					</TABLE>
				</DIV>
			</asp:panel><br>
			<!--
			<asp:panel id="ErrorPanelMuenster" runat="server">
				<DIV class="HL">System Status M&uuml;nster</DIV>
				<DIV class="reportSummary">
					<TABLE id="statusTableMuenster" cellPadding="3" width="100%" runat="server">
						<TR>
							<TD colSpan="4"><STRONG>Transmission M&uuml;nster:</STRONG><%# tran_id_muenster %><BR>
								<STRONG>Order Entry of: </STRONG>
								<%# tran_date_muenster %>
								<BR>
								<STRONG>Imported at: </STRONG>
								<%# import_date_muenster %>
								<BR>
							</TD>
						</TR>
						</SPAN>
						<TR class="tableBGColor1Class">
							<TD class="field" style="WIDTH: 156px">Last M&uuml;nster Import:</TD>
							<TD colSpan="3">
								<ASP:PLACEHOLDER id="phErrorsMuenster" RUNAT="server"></ASP:PLACEHOLDER></TD>
						</TR>
						<TR class="tableBGColor1Class">
							<TD class="field" style="WIDTH: 156px">M&uuml;nster Transmisions missing:</TD>
							<TD colSpan="3">
								<asp:PlaceHolder id="phNotImportedTransmisisonsMuenster" Runat="server"></asp:PlaceHolder></TD>
						</TR>
						<TR class="tableBGColor1Class">
							<TD class="field" style="WIDTH: 156px; HEIGHT: 20px">M&uuml;nster Stock&nbsp; 
								Check:</SPAN></TD>
							<TD style="HEIGHT: 20px" colSpan="3">
								<asp:PlaceHolder id="phstockerrorsMuenster" Runat="server"></asp:PlaceHolder></TD>
						</TR>
					</TABLE>
				</DIV>
			</asp:panel> --><asp:panel id="exportStatusPanel" runat="server">
				<DIV class="HL">System Status APO Export</DIV>
				<DIV class="reportSummary">
					<TABLE id="exportStatusTable" cellPadding="3" width="100%" runat="server">
						<TR class="tableBGColor1Class">
							<TD class="field" style="WIDTH: 156px">Last APO Export:</TD>
							<TD colSpan="3"><%# lastExportDate  %></TD>
						</TR>
					</TABLE>
				</DIV>
			</asp:panel><br>
			<div class="tableBGColor1Class" align="center"><A id="lnkAbout" href="javascript:" runat="server">-- 
					About --</A></div>
		</form>
		<script language="javascript" src="js/additionalHeaderRows.js"></script>
		<script language="javascript">
		<!--
			//hinzufügen von doppelten headlines. aufgrund von bugs im c1.webgrid
			//muss man es mittels javascript lösen. 
			createRow("myGrid");
			addTableCell("myGrid", "Day", "3", "", 0);
			addTableCell("myGrid", "Month to Date", "3", "", 1);
			addTableCell("myGrid", "Month", "3", "", 2);
			
			createRow("MyGridYear");
			addTableCell("MyGridYear", "Year To Date", "3", "", 0);
			addTableCell("MyGridYear", "Year", "3", "", 1);
			
		//-->
		</script>
	</body>
</HTML>
