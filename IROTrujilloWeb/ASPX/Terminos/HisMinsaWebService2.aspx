<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HisMinsaWebService2.aspx.cs" Inherits="ASPX_Terminos_HisMinsaWebService2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="CargaHISMINSA" runat="server" Text="Carga HISMINSA" OnClick="CargaHISMINSA_Click" />

            <asp:Literal ID="LitToken" runat="server" Text=""></asp:Literal>


            <asp:Literal ID="LitString" runat="server"></asp:Literal>

        </div>
    </form>
</body>
</html>
