<%@ Page Language="C#" MasterPageFile="../../MasterPage.master" AutoEventWireup="true" CodeFile="ProgPersonalM.aspx.cs" Inherits="ASPX_IROJVAR_ProgCupos_ProgPersonalM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>Optica</title>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/json2.js")%>"></script>
    <%--<link rel="stylesheet" media="screen" href="style1.css?id=1">--%>

    <script language="javascript" type="text/javascript">

        function btnGuardar() {
            var params = new Object();
            params.idProgServ = document.getElementById("<%=txtidProgServ.ClientID%>").value;
            params.COD_SER = document.getElementById("<%=txtCodServicio.ClientID%>").value;
            params.Cod_Per = document.getElementById("<%=txtPersonalId.ClientID%>").value;
            params.PIS_SER = document.getElementById("<%=txtPiso.ClientID%>").value;
            params.Tur_Ser = document.getElementById("<%=txtTurno.ClientID%>").value;
            params.PPAnio = "0";
            params.PPMes = "0";
            params.PPFechaCupos = document.getElementById("<%=txtFecha.ClientID%>").value;
            params.PPCupos = document.getElementById("<%=txtCupos.ClientID%>").value;
            params.PPAdiLimite = document.getElementById("<%=txtAdiLimite.ClientID%>").value;
            params.Hr_Ser = document.getElementById("<%=txtHorario.ClientID%>").value;
            params.PPObservacion = document.getElementById("<%=txtObs.ClientID%>").value;
			params.PPEstado = "Activo";
            params.PPTipTrabajo = document.getElementById("<%=ddlTipTrab.ClientID%>").value;
			params = JSON.stringify(params);

			$.ajax({
                type: "POST", url: "ProgPersonalM.aspx/SetBtnGuardar", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) {
                    $("#divmsj2").html(result.d);
                    //alert(result.d);
                    //location.reload();
                }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divmsj3").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});
        }
        
        function btnBuscarServicio() {
            var params = new Object();
            params.PSEstado = "Activo";
			params = JSON.stringify(params);

			$.ajax({
                type: "POST", url: "ProgPersonalM.aspx/GetBtnBuscarServ", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divShowServicio").html(result.d); }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divShowServicio").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});
        }

        function fSelServicio(idProgServ, COD_SER, Servicio, PIS_SER, Tur_Ser, PPCupos, PPAdiLimite, Hr_Ser) {
            document.getElementById("<%=txtidProgServ.ClientID%>").value = idProgServ;
            document.getElementById("<%=txtCodServicio.ClientID%>").value = COD_SER;
            document.getElementById("<%=txtServicio.ClientID%>").value = Servicio;
            document.getElementById("<%=txtPiso.ClientID%>").value = PIS_SER;
            document.getElementById("<%=txtTurno.ClientID%>").value = Tur_Ser;
            document.getElementById("<%=txtCupos.ClientID%>").value = PPCupos;
            document.getElementById("<%=txtAdiLimite.ClientID%>").value = PPAdiLimite;
            document.getElementById("<%=txtHorario.ClientID%>").value = Hr_Ser;
        }

        function btnBuscarPersonal() {
            var params = new Object();
            params.PSEstado = "Activo";
			params = JSON.stringify(params);

			$.ajax({
                type: "POST", url: "ProgPersonalM.aspx/GetBtnBuscarPer", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divShowPersonal").html(result.d); }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divShowPersonal").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});
        }

        function fSelPersonal(COD_PER, APE_PER) {
            document.getElementById("<%=txtPersonal.ClientID%>").value = APE_PER;
            document.getElementById("<%=txtPersonalId.ClientID%>").value = COD_PER;
        }

	    function fGuardarPrev() {
            if (document.getElementById("<%=txtFecha.ClientID%>").value == document.getElementById("<%=txtFechaHasta.ClientID%>").value) {
                btnGuardar();
            }
            if (document.getElementById("<%=txtFecha.ClientID%>").value > document.getElementById("<%=txtFechaHasta.ClientID%>").value) {
                alert("Fecha Inicial no Puede ser Mayor a la Fecha Final");
            }
            if (document.getElementById("<%=txtFecha.ClientID%>").value < document.getElementById("<%=txtFechaHasta.ClientID%>").value) {
                btnGuardarVarios();
            }
        }

        function btnGuardarVarios() {
            var cantMiliSeg = new Date(document.getElementById("<%=txtFechaHasta.ClientID%>").value) - new Date(document.getElementById("<%=txtFecha.ClientID%>").value);
            var cantDias = (cantMiliSeg / 24 / 60 / 60 / 1000) + 1;
            var fLun = ""; var fMar = ""; var fMie = ""; var fJue = ""; var fVie = ""; var fSab = "";
            if (document.getElementById("chkILun").checked) { fLun = "Lun"; }
            if (document.getElementById("chkIMar").checked) { fMar = "Mar"; }
            if (document.getElementById("chkIMie").checked) { fMie = "Mie"; }
            if (document.getElementById("chkIJue").checked) { fJue = "Jue"; }
            if (document.getElementById("chkIVie").checked) { fVie = "Vie"; }
            if (document.getElementById("chkISab").checked) { fSab = "Sab"; }

            var params = new Object();
            params.idProgServ = document.getElementById("<%=txtidProgServ.ClientID%>").value;
            params.COD_SER = document.getElementById("<%=txtCodServicio.ClientID%>").value;
            params.Cod_Per = document.getElementById("<%=txtPersonalId.ClientID%>").value;
            params.PIS_SER = document.getElementById("<%=txtPiso.ClientID%>").value;
            params.Tur_Ser = document.getElementById("<%=txtTurno.ClientID%>").value;
            params.PPAnio = "0";
            params.PPMes = "0";
            params.PPFechaCupos = document.getElementById("<%=txtFecha.ClientID%>").value;
            params.PPCupos = document.getElementById("<%=txtCupos.ClientID%>").value;
            params.PPAdiLimite = document.getElementById("<%=txtAdiLimite.ClientID%>").value;
            params.Hr_Ser = document.getElementById("<%=txtHorario.ClientID%>").value;
            params.PPObservacion = document.getElementById("<%=txtObs.ClientID%>").value;
            params.PPEstado = "Activo";
            params.PPTipTrabajo = document.getElementById("<%=ddlTipTrab.ClientID%>").value;
            params.vCantDias = cantDias;
            params.vFechaHasta = document.getElementById("<%=txtFechaHasta.ClientID%>").value;
            params.vLun = fLun; params.vMar = fMar;
            params.vMie = fMie; params.vJue = fJue;
            params.vVie = fVie; params.vSab = fSab;
			params = JSON.stringify(params);

			$.ajax({
                type: "POST", url: "ProgPersonalM.aspx/SetBtnGuardarVarios", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) {
                    $("#divmsj2").html(result.d);
                }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divmsj3").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});
        }

    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div class="col-xs-8 col-xs-offset-2">
        <p class="cazador2">Programacion - Personal</p>
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
							 <div class="btn btn-app" style="background-color:#D4E6F1" onclick="fGuardarPrev()">
								 <i class="fa fa-save"></i> Guardar
							 </div>
							<a class="btn btn-app btn-danger bg-danger" style="background-color:#E6B0AA">
								<i class="fa fa-times-circle"></i> Anular
							</a>
                             <asp:LinkButton ID="btnListar" class="btn btn-app" runat="server" OnClick="btnListar_Click">
                                 <i class="fa fa-list-ol"></i> Listar
                             </asp:LinkButton>
<script>
    function fVerCalendarioPer() {
        var vcodPer = document.getElementById("<%=txtPersonalId.ClientID%>").value; 
        if (vcodPer == "") {
            alert("Falta Codigo de Personal");
            return false;
        }
        //location = "ProgPerCalendar.aspx?codPer=" + vcodPer; 
        window.open("ProgPerCalendar.aspx?codPer=" + vcodPer, "_blank");
    }
</script>
					<div class="btn btn-app" style="background-color:#D4E6F1" onclick="fVerCalendarioPer()">
						<i class="fa fa-calendar"></i> Calendar
					</div>
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

                <div class="column">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="input-group margin">
                            <asp:TextBox ID="txtPersonal" runat="server" class='form-control'></asp:TextBox>
                            <asp:HiddenField ID="txtPersonalId" runat="server" />
			                <span class="input-group-btn">
                                <div class="btn btn-info btn-flat" id="btnBuscarProd" onclick="btnBuscarPersonal();" data-toggle="modal" data-target="#mBPersonal">...</div>
			                </span>                            
                        </div>
                         <code style="font-size:large; background-color:transparent; ">Personal Asistencial</code>
                     </div>
                  </div>
                </div>

                <div class="column">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <button type="button" class="btn btn-success" data-toggle="modal" data-target="#mBServicio" onclick="btnBuscarServicio()"><i class="fa fa-fw fa-search"></i> Buscar Servicio y Turno</button>
                            <asp:HiddenField ID="txtidProgServ" runat="server" />
                        </div>
                     </div>
                  </div>
               </div>

                <div class="column">
                    <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                        <div class="box-body">
                        <div class="form-group">
                            <asp:DropDownList ID="ddlTipTrab" runat="server" class='form-control' >
                                <asp:ListItem Value="TR">TR</asp:ListItem>
                                <asp:ListItem Value="TP">TP</asp:ListItem>
                                <asp:ListItem Value="Vacaciones">Vacaciones</asp:ListItem>
                                <asp:ListItem Value="Permiso">Permiso</asp:ListItem>
                                <asp:ListItem Value="Licencia">Licencia</asp:ListItem>
                            </asp:DropDownList>
                            <code style="font-size:large; background-color:transparent; ">T. Trabajo</code>
                        </div>
                        </div>
                    </div>
                </div>

             </div>



<script>
    function fAnioMes(vfecha) {
        document.getElementById("<%=txtFechaHasta.ClientID%>").value = document.getElementById("<%=txtFecha.ClientID%>").value;

        let fecha = new date(vfecha.value);

        alert(fecha.getMonth() + 1);
        document.getElementById("<%=txtAnio1.ClientID%>").value = vfecha.getFullYear();
        document.getElementById("<%=txtMes1.ClientID%>").value = vfecha.getMonth() + 1;
    }
</script>

            <div class="row" style="display:block; background-color:#4dd2ff">
                <div class="column">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:TextBox ID="txtFecha" runat="server" class='form-control' type="date" onchange="fAnioMes(this)"></asp:TextBox>
                            <asp:HiddenField ID="txtAnio1" runat="server" />
                            <asp:HiddenField ID="txtMes1" runat="server" />
                            <code style="font-size:large; background-color:transparent; ">Fecha Desde</code>
                        </div>
                     </div>
                  </div>
               </div>

                <div class="column">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:TextBox ID="txtFechaHasta" runat="server" class='form-control' type="date" ></asp:TextBox>
                            <code style="font-size:large; background-color:transparent; ">Fecha Hasta</code>
                        </div>
                     </div>
                  </div>
                </div>

               <div class="column hide">
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

            <div class="row" style="display:block; background-color:#4dd2ff">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <code>Inc. Lun <input type="checkbox" id="chkILun" value="Lun" checked> </code>
                        </div>
                     </div>
                  </div>
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <code>Inc. Mar <input type="checkbox" id="chkIMar" value="Mar" checked> </code>
                        </div>
                     </div>
                  </div>
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <code>Inc. Mie <input type="checkbox" id="chkIMie" value="Mie" checked> </code>
                        </div>
                     </div>
                  </div>
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <code>Inc. Jue <input type="checkbox" id="chkIJue" value="Jue" checked> </code>
                        </div>
                     </div>
                  </div>
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <code>Inc. Vie <input type="checkbox" id="chkIVie" value="Vie" checked> </code>
                        </div>
                     </div>
                  </div>
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <code>Inc. Sab <input type="checkbox" id="chkISab" value="Sab" checked> </code>
                        </div>
                     </div>
                  </div>

            </div>


            <div class="row" style="display:block; background-color:#4dd2ff">
				<p></p>
                <div class="column">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:TextBox ID="txtServicio" runat="server" class='form-control'></asp:TextBox>
                            <asp:HiddenField ID="txtCodServicio" runat="server" />
                            <code style="font-size:large; background-color:transparent; ">Servicio</code>
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                           <asp:TextBox ID="txtPiso" runat="server" class='form-control'></asp:TextBox>
                            <code style="font-size:large; background-color:transparent; ">Piso</code>
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                           <asp:TextBox ID="txtTurno" runat="server" class='form-control'></asp:TextBox>
                            <code style="font-size:large; background-color:transparent; ">Turno</code>
                        </div>
                     </div>
                  </div>
               </div>
				
                <div class="column">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:TextBox ID="txtCupos" runat="server" class='form-control'></asp:TextBox>
                            <code style="font-size:large; background-color:transparent; ">Cantidad de Cupos</code>
                        </div>
                     </div>
                  </div>
               </div>

                <div class="column">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
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
                        <div class="form-group">
                            <asp:TextBox ID="txtHorario" runat="server" class='form-control'></asp:TextBox>
                            <code style="font-size:large; background-color:transparent; ">Horario [07:20-07:40-08:00]</code>
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


        <div class="modal modal-warning fade" id="mBServicio">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Buscar Servicio y Turno</h4>
              </div>
              <div class="modal-body">
                  <div id="divShowServicio"></div>
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

        <div class="modal modal-warning fade" id="mBPersonal">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Buscar Personal Asistencial</h4>
              </div>
              <div class="modal-body">
                  <div id="divShowPersonal"></div>
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
        document.getElementById("liPPersonalM").classList.add('active');
        var f = new Date();

    </script>
</asp:Content>

