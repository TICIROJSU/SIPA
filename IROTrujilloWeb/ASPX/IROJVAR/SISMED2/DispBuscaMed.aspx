<%@ Page Language="C#" MasterPageFile="../../MasterPage.master" AutoEventWireup="true" CodeFile="DispBuscaMed.aspx.cs" Inherits="ASPX_IROJVAR_SISMED2_DispBuscaMed" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>IRO - HISMINSA</title>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/json2.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/funciones.js?vfd=1")%>"></script>
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

		function fShowProductos() {
            var params = new Object();
            params.txtFiltro = document.getElementById("<%=txtMedicam.ClientID%>").value; 
            params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "DispBuscaMed.aspx/GetMedicamentos", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divProductos").html(result.d) }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divProductos").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });
        }

    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div class="col-xs-8 col-xs-offset-2">
        <p class="cazador2">Busqueda por Productos</p>
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
                  <div class="col-md-6" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="input-group margin">
                            <div class="disabled">
								<asp:TextBox ID="txtMedicam" runat="server" class='form-control' autofocus="true" onClick="this.select();" placeholder="Medicamento (Min. 4 Letras)"></asp:TextBox>
                            </div>
                            <asp:HiddenField ID="txtHideCodMed" runat="server" />
                            <span class="input-group-btn">
								<button type="button" id="btnProd" name="btnProd" class='btn btn-info btn-flat' onclick="validaTxtMed()" data-toggle="modal" data-target="#modalPROD">...</button>
                                <%--<div class='btn btn-info btn-flat'>...</div>--%>
                            </span>
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
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                           <asp:TextBox ID="txtAnio" runat="server" class='form-control' ReadOnly="True"></asp:TextBox>
                        </div>
                     </div>
                  </div>
               </div>

               <!-- /.box-header -->
            </div>
            <!--FIN SEGUNDA FILA-->

<script>

    function txtMedicamCarga(Cod, Nombre)
    {
        document.getElementById("<%=txtHideCodMed.ClientID%>").value = Cod; 
        document.getElementById("<%=txtMedicam.ClientID%>").value = Nombre; 
    }

	function validaTxtMed() {
		numCar = document.getElementById("<%=txtMedicam.ClientID%>").value.length;
		if (numCar >= 4) {
			fShowProductos();
		}
		else {
			alert("Minimo 4 Letras para la Busqueda");
			 $("#divProductos").html("<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>Minimo 4 Caracteres</div></div>")
		}
	}

	var inputText = document.getElementById("<%=txtMedicam.ClientID%>");
	inputText.addEventListener("keyup", function(event) {
      if (event.keyCode === 13) {
		  event.preventDefault();
		  document.getElementById("btnProd").click();
      }
	});


</script>
<div style="display:block">

</div>

            <!-- TERCERA FILA -->
            <div class="row">
               <div class="col-md-12">
                  <div class="box" >
                        <div class="box-header">
                            <h3 class="box-title" style="margin-top: -6px; margin-bottom: -11px; margin-left: -7px;">
                            <div class="input-group-btn" style="display:none"><button class="btn btn-default" runat="server" type="button" title="Max.: 3,000 Registros" onserverclick="ExportarExcel_Click"><i class="fa fa-download "></i></button></div>

                            <div class="input-group-btn">
                                    <button class="btn btn-default" type="button" title="Max.: 1,000 Registros" onclick="exportTableToExcel('tbldscrg')"><i class="fa fa-download "></i>
                                    </button>
                            </div>

                            </h3>
                            <div class="box-tools">
                                <asp:Label ID="lblRuta" runat="server" Text="Descarga en: C:\"></asp:Label>
                                <div id="divtxtRuta"></div>
                            </div>
                        </div>
                     <!-- /.box-header -->
                     <div class="box-body table-responsive no-padding" id="habfiltro" style="display:block">
                         <asp:GridView ID="GVtable" runat="server" class="table table-condensed table-bordered"></asp:GridView>
                         <asp:Literal ID="LitTABL1" runat="server"></asp:Literal>
                         <div style="display:none"><div id="LitTABL1_1" runat="server"></div></div>



                         <div id="divDispo1_2" ></div>
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

        <div class="modal modal-success fade" id="modalPROD">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">
                    <div class="form-group">
                      <label for="bscprod" class="col-sm-3 control-label">Buscar Producto: </label>
                      <div class="col-sm-9">
	                    <input type="text" class="form-control" id="bscprod" placeholder="Nombre de Producto" onkeyup="fBscTblHTML('bscprod', 'tblproductos', 2)" autofocus="autofocus">
                      </div>
                    </div>
                </h4>
              </div>
              <div class="modal-body">
                  <div id="divProductos"></div>
                  <asp:Literal ID="LitProductos" runat="server"></asp:Literal>
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
        document.getElementById("ulSISMEDBuscaMed2").classList.add('active');
        var f = new Date();

    </script>
</asp:Content>


