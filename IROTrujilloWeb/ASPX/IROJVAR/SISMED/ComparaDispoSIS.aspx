<%@ Page Language="C#" MasterPageFile="../../MasterPage.master" AutoEventWireup="true" CodeFile="ComparaDispoSIS.aspx.cs" Inherits="ASPX_IROJVAR_SISMED_ComparaDispoSIS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>IRO - HISMINSA</title>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/json2.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/funciones.js?vfd=1")%>"></script>
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

    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div class="col-xs-8 col-xs-offset-2">
        <p class="cazador2">Busca Medicamento en Disponibilidad General</p>
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

               <div class="column hide">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:TextBox ID="txtDato" runat="server" class='form-control'></asp:TextBox>
                            <code style="font-size:large; background-color:transparent; ">Medicamento</code>
                        </div>
                     </div>
                  </div>
               </div>

<script>
    function fvaltxtdato() {

    }
</script> 

               <div class="column" style="display:block">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:LinkButton ID="bntBuscar" runat="server" class="btn btn-success" OnClick="bntBuscar_Click" onclientclick="return fvaltxtdato();" ><i class="fa fa-search"></i> Consultar</asp:LinkButton>
                        </div>
                     </div>
                  </div>
               </div>

               <!-- /.box-header -->
            </div>
            <!--FIN SEGUNDA FILA-->

<script>
	var inputText = document.getElementById("<%=txtDato.ClientID%>");
	inputText.addEventListener("keyup", function(event) {
      if (event.keyCode === 13) {
		  event.preventDefault();
		  document.getElementById("<%=bntBuscar.ClientID%>").click();
      }
    });

    function ddlSel1(ddl) {
        document.getElementById(ddl).value = "";
    }

</script>


            <div class="row">
               <div class="col-md-12">
                  <div class="box" >
                     <!-- /.box-header -->
                     <div class="box-body table-responsive no-padding" >
<table class="table table-condensed table-bordered" >
  <tr>
      <th class="box-body text-center">
            <input type="hidden" id="h1Disp" value="" />
            <input type="hidden" id="h1NS" value="NORMOST" /> 
            <input type="hidden" id="h1Sob" value="SOBRE S" /> 
            <input type="hidden" id="h1Sub" value="SUBSTOC" /> 
            <input type="hidden" id="h1SR" value="SIN ROTACION" /> 
            <input type="hidden" id="h1Des" value="DESABAS" /> 
            <input type="hidden" id="h1NCo" value="NoConsid" /> 

            <input type="hidden" id="h2Disp" value="" />
            <input type="hidden" id="h2NS" value="NORMOST" /> 
            <input type="hidden" id="h2Sob" value="SOBREST" /> 
            <input type="hidden" id="h2Sub" value="SUBSTOC" /> 
            <input type="hidden" id="h2SR" value="SIN ROT" /> 
            <input type="hidden" id="h2Des" value="DESABAS" /> 
            <input type="hidden" id="h2NCo" value="NO CONS" /> 
      </th>
      <th class="box-body text-center bg-aqua">% DISP</th> 
	  <th class="box-body text-center bg-green">NORMO STOCK</th>    
	  <th class="box-body text-center bg-yellow">SOBR - No Acep</th> 
	  <th class="box-body text-center bg-red">SUB - Critica</th>
	  <th class="box-body text-center bg-green">SR</th> 
	  <th class="box-body text-center bg-red">DES</th>
	  <th class="box-body text-center bg-aqua">NoCons</th>
  </tr>
  <tr>
      <td class="box-body text-center">SIS %</td>
    <td class="box-body text-center text-aqua " onclick="fBscTblHTML('h1Disp', '<%=GVtable.ClientID%>', 7)"> <asp:Label ID="lbl1Disp" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-green" onclick="fBscTblHTML('h1NS', '<%=GVtable.ClientID%>', 7)"> <asp:Label ID="lbl1NS" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-yellow" onclick="fBscTblHTML('h1Sob', '<%=GVtable.ClientID%>', 7)"> <asp:Label ID="lbl1Sob" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-red" onclick="fBscTblHTML('h1Sub', '<%=GVtable.ClientID%>', 7)"> <asp:Label ID="lbl1Sub" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-green"onclick="fBscTblHTML('h1SR', '<%=GVtable.ClientID%>', 7)"> <asp:Label ID="lbl1SR" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-red"onclick="fBscTblHTML('h1Des', '<%=GVtable.ClientID%>', 7)"> <asp:Label ID="lbl1Des" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-aqua"onclick="fBscTblHTML('h1NCo', '<%=GVtable.ClientID%>', 7)"> <asp:Label ID="lbl1NCo" runat="server" ></asp:Label> </td>
  </tr>
  <tr>
      <td class="box-body text-center">SIS Conteo</td>
    <td class="box-body text-center text-aqua " onclick="fBscTblHTMLOrden('h1Disp', '<%=GVtable.ClientID%>', 7)"> <asp:Label ID="lbl1DispC" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-green" onclick="fBscTblHTMLOrden('h1NS', '<%=GVtable.ClientID%>', 7)"> <asp:Label ID="lbl1NSC" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-yellow" onclick="fBscTblHTMLOrden('h1Sob', '<%=GVtable.ClientID%>', 7)"> <asp:Label ID="lbl1SobC" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-red" onclick="fBscTblHTMLOrden('h1Sub', '<%=GVtable.ClientID%>', 7)"> <asp:Label ID="lbl1SubC" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-green"onclick="fBscTblHTMLOrden('h1SR', '<%=GVtable.ClientID%>', 7)"> <asp:Label ID="lbl1SRC" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-red"onclick="fBscTblHTMLOrden('h1Des', '<%=GVtable.ClientID%>', 7)"> <asp:Label ID="lbl1DesC" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-aqua"onclick="fBscTblHTMLOrden('h1NCo', '<%=GVtable.ClientID%>', 7)"> <asp:Label ID="lbl1NCoC" runat="server" ></asp:Label> </td>
  </tr>

  <tr>
      <td class="box-body text-center">IRO %</td>
    <td class="box-body text-center text-aqua " onclick="fBscTblHTML('h2Disp', '<%=GVtable.ClientID%>', 19)"> <asp:Label ID="lbl2Disp" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-green" onclick="fBscTblHTML('h2NS', '<%=GVtable.ClientID%>', 19)"> <asp:Label ID="lbl2NS" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-yellow" onclick="fBscTblHTML('h2Sob', '<%=GVtable.ClientID%>', 19)"> <asp:Label ID="lbl2Sob" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-red" onclick="fBscTblHTML('h2Sub', '<%=GVtable.ClientID%>', 19)"> <asp:Label ID="lbl2Sub" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-green"onclick="fBscTblHTML('h2SR', '<%=GVtable.ClientID%>', 19)"> <asp:Label ID="lbl2SR" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-red"onclick="fBscTblHTML('h2Des', '<%=GVtable.ClientID%>', 19)"> <asp:Label ID="lbl2Des" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-aqua"onclick="fBscTblHTML('h2NCo', '<%=GVtable.ClientID%>', 19)"> <asp:Label ID="lbl2NCo" runat="server" ></asp:Label> </td>
  </tr>
  <tr>
      <td class="box-body text-center">IRO Conteo</td>
    <td class="box-body text-center text-aqua " onclick="fBscTblHTMLOrden('h2Disp', '<%=GVtable.ClientID%>', 19)"> <asp:Label ID="lbl2DispC" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-green" onclick="fBscTblHTMLOrden('h2NS', '<%=GVtable.ClientID%>', 19)"> <asp:Label ID="lbl2NSC" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-yellow" onclick="fBscTblHTMLOrden('h2Sob', '<%=GVtable.ClientID%>', 19)"> <asp:Label ID="lbl2SobC" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-red" onclick="fBscTblHTMLOrden('h2Sub', '<%=GVtable.ClientID%>', 19)"> <asp:Label ID="lbl2SubC" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-green"onclick="fBscTblHTMLOrden('h2SR', '<%=GVtable.ClientID%>', 19)"> <asp:Label ID="lbl2SRC" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-red"onclick="fBscTblHTMLOrden('h2Des', '<%=GVtable.ClientID%>', 19)"> <asp:Label ID="lbl2DesC" runat="server" ></asp:Label> </td>
    <td class="box-body text-center text-aqua"onclick="fBscTblHTMLOrden('h2NCo', '<%=GVtable.ClientID%>', 19)"> <asp:Label ID="lbl2NCoC" runat="server" ></asp:Label> </td>
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
                        <div class="box-header">
                            <h3 class="box-title" style="margin-top: -6px; margin-bottom: -11px; margin-left: -7px;">

                            <div class="input-group margin">
                                <div class="input-group-btn">
                                    <button class="btn btn-default" type="button" onclick="exportTableToExcel('ContentPlaceHolder2_GVtable')"><i class="fa fa-download "></i>
                                    </button>
                                </div>
                                <div>
                                    <input type="text" class="form-control" id="bscprod2" placeholder="Buscar Nombre de Producto" onkeyup="fBscTblHTML('bscprod2', 'ContentPlaceHolder2_GVtable', 2)" autofocus="autofocus">
                                </div>
                            </div>

                            </h3>
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

        <div class="modal modal-success fade" id="modalDetAtenciones">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Detalle de la Atencion</h4>
              </div>
              <div class="modal-body">
                <%--<p>One fine body&hellip;</p>--%>
                  <div id="divDetAtencion"></div>
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
          
        <div class="modal modal-warning fade" id="modalDetDx">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Detalle de la Atencion</h4>
              </div>
              <div class="modal-body">
                <%--<p>One fine body&hellip;</p>--%>
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
        <!-- /.modal -->

        <div class="modal modal-warning fade" id="modalShowProf">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Profesionales</h4>
              </div>
              <div class="modal-body">
                <%--<p>One fine body&hellip;</p>--%>
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
        <!-- /.modal -->


      </div>
      <!-- /.content-wrapper -->
   </form>
</asp:Content>
