<%@ Page Language="C#" MasterPageFile="../../MasterPageExtPac.master" AutoEventWireup="true" CodeFile="ProgPerCalendar.aspx.cs" Inherits="ASPX_IROJVAR_ProgCupos_ProgPerCalendar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>IRO</title>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/json2.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/funciones.js?vfd=1")%>"></script>

    <script>

        function DetAtenciones(vanio, vmes, vdia, turno, vplaza, vCodPer, vFechaFor, vCodSer) {
            var params = new Object();
            params.vanio = vanio; // cambiar la descripcion del params y el ddl
            params.vmes = vmes; 
            params.vdia = vdia; 
            params.turno = turno; 
            params.vplaza = vplaza; 
            params.vCodPer = vCodPer; 
            params.vFechaFor = vFechaFor; 
            params.vCodSer = vCodSer; 
            params = JSON.stringify(params);

            $("#divDetAtencion").html("");

            $.ajax({
                type: "POST", url: "ProgPerCalendar.aspx/GetDetAtenciones", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divDetAtencion").html(result.d) }, //success: LoadPrueba01, //Procesar 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divDetAtencion").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });
        }

        function btnAnularProgPer(Cod_Per, PPFechaCupos, Tur_Ser) {
            varc = confirm("Esta seguro que desea Anular esta Programación?");
            if (varc == false) {
                alert("Operacion Cancelada");
                return;
            }
            var params = new Object();
            params.Cod_Per = Cod_Per; 
            params.PPFechaCupos = PPFechaCupos; 
            params.Tur_Ser = Tur_Ser; 
            params = JSON.stringify(params);

            $("#divDetAtencion").html("");

            $.ajax({
                type: "POST", url: "ProgPerCalendar.aspx/SetAnulaRegProg", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divDetAtencion").html(result.d) }, //success: LoadPrueba01, //Procesar 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divDetAtencion").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });

            alert("Operacion Exitosa");
        }


    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div class="col-xs-8 col-xs-offset-2">
        <p class="cazador2">Consulta de Citas Medicas - Pacientes</p>
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
                     </div>
                  </div>
                </div>
                <div class="column">
                    <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                        <div class="box-body">
                        <div class="form-group">
                           <asp:DropDownList ID="DDLMes" runat="server" class='form-control'>
                              <asp:ListItem Value="1">Enero</asp:ListItem>
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
                
                <div class="column">
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

<script>
	var inputText = document.getElementById("");
	inputText.addEventListener("keyup", function(event) {
      if (event.keyCode === 13) {
		  event.preventDefault();
		  <%--document.getElementById("<%=bntBuscar.ClientID%>").click();--%>
      }
	});
</script>



        <div class="col-md-9">
          <div class="box box-primary">
            <div class="box-body no-padding fc ">
				<div class="fc-toolbar fc-header-toolbar">
					<div class="fc-left hide">
						<div class="fc-button-group">
							<div class="btn btn-danger fc-corner-right" onclick="GPresencial(' ');" >Eliminar</div>
						</div>
					</div>
					<div class="fc-right hide">
						<div class="fc-button-group">
							<div class="btn btn-success fc-corner-left" onclick="G-Presencial('PRESENCIAL');" >Presencial</div>
							<div class="btn btn-warning fc-corner-right" onclick="G-Presencial('REMOTO');" >Remoto</div>
						</div>
					</div>
					<div class="fc-center">
						<h2><asp:Literal ID="LitPeriodo" runat="server"></asp:Literal></h2>
					</div>
					<div class="fc-clear"></div>
			    </div>
			    <asp:Literal ID="LitCalendar" runat="server"></asp:Literal>
			</div>
		  </div>
		</div>


        <div class="col-md-9">
          <div class="box box-primary">
            <div class="box-body no-padding fc ">
				<div class="fc-toolbar fc-header-toolbar">
					<div class="fc-center">
						<h2><asp:Literal ID="LitPeriodoT" runat="server"></asp:Literal></h2>
					</div>
			    </div>
			    <asp:Literal ID="LitCalendarT" runat="server"></asp:Literal>
			</div>
		  </div>
		</div>


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
                            <div><input type="text" class="form-control" id="bscprod2" placeholder="Buscar" onkeyup="fBscTblHTML('bscprod2', '<%=GVtable.ClientID%>', 4)" autofocus="autofocus">
                            </div>
            </div>
                            </h3>
                            
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

        <div class="modal modal-success fade" id="modalDetAtenciones">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Detalle de la Atencion</h4>
              </div>
              <div class="modal-body">
                <%--<p>One fine body&hellip;</p>--%>
                  <div id="divDetAtencion"></div>
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
        var f = new Date();

    </script>
</asp:Content>

