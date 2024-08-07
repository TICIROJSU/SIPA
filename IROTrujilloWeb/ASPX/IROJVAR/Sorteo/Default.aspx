<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ASPX_IROJVAR_Sorteo_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <!-- Bootstrap 3.3.7 -->
    <link rel="stylesheet" href="../../Estilos/bower_components/bootstrap/dist/css/bootstrap.min.css" type="text/css"/>
    <!-- Font Awesome -->
    <link rel="stylesheet" href="../../Estilos/bower_components/font-awesome/css/font-awesome.min.css" type="text/css"/>
    <!-- Ionicons -->
    <link rel="stylesheet" href="../../Estilos/bower_components/Ionicons/css/ionicons.min.css" type="text/css"/>
    <!-- Theme style -->
    <link rel="stylesheet" href="../../Estilos/dist/css/AdminLTE.min.css" type="text/css"/>
    <!-- iCheck -->
    <link rel="stylesheet" href="../../Estilos/plugins/iCheck/square/blue.css" type="text/css"/>
    <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
    <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <script type="text/javascript" src="../../ASPX/Estilos/Funciones/json2.js"></script>


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

		function fShowSorteados() {
			if (parseInt(document.getElementById("hIntentoNro").value) == parseInt(document.getElementById("<%=ddlCantIntentos.ClientID%>").value)) {
				alert("Numero de Intentos Utilizados, Crear un Sorteo Nuevo");
				return;
			}
            document.getElementById("hIntentoNro").value = parseInt(document.getElementById("hIntentoNro").value) + 1; 
			document.getElementById("<%=lblIntentoNro.ClientID%>").innerText = document.getElementById("hIntentoNro").value;
            var params = new Object();
            params.IntentosCant = document.getElementById("<%=ddlCantIntentos.ClientID%>").value; 
            params.IntentoNro = document.getElementById("hIntentoNro").value; 
            params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "Default.aspx/GetSorteado", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
				success: function (result) {
					document.getElementById("tblSorteos").insertRow(1).innerHTML = result.d;
				}, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divError").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});



        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
			Sorteo
        </div>
		<div class="row">
			<!-- Columna 1 --> 
			<div class="column">
				<div class="col-md-1"></div>
			</div>
			<div class="column">
				<div class="col-md-6">
					<div class="row">
						<div class="col-xs-2">
							Intentos: 
						</div>
						<div class="col-xs-4">
							<asp:dropdownlist ID="ddlCantIntentos" runat="server" class='form-control'>
								<asp:ListItem Value="1" Selected="true">Al 1er Intento</asp:ListItem>
								<asp:ListItem Value="3">Al 3er Intento</asp:ListItem>
								<asp:ListItem Value="5">Al 5to Intento</asp:ListItem>
								<asp:ListItem Value="15">Al 5to Intento</asp:ListItem>
							</asp:dropdownlist>
						</div>
					</div>

					<br />
					<div class="row">
						<div class="col-xs-4">
							<div class="btn btn-success" onclick="fShowSorteados()">Sortear</div>
							<a href="http://190.116.184.171/ASPX/IROJVAR/Sorteo/" class="btn btn-info">Nuevo Sorteo</a>
						</div>
					</div>
					<br />
					<div class="row">
						<div class="col-xs-2">
							Intento Nro. 
						</div>
						<div class="col-xs-2">
							<asp:Label ID="lblIntentoNro" runat="server" Text="0"></asp:Label>
							<input type="hidden" id="hIntentoNro" value="0" />
						</div>

					</div>					
					
					<div class="row">
						<div class="col-xs-12">

<script>
	function agregarFila(Nro, Personal, Mensaje){
		document.getElementById("tblSorteos").insertRow(-1).innerHTML = '<td>' + Nro + '</td><td>' + Personal + '</td><td>' + Mensaje + '</td>';
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
							<table id="tblSorteos" class="table table-hover" >
								<thead>
									<tr>
										<th>Nro</th>
										<th>Personal</th>
										<th>Mensaje</th>
									</tr>
								</thead>
								<tbody>

								</tbody>
							</table>
							</div>

						</div>
					</div>




				</div>
			</div>

			<!-- Columna 2 --> 
			<div class="column">
				<div class="col-md-5">
					<div class="row">
						<div class="col-xs-10">
							
						</div>
						<div class="col-xs-2">
							<div class="btn btn-success" onclick="fShowSorteados()">Sortear</div>
						</div>
					</div>
					<div class="row">
						<div class="col-xs-12">
							Ganadores
						</div>
					</div>
					<div class="row">
						<div class="col-xs-12">
							<div class="box box-body table-responsive no-padding" style='color:#000000; '>
								<asp:GridView ID="GVtable" runat="server" class="table table-condensed table-bordered"></asp:GridView>
							</div>							
						</div>
					</div>
					<div class="row">
						<div class="col-xs-12">
							<asp:TextBox ID="TextBox1" runat="server" class="form-control"></asp:TextBox>
						</div>
					</div>

					<div class="row">
						<div class="col-xs-12">
							<asp:Literal ID="LitError" runat="server"></asp:Literal>
							<div id="divError"></div>
						</div>
					</div>
					

				</div>
			</div>


			<!-- /.col -->
		</div> 

		

    </form>

      <script src="../../Estilos/bower_components/jquery/dist/jquery.min.js"></script>
      <!-- Bootstrap 3.3.7 -->
      <script src="../../Estilos/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
      <!-- iCheck -->
      <script src="../../Estilos/plugins/iCheck/icheck.min.js"></script>

</body>
</html>
