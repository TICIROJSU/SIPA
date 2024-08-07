<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Prueba01.aspx.cs" Inherits="ASPXMyS_IROJVAR_Votacion_Prueba01" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Consulta Inicial MySql
        </div>
        <br />
        <asp:Button ID="btnConsulta" runat="server" Text="Consultar" OnClick="btnConsulta_Click" />
        <br />
        <asp:GridView ID="gvTable" runat="server"></asp:GridView>

    </form>
</body>
</html>
