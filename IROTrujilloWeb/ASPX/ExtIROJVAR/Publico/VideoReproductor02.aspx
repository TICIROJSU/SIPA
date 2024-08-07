<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VideoReproductor02.aspx.cs" Inherits="ASPX_ExtIROJVAR_Publico_VideoReproductor02" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/jquery-1.11.1.min.js")%>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/json2.js")%>"></script>

    <script>

        var fgetvideos = "05";
        var fvideos = "05"; 

        videos = ['Videos/aInicial', 'Videos/0001-Ojo', 'Videos/zFinal'];

	    function fListaCitados() { 
            var params = new Object();
            params.var = "";
            params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "VideoReproductor02.aspx/GetListaVideos", data: params,
                contentType: "application/json; charset=utf-8", dataType: "json", async: true,
                success: function (result) {
                    fgetvideos = result.d;
                }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    //alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divError").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });

            if (fgetvideos != fvideos) {
                location.reload();
            }

            document.getElementById("btnIniciar").click();
            //document.getElementById("reproductor").play();

        }

        timerId1 = setInterval(fListaCitados, 5000); /// 1000 = 1 seg

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>

            <input type="button" autofocus="autofocus" OnClick="fIniciar()" value="Iniciar" id="btnIniciar" />
            <label id="info"></label>
            
            <br />
            <video id="reproductor" controls width="85%" height="55%"></video>


        </div>
    </form>

    <div id="divError"></div>

    <script>
        function fIniciar() {
            let vid = document.getElementById("reproductor");
            vid.muted = false;
            vid.play();
            vid.requestFullscreen();
        }

    </script>

    <script>
        window.onload = function playlist(){
             var reproductor = document.getElementById("reproductor"),
                 info = document.getElementById("info");
 
             info.innerHTML = "Vídeo: " + videos[0];
             reproductor.src = videos[0] + ".mp4";
             reproductor.play();
 
            reproductor.addEventListener("ended", function() {
                var nombreActual = info.innerHTML.split(": ")[1];
                var actual = videos.indexOf(nombreActual);
                this.src = (actual == videos.length - 1 ? videos[0] : videos[actual + 1]) + ".mp4";
                info.innerHTML = "Vídeo: " + videos[actual + 1];
                this.play();
            }, false);

        }
    </script>


    <script src="<%=ResolveUrl("../../Estilos/bower_components/jquery/dist/jquery.min.js")%>"></script>

</body>
</html>
