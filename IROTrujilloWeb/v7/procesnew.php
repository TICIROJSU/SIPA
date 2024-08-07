<?php

	include("conec.php");

	if(isset($_POST['email']) && !empty($_POST['email']))
	{
		//ini zona horaria
		$tzona= date_default_timezone_get('America/Lima');
		//$timezone	= new DateTimeZone($timezone);
		//$now		= new DateTime('now', $timezone);
		//$offset		= $timezone->getOffset($now);
		//$offset		= $timezone-&gt;getOffset($now);
		$time=time()+" "+$tzona;
		//fin zona horaria
		$correo=$_POST['email'];
		$name=$_POST['nombre'];
		//Recoges la IP y mandas el email    (aquí o en una página a parte)
		$ip = $_SERVER['REMOTE_ADDR'];
		$fecha=(date('Y-m-d'));
		$fechat=(date("Y-m-d", time()));
		$hora=(date("H:i:s e O"));
		$email_from="informatica@irotrujillo.gob.pe";
		$email_to = "slylurk@gmail.com"; //nueva linea para correo
		$email_subject = "Contacto desde el sitio web - News";  //para correo
		$email_message = "Detalles del formulario de contacto:\n\n";
		$email_message .= "Nombre de Subscrito: " . $_POST['nombre'] . "\n";
		$email_message .= "Email Subscrito: " . $_POST['email'] . "\n";
		$email_message .= "IP: " . $ip . "\n";
		$email_message .= "Fecha: " . $fechat . "\n";
		$email_message .= "Hora: " . $hora . "\n\n";

		$conec=mysql_connect($host,$user,$pw)or die("Problema al conectar con el host");
		mysql_select_db($db,$conec)or die("Problema al conectar la BD");

		mysql_query("INSERT INTO unews (email,nombre,estado,fecha,ip)
		VALUES ('$correo','$name','1','$fecha','$ip')",$conec);
		
		ini_set('sendmail_from','informatica@irotrujillo.gob.pe'); 
  
  
		//Asunto
		$asunto="Correo Enviado desde Pagina Web - News"; 

		//Correo del destinatario
		$para=$correo;
		 
		//Cuerpo
		$contenido="	<html>
		<center><body>
		<div style='background-color:#B7D6D1;'>
		<img src='http://irotrujillo.gob.pe/v7/images/Logo_IRO.jpg' href='http://irotrujillo.gob.pe' alt='IRO Trujillo'/>
		</div>";
		 
		$contenido.="<p>Hola <span style='color:#B7D6D1;font-weight:bold;'>".$name."!</span><br><br>
			Tus datos de registro son:<br>    
			Correo: ".$correo."<br>
			Fecha: ".$fecha."<br>
			Hora: ".$time."<br><br>
			<small style='color:#FEC8D6;'>Este email fue enviado Automaticamente, no responda este e-mail</small></p>
			</body></center>
			</html>";
		 
		//Cabecera
		$cabecera="From: 'Instituto Regional de Oftalmología'<informatica@irotrujillo.gob.pe>\r\n";//Remitente
			$cabecera.="Bcc: slylurk@hotmail.com\r\n";//Copia oculta
		$cabecera.="Content-type: text/html; charset=UTF-8\r\n";
					  
		// Enviar mail
		$resultado=mail($para,$asunto,$contenido,$cabecera);
		if($resultado){
			echo"El mail ha sido enviado correctamente";
		}else{
			echo"Ocurrió un problema al enviar el mail, intenta mas tarde por favor";
		}



				
				// Ahora se envía el e-mail usando la función mail() de PHP
				/* $headers = 'From: '.$email_from."\r\n".
				'Reply-To: '.$email_from."\r\n" .
				'X-Mailer: PHP/' . phpversion();
				@mail($email_to, $email_subject, $email_message, $headers);
		 */
				//echo "Datos procesados correctamente desde: " . $ip ." fecha: " .$fechat. " Bye";
				
	}	else{
			echo "Problema al procesar los datos";
		}

	
	
	
		/**
 * @return string
 * @desc Gets the client IP address
 */
function GetClientIP()
{
        $ip = $_SERVER['REMOTE_ADDR'];

        if(empty($ip))
                $ip = getenv('REMOTE_ADDR');
        if(!empty($HTTP_SERVER_VARS['HTTP_CLIENT_IP']))
                $ip = $HTTP_SERVER_VARS['HTTP_CLIENT_IP'];

        $tmpip = getenv('HTTP_CLIENT_IP');
        if(!empty($tmpip))
                $ip = $tmpip;
        if(!empty($HTTP_SERVER_VARS['HTTP_X_FORWARDED_FOR']))
                $ip = preg_replace('/,.*/', '',
$HTTP_SERVER_VARS['HTTP_X_FORWARDED_FOR']);

        $tmpip = getenv('HTTP_X_FORWARDED_FOR');
        if(!empty($tmpip))
                $ip = preg_replace('/,.*/', '', $tmpip);

        return $ip;
}



// Function to get the client ip address
function get_client_ip_env() {
    $ipaddress = '';
    if (getenv('HTTP_CLIENT_IP'))
        $ipaddress = getenv('HTTP_CLIENT_IP');
    else if(getenv('HTTP_X_FORWARDED_FOR'))
        $ipaddress = getenv('HTTP_X_FORWARDED_FOR');
    else if(getenv('HTTP_X_FORWARDED'))
        $ipaddress = getenv('HTTP_X_FORWARDED');
    else if(getenv('HTTP_FORWARDED_FOR'))
        $ipaddress = getenv('HTTP_FORWARDED_FOR');
    else if(getenv('HTTP_FORWARDED'))
        $ipaddress = getenv('HTTP_FORWARDED');
    else if(getenv('REMOTE_ADDR'))
        $ipaddress = getenv('REMOTE_ADDR');
    else
        $ipaddress = 'UNKNOWN';
 
    return $ipaddress;
}


// Function to get the client ip address
function get_client_ip_server() {
    $ipaddress = '';
    if ($_SERVER['HTTP_CLIENT_IP'])
        $ipaddress = $_SERVER['HTTP_CLIENT_IP'];
    else if($_SERVER['HTTP_X_FORWARDED_FOR'])
        $ipaddress = $_SERVER['HTTP_X_FORWARDED_FOR'];
    else if($_SERVER['HTTP_X_FORWARDED'])
        $ipaddress = $_SERVER['HTTP_X_FORWARDED'];
    else if($_SERVER['HTTP_FORWARDED_FOR'])
        $ipaddress = $_SERVER['HTTP_FORWARDED_FOR'];
    else if($_SERVER['HTTP_FORWARDED'])
        $ipaddress = $_SERVER['HTTP_FORWARDED'];
    else if($_SERVER['REMOTE_ADDR'])
        $ipaddress = $_SERVER['REMOTE_ADDR'];
    else
        $ipaddress = 'UNKNOWN';
 
    return $ipaddress;
}

?>