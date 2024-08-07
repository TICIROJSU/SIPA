<%@ Page Language="C#" MasterPageFile="../../MasterPage.master" AutoEventWireup="true" CodeFile="RegTrabPres.aspx.cs" Inherits="ASPX_IROJVAR_TrabPres_RegTrabPres" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>Optica</title>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/json2.js")%>"></script>
    <%--<link rel="stylesheet" media="screen" href="style1.css?id=1">--%>

    <script language="javascript" type="text/javascript">

		function buscarProd() {
            var params = new Object();
            params.vbuscarProd = document.getElementById("txtbuscaprod").value;
            params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "ProcIngresos.aspx/GetbuscarProd", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divShowProd").html(result.d) }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divShowProd").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});

			document.getElementById("txtbuscaprod").value = "";
			document.getElementById("txtbuscaprod").focus();
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
                           <asp:DropDownList ID="DDLAnio" runat="server" class='form-control'>
                              <asp:ListItem Value="2017">2017</asp:ListItem>
                              <asp:ListItem Value="2018">2018</asp:ListItem>
                              <asp:ListItem Value="2019">2019</asp:ListItem>
                              <asp:ListItem Value="2020" Selected="true">2020</asp:ListItem>
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

               <div class="column" style="display:block">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:LinkButton ID="bntBuscar" runat="server" class="btn btn-success" ><i class="fa fa-search"></i> Consultar</asp:LinkButton>
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
					<div class="fc-left">
						<div class="fc-button-group">
							<button type="button" class="fc-prev-button fc-button fc-state-default fc-corner-left" ><span class="fc-icon fc-icon-left-single-arrow"></span></button>
							<button type="button" class="fc-next-button fc-button fc-state-default fc-corner-right" ><span class="fc-icon fc-icon-right-single-arrow"></span></button>
						</div>
					</div>
					<div class="fc-right">
						<div class="fc-button-group">
							<button type="button" class=" fc-state-default fc-corner-left fc-state-active">month</button>
							<button type="button" class="fc-button fc-state-default">week</button>
							<button type="button" class="fc-button fc-state-default fc-corner-right">day</button>
						</div>
					</div>
					<div class="fc-center"><h2>July 2021</h2></div>
					<div class="fc-clear"></div>
			  </div>

			<asp:Literal ID="LitCalendar" runat="server"></asp:Literal>


<%--			  <table>
				<thead class='fc-head'>
					<tr>
						<th>Dom</th>
						<th>Lun</th><th>Mar</th><th>Mie</th>
						<th>Jue</th><th>Vie</th><th>Sab</th>
					</tr>
				</thead>
				<tbody class='fc-body'>
					<tr height='70'>
						<td>
							<h3><span id='' class='pull-right'>1</span></h3>
							<span >Meeting</span>
						</td>
					</tr>
				</tbody>
			  </table>--%>
			  
				
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
         <!-- FIN MAIN CONTENT -->
         <!-- FIN MAIN CONTENT -->
         <!-- FIN MAIN CONTENT -->
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


<script>

</script>

      </div>
      <!-- /.content-wrapper -->
   </form>
    <script>
        //document.getElementById('LiCIEIm').className = "treeview menu-open";
        //$('#LiCIEIm').addClass('menu-open');
        document.getElementById("LiTrabPres").classList.add('menu-open');
        document.getElementById('UlTrabPres').style.display = 'block';
        document.getElementById("ulTrabReg").classList.add('active');
        var f = new Date();

    </script>
</asp:Content>


