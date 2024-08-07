<%@ Page Language="C#" MasterPageFile="../../MPVotacion.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ASPXMyS_IROJVAR_Inicio_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPhead1" Runat="Server">
   <title>IRO - Votacion</title>
    <script>
		const intervalID = setTimeout(fvotar, 200);

		function fvotar()
		{
			location = "../Votacion/Votar.aspx"; 
		}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPbodyContent1" Runat="Server">
   <form id="form1" runat="server">

   </form>
</asp:Content>
