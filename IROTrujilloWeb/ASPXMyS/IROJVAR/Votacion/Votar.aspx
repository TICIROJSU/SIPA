<%@ Page Language="C#" MasterPageFile="../../MPVotacion.master" AutoEventWireup="true" CodeFile="Votar.aspx.cs" Inherits="ASPXMyS_IROJVAR_Votacion_Votar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPhead1" Runat="Server">
    <title>IRO - Votacion</title>
    <script type="text/javascript" src="<%=ResolveUrl("../Estilos/Funciones/json2.js")%>"></script>

    <script>
		function btnRegVoto(Voto, idElec) {
            
            if (document.getElementById("vot1").value == "1") {
                alert("Usted ya realizo un voto,\nPruebe visualizar los resultados de las Elecciones");
                return true;
            }

			var params = new Object();
            params.Voto = Voto;
            params.idElector = <%=Session["eID"].ToString()%>;
			params = JSON.stringify(params);

			$.ajax({
                type: "POST", url: "Votar.aspx/SetBtnRegVoto", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) {
                    alert(result.d);
                    $("#divmsj1").html(result.d);
                    location = "../Inicio/Votacion.aspx"; 
                }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divmsj1").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});
        }

        function fmsjRangoFec(fIni, fFin, fAct, msj) {
            window.alert(msj + "\nEl Horario de Elecciones es: " + 
                " \nFecha Inicio:-" + fIni +
                " \nFecha Fin:----" + fFin +
                " \nFecha Actual:-" + fAct +
                "\n -------------------------------------");
            
            if ("<%=Session["varTipoUser"].ToString()%>" == "Admin")        { location = "../Votacion/Tablero1.aspx"; }
            else if (msj == "Espere a que el Horario de Votacion Inicie.")  { location = "../Inicio/Votacion.aspx"; }
            else if (msj == "El Horario de Votacion ha Finalizado.")        { location = "../Votacion/Tablero1.aspx"; }

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPbodyContent1" Runat="Server">
   <form id="form1" runat="server">

        <div class="content">
            <div class="row">
                <div class="col-sm-4 col-3"><h4 class="page-title">Listas</h4></div>
            </div>
			<div class="row doctor-grid" id="gridCandidatos" name="gridCandidatos">
                <input type="hidden" id="vot1" name="vot1" value="<%=Session["eVOTO"].ToString()%>" />
                <div class="col-lg-6">
                    <div class="profile-widget">
                        <div class="doctor-img">
                            <a class="avatar" href="#"><img alt="" src="../Estilos/Preclinic/assets/img/user.jpg"></a>
                        </div>
                        <h4 class="doctor-name text-ellipsis">JACINTO ARMAS, INÉS VIOLETA</h4>
                        <h4 class="doctor-name text-ellipsis">CAMPOS ZÁRATE, SHIRLEY LIZBETH</h4>
                        <h4 class="doctor-name text-ellipsis">SOTERO MENDOZA DE HUAMÁN, MIXI YASMIN</h4>
                        <div class="doc-prof">Lista Única</div>
                        <div class="user-country">
                            <i class="fa fa-map-marker"></i> Elige la opcion de tu preferencia:
                        </div> 
						<div class="row">
							<div class="col-4">
								<a href="#" onclick="btnRegVoto('SI')" class="btn btn-success btn-block btn-flat" >[ SI ]</a>
							</div>
							<div class="col-4">
								<a href="#" onclick="btnRegVoto('NO')" class="btn btn-primary btn-block btn-flat" >[ NO ]</a>
							</div>
							<div class="col-4">
								<a href="#" onclick="btnRegVoto('Ninguno')" class="btn btn-warning btn-block btn-flat">[ NINGUNO ]</a>
							</div>
						</div>
                    </div>
                </div>

            </div>
        </div>

        <div id="divmsj1"></div>
   </form>
</asp:Content>
