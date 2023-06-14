<%@ Register TagPrefix="IntranetMenu" Namespace="wyeth.alf" Assembly="alf" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="leftnavi.aspx.vb" Inherits="Wyeth.Alf.leftnavi"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>leftnavi</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="index_Style.css" type="text/css" rel="stylesheet">
		<script language="javascript">	
	var currNavNum;
var currLinkID;
var linkID;

function expander(strNode, strChild)
{
	try //first time curMenu has no value
	{
		currLinkID.className = "navlink";
	}
	catch(e) { }

	linkID = document.getElementById("link_" + strNode);
	linkID.className = "navlinkSelected";
	currLinkID = linkID;

	try //hide current menu block - first time curMenu has no value
	{
				// only hide current block if new item is higher level than current
		if (currNavNum.length >= strNode.length)
		{
			//if collapsing items, loop to collapse parent items too
			while (currNavNum.length >= 4)
			{
			// don't hide parent block if new child is in same block
				if (strNode.substr(0,currNavNum.length) != currNavNum)
				{
				document.getElementById("grp_"+currNavNum).style.display = 'none';
				document.getElementById("img_"+currNavNum).src = "images/navClosed.gif";
				}
				currNavNum = currNavNum.substr(0,currNavNum.length - 2);
			}
		}
	}
	catch(e) {}

	// if this menu item has a sub-block associated with it, display this
	if (typeof(strChild) != "undefined")
	{
		try
		{
		document.getElementById("grp_"+strChild).style.display = '';
		currNavNum = strChild;
		document.getElementById("img_"+strNode).src = "images/navOpen.gif?";
		}
		catch(e)
		{
			alert("Error: " + e.description + " = Menu IDs missing!!");
		}
	}
}
		function hideMe(act) {
			if (act == 'close') {
				top.document.all.menu.cols="15,*";
				myMenu.style.display = "none";
				closeButton.style.display = "none";
				openButton.style.display = "block";
			} else {
				top.document.all.menu.cols="180,*";
				myMenu.style.display = "block";
				closeButton.style.display = "block";
				openButton.style.display = "none";
			}
		}
		//resize the frame to real size because maybe its resized
		top.document.all.menu.cols="180,*";
		</script>
		<LINK href="index_style.css" type="text/css" rel="stylesheet">
		<style>
			.button_common {
				border-left:solid 0px;
				height:250px;
				font-weight:bold;
				color:#999;
				font-family:tahoma;
				font-size:6pt;
			}
		</style>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
		<table height=100%>
		<tr>
			<td valign=top>
				<div id="myMenu">
				<INTRANETMENU:INTRANETMENU id="Menu1" runat="server" MenuCtryID="56"></INTRANETMENU:INTRANETMENU>
				</div>
			</td>
		</tr>
		<tr>
			<td valign=middle align=center>
				<div style="background-color:#DDD;" align=center>
					<div id=closeButton>
						<button class="button_common" onclick="javascript:hideMe('close');" title="Hide Menu better report-view"><</button>	
					</div>
					<div id=openButton style="display:none;">
						<button class="button_common" onclick="javascript:hideMe('open');" title="View Menu">></button>	
					</div>
				</div>
			</td>
		</tr>
		</table>
			
		</form>
	</body>
</HTML>
