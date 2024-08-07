<%@ Page Language="C#" MasterPageFile="../../MasterPage.master" AutoEventWireup="true" CodeFile="ProcOrdenTrabajo.aspx.cs" Inherits="ASPX_IROJVAR_Optica_ProcOrdenTrabajo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>IRO - HISMINSA</title>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/json2.js")%>"></script>
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

		function buscarProd() {

			$("#divShowProd").html("");

            var params = new Object();
			//alert('hola');
            params.vbuscarProd = document.getElementById("txtbuscaprod").value;
            params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "ProcOrdenTrabajo.aspx/GetbuscarProd", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divShowProd").html(result.d) }, //success: LoadPrueba01, //Procesar 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divShowProd").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});

			document.getElementById("txtbuscaprod").value = "";
			document.getElementById("txtbuscaprod").focus();
		}

		function buscarPac() {
            var params = new Object();
            params.vbuscarPac = document.getElementById("<%=txtNombrePaciente.ClientID%>").value;
            params = JSON.stringify(params);
			//alert('hola');

            $.ajax({
                type: "POST", url: "ProcOrdenTrabajo.aspx/GetbuscarPac", data: params, 
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
                type: "POST", url: "ProcOrdenTrabajo.aspx/GetBtnNuevo", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) {
					<%--var aResult = result.d.split("||sep||");
                    $("#<%=txtNroDoc.ClientID%>").val(aResult[1]);--%>
                    $("#<%=lblNroOT.ClientID%>").html(result.d);
                }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divErrores").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});
		}

		function btnGuardar() {
			tblcant = document.getElementById("tblcant").value;
			//tblcant = "2";
			if (parseInt(tblcant)<=0) {
				alert("Registre Informacion");
				return;
			}
			btnGuardarCab();
		}

		function btnGuardarCab() {
			var params = new Object();
			params.vNumeroOT = document.getElementById("<%=lblNroOT.ClientID%>").innerText; 
			params.vAddOrden = document.getElementById("<%=txtADD.ClientID%>").value; 
			params.vEsferaODLejos = document.getElementById("<%=txtLejODEsf.ClientID%>").value; 
			params.vEsferaOILejos = document.getElementById("<%=txtLejOIEsf.ClientID%>").value; 
			params.vEsferaODCerca = document.getElementById("<%=txtCerODEsf.ClientID%>").value; 
			params.vEsferaOICerca = document.getElementById("<%=txtCerOIEsf.ClientID%>").value; 
			params.vCilindroODLejos = document.getElementById("<%=txtLejODCil.ClientID%>").value; 
			params.vCilindroOILejos = document.getElementById("<%=txtLejOICil.ClientID%>").value; 
			params.vCilindroODCerca = document.getElementById("<%=txtCerODCil.ClientID%>").value; 
			params.vCilindroOICerca = document.getElementById("<%=txtCerOICil.ClientID%>").value; 
			params.vEjeODLejos = document.getElementById("<%=txtLejODEje.ClientID%>").value; 
			params.vEjeOILejos = document.getElementById("<%=txtLejOIEje.ClientID%>").value; 
			params.vEjeODCerca = document.getElementById("<%=txtCerODEje.ClientID%>").value; 
			params.vEjeOICerca = document.getElementById("<%=txtCerOIEje.ClientID%>").value; 
			params.vColorODLejos = document.getElementById("<%=txtLejODCol.ClientID%>").value; 
			params.vColorOILejos = document.getElementById("<%=txtLejOICol.ClientID%>").value; 
			params.vColorODCerca = document.getElementById("<%=txtCerODCol.ClientID%>").value; 
			params.vColorOICerca = document.getElementById("<%=txtCerOICol.ClientID%>").value; 
			params.vDipLejos = document.getElementById("<%=txtLejODDIP.ClientID%>").value; 
			params.vDipCerca = document.getElementById("<%=txtCerODDIP.ClientID%>").value; 
			params.vComentario = document.getElementById("<%=txtComentario.ClientID%>").value; 
			params.vEstadoCuota = ""; 
			params.vUsuarioRegistro = <%=Session["idUser2"].ToString()%>; 
            params.vUsuarioModificacion = <%=Session["idUser2"].ToString()%>; 
            params.vTipCliente = "";
            if (document.getElementById("<%=chkTipoCli.ClientID%>").checked) {
                params.vTipCliente = "Niño";
            }
            params.vTipReceta = document.getElementById("<%=DDLTipoRec.ClientID%>").value;
			params.vcodigopac = document.getElementById("txtIdPaciente").value;
			params.vDNIpac = document.getElementById("<%=txtNroDoc.ClientID%>").value;
			params.vnombrepac = document.getElementById("<%=txtNombrePaciente.ClientID%>").value;
			params.vtipodocumento = document.getElementById("<%=DDLTipoDoc.ClientID%>").options[document.getElementById("<%=DDLTipoDoc.ClientID%>").selectedIndex].text;
			params.vmontototal = document.getElementById("<%=txtMontoTotal.ClientID%>").value;
			params.vmotiDesc = document.getElementById("<%=DDLMotivo.ClientID%>").value; 
			params.vporcDesc = document.getElementById("<%=txtPorcDesc.ClientID%>").value; 
            params.vmontDesc = document.getElementById("<%=txtDescMonto.ClientID%>").value; 
            params.vACuenta = "0"; 
            if (document.getElementById("chkCuotas").checked) {
                params.vACuenta = "1";
            }
			params.vACueC1 = document.getElementById("<%=txtACuenta.ClientID%>").value; 
			params.vACueC2 = document.getElementById("<%=txtSaldo.ClientID%>").value; 
            params = JSON.stringify(params);

			$.ajax({
                type: "POST", url: "ProcOrdenTrabajo.aspx/SetBtnGuardar", data: params, 
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
						////Actualiza Stock
						//stkProductoDet(IdProdDet, ID, CodMontura, Cantidad);
						//stkProductoAlmacen(document.getElementById("txtDestinoId").value, ID, IdProdDet, Cantidad);
						//ProductosOptica(ID, Cantidad);
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
                type: "POST", url: "ProcOrdenTrabajo.aspx/SetBtnGuardarDet", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) {
                    $("#divmsj2").html(result.d);
                    alert(result.d);
                }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divmsj2").html(textStatus + ": " + XMLHttpRequest.responseText);
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
                type: "POST", url: "ProcOrdenTrabajo.aspx/GetNIngresoList", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divShowNIng").html(result.d); }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divShowNIng").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});

        }

		function gSelNIng(vpro, vfec, vcod, vmon, AddOrden, EsferaODLejos, EsferaOILejos, EsferaODCerca, EsferaOICerca, CilindroODLejos, CilindroOILejos, CilindroODCerca, CilindroOICerca, EjeODLejos, EjeOILejos, EjeODCerca, EjeOICerca, ColorODLejos, ColorOILejos, ColorODCerca, ColorOICerca, DipLejos, DipCerca, vTipCliente, vTipReceta) {
			<%--document.getElementById("<%=txtNroDoc.ClientID%>").value = vprovruc;--%>
			document.getElementById("<%=txtNombrePaciente.ClientID%>").value = vpro;
			<%--document.getElementById("<%=txtNroDoc.ClientID%>").value = vndoc;--%>
			document.getElementById("<%=txtFechaEmi.ClientID%>").value = vfec;
			document.getElementById("<%=txtMontoTotal.ClientID%>").value = vmon;

            document.getElementById("<%=txtADD.ClientID%>").value = AddOrden;
            document.getElementById("<%=txtLejODEsf.ClientID%>").value = EsferaODLejos;
            document.getElementById("<%=txtLejODCil.ClientID%>").value = CilindroODLejos;
            document.getElementById("<%=txtLejODEje.ClientID%>").value = EjeODLejos;
            document.getElementById("<%=txtLejODCol.ClientID%>").value = ColorODLejos;
            document.getElementById("<%=txtLejODDIP.ClientID%>").value = DipLejos;
            document.getElementById("<%=txtLejOIEsf.ClientID%>").value = EsferaOILejos;
            document.getElementById("<%=txtLejOICil.ClientID%>").value = CilindroOILejos;
            document.getElementById("<%=txtLejOIEje.ClientID%>").value = EjeOILejos;
            document.getElementById("<%=txtLejOICol.ClientID%>").value = ColorOILejos;
            document.getElementById("<%=txtCerODEsf.ClientID%>").value = EsferaODCerca;
            document.getElementById("<%=txtCerODCil.ClientID%>").value = CilindroODCerca;
            document.getElementById("<%=txtCerODEje.ClientID%>").value = EjeODCerca;
            document.getElementById("<%=txtCerODCol.ClientID%>").value = ColorODCerca;
            document.getElementById("<%=txtCerODDIP.ClientID%>").value = DipCerca;
            document.getElementById("<%=txtCerOIEsf.ClientID%>").value = EsferaOICerca;
            document.getElementById("<%=txtCerOICil.ClientID%>").value = CilindroOICerca;
            document.getElementById("<%=txtCerOIEje.ClientID%>").value = EjeOICerca;
            document.getElementById("<%=txtCerOICol.ClientID%>").value = ColorOICerca;

			document.getElementById("<%=lblNroOT.ClientID%>").innerText = vcod;

            if (true) {

            }

            gSelNIngDet(vcod);
		}

		function gSelNIngDet(vcod) {
			var params = new Object();
            params.vcod = vcod;
			params = JSON.stringify(params);

			$.ajax({
                type: "POST", url: "ProcOrdenTrabajo.aspx/GetNIngresoDet", data: params, 
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

        function btnImprimir() {
            //http://161.132.97.187:8888/ASPX/IROJVAR/Optica/RptDocOT.aspx?Codigo=37767
            vurl = "http://161.132.97.187:8888/ASPX/IROJVAR/Optica/RptDocOT.aspx?Codigo=";
            vcod = document.getElementById("<%=lblNroOT.ClientID%>").innerText;
            window.open(vurl + vcod, '_blank');


		}



    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div class="col-xs-8 col-xs-offset-2">
        <p class="cazador2">Optica - Orden de Trabajo</p>
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
							 <div class="btn btn-app" style="background-color:#D4E6F1" onclick="btnGuardar()">
								 <i class="fa fa-save"></i> Guardar
							 </div>
							 <div class="btn btn-app" style="background-color:#92EEB6" onclick="btnImprimir()">
								 <i class="fa fa-search"></i> Imprimir
							 </div>
							<a class="btn btn-app btn-danger bg-danger" style="background-color:#E6B0AA">
								<i class="fa fa-times-circle"></i> Anular
							</a>
							 <div class="btn btn-app" style="background-color:#D5F5E3" onclick="btnbuscar0()">
								 <i class="fa fa-search"></i> Buscar
							 </div>
                             <!-- Buscar -->
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
                             <!-- /Buscar -->


						 </div>
					 </div>
					 <div class="col-md-10">

					 </div>
				 </div>
			 </div>
            <div class="row" style="display:block;">
				<div class="column">
					<div class="col-md-8" style="padding:  0px 30px 10px 30px; background-color:#4dd2ff">
						<asp:Label ID="Label1" runat="server" Text="Datos Paciente"></asp:Label>

						<div class="row">
							<div class="col-md-2">
								<asp:Label ID="Label3" runat="server" Text="Tipo Documento"></asp:Label>
							</div>
							<div class="col-md-4">
								<asp:dropdownlist ID="DDLTipoDoc" runat="server" class='form-control'>
									<asp:ListItem Value="03" Selected="true">BOLETA DE VENTA</asp:ListItem>
									<asp:ListItem Value="01">FACTURA</asp:ListItem>
								</asp:dropdownlist>
							</div>
							<div class="col-md-1">DNI</div>
							<div class="col-md-3">
								<asp:TextBox ID="txtNroDoc" runat="server" class='form-control'></asp:TextBox>
							</div>
							<div class="col-md-2">
								<a href="../../IROJVAR/Optica/MantPaciente.aspx" target="_blank">
									<div class="btn btn-info" >
										<i class="fa fa-user-plus"></i>
									</div>
								</a>
							</div>
						</div>
						<p></p>
						<div class="row">
							<div class="col-md-2">
								<asp:Label ID="Label8" runat="server" Text="Paciente"></asp:Label>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="txtNombrePaciente" runat="server" class='form-control'></asp:TextBox>
								<input type="hidden" id="txtIdPaciente" name="txtIdPaciente" />
							</div>
							
                            <div class="col-md-2">
								<asp:dropdownlist ID="DDLTipoRec" runat="server" class='form-control'>
									<asp:ListItem Value="IRO" Selected="true">Rec. IRO</asp:ListItem>
									<asp:ListItem Value="Externa">Rec. Externa</asp:ListItem>
								</asp:dropdownlist>								
							</div>
                            <div class="col-md-2">
							    <div class="btn" id="divChkVerTodo" style="visibility:visible" >
                                    <asp:CheckBox ID="chkTipoCli" runat="server" Text="&nbsp;Niño" />
							    </div>				
							</div>

							<div class="col-md-2">
								<div class="btn btn-info" id="btnBuscarPac" onclick="buscarPac();" data-toggle="modal" data-target="#modalShowPac">
									<i class="fa fa-users"></i>
								</div>
							</div>

						</div>

					</div>
				</div>
				<div class="column">
					<div class="col-md-4" style="padding:  0px 30px 10px 30px; background-color:#1ac6ff">
						<div class="row">
							<div class="col-md-6" style="display:none">
								<input type="text" id="txtIdIngreso" class='form-control' />
								<code style="color:black; background-color:transparent">Id OT</code>
							</div>
							<div class="col-md-6" style="display:none">
								<asp:TextBox ID="txtOrigen" runat="server" class='form-control'>Tienda</asp:TextBox>
								<code style="color:black; background-color:transparent">Alm Origen</code>
								<input type="hidden" id="txtOrigenId" name="txtOrigenId" value="2" />
							</div>
						</div>
						<asp:Label ID="Label2" runat="server" Text="Orden de Trabajo"></asp:Label>
						<div class="row">
							<div class="col-md-6">
								<asp:Label ID="Label4" runat="server" Text="Nro. Ord. Trabajo"></asp:Label>
							</div>
							<div class="col-md-6">
								<asp:Label ID="Label5" runat="server" Text="Fecha Emision"></asp:Label>
							</div>

						</div>
						<br />
						<div class="row">
							<div class="col-md-6">
								<asp:Label ID="lblNroOT" runat="server" class='form-control' Text="00000000"></asp:Label>
							</div>
							<div class="col-md-6">
								<asp:TextBox ID="txtFechaEmi" runat="server" class='form-control' type="date"></asp:TextBox>
							</div>

						</div>
					</div>
				</div>
			</div>
        




            <!--SEGUNDA FILA-->             
			 <div class="row" style="display:block;">
				<div class="column">
					<div class="col-md-2" style="padding:  0px 30px 10px 30px; background-color:#ffff80">
						<asp:Label ID="Label9" runat="server" Text="ADD"></asp:Label>
						<br />
						<div class="row">
							<div class="col-md-12">
								<asp:textbox runat="server" ID="txtADD" class='form-control' type="number" step="0.1">0</asp:textbox>
							</div>

						</div>
						<br />
						<div class="row">
							<div class="col-md-12">

							</div>
						</div>
						<br />
						<div class="row">
							<div class="col-md-12">

							</div>
						</div>

					</div>
				</div>
				<div class="column">
					<div class="col-md-5" style="padding:  0px 30px 10px 30px; background-color:#ffff99">
						<asp:Label ID="Label13" runat="server" Text="Lejos"></asp:Label>
						<div class="row">
							<div class="col-md-2"></div>
							<div class="col-md-2">Esfera</div>
							<div class="col-md-2">Cilindro</div>
							<div class="col-md-2">Eje</div>
							<div class="col-md-2">Color</div>
							<div class="col-md-2">DIP</div>
						</div>
						<p></p>
						<div class="row">
							<div class="col-md-2">OD</div>
							<div class="col-md-2" style="padding:  0px 3px 0px 3px; ">
					<asp:textbox runat="server" ID="txtLejODEsf" class='form-control' type="number" step="0.05">0</asp:textbox>
							</div>
							<div class="col-md-2" style="padding:  0px 3px 0px 3px; ">
					<asp:textbox runat="server" ID="txtLejODCil" class='form-control' type="number" step="0.05">0</asp:textbox>
							</div>
							<div class="col-md-2" style="padding:  0px 3px 0px 3px; ">
					<asp:textbox runat="server" ID="txtLejODEje" class='form-control' type="number" step="0.05">0</asp:textbox>
							</div>
							<div class="col-md-2" style="padding:  0px 3px 0px 3px; ">
					<asp:textbox runat="server" ID="txtLejODCol" class='form-control' type="number" step="0.05">0</asp:textbox>
							</div>
							<div class="col-md-2" style="padding:  0px 3px 0px 3px; ">
					<asp:textbox runat="server" ID="txtLejODDIP" class='form-control' type="number" step="0.05">0</asp:textbox>
							</div>

						</div>
						<p></p>
						<div class="row">
							<div class="col-md-2">OI</div>
							<div class="col-md-2" style="padding:  0px 3px 0px 3px; ">
					<asp:textbox runat="server" ID="txtLejOIEsf" class='form-control' type="number" step="0.05">0</asp:textbox>
							</div>
							<div class="col-md-2" style="padding:  0px 3px 0px 3px; ">
					<asp:textbox runat="server" ID="txtLejOICil" class='form-control' type="number" step="0.05">0</asp:textbox>
							</div>
							<div class="col-md-2" style="padding:  0px 3px 0px 3px; ">
					<asp:textbox runat="server" ID="txtLejOIEje" class='form-control' type="number" step="0.05">0</asp:textbox>
							</div>
							<div class="col-md-2" style="padding:  0px 3px 0px 3px; ">
					<asp:textbox runat="server" ID="txtLejOICol" class='form-control' type="number" step="0.05">0</asp:textbox>
							</div>
							<div class="col-md-2" style="padding:  0px 3px 0px 3px; ">
							</div>

						</div>
					</div>
				</div>

				 <div class="column">
					<div class="col-md-5" style="padding:  0px 30px 10px 30px; background-color:#ffffb3">
						<asp:Label ID="Label10" runat="server" Text="Cerca"></asp:Label>
						<div class="row">
							<div class="col-md-2"></div>
							<div class="col-md-2">Esfera</div>
							<div class="col-md-2">Cilindro</div>
							<div class="col-md-2">Eje</div>
							<div class="col-md-2">Color</div>
							<div class="col-md-2">DIP</div>
						</div>
						<p></p>
						<div class="row">
							<div class="col-md-2">OD</div>
							<div class="col-md-2" style="padding:  0px 3px 0px 3px; ">
					<asp:textbox runat="server" ID="txtCerODEsf" class='form-control' type="number" step="0.05">0</asp:textbox>
							</div>
							<div class="col-md-2" style="padding:  0px 3px 0px 3px; ">
					<asp:textbox runat="server" ID="txtCerODCil" class='form-control' type="number" step="0.05">0</asp:textbox>
							</div>
							<div class="col-md-2" style="padding:  0px 3px 0px 3px; ">
					<asp:textbox runat="server" ID="txtCerODEje" class='form-control' type="number" step="0.05">0</asp:textbox>
							</div>
							<div class="col-md-2" style="padding:  0px 3px 0px 3px; ">
					<asp:textbox runat="server" ID="txtCerODCol" class='form-control' type="number" step="0.05">0</asp:textbox>
							</div>
							<div class="col-md-2" style="padding:  0px 3px 0px 3px; ">
					<asp:textbox runat="server" ID="txtCerODDIP" class='form-control' type="number" step="0.05">0</asp:textbox>
							</div>

						</div>
						<p></p>
						<div class="row">
							<div class="col-md-2">OI</div>
							<div class="col-md-2" style="padding:  0px 3px 0px 3px; ">
					<asp:textbox runat="server" ID="txtCerOIEsf" class='form-control' type="number" step="0.05">0</asp:textbox>
							</div>
							<div class="col-md-2" style="padding:  0px 3px 0px 3px; ">
					<asp:textbox runat="server"  ID="txtCerOICil" class='form-control' type="number" step="0.05">0</asp:textbox>
							</div>
							<div class="col-md-2" style="padding:  0px 3px 0px 3px; ">
					<asp:textbox runat="server"  ID="txtCerOIEje" class='form-control' type="number" step="0.05">0</asp:textbox>
							</div>
							<div class="col-md-2" style="padding:  0px 3px 0px 3px; ">
					<asp:textbox runat="server"  ID="txtCerOICol" class='form-control' type="number" step="0.05">0</asp:textbox>
							</div>
							<div class="col-md-2" style="padding:  0px 3px 0px 3px; ">
							</div>

						</div>
					</div>
				</div>


			</div>


			<!--TERCERA FILA-->             
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

	var txtNomPac = document.getElementById("<%=txtNombrePaciente.ClientID%>");
	txtNomPac.addEventListener("keyup", function(event) {
      if (event.keyCode === 13) {
		  event.preventDefault();
		  document.getElementById("btnBuscarPac").click();
      }
	});

	function agregaprodKD(nroitem) {
		if(window["event"]["keyCode"]===13) 
		{
			event.preventDefault();
			document.getElementById("btnAgregaProd" + nroitem).click();
		}
	}

    var el = document.getElementById("<%=txtADD.ClientID%>")
    el.value = el.valueAsNumber.toFixed(2)
    el.addEventListener('input', function(event) {
      event.target.value = event.target.valueAsNumber.toFixed(2)
    });


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
			document.getElementById("tblProductos").getElementsByTagName('tbody')[0].insertRow(-1).innerHTML = '<td>' + cod + '</td><td>' + prod + '</td><td>' + codmontura + '</td><td>' + cant + '</td><td>' + prec + '</td><td>' + (cant * prec) + '</td><td>' + idmontura + '</td>';
			document.getElementById("tblcant").value = parseInt(document.getElementById("tblcant").value) + 1;
			document.getElementById("<%=txtMontoTotal.ClientID%>").value = parseFloat(document.getElementById("<%=txtMontoTotal.ClientID%>").value) + (cant * prec);
		}
		else {
			alert('Debe ingresar una cantidad');
		}
        SumaFilas();
	}

	function agregarPaciente(cod, dni, pac) {
		document.getElementById("<%=txtNroDoc.ClientID%>").value = dni;
		document.getElementById("<%=txtNombrePaciente.ClientID%>").value = pac;
		document.getElementById("txtIdPaciente").value = cod;
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

    function SumaFilas() {
        var table = document.getElementById("tblProductos");
        var rowCount = table.rows.length;
        if (rowCount <= 1) {
            alert('Sin Filas para sumar');
            return;
        }

        for (var i = 0; i < length; i++) {

        }
        
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
					<div class="col-md-3" style="padding:  0px 30px 10px 30px; ">
						<div class="row">
							<div class="col-md-12">
								Comentario
							</div>
							<div class="col-md-12">
								<asp:textbox ID="txtComentario" runat="server" TextMode="multiline" Rows="5" class='form-control' ></asp:textbox>
							</div>
						</div>
						<p></p>
					</div>
				</div>
				
				<div class="column">
					<div class="col-md-3" style="padding:  0px 30px 10px 30px; visibility:hidden; display:block ">
						<div class="row">
							<div class="col-md-6 checkbox">
								<label>
									<input type="checkbox" id="chkDescuento" class='' value="Descuento" />
									Descuento
								</label>
							</div>
						</div>
						<div class="row">
							<div class="col-md-4">Motivo</div>
							<div class="col-md-8">
								<asp:dropdownlist ID="DDLMotivo" runat="server" class='form-control'>
									<asp:ListItem Value="" Selected="true"></asp:ListItem>
									<asp:ListItem Value="TRABAJADOR INSTITUCIONAL" >TRABAJADOR INSTITUCIONAL</asp:ListItem>
									<asp:ListItem Value="SERVICIO SOCIAL">SERVICIO SOCIAL</asp:ListItem>
								</asp:dropdownlist>
							</div>
						</div>

						<div class="row">
							<div class="col-md-1">%</div>
							<div class="col-md-4">
								<asp:textbox runat="server" ID="txtPorcDesc" class='form-control' type="number" step="1" onchange="fCalDescuento()">0</asp:textbox>
							</div>
							<div class="col-md-1" style="text-align:left"><b>%</b></div>
							<div class="col-md-1" style="text-align:left"><b>&nbsp;</b></div>
							<div class="col-md-4">
								<asp:textbox runat="server" ID="txtDescMonto" class='form-control' >0</asp:textbox>
							</div>
						</div>
						<div class="row">
							
							<div class="col-md-12 hide">
								<asp:textbox runat="server" class='form-control' ></asp:textbox>
							</div>
						</div>

						<p></p>
					</div>
				</div>
<script>
    function fCuotas() {
	    if (document.getElementById("divACuenta").style.visibility=="visible") {
		    document.getElementById("divACuenta").style.visibility="hidden";
		    document.getElementById("divSaldo").style.visibility="hidden";
	    }
	    else {
		    document.getElementById("divACuenta").style.visibility="visible";
            document.getElementById("divSaldo").style.visibility = "visible";
            document.getElementById("<%=txtACuenta.ClientID%>").focus();
            document.getElementById("<%=txtACuenta.ClientID%>").select();
        }

        fCalACuenta();
    }

    function fCalDescuento() {

        if (document.getElementById("chkDescuento").checked) {
            document.getElementById("<%=txtDescMonto.ClientID%>").value = Math.round(document.getElementById("<%=txtMontoTotal.ClientID%>").value * document.getElementById("<%=txtPorcDesc.ClientID%>").value) / 100;
        }
        else {
            document.getElementById("<%=txtPorcDesc.ClientID%>").value = 0;
            document.getElementById("<%=txtDescMonto.ClientID%>").value = 0;
        }
        
    }

    function fCalACuenta() {
        if (document.getElementById("chkCuotas").checked) {
            tmpCalc = document.getElementById("<%=txtMontoTotal.ClientID%>").value - document.getElementById("<%=txtACuenta.ClientID%>").value;
            document.getElementById("<%=txtSaldo.ClientID%>").value = Math.round(tmpCalc * 100) / 100
        }
        else {
            document.getElementById("<%=txtACuenta.ClientID%>").value = 0.00;
            document.getElementById("<%=txtSaldo.ClientID%>").value = 0.00;
        }
       
    }

</script>
				<div class="column">
					<div class="col-md-3" style="padding: 0px 30px 10px 30px; ">
						<div class="row">
							<div class="col-md-12 checkbox">
								<label>
									<input type="checkbox" id="chkCuotas" value="Cuotas" onclick="fCuotas()" /> ¿Desea Pagar a Cuotas?
								</label>
							</div>
						</div>
						<div class="row" id="divACuenta" style="visibility:hidden;">
							<div class="col-md-4">A Cuenta</div>
							<div class="col-md-8">
								<asp:textbox runat="server" ID="txtACuenta" class='form-control' value="0.00" onchange="fCalACuenta()"></asp:textbox>
                                <input type="hidden" id="txtACuentaMin" />
							</div>
						</div>
						<div class="row" id="divSaldo" style="visibility: hidden;">
							<div class="col-md-4">Saldo</div>
							<div class="col-md-8">
								<asp:textbox runat="server" ID="txtSaldo" class='form-control' value="0.00" ></asp:textbox>
							</div>
						</div>
						<p></p>
					</div>
				</div>

				<div class="column">
					<div class="col-md-3" style="padding:  0px 30px 10px 30px; ">
						<div class="row" style="display:none;" >
							<div class="col-md-4" style="text-align: right; ">Sub Total</div>
							<div class="col-md-8">
								<asp:textbox runat="server" ID="txtSubTotal" class='form-control' value="0.00" ></asp:textbox>
							</div>
						</div>
						<div class="row" style="display:none;" >
							<div class="col-md-4" style="text-align: right;">IGV</div>
							<div class="col-md-8">
								<asp:textbox runat="server" ID="txtIGV" class='form-control' value="0.00" ></asp:textbox>
							</div>
						</div>
						<div class="row" style="display:none;" >
							<div class="col-md-4" style="text-align: right;">Descuento</div>
							<div class="col-md-8">
								<asp:textbox runat="server" ID="txtDescuento" class='form-control' value="0.00" ></asp:textbox>
							</div>
						</div>
						<div class="row">
							<div class="col-md-4" style="text-align: right;"><b>TOTAL</b></div>
							<div class="col-md-8">
								<asp:textbox runat="server" ID="txtMontoTotal" class='form-control' value="0.00" oninput="fCalDescuento()"></asp:textbox>
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

        <div class="modal modal-warning fade" id="modalShowPac">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Pacientes</h4>
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

        <div class="modal modal-warning fade" id="modalShowNIng">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Listado de Documentos</h4>
              </div>
              <div class="modal-body">
                  <div id="divShowNIng"></div>
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
        document.getElementById("ulOpticaProc").classList.add('active');
        document.getElementById("ulLiOptProcOrdTrab").classList.add('active');
        var f = new Date();

    </script>
</asp:Content>
