<%@ Page Language="C#" MasterPageFile="../../MasterPage.master" AutoEventWireup="true" CodeFile="DispoIROFechas.aspx.cs" Inherits="ASPX_IROJVAR_SISMED_DispoIROFechas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>IRO - HISMINSA</title>
    <link rel="stylesheet" href="../../Estilos/jquery.dataTables.min.css">
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/json2.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/funciones.js?vfd=2")%>"></script>
    <%--<link rel="stylesheet" media="screen" href="style1.css?id=1">--%>

    <script language="javascript" type="text/javascript">

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
        <p class="cazador2">Disponibilidad</p>
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
                           <asp:DropDownList ID="DDLDesdeXXX" runat="server" class='form-control hide' >
                           </asp:DropDownList>
                            <asp:TextBox ID="txtDesde" runat="server" Type="date" class='form-control' ></asp:TextBox>
                            <code style="font-size:large; background-color:transparent; ">Desde</code>
                        </div>
                     </div>
                  </div>
               </div>
        <asp:HiddenField ID="txtCantMeses" runat="server" value="1" />
               <div class="column">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                           <asp:DropDownList ID="DDLHastaXXX" runat="server" class='form-control hide' onChange="fCantMeses()">
                           </asp:DropDownList>
                            <asp:TextBox ID="txtHasta" runat="server" Type="date" class='form-control' ></asp:TextBox>
                            <code style="font-size:large; background-color:transparent; ">Hasta</code>
                        </div>
                     </div>
                  </div>
               </div>

        <script>
            function validaDesdeHasta() {
                if (document.getElementById("<%=txtDesde.ClientID%>").value > document.getElementById("<%=txtHasta.ClientID%>").value) {
                    alert("Periodos: 'Desde' debe ser menor");
                    return false;
                }
                else {
                    return true;
                }
            }

            function fCantMeses() {
                //formato fecha sql: DD/MM/AAAA
                //formato fecha JS:  MM/DD/AAAA
                //formato sql fecha: AAAA/MM/DD (compatible para ambos)

                var fecdesde = new Date(document.getElementById("<%=txtDesde.ClientID%>").value);
                var fechasta = new Date(document.getElementById("<%=txtHasta.ClientID%>").value);

                var fecDifSegundos= (fechasta - fecdesde);
                //var fecDifSegundos = (fechasta.getDate() - fecdesde.getDate());
                var fecDias = fecDifSegundos / (1000 * 3600 * 24);
                var fecMeses = Math.round(fecDias / (30)) + 1;

                document.getElementById("<%=txtCantMeses.ClientID%>").value = fecMeses;

                //alert(fecdesde + " ------ " + fechasta);
            }
        </script>

               <div class="column" style="display:block">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:LinkButton ID="bntBuscar" runat="server" class="btn btn-success" OnClick="bntBuscar_Click" OnClientClick="return validaDesdeHasta()"><i class="fa fa-search"></i> Consultar</asp:LinkButton>
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

               <!-- /.box-header -->
            </div>
            <!--FIN SEGUNDA FILA-->

<script>

</script>

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
	  <th class="box-body text-center bg-green">SR <input type="hidden" id="hSR" value="SINROTACION" /> </th> 
	  <th class="box-body text-center bg-red">DES <input type="hidden" id="hDes" value="DESABASTECIDO" /> </th>
	  <th class="box-body text-center bg-aqua hide">NoCons <input type="hidden" id="hNCo" value="NoConsid" /> </th>
  </tr>
  <tr>
    <td class="box-body text-center text-aqua " onclick="fxBscTblHTML('hDisp', 'tblbscrJS')"> <asp:Label ID="lblDisp" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-green" onclick="fxBscTblHTML('hNS', 'tblbscrJS')"> <asp:Label ID="lblNS" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-yellow" onclick="fxBscTblHTML('hSob', 'tblbscrJS')"> <asp:Label ID="lblSob" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-red" onclick="fxBscTblHTML('hSub', 'tblbscrJS')"> <asp:Label ID="lblSub" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-green"onclick="fxBscTblHTML('hSR', 'tblbscrJS')"> <asp:Label ID="lblSR" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-red"onclick="fxBscTblHTML('hDes', 'tblbscrJS')"> <asp:Label ID="lblDes" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-aqua hide"onclick="fxBscTblHTML('hNCo', 'tblbscrJS')"> <asp:Label ID="lblNCo" runat="server" ></asp:Label> </td>
  </tr>
  <tr>
    <td class="box-body text-center text-aqua" onclick="fxBscTblHTML('hDisp', 'tblbscrJS')"> <asp:Label ID="lblDispC" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-green" onclick="fxBscTblHTML('hNS', 'tblbscrJS')"> <asp:Label ID="lblNSC" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-yellow" onclick="fxBscTblHTML('hSob', 'tblbscrJS')"> <asp:Label ID="lblSobC" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-red" onclick="fxBscTblHTML('hSub', 'tblbscrJS')"> <asp:Label ID="lblSubC" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-green" onclick="fxBscTblHTML('hSR', 'tblbscrJS')"> <asp:Label ID="lblSRC" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-red" onclick="fxBscTblHTML('hDes', 'tblbscrJS')"> <asp:Label ID="lblDesC" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-aqua hide" onclick="fxBscTblHTML('hNCo', 'tblbscrJS')"> <asp:Label ID="lblNCoC" runat="server" ></asp:Label> </td>
  </tr>
</table>
    <script>
        function fxBscTblHTML(vSSO, vtbl) {
            vCantMes = document.getElementById("<%=txtCantMeses.ClientID%>").value * 1; 
            //alert(6 + vCantMes);
            fBscTblHTMLOrden(vSSO, vtbl, 6 + vCantMes);
        }
        
    </script>
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
                        <asp:GridView ID="GVtable" runat="server" class="table table-condensed table-bordered"></asp:GridView>
                        <asp:Literal ID="LitTABL1" runat="server"></asp:Literal>
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


      </div>
      <!-- /.content-wrapper -->
   </form>
    <script>
        //document.getElementById('LiCIEIm').className = "treeview menu-open";
        //$('#LiCIEIm').addClass('menu-open');
        document.getElementById("LiSISMEDI").classList.add('menu-open');
        document.getElementById('UlSISMED').style.display = 'block';
        document.getElementById("ulDispoIROFec").classList.add('active');
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
