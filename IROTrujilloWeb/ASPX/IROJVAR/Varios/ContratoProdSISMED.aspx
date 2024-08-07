<%@ Page Language="C#" MasterPageFile="../../MasterPage.master" AutoEventWireup="true" CodeFile="ContratoProdSISMED.aspx.cs" Inherits="ASPX_IROJVAR_Varios_ContratoProdSISMED" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>Logistica - Contratos - SISMED</title>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/json2.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/funciones.js?vfd=1")%>"></script>
    <script language="javascript" type="text/javascript">

        function buscarProd() {
            var params = new Object();
            params.vtxtcodSIGA = document.getElementById("<%=txtCodSIGA.ClientID%>").value; 
            params = JSON.stringify(params);
            
            $.ajax({
                type: "POST", url: "ContratoProdSISMED.aspx/GetProductosSISMED", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divShowProd").html(result.d); },  
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divShowProd").html(textStatus + ": " + XMLHttpRequest.responseText); 
                }
            });
        }

        function fSelProd(vcodsg, vdessg, vest) {
            $("#<%=txtCodSISMED.ClientID%>").val(vcodsg);
            $("#<%=txtProducto.ClientID%>").val(vdessg);
        }

    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div class="col-xs-8 col-xs-offset-2">
        <p class="cazador2">Contratos SISMED</p>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
   <form id="form1" runat="server">
      <!-- Content Wrapper. Contains page content -->
      <div class="content-wrapper">
         <!-- Content Header (Page header) -->
         <!-- Main content -->
         <section class="content">
            <!-- FIN PRIMERA FILA -->
            <!--SEGUNDA FILA-->             
            <div class="row" style="display:block;">
               <div class="column">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="input-group margin">                            
                            <asp:TextBox ID="txtCodSIGA" runat="server" class='form-control'></asp:TextBox>
			                <span class="input-group-btn">
                                <div class="btn btn-info btn-flat" id="btnBuscarProd" onclick="buscarProd();" data-toggle="modal" data-target="#modalShowProd">...</div>
			                </span>                            
                        </div>
                         <code style="font-size:large; background-color:transparent; ">Codigo SIGA</code>
                     </div>
                  </div>
               </div>

<script>
    var inputText = document.getElementById("<%=txtCodSIGA.ClientID%>");
	inputText.addEventListener("keyup", function(event) {
      if (event.keyCode === 13) {
		  event.preventDefault();
		  document.getElementById("btnBuscarProd").click();
      }
	});
</script>

               <div class="column">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:TextBox ID="txtCodSISMED" runat="server" class='form-control'></asp:TextBox>
                            <code style="font-size:large; background-color:transparent; ">Codigo SISMED</code>
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:TextBox ID="txtProducto" runat="server" class='form-control'></asp:TextBox>
                            <code style="font-size:large; background-color:transparent; ">Nombre del Producto</code>
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                           <asp:DropDownList ID="DDLContrato" runat="server" class='form-control' >
                              <asp:ListItem Value="SI" Selected="true">Si</asp:ListItem>
                              <asp:ListItem Value="NO">No</asp:ListItem>
                           </asp:DropDownList>
                            <code style="font-size:large; background-color:transparent; ">¿Contrato?</code>
                        </div>
                     </div>
                  </div>
               </div>
                
               <div class="column">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:TextBox ID="txtContAnio" runat="server" class='form-control'></asp:TextBox>
                            <code style="font-size:large; background-color:transparent; ">Año del Contrato</code>
                        </div>
                     </div>
                  </div>
               </div>
                
               <div class="column">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:TextBox ID="txtContNro" runat="server" class='form-control'></asp:TextBox>
                            <code style="font-size:large; background-color:transparent; ">Nro. del Contrato</code>
                        </div>
                     </div>
                  </div>
               </div>
                
               <div class="column">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:TextBox ID="txtContCant" runat="server" class='form-control'></asp:TextBox>
                            <code style="font-size:large; background-color:transparent; ">Cantidad Contratada</code>
                        </div>
                     </div>
                  </div>
               </div>
                
               <div class="column">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:TextBox ID="txtCantAtend" runat="server" class='form-control'></asp:TextBox>
                            <code style="font-size:large; background-color:transparent; ">Cantidad Atendida</code>
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:TextBox ID="txtCantxAtend" runat="server" class='form-control'></asp:TextBox>
                            <code style="font-size:large; background-color:transparent; ">Cantidad por Atender</code>
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column" style="display:block">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:LinkButton ID="bntBuscar" runat="server" class="btn btn-success" OnClick="bntBuscar_Click" ><i class="fa fa-search"></i> Registrar</asp:LinkButton>
                        </div>
                     </div>
                  </div>
               </div>
               <!-- /.box-header -->
            </div>
            <!--FIN SEGUNDA FILA-->
             
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

            <div class="row" style="display:none;">
               <div class="column">
                  <div class="col-md-12" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <h2><a >Analisis de Disponibilidad con Distribucion</a></h2>
                            <%--<h2><a onserverclick="ExcelAnalisisDistribDispo_Click" runat="server">Analisis de Disponibilidad con Distribucion</a></h2>--%>
                            
                        </div>
                     </div>
                  </div>
               </div>
            </div>

            <!--FIN TERCERA FILA-->
            <!-- CUARTA FILA-->
            <!-- CUARTA FILA-->
            <!-- CUARTA FILA-->
            <!-- CUARTA FILA-->
            
             <asp:Literal ID="LitGraf1" runat="server"></asp:Literal>
             <asp:Literal ID="LitErrores" runat="server"></asp:Literal>

            <!-- FIN CUARTA FILA-->
            <!-- FIN CUARTA FILA-->
            <!-- FIN CUARTA FILA-->
            <!-- FIN CUARTA FILA-->
         </section>
         <!-- FIN MAIN CONTENT -->
         <!-- FIN MAIN CONTENT -->
         <!-- FIN MAIN CONTENT -->
         <!-- FIN MAIN CONTENT -->

        <div class="modal modal-warning fade" id="modalShowProd">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Productos</h4>
              </div>
              <div class="modal-body">
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



      </div>
      <!-- /.content-wrapper -->
   </form>
    <script>
        //document.getElementById('LiCIEIm').className = "treeview menu-open";
        //$('#LiCIEIm').addClass('menu-open');
        document.getElementById("LiVariosI").classList.add('menu-open');
        document.getElementById('UlVarios').style.display = 'block';
        document.getElementById("ulContrProdSISMED").classList.add('active');
        var f = new Date();

    </script>
</asp:Content>


