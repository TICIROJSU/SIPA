<%@ Page Language="C#" MasterPageFile="../../MasterPage.master" AutoEventWireup="true" CodeFile="ProgServicios.aspx.cs" Inherits="ASPX_IROJVAR_ProgCupos_ProgServicios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>Servicios</title>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/json2.js")%>"></script>
    <%--<link rel="stylesheet" media="screen" href="style1.css?id=1">--%>

    <script language="javascript" type="text/javascript">

        function btnGuardar() {
            var params = new Object();
            params.COD_SER = document.getElementById("<%=ddlServicio.ClientID%>").value;
            params.PIS_SER = document.getElementById("<%=ddlPiso.ClientID%>").value;
            params.Tur_Ser = document.getElementById("<%=ddlTurno.ClientID%>").value;
            params.PSCupos = document.getElementById("<%=txtCupos.ClientID%>").value;
            params.PSAdiLimite = document.getElementById("<%=txtAdiLimite.ClientID%>").value;
            params.Hr_Ser = document.getElementById("<%=txtHorario.ClientID%>").value;
			params.PSObservacion = document.getElementById("<%=txtObs.ClientID%>").value;
			params.PSEstado = "Activo";
			params = JSON.stringify(params);

			$.ajax({
                type: "POST", url: "ProgServicios.aspx/SetBtnGuardar", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) {
                    $("#divmsj2").html(result.d);
                    alert(result.d);
                    location.reload();
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
        <p class="cazador2">Programacion - Servicio</p>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

    <form id="form1" runat="server" autocomplete="off">
      <!-- Content Wrapper. Contains page content -->
      <div class="content-wrapper">
         <!-- Content Header (Page header) -->
         <!-- Main content -->
         <section class="content">
			 <div class="row" style="display:block; background-color:#AEB6BF">
				 <div class="column">
					 <div class="col-md-10">
						 <div class="row">
							<a class="btn btn-app" onclick="btnNuevo()" href="javascript:location.reload()">
								<i class="fa fa-file-o"></i> Nuevo
							</a>
							 <div class="btn btn-app" style="background-color:#D4E6F1" onclick="btnGuardar()">
								 <i class="fa fa-save"></i> Guardar
							 </div>
							<a class="btn btn-app btn-danger bg-danger" style="background-color:#E6B0AA">
								<i class="fa fa-times-circle"></i> Anular
							</a>
                             <asp:LinkButton ID="btnListar" class="btn btn-app" runat="server" OnClick="btnListar_Click">
                                 <i class="fa fa-list-ol"></i> Listar
                             </asp:LinkButton>
							 <div class="btn btn-app hide" style="background-color:#D5F5E3" onclick="btnbuscar0()">
								 <i class="fa fa-search"></i> Buscar
							 </div>
							<div class="btn" id="divFDesde" style="visibility:hidden">
								<asp:TextBox ID="txtFDesde" runat="server" class='form-control' type="date"></asp:TextBox>
								<code style="color:black; background-color:transparent">Desde</code>
							</div>
							<div class="btn" id="divFHasta" style="visibility:hidden" >
								<asp:TextBox ID="txtFHasta" runat="server" class='form-control' type="date"></asp:TextBox>
								<code style="color:black; background-color:transparent">Hasta</code>
							</div>
							<div class="btn" id="divBtnBuscarNI" style="visibility:hidden" >
							 	<div class="btn btn-info" id="btnBuscarNIngreso" onclick="buscarNIng();" data-toggle="modal" data-target="#modalShowNIng">
									<i class="fa fa-search"></i>
								</div>
							</div>
							<div class="btn" id="divChkVerTodo" style="visibility:hidden" >
                                <input id="chkVerTodo" type="checkbox" title="Ver Todo" />
							</div>

						 </div>
					 </div>
					 <div class="col-md-10">

					 </div>
				 </div>
			 </div>

            <div class="row" style="display:block; background-color:#4dd2ff">
				<p></p>
                <div class="column">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                           <asp:DropDownList ID="ddlServicio" runat="server" class='form-control ' > <%--select2--%>
                            <asp:ListItem Value="04">BAJA VISION</asp:ListItem>
                            <asp:ListItem Value="20">CATARATA</asp:ListItem>
                            <asp:ListItem Value="21">CORNEA</asp:ListItem>
                            <asp:ListItem Value="75">EKG + CMEC</asp:ListItem>
                            <asp:ListItem Value="39">EMERGENCIA</asp:ListItem>
                            <asp:ListItem Value="72">ENFERMERIA</asp:ListItem>
                            <asp:ListItem Value="23">GLAUCOMA</asp:ListItem>
                            <asp:ListItem Value="99">LASER GLAUCOMA</asp:ListItem>
                            <asp:ListItem Value="24">NEURO</asp:ListItem>
                            <asp:ListItem Value="25">OCULO PLASTICA</asp:ListItem>
                            <asp:ListItem Value="26">OFT. PED. Y ESTRABISMO</asp:ListItem>
                            <asp:ListItem Value="71">OFTALMOLOGIA GENERAL</asp:ListItem>
                            <asp:ListItem Value="03">PROGRAMACION CATARATA</asp:ListItem>
                            <asp:ListItem Value="69">PROGRAMACION PEDIATRIA</asp:ListItem>
                            <asp:ListItem Value="37">PROGRAMACION RETINA</asp:ListItem>
                            <asp:ListItem Value="46">REFRACCION</asp:ListItem>
                            <asp:ListItem Value="32">REFRACCION CATARATA</asp:ListItem>
                            <asp:ListItem Value="31">REFRACCION PEDIATRICA</asp:ListItem>
                            <asp:ListItem Value="28">RETINA</asp:ListItem>
                            <asp:ListItem Value="06">SERVICIO SOCIAL</asp:ListItem>
                            <asp:ListItem Value="5S">SST</asp:ListItem>
                            <asp:ListItem Value="81">TELESALUD</asp:ListItem>
                            <asp:ListItem Value="29">UVEITIS</asp:ListItem>
                           </asp:DropDownList>
                            <code style="font-size:large; background-color:transparent; ">Servicio</code>
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                           <asp:DropDownList ID="ddlPiso" runat="server" class='form-control' >
                              <asp:ListItem Value="1">1</asp:ListItem>
                              <asp:ListItem Value="2">2</asp:ListItem>
                              <asp:ListItem Value="3">3</asp:ListItem>
                              <asp:ListItem Value="4">4</asp:ListItem>
                              <asp:ListItem Value="5">5</asp:ListItem>
                           </asp:DropDownList>
                            <code style="font-size:large; background-color:transparent; ">Piso</code>
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                           <asp:DropDownList ID="ddlTurno" runat="server" class='form-control' >
                              <asp:ListItem Value="M">M</asp:ListItem>
                              <asp:ListItem Value="T">T</asp:ListItem>
                           </asp:DropDownList>
                            <code style="font-size:large; background-color:transparent; ">Turno</code>
                        </div>
                     </div>
                  </div>
               </div>
				
			</div>
        
             <div class="row" style="display:block; background-color:#4dd2ff">
                <div class="column">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:TextBox ID="txtCupos" runat="server" class='form-control'></asp:TextBox>
                            <code style="font-size:large; background-color:transparent; ">Cantidad de Cupos</code>
                        </div>
                     </div>
                  </div>
               </div>

                <div class="column">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:TextBox ID="txtAdiLimite" runat="server" class='form-control'></asp:TextBox>
                            <code style="font-size:large; background-color:transparent; ">Limite de Adicional</code>
                        </div>
                     </div>
                  </div>
               </div>

             </div>
             
             <div class="row" style="display:block; background-color:#4dd2ff">
                <div class="column">
                <div class="col-md-6" style="text-align: center;padding:  0px 30px 0px 30px;">
                    <div class="box-body">
                    <div class="input-group margin">
                        <asp:TextBox ID="txtHorario" runat="server" class='form-control'></asp:TextBox>
                        <asp:HiddenField ID="txtPersonalId" runat="server" />
			            <span class="input-group-btn">
                            <div class="btn btn-info btn-flat" id="btnSelHorario" onclick="fSelHorario();" data-toggle="modal" data-target="#mBHorario">...</div>
			            </span>
                    </div>
                    <code style="font-size:large; background-color:transparent; ">Horario [07:20-07:40-08:00]</code>
                    </div>
                </div>
                </div>                
             </div>


             <div class="row" style="display:block; background-color:#4dd2ff">
                <div class="column">
                  <div class="col-md-6" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:TextBox ID="txtObs" runat="server" class='form-control'></asp:TextBox>
                            <code style="font-size:large; background-color:transparent; ">Observacion</code>
                        </div>
                     </div>
                  </div>
               </div>
             </div>


            <!--SEGUNDA FILA-->             
			 <div class="row" style="display:block;">
				<div class="column">
		
				</div>
				 <br />
			</div>

			<!-- CUARTA FILA -->
            <div class="row">
               <div class="col-md-12">
                  <div class="box" >
                     <div class="box-body table-responsive no-padding" id="habfiltro">
                        <asp:GridView ID="GVtable" runat="server" class="table table-condensed table-bordered"></asp:GridView>
                        <asp:Literal ID="LitTABL1" runat="server"></asp:Literal>
                     </div>
                  </div>
               </div>
            </div>			


<script>

	var inputText = document.getElementById("txtbuscaprod");
	inputText.addEventListener("keyup", function(event) {
      if (event.keyCode === 13) {
		  event.preventDefault();
		  document.getElementById("btnBuscarProd").click();
      }
	});
    


</script>

            <!-- TERCERA FILA -->
            

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
		  <div id="divmsj1"></div><div id="divmsj2"></div><div id="divmsj3"></div><div id="divmsj4"></div>
		  <div id="divmsj5"></div><div id="divmsj6"></div><div id="divmsj7"></div><div id="divmsj8"></div>
         <!-- FIN MAIN CONTENT -->
         <!-- FIN MAIN CONTENT -->
         <!-- FIN MAIN CONTENT -->
         <!-- FIN MAIN CONTENT -->
        
         <!-- modal-sm | small || modal-lg | largo || modal-xl | extra largo -->
        <div class="modal modal-info fade" id="modal-info">
          <div class="modal-dialog modal-sm">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Info Modal</h4>
              </div>
              <div class="modal-body">
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

        <div class="modal modal-warning fade" id="mBHorario">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Crea Horario</h4>
              </div>
              <div class="modal-body">
                  <div id="divShowHorario"></div>
<script>
	function ActivAgregar() {
		var TxtHora = document.getElementById("txtActHora").value;
		var TxtHorario = document.getElementById("txtHorarioTmp").value;
		var txtCantCupos = document.getElementById("dCupostmp").innerHTML;
		var txtContCupos = document.getElementById("dCuposConttmp").innerHTML;

        if (txtCantCupos==txtContCupos) {
            alert("Cantidad de Cupos Completo");
            return false;
        }

        if (TxtHorario=="") {
            document.getElementById("txtHorarioTmp").value = TxtHora;
        }
        else {
            document.getElementById("txtHorarioTmp").value = TxtHorario + "-" + TxtHora;
        }

        const vHorariotmp = document.getElementById("txtHorarioTmp").value.split("-");
        document.getElementById("dCuposConttmp").innerHTML = vHorariotmp.length;
    }

    function fSelHorario() {
        if (document.getElementById("<%=txtCupos.ClientID%>").value=="" || document.getElementById("<%=txtCupos.ClientID%>").value == "0") {
            alert("Ingrese cupos");
            return false;
        }
        document.getElementById("dCupostmp").innerHTML = document.getElementById("<%=txtCupos.ClientID%>").value;
        document.getElementById("txtHorarioTmp").value = "07:20-07:40-08:00-08:20-08:40-09:00-09:20-09:40-10:00-10:20-10:40-11:00-11:20-11:40-12:00-12:20";
    }

    function fHorariotmp() {
        document.getElementById("<%=txtHorario.ClientID%>").value = document.getElementById("txtHorarioTmp").value;
    }
</script>
			<div class="row" >

				<div class="column">
				  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
					 <div class="box-body">
						<div class="input-group margin">
							<input id="txtActHora" class="form-control" type="time" min="07:00" max="18:00" value="07:20" step="300" />
							<span class="input-group-btn">
								<div class="btn btn-primary btn-flat" onclick="ActivAgregar();">Agregar</div>
							</span>
						</div>
						 <code style="font-size:large; background-color:transparent; ">Hora</code>
					 </div>
				  </div>
			   </div>

				<div class="column">
				  <div class="col-md-3" style="text-align: left;padding:  0px 30px 0px 30px;">
					 <div class="box-body">
						<div class="input-group margin">
                            <div class="form-control" id="dCupostmp">0</div>
						</div>
                         <code style="font-size:large; background-color:transparent; ">Cupos</code>
					 </div>
				  </div>
				</div>
				
				<div class="column">
				  <div class="col-md-3" style="text-align: left;padding:  0px 30px 0px 30px;">
					 <div class="box-body">
						<div class="input-group margin">
                            <div class="form-control" id="dCuposConttmp">0</div>
						</div>
                         <code style="font-size:large; background-color:transparent; ">Conteo</code>
					 </div>
				  </div>
				</div>

			</div>

                  <p>

	<div class="row" style="display:block;">
        <div class="column">
            <div class="col-md-10" >
                <div class="box-body">
                    <input type="text" id="txtHorarioTmp" class="form-control" />
                </div>
            </div>
        </div>
    </div>

                  </p>
<p align="left">
    <button type="button" class="btn btn-primary" data-dismiss="modal" onclick="fHorariotmp()">Seleccionar Horario</button>
</p>
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
        document.getElementById("LiPROGRAMACION").classList.add('menu-open');
        document.getElementById('UlProgramacion').style.display = 'block';
        document.getElementById("liPServicio").classList.add('active');
        var f = new Date();

    </script>
</asp:Content>
