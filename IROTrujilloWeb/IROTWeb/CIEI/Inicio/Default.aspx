<%@ Page Language="C#" MasterPageFile="../../MasterPageIRO.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="IROTWeb_CIEI_Inicio_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderTitle1" Runat="Server">
   <title>Comite de Etica Institucional</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderHead2" Runat="Server">
    <%-- Para Scripts --%>
    <script>

    </script>
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
<script runat="server">


</script>

<asp:Content ID="Content3" ContentPlaceHolderID="CPHSupIzq" Runat="Server">
	<div class="facilities">
	    <div class="our-facilities">
	        <h2>
                <asp:Literal ID="LitSITitulo" runat="server"></asp:Literal>
	        </h2>
	        <%--<span class="sub-heading"><p>Como nació el IRO..</p></span>--%>
<p>
    <asp:Literal ID="LitSIContenido" runat="server"></asp:Literal>
</p>
	    </div><!--our-facilities-->
	</div><!--facilities end-->

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
                <%--<img alt="" class="img-responsive" src="../../Estilos/images/CIEIMiembros.png" />--%>
                <asp:Literal ID="LitSDMiembros" runat="server"></asp:Literal>
                <%--<img alt="" class="img-responsive" src="http://drive.google.com/uc?export=view&id=1aupPl6BhEMwPBxuSFOmwhZ_tIkGdSjBK" />--%>
                <%--<img alt="" class="img-responsive" src="https://mzxzrg.sn.files.1drv.com/y4mbbfnbvaPM5jKOgAJMdk3nQgcNVA9pBWGWFds7BB88klsqSqs4PDom5wMCYNWWasX-C_6ZXMUQlq-lI6qP4nT_scx4S0l9a9w-5h1chY2cEf2ZzMV0X3KMXdmEOn_JaaTWDXm3l5VwbsKMYlWHt_B2-5FVhhlcCjTai76oiwGmPYmolc69shuyQgmHjxMTu-_-jZm5QxO1SYjm7k3-zRTGg?width=1600&height=900&cropmode=none" />--%>
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


<asp:Content ID="Content6" ContentPlaceHolderID="CPHInfIzq" Runat="Server">
<div class="subtitle col-xs-12 no-pad col-sm-11 col-md-12 pull-left news-sub">Institucional</div>

<div class="latest-post-wrap pull-left ">

	<!--Post item-->
	<div class="post-item-wrap pull-left col-sm-6 col-md-12 col-xs-12  ">
		<img src="../../Estilos/images/Publicacion.png" class="img-responsive post-author-img" alt="" />
			<div class="post-content1 pull-left col-md-9 col-sm-9 col-xs-8">
				<div class="post-title pull-left"><h3>Publicaciones</h3></div>
			</div>
			<div class="post-content2 pull-left">
				<a href="https://drive.google.com/open?id=177Ni04623MJlWunoaYYmXdqPqg98mcjW" target="_blank" ><p>Artículo de Investigación 2010-2019</p></a>
                <a href="https://drive.google.com/file/d/1h6kEZ0scbmh1owrlTs4mttRazr9oPM0C/view?usp=drive_link" target="_blank" ><p>Ensayos Clínicos</p></a>
                <a href="https://drive.google.com/file/d/17ISjiHEh2vf9l8pcGMTi6_D3qklXZ3ET/view?usp=drive_link" target="_blank" ><p>Registro de Proyectos de Investigación de médicos residentes a diciembre 2023</p></a>
			</div>

	 </div>

	<!--Post item-->
	<div class="post-item-wrap pull-left col-sm-6 col-md-12 col-xs-12  ">
		<img src="../../Estilos/images/acred.png" class="img-responsive post-author-img" alt="" />
			<div class="post-content1 pull-left col-md-9 col-sm-9 col-xs-8">
				<div class="post-title pull-left"><h3>Constancia de Acreditacion</h3></div>
			</div>
			<div class="post-content2 pull-left">
				<a href="https://drive.google.com/open?id=1mXLiVYoExP9BzzdtfqSGxSzRFEM5ZznK" target="_blank" ><p>Constancia de Acreditacion 2022</p></a>
                <a href="https://drive.google.com/open?id=1-IU6RtQ7szwhAqkFfYzZ5A9lVHF7l7SD" target="_blank" ><p>Constancia de Acreditacion 2018</p></a>
			</div>

	 </div>

	<!--Post item-->
	<div class="post-item-wrap pull-left col-sm-6 col-md-12 col-xs-12  ">
		<img src="../../Estilos/images/plan.png" class="img-responsive post-author-img" alt="" />
			<div class="post-content1 pull-left col-md-9 col-sm-9 col-xs-8">
				<div class="post-title pull-left"><h3>Planes de Trabajo</h3></div>
			</div>
			<div class="post-content2 pull-left">                   
				<a href="https://drive.google.com/open?id=1X8vXdrkFcyEKLt6xcXIj0LxBDNXDwzZJ" target="_blank" ><p>Documentos de Gestion 2021</p></a>
<%--				<a href="https://drive.google.com/open?id=1tMIaMTzT8TVj0JofwcRQnaGZCXZviKn4" target="_blank" ><p>Plan Anual de Actividades 2020</p></a>
				<a href="https://drive.google.com/open?id=1TfH0rMALP6o8MLjHdGw3Swd07ny3QFmO" target="_blank" ><p>Plan de Capacitación 2020</p></a>
				<a href="https://drive.google.com/open?id=1XH6FXN66VI9LoUi4PJXE4sAGXbFPGZSm" target="_blank" ><p>Plan de Supervisiones 2020</p></a>--%>
			</div>
	 </div>
	 
	<!--Post item-->
	<div class="post-item-wrap pull-left col-sm-6 col-md-12 col-xs-12  ">
		<img src="../../Estilos/images/carpet.png" class="img-responsive post-author-img" alt="" />
			<div class="post-content1 pull-left col-md-9 col-sm-9 col-xs-8">
				<div class="post-title pull-left"><h3>Memoria Anual</h3></div>
			</div>
			<div class="post-content2 pull-left">                   
				<a href="https://drive.google.com/file/d/1ZEHQ7q1rT-MBjrT9k6W9WMnCA5GSpmxw/view" target="_blank" ><p>Memoria anual 2021</p></a>
				<a href="https://drive.google.com/file/d/106f4x5JNOVfiZjSAAyke9A4PqDpNELwU/view" target="_blank" ><p>Memoria anual 2020</p></a>
				<a href="https://drive.google.com/file/d/1SS8LYwowxYQ0kjAGJvMt_bjGRiZdy3bj/view" target="_blank" ><p>Memoria anual 2019</p></a>
			</div>
	 </div>
	 

</div>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="CPHInfDer" Runat="Server">
	<div class="subtitle col-xs-12 no-pad col-sm-11 col-md-12 pull-right news-sub">
<h3 style="background: #0C212E; color: snow">
	<i class="icon-stethoscope dept-icon"></i>
	<span class="dep-txt">Anexos</span>
</h3>        
	</div>
			
	<div id="imedica-dep-accordion">
		<!-- Accordion Item -->
		<h3>
            <i class="fa fa-cloud-download dept-icon" ></i>
            <span class="dep-txt">Requisitos</span>
		</h3>
		<div>
			<div class="service-icon-container rot-y"><i class="fa fa-file dept-icon dept-icon-sub  panel-icon"></i></div>
		   
		   <div class="dept-content pull-left col-md-7 col-lg-8">
			<div class="dept-title pull-left">Descargar Anexos</div> 
			<strong><a href="https://drive.google.com/open?id=1ksjS3SlIm5qayWMSggTIfnfuEFoEhP2v" target="_blank">ANEXO A FORMATO BÁSICO</a></strong>
               <br /><br />
			<strong><a href="https://drive.google.com/drive/folders/1fHIBF7hU2H8yBG2Qxv6Rb7rP7jRFkeNc?usp=sharing" target="_blank">Mas Anexos »</a></strong>
		   
			<div class="vspacer"></div>
			
			</div>
			
		</div>
		<!-- Accordion Item -->

    <!-- Accordion Item -->
		<h3>
            <i class="fa fa-cloud-download dept-icon" ></i>
            <span class="dep-txt">Resoluciones - Miembros</span>
		</h3>
		<div>
            <div class="col-md-12">
			    <div class="service-icon-container rot-y"><i class="fa fa-file dept-icon dept-icon-sub  panel-icon"></i></div>
		        <div class="dept-content pull-left col-md-7 col-lg-8">
		            <%--<div class="dept-title pull-left">Manual de Procedimientos 2022</div>--%> 
		            <span class="post-meta-bottom read-more-btn-section">
                        <a href="https://drive.google.com/file/d/1KjmiUyRs-fwpgDmUptEn05rdRe_Wf922/view?usp=drive_link" target="_blank" >R.D. N°100-2024 RECONFORMAR CEI »</a>
		            </span>
		            <div class="vspacer"></div>
		        </div>
            </div>
            
            <div class="col-md-12">
			    <div class="service-icon-container rot-y"><i class="fa fa-file dept-icon dept-icon-sub  panel-icon"></i></div>
		        <div class="dept-content pull-left col-md-7 col-lg-8">
		            <%--<div class="dept-title pull-left">Manual de Procedimientos 2022</div>--%> 
		            <span class="post-meta-bottom read-more-btn-section">
                        <a href="https://drive.google.com/file/d/1mPbem8ptj4vAv93OvPmtjFd5bsG2Meyu/view?usp=drive_link" target="_blank" >R.D. N°024-2024 RECONFORMACIÓN CEI »</a>
		            </span>
		            <div class="vspacer"></div>
		        </div>
            </div>
            
            <div class="col-md-12">
			    <div class="service-icon-container rot-y"><i class="fa fa-file dept-icon dept-icon-sub  panel-icon"></i></div>
		        <div class="dept-content pull-left col-md-7 col-lg-8">
		            <%--<div class="dept-title pull-left">Manual de Procedimientos 2022</div>--%> 
		            <span class="post-meta-bottom read-more-btn-section">
                        <a href="https://drive.google.com/file/d/1nfjgRhXgjsQA4kvroLg91cFqB-8kjTd0/view" target="_blank" >RD 218-2022-GRLL-GGR-GS-IROJSU »</a>
		            </span>
		            <div class="vspacer"></div>
		        </div>
            </div>
            

		</div>
	<!-- Accordion Item -->

		<!-- Accordion Item -->
		<h3>
            <i class="fa fa-cloud-download dept-icon" ></i>
            <span class="dep-txt">Manual de Procedimientos Vigente</span>
		</h3>
		<div>
            <div class="col-md-12">
			    <div class="service-icon-container rot-y"><i class="fa fa-file dept-icon dept-icon-sub  panel-icon"></i></div>
		        <div class="dept-content pull-left col-md-7 col-lg-8">
		            <%--<div class="dept-title pull-left">Manual de Procedimientos 2022</div>--%> 
		            <span class="post-meta-bottom read-more-btn-section">
                        <a href="https://drive.google.com/file/d/1ShkjHZ5APMRE8XfPzm1iK94WYxflDewZ/view" target="_blank" >Manual de Procedimientos 2022 »</a>
		            </span>
		            <div class="vspacer"></div>
		        </div>
            </div>
            <div class="col-md-12">
			    <div class="service-icon-container rot-y"><i class="fa fa-file dept-icon dept-icon-sub  panel-icon"></i></div>
		        <div class="dept-content pull-left col-md-7 col-lg-8">
		            <%--<div class="dept-title pull-left">Manual de Procedimientos 2018</div>--%> 
		            <span class="post-meta-bottom read-more-btn-section">
                        <a href="https://drive.google.com/file/d/1Z0QHLDumWxGQD5LoiqFgsnkCa3i1fh0V/view" target="_blank" >Manual de Procedimientos 2018 »</a>
		            </span>
		            <div class="vspacer"></div>
		        </div>
            </div>

		</div>

		<!-- Accordion Item -->

		<!-- Accordion Item -->
		<h3>
            <i class="fa fa-cloud-download dept-icon" ></i>
            <span class="dep-txt">Reglamento Interno Vigente</span>
		</h3>
		<div>

            <div class="col-md-12">
			    <div class="service-icon-container rot-y"><i class="fa fa-file dept-icon dept-icon-sub  panel-icon"></i></div>
		        <div class="dept-content pull-left col-md-7 col-lg-8">
		            <%--<div class="dept-title pull-left">Descargar Reglamento Interno 2022</div>--%> 
		            <span class="post-meta-bottom read-more-btn-section">
                        <a href="https://drive.google.com/file/d/1Elw4R4lHiMBf5aFjbRyEZ22rt9J0MKBo/view" target="_blank" >Descargar Reglamento Interno 2022 »</a>
		            </span>
		            <div class="vspacer"></div>
		        </div>
            </div>
            <div class="col-md-12">
                <div class="service-icon-container rot-y"><i class="fa fa-file dept-icon dept-icon-sub  panel-icon"></i></div>
		        <div class="dept-content pull-left col-md-7 col-lg-8">
		            <%--<div class="dept-title pull-left">Descargar Reglamento Interno 2018</div>--%> 
		            <span class="post-meta-bottom read-more-btn-section">
                        <a href="https://drive.google.com/file/d/15qj73T1Dl0xIFGi6TnMrakH5JZVSxFGP/view" target="_blank" >Descargar Reglamento Interno 2018 »</a></span>
		            <div class="vspacer"></div>
		        </div>
            </div>

		</div>
		<!-- Accordion Item -->
		
        <!-- Accordion Item -->
		<h3>
            <i class="fa fa-cloud-download dept-icon" ></i>
            <span class="dep-txt">Plan Anual de Actividades</span>
		</h3>
		<div>
            <div class="col-md-12">
			    <div class="service-icon-container rot-y"><i class="fa fa-file dept-icon dept-icon-sub  panel-icon"></i></div>
		        <div class="dept-content pull-left col-md-7 col-lg-8">
<a href="https://drive.google.com/file/d/1UHmIEgZR5dV2CO6t3prf4-8ZttS-UJlN/view" target="_blank" >
		                <div class="dept-title pull-left">Plan de Actividades 2022 »</div> 
		                <span class="post-meta-bottom read-more-btn-section">Detalles »</span>
		                <div class="vspacer"></div>
                    </a>
		        </div>
            </div>
            <div class="col-md-12">
			    <div class="service-icon-container rot-y"><i class="fa fa-file dept-icon dept-icon-sub  panel-icon"></i></div>
		        <div class="dept-content pull-left col-md-7 col-lg-8">
<a href="https://drive.google.com/open?id=1tMIaMTzT8TVj0JofwcRQnaGZCXZviKn4" target="_blank" >
		                <div class="dept-title pull-left">Plan de Actividades 2020 »</div> 
		                <span class="post-meta-bottom read-more-btn-section">Detalles »</span>
		                <div class="vspacer"></div>
                    </a>
		        </div>
            </div>
		</div>
		<!-- Accordion Item -->
        <!-- Accordion Item -->
		<h3>
            <i class="fa fa-cloud-download dept-icon" ></i>
            <span class="dep-txt">Plan de Capacitacion</span>
		</h3>
		<div>
            <div class="col-md-12">
			    <div class="service-icon-container rot-y"><i class="fa fa-file dept-icon dept-icon-sub  panel-icon"></i></div>
		        <div class="dept-content pull-left col-md-7 col-lg-8">
<a href="https://drive.google.com/open?id=1DE6-kK4StCAucdfEYYjWT4F_VXi4F1Hg" target="_blank" >
		                <div class="dept-title pull-left">Plan de Capacitacion 2022 »</div> 
		                <span class="post-meta-bottom read-more-btn-section">Detalles »</span>
		                <div class="vspacer"></div>
                    </a>
		        </div>
            </div>
            <div class="col-md-12">
			    <div class="service-icon-container rot-y"><i class="fa fa-file dept-icon dept-icon-sub  panel-icon"></i></div>
		        <div class="dept-content pull-left col-md-7 col-lg-8">
<a href="https://drive.google.com/open?id=1TfH0rMALP6o8MLjHdGw3Swd07ny3QFmO" target="_blank" >
		                <div class="dept-title pull-left">Plan de Capacitacion 2020 »</div> 
		                <span class="post-meta-bottom read-more-btn-section">Detalles »</span>
		                <div class="vspacer"></div>
                    </a>
		        </div>
            </div>
		</div>
		<!-- Accordion Item -->
        <!-- Accordion Item -->
		<h3>
            <i class="fa fa-cloud-download dept-icon" ></i>
            <span class="dep-txt">Plan de Supervisiones</span>
		</h3>
		<div>
            <div class="col-md-12">
			    <div class="service-icon-container rot-y"><i class="fa fa-file dept-icon dept-icon-sub  panel-icon"></i></div>
		        <div class="dept-content pull-left col-md-7 col-lg-8">
<a href="https://drive.google.com/open?id=15Nk21wVChMlLce1SwUFqAPdXTYH0hOPB" target="_blank" >
		                <div class="dept-title pull-left">Plan de Supervision 2022 »</div> 
		                <span class="post-meta-bottom read-more-btn-section">Detalles »</span>
		                <div class="vspacer"></div>
                    </a>
		        </div>
            </div>
            <div class="col-md-12">
			    <div class="service-icon-container rot-y"><i class="fa fa-file dept-icon dept-icon-sub  panel-icon"></i></div>
		        <div class="dept-content pull-left col-md-7 col-lg-8">
<a href="https://drive.google.com/open?id=1XH6FXN66VI9LoUi4PJXE4sAGXbFPGZSm" target="_blank" >
		                <div class="dept-title pull-left">Plan de Supervision 2020 »</div> 
		                <span class="post-meta-bottom read-more-btn-section">Detalles »</span>
		                <div class="vspacer"></div>
                    </a>
		        </div>
            </div>
		</div>
		<!-- Accordion Item -->
        <!-- Accordion Item -->
		<h3>
            <i class="fa fa-cloud-download dept-icon" ></i>
            <span class="dep-txt">Dpto. de Investigación</span>
		</h3>
		<div>
            <div class="col-md-12">
			    <div class="service-icon-container rot-y"><i class="fa fa-file dept-icon dept-icon-sub  panel-icon"></i></div>
		        <div class="dept-content pull-left col-md-7 col-lg-8">
                    <a href="https://drive.google.com/open?id=1LcQIWdOxBtjb8ewa-yilhMirsDyT69ay" target="_blank" >
		                <div class="dept-title pull-left">R.D. 097-24 DIRECTIVA INSTITUCIONAL DE INVESTIGACION ED. 2024 »</div> 
		                <span class="post-meta-bottom read-more-btn-section">Detalles »</span>
		                <div class="vspacer"></div>
                    </a>
		        </div>
            </div>
            <div class="col-md-12">
			    <div class="service-icon-container rot-y"><i class="fa fa-file dept-icon dept-icon-sub  panel-icon"></i></div>
		        <div class="dept-content pull-left col-md-7 col-lg-8">
                    <a href="https://drive.google.com/open?id=17sw6QxYFO9lr_AmBbSXeJ-r-3ezp2NI6" target="_blank" >
		                <div class="dept-title pull-left">R.D. 098-24 PLAN ANUAL 2024 »</div> 
		                <span class="post-meta-bottom read-more-btn-section">Detalles »</span>
		                <div class="vspacer"></div>
                    </a>
		        </div>
            </div>
            <div class="col-md-12">
			    <div class="service-icon-container rot-y"><i class="fa fa-file dept-icon dept-icon-sub  panel-icon"></i></div>
		        <div class="dept-content pull-left col-md-7 col-lg-8">
                    <a href="https://drive.google.com/open?id=1gHJi4j7MNKN1WqEV8W_AuTHc7jeCxJRm" target="_blank" >
		                <div class="dept-title pull-left">R.D. 099-24 POLITICAS INSTITUCIONALES INVESTIGACIÓN 2024 »</div> 
		                <span class="post-meta-bottom read-more-btn-section">Detalles »</span>
		                <div class="vspacer"></div>
                    </a>
		        </div>
            </div>

            <div class="col-md-12">
			    <div class="service-icon-container rot-y"><i class="fa fa-file dept-icon dept-icon-sub  panel-icon"></i></div>
		        <div class="dept-content pull-left col-md-7 col-lg-8">
<a href="https://drive.google.com/open?id=1LHwn3UQyTDKj8p1Avg_7MzuKhLaK2zf6" target="_blank" >
		                <div class="dept-title pull-left">REGISTRO DE TRABAJOS DE ENSAYOS CLÍNICOS ACTIVOS - 2023 »</div> 
		                <span class="post-meta-bottom read-more-btn-section">Detalles »</span>
		                <div class="vspacer"></div>
                    </a>
		        </div>
            </div>
            <div class="col-md-12">
			    <div class="service-icon-container rot-y"><i class="fa fa-file dept-icon dept-icon-sub  panel-icon"></i></div>
		        <div class="dept-content pull-left col-md-7 col-lg-8">
<a href="https://drive.google.com/open?id=1BvVHVULMHlYoOjta1rxBCABrWtnAy305" target="_blank" >
		                <div class="dept-title pull-left">REGISTRO DE PROYECTOS DE INVESTIGACIÓN EN EJECUCIÓN A AGOSTO 2023 MÉDICOS RESIDENTES »</div> 
		                <span class="post-meta-bottom read-more-btn-section">Detalles »</span>
		                <div class="vspacer"></div>
                    </a>
		        </div>
            </div>

		</div>
		<!-- Accordion Item -->

		
	</div>
</asp:Content>






<%--<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderHead3" Runat="Server">

</asp:Content>--%>
