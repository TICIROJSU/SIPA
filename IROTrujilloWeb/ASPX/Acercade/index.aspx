<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="ASPX_Acercade_index" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
   <head runat="server">
      <meta http-equiv="X-UA-Compatible; Content-Type" content="IE=edge;text/html; charset=utf-8"/>
      <title>ACERCA DE</title>
      <link rel="shortcut icon" href="../../Images/favicon.png" type="image/x-icon" />
      <!-- Tell the browser to be responsive to screen width -->
      <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport"/>
      <!-- Bootstrap 3.3.7 -->
      <link rel="stylesheet" href="../Estilos/bower_components/bootstrap/dist/css/bootstrap.min.css"/>
      <!-- Font Awesome -->
      <link rel="stylesheet" href="../Estilos/bower_components/font-awesome/css/font-awesome.min.css"/>
      <!-- Ionicons -->
      <link rel="stylesheet" href="../Estilos/bower_components/Ionicons/css/ionicons.min.css"/>
      <!-- Theme style -->
      <link rel="stylesheet" href="../Estilos/dist/css/AdminLTE.min.css"/>
      <!-- iCheck -->
      <link rel="stylesheet" href="../Estilos/plugins/iCheck/square/blue.css"/>
      <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
      <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
      <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
      <![endif]-->
      <!-- Google Font -->
      <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic"/>
   </head>
   <body class="hold-transition">
      <!-- Content Wrapper. Contains page content -->
      <div class="content">
         <!-- Content Header (Page header) -->
         <section class="content-header">
         </section>
         <section class="nuevoheader">
         </section>
         <!-- Main content -->
         <section class="invoice">
            <!-- title row -->
            <div class="row">
               <div class="col-xs-12">
                  <h2 class="page-header">
                     <i class="fa fa-globe"></i> IRO Trujillo
                     <small>
                        <p>Unidad de Tecnologias de la Informacion y Comunicaciones</p>
                     </small>
                  </h2>
               </div>
               <!-- /.col -->
            </div>
            <!-- info row -->
            <div class="row invoice-info">
               <div class="col-sm-4 invoice-col">
                  Responsable:
                  <address>
                     <strong>Rocky</strong><br>
                     <!--            795 Folsom Ave, Suite 600<br>
                        San Francisco, CA 94107<br>
                        Phone: (804) 123-5432<br>
                        Email: info@almasaeedstudio.com-->
                  </address>
               </div>
               <!-- /.col -->
               <div class="col-sm-4 invoice-col">
                  Analista - Programador:
                  <address>
                     <strong>Juan Aguirre</strong><br>
                     <!--            795 Folsom Ave, Suite 600<br>
                        San Francisco, CA 94107<br>
                        Phone: (555) 539-1037<br>
                        Email: john.doe@example.com-->
                  </address>
               </div>
               <!-- /.col -->
               <div class="col-sm-4 invoice-col" style="visibility:hidden">
                  Programador - Diseñador:
                  <address>
                     <strong>Fernando Salinas</strong><br>
                     <!--            795 Folsom Ave, Suite 600<br>
                        San Francisco, CA 94107<br>
                        Phone: (555) 539-1037<br>
                        Email: john.doe@example.com-->
                  </address>
               </div>
               <!-- /.col -->
            </div>
            <!-- /.row -->
            <!-- /.row -->
            <!-- this row will not appear when printing -->
         </section>
         <!-- /.content -->
      </div>
      <!-- /.content-wrapper -->
      <footer class="main-footer" style="margin-left: 0px;">
         <div class="pull-right hidden-xs">
            <b>Version</b> 1.0.0
         </div>
         <strong>Copyright &copy; 2019 <a href="http://www.irotrujillo.gob.pe/">IRO Trujillo</a>.</strong> 
      </footer>
      <!-- ./wrapper -->
      <!-- jQuery 3 -->
      <script src="../Estilos/bower_components/jquery/dist/jquery.min.js"></script>
      <!-- Bootstrap 3.3.7 -->
      <script src="../Estilos/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
      <!-- FastClick -->
      <script src="../Estilos/bower_components/fastclick/lib/fastclick.js"></script>
      <!-- AdminLTE App -->
      <script src="../Estilos/dist/js/adminlte.min.js"></script>
      <!-- AdminLTE for demo purposes -->
      <script src="../Estilos/dist/js/demo.js"></script>
   </body>
</html>
