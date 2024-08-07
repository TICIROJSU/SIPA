/**
 * /
 * @param {any} number
 * @param {any} targetLength
 */
// Variables Globales
var ipwservice = 'http://150.10.50.7:8000';


function rellenaizq(number, targetLength) {
    var output = number + '';
    while (output.length < targetLength) {
        output = '0' + output;
    }
    return output;
}

function exportTableToExcel(tableID, filename = '') {
    var tableHTML = document.getElementById(tableID).outerHTML;
    var blob = new Blob([
        new Uint8Array([0xEF, 0xBB, 0xBF]), // UTF-8 BOM
        tableHTML
    ], {
            type: "application/vnd.ms-excel;charset=utf-8"
        });

    var downloadLink = document.createElement("a");
    document.body.appendChild(downloadLink);
    // Create a link to the file
    downloadLink.href = URL.createObjectURL(blob);

    filename = filename ? filename + '.xls' : 'ReporteExcel.xls';
    downloadLink.download = filename;
    //triggering the function
    downloadLink.click();
}

function exportTableToExcel_versionOriginal(tableID, filename = '') {
    var downloadLink;
    var dataType = 'application/vnd.ms-excel;charset=utf-8';
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
        navigator.msSaveOrOpenBlob(blob, filename + "blob");
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

    tblCantRegMostrados = 0;
    tblCantRegOcultos = 0;
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[nrocol];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
                tblCantRegMostrados++;
            } else {
                tr[i].style.display = "none";
                tblCantRegOcultos;
            }
        }
    }

    localStorage.setItem('tblCantRegMostrados', tblCantRegMostrados);

}

function fBscTblHTML5(tblBusca, txt1, col1, txt2, col2, txt3, col3, txt4, col4, txt5, col5) {
    // Declare variables
    var input1, filter1, table, tr, td1, i, txtValue1;
    var input2, filter2, td2, txtValue2; var input3, filter3, td3, txtValue3;
    var input4, filter4, td4, txtValue4; var input5, filter5, td5, txtValue5;
    input1 = document.getElementById(txt1); filter1 = input1.value.toUpperCase();
    input2 = document.getElementById(txt2); filter2 = input2.value.toUpperCase();
    input3 = document.getElementById(txt3); filter3 = input3.value.toUpperCase();
    input4 = document.getElementById(txt4); filter4 = input4.value.toUpperCase();
    input5 = document.getElementById(txt5); filter5 = input5.value.toUpperCase();
    table = document.getElementById(tblBusca);
    tr = table.getElementsByTagName("tr");

    tblCantRegMostrados = 0;
    tblCantRegOcultos = 0;
    for (i = 0; i < tr.length; i++) {
        td1 = tr[i].getElementsByTagName("td")[col1];
        td2 = tr[i].getElementsByTagName("td")[col2]; td3 = tr[i].getElementsByTagName("td")[col3];
        td4 = tr[i].getElementsByTagName("td")[col4]; td5 = tr[i].getElementsByTagName("td")[col5];
        if (td1) {
            txtValue1 = td1.textContent || td1.innerText;
            txtValue2 = td2.textContent || td2.innerText;
            txtValue3 = td3.textContent || td3.innerText;
            txtValue4 = td4.textContent || td4.innerText;
            txtValue5 = td5.textContent || td5.innerText;
            if ( (txtValue1.toUpperCase().indexOf(filter1) > -1) && (txtValue2.toUpperCase().indexOf(filter2) > -1) && (txtValue3.toUpperCase().indexOf(filter3) > -1) && (txtValue4.toUpperCase().indexOf(filter4) > -1) && (txtValue5.toUpperCase().indexOf(filter5) > -1) ) {
                tr[i].style.display = "";
                tblCantRegMostrados++;
            } else {
                tr[i].style.display = "none";
                tblCantRegOcultos;
            }
        }
    }

    localStorage.setItem('tblCantRegMostrados', tblCantRegMostrados);

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


function fsortTblEliminado(n, tbl) {
    var table, rows, switching, i, x, y, shouldSwitch, dir, switchcount = 0;
    table = document.getElementById('tblbscrJS');
    //table = tbl;
    switching = true;
    dir = "asc";
    while (switching) {
        switching = false;
        rows = table.rows;
        for (i = 1; i < (rows.length - 1); i++) {
            shouldSwitch = false;
            x = rows[i].getElementsByTagName("TD")[n];
            y = rows[i + 1].getElementsByTagName("TD")[n];
            if (dir == "asc") {
                if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
                    shouldSwitch = true;
                    break;
                }
            } else if (dir == "desc") {
                if (x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase()) {
                    shouldSwitch = true;
                    break;
                }
            }
        }
        if (shouldSwitch) {
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
            switchcount++;
        } else {
            if (switchcount == 0 && dir == "asc") {
                dir = "desc";
                switching = true;
            }
        }
    }
}



