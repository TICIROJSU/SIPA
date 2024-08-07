<%@ Page Language="C#" MasterPageFile="../../MasterPage.master" AutoEventWireup="true" CodeFile="DonListar.aspx.cs" Inherits="ASPX_IROJVAR_Optica_DonListar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>IRO - HISMINSA</title>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/json2.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/funciones.js?vfd=1")%>"></script>
    <%--<link rel="stylesheet" media="screen" href="style1.css?id=1">--%>

    <script language="javascript" type="text/javascript">

        function DetAtenciones(vanio, vmes, vdia, turno, vplaza) {
            var params = new Object();
            params.vanio = vanio; // cambiar la descripcion del params y el ddl
            params.vmes = vmes; 
            params.vdia = vdia; 
            params.turno = turno; 
            params.vplaza = vplaza; 
            params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "Atenciones.aspx/GetDetAtenciones", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divDetAtencion").html(result.d) }, //success: LoadPrueba01, //Procesar 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divDetAtencion").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });
        }

		function btnAgregar() {
            var params = new Object();
            params.vNombre = document.getElementById("<%=txtNombre.ClientID%>").value; 
			var vddlmoneda = document.getElementById("<%=ddlMoneda.ClientID%>");
			params.vTMoneda = vddlmoneda.value; 
			params.vVTMoneda = vddlmoneda.options[vddlmoneda.selectedIndex].innerText;
            params.vPrecCompra = document.getElementById("<%=txtPrecioCompra.ClientID%>").value; 
            params.cPrecVenta = document.getElementById("<%=txtPrecioVenta.ClientID%>").value; 
            params.vCategoria = document.getElementById("<%=ddlCategoria.ClientID%>").value; 
            params.vUser = "<%=Session["idUser2"]%>";
			params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "MantProductos.aspx/SetBtnGuardar", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divMensaje").html(result.d) }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divMensaje").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });
		}

		function btnModificar() {
			if (document.getElementById("<%=lblCodProd.ClientID%>").innerText == "") {
				alert("Seleccione un Producto.");
				return;
			}
            var params = new Object();
            params.vCodigo = document.getElementById("<%=lblCodProd.ClientID%>").innerText; 
            params.vNombre = document.getElementById("<%=txtNombre.ClientID%>").value; 
			var vddlmoneda = document.getElementById("<%=ddlMoneda.ClientID%>");
			params.vTMoneda = vddlmoneda.value; 
			params.vVTMoneda = vddlmoneda.options[vddlmoneda.selectedIndex].innerText;
            params.vPrecCompra = document.getElementById("<%=txtPrecioCompra.ClientID%>").value; 
            params.cPrecVenta = document.getElementById("<%=txtPrecioVenta.ClientID%>").value; 
            params.vCategoria = document.getElementById("<%=ddlCategoria.ClientID%>").value; 
            params.vUser = "<%=Session["idUser2"]%>";
			params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "MantProductos.aspx/SetBtnModificar", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divMensaje").html(result.d) }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divMensaje").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });
        }

		function BorraReg(vcod) {
            var params = new Object();
            params.vCodigo = vcod; 
			params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "MantProductos.aspx/SetBtnEliminar", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
				success: function (result) { $("#divMensaje").html(result.d); Alert(result.d); }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divMensaje").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});

			document.getElementById("<%=bntBuscar.ClientID%>").click();
        }

    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div class="col-xs-8 col-xs-offset-2">
        <p class="cazador2">Mantenimiento de Productos</p>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

   
    <form id="form1" runat="server" autocomplete="off">
      <!-- Content Wrapper. Contains page content -->
      <div class="content-wrapper">
         <!-- Content Header (Page header) -->
         <!-- Main content -->
         <section class="content">

			 <div class="row" style="display:block; background-color:#AEB6BF; color:black">
				 <div class="column">
					 <div class="col-md-10">
						 <div class="row">
							<a class="btn btn-app" onclick="btnNuevo()">
								<i class="fa fa-file-o"></i> Nuevo
							</a>
							<a class="btn btn-app " style="background-color:#D5F5E3; display:none; ">
								<i class="fa fa-search"></i> Buscar
							</a>

						 </div>
					 </div>
					 <div class="col-md-10">

					 </div>
				 </div>
			 </div>
<script>

function btnNuevo()
{
	document.getElementById("divregistro").style.display="block";
}

function btnCancelar()
{
	document.getElementById("divregistro").style.display = "none";
	document.getElementById("<%=lblCodProd.ClientID%>").innerText = "";

}




</script>        
            <!-- inicia FILA de REGISTRO -->

            <div class="row" id="divregistro" style="display:none;">
				<p></p>
				<div class="column">
					<div class="col-md-8" style="padding:  0px 30px 10px 30px; ">

						<div class="row">
							<div class="col-md-2">Codigo</div>
							<div class="col-md-2">
								<asp:Label ID="lblCodProd" runat="server" class='form-control'></asp:Label>
							</div>
						</div>
						<p></p>
						<div class="row">
							<div class="col-md-2">Nombre</div>
							<div class="col-md-10">
								<asp:TextBox ID="txtNombre" runat="server" class='form-control'></asp:TextBox>
							</div>
						</div>
						<p></p>
						<div class="row">
							<div class="col-md-2">Moneda</div>
							<div class="col-md-4">
								<asp:dropdownlist ID="ddlMoneda" runat="server" class='form-control'>
								</asp:dropdownlist>
							</div>
							<div class="col-md-2">Categoria</div>
							<div class="col-md-4">
								<asp:dropdownlist ID="ddlCategoria" runat="server" class='form-control'>
								</asp:dropdownlist>
							</div>
						</div>
						<p></p>
						<div class="row">
							<div class="col-md-2">Precio Compra</div>
							<div class="col-md-4">
								<asp:TextBox ID="txtPrecioCompra" runat="server" class='form-control' style="text-align: right"></asp:TextBox>
							</div>
							<div class="col-md-2">Precio Venta</div>
							<div class="col-md-4">
								<asp:TextBox ID="txtPrecioVenta" runat="server" class='form-control' style="text-align: right"></asp:TextBox>
							</div>
						</div>


					</div>
				</div>
				<div class="column">
					<div class="col-md-4" style="padding:  0px 30px 10px 30px; ">
						<div class="row">
							<a class="btn btn-app" style="background-color:#F9E79F" onclick="btnAgregar()">
								<i class="fa fa-save"></i> Agregar
							</a>
							<a class="btn btn-app" style="background-color:#FDEBD0" onclick="btnModificar()">
								<i class="fa fa-edit"></i> Modificar
							</a>
						</div>
						<div class="row">
							<a class="btn btn-app btn-danger bg-danger" onclick="btnCancelar()" style="background-color:#E6B0AA">
								<i class="fa fa-times-circle"></i> Cancelar
							</a>
						</div>
					</div>
				</div>
				<p></p>
				<div class="row">
					<div class="col-md-12">
						<div class='form-control' id="divMensaje"></div>
					</div>
				</div>
			</div>

            <!-- fin FILA de REGISTRO -->

			 <hr style="height:2px;border-width:0;color:gray;background-color:gray"/>

            <!--SEGUNDA FILA-->             
            <div class="row" style="display:block;">
                <div class="column" style="display:block">
                  <div class="col-md-1" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:LinkButton ID="bntBuscar" runat="server" class="btn btn-success" OnClick="bntBuscar_Click" ><i class="fa fa-search"></i> Listar</asp:LinkButton>
                        </div>
                     </div>
                  </div>
               </div>

              <div class="column">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:TextBox ID="txtProd" runat="server" class='form-control' ></asp:TextBox>
                        </div>
                     </div>
                  </div>
               </div>
               
               <!-- /.box-header -->
            </div>
            <!--FIN SEGUNDA FILA-->

<script>
	var inputText = document.getElementById("<%=txtProd.ClientID%>");
	inputText.addEventListener("keyup", function(event) {
      if (event.keyCode === 13) {
		  event.preventDefault();
		  document.getElementById("<%=bntBuscar.ClientID%>").click();
      }
	});
</script>


            <!-- TERCERA FILA -->
            <div class="row">
               <div class="col-md-12">
                  <div class="box" >
                        <div class="box-header">
                            <h3 class="box-title" style="margin-top: -6px; margin-bottom: -11px; margin-left: -7px;">
                            <div class="input-group-btn" style="display:none">
                                    <button class="btn btn-default" runat="server" type="button" title="Max.: 3,000 Registros" onserverclick="ExportarExcel_Click">
                                    <i class="fa fa-download "></i>
                                    </button>
                            </div>
            <div class="input-group margin">
                            <div class="input-group-btn">
                                    <button class="btn btn-default" type="button" title="Max.: 1,000 Registros" onclick="exportTableToExcel('tblbscrJS')"><i class="fa fa-download "></i>
                                    </button>
                            </div>
                            <div><input type="text" class="form-control" id="bscprod2" placeholder="Buscar Nombre de Producto" onkeyup="fBscTblHTML('bscprod2', 'tblbscrJS', 1)" autofocus="autofocus">
                            </div>
            </div>
                            </h3>
                            <div class="box-tools" style="display:none">
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

	function SelFilatbl(vcod, vprod, vmon, vprecc, vprecv, vstk, vest, vcat, vcatcod, vmoncod)
	{
        document.getElementById("<%=lblCodProd.ClientID%>").innerText = vcod; 
        document.getElementById("<%=txtNombre.ClientID%>").value = vprod; 
        document.getElementById("<%=txtPrecioCompra.ClientID%>").value = vprecc; 
        document.getElementById("<%=txtPrecioVenta.ClientID%>").value = vprecv; 
        document.getElementById("<%=ddlCategoria.ClientID%>").value = vcatcod;
		document.getElementById("<%=ddlMoneda.ClientID%>").value = vmoncod;
		document.getElementById("upsubirpag").click();
		document.getElementById("divregistro").style.display="block";
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
          
        <div class="modal modal-warning fade" id="modalDetDx">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Detalle de la Atencion</h4>
              </div>
              <div class="modal-body">
                <%--<p>One fine body&hellip;</p>--%>
                  <div id="divDetDx"></div>
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

        <div class="modal modal-warning fade" id="modalShowProf">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Profesionales</h4>
              </div>
              <div class="modal-body">
                <%--<p>One fine body&hellip;</p>--%>
                  <div id="divShowProf"></div>
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
        document.getElementById("LiOpticaI").classList.add('menu-open');
        document.getElementById('UlOptica').style.display = 'block';
        document.getElementById("ulOpticaDon").classList.add('active');
        document.getElementById("ulLiOptDonListar").classList.add('active');
        var f = new Date();

    </script>
</asp:Content>
