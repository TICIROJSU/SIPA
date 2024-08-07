<%@ Page Language="C#" MasterPageFile="../../MasterPSISMED.master" AutoEventWireup="true" CodeFile="DispCompPorc.aspx.cs" Inherits="SISMEDG_JVAR_SISMED_DispCompPorc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>SISMED - La Libertad</title>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/json2.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/funciones.js?vfd=1")%>"></script>
    <%--<link rel="stylesheet" media="screen" href="style1.css?id=1">--%>

    <script language="javascript" type="text/javascript">

        function LoadCiudades(result) {
			$("#<%=DDLMRed.ClientID%>").html("");
			$("#<%=DDLMRed.ClientID%>").append($("<option></option>").attr("value", "").text("Micro Red"));
            $.each(result.d, function() {
                $("#<%=DDLMRed.ClientID%>").append($("<option></option>").attr("value", this.cod).text(this.descripcion))
            });
        }

        function LoadLocalidad(result) {
            $("#<%=DDLEESS.ClientID%>").html("");
            $.each(result.d, function() {
                $("#<%=DDLEESS.ClientID%>").append($("<option></option>").attr("value", this.cod).text(this.descripcion))
            });
        }

        function prueba01(d1, d2, d3) {
            var params = new Object();
            params.dat1 = d1; // cambiar la descripcion del params y el ddl
            params.dat2 = d2; 
            params.dat3 = d3; 
            params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "Default.aspx/GetPrueba01", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: LoadCiudades, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                }
            });
		}

        function RedToMRed() {
            var params = new Object();
            params.pais = params.pais = $("#<%=DDLAnio.ClientID%>").val();
            params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "DispCompPorc.aspx/GetCiudadesByPais", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: LoadCiudades, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                }
            });
		}

        function MRedToEESS() {
            var params = new Object();
            params.pais = params.pais = $("#<%=DDLAnio.ClientID%>").val();
            params = JSON.stringify(params);

            //$.ajax({
            //    type: "POST", url: "DispCompPorc.aspx/GetEESSByMRed", data: params, 
            //    contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
            //    success: LoadLocalidad, 
            //    error: function(XMLHttpRequest, textStatus, errorThrown) { 
            //        alert(textStatus + ": " + XMLHttpRequest.responseText); 
            //    }
            //});
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
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
							<asp:DropDownList ID="DDLFiltro" runat="server" class='form-control' OnSelectedIndexChanged="DDLFiltro_SelectedIndexChanged" AutoPostBack="true" >
								<asp:ListItem Text="Anual" Value="Anual"></asp:ListItem>
								<asp:ListItem Text="Mensual" Value="Mensual"></asp:ListItem>
							</asp:DropDownList>
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
							<asp:DropDownList ID="DDLAnio" runat="server" class='form-control' OnSelectedIndexChanged="DDLAnio_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column" style="display:block">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
							<asp:DropDownList ID="DDLMRed" runat="server" class='form-control' OnSelectedIndexChanged="DDLMRed_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>
                     </div>
                  </div>
               </div>
               <div class="column" style="display:block">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
							<asp:DropDownList ID="DDLEESS" runat="server" class='form-control' OnSelectedIndexChanged="DDLEESS_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
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
        <%--document.getElementById("<%=txtEESS.ClientID%>").value = dato; --%>
    }

</script>

<div class="row">
	<div> <%--class="col-lg-12 col-xs-6"--%>
		<asp:Literal ID="LitGraf1" runat="server"></asp:Literal>
	</div>

	<!-- ./col -->
</div>


            <!-- TERCERA FILA -->
            

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

        <div class="modal modal-success fade" id="modalDispo1-ss">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">
                    <div class="form-group">
                      <label for="bscprod" class="col-sm-3 control-label">Buscar Producto: </label>
                      <div class="col-sm-9">
	                    <input type="text" class="form-control" id="bscprod" placeholder="Nombre de Producto" onkeyup="fBscTblHTML('bscprod', 'tblbscrJS', 1)" autofocus="autofocus">
                      </div>
                    </div>
                </h4>
              </div>
              <div class="modal-body">
                  <div id="divDispo1"></div>
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
        document.getElementById("LiSISMEDI2").classList.add('menu-open');
        document.getElementById('UlSISMED2').style.display = 'block';
        document.getElementById("ulSISMEDCompDispo").classList.add('active');
        var f = new Date();

    </script>
</asp:Content>


