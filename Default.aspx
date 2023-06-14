<%@ Page CodeBehind="Default.aspx.vb" Language="vb" AutoEventWireup="false" Inherits="Wyeth.Alf._Defaultcopy" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Frameset//EN">
<HTML>
  <HEAD><TITLE>ALF 2</TITLE>

	<script language="JavaScript">
		//frame checker
		var correct_frame = 0 + (parent.header ? 1 : 0);
		if (correct_frame == 1) {
			top.location.href = 'default.aspx';
		}
   </script>
	

     </HEAD>
	<frameset frameSpacing="0" rows="60,*" frameBorder="0" RUNAT=server>
		<frame name="header" id="header" src="header.aspx" frameBorder="0" noResize scrolling="no">
		<frameset frameSpacing="0" frameBorder="0" cols="180,*" id=menu>
			<frame name="leftnavi" src="leftnavi.aspx" frameBorder="0" scrolling=no >
			<frame name="main" src="<%=requestedpage %>" frameBorder="0" scrolling="auto" >
		</frameset>
	</frameset>

</HTML>
