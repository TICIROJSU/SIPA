<%@ Page Language="C#" MasterPageFile="../../MasterPage.master" AutoEventWireup="true" CodeFile="RecaudacionFar.aspx.cs" Inherits="ASPX_IROJVAR_SISMED_RecaudacionFar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>IRO - HISMINSA</title>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/json2.js")%>"></script>
    <%--<link rel="stylesheet" media="screen" href="style1.css?id=1">--%>

    <script language="javascript" type="text/javascript">

        function fDetRecaudacion(vmes, vanio, vtotal, vservicio) {
            var params = new Object();
            params.vAnio = document.getElementById("<%=DDLAnio.ClientID%>").value; 
            params.vMes = vmes; 
            params.vServicio = vservicio; 
            params = JSON.stringify(params);
            var vTitle = "";

            $.ajax({
                type: "POST", url: "RecaudacionFar.aspx/GetDetRecaudacion", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) {
                    var aResult = result.d.split("||sep||");
                    $("#divDetRecaudacion").html(aResult[0]);
                    vTitle = aResult[1]; // No funciona fuera del ajax
                    document.getElementById("<%=mdrecTitle1.ClientID%>").innerText = "Mes: " + vmes + aResult[1];
                }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divDetRecaudacion").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });

            <%--document.getElementById("<%=mdrecTitle1.ClientID%>").innerText = "Mes: " + vmes + " | " + vservicio + " | Total: " + vtotal;--%>

        }

        function fDetRecaudacion2(vanio, vmes, vdia, vtotal) {
            var params = new Object();
            params.vAnio = document.getElementById("<%=DDLAnio.ClientID%>").value; 
            params.vMes = vmes; 
            params.vDia = vdia; 
            params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "RecaudacionFar.aspx/GetDetRecaudacion2", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divDetRecaudacion2").html(result.d) }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divDetRecaudacion2").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });

            document.getElementById("<%=mdrecTitle2.ClientID%>").innerText = "Mes: " + vmes + " | " + vdia + " | Total: " + vtotal;
        }

        function fDetRecaudacion3(vanio, vmes, vdia, vcli) {
            var params = new Object();
            params.vAnio = vanio; 
            params.vMes = vmes; 
            params.vDia = vdia; 
            params.vCliente = vcli; 
            params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "RecaudacionFar.aspx/GetDetRecaudacion3", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divDetRecaudacion3").html(result.d) }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divDetRecaudacion3").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });

            document.getElementById("<%=mdrecTitle3.ClientID%>").innerText = "Mes: " + vmes + " | " + vdia + " | Cliente: " + vcli;
        }

    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div class="col-xs-8 col-xs-offset-2">
        <p class="cazador2">Reporte de Recaudacion Diaria - Farmacia</p>
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
                              <asp:ListItem Value="2017">2017</asp:ListItem>
                              <asp:ListItem Value="2018">2018</asp:ListItem>
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

               <div class="column" style="display:block">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:LinkButton ID="bntBuscar" runat="server" class="btn btn-success" OnClick="bntBuscar_Click" ><i class="fa fa-search"></i> Consultar</asp:LinkButton>
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group" data-toggle="modal" data-target="#modalShowProf" onclick="fShowProf()">
                            <asp:TextBox ID="txtProfesional" runat="server" Text="" class='form-control' ReadOnly="True" placeholder="Profesional"></asp:TextBox>
                            <asp:HiddenField ID="txtIdProf" runat="server" Value="" />
                        </div>
                     </div>
                  </div>
               </div>
               <div class="column">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group has-success" >
                            <asp:TextBox ID="txtTotMonto" runat="server" Text="" class='form-control input-lg' ReadOnly="True" style='text-align:center'></asp:TextBox>
                        </div>
                     </div>
                  </div>
               </div>

               <!-- /.box-header -->
            </div>
            <!--FIN SEGUNDA FILA-->

<script>


</script>
<div style="display:block">
<div class="row">
	<div class="col-lg-3 col-xs-6">
	  <div class="small-box bg-aqua">
		<div class="inner" ><h3 data-toggle='modal' data-target='#modalDetRecaudacion' onclick="fDetRecaudacion('1', '', '', 'Total')">Enero</h3>
            <asp:Literal ID="Lit01" runat="server"></asp:Literal>
		</div>
	  </div>
	</div>
	<!-- ./col -->
	<div class="col-lg-3 col-xs-6">
	  <div class="small-box bg-green">
		<div class="inner"><h3 data-toggle='modal' data-target='#modalDetRecaudacion' onclick="fDetRecaudacion('2', '', '', 'Total')">Febrero</h3>
            <asp:Literal ID="Lit02" runat="server"></asp:Literal>
		</div>
	  </div>
	</div>
	<!-- ./col -->
	<div class="col-lg-3 col-xs-6">
	  <div class="small-box bg-yellow">
		<div class="inner"><h3 data-toggle='modal' data-target='#modalDetRecaudacion' onclick="fDetRecaudacion('3', '', '', 'Total')">Marzo</h3>
            <asp:Literal ID="Lit03" runat="server"></asp:Literal>
		</div>
	  </div>
	</div>
	<!-- ./col -->
	<div class="col-lg-3 col-xs-6" >
	  <div class="small-box bg-navy">
		<div class="inner"><h3 data-toggle='modal' data-target='#modalDetRecaudacion' onclick="fDetRecaudacion('4','', '', 'Total')">Abril</h3>
            <asp:Literal ID="Lit04" runat="server"></asp:Literal>
		</div>
	  </div>
	</div>
	<!-- ./col -->
</div>

<div class="row">
	<div class="col-lg-3 col-xs-6" >
	  <div class="small-box bg-aqua">
		<div class="inner"><h3 data-toggle='modal' data-target='#modalDetRecaudacion' onclick="fDetRecaudacion('5','', '', 'Total')">Mayo</h3>
            <asp:Literal ID="Lit05" runat="server"></asp:Literal>
		</div>
	  </div>
	</div>
	<!-- ./col -->
	<div class="col-lg-3 col-xs-6" >
	  <div class="small-box bg-green">
		<div class="inner"><h3 data-toggle='modal' data-target='#modalDetRecaudacion' onclick="fDetRecaudacion('6','', '', 'Total')">Junio</h3>
            <asp:Literal ID="Lit06" runat="server"></asp:Literal>
		</div>
	  </div>
	</div>
	<!-- ./col -->
	<div class="col-lg-3 col-xs-6" >
	  <div class="small-box bg-yellow">
		<div class="inner"><h3 data-toggle='modal' data-target='#modalDetRecaudacion' onclick="fDetRecaudacion('7', '', '', 'Total')">Julio</h3>
            <asp:Literal ID="Lit07" runat="server"></asp:Literal>
		</div>
	  </div>
	</div>
	<!-- ./col -->
	<div class="col-lg-3 col-xs-6" >
	  <div class="small-box bg-navy">
		<div class="inner"><h3 data-toggle='modal' data-target='#modalDetRecaudacion' onclick="fDetRecaudacion('8', '', '', 'Total')">Agosto</h3>
            <asp:Literal ID="Lit08" runat="server"></asp:Literal>
		</div>
	  </div>
	</div>
	<!-- ./col -->
</div>

<div class="row">
	<div class="col-lg-3 col-xs-6" >
	  <div class="small-box bg-aqua">
		<div class="inner"><h3 data-toggle='modal' data-target='#modalDetRecaudacion' onclick="fDetRecaudacion('9', '', '', 'Total')">Setiembre</h3>
            <asp:Literal ID="Lit09" runat="server"></asp:Literal>
		</div>
	  </div>
	</div>
	<!-- ./col -->
	<div class="col-lg-3 col-xs-6" >
	  <div class="small-box bg-green">
		<div class="inner"><h3 data-toggle='modal' data-target='#modalDetRecaudacion' onclick="fDetRecaudacion('10','', '', 'Total')">Octubre</h3>
            <asp:Literal ID="Lit10" runat="server"></asp:Literal>
		</div>
	  </div>
	</div>
	<!-- ./col -->
	<div class="col-lg-3 col-xs-6" >
	  <div class="small-box bg-yellow">
		<div class="inner"><h3 data-toggle='modal' data-target='#modalDetRecaudacion' onclick="fDetRecaudacion('11', '', '', 'Total')">Noviembre</h3>
            <asp:Literal ID="Lit11" runat="server"></asp:Literal>
		</div>
	  </div>
	</div>
	<!-- ./col -->
	<div class="col-lg-3 col-xs-6" >
	  <div class="small-box bg-red">
		<div class="inner"><h3 data-toggle='modal' data-target='#modalDetRecaudacion' onclick="fDetRecaudacion('12', '', '', 'Total')">Diciembre</h3>
            <asp:Literal ID="Lit12" runat="server"></asp:Literal>
		</div>
		<div class="icon"><i class="fa fa-calendar-check-o"></i></div>
	  </div>
	</div>
	<!-- ./col -->
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

            <div>
        <div class="modal modal-success fade" id="modalDetRecaudacion2">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title"><div runat="server" id="mdrecTitle2">Recaudacion Diaria</div> </h4>
              </div>
              <div class="modal-body">
                <%--<p>One fine body&hellip;</p>--%>
                  <div id="divDetRecaudacion2"></div>
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

            <div>
        <div class="modal modal-success fade" id="modalDetRecaudacion3">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title"><div runat="server" id="mdrecTitle3"></div> </h4>
              </div>
              <div class="modal-body">
                <%--<p>One fine body&hellip;</p>--%>
                  <div id="divDetRecaudacion3"></div>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-outline pull-left" data-dismiss="modal">Close</button>
              </div>
            </div>
          </div>
        </div>
        <!-- /.modal -->
            </div>

      </div>
      <!-- /.content-wrapper -->
   </form>
    <script>
        //document.getElementById('LiCIEIm').className = "treeview menu-open";
        //$('#LiCIEIm').addClass('menu-open');
        document.getElementById("LiSISMEDI").classList.add('menu-open');
        document.getElementById('UlSISMED').style.display = 'block';
        document.getElementById("ulRecaudacionFarRep").classList.add('active');
        var f = new Date();

    </script>
</asp:Content>

