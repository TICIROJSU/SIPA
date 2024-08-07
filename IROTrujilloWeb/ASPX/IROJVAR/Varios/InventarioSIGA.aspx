<%@ Page Language="C#" MasterPageFile="../../MasterPage.master" AutoEventWireup="true" CodeFile="InventarioSIGA.aspx.cs" Inherits="ASPX_IROJVAR_Varios_InventarioSIGA" %>

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

            $.ajax({
                type: "POST", url: "AtencXservic3.aspx/GetDetAtenciones", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divAtencion").html(result.d) }, //success: LoadPrueba01, //Procesar 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divAtencion").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });
        }

        function DetAtenCli(vanio, vmes, vdia, vcodserv, vservicio) {
            var params = new Object();
            params.vanio = vanio; // cambiar la descripcion del params y el ddl
            params.vmes = vmes; 
            params.vdia = vdia; 
            params.vcodserv = vcodserv; 
            params.vservicio = vservicio; 
            params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "AtencXservic3.aspx/GetDetAtenCli", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divAtenCli").html(result.d) }, //success: LoadPrueba01, //Procesar 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divAtenCli").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });
        }

    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div class="col-xs-8 col-xs-offset-2">
        <p class="cazador2">Citas - HISMINSA - Citas No Atendidas</p>
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
                  <div class="col-md-8" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
							<label>EESS: </label>
                            <asp:TextBox ID="txtEESS" runat="server" Text="05197 - IRO" class='form-control' ></asp:TextBox>
                        </div>
                     </div>
                  </div>
               </div>

				<div class="column">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
							<label>Fecha: </label>
                            <asp:TextBox ID="txtFechaDoc" runat="server" class='form-control' type="date"></asp:TextBox>
                        </div>
                     </div>
                  </div>
               </div>


               <div class="column">
                  <div class="col-md-12" style="text-align: left;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
						 <asp:Label ID="lblResponsable" runat="server" Text="USUARIO RESPONSABLE"></asp:Label>
                     </div>
                  </div>
               </div>

				<div class="row">
               <div class="column">
                  <div class="col-md-2" style="text-align: right;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
							DNI: 
                        </div>
                     </div>
                  </div>
               </div>
               <div class="column">
                  <div class="col-md-3" style="text-align: left;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
							<asp:TextBox ID="txtDNI" runat="server" class='form-control'></asp:TextBox>
                        </div>
                     </div>
                  </div>
               </div>
				

               <div class="column" style="display:block">
                  <div class="col-md-2" style="text-align: left;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:LinkButton ID="bntBuscar" runat="server" class="btn btn-success" OnClick="bntBuscar_Click" ><i class="fa fa-search"></i> Consultar</asp:LinkButton>
                        </div>
                     </div>
                  </div>
               </div>
				</div>

				
				<div class="row">
               <div class="column">
                  <div class="col-md-2" style="text-align: right;padding:  0px 30px 0px 30px;">Apellidos y Nombres: </div>
               </div>
               <div class="column">
                  <div class="col-md-3" style="text-align: left;padding:  0px 30px 0px 30px;">
					<asp:TextBox ID="txtApeNom" runat="server" class='form-control'></asp:TextBox>
                  </div>
               </div>
				</div>

				<div class="row">
               <div class="column">
                  <div class="col-md-2" style="text-align: right;padding:  0px 30px 0px 30px;">Dependencia: </div>
               </div>
               <div class="column">
                  <div class="col-md-3" style="text-align: left;padding:  0px 30px 0px 30px;">
					<asp:TextBox ID="txtDependencia" runat="server" class='form-control'></asp:TextBox>
                  </div>
               </div>
				</div>

				<div class="row">
               <div class="column">
                  <div class="col-md-2" style="text-align: right;padding:  0px 30px 0px 30px;">Ubicacion:</div>
               </div>
               <div class="column">
                  <div class="col-md-3" style="text-align: left;padding:  0px 30px 0px 30px;">
					<asp:TextBox ID="txtUbicacion" runat="server" class='form-control'></asp:TextBox>
                  </div>
               </div>
				</div>
               
				<div class="row">
               <div class="column">
                  <div class="col-md-2" style="text-align: right;padding:  0px 30px 0px 30px;">Modalidad: </div>
               </div>
               <div class="column">
                  <div class="col-md-3" style="text-align: left;padding:  0px 30px 0px 30px;">
					<asp:DropDownList ID="DDLModalidad" runat="server" class='form-control'>
						<asp:ListItem Value="Funcionario" Selected="true">Funcionario</asp:ListItem>
						<asp:ListItem Value="CAP">CAP</asp:ListItem>
						<asp:ListItem Value="CAS">CAS</asp:ListItem>
						<asp:ListItem Value="Locacion">Locacion</asp:ListItem>
					</asp:DropDownList>
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
                <h4 class="modal-title">Atenciones por Dia</h4>
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
          

      </div>
      <!-- /.content-wrapper -->
   </form>
    <script>
        //document.getElementById('LiCIEIm').className = "treeview menu-open";
        //$('#LiCIEIm').addClass('menu-open');
        document.getElementById("LiVariosI").classList.add('menu-open');
        document.getElementById('UlVarios').style.display = 'block';
        document.getElementById("ulInvSIGA").classList.add('active');
        var f = new Date();
		<%--document.getElementById("<%=txtFechaDoc.ClientID%>").value = f.getFullYear() + "-" + (f.getMonth() +1) + "-" + f.getDate();--%>

    </script>
</asp:Content>


