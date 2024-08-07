<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VideoReproductorSolo.aspx.cs" Inherits="ASPX_ExtIROJVAR_Publico_VideoReproductorSolo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <input type="button" autofocus="autofocus" OnClick="fIniciar()" value="Iniciar" />
            <label id="info"></label>
            
            <br />
            <video id="reproductor" controls loop autoplay width="33%" height="20%">
                <source src="Videos\20240506-DiaLavadoManos.mp4" type="video/mp4">
            </video>


        </div>
    </form>

    <script>
        function fIniciar() {
            let vid = document.getElementById("reproductor");
            vid.muted = false;
            vid.play();
            vid.requestFullscreen();
        }

    </script>

</body>
</html>
