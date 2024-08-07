<%@ Page Language="C#" MasterPageFile="../../MasterPage.master" AutoEventWireup="true" CodeFile="AtOtrosS.aspx.cs" Inherits="ASPX_IROJVAR_UPE_AtOtrosS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>IRO - HISMINSA</title>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/json2.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/funciones.js?vfd=10")%>"></script>

    <%--<link rel="stylesheet" media="screen" href="style1.css?id=1">--%>

    <script language="javascript" type="text/javascript">

        vFilaAnteriorMes = "";
        vFilaAnteriorEspec = "";
        vFilaAnteriorPac = "";

        function fEspeMes(vanio, vmes, vmesnom, vfila, vPrg) {
            $("#divTblEspec").html("");
            $("#divTblPaciente").html("");

            var params = new Object();
            params.vanio = vanio; 
            params.vmes = vmes; 
            params.vmesnom = vmesnom; 
            params.vPrg = vPrg; 
            params = JSON.stringify(params); 

            $.ajax({
                type: "POST", url: "AtOtrosS.aspx/GetDetEspecialidadxMes", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divTblEspec").html(result.d) }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divTblEspec").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });
            
            colorFilaMes("tblAnioMes", vfila);

        }

        function fPacEspeMes(vanio, vmes, vmesnom, vHCli, vfila, vDNI) {
            $("#divTblPaciente").html("");

            var params = new Object();
            params.vanio = vanio; 
            params.vmes = vmes; 
            params.vHCli = vHCli; 
            params.vDNI = vDNI; 
            params = JSON.stringify(params); 

            $.ajax({
                type: "POST", url: "AtOtrosS.aspx/GetDetPacientexEspexMes", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { 
                    var aResult = result.d.split("||sep||");
                    $("#cbotblPacMedico").html(aResult[2]);
                    $("#cbotblPacDia").html(aResult[1]);
                    $("#divTblPaciente").html(aResult[0]);
                }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divTblPaciente").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });
            
            colorFilaEspec(vfila);
            //
            document.getElementById("divSISurl").innerHTML = "<a href='../../ExtIROJVAR/Citas/ConsultaSIS.aspx?iCita=" + vDNI + "' target='_blank' class='btn bg-navy'><img src=\"../../Estilos/Imagenes/sis32.png\" /></a>";
        }

        function fPacEspeMesSelFila(vfila) {
            colorFilaPac(vfila);
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
        <div class="col-md-2">
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

        <div class="col-md-4">
            <div class="box" >
                <div class="box-header">
                    <div class="input-group-btn">
                        <button class="btn btn-default" runat="server" type="button" onclick="exportTableToExcel('tblEspecialid')">
                            <i class="fa fa-download "></i>
                        </button>
                    </div>
                </div>
                <div class="box-body table-responsive no-padding" id="divTblEspec"></div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="box" >
                <div class="box-header">
                    <div class="row">
                        <div class="col-md-1">
                            <button class="btn btn-default" runat="server" type="button" onclick="exportTableToExcel('tblPaciente')">
                                <i class="fa fa-download "></i>
                            </button>
                        </div>
                        <div class="col-md-3" id="divSISurl">
                        </div>

                        <div class="col-md-2">
                            <a href="https://app1.susalud.gob.pe/registro/" target="_blank"><img src="../../Estilos/Imagenes/SUSALUD_ACMlogo32.png"  /></a>
                            
                        </div>

                        <%--<div class="col-md-2 hide">
                            <select id="cbotblPacDia" class='form-control' onchange="ddlSel1('cbotblPacMedico'); fBscTblHTML('cbotblPacDia', 'tblPaciente', 1); " >
                                <option value=""></option>
                            </select>
                            <code>Dia</code>
                        </div>--%>
                        <div class="col-md-3 hide">
                            <select id="cbotblPacMedico" class='form-control' onchange="ddlSel1('cbotblPacDia'); fBscTblHTML('cbotblPacMedico', 'tblPaciente', 6); " >
                                <option value=""></option>
                            </select>
                            <code>Medico</code>
                        </div>

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

      </div>
      <!-- /.content-wrapper -->
   </form>
    <script>
        var f = new Date();

    </script>
</asp:Content>
