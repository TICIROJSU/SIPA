<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HisMinsaWebService.aspx.cs" Inherits="ASPX_Terminos_HisMinsaWebService" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="CargaHISMINSA" runat="server" Text="Carga HISMINSA" OnClick="CargaHISMINSA_Click" />
            <asp:Literal ID="LitToken" runat="server" Text=""></asp:Literal>


        </div>
    </form>
</body>
</html>
