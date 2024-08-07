<%@ Page Language="C#" MasterPageFile="../../MasterPage.master" AutoEventWireup="true" CodeFile="AtCirugVsCons.aspx.cs" Inherits="ASPX_IROJVAR_HISMINSA_AtCirugVsCons" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>IRO - HISMINSA</title>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/json2.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/funciones.js?vfd=10")%>"></script>

    <%--<link rel="stylesheet" media="screen" href="style1.css?id=1">--%>

    <script language="javascript" type="text/javascript">

        vFilaAnteriorMes = "";
        vFilaAnteriorEspec = "";
        vFilaAnteriorPac = "";

        function DetAtenciones(vanio, vmes, vcodserv, vservicio) {
            var params = new Object();
            params.vmes = vmes; 
            params = JSON.stringify(params);

            $("#divAtencion").html("");
            $.ajax({
                type: "POST", url: "AtCirugVsCons.aspx/GetDetAtenciones", data: params, 
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

        function fEspeMes(vanio, vmes, vmesnom, vfila) {
            $("#divTblEspec").html("");
            $("#divTblPaciente").html("");

            var params = new Object();
            params.vanio = vanio; 
            params.vmes = vmes; 
            params.vmesnom = vmesnom; 
            params = JSON.stringify(params); 

            $.ajax({
                type: "POST", url: "AtCirugVsCons.aspx/GetDetEspecialidadxMes", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divTblEspec").html(result.d) }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divTblEspec").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });
            
            colorFilaMes("tblAnioMes", vfila);
            document.getElementById("txtBscProf").focus();

        }

        function fPacEspeMes(vanio, vmes, vmesnom, vespec, vfila, vhisper, vnomper) {
            $("#divTblPaciente").html("");

            var params = new Object();
            params.vanio = vanio; 
            params.vmes = vmes; 
            params.vmesnom = vmesnom; 
            params.vespec = vespec; 
            params.vhisper = vhisper; 
            params.vnomper = vnomper; 
            params = JSON.stringify(params); 

            $.ajax({
                type: "POST", url: "AtCirugVsCons.aspx/GetDetPacientexEspexMes", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { 
                    var aResult = result.d.split("||sep||");
                    //$("#cbotblPacMedico").html(aResult[2]);
                    $("#cbotblPacDia").html(aResult[1]);
                    $("#divTblPaciente").html(aResult[0]);
                }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divTblPaciente").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });
            
            colorFilaEspec(vfila);

        }

        function fPacEspeMesSelFila(vfila) {
            colorFilaPac(vfila);
        }

        function fPacEspeMesSelFilaModal(vfila, oper, vanio, vmes, vmesnom, DIA, COD_PER, HIS_PER, APE_PER) {
            colorFilaPac(vfila);
            
            document.getElementById("divDetDx").innerHTML = "";
            document.getElementById("btnmDetDx").click();

            var params = new Object();
            params.oper = oper; 
            params.vanio = vanio; 
            params.vmes = vmes; 
            params.vmesnom = vmesnom; 
            params.DIA = DIA; 
            params.COD_PER = COD_PER; 
            params.HIS_PER = HIS_PER; 
            params.APE_PER = APE_PER; 
            params = JSON.stringify(params); 

            $.ajax({
                type: "POST", url: "AtCirugVsCons.aspx/GetDetxProfesionalxDia", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divDetDx").html(result.d) }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divDetDx").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });

        }

        function colorFilaMes(vidtbl, vfila) {
            //document.getElementById(vidtbl).style.backgroundColor = null;
            //document.getElementById(vidtbl).style.removeAttribute("style");
            vfila.style.backgroundColor = '#2AB7C6';
            if (vFilaAnteriorMes != "") {
                vFilaAnteriorMes.style.backgroundColor = '#fff';
            }
            vFilaAnteriorMes = vfila;
        }

        function colorFilaEspec(vfila) {
            vfila.style.backgroundColor = '#2AB7C6';
            if (vFilaAnteriorEspec != "") {
                vFilaAnteriorEspec.style.backgroundColor = '#fff';
            }
            vFilaAnteriorEspec = vfila;
        }

        function colorFilaPac(vfila) {
            vfila.style.backgroundColor = '#2AB7C6';
            if (vFilaAnteriorPac != "") {
                vFilaAnteriorPac.style.backgroundColor = '#fff';
            }
            vFilaAnteriorPac = vfila;
        }

        function ddlSel1(ddl) {
            document.getElementById(ddl).value = "";
        }

    </script>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div class="col-xs-8 col-xs-offset-2">
        <p class="cazador2">HISMINSA - Atenciones por Servicio</p>
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
        <div class="col-md-3">
            <div class="box" >
                <div class="box-header">
                    <div class="input-group-btn">
                        <button class="btn btn-default" runat="server" type="button" onclick="exportTableToExcel('tblAnioMes')">
                            <i class="fa fa-download "></i>
                        </button>
                    </div>
                </div>
                <div class="box-body table-responsive no-padding" id="habfiltro">
                    <asp:GridView ID="GVtable" runat="server" class="table table-condensed table-bordered"></asp:GridView>
                    <asp:Literal ID="LitTABL1" runat="server"></asp:Literal>
                </div>
            </div>
        </div>

        <div class="col-md-5">
            <div class="box" >
                <div class="box-header">
                    <div class="row">
                        <div class="col-md-1">
                            <button class="btn btn-default" runat="server" type="button" onclick="exportTableToExcel('tblEspecialid')">
                                <i class="fa fa-download "></i>
                            </button>
                        </div>
                        <div class="col-md-8">
                            <input type="text" class='form-control' id="txtBscProf" onkeyup="fBscTblHTML('txtBscProf', 'tblEspecialid', 0); " placeholder="Profesional" />
                        </div>
                    </div>
                </div>
                <div class="box-body table-responsive no-padding" id="divTblEspec"></div>
            </div>
        </div>

        <div class="col-md-4">
            <div class="box" >
                <div class="box-header">
                    <div class="row">
                        <div class="col-md-1">
                            <button class="btn btn-default" runat="server" type="button" onclick="exportTableToExcel('tblPaciente')">
                                <i class="fa fa-download "></i>
                            </button>
                        </div>
                        <div class="col-md-2">
                            <select id="cbotblPacDia" class='form-control' onchange="ddlSel1('cbotblPacMedico'); fBscTblHTML('cbotblPacDia', 'tblPaciente', 1); " >
                                <option value=""></option>
                            </select>
                            <code>Dia</code>
                        </div>
                        <%--<div class="col-md-3">
                            <select id="cbotblPacMedico" class='form-control' onchange="ddlSel1('cbotblPacDia'); fBscTblHTML('cbotblPacMedico', 'tblPaciente', 6); " >
                                <option value=""></option>
                            </select>
                            <code>Medico</code>
                        </div>--%>

                    </div>
                </div>
                <div class="box-body table-responsive no-padding" id="divTblPaciente"></div>
            </div>
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


        <!-- modal -->
          <div style="display:none"><button type="button" id="btnmDetDx" data-toggle="modal" data-target="#mDetDx"></button></div>

        <div class="modal modal-info fade" id="mDetDx">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="titlemDetDx">Detalle de la Atencion</h4>
              </div>
              <div class="modal-body">
                <p><div id="divDetDx"></div></p>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-outline pull-left" data-dismiss="modal">Close</button>
              </div>
            </div>
          </div>
        </div>
        <!-- /.modal -->


      </div>
      <!-- /.content-wrapper -->
   </form>
    <script>
        //document.getElementById('LiCIEIm').className = "treeview menu-open";
        //$('#LiCIEIm').addClass('menu-open');
        document.getElementById("LiCIRUGIAPROC").classList.add('menu-open');
        document.getElementById('UlCIRUGIAP').style.display = 'block';
        document.getElementById("ulCirugEjecu").classList.add('active');
        var f = new Date();

    </script>
</asp:Content>
