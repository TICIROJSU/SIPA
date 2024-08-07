<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImprimirReg.aspx.cs" Inherits="IROTCovid19_IROJVAR_RegCovid_ImprimirReg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body >
    <form id="form1" runat="server">
        <div style="background-image:url('../../Imagenes/ImpF100Cov.jpg')">
			<!--
				margin: 35px 70px 50px; 35 Arriba, 70 derecha e izquierda y 50 Abajo
				margin: 35px 70px 50px 90px;			
				35 Arriba, 70 Derecha, 50 Abajo y 90 Izquierda
			-->
			<div style="float:left; "><!-- IZQUIERDA -->

				<div runat="server" id="dPerDNI" style="margin:230px 0px 0px 360px; ">dPerDNI</div>
				<div runat="server" id="dPerApeP" style="margin:5px 0px 0px 360px; ">dPerApeP</div>
				<div runat="server" id="dPerApeM" style="margin:15px 0px 0px 360px; ">dPerApeM</div>
				<div runat="server" id="dPerNomb" style="margin:15px 0px 0px 360px; ">dPerNomb</div>
				<div runat="server" id="dPacTipDoc" style="margin:70px 0px 0px 360px; ">dPacTipDoc</div>
				<div runat="server" id="dPacDNI" style="margin:4px 0px 0px 360px; ">dPacDNI</div>
				<div runat="server" id="dPacApeP" style="margin:4px 0px 0px 360px; ">dPacApeP</div>
				<div runat="server" id="dPacApeM" style="margin:14px 0px 0px 360px; ">dPacApeM</div>
				<div runat="server" id="dPacNombres" style="margin:10px 0px 0px 360px; ">dPacNombres</div>
				<div runat="server" id="dPacEdad" style="margin:20px 0px 0px 360px; ">dPacEdad</div>
				<div runat="server" id="dPacSexo" style="margin:20px 0px 0px 360px; ">dPacSexo</div>
				<div runat="server" id="dPacCel" style="margin:15px 0px 0px 360px; ">dPacCel</div>
				<div runat="server" id="dPacOtroT" style="margin:5px 0px 0px 360px; ">dPacOtroT</div>
				<div runat="server" id="dPacDomicA" style="margin:0px 0px 0px 360px; ">dPacDomicA</div>
				<div runat="server" id="dPacDomicB" style="margin:0px 0px 0px 360px; ">dPacDomicB</div>
				<div runat="server" id="dPacDireccion" style="margin:5px 0px 0px 360px; ">dPacDireccion</div>
				<div runat="server" id="dPacProv" style="margin:15px 0px 0px 360px; ">dPacProv</div>
				<div runat="server" id="dPacGeoLoc" style="margin:20px 0px 0px 360px; ">dPacGeoLoc</div>
				<div runat="server" id="dPacPerSaludA" style="margin:80px 0px 0px 360px; ">dPacPerSaludA</div>
				<div runat="server" id="dPacPerSaludB" style="margin:0px 0px 0px 360px; ">dPacPerSaludB</div>
				<div runat="server" id="dPacProfA" style="margin:0px 0px 0px 360px; ">&nbsp;</div>
				<div runat="server" id="dPacProfB" style="margin:0px 0px 0px 360px; ">&nbsp;</div>
				<div runat="server" id="dPacProfC" style="margin:0px 0px 0px 360px; ">&nbsp;</div>
				<div runat="server" id="dPacProfD" style="margin:0px 0px 0px 360px; ">&nbsp;</div>
				<div runat="server" id="dPacProfE" style="margin:-1px 0px 0px 360px; ">&nbsp;</div>
				<div runat="server" id="dPacProfF" style="margin:-1px 0px 0px 360px; ">&nbsp;</div>
				<div runat="server" id="dPacProfG" style="margin:-1px 0px 0px 360px; ">&nbsp;</div>
			</div>

			<div style="float:right; margin:115px 0px 0px 0px;"><!-- DERECHA -->
				
				<div runat="server" id="dPacSintA" style="margin:0px 0px 0px -350px; ">&nbsp;</div>
				<div runat="server" id="dPacSintB" style="margin:0px 0px 0px -350px; ">&nbsp;</div>
				<div runat="server" id="dPacSintFecha" style="margin:10px 0px 0px -350px; ">&nbsp;</div>

				<div runat="server" id="dPacMarSintA" style="margin:10px 0px 0px -350px; ">&nbsp;</div>
				<div runat="server" id="dPacMarSintB" style="margin:0px 0px 0px -350px; ">&nbsp;</div>
				<div runat="server" id="dPacMarSintC" style="margin:0px 0px 0px -350px; ">&nbsp;</div>
				<div runat="server" id="dPacMarSintD" style="margin:-1px 0px 0px -350px; ">&nbsp;</div>
				<div runat="server" id="dPacMarSintE" style="margin:0px 0px 0px -350px; ">&nbsp;</div>
				<div runat="server" id="dPacMarSintF" style="margin:0px 0px 0px -350px; ">&nbsp;</div>
				<div runat="server" id="dPacMarSintG" style="margin:-1px 0px 0px -350px; ">&nbsp;</div>
				<div runat="server" id="dPacMarSintH" style="margin:-1px 0px 0px -350px; ">&nbsp;</div>
				<div runat="server" id="dPacMarSintI" style="margin:-1px 0px 0px -350px; ">&nbsp;</div>
				<div runat="server" id="dPacMarSintJ" style="margin:-1px 0px 0px -350px; ">&nbsp;</div>
				<div runat="server" id="dPacMarSintK" style="margin:-1px 0px 0px -350px; ">&nbsp;</div>
				<div runat="server" id="dPacMarSintL" style="margin:-1px 0px 0px -350px; ">&nbsp;</div>

				<div runat="server" id="dPacDolorA" style="margin:10px 0px 0px -350px; ">&nbsp;</div>
				<div runat="server" id="dPacDolorB" style="margin:0px 0px 0px -350px; ">&nbsp;</div>
				<div runat="server" id="dPacDolorC" style="margin:-1px 0px 0px -350px; ">&nbsp;</div>
				<div runat="server" id="dPacDolorD" style="margin:0px 0px 0px -350px; ">&nbsp;</div>
				
				<div runat="server" id="dPacOtrSint" style="margin:15px 0px 0px -350px; ">&nbsp;</div>

				<div runat="server" id="dPRFecha" style="margin:80px 0px 0px -275px; ">&nbsp;</div>
				<div runat="server" id="dPRProcedA" style="margin:10px 0px 0px -275px; ">&nbsp;</div>
				<div runat="server" id="dPRProcedB" style="margin:0px 0px 0px -275px; ">&nbsp;</div>
				<div runat="server" id="dPRProcedC" style="margin:0px 0px 0px -275px; ">&nbsp;</div>
				<div runat="server" id="dPRProcedD" style="margin:0px 0px 0px -275px; ">&nbsp;</div>
				<div runat="server" id="dPRProcedE" style="margin:-1px 0px 0px -275px; ">&nbsp;</div>
				<div runat="server" id="dPRProcedF" style="margin:0px 0px 0px -275px; ">&nbsp;</div>
				<div runat="server" id="dPRProcedG" style="margin:13px 0px 0px -275px; ">&nbsp;</div>
				<div runat="server" id="dPRResultA" style="margin:3px 0px 0px -275px; ">&nbsp;</div>
				<div runat="server" id="dPRResultB" style="margin:0px 0px 0px -275px; ">&nbsp;</div>
				<div runat="server" id="dPRResultC" style="margin:-1px 0px 0px -275px; ">&nbsp;</div>
				<div runat="server" id="dPRResultD" style="margin:-1px 0px 0px -275px; ">&nbsp;</div>
				<div runat="server" id="dPRResultE" style="margin:0px 0px 0px -275px; ">&nbsp;</div>
				
			</div>
			<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
			<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
			<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
			<br /><br /><br /><br /><br /><br /><br />
			



        </div>
		<asp:Literal ID="LitError" runat="server"></asp:Literal>
    </form>
</body>
</html>
