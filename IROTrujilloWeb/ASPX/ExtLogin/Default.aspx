<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ASPX_ExtLogin_Default" %>

<!DOCTYPE html>

<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <title>Login HISMINSA</title>    
    <link rel="shortcut icon" href="../../imagenes/faviconHIS.ico" type="image/x-icon" />
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport"/>
    <!-- Bootstrap 3.3.7 -->
    <link rel="stylesheet" href="../Estilos/bower_components/bootstrap/dist/css/bootstrap.min.css" type="text/css"/>
    <!-- Font Awesome -->
    <link rel="stylesheet" href="../Estilos/bower_components/font-awesome/css/font-awesome.min.css" type="text/css"/>
    <!-- Ionicons -->
    <link rel="stylesheet" href="../Estilos/bower_components/Ionicons/css/ionicons.min.css" type="text/css"/>
    <!-- Theme style -->
    <link rel="stylesheet" href="../Estilos/dist/css/AdminLTE.min.css" type="text/css"/>
    <!-- iCheck -->
    <link rel="stylesheet" href="../Estilos/plugins/iCheck/square/blue.css" type="text/css"/>
    <!-- BarraAzul -->
    <link rel="stylesheet" href="../Estilos/BotonIrArriba/Botonsubir.css" type="text/css"/>
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]-->
    <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
    <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <!--[endif]-->
    <!-- Google Font -->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic" type="text/css"/>


</head>

    <!-- MEDIOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO-->

<body class="hold-transition login-page">
    <form ID="FormView1" runat="server">
      <div class="nuevoheader">
      </div>
      <div class="login-box">
         <div class="login-logo" style="display: block">
            <img src="../Estilos/imagenes/logo.png" height="35%" width="35%"/>
            <a>
               <b>
                  <p>Consulta de Citas Médicas - IRO</p>
               </b>
            </a>
         </div>
         <!-- /.login-logo -->
          <div style="text-align:center; color:red"><asp:Label ID="LblMensaje" runat="server" Text=""></asp:Label></div>
         <div class="login-box-body">
            <p class="login-box-msg">Inicia sesión</p>
            
               <div class="form-group has-feedback">
                   <asp:TextBox ID="txtusuario" runat="server" class="form-control" placeholder="Usuario" required></asp:TextBox>
                  <span class="glyphicon glyphicon-user form-control-feedback"></span>
                  <!--<span class="glyphicons glyphicons-user"></span>
                     <!--<span class="fas fa-user form-control-feedback"></span>-->
               </div>
               <div class="form-group has-feedback">
                   <asp:TextBox ID="txtcontrasena" runat="server" type="password" class="form-control" placeholder="Contraseña" required></asp:TextBox>
                  <span class="glyphicon glyphicon-lock form-control-feedback"></span>
               </div>
               <div class="row">
                  <div class="col-xs-8">
                     <div class="checkbox icheck">
                        <label>
                        <input type="checkbox" checked required> Acepto los términos.
                        </label>
                     </div>
                  </div>
                  <!-- /.col -->
                  <div class="col-xs-4">
                      <asp:Button ID="enviar" runat="server" Text="Entrar" class="btn btn-primary btn-block btn-flat" OnClick="enviar_Click" />
                  </div>
                  <!-- /.col -->
               </div>
               <div class="row">
                  <div class="col-xs-8">
                     <a style="cursor: pointer;" onclick="window.open('Terminos/','Acerca de...','width=400,height=500,menubar=no,scrollbars=yes,toolbar=no,location=no,directories=no,resizable=no,top=100,left=100')">Ver términos y condiciones.</a>
                  </div>
                  <!-- /.col -->
                  <div class="col-xs-4">
                     <a style="cursor: pointer;" href="Registro/">Regístrate.</a>
                  </div>
                  <!-- /.col -->
               </div>            
         </div>
         <!-- /.login-box-body -->
         <div class="login-logo" style="margin-top: 15px;">
             <div class="row">
                 <div class="column">
                     <div class="col-xs-6 pull-left" style="padding-left: 20%; margin-right: 0px">

                     </div>
                 </div>
                 <div class="column">
                     <div class="col-xs-6 pull-right" style="padding-right: 28%;">

                    </div>
                 </div>
             </div>           
         </div>
         <!-- /.login-logo -->
      </div>
      <!-- /.login-box -->
      <!-- jQuery 3 -->
      <script src="../Estilos/bower_components/jquery/dist/jquery.min.js"></script>
      <!-- Bootstrap 3.3.7 -->
      <script src="../Estilos/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
      <!-- iCheck -->
      <script src="../Estilos/plugins/iCheck/icheck.min.js"></script>
      <script>
          $(function () {
              $('input').iCheck({
                  checkboxClass: 'icheckbox_square-blue',
                  radioClass: 'iradio_square-blue',
                  increaseArea: '20%' /* optional */
              });
          });
      </script>
    </form>
    </body>

</html>

