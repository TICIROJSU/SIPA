<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ASPX_IROJVAR_Sorteo202312_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>IRO - Sorteo</title>
	<link rel="stylesheet" href="styleimg.css">
    <link rel="shortcut icon" href="../../Estilos/imagenes/favicon.png" type="image/x-icon" /><meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />

    <script type="text/javascript" src="../../ASPX/Estilos/Funciones/json2.js"></script>

    <script language="javascript" type="text/javascript">
		var pasoLibre = "Si";
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
			if (pasoLibre != "Si") {
				return;
			}
			pasoLibre = "No";
			document.getElementById("imagenTrofeo").src="SeleccionandoGanador.gif";
			setTimeout(fShowSorteados2, 5000);

        }		
		
		function fShowSorteados2() {
			//alert("hola");
			document.getElementById("imagenTrofeo").src="ganadores-png.png";

			document.getElementById("<%=lblCant.ClientID%>").innerText = parseInt(document.getElementById("<%=lblCant.ClientID%>").innerText) + 1;
			var params = new Object();
			params.IntentosCant = parseInt(document.getElementById("<%=lblCant.ClientID%>").innerText);
			params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "Default.aspx/GetSorteado", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
				success: function (result) {
					var aResult = result.d.split("||sep||");
					document.getElementById("tblSorteos").insertRow(1).innerHTML = aResult[0];
					document.getElementById("<%=lblGanador.ClientID%>").innerText = aResult[1];

				}, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divError").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});
			pasoLibre = "Si";
        }


    </script>


</head>
<body>
    <form id="form1" runat="server">
	<div class="portada">
		<div id="desarrollofrontend">
		   <!--a href="#" target="blank">link sorteo</a-->
		   <h1>Bienvenidos al sorteo de Navidad</h1>
		</div>
		
		<div>		  
				<div>
					<img src="boton-jugar.png" onclick="fShowSorteados()" />
				</div>
                <div style="position: absolute; top: 85px; left: 1555px;" >
                    <a href="ListaParticipantes.aspx" target="_blank">
                        <img src="BotonListado.png" />
                    </a>
                </div>
		</div>  

        <br />
		<div>
			  <img id="imagenTrofeo" src="ganadores-png.png">
		</div>	
		<br />

		<div class="text" style="color:yellow">
			<h1><asp:Label ID="lblGanador" runat="server" Text="0"></asp:Label></h1> 
		</div>
		<br />

		<div class="text">
		  <h2>Lista de Ganadores (<asp:Label ID="lblCant" runat="server" Text="0"></asp:Label>)</h2> 
			<h3>
				<table id="tblSorteos" >
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
			<asp:GridView ID="gvGanadores" runat="server" ShowHeader="False"></asp:GridView>
		  </h3>
		</div> 	

	</div>
		<div id="divError"></div>
		<asp:Literal ID="LitError" runat="server"></asp:Literal>
    </form>

      <script src="../../Estilos/bower_components/jquery/dist/jquery.min.js"></script>
      <script src="../../Estilos/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
    <script language="javascript" type="text/javascript">

		function fCargaGanadores() {
			document.getElementById("<%=lblCant.ClientID%>").innerText = parseInt(document.getElementById("<%=lblCant.ClientID%>").innerText) + 1;
			var params = new Object();
			params.IntentosCant = parseInt(document.getElementById("<%=lblCant.ClientID%>").innerText);
			params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "Default.aspx/GetGanadores", data: params, 
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

</body>
</html>
