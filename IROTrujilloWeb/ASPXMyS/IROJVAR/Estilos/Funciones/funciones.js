/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
function vercboMred(id){
    var id = id;
    //alert(id);
    document.getElementById("divcboMicroRed").innerHTML = "<img src='../imagenes/preload.gif' width='16' height='16'>";
        $.post("cbomred.php",{
            id:id,
        },function(data){
            $("#divcboMicroRed").html(data);
        });
    //document.getElementById("divcboestablecimiento").innerHTML = "<select class='form-control' name='cboeess' id='cboeess'><option value=''>Selecciona EESS</option></select>";
    verHabDet(id, '', '');
}
function vercboEESS(idRed, idMRed){
    document.getElementById("divcboestablecimiento").innerHTML = "<img src='../imagenes/preload.gif' width='16' height='16'>";
        $.post("cboeess.php",{
            idRed:idRed,
            idMRed:idMRed,
        },function(data){
            $("#divcboestablecimiento").html(data);
        });
    verHabDet(idRed, idMRed, '');
}
function verHabDet(idRed, idMRed, idEESS){
    document.getElementById("HabDet").innerHTML = "<img src='../imagenes/preload.gif' width='128' height='128'>";
        $.post("habdet.php",{
            idRed:idRed,
            idMRed:idMRed,
            idEESS:idEESS,
        },function(data){
            $("#HabDet").html(data);
        });
	verHabFiltro(idRed, idMRed, idEESS);
}
function verHabDet2(idRed, idMRed, idEESS, idEdad, idSexo, idEstado, idSeguro){
    document.getElementById("HabDet").innerHTML = "<img src='../imagenes/preload.gif' width='128' height='128'>";
        $.post("habdet.php",{
            idRed:idRed,
            idMRed:idMRed,
            idEESS:idEESS,
            idEdad:idEdad,
            idSexo:idSexo,
            idEstado:idEstado,
            idSeguro:idSeguro,
        },function(data){
            $("#HabDet").html(data);
        });
}
function verHabDetDNI(idRed, idMRed, idEESS, idDNI, idTipoBuscar){
    document.getElementById("HabDet").innerHTML = "<img src='../imagenes/preload.gif' width='32' height='32'>";
        $.post("habdet.php",{
            idRed:idRed,
            idMRed:idMRed,
            idEESS:idEESS,
            idDNI:idDNI,
            idTipoBuscar:idTipoBuscar,
        },function(data){
            $("#HabDet").html(data);
        });
}
function verHabFiltro(idRed, idMRed, idEESS){
    document.getElementById("habfiltro").innerHTML = "<img src='../imagenes/preload.gif' width='32' height='32'>";
        $.post("habfiltro.php",{
            idRed:idRed,
            idMRed:idMRed,
            idEESS:idEESS,
        },function(data){
            $("#habfiltro").html(data);
        });
}
//Opciones para Registro de Usuarios:
function vercboRedreg(idDISA){
    document.getElementById("divcboREDreg").innerHTML = "<img src='../imagenes/preload.gif' width='16' height='16'>";
        $.post("cbored.php",{
            idDISA:idDISA,
        },function(data){
            $("#divcboREDreg").html(data);
        });
    document.getElementById("divcboMREDreg").innerHTML = "<select class='form-control' style='padding-right: 0px; padding-left: 2px;' id='cboMREDreg' name='cboMREDreg' onchange='FcargaEESS();'><option value=''>MICRORED</option></select>";
    document.getElementById("divcboEESSreg").innerHTML = "<select class='form-control' name='cboEESSreg' id='cboEESSreg'><option value=''>EESS</option></select>";
}
function vercboMredreg(idDISA, idRed){
    document.getElementById("divcboMREDreg").innerHTML = "<img src='../imagenes/preload.gif' width='16' height='16'>";
        $.post("cbomred.php",{
            idDISA:idDISA,
            idRed:idRed,
        },function(data){
            $("#divcboMREDreg").html(data);
        });
    document.getElementById("divcboEESSreg").innerHTML = "<select class='form-control' name='cboEESSreg' id='cboEESSreg'><option value=''>EESS</option></select>";
}
function vercboEESSreg(idDISA, idRed, idMRed){
    document.getElementById("divcboEESSreg").innerHTML = "<img src='../imagenes/preload.gif' width='16' height='16'>";
        $.post("cboeess.php",{
            idDISA:idDISA,
            idRed:idRed,
            idMRed:idMRed,
        },function(data){
            $("#divcboEESSreg").html(data);
        });
}
///////////////////////////////////////
function vercboPROVreg(idDEPA){
    document.getElementById("divcboREDreg").innerHTML = "<img src='../imagenes/preload.gif' width='16' height='16'>";
        $.post("cboprov.php",{
            idDEPA:idDEPA,
        },function(data){
            $("#divcboREDreg").html(data);
        });
    document.getElementById("divcboMREDreg").innerHTML = "<select class='form-control' style='padding-right: 0px; padding-left: 2px;' id='cboMREDreg' name='cboMREDreg''><option value=''>DISTRITO</option></select>";
    document.getElementById("divcboEESSreg").innerHTML = "<select class='form-control' name='cboEESSreg' id='cboEESSreg'><option value=''></option></select>";
}
function vercboDistritoreg(idDEPA, idPROV){
    document.getElementById("divcboMREDreg").innerHTML = "<img src='../imagenes/preload.gif' width='16' height='16'>";
        $.post("cbodist.php",{
            idDEPA:idDEPA,
            idPROV:idPROV,
        },function(data){
            $("#divcboMREDreg").html(data);
        });
    document.getElementById("divcboEESSreg").innerHTML = "<select class='form-control' name='cboEESSreg' id='cboEESSreg'><option value=''></option></select>";
}


function rellenaizq(number, targetLength) {
    var output = number + '';
    while (output.length < targetLength) {
        output = '0' + output;
    }
    return output;
}

function exportTableToExcel(tableID, filename = '') {
    var downloadLink;
    var dataType = 'application/vnd.ms-excel';
    var tableSelect = document.getElementById(tableID);
    var tableHTML = tableSelect.outerHTML.replace(/ /g, '%20').replace(/#/g, '%23');
    // Reemplaza espacio en blanco ' ' por %20. # por %23
    //var tableHTML = tableHTML.outerHTML.replace(/ /g, '%20');
    // Specify file name
    filename = filename ? filename + '.xls' : 'excel_Reporte.xls';
    // Create download link element
    downloadLink = document.createElement("a");
    document.body.appendChild(downloadLink);
    if (navigator.msSaveOrOpenBlob) {
        var blob = new Blob(['ufeff', tableHTML], {
            type: dataType
        });
        navigator.msSaveOrOpenBlob(blob, filename);
    } else {
        // Create a link to the file
        downloadLink.href = 'data:' + dataType + ', ' + tableHTML;
        // Setting the file name
        downloadLink.download = filename;
        //triggering the function
        downloadLink.click();
    }
}

function fBscTblHTML(txtBusca, tblBusca, nrocol) {
    // Declare variables
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById(txtBusca);
    filter = input.value.toUpperCase();
    table = document.getElementById(tblBusca);
    tr = table.getElementsByTagName("tr");

    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[nrocol];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}

function fBscTblHTMLOrden(txtBusca, tblBusca, nrocol) {
    // Declare variables
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById(txtBusca);
    filter = input.value.toUpperCase();
    table = document.getElementById(tblBusca);
    tr = table.getElementsByTagName("tr");

    var item = 0;

    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[nrocol];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                item = item + 1;
                tr[i].style.display = "";
                tr[i].getElementsByTagName("td")[0] = item;
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}


