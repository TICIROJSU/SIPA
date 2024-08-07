<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListaCitaxFecha2.aspx.cs" Inherits="ASPX_ExtIROJVAR_Publico_ListaCitaxFecha2" %>

<html translate="no">
<head><meta http-equiv="Content-Type" content="text/html" /><meta charset="ISO-8859-1" />
   <title>IRO - Usuarios</title>
    <script type="text/javascript" src="../../Estilos/Funciones/json2.js"></script>
    <script type="text/javascript" src="../../Estilos/Funciones/funciones.js?vfd=1"></script>
    
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
      <!-- Tell the browser to be responsive to screen width -->

      <link rel="shortcut icon" href="../../Estilos/imagenes/favicon.png" type="image/x-icon" /><meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
      <!-- fullCalendar -->
      <link rel="stylesheet" href="../../Estilos/bower_components/fullcalendar/dist/fullcalendar.min.css" />
      <!-- Bootstrap 3.3.7 -->
      <link rel="stylesheet" href="../../Estilos/bower_components/bootstrap/dist/css/bootstrap.min.css" />
      <!-- Font Awesome -->
      <link rel="stylesheet" href="../../Estilos/bower_components/font-awesome/css/font-awesome.min.css" />
      <!-- Ionicons -->
      <link rel="stylesheet" href="../../Estilos/bower_components/Ionicons/css/ionicons.min.css" />
      <!-- Theme style -->
      <link rel="stylesheet" href="../../Estilos/dist/css/AdminLTE.min.css" />
      <!-- AdminLTE Skins. Choose a skin from the css/skins
         folder instead of downloading all of them to reduce the load. -->
      <link rel="stylesheet" href="../../Estilos/dist/css/skins/_all-skins.min.css" />
      <!-- Morris chart -->
      <link rel="stylesheet" href="../../Estilos/bower_components/morris.js/morris.css" />
      <!-- jvectormap -->
      <link rel="stylesheet" href="../../Estilos/bower_components/jvectormap/jquery-jvectormap.css" />
      <!-- Date Picker -->
      <link rel="stylesheet" href="../../Estilos/bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css" />
      <!-- Daterange picker -->
      <link rel="stylesheet" href="../../Estilos/bower_components/bootstrap-daterangepicker/daterangepicker.css" />
      <!-- bootstrap wysihtml5 - text editor -->
      <link rel="stylesheet" href="../../Estilos/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css" />
      <!-- Boton subir -->
      <link rel="stylesheet" href="../../Estilos/BotonIrArriba/Botonsubir.css" />
      <!-- iCheck -->
      <link rel="stylesheet" href="../../Estilos/plugins/iCheck/square/blue.css" />
      <!-- Select2 -->
      <link rel="stylesheet" href="../../Estilos/bower_components/select2/dist/css/select2.min.css" />
      <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
      <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
      <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
      <![endif]-->
      <!-- Google Font -->
      <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic" />
      <script src="Estilos/Funciones/funciones.js"></script>
		<!-- Development version: -->
		<!-- <script>document.write('<script src="../Funciones/funciones.js?dev=' + Math.floor(Math.random() * 100) + '"\><\/script>');</script> -->
      <script type="text/javascript" src="../../Estilos/Funciones/jquery-1.11.1.min.js"></script>
      <script type="text/javascript" src="../../Estilos/Funciones/jquery.canvasjs.min.js"></script>

    <script>
        // toggle full screen
        function toggleFullScreen() {
            var a = $(window).height() - 10;

            if (!document.fullscreenElement && // alternative standard method
                !document.mozFullScreenElement && !document.webkitFullscreenElement) { // current working methods
                if (document.documentElement.requestFullscreen) {
                    document.documentElement.requestFullscreen();
                } else if (document.documentElement.mozRequestFullScreen) {
                    document.documentElement.mozRequestFullScreen();
                } else if (document.documentElement.webkitRequestFullscreen) {
                    document.documentElement.webkitRequestFullscreen(Element.ALLOW_KEYBOARD_INPUT);
                }
            } else {
                if (document.cancelFullScreen) {
                    document.cancelFullScreen();
                } else if (document.mozCancelFullScreen) {
                    document.mozCancelFullScreen();
                } else if (document.webkitCancelFullScreen) {
                    document.webkitCancelFullScreen();
                }
            }
        }

        
    </script>

<script type="text/javascript">
    function $_GET(q,s) {
    s = s ? s : window.location.search;
    var re = new RegExp('&'+q+'(?:=([^&]*))?(?=&|$)','i');
    return (s=s.replace(/^[?]/,'&').match(re)) ? (typeof s[1] == 'undefined' ? '' : decodeURIComponent(s[1])) : undefined;
    }

    function speak(TextoVoz) {
        // Create a SpeechSynthesisUtterance
        const utterance = new SpeechSynthesisUtterance(TextoVoz);
        utterance.lang = "es-MX";

        utterance.volume = 1;

        // Select a voice
        const voices = speechSynthesis.getVoices();
        //utterance.voice = voices[0]; // Choose a specific voice
        //utterance.voiceURI = voices[0].voiceURI;
        //utterance.voice = voices.find(v => v.name === 'Microsoft Raul - Spanish (Mexico)')
        utterance.voice = voices.find(v => v.name === 'Microsoft Sabina - Spanish (Mexico)')

        console.log(voices);

        // Speak the text
        speechSynthesis.speak(utterance);
    }


</script>    

<style>
    .modal { overflow: auto !important; }
</style>

<script type="text/javascript">
    var color = ['#09127C', '#F1F2F9'];
    var bgcolor = ['#F1F2F9', '#09127C'];
	var num = 0;
	function cambiar() {
        document.getElementById('trial1').style.color = color[num];
        document.getElementById('trial1').style.background  = bgcolor[num];
        num++;
        if (num==color.length) num=0;
	}
    //timerId0 = setInterval(cambiar, 3000);

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

    // Habilitar para funcionar //fListaCitados();
    // Habilitar para funcionar //timerId1 = setInterval(fListaCitados, 5500);

    var repetirPaciente = "";
    var repetirPacienteNum = 0;
    function fhPaciente() {
        if (repetirPaciente != document.getElementById('vhPaciente').value) {
            //if (repetirPacienteNum == 0) {
                audio = new Audio("tonomessenger2000.mp3");
                audio.playbackRate = 0.8;
                audio.play();
            //}
            speak(document.getElementById('vhPaciente').value);
            if (repetirPacienteNum > 2) {
                repetirPacienteNum = 0;
            }
        }
        repetirPacienteNum++;
        if (repetirPacienteNum > 2) {
            repetirPaciente = document.getElementById('vhPaciente').value;
            clearInterval(timerId3);
            timerId3 = null;
        }

    }

</script>
    
    <script>

        vActualizacion = "";

        function fParamActualizaciones() {
            var params = new Object();
            params.var = ""; 
            params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "ListaCitaxFecha2.aspx/getParamActualizar", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { vActualizacion = result.d }, //success: LoadPrueba01, //Procesar 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divDetAtencion").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });
        }
    </script>

</head>
<body class="skin-blue sidebar-mini sidebar-collapse" onload="setInterval('cambiar()', 1000)">
    <div class="wrapper">
      
      <!-- Left side column. contains the logo and sidebar -->
      <!-- Left side column. contains the logo and sidebar -->
      <!-- Left side column. contains the logo and sidebar -->
      

<style>
    .super_script {
		font-size: 85%;
		vertical-align: super;
    }
</style>

    
        

    <form id="form1" runat="server" autocomplete="off">

      <!-- Content Wrapper. Contains page content -->
      <div class="content-wrapper">
         <!-- Content Header (Page header) -->
         <!-- Main content -->
         <section class="content">

<style>
    #doublescroll
    {
      overflow: auto; overflow-y: hidden; 
    }

    #doublescroll p
    {
      margin: 0; 
      padding: 1em; 
      white-space: nowrap; 
    }
</style>

    <div class="row" id="rowIniciar" name="rowIniciar">
        <input type="button" autofocus="autofocus" OnClick="fIniciar()" value="Iniciar" />
        <input type="button" OnClick="fLeerPrueba()" value="LeerPrueba" />
    </div>

        <script>
            function fIniciar() {
                document.getElementById("rowIniciar").style.display = "none";
                //fMasVolumen();
                    let vid = document.getElementById("reproductor");
                    vid.muted = false;
                    vid.play();
                //toggleFullScreen();
                    vid.requestFullscreen();
            }


            function fLeerPrueba() {
                speak("Esto es una prueba, 1° Piso, Paciente de Prueba");
            }


        </script>

            <!-- TERCERA FILA -->
            <div class="row">
                <%--<div class="col-md-12 text-center">
                    <img src="LogoIRO2.png" alt="Alternate Text" width="25%" />
                </div>--%>

               <div class="col-md-12">
                  <div class="box" >

                    <div class="col-md-0"> 
                        <div id="divShowPac"></div>
                    </div>

                    <div class="col-md-6 text-center">
<%--                        <img src="LogoIRO2.png" alt="Alternate Text" width="60%" />
                        <video width="100%" height="55%" controls autoplay muted loop id="video01">
                            <source src="Videos\*.mp4" type="video/mp4">--%>
<%--                            <source src="Videos\0001-Ojo.mp4" type="video/mp4">
                            <source src="Videos\0002-Musica.mp4" type="video/mp4">
                            <source src="Videos\0003-Ojo.mp4" type="video/mp4">
                            <source src="Videos\0004-Musica.mp4" type="video/mp4">--%>
<%--                        </video>--%>
<label id="info"></label>
<video id="reproductor" controls width="100%" height="55%"></video>
                    </div>

                     <!-- /.box-body -->
                  </div>
                  <!-- /.box -->
               </div>
                <div class="col-md-12" style="display:none">
                    <audio controls id="audio01">
                        <source src="tonomessenger2000.mp3" type="audio/mp3" />
                    </audio>
                </div>
            </div>

            <!--FIN TERCERA FILA-->
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

<script>
    function fMasVolumen() {
        let vid = document.getElementById("video01");
        vid.muted = false;
        vid.play();
    }

    setTimeout(fMasVolumen, 500);


    function DoubleScroll(element) {
        var scrollbar = document.createElement('div');
        scrollbar.appendChild(document.createElement('div'));
        scrollbar.style.overflow = 'auto';
        scrollbar.style.overflowY = 'hidden';
        scrollbar.firstChild.style.width = element.scrollWidth+'px';
        scrollbar.firstChild.style.paddingTop = '1px';
        scrollbar.firstChild.appendChild(document.createTextNode('\xA0'));
        scrollbar.onscroll = function() {
            element.scrollLeft = scrollbar.scrollLeft;
        };
        element.onscroll = function() {
            scrollbar.scrollLeft = element.scrollLeft;
        };
        element.parentNode.insertBefore(scrollbar, element);
    }

    DoubleScroll(document.getElementById('doublescroll'));

</script>

         </section>
          <div id="divAtenciones">
              
          </div>
          <div id="divDetAtenciones">
              
          </div>
          <div id="divErrores">
              
          </div>
         <!-- FIN MAIN CONTENT -->
         <!-- FIN MAIN CONTENT -->
         <!-- FIN MAIN CONTENT -->
         <!-- FIN MAIN CONTENT -->
        
         <!-- modal-sm | small || modal-lg | largo || modal-xl | extra largo -->
        <div class="modal modal-info fade" id="modal-info">
          <div class="modal-dialog modal-sm">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Info Modal</h4>
              </div>
              <div class="modal-body">
                <p>One fine body&hellip;</p>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-outline pull-left" data-dismiss="modal">Close</button>
                <button type="button" class="btn btn-success btn-outline" data-toggle="modal" data-target="#modal-success">
                    Otro Modal
                </button>
              </div>
            </div>
            <!-- /.modal-content -->
          </div>
          <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->

       
      </div>
      <!-- /.content-wrapper -->



   </form>

      </div>
    

      <!-- jQuery 3 -->
      <script>  
          $(function () {
          //Initialize Select2 Elements
          $('.select2').select2()
          })
      </script>        
      <!-- jQuery 3 -->
      <script src="/ASPX/Estilos/bower_components/jquery/dist/jquery.min.js"></script>
      <!-- jQuery UI 1.11.4 -->
      
      <!-- Resolve conflict in jQuery UI tooltip with Bootstrap tooltip -->
      <script>
         $.widget.bridge('uibutton', $.ui.button);
      </script>
      <!-- Bootstrap 3.3.7 -->
      <script src="/ASPX/Estilos/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
      
      <!-- datepicker -->
      <script src="/ASPX/Estilos/bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>

      <!-- Bootstrap WYSIHTML5 -->
      <script src="/ASPX/Estilos/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js"></script>
      <!-- Slimscroll -->
      <script src="/ASPX/Estilos/bower_components/jquery-slimscroll/jquery.slimscroll.min.js"></script>
      <!-- FastClick -->
      <script src="/ASPX/Estilos/bower_components/fastclick/lib/fastclick.js"></script>
      <!-- AdminLTE App -->
      <script src="/ASPX/Estilos/dist/js/adminlte.min.js"></script>
      <!-- AdminLTE dashboard demo (This is only for demo purposes) -->
      <script src="/ASPX/Estilos/dist/js/pages/dashboard.js"></script>
      <!-- AdminLTE for demo purposes -->
      <script src="/ASPX/Estilos/dist/js/demo.js"></script>
      <!--BOTON SUBIR-->
      <script src="/ASPX/Estilos/BotonIrArriba/botonsubir.js"></script>
      <!-- ICHECK -->
      <script src="/ASPX/Estilos/plugins/iCheck/icheck.min.js"></script>
      <!-- JQUERY -->
      <script type="text/javascript" src="/ASPX/Estilos/Funciones/jquery-1.12.1.min.js"></script>
        <!-- Select2 -->
        <script src="/ASPX/Estilos/bower_components/select2/dist/js/select2.full.min.js"></script>
        <!-- InputMask -->
        <script src="/ASPX/Estilos/plugins/input-mask/jquery.inputmask.js"></script>
        <script src="/ASPX/Estilos/plugins/input-mask/jquery.inputmask.date.extensions.js"></script>
        <script src="/ASPX/Estilos/plugins/input-mask/jquery.inputmask.extensions.js"></script>
      
        <!-- FLOT CHARTS -->
        <script src="/ASPX/Estilos/bower_components/Flot/jquery.flot.js"></script>
        <!-- FLOT RESIZE PLUGIN - allows the chart to redraw when the window is resized -->
        <script src="/ASPX/Estilos/bower_components/Flot/jquery.flot.resize.js"></script>
        <!-- FLOT PIE PLUGIN - also used to draw donut charts -->
        <script src="/ASPX/Estilos/bower_components/Flot/jquery.flot.pie.js"></script>
        <!-- FLOT CATEGORIES PLUGIN - Used to draw bar charts -->
        <script src="/ASPX/Estilos/bower_components/Flot/jquery.flot.categories.js"></script>
    
    
</body>
</html>
