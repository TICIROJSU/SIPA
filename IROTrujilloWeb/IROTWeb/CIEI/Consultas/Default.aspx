<%@ Page Language="C#" MasterPageFile="../../MasterPageIRO.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="IROTWeb_CIEI_Consultas_Default" %>

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
	        <h3>Consultas e Informes</h3>
	    </div><!--our-facilities-->
	</div><!--facilities end-->

<div class="col-xs-12 col-lg-12 col-sm-12 col-md-12 " style="display:block">
	<!--contact widgets-->
	<div class="col-xs-12 col-lg-12 col-sm-12 col-md-12 " style="background-color: #f8f8f8">
        <br />
		<div class="subtitle col-xs-12 no-pad col-sm-12 col-md-12 ">Solicitud de Informacion</div>
		<!--Contact form-->
		<div class="col-lg-12 col-sm-12 col-md-12 col-xs-12 " data-wow-delay="0.5s" data-wow-offset="160" style="visibility: visible;-webkit-animation-delay: 0.5s; -moz-animation-delay: 0.5s; animation-delay: 0.5s;">                
			<div class="col-lg-12 col-sm-12 col-md-12 col-xs-12 no-pad">
				<!-- <form class="contact2-page-form col-lg-12 col-sm-12 col-md-12 col-xs-12 no-pad contact-v1" id="contactForm"> -->
				<form class="contact2-page-form col-lg-12 col-sm-12 col-md-12 col-xs-12 no-pad contact-v1" id="contact_formc" method="post" name="formc" runat="server" autocomplete="off">
					<div class="col-lg-12 control-group">
                        <asp:TextBox ID="name" runat="server" class="contact2-textbox" placeholder="Nombre" required="true" ></asp:TextBox>
					</div>
					<div class="col-md-12 control-group">
                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="email" ErrorMessage="El formato del correo no es válido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"></asp:RegularExpressionValidator>--%>
                        <asp:TextBox ID="email" runat="server" type="email" class="contact2-textbox" placeholder="Email" required="true" ></asp:TextBox>
					</div>
					<div class="col-lg-12 col-sm-12 col-md-12 col-xs-12 control-group">
                        <asp:TextBox ID="subject" runat="server" class="contact2-textbox" placeholder="Asunto" required="true" ></asp:TextBox>
					</div>
					<div class="col-lg-12 col-sm-12 col-md-12 col-xs-12">
                        <asp:TextBox ID="message" runat="server" class="contact2-textbox" placeholder="Consulta" TextMode="MultiLine" rows="7"  required="true" ></asp:TextBox>
					</div>
					<div class="col-lg-12 col-sm-12 col-md-12 col-xs-12">
                        <asp:LinkButton ID="btnSendMail" runat="server" class="icon-mail btn2-st2 btn-8 btn-8b" OnClick="btnSendMail_Click" ><%--<i class="fa fa-search"></i>--%> Enviar Correo</asp:LinkButton>
                        <br />
					</div>	
                    <div class="col-lg-12 col-sm-12 col-md-12 col-xs-12 iconlist-title">
                        <small>
                        <asp:Label ID="LabelError" runat="server" Text=""></asp:Label>
                        </small>
                    </div>
                    
				</form>
			</div>
		</div>                       
	</div>                    
</div>				

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CPHSupDer" Runat="Server">

<div class="slider-border">
    <div id="main_area">
	<div>
        <p>
            <br />
        </p>
        <asp:Literal ID="LitContactos" runat="server"></asp:Literal>

<%--        <div class="dept-subtitle-tabs">Contacto</div>
        <p>Milagros Vasquez
            <br /> Asistente Administrativa
            <br /> Comite de Etica en Investigacion IRO
            <br /> <a href="mailto:comitedeeticairo@gmail.com">comitedeeticairo@gmail.com</a>
        </p>

        <div class="dept-subtitle-tabs">Horario de Atencion</div>
        <p>Lunes a Viernes 
            <br /> 7.30 am a 2.45 p.m.
        </p>

        <div class="dept-subtitle-tabs">Telefono</div>
        <p>044 287236 
            <br /> Anexo 402
        </p>--%>
        
        
	</div>
	</div>
</div>
</asp:Content>


<asp:Content ID="Content6" ContentPlaceHolderID="CPHInfIzq" Runat="Server" >
    <asp:Literal ID="LitError" runat="server"></asp:Literal>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="CPHInfDer" Runat="Server">


</asp:Content>
