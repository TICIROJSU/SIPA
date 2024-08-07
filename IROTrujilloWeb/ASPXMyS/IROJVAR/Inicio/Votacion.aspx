<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Votacion.aspx.cs" Inherits="ASPXMyS_IROJVAR_Inicio_Votacion" %>

<!DOCTYPE html>
<html lang="en">

<!-- register24:03-->
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0">
    <link rel="shortcut icon" type="image/x-icon" href="../Estilos/Imagenes/favicon.ico">
    <title>IRO-JSU Formulario Elección Virtual de Representantes </title>

    <link rel="stylesheet" type="text/css" href="../Estilos/Preclinic/assets/css/bootstrap.min.css">
    <link rel="stylesheet" type="text/css" href="../Estilos/Preclinic/assets/css/font-awesome.min.css">
    <link rel="stylesheet" type="text/css" href="../Estilos/Preclinic/assets/css/dataTables.bootstrap4.min.css">
    <link rel="stylesheet" type="text/css" href="../Estilos/Preclinic/assets/css/select2.min.css">
    <link rel="stylesheet" type="text/css" href="../Estilos/Preclinic/assets/css/bootstrap-datetimepicker.min.css">
    <link rel="stylesheet" type="text/css" href="../Estilos/Preclinic/assets/css/style.css">

    <!--[if lt IE 9]>
		<script src="assets/js/html5shiv.min.js"></script>
		<script src="assets/js/respond.min.js"></script>
	<![endif]-->
    <script>
		function btnBuscaDNI(DNI) {
			var params = new Object();
            params.DNI = DNI;
			params = JSON.stringify(params);

			$.ajax({
                type: "POST", url: "Votacion.aspx/GetBtnDNI", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#dniElector").html(result.d); }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#dniElector").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});
		}
    </script>
</head>

<body>
    <div class="main-wrapper  account-wrapper">
        <div class="account-page">
            <div class="account-center">
                <div class="account-box">
                    <form class="form-signin" runat="server">
						<div class="account-logo">
                            <a href="#"><img src="../Estilos/Imagenes/favicon.png" alt=""></a>
                        </div>
                        <div class="form-group">
                            <h4 class="payslip-title">
                                <strong>
                                    Proceso De Elección Complementaria De Los Representantes Suplentes De Los Trabajadores Ante El Comité De Seguridad Y Salud En El Trabajo <br /> "IRO JSU" <br /> Periodo 2023-2025
                                </strong>
                            </h4>
                            <br />
                            Proporcione sus Datos personales para iniciar su Votacion.
                        </div>
                        <div style="text-align:center; color:red"><asp:Label ID="LblMensaje" runat="server" Text=""></asp:Label></div>
                        <div class="form-group">
                            <label>DNI</label>
                            <asp:TextBox ID="txtDNI" runat="server" class="form-control" onBlur="btnBuscaDNI(this.value)" placeholder="DNI" required="true" autofocus="autofocus" ></asp:TextBox>
                            <code id="dniElector">&nbsp</code>
                        </div>
                        <div class="form-group">
                            <label>Fecha de Nacimiento</label>
                            <asp:TextBox ID="txtFNac" Type="date" runat="server" class="form-control" placeholder="Fecha de Nacimiento" required="true"></asp:TextBox>
                        </div>
                        <%--<input type="text" class="form-control datetimepicker" />--%>
                        <div class="form-group">
                            <label>Genero</label>
                            <asp:DropDownList ID="ddlGenero" class="form-control" runat="server">
                                <asp:ListItem Value="M">Masculino</asp:ListItem>
                                <asp:ListItem Value="F">Femenino</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        
                        <div class="form-group text-center">
                            <asp:Button ID="btnEntrar" runat="server" Text="Entrar [Iniciar]" class="btn btn-primary btn-block btn-flat" OnClick="btnEntrar_Click" />
                        </div>
                        
                    </form>
                </div>
            </div>
        </div>
    </div>
    
    <script src="../Estilos/Preclinic/assets/js/jquery-3.2.1.min.js"></script>
	<script src="../Estilos/Preclinic/assets/js/popper.min.js"></script>
    <script src="../Estilos/Preclinic/assets/js/bootstrap.min.js"></script>
    <script src="../Estilos/Preclinic/assets/js/jquery.dataTables.min.js"></script>
    <script src="../Estilos/Preclinic/assets/js/dataTables.bootstrap4.min.js"></script>
    <script src="../Estilos/Preclinic/assets/js/jquery.slimscroll.js"></script>
    <script src="../Estilos/Preclinic/assets/js/select2.min.js"></script>
    <script src="../Estilos/Preclinic/assets/js/moment.min.js"></script>
    <script src="../Estilos/Preclinic/assets/js/bootstrap-datetimepicker.min.js"></script>
    <script src="../Estilos/Preclinic/assets/js/app.js"></script>

</body>

<!-- register24:03-->
</html>
