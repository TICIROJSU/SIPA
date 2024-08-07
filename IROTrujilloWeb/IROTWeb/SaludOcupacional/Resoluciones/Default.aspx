<%@ Page Language="C#" MasterPageFile="../../MasterPageIRO.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="IROTWeb_SaludOcupacional_Resoluciones_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderTitle1" Runat="Server">
   <title>Comite de Etica Institucional</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderHead2" Runat="Server">
    <%-- Para Scripts --%>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="CPHSettings" Runat="Server">
    <%  //#include file="include/leftmenuscript.inc"       
        Response.WriteFile("../SubMenuSett.html");
    %>
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderTitle3" Runat="Server">
    <%  //#include file="include/leftmenuscript.inc"       
        Response.WriteFile("../Subtitulo.html");
    %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CPHSupIzq" Runat="Server">

    <div class="row" >
	    <div class="facilities">
	        <div class="our-facilities">
                <a href="https://drive.google.com/file/d/1yFcF7RmZIFTB0pfggIKFNn6Da56ecP2z/view?usp=drive_link" target="_blank">
	                <h2>REGLAMENTO INTERNO DE SEGURIDAD Y SALUD EN EL TRABAJO</h2>
                </a>
	        </div>
	    </div>
    </div>
    <div class="row" >
	    <div class="facilities">
	        <div class="our-facilities">
                <a href="https://drive.google.com/file/d/18_YBhaZYMsipMGK5xEUbjw2Ei2BNESgy/view?usp=drive_link" target="_blank">
	                <h2>R.D. 120-2023 RISST</h2>
                </a>
	        </div>
	    </div>
    </div>
    <div class="row" >
	    <div class="facilities">
	        <div class="our-facilities">
                <a href="https://drive.google.com/file/d/1hu9WLvYakRU17KGQS4m1OBI40Evfubpf/view?usp=drive_link" target="_blank">
	                <h2>POLITICA DE SEGURIDAD Y SALUD EN EL TRABAJO-IRO</h2>
                </a>
	        </div>
	    </div>
    </div>

	<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
        <asp:Literal ID="LitSIimg" runat="server"></asp:Literal>
	</div>

    <asp:Literal ID="LitError" runat="server"></asp:Literal>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CPHSupDer" Runat="Server">

<div class="slider-border">
    <div id="main_area">
	<!-- Slider -->
	<div>
		<div id="slider slider-section ">
            <div class="carousel-inner carousel-inner-border">
                <img alt="" class="img-responsive" src="../../Estilos/images/resoluciones.png" />
            </div>
        </div>
	</div>
	</div>
</div><!--/Slider-->

	<div class=" hidden-xs" id="slider-thumbs" style="display:none">
			<!-- Bottom switcher of slider -->
			<ul class="hide-bullets">
				<li class="thumbnail-img block1">
					<a class="thumbnail thumbnail-setting" id="carousel-selector-0"> <img alt="" class="img-responsive" src="../../Estilos/images/dept-dymmy.jpg" /></a>
				</li>
				<li class="thumbnail-img">
					<a class="thumbnail thumbnail-setting" id="carousel-selector-1"> <img alt="" class="img-responsive" src="../../Estilos/images/dept-page-02.jpg" /></a>
				</li>
				<li class="thumbnail-img">
					<a class="thumbnail thumbnail-setting" id="carousel-selector-2"><img alt="" class="img-responsive" src="../../Estilos/images/dept-page-03.jpg" /></a>
				</li>
			</ul>                 
	</div>
	<!--slider-border end-->

</asp:Content>


<asp:Content ID="Content6" ContentPlaceHolderID="CPHInfIzq" Runat="Server" >

</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="CPHInfDer" Runat="Server">


    <script>
        
        document.getElementById("settings").innerHTML = "";
        document.getElementById("settings").innerText = "";
        document.getElementById("settings").textContent = "";
        document.getElementById("btnsettings").click();
        $('#settings').html("");
        
    </script>

</asp:Content>
