<?php	
	include("conecx.php");
	
	if(isset($_POST['email']) && !empty($_POST['email']))
	{
		//ini zona horaria
		$tzona= date_default_timezone_set('America/Lima');
		//$timezone	= new DateTimeZone($timezone);
		//$now		= new DateTime('now', $timezone);
		//$offset		= $timezone->getOffset($now);
		//$offset		= $timezone-&gt;getOffset($now);
		$time		= time() + $tzona;
		//fin zona horaria
		$correo=$_POST['email'];
		$name=$_POST['name'];
		$asunto=$_POST['subject'];
		$mensaje=$_POST['message'];
		//Recoges la IP y mandas el email    (aquí o en una página a parte)
		$ip = $_SERVER['REMOTE_ADDR'];
		$fechaf=(date("Y-m-d H:i:s"));
		$fecha=date('j-n-Y'). " " .date('G:i:s');
		//$fechar=now();
		$hora=(date("H:i:s e O"));
		$email_from="informatica@irotrujillo.gob.pe";
		$email_to = "slylurk@gmail.com"; //nueva linea para correo
		$email_subject = "Contacto desde el sitio web - Form";  //para correo
		$email_message = "Detalles del formulario de contacto:\n\n";
		$email_message .= "Nombre de Subscrito: " . $_POST['name'] . "\n";
		$email_message .= "Email Subscrito: " . $_POST['email'] . "\n";
		$email_message .= "Asunto Subscrito: " . $_POST['subject'] . "\n";
		$email_message .= "Mensaje Subscrito: " . $_POST['message'] . "\n";
		$email_message .= "IP: " . $ip . "\n";
		$email_message .= "Fecha: " . $fecha . "\n";
		$email_message .= "Hora: " . $hora . "\n\n";

		$conecfc=mysql_connect($host,$user,$pw)or die("Problema al conectar con el host");
		mysql_select_db($db,$conecfc)or die("Problema al conectar la BD");

		mysql_query("INSERT INTO formc (fnombre,femail,fasunto,fmensaje,festado,ffecha,fip)
		VALUES ('$name','$correo','$asunto','$mensaje','1','$fechaf','$ip')", $conecfc);
				
		ini_set('sendmail_from','informatica@irotrujillo.gob.pe'); 				
		
		// Ahora se envía el e-mail usando la función mail() de PHP
		$headers = 'From: '.$email_from."\r\n".
		'Reply-To: '.$email_from."\r\n" .
		'X-Mailer: PHP/' . phpversion();
		@mail($email_to, $email_subject, $email_message, $headers);
		
		echo "Datos procesados correctamente desde: " . $ip ." hoy: " .$fecha. " Bye";
		include("contact-form.php");				
		
	}	else{
		
		echo "Problema al procesar los datos";
	}

?>
