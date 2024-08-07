<%@ Page Language="C#" MasterPageFile="../../MasterPage.master" AutoEventWireup="true" CodeFile="DispoIRO2.aspx.cs" Inherits="ASPX_IROJVAR_SISMED_DispoIRO2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>IRO - HISMINSA</title>
    <link rel="stylesheet" href="../../Estilos/jquery.dataTables.min.css">
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/json2.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/funciones.js?vfd=2")%>"></script>
    <%--<link rel="stylesheet" media="screen" href="style1.css?id=1">--%>

    <script language="javascript" type="text/javascript">

        function DetAtenciones(vanio, vmes, vdia, turno, vplaza) {
            var params = new Object();
            params.vanio = vanio; // cambiar la descripcion del params y el ddl
            params.vmes = vmes; 
            params.vdia = vdia; 
            params.turno = turno; 
            params.vplaza = vplaza; 
            params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "Atenciones.aspx/GetDetAtenciones", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divDetAtencion").html(result.d) }, //success: LoadPrueba01, //Procesar 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divDetAtencion").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });
        }

        function fStockVto(vMedCod) {
            var params = new Object();
            params.vCodSisMed = vMedCod; 
            params = JSON.stringify(params);

            $("#divDetStkVto").html("");

            $.ajax({
                type: "POST", url: "DispoIRO2.aspx/GetStockVto", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divDetStkVto").html(result.d) }, //success: LoadPrueba01, //Procesar 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divDetStkVto").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });
        }

        function fSIGA1(vCodSIGA, vProducto, vStock, vConsProm, vStkMes) {
            var params = new Object();
            params.vCodSIGA = vCodSIGA; 
            params.vProducto = vProducto; 
            params.vStock = vStock; 
            params.vConsProm = vConsProm; 
            params.vStkMes = vStkMes; 
            params = JSON.stringify(params);

            $("#divDetSIGA1").html("");
            $("#SIGA1Titulo").html("SIGA - " + vProducto);

            $.ajax({
                type: "POST", url: "DispoIRO2.aspx/GetSIGA1", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divDetSIGA1").html(result.d) }, //success: LoadPrueba01, //Procesar 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divDetSIGA1").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });
        }

        function fSIGA2(vCodSIGA, vProducto) {
            var params = new Object();
            params.vCodSIGA = vCodSIGA; 
            params.vProducto = vProducto; 
            params = JSON.stringify(params);

            $("#divDetSIGA2").html("");
            $("#SIGA2Titulo").html("SIGA - " + vProducto);

            $.ajax({
                type: "POST", url: "DispoIRO2.aspx/GetSIGA2", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divDetSIGA2").html(result.d) }, //success: LoadPrueba01, //Procesar 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divDetSIGA2").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });
        }

        function fDetProd(vCodSisMed, vAnio, vMes, consTotal, ConsProm, stkTotal, stkMes, SSO, vProducto) {
            var params = new Object();
            params.vCodSisMed = vCodSisMed; 
            params.vAnio = vAnio; 
            params.vMes = vMes; 
            params.consTotal = consTotal; 
            params.ConsProm = ConsProm; 
            params.stkTotal = stkTotal; 
            params.stkMes = stkMes; 
            params.SSO = SSO; 
            params.vProducto = vProducto; 
            params = JSON.stringify(params);

            $("#divDetMed").html("");
            $("#DetMedTitulo").html("Medicamento - " + vProducto);

            $.ajax({
                type: "POST", url: "DispoIRO2.aspx/GetDetCodSISMED", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divDetMed").html(result.d) }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divDetMed").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });
        }

        function fDetProd2(vCodSisMed, vAnio, vMes, consTotal, ConsProm, stkTotal, stkMes, SSO, vProducto) {

<%--            var datatable = $(".<%=GVtable.ClientID%>").DataTable();
            alert(datatable);--%>

            var params = new Object();
            params.vCodSisMed = vCodSisMed; 
            params.vAnio = vAnio; 
            params.vMes = vMes; 
            params.consTotal = consTotal; 
            params.ConsProm = ConsProm; 
            params.stkTotal = stkTotal; 
            params.stkMes = stkMes; 
            params.SSO = SSO; 
            params.vProducto = vProducto; 
            params.vTable = document.getElementById("<%=GVtable.ClientID%>").innerHTML; 
            //params.vTable = document.getElementById("dgvTable").innerHTML; 
            //params.vTable = datatable; 
            params = JSON.stringify(params);

            $("#divDetMed").html("");
            $("#DetMedTitulo").html("Medicamento - " + vProducto);

            $.ajax({
                type: "POST", url: "DispoIRO2.aspx/GetDetCodSISMED2", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divDetMed").html(result.d) }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divDetMed").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });
        }

        function fMostrarTodo() {
            //alert("1");
            //document.getElementsByClassName('Segundo')[0].style.display = 'table-row';
            var x = document.getElementsByClassName("Segundo");
            var i;
            if (x[0].style.display=='table-row') {
                for (i = 0; i < x.length; i++) {
                    x[i].style.display = 'none';
                }
            }
            else {
                for (i = 0; i < x.length; i++) {
                    x[i].style.display = 'table-row';
                }
            }
        }

    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div class="col-xs-8 col-xs-offset-2">
        <a href="../SISMED2/DispGenBuscaMed.aspx" target="_blank"><p class="cazador2">Disponibilidad</p></a>
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
               <div class="column hide">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:TextBox ID="txtEESS" runat="server" Text="05197-IRO" class='form-control' ReadOnly="True"></asp:TextBox>
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                           <asp:DropDownList ID="DDLAnio" runat="server" class='form-control' >
                              <asp:ListItem Value="2018">2018</asp:ListItem>
                              <asp:ListItem Value="2019">2019</asp:ListItem>
                              <asp:ListItem Value="2020">2020</asp:ListItem>
                              <asp:ListItem Value="2021">2021</asp:ListItem>
                              <asp:ListItem Value="2022">2022</asp:ListItem>
                              <asp:ListItem Value="2023">2023</asp:ListItem>
                              <asp:ListItem Value="2024">2024</asp:ListItem>
                              <asp:ListItem Value="2025">2025</asp:ListItem>
                              <asp:ListItem Value="2026">2026</asp:ListItem>
                           </asp:DropDownList>
                        </div>
                     </div>
                  </div>
               </div>
               <div class="column">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                           <asp:DropDownList ID="DDLMes" runat="server" class='form-control'>
                              <asp:ListItem Value="1">Enero</asp:ListItem>
                              <asp:ListItem Value="2">Febrero</asp:ListItem>
                              <asp:ListItem Value="3">Marzo</asp:ListItem>
                              <asp:ListItem Value="4">Abril</asp:ListItem>
                              <asp:ListItem Value="5">Mayo</asp:ListItem>
                              <asp:ListItem Value="6">Junio</asp:ListItem>
                              <asp:ListItem Value="7">Julio</asp:ListItem>
                              <asp:ListItem Value="8">Agosto</asp:ListItem>
                              <asp:ListItem Value="9">Setiembre</asp:ListItem>
                              <asp:ListItem Value="10">Octubre</asp:ListItem>
                              <asp:ListItem Value="11">Noviembre</asp:ListItem>
                              <asp:ListItem Value="12">Diciembre</asp:ListItem>
                           </asp:DropDownList>
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column" style="display:block">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:LinkButton ID="bntBuscar" runat="server" class="btn btn-success" OnClick="bntBuscar_Click" ><i class="fa fa-search"></i> Consultar</asp:LinkButton>
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column" style="display:block">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:LinkButton ID="btnPetitorio" runat="server" class="btn btn-success" OnClick="btnPetitorio_Click" ><i class="fa fa-search"></i> Petitorio</asp:LinkButton>
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column" style="display:block">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:LinkButton ID="btnNoPetitorio" runat="server" class="btn btn-success" OnClick="btnNoPetitorio_Click" ><i class="fa fa-search"></i> No Petitorio</asp:LinkButton>
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column">
                  <div class="col-md-2" style="text-align: center; padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:DropDownList ID="ddlDepto" runat="server" class='form-control' AppendDataBoundItems="true" onchange="fBscTblHTML('ContentPlaceHolder2_ddlDepto', 'tblbscrJS', 16); " >
                                <asp:ListItem Value="">Todos</asp:ListItem>
                            </asp:DropDownList>
                            <code style="font-size:large; background-color:transparent; ">Depto/Serv</code>
                        </div>
                     </div>
                  </div>
               </div>

               <!-- /.box-header -->
            </div>
            <!--FIN SEGUNDA FILA-->

<script>

</script>

            <div class="row" style="display:block;">

               <div class="column" style="display:block">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                    <div class="form-group">
                        <label>
                            <asp:CheckBox ID="chkCatarata" runat="server" /> CATARATA
                        </label>
                    </div>
                  </div>
               </div>

               <div class="column" style="display:block">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                    <div class="form-group">
                        <label>
                            <asp:CheckBox ID="chkGlaucoma" runat="server" /> GLAUCOMA
                        </label>
                    </div>
                  </div>
               </div>
               <div class="column" style="display:block">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                    <div class="form-group">
                        <label>
                            <asp:CheckBox ID="chkRetina" runat="server" /> RETINA
                        </label>
                    </div>
                  </div>
               </div>
               <div class="column" style="display:block">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                    <div class="form-group">
                        <label>
                            <asp:CheckBox ID="chkOG" runat="server" /> OFTALMOLOGIA GENERAL
                        </label>
                    </div>
                  </div>
               </div>

               <div class="column" style="display:block">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                    <div class="form-group">
                        <label>
                            <a href="ComparaDispoSIS.aspx" ID="btnComparaSIS" runat="server" class="btn btn-info" target="_blank" >Compara SIS</a>
                        </label>
                    </div>
                  </div>
               </div>




               <!-- /.box-header -->
            </div>


            <div class="row">
               <div class="col-md-12">
                  <div class="box" >
                     <!-- /.box-header -->
                     <div class="box-body table-responsive no-padding" >
<table class="table table-condensed table-bordered" >
    <tr> 
        <th class="box-body text-center bg-black" colspan="7"> <asp:Label ID="lblTitulo" runat="server" ></asp:Label> </th> 
    </tr>
  <tr>
    <th class="box-body text-center bg-aqua">% DISP <input type="hidden" id="hDisp" value="" /> </th> 
	  <th class="box-body text-center bg-green">NORMO STOCK <input type="hidden" id="hNS" value="NORMOSTOCK" /> </th>    
	  <th class="box-body text-center bg-yellow">SOBR - No Acep <input type="hidden" id="hSob" value="SOBRESTOCK" /> </th> 
	  <th class="box-body text-center bg-red">SUB - Critica <input type="hidden" id="hSub" value="SUBSTOCK" /> </th>
	  <th class="box-body text-center bg-green">SR <input type="hidden" id="hSR" value="SIN ROTACION" /> </th> 
	  <th class="box-body text-center bg-red">DES <input type="hidden" id="hDes" value="DESABASTECIDO" /> </th>
	  <th class="box-body text-center bg-aqua">NoCons <input type="hidden" id="hNCo" value="NoConsid" /> </th>
  </tr>
  <tr>
    <td class="box-body text-center text-aqua " onclick="fBscTblHTML('hDisp', 'tblbscrJS', 7)"> <asp:Label ID="lblDisp" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-green" onclick="fBscTblHTML('hNS', 'tblbscrJS', 7)"> <asp:Label ID="lblNS" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-yellow" onclick="fBscTblHTML('hSob', 'tblbscrJS', 7)"> <asp:Label ID="lblSob" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-red" onclick="fBscTblHTML('hSub', 'tblbscrJS', 7)"> <asp:Label ID="lblSub" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-green"onclick="fBscTblHTML('hSR', 'tblbscrJS', 7)"> <asp:Label ID="lblSR" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-red"onclick="fBscTblHTML('hDes', 'tblbscrJS', 7)"> <asp:Label ID="lblDes" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-aqua"onclick="fBscTblHTML('hNCo', 'tblbscrJS', 7)"> <asp:Label ID="lblNCo" runat="server" ></asp:Label> </td>
  </tr>
  <tr>
    <td class="box-body text-center text-aqua " onclick="fBscTblHTMLOrden('hDisp', 'tblbscrJS', 7)"> <asp:Label ID="lblDispC" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-green" onclick="fBscTblHTMLOrden('hNS', 'tblbscrJS', 7)"> <asp:Label ID="lblNSC" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-yellow" onclick="fBscTblHTMLOrden('hSob', 'tblbscrJS', 7)"> <asp:Label ID="lblSobC" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-red" onclick="fBscTblHTMLOrden('hSub', 'tblbscrJS', 7)"> <asp:Label ID="lblSubC" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-green"onclick="fBscTblHTMLOrden('hSR', 'tblbscrJS', 7)"> <asp:Label ID="lblSRC" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-red"onclick="fBscTblHTMLOrden('hDes', 'tblbscrJS', 7)"> <asp:Label ID="lblDesC" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-aqua"onclick="fBscTblHTMLOrden('hNCo', 'tblbscrJS', 7)"> <asp:Label ID="lblNCoC" runat="server" ></asp:Label> </td>
  </tr>
</table>
                     </div>
                     <!-- /.box-body -->
                  </div>
                  <!-- /.box -->
               </div>
            </div>


            <!-- TERCERA FILA -->
            <div class="row">
               <div class="col-md-12">
                  <div class="box" >
<%--                        <div class="box-header">
                            <h3 class="box-title" style="margin-top: -6px; margin-bottom: -11px; margin-left: -7px;">
                            <div class="input-group-btn">
                                    <button class="btn btn-default" runat="server" type="button" title="Max.: 3,000 Registros" onserverclick="ExportarExcel_Click">
                                    <i class="fa fa-download "></i>
                                    </button>
                            </div>
                            </h3>
                            <div class="box-tools">
                                <asp:Label ID="lblRuta" runat="server" Text="Descarga en: C:\"></asp:Label>
                                <div id="divtxtRuta"></div>
                            </div>
                        </div>--%>
                        <div class="box-header">
                            <h3 class="box-title" style="margin-top: -6px; margin-bottom: -11px; margin-left: -7px;">
					<div class="input-group margin">
						<div class="input-group-btn">
								<button class="btn btn-default" type="button" title="Max.: 1,000 Registros" onclick="exportTableToExcel('tblbscrJS')"><i class="fa fa-download "></i>
								</button>
						</div>
						<div><input type="text" class="form-control" id="bscprod2" placeholder="Buscar Nombre de Producto" onkeyup="fBscTblHTML('bscprod2', 'tblbscrJS', 3)" autofocus="autofocus">
						</div>
					</div>
                            </h3>
                            <div class="box-tools">
                                <asp:Label ID="Label1" runat="server" Text="/\/\"></asp:Label>
                            </div>
                        </div>
                     <!-- /.box-header -->
                     <div class="box-body table-responsive no-padding" id="habfiltro">
                        <asp:Literal ID="LitTABL1" runat="server"></asp:Literal>
                     </div>
                      <div id="dgvTableHide" class="hide">
                          <div id="dgvTable">
                            <asp:GridView ID="GVtable" runat="server" class="table table-condensed table-bordered"></asp:GridView>
                          </div>
                      </div>


                     <!-- /.box-body -->
                  </div>
                  <!-- /.box -->
               </div>
            </div>

            <!--FIN TERCERA FILA-->
<script>
    function cargaDetAtenciones(vanio, vmes, vdia, vturno, vplaza)
    {
        window.open("./Atencioneset.aspx","ventana1","width=120,height=300,scrollbars=NO")
    }
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

        <div class="modal modal-success fade" id="modalStkVto">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Detalle de Stock de Productos</h4>
              </div>
              <div class="modal-body">
                <%--<p>One fine body&hellip;</p>--%>
                  <div id="divDetStkVto"></div>
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
          
        <div class="modal modal-success fade" id="modalSIGA1">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title"><div id="SIGA1Titulo"></div></h4>
              </div>
              <div class="modal-body">
                  <div id="divDetSIGA1"></div>
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

        <div class="modal modal-success fade" id="modalSIGA2">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title"><div id="SIGA2Titulo"></div></h4>
              </div>
              <div class="modal-body">
                  <div id="divDetSIGA2"></div>
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

        <div class="modal modal-success fade" id="mDetMed">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title"><div id="DetMedTitulo"></div></h4>
              </div>
              <div class="modal-body">
                  <div id="divDetMed"></div>
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

<%--        <div class="modal modal-warning fade" id="modalDetDx">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Detalle de la Atencion</h4>
              </div>
              <div class="modal-body">
                <p>One fine body&hellip;</p>
                  <div id="divDetDx"></div>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-outline pull-left" data-dismiss="modal">Close</button>
              </div>
            </div>
            <!-- /.modal-content -->
          </div>
          <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->--%>

<%--        <div class="modal modal-warning fade" id="modalShowProf">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Profesionales</h4>
              </div>
              <div class="modal-body">
                <p>One fine body&hellip;</p>
                  <div id="divShowProf"></div>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-outline pull-left" data-dismiss="modal">Close</button>
              </div>
            </div>
            <!-- /.modal-content -->
          </div>
          <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->--%>


      </div>
      <!-- /.content-wrapper -->
   </form>
    <script>
        //document.getElementById('LiCIEIm').className = "treeview menu-open";
        //$('#LiCIEIm').addClass('menu-open');
        document.getElementById("LiSISMEDI").classList.add('menu-open');
        document.getElementById('UlSISMED').style.display = 'block';
        document.getElementById("ulDispoIRO2").classList.add('active');
        var f = new Date();

    </script>
    <script>
        $(document).ready(function() {
            $('#tblbscrJS').DataTable( {
                "columnDefs": [
                    {
                        "targets": [ 8 ],
                        "visible": false,
                        "searchable": false
                    },
                    {
                        "targets": [ 9 ],
                        "visible": false
                    }
                ]
            } );
        } );
    </script>
</asp:Content>

