<%@ Page Language="C#" MasterPageFile="../../MasterPage.master" AutoEventWireup="true" CodeFile="Inicio.aspx.cs" Inherits="ASPX_IROJVAR_CIEIMaster_Inicio" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<title>IRO - CIEI</title>
    <link rel="stylesheet" media="screen" href="style1.css?id=1">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
	<div class="col-xs-8 col-xs-offset-2">
        <p class="cazador2">Edicion de la Pagina de Inicio - CIEI</p>
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
                        <div class="form-group">
                            <p></p>
                                <div class="form-group has-warning">
                                <asp:TextBox ID="txtTitulo" runat="server" class="form-control input-lg" placeholder="Titulo de Inicio">

                                </asp:TextBox>
                                </div>
                            <p></p>
<%--<section class="fdf-block" id="editor-docs">--%>
<%--<section id="editor-docs">--%>
  <%--<div class="container">--%>
    <%--<div class="row">--%>
      <%--<div class="col-xs-12 col-sm-9">--%>
		<textarea id="froala_editor" class="clearfix" style="display: none;" runat="server">
			Inserte Texto. 2.
            <%--<asp:Literal ID="LitSIContenido" runat="server"></asp:Literal>--%>
		</textarea>
		<br />
      <%--</div>--%>
    <%--</div>--%>
  <%--</div>--%>
<%--</section>--%>
  
<script src="application3.js?id=s"></script>
<script>
  new FroalaEditor('textarea#<%=froala_editor.ClientID%>')
</script>
                            <div class="box-body">
                                <div class="form-group">
                                    <asp:LinkButton ID="bntGuardarSI" runat="server" class="btn btn-success" OnClick="bntGuardarSI_Click" ><i class="fa fa-search"></i> Guardar Sup Izq</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                     </div>
                  </div>
               </div>
                <div class="column">
                  <div class="col-md-4" style="text-align: center;padding:  0px 30px 0px 30px;">
                     <div class="box-body">
                        <div class="form-group">
                            <br />
                                <div class="form-group has-warning">
                                <asp:TextBox ID="txtURLimg" runat="server" TextMode="MultiLine" class="form-control input-lg" placeholder="URL Imagen" onchange="gDriveIdRecov(this.value)" onkeyup="gDriveIdRecov(this.value)" onfocus="this.select();"></asp:TextBox>
                                <asp:TextBox ID="txtURLimgID" runat="server" class="form-control input-lg" ></asp:TextBox>
                                </div>
                            <p></p>
                            <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-success" OnClick="bntGuardarSD_Click" ><i class="fa fa-search"></i> Guardar Sup Der</asp:LinkButton>
<script>
    function gDriveIdRecov(url)
	{
        var arrayURL = url.split("/");
        document.getElementById("<%=txtURLimgID.ClientID%>").value = arrayURL[5];
		document.getElementById('imgMiemb').src = "http://drive.google.com/uc?export=view&id=" + arrayURL[5] + "";
    }
</script>
                            <asp:Literal ID="LitSDMiembros" runat="server"></asp:Literal>
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

            <div class="column">
                <div class="col-md-5" style="text-align: center;padding:  0px 30px 0px 30px;">
                    <div class="box-body">
                    <div class="form-group">
                        <asp:Literal ID="Literal3" runat="server"></asp:Literal>
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
                        <asp:Literal ID="Literal2" runat="server"></asp:Literal>
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
        document.getElementById("ulCIEImINI").classList.add('active');
    </script>
</asp:Content>
