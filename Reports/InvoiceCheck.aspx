<%@ Register TagPrefix="box" Namespace="Wyeth.Utilities" Assembly="Utilities" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Page Language="vb" AutoEventWireup="False" Codebehind="InvoiceCheck.aspx.vb" Inherits="Wyeth.Alf.InvoiceCheck" smartNavigation="False"%>
<%@ Register TagPrefix="rep" TagName="reportData" Src="../Util/reportData.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>InvoiceCheck</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../JS/ClientScripts.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<DIV class="HL"><ASP:LABEL id="lblPageTitle" RUNAT="server"></ASP:LABEL></DIV>
			<ASP:PANEL ID="FilterPanel" RUNAT="server" CSSCLASS="FilterPanel" WIDTH="100%">
				<TABLE style="FLOAT: none" cellSpacing="0" cellPadding="0" width="100%" align="right">
					<TR align="right">
						<TD class="field" noWrap>
							<DIV id="startDate1" RUNAT="server">Start Date:</DIV>
						</TD>
						<TD>
							<DIV id="startDate3" RUNAT="server">
								<BOX:WYETHTEXTBOX id="txtStartDate" RUNAT="server" TOOLTIP="Please enter a date" FRIENDLYNAME="Report Start Date"
									ALLOWBLANK="false" FIELDTYPE="DATE"></BOX:WYETHTEXTBOX></DIV>
						</TD>
						<TD>
							<DIV id="startDate2" RUNAT="server"><A onclick="OpenDatePopUp('Form1.txtStartDate');"><IMG src="/Images/kalender.gif" border="0"></A></DIV>
						</TD>
						<TD class="field" noWrap>
							<DIV id="EndDate1" RUNAT="server">End Date:</DIV>
						</TD>
						<TD>
							<DIV id="EndDate2" RUNAT="server">
								<BOX:WYETHTEXTBOX id="txtEndDate" RUNAT="server" TOOLTIP="Please enter a date" FRIENDLYNAME="Report End Date"
									ALLOWBLANK="false" FIELDTYPE="DATE"></BOX:WYETHTEXTBOX></DIV>
						</TD>
						<TD>
							<DIV id="EndDate3" RUNAT="server"><A onclick="OpenDatePopUp('Form1.txtEndDate');"><IMG src="/Images/kalender.gif" border="0"></A></DIV>
						</TD>
						<TD>
							<ASP:DROPDOWNLIST id="ddInvoiceSelect" RUNAT="server" WIDTH="122px" AutoPostBack="True"></ASP:DROPDOWNLIST></TD>
						<TD>
							<ASP:DROPDOWNLIST id="ddDistribSelect" RUNAT="server" WIDTH="78px"></ASP:DROPDOWNLIST></TD>
						<TD>
							<ASP:DROPDOWNLIST id="ddLineSelect" RUNAT="server"></ASP:DROPDOWNLIST></TD>
					</TR>
				</TABLE>
				<DIV class="reportsButtonBar">
					<TABLE id="Table1" width="100%">
						<TR>
							<TD noWrap>
								<REP:REPORTDATA id="repData" RUNAT="server"></REP:REPORTDATA></TD>
							<TD align="center" width="100%">
								<DIV class="noprint">
									<ASP:BUTTON id="btnGenRep" RUNAT="server" CSSCLASS="button" TEXT="Generate Report" CausesValidation="False"></ASP:BUTTON>&nbsp;
									<ASP:BUTTON id="btn_add_git" RUNAT="server" CSSCLASS="button" TEXT="Add new GIT" CAUSESVALIDATION="False"></ASP:BUTTON>&nbsp;<BUTTON class="button" onclick="javascript:window.print();" type="button">Print 
										Report</BUTTON></DIV>
							</TD>
						</TR>
					</TABLE>
				</DIV>
			</ASP:PANEL>
			<asp:panel id="GridPanel" runat="server" CssClass="GridPanel">
				<C1WEBGRID:C1WEBGRID id="MyGrid" runat="server" DefaultRowHeight="22px" DefaultColumnWidth="120px" OnItemCommand="MyGrid_ItemCommand"
					AutoGenerateColumns="False" Width="100%">
					<Columns>
						<c1webgrid:C1BoundColumn Visible="False" HeaderText="ID" DataField="stoc_id"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Visible="False" HeaderText="ID" DataField="prod_id"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Beweg KZ" DataField="CODE_CODE"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Prod. No." DataField="prod_phznr"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Presentation" DataField="PROD_PRESENTATION"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Date" DataField="STOC_DATE_STOCK">
							<ItemStyle Wrap="False"></ItemStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Order No." DataField="STOC_ORDER_NUMBER"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Wyeth Invoice Number" DataField="STOC_INVOICE_NUMBER"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Units" DataField="STOC_UNIT" DataFormatString="{0:0}">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Visible="False" HeaderText="Currency" DataField="currency_invoice">
							<HeaderStyle Width="50px"></HeaderStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Invoice Value" DataField="STOC_INVOICE_VALUE" DataFormatString="{0:#,##0.00}">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Visible="False" HeaderText="Currency Accrued" DataField="currency_accrued">
							<HeaderStyle Width="50px"></HeaderStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Accrued  Value" DataField="STOC_ACCRUED_VALUE" DataFormatString="{0:#,##0.00}">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Comment" DataField="STOC_COMMENT"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1ButtonColumn CommandName="Select" Visible="False" ButtonType="PushButton" Text="Select"></c1webgrid:C1ButtonColumn>
						<c1webgrid:C1BoundColumn Visible="False" DataField="CURR_ID_INVOICE"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Visible="False" DataField="CURR_ID_ACCRUED"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="GIT Difference" DataField="STOC_DIFF_VALUE" DataFormatString="{0:#,##0.00}">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="Accrued Difference" DataField="STOC_DIFF_VALUE_ACCRUED" DataFormatString="{0:#,##0.00}">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Visible="False" HeaderText="GIT ID" DataField="STOC_ID_GIT"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn Visible="False" DataField="stoc_date_correct"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn HeaderText="GIT" DataField="stoc_id_git">
							<HeaderStyle Width="30px"></HeaderStyle>
						</c1webgrid:C1BoundColumn>
					</Columns>
				</C1WEBGRID:C1WEBGRID>
			</asp:panel><ASP:PANEL id="EditPanelGIT" RUNAT="server" CSSCLASS="EditPanel" VISIBLE="False">
				<DIV class="HL">
					<ASP:LABEL id="lblEditGIT" RUNAT="server">Edit GIT</ASP:LABEL></DIV>
				<TABLE cellSpacing="0" cellPadding="2" width="100%" border="0">
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field">Stock Date:</TD>
						<TD colSpan="3">
							<BOX:WYETHTEXTBOX id="txtstockdateGIT" RUNAT="server" CSSCLASS="formfield" Enabled="False" FieldType="DATE"></BOX:WYETHTEXTBOX></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field">Product:</TD>
						<TD>
							<ASP:DROPDOWNLIST id="ddProductSelect" RUNAT="server" AutoPostBack="True" Width="345px"></ASP:DROPDOWNLIST>
							<asp:TextBox id="txtstock_id_git" runat="server" visible="False"></asp:TextBox></TD>
						<TD class="field" style="WIDTH: 63px">StdCogs:</TD>
						<TD>
							<asp:Label id="lblstdcogs" runat="server"></asp:Label></TD>
					</TR>
					<TR>
						<TD class="field" style="HEIGHT: 24px">Wyeth Invoice No:</TD>
						<TD colSpan="3">
							<BOX:WYETHTEXTBOX id="txtwyethInvoiceNo_git" RUNAT="server" CSSCLASS="formfield"></BOX:WYETHTEXTBOX></TD>
					</TR>
					<TR>
						<TD class="field" style="HEIGHT: 24px">Order No.:</TD>
						<TD colSpan="3">
							<BOX:WYETHTEXTBOX id="txtOrderNo_git" RUNAT="server" CSSCLASS="formfield"></BOX:WYETHTEXTBOX></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field">Units:</TD>
						<TD colSpan="3">
							<BOX:WYETHTEXTBOX id="txtunits_git" RUNAT="server" CSSCLASS="formfield" AutoPostBack="True" FieldType="INTEGER"></BOX:WYETHTEXTBOX></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field">Invoice Value:</TD>
						<TD>
							<BOX:WYETHTEXTBOX id="txtInvoiceValue_git" RUNAT="server" WIDTH="232px" CSSCLASS="formfield" FieldType="DOUBLE"></BOX:WYETHTEXTBOX></TD>
						<TD class="field" style="WIDTH: 63px">Currency</TD>
						<TD>
							<ASP:DROPDOWNLIST id="ddInvoiceCurrencyIDGIT" RUNAT="server" WIDTH="115px" CSSCLASS="formfield"></ASP:DROPDOWNLIST></TD>
					</TR>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field">Comment:</TD>
						<TD colSpan="3">
							<ASP:TEXTBOX id="txtComment_git" RUNAT="server" CSSCLASS="formfield" Height="39px" Rows="2"></ASP:TEXTBOX></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD colSpan="4">
							<ASP:BUTTON id="btn_update_git" RUNAT="server" CSSCLASS="Button" TEXT="Update" CausesValidation="False"></ASP:BUTTON>
							<ASP:BUTTON id="btn_insert_git" RUNAT="server" CSSCLASS="Button" TEXT="Insert" CausesValidation="False"></ASP:BUTTON>
							<ASP:BUTTON id="btn_delete_git" RUNAT="server" CSSCLASS="Button" TEXT="Delete" CausesValidation="False"></ASP:BUTTON>
							<ASP:BUTTON id="btn_cancel_git" RUNAT="server" CSSCLASS="Button" TEXT="Cancel" CAUSESVALIDATION="False"></ASP:BUTTON></TD>
						<TD></TD>
					</TR>
				</TABLE>
			</ASP:PANEL>
			<asp:panel id="EditPanel" runat="server" CssClass="EditPanel" Visible="False">
				<DIV class="HL">
					<ASP:LABEL id="lblEditWE" RUNAT="server">Edit WE</ASP:LABEL></DIV>
				<TABLE cellSpacing="0" cellPadding="2" width="100%" border="0">
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field">Stock Date:</TD>
						<TD colSpan="3">
							<ASP:TEXTBOX id="txtStockDateStock" RUNAT="server" CSSCLASS="formfield" ENABLED="False"></ASP:TEXTBOX></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field">PhzNr:</TD>
						<TD colSpan="3">
							<asp:TextBox id="txtprod_id" runat="server" visible="False"></asp:TextBox>
							<asp:TextBox id="txtstock_id" runat="server" visible="False"></asp:TextBox>
							<asp:TextBox id="txt_stock_id_git" runat="server" visible="False"></asp:TextBox>
							<asp:TextBox id="txtphznr" runat="server" Enabled="False" cssClass="formfield"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field">Presentation:</TD>
						<TD>
							<asp:TextBox id="txtPresentation" runat="server" Width="358px" Enabled="False" cssClass="formfield"></asp:TextBox></TD>
						<TD class="field" colSpan="3">StdCogs:
							<asp:Label id="lblSTdCogsWE" runat="server"></asp:Label></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field">Order No.:</TD>
						<TD colSpan="3">
							<box:WyethTextBox id="txtOrderNo" runat="server" cssClass="formfield" AllowBlank="True" ToolTip=""></box:WyethTextBox></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field">Wyeth Invoice No:</TD>
						<TD colSpan="3">
							<asp:TextBox id="txtInvoiceNumber" runat="server" cssClass="formfield"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field">Units:</TD>
						<TD colSpan="3">
							<BOX:WYETHTEXTBOX id="txtInvoiceUnits" runat="server" Enabled="False" FieldType="INTEGER" cssClass="formfield"></BOX:WYETHTEXTBOX></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field">Invoice Value:</TD>
						<TD>
							<asp:TextBox id="txtInvoiceValue" runat="server" Width="168px" Enabled="False" cssClass="formfield"></asp:TextBox>
							<asp:Button id="Button1" runat="server" CssClass="button" Width="176px" Text="Recalculate Invoice Value"></asp:Button></TD>
						<TD class="field">Invoice Currency</TD>
						<TD>
							<ASP:DROPDOWNLIST id="ddInvoiceCurrencyid" RUNAT="server" WIDTH="115px" CSSCLASS="formfield"></ASP:DROPDOWNLIST></TD>
					</TR>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field">Invoice Value accrued:</TD>
						<TD style="WIDTH: 320px">
							<asp:TextBox id="txtValueAccrued" runat="server" Width="168px" Enabled="False" cssClass="formfield"></asp:TextBox></TD>
						<TD class="field">Accrued Currency</TD>
						<TD>
							<ASP:DROPDOWNLIST id="ddAccruedCurrencyID" RUNAT="server" CSSCLASS="formfield" Width="116px"></ASP:DROPDOWNLIST></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field">
							<asp:Label id="lblDiff" runat="server" CssClass="form" Width="73px"></asp:Label></TD>
						<TD>
							<ASP:TEXTBOX id="txtDiff" RUNAT="server" WIDTH="232px" CSSCLASS="formfield" Enabled="False"></ASP:TEXTBOX>
							<ASP:TEXTBOX id="txtDiffValueAccrued" RUNAT="server" WIDTH="232px" CSSCLASS="formfield" Enabled="False"></ASP:TEXTBOX></TD>
						<TD><INPUT id="txtdiffhidden" type="hidden" RUNAT="server"><INPUT id="txtInvoiceValueHidden" type="hidden" name="txtInvoiceValueHidden" RUNAT="server"></TD>
						<TD></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field" style="HEIGHT: 26px">Comment:</TD>
						<TD style="HEIGHT: 26px" colSpan="3">
							<asp:TextBox id="txtComment" runat="server" Height="40px" Rows="2" cssClass="formfield"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD colSpan="3">
							<asp:Button id="Button_Update" runat="server" CausesValidation="False" CssClass="Button" Text="Update"></asp:Button>
							<asp:Button id="Button_Cancel" runat="server" CausesValidation="False" CssClass="Button" Text="Cancel"></asp:Button></TD>
						<TD>
							<ASP:BUTTON id="BTN_GIT_ASSIGN" RUNAT="server" CSSCLASS="button" TEXT="Assign GIT" CausesValidation="False"></ASP:BUTTON>
							<ASP:BUTTON id="btn_cancel_git_assignment" RUNAT="server" CSSCLASS="button" TEXT="Remove GIT Assignment"
								CausesValidation="False"></ASP:BUTTON></TD>
					</TR>
				</TABLE>
			</asp:panel></form>
	</body>
</HTML>
