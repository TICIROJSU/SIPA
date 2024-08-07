<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VideoReproductor.aspx.cs" Inherits="ASPX_ExtIROJVAR_Publico_VideoReproductor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <script>
	    function fListaCitados() {
            var params = new Object();
            params.var = "";
            params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "ListaCitaxFecha2.aspx/GetListaCitados", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divShowPac").html(result.d); cambiar(); }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    //alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divShowPac").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
		    });

            timerId3 = setInterval(fhPaciente, 200);

        
	    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>

            <input type="button" autofocus="autofocus" OnClick="fIniciar()" value="Iniciar" />
            <label id="info"></label>
            
            <br />
            <video id="reproductor" controls width="80%" height="55%"></video>


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

    <script>
        window.onload = function playlist(){
             var reproductor = document.getElementById("reproductor"),
                 videos = ["0002-Musica", "0001-Ojo", "0004-Musica", "0003-Ojo", "0006-Musica", "0005-ArbolParlanchin", "0007-Musica"],
                 info = document.getElementById("info");
 
             info.innerHTML = "Vídeo: " + videos[0];
             reproductor.src = videos[0] + ".mp4";
             reproductor.play();
 
            reproductor.addEventListener("ended", function() {
                var nombreActual = info.innerHTML.split(": ")[1];
                var actual = videos.indexOf(nombreActual);
                this.src = (actual == videos.length - 1 ? videos[0] : videos[actual + 1]) + ".mp4";
                info.innerHTML = "Vídeo: " + videos[actual + 1];

                if (info.innerHTML == "Vídeo: 0007-Musica")
                {
                    info.innerHTML = "Vídeo: 0002-Musica";
                }

                this.play();

            }, false);
        }
    </script>

</body>
</html>
