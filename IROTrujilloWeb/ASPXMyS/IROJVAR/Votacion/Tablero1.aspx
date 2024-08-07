<%@ Page Language="C#" MasterPageFile="../../MPVotacion.master" AutoEventWireup="true" CodeFile="Tablero1.aspx.cs" Inherits="ASPXMyS_IROJVAR_Votacion_Tablero1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPhead1" Runat="Server">
   <title>IRO - Tablero de Votacion</title>
    <script type="text/javascript" src="<%=ResolveUrl("../Estilos/Funciones/json2.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("../Estilos/Funciones/funciones.js")%>"></script>

    <script>
        var extVotoR = "";
        
		function fGetListElectrores(VotoR) {
			var params = new Object();
            params.vVoto = VotoR;
			params = JSON.stringify(params);

			$.ajax({
                type: "POST", url: "Tablero1.aspx/GetListaElectores", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#tbody1").html(result.d); }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#tbody1").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });
            
            extVotoR = VotoR;
            const TimeoutID1 = setTimeout(cConteoElect, 300);
            const TimeoutID2 = setTimeout(cConteoElect, 2500);
        }

        function cConteoElect()
		{
            var btnClass = "btn-primary btn-primary-two";
            if (extVotoR == "ConVoto") {btnClass = "btn-success btn-primary-three";}
            if (extVotoR == "SinVoto") {btnClass = "btn-warning btn-primary-four"; }
            //document.getElementById("ContListElec").innerhtml = "<button class='btn " + btnClass + "'>" + document.getElementById("txtContListElec").value + "</button>";
            $("#ContListElec").html("<span class='btn " + btnClass + "'>" + document.getElementById("txtContListElec").value + "</span>");
		}

		function fSetBtnTipoUser(id) {
			var params = new Object();
            params.id = id;
			params = JSON.stringify(params);

			$.ajax({
                type: "POST", url: "Tablero1.aspx/SetBtnTipoUser", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) {
                    //$("#tbody1").html(result.d);
                    alert(result.d);
                    location.reload();
                }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    //$("#tbody1").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });
            
        }

    </script>

    <link rel="stylesheet" type="text/css" href="../Estilos/Preclinic/assets/css/dataTables.bootstrap4.min.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPbodyContent1" Runat="Server">

   <form id="form1" runat="server">

        <div class="content">
            <div class="row"> <br /> </div>
            <div class="row">
                <div class="col-sm-3">
                    <h4 class="page-title">Tablero de Votos</h4>
                </div>

<%    if (Session["varTipoUser"].ToString() == "Admin")    {    %>
                <div class="col-sm-9">
                    <div class="row">
                        <div class="col-sm-2" id="ContListElec"></div>
                        <div class="col-sm-3">
                            <a href="#" class="btn btn-primary btn-rounded" onclick="fGetListElectrores('Total')"><i class="fa fa-hospital-o"></i> Relacion Total de Sufragantes</a>
                        </div>
                        <div class="col-sm-3">
                            <a href="#" class="btn btn-success btn-rounded" onclick="fGetListElectrores('ConVoto')"><i class="fa fa-calendar-check-o"></i> Relacion Sufragantes Con Voto</a>
                        </div>
                        <div class="col-sm-3">
                            <a href="#" class="btn btn-warning btn-rounded" onclick="fGetListElectrores('SinVoto')"><i class="fa fa-calendar-times-o"></i> Relacion Sufragantes Sin Voto</a>
                        </div>
                    </div>
                </div>
<%    } %>
                
            </div>
            <div class="row">
                <div class="col-md-3">
                    <div class="table-responsive">
                        <table class="table table-striped custom-table text-center">
                            <thead>
                                <tr>
                                    <th>Opcion a Elegir</th>
                                    <th>Numero de Votos</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td><a href="#" class="btn btn-success">SI</a></td>
                                    <td><asp:Label ID="lblSI" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><a href="#" class="btn btn-warning">NO</a></td>
                                    <td><asp:Label ID="lblNO" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><span class="custom-badge status-grey">En Blanco</span></td>
                                    <td><asp:Label ID="lblBlank" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><span class="custom-badge status-purple">Total</span></td>
                                    <td><asp:Label ID="lblTOT" runat="server"></asp:Label></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>

<%    if (Session["varTipoUser"].ToString() == "Admin")    {    %>
                <div class="col-md-9">
            <div class="content">
				<div class="row">
					<div class="col-md-12">
						<div class="table-responsive">
							<table class="table table-border table-striped custom-table datatable mb-0" id="tblElectores">
								<thead>
									<tr>
										<th>
                                            Apellidos y Nombres <br />
                                            <input type="text" class="form-control" id="bscprod2" placeholder="Buscar" onkeyup="fBscTblHTML('bscprod2', 'tblElectores', 0)" autofocus="autofocus">
										</th>
										<th>DNI</th>
										<th>Fecha de <br />Nacimiento</th>
										<th>Fecha de <br />Ingreso</th>
										<th>Regimen Laboral</th>
										<th>Condicion Laboral</th>
										<th> 
                                            <a href="#" class="dash-widget-bg4" onclick="exportTableToExcel('tblElectores')">
                                                <i class="fa fa-file-excel-o" ></i>
                                            </a> 
										</th>
									</tr>
								</thead>
								<tbody id="tbody1">
									<tr>
										<td> <a class="avatar" href="#"><i class="fa fa-user-md"></i></a>  </td>
                                        <td></td><td></td><td></td><td></td><td></td>
									</tr>
								</tbody>
							</table>
						</div>
					</div>
                </div>
            </div>
                </div>
<%    } %>

            </div>
        </div>

   </form>


</asp:Content>
