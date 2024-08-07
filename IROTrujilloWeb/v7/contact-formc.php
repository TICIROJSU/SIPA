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
		$fecha=date('j-n-Y'). " " .date('G:i:s');  		//$fechar=now();
		$hora=(date("H:i:s e O"));
		$email_from="consultas@irotrujillo.gob.pe";
		$email_to = "informes@irotrujillo.gob.pe"; //nueva linea para correo
		$email_subject = "Mensaje desde el sitio web - Form";  //para correo
		$email_message = "Detalles del formulario de contacto:\n\n";
		$email_message .= "Nombre de Subscrito: " . $_POST['name'] . "\n";
		$email_message .= "Email Subscrito: " . $_POST['email'] . "\n";
		$email_message .= "Asunto Subscrito: " . $_POST['subject'] . "\n";
		$email_message .= "Mensaje Subscrito: " . $_POST['message'] . "\n\n";
		/* $email_message .= "IP: " . $ip . "\n";
		$email_message .= "Fecha: " . $fecha . "\n";
		$email_message .= "Hora: " . $hora . "\n\n"; */
		
		// Para el Emisor
		$email_frome="informes@irotrujillo.gob.pe";
		$email_messagee = "Respuesta Automática\n\n";
		$email_messagee .= "Gracias por contactarte con nosotros " . $_POST['name'] . "\n";
		$email_messagee .= "Te estaremos respondiendo al Email Subscrito: " . $_POST['email'] . "\n";
		//$email_messagee .= "Asunto Subscrito: " . $_POST['subject'] . "\n";
		//$email_messagee .= "Mensaje Subscrito: " . $_POST['message'] . "\n";
		/* $headerse = 'From: '.$email_frome."\r\n".
		'Reply-To: '.$email_frome."\r\n" .
		'X-Mailer: PHP/' . phpversion(); */

		$conecfc=mysql_connect($host,$user,$pw)or die("Problema al conectar con el host");
		mysql_select_db($db,$conecfc)or die("Problema al conectar la BD");

		mysql_query("INSERT INTO formc (fnombre,femail,fasunto,fmensaje,festado,ffecha,fip)
		VALUES ('$name','$correo','$asunto','$mensaje','1','$fechaf','$ip')", $conecfc);
				
		ini_set('sendmail_from','informatica@irotrujillo.gob.pe'); 				
		
		// Ahora se envía el e-mail usando la función mail() de PHP
		$headers = 'From: '.$email_from."\r\n".
		'Reply-To: '.$email_to."\r\n" .
		'X-Mailer: PHP/' . phpversion();
		
		//mail($email_to, $email_subject, $email_message, $headers);
		//<?php {"response":"success"} ?
		//exec("contact-form.php > /dev/null 2>&1 &");
		//echo "Datos procesados correctamente desde: " . $ip ." hoy: " .$fecha. " Bye";
		//include("../v7/contact-form.php");	
		
			if (@mail($email_to, $email_subject, $email_message, $headers)) {
				 mail($correo, $email_subject, $email_messagee, $headers);
				 /* echo "<script language='javascript'>
					alert('Mensaje enviado, muchas gracias.');
				 </script>"; */
				 
				 /* header('HTTP/1.1 200 OK');
				echo "SUCCESS";
				return; */
				header("Location:../index.html"); 
			} else {
				 echo "<script language='javascript'>
					alert('Envio de Form fallado');
				 </script>";
			}		
	}	else{		
		echo "Problema al procesar los datos";
	}
?>