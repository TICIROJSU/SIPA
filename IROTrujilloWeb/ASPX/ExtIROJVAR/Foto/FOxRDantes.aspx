<%@ Page Language="C#" MasterPageFile="../../MPFotos.master" AutoEventWireup="true" CodeFile="FOxRDantes.aspx.cs" Inherits="ASPX_ExtIROJVAR_Foto_FOxRDantes" %>




<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>IRO</title>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/json2.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/funciones.js?vfd=1")%>"></script>
    <%--<link rel="stylesheet" media="screen" href="style1.css?id=1">--%>

    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div class="col-xs-8 col-xs-offset-2">
        <p class="cazador2">Paciente ciego al menos un ojo debido a la RD</p>
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

    function fHideEESS(chk) {
        if (chk.checked) { document.getElementById("colEESS").style.display = 'block'; }
        else { document.getElementById("colEESS").style.display = 'none'; }
    }

    function fHideDNI(chk) {
        if (chk.checked) { document.getElementById("colDNI").style.display = 'block'; }
        else { document.getElementById("colDNI").style.display = 'none'; }
    }

    function fHideFecha(chk) {
        if (chk.checked)
        {
            document.getElementById("colFecDesde").style.display = 'block';
            document.getElementById("colFecHasta").style.display = 'block';
        }
        else
        {
            document.getElementById("colFecDesde").style.display = 'none';
            document.getElementById("colFecHasta").style.display = 'none';
        }
    }

</script>

            <!--SEGUNDA FILA-->             
            <div class="row" style="display:block;">

                <div class="column" id="colFecDesde">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:TextBox ID="txtFechaDesde" runat="server" class='form-control' type="date" ></asp:TextBox>
                            <code style="font-size:large; background-color:transparent; ">Desde</code>
                        </div>
                     </div>
                  </div>
                </div>
                
                <div class="column" id="colFecHasta">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <asp:TextBox ID="txtFechaHasta" runat="server" class='form-control' type="date" ></asp:TextBox>
                            <code style="font-size:large; background-color:transparent; ">Hasta</code>
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
	var inputText = document.getElementById("<%=txtFechaHasta.ClientID%>");
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
<style>
    #doublescroll
    {
      overflow: auto; overflow-y: hidden; 
    }

    #doublescroll p
    {
      margin: 0; 
      padding: 1em; 
      white-space: nowrap; 
    }
</style>
                     <!-- /.box-header -->
                    <div class="col-md-4">
                        <!-- id="habfiltro" -->
                         <div class="box-body table-responsive no-padding" id="doublescroll">
                            <asp:GridView ID="GVtable" runat="server" class="table table-condensed table-bordered"></asp:GridView>
                            <asp:Literal ID="LitTABL1" runat="server"></asp:Literal>
                         </div>
                    </div>

                    <div class="col-md-7">
                        <asp:Literal ID="LitGraf1" runat="server"></asp:Literal>
                    </div>
                      
                     <!-- /.box-body -->
                  </div>
                  <!-- /.box -->
               </div>
            </div>

            <!--FIN TERCERA FILA-->

             <div>
                 <asp:Image runat="server" ID="Image1" />
             </div>

<script>
    function DoubleScroll(element) {
        var scrollbar = document.createElement('div');
        scrollbar.appendChild(document.createElement('div'));
        scrollbar.style.overflow = 'auto';
        scrollbar.style.overflowY = 'hidden';
        scrollbar.firstChild.style.width = element.scrollWidth+'px';
        scrollbar.firstChild.style.paddingTop = '1px';
        scrollbar.firstChild.appendChild(document.createTextNode('\xA0'));
        scrollbar.onscroll = function() {
            element.scrollLeft = scrollbar.scrollLeft;
        };
        element.onscroll = function() {
            scrollbar.scrollLeft = element.scrollLeft;
        };
        element.parentNode.insertBefore(scrollbar, element);
    }

    DoubleScroll(document.getElementById('doublescroll'));
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

    <script>
        //const intervalID = setTimeout(showhideColumnPrev, 200);
        show_hide_column(0, false, "<%=GVtable.ClientID%>");

        function showhideColumnPrev() {
            show_hide_column(0, false, "<%=GVtable.ClientID%>");
        }

        function show_hide_column(col_no, do_show, idtable) 
        {
            var stl;
            if (do_show) stl = 'block';
            else         stl = 'none';

            var tbl  = document.getElementById(idtable);
            var rows = tbl.getElementsByTagName('tr');

            for (var row=0; row<rows.length;row++) {
                td = rows[row].getElementsByTagName("td")[col_no];
                if (td) { td.style.display = stl; }

                th = rows[row].getElementsByTagName("th")[col_no];
                if (th) { th.style.display = stl; }
            }
        }

    </script>

</asp:Content>
