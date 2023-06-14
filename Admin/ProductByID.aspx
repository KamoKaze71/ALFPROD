<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ProductByID.aspx.vb" Inherits="Wyeth.Alf.ProductByID"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ProductByID</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<SCRIPT LANGUAGE="JavaScript" SRC="../JS/ClientScripts.js"> </SCRIPT>
		<asp:literal id="_clientScript" runat="server"></asp:literal>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:panel id="EditPanel" runat="server" cssclass="EditPanel" Width="100%">
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%" border="0">
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field" style="WIDTH: 103px; HEIGHT: 22px">Product No.</TD>
						<TD style="WIDTH: 62px; HEIGHT: 22px">
							<asp:TextBox id="txtProd_PhZnr" runat="server" Width="102px" cssClass="formfield" Enabled="False"></asp:TextBox></TD>
						<TD class="field" style="WIDTH: 41px; HEIGHT: 22px" width="41">Status</TD>
						<TD colSpan="3">
							<asp:TextBox id="txtProdObsCode" runat="server" Width="128px" cssClass="formfield" Enabled="False"></asp:TextBox>
							<asp:Button id="btnObsCode" runat="server" Width="256px" CssClass="button_common" Visible="False"
								Text="Button"></asp:Button></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field" style="WIDTH: 103px">Bus Unit</TD>
						<TD style="WIDTH: 62px">
							<asp:TextBox id="txtProdBusUnit" runat="server" Width="102px" cssClass="formfield" Enabled="False"></asp:TextBox></TD>
						<TD class="field" style="WIDTH: 41px">PCR Seg.</TD>
						<TD style="WIDTH: 104px">
							<asp:TextBox id="txtProdSegment" runat="server" Width="74px" cssClass="formfield" Enabled="False"></asp:TextBox></TD>
						<TD class="field" style="WIDTH: 30px">Product Group</TD>
						<TD>
							<asp:DropDownList id="ddProdGroup" runat="server" Width="193px" cssClass="formfield" Enabled="False"></asp:DropDownList></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field" style="WIDTH: 103px" noWrap>Presentation</TD>
						<TD style="WIDTH: 200px" colSpan="2">
							<asp:TextBox id="txtProdPresentation" runat="server" Width="220px" cssClass="formfield" Enabled="False"></asp:TextBox></TD>
						<TD class="field" style="WIDTH: 104px">Description</TD>
						<TD style="WIDTH: 298px" colSpan="2">
							<asp:TextBox id="txtProdDescription" runat="server" Width="204px" cssClass="formfield" Enabled="False"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field" style="WIDTH: 103px; HEIGHT: 24px">Pack Size</TD>
						<TD style="WIDTH: 200px; HEIGHT: 24px" colSpan="2">
							<asp:TextBox id="txtProdPacksize" runat="server" Width="220px" cssClass="formfield" Enabled="False"></asp:TextBox></TD>
						<TD class="field" style="WIDTH: 104px; HEIGHT: 24px">Strength</TD>
						<TD style="HEIGHT: 24px" colSpan="2">
							<asp:TextBox id="txtProdStrength" runat="server" Width="204px" cssClass="formfield" Enabled="False"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field" style="WIDTH: 103px; HEIGHT: 21px">CC ID</TD>
						<TD style="WIDTH: 200px; HEIGHT: 21px" colSpan="2">
							<asp:TextBox id="txtProdCCId" runat="server" Width="220px" cssClass="formfield" Enabled="False"></asp:TextBox></TD>
						<TD class="field" style="WIDTH: 104px; HEIGHT: 21px">CCDescription</TD>
						<TD style="HEIGHT: 21px" colSpan="2">
							<asp:TextBox id="txtProdCCDesc" runat="server" Width="204px" cssClass="formfield" Enabled="False"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field" style="WIDTH: 103px" noWrap>WWS ItemCode</TD>
						<TD style="WIDTH: 200px" colSpan="2">
							<asp:TextBox id="txtProdWWSItemCode" runat="server" Width="220px" cssClass="formfield" Enabled="False"></asp:TextBox></TD>
						<TD class="field" style="WIDTH: 104px">FSD</TD>
						<TD colSpan="2">
							<asp:TextBox id="txtProdFSD" runat="server" Width="204px" cssClass="formfield" Enabled="False"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field" style="WIDTH: 103px">PlantItem No.</TD>
						<TD style="WIDTH: 200px" colSpan="2">
							<asp:TextBox id="txtProdPlantItemNo" runat="server" Width="220px" cssClass="formfield" Enabled="False"></asp:TextBox></TD>
						<TD class="field" style="WIDTH: 104px">Packer Code</TD>
						<TD colSpan="2">
							<asp:TextBox id="txtProdPackerCode" runat="server" Width="204px" cssClass="formfield" Enabled="False"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field" style="WIDTH: 103px" noWrap>Invoicer Code</TD>
						<TD style="WIDTH: 200px" colSpan="2">
							<asp:TextBox id="txtProdInvoicerCode" runat="server" Width="220px" cssClass="formfield" Enabled="False"></asp:TextBox></TD>
						<TD class="field" style="WIDTH: 104px">Routing</TD>
						<TD colSpan="2">
							<asp:TextBox id="txtProdRouting" runat="server" Width="204px" cssClass="formfield" Enabled="False"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field" style="WIDTH: 103px">Manufacturer</TD>
						<TD style="WIDTH: 200px" colSpan="2">
							<asp:TextBox id="txtProdManufacturer" runat="server" Width="220px" cssClass="formfield" Enabled="False"></asp:TextBox></TD>
						<TD class="field" style="WIDTH: 104px" noWrap>MfG Location</TD>
						<TD colSpan="2">
							<asp:TextBox id="txtProdMfGLocation" runat="server" Width="204px" cssClass="formfield" Enabled="False"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field" style="WIDTH: 103px; HEIGHT: 25px">Info</TD>
						<TD style="HEIGHT: 25px" colSpan="5">
							<asp:TextBox id="txtProdInfo" runat="server" Width="538px" cssClass="formfield"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD class="field" style="WIDTH: 103px; HEIGHT: 43px">Sample Product</TD>
						<TD style="WIDTH: 200px; HEIGHT: 43px" colSpan="2">
							<asp:DropDownList id="ddProdIdSampleProd" runat="server" Width="224px" cssClass="formfield"></asp:DropDownList>
							<asp:TextBox id="txtprodsampleproduct" runat="server" Width="224px" Enabled="False" CssClass="formfield"></asp:TextBox>
							<asp:Button id="btn_goto_sample" runat="server" CssClass="button_common"></asp:Button>
							<asp:TextBox id="txtprod_id_sample_prod" runat="server" Visible="False"></asp:TextBox></TD>
						<TD class="field" style="WIDTH: 104px; HEIGHT: 43px">Currency</TD>
						<TD style="HEIGHT: 43px" colSpan="2">
							<asp:DropDownList id="ddProdCurrId" runat="server" Width="210px" cssClass="formfield" Enabled="False"></asp:DropDownList></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field" style="WIDTH: 103px">Country</TD>
						<TD style="WIDTH: 200px" colSpan="2">
							<asp:DropDownList id="ddProdCtryId" runat="server" Width="193px" cssClass="formfield"></asp:DropDownList></TD>
						<TD class="field" style="WIDTH: 104px">Current TCogs</TD>
						<TD colSpan="2">
							<asp:TextBox id="txtStdCogs" runat="server" Width="74px" cssClass="formfield" Enabled="False"></asp:TextBox>
							<asp:Button id="btn_TcogsHistory" runat="server" Width="104px" CssClass="button_common" Text="View TCogs History"></asp:Button></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor1Class';">
						<TD class="field" style="WIDTH: 103px">Line</TD>
						<TD style="WIDTH: 200px" colSpan="2">
							<asp:DropDownList id="ddLineId" runat="server" Width="193px" cssClass="formfield"></asp:DropDownList></TD>
						<TD class="field" style="WIDTH: 104px">FAP</TD>
						<TD colSpan="2">
							<asp:TextBox id="txtProdFAP" runat="server" Width="114px" cssClass="formfield" Enabled="False"></asp:TextBox></TD>
					</TR>
					<TR class="tableBgColor1Class" onmouseover="this.className='tableMouseoverColor';" onmouseout="this.className='tableBgColor2Class';">
						<TD class="field" style="WIDTH: 103px">Target Product Group</TD>
						<TD style="WIDTH: 200px" colSpan="2">
							<asp:DropDownList id="ddProdTaPG" runat="server" Width="193px" cssClass="formfield"></asp:DropDownList></TD>
						<TD class="field" style="WIDTH: 104px">Unit Measure</TD>
						<TD colSpan="2">
							<asp:TextBox id="txtUnitMeasure" runat="server" Width="210px" cssClass="formfield" Enabled="False"></asp:TextBox></TD>
						</TD></TR>
				</TABLE>
				<asp:Button id="Button_update" runat="server" Width="110px" CssClass="button" Text="Update"></asp:Button>
				<BUTTON class="button" id="button_cancel" type="button" runat="server">Cancel</BUTTON>
				<asp:TextBox id="txtProdSubsegment" runat="server" Width="107px" cssClass="formfield" Enabled="False"
					Visible="False"></asp:TextBox>
				<asp:TextBox id="txtProd_id" runat="server" Width="82px" cssClass="formfield" Enabled="False"
					Visible="False"></asp:TextBox>
				<asp:TextBox id="txtProdForteProductId" runat="server" Width="108px" cssClass="formfield" Enabled="False"
					Visible="False"></asp:TextBox>
				<asp:TextBox id="txtProdStatus" runat="server" Width="185px" cssClass="formfield" Enabled="False"
					Visible="False"></asp:TextBox>
			</asp:panel></form>
	</body>
</HTML>
