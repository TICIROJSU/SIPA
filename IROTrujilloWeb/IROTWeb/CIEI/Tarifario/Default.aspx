<%@ Page Language="C#" MasterPageFile="../../MasterPageIRO.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="IROTWeb_CIEI_Tarifario_Default" %>

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
	        <h2>
                <asp:Literal ID="LitSITitulo" runat="server"></asp:Literal><%--Tarifario--%>
	        </h2>
	        <%--<span class="sub-heading"><p>Como nació el IRO..</p></span>--%>
	    </div><!--our-facilities-->
	</div><!--facilities end-->
				
	<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
        <asp:Literal ID="LitSIimg" runat="server"></asp:Literal>
        <%--<img src="../../Estilos/images/TARIFARIO.png" class="img-responsive" alt="" />--%>
	</div>

    <br />
    <asp:Literal ID="LitError" runat="server"></asp:Literal>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CPHSupDer" Runat="Server">

<div class="slider-border">
    <div id="main_area">
	<!-- Slider -->
	<div>
		<div id="slider slider-section ">
			<!-- Top part of the slider -->
			<div class="carousel slide" id="photo-carousel" style="display:none">
				<!-- Carousel items -->
				<div class="carousel-inner carousel-inner-border">
					<div class="active item" data-slide-number="0">
					<img alt="" class="img-responsive" src="../../Estilos/images/dept-dymmy.jpg" /></div>
					<div class="item" data-slide-number="1">
					<img alt="" class="img-responsive" src="../../Estilos/images/dept-page-02.jpg" /></div>
					<div class="item" data-slide-number="2">
					<img alt="" class="img-responsive" src="../../Estilos/images/dept-page-03.jpg" /></div>
				</div><!-- Carousel nav -->
				<a class="left left-arrow-section carousel-control" href="#photo-carousel" role="button" data-slide="prev">
					<span class="glyphicon glyphicon-chevron-left"></span>
				</a>
				<a class="right right-arrow-section carousel-control" href="#photo-carousel" role="button" data-slide="next">
					<span class="glyphicon glyphicon-chevron-right"></span>
				</a>
			</div>
            <div class="carousel-inner carousel-inner-border">
                <img alt="" class="img-responsive" src="../../Estilos/images/tarif.png" />
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

