<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConsultaCitaPacRep.aspx.cs" Inherits="ASPX_ExtIROJVAR_Citas_ConsultaCitaPacRep" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
<style type="text/css">
	.Bold {
		font-weight: bold;
	}
	.SizeP{
		font-size:24px;
	}
</style>
</head>

<body text="#FF0000" style="font-family:Verdana, Geneva, sans-serif" >
    <form id="form1" runat="server">
        <%--<div style="background-image:url('../../../Images/Pconstruccion.png')">--%>
        <div>

<%--	<div style="float:left; "><!-- IZQUIERDA -->
        <div>INSTITUTO REGIONAL OFTALMOLOGÍA</div><br />
		<div runat="server" id="dPerDNI" style="margin:230px 0px 0px 360px; ">dPerDNI</div>
		<div runat="server" id="dPerApeP" style="margin:5px 0px 0px 360px; ">dPerApeP</div>
	</div>--%>

        <table cellpadding="0" cellspacing="0">
            <tr><td width="354" colspan="2" align="center">INSTITUTO REGIONAL OFTALMOLOGÍA</td></tr>
            <tr><td colspan="2">&nbsp</td></tr>
            <tr>
                <td colspan="2" align="center">CITA - <asp:Label ID="lblTipo" runat="server" ></asp:Label></td>
            </tr>
            <tr><td colspan="2" align="center" runat="server" id="tdFecCita" class="Bold SizeP"></td></tr>
            <tr><td colspan="2">&nbsp</td></tr>
            <tr><td colspan="2" align="center" runat="server" id="tdHorCita" class="Bold SizeP">09:30 a.m.</td></tr>
            <tr><td colspan="2" align="center" runat="server" id="tdSerCita" class="Bold"></td></tr>
            <tr><td colspan="2" align="center" runat="server" id="tdPisCita" class="Bold"></td></tr>
            <tr><td colspan="2">&nbsp</td></tr>
            <tr>
                <td align="right" class="Bold SizeP">Nº H.C.:&nbsp </td>
                <td align="left" runat="server" id="tdHClCita" class="Bold SizeP"></td>
            </tr>
            <tr><td colspan="2" align="center" runat="server" id="tdPacCita"></td></tr>
            <tr><td colspan="2" align="center">_________________________________________________</td></tr>
            <tr><td colspan="2">&nbsp</td></tr>
            <tr><td colspan="2" align="center">CITAS:    044-287236   044-287222</td></tr>
            <tr><td colspan="2">&nbsp</td></tr>
            <tr><td colspan="2" align="center">POR AFORO, NO SE PERMITIRÁ EL </td></tr>
            <tr><td colspan="2" align="center">INGRESO ANTES DE HORA DE CITA </td></tr>
            <tr><td colspan="2">&nbsp</td></tr>
            <tr>
                <td align="right" class="Bold SizeP">Nº OPER.:&nbsp </td>
                <td align="left" runat="server" id="tdOpeCita" class="Bold SizeP"></td>
            </tr>
            <tr>
                <td align="right">Fecha/Hora Registro:&nbsp </td>
                <td align="left" runat="server" id="tdRegCita"></td>
            </tr>
            <tr>
                <td align="right">Usuario:&nbsp </td>
                <td align="left" runat="server" id="tdUsuCita"></td>
            </tr>
            <tr><td colspan="2">&nbsp</td></tr>
            <tr><td colspan="2" align="center" class="Bold">ATENCIÓN</td></tr>
            <tr><td colspan="2" align="center" class="Bold">PENSANDO EN TU BIENESTAR Y EVITAR LA </td></tr>
            <tr><td colspan="2" align="center" class="Bold">PROPAGACION DEL COVID-19, IRO INFORMA </td></tr>
            <tr><td colspan="2" align="center" class="Bold">LAS SIGUIENTES MEDIDAS ADOPTADAS: </td></tr>
            <tr><td colspan="2">&nbsp</td></tr>
            <tr><td colspan="2" align="center" class="Bold">SOLO SE PERMITE ACOMPAÑANTE </td></tr>
            <tr><td colspan="2" align="center" class="Bold">SI ES MENOR DE EDAD.</td></tr>
            <tr><td colspan="2" align="center" class="Bold">NO SE ADMITEN NIÑOS COMO ACOMPAÑANTES. </td></tr>
            <tr><td colspan="2">&nbsp</td></tr>
            <tr><td colspan="2" align="center" class="Bold">EN EL CASO DE TENER FIEBRE Y/O TOS SE </td></tr>
            <tr><td colspan="2" align="center" class="Bold">REPROGRAMARÁ SU CITA. </td></tr>
        </table>

            <br /><br /><br /><br />

        </div>
		<asp:Literal ID="LitError" runat="server"></asp:Literal>
    </form>
</body>
</html>
