<%@ Page Language="C#" MasterPageFile="../../MasterPage.master" AutoEventWireup="true" CodeFile="StockDiario.aspx.cs" Inherits="ASPX_IROJVAR_SISMED2_StockDiario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>IRO - HISMINSA</title>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/json2.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/funciones.js?vfd=1")%>"></script>
    <%--<link rel="stylesheet" media="screen" href="style1.css?id=1">--%>

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



        
        function jsCargaMedicam(codEESS) {
            var params = new Object();
            params.vEESS = codEESS; 
            params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "StockDiario.aspx/GetStkProd", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divStkProd").html(result.d) }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divStkProd").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });

        }

        function fShowMRed() {
            var params = new Object();
            params.vRed = document.getElementById("<%=txtRed.ClientID%>").value; 
            params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "DispBuscaMedxEESS.aspx/GetMRed", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divMRed").html(result.d) }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divMRed").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });
        }

        function fShowEESS() {
            var params = new Object();
            params.vRed = document.getElementById("<%=txtRed.ClientID%>").value; 
            params.vMRed = document.getElementById("<%=txtMicroRed.ClientID%>").value; 
            params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "DispBuscaMedxEESS.aspx/GetEESS", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divEESS").html(result.d) }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divEESS").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });
        }

    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div class="col-xs-8 col-xs-offset-2">
        <p class="cazador2">Busqueda por Productos</p>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

    <form id="form1" runat="server" autocomplete="off">
      <!-- Content Wrapper. Contains page content -->
      <div class="content-wrapper">
         <!-- Content Header (Page header) -->
         <!-- Main content -->
         <section class="content">
        
            <!--SEGUNDA FILA-->             
            <div class="row" style="display:block;">

               <div class="column">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="input-group margin">
                            <asp:TextBox ID="txtRed" runat="server" class='form-control' placeholder="Red"></asp:TextBox>
                            <span class="input-group-btn">
                                <div class='btn btn-info btn-flat' data-toggle="modal" data-target="#modalRed">...</div>
                            </span>
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="input-group margin">
                            <asp:TextBox ID="txtMicroRed" runat="server" class='form-control' placeholder="Micro Red"></asp:TextBox>
                            <span class="input-group-btn">
                                <div class='btn btn-info btn-flat' onclick="fShowMRed()" data-toggle="modal" data-target="#modalMRed">...</div>
                            </span>
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column" style="display:none">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="input-group margin">
                            <asp:TextBox ID="txtEESS" runat="server" class='form-control' placeholder="EESS"></asp:TextBox>
                            <asp:HiddenField ID="txtEESSCodHide" runat="server" />
                            <span class="input-group-btn">
                                <div class='btn btn-info btn-flat' onclick="fShowEESS()" data-toggle="modal" data-target="#modalEESS">...</div>
                            </span>
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column" style="display:block">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="input-group margin">
                            <asp:LinkButton ID="bntBuscar" runat="server" class="btn btn-success" OnClick="bntBuscar_Click" ><i class="fa fa-search"></i> Consultar</asp:LinkButton>
                        </div>
                     </div>
                  </div>
               </div>

               <!-- /.box-header -->
            </div>
            <!--FIN SEGUNDA FILA-->

<script>

    function txtRedCarga(Nombre)
    {
        document.getElementById("<%=txtRed.ClientID%>").value = Nombre; 
    }

    function txtMRedCarga(Nombre)
    {
        document.getElementById("<%=txtMicroRed.ClientID%>").value = Nombre; 
    }

    function txtEESSCarga(Nombre, Codigo)
    {
        document.getElementById("<%=txtEESS.ClientID%>").value = Nombre; 
        document.getElementById("<%=txtEESSCodHide.ClientID%>").value = Codigo; 
    }

</script>
<div style="display:block">

</div>

            <!-- TERCERA FILA -->
            <div class="row">
               <div class="col-md-12">
                  <div class="box" >
                        <div class="box-header">
                            <h3 class="box-title" style="margin-top: -6px; margin-bottom: -11px; margin-left: -7px;">
                            <div class="input-group-btn" style="display:none"><button class="btn btn-default" runat="server" type="button" title="Max.: 3,000 Registros" onserverclick="ExportarExcel_Click"><i class="fa fa-download "></i></button></div>

                            <div class="input-group-btn">
                                    <button class="btn btn-default" type="button" title="Max.: 1,000 Registros" onclick="exportTableToExcel('tbldscrg')"><i class="fa fa-download "></i>
                                    </button>
                            </div>

                            </h3>
                            <div class="box-tools">
                                <asp:Label ID="lblRuta" runat="server" Text="Descarga en: C:\"></asp:Label>
                                <div id="divtxtRuta"></div>
                            </div>
                        </div>
                     <!-- /.box-header -->
                     <div class="box-body table-responsive no-padding" id="habfiltro" style="display:block">
                         <asp:GridView ID="GVtable" runat="server" class="table table-condensed table-bordered"></asp:GridView>
                         <asp:Literal ID="LitTABL1" runat="server"></asp:Literal>
                         <div style="display:none"><div id="LitTABL1_1" runat="server"></div></div>



                         <div id="divDispo1_2" ></div>
                     </div>
                     
                     <!-- /.box-body -->
                  </div>
                  <!-- /.box -->
               </div>
            </div>

            <!--FIN TERCERA FILA-->
<script>

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

        <div class="modal modal-success fade" id="modalEESS">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Establecimiento de Salud </h4>
              </div>
              <div class="modal-body">
                <%--<p>One fine body&hellip;</p>--%>
                  <div id="divEESS"></div>
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

        <div class="modal modal-success fade" id="modalMRed">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">
                    <div class="form-group">
                      <label for="bscprod" class="col-sm-3 control-label">Buscar Micro Red: </label>
                      <div class="col-sm-9">
	                    <input type="text" class="form-control" id="bscprod" placeholder="Micro Red" onkeyup="fBscTblHTML('bscMRed', 'tblMRed', 2)" autofocus="autofocus">
                      </div>
                    </div>
                </h4>
              </div>
              <div class="modal-body">
                  <div id="divMRed"></div>
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

        <div class="modal modal-success fade" id="modalRed">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">
                    <div class="form-group">
                      <label for="bscprod" class="col-sm-3 control-label">Buscar Red: </label>
                      <div class="col-sm-9">
	                    <input type="text" class="form-control" id="bscRed" placeholder="Red" onkeyup="fBscTblHTML('bscRed', 'tblRed', 1)" autofocus="autofocus">
                      </div>
                    </div>
                </h4>
              </div>
              <div class="modal-body">
                  <asp:Literal ID="LitRed" runat="server"></asp:Literal>
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

        <div class="modal modal-success fade" id="modalstkprod">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">
                    <div class="form-group">
                      <label for="bscprod" class="col-sm-3 control-label">Buscar Red: </label>
                      <div class="col-sm-9">
	                    <input type="text" class="form-control" id="bscstkprod" placeholder="Producto" onkeyup="fBscTblHTML('bscRed', 'tblRed', 1)" autofocus="autofocus">
                      </div>
                    </div>
                </h4>
              </div>
              <div class="modal-body">
                  <div id="divStkProd"></div>
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
        document.getElementById("LiSISMEDI2").classList.add('menu-open');
        document.getElementById('UlSISMED2').style.display = 'block';
        document.getElementById("ulSISMEDStkDiario").classList.add('active');
        var f = new Date();

    </script>
</asp:Content>
