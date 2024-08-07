<%@ Page Language="C#" MasterPageFile="../../MasterPCov19.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="IROTCovid19_IROJVAR_RegCovid_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <title>IRO - COVID19</title>
    <%--<script type="text/javascript" src="<%=ResolveUrl("../../Estilos/Funciones/json2.js")%>"></script>--%>
    <%--<link rel="stylesheet" media="screen" href="style1.css?id=1">--%>

    <script language="javascript" type="text/javascript">

        function fShowEESS() {
            var params = new Object();
            params.vRed = document.getElementById("").value; 
            params.vMRed = document.getElementById("").value; 
            params = JSON.stringify(params);

            $.ajax({
                type: "POST", url: "DispBuscaMedxEESS.aspx/GetEESS", data: params, 
                contentType: "application/json; charset=utf-8", dataType: "json", async: true, 
                success: function (result) { $("#divEESS").html(result.d) }, 
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert(textStatus + ": " + XMLHttpRequest.responseText); 
                    $("#divEESS").html(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });
        }

    </script>

</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div class="title-covid19">Seguimiento COVID-19</div>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

  <div class="container-fichas">
    <form id="form1" runat="server" autocomplete="off">
		<asp:literal id="litError" runat="server"></asp:literal>

<div class="row">
  <div class="col-12">
    <div class="card shadow mb-4">
      <div class="card-header d-flex align-items-center">
        <h3 class="m-0 font-weight-bold text-primary">Personal que realiza la Prueba</h3>
      </div>
      <div class="card-body">
        <div class="form-row">
          <div class="form-group col-md-6 col-lg-3">
                <input runat="server" class="form-control form-control-lg text-center" id="txtpNroDocu" type="text" value="42084355">
          </div>          
          <div class="form-group  col-md-12 col-lg-7 ">
              <input runat="server" class="form-control form-control-lg text-center" id="txtpNombres" placeholder="Nombres y apellidos" type="text" value="ASPIROS REYNA, LILET MARIBEL">
          </div>
        </div>
      </div>
    </div>
  </div>
</div>


<div class="row">
  <div class="col-12">
    <div class="card shadow mb-4">
      <div class="card-header d-flex align-items-center">
        <h3 class="m-0 font-weight-bold text-primary">Busqueda de Paciente</h3>
      </div>
      <div class="card-body">
        <div class="form-row">
          <div class="form-group col-md-6 col-lg-4">
            <label for="even_select">Tipo Documento <span>(*)</span></label>
            <select name="selTipoDoc" runat="server" class="form-control form-control-lg" id="selTipoDoc">
			  <option value="01" selected="">DNI</option>
			  <option value="03">Carnet de Extranjería</option>
			  <option value="02">Pasaporte</option>
			  <option value="04">Cedula de Identidad</option>
			  <option value="05">Carnet de solicitante de refugio</option>
			  <option value="00">Sin Documento</option>
			</select>
          </div>
          <div class="form-group col-md-6 col-lg-4">
            <label for="event_numero">Nro Documento <span>(*)</span></label>
            <input type="text" runat="server" name="frmNroDoc" value="" class="form-control form-control-lg text-center" maxlength="15"  id="frmNroDoc" inputmode="text" placeholder="Ingrese el número de documento">
          </div>
          <div class="form-group col-md-6 col-lg-2">
            <label class="espacio" for="event_numero">&nbsp;</label>
			  <asp:linkbutton id="btnBuscar" runat="server" class="limpiar-data btn btn-warning btn-lg btn-block" OnClick="btnBuscar_Click">Buscar</asp:linkbutton>
          </div>
          <div class="form-group col-md-6 col-lg-2">
            <label class="espacio" for="event_numero">&nbsp;</label>
			  <asp:linkbutton id="btnLimpiar" runat="server" class="limpiar-data btn btn-info btn-lg btn-block" OnClick="btnLimpiar_Click">Limpiar</asp:linkbutton>
          </div>
        </div>
        <div class="form-row">
          
<script>

	var inputText = document.getElementById("<%=frmNroDoc.ClientID%>");
	inputText.addEventListener("keyup", function(event) {
      if (event.keyCode === 13) {
		  event.preventDefault();
		  document.getElementById("<%=btnBuscar.ClientID%>").click();
      }
	});

</script>
          

          
            <div class="form-group col-md-6 col-lg-2 oculto">
              
              <input class="form-control form-control-lg text-center" id="txtNroDocu" type="hidden" >
            </div>
          
          <div class="form-group  col-md-12 col-lg-4 ">
            <label for="event_name">Nombre y apellidos</label>
              <input runat="server" class="form-control form-control-lg text-center" id="txtNombres" placeholder="Nombres y apellidos" type="text" value="">
          </div>
          <div class="form-group col-md-6 col-lg-3 oculto">
                <label for="event_name">Clasificación de Riesgo</label>
                <input class="form-control form-control-lg text-center" type="text" value="< 65 años sin comorbilidad">
          </div>
          <div class="form-group col-md-6 col-lg-3">
            <label for="event_date">Fecha de Nacimiento</label>
              <input runat="server" class="form-control form-control-lg text-center" id="txtFecha" placeholder="dd/mm/yyyy" value="">
			  <%--type="date" --%>
          </div>
          <div class="form-group col-md-6 col-lg-2">
            <label for="event_edad">Edad</label>
              <input runat="server" class="form-control form-control-lg text-center" id="txtEdad" placeholder="Edad" type="text" value="">
          </div>
          <div class="form-group col-md-6 col-lg-2">
			<label for="event_name">Sexo</label>
                <input runat="server" class="form-control form-control-lg text-center" id="txtSexo" type="text" value="">
          </div>
          <div class="form-group col-md-6 col-lg-2">
			<label for="event_name">Celular</label>
                <input runat="server" class="form-control form-control-lg text-center" id="txtCel" type="text" value="">
          </div>
          <div class="form-group col-md-6 col-lg-2 oculto">
			<label for="event_name">Otro Telefono</label>
                <input runat="server" class="form-control form-control-lg text-center" id="txtTel" type="text" value="">
          </div>
          <div class="form-group col-md-6 col-lg-2 oculto">
			<label for="event_name">Domicilio o Residencia</label>
                <input class="form-control form-control-lg text-center" id="txtDomRes" type="text" value="">
          </div>
          <div class="form-group col-md-6 col-lg-2">
			<label for="event_name">Direccion</label>
                <input runat="server" class="form-control form-control-lg text-center" id="txtDir" type="text" value="">
          </div>

          <div class="form-group col-md-6 col-lg-2">
			<label for="event_name">¿Es Personal de Salud?</label>
			  <asp:dropdownlist ID="ddlPerSalud" runat="server" class="form-control form-control-lg text-center" >
				  <asp:ListItem value="SI">SI</asp:ListItem>
				  <asp:ListItem value="NO">NO</asp:ListItem>
			  </asp:dropdownlist>
          </div>
          <div class="form-group col-md-6 col-lg-2">
			<label for="event_name">Profesion</label>
			  <asp:dropdownlist ID="ddlProf" runat="server" class="form-control form-control-lg text-center" >
				  <asp:ListItem value="A. Medico">A. Medico</asp:ListItem>
				  <asp:ListItem value="B. Enfermero(a)">B. Enfermero(a)</asp:ListItem>
				  <asp:ListItem value="C. Obstetra">C. Obstetra</asp:ListItem>
				  <asp:ListItem value="D. Biologo">D. Biologo</asp:ListItem>
				  <asp:ListItem value="E. Tecnologo Medico">E. Tecnologo Medico</asp:ListItem>
				  <asp:ListItem value="F. Tecnico de Enfermeria">F. Tecnico de Enfermeria</asp:ListItem>
				  <asp:ListItem value="G. Otros">G. Otros</asp:ListItem>
			  </asp:dropdownlist>
          </div>

          <div class="form-group col-md-6 col-lg-2">
			<label for="event_name">¿El paciente cumple alguna condicion de riesgo?</label>
			  <asp:dropdownlist ID="ddlCondRiesgo" runat="server" class="form-control form-control-lg text-center" >
				  <asp:ListItem value="Ninguna">Ninguna</asp:ListItem>
				  <asp:ListItem value="A. Mayor de 60 Años">A. Mayor de 60 Años</asp:ListItem>
				  <asp:ListItem value="B. Hipertension Arterial">B. Hipertension Arterial</asp:ListItem>
				  <asp:ListItem value="C. Enfermedades Cardiovasculares">C. Enfermedades Cardiovasculares</asp:ListItem>
				  <asp:ListItem value="D. Diabetes">D. Diabetes</asp:ListItem>
				  <asp:ListItem value="E. Obesidad">E. Obesidad</asp:ListItem>
				  <asp:ListItem value="F. Asma">F. Asma</asp:ListItem>
				  <asp:ListItem value="G. Enfermedad Pulmonar Cronica">G. Enfermedad Pulmonar Cronica</asp:ListItem>
				  <asp:ListItem value="H. Insuficiencia Renal Cronica">H. Insuficiencia Renal Cronica</asp:ListItem>
				  <asp:ListItem value="I. Enfermedad o Tratamiento Inmunosupresor">I. Enfermedad o Tratamiento Inmunosupresor</asp:ListItem>
				  <asp:ListItem value="J. Cancer">J. Cancer</asp:ListItem>
				  <asp:ListItem value="K. Embarazo o Puerperio">K. Embarazo o Puerperio</asp:ListItem>
				  <asp:ListItem value="L. Personal de Salud">L. Personal de Salud</asp:ListItem>
			  </asp:dropdownlist>
          </div>

          <div class="form-group col-md-6 col-lg-2">
            <label class="espacio" for="event_numero">&nbsp;</label>
			  <asp:linkbutton id="btnActualizar" runat="server" class="limpiar-data btn btn-danger btn-lg btn-block" OnClick="btnActualizar_Click">Actualizar</asp:linkbutton>
          </div>

        </div>

	<div class="form-row">

	</div>

	
		  
      </div>
    </div>
  </div>
</div>

    <div class="container-ficha01">

        <div class="card shadow mb-4">
          <DIV class="d-block card-header py-3" data-toggle="collapse" role="button" aria-expanded="true" aria-controls="collapseCardExample">
            <h3 class="m-0 font-weight-bold text-primary">F100 Fichas de Prueba Rápida</h3>
          </DIV>
          <div class="collapse show" id="collapseCardExample">
            <div class="row">
              <div class="col-12">
                <div class="card-inner-form mb-4">
                  <div class="card-body">

          <div class="form-group col-md-6 col-lg-2">
			<label for="event_name">¿Tiene Sintomas?</label>
			  <asp:dropdownlist ID="ddlSintomas" runat="server" class="form-control form-control-lg text-center" >
				  <asp:ListItem value="SI">SI</asp:ListItem>
				  <asp:ListItem value="NO">NO</asp:ListItem>
			  </asp:dropdownlist>
          </div>
          <div class="form-group col-md-6 col-lg-3">
            <label for="event_date">Fecha de Inicio Sintomas</label>
              <input runat="server" class="form-control form-control-lg text-center" id="txtFechaSint" placeholder="dd/mm/yyyy" type="date" value="">
          </div>

<div class="form-row" >
    <div class="form-group col-md-12 col-lg-6 lista-radio">
    <label><strong>Marque los Sintomas que presente <span>(*)</span></strong></label>
    <ul style="column-count:2;">
		<li>
			<asp:checkbox id="chksintA" runat="server" text="A. Tos" class="custom-control"/>
		</li>
		<li>
			<asp:checkbox id="chksintB" runat="server" text="B. Dolor de Garganta" class="custom-control"/>
		</li>
		<li>
			<asp:checkbox id="chksintC" runat="server" text="C. Congestion Nasal" class="custom-control"/>
		</li>
		<li>
			<asp:checkbox id="chksintD" runat="server" text="D. Dificultad Respiratoria" class="custom-control"/>
		</li>
		<li>
			<asp:checkbox id="chksintE" runat="server" text="E. Fiebre / Escalofrio" class="custom-control"/>
		</li>
		<li>
			<asp:checkbox id="chksintF" runat="server" text="F. Malestar General" class="custom-control"/>
		</li>
		<li>
			<asp:checkbox id="chksintG" runat="server" text="G. Diarrea" class="custom-control"/>
		</li>
		<li>
			<asp:checkbox id="chksintH" runat="server" text="H. Nauseas / Vomitos" class="custom-control"/>
		</li>
		<li>
			<asp:checkbox id="chksintI" runat="server" text="I. Cefalea" class="custom-control"/>
		</li>
		<li>
			<asp:checkbox id="chksintJ" runat="server" text="J. Irritabilidad / Confusion" class="custom-control"/>
		</li>
		<li>
			<asp:checkbox id="chksintK" runat="server" text="K. Dolor" class="custom-control"/>
		</li>
		<li>
			<asp:checkbox id="chksintL" runat="server" text="L. Otros (especificar)" class="custom-control"/>
		</li>

	</ul>
    </div>
</div>

<div class="form-row" >
    <div class="form-group col-md-12 col-lg-6 lista-radio">
    <label><strong>Tipo de Dolor que presenta <span>(*)</span></strong></label>
    <ul>
		<li>
			<asp:checkbox id="chkdolA" runat="server" text="A. Muscular" class="custom-control"/>
		</li>
		<li>
			<asp:checkbox id="chkdolB" runat="server" text="B. Abdominal" class="custom-control"/>
		</li>
		<li>
			<asp:checkbox id="chkdolC" runat="server" text="C. Pecho" class="custom-control"/>
		</li>
		<li>
			<asp:checkbox id="chkdolD" runat="server" text="D. Articulaciones" class="custom-control"/>
		</li>
	</ul>
    </div>
</div>

          <div class="form-group col-md-6 col-lg-2">
			<label for="event_name"><b>Otros Sintomas</b></label>
                <input runat="server" class="form-control form-control-lg text-center" id="txtOtrosSint" type="text" value="">
          </div>


                    <div class="form-row">
                      <div class="form-group col-md-12 col-lg-4">
                        <label for="event_fecha"> <strong>Fecha de ejecución de la prueba rápida
                          <span>(*)</span></strong></label>
                          
                          <input type="text" runat="server" name="txtPRFecha" class="form-control form-control-lg text-center" id="txtPRFecha" >
                      </div>
                      <div class="form-group col-md-12 col-lg-4">
                        <label for="event_hora"> <strong>Hora Ejecución de la prueba rápida <span>(*)</span></strong></label>
                          <input type="time" runat="server" name="txtPRFHora" class="form-control"  id="txtPRFHora" value="08:00">
                      </div>
                    </div>
                    
                    <div class="form-row">
                      <div class="form-group col-md-12 col-lg-4 lista-radio">
                        <label><strong>Procedencia de la solicitud de diagnóstico <span>(*)</span></strong></label>

			  <asp:dropdownlist ID="ddlProcSolicitud" runat="server" class="form-control form-control-lg text-center" >
				  <asp:ListItem value="Llamada al 113">Llamada al 113</asp:ListItem>
				  <asp:ListItem value="Prueba de EESS" Selected="true">Prueba de EESS</asp:ListItem>
				  <asp:ListItem value="Personal de salud">Personal de salud</asp:ListItem>
				  <asp:ListItem value="Contacto con caso confirmado">Contacto con caso confirmado</asp:ListItem>
				  <asp:ListItem value="Contacto con caso sospechoso">Contacto con caso sospechoso</asp:ListItem>
				  <asp:ListItem value="Persona proveniente del extranjero (migraciones)">Persona proveniente del extranjero (migraciones)</asp:ListItem>
				  <asp:ListItem value="Otro priorizado">Otro priorizado</asp:ListItem>
			  </asp:dropdownlist>

                      </div>
                    </div>
                    <div class="form-row">
                      <div class="form-group col-md-12 col-lg-4 lista-radio">
                        <label><strong>Resultado de la prueba rápida <span>(*)</span></strong></label>

			  <asp:dropdownlist ID="ddlResultPR" runat="server" class="form-control form-control-lg text-center" >
				  <asp:ListItem value="No Reactivo">No Reactivo</asp:ListItem>
				  <asp:ListItem value="Indeterminado">Indeterminado</asp:ListItem>
				  <asp:ListItem value="IgM Reactivo">IgM Reactivo</asp:ListItem>
				  <asp:ListItem value="IgG Reactivo">IgG Reactivo</asp:ListItem>
				  <asp:ListItem value="IgM e IgG Reactivo">IgM e IgG Reactivo</asp:ListItem>
			  </asp:dropdownlist>

                      </div>
                    </div>
                    
                    <div class="form-row visible" id="contenedorSeveridad">
                      <div class="form-group col-md-12 col-lg-9 lista-radio">
                        <label><strong>Clasificación clínica de severidad:</strong> <span>(*)</span></label>
                        <ul id="id_ficha100-severidad">
    <li><div class="custom-control custom-radio"><input type="radio" name="ficha100-severidad" value="asintomatico" id="id_ficha100-severidad_0" class="custom-control-input"><label for="id_ficha100-severidad_0" class="custom-control-label">
 Asintomático (No presenta síntomas)</label></div>

</li>
    <li><div class="custom-control custom-radio"><input type="radio" name="ficha100-severidad" value="leve_asintomatico" id="id_ficha100-severidad_1" class="custom-control-input"><label for="id_ficha100-severidad_1" class="custom-control-label">
 Leve (Tratamiento domiciliario, Tos, malestar general, dolor de garganta, fiebre, congestión nasal)</label></div>

</li>
    <li><div class="custom-control custom-radio"><input type="radio" name="ficha100-severidad" value="moderada" id="id_ficha100-severidad_2" class="custom-control-input"><label for="id_ficha100-severidad_2" class="custom-control-label">
 Moderada (Paciente hospitalizada. Disnea o dificultad respiratoria, FR &gt; 22 respiraciones/minuto, Alteración de conciencia (desorientación, confusión), Hipertensión arterial o shock, Signos clínicos o radiológicos de neumonía, Recuento de infocitos &lt; 100 celuulas/uL)</label></div>

</li>
    <li><div class="custom-control custom-radio"><input type="radio" name="ficha100-severidad" value="severa" id="id_ficha100-severidad_3" class="custom-control-input"><label for="id_ficha100-severidad_3" class="custom-control-label">
 Severa (Paciente hospitalizado en unidades críticos: FR &gt; 22 respiraciones/minuto o PaCO2 &lt; 32mmHg, Alteración de conciencia, Pasitolica &lt; 100mmHg o RAM &lt; 65mmHg,PaO2 &lt; 60mmHg o PaFi &lt; 300, Signos cñinicos de fatiga muscular: aleteo nasal, uso de músculos accesorios, desbalance tócaro-abdominal, Lactato sérico&gt; 2mosm/l)</label></div>

</li>
</ul>
                      </div>
                    </div>

                    
                    <div class="form-row oculto" id="contenedorEmbarazo" style="display: none;">
                      <div class="form-group col-md-12 col-lg-4">
                        <label><strong>Embarazo Trimestre</strong><span>(*)</span></label>
                        <select name="ficha100-embarazo_trimestre" class="form-control select2-hidden-accessible" id="id_ficha100-embarazo_trimestre" tabindex="-1" aria-hidden="true">
						  <option value="" selected="">------Eligir------</option>
						  <option value="primer_trimestre">1° Trimestre</option>
						  <option value="segundo_trimestre">2° Trimestre</option>
						  <option value="tercer_trimestre">3° Trimestre</option>
						</select>

                      </div>
                    </div>
                    <div class="form-row oculto" id="contenedorOtraCondicion" style="display: none;">
                      <div class="form-group col-md-12 col-lg-4">
                        <label><strong>Otros, Especificar</strong><span>(*)</span></label>
                        <input type="text" name="ficha100-otra_condicion" class="form-control" maxlength="100" id="id_ficha100-otra_condicion">
                      </div>
                    </div>


          <div class="form-group col-md-6 col-lg-2">
			<label for="event_name">¿Se Aplicara PCR?</label>
			  <asp:dropdownlist ID="ddlPCR" runat="server" class="form-control form-control-lg text-center" >
				  <asp:ListItem value="NO">NO</asp:ListItem>
				  <asp:ListItem value="SI">SI</asp:ListItem>
			  </asp:dropdownlist>
          </div>
          <div class="form-group col-md-6 col-lg-6">
            <label for="event_date">¿Que seguimiento procede?</label>
			  <asp:dropdownlist ID="ddlProced" runat="server" class="form-control form-control-lg text-center" >
				  <asp:ListItem value="Ninguno">Ninguno</asp:ListItem>
				  <asp:ListItem value="A. Repetir prueba en 7 días">A. Repetir prueba en 7 días</asp:ListItem>
				  <asp:ListItem value="B. Seguimiento clínico remoto cada 24 horas">B. Seguimiento clínico remoto cada 24 horas</asp:ListItem>
				  <asp:ListItem value="C. Seguimiento clínico presencial cada 72 horas y seguimiento clínico remota cada 24 horas">C. Seguimiento clínico presencial cada 72 horas y seguimiento clínico remota cada 24 horas</asp:ListItem>
				  <asp:ListItem value="D. Traslado al hospital">D. Traslado al hospital</asp:ListItem>
				  <asp:ListItem value="E. Traslado al hospital en UCI">E. Traslado al hospital en UCI</asp:ListItem>
			  </asp:dropdownlist>
          </div>

                    <fieldset class="border p-2" id="cambia_direccion">                 

                    </fieldset>

                    <div class="form-row">
                      <div class="form-group col-md-12 col-lg-12">
                        <div class="form-group">
                          <label for="agregar_observacion"><strong>¿Desea añadir alguna observación?</strong></label>
                          <textarea runat="server" name="txtObserv" cols="40" rows="5" max_length="100" class="form-control ancho100 no-resize" maxlength="100" id="txtObserv"></textarea>
                        </div>
                      </div>
                    </div>
                    <div class="center-btn">
						<asp:linkbutton id="btnGuardar" runat="server" class="btn btn-success btn-lg" OnClick="btnGuardar_Click">Guardar ficha</asp:linkbutton>
                    </div>

                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      
    </div>
		</form>
  </div>    

</asp:Content>

