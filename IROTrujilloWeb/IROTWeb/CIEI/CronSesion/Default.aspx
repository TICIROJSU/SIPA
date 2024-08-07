<%@ Page Language="C#" MasterPageFile="../../MasterPageIRO.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="IROTWeb_CIEI_CronSesion_Default" %>

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
	<div class="facilities">
	    <div class="our-facilities">
	        <h2><asp:Literal ID="LitSITitulo" runat="server"></asp:Literal> 
                <%--Cronograma de Sesiones 2020--%>
	        </h2>
	        <%--<span class="sub-heading"><p>Como nació el IRO..</p></span>--%>
<p><asp:Literal ID="LitSIContenido" runat="server"></asp:Literal>
    <%--La presentación del expediente ante la Oficina administrativa del CEI, cuyo horario de labores de la asistente administrativa es de 07:30 a.m. a 2:45 p.m. (Lunes a viernes) con una anticipación de 15 días a la sesión de acuerdo a lo establecido en el cronograma anual de sesiones.--%> 
</p>
	    </div><!--our-facilities-->
	</div><!--facilities end-->
				
	<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
        <asp:Literal ID="LitSIimg" runat="server"></asp:Literal>
        <%--<img src="../../Estilos/images/CRONOSESIONES.png" class="img-responsive" alt="" />--%>
	</div>
    <!--sub 12 col-end-->
<%--				<div class="learn-more-btn">
				    <a href="#">Leer aquí</a>
				</div>--%>
    <!--learn-more-section-->
    <asp:Literal ID="LitError" runat="server"></asp:Literal>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CPHSupDer" Runat="Server">

<div class="slider-border">
    <div id="main_area">
	<!-- Slider -->
	<div>
		<div id="slider slider-section ">
            <div class="carousel-inner carousel-inner-border">
                <img alt="" class="img-responsive" src="../../Estilos/images/CronoSesMesa.png" />
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


</asp:Content>
