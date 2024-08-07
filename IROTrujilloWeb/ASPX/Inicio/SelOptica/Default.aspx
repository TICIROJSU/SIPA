<%@ Page Language="C#" MasterPageFile="../../MPOptica.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ASPX_Inicio_SelOptica_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>Optica Iro Trujillo</title>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div class="col-xs-8 col-xs-offset-2">
        <p class="cazador2">Selecciona una Opcion</p>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
   <form id="form1" runat="server">
      <div class="content-wrapper">
      
      </div>

   </form>

    <script>
        document.getElementById("LiMOV").classList.add('menu-open');
        document.getElementById('UlMOV').style.display = 'block';
        var f = new Date();

    </script>

</asp:Content>
