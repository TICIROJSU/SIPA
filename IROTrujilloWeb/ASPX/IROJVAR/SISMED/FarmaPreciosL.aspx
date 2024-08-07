<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FarmaPreciosL.aspx.cs" Inherits="ASPX_IROJVAR_SISMED_FarmaPreciosL" %>

<!DOCTYPE html>

<html style="height: auto; min-height: 100%;"><head><meta http-equiv="Content-Type" content="text/html"><meta charset="ISO-8859-1">
   <title>Optica</title>
    <script type="text/javascript" src="/ASPX/Estilos/Funciones/funciones.js?vfd=1"></script>
    

    <script language="javascript" type="text/javascript">

        function DetMontura(valmacen) {
            var params = new Object();
            params.vAlmacen = valmacen; 
            params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "RptP.aspx/GetDet", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divS").html(result.d) }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divS").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });
        }

    </script>

<meta http-equiv="X-UA-Compatible" content="IE=edge"><title>
	IRO
</title>
      <!-- Tell the browser to be responsive to screen width -->

      <link rel="shortcut icon" href="../../Estilos/imagenes/favicon.png" type="image/x-icon"><meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
      <!-- Bootstrap 3.3.7 -->
      <link rel="stylesheet" href="../../Estilos/bower_components/bootstrap/dist/css/bootstrap.min.css">
      <!-- Font Awesome -->
      <link rel="stylesheet" href="../../Estilos/bower_components/font-awesome/css/font-awesome.min.css">
      <!-- Ionicons -->
      <link rel="stylesheet" href="../../Estilos/bower_components/Ionicons/css/ionicons.min.css">
      <!-- Theme style -->
      <link rel="stylesheet" href="../../Estilos/dist/css/AdminLTE.min.css">
      <!-- AdminLTE Skins. Choose a skin from the css/skins
         folder instead of downloading all of them to reduce the load. -->
      <link rel="stylesheet" href="../../Estilos/dist/css/skins/_all-skins.min.css">
      <!-- bootstrap wysihtml5 - text editor -->
      <link rel="stylesheet" href="../../Estilos/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css">
      <!-- Boton subir -->
      <link rel="stylesheet" href="../../Estilos/BotonIrArriba/Botonsubir.css">
      <!-- iCheck -->
      <link rel="stylesheet" href="../../Estilos/plugins/iCheck/square/blue.css">
      <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
      <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
      <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
      <![endif]-->
      <!-- Google Font -->
      <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic">
      <script src="Estilos/Funciones/funciones.js"></script>
		<!-- Development version: -->
		<!-- <script>document.write('<script src="../Funciones/funciones.js?dev=' + Math.floor(Math.random() * 100) + '"\><\/script>');</script> -->
      <script type="text/javascript" src="/ASPX/Estilos/Funciones/jquery-1.11.1.min.js"></script>

<script type="text/javascript">
    function $_GET(q,s) {
    s = s ? s : window.location.search;
    var re = new RegExp('&'+q+'(?:=([^&]*))?(?=&|$)','i');
    return (s=s.replace(/^[?]/,'&').match(re)) ? (typeof s[1] == 'undefined' ? '' : decodeURIComponent(s[1])) : undefined;
    }
</script>    

<style>
    .modal { overflow: auto !important; }
</style>

<script type="text/javascript">
	var color = ['#FFFF00','#F80000']
	var num = 0;
	function cambiar() {
	  document.getElementById('trial1').style.color = color[num];
	  num++;
	  if (num==color.length) num=0;
	}
</script>

</head>
<body class="skin-blue sidebar-mini sidebar-collapse" onload="setInterval('cambiar()',500)" style="height: auto; min-height: 100%;">
    
    <div class="wrapper" style="height: auto; min-height: 100%;">
      <header class="main-header">
         <!-- Logo -->
         <a class="logo">
            <!-- mini logo for sidebar mini 50x50 pixels -->
            <span class="logo-mini"><b>I</b></span>
            <!-- logo for regular state and mobile devices -->
            <span class="logo-lg"><b>IRO</b></span>
         </a>
         <!-- Header Navbar: style can be found in header.less -->
         <nav class="navbar navbar-static-top ">
            <!-- Sidebar toggle button-->
            <a class="sidebar-toggle" data-toggle="push-menu" role="button">
            <span class="sr-only">Navegacion</span>
            </a>
            
    <div class="col-xs-8 col-xs-offset-2">
        <p class="cazador2">Stock de Productos</p>
    </div>

             



            <div class="navbar-custom-menu">
               <ul class="nav navbar-nav">
                  <li class="dropdown user user-menu">
                      <style>
                          @media (min-width: 768px){
                              #apurado{
                                  padding-bottom:35px;
                              }
                          }
                      </style>
                      <a href="#" class="dropdown-toggle" data-toggle="dropdown" id="apurado">
                     <img src="../../Estilos/imagenes/user.jpg" class="user-image" alt="User Image">
                     <span class="hidden-xs">
                         
                     </span>
                     </a>
                      <ul class="dropdown-menu" style="right: 0%;">
                        <!-- User image -->
                        <li class="user-header">
                           <img src="../../Estilos/imagenes/user.jpg" class="img-circle" alt="User Image">
                            <p>Juan Aguirre</p>
                        </li>
                        <!-- Menu Footer-->
                        <li class="user-footer">
                            <div class="pull-left" style="visibility:hidden">
                                <a href="../CambiarClave/" class="btn btn-default btn-flat" style="width: 98%">Cambiar clave</a>
                           </div>
                           <div class="pull-right">
                              <a style="display:none" href="../Login/CerrarSesion.aspx" class="btn btn-default btn-flat">Cerrar Sesión</a>
                              <a style="display:block" class="btn btn-default btn-flat" onclick="return functrigger();" href="javascript:__doPostBack('ctl00$ctl01','')">Cerrar Sesión</a>
                           </div>
<script>
    function functrigger() {
        varc = confirm("Esta seguro de Cerrar la Sesion?");
        return varc;
    }
</script>
                        </li>
                     </ul>
                  </li>

               </ul>
            </div>
         </nav>
      </header>
      <!-- Left side column. contains the logo and sidebar -->
      <!-- Left side column. contains the logo and sidebar -->
      <!-- Left side column. contains the logo and sidebar -->
      <aside class="main-sidebar">
         <!-- sidebar: style can be found in sidebar.less -->
         <section class="sidebar" style="height: auto;">
            <!-- sidebar menu: : style can be found in sidebar.less -->
            <ul class="sidebar-menu tree" data-widget="tree">
               <li class="header">PRINCIPAL</li>




<li class="treeview" style="height: auto;" id="LiCajaI">
  <a href="#">
	<i class="fa fa-dollar"></i> <span>Caja</span>
	<span class="pull-right-container">
	  <i class="fa fa-angle-left pull-right"></i>
	</span>
  </a>
  <ul class="treeview-menu" style="" id="UlCaja">
	<li id="ulCajaRep"><a href="../../IROJVAR/Caja/RecaudDia.aspx"><i class="fa fa-hand-pointer-o"></i> Recaudacion <br> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Reporte Diario
    </a></li>
  </ul>
</li>


<li class="treeview" style="height: auto;" id="LiSISMEDI">
  <a href="#">
	<i class="fa ion-erlenmeyer-flask-bubbles"></i> <span>Farmacia</span>
	<span class="pull-right-container">
	  <i class="fa fa-angle-left pull-right"></i>
	</span>
  </a>
  <ul class="treeview-menu" style="" id="UlSISMED">
	<li id="ulSISMEDRep"><a href="../../IROJVAR/SISMED/DisponibilidadIRO2.aspx">
        <i class="fa fa-hand-pointer-o"></i> Disponibilidad
    </a></li>
	<li id="ulDispoInterna"><a href="../../IROJVAR/SISMED/DispoIROInterna.aspx">
        <i class="fa fa-hand-pointer-o"></i> Dispo. Interna IRO
    </a></li>
    <li id="ulRecaudacionFarRep"><a href="../../IROJVAR/SISMED/RecaudacionFar.aspx">
        <i class="fa fa-hand-pointer-o"></i> Recaudacion 
    </a></li>
    <li id="ulFarStock"><a href="../../IROJVAR/SISMED/FarmaStock.aspx">
        <i class="fa fa-hand-pointer-o"></i> Stock 
    </a></li>
    <li id="ulFarICI"><a href="../../IROJVAR/SISMED/FarmaICI.aspx">
        <i class="fa fa-hand-pointer-o"></i> ICI SISMED 
    </a></li>
  </ul>
</li>



<li class="treeview" style="height: auto;" id="LiVariosI">
  <a href="#">
	<i class="fa ion-eye"></i> <span>Varios</span>
	<span class="pull-right-container">
	  <i class="fa fa-angle-left pull-right"></i>
	</span>
  </a>
  <ul class="treeview-menu" style="" id="UlVarios">
	<li id="ulVariosRep1"><a href="../../IROJVAR/Varios/VariosRep1.aspx"><i class="fa fa-hand-pointer-o"></i> Reporte <br> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Citas no Atendidas
    </a></li>
	<li id="ulInvSIGA"><a href="../../IROJVAR/Varios/InventarioSIGA.aspx"><i class="fa fa-hand-pointer-o"></i> Inventario SIGA 
    </a></li>
  </ul>
</li>



<li class="treeview" style="height: auto;" id="LiTrabPres">
  <a href="#">
	<i class="fa ion-eye"></i> <span>Registro Remoto</span>
	<span class="pull-right-container">
	  <i class="fa fa-angle-left pull-right"></i>
	</span>
  </a>
  <ul class="treeview-menu" style="" id="UlTrabPres">
	<li id="ulTrabReg"><a href="../../IROJVAR/TrabPres/RegTrabPres.aspx"><i class="fa fa-hand-pointer-o"></i> Trabajo Presencial 
    </a></li>
  </ul>
</li>

                <li><hr></li>
                <li><hr></li>

            </ul>
         </section>
         <!-- /.sidebar -->
      </aside>

<style>
    .super_script {
		font-size: 85%;
		vertical-align: super;
    }
</style>

    
    
        

    <form autocomplete="off" runat="server">

      <!-- Content Wrapper. Contains page content -->
      <div class="content-wrapper" style="min-height: 590px;">
         <!-- Content Header (Page header) -->
         <!-- Main content -->
         <section class="content">
        
            <!--SEGUNDA FILA-->             
            
            <!--FIN SEGUNDA FILA-->

<script>


</script>

            <!-- TERCERA FILA -->
            <div class="row">
               <div class="col-md-12">
                    <div class="col-md-2">
                        <asp:Button ID="btnBuscarProd" runat="server" Text="Buscar Productos" class="btn btn-info" OnClick="btnBuscarProd_Click" />
					</div>
                  <div class="box">


                  </div>
                  <!-- /.box -->
               </div>
            </div>

            <!--FIN TERCERA FILA-->

			 <div class="row">
               <div class="col-md-12">
                  <div class="box">
                        <div class="box-header">
                            <h3 class="box-title" style="margin-top: -6px; margin-bottom: -11px; margin-left: -7px;">
					<div class="input-group margin">
						<div class="input-group-btn">
								<button class="btn btn-default" type="button" title="Max.: 1,000 Registros" onclick="exportTableToExcel('GVtable')"><i class="fa fa-download "></i>
								</button>
						</div>
						<div><input type="text" class="form-control" id="bscprod2" placeholder="Buscar Nombre de Producto" onkeyup="fBscTblHTML('bscprod2', 'GVtable', 1)" autofocus="autofocus">
						</div>
					</div>
                            </h3>
                        </div>
                     <!-- /.box-header -->
                     <div class="box-body table-responsive no-padding" id="habfiltro" style="display:block">
                        <asp:GridView ID="GVtable" runat="server" class="table table-condensed table-bordered"></asp:GridView>
                        <asp:Literal ID="LitTABL1" runat="server"></asp:Literal>
                     </div>
                     
                     <!-- /.box-body -->
                  </div>
                  <!-- /.box -->
               </div>
            </div>

<script>

</script>

         </section>
          <div id="divAtenciones">
              
          </div>

          <div id="divErrores">
              
          </div>
         <!-- FIN MAIN CONTENT -->
         <!-- FIN MAIN CONTENT -->
         <!-- FIN MAIN CONTENT -->
         <!-- FIN MAIN CONTENT -->
        
         <!-- modal-sm | small || modal-lg | largo || modal-xl | extra largo -->

        <!-- /.modal -->
          
        <div class="modal modal-success fade" id="modalstkprod">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">×</span></button>
                <h4 class="modal-title">
                    <div class="form-group" style="display:none">
                      <label for="bscprod" class="col-sm-3 control-label">Buscar Red: </label>
                      <div class="col-sm-9">
	                    <input type="text" class="form-control" id="bscstkprod" placeholder="Producto" onkeyup="fBscTblHTML('bscstkprod', 'tblstkprod', 2)" autofocus="autofocus">
                      </div>
                    </div>
                </h4>
              </div>
              <div class="modal-body">
                  <div id="divStkProd"></div>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-outline pull-left" data-dismiss="modal">Close</button>
              </div>
            </div>
            <!-- /.modal-content -->
          </div>
          <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->
          

      </div>
      <!-- /.content-wrapper -->
   </form>

            <footer class="main-footer">
                <div class="pull-right hidden-xs">
                   <b>Version</b> 1.0.0
                </div>
                <strong>Copyright © 2019 <a href="http://www.irotrujillo.gob.pe/" target="_blank">IRO Trujillo</a>.</strong> 
            </footer>
      </div>
      <span class="ir-arriba fa fa-angle-double-up" id="upsubirpag"></span>
    

      <!-- jQuery 3 -->
      <script src="/ASPX/Estilos/bower_components/jquery/dist/jquery.min.js"></script>
      <!-- jQuery UI 1.11.4 -->
      
      <!-- Resolve conflict in jQuery UI tooltip with Bootstrap tooltip -->
      <script>
         $.widget.bridge('uibutton', $.ui.button);
      </script>
      <!-- Bootstrap 3.3.7 -->
      <script src="/ASPX/Estilos/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
      
      <!-- Bootstrap WYSIHTML5 -->
      <script src="/ASPX/Estilos/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js"></script>
      <!-- Slimscroll -->
      <script src="/ASPX/Estilos/bower_components/jquery-slimscroll/jquery.slimscroll.min.js"></script>
      <!-- FastClick -->
      <script src="/ASPX/Estilos/bower_components/fastclick/lib/fastclick.js"></script>
      <!-- AdminLTE App -->
      <script src="/ASPX/Estilos/dist/js/adminlte.min.js"></script>
      <!-- AdminLTE dashboard demo (This is only for demo purposes) -->
      <script src="/ASPX/Estilos/dist/js/pages/dashboard.js"></script>
      <!-- AdminLTE for demo purposes -->
      <script src="/ASPX/Estilos/dist/js/demo.js"></script>
      <!--BOTON SUBIR-->
      <script src="/ASPX/Estilos/BotonIrArriba/botonsubir.js"></script>
      <!-- ICHECK -->
      <script src="/ASPX/Estilos/plugins/iCheck/icheck.min.js"></script>
      <!-- JQUERY -->
      <script type="text/javascript" src="/ASPX/Estilos/Funciones/jquery-1.12.1.min.js"></script>

</body>
</html>
