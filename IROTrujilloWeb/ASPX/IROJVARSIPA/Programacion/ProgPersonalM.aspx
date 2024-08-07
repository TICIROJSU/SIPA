<%@ Page Language="C#" MasterPageFile="../../MasterPageSIPA.master" AutoEventWireup="true" CodeFile="ProgPersonalM.aspx.cs" Inherits="ASPX_IROJVARSIPA_Programacion_ProgPersonalM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>Optica</title>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/json2.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/funciones.js?vfd=1")%>"></script>
    <%--<link rel="stylesheet" media="screen" href="style1.css?id=1">--%>

    <script language="javascript" type="text/javascript">

        function btnGuardar(txtFecCalendar) {
            var params = new Object();
            params.idProgServ = document.getElementById("<%=txtidProgServ.ClientID%>").value;
            params.COD_SER = document.getElementById("<%=txtCodServicio.ClientID%>").value;
            params.Cod_Per = document.getElementById("<%=txtPersonalId.ClientID%>").value;
            params.PIS_SER = document.getElementById("<%=txtPiso.ClientID%>").value;
            params.Tur_Ser = document.getElementById("<%=txtTurno.ClientID%>").value;
            params.PPAnio = "0";
            params.PPMes = "0";
            params.PPFechaCupos = txtFecCalendar;
            params.PPCupos = document.getElementById("<%=txtCupos.ClientID%>").value;
            params.PPAdiLimite = document.getElementById("<%=txtAdiLimite.ClientID%>").value;
            params.Hr_Ser = document.getElementById("<%=txtHorario.ClientID%>").value;
            params.PPObservacion = "";
			params.PPEstado = "Activo";
            params.PPTipTrabajo = document.getElementById("<%=ddlTipTrab.ClientID%>").value;
            params.PPCantTurno = document.getElementById("<%=txtCantTurnos.ClientID%>").value;
			params = JSON.stringify(params);

			$.ajax({
                type: "POST", url: "ProgPersonalM.aspx/SetBtnGuardar", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) {
                    $("#divmsj2").append(result.d);
                    //$("#divmsj2").html(result.d);
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

        function fSelServicio(idProgServ, COD_SER, Servicio, PIS_SER, Tur_Ser, PPCupos, PPAdiLimite, Hr_Ser, PSCantTurno) {
            document.getElementById("<%=txtidProgServ.ClientID%>").value = idProgServ;
            document.getElementById("<%=txtCodServicio.ClientID%>").value = COD_SER;
            document.getElementById("<%=txtServicio.ClientID%>").value = Servicio;
            document.getElementById("<%=txtPiso.ClientID%>").value = PIS_SER;
            document.getElementById("<%=txtTurno.ClientID%>").value = Tur_Ser;
            document.getElementById("<%=txtCupos.ClientID%>").value = PPCupos;
            document.getElementById("<%=txtAdiLimite.ClientID%>").value = PPAdiLimite;
            document.getElementById("<%=txtHorario.ClientID%>").value = Hr_Ser;
            document.getElementById("<%=txtCantTurnos.ClientID%>").value = PSCantTurno;
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

            document.getElementById("bscprod2").value = "";
            document.getElementById("bscprod2").select();

            const myTimeout = setTimeout(ffocusbsc, 500);
            
        }

        function ffocusbsc() {
            document.getElementById("bscprod2").focus();
        }

        function fSelPersonal(COD_PER, APE_PER) {
            document.getElementById("<%=txtPersonal.ClientID%>").value = APE_PER;
            document.getElementById("<%=txtPersonalId.ClientID%>").value = COD_PER;
        }

        function fGuardarPrev() {
            vCantDiasMes = document.getElementById("txtCantDiasMes").value;
            $("#divmsj2").html("");

            for (var i = 1; i <= vCantDiasMes; i++) {
                if (document.getElementById("chk" + i).checked) {
                    btnGuardar(document.getElementById("txtFecCalendar" + i).value);
                }
            }

            //fLimpiarChk();
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
        //window.open("ProgPerCalendar.aspx?codPer=" + vcodPer, "_blank");
        window.open("../../IROJVAR/ProgCupos/ProgPerCalendar.aspx?codPer=" + vcodPer, "_blank");
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
                  <div class="col-md-4" style="text-align: center;padding:  0px 30px 0px 30px;">
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
                  <div class="col-md-4" style="text-align: center;padding:  0px 30px 0px 30px;">
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
                            <asp:DropDownList ID="ddlTipTrab" runat="server" class='form-control' >
                                <asp:ListItem Value="TR">TR</asp:ListItem>
                                <asp:ListItem Value="TP" selected="selected">TP</asp:ListItem>
                                <asp:ListItem Value="V">Vacaciones</asp:ListItem>
                                <asp:ListItem Value="F">Feriado</asp:ListItem>
                                <asp:ListItem Value="P">Permiso</asp:ListItem>
                                <asp:ListItem Value="C">Capacitacion</asp:ListItem>
                                <asp:ListItem Value="TD">Turno Devolucion</asp:ListItem>
                                <asp:ListItem Value="LSG">Licencia Sin Goce</asp:ListItem>
                                <asp:ListItem Value="Ono">Onomastico</asp:ListItem>
                                <asp:ListItem Value="Cam">Campaña</asp:ListItem>
                            </asp:DropDownList>
                            <code style="font-size:large; background-color:transparent; ">T. Trabajo</code>
                        </div>
                        </div>
                    </div>
                </div>

             </div>

            <div class="row hide" style="display:block; background-color:#4dd2ff">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <code>Inc. Lun <input type="checkbox" id="chkILun" value="Lun" onclick="fchkDiaSem('1', 'chkILun')" > </code>
                        </div>
                     </div>
                  </div>
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <code>Inc. Mar <input type="checkbox" id="chkIMar" value="Mar" onclick="fchkDiaSem('2', 'chkIMar')" > </code>
                        </div>
                     </div>
                  </div>
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <code>Inc. Mie <input type="checkbox" id="chkIMie" value="Mie" onclick="fchkDiaSem('3', 'chkIMie')" > </code>
                        </div>
                     </div>
                  </div>
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <code>Inc. Jue <input type="checkbox" id="chkIJue" value="Jue" onclick="fchkDiaSem('4', 'chkIJue')" > </code>
                        </div>
                     </div>
                  </div>
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <code>Inc. Vie <input type="checkbox" id="chkIVie" value="Vie" onclick="fchkDiaSem('5', 'chkIVie')" > </code>
                        </div>
                     </div>
                  </div>
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <code>Inc. Sab <input type="checkbox" id="chkISab" value="Sab" onclick="fchkDiaSem('6', 'chkISab')" > </code>
                        </div>
                     </div>
                  </div>

            </div>

    <script>
        function fchkDiaSem(vDiaSem, vchknom) {
            vchk = document.getElementById(vchknom).checked;
            vCantDiasMes = document.getElementById("txtCantDiasMes").value;

            for (var i = 1; i <= vCantDiasMes; i++) {
                //alert(i);
                if (document.getElementById("txtDiaSem" + vDiaSem + "D" + i)) {
                    vFecDia = document.getElementById("txtDiaSem" + vDiaSem + "D" + i).value;
                    document.getElementById("chk" + vFecDia).checked = vchk;
                }
            }
        }

        function fchkDiaSemCab(vDiaSem, vchknom) {
            document.getElementById(vchknom).checked = !document.getElementById(vchknom).checked;
            fchkDiaSem(vDiaSem, vchknom);
        }

    </script>

    <div class="row" style="display:block; background-color:#4dd2ff">
        <div class="column">
            <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                <div class="box-body">
                <div class="form-group">
                    <asp:DropDownList ID="DDLAnio" runat="server" class='form-control' >
                              <asp:ListItem Value="2021">2021</asp:ListItem>
                              <asp:ListItem Value="2022">2022</asp:ListItem>
                              <asp:ListItem Value="2023">2023</asp:ListItem>
                              <asp:ListItem Value="2024">2024</asp:ListItem>
                              <asp:ListItem Value="2025">2025</asp:ListItem>
                              <asp:ListItem Value="2026">2026</asp:ListItem>
                    </asp:DropDownList>
                </div>
                    <div class="form-group">
                        <a class="btn btn-success" onclick="fLimpiarChk()"><i class="fa fa-search"></i> Limpiar Chk</a>
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
                    <div class="form-group">
                        <asp:LinkButton ID="bntBuscar" runat="server" class="btn btn-success" OnClick="bntBuscar_Click" ><i class="fa fa-search"></i> Consultar</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <div class="column">
            <div class="col-md-8" style="text-align: center;padding:  0px 30px 0px 30px;">
                <div class="box-body">
                    <asp:Literal ID="LitCalendarServer" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="display:block; background-color:#4dd2ff">
        
    </div>
          
    <script>
        function fClickChk(vDia, diaSemNro) {
            if (diaSemNro == 0) {
                document.getElementById("chk" + vDia).checked = false;
                return;
            }
            if (document.getElementById("chk" + vDia).checked) {
                document.getElementById("chk" + vDia).checked = false;
            }
            else {
                document.getElementById("chk" + vDia).checked = true;
            }
        }

        function fLimpiarChk() {
            vCantDiasMes = document.getElementById("txtCantDiasMes").value;

            for (var i = 1; i <= vCantDiasMes; i++) {
                document.getElementById("chk" + i).checked = false;
            }
        }

    </script>

            <div class="row" style="display:block; background-color:#4dd2ff">
				<p></p>


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

                <div class="column">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:TextBox ID="txtCantTurnos" runat="server" class='form-control'></asp:TextBox>
                            <code style="font-size:large; background-color:transparent; ">Cantidad de Turnos</code>
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
                  
                  <div class="box" >
                        <div class="box-header">
                            <h3 class="box-title" style="margin-top: -6px; margin-bottom: -11px; margin-left: -7px;">
					<div class="input-group margin">
						<div class="input-group-btn">
								<button class="btn btn-default" type="button" title="Max.: 1,000 Registros" onclick="exportTableToExcel('tblbscrJS')"><i class="fa fa-download "></i>
								</button>
						</div>
						<div>
							<input type="text" class="form-control" id="bscprod2" placeholder="Buscar Nombre del Personal" onkeyup="fBscTblHTML('bscprod2', 'tblbscrJS', 3)" autofocus="autofocus">
						</div>
					</div>
                            </h3>
                            <div class="box-tools"></div>
                        </div>
                     <div class="box-body table-responsive no-padding" >
                        
                         <div id="divShowPersonal"></div>

                     </div>
                  </div>

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
        document.getElementById("liPPerMasiv").classList.add('active');
        var f = new Date();

    </script>
</asp:Content>


