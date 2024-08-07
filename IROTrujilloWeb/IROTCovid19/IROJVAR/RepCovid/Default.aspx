<%@ Page Language="C#" MasterPageFile="../../MasterPCov19.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="IROTCovid19_IROJVAR_RepCovid_Default" %>

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
    <div class="card text-center">
      <div class="card-header">
        <h5 class="card-title">Fichas Generadas</h5>
      </div>
      <div class="card-body">
        <div class="form-row">
          <div class="col-lg-5">
            <div class="form-row">
              <div class="col-md-5">
                <label>Inicio </label>
				  <asp:TextBox ID="idfecha1" runat="server" type="date" class="form-control input-sm">2020-10-19</asp:TextBox>
              </div>
              <div class="col-md-5">
                <label>Final </label>
				  <asp:TextBox ID="idfecha2" runat="server" type="date" class="form-control input-sm">2020-10-19</asp:TextBox>
              </div>
              <div class="col-md-2  box-buscar-rango">
				  <label>_ </label>
			<asp:LinkButton ID="btnBuscar" runat="server" OnClientClick="return ValidaFechas();" class="btn btn-success" OnClick="btnBuscar_Click">Buscar</asp:LinkButton>
              </div>
            </div>
          </div>
			
        </div>
      </div>
    </div>

<script>
	var f = new Date();
	//document.write(f.getDate() + "/" + (f.getMonth() +1) + "/" + f.getFullYear());
	function ValidaFechas() {
		if (<%=idfecha1.ClientID%>.value > <%=idfecha2.ClientID%>.value) {
			alert("Fechas Incorrectas");
			return false;
		}
	}
</script>

    <br>
    <div class="card shadow mb-4">
      <div class="card-header py-3">
		  
        <h6 class="m-0 font-weight-bold text-primary"><img src="../../Imagenes/XLSDownLoad.png" width="50px" /> Lista de registros</h6>
      </div>
      <div class="card-body">

        <div class="tab-content">
          <div id="menu1" class="tab-pane active">
            <div class="table">
              <div id="table-ficha-100_wrapper" class="dataTables_wrapper dt-bootstrap4 no-footer">

			  <div class="row"><div class="col-sm-12">
			  <table class="table table-bordered table-hover table-striped dataTable no-footer" id="table-ficha-1010" style="width: 100%;" role="grid" >
                <thead>
                <tr role="row">
					<th class="sorting_disabled" style="width: 46px;">#</th>
					<th class="sorting_disabled" style="width: 131px;">N°Doc.</th>
					<th class="sorting_disabled" style="width: 338px;">Nombre</th>
					<th class="sorting_disabled" style="width: 101px;">Sexo</th>
					<th class="sorting_disabled" style="width: 103px;">Edad</th>
					<th class="sorting_disabled" style="width: 155px;">Teléfono</th>
					<th class="sorting_disabled" style="width: 130px;">Acción</th>
				</tr>
                </thead>
              <tbody id="tbf100" runat="server">
				  <tr class="odd">
					<td valign="top" colspan="8" class="dataTables">Ningún dato disponible en esta tabla</td>
				  </tr>
			  </tbody>
			</table>
			  <div id="table-ficha-100_processing" class="dataTables_processing card" style="display: none;">Procesando...</div>
			  </div></div>
			  <div class="row">
			  <div class="col-sm-12 col-md-5">
				  <div class="dataTables_info" id="table-ficha-100_info" role="status" aria-live="polite">
					  Mostrando <span runat="server" id="dCantReg">0</span> registros
				  </div>
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


