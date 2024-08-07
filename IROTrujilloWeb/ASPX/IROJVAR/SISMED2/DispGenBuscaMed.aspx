<%@ Page Language="C#" MasterPageFile="../../MasterPage.master" AutoEventWireup="true" CodeFile="DispGenBuscaMed.aspx.cs" Inherits="ASPX_IROJVAR_SISMED2_DispGenBuscaMed" %>

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
               <div class="column">
                  <div class="col-md-2 hide" style="text-align: center;padding:  0px 30px 0px 30px;">
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
                  <div class="col-md-2 hide" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                           <asp:DropDownList ID="DDLMes" runat="server" class='form-control'>
                              <asp:ListItem Value="0">Meses</asp:ListItem>
                              <asp:ListItem Value="1" Selected="true">Enero</asp:ListItem>
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


               <div class="column">
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
        if (document.getElementById("<%=txtDato.ClientID%>").value.length>=5) {
            return true;
        }
        else {
            alert("Al menos 5 Digitos o Letras para la Busqueda");
            return false;
        }
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

               <div class="column">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:DropDownList ID="ddlRed" runat="server" class='form-control' AppendDataBoundItems="true" onchange="ddlSel1('ContentPlaceHolder2_ddlSSO'); fBscTblHTML('ContentPlaceHolder2_ddlRed', 'ContentPlaceHolder2_GVtable', 0); " >
                                <asp:ListItem Value="">Todos</asp:ListItem>
                            </asp:DropDownList>
                            <code style="font-size:large; background-color:transparent; ">Red</code>
                        </div>
                     </div>
                  </div>
               </div>
               <div class="column">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:DropDownList ID="ddlSSO" runat="server" class='form-control' AppendDataBoundItems="true" onchange="ddlSel1('ContentPlaceHolder2_ddlRed'); fBscTblHTML('ContentPlaceHolder2_ddlSSO', 'ContentPlaceHolder2_GVtable', 21); " >
                                <asp:ListItem Value="">Todos</asp:ListItem>
                            </asp:DropDownList>
                            <code style="font-size:large; background-color:transparent; ">SSO</code>
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

            <!-- TERCERA FILA -->
            <div class="row">
               <div class="col-md-12">
                  <div class="box" >
                        <div class="box-header">
                            <h3 class="box-title" style="margin-top: -6px; margin-bottom: -11px; margin-left: -7px;">
                            <div class="input-group-btn">
<%--                                    <button class="btn btn-default" runat="server" type="button" title="Max.: 3,000 Registros" onserverclick="ExportarExcel_Click">
                                    <i class="fa fa-download "></i>
                                    </button>--%>
                                <button class="btn btn-default" type="button" title="Max.: 1,000 Registros" onclick="exportTableToExcel('ContentPlaceHolder2_GVtable')"><i class="fa fa-download "></i></button>
                            </div>
                            </h3>
                            <div class="box-tools">
                                <asp:Label ID="lblRuta" runat="server" Text="Descarga en: C:\"></asp:Label>
                                <div id="divtxtRuta"></div>
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
    <script>
        //document.getElementById('LiCIEIm').className = "treeview menu-open";
        //$('#LiCIEIm').addClass('menu-open');
        document.getElementById("LiSISMEDI").classList.add('menu-open');
        document.getElementById('UlSISMED').style.display = 'block';
        //document.getElementById("ulFarICI").classList.add('active');
        var f = new Date();

    </script>
</asp:Content>
