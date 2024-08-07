<%@ Page Language="C#" MasterPageFile="../../MasterPage.master" AutoEventWireup="true" CodeFile="Consultas.aspx.cs" Inherits="ASPX_IROJVAR_CIEIMaster_Consultas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>IRO - CIEI</title>
    <%--<link rel="stylesheet" media="screen" href="style1.css?id=1">--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div class="col-xs-8 col-xs-offset-2">
        <p class="cazador2">Edicion de la Pagina de Consultas - CIEI</p>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
   <form id="form1" runat="server" autocomplete="off">
      <!-- Content Wrapper. Contains page content -->
      <div class="content-wrapper">
         <!-- Content Header (Page header) -->
         <!-- Main content -->
         <section class="content">
        <div class="row" style="display:none;" id="ConsultaContenido">
            <div class="column">
                <div class="col-md-11" style="text-align: center;padding:  0px 30px 0px 30px;">
                    <div class="box-body">
                    <div class="form-group box box-primary">
                        <div class="form-group box box-primary">
                            <h1>Consultas - Contenido</h1>
                                <div class="form-group has-success">
    <div class="col-sm-8">
        <asp:TextBox ID="txtTitulo" runat="server" class="form-control input-lg" placeholder="Titulo" onfocus="this.select();"></asp:TextBox>        
    </div>
    <div style="display:none">
        <asp:TextBox ID="txtid" runat="server" class="form-control input-lg"></asp:TextBox>
    </div>
    <div class="col-sm-2">
        <asp:TextBox ID="txtPagina" runat="server" class="form-control input-lg" ReadOnly="True"></asp:TextBox>
    </div>
    <div class="col-sm-2">
        <asp:TextBox ID="txtSeccion" runat="server" class="form-control input-lg" ReadOnly="True"></asp:TextBox>
    </div>
    <br /><br />

    <br />
    <asp:TextBox ID="txtContenido" runat="server" class="form-control input-lg"  placeholder="Contenido" onfocus="this.select();"></asp:TextBox>
    <br />
    <div class="col-sm-4">
    <asp:TextBox ID="txtDetalle1" runat="server" class="form-control input-lg"  placeholder="Detalle1" onfocus="this.select();"></asp:TextBox>
    </div>
    <div class="col-sm-4">
    <asp:TextBox ID="txtDetalle2" runat="server" class="form-control input-lg"  placeholder="Detalle2" onfocus="this.select();"></asp:TextBox>
    </div>
    <div class="col-sm-4">
    <asp:TextBox ID="txtDetalle3" runat="server" class="form-control input-lg"  placeholder="Detalle3" onfocus="this.select();"></asp:TextBox>
    </div>
                                    <br />
                                </div>
                            <p><br /></p>
                            <asp:LinkButton ID="bntGuardar" runat="server" class="btn btn-primary" OnClick="bntGuardar_Click" ><i class="fa fa-save"></i>  Guardar Contenido</asp:LinkButton>
                            <asp:Literal ID="LitSITar" runat="server"></asp:Literal>
                        </div>
                    </div>
                    </div>
                </div>
            </div>
        </div>
            <!-- FIN PRIMERA FILA -->
<script>
    function cargaContenido(id, Pagina, Seccion, Titulo, Contenido, Det1, Det2, Det3)
    {
        document.getElementById('ConsultaContenido').style.display = 'block';
        document.getElementById('<%=txtid.ClientID%>').value = id;
        document.getElementById('<%=txtPagina.ClientID%>').value = Pagina;
        document.getElementById('<%=txtSeccion.ClientID%>').value = Seccion;
        document.getElementById('<%=txtTitulo.ClientID%>').value = Titulo;
        document.getElementById('<%=txtContenido.ClientID%>').value = Contenido;
        document.getElementById('<%=txtDetalle1.ClientID%>').value = Det1;
        document.getElementById('<%=txtDetalle2.ClientID%>').value = Det2;
        document.getElementById('<%=txtDetalle3.ClientID%>').value = Det3;
    }
</script>
            <!--SEGUNDA FILA-->
            <div class="row" style="display:block;">
               <div class="column">
                  <div class="col-md-11" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group box box-info">
                            <asp:Literal ID="LitSIConsulta" runat="server"></asp:Literal>
                        </div>
                     </div>
                  </div>
               </div>
                <div class="column">
                  <div class="col-md-1" style="text-align: center;padding:  0px 30px 0px 30px; display:none">
                     <div class="box-body">
                        
                     </div>
                  </div>
               </div>
           </div>

        <div class="row" style="display:block;">
            <div class="column">
                <div class="col-md-5" style="text-align: center;padding:  0px 30px 0px 30px;">
                    <div class="box-body">
                    <div class="form-group">
                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                    </div>
                    </div>
                </div>
            </div>
        </div>

         </section>
         <!-- FIN MAIN CONTENT -->
         <!-- FIN MAIN CONTENT -->
         <!-- FIN MAIN CONTENT -->
         <!-- FIN MAIN CONTENT -->
      </div>
      <!-- /.content-wrapper -->
   </form>
    <script>
        //document.getElementById('LiCIEIm').className = "treeview menu-open";
        //$('#LiCIEIm').addClass('menu-open');
        document.getElementById("LiCIEIm").classList.add('menu-open');
        document.getElementById('UlCIEIm').style.display = 'block';
        document.getElementById("ulCIEImCON").classList.add('active');
    </script>
</asp:Content>


