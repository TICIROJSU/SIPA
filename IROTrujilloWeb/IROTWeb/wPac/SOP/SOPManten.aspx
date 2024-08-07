<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SOPManten.aspx.cs" Inherits="IROTWeb_wPac_SOP_SOPManten" %>

<!DOCTYPE html>
<html translate="no">
  <head>
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>IRO - SIPA Web</title>
    <!-- plugins:css -->
    <link rel="stylesheet" href="../Estilos/plus-admin-free/src/assets/vendors/mdi/css/materialdesignicons.min.css">
    <link rel="stylesheet" href="../Estilos/plus-admin-free/src/assets/vendors/ti-icons/css/themify-icons.css">
    <link rel="stylesheet" href="../Estilos/plus-admin-free/src/assets/vendors/css/vendor.bundle.base.css">
    <link rel="stylesheet" href="../Estilos/plus-admin-free/src/assets/vendors/font-awesome/css/font-awesome.min.css">
    <!-- endinject -->
    <!-- Plugin css for this page -->
    <link rel="stylesheet" href="../Estilos/plus-admin-free/src/assets/vendors/select2/select2.min.css">
    <link rel="stylesheet" href="../Estilos/plus-admin-free/src/assets/vendors/select2-bootstrap-theme/select2-bootstrap.min.css">
    <!-- End plugin css for this page -->
    <!-- inject:css -->
    <!-- endinject -->
    <!-- Layout styles -->
    <link rel="stylesheet" href="../Estilos/plus-admin-free/src/assets/css/style.css">
    <!-- End layout styles -->
    <link rel="shortcut icon" href="../Estilos/Imagenes/favicon.png" />
      <script src="../Estilos/fnGen.js"></script>

  </head>
  <body>
    <div class="container-scroller">
      <!-- partial:../../partials/_sidebar.html -->
      <nav class="sidebar sidebar-offcanvas" id="sidebar">

          <script src="../menuvert.js" charset="ISO-8859-1" ></script>

      </nav>
      <!-- partial -->
      <div class="container-fluid page-body-wrapper">
        <!-- partial:../../partials/_navbar.html -->
        <nav class="navbar default-layout-navbar col-md-lg-12 col-md-12 p-0 fixed-top d-flex flex-row">
          
            <script src="../menuhori.js" charset="ISO-8859-1" ></script>

        </nav>
        <!-- partial -->
        <div class="main-panel">
          <div class="content-wrapper">
          <form method="post" id="form1" autocomplete="off">  
            <div class="row">
              <div class="col-md-12 grid-margin stretch-card">
                <div class="card">
                  <div class="card-body">
                      <div class="form-group">
                          <div class="row">
                              <div class="col-md-12">
                                  <img src="../Estilos/Imagenes/Adic/MaestrosPrincipal.png" />
                              </div>
                          </div>

                      </div>

                  </div>
                </div>
              </div>
            </div>


          </form>
          </div>
          <!-- content-wrapper ends -->
          <!-- partial:../../partials/_footer.html -->
          <footer class="footer" style="display:none">
            <div class="d-sm-flex justify-content-center justify-content-sm-between">
              <span class="text-muted text-center text-sm-left d-block d-sm-inline-block">IRO-JSU © 2024 <a href="https://www.bootstrapdash.com/" target="_blank">BootstrapDash</a>. All rights reserved.</span>
              <span class="float-none float-sm-end d-block mt-1 mt-sm-0 text-center">Hand-crafted & made with <i class="mdi mdi-heart text-danger"></i></span>
            </div>
          </footer>
          <!-- partial -->
        </div>
        <!-- main-panel ends -->
      </div>
      <!-- page-body-wrapper ends -->
    </div>

         <!-- modal-sm | small || modal-lg | largo || modal-xl | extra largo -->
    <div class="modal fade" id="modalexample" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog modal-xl" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">Modal title</h5>
            <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">×</span>
            </button>
          </div>
          <div class="modal-body">
            <p>This is a modal with default size</p>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            <button type="button" class="btn btn-primary">Save changes</button>
          </div>
        </div>
      </div>
    </div>

    <div class="modal fade" id="modalShowNIng" tabindex="-1" role="dialog" aria-labelledby="mdlabel" aria-hidden="true">
        <div class="modal-dialog modal-xl" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <div class="modal-title" id="mdlabel">
                        Modal title2

                    </div>
                    <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p>This is a modal with default size</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary">Save changes</button>
                </div>
            </div>
        </div>
    </div>


    <div style="display:none">
        <button type="button" id="btnModConsulta" data-bs-toggle="modal" data-bs-target="#modalShowNIng" >...</button>


    </div>

      <script>
          function fnGuardar() {

          }

          function fnConsultar() {
              document.getElementById("btnModConsulta").click();
              const myTimeout = setTimeout(fnFocusModtxt1, 500);
          }
          function fnFocusModtxt1() {
                //document.getElementById("txtConsDNI2").focus();
          }

          function fnEditar() {

          }

          function fnAnular() {

          }

          function fnImprimir() {

          }

      </script>

    <!-- container-scroller -->
    <!-- plugins:js -->
    <script src="../Estilos/plus-admin-free/src/assets/vendors/js/vendor.bundle.base.js"></script>
    <!-- endinject -->
    <!-- Plugin js for this page -->
    <script src="../Estilos/plus-admin-free/src/assets/vendors/select2/select2.min.js"></script>
    <script src="../Estilos/plus-admin-free/src/assets/vendors/typeahead.js/typeahead.bundle.min.js"></script>
    <!-- End plugin js for this page -->
    <!-- inject:js -->
    <script src="../Estilos/plus-admin-free/src/assets/js/off-canvas.js"></script>
    <script src="../Estilos/plus-admin-free/src/assets/js/misc.js"></script>
    <script src="../Estilos/plus-admin-free/src/assets/js/settings.js"></script>
    <script src="../Estilos/plus-admin-free/src/assets/js/todolist.js"></script>
    <script src="../Estilos/plus-admin-free/src/assets/js/hoverable-collapse.js"></script>
    <!-- endinject -->
    <!-- Custom js for this page -->
    <script src="../Estilos/plus-admin-free/src/assets/js/file-upload.js"></script>
    <script src="../Estilos/plus-admin-free/src/assets/js/typeahead.js"></script>
    <script src="../Estilos/plus-admin-free/src/assets/js/select2.js"></script>
    <!-- End custom js for this page -->

    <script>

        txtPacFN.max = new Date().toISOString().split("T")[0];
        
    </script>

  </body>
</html>
