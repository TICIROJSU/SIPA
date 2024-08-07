<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="ASPX_Registro_default" %>


<!DOCTYPE html>
<html>
   <head>
      <meta charset="utf-8">
      <meta http-equiv="X-UA-Compatible" content="IE=edge">
      <title>Registro</title>
      <link rel="shortcut icon" href="../../Estilos/imagenes/favicongeresa.ico" type="image/x-icon" />
      <!-- Tell the browser to be responsive to screen width -->
      <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
      <!-- Bootstrap 3.3.7 -->
      <link rel="stylesheet" href="../../Estilos/bower_components/bootstrap/dist/css/bootstrap.min.css">
      <!-- daterange picker -->
      <link rel="stylesheet" href="../../Estilos/bower_components/bootstrap-daterangepicker/daterangepicker.css">
      <!-- bootstrap datepicker -->
      <link rel="stylesheet" href="../../Estilos/bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css">
      <!-- iCheck for checkboxes and radio inputs -->
      <link rel="stylesheet" href="../../Estilos/plugins/iCheck/all.css">
      <!-- Select2 -->
      <link rel="stylesheet" href="../../Estilos/bower_components/select2/dist/css/select2.min.css">
      <!-- Font Awesome -->
      <link rel="stylesheet" href="../../Estilos/bower_components/font-awesome/css/font-awesome.min.css">
      <!-- Ionicons -->
      <link rel="stylesheet" href="../../Estilos/bower_components/Ionicons/css/ionicons.min.css">
      <!-- Theme style -->
      <link rel="stylesheet" href="../../Estilos/dist/css/AdminLTE.min.css">
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
      <script src="../../Estilos/Funciones/funciones.js" type="text/javascript"></script>
      
<script>
	function fShowEESS() {
        var params = new Object();
        params.vEESS = document.getElementById("txtNomEESS").value; 
        params = JSON.stringify(params);

        $.ajax({
            type: "POST", url: "default.aspx/GetEESS", data: params, 
            contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
            success: function (result) { $("#bodyModEESS").html(result.d) }, 
            error: function(XMLHttpRequest, textStatus, errorThrown) { 
                alert(textStatus + ": " + XMLHttpRequest.responseText); 
                $("#bodyModEESS").html(textStatus + ": " + XMLHttpRequest.responseText);
            }
        });
    }

    function gSelEESS(vcod, vdisa, vprov, vdist, veess, vubi){
        document.getElementById("txtCodEESS").value = vcod;
        document.getElementById("txtNomEESS").value = veess;
    }




</script>

   </head>
   <body class="hold-transition register-page">
      <div class="register-box" style="width: 468px; margin: 2% auto;">
         <div class="register-logo" style="margin-bottom: 15px;">
            <b>REGISTRO</b>
         </div>
         <div class="register-box-body" >
            <form runat="server">
               <div class="row">
                  <div class="column">
                     <div class="col-md-3" style="float: left;">
                        <div class="form-group">
                           <button type="button" class="btn btn-default" style="margin-top: 25px" onclick="location='../'"><i class="fa fa-reply"></i> Regresar</button>
                        </div>
                     </div>
                  </div>
                  <div class="column">
                     <div class="col-md-6" style="float: right;">
                        <div class="form-group">
                           <label>FECHA:</label>
                           <div class="input-group date">
                              <div class="input-group-addon">
                                 <i class="fa fa-calendar"></i>
                              </div>                               
                              <input type="text" class="form-control" id="datepicker" name="datepicker" placeholder="Fecha" disabled>

                           </div>
                           <!-- /.input group -->
                        </div>
                        <!-- /.form group -->
                     </div>
                  </div>
               </div>
               <div class="row">
                  <div class="column">
                     <div class="col-md-6 pull-left">
                        <label>DATOS PERSONALES</label>
                        <div class="form-group has-feedback">
                            <asp:TextBox ID="txtDNIreg" runat="server" class="form-control" placeholder="Ingrese DNI" required></asp:TextBox>                           
                        </div>
                     </div>
                  </div>

                  <div class="column hide">
                     <div class="col-md-5 pull-right">
                         <a class="btn btn-primary btn-block btn-flat" style="margin-right: 0px; margin-top: 25px" id="btnBuscar2" name="btnBuscar" onclick="VerFicha();">Buscar</a>                    
                     </div>
                  </div>
               </div>
<script>
    function VerFicha() {
        location = "HojaRegistro.aspx?Usuario=" + document.getElementById("txtDNIreg").value; 
    }
</script>
               <div class="row">
                  <div class="col-md-12">
                     <div class="form-group has-feedback">
                         <asp:TextBox ID="txtNOMBREreg" runat="server" class="form-control" placeholder="Ingrese su(s) nombre(s)" required="true"></asp:TextBox>                        
                        <span class="glyphicon glyphicon-user form-control-feedback"></span>
                     </div>
                  </div>
                  <div class="col-md-12">
                     <div class="form-group has-feedback">
                         <asp:TextBox ID="txtAPELLIDOSreg" runat="server" class="form-control" placeholder="Ingrese sus apellidos" required></asp:TextBox>

                        
                        <span class="glyphicon glyphicon-user form-control-feedback"></span>
                     </div>
                  </div>
                  <div class="col-md-12">
                     <!-- Date dd/mm/yyyy -->
                     <div class="form-group">
                        <div class="input-group">
                           <div class="input-group-addon">
                              <i class="fa fa-calendar"></i>
                           </div>
                            <asp:TextBox ID="txtFECNACreg" runat="server" class="form-control" placeholder="Ingrese su fecha de nacimiento" type="date" required></asp:TextBox>                           
                        </div>
                        <!-- /.input group -->
                     </div>
                  </div>
                  <div class="col-md-12">
                     <div class="form-group has-feedback">
                         <asp:TextBox type="email" ID="txtEMAILreg" runat="server" class="form-control" placeholder="Ingrese su Email" required></asp:TextBox>
                        <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
                     </div>
                  </div>
                  <div class="col-md-12">
                     <!-- phone mask -->
                     <div class="form-group">
                        <div class="input-group">
                           <div class="input-group-addon">
                              <i class="fa fa-phone"></i>
                           </div>
                            <asp:TextBox type="text" ID="txtTELEFONOreg" runat="server" class="form-control" placeholder="Ingrese su número de teléfono" required></asp:TextBox>
                        </div>
                        <!-- /.input group -->
                     </div>
                     <!-- /.form group -->
                  </div>
               </div>
              
               <div class="row">
                  <div class="column pull-left">
                     <div class="col-md-12">
                        <label>DATOS LABORALES</label>
                        <div class="form-group has-feedback">
                         <asp:DropDownList ID="ddlCargo" runat="server" class="form-control" >
                            <asp:ListItem value="ASISTENTE EN SERVICIOS SOCIALES">ASISTENTE EN SERVICIOS SOCIALES</asp:ListItem>
                            <asp:ListItem value="ENFERMERA(O)">ENFERMERA(O)</asp:ListItem>
                            <asp:ListItem value="MEDICO">MEDICO</asp:ListItem>
                            <asp:ListItem value="OBSTETRA">OBSTETRA</asp:ListItem>
                            <asp:ListItem value="TECNICO(A) ASISTENCIAL">TECNICO(A) ASISTENCIAL</asp:ListItem>
                            <asp:ListItem value="TECNICO(A) EN ENFERMERIA">TECNICO(A) EN ENFERMERIA</asp:ListItem>
                            <asp:ListItem value="ASISTENTE(A) TECNICO(A) SECRETARIAL">ASISTENTE(A) TECNICO(A) SECRETARIA</asp:ListItem>
                            <asp:ListItem value="OTRO">OTRO</asp:ListItem>
                         </asp:DropDownList>    



                        </div>
                     </div>
                  </div>

               </div>
               <div class="row">
                  <div class="column">
                     <div class="col-xs-12">
                         <div class="input-group margin">
                             <asp:TextBox ID="txtNomEESS" runat="server" class="form-control" placeholder="Buscar Nombre del EESS/IPRESS"></asp:TextBox>
	                        <span class="input-group-btn">
		                        <div class="btn btn-info btn-flat" onclick="fShowEESS()" data-toggle="modal" data-target="#modalEESS">...</div>
	                        </span>
                        </div>
                     </div>
                  </div>       
               </div>
               <div class="row">
                  <div class="column">
                     <div class="col-xs-12">
                        <div class="form-group hide" style="margin-right: 0px; margin-top: 0px">
                            <asp:TextBox ID="txtCodEESS" runat="server" class="form-control" placeholder="Codigo del EESS/IPRESS" ></asp:TextBox>
                        </div>
                     </div>
                  </div>       
               </div>
               <div class="row">
                  <div class="col-md-12">
                     <label>ACUERDO LEGAL</label>
                     <div class="nav-tabs-custom" style="border-width: 1px; border-color: rgb(210, 214, 222); border-style: solid;">
                        <div class="tab-content" style="overflow-y: scroll; height: 200px" >
                           <div class="tab-pane active" id="tab_1">

                              <p>La <b>información</b> brindada por esta página, tiene carácter de <b>confidencial</b>, por lo que al acceder a esta página, <b>usted está aceptando los términos y condiciones</b> del Sitio Web, de acuerdo a la <a href="https://www.minjus.gob.pe/wp-content/uploads/2013/04/LEY-29733.pdf">Ley 29733</a>, <b>Ley de Proteccion de Datos Personales</b>.  </p>
                              <p>Cualquier distribución, publicación o explotación comercial o promocional del Sitio Web, o de cualquiera de los contenidos, datos o materiales en el Sitio Web, está estrictamente prohibida, a menos de que usted haya recibido el previo permiso expreso por escrito del personal autorizado del Instituto Regional de Oftalmologia.</p>
                              <p>En el caso de faltar con los términos y condiciones de uso del presente Sitio Web, se procederá con la ejecución de las <b>sanciones</b> del Art. 37 de la Ley 29733. </p>
                           </div>
                           <!-- /.tab-pane -->
                        </div>
                        <!-- /.tab-content -->
                     </div>
                     <!-- nav-tabs-custom -->
                  </div>
               </div>

               <div class="row">
                  <div class="column">
                     <div class="col-xs-8">
                        <div class="checkbox icheck" >
                            <asp:CheckBox ID="cbkAcepto" runat="server" Text="Acepto el Acuerdo Legal" required="true" Checked="true" />
                            <asp:Label ID="lblAcepto" runat="server" Text="" style="color:red"></asp:Label>
                        </div>
                     </div>
                     <!-- /.col -->
                  </div>
<script>
    function confirmar() {
        varc = confirm("Los Datos Registrados, son correctos?");
        return varc;
    }
</script>
                  <div class="column">
                     <div class="col-xs-4 pull-right" >
                         <button type="submit" class="btn btn-primary btn-block btn-flat" style="margin-right: 0px; margin-top: 5px" id="btnRegistrar2" name="btnRegistrar" value="Registrar" onserverclick="btnRegistrar_Click" >Registrar</button>
                         <div style="display:none">
                             <asp:LinkButton ID="btnRegistrar" runat="server" class="btn btn-primary btn-block btn-flat" style="margin-right: 0px; margin-top: 5px" OnClick="btnRegistrar_Click" >Registrar</asp:LinkButton>
<%--                             <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-primary btn-block btn-flat" style="margin-right: 0px; margin-top: 5px" OnClick="btnRegistrar_Click">Registrar</asp:LinkButton>--%>
                         </div>
                     </div>
                     <!-- /.col -->
                  </div>

         <!-- modal-sm | small || modal-lg | largo || modal-xl | extra largo -->
        <div class="modal modal-info fade" id="modalEESS">
          <div class="modal-dialog modal-lg ">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Establecimiento de Salud - IPRESS</h4>
              </div>
              <div class="modal-body" id="bodyModEESS">
                <p>One fine body&hellip;</p>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-outline pull-left" data-dismiss="modal">Close</button>
                <button type="button" class="btn btn-success btn-outline" data-toggle="modal" data-target="#modal-success">
                    Otro Modal
                </button>
              </div>
            </div>
            <!-- /.modal-content -->
          </div>
          <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->

               </div>
            </form>

         </div>
         <!-- /.form-box -->
      </div>
      <!-- /.register-box -->
      <!-- jQuery 3 -->
      <script src="../../Estilos/bower_components/jquery/dist/jquery.min.js"></script>
      <!-- Bootstrap 3.3.7 -->
      <script src="../../Estilos/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
      <!-- iCheck -->
      <script src="../../Estilos/plugins/iCheck/icheck.min.js"></script>
      <!-- Select2 -->
      <script src="../../Estilos/bower_components/select2/dist/js/select2.full.min.js"></script>
      <!-- InputMask -->
      <script src="../../Estilos/plugins/input-mask/jquery.inputmask.js"></script>
      <script src="../../Estilos/plugins/input-mask/jquery.inputmask.date.extensions.js"></script>
      <script src="../../Estilos/plugins/input-mask/jquery.inputmask.extensions.js"></script>
      <!-- date-range-picker -->
      <script src="../../Estilos/bower_components/moment/min/moment.min.js"></script>
      <script src="../../Estilos/bower_components/bootstrap-daterangepicker/daterangepicker.js"></script>
      <!-- bootstrap datepicker -->
      <script src="../../Estilos/bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
      <!-- bootstrap color picker -->
      <script src="../../Estilos/bower_components/bootstrap-colorpicker/dist/js/bootstrap-colorpicker.min.js"></script>
      <!-- bootstrap time picker -->
      <script src="../../Estilos/plugins/timepicker/bootstrap-timepicker.min.js"></script>
      <!-- SlimScroll -->
      <script src="../../Estilos/bower_components/jquery-slimscroll/jquery.slimscroll.min.js"></script>
      <!-- iCheck 1.0.1 -->
      <script src="../../Estilos/plugins/iCheck/icheck.min.js"></script>
      <!-- FastClick -->
      <script src="../../Estilos/bower_components/fastclick/lib/fastclick.js"></script>
      <!-- AdminLTE App -->
      <script src="../../Estilos/dist/js/adminlte.min.js"></script>
      <!-- AdminLTE for demo purposes -->
      <script src="../../Estilos/dist/js/demo.js"></script>
      <script>
         $(function () {
           $('input').iCheck({
             checkboxClass: 'icheckbox_square-blue',
             radioClass: 'iradio_square-blue',
             increaseArea: '20%' /* optional */
           });
         });
      </script>
      <!-- Page script -->
      <script>

      </script>
      <script languaje="javascript">
         // creamos la fecha en la variable date
         var date = new Date();
         // Luego le sacamos los datos año, dia, mes 
         // y numero de dia de la variable date
         var año = date.getFullYear();
         var mes = date.getMonth();
         var ndia = date.getDate();
         //Damos a los meses el valor en número
         if (mes==11) {var mes="12"}
         if (mes==10) {var mes="11"}
         if (mes==9) {var mes="10"}
         if (mes==8) {var mes="09"}
         if (mes==7) {var mes="08"}
         if (mes==6) {var mes="07"}
         if (mes==5) {var mes="06"}
         if (mes==4) {var mes="05"}
         if (mes==3) {var mes="04"}
         if (mes==2) {var mes="03"}
         if (mes==1) {var mes="02"}
         if (mes==0) {var mes="01"}
         //juntamos todos los datos en una variable
         ndia=rellenaizq(ndia, 2);
         var fecha = ndia + "/" + mes + "/" + año;
         //y procedemos a escribir dicha fecha
         //document.write (fecha)
         document.getElementById("datepicker").value=fecha;
      </script>
      <!-- jQuery 3 -->
      <script>  
          $(function () {
          //Initialize Select2 Elements
          $('.select2').select2()
          })
      </script>  

   </body>
</html>