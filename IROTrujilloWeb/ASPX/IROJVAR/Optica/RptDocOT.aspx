<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RptDocOT.aspx.cs" Inherits="ASPX_IROJVAR_Optica_RptDocOT" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        .tdborder
        {
	        color:black;
	        vertical-align:middle;
	        border-top:1.0pt solid windowtext;
	        border-right:none;
	        border-bottom:1.0pt solid windowtext;
	        border-left:none;
	        white-space:normal;
        }
    </style>

<%
    string strCodigo = Request.QueryString["Codigo"];

    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    string qSql = "select * from IROBDOptica.dbo.OrdenTrabajo where CodigoOT = '" + strCodigo + "'";
    SqlCommand cmd2 = new SqlCommand(qSql, conSAP00);
    cmd2.CommandType = CommandType.Text;
    SqlDataAdapter adapter2 = new SqlDataAdapter();
    adapter2.SelectCommand = cmd2;

    conSAP00.Open();
    DataSet objdataset2 = new DataSet();
    adapter2.Fill(objdataset2);
    conSAP00.Close();

    DataTable dtCab = objdataset2.Tables[0];

%>
</head>
<body>
    <form id="form1" runat="server">
        <div>

        <br />

        <table style="width:330px; display:block; font-family:'MS Reference Sans Serif'; font-size:10px; ">
          <tr>
            <td style="text-align:center">
                <%--<div align="center">INSTITUTO REGIONAL DE OFTALMOLOGIA</div>--%>
                INSTITUTO REGIONAL DE <br />
                OFTALMOLOGIA - JSU <br />
                <p>
                    R.U.C.: 20314801327
                </p>
                BL. AMERICA OESTE S/ MZA. P LOTE. 7A URB. <br />
                NATASHA ALTA (COSTADO DE LA CORTE <br />
                SUP.JUSTICIA) La Libertad <br />
                <p>Trujillo - La Libertad</p>
                <b>TICKET ORDEN DE TRABAJO</b> <br />
                N° <asp:Label ID="lblNro" runat="server" ></asp:Label> <br />

            </td>
        </tr>
        <tr>
            <td>
                <table>
                  <tr>
                    <td width="100">FECHA</td><td>07/04/2021</td>
                  </tr>
                  <tr>
                    <td>PACIENTE</td><td>BACA ARIAS, SOFIA CATALINA</td>
                  </tr>
                </table>
                <br />
                [LEJOS]
                <table width="100%" style="text-align:center">
                  <tr class="tdborder">
                    <th scope="col" class="tdborder"></th>
                    <th scope="col" class="tdborder">Esfera</th>
                    <th scope="col" class="tdborder">Cilindro</th>
                    <th scope="col" class="tdborder">Eje</th>
                    <th scope="col" class="tdborder">Color</th>
                  </tr>
                  <tr>
                    <td>OD</td><td>0</td><td>0</td><td>0</td><td>0</td>
                  </tr>
                  <tr>
                    <td>OI</td><td>0</td><td>0</td><td>0</td><td>0</td>
                  </tr>
                </table>
                <b>DIP: 0 mm</b><br />
                [CERCA]
                <table width="100%" style="text-align:center">
                  <tr>
                    <th scope="col" class="tdborder"></th>
                    <th scope="col" class="tdborder">Esfera</th>
                    <th scope="col" class="tdborder">Cilindro</th>
                    <th scope="col" class="tdborder">Eje</th>
                    <th scope="col" class="tdborder">Color</th>
                  </tr>
                  <tr>
                    <td>OD</td><td>0</td><td>0</td><td>0</td><td>0</td>
                  </tr>
                  <tr>
                    <td>OI</td><td>0</td><td>0</td><td>0</td><td>0</td>
                  </tr>
                </table>
                <b>DIP: 0 mm</b><br />
                <h3><p><b>ADD: 0 </b></p></h3>





                -------------------------------------------------------------------------

            </td>
          </tr>
        </table>
                


        </div>
    </form>
</body>
</html>
