<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConsultaSIS.aspx.cs" Inherits="ASPX_ExtIROJVAR_Citas_ConsultaSIS" %>

<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN'>
<html>
<body ms_positioning='GridLayout' bgcolor='#e9edf1' class='container'>
    <form name='Form1' method='get' action='http://app.sis.gob.pe/SisConsultaEnLinea/Consulta/frmConsultaEnLinea.aspx' id='Form1'>
<div>
<input type='hidden' name='__EVENTTARGET' id='__EVENTTARGET' value='' />
<input type='hidden' name='__EVENTARGUMENT' id='__EVENTARGUMENT' value='' />
<input type='hidden' name='__VIEWSTATE' id='__VIEWSTATE' value='/wEPDwUKLTgxNzQ0Nzc1NmQYAQUPQ2FwdGNoYUNvbnRyb2wxDwUkMzc1NDI0MjYtN2VkZC00MGMzLWE3ODItZDVjOTY5YWNjMmVjZHw6q20op+5RL7nNbcqxOUse9+/WDA4fJS5kyhO3DK/F' />
</div>
<div>
	<input type='hidden' name='__EVENTVALIDATION' id='__EVENTVALIDATION' value='/wEWDwKhm8T5AwK81oaXAgKY4sHZDAKU4o3aDAKV4o3aDAKwoOzhCwKnscqhCwKHkryACwKajvi6BQKBxNfMAQKNxJvPAQKPxJvPAQLHgbrgDgKVq7KvCALxm8umBfayLDtHdP4qyaOWzrxNsZ4l5ESx3ccg8PpgrDuXeXoh' />
</div>
    <input type='hidden' name='hdnTipo' id='hdnTipo' />
    <table cellspacing='0' cellpadding='0' width='900' align='center' bgcolor='#ffffff' 
        border='0'>
        <tr>
            <td align='center' colspan='2'></td>
        </tr>
        <tr>
            <td style='height: 365px' valign='top' align='left' width='250'>
                <div id='tblBusqueda'>
                    <table cellspacing='0' cellpadding='0' width='100%' border='0'>
                        <tr style='display:none'>
                            <td class='TituloEncabezado2' valign='top' width='100%' height='24'>
                                <br />Búsqueda por:</td>
                            <td width='240'>
                            </td>
                        </tr>
                        <tr style='display:none'>
                            <td align='center' width='100%' height='24'>
                                <select name='cboTipoBusqueda' id='cboTipoBusqueda' class='control' onchange='jsf_TipoBusqueda(this.value);' style='width:158px;'>
									<option value='2' selected='selected'>Tipo de Documento</option>
								</select>
                            </td>
                            <td align='center' width='100%' height='24'>
                            </td>
                        </tr>
                        <tr>
                            <td valign='top' height='285' width='240'>
                                <table id='tblFiltro1' height='200' cellspacing='0' cellpadding='0' width='100%' 
                                    border='0' style='display:none'>
                                    <tr>
                                        <td align='center' height='20'>
                                            <input name='txtApePaterno' type='text' maxlength='40' id='txtApePaterno' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align='center' height='20'>
                                            <input name='txtApeMaterno' type='text' maxlength='40' id='txtApeMaterno' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align='center' height='20'>
                                            <input name='txtPriNombre' type='text' maxlength='40' id='txtPriNombre' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align='center' height='20'>
                                            <input name='txtSegNombre' type='text' maxlength='40' id='txtSegNombre' />
                                        </td>
                                    </tr>
                                </table>
                                <table id='tblFiltro2' height='110' cellspacing='0' cellpadding='0' width='100%' 
                                    border='0'>
                                    <tr>
                                        <td width='252' height='20' class='TituloEncabezado2'>Tipo de Documento:</td>
                                    </tr>
                                    <tr>
                                        <td align='center' width='252' height='20'>
                                            <select name='cboTipoDocumento' id='cboTipoDocumento' class='control' onchange='jsf_TipoDocumento(this.value);' style='width:158px;'>
												<option value='1' selected='selected' >DNI</option>
												<option value='3'>CARNE DE EXTRANJERIA</option>
											</select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width='252' height='20' class='TituloEncabezado2'>Numero de Documento:</td>
                                    </tr>
                                    <tr>
                                        <td align='center' height='20'>
                                            <input name='txtNroDocumento' type='text' maxlength='9' id='txtNroDocumento' name='txtNroDocumento' onkeypress='return jsf_SoloNumero(event,this,true);' onblur='this.value=jsf_OnPasteNumbers(this.value);' style='width: 100px' autofocus /><br />
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                                <table id='tblFiltro3' height='110' cellspacing='0' cellpadding='0' width='100%' 
                                    border='0'>
                                    <tr>
                                        <td align='center' height='20'>
                                            <div id='ValidationSummary1' class='TituloEncabezado2' style='color:Red;display:none;'></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align='center' height='20'>
                                            <input type='submit' name='btnConsultar' value='Consultar' onclick='return jsf_Consultar();WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions(&quot;btnConsultar&quot;, &quot;&quot;, true, &quot;&quot;, &quot;&quot;, false, false))' id='btnConsultar' class='boton' name='btnConsultar' style='width: 80px; background-color: #8b6c17' />
                                            <input type='submit' name='btnBorrar' value='Borrar' onclick='jsf_Borrar();WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions(&quot;btnBorrar&quot;, &quot;&quot;, true, &quot;&quot;, &quot;&quot;, false, false))' id='btnBorrar' class='boton' name='btnBorrar' style='width: 50px; background-color: #8b6c17' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height='5'>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</form>

    <script>
        const valores = window.location.search;
        const urlParams = new URLSearchParams(valores);
        var producto = urlParams.get('iCita');

        document.getElementById("txtNroDocumento").value = producto;

        document.getElementById("btnConsultar").click();

	</script>  

</body>
</html>

