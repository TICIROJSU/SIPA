<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EnviaCorreo.aspx.cs" Inherits="IROTWeb_EnviaCorreo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Prueba para enviar correo usando ASP.NET 2.0 (con C#)</title>
</head>
<body>
    <form id="form1" runat="server">
        <table style="width: 550px">
            <tr>
                <td valign="top">
                    <asp:Label ID="Label1" runat="server" Text="De:"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtDe" runat="server" Width="95%" BackColor="Gainsboro" ReadOnly="False">jvaguirrerosales@gmail.com</asp:TextBox></td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:Label ID="Label2" runat="server" Text="Para:"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtPara" runat="server" Width="95%">grllufgss@gmail.com</asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPara" ErrorMessage="El formato del correo no es válido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:Label ID="Label3" runat="server" Text="Asunto:"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtAsunto" runat="server" Width="95%">Prueba de envio de correo con ASP.NET 2.0 (C#)</asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAsunto" ErrorMessage="Debes escribir el asunto"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:Label ID="Label4" runat="server" Text="Texto:"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtTexto" runat="server" Columns="50" Rows="10" TextMode="MultiLine"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTexto" ErrorMessage="Debes escribir algo en el texto"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td><asp:Button ID="btnEnviar" runat="server" Text="Enviar" OnClick="btnEnviar_Click" /></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td><asp:Label ID="LabelError" runat="server" Text=""></asp:Label></td>
            </tr>
        </table>
    </form>
</body>

</html>
