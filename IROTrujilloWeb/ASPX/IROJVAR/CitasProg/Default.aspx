<%@ Page Language="C#" MasterPageFile="../../MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ASPX_IROJVAR_CitasProg_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>Programacion - Citas</title>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div class="col-xs-8 col-xs-offset-2">
        <p class="cazador2">Programacion - Citas</p>
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
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                           <asp:DropDownList ID="DDLAnio" runat="server" class='form-control' >
                              <asp:ListItem Value="2019">2019</asp:ListItem>
                              <asp:ListItem Value="2020" Selected="true">2020</asp:ListItem>
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
               <div class="column">
                  <div class="col-md-2" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                           <asp:DropDownList ID="DDLMes" runat="server" class='form-control' >
                              <asp:ListItem Value="1">01</asp:ListItem>
                              <asp:ListItem Value="2">02</asp:ListItem>
                              <asp:ListItem Value="3">03</asp:ListItem>
                              <asp:ListItem Value="4">04</asp:ListItem>
                              <asp:ListItem Value="5">05</asp:ListItem>
                              <asp:ListItem Value="6">06</asp:ListItem>
                              <asp:ListItem Value="7">07</asp:ListItem>
                              <asp:ListItem Value="8">08</asp:ListItem>
                              <asp:ListItem Value="9">09</asp:ListItem>
                              <asp:ListItem Value="10">10</asp:ListItem>
                              <asp:ListItem Value="11">11</asp:ListItem>
                              <asp:ListItem Value="12">12</asp:ListItem>
                           </asp:DropDownList>
                        </div>
                     </div>
                  </div>
               </div>

               <div class="column" style="display:block">
                  <div class="col-md-3" style="text-align: center;padding:  0px 30px 0px 30px;">
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

            <div class="row" style="display:none;">
               <div class="column">
                  <div class="col-md-12" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <h2><a >Analisis de Disponibilidad con Distribucion</a></h2>
                            <%--<h2><a onserverclick="ExcelAnalisisDistribDispo_Click" runat="server">Analisis de Disponibilidad con Distribucion</a></h2>--%>
                            
                        </div>
                     </div>
                  </div>
               </div>
            </div>

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

