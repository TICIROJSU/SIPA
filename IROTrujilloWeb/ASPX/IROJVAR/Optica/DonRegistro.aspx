<%@ Page Language="C#" MasterPageFile="../../MasterPage.master" AutoEventWireup="true" CodeFile="DonRegistro.aspx.cs" Inherits="ASPX_IROJVAR_Optica_DonRegistro" %>

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


    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div class="col-xs-8 col-xs-offset-2">
        <p class="cazador2">Optica - Ventas <asp:Label ID="lblTitulo" runat="server"></asp:Label></p>
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

                            <asp:LinkButton ID="btnGuardar" runat="server" class="btn btn-app" OnClick="btnGuardar_Click" >
                                <i class="fa fa-save"></i> Guardar
                            </asp:LinkButton>

							 <div class="btn btn-app" style="background-color:#D5F5E3" >
								 <i class="fa fa-search"></i> Buscar
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
					<div class="col-md-12" style="padding:  0px 30px 10px 30px; ">
						<div class="row" style="display:block">
							<div class="col-md-4">
								<asp:TextBox ID="CCOSTO_NOMBRE" runat="server" class='form-control'></asp:TextBox>
                                <asp:HiddenField ID="idCMNLlenado" runat="server" />
								<code style="color:black; background-color:transparent">CCosto Nombre</code>
							</div>
							<div class="col-md-4">
                                <asp:DropDownList ID="B_S" runat="server" class='form-control'>
                                    <asp:ListItem Value="B" Selected="true" >B</asp:ListItem>
                                    <asp:ListItem Value="S" >S</asp:ListItem>
                                </asp:DropDownList>
								<code style="color:black; background-color:transparent">Bien o Servicio</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="Fuente_Rubro" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">Fuente_Rubro</code>
							</div>
						</div>
						<p></p>
						<div class="row" style="display:block">
							<div class="col-md-4">
                                <asp:DropDownList ID="FF_Desc" runat="server" class='form-control'>
                                    <asp:ListItem Value="DYT" Selected="true" >DYT</asp:ListItem>
                                    <asp:ListItem Value="RD" >RD</asp:ListItem>
                                    <asp:ListItem Value="RDR" >RDR</asp:ListItem>
                                    <asp:ListItem Value="RO" >RO</asp:ListItem>
                                </asp:DropDownList>
								<code style="color:black; background-color:transparent">FF_Desc</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="Meta" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">Meta</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="PROGRAMA_PRESUPUESTAL" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">PROGRAMA_PRESUPUESTAL</code>
							</div>
						</div>
						<p></p>						
						<div class="row" style="display:block">
							<div class="col-md-4">
								<asp:TextBox ID="RJ" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">RJ</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="Generica_de_Gasto" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">Generica_de_Gasto</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="Clasificador_de_Gasto" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">Clasificador_de_Gasto</code>
							</div>
						</div>
						<p></p>						
						<div class="row" style="display:block">
							<div class="col-md-4">
								<asp:TextBox ID="FECHA_VB" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">FECHA_VB</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="N_PEDIDO" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">N_PEDIDO</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="CCP_SIAF" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">CCP_SIAF</code>
							</div>
						</div>
						<p></p>						
						<div class="row" style="display:block">
							<div class="col-md-4">
								<asp:TextBox ID="Codigo_Item_N" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">Codigo_Item_N</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="Descripcion_del_Items" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">Descripcion_del_Items</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="Unidad_de_Medida" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">Unidad_de_Medida</code>
							</div>
						</div>
<script>
    function fMultiplica(Val1, Val2, Resp) {

    }
</script>
						<p></p>						
						<div class="row" style="display:block">
							<div class="col-md-4">
								<asp:TextBox ID="Precio_Unitario" runat="server" class='form-control' OnChange="fMultiplica(this,  document.getElementById('<%=Cantidad_Total_CMN_ACTUAL.ClientID%>').value, document.getElementById('<%=Valor_Total_CMN_ACTUAL.ClientID%>').value)">0</asp:TextBox>
								<code style="color:black; background-color:transparent">Precio_Unitario</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="Cantidad_Total_CMN_ACTUAL" runat="server" class='form-control' OnChange='fMultiplica(this,  document.getElementById(\"<%=Cantidad_Total_CMN_ACTUAL.ClientID%>\").value, document.getElementById(\"<%=Valor_Total_CMN_ACTUAL.ClientID%>\").value)'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">Cantidad_Total_CMN_ACTUAL</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="Valor_Total_CMN_ACTUAL" runat="server" class='form-control'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">Valor_Total_CMN_ACTUAL</code>
							</div>
						</div>
						<p></p>						
						<div class="row" style="display:block">
							<div class="col-md-3">
								<asp:TextBox ID="Cantidad_EXCLUSION" runat="server" class='form-control'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">Cantidad_EXCLUSION</code>
							</div>
							<div class="col-md-3">
								<asp:TextBox ID="Valor_EXCLUSION" runat="server" class='form-control'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">Valor_EXCLUSION</code>
							</div>
							<div class="col-md-3">
								<asp:TextBox ID="Cantidad_INCLUSION" runat="server" class='form-control'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">Cantidad_INCLUSION</code>
							</div>
                            <div class="col-md-3">
								<asp:TextBox ID="Valor_INCLUSION" runat="server" class='form-control'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">Valor_INCLUSION</code>
							</div>
						</div>
						<p></p>						
						<div class="row" style="display:block">
							<div class="col-md-4">
								<asp:TextBox ID="CANTIDAD_INCLUSION_A_TABLA" runat="server" class='form-control'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">CANTIDAD_INCLUSION_A_TABLA</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="IMPORTE_TABLA" runat="server" class='form-control'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">IMPORTE_TABLA</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="CANTIDAD_ENERO" runat="server" class='form-control'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">CANTIDAD_ENERO</code>
							</div>
						</div>
						<p></p>						
						<div class="row" style="display:block">
							<div class="col-md-4">
								<asp:TextBox ID="TOTAL_GASTO_ENERO" runat="server" class='form-control'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">TOTAL_GASTO_ENERO</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="CANTIDAD_FEBRERO" runat="server" class='form-control'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">CANTIDAD_FEBRERO</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="TOTAL_GASTO_FEBRERO" runat="server" class='form-control'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">TOTAL_GASTO_FEBRERO</code>
							</div>
						</div>
						<p></p>						
						<div class="row" style="display:block">
							<div class="col-md-4">
								<asp:TextBox ID="NroOC_OS_Feb" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">NroOC_OS_Feb</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="NroEXPEDIENTE_SIAF_Feb" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">NroEXPEDIENTE_SIAF_Feb</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="FECHA_DEVENGADA_Feb" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">FECHA_DEVENGADA_Feb</code>
							</div>
						</div>
						<p></p>						
						<div class="row" style="display:block">
							<div class="col-md-4">
								<asp:TextBox ID="CANTIDAD_MARZO" runat="server" class='form-control'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">CANTIDAD_MARZO</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="TOTAL_GASTO_MARZO" runat="server" class='form-control'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">TOTAL_GASTO_MARZO</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="NroOC_OS_Mar" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">NroOC_OS_Mar</code>
							</div>
						</div>
						<p></p>						
						<div class="row" style="display:block">
							<div class="col-md-4">
								<asp:TextBox ID="NroEXPEDIENTE_SIAF_Mar" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">NroEXPEDIENTE_SIAF_Mar</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="FECHA_DEVENGADA_Mar" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">FECHA_DEVENGADA_Mar</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="CANTIDAD_ABRIL" runat="server" class='form-control'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">CANTIDAD_ABRIL</code>
							</div>
						</div>
						<p></p>						
						<div class="row" style="display:block">
							<div class="col-md-4">
								<asp:TextBox ID="TOTAL_GASTO_ABRIL" runat="server" class='form-control'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">TOTAL_GASTO_ABRIL</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="NroOC_OS_Abr" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">NroOC_OS_Abr</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="NroEXPEDIENTE_SIAF_Abr" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">NroEXPEDIENTE_SIAF_Abr</code>
							</div>
						</div>
						<p></p>						
						<div class="row" style="display:block">
							<div class="col-md-4">
								<asp:TextBox ID="FECHA_DEVENGADA_Abr" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">FECHA_DEVENGADA_Abr</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="CANTIDAD_MAYO" runat="server" class='form-control'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">CANTIDAD_MAYO</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="TOTAL_GASTO_MAYO" runat="server" class='form-control'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">TOTAL_GASTO_MAYO</code>
							</div>
						</div>
						<p></p>						
						<div class="row" style="display:block">
							<div class="col-md-4">
								<asp:TextBox ID="NroOC_OS_May" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">NroOC_OS_May</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="NroEXPEDIENTE_SIAF_May" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">NroEXPEDIENTE_SIAF_May</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="FECHA_DEVENGADA_May" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">FECHA_DEVENGADA_May</code>
							</div>
						</div>
						<p></p>
						<div class="row" style="display:block">
							<div class="col-md-4">
								<asp:TextBox ID="CANTIDAD_JUNIO" runat="server" class='form-control'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">CANTIDAD_JUNIO</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="TOTAL_GASTO_JUNIO" runat="server" class='form-control'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">TOTAL_GASTO_JUNIO</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="NroOC_OS_Jun" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">NroOC_OS_Jun</code>
							</div>
						</div>
						<p></p>
						<div class="row" style="display:block">
							<div class="col-md-4">
								<asp:TextBox ID="NroEXPEDIENTE_SIAF_Jun" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">NroEXPEDIENTE_SIAF_Jun</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="FECHA_DEVENGADA_Jun" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">FECHA_DEVENGADA_Jun</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="CANTIDAD_JULIO" runat="server" class='form-control'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">CANTIDAD_JULIO</code>
							</div>
						</div>
						<p></p>
						<div class="row" style="display:block">
							<div class="col-md-4">
								<asp:TextBox ID="TOTAL_GASTO_JULIO" runat="server" class='form-control'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">TOTAL_GASTO_JULIO</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="NroOC_OS_Jul" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">NroOC_OS_Jul</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="NroEXPEDIENTE_SIAF_Jul" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">NroEXPEDIENTE_SIAF_Jul</code>
							</div>
						</div>
						<p></p>
						<div class="row" style="display:block">
							<div class="col-md-4">
								<asp:TextBox ID="FECHA_DEVENGADA_Jul" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">FECHA_DEVENGADA_Jul</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="CANTIDAD_AGOSTO" runat="server" class='form-control'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">CANTIDAD_AGOSTO</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="TOTAL_GASTO_AGOSTO" runat="server" class='form-control'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">TOTAL_GASTO_AGOSTO</code>
							</div>
						</div>
						<p></p>
						<div class="row" style="display:block">
							<div class="col-md-4">
								<asp:TextBox ID="NroOC_OS_Ago" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">NroOC_OS_Ago</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="NroEXPEDIENTE_SIAF_Ago" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">NroEXPEDIENTE_SIAF_Ago</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="FECHA_DEVENGADA_Ago" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">FECHA_DEVENGADA_Ago</code>
							</div>
						</div>
						<p></p>
						<div class="row" style="display:block">
							<div class="col-md-4">
								<asp:TextBox ID="CANTIDAD_SETIEMBRE" runat="server" class='form-control'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">CANTIDAD_SETIEMBRE</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="TOTAL_GASTO_SETIEMBRE" runat="server" class='form-control'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">TOTAL_GASTO_SETIEMBRE</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="NroOC_OS_Set" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">NroOC_OS_Set</code>
							</div>
						</div>
						<p></p>
						<div class="row" style="display:block">
							<div class="col-md-4">
								<asp:TextBox ID="NroEXPEDIENTE_SIAF_Set" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">NroEXPEDIENTE_SIAF_Set</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="FECHA_DEVENGADA_Set" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">FECHA_DEVENGADA_Set</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="CANTIDAD_OCTUBRE" runat="server" class='form-control'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">CANTIDAD_OCTUBRE</code>
							</div>
						</div>
						<p></p>
						<div class="row" style="display:block">
							<div class="col-md-4">
								<asp:TextBox ID="TOTAL_GASTO_OCTUBRE" runat="server" class='form-control'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">TOTAL_GASTO_OCTUBRE</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="NroOC_OS_Oct" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">NroOC_OS_Oct</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="NroEXPEDIENTE_SIAF_Oct" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">NroEXPEDIENTE_SIAF_Oct</code>
							</div>
						</div>
						<p></p>
						<div class="row" style="display:block">
							<div class="col-md-4">
								<asp:TextBox ID="FECHA_DEVENGADA_Oct" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">FECHA_DEVENGADA_Oct</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="CANTIDAD_NOVIEMBRE" runat="server" class='form-control'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">CANTIDAD_NOVIEMBRE</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="TOTAL_GASTO_NOVIEMBRE" runat="server" class='form-control'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">TOTAL_GASTO_NOVIEMBRE</code>
							</div>
						</div>
						<p></p>
						<div class="row" style="display:block">
							<div class="col-md-4">
								<asp:TextBox ID="NroOC_OS_Nov" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">NroOC_OS_Nov</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="NroEXPEDIENTE_SIAF_Nov" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">NroEXPEDIENTE_SIAF_Nov</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="FECHA_DEVENGADA_Nov" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">FECHA_DEVENGADA_Nov</code>
							</div>
						</div>
						<p></p>
						<div class="row" style="display:block">
							<div class="col-md-4">
								<asp:TextBox ID="CANTIDAD_DICIEMBRE" runat="server" class='form-control'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">CANTIDAD_DICIEMBRE</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="TOTAL_GASTO_DICIEMBRE" runat="server" class='form-control'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">TOTAL_GASTO_DICIEMBRE</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="NroOC_OS_Dic" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">NroOC_OS_Dic</code>
							</div>
						</div>
						<p></p>
						<div class="row" style="display:block">
							<div class="col-md-4">
								<asp:TextBox ID="NroEXPEDIENTE_SIAF_Dic" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">NroEXPEDIENTE_SIAF_Dic</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="FECHA_DEVENGADA_Dic" runat="server" class='form-control'></asp:TextBox>
								<code style="color:black; background-color:transparent">FECHA_DEVENGADA_Dic</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="CANTIDAD_ANIO" runat="server" class='form-control'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">CANTIDAD_ANIO</code>
							</div>
						</div>
						<p></p>
						<div class="row" style="display:block">
							<div class="col-md-4">
								<asp:TextBox ID="TOTAL_GASTO_ANIO" runat="server" class='form-control'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">TOTAL_GASTO_ANIO</code>
							</div>
							<div class="col-md-4">
								<asp:TextBox ID="TOT_GASTO_IMPORTE_VALIDA" runat="server" class='form-control'>0</asp:TextBox>
								<code style="color:black; background-color:transparent">TOT_GASTO_IMPORTE_VALIDA</code>
							</div>
						</div>
						<p></p>
						

					</div>
				</div>
				
			</div>
        




            <!--SEGUNDA FILA-->             
			 <div class="row" style="display:block;">
				<div class="column">
					<div class="col-md-12 hide" style="padding:  0px 30px 10px 30px; background-color:#F5B7B1">
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

					</div>
				</div>

			</div>


            <!-- TERCERA FILA -->
            

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

      </div>
      <!-- /.content-wrapper -->
   </form>
    <script>
        //document.getElementById('LiCIEIm').className = "treeview menu-open";
        //$('#LiCIEIm').addClass('menu-open');
        document.getElementById("LiOpticaI").classList.add('menu-open');
        document.getElementById('UlOptica').style.display = 'block';
        document.getElementById("ulOpticaDon").classList.add('active');
        document.getElementById("ulLiOptDonRegist").classList.add('active');
        var f = new Date();

    </script>
</asp:Content>


