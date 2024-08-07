<%@ Page Language="C#" MasterPageFile="../../MPSaludInfantil.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="ASPX_Inicio_SelSInf_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>Consultas Salud Infantil</title>
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
        document.getElementById("LICompG").classList.add('menu-open');
        document.getElementById('UlCompG').style.display = 'block';

    </script>

</asp:Content>