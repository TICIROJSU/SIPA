<%@ Page Language="C#" MasterPageFile="../../MasterPagePer.master" AutoEventWireup="true" CodeFile="VerRegRemoto.aspx.cs" Inherits="ASPX_PerIROJVAR_TrabPresencial_VerRegRemoto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>Optica</title>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/json2.js")%>"></script>
    <%--<link rel="stylesheet" media="screen" href="style1.css?id=1">--%>

    <script language="javascript" type="text/javascript">
        
		function fTrabDescr(vDia, vFechaTrab) {
			var vIDPER = document.getElementById("<%=txtIdPer.ClientID%>").value;
			var vDNIPER = document.getElementById("<%=txtDNI.ClientID%>").value;
			var vDatosPER = document.getElementById("<%=txtDatosPer.ClientID%>").value;
			document.getElementById("dmTrabDTitle").innerHTML = vFechaTrab + " | " + vDNIPER + "-" + vDatosPER;
			document.getElementById("inputDiaTrab").value = vDia;
			document.getElementById("<%=txtDiaSel.ClientID%>").value = vFechaTrab;

			var params = new Object();
            params.vIdPer = vIDPER;
            params.vDNIPer = vDNIPER;
            params.vFechaTrab = vFechaTrab;
            params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "RegistraRemoto.aspx/GetTipoTrabDescrip", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
				success: function (result) {
					//$("#divmsj3").html(result.d); 
					var aResult = result.d.split("||sep||");
					$("#divDetRecaudacion").html(aResult[0]);

					document.getElementById("<%=txtTrabActividad.ClientID%>").value = aResult[0];
					document.getElementById("inputTxtTrabActividad").value = aResult[0];
					document.getElementById("<%=txtDiaSelRuta.ClientID%>").value = aResult[1];
				}, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divmsj3").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});

			document.getElementById("txtActHora").value = "";
			document.getElementById("txtActAct").value = "";
			//document.getElementById("txtActHora").focus();
			//$("#txtActHora").focus();
			//$('#txtActHora').focus().val($('#txtActHora').val());
		}


        function fcal1clic() {
            alert("1 Click");
        }

        function fcal2clic() {
            alert("2 Click");
        }

    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div class="col-xs-8 col-xs-offset-2">
        <p class="cazador2">Trabajo Presencial / Remoto</p>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

   
    <form id="form1" runat="server" autocomplete="off">
      <!-- Content Wrapper. Contains page content -->
      <div class="content-wrapper">
         <!-- Content Header (Page header) -->
         <!-- Main content -->

	<section class="content">

<div style="display:none">
	<asp:TextBox ID="txtCantDias" runat="server" ></asp:TextBox>
	<asp:TextBox ID="txtIdPer" runat="server" ></asp:TextBox>
	<asp:TextBox ID="txtDNI" runat="server" ></asp:TextBox>
	<asp:TextBox ID="txtDatosPer" runat="server" ></asp:TextBox>
	<asp:TextBox ID="txtDiaSel" runat="server" ></asp:TextBox>
	<asp:TextBox ID="txtDiaSelRuta" runat="server" ></asp:TextBox>
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

        <div class="col-md-9">
          <div class="box box-primary">
            <div class="box-body no-padding fc ">
				<div class="fc-toolbar fc-header-toolbar">
					<div class="fc-left" style="visibility:hidden">
						<div class="fc-button-group">
							<div class="btn btn-danger fc-corner-right" onclick="GPresencial(' ');" >Eliminar</div>
						</div>
					</div>
					<div class="fc-right" style="visibility:hidden">
						<div class="fc-button-group">
							<div class="btn btn-success fc-corner-left" onclick="G-Presencial('PRESENCIAL');" >Presencial</div>
							<button type="button" class="fc-state-default fc-corner-left fc-state-active">month</button>
							<div class="btn btn-warning fc-corner-right" onclick="G-Presencial('REMOTO');" >Remoto</div>
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
       <asp:Label id="UploadStatusLabel" runat="server"> </asp:Label>
         <!-- FIN MAIN CONTENT -->
        
         <!-- modal-sm | small || modal-lg | largo || modal-xl | extra largo -->
        
        <div class="modal modal-warning fade" id="modalShowProd">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Productos</h4>
              </div>
              <div class="modal-body">
                <%--<p>One fine body&hellip;</p>--%>
                  <div id="divShowProd"></div>
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

        <div class="modal modal-info fade" id="modalTrabDescrip">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Descripcion del Trabajo</h4>
				  <div id="dmTrabDTitle"></div>
				  <input type="hidden" id="inputDiaTrab" />
				  <input type="hidden" id="inputTxtTrabActividad" />
              </div>
              <div class="modal-body">
				  <p>
					<div class="input-group input-group hidden">

			<div class="row" style="display:block;">
               <div class="column">
                  <div class="col-md-1" >
                     <div class="box-body">
						 Hora: 
                     </div>
                  </div>
                  <div class="col-md-2" >
                     <div class="box-body">
                            <input type="text" id="txtActHora" class="form-control" placeholder="00:00" />
                     </div>
                  </div>
                  <div class="col-md-1" >
                     <div class="box-body">
						 Actividad: 
                     </div>
                  </div>
                  <div class="col-md-8" >
                     <div class="box-body">
                        <div class="form-group">
							<input type="text" id="txtActAct" class="form-control" />
                        </div>
                     </div>
                  </div>
               </div>
            </div>
						<span class="input-group-btn ">
							<div class="btn btn-primary btn-flat" onclick="ActivAgregar();" >Agregar</div>
						</span>
					</div>
				  </p>
				  <asp:textbox ID="txtTrabActividad" runat="server" TextMode="MultiLine" Rows="8" class="form-control"></asp:textbox>
				  <p align="right">
					<button type="button" class="btn btn-primary hidden" data-dismiss="modal" onclick="GActividad()" >Guardar Actividad</button>
				  </p>
				  <br />
				<div class="input-group input-group hidden">
					<asp:FileUpload ID="FileUpload1" runat="server" class="form-control" />
					<span class="input-group-btn">
						<asp:Button id="UploadButton" class="btn btn-primary btn-flat" Text="Guardar Archivo Evidencia" OnClick="UploadButton_Click" runat="server"> </asp:Button>    
					</span>
				</div>
				  <br />
				  <p>
					  <asp:Button ID="btnDescarga" runat="server" Text="Descarga Archivo" class="btn btn-primary btn-flat" OnClick="btnDescarga_Click" />
				  </p>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-outline pull-left" data-dismiss="modal">Cerrar</button>

              </div>
            </div>
            <!-- /.modal-content -->
          </div>
          <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->


<script>
	function ActivAgregar() {
		//alert("01");
		var TextoActual = document.getElementById("<%=txtTrabActividad.ClientID%>").value;
		var TextoHora = document.getElementById("txtActHora").value;
		var TextoActiv = document.getElementById("txtActAct").value;
		
		document.getElementById("<%=txtTrabActividad.ClientID%>").value = TextoActual + "|*|\n" + TextoHora + " - " + TextoActiv;
	}
</script>

      </div>
      <!-- /.content-wrapper -->
   </form>
    <script>
        //document.getElementById('LiCIEIm').className = "treeview menu-open";
        //$('#LiCIEIm').addClass('menu-open');
        document.getElementById("LiTrabJef").classList.add('menu-open');
        document.getElementById('UlTrabJef').style.display = 'block';
        document.getElementById("ulTrabList").classList.add('active');
        var f = new Date();

    </script>
</asp:Content>

