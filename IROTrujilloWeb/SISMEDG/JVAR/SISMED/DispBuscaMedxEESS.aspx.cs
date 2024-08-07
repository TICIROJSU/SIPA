using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class SISMEDG_JVAR_SISMED_DispBuscaMedxEESS : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        if (!Page.IsPostBack)
        {
            CargaInicial();
        }
        if (Page.IsPostBack)
        {
        }
    }

    [WebMethod]
    public static string GetMRed(string vRed)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetAtenciones = "";
        try
        {
            string qSql = "select Red, PtoDig " +
                "from SISMED.dbo.Disp where Red like '" + vRed + "%' group by Red, PtoDig; ";
            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            DataTable dtDatoDetAt = objdataset.Tables[0];

            if (dtDatoDetAt.Rows.Count > 0)
            {
                gDetAtenciones += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetAtenciones += "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
                gDetAtenciones += "<th class=''>Nro </th>";
                gDetAtenciones += "<th class=''>Micro Red</th>";
                gDetAtenciones += "</tr>" + Environment.NewLine;
                int nroitem = 0;

                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    gDetAtenciones += "<tr onclick=\"txtMRedCarga('" + dbRow["PtoDig"].ToString() + "')\" data-dismiss='modal'>" +
                        "<td>" + nroitem + "</td>";
                    gDetAtenciones += "<td style='text-align: left;' >" + dbRow["PtoDig"].ToString() + "</td>" +
                        "</tr>";
                    gDetAtenciones += Environment.NewLine;
                }

                gDetAtenciones += "</table></div></div><hr style='border-top: 1px solid blue'>";
            }
        }
        catch (Exception ex)
        {
            gDetAtenciones += "-" + "-" + ex.Message.ToString();
        }
        return gDetAtenciones;
    }
    [WebMethod]
    public static string GetEESS(string vRed, string vMRed)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetAtenciones = "";
        try
        {
            string qSql = "select Red, PtoDig, CodES, Establecimiento " +
                "from SISMED.dbo.Disp where Red like '" + vRed + "%' and PtoDig like '" + vMRed + "%' " +
                "group by Red, PtoDig, CodES, Establecimiento; ";
            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            DataTable dtDatoDetAt = objdataset.Tables[0];

            if (dtDatoDetAt.Rows.Count > 0)
            {
                gDetAtenciones += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetAtenciones += "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
                gDetAtenciones += "<th class=''>Nro </th>";
                gDetAtenciones += "<th class=''>CodEESS</th>";
                gDetAtenciones += "<th class=''>Establecimiento</th>";
                gDetAtenciones += "</tr>" + Environment.NewLine;
                int nroitem = 0;

                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    gDetAtenciones += "<tr onclick=\"txtEESSCarga('" + dbRow["Establecimiento"].ToString() + "', '" + dbRow["CodES"].ToString() + "')\" data-dismiss='modal'>" +
                        "<td>" + nroitem + "</td>";
                    gDetAtenciones += "<td>" + dbRow["CodES"].ToString() + "</td>";
                    gDetAtenciones += "<td style='text-align: left;' >" + dbRow["Establecimiento"].ToString() + "</td>" +
                        "</tr>";
                    gDetAtenciones += Environment.NewLine;
                }

                gDetAtenciones += "</table></div></div><hr style='border-top: 1px solid blue'>";
            }
        }
        catch (Exception ex)
        {
            gDetAtenciones += "-" + "-" + ex.Message.ToString();
        }
        return gDetAtenciones;
    }

    public void CargaInicial()
    {
        try
        {
            //con.Open();
            string qSql = "select * from SISMED.dbo.parametros;";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00); cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter(); adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet(); adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];
            DataRow dbRow = dtDato.Rows[0];

            txtAnio.Text = dbRow["periododisp"].ToString();
            ClassGlobal.tmpvAnio = dbRow["periodo"].ToString();

            //CargaPorcDisp();
            //CargaPorcDisp2();
            CargaRed();
        }
        catch (Exception ex)
        {
            LitTABL1.Text = ex.Message.ToString();
        }
    }

    public void CargaRed()
    {
        try
        {
            string qSql = "select Red from SISMED.dbo.Disp group by Red;";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00); cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter(); adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet(); adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dt = objdataset.Tables[0];
            string gDetAtenciones = "";

            if (dt.Rows.Count > 0)
            {
                gDetAtenciones += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetAtenciones += "<table class='table table-hover' id='tblRed' style='text-align: right; font-size: 14px; '>";
                gDetAtenciones += "<th class=''>Nro </th>";
                gDetAtenciones += "<th class=''>Red </th>";

                int nroitem = 0;
                gDetAtenciones += Environment.NewLine;

                foreach (DataRow dbRow in dt.Rows)
                {
                    nroitem += 1;
                    gDetAtenciones += "<tr onclick=\"txtRedCarga('" + dbRow["Red"].ToString() + "')\" data-dismiss='modal'>";
                    gDetAtenciones += "<td>" + nroitem + "</td>";
                    gDetAtenciones += "<td style='text-align: left;'>" + dbRow["Red"].ToString() + "</td>";
                    gDetAtenciones += "</tr>";
                    gDetAtenciones += Environment.NewLine;
                }

                gDetAtenciones += "</table></div></div><hr style='border-top: 1px solid blue'>";
            }
            else
            {
                gDetAtenciones = "<table></table><hr style='border-top: 1px solid blue'>";
            }
            LitRed.Text = gDetAtenciones;
        }
        catch (Exception ex)
        {
            LitTABL1.Text = ex.Message.ToString();
        }

    }

    protected void bntBuscar_Click(object sender, EventArgs e)
    {
        string vRed = txtRed.Text;
        string vMRed = txtMicroRed.Text;
        string vEESS = txtEESS.Text;
        string vTDisp = DDLTipoDispo.Value;
        try
        {
            //con.Open();
            string qSql = "select SSO, CodMed, Medicamento, CAST(ConsProm AS int) ConsProm, Stock, CAST(Stock_Mes AS int) Stock_Mes " +
                "from SISMED.dbo.Disp where Red like '" + vRed + "%' and PtoDig like '" + vMRed + "%' " +
                "and Establecimiento like '" + vEESS + "%' and SSO like '" + vTDisp + "%' " +
                "order by Stock_Mes desc;";
            qSql = "select CodMed, Medicamento, CAST(sum(ConsProm) AS int) ConsProm, Sum(Stock) Stock, CAST(avg(Stock_Mes) AS int) Stock_Mes " +
                "from SISMED.dbo.Disp where Red like '" + vRed + "%' and PtoDig like '" + vMRed + "%' " +
                "and Establecimiento like '" + vEESS + "%' and SSO like '" + vTDisp + "%' " +
                "group by CodMed, Medicamento " +
                "order by Stock_Mes desc;";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00); cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter(); adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet(); adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];

            GVtable.DataSource = dtDato;
            GVtable.DataBind();

			lblRuta.Text = dtDato.Rows.Count.ToString() + " - Items Mostrados";
        }
        catch (Exception ex)
        {
            LitTABL1.Text = ex.Message.ToString();
        }
    }

    protected void ExportarExcel2_Click(object sender, EventArgs e)
    {
        HttpResponse response = Response;
        response.Clear();
        response.Buffer = true;
        response.ContentType = "application/vnd.ms-excel";
        response.AddHeader("Content-Disposition", "attachment;filename=Reporte.xls");
        response.Charset = "UTF-8";
        response.ContentEncoding = Encoding.Default;
        response.Write(LitTABL1_1.InnerHtml);
        response.End();
    }

    protected void ExportarExcel_Click(object sender, EventArgs e)
    {
        HttpResponse response = Response;
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        Page pageToRender = new Page();
        HtmlForm form = new HtmlForm();
        form.Controls.Add(GVtable);
        pageToRender.Controls.Add(form);
        response.Clear();
        response.Buffer = true;
        response.ContentType = "application/vnd.ms-excel";
        response.AddHeader("Content-Disposition", "attachment;filename=Reporte.xls");
        response.Charset = "UTF-8";
        response.ContentEncoding = Encoding.Default;
        pageToRender.RenderControl(htw);
        response.Write(sw.ToString());
        response.End();
    }

}