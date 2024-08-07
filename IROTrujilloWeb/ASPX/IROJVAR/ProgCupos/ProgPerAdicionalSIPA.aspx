<%@ Page Language="C#" MasterPageFile="../../MasterPage.master" AutoEventWireup="true" CodeFile="ProgPerAdicionalSIPA.aspx.cs" Inherits="ASPX_IROJVAR_ProgCupos_ProgPerAdicionalSIPA" %>
<%@ MasterType VirtualPath="../../MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>Adicionales</title>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/json2.js")%>"></script>
    <%--<link rel="stylesheet" media="screen" href="style1.css?id=1">--%>

    <script language="javascript" type="text/javascript">
                 
        function btnBuscarServicio() {
            var params = new Object();
            params.vCodPer = document.getElementById("<%=lblPerCod.ClientID%>").innerHTML;
			params = JSON.stringify(params);

			$.ajax({
                type: "POST", url: "ProgPerAdicionalSIPA.aspx/GetBtnBuscarServ", data: params, 
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
        

        function fAdiPac(vFecha) {
            var params = new Object();
            params.vFecha = vFecha;
            params.vSer = document.getElementById("<%=ddlServicio.ClientID%>").value;
            params.vPer = document.getElementById("<%=lblPerCod.ClientID%>").innerHTML;
            params.vTur = document.getElementById("<%=ddlTurno.ClientID%>").value;
			params = JSON.stringify(params);

            document.getElementById("mAdiPacTitle").innerText = vFecha;
            $("#divListAdiPac").html("");

			$.ajax({
                type: "POST", url: "ProgPerAdicionalSIPA.aspx/GetAdiPac", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divListAdiPac").html(result.d); }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divListAdiPac").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});
        }


    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div class="col-xs-8 col-xs-offset-2">
        <p class="cazador2">Programacion - Adicionales</p>
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

            <div class="column">
                <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                    <div class="box-body">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlServicio" runat="server" class='form-control' >
                            <asp:ListItem Value="tmp">tmp</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    </div>
                </div>
            </div>

            <div class="column">
                <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                    <div class="box-body">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlTurno" runat="server" class='form-control' >
                            <asp:ListItem Value="M">Mañana</asp:ListItem>
                            <asp:ListItem Value="T">Tarde</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    </div>
                </div>
            </div>


             </div>


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
                        <asp:DropDownList ID="DDLAnio" runat="server" class='form-control' >
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
                        <asp:LinkButton ID="bntBuscar" runat="server" class="btn btn-info" OnClick="bntBuscar_Click" ><i class="fa fa-search"></i> Consultar</asp:LinkButton>
                    </div>
                    </div>
                </div>
            </div>

            <!-- /.box-header -->
        </div>
        <!--FIN SEGUNDA FILA-->


        <div class="row">
          <div class="box box-primary">
            <div class="box-body no-padding fc ">
				<div class="fc-toolbar fc-header-toolbar">
					<div class="fc-left">
						<div class="fc-button-group">
							<div class="btn btn-danger fc-corner-right" onclick="GPresencial(' ');" >opt1</div>
						</div>
					</div>
					<div class="fc-right">
						<div class="fc-button-group">
							<button type="button" class="fc-state-default fc-corner-left fc-state-active">opt2</button>
						</div>
					</div>
					<div class="fc-center">
						<h2><asp:Literal ID="LitPeriodo" runat="server"></asp:Literal></h2>
					</div>
					<div class="fc-clear"></div>
			  </div>

	<asp:Literal ID="LitCalendar" runat="server"></asp:Literal>

			</div>
		  </div>
		</div>


             <br />

<div class="row" style="display:block; ">
    <div class="col-md-3">
        <div class="btn btn-block bg-orange">
            <h4>Adicionales Citados</h4>
        </div>
    </div>
    <div class="col-md-3">
        <div class="btn btn-block btn-warning">
            <h4>Adicionales No Citados</h4>
        </div>
    </div>
    <div class="col-md-3">
        <div class="btn btn-block bg-purple">
            <h4>Citados</h4>
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

            <div class="row hide" style="display:block; background-color:#4dd2ff">
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

             <div class="row hide" style="display:block; background-color:#4dd2ff">
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
			 <div class="row hide" style="display:block;">
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

        <div class="modal modal-warning fade" id="mAdiPac">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Pacientes Citados por Adicional. Fecha: <spam id="mAdiPacTitle"></spam> </h4>
              </div>
              <div class="modal-body">
                  <div id="divListAdiPac"></div>
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
        document.getElementById("liPAdicionalS").classList.add('active');
        var f = new Date();

    </script>

<div class="hide">
    <asp:Label ID="lblidUser2" runat="server" ><%=Session["idUser2"].ToString()%></asp:Label>
</div>

</asp:Content>


