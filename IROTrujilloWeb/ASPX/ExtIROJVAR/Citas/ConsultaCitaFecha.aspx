<%@ Page Language="C#" MasterPageFile="../../MasterPageExtPac.master" AutoEventWireup="true" CodeFile="ConsultaCitaFecha.aspx.cs" Inherits="ASPX_ExtIROJVAR_Citas_ConsultaCitaFecha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>IRO</title>
    <link rel="stylesheet" href="../../Estilos/bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css">

    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/json2.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/funciones.js?vfd=7")%>"></script>
    <%--<link rel="stylesheet" media="screen" href="style1.css?id=1">--%>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div class="col-xs-8 col-xs-offset-2">
        <p class="cazador2">Consulta de Citas Medicas - Pacientes</p>
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

    function contarFilTbl(vTbl) {
        //var $num = document.getElementById(vTbl).getElementsByTagName('tr').length - 1;
        var $num = document.getElementById(vTbl).rows.length - 1;
        document.getElementById('txtTotal').value = $num;
    }

    function contarFilTbl2() {
        document.getElementById('txtTotal').value = localStorage.getItem('tblCantRegMostrados');
    }

</script>        

            <!--SEGUNDA FILA-->             
            <div class="row" style="display:block;">

                <div class="column">
                    <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                        <div class="box-body">
                        <div class="form-group">
                            <asp:TextBox ID="txtProd" runat="server" class='form-control' Type="date" ></asp:TextBox>
                            <code style="font-size:large; background-color:transparent; ">Fecha Cita</code>
                        </div>
                        </div>
                    </div>
                </div>    
                
                <div class="column" style="display:block">
                  <div class="col-md-1" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:LinkButton ID="bntBuscar" runat="server" class="btn btn-success" OnClick="bntBuscar_Click" ><i class="fa fa-search"></i> Citas</asp:LinkButton>
                        </div>
                     </div>
                  </div>
               </div>

                <div class="column" style="display:block">
                  <div class="col-md-1" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:LinkButton ID="btnCirugias" runat="server" class="btn btn-success" OnClick="btnCirugias_Click"  ><i class="fa fa-search"></i> Cirugias</asp:LinkButton>
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <select id="cboFinanc" class='form-control' onchange="fBuscaTBL('cboFinanc', 'tblbscrJS', 8, this.value); contarFilTbl2(''); " >
                                <option value="">Todos</option>
                                <option value="SIS">SIS</option>
                            </select>
                            <code style="font-size:large; background-color:transparent; ">Seguro</code>
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <select id="cboTurno" class='form-control' onchange="fBuscaTBL('cboTurno', 'tblbscrJS', 9, this.value); contarFilTbl2(''); " >
                                <option value="">Todos</option>
                                <option value="M">M</option>
                                <option value="T">T</option>
                            </select>
                            <code style="font-size:large; background-color:transparent; ">Turno</code>
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column hide">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <select id="cboSOP" class='form-control' onchange="fBuscaTBL('cboSOP', 'tblbscrJS', 10, this.value); " >
                                <option value="">Todos</option>
                                <option value="Cirugia">con Cirugia</option>
                            </select>
                            <code style="font-size:large; background-color:transparent; ">Cirugia</code>
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column">
                  <div class="col-md-1" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <input type="text" id="txtTotal" class='form-control' readonly/>
                            <code style="font-size:large; background-color:transparent; ">Total</code>
                        </div>
                     </div>
                  </div>
               </div>


               <!-- /.box-header -->
            </div>
            <!--FIN SEGUNDA FILA-->

<script>
	var inputText = document.getElementById("<%=txtProd.ClientID%>");
	inputText.addEventListener("keyup", function(event) {
      if (event.keyCode === 13) {
		  event.preventDefault();
		  document.getElementById("<%=bntBuscar.ClientID%>").click();
      }
    });

    function fBuscaTBL(cboNombre, tblDondeBuscar, NroColumnaBuscar, ValorCbo) {

        //if (cboNombre != "cboFinanc") { document.getElementById("cboFinanc").selectedIndex = "0"; }
        //if (cboNombre != "cboTurno") { document.getElementById("cboTurno").selectedIndex = "0"; }
        //if (cboNombre != "cboSOP") { document.getElementById("cboSOP").selectedIndex = "0"; }
        
        //fBscTblHTML(cboNombre, 'tblbscrJS', NroColumnaBuscar);
        fBscTblHTML5('tblbscrJS', "cboFinanc", '8', "cboTurno", '9', "cboSOP", '10', "cboSOP", '10', "cboSOP", '10')
    }

    function fMostrarTXT(id) {
        document.getElementById(id).value = "";
        fBscTblHTML(id, 'tblbscrJS', 4);

        if (document.getElementById(id).style.display == "block") {
            document.getElementById(id).style.display = "none";
        }
        else {
            document.getElementById(id).style.display = "block";
            document.getElementById(id).focus();
        }
        
    }
</script>


            <!-- TERCERA FILA -->
            <div class="row">
               <div class="col-md-12">
                  <div class="box" >
                        <div class="box-header hide">
                            <h3 class="box-title" style="margin-top: -6px; margin-bottom: -11px; margin-left: -7px;">
                            
            <div class="input-group margin">
                            <div class="input-group-btn">
                                    <button class="btn btn-default" type="button" title="Max.: 1,000 Registros" onclick="exportTableToExcel('<%=GVtable.ClientID%>')"><i class="fa fa-download "></i>
                                    </button>
                            </div>
                            <div><input type="text" class="form-control" id="bscprod2" placeholder="Buscar" onkeyup="fBscTblHTML('bscprod2', '<%=GVtable.ClientID%>', 4)" autofocus="autofocus">
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

<script type="text/javascript">
    /// Ordenar Tabla
    const getCellValue = (tr, idx) => tr.children[idx].innerText || tr.children[idx].textContent;

    const comparer = (idx, asc) => (a, b) => ((v1, v2) => 
        v1 !== '' && v2 !== '' && !isNaN(v1) && !isNaN(v2) ? v1 - v2 : v1.toString().localeCompare(v2)
        )(getCellValue(asc ? a : b, idx), getCellValue(asc ? b : a, idx));

    // do the work...
    document.querySelectorAll('th').forEach(th => th.addEventListener('click', (() => {
        const table = th.closest('table');
        Array.from(table.querySelectorAll('tr:nth-child(n+2)'))
            .sort(comparer(Array.from(th.parentNode.children).indexOf(th), this.asc = !this.asc))
            .forEach(tr => table.appendChild(tr) );
    })));

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
