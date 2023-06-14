<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Page Language="vb" AutoEventWireup="False" Codebehind="CoCustomer.aspx.vb" Inherits="Wyeth.Alf.Customercop"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Customer</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../JS/ClientScripts.js"> </script>
	</HEAD>
	<body xmlns:c1webgrid="urn:http://www.componentone.com/schemas/c1webgrid">
		<form id="Form" method="post" runat="server">
			<DIV class="HL"><asp:label id="lblPageTitle" runat="server" Width="100%"></asp:label></DIV>
			<div class="reportsButtonBar" id="FilterPanel" runat="server">
				<TABLE cellPadding="2" width="95%">
					<TR>
						<TD style="WIDTH: 54px">Keyword:</TD>
						<TD style="WIDTH: 231px"><asp:textbox id="txtsearch" runat="server" CSSCLASS="formfield"></asp:textbox></TD>
						<TD><asp:dropdownlist id="ddDistribSelect" runat="server"></asp:dropdownlist><asp:dropdownlist id="ddCustGroupFilter" runat="server"></asp:dropdownlist></TD>
						<TD><ASP:BUTTON id="ShowCustomer" CSSCLASS="button" RUNAT="server" WIDTH="110px" TEXT="Search"></ASP:BUTTON></TD>
					</TR>
				</TABLE>
			</div>
			<ASP:LABEL id="lbDownloadArea" RUNAT="server" WIDTH="100%" CssClass="success">Enter a Keyword to search for Customers <br> or leave Keyword blank to get all Customers</ASP:LABEL><asp:panel id="GridPanel" runat="server" Width="95%" CssClass="GridPanel">
				<C1WEBGRID:C1WEBGRID id="MyGrid" runat="server" Width="99%" AutoGenerateColumns="False" DefaultColumnWidth="120px"
					DefaultRowHeight="22px" AllowAutoSort="True" AllowSorting="true">
					<Columns>
						<c1webgrid:C1BoundColumn DataField="cust_id" Visible="False" SortExpression="cust_id" ReadOnly="True" HeaderText="Customer ID"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="cudi_customer_nr" SortExpression="cudi_customer_nr" ReadOnly="True" HeaderText="Customer No."></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="cust_name" SortExpression="cust_name" ReadOnly="True" HeaderText="Customer Name"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="cust_department" SortExpression="cust_department" HeaderText="Department"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="cust_street" SortExpression="cust_street" ReadOnly="True" HeaderText="Address"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="cust_zip" SortExpression="cust_zip" ReadOnly="True" HeaderText="ZIP"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="cust_city" SortExpression="cust_city" ReadOnly="True" HeaderText="City"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="cugr_id" SortExpression="cugr_id" ReadOnly="True" visible="false"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1BoundColumn DataField="cust_wyeth_name" SortExpression="cust_wyethname" ReadOnly="True" visible="false"></c1webgrid:C1BoundColumn>
						<c1webgrid:C1ButtonColumn Visible="False" Text="Button" CommandName="Row"></c1webgrid:C1ButtonColumn>
					</Columns>
				</C1WEBGRID:C1WEBGRID>
			</asp:panel><asp:panel id="EditPanel" runat="server" Width="99%" CssClass="EditPanel" Visible="False">
				<TABLE id="Table2" cellSpacing="2" cellPadding="2" width="100%" border="0">
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field" width="120">Cust. No.</TD>
						<TD colSpan="3">
							<asp:TextBox id="txtCustID" runat="server" CssClass="formField" Visible="False" Enabled="False"></asp:TextBox>
							<asp:TextBox id="txtCudiNr" runat="server" CssClass="formField" Visible="true" Enabled="False"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field">Name</TD>
						<TD colSpan="3">
							<asp:TextBox id="txtCustName" runat="server" CssClass="formField" Enabled="False"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field">Department</TD>
						<TD colSpan="3">
							<asp:TextBox id="txtCustDepartment" runat="server" CssClass="formField" Enabled="False"></asp:TextBox></TD>
					</TR>
					<TR  class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field">Short Name</TD>
						<TD colSpan="3">
							<asp:TextBox id="txtwyethName" runat="server" CssClass="formField" Enabled="True"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field">Address</TD>
						<TD colSpan="3">
							<asp:TextBox id="txtCustAddress" runat="server" CssClass="formField" Enabled="False"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor2Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field">City</TD>
						<TD>
							<asp:TextBox id="txtCustCity" runat="server" CssClass="formField" Enabled="False"></asp:TextBox></TD>
						<TD class="field" style="WIDTH: 35px" width="35">ZIP</TD>
						<TD>
							<asp:TextBox id="txtCustZip" runat="server" CssClass="formField" Enabled="False"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field">Group:</TD>
						<TD>
							<asp:DropDownList id="ddCustGroup" runat="server" Width="336px"></asp:DropDownList></TD>
					</TR>
				</TABLE>
				<TABLE id="Table3" cellPadding="10" width="90%">
					<TR>
						<TD>
							<asp:Button id="Button_Insert" tabIndex="1" runat="server" Width="110px" CssClass="button" Enabled="False"
								Text="Insert"></asp:Button>
							<asp:Button id="Button_Update" tabIndex="2" runat="server" Width="110px" CssClass="button" Text="Update"></asp:Button>
							<asp:Button id="Button_Cancel" tabIndex="3" runat="server" Width="110px" CssClass="button" Text="Cancel"
								CommandName="Cancel" EnableViewState="False" CausesValidation="False"></asp:Button></TD>
					</TR>
				</TABLE>
			</asp:panel><input id="inpID" type="hidden" name="inpID" runat="server">
		</form>
	</body>
</HTML>
