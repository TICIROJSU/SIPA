<%@ Page Language="C#" MasterPageFile="../../MasterPage.master" AutoEventWireup="true" CodeFile="DisponibilidadIRO.aspx.cs" Inherits="ASPX_IROJVAR_SISMED_DisponibilidadIRO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>IRO - HISMINSA</title>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/json2.js")%>"></script>
    <%--<link rel="stylesheet" media="screen" href="style1.css?id=1">--%>

    <script language="javascript" type="text/javascript">

        function fDetRecXXXXXX(vmes, vservicio, vtotal) {
            $.ajax({
                type: "POST", url: "RecaudDia.aspx/GetDetRecaudacion", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divDetRecaudacion").html(result.d) }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divDetRecaudacion").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });
        }

        function fDetDisponibilidad(Indicador) {
            var params = new Object();
            params.vIndicador = Indicador; 
            params.vPeriodo = document.getElementById("<%=txtAnio.ClientID%>").value; 
            params.vEESS = document.getElementById("<%=txtEESS.ClientID%>").value; 
            params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "DisponibilidadIRO.aspx/GetDetDisponibilidad", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function() /*(result)*/ { window.open("../../IROJVAR/Caja/RecauDiaDescarga.aspx", '_blank'); }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divErrores").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });

        }

        
        function fShowEESS() {
            $.ajax({
                type: "POST", url: "DisponibilidadIRO.aspx/GetEESS",  
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divEESS").html(result.d) }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divEESS").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });

        }

    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div class="col-xs-8 col-xs-offset-2">
        <p class="cazador2">Reporte de Disponibilidad</p>
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
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="input-group margin">
                            <asp:TextBox ID="txtEESS" runat="server" Text="05197-IRO" class='form-control' ReadOnly="True" AutoPostBack="True"></asp:TextBox>
                            <span class="input-group-btn">
                                <div class='btn btn-info btn-flat' onclick="fShowEESS()" data-toggle="modal" data-target="#modalEESS">...</div>
                            </span>
                            
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                           <asp:TextBox ID="txtAnio" runat="server" class='form-control' ReadOnly="True"></asp:TextBox>

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

    function txtEESSCarga(dato)
    {
        document.getElementById("<%=txtEESS.ClientID%>").value = dato; 
    }

</script>
<div style="display:block">
<div class="row">
	<div class="col-md-3 col-sm-6 col-xs-12">
	  <div class="info-box" onclick="fDetDisponibilidad('')">
		<span class="info-box-icon bg-aqua"><i class="ion ion-stats-bars"></i></span>
		<div class="info-box-content">
		  <span class="info-box-text">Disponibilidad</span>
		  <span class="info-box-number"><div runat="server" id="divDisp"></div></span>
		</div>
		<!-- /.info-box-content -->
	  </div>
	  <!-- /.info-box -->
	</div>
	<div class="col-lg-3 col-xs-6" >
	  <div class="small-box bg-green">
		<div class="inner"><h3><div runat="server" id="divDispNivel"></div></h3>
		</div>
	  </div>
	</div>
</div>

<div class="row">
	<div class="col-md-3 col-sm-6 col-xs-12">
	  <div class="info-box" onclick="fDetDisponibilidad('NORMO')">
		<span class="info-box-icon bg-green"><i class="ion ion-stats-bars"></i></span>
		<div class="info-box-content">
		  <span class="info-box-text">Normo Stock</span>
		  <span class="info-box-number"><div runat="server" id="divNormo"></div></span>
		</div>
	  </div>
	</div>	

	<div class="col-md-3 col-sm-6 col-xs-12">
	  <div class="info-box" onclick="fDetDisponibilidad('SOB-NoAcep')">
		<span class="info-box-icon bg-yellow"><i class="ion ion-stats-bars"></i></span>
		<div class="info-box-content">
		  <span class="info-box-text">SobreStock</span>
		  <span class="info-box-number"><div runat="server" id="divSobre"></div></span>
		</div>
	  </div>
	</div>	

	<div class="col-md-3 col-sm-6 col-xs-12">
	  <div class="info-box" onclick="fDetDisponibilidad('SUB-STK')">
		<span class="info-box-icon bg-red"><i class="ion ion-stats-bars"></i></span>
		<div class="info-box-content">
		  <span class="info-box-text">SubStock</span>
		  <span class="info-box-number"><div runat="server" id="divSub"></div></span>
		</div>
	  </div>
	</div>	

	<div class="col-md-3 col-sm-6 col-xs-12">
	  <div class="info-box" onclick="fDetDisponibilidad('SIN-ROTACION')">
		<span class="info-box-icon bg-green"><i class="ion ion-stats-bars"></i></span>
		<div class="info-box-content">
		  <span class="info-box-text">Sin Rotacion</span>
		  <span class="info-box-number"><div runat="server" id="divSin"></div></span>
		</div>
	  </div>
	</div>	
</div>
<div class="row">
	<div class="col-md-3 col-sm-6 col-xs-12">
	  <div class="info-box" onclick="fDetDisponibilidad('DESAB')">
		<span class="info-box-icon bg-red"><i class="ion ion-stats-bars"></i></span>
		<div class="info-box-content">
		  <span class="info-box-text">Desabastecido</span>
		  <span class="info-box-number"><div runat="server" id="divDes"></div></span>
		</div>
	  </div>
	</div>	

	<div class="col-md-3 col-sm-6 col-xs-12">
	  <div class="info-box" onclick="fDetDisponibilidad('No-Consid')">
		<span class="info-box-icon bg-purple"><i class="ion ion-stats-bars"></i></span>
		<div class="info-box-content">
		  <span class="info-box-text">No Considerados</span>
		  <span class="info-box-number"><div runat="server" id="divNo"></div></span>
		</div>
	  </div>
	</div>	

	<div class="col-md-3 col-sm-6 col-xs-12">
	  <div class="info-box" onclick="fDetDisponibilidad('Covid')">
		<span class="info-box-icon bg-light-blue"><i class="ion ion-stats-bars"></i></span>
		<div class="info-box-content">
		  <span class="info-box-text">Covid</span>
		  <span class="info-box-number"><div runat="server" id="divCovid"></div></span>
		</div>
	  </div>
	</div>	

</div>


</div>

            <!-- TERCERA FILA -->
            <div class="row">
               <div class="col-md-12">
                  <div class="box" >
                        <div class="box-header">
                            <h3 class="box-title" style="margin-top: -6px; margin-bottom: -11px; margin-left: -7px;">
                            <div class="input-group-btn">
                                    <button class="btn btn-default" runat="server" type="button" title="Max.: 3,000 Registros" onserverclick="ExportarExcel_Click">
                                    <i class="fa fa-download "></i>
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

            <div>
        <div class="modal modal-success fade" id="modalDetRecaudacion">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title"><div runat="server" id="mdrecTitle1">Recaudacion Diaria</div> </h4>
              </div>
              <div class="modal-body">
                <%--<p>One fine body&hellip;</p>--%>
                  <div id="divDetRecaudacion"></div>
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

        <div class="modal modal-success fade" id="modalEESS">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Establecimiento de Salud </h4>
              </div>
              <div class="modal-body">
                <%--<p>One fine body&hellip;</p>--%>
                  <div id="divEESS"></div>
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
        document.getElementById("LiSISMEDI").classList.add('menu-open');
        document.getElementById('UlSISMED').style.display = 'block';
        document.getElementById("ulSISMEDRep").classList.add('active');
        var f = new Date();

    </script>
</asp:Content>

