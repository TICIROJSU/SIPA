<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Registro.aspx.cs" Inherits="ASPX_IROJVAR_Sorteo2_Registro" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <title>Login Consultas Acceso</title>    
    <link rel="shortcut icon" href="../../Estilos/imagenes/favicon.png" type="image/png" />
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport"/>
    <!-- Bootstrap 3.3.7 -->
    <link rel="stylesheet" href="../../Estilos/bower_components/bootstrap/dist/css/bootstrap.min.css" type="text/css"/>
    <!-- Font Awesome -->
    <link rel="stylesheet" href="../../Estilos/bower_components/font-awesome/css/font-awesome.min.css" type="text/css"/>
    <!-- Ionicons -->
    <link rel="stylesheet" href="../../Estilos/bower_components/Ionicons/css/ionicons.min.css" type="text/css"/>
    <!-- Theme style -->
    <link rel="stylesheet" href="../../Estilos/dist/css/AdminLTE.min.css" type="text/css"/>
    <!-- iCheck -->
    <link rel="stylesheet" href="../../Estilos/plugins/iCheck/square/blue.css" type="text/css"/>
    <!-- BarraAzul -->
    <link rel="stylesheet" href="../../Estilos/BotonIrArriba/Botonsubir.css" type="text/css"/>
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
        <div class="row">
            <div class="col-md-3 right">
                <img class="img-responsive" src="../../../Images/imgnavidad.png" />
            </div>
            <div class="col-md-5">
                <div class="login-box">

                 <!-- /.login-logo -->
                  <div style="text-align:center; color:red">
                      <div id="divMensaje"></div>
                      <asp:Label ID="lblMensaje" runat="server" ></asp:Label>
                  </div>
                 <div class="login-box-body">
                    <p class="login-box-msg">Registro</p>
            
                       <div class="form-group has-feedback">
                           <asp:TextBox ID="txtDNI" runat="server" class="form-control" placeholder="DNI" required="true"></asp:TextBox>
                          <span class="glyphicon glyphicon-user form-control-feedback"></span>
                       </div>
                       <div class="form-group has-feedback">
                           <asp:TextBox ID="txtApellidos" runat="server" class="form-control" placeholder="Apellidos" required="true"></asp:TextBox>
                          <span class="glyphicon glyphicon-user form-control-feedback"></span>
                       </div>
                       <div class="form-group has-feedback">
                           <asp:TextBox ID="txtNombres" runat="server" class="form-control" placeholder="Nombres" required="true"></asp:TextBox>
                          <span class="glyphicon glyphicon-user form-control-feedback"></span>
                       </div>
                       <div class="row">
                          <div class="col-xs-4">
                              <asp:Button ID="enviar" runat="server" Text="Registrar" class="btn btn-primary btn-block btn-flat" OnClick="enviar_Click" />
                          </div>
                          <!-- /.col -->
                       </div>
             
                 </div>
              </div>
            </div>
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

