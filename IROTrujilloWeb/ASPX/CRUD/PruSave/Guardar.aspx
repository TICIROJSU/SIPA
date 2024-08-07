<%@ Page Language="C#" MasterPageFile="../../MasterPage.master" AutoEventWireup="true" CodeFile="Guardar.aspx.cs" Inherits="ASPX_CRUD_PruSave_Guardar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>Tiempos de Espera</title>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div class="col-xs-8 col-xs-offset-2">
        <p class="cazador2">Descarga Consultas SQL</p>
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
                  <div class="col-md-3" style="padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <label for="txtnombre">Nombre</label>
                            <asp:TextBox ID="txtnombre" runat="server" class="form-control" placeholder="Nombre" AutoComplete="off"></asp:TextBox>
                        </div>
                     </div>
                  </div>
               </div>
               <div class="column">
                  <div class="col-md-3" style="padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <label for="txtnombre">Fecha</label>
                            <asp:TextBox ID="datepicker" runat="server" class="form-control pull-right" AutoComplete="off"></asp:TextBox>
                            <%--<input type="text" class="form-control pull-right" id="datepicker">--%>
                        </div>
                     </div>
                  </div>
               </div>
               <div class="column">
                  <div class="col-md-3" style="padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <label for="txtnombre">Cantidad</label>
                            <asp:TextBox ID="txtcantidad" runat="server" class="form-control" placeholder="Cantidad" AutoComplete="off"></asp:TextBox>

                        </div>
                     </div>
                  </div>
               </div>

               <div class="column" style="display:block">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <label for="txtnombre">&nbsp;</label>
                            <asp:LinkButton ID="btnGuardar" runat="server" class="btn btn-success" OnClick="btnGuardar_Click" ><i class="fa fa-search"></i> Guardar</asp:LinkButton>
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
                                    <button class="btn btn-default" runat="server" type="button" title="Max.: 3,000 Registros" >
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
                         <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>


                     </div>
                     <!-- /.box-body -->
                  </div>
                  <!-- /.box -->
               </div>
            </div>
             <script>
                        <%
                 if (true)
                 {%>
                     document.getElementById('<%=Label1.ClientID%>').innerHTML = "Hola";
                 <%}
                        %>
                 <%="document.getElementById('"+Label1.ClientID+"').innerHTML = 'Hjs';"%>
             </script>
            <!--FIN TERCERA FILA-->
            <!-- CUARTA FILA-->
            <!-- CUARTA FILA-->
            <!-- CUARTA FILA-->
            <!-- CUARTA FILA-->
            
             <asp:Literal ID="LitGraf1" runat="server"></asp:Literal>

            <!-- FIN CUARTA FILA-->
            <!-- FIN CUARTA FILA-->
            <!-- FIN CUARTA FILA-->
            <!-- FIN CUARTA FILA-->
         </section>
         <!-- FIN MAIN CONTENT -->
         <!-- FIN MAIN CONTENT -->
         <!-- FIN MAIN CONTENT -->
         <!-- FIN MAIN CONTENT -->
      </div>
      <!-- /.content-wrapper -->
   </form>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder4Script" Runat="Server">

    <script src="../../Estilos/bower_components/select2/dist/js/select2.full.min.js"></script>
    <script src="../../Estilos/bower_components/inputmask/dist/min/inputmask/jquery.inputmask.min.js"></script>
    <script src="../../Estilos/bower_components/inputmask/dist/min/inputmask/inputmask.date.extensions.min.js"></script>
    <script src="../../Estilos/bower_components/inputmask/dist/min/inputmask/inputmask.extensions.min.js"></script>
    <script src="../../Estilos/bower_components/moment/min/moment.min.js"></script>
    <script src="../../Estilos/bower_components/bootstrap-daterangepicker/daterangepicker.js"></script>
    <script src="../../Estilos/bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <script src="../../Estilos/bower_components/bootstrap-datepicker/dist/locales/bootstrap-datepicker.es.min.js"></script>
    <script>
        $(function () {
            //Date picker
            //$('#ctl00_ContentPlaceHolder2_datepicker').datepicker({
            $('#<%=datepicker.ClientID%>').datepicker({
                //format: "DD dd/mm/yyyy",
                language: "es",
                todayHighlight: true,
                autoclose: true
            })
        })
    </script>
</asp:Content>