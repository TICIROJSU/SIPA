<%@ Page Language="C#" MasterPageFile="../../MasterPageSIPA.master" AutoEventWireup="true" CodeFile="RepHorasMedico.aspx.cs" Inherits="ASPX_IROJVARSIPA_Reportes_RepHorasMedico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>IRO - HISMINSA</title>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/json2.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/funciones.js?vfd=1")%>"></script>
    <%--<link rel="stylesheet" media="screen" href="style1.css?id=1">--%>

    <script language="javascript" type="text/javascript">

        function fPerDet(vDNI, vIDPER, vPersonal) {
            var params = new Object();
            params.vDNI = vDNI; 
            params.vAnio = document.getElementById("<%=DDLAnio.ClientID%>").value; 
            params.vMes = document.getElementById("<%=DDLMes.ClientID%>").value; 
            params = JSON.stringify(params);

            document.getElementById("dmTrabDTitle").innerHTML = vDNI + " - " + vPersonal; 
            
            $.ajax({
                type: "POST", url: "ListarPersonal.aspx/GetPerDet", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divListaTrab").html(result.d) }, //success: LoadPrueba01, //Procesar 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divListaTrab").html(textStatus + ": " + XMLHttpRequest.responseText); 
                }
            });
            
            document.getElementById("hideIDPER").value = vIDPER; 
            document.getElementById("hideDNI").value = vDNI; 
        }

        function fDeletePerDet(vDNI, vIdTrabajo, vFecha) {
            var params = new Object();
            params.vDNI = vDNI; 
            params.vIdT = vIdTrabajo; 
            params.vFechaT = vFecha; 
            params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "ListarPersonal.aspx/GetDeletePetDet", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divErrores").html(result.d) }, //success: LoadPrueba01, //Procesar 
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
        <p class="cazador2">Lista de Personal</p>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

   
    <form id="form1" runat="server" autocomplete="off">
      <!-- Content Wrapper. Contains page content -->
      <div class="content-wrapper">
         <!-- Content Header (Page header) -->
         <!-- Main content -->
         <section class="content">
        
            <!--SEGUNDA FILA-->             
            <div class="row" style="display:block;">
               <div class="column hidden">
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
                              <asp:ListItem Value="2019">2019</asp:ListItem>
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
                              <asp:ListItem Value="0">Meses</asp:ListItem>
                              <asp:ListItem Value="1">Enero</asp:ListItem>
                              <asp:ListItem Value="2" Selected="true">Febrero</asp:ListItem>
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

               <div class="column">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:TextBox ID="txtUnidad" runat="server" class='form-control' ></asp:TextBox>
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column" style="display:block">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:LinkButton ID="bntBuscar" runat="server" class="btn btn-success" OnClick="bntBuscar_Click" ><i class="fa fa-search"></i> Consultar</asp:LinkButton>
                        </div>
                     </div>
                  </div>
               </div>

               <!-- /.box-header -->
            </div>
            <!--FIN SEGUNDA FILA-->

<script>

</script>

            <!-- TERCERA FILA -->
            <div class="row">
               <div class="col-md-12">
                  <div class="box" >
                        <div class="box-header">
                            <h3 class="box-title" style="margin-top: -6px; margin-bottom: -11px; margin-left: -7px;">
                            <div class="input-group-btn">
<%--                                    <button class="btn btn-default" runat="server" type="button" title="Max.: 3,000 Registros" onserverclick="ExportarExcel_Click">
                                    <i class="fa fa-download "></i>
                                    </button>--%>
                                <%--tbldscrg--%>
								<button class="btn btn-default" type="button" title="Max.: 1,000 Registros" onclick="exportTableToExcel('<%=GVtable.ClientID%>')"><i class="fa fa-download "></i>
								</button>
                            </div>
                            </h3>
                            <div class="box-tools">
                                <asp:Label ID="lblRuta" runat="server" Text="Descarga en: C:\"></asp:Label>
                                <div id="divtxtRuta"></div>
                            </div>
                        </div>
                     <!-- /.box-header -->
                     <div class="box-body table-responsive no-padding" id="habfiltro">
                        <asp:GridView ID="GVtable" runat="server" class="table table-condensed table-bordered"></asp:GridView>
                        <asp:Literal ID="LitTABL1" runat="server"></asp:Literal>
                     </div>
                     <!-- /.box-body -->
                  </div>
                  <!-- /.box -->
               </div>
            </div>

            <!--FIN TERCERA FILA-->
<script>
    function cargaDetAtenciones(vanio, vmes, vdia, vturno, vplaza)
    {
        window.open("./Atencioneset.aspx","ventana1","width=120,height=300,scrollbars=NO")
    }
</script>

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

        <div class="modal modal-info fade" id="modalPerDet">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Descripcion del Trabajo</h4>
				  <div id="dmTrabDTitle"></div>
				  <div id="HastaCant"></div>
				  <input type="hidden" id="inputDiaTrab" />
				  <input type="hidden" id="inputTxtTrabActividad" />
				  <input type="hidden" id="hideIDPER" />
				  <input type="hidden" id="hideDNI" />
              </div>
              <div class="modal-body">
				  <p>
					<div class="input-group input-group">

<script>
    function fDesdeOK() {

        document.getElementById("<%=txtHasta.ClientID%>").value = document.getElementById("<%=txtDesde.ClientID%>").value
    }

    function fHasta() {

    }

    function fHastaCant() {
        var cantMiliSeg = new Date(document.getElementById("<%=txtHasta.ClientID%>").value) - new Date(document.getElementById("<%=txtDesde.ClientID%>").value);
        document.getElementById("HastaCant").innerHTML = (cantMiliSeg / 24 / 60 / 60 / 1000) + 1;
    }

</script>

			<div class="row" style="display:block;">
               <div class="column">
                  <div class="col-md-1" >
                     <div class="box-body">
						 Desde: 
                     </div>
                  </div>
                  <div class="col-md-4" >
                     <div class="box-body">
                         <asp:TextBox ID="txtDesde" runat="server" type="date" onchange="fDesdeOK()" class="form-control" max="2022-12-31"></asp:TextBox>
                         <code>Incluir Sabados <input type="checkbox" id="chkISabado" value="Sab" /> </code>
                     </div>
                  </div>
                  <div class="col-md-1" style="visibility:visible" >
                     <div class="box-body">
						 Hasta: 
                     </div>
                  </div>
                  <div class="col-md-4" style="visibility:visible" >
                     <div class="box-body">
                        <div class="form-group">
                            <asp:TextBox ID="txtHasta" runat="server" type="date" onchange="fHasta()" class="form-control" max="2022-12-31"></asp:TextBox>
                         <code>Incluir Domingos <input type="checkbox" id="chkIDomingo" value="Dom" /> </code>
                        </div>
                     </div>
                  </div>
                  <div class="col-md-2" >
                     <div class="box-body">
                        <div class="form-group">
                            <button type="button" class="btn btn-primary" data-dismiss="modal" onclick="GPresencialInsertValida()" >Guardar Trabajo</button>
                        </div>
                     </div>
                  </div>

               </div>
            </div>

			<div class="row" style="display:block;">
               <div class="column">
                  <div class="col-md-4" >
                     <div class="box-body">
						 Numero de Turnos para Este Día: 
                     </div>
                  </div>
                  <div class="col-md-2" >
                     <div class="box-body">
                         <asp:TextBox ID="txtCantTurnos" runat="server" type="number" class="form-control" >1</asp:TextBox>
                     </div>
                  </div>
               </div>
            </div>


<script language="javascript" type="text/javascript"> 
    
	function GPresencialInsert() {
        var params = new Object();
        params.vIdPer = document.getElementById("hideIDPER").value;
        params.vDNIPer = document.getElementById("hideDNI").value;
        params.vFechaTrab = document.getElementById("<%=txtDesde.ClientID%>").value;
        //params.vTipoTrab = document.getElementById("TipoTrab").checked.value;
        params.vTipoTrab = $('input[name="TipoTrab"]:checked').val();
        params.vCantTurnos = document.getElementById("<%=txtCantTurnos.ClientID%>").value;
        params = JSON.stringify(params);

        $.ajax({
            type: "POST", url: "ListarPersonal.aspx/SetTipoTrabajoInsert", data: params, 
            contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
            success: function (result) { $("#divErrores").html(result.d) }, 
            error: function(XMLHttpRequest, textStatus, errorThrown) { 
                alert(textStatus + ": " + XMLHttpRequest.responseText); 
                $("#divErrores").html(textStatus + ": " + XMLHttpRequest.responseText);
            }
		});
	}

    function GPresencialInsertVDias() {
        var cantMiliSeg = new Date(document.getElementById("<%=txtHasta.ClientID%>").value) - new Date(document.getElementById("<%=txtDesde.ClientID%>").value);
        var cantDias = (cantMiliSeg / 24 / 60 / 60 / 1000) + 1;

        var params = new Object();
        params.vIdPer = document.getElementById("hideIDPER").value;
        params.vDNIPer = document.getElementById("hideDNI").value;
        params.vFechaI = document.getElementById("<%=txtDesde.ClientID%>").value;
        params.vFechaF = document.getElementById("<%=txtHasta.ClientID%>").value;
        params.vTipoTrab = $('input[name="TipoTrab"]:checked').val();
        params.vCantTurnos = document.getElementById("<%=txtCantTurnos.ClientID%>").value;
        params.vCantDias = cantDias;
        var fSab = ""; var fDom = "";
        if (document.getElementById("chkISabado").checked) { fSab = "Sabado"; }
        if (document.getElementById("chkIDomingo").checked) { fDom = "Domingo"; }
        params.vSab = fSab;
        params.vDom = fDom;
        params = JSON.stringify(params);

        $.ajax({
            type: "POST", url: "ListarPersonal.aspx/SetTipoTrabajoInsertVDias", data: params, 
            contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
            success: function (result) { $("#divErrores").html(result.d) }, 
            error: function(XMLHttpRequest, textStatus, errorThrown) { 
                alert(textStatus + ": " + XMLHttpRequest.responseText); 
                $("#divErrores").html(textStatus + ": " + XMLHttpRequest.responseText);
            }
		});
	}

	function GPresencialInsertValida() {
        if (document.getElementById("<%=txtDesde.ClientID%>").value == document.getElementById("<%=txtHasta.ClientID%>").value) {
            GPresencialInsert1Dia();
        }
        if (document.getElementById("<%=txtDesde.ClientID%>").value > document.getElementById("<%=txtHasta.ClientID%>").value) {
            alert("Fecha Inicial no Puede ser Mayor a la Fecha Final");
        }
        if (document.getElementById("<%=txtDesde.ClientID%>").value < document.getElementById("<%=txtHasta.ClientID%>").value) {
            GPresencialInsertVDias();
        }
    }

    function GPresencialInsert1Dia() {
        var params = new Object();
        params.vIdPer = document.getElementById("hideIDPER").value;
        params.vDNIPer = document.getElementById("hideDNI").value;
        params.vFechaTrab = document.getElementById("<%=txtDesde.ClientID%>").value;
        params = JSON.stringify(params);

        $.ajax({
            type: "POST", url: "ListarPersonal.aspx/GetTipoTrabajoInsertValida", data: params, 
            contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
            success: function (result) {
                if (result.d == "Conforme") { GPresencialInsert(); }
                if (result.d == "Discorde") { alert("Ya existe Registro de Esta Fecha."); }
                $("#divErrores").html(result.d);
                //$("#divAtenciones").html(result.d);
            }, 
            error: function(XMLHttpRequest, textStatus, errorThrown) { 
                alert(textStatus + ": " + XMLHttpRequest.responseText); 
                $("#divErrores").html(textStatus + ": " + XMLHttpRequest.responseText);
            }
		});
    }


</script>

			<div class="row" style="display:block;">
               <div class="column">
                  <div class="col-md-3" >
                     <div class="box-body">
						 <input type="radio" id="TipoTrab" name="TipoTrab" value="REMOTO" checked> Trabajo Remoto
                     </div>
                  </div>
                  <div class="col-md-3" >
                     <div class="box-body">
						 <input type="radio" id="TipoTrab" name="TipoTrab" value="PRESENCIAL"> Trabajo Presencial
                     </div>
                  </div>
                  <div class="col-md-3" >
                     <div class="box-body">
						 <input type="radio" id="TipoTrab" name="TipoTrab" value="LICENCIA"> Licencia
                     </div>
                  </div>
                  <div class="col-md-3" >
                     <div class="box-body">
						 <input type="radio" id="TipoTrab" name="TipoTrab" value="VACACIONES"> Vacaciones
                     </div>
                  </div>

               </div>
            </div>

					</div>
				  </p>

                  <p>
                      <div id="divListaTrab"></div>

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
          

      </div>
      <!-- /.content-wrapper -->
   </form>
    <script>
        //document.getElementById('LiCIEIm').className = "treeview menu-open";
        //$('#LiCIEIm').addClass('menu-open');
        document.getElementById("LiReportes").classList.add('menu-open');
        document.getElementById('UlReportes').style.display = 'block';
        document.getElementById("liRepHorasMedico").classList.add('active');
        var f = new Date();

    </script>
</asp:Content>
