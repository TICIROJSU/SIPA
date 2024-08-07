<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileUpload.aspx.cs" Inherits="zPruebas_FileUpload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

				<div class="input-group input-group">
					<asp:FileUpload ID="FileUpload1" runat="server" class="form-control" />
					<span class="input-group-btn">
						<asp:Button id="UploadButton" class="btn btn-primary btn-flat" Text="Guardar Archivo Evidencia" OnClick="UploadButton_Click" runat="server"> </asp:Button>
					</span>
				</div>

        </div>
    </form>
</body>
</html>
