<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HojaRegistro.aspx.cs" Inherits="ASPX_Registro_HojaRegistro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
      <meta charset="ISO-8859-1" />
      <meta http-equiv="X-UA-Compatible" content="IE=edge" />
      <title>Acepto los Terminos y Condiciones del Sitio Web</title>
	  <link rel="shortcut icon" href="../imagenes/favicongeresa.ico" type="image/x-icon" />

    </head>
    <body style="padding:4%">
	
	<header>
		<div class="col-md-3" style="float: right;"> 
			<div class="form-group">
				<button type="button" class="btn btn-default" style="margin-top: 25px" onclick="location='../'"><i class="fa fa-reply"></i> Regresar</button>
			</div>
		</div>
		<div class="col-md-3" style="float: right;"> 
			<div class="form-group">
				<button type="button" class="btn btn-default" style="margin-top: 25px" onclick="window.print();"><i class="fa fa-reply"></i> Imprimir</button>
			</div>
		</div>
	</header>
	<section>
		<br /><br />
		<p style="text-align: center;"><b> ACTA DE COMPROMISO </b></p>
		<p><b>IRO - La Libertad </b>
        <br /><b>Unidad de Tecnologias de Informacion y Comunicaciones </b></p>
		<br /><br />
		<p style="text-align: justify;">
		Yo, <b><asp:Label ID="lblNombres" runat="server" Text=""></asp:Label></b>, con DNI Nº <b><asp:Label ID="lblDNI" runat="server" Text=""></asp:Label></b> me encuentro laborando, en el EESS <asp:Label ID="lblEESS" runat="server"></asp:Label> con cargo <asp:Label ID="lblCargo" runat="server"></asp:Label>. 
		Expreso que acepto los términos, condiciones y políticas en el sitio web creado por la Unidad de Tecnologias de Informacion y Comunicaciones del IRO; y <b>guardar estricta confidencialidad de los datos sensibles</b> relacionados a la salud de los pacientes o usuarios de salud que se encuentren contenidos en la información que accedo.  
		</p>
		
        <p style="text-align: justify;">Tengo conocimiento que la <b>información</b> brindada por esta página, tiene carácter de <b>confidencial</b>, por lo que, al acceder a ella, estoy aceptando conocer lo que establece la <a href="https://www.minjus.gob.pe/wp-content/uploads/2013/04/LEY-29733.pdf">Ley 29733</a>, <b>Ley de Protección de Datos Personales</b>.  </p>
		
        <p style="text-align: justify;">Además, la distribución, publicación y/o explotación comercial de la información o parte de ella obtenidos en el Sitio Web, está estrictamente prohibida, a menos de que tenga autorización o permiso, por escrito, por parte del personal autorizado en el Instituto Regional de Oftalmologia.</p>
		<!--<p style="text-align: justify;">En el caso de faltar con los terminos y condiciones de uso del presente Sitio Web, me someto a todos los procesos y <b>sanciones</b> del Art. 37 de la Ley 29733. </p>-->
		<br />		<br />		<br />		<br />
		Atentamente, 
		<p></p>
		<br />		<br />
		<div style="float: right"><img src="../../Estilos/imagenes/huella_cuadro.png" width="70%" height="23%" /></div><br /><br /><br />
		<div style="float: right">______________________________________</div><br />
		<div style="float: right"><asp:Label ID="lblNombres2" runat="server" Text="Label"></asp:Label></div><br />
		<div style="float: right">DNI: <asp:Label ID="lblDNI2" runat="server" Text="Label"></asp:Label></div><br />
	</section>
	<footer> 
	  <p></p>
	</footer>
	</body>
    <script>alert("EL PRESENTE FORMATO DEBE SER FIRMADO Y REMITIDO VIA CORREO A LA OFICINA DE ---------- DEL IRO (Lic. --------- <fooooooffff@irotrujillo.gob.pe>)");</script>
    <script>print();</script>
</html>
