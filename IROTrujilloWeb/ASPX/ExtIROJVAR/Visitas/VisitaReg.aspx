<%@ Page Language="C#" MasterPageFile="../../MasterPageExt.master" AutoEventWireup="true" CodeFile="VisitaReg.aspx.cs" Inherits="ASPX_ExtIROJVAR_Visitas_VisitaReg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <title>IRO</title>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/json2.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/funciones.js?vfd=1")%>"></script>
    <%--<link rel="stylesheet" media="screen" href="style1.css?id=1">--%>
    

    <script language="javascript" type="text/javascript">

        function fRegVisitaSalida(vIdVisita) {
            var params = new Object();
            params.vIdVisita = vIdVisita; 
<%--            params.vAnio = document.getElementById("<%=DDLAnio.ClientID%>").value; --%>
            params = JSON.stringify(params);
            
            $.ajax({
                type: "POST", url: "VisitaReg.aspx/SetRegVisitaSalida", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { 
                    if (result.d=="Correcto") {
                        alert("Registro Correcto");
                    }
                }, //success: LoadPrueba01, //Procesar 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divErrores").html(textStatus + ": " + XMLHttpRequest.responseText); 
                }
            });
        }


        function fcargaservidor() {
            var txtnom = document.getElementById("txtPerNombres1").value;
            var txtape = document.getElementById("txtPerApellidos1").value;

            if (txtnom.length>=4 || txtape.length>=4) {
                var params = new Object();
                params.vtxtape = txtnom; 
                params.vtxtnom = txtape; 
                params = JSON.stringify(params);
            
                $.ajax({
                    type: "POST", url: "VisitaReg.aspx/GetPersonalIRO", data: params, 
                    contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                    success: function (result) { $("#divShowPer").html(result.d); },  
                    error: function(XMLHttpRequest, textStatus, errorThrown) { 
                        alert(textStatus + ": " + XMLHttpRequest.responseText); 
                        $("#divErrores").html(textStatus + ": " + XMLHttpRequest.responseText); 
                    }
                });
            }
            else {
                $("#divShowPer").html("Nombres o Apellidos, al menos 4 Letras");
            }
        }
        
        function fSelPer(vdni, vnom, vcargo, vuoper) {
            $("#txtPerNombres").val(vnom);
            $("#txtPerNombres1").val(vnom);
            $("#txtPerDNI").val(vdni);
            $("#txtPerUnidad").val(vuoper);
            $("#txtPerUnidad1").val(vuoper);
        }

        function fRegIngreso() {
            if (document.getElementById("txtMotivo").value.length < 5) {
                alert("Registre Motivo [>5 Letras]");
                return false;
            }
            if (document.getElementById("<%=txtVisNombres.ClientID%>").value.length < 5) {
                alert("Registre Visitante [>5 Letras]");
                return false;
            }
            if (document.getElementById("<%=txtVisEntidad.ClientID%>").value.length < 5) {
                alert("Registre Entidad del Visitante [>5 Letras]");
                return false;
            }
            if (document.getElementById("txtPerDNI").value.length < 5) {
                alert("Seleccione Funcionario");
                return false;
            }

            var params = new Object();
            params.fechavisita = document.getElementById("txtFechaIng").value;
            params.VisNombres = document.getElementById("<%=txtVisNombres.ClientID%>").value;
            params.VisApellidos = document.getElementById("<%=txtVisApellidos.ClientID%>").value;
            params.VisDNI = document.getElementById("<%=txtVisDNI.ClientID%>").value;
            params.VisEntidad = document.getElementById("<%=txtVisEntidad.ClientID%>").value;
            params.VisMotivo = document.getElementById("txtMotivo").value;
            params.PerNombres = document.getElementById("txtPerNombres").value;
            params.PerApellidos = document.getElementById("txtPerApellidos").value;
            params.PerDNI = document.getElementById("txtPerDNI").value;
            params.PerUnidad = document.getElementById("txtPerUnidad").value;
            params.HoraIng = document.getElementById("txtHoraIng").value;
            params = JSON.stringify(params);
			//alert('hola');

            $.ajax({
                type: "POST", url: "VisitaReg.aspx/BtnRegIngreso", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) {
                    $("#divAtenciones").html(result.d);
                    if (result.d=="Correcto") { alert("Registro Conforme"); }
                }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divErrores").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});
		}

    </script>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div class="col-xs-8 col-xs-offset-2">
        <p class="cazador2">Registro de Visitas - IRO</p>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

   
    <form id="form1" runat="server" autocomplete="off">
      <!-- Content Wrapper. Contains page content -->
      <div class="content-wrapper">
         <!-- Content Header (Page header) -->
         <!-- Main content -->
         <section class="content">

<script>
    function fvaltxtdni() {
        if (document.getElementById("<%=txtProd.ClientID%>").value.length>=7) {
            return true;
        }
        else {
            alert("Al menos 7 Digitos o Letras para la Busqueda");
            return false;
        }
    }
</script>        

            <!--SEGUNDA FILA-->             
            <div class="row" style="display:block;">
                <div class="column" style="display:block">
                  <div class="col-md-1" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:LinkButton ID="bntBuscar" runat="server" class="btn btn-success" OnClick="bntBuscar_Click" onclientclick="return fvaltxtdni();" ><i class="fa fa-search"></i> Buscar</asp:LinkButton>
                        </div>
                     </div>
                  </div>
               </div>

              <div class="column">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:TextBox ID="txtProd" runat="server" class='form-control' placeholder="DNI" onFocus="this.select()" ></asp:TextBox>
                        </div>
                     </div>
                  </div>
               </div>

              <div class="column">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <button type="button" runat="server" class="btn bg-olive" onserverclick="btnNuevaVisita_Click"><i class="fa fa-fw fa-check"></i>Nuevo Registro</button>
                        </div>
                     </div>
                  </div>
               </div>
               
               <!-- /.box-header -->
            </div>
            <!--FIN SEGUNDA FILA-->

<script>
	var inputText = document.getElementById("<%=txtProd.ClientID%>");
	inputText.addEventListener("keyup", function(event) {
      if (event.keyCode === 13) {
		  event.preventDefault();
		  document.getElementById("<%=bntBuscar.ClientID%>").click();
      }
	});

</script>


            <!-- TERCERA FILA -->
            <div class="row">
               <div class="col-md-12">
                  <div class="box" >
                        <div class="box-header hide">
                            <h3 class="box-title" style="margin-top: -6px; margin-bottom: -11px; margin-left: -7px;">
                            
            <div class="input-group margin">
                            <div class="input-group-btn">
                                    <button class="btn btn-default" type="button" title="Max.: 1,000 Registros" onclick="exportTableToExcel('<%=GVtable.ClientID%>')"><i class="fa fa-download "></i>
                                    </button>
                            </div>
                            <div><input type="text" class="form-control" id="bscprod2" placeholder="Buscar" onkeyup="fBscTblHTML('bscprod2', '<%=GVtable.ClientID%>', 4)" >
                            </div>
            </div>
                            </h3>
                            
                        </div>
                     <!-- /.box-header -->
                     <div class="box-body table-responsive no-padding" id="habfiltro">
                        <asp:GridView ID="GVtable" runat="server" class="table table-condensed table-bordered"></asp:GridView>
                        <asp:Literal ID="LitTABL1" runat="server"></asp:Literal>
                         <asp:label runat="server" ID="lblMensaje"></asp:label>
                     </div>
                     <!-- /.box-body -->
                  </div>
                  <!-- /.box -->
               </div>
            </div>

            <!--FIN TERCERA FILA-->

            <!-- TERCERA FILA -->
    <div class="row" id="divRegVisita" style="display:none; ">
        <div class="col-md-12">
            <div class="box" >
                <div class="col-md-8" style="padding:  0px 30px 10px 30px; background-color:#87CACB">
					<span>Registro de Visita</span>
					<div class="row">
						<div class="col-md-3">
                            <input type="date" class="form-control" name="txtFechaIng" id="txtFechaIng" />
                            <code style="color:black; background-color:transparent">Fecha</code>
						</div>						
						<div class="col-md-3">
                            <input type="time" class="form-control" name="txtHoraIng" id="txtHoraIng" />
                            <code style="color:black; background-color:transparent">Hora de Ingreso</code>
						</div>
						<div class="col-md-2">
                            <button type="button" class="btn btn-danger" onclick="fRegIngreso();" >
                                <i class="fa fa-user-plus"></i> Registrar Ingreso
                            </button>
						</div>
					</div>

					<div class="row">
						<div class="col-md-12">
                            <input type="text" class="form-control" name="txtMotivo" id="txtMotivo" placeholder="Motivo - Asunto" />
                            <code style="color:black; background-color:transparent">Motivo</code>
						</div>						
					</div>
                    <div class="col-md-10 invisible"> <input type="text" /> </div>
				</div>

<script>
    
    var now = new Date();
    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var today = now.getFullYear()+"-"+(month)+"-"+(day) ;
    $('#txtFechaIng').val(today);

    var hora = ("0" + now.getHours()).slice(-2);
    var minu = ("0" + now.getMinutes()).slice(-2);
    $('#txtHoraIng').val(hora + ":" + minu);

</script>

    <div class="col-md-8" style="padding:  0px 30px 10px 30px; background-color:#4dd2ff">
		<span>Datos del Visitante</span>
		<div class="row">
			<div class="col-md-5">
                <input type="text" class="form-control" name="txtVisNombres" id="txtVisNombres" runat="server" placeholder="Nombre del Visitante" />
                <code style="color:black; background-color:transparent">Nombres</code>
			</div>
			<div class="col-md-5">
                <input type="text" class="form-control" name="txtVisApellidos" id="txtVisApellidos" runat="server" placeholder="Apellido del Visitante" />
                <code style="color:black; background-color:transparent">Apellidos</code>
			</div>
			<div class="col-md-2">
                <input type="text" class="form-control" name="txtVisDNI" id="txtVisDNI" runat="server" placeholder="DNI del Visitante" />
                <code style="color:black; background-color:transparent">DNI</code>
			</div>

			<div class="col-md-10">
                <input type="text" class="form-control" name="txtVisEntidad" id="txtVisEntidad" runat="server" placeholder="Entidad o Empresa del Visitante"/>
                <code style="color:black; background-color:transparent">Entidad</code>
			</div>
            <div class="col-md-10 invisible"> <input type="text" /> </div>
		</div>
	</div>

    <div class="col-md-8" style="padding:  0px 30px 10px 30px; background-color:#B9C85B">
		<span>Datos del Visitado</span>
		<div class="row">
			<div class="col-md-10">
                <input type="text" class="form-control" name="txtPerNombres1" id="txtPerNombres1" placeholder="Buscar Funcionario [Apellidos | Nombres]" />
                <input type="hidden" id="txtPerNombres" />
                <code style="color:black; background-color:transparent">Nombres</code>
			</div>
			<div style="display:none">
                <input type="text" class="form-control" name="txtPerApellidos1" id="txtPerApellidos1" disabled />
                <code style="color:black; background-color:transparent">Apellidos</code>
                <input type="hidden" id="txtPerApellidos" />
			</div>

			<div class="col-md-2 ">
                <a id="btnBuscaServidor" class="btn btn-info" data-toggle="modal" data-target="#modServidor" onclick="fcargaservidor()">
                    <i class="fa fa-user-plus"></i>Buscar Servidor
                </a>
			</div>

			<div class="col-md-2 invisible">
                <input type="text" class="form-control" name="txtPerDNI" id="txtPerDNI" />
                <code style="color:black; background-color:transparent">DNI</code>
			</div>

			<div class="col-md-10">
                <input type="hidden" id="txtPerUnidad" />
                <input type="text" class="form-control" name="txtPerUnidad1" id="txtPerUnidad1" placeholder="Unidad u Oficina del Funcionario" disabled />
                <code style="color:black; background-color:transparent">Unidad Organica IRO</code>
			</div>
		</div>
	</div>

            </div>
            <!-- /.box -->
        </div>
    </div>

            <!--FIN TERCERA FILA-->

         </section>
          <div id="divAtenciones">
              <asp:Literal ID="LitAtenciones" runat="server"></asp:Literal>
          </div>
          <div id="divDetAtenciones">
              <asp:Literal ID="LitDetAtenciones" runat="server"></asp:Literal>
          </div>
          <div id="divErrores">
              <asp:Literal ID="LitErrores" runat="server"></asp:Literal>
          </div>
         <!-- FIN MAIN CONTENT -->
         <!-- FIN MAIN CONTENT -->
         <!-- FIN MAIN CONTENT -->
         <!-- FIN MAIN CONTENT -->
        
         <!-- modal-sm | small || modal-lg | largo || modal-xl | extra largo -->
        <div class="modal modal-info fade" id="modServidor">
          <div class="modal-dialog ">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Servidores</h4>
              </div>
              <div class="modal-body">
                  <div id="divShowPer"></div>
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
    <script>
        //document.getElementById('LiCIEIm').className = "treeview menu-open";
        //$('#LiCIEIm').addClass('menu-open');
        document.getElementById("LiVisita").classList.add('menu-open');
        document.getElementById('UlVisita').style.display = 'block';
        document.getElementById("ulVisitaReg").classList.add('active');
        var f = new Date();

    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder4Script" Runat="Server">

    <script>
        document.getElementById("<%=bntBuscar.ClientID%>").focus();
        //document.getElementById("<%=txtProd.ClientID%>").select();

        function fShowRegVisita() {
            document.getElementById('divRegVisita').style.display = 'block';
            document.getElementById('<%=txtVisNombres.ClientID%>').focus();
        }

        if (document.getElementById("<%=lblMensaje.ClientID%>").innerText!="") {
            fShowRegVisita();
        }


    var inputPerNombres1 = document.getElementById("txtPerNombres1");
	inputPerNombres1.addEventListener("keyup", function(event) {
      if (event.keyCode === 13) {
		  event.preventDefault();
		  document.getElementById("btnBuscaServidor").click();
      }
	});


    </script>

</asp:Content>
