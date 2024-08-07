<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ScriptServer.aspx.cs" Inherits="ASPX_IROJVAR_zPruebas_ScriptServer" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
<script runat="server">
void Welcome()
{
    Response.Write("Welcome to our web site.");
}

</script>
    <%

        SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
        string qSql = "select * from IROBDOptica.dbo.ProductosOptica ";
        SqlCommand cmd2 = new SqlCommand(qSql, conSAP00);
        cmd2.CommandType = CommandType.Text;
        SqlDataAdapter adapter2 = new SqlDataAdapter();
        adapter2.SelectCommand = cmd2;

        	conSAP00.Open();
			DataSet objdataset = new DataSet();
			adapter2.Fill(objdataset);
			conSAP00.Close();

        DataTable dt = objdataset.Tables[0];

        string gHTML="";

            if (dt.Rows.Count > 0)
			{
				gHTML += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
				gHTML += "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
				gHTML += "<th class=''>cod </th>";
				gHTML += "<th class=''>producto</th>";
				gHTML += "<th class=''>precio</th>";
				gHTML += "<th class=''>Montura</th>";
				gHTML += "<th class=''>stock</th>";
				gHTML += "<th class=''>cantidad</th>";
				gHTML += "<th class=''>Agregar</th>";
				gHTML += "</tr>" + Environment.NewLine;
				int nroitem = 0;

				foreach (DataRow dbRow in dt.Rows)
				{
					nroitem += 1;
					string vcod, vprod, vprec, vstk;
					vcod = dbRow["Codigo"].ToString();
					vprod = dbRow["Nombre"].ToString();
					vprec = dbRow["PrecioVenta"].ToString();
					vstk = dbRow["StockActual"].ToString();
					//gHTML += "<tr onclick=\"agregarFila('" + vcod + "', '" + vprod + "', '" + vprec + "')\" data-dismiss='modal'>";
					gHTML += "<tr>";
					gHTML += "<td class='' >" + vcod + "</td>";
					gHTML += "<td style='text-align: left;' >" + vprod + "</td>";
					gHTML += "<td style='text-align: left;' >" + vprec + "</td>";
					gHTML += "<td style='text-align: left;' >" + vstk + "</td>";
					gHTML += "<td style='text-align: left;' ><input type='number' name='txtCant_" + nroitem + "' id='txtCant_" + nroitem + "' onkeyup='agregaprodKD(" + nroitem + ")' class='form-control input-lg'></td>";
					gHTML += "<td style='text-align: left;' >" +
						"<div class='btn btn-info' id='btnAgregaProd" + nroitem + "' onclick=\"agregarFila('" + vcod + "', '" + vprod + "', '" + vprod + "', '" + vprec + "', document.getElementById('txtCant_" + nroitem + "').value, '" + vprod + "')\" data-dismiss='modal'> Agregar</div>" +
						"</td>";
					//cod, prod, codmontura, prec, cant, idmontura
					gHTML += "</tr>";
					gHTML += Environment.NewLine;
				}

				gHTML += "</table></div></div><hr style='border-top: 1px solid blue'>";
			}


        Response.Write(gHTML);
    %>
</head>
<body>
<%
    Welcome();
%>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
