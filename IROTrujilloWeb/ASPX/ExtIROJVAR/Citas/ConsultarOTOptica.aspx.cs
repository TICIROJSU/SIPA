using System;
using System.Data;
using System.Data.SqlClient;

public partial class ASPX_ExtIROJVAR_Citas_ConsultarOTOptica : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        string idPruebaCovid = Request.QueryString["OTrabajo"];
        CargaInicial(idPruebaCovid);
    }

    public void CargaInicial(string idcov)
    {
        try
        {
            //con.Open();
            string qSql = "DECLARE @vCodOTrabajo VARCHAR(50); " +
                "   DECLARE @vCodVenta VARCHAR(50); " +
                "select @vCodOTrabajo = Codigo from IRO_BD_OPTICADESK.dbo.OrdenTrabajo where NumeroOrdenTrabajo = '" + idcov + "'; " +
                "select * from IRO_BD_OPTICADESK.dbo.OrdenTrabajo where NumeroOrdenTrabajo = '" + idcov + "'; " +
                "select @vCodVenta = Codigo from IRO_BD_OPTICADESK.dbo.Venta where CodigoOrdenTrabajo = @vCodOTrabajo; " +
                "select * from IRO_BD_OPTICADESK.dbo.Venta where CodigoOrdenTrabajo = @vCodOTrabajo; " +
                "select * from IRO_BD_OPTICADESK.dbo.LineaProductoVenta where CodigoVenta = @vCodVenta";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00); cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter(); adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet(); adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato1 = objdataset.Tables[0];

            if (dtDato1.Rows.Count > 1)
            {
                this.Page.Response.Write("<script language='JavaScript'>window.alert('Codigo encuentra mas de 1 Registro. Verifique');</script>");
                return;
            }

            DataTable dtDato2 = objdataset.Tables[1];

            DataRow dbRow1 = dtDato1.Rows[0];
            DataRow dbRow2 = dtDato2.Rows[0];

            DataTable dtDato3 = objdataset.Tables[2];

            //dPerDNI.InnerHtml = "<b>" + dbRow["TipCita"].ToString() + "&nbsp;</b>";
            //dPerApeP.InnerHtml = "<b>" + dbRow["AnioCita"].ToString() + "&nbsp;</b>";

            tdOTrabajo.InnerText = "N° " + dbRow1["NumeroOrdenTrabajo"].ToString();
            tdFechaDoc.InnerText = dbRow1["FechaRegistro"].ToString();
            tdLejosODEsf.InnerText = dbRow1["EsferaODLejos"].ToString();
            tdLejosODCil.InnerText = dbRow1["CilindroODLejos"].ToString();
            tdLejosODEje.InnerText = dbRow1["EjeODLejos"].ToString();
            tdLejosODCol.InnerText = dbRow1["ColorODLejos"].ToString();
            tdLejosOIesf.InnerText = dbRow1["EsferaOILejos"].ToString();
            tdLejosOICil.InnerText = dbRow1["CilindroOILejos"].ToString();
            tdLejosOIEje.InnerText = dbRow1["EjeOILejos"].ToString();
            tdLejosOICol.InnerText = dbRow1["ColorOILejos"].ToString();
            tdLejosDIP.InnerText = ": " + dbRow1["DipLejos"].ToString() + "mm";
            tdCercaODEsf.InnerText = dbRow1["EsferaODCerca"].ToString();
            tdCercaODCil.InnerText = dbRow1["CilindroODCerca"].ToString();
            tdCercaODEje.InnerText = dbRow1["EjeODCerca"].ToString();
            tdCercaODCol.InnerText = dbRow1["ColorODCerca"].ToString();
            tdCercaOIEsf.InnerText = dbRow1["EsferaOICerca"].ToString();
            tdCercaOICil.InnerText = dbRow1["CilindroOICerca"].ToString();
            tdCercaOIEje.InnerText = dbRow1["EjeOICerca"].ToString();
            tdCercaOICol.InnerText = dbRow1["ColorOICerca"].ToString();
            tdCercaDIP.InnerText = ": " + dbRow1["DipCerca"].ToString() + "mm";
            tdADD.InnerText = ": " + dbRow1["AddOrden"].ToString();
            tdDocComent.InnerText = dbRow1["Comentario"].ToString();

            tdPaciente.InnerText = dbRow2["DescripcionPaciente"].ToString();
            tdDocSubTotal.InnerText = dbRow2["SubTotal"].ToString();
            tdDocIGV.InnerText = dbRow2["Igv"].ToString();
            tdDocDesc.InnerText = dbRow2["Descuento"].ToString();
            tdDocTotal.InnerText = dbRow2["Total"].ToString();

            string gHTML = "";
            foreach (DataRow dbRow3 in dtDato3.Rows)
            {
                gHTML += "<tr height='58' style='mso-height-source:userset;height:43.5pt'>";
                gHTML += "  <td height='58' class='xl6828770' style='height:43.5pt;border-top:none'>" + dbRow3["Cantidad"].ToString() + "</td>";
                gHTML += "  <td colspan='2' class='xl8728770' width='118' style='width:88pt'>" + dbRow3["Descripcion"].ToString() + "</td>";
                gHTML += "  <td class='xl6928770' style='border-top:none'>" + dbRow3["PrecioActivo"].ToString() + "</td>";
                gHTML += "  <td class='xl6928770' style='border-top:none'>" + dbRow3["Importe"].ToString() + "</td>";
                gHTML += "</tr>";
            }

            LitDocuDet.Text = gHTML;
            //tdFecCita.InnerText = dbRow["DiaCita"].ToString() + '/' + dbRow["MesCita"].ToString() + '/' + dbRow["AnioCita"].ToString();
            //tdFecCita.InnerText = dbRow["FecCita"].ToString();


        }
        catch (Exception ex)
        {
            LitError.Text = ex.Message.ToString();
        }
    }

}