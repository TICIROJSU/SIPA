<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="IROTWeb_wPac_SesionA_Default" %>

<!DOCTYPE html>

<html lang="es">
  <head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>IRO - SIPA Web</title>
    <link rel="stylesheet" href="../Estilos/plus-admin-free/src/assets/vendors/mdi/css/materialdesignicons.min.css">
    <link rel="stylesheet" href="../Estilos/plus-admin-free/src/assets/vendors/ti-icons/css/themify-icons.css">
    <link rel="stylesheet" href="../Estilos/plus-admin-free/src/assets/vendors/css/vendor.bundle.base.css">
    <link rel="stylesheet" href="../Estilos/plus-admin-free/src/assets/vendors/font-awesome/css/font-awesome.min.css">
    <link rel="stylesheet" href="../Estilos/plus-admin-free/src/assets/css/style.css">
    <link rel="shortcut icon" href="../Estilos/Imagenes/favicon.png" />
  </head>
  <body>
   <form id="form1" runat="server">
    <div class="container-scroller">
      <div class="container-fluid page-body-wrapper full-page-wrapper">
        <div class="content-wrapper d-flex align-items-center auth">
          <div class="row flex-grow">
            <div class="col-lg-4 mx-auto">
              <div class="auth-form-light text-left p-5">
                <div class="brand-logo" style="text-align:center">
                    <img src="../Estilos/Imagenes/Logo.png" />
                </div>
                <h4>Hola, bienvenido a esta Aplicacion</h4>
                <h6 class="fw-light">Inicia Sesion para continuar.</h6>
                  <div class="form-group">
                    <input type="text" class="form-control form-control-lg" id="PacDNI" placeholder="DNI">
                  </div>
                  <div class="form-group">
                    <input type="text" class="form-control form-control-lg" id="PacHHCC" placeholder="Historia Clinica">
                  </div>
                  <div class="mt-3 d-grid gap-2">
                    <asp:Button ID="btnIniSesion" runat="server" Text="Iniciar Sesion" OnClick="btnIniSesion_Click" class="btn btn-block btn-primary btn-lg fw-semibold auth-form-btn" />
                  </div>
                  <div class="my-2 d-flex justify-content-between align-items-center">
                    <a href="#" class="auth-link text-black">¿Olvidó su contraseña?</a>
                  </div>
                  
              </div>
            </div>
          </div>
        </div>
        <!-- content-wrapper ends -->
      </div>
      <!-- page-body-wrapper ends -->
    </div>
   </form>
    <script src="../Estilos/plus-admin-free/src/assets/vendors/js/vendor.bundle.base.js"></script>
    <script src="../Estilos/plus-admin-free/src/assets/js/off-canvas.js"></script>
    <script src="../Estilos/plus-admin-free/src/assets/js/misc.js"></script>
    <script src="../Estilos/plus-admin-free/src/assets/js/settings.js"></script>
    <script src="../Estilos/plus-admin-free/src/assets/js/todolist.js"></script>
    <script src="../Estilos/plus-admin-free/src/assets/js/hoverable-collapse.js"></script>
  </body>
</html>
