<%@ Page Language="C#" MasterPageFile="../../MPSaludInfantil.master" AutoEventWireup="true" CodeFile="ConvG_FT03.aspx.cs" Inherits="ASPX_SInfantil_ConvG2022_ConvG_FT03" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>Salud Infantil</title>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/json2.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/funciones.js?vfd=1")%>"></script>
    <%--<link rel="stylesheet" media="screen" href="style1.css?id=1">--%>

<script>

    function returnObj() {
        var params = new Object(); 
        
        params.vInstitucion = document.getElementById("<%=txtInstitucion.ClientID%>").value; 
        params.vRed = document.getElementById("<%=txtRed.ClientID%>").value; 
        params.vMRed = document.getElementById("<%=txtMicroRed.ClientID%>").value; 
        params.vProvincia = document.getElementById("<%=txtProvincia.ClientID%>").value; 
        params.vDistrito = document.getElementById("<%=txtDistrito.ClientID%>").value; 
        params.vEESS = document.getElementById("<%=txtEESS.ClientID%>").value; 
        
        return params; 
    }

    function fShowInstitucion() { 
        var params = returnObj(); 
        params.vtxtCarga = ("<%=txtInstitucion.ClientID%>"); 
        params.vSel = "Institucion"; 
        params = JSON.stringify(params); 

        $.ajax({ 
            type: "POST", url: "ConvG_FT03.aspx/GetDatoFiltro", data: params, 
            contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
            success: function (result) { $("#divmodAll").html(result.d) }, 
            error: function(XMLHttpRequest, textStatus, errorThrown) { 
                alert(textStatus + ": " + XMLHttpRequest.responseText); 
                $("#divmodAll").html(textStatus + ": " + XMLHttpRequest.responseText); 
            } 
        }); 
    } 

    function fShowRed() { 
        var params = returnObj(); 
        params.vtxtCarga = ("<%=txtRed.ClientID%>"); 
        params.vSel = "Red"; 
        params = JSON.stringify(params); 

        $.ajax({ 
            type: "POST", url: "ConvG_FT03.aspx/GetDatoFiltro", data: params, 
            contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
            success: function (result) { $("#divmodAll").html(result.d) }, 
            error: function(XMLHttpRequest, textStatus, errorThrown) { 
                alert(textStatus + ": " + XMLHttpRequest.responseText); 
                $("#divmodAll").html(textStatus + ": " + XMLHttpRequest.responseText); 
            } 
        }); 
    } 

    function fShowMRed() { 
        var params = returnObj(); 
        params.vtxtCarga = ("<%=txtMicroRed.ClientID%>"); 
        params.vSel = "MRed"; 
        params = JSON.stringify(params); 

        $.ajax({ 
            type: "POST", url: "ConvG_FT03.aspx/GetDatoFiltro", data: params, 
            contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
            success: function (result) { $("#divmodAll").html(result.d) }, 
            error: function(XMLHttpRequest, textStatus, errorThrown) { 
                alert(textStatus + ": " + XMLHttpRequest.responseText); 
                $("#divmodAll").html(textStatus + ": " + XMLHttpRequest.responseText); 
            } 
        }); 
    } 

    function fShowProvincia() { 
        var params = returnObj(); 
        params.vtxtCarga = ("<%=txtProvincia.ClientID%>"); 
        params.vSel = "Provincia"; 
        params = JSON.stringify(params); 

        $.ajax({ 
            type: "POST", url: "ConvG_FT03.aspx/GetDatoFiltro", data: params, 
            contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
            success: function (result) { $("#divmodAll").html(result.d) }, 
            error: function(XMLHttpRequest, textStatus, errorThrown) { 
                alert(textStatus + ": " + XMLHttpRequest.responseText); 
                $("#divmodAll").html(textStatus + ": " + XMLHttpRequest.responseText); 
            } 
        }); 
    } 

    function fShowDistrito() { 
        var params = returnObj(); 
        params.vtxtCarga = ("<%=txtDistrito.ClientID%>"); 
        params.vSel = "Distrito"; 
        params = JSON.stringify(params); 

        $.ajax({ 
            type: "POST", url: "ConvG_FT03.aspx/GetDatoFiltro", data: params, 
            contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
            success: function (result) { $("#divmodAll").html(result.d) }, 
            error: function(XMLHttpRequest, textStatus, errorThrown) { 
                alert(textStatus + ": " + XMLHttpRequest.responseText); 
                $("#divmodAll").html(textStatus + ": " + XMLHttpRequest.responseText); 
            } 
        }); 
    } 

    function fShowEESS() { 
        var params = returnObj(); 
        params.vtxtCarga = ("<%=txtEESS.ClientID%>"); 
        params.vSel = "EESS"; 
        params = JSON.stringify(params); 

        $.ajax({ 
            type: "POST", url: "ConvG_FT03.aspx/GetDatoFiltro", data: params, 
            contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
            success: function (result) { $("#divmodAll").html(result.d) }, 
            error: function(XMLHttpRequest, textStatus, errorThrown) { 
                alert(textStatus + ": " + XMLHttpRequest.responseText); 
                $("#divmodAll").html(textStatus + ": " + XMLHttpRequest.responseText); 
            } 
        }); 
    } 





</script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div class="col-xs-8 col-xs-offset-2">
        <p class="cazador2">Nominal - Convenio de  Gestión FT-03 
            <a class="label label-danger" href="https://datastudio.google.com/u/1/reporting/80196dec-fcb3-43e3-a8e0-8d8dd2d46405/page/p_81scf34i0c" target="_blank"> (Gráfico)</a>
        </p>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

   
    <form id="form1" runat="server" autocomplete="off">
      <!-- Content Wrapper. Contains page content -->
      <div class="content-wrapper">
         <!-- Content Header (Page header) -->
         <!-- Main content -->
         <section class="content">

<script>
    

</script>


             
            <!--SEGUNDA FILA-->             
            <div class="row" style="display:block;">

               <div class="column">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="input-group margin">
                            <asp:TextBox ID="txtInstitucion" runat="server" class='form-control' placeholder="Institucion">GOBIERNO REGIONAL</asp:TextBox>
                            <span class="input-group-btn">
                                <div class='btn btn-info btn-flat' data-toggle="modal" data-target="#modAll" onclick="fShowInstitucion()" >...</div>
                            </span>
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="input-group margin">
                            <asp:TextBox ID="txtRed" runat="server" class='form-control' placeholder="Red"></asp:TextBox>
                            <span class="input-group-btn">
                                <div class='btn btn-info btn-flat' data-toggle="modal" data-target="#modAll" onclick="fShowRed()" >...</div>
                            </span>
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="input-group margin">
                            <asp:TextBox ID="txtMicroRed" runat="server" class='form-control' placeholder="Micro Red"></asp:TextBox>
                            <span class="input-group-btn">
                                <div class='btn btn-info btn-flat' data-toggle="modal" data-target="#modAll" onclick="fShowMRed()" >...</div>
                            </span>
                        </div>
                     </div>
                  </div>
               </div>

               <!-- /.box-header -->
            </div>
            <!--FIN SEGUNDA FILA-->

            <!--SEGUNDA FILA-->             
            <div class="row" style="display:block;">

                
               <div class="column">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="input-group margin">
                            <asp:TextBox ID="txtProvincia" runat="server" class='form-control' placeholder="Provincia"></asp:TextBox>
                            <span class="input-group-btn">
                                <div class='btn btn-info btn-flat' data-toggle="modal" data-target="#modAll" onclick="fShowProvincia()" >...</div>
                            </span>
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="input-group margin">
                            <asp:TextBox ID="txtDistrito" runat="server" class='form-control' placeholder="Distrito"></asp:TextBox>
                            <span class="input-group-btn">
                                <div class='btn btn-info btn-flat' data-toggle="modal" data-target="#modAll" onclick="fShowDistrito()" >...</div>
                            </span>
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column" style="display:block">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="input-group margin">
                            <asp:TextBox ID="txtEESS" runat="server" class='form-control' placeholder="Establecimiento"></asp:TextBox>
                            <asp:HiddenField ID="txtEESSCodHide" runat="server" />
                            <span class="input-group-btn">
                                <div class='btn btn-info btn-flat' data-toggle="modal" data-target="#modAll" onclick="fShowEESS()" >...</div>
                            </span>
                        </div>
                     </div>
                  </div>
               </div>
                
                <div class="column" style="display:block">
                  <div class="col-md-1" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:LinkButton ID="bntBuscar" runat="server" class="btn btn-success" OnClick="bntBuscar_Click" ><i class="fa fa-search"></i> Listar</asp:LinkButton>
                        </div>
                     </div>
                  </div>
               </div>
               <!-- /.box-header -->
            </div>
            <!--FIN SEGUNDA FILA-->

<script>
	var inputText = document.getElementById("<%=txtEESS.ClientID%>");
	inputText.addEventListener("keyup", function(event) {
      if (event.keyCode === 13) {
		  event.preventDefault();
		  document.getElementById("<%=bntBuscar.ClientID%>").click();
      }
	});
</script>


            <!-- TERCERA FILA -->
            <div class="row">
               <div class="col-md-12">
                  <div class="box" >
                        <div class="box-header ">
                            <h3 class="box-title" style="margin-top: -6px; margin-bottom: -11px; margin-left: -7px;">
                            
            <div class="input-group margin">
                            <div class="input-group-btn">
                                    <button class="btn btn-default" type="button" title="Max.: 1,000 Registros" onclick="exportTableToExcel('<%=GVtable.ClientID%>')"><i class="fa fa-download "></i>
                                    </button>
                            </div>
                            <div><input type="text" class="form-control" id="bscprod2" placeholder="Buscar" onkeyup="fBscTblHTML('bscprod2', '<%=GVtable.ClientID%>', 0)" autofocus="autofocus">
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

          
        <div class="modal modal-success fade" id="modAll">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">
                    <div class="form-group">
                      <label for="bscprod" class="col-sm-3 control-label" id="lblmodAllBuscar">Buscar: </label>
                      <div class="col-sm-9">
	                    <input type="text" class="form-control" id="txtBuscarModal" placeholder="Buscar" onkeyup="fBscTblHTML('txtBuscarModal', 'tblTablaModal', 1)" autofocus="autofocus">
                      </div>
                    </div>
                </h4>
              </div>
              <div class="modal-body">
                  <div id="divmodAll"></div>
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
        document.getElementById("LIConvG").classList.add('menu-open');
        document.getElementById('UlConvG').style.display = 'block';
        document.getElementById("ulNomFT03").classList.add('active');
        var f = new Date();

    </script>
</asp:Content>
