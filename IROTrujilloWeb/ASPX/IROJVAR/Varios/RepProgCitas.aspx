﻿<%@ Page Language="C#" MasterPageFile="../../MasterPage.master" AutoEventWireup="true" CodeFile="RepProgCitas.aspx.cs" Inherits="ASPX_IROJVAR_Varios_RepProgCitas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>IRO - HISMINSA</title>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/json2.js")%>"></script>
    <%--<link rel="stylesheet" media="screen" href="style1.css?id=1">--%>

    <script language="javascript" type="text/javascript">

        function DetAtenciones(vanio, vmes, vcodserv, vservicio) {
            var params = new Object();
            params.vanio = vanio; // cambiar la descripcion del params y el ddl
            params.vmes = vmes; 
            params.vcodserv = vcodserv; 
            params.vservicio = vservicio; 
            params = JSON.stringify(params);

            $("#divAtencion").html("");
            $.ajax({
                type: "POST", url: "RepProgCitas.aspx/GetDetAtenciones", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) {
                    var aResult = result.d.split("||sep||");
                    $("#divmod01").html(aResult[1]);
                    $("#divAtencion").html(aResult[0]);

                }, //success: LoadPrueba01, //Procesar 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divAtencion").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });
        }

        function DetAtenCli(vanio, vmes, vdia, vcodserv, vservicio, vcodplaza) {
            var params = new Object();
            params.vanio = vanio; // cambiar la descripcion del params y el ddl
            params.vmes = vmes; 
            params.vdia = vdia; 
            params.vcodserv = vcodserv; 
            params.vservicio = vservicio; 
            params.vcodplaza = vcodplaza; 
            params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "RepProgCitas.aspx/GetDetAtenCli", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divAtenCli").html(result.d) }, //success: LoadPrueba01, //Procesar 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divAtenCli").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });
        }

        function DetProfesional(vanio, vmes, vcodserv, vservicio) {
            var params = new Object();
            params.vanio = document.getElementById("<%=DDLAnio.ClientID%>").value; 
            params.vmes = document.getElementById("<%=DDLMes.ClientID%>").value;  
            params.vcodserv = document.getElementById("<%=ddlServicio.ClientID%>").value;  
            params.vservicio = document.getElementById("<%=ddlServicio.ClientID%>").options[document.getElementById("<%=ddlServicio.ClientID%>").selectedIndex].text;
            params = JSON.stringify(params);

            $("#divAtencion").html("");
            $.ajax({
                type: "POST", url: "RepProgCitas.aspx/GetDetProfesional", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) {
                    var aResult = result.d.split("||sep||");
                    $("#divmod01").html(aResult[1]);
                    $("#divAtencion").html(aResult[0]);
                }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divAtencion").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });
        }

        function fXservXprofXdia(vanio, vmes, vdia, vcodserv, vservicio, vcodplaza) {
            var params = new Object();
            params.vanio = vanio; // cambiar la descripcion del params y el ddl
            params.vmes = vmes; 
            params.vcodserv = vcodserv; 
            params.vservicio = vservicio; 
            params.vplaza = vcodplaza; 
            params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "RepProgCitas.aspx/GetDetServXProfXDia", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { 
                    //$("#divmtXservXprofXdia").html(result.d) 
                    var aResult = result.d.split("||sep||");
                    $("#divmtXservXprofXdia").html(aResult[1]);
                    $("#divmbXservXprofXdia").html(aResult[0]);
                }, //success: LoadPrueba01, //Procesar 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divmbXservXprofXdia").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });
        }


        function DetDx(dat1) {
            var params = new Object();
            params.dat1 = dat1;
            params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "RepProgCitas.aspx/GetDetDx", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divDetDx").html(result.d) }, //success: LoadPrueba01, //Procesar 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divDetDx").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });
        }

    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div class="col-xs-8 col-xs-offset-2">
        <p class="cazador2">Reporte de Citas Programadas</p>
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
                              <asp:ListItem Value="2020" >2020</asp:ListItem>
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

               <div class="column hide">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                           <asp:DropDownList ID="ddlTurno" runat="server" class='form-control' >
                              <asp:ListItem Value="M">M</asp:ListItem>
                              <asp:ListItem Value="T">T</asp:ListItem>
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
                              <asp:ListItem Value="0">0</asp:ListItem>
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
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                           <asp:DropDownList ID="ddlServicio" runat="server" class='form-control' >
                            <asp:ListItem Value="04">BAJA VISION</asp:ListItem>
                            <asp:ListItem Value="20">CATARATA</asp:ListItem>
                            <asp:ListItem Value="21">CORNEA</asp:ListItem>
                            <asp:ListItem Value="75">EKG + CMEC</asp:ListItem>
                            <asp:ListItem Value="39">EMERGENCIA</asp:ListItem>
                            <asp:ListItem Value="72">ENFERMERIA</asp:ListItem>
                            <asp:ListItem Value="23">GLAUCOMA</asp:ListItem>
                            <asp:ListItem Value="24">NEURO</asp:ListItem>
                            <asp:ListItem Value="25">OCULO PLASTICA</asp:ListItem>
                            <asp:ListItem Value="26">OFT. PED. Y ESTRABISMO</asp:ListItem>
                            <asp:ListItem Value="71">OFTALMOLOGIA GENERAL</asp:ListItem>
                            <asp:ListItem Value="03">PROGRAMACION CATARATA</asp:ListItem>
                            <asp:ListItem Value="69">PROGRAMACION PEDIATRIA</asp:ListItem>
                            <asp:ListItem Value="37">PROGRAMACION RETINA</asp:ListItem>
                            <asp:ListItem Value="46">REFRACCION</asp:ListItem>
                            <asp:ListItem Value="32">REFRACCION CATARATA</asp:ListItem>
                            <asp:ListItem Value="31">REFRACCION PEDIATRICA</asp:ListItem>
                            <asp:ListItem Value="28">RETINA</asp:ListItem>
                            <asp:ListItem Value="06">SERVICIO SOCIAL</asp:ListItem>
                            <asp:ListItem Value="5S">SST</asp:ListItem>
                            <asp:ListItem Value="81">TELESALUD</asp:ListItem>
                            <asp:ListItem Value="29">UVEITIS</asp:ListItem>
                           </asp:DropDownList>
                        </div>
                     </div>
                  </div>
               </div>
                
                <div class="column hide">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                           <asp:DropDownList ID="ddlPersonal" runat="server" class='form-control' >
                            <asp:ListItem Value="000">Prof. Salud</asp:ListItem>
                            <asp:ListItem Value="045">Rioja Garcia</asp:ListItem>
                            <asp:ListItem Value="058">Barba Chirinos</asp:ListItem>
                            <asp:ListItem Value="064">Acosta Pretell</asp:ListItem>
                            <asp:ListItem Value="172">Molina Socola</asp:ListItem>
                            
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

               <!-- /.box-header -->
            </div>
            <!--FIN SEGUNDA FILA-->

<script>


</script>

            <!-- TERCERA FILA -->
            <div class="row">
               <div class="col-md-12">
                  <div class="box" >
                        <div class="box-header">
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
                        </div>
                     <!-- /.box-header -->
                     <div class="box-body table-responsive no-padding" id="habfiltro">
<table class="table table-hover" style="text-align: right; font-size: 14px; ">
	<caption>Listado</caption>
<tbody>
<tr>
	<th class="text-center">Turno</th>
	<th class="text-center">Cant. Cupos</th>
    <th class="text-center">Cant. Citados</th>
	<th class="text-center">Cant. Atendidos</th>
	<th class="text-center">Dif. Citados - Atendidos</th>
	<th class="text-center">Dif. Cupos - Citados</th>
	<th class="text-center">Det</th>
</tr>
<tr>
	<td class="text-center"><span runat="server" ID="TurnoM">M</span></td>
	<td class="text-center"><span runat="server" ID="CuposM"></span></td>
	<td class="text-center"><span runat="server" ID="CitadM"></span></td>
	<td class="text-center"><span runat="server" ID="AtendM"></span></td>
	<td class="text-center"><span runat="server" ID="CitAteM"></span></td>
	<td class="text-center"><span runat="server" ID="CupCitM"></span></td>
	<td class="text-center"><span runat="server" ID="DetM"></span></td>
</tr>
<tr>
	<td class="text-center"><span runat="server" ID="TurnoT">T</span></td>
	<td class="text-center"><span runat="server" ID="CuposT"></span></td>
	<td class="text-center"><span runat="server" ID="CitadT"></span></td>
	<td class="text-center"><span runat="server" ID="AtendT"></span></td>
	<td class="text-center"><span runat="server" ID="CitAteT"></span></td>
	<td class="text-center"><span runat="server" ID="CupCitT"></span></td>
	<td class="text-center"><span class="hide" runat="server" ID="DetT"></span></td>
</tr>
</tbody>
</table>

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

</script>

         </section>
          <div id="divAtenciones">
              <asp:Literal ID="LitAtenciones" runat="server"></asp:Literal>
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

        <div class="modal modal-success fade" id="modalAtenciones">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title"><div id="divmod01"></div></h4>
              </div>
              <div class="modal-body">
                <%--<p>One fine body&hellip;</p>--%>
                  <div id="divAtencion"></div>
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


        <div class="modal modal-info fade" id="mXServXProfXDia">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title"><div id="divmtXservXprofXdia"></div></h4>
              </div>
              <div class="modal-body">
                <%--<p>One fine body&hellip;</p>--%>
                  <div id="divmbXservXprofXdia"></div>
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

          
        <div class="modal modal-warning fade" id="modalAtenCli">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Atenciones por Cliente</h4>
              </div>
              <div class="modal-body">
                <%--<p>One fine body&hellip;</p>--%>
                  <div id="divAtenCli"></div>
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
          
        <div class="modal modal-warning fade" id="modDetProgCit">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Detalle de Programacion de Citas</h4>
              </div>
              <div class="modal-body">
                <asp:GridView ID="GVDetProgCit" runat="server" class="table table-condensed table-bordered"></asp:GridView>
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
          
        <div class="modal modal-danger fade" id="modalDetDx">
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



      </div>
      <!-- /.content-wrapper -->
   </form>
    <script>
        //document.getElementById('LiCIEIm').className = "treeview menu-open";
        //$('#LiCIEIm').addClass('menu-open');
        document.getElementById("LiVariosI").classList.add('menu-open');
        document.getElementById('UlVarios').style.display = 'block';
        document.getElementById("ulRepProgCit01").classList.add('active');
        var f = new Date();

    </script>
</asp:Content>

