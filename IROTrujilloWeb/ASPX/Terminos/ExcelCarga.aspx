<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExcelCarga.aspx.cs" Inherits="ASPX_Terminos_ExcelCarga" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<script>
function fileValidation(){
    var fileInput = document.getElementById('<%=FileUpload1.ClientID%>');
    var filePath = fileInput.value;
    var allowedExtensions = /(.xlsx|.xls)$/i;
    if(!allowedExtensions.exec(filePath)){
        alert('Por favor, seleccione unicamente excel superior a 2007.');
        fileInput.value = '';
        return false;
    }else{
        //Image preview
        if (fileInput.files && fileInput.files[0]) {
            var reader = new FileReader();
            reader.onload = function(e) {
                document.getElementById('imagePreview').innerHTML = '<img src="'+e.target.result+'"/>';
            };
            reader.readAsDataURL(fileInput.files[0]);
        }
    }
}
</script>
    <form id="form1" runat="server">
        <div>
            <asp:FileUpload ID="FileUpload1" runat="server" onchange="return fileValidation()"/>
            <asp:Button ID="btnImport1" runat="server" Text="Cargar1" OnClick="ImportExcel" />
            <asp:Button ID="btnImport2" runat="server" Text="Cargar2" OnClick="ImportExcel2" />
            <asp:Button ID="btnGuarda" runat="server" Text="Guardar" OnClick="Guardar1" />
            <hr />
            <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        </div>
        <div>
            <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
