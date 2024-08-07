<%@ Page Language="C#" MasterPageFile="../../MasterPage.master" AutoEventWireup="true" CodeFile="ProgPerAdicional.aspx.cs" Inherits="ASPX_IROJVAR_ProgCupos_ProgPerAdicional" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>Optica</title>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/json2.js")%>"></script>
    <%--<link rel="stylesheet" media="screen" href="style1.css?id=1">--%>

    <script language="javascript" type="text/javascript">

        function btnGuardar() {
            var params = new Object();
            //idProgPer, DNIPac, PPFechaCupos, PPAEstado
            params.idProgPer = document.getElementById("<%=txtidProgPer.ClientID%>").value;
            params.DNIPac = document.getElementById("<%=txtPacDNI.ClientID%>").value;
            params.PPFechaCupos = document.getElementById("<%=txtFecha.ClientID%>").value;
			params.PPAEstado = "Activo";
			params = JSON.stringify(params);

			$.ajax({
                type: "POST", url: "ProgPerAdicional.aspx/SetBtnGuardar", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) {
                    $("#divmsj2").html(result.d);
                    alert(result.d);
                    //location.reload();
                }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divmsj3").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});
        }
         
        function btnBuscarServicio() {
            var params = new Object();
            params.vCodPer = document.getElementById("<%=lblPerCod.ClientID%>").innerHTML;
			params = JSON.stringify(params);

			$.ajax({
                type: "POST", url: "ProgPerAdicional.aspx/GetBtnBuscarServ", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divShowServicio").html(result.d); }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divShowServicio").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});
        }

        function fSelServicio(idProgPer, COD_SER, DSC_SER, Tur_Ser, PPFechaCupos) {
            document.getElementById("<%=txtidProgPer.ClientID%>").value = idProgPer;
            document.getElementById("<%=txtCodServicio.ClientID%>").value = COD_SER;
            document.getElementById("<%=txtServicio.ClientID%>").value = DSC_SER;
            document.getElementById("<%=txtTurno.ClientID%>").value = Tur_Ser;
            document.getElementById("<%=txtFecha.ClientID%>").value = PPFechaCupos;
        }
        
        function btnListarAdicional() {
            var params = new Object();
            params.idProgPer = document.getElementById("<%=txtidProgPer.ClientID%>").value;
			params = JSON.stringify(params);

			$.ajax({
                type: "POST", url: "ProgPerAdicional.aspx/GetBtnListarAdic", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divTabla1").html(result.d); }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divTabla1").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});
        }

    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div class="col-xs-8 col-xs-offset-2">
        <p class="cazador2">Programacion - Personal</p>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

    <form id="form1" runat="server" autocomplete="off">
      <!-- Content Wrapper. Contains page content -->
      <div class="content-wrapper">
         <!-- Content Header (Page header) -->
         <!-- Main content -->
         <section class="content">

             <div class="row" style="display:block; background-color:#4dd2ff">

                <div class="column">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:Label ID="lblPerCod" runat="server" class='form-control'></asp:Label>
                        </div>
                     </div>
                  </div>
               </div>
                <div class="column">
                  <div class="col-md-4" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:Label ID="lblPerNom" runat="server" class='form-control'></asp:Label>
                        </div>
                     </div>
                  </div>
               </div>

             </div>

<script>
    function fAnioMes(vfecha) {
        let fecha = new date(vfecha.value);

        alert(fecha.getMonth() + 1);
        document.getElementById("<%=txtAnio1.ClientID%>").value = vfecha.getFullYear();
        document.getElementById("<%=txtMes1.ClientID%>").value = vfecha.getMonth() + 1;
    }
</script>

            <div class="row" style="display:block; background-color:#4dd2ff">
                <div class="column">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:TextBox ID="txtFecha" runat="server" class='form-control' onchange="fAnioMes(this)" placeholder="dd/mm/aaaa"></asp:TextBox>
                            <asp:HiddenField ID="txtAnio1" runat="server" />
                            <asp:HiddenField ID="txtMes1" runat="server" />
                            <code style="font-size:large; background-color:transparent; ">Fecha</code>
                        </div>
                     </div>
                  </div>
                </div>

                <div class="column">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:TextBox ID="txtServicio" runat="server" class='form-control'></asp:TextBox>
                            <asp:HiddenField ID="txtCodServicio" runat="server" />
                            <code style="font-size:large; background-color:transparent; ">Servicio</code>
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                           <asp:TextBox ID="txtTurno" runat="server" class='form-control'></asp:TextBox>
                            <code style="font-size:large; background-color:transparent; ">Turno</code>
                        </div>
                     </div>
                  </div>
               </div>

             </div>

             <div class="row" style="display:block; background-color:#4dd2ff">
                <div class="column">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <button type="button" class="btn btn-success" data-toggle="modal" data-target="#mBServicio" onclick="btnBuscarServicio()"><i class="fa fa-fw fa-search"></i> Buscar Servicio y Turno</button>
                            <asp:HiddenField ID="txtidProgPer" runat="server" />
                        </div>
                     </div>
                  </div>
                </div>

                <div class="column">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                           <asp:TextBox ID="txtPacDNI" runat="server" class='form-control'></asp:TextBox>
                            <code style="font-size:large; background-color:transparent; ">DNI Paciente</code>
                        </div>
                     </div>
                  </div>
                </div>

                <div class="column">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <button type="button" class="btn btn-primary" onclick= "btnGuardar()"><i class="fa fa-fw fa-user-plus"></i> Añadir Adicional</button>
                        </div>
                     </div>
                  </div>
                </div>

             </div>


            <!--SEGUNDA FILA-->             
			 <div class="row" style="display:block;">
                <div class="column">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <button type="button" class="btn btn-info" onclick="btnListarAdicional()"><i class="fa fa-fw fa-search"></i> Listar Adicionales</button>
                        </div>
                     </div>
                  </div>
                </div>
				 <br />
			</div>

			<!-- CUARTA FILA -->
            <div class="row">
               <div class="col-md-12">
                  <div class="box" >
                     <div class="box-body table-responsive no-padding" id="divTabla1">
                        <asp:GridView ID="GVtable" runat="server" class="table table-condensed table-bordered"></asp:GridView>
                        <asp:Literal ID="LitTABL1" runat="server"></asp:Literal>
                     </div>
                  </div>
               </div>
            </div>			


<script>

	var inputText = document.getElementById("txtbuscaprod");
	inputText.addEventListener("keyup", function(event) {
      if (event.keyCode === 13) {
		  event.preventDefault();
		  document.getElementById("btnBuscarProd").click();
      }
	});
    


</script>

            <!-- TERCERA FILA -->
            

            <!--FIN TERCERA FILA-->

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
		  <div id="divmsj1"></div><div id="divmsj2"></div><div id="divmsj3"></div><div id="divmsj4"></div>
		  <div id="divmsj5"></div><div id="divmsj6"></div><div id="divmsj7"></div><div id="divmsj8"></div>
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


        <div class="modal modal-warning fade" id="mBServicio">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Buscar Servicio y Turno</h4>
              </div>
              <div class="modal-body">
                  <div id="divShowServicio"></div>
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

        <div class="modal modal-warning fade" id="mBPersonal">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Buscar Personal Asistencial</h4>
              </div>
              <div class="modal-body">
                  <div id="divShowPersonal"></div>
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
        document.getElementById("LiPROGRAMACION").classList.add('menu-open');
        document.getElementById('UlProgramacion').style.display = 'block';
        document.getElementById("liPAdicional").classList.add('active');
        var f = new Date();

    </script>
</asp:Content>
