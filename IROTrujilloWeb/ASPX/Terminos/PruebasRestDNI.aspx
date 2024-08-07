<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PruebasRestDNI.aspx.cs" Inherits="ASPX_Terminos_PruebasRestDNI" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnCargaToken" runat="server" Text="Carga DNI" OnClick="btnCargaToken_Click" />
            <asp:Literal ID="LitToken" runat="server"></asp:Literal>


        </div>
    </form>
</body>
</html>
