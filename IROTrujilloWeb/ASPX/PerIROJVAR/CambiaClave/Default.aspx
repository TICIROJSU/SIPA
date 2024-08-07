<%@ Page Language="C#" MasterPageFile="../../MasterPagePer.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ASPX_PerIROJVAR_CambiaClave_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>Cambio de Clave</title>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div class="col-xs-8 col-xs-offset-2">
        <p class="cazador2">Cambio de Clave</p>
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
                  <div class="col-md-2" >
                     <div class="box-body">
                         <asp:TextBox ID="txtDNI" runat="server" class="form-control"></asp:TextBox>
                     </div>
                  </div>
               </div>
            </div>

            <div class="row" style="display:block;">
               <div class="column">
                  <div class="col-md-2" >
                     <div class="box-body"><label for="txtAnterior">Clave Actual</label></div>
                  </div>
               </div>
               <div class="column">
                  <div class="col-md-3" >
                     <div class="box-body">
                            <asp:TextBox ID="txtAnterior" runat="server" class="form-control" placeholder="Clave Actual" AutoComplete="off"></asp:TextBox>
                     </div>
                  </div>
               </div>
            </div>

            <div class="row" style="display:block;">
               <div class="column">
                  <div class="col-md-2" >
                     <div class="box-body"><label for="txtNuevo1">Clave Nueva</label></div>
                  </div>
               </div>
               <div class="column">
                  <div class="col-md-3" >
                     <div class="box-body">
                            <asp:TextBox ID="txtNuevo1" runat="server" class="form-control" placeholder="Clave Nueva" AutoComplete="off"></asp:TextBox>
                     </div>
                  </div>
               </div>
            </div>

            <div class="row" style="display:block;">
               <div class="column">
                  <div class="col-md-2" >
                     <div class="box-body"><label for="txtNuevo2">Repita Clave Nueva</label></div>
                  </div>
               </div>
               <div class="column">
                  <div class="col-md-3" >
                     <div class="box-body">
                            <asp:TextBox ID="txtNuevo2" runat="server" class="form-control" placeholder="Repita Clave Nueva" AutoComplete="off"></asp:TextBox>
                     </div>
                  </div>
               </div>
            </div>

            <div class="row" style="display:block;">
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

    <script src="../../Estilos/bower_components/inputmask/dist/min/inputmask/jquery.inputmask.min.js"></script>
    <script src="../../Estilos/bower_components/inputmask/dist/min/inputmask/inputmask.date.extensions.min.js"></script>
    <script src="../../Estilos/bower_components/inputmask/dist/min/inputmask/inputmask.extensions.min.js"></script>
    <script src="../../Estilos/bower_components/moment/min/moment.min.js"></script>


</asp:Content>
