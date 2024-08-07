<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConsRegistro.aspx.cs" Inherits="IROTWeb_wPac_Consultorio_ConsRegistro" %>

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

      <script src="https://code.jquery.com/jquery-3.3.1.min.js"></script>

        <script>

            function fnPacBuscarHCget() {
                if (document.getElementById("ddlServicio").value == 0) {
                    alert("Seleccione Servicio");
                    document.getElementById("ddlServicio").focus();
                    return false;
                }

                document.getElementById("txtHCBuscaH").value = document.getElementById("txtHCBusca").value;
                $("#tdPacDNI").html("");
                $("#tdPacApe").html("");
                $("#tdPacNom").html("");
                $("#thPacDiabetico").css("background-color", "#cfe2ff"); 
                $("#tdPacEdadNum").html("");
                $("#tdPacEdadTip").html("");
                $("#tdPacSexo").html("");
                $("#tdPacUbigeo").html("");
                $("#tdPacDistrito").html("");
                $("#tdPacEstb").html("");
                $("#tdPacServ").html("");

                var params = new Object();
                params.vHC = document.getElementById("txtHCBusca").value;
                params.vServ = document.getElementById("ddlServicioH").value;
                params = JSON.stringify(params);
                $.ajax({
                    type: "POST", url: "ConsRegistro.aspx/GetPacienteHC", data: params, 
                    contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                    success: function (result) {
		                var aResult = result.d.split("||sep||");
                        $("#tdPacDNI").html(aResult[0]);
                        $("#tdPacApe").html(aResult[1]);
                        $("#tdPacNom").html(aResult[2]);
                        if (aResult[3] == 1) { $("#thPacDiabetico").css("background-color", "#ff0854"); }
                        $("#tdPacEdadNum").html(aResult[4]);
                        $("#tdPacEdadTip").html(aResult[5]);
                        $("#tdPacSexo").html(aResult[6]);
                        $("#tdPacUbigeo").html(aResult[7]);
                        $("#tdPacDistrito").html(aResult[8]);
                        $("#tdPacEstb").html(aResult[9]);
                        $("#tdPacServ").html(aResult[10]);
                        $("#ddlPacFinS").val(aResult[11]);
                    }, 
                    error: function(XMLHttpRequest, textStatus, errorThrown) { 
                        alert(textStatus + ": " + XMLHttpRequest.responseText); 
                        $("#lblError").html(textStatus + ": " + XMLHttpRequest.responseText);
                    }
                });
            }

            function fGetHIS() {
                //alert("getHIS");
                var params = new Object();
                params.vAnio = document.getElementById("tdAnioHIS").innerHTML;
                params.vMes = document.getElementById("tdMesHIS").innerHTML;
                params.vDia = document.getElementById("tdDiaHIS").innerHTML;
                params.vPlaza = document.getElementById("tdPersonalHis").value;
                params.vCodServ1 = document.getElementById("<%=ddlServicio.ClientID%>").value;
                params.vTurno = document.getElementById("cboSerTurno").value;
                params = JSON.stringify(params);

                $.ajax({
                    type: "POST", url: "ConsRegistro.aspx/GetbuscarHIS", data: params, 
                    contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                    success: function (result) {
                        document.getElementById("tblHISPacDet").getElementsByTagName('tbody')[0].innerHTML = result.d;
                    }, 
                    error: function(XMLHttpRequest, textStatus, errorThrown) { 
                        alert(textStatus + ": " + XMLHttpRequest.responseText); 
                        $("#lblError").html(textStatus + ": " + XMLHttpRequest.responseText);
                    }
			    });

		    }

            function getMBuscaDxCIEX() {
                var params = new Object();
                params.vDes = document.getElementById("txtMBuscaDxCIEX").value;
                params = JSON.stringify(params);

                $.ajax({
                    type: "POST", url: "ConsRegistro.aspx/GetMBuscaDxCIEX", data: params, 
                    contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                    success: function (result) {
                        document.getElementById("tblMDxCiexDet").getElementsByTagName('tbody')[0].innerHTML = result.d;
                    }, 
                    error: function(XMLHttpRequest, textStatus, errorThrown) { 
                        alert(textStatus + ": " + XMLHttpRequest.responseText); 
                        $("#lblError").html(textStatus + ": " + XMLHttpRequest.responseText);
                    }
			    });

            }


        </script>

        <script src="../Estilos/fnGen.js"></script>
        <script>
            /* ////////  Variables Globales  ///////// */
            DxFilaSel = 0;
        </script>

        <script src="../Estilos/jquery.js" type="text/javascript"></script>
        <script type="text/javascript">
            /* ////////  No enviar ENTER  ///////// */
            $(document).ready(function() {
                $("form").keypress(function(e) {
                    if (e.which == 13) {
                        return false;
                    }
                });
            });
        </script>

    <style>
        select {
            width: 10rem;
            background-color: #c3f0c6;
            border: #b2eeac 2px solid;
            color: black;
        }
         
        select>option {
			font-size: 18px;
            /*background-color: #b8e1ba;*/
        }
    </style>
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
          <form method="post" id="form1" autocomplete="off" runat="server">
<asp:Label ID="lblError" runat="server" />
<asp:GridView ID="GVtable" runat="server"></asp:GridView>
            <div class="row">
              <div class="col-md-5 grid-margin stretch-card">
                <div class="card">
                          <div class="row">
                              <div class="col-md-12 table-responsive">
                                    <table class="table table-hover table-bordered ">
                                     <tr class="table-primary">
                                      <th>N° FRT</th>
                                      <th>N° LOT</th>
                                      <th>N° PAG</th>
                                      <th>MES</th>
                                      <th>AÑO</th>
                                      <th>TURNO</th>
                                     </tr>
                                     <tr>
                                      <td></td>
                                      <td></td>
                                      <td></td>
                                      <td id="tdMesHIS" runat="server"></td>
                                      <td id="tdAnioHIS" runat="server"></td>
                                      <td>
                                        <select class="form-select" id="cboSerTurno" runat="server" style="margin-top:-20px; margin-bottom:-20px; " autofocus>
                                          <option value=""></option>
                                          <option value="M">Mañana</option>
                                          <option value="T">Tarde</option>
                                        </select>
                                      </td>
                                     </tr>
                                    </table>
                              </div>
                          </div>
                </div>
              </div>

              <div class="col-md-7 grid-margin stretch-card">
                <div class="card">
                  <div class="">
                      <div class="row">
                        <div class="col-md-12 table-responsive">
                            <input type="hidden" id="tdPerCodProfH" runat="server" />
                            <input type="hidden" id="tdPersonalHis" runat="server" />
                            <table class="table table-hover table-bordered">
                              <tr class="table-primary">
                                <th colspan="2">SERVICIO</th>
                                <th>NOMBRE DEL RESPONSABLE DE LA ATENCION</th>
                                <th>N° FUA / AFILIACION</th>
                              </tr>
                              <tr>
                                  <td>
                                        <asp:DropDownList ID="ddlServicio" runat="server" class='form-select' AppendDataBoundItems="true" style="margin-top:-20px; margin-bottom:-20px; max-width:200px;" onchange="document.getElementById('ddlServicioH').value=this.value; " >
                                        </asp:DropDownList>
                                        <input type="hidden" id="ddlServicioH" />
                                  </td>
                                  <td>
                                        <button type="button" id="btnGetHIS" class="btn btn-inverse-primary btn-icon" onclick="fGetHIS()" style="margin-top:-20px; margin-bottom:-20px; " >
                                            <i class="fa fa-search"></i>
                                        </button>
                                  </td>
                                <td runat="server" id="tdPersonalID"></td>
                                <td></td>
                              </tr>
                            </table>
                        </div>

                      </div>


                  </div>
                </div>
              </div>
            </div>

            <div class="row">
              <div class="col-md-5 grid-margin stretch-card">
                <div class="card">
                    <div class="row">
                        <div class="col-md-12 table-responsive">
                            <table class="table table-hover table-sm table-bordered" style="">
                                <col width="32">
                                <col width="34" span="2">
                                <col width="51">
                                <col width="70">
                                <col width="37" span="2">
                                <col width="90">
                                <tr class="table-primary" >
                                    <th width="32">DIA</th>
                                    <th colspan="3" width="119">N° HC</th>
                                    <th colspan="4" width="234">APELLIDOS</th>
                                </tr>
                                <tr>
                                    <td rowspan="7" runat="server" ID="tdDiaHIS"></td>
                                    <td colspan="3">
                                        <input type="text" runat="server" class="form-control border-0" id="txtHCBusca" style="margin-top:-15px; margin-bottom:-20px; " />
                                        <input type="hidden" runat="server" id="txtHCBuscaH" />
                                    </td>
                                    <td colspan="4" runat="server" ID="tdPacApe"></td>
                                </tr>
                                <tr class="table-primary">
                                    <th colspan="3">DNI</th>
                                    <th colspan="3">NOMBRES</th>
                                    <th runat="server" id="thPacDiabetico" style="color: #cfe2ff">DIABETICO</th>
                                </tr>
                                <tr>
                                    <td colspan="3" runat="server" ID="tdPacDNI"></td>
                                    <td colspan="4" runat="server" ID="tdPacNom"></td>
                                </tr>
                                <tr class="table-primary">
                                    <th colspan="2">EDAD</th>
                                    <th>SEXO</th>
                                    <th>UBIGEO</th>
                                    <th colspan="3">DISTRITO</th>
                                </tr>
                                <tr>
                                    <td runat="server" ID="tdPacEdadNum"></td>
                                    <td runat="server" ID="tdPacEdadTip"></td>
                                    <td runat="server" ID="tdPacSexo"></td>
                                    <td runat="server" ID="tdPacUbigeo"></td>
                                    <td colspan="3" runat="server" ID="tdPacDistrito"></td>
                                </tr>
                                <tr class="table-primary">
                                    <th colspan="2">ESTB</th>
                                    <th>SERV</th>
                                    <th colspan="2">F.S.</th>
                                    <th colspan="2">P.E.</th>
                                </tr>
                                <tr>
                                    <td colspan="2" runat="server" ID="tdPacEstb"></td>
                                    <td runat="server" ID="tdPacServ"></td>
                                    <td colspan="2" runat="server" ID="tdPacFinS" >
                                        <asp:DropDownList ID="ddlPacFinS" runat="server" class='form-select' AppendDataBoundItems="true" style="margin-top:-12px; margin-bottom:-12px; max-width:200px; pointer-events: none; "  >
                                        </asp:DropDownList>
                                    </td>
                                    <td colspan="2" runat="server" ID="tdPacEtn">
                                        <asp:DropDownList ID="ddlPacEtn" runat="server" class='form-select' AppendDataBoundItems="true" style="margin-top:-20px; margin-bottom:-20px; max-width:200px;" disabled>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>

                </div>
              </div>
                
              <div class="col-md-6 grid-margin stretch-card">
                <div class="card">
                      <div class="row">
                        <div class="table-sorter-wrapper col-md-12 table-responsive">
                            <table class="table table-hover table-bordered table-striped ">
                              <col width="10">
                              <tr class="table-primary">
                                <%--<th></th>--%>
                                <th width="486" colspan="2">DIAGNOSTICO, MOTIVO DE CONSULTA Y/O ACTIVIDAD DE SALUD</th>
                                <th width="80">TIPO</th>
                                <th width="80">OJO/SES</th>
                                <th width="80">CIE10</th>
                                <th style="display:none"></th>
                              </tr>
                              <tr>
                                <td>
                                    <a class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalDxCIEX" onclick="fnMDxBusca(1)" style="margin-top:-11px; margin-bottom:-11px; ">
                                        <i class="fa fa-ellipsis-h"></i>
                                    </a>
                                </td>
                                <td><label id="lblDxNom1"></label></td>
                                <td><label id="lblDxTip1"></label></td>
                                <td><label id="lblDxOjo1"></label></td>
                                <td><label id="lblDxCIE1"></label></td>
                                <td style="display:none"><label id="lblDxCod1"></label></td>
                              </tr>
                              <tr>
                                <td>
                                    <a class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalDxCIEX" onclick="fnMDxBusca(2)" style="margin-top:-11px; margin-bottom:-11px; ">
                                        <i class="fa fa-ellipsis-h"></i>
                                    </a>
                                </td>
                                <td><label id="lblDxNom2"></label></td>
                                <td><label id="lblDxTip2"></label></td>
                                <td><label id="lblDxOjo2"></label></td>
                                <td><label id="lblDxCIE2"></label></td>
                                <td style="display:none"><label id="lblDxCod2"></label></td>
                              </tr>
                              <tr>
                                <td>
                                    <a class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalDxCIEX" onclick="fnMDxBusca(3)" style="margin-top:-11px; margin-bottom:-11px; ">
                                        <i class="fa fa-ellipsis-h"></i>
                                    </a>
                                </td>
                                <td><label id="lblDxNom3"></label></td>
                                <td><label id="lblDxTip3"></label></td>
                                <td><label id="lblDxOjo3"></label></td>
                                <td><label id="lblDxCIE3"></label></td>
                                <td style="display:none"><label id="lblDxCod3"></label></td>
                              </tr>
                              <tr>
                                <td>
                                    <a class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalDxCIEX" onclick="fnMDxBusca(4)" style="margin-top:-11px; margin-bottom:-11px; ">
                                        <i class="fa fa-ellipsis-h"></i>
                                    </a>
                                </td>
                                <td><label id="lblDxNom4"></label></td>
                                <td><label id="lblDxTip4"></label></td>
                                <td><label id="lblDxOjo4"></label></td>
                                <td><label id="lblDxCIE4"></label></td>
                                <td style="display:none"><label id="lblDxCod4"></label></td>
                              </tr>
                              <tr>
                                <td>
                                    <a class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalDxCIEX" onclick="fnMDxBusca(5)" style="margin-top:-11px; margin-bottom:-11px; ">
                                        <i class="fa fa-ellipsis-h"></i>
                                    </a>
                                </td>
                                <td><label id="lblDxNom5"></label></td>
                                <td><label id="lblDxTip5"></label></td>
                                <td><label id="lblDxOjo5"></label></td>
                                <td><label id="lblDxCIE5"></label></td>
                                <td style="display:none"><label id="lblDxCod5"></label></td>
                              </tr>
                              <tr>
                                <td>
                                    <a class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalDxCIEX" onclick="fnMDxBusca(6)" style="margin-top:-11px; margin-bottom:-11px; ">
                                        <i class="fa fa-ellipsis-h"></i>
                                    </a>
                                </td>
                                <td><label id="lblDxNom6"></label></td>
                                <td><label id="lblDxTip6"></label></td>
                                <td><label id="lblDxOjo6"></label></td>
                                <td><label id="lblDxCIE6"></label></td>
                                <td style="display:none"><label id="lblDxCod6"></label></td>
                              </tr>
                            </table>
                        </div>

                      </div>

                </div>
              </div>
              <div class="col-md-1 grid-margin stretch-card">
                <div class="card-body">
                    <div class="form-group row">
                        <div class="form-group col">
                            <button type="button" class="btn btn-outline-primary btn-icon-text" style="margin-bottom:-10px; ">
                                <i class="fa fa-plus-circle"></i><br /> F10
                            </button>

                        </div>
                        <div class="form-group col">
                            <button type="button" class="btn btn-outline-warning btn-icon-text" style="margin-top:-10px; margin-bottom:-10px; ">
                                <i class="fa fa-minus-circle"></i><br /> F11
                            </button>
                        </div>
                        <div class="form-group col">
                            <button type="button" class="btn btn-outline-success btn-icon-text" style="margin-top:-10px; margin-bottom:-10px; ">
                                <i class="fa fa-edit"></i><br /> F12
                            </button>
                        </div>
                        <div class="form-group col">
                            <button type="button" class="btn btn-outline-danger btn-rounded btn-icon-text" style="margin-top:-10px; margin-bottom:-10px; ">
                                <i class="fa fa-times-circle"></i><br /> F13
                            </button>
                        </div>

                    </div>

                </div>
              </div>


            </div>

              
            <div class="row">
                <div class="col-md-9 table-responsive">
                    <table id="tblHISPacDet" class="table table-hover table-bordered table-striped">
                        <thead>
                            <tr class="table-primary" style="font-weight: bolder;">
                                <th><b>N°</b></th>
                                <th><b>DIA</b></th>
                                <th><b>HC</b></th>
                                <th><b>EDAD</b></th>
                                <th><b>T</b></th>
                                <th><b>S</b></th>
                                <th><b>TD</b></th><th>LB</th><th>CIE</th>
                                <th><b>TD</b></th><th>LB</th><th>CIE</th>
                                <th><b>TD</b></th><th>LB</th><th>CIE</th>
                                <th><b>TD</b></th><th>LB</th><th>CIE</th>
                                <th><b>TD</b></th><th>LB</th><th>CIE</th>
                                <th><b>TD</b></th><th>LB</th><th>CIE</th>
                                <th><b>O.LAB</b></th>
                                <th><b>O.CIR</b></th>
                                <th><b>Accion</b></th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>1</td><td></td>
                                <td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>
                                <td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>
                                <td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>
                                <td>
                                    <button type="button" class="btn btn-inverse-success btn-icon" onclick="editarFila(this)" style="margin-top:-20px; margin-bottom:-20px; ">
                                        <i class="fa fa-edit"></i>
                                    </button>
                                    <button type="button" class="btn btn-inverse-danger btn-icon" onclick="eliminarFila(this)" style="margin-top:-20px; margin-bottom:-20px; ">
                                        <i class="fa fa-trash-o"></i>
                                    </button>
                                </td>
                            </tr>
                        </tbody>

                    </table>                    
                </div>
            
                <div class="col-md-3 table-responsive">
                    <table class="table table-hover table-bordered table-striped">
                        <tr class="table-primary">
                            <th>TUR</th>
                            <th>DIA</th>
                            <th>SERVICIO</th>
                            <th>REG</th>
                            <th>Accion</th>
                        </tr>
                        <tr>
                            <td>1</td>
                            <td></td><td></td><td></td>
                            <td>
                                <button type="button" class="btn btn-inverse-success btn-icon" onclick="editarFila(this)" style="margin-top:-20px; margin-bottom:-20px; ">
                                    <i class="fa fa-edit"></i>
                                </button>
                                <button type="button" class="btn btn-inverse-danger btn-icon" onclick="eliminarFila(this)" style="margin-top:-20px; margin-bottom:-20px; ">
                                    <i class="fa fa-trash-o"></i>
                                </button>
                            </td>
                        </tr>
                    </table>
                </div>

            </div>

            <div class="row">
                
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

    
    <div class="modal fade" id="modalDxCIEX" tabindex="-1" role="dialog" aria-labelledby="mdlabel" aria-hidden="true">
        <div class="modal-dialog modal-xl" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <div class="modal-title">
                        <div class="input-group">
                            <input type="text" id="txtMBuscaDxCIEX" class="form-control" placeholder="Diagnostico" aria-label="Diagnostico" aria-describedby="basic-addon2" />
                            <div class="input-group-append">
                                <button id="btnMBuscaDxCIEX" class="btn btn-sm btn-primary" type="button" onclick="getMBuscaDxCIEX()"><i class="fa fa-search"></i></button>
                            </div>
                        </div>
                    </div>
                    <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p>
                        <div class="row">
                            <div class="col-md-6">
                                <input type="text" class="form-control" id="txtPacDxG" placeholder="DIAGNOSTICO, MOTIVO DE CONSULTA Y/O ACTIVIDAD DE SALUD" style="pointer-events: none; " >
                            </div>
                            <div class="col-md-1">
                                <%--<input type="text" class="form-control" id="txtPacDxTipo" placeholder="TIPO">--%>
                                <select class="form-select" id="txtPacDxTipo" style="margin-top:5px; margin-bottom:5px; ">
                                    <option value=""></option>
                                    <option value="R">R</option>
                                    <option value="P">P</option>
                                    <option value="D">D</option>
                                </select>
                            </div>
                            <div class="col-md-2">
                                  <input list="lLabOjos" type="text" class="form-control" id="txtPacDxOjoSes" placeholder="OJO/SES" onclick="this.select()">
                                  <datalist id="lLabOjos">
                                    <option value="AO">
                                    <option value="OI">
                                    <option value="OD">
                                  </datalist>
                            </div>
                            <div class="col-md-2">
                                <input type="text" class="form-control" id="txtPacDxCIEX" placeholder="CIE10" style="pointer-events: none; " >
                                <input type="hidden" id="txtPacDxCodigoH" />
                            </div>
                            <div class="col-md-1">
                                <button class="btn btn-sm btn-primary" type="button" data-bs-dismiss="modal" onclick="fnMDxAgregar()">
                                    <i class="fa fa-arrow-right"></i>Agregar
                                </button>
                            </div>

                        </div>
                    </p>
                    <p>
                        <div class="row">
                            <div class="col-md-12">
                                <table id="tblMDxCiexDet" class="table table-hover table-bordered table-striped">
                                    <thead>
                                        <tr class="table-primary">
                                            <th width="486"><b>DIAGNOSTICO, MOTIVO DE CONSULTA Y/O ACTIVIDAD DE SALUD</b></th>
                                            <th width="80"><b>CIE10</b></th>
                                            <th style="display:none"><b>CIE10</b></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr onclick="fnMDxSelect(this)">
                                            <td></td>
                                            <td></td>
                                            <td style="display:none"></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>

                        </div>
                    </p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    <%--<button type="button" class="btn btn-primary">Save changes</button>--%>
                </div>
            </div>
        </div>
    </div>



    <div style="display:none">
        <button type="button" id="btnModConsulta" data-bs-toggle="modal" data-bs-target="#modalShowNIng" >...</button>

        <button type="button" id="btnBscPaciente"  >...</button>


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

          function fnPacBuscarHC() {
              //alert('Buscar Paciente HC');
              fnPacBuscarHCget();
          }

      </script>
      <script>
          /* /////////  Abrir modal  /////////////  */

          function fnMDxBusca(fila) {
              DxFilaSel = fila;
              fnMfocustxt('txtMBuscaDxCIEX');
          }

          function fnMDxSelect(vtr) {
              //alert(vtr.textContent);
              td01 = vtr.getElementsByTagName("td")[0];
              td02 = vtr.getElementsByTagName("td")[1];
              td03 = vtr.getElementsByTagName("td")[2];
              //td04 = vtr.getElementsByTagName("td")[3];
              
              document.getElementById("txtPacDxG").value = td01.textContent || td01.innerText;
              document.getElementById("txtPacDxCIEX").value = td02.textContent || td02.innerText;
              document.getElementById("txtPacDxCodigoH").value = td03.textContent || td03.innerText;

              document.getElementById("txtPacDxTipo").focus();
              document.getElementById("txtPacDxTipo").select();
          }

          function fnMDxAgregar() {
              //alert(DxFilaSel);
              document.getElementById("lblDxNom" + DxFilaSel).innerText = document.getElementById("txtPacDxG").value;
              document.getElementById("lblDxTip" + DxFilaSel).innerText = document.getElementById("txtPacDxTipo").value;
              document.getElementById("lblDxOjo" + DxFilaSel).innerText = document.getElementById("txtPacDxOjoSes").value;
              document.getElementById("lblDxCIE" + DxFilaSel).innerText = document.getElementById("txtPacDxCIEX").value;
              document.getElementById("lblDxCod" + DxFilaSel).innerText = document.getElementById("txtPacDxCodigoH").value;
          }

          function fnMPacTDoc(tDoc) {
              document.getElementById('cboPacBuscTDoc').value = tDoc;
          }


          /* //////////////  Input Focus  /////////// */

          vinputtxt = "";
          function fnMfocustxt(txt) {
              vinputtxt = txt;
              const intervalID = setTimeout(fnMfocustxtSet, 800);
          }
          function fnMfocustxtSet() {
              document.getElementById(vinputtxt).focus();
          }

      </script>

        <script>
            /* /////////  Input Text Box ENTER  /////////////  */

            var itxtMBuscaDxCIEX = document.getElementById("txtMBuscaDxCIEX");
            itxtMBuscaDxCIEX.addEventListener("keyup", function(event) {
                if (event.keyCode === 13) {
		            event.preventDefault();
		            document.getElementById("btnMBuscaDxCIEX").click();
                }
            });

            var itxtHCBusca = document.getElementById("txtHCBusca");
            itxtHCBusca.addEventListener("keyup", function(event) {
                if (event.keyCode === 13) {
		            event.preventDefault();
		            fnPacBuscarHC();
                }
            });

            var iddlServicio = document.getElementById("<%=ddlServicio.ClientID%>");
            iddlServicio.addEventListener("keyup", function(event) {
                if (event.keyCode === 13) {
		            event.preventDefault();
		            document.getElementById("btnGetHIS").click();
                }
            });

        </script>
        
        <script>
            /* /////////  scripts Funciones con Tablas  /////////////  */

            function eliminarFila(boton) {
                // navegar hasta el nodo fila
                fila = boton.parentNode.parentNode;
                // navegar al nodo superior de la fila y borrar la fila
                fila.parentNode.removeChild(fila);
            }

            function fTblHISPacDetHide() {
                // Declare variables
                tblBusca = "tblHISPacDet";
                var table, tr, i, td4;
                table = document.getElementById(tblBusca);
                tr = table.getElementsByTagName("tr");

                for (i = 0; i < tr.length; i++) {
                    td4 = tr[i].getElementsByTagName("td")[4];
                    td4.style.display = "none";
                }

            }


            //fTblHISPacDetHide()

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

        //txtPacFN.max = new Date().toISOString().split("T")[0];

    </script>

        <script>
            $(document).on('keyup keydown', function(e) {
                if (e.keyCode === 121 && e.shiftKey === true) {
                    console.log('shift-f10 detected');
                    e.preventDefault();
                    e.stopPropagation();
                    e.stopImmediatePropagation();
	                //alert('Shift+F10');
                    fnConsultar();
                }
            });
        </script>

  </body>
</html>
