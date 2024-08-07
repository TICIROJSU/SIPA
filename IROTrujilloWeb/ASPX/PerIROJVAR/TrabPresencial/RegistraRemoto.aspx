<%@ Page Language="C#" MasterPageFile="../../MasterPagePer.master" AutoEventWireup="true" CodeFile="RegistraRemoto.aspx.cs" Inherits="ASPX_PerIROJVAR_TrabPresencial_RegistraRemoto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>Remoto/Presencial</title>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/json2.js")%>"></script>
    <%--<link rel="stylesheet" media="screen" href="style1.css?id=1">--%>

    <script language="javascript" type="text/javascript">

		function buscarProd() {
            var params = new Object();
            params.vbuscarProd = document.getElementById("txtbuscaprod").value;
            params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "ProcIngresos.aspx/GetbuscarProd", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divShowProd").html(result.d) }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divShowProd").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});

			document.getElementById("txtbuscaprod").value = "";
			document.getElementById("txtbuscaprod").focus();
		}

		function GPresencialxxxx(vtipoTrabajo) {
			var vIDPER = document.getElementById("<%=txtIdPer.ClientID%>").value;
			var vDNIPER = document.getElementById("<%=txtDNI.ClientID%>").value;
			var vCantDias = document.getElementById("<%=txtCantDias.ClientID%>").value;

			for (var i = 1; i < vCantDias; i++){
				var vhFecDia = document.getElementById("hFecDia" + i).value;
				if (document.getElementById("chkDia" + i).checked) {
					if (document.getElementById("spDiaMsg" + i).innerHTML == "") {
						GPresencialInsert(vIDPER, vDNIPER, vhFecDia, vtipoTrabajo);
					}
					else {
						GPresencialEdit(vIDPER, vDNIPER, vhFecDia, vtipoTrabajo);
					}
					document.getElementById("spDiaMsg" + i).innerHTML = vtipoTrabajo;
				}

			}
		}

		function GPresencialInsert(vIdPer, vDNIPer, vFechaTrab, vTipoTrab) {
            var params = new Object();
            params.vIdPer = vIdPer;
            params.vDNIPer = vDNIPer;
            params.vFechaTrab = vFechaTrab;
            params.vTipoTrab = vTipoTrab;
            params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "RegistraRemoto.aspx/SetTipoTrabajoInsert", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divmsj1").html(result.d) }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divmsj1").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});
		}

		function GPresencialEdit(vIdPer, vDNIPer, vFechaTrab, vTipoTrab) {
            var params = new Object();
            params.vIdPer = vIdPer;
            params.vDNIPer = vDNIPer;
            params.vFechaTrab = vFechaTrab;
            params.vTipoTrab = vTipoTrab;
            params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "RegistraRemoto.aspx/SetTipoTrabajoEdit", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divmsj2").html(result.d) }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divmsj2").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});
		}

		function fTrabDescr(vDia, vFechaTrab) {
			var vIDPER = document.getElementById("<%=txtIdPer.ClientID%>").value;
			var vDNIPER = document.getElementById("<%=txtDNI.ClientID%>").value;
			var vDatosPER = document.getElementById("<%=txtDatosPer.ClientID%>").value;
			document.getElementById("dmTrabDTitle").innerHTML = vFechaTrab + " | " + vDNIPER + "-" + vDatosPER;
			document.getElementById("inputDiaTrab").value = vDia;
			document.getElementById("<%=txtDiaSel.ClientID%>").value = vFechaTrab;

			var params = new Object();
            params.vIdPer = vIDPER;
            params.vDNIPer = vDNIPER;
            params.vFechaTrab = vFechaTrab;
            params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "RegistraRemoto.aspx/GetTipoTrabDescrip", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
				success: function (result) {
					//$("#divmsj3").html(result.d); 
					var aResult = result.d.split("||sep||");
					$("#divDetRecaudacion").html(aResult[0]);

					document.getElementById("<%=txtTrabActividad.ClientID%>").value = aResult[0];
                    document.getElementById("inputTxtTrabActividad").value = aResult[0];
                    document.getElementById("<%=divDiaSelRuta.ClientID%>").innerHTML = aResult[1];
                    document.getElementById("<%=txtDiaSelRuta.ClientID%>").value = aResult[1];
                    $("#divnomarchivo").html(aResult[2]);
                    
				}, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divmsj3").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});

			document.getElementById("txtActHora").value = "";
			document.getElementById("txtActAct").value = "";
			//document.getElementById("txtActHora").focus();
			//$("#txtActHora").focus();
			//$('#txtActHora').focus().val($('#txtActHora').val());
		}

//$(document).ready(function(){
//    $("#modalTrabDescrip").on('shown.bs.modal', function(){
//        $(this).find('#txtActHora').focus();
//    });
//});

		function GActividad() {
			var vIDPER = document.getElementById("<%=txtIdPer.ClientID%>").value;
			var vDNIPER = document.getElementById("<%=txtDNI.ClientID%>").value;
			var vCantDias = document.getElementById("<%=txtCantDias.ClientID%>").value;
			var vDia = document.getElementById("inputDiaTrab").value;
			var vActividad = document.getElementById("<%=txtTrabActividad.ClientID%>").value;

			var vhFecDia = document.getElementById("hFecDia" + vDia).value;
			if (document.getElementById("inputTxtTrabActividad").value == "") {
				GActividadInsert(vIDPER, vDNIPER, vhFecDia, vActividad);
			}
			else {
				GActividadEdit(vIDPER, vDNIPER, vhFecDia, vActividad);
			}
		}

		function GActividadInsert(vIdPer, vDNIPer, vFechaTrab, vActividad) {
            var params = new Object();
            params.vIdPer = vIdPer;
            params.vDNIPer = vDNIPer;
            params.vFechaTrab = vFechaTrab;
            params.vActividad = vActividad;
            params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "RegistraRemoto.aspx/SetTipoTrabajoDescripInsert", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) {
                    $("#divmsj1").html(result.d);
                    if (result.d == "FechaIncorrecta") {
                        alert("Las actividades, solo pueden registrarse en el mismo dia.");
                    }
                }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divmsj1").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});
		}

		function GActividadEdit(vIdPer, vDNIPer, vFechaTrab, vActividad) {
            var params = new Object();
            params.vIdPer = vIdPer;
            params.vDNIPer = vDNIPer;
            params.vFechaTrab = vFechaTrab;
            params.vActividad = vActividad;
            params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "RegistraRemoto.aspx/SetTipoTrabajoDescripEdit", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) {
                    $("#divmsj2").html(result.d);
                    if (result.d == "FechaIncorrecta") {
                        alert("Las actividades, solo pueden Modificarse en el mismo dia.");
                    }
                }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divmsj2").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});
		}


    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div class="col-xs-8 col-xs-offset-2">
        <p class="cazador2">Trabajo Presencial / Remoto</p>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

   
    <form id="form1" runat="server" autocomplete="off">
      <!-- Content Wrapper. Contains page content -->
      <div class="content-wrapper">
         <!-- Content Header (Page header) -->
         <!-- Main content -->

	<section class="content">

<div style="display:none">
	<asp:TextBox ID="txtCantDias" runat="server" ></asp:TextBox>
	<asp:TextBox ID="txtIdPer" runat="server" ></asp:TextBox>
	<asp:TextBox ID="txtDNI" runat="server" ></asp:TextBox>
	<asp:TextBox ID="txtDatosPer" runat="server" ></asp:TextBox>
	<asp:TextBox ID="txtDiaSel" runat="server" ></asp:TextBox>
	<asp:TextBox ID="txtDiaSelRuta" runat="server" ></asp:TextBox>
    <div id="divDiaSelRuta" runat="server"></div>
</div>


		<!--SEGUNDA FILA-->             
            <div class="row" style="display:block;">
               <div class="column">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:TextBox ID="txtEESS" runat="server" Text="05197-IRO" class='form-control' ReadOnly="True"></asp:TextBox>
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                           <asp:DropDownList ID="DDLAnio" runat="server" class='form-control' >
                              <asp:ListItem Value="2020">2020</asp:ListItem>
                              <asp:ListItem Value="2021">2021</asp:ListItem>
                              <asp:ListItem Value="2022">2022</asp:ListItem>
                              <asp:ListItem Value="2023">2023</asp:ListItem>
                              <asp:ListItem Value="2024">2024</asp:ListItem>
                              <asp:ListItem Value="2025">2025</asp:ListItem>
                              <asp:ListItem Value="2026">2026</asp:ListItem>
                           </asp:DropDownList>
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                           <asp:DropDownList ID="DDLMes" runat="server" class='form-control'>
                              <asp:ListItem Value="1" Selected="true">Enero</asp:ListItem>
                              <asp:ListItem Value="2">Febrero</asp:ListItem>
                              <asp:ListItem Value="3">Marzo</asp:ListItem>
                              <asp:ListItem Value="4">Abril</asp:ListItem>
                              <asp:ListItem Value="5">Mayo</asp:ListItem>
                              <asp:ListItem Value="6">Junio</asp:ListItem>
                              <asp:ListItem Value="7">Julio</asp:ListItem>
                              <asp:ListItem Value="8">Agosto</asp:ListItem>
                              <asp:ListItem Value="9">Setiembre</asp:ListItem>
                              <asp:ListItem Value="10">Octubre</asp:ListItem>
                              <asp:ListItem Value="11">Noviembre</asp:ListItem>
                              <asp:ListItem Value="12">Diciembre</asp:ListItem>
                           </asp:DropDownList>
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column" style="display:block">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:LinkButton ID="bntBuscar" runat="server" class="btn btn-info" OnClick="bntBuscar_Click" ><i class="fa fa-search"></i> Consultar</asp:LinkButton>
                        </div>
                     </div>
                  </div>
               </div>



               <!-- /.box-header -->
            </div>
            <!--FIN SEGUNDA FILA-->



        <div class="col-md-9">
          <div class="box box-primary">
            <div class="box-body no-padding fc ">
				<div class="fc-toolbar fc-header-toolbar">
					<div class="fc-left">
						<div class="fc-button-group">
							<%--<button type="button" class="fc-prev-button fc-button fc-state-default fc-corner-left" ><span class="fc-icon fc-icon-left-single-arrow"></span></button>
							<button type="button" class="fc-next-button fc-button fc-state-default fc-corner-right" ><span class="fc-icon fc-icon-right-single-arrow"></span></button>--%>
							<div class="btn btn-danger fc-corner-right" onclick="GPresencial(' ');" >Eliminar</div>
						</div>
					</div>
					<div class="fc-right">
						<div class="fc-button-group">
							<div class="btn btn-success fc-corner-left" onclick="G-Presencial('PRESENCIAL');" >Presencial</div>
							<button type="button" class="fc-state-default fc-corner-left fc-state-active">month</button>
							<div class="btn btn-warning fc-corner-right" onclick="G-Presencial('REMOTO');" >Remoto</div>
							<%--<button type="button" class="fc-button fc-state-default">week</button>
							<button type="button" class="fc-button fc-state-default fc-corner-right">day</button>--%>
						</div>
					</div>
					<div class="fc-center">
						<h2><asp:Literal ID="LitPeriodo" runat="server"></asp:Literal></h2>
					</div>
					<div class="fc-clear"></div>
			  </div>

			<asp:Literal ID="LitCalendar" runat="server"></asp:Literal>


<%--			  <table>
				<thead class='fc-head'>
					<tr>
						<th>Dom</th>
						<th>Lun</th><th>Mar</th><th>Mie</th>
						<th>Jue</th><th>Vie</th><th>Sab</th>
					</tr>
				</thead>
				<tbody class='fc-body'>
					<tr height='70'>
						<td>
							<h3><span id='' class='pull-right'>1</span></h3>
							<span >Meeting</span>
						</td>
					</tr>
				</tbody>
			  </table>--%>
			  
				
			</div>
		  </div>
		</div>

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
		  <div id="divmsj1"></div><div id="divmsj2"></div><div id="divmsj3"></div><div id="divmsj4"></div>
		  <div id="divmsj5"></div><div id="divmsj6"></div><div id="divmsj7"></div><div id="divmsj8"></div>
       <asp:Label id="UploadStatusLabel" runat="server"> </asp:Label>
         <!-- FIN MAIN CONTENT -->
         <!-- FIN MAIN CONTENT -->
         <!-- FIN MAIN CONTENT -->
         <!-- FIN MAIN CONTENT -->
        
         <!-- modal-sm | small || modal-lg | largo || modal-xl | extra largo -->
        
        <div class="modal modal-warning fade" id="modalShowProd">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Productos</h4>
              </div>
              <div class="modal-body">
                <%--<p>One fine body&hellip;</p>--%>
                  <div id="divShowProd"></div>
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

        <div class="modal modal-info fade" id="modalTrabDescrip">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Descripcion del Trabajo</h4>
				  <div id="dmTrabDTitle"></div>
				  <input type="hidden" id="inputDiaTrab" />
				  <input type="hidden" id="inputTxtTrabActividad" />
              </div>
              <div class="modal-body">
				  <p>
					<div class="input-group input-group">

			<div class="row" style="display:block;">
               <div class="column">
                  <div class="col-md-1" >
                     <div class="box-body">
						 Hora: 
                     </div>
                  </div>
                  <div class="col-md-2" >
                     <div class="box-body">
                            <input type="text" id="txtActHora" class="form-control" placeholder="00:00" />
                     </div>
                  </div>
                  <div class="col-md-1" >
                     <div class="box-body">
						 Actividad: 
                     </div>
                  </div>
                  <div class="col-md-8" >
                     <div class="box-body">
                        <div class="form-group">
							<input type="text" id="txtActAct" class="form-control" />
                        </div>
                     </div>
                  </div>
               </div>
            </div>
						<span class="input-group-btn">
							<div class="btn btn-primary btn-flat" onclick="ActivAgregar();" >Agregar</div>
						</span>
					</div>
				  </p>
				  <asp:textbox ID="txtTrabActividad" runat="server" TextMode="MultiLine" Rows="8" class="form-control"></asp:textbox>
				  <p align="right">
					<button type="button" class="btn btn-primary" data-dismiss="modal" onclick="GActividad()" >Guardar Actividad</button>
				  </p>
				  <br />
				<div class="input-group input-group">
					<asp:FileUpload ID="FileUpload1" runat="server" class="form-control" />
					<span class="input-group-btn">
						<asp:Button id="UploadButton" class="btn btn-primary btn-flat" Text="Guardar Archivo Evidencia" OnClick="UploadButton_Click" runat="server"> </asp:Button>    
					</span>
				</div>
				<div class="input-group input-group">
                    <div id="divnomarchivo">Sin Archivo</div>
				</div>
				  <br />
				  <p>
					  <asp:Button ID="btnDescarga" runat="server" Text="Descarga Archivo" class="btn btn-primary btn-flat" OnClick="btnDescarga_Click" />
				  </p>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-outline pull-left" data-dismiss="modal">Cerrar</button>

              </div>
            </div>
            <!-- /.modal-content -->
          </div>
          <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->


<script>
	function ActivAgregar() {
		//alert("01");
		var TextoActual = document.getElementById("<%=txtTrabActividad.ClientID%>").value;
		var TextoHora = document.getElementById("txtActHora").value;
		var TextoActiv = document.getElementById("txtActAct").value;
		
		document.getElementById("<%=txtTrabActividad.ClientID%>").value = TextoActual + TextoHora + " - " + TextoActiv + "|*|\n";
	}
</script>

      </div>
      <!-- /.content-wrapper -->
   </form>
    <script>
        //document.getElementById('LiCIEIm').className = "treeview menu-open";
        //$('#LiCIEIm').addClass('menu-open');
        document.getElementById("LiTrabPres").classList.add('menu-open');
        document.getElementById('UlTrabPres').style.display = 'block';
        document.getElementById("ulTrabReg").classList.add('active');
        var f = new Date();

    </script>
</asp:Content>
