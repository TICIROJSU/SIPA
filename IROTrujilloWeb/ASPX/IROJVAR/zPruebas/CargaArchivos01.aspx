<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CargaArchivos01.aspx.cs" Inherits="ASPX_IROJVAR_zPruebas_CargaArchivos01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"> 

<script runat="server">

  protected void UploadButton_Click(object sender, EventArgs e)
  {
    String savePath = @"J:\IROTrujilloWeb\IROTrujilloWeb\ASPX\Carga\";
    if (FileUpload1.HasFile)
    {
      String fileName = FileUpload1.FileName;      
      savePath += fileName;
      FileUpload1.SaveAs(savePath);
      UploadStatusLabel.Text = "Tu Archivo fue guardado como " + fileName;
    }
    else
    { UploadStatusLabel.Text = "No haz Cargado ningun Archivo."; }
  }
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>FileUpload Example</title>

    <script language="javascript" type="text/javascript">

		function UploadButton2() {
            var params = new Object();
			params.FileUpload1_ruta = document.getElementById("<%=FileUpload1.ClientID%>").value;
			params = JSON.stringify(params);
			<%--var vIDPER = document.getElementById("<%=txtIdPer.ClientID%>").value;--%>
		
            $.ajax({
                type: "POST", url: "CargaArchivos01.aspx/CargaArchivo01", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#<%=UploadStatusLabel.ClientID%>").html(result.d) }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#<%=UploadStatusLabel.ClientID%>").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
			});
		}

    </script>


</head>
<body>
    <form id="form1" runat="server">
    <div>
       <h4>Select a file to upload:</h4>
   
       <asp:FileUpload id="FileUpload1"                 
           runat="server" onchange="javascript: document.getElementById('txtprueba').value = this">
       </asp:FileUpload>
            
       <br /><br />
       
       <asp:Button id="UploadButton" 
           Text="Upload file"
           OnClick="UploadButton_Click"
           runat="server">
       </asp:Button>    
       
       <hr />
		<input type="button" name="name" value="Carga Archivo" onclick="UploadButton2()" />
       <hr />
       
       <asp:Label id="UploadStatusLabel"
           runat="server">
       </asp:Label>
    </div>
		<input type="text" id="txtprueba" value="" />

    </form>
</body>
</html>

