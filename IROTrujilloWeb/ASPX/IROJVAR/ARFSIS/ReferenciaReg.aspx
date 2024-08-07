<%@ Page Language="C#" MasterPageFile="../../MasterPage.master" AutoEventWireup="true" CodeFile="ReferenciaReg.aspx.cs" Inherits="ASPX_IROJVAR_ARFSIS_ReferenciaReg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>IRO - HISMINSA</title>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/json2.js")%>"></script>
    <%--<link rel="stylesheet" media="screen" href="style1.css?id=1">--%>

    <script language="javascript" type="text/javascript">

        function fRegRef(RefIdNro) {
            var params = new Object();
            params.vIdNro = RefIdNro; 
            params = JSON.stringify(params);

            $("#divRegRef").html("");

            $.ajax({
                type: "POST", url: "ReferenciaReg.aspx/GetRegRef", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divRegRef").html(result.d) }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divRegRef").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });
        }

        function fRegRefG() {
            var params = new Object();
            params.idNro =  document.getElementById("txtid").value; 
            params.Ventanilla = document.getElementById("txtVentanilla").value; 
            params.citaSipa = document.getElementById("txtcitaSipa").value; 
            params.Observacion = document.getElementById("txtObservacion").value; 
            params.fechaobsv = document.getElementById("txtfechaobsv").value; 
            params.NrObs = document.getElementById("txtNrObs").value; 
            params.retirohc = document.getElementById("txtretirohc").value; 
            params.crearhc = document.getElementById("txtcrearhc").value; 
            params.disaorigen = document.getElementById("txtdisaorigen").value; 
            params.redorigen = document.getElementById("txtredorigen").value; 
            params.mredorigen = document.getElementById("txtmredorigen").value; 
            params.Estorigen = document.getElementById("txtEstorigen").value; 
            params = JSON.stringify(params);

            $("#divMensaje").html("");
            $.ajax({
                type: "POST", url: "ReferenciaReg.aspx/SetRegRefG", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divMensaje").html(result.d) }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divMensaje").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });

            window.setTimeout("fMensajeElim('divMensaje')", 5000);
        }

        function fMensajeElim(div) {
            $("#" + div).html("");
        }
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div class="col-xs-8 col-xs-offset-2">
        <p class="cazador2">Registro de RefCon</p>
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
                            <code style="color:black; background-color:transparent">EESS</code>
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
                            <code style="color:black; background-color:transparent">Año</code>
                        </div>
                     </div>
                  </div>
               </div>
               <div class="column">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                           <asp:DropDownList ID="DDLMes" runat="server" class='form-control'>
                              <asp:ListItem Value="0">Meses</asp:ListItem>
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
                            <code style="color:black; background-color:transparent">Mes de Envio</code>
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

               <div class="column">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group hide" data-toggle="modal" data-target="#modalShowProf" onclick="fShowProf()">
                            <asp:TextBox ID="txtProfesional" runat="server" Text="" class='form-control' ReadOnly="True" placeholder="Profesional"></asp:TextBox>
                            <asp:HiddenField ID="txtIdProf" runat="server" Value="" />
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                         <div id="divMensaje" style="color:darkblue; "></div>
                     </div>
                  </div>
               </div>

               <!-- /.box-header -->
            </div>
            <!--FIN SEGUNDA FILA-->

<script>
    function fSelProf(vPlaza, vProf)
    {
        document.getElementById("<%=txtIdProf.ClientID%>").value = vPlaza; 
        document.getElementById("<%=txtProfesional.ClientID%>").value = vPlaza + '-' + vProf; 
    }
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

        <div class="modal modal-success fade" id="modalRegRef">
          <div class="modal-dialog " style="width: 1100px;" >
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Registro de Referencia</h4>
              </div>
              <div class="modal-body">
                <%--<p>One fine body&hellip;</p>--%>
                  <div id="divRegRef"></div>
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
        document.getElementById("LiARFSIS").classList.add('menu-open');
        document.getElementById('UlARFSIS').style.display = 'block';
        document.getElementById("ulASISRefe").classList.add('active');
        var f = new Date();

        document.getElementById("<%=txtIdProf.ClientID%>").value = '';
    </script>
</asp:Content>

