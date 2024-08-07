<%@ Page Language="C#" MasterPageFile="../../MasterPage.master" AutoEventWireup="true" CodeFile="Cronograma.aspx.cs" Inherits="ASPX_IROJVAR_CIEIMaster_Cronograma" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>IRO - CIEI</title>
    <%--<link rel="stylesheet" media="screen" href="style1.css?id=1">--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div class="col-xs-8 col-xs-offset-2">
        <p class="cazador2">Edicion del Cronograma y Tarifa - CIEI</p>
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
                  <div class="col-md-6" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group box box-info">
                            <h1>Cronograma</h1>
                                <div class="form-group has-success">
                                    <asp:TextBox ID="txtTituloCron" runat="server" class="form-control input-lg" placeholder="Titulo" autocomplete="off"></asp:TextBox>
                                    <br />
                                    <asp:TextBox ID="txtContenidoCron" runat="server" TextMode="MultiLine" Rows="5" class="form-control input-lg"  placeholder="Contenido" autocomplete="off"></asp:TextBox>
                                    <br />
                                <asp:TextBox ID="txtURLimgCron" runat="server" TextMode="MultiLine" class="form-control input-lg" placeholder="URL Imagen" onchange="gDriveIdRecovCron(this.value)" onkeyup="gDriveIdRecovCron(this.value)" onfocus="this.select();" autocomplete="off"></asp:TextBox>
                                    <br />
                                <asp:TextBox ID="txtURLimgIDCron" runat="server" class="form-control input-lg" autocomplete="off"></asp:TextBox>
                                </div>
                            <p></p>
                            <asp:LinkButton ID="bntGuardarCronSI" runat="server" class="btn btn-success" OnClick="bntGuardarCronSI_Click" ><i class="fa fa-search"></i> Guardar Cronograma</asp:LinkButton>
<script>
    function gDriveIdRecovCron(url)
    {
        var arrayURL = url.split("/");
        document.getElementById("<%=txtURLimgIDCron.ClientID%>").value = arrayURL[5];
        document.getElementById('imgCron').src="http://drive.google.com/uc?export=view&id=" + arrayURL[5] + "";
    }
</script>
                            <div align="center">
                                <asp:Literal ID="LitSICron" runat="server"></asp:Literal>
                            </div>
                        </div>
                     </div>
                  </div>
               </div>
                <div class="column">
                  <div class="col-md-6" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group box box-primary">
                            <h1>Tarifario</h1>
                                <div class="form-group has-warning">
                                    <asp:TextBox ID="txtTituloTar" runat="server" class="form-control input-lg" placeholder="Titulo" autocomplete="off"></asp:TextBox>
                                    <br />
                                <asp:TextBox ID="txtURLimg" runat="server" TextMode="MultiLine" class="form-control input-lg" placeholder="URL Imagen" onchange="gDriveIdRecovTar(this.value)" onkeyup="gDriveIdRecovTar(this.value)" onfocus="this.select();" autocomplete="off"></asp:TextBox>
                                    <br />
                                <asp:TextBox ID="txtURLimgID" runat="server" class="form-control input-lg" autocomplete="off"></asp:TextBox>
                                </div>
                            <p></p>
                            <asp:LinkButton ID="bntGuardarTarSI" runat="server" class="btn btn-success" OnClick="bntGuardarTarSI_Click" ><i class="fa fa-search"></i> Guardar Tarifario</asp:LinkButton>
<script>
    function gDriveIdRecovTar(url)
    {
        var arrayURL = url.split("/");
        document.getElementById("<%=txtURLimgID.ClientID%>").value = arrayURL[5];
        document.getElementById('imgTar').src="http://drive.google.com/uc?export=view&id=" + arrayURL[5] + "";
    }
</script>
                            <asp:Literal ID="LitSITar" runat="server"></asp:Literal>
                        </div>
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
        document.getElementById("ulCIEImCRO").classList.add('active');
    </script>

</asp:Content>

