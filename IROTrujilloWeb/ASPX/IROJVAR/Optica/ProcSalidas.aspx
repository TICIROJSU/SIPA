<%@ Page Language="C#" MasterPageFile="../../MasterPage.master" AutoEventWireup="true" CodeFile="ProcSalidas.aspx.cs" Inherits="ASPX_IROJVAR_Optica_ProcSalidas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>Optica</title>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/json2.js")%>"></script>
    <%--<link rel="stylesheet" media="screen" href="style1.css?id=1">--%>

    <script language="javascript" type="text/javascript">

		function buscarProd() {	
            var params = new Object();
			//alert('hola');
            params.vbuscarProd = document.getElementById("txtbuscaprod").value;
            params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "ProcSalidas.aspx/GetbuscarProd", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divShowProd").html(result.d) }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divShowProd").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});

			document.getElementById("txtbuscaprod").value = "";
			document.getElementById("txtbuscaprod").focus();
		}

		function buscarProv() {
            var params = new Object();
			//alert('hola');
            params.vbuscarPac = document.getElementById("<%=txtRUC.ClientID%>").value;
            params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "ProcSalidas.aspx/GetbuscarPac", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
				success: function (result) { $("#divShowPac").html(result.d); }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divShowPac").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});

		}

		function btnNuevo() {
			var params = new Object();
            params.vTipDoc = document.getElementById("<%=DDLTipoDoc.ClientID%>").value;
            params.vUser = "";
            params = JSON.stringify(params);
			//alert('hola');

            $.ajax({
                type: "POST", url: "ProcSalidas.aspx/GetBtnNuevo", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) {
					var aResult = result.d.split("||sep||");
                    $("#<%=txtNroDoc.ClientID%>").val(aResult[1]);
                    $("#<%=txtNroDoc.ClientID%>").html(aResult[0]);
                }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divErrores").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});
		}

		function btnGuardar() {
			tblcant = document.getElementById("tblcant").value;
			if (parseInt(tblcant)<=0) {
				alert("Registre Informacion");
				return;
			}
			btnGuardarCab();



		}

		function btnGuardarCab() {
			var params = new Object();
            params.vfechadoc = document.getElementById("<%=txtFechaEmi.ClientID%>").value;
            params.vidproveedor = document.getElementById("txtidProveedor").value;
            params.viduser = <%=Session["idUser2"].ToString()%>;
            params.vmotivo = "";
            params.vobservacion = "";
            params.vmontoc = document.getElementById("<%=txtMontoTotal.ClientID%>").value;
            params.vtipodoc = document.getElementById("<%=DDLTipoDoc.ClientID%>").value;
            params.vnrodoc = document.getElementById("<%=txtNroDoc.ClientID%>").value;
            params.vidalmorig = document.getElementById("txtOrigenId").value;
            params.vidalmdst = document.getElementById("txtDestinoId").value;
            params = JSON.stringify(params);

			$.ajax({
                type: "POST", url: "ProcSalidas.aspx/SetBtnGuardar", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
				success: function (result) {
					$("#txtIdIngreso").val(result.d);
					var idming = document.getElementById("txtIdIngreso").value;
					var table = document.getElementById('tblProductos'); 
					for (var i=1; i < table.rows.length; i++){								
						ID = table.rows[i].cells[0].innerHTML;
						CodMontura = table.rows[i].cells[2].innerHTML;
						Cantidad = table.rows[i].cells[3].innerHTML;
						Precio = table.rows[i].cells[4].innerHTML;
						Importe = table.rows[i].cells[5].innerHTML;
						IdProdDet = table.rows[i].cells[6].innerHTML;
						btnGuardarDet(idming, ID, CodMontura, Cantidad, Precio, Importe, IdProdDet);
						//Actualiza Stock
						stkProductoDet(IdProdDet, ID, CodMontura, Cantidad);
						stkProductoAlmacen(document.getElementById("txtDestinoId").value, ID, IdProdDet, Cantidad);
						ProductosOptica(ID, Cantidad);
					}
				}, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divmsj1").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});
		}

		function btnGuardarDet(idming, IDProd, CodMontura, Cantidad, Precio, Importe, IdProdDet) {
			var params = new Object();
            params.idming = idming;
            params.CodigoProd = IDProd;
            params.idproddet = IdProdDet;
            params.codmontura = CodMontura;
            params.Cantidad = Cantidad;
            params.PrecioC = Precio;
			params.ImporteC = Importe;
			params = JSON.stringify(params);

			$.ajax({
                type: "POST", url: "ProcSalidas.aspx/SetBtnGuardarDet", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divmsj2").html(result.d); }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divmsj2").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});
		}

		function stkProductoDet(IdProdDet, CodProd, CodMontura, Cantidad) {
			var params = new Object();
            params.IdProdDet = IdProdDet;
            params.CodProd = CodProd;
            params.CodMontura = CodMontura;
            params.Cantidad = Cantidad;
			params = JSON.stringify(params);

			$.ajax({
                type: "POST", url: "ProcSalidas.aspx/SetstkProductoDet", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divmsj3").html(result.d); }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divmsj3").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});
		}

		function ProductosOptica(CodProd, Cantidad) {
			var params = new Object();
            params.CodProd = CodProd;
            params.Cantidad = Cantidad;
			params = JSON.stringify(params);

			$.ajax({
                type: "POST", url: "ProcSalidas.aspx/SetProductosOptica", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divmsj4").html(result.d); }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divmsj4").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});
		}

		function stkProductoAlmacen(idDestino, CodProd, IdProdDet, Cantidad) {
			var params = new Object();
            params.idDestino = idDestino;
            params.CodProd = CodProd;
            params.IdProdDet = IdProdDet;
            params.Cantidad = Cantidad;
			params = JSON.stringify(params);

			$.ajax({
                type: "POST", url: "ProcSalidas.aspx/SetstkProductoAlmacen", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divmsj5").html(result.d); }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divmsj5").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});
		}

		function btnbuscar0() {
			if (document.getElementById("divFDesde").style.visibility=="visible") {
				document.getElementById("divFDesde").style.visibility="hidden";
				document.getElementById("divFHasta").style.visibility="hidden";
				document.getElementById("divBtnBuscarNI").style.visibility="hidden";
			}
			else {
				document.getElementById("divFDesde").style.visibility="visible";
				document.getElementById("divFHasta").style.visibility="visible";
				document.getElementById("divBtnBuscarNI").style.visibility="visible";
			}
		}

		function buscarNIng() {
			var params = new Object();
            params.vDesde = document.getElementById("<%=txtFDesde.ClientID%>").value;
            params.vHasta = document.getElementById("<%=txtFHasta.ClientID%>").value;
			params = JSON.stringify(params);

			$.ajax({
                type: "POST", url: "ProcSalidas.aspx/GetNIngresoList", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divShowNIng").html(result.d); }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divShowNIng").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});

		}

		function gSelNIng(vprovruc, vpro, vndoc, vfec, vcod, vmon) {
			document.getElementById("<%=txtRUC.ClientID%>").value = vprovruc;
			document.getElementById("<%=txtProveedor.ClientID%>").value = vpro;
			document.getElementById("<%=txtNroDoc.ClientID%>").value = vndoc;
			document.getElementById("<%=txtFechaEmi.ClientID%>").value = vfec;
			document.getElementById("<%=txtMontoTotal.ClientID%>").value = vmon;

			gSelNIngDet(vcod);
		}

		function gSelNIngDet(vcod) {
			var params = new Object();
            params.vcod = vcod;
			params = JSON.stringify(params);

			$.ajax({
                type: "POST", url: "ProcSalidas.aspx/GetNIngresoDet", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
				success: function (result) {
					//$("#divShowNIng").html(result.d);
					document.getElementById("tblProductos").getElementsByTagName('tbody')[0].innerHTML = result.d;
				}, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divmsj5").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});

		}

    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div class="col-xs-8 col-xs-offset-2">
        <p class="cazador2">Optica - Salida de Productos</p>
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
							<a class="btn btn-app" onclick="btnNuevo()">
								<i class="fa fa-file-o"></i> Nuevo
							</a>
							 <div class="btn btn-app" style="background-color:#D4E6F1" onclick="btnGuardar()">
								 <i class="fa fa-save"></i> Guardar
							 </div>
							<a class="btn btn-app btn-danger bg-danger" style="background-color:#E6B0AA">
								<i class="fa fa-times-circle"></i> Anular
							</a>
							 <div class="btn btn-app" style="background-color:#D5F5E3" onclick="btnbuscar0()">
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

						 </div>
					 </div>
					 <div class="col-md-10">

					 </div>
				 </div>
			 </div>

            <div class="row" style="display:block; background-color:#4dd2ff">
				<p></p>
				<div class="column">
					<div class="col-md-8" style="padding:  0px 30px 10px 30px; ">
						<div class="row" style="display:none">
							<div class="col-md-2">
								<asp:Label ID="Label8" runat="server" Text="Proveedor"></asp:Label>
							</div>
							<div class="col-md-2">
								<asp:TextBox ID="txtRUC" runat="server" class='form-control'></asp:TextBox>
								<input type="hidden" name="txtidProveedor" id="txtidProveedor" />
								<code style="color:black; background-color:transparent">RUC</code>
							</div>
							<div class="col-md-6">
								<asp:TextBox ID="txtProveedor" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">Razón Social</code>
							</div>
							<div class="col-md-2">
								<div class="btn btn-info" id="btnBuscarProv" onclick="buscarProv();" data-toggle="modal" data-target="#modalShowProv">
									<i class="fa fa-users"></i>
								</div>
							</div>

						</div>
						<p></p>
						<div class="row">
							<div class="col-md-2">
								<asp:Label ID="Label3" runat="server" Text="Documento"></asp:Label>
							</div>
							<div class="col-md-4">
								<asp:dropdownlist ID="DDLTipoDoc" runat="server" class='form-control'>
									<asp:ListItem Value="GUIA DE REMISION" Selected="true">GUIA DE REMISION</asp:ListItem>
									<asp:ListItem Value="OTRO">OTRO</asp:ListItem>
								</asp:dropdownlist>
								<code style="color:black; background-color:transparent">Tipo</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="txtNroDoc" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">Numero</code>
							</div>
							<div class="col-md-2">
								<div class="btn btn-info" >
									<i class="fa fa-user-plus"></i>
								</div>
							</div>

						</div>

					</div>
				</div>
				<div class="column">
					<div class="col-md-4" style="padding:  0px 30px 10px 30px; ">
						<div class="row">
							<div class="col-md-6" style="display:none">
								<input type="text" id="txtIdIngreso" class='form-control' />
								<code style="color:black; background-color:transparent">Id Salida</code>
							</div>
							<div class="col-md-6" style="display:none">
								<asp:TextBox ID="txtOrigen" runat="server" class='form-control'>Tienda</asp:TextBox>
								<code style="color:black; background-color:transparent">Alm Origen</code>
								<input type="hidden" id="txtOrigenId" name="txtOrigenId" value="2" />
							</div>
						</div>
						<p></p>
						<div class="row">
							<div class="col-md-6">
								<asp:TextBox ID="txtFechaEmi" runat="server" class='form-control' type="date"></asp:TextBox>
								<code style="color:black; background-color:transparent">Fecha Emision</code>
							</div>
							<div class="col-md-6" style="display:none">
								<asp:TextBox ID="txtDestino" runat="server" class='form-control'>Logistica</asp:TextBox>
								<code style="color:black; background-color:transparent">Alm Destino</code>
								<input type="hidden" id="txtDestinoId" name="txtDestinoId" value="1" />
							</div>
						</div>
					</div>
				</div>
			</div>
        




            <!--SEGUNDA FILA-->             
			 <div class="row" style="display:block;">
				<div class="column">
					<div class="col-md-12" style="padding:  0px 30px 10px 30px; background-color:#F5B7B1">
						<asp:Label ID="Label11" runat="server" Text="DETALLE DE PRODUCTOS"></asp:Label>
						<p></p>
						<div class="row">
							<div class="col-md-2">
								<input type="text" id="txtbuscaprod" class='form-control' placeholder="Producto" />
							</div>
							<div class="col-md-2">
								<div class="btn btn-info" id="btnBuscarProd" onclick="buscarProd();" data-toggle="modal" data-target="#modalShowProd"> Buscar Productos</div>
							</div>
							<div class="col-md-2">
								<asp:linkbutton runat="server" class="btn btn-info" ID="btnQuitarP">
									Quitar
								</asp:linkbutton>
							</div>
							<div class="col-md-1">
								<input type="hidden" name="tblcant" id="tblcant" value="0" />
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

	var inputTextProv = document.getElementById("<%=txtRUC.ClientID%>");
	inputTextProv.addEventListener("keyup", function(event) {
      if (event.keyCode === 13) {
		  event.preventDefault();
		  document.getElementById("btnBuscarProv").click();
      }
	});

	function agregaprodKD(nroitem) {
		if(window["event"]["keyCode"]===13) 
		{
			event.preventDefault();
			document.getElementById("btnAgregaProd" + nroitem).click();
		}
	}

</script>
					<div class="col-md-12" style="padding:  0px 30px 10px 30px; background-color:#F5B7B1">
						<asp:GridView ID="GVProductos" runat="server">
						   <Columns>
							   <asp:BoundField HeaderText="ID" DataField="ID"  />
							   <asp:BoundField HeaderText="Name (long)" DataField="Name">
								   <ItemStyle Width="140px"></ItemStyle>
							   </asp:BoundField>
							   <asp:BoundField HeaderText="other" DataField="other"  />
						   </Columns>

						</asp:GridView>
<script>

	function agregarFila(cod, prod, codmontura, prec, cant, idmontura) {
		if (cant > 0 || cant != '') {
			//if (idmontura == "0") {
				//idmontura = GeneraIdMontura(cod, codmontura);
				//setTimeout(agregarFila2(cod, prod, codmontura, prec, cant, idmontura), 2000);
				//setTimeout( function () { agregarFila2 ( cod, prod, codmontura, prec, cant, idmontura ); }, 2000 );
			//}
			agregarFila2(cod, prod, codmontura, prec, cant, idmontura);
		}
		else {
			alert('Debe ingresar una cantidad');
		}
	}

	function agregarFila2(cod, prod, codmontura, prec, cant, idmontura) {
		document.getElementById("tblProductos").getElementsByTagName('tbody')[0].insertRow(-1).innerHTML = '<td>' + cod + '</td><td>' + prod + '</td><td>' + codmontura + '</td><td>' + cant + '</td><td>' + prec + '</td><td>' + (cant * prec) + '</td><td>' + idmontura + '</td>';
		document.getElementById("tblcant").value = parseInt(document.getElementById("tblcant").value) + 1;
		document.getElementById("<%=txtMontoTotal.ClientID%>").value = parseFloat(document.getElementById("<%=txtMontoTotal.ClientID%>").value) + (cant * prec);
	}


	function agregarPer(cod, dni, pac) {
		document.getElementById("<%=txtProveedor.ClientID%>").value = pac;
		document.getElementById("txtidProveedor").value = cod;
		document.getElementById("<%=txtRUC.ClientID%>").value = dni;
	}

	function eliminarFila(){
	  var table = document.getElementById("tblProductos");
	  var rowCount = table.rows.length;
	  //console.log(rowCount);
  
	  if(rowCount <= 1)
		alert('No se puede eliminar el encabezado');
	  else
		table.deleteRow(rowCount -1);
	}
</script>
						<div class="box box-body table-responsive no-padding" style='color:#000000; '>
						<table id="tblProductos" class="table table-hover" >
							<thead>
								<tr>
									<td>ID</td>
									<td>Producto</td>
									<td>CodMontura</td>
									<td>Cantidad</td>
									<td>Precio</td>
									<td>Importe</td>
									<td>IdMont</td>
								</tr>
							</thead>
							<tbody>

							</tbody>
						</table>
						</div>


					</div>
				</div>


				 <br />
			</div>


			<!-- CUARTA FILA -->
            <div class="row" style="display:block; background-color:#EBDEF0">
				<div class="column">
					<div class="col-md-3" style="padding:  0px 30px 10px 30px; visibility:hidden ">
						<div class="row">
							<div class="col-md-12">
								<asp:Label ID="Label12" runat="server" Text="Comentario"></asp:Label>
							</div>
							<div class="col-md-12">
								<asp:textbox ID="txtComentario" runat="server" TextMode="multiline" Rows="5" class='form-control' ></asp:textbox>
							</div>
						</div>
						<p></p>
					</div>
				</div>
				
				<div class="column">
					<div class="col-md-3" style="padding:  0px 30px 10px 30px; visibility:hidden ">
						<div class="row">
							<div class="col-md-6 checkbox">
								<label>
									<input type="checkbox" id="Descuento" class='' value="Descuento" />
									Descuento
								</label>
							</div>
						</div>
						<div class="row">
							<div class="col-md-4">Motivo</div>
							<div class="col-md-8">
								<asp:dropdownlist ID="DDLMotivo" runat="server" class='form-control'>
									<asp:ListItem Value="0" Selected="true"> </asp:ListItem>
									<asp:ListItem Value="1" >TRABAJADOR INSTITUCIONAL</asp:ListItem>
									<asp:ListItem Value="2">SERVICIO SOCIAL</asp:ListItem>
								</asp:dropdownlist>
							</div>
						</div>

						<div class="row">
							<div class="col-md-4">Seleccione</div>
							<div class="col-md-4">
								<asp:textbox runat="server" class='form-control' type="number" step="0.1"></asp:textbox>
							</div>
							<div class="col-md-1"><b>%</b></div>
						</div>
						<div class="row">
							
							<div class="col-md-12">
								<asp:textbox runat="server" class='form-control' ></asp:textbox>
							</div>
						</div>

						<p></p>
					</div>
				</div>
				
				<div class="column">
					<div class="col-md-3" style="padding:  0px 30px 10px 30px; visibility:hidden ">
						<div class="row">
							<div class="col-md-12 checkbox">
								<label>
									<input type="checkbox" id="Cuotas" class='' value="Cuotas" />
									¿Desea Pagar a Cuotas?
								</label>
							</div>
						</div>
						<div class="row">
							<div class="col-md-4">A Cuenta</div>
							<div class="col-md-8">
								<asp:textbox runat="server" class='form-control' value="0.00" ></asp:textbox>
							</div>
						</div>
						<div class="row">
							<div class="col-md-4">Saldo</div>
							<div class="col-md-8">
								<asp:textbox runat="server" class='form-control' value="0.00" ></asp:textbox>
							</div>
						</div>
						<p></p>
					</div>
				</div>

				<div class="column">
					<div class="col-md-3" style="padding:  0px 30px 10px 30px; ">
						<div class="row" style="display:none">
							<div class="col-md-4" style="text-align: right;">Sub Total</div>
							<div class="col-md-8">
								<asp:textbox runat="server" ID="txt1" class='form-control' value="0.00" ></asp:textbox>
							</div>
						</div>
						<div class="row" style="display:none">
							<div class="col-md-4" style="text-align: right;">IGV</div>
							<div class="col-md-8">
								<asp:textbox runat="server" ID="Textbox4" class='form-control' value="0.00" ></asp:textbox>
							</div>
						</div>
						<div class="row" style="display:none">
							<div class="col-md-4" style="text-align: right;">Descuento</div>
							<div class="col-md-8">
								<asp:textbox runat="server" ID="Textbox5" class='form-control' value="0.00" ></asp:textbox>
							</div>
						</div>
						<div class="row">
							<div class="col-md-4" style="text-align: right;"><b>TOTAL</b></div>
							<div class="col-md-8">
								<asp:textbox runat="server" ID="txtMontoTotal" class='form-control' value="0.00" ></asp:textbox>
							</div>
						</div>
						<p></p>
					</div>
				</div>

			</div>
        


<script>

</script>

            <!-- TERCERA FILA -->
            

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

        <div class="modal modal-warning fade" id="modalShowProv">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Proveedores</h4>
              </div>
              <div class="modal-body">
                  <div id="divShowPac"></div>
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
<script>

</script>

      </div>
      <!-- /.content-wrapper -->
   </form>
    <script>
        //document.getElementById('LiCIEIm').className = "treeview menu-open";
        //$('#LiCIEIm').addClass('menu-open');
        document.getElementById("LiOpticaI").classList.add('menu-open');
        document.getElementById('UlOptica').style.display = 'block';
        document.getElementById("ulOpticaProc").classList.add('active');
        document.getElementById("ulLiOptProcSal").classList.add('active');
        var f = new Date();

    </script>
</asp:Content>
