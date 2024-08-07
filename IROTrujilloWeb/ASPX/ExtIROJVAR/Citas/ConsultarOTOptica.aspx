<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConsultarOTOptica.aspx.cs" Inherits="ASPX_ExtIROJVAR_Citas_ConsultarOTOptica" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv=Content-Type content="text/html; charset=windows-1252">
	<style id="OrdenTrabajo_28770_Styles">
	<!--table
	{mso-displayed-decimal-separator:"\.";
	mso-displayed-thousand-separator:"\,";}
.xl6328770
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:11.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:center;
	vertical-align:middle;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl6428770
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:11.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:general;
	vertical-align:middle;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl6528770
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:11.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:Fixed;
	text-align:center;
	vertical-align:middle;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl6628770
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:11.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:right;
	vertical-align:middle;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl6728770
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:11.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:center;
	vertical-align:middle;
	border-top:1.5pt solid windowtext;
	border-right:none;
	border-bottom:1.5pt solid windowtext;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl6828770
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:11.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:center;
	vertical-align:middle;
	border-top:1.5pt solid windowtext;
	border-right:none;
	border-bottom:.5pt hairline windowtext;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl6928770
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:11.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:right;
	vertical-align:middle;
	border-top:1.5pt solid windowtext;
	border-right:none;
	border-bottom:.5pt hairline windowtext;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl7028770
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:11.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:center;
	vertical-align:middle;
	border-top:.5pt hairline windowtext;
	border-right:none;
	border-bottom:.5pt hairline windowtext;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl7128770
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:11.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:right;
	vertical-align:middle;
	border-top:.5pt hairline windowtext;
	border-right:none;
	border-bottom:.5pt hairline windowtext;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl7228770
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:11.0pt;
	font-weight:700;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:center;
	vertical-align:middle;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl7328770
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:10.0pt;
	font-weight:700;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:center;
	vertical-align:middle;
	border-top:1.5pt solid windowtext;
	border-right:none;
	border-bottom:1.5pt solid windowtext;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl7428770
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:8.0pt;
	font-weight:700;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:center;
	vertical-align:middle;
	border-top:1.5pt solid windowtext;
	border-right:none;
	border-bottom:1.5pt solid windowtext;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl7528770
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:8.0pt;
	font-weight:700;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:center;
	vertical-align:middle;
	border-top:1.5pt solid windowtext;
	border-right:none;
	border-bottom:1.5pt solid windowtext;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl7628770
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:12.0pt;
	font-weight:700;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:general;
	vertical-align:middle;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl7728770
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:14.0pt;
	font-weight:700;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:general;
	vertical-align:middle;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl7828770
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:12.0pt;
	font-weight:700;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:right;
	vertical-align:middle;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl7928770
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:11.0pt;
	font-weight:700;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:left;
	vertical-align:middle;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl8028770
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:11.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:left;
	vertical-align:middle;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl8128770
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:11.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:center;
	vertical-align:middle;
	border-top:.5pt hairline windowtext;
	border-right:none;
	border-bottom:none;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl8228770
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:11.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:left;
	vertical-align:middle;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl8328770
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:12.0pt;
	font-weight:700;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:left;
	vertical-align:middle;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl8428770
	{color:black;
	font-size:11.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:right;
	vertical-align:middle;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;
	padding-right:9px;
	mso-char-indent-count:1;}
.xl8528770
	{color:black;
	font-size:11.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:Fixed;
	text-align:right;
	vertical-align:middle;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;
	padding-right:9px;
	mso-char-indent-count:1;}
.xl8628770
	{color:black;
	font-size:12.0pt;
	font-weight:700;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:Fixed;
	text-align:right;
	vertical-align:middle;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;
	padding-right:9px;
	mso-char-indent-count:1;}
.xl8728770
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:11.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:left;
	vertical-align:middle;
	border-top:.5pt hairline windowtext;
	border-right:none;
	border-bottom:.5pt hairline windowtext;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl8828770
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:11.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:left;
	vertical-align:middle;
	border-top:none;
	border-right:none;
	border-bottom:1.5pt solid windowtext;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl8928770
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:14.0pt;
	font-weight:700;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:left;
	vertical-align:middle;
	border-top:none;
	border-right:none;
	border-bottom:1.5pt solid windowtext;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl9028770
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:12.0pt;
	font-weight:700;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:center;
	vertical-align:middle;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl9128770
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:16.0pt;
	font-weight:700;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:center;
	vertical-align:middle;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl9228770
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:11.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:left;
	vertical-align:middle;
	border-top:1.5pt solid windowtext;
	border-right:none;
	border-bottom:.5pt hairline windowtext;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl9328770
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:12.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:center;
	vertical-align:middle;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl9428770
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:9.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:center;
	vertical-align:middle;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
	-->
	</style>
</head>
<body>
	<div>
		<table border=0 cellpadding=0 cellspacing=0 width=297 class=xl6428770 style='border-collapse:collapse;table-layout:fixed;width:222pt'>
			<col class=xl6428770 width=61 style='mso-width-source:userset;mso-width-alt: 2230; width:46pt' />
			<col class=xl6428770 width=59 span=4 style='mso-width-source:userset; mso-width-alt:2157;width:44pt' />
			<tr height=20 style='height:15.0pt'>
				<td height=20 class=xl6428770 width=61 style='height:15.0pt;width:46pt'/>
				<td class=xl6428770 width=59 style='width:44pt'/>
				<td class=xl6428770 width=59 style='width:44pt'/>
				<td class=xl6428770 width=59 style='width:44pt'/>
				<td class=xl6428770 width=59 style='width:44pt'/>
			</tr>
			<tr height=21 style='height:15.75pt'>
				<td colspan=5 height=21 class=xl9328770 style='height:15.75pt'>INSTITUTO REGIONAL DE</td>
			</tr>
			<tr height=21 style='height:15.75pt'>
				<td colspan=5 height=21 class=xl9328770 style='height:15.75pt'>OFTALMOLOGIA - JSU</td>
			</tr>
			<tr height=20 style='height:15.0pt'>
				<td colspan=5 height=20 class=xl6328770 style='height:15.0pt'>R.U.C. 20314801327</td>
			</tr>
			<tr height=20 style='height:15.0pt'>
				<td colspan=5 height=20 class=xl9428770 style='height:15.0pt'>BL. AMERICA OESTE S/ MZ.P LOTE 7A URB. NATASHA</td>
			</tr>
			<tr height=20 style='height:15.0pt'>
				<td colspan=5 height=20 class=xl9428770 style='height:15.0pt'>ALTA (COSTADO DE LA CORTE SUP. JUSTICIA) La Libertad</td>
			</tr>
			<tr height=20 style='height:15.0pt'>
				<td colspan=5 height=20 class=xl6328770 style='height:15.0pt'>Trujillo - La Libertad</td>
			</tr>
			<tr height=21 style='height:15.75pt'>
				<td colspan=5 height=21 class=xl9028770 style='height:15.75pt'>TICKET ORDEN TRABAJO</td>
			</tr>
			<tr height=28 style='height:21.0pt'>
				<td colspan=5 height=28 class=xl9128770 style='height:21.0pt' runat="server" id="tdOTrabajo" >N° </td>
			</tr>
			<tr height=20 style='height:15.0pt'>
				<td colspan=2 height=20 class=xl8228770 style='height:15.0pt'>FECHA</td>
				<td colspan=3 class=xl8228770 runat="server" id="tdFechaDoc" >: </td>
			</tr>
			<tr height=20 style='height:15.0pt'>
				<td colspan=2 height=20 class=xl8228770 style='height:15.0pt'>PACIENTE</td>
				<td colspan=3 class=xl8228770 runat="server" id="tdPaciente" >: </td>
			</tr>
			<tr height=20 style='height:15.0pt'>
				<td colspan=5 height=20 class=xl6328770 style='height:15.0pt'/>
			</tr>
			<tr height=21 style='height:15.75pt'>
				<td colspan=5 height=21 class=xl8828770 style='height:15.75pt'>[LEJOS]</td>
			</tr>
			<tr height=22 style='height:16.5pt'>
				<td height=22 class=xl6728770 style='height:16.5pt;border-top:none'>&nbsp;</td>
				<td class=xl7328770 style='border-top:none'>ESFERA</td>
				<td class=xl7428770 style='border-top:none'>CILINDRO</td>
				<td class=xl7328770 style='border-top:none'>EJE</td>
				<td class=xl7328770 style='border-top:none'>COLOR</td>
			</tr>
			<tr height=21 style='height:15.75pt'>
				<td height=21 class=xl7228770 style='height:15.75pt'>OD</td>
				<td class=xl6328770 runat="server" id="tdLejosODEsf" ></td>
				<td class=xl6528770 runat="server" id="tdLejosODCil" ></td>
				<td class=xl6328770 runat="server" id="tdLejosODEje" ></td>
				<td class=xl6328770 runat="server" id="tdLejosODCol" ></td>
			</tr>
			<tr height=20 style='height:15.0pt'>
				<td height=20 class=xl7228770 style='height:15.0pt'>OI</td>
				<td class=xl6328770 runat="server" id="tdLejosOIesf" ></td>
				<td class=xl6528770 runat="server" id="tdLejosOICil" ></td>
				<td class=xl6328770 runat="server" id="tdLejosOIEje" ></td>
				<td class=xl6328770 runat="server" id="tdLejosOICol" ></td>
			</tr>
			<tr height=21 style='height:15.75pt'>
				<td height=21 class=xl7628770 style='height:15.75pt'>DIP</td>
				<td colspan=4 class=xl8328770 runat="server" id="tdLejosDIP" >: mm</td>
			</tr>
			<tr height=21 style='height:15.75pt'>
				<td colspan=5 height=21 class=xl8828770 style='height:15.75pt'>[CERCA]</td>
			</tr>
			<tr height=22 style='height:16.5pt'>
				<td height=22 class=xl6728770 style='height:16.5pt;border-top:none'>&nbsp;</td>
				<td class=xl7328770 style='border-top:none'>ESFERA</td>
				<td class=xl7428770 style='border-top:none'>CILINDRO</td>
				<td class=xl7328770 style='border-top:none'>EJE</td>
				<td class=xl7328770 style='border-top:none'>COLOR</td>
			</tr>
			<tr height=21 style='height:15.75pt'>
				<td height=21 class=xl7228770 style='height:15.75pt'>OD</td>
				<td class=xl6328770 runat="server" id="tdCercaODEsf" ></td>
				<td class=xl6328770 runat="server" id="tdCercaODCil" ></td>
				<td class=xl6328770 runat="server" id="tdCercaODEje" ></td>
				<td class=xl6328770 runat="server" id="tdCercaODCol" ></td>
			</tr>
			<tr height=20 style='height:15.0pt'>
				<td height=20 class=xl7228770 style='height:15.0pt'>OI</td>
				<td class=xl6328770 runat="server" id="tdCercaOIEsf" ></td>
				<td class=xl6328770 runat="server" id="tdCercaOICil" ></td>
				<td class=xl6328770 runat="server" id="tdCercaOIEje" ></td>
				<td class=xl6328770 runat="server" id="tdCercaOICol" ></td>
			</tr>
			<tr height=21 style='height:15.75pt'>
				<td height=21 class=xl7628770 style='height:15.75pt'>DIP</td>
				<td colspan=4 class=xl8328770 runat="server" id="tdCercaDIP" >: mm</td>
			</tr>
			<tr height=26 style='height:19.5pt'>
				<td height=26 class=xl7728770 style='height:19.5pt'>ADD</td>
				<td colspan=4 class=xl8928770 runat="server" id="tdADD" >: </td>
			</tr>
			<tr height=32 style='height:24.0pt'>
				<td height=32 class=xl7428770 style='height:24.0pt'>CANTIDAD</td>
				<td colspan=2 class=xl7328770>DESCRIPCION</td>
				<td class=xl7528770 width=59 style='border-top:none;width:44pt'>PRECIO UNITARIO</td>
				<td class=xl7328770 style='border-top:none'>TOTAL</td>
			</tr>

                <asp:Literal ID="LitDocuDet" runat="server"></asp:Literal>


			<tr height=20 style='height:15.0pt'>
				<td colspan=5 height=20 class=xl8128770 style='height:15.0pt'>&nbsp;</td>
			</tr>
			<tr height=20 style='height:15.0pt'>
				<td colspan=5 height=20 class=xl6328770 style='height:15.0pt'/>
			</tr>
			<tr height=20 style='height:15.0pt'>
				<td colspan=2 height=20 class=xl8228770 style='height:15.0pt'>SUB TOTAL</td>
				<td class=xl6628770>:</td>
				<td colspan=2 class=xl8428770 runat="server" id="tdDocSubTotal" >86.44</td>
			</tr>
			<tr height=20 style='height:15.0pt'>
				<td colspan=2 height=20 class=xl8228770 style='height:15.0pt'>I.G.V.</td>
				<td class=xl6628770>:</td>
				<td colspan=2 class=xl8428770 runat="server" id="tdDocIGV" >15.56</td>
			</tr>
			<tr height=20 style='height:15.0pt'>
				<td colspan=2 height=20 class=xl8228770 style='height:15.0pt'>DESCUENTO</td>
				<td class=xl6628770>:</td>
				<td colspan=2 class=xl8528770 runat="server" id="tdDocDesc" >0.00</td>
			</tr>
			<tr height=21 style='height:15.75pt'>
				<td colspan=2 height=21 class=xl8328770 style='height:15.75pt'>TOTAL</td>
				<td class=xl7828770>:</td>
				<td colspan=2 class=xl8628770 runat="server" id="tdDocTotal" >102.00</td>
			</tr>
			<tr height=20 style='height:15.0pt'>
				<td colspan=5 height=20 class=xl6328770 style='height:15.0pt'>------------------------------------------------------</td>
			</tr>
			<tr height=20 style='height:15.0pt'>
				<td colspan=5 height=20 class=xl7928770 style='height:15.0pt'>Comentario:<span style='mso-spacerun:yes'> </span>
				</td>
			</tr>
			<tr height=40 style='mso-height-source:userset;height:30.0pt'>
				<td colspan=5 height=40 class=xl8028770 width=297 style='height:30.0pt; width:222pt' runat="server" id="tdDocComent" >MONTURA DE ACETATO JACK MARRON FOTOCROM. MARRON</td>
			</tr>
		</table>
	</div>

    <asp:Literal ID="LitError" runat="server"></asp:Literal>
</body>
</html>
