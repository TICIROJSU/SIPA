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

public partial class ASPX_PerIROJVAR_TrabPresencial_HabilitarRegistro : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;

        if (!Page.IsPostBack)
        {
            int vmonth = DateTime.Now.Month;
            //DDLMes.SelectedIndex = vmonth;

            string vd, vm, vy;
            vd = DateTime.Now.Day.ToString();
            vm = DateTime.Now.Month.ToString();
            vy = DateTime.Now.Year.ToString();

            //txtDesde.Text = vy + "-" + (vm).PadLeft(2, '0') + "-" + vd.PadLeft(2, '0');
            //txtHasta.Text = vy + "-" + (vm).PadLeft(2, '0') + "-" + vd.PadLeft(2, '0');
        }

    }

    [WebMethod]
    public static string SetHabilitaRem(string vDNIPer)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetHtml = "";
        try
        {
            string qSql = "UPDATE RRHH.dbo.PERSONAL SET REMOTO_PER = '1' WHERE DNI_PER = @DNI_PER";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@DNI_PER", vDNIPer);
            adapter.SelectCommand = cmd;

            conSAP00i.Open(); cmd.ExecuteScalar(); conSAP00i.Close();

            gDetHtml += "Conforme";
        }
        catch (Exception ex)
        {
            gDetHtml += "ERROR: " + ex.Message.ToString() + Environment.NewLine + ex.ToString();
        }
        return gDetHtml;
    }

    [WebMethod]
    public static string SetInHabilitaRem(string vDNIPer)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetHtml = "";
        try
        {
            string qSql = "UPDATE RRHH.dbo.PERSONAL SET REMOTO_PER = '0' WHERE DNI_PER = @DNI_PER";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@DNI_PER", vDNIPer);
            adapter.SelectCommand = cmd;

            conSAP00i.Open(); cmd.ExecuteScalar(); conSAP00i.Close();

            gDetHtml += "Conforme";
        }
        catch (Exception ex)
        {
            gDetHtml += "ERROR: " + ex.Message.ToString() + Environment.NewLine + ex.ToString();
        }
        return gDetHtml;
    }

    [WebMethod]
    public static string GetTipoTrabajoInsertValida(string vIdPer, string vDNIPer, string vFechaTrab)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gHTML = "Conforme";
        try
        {
            string qSql = "select * from NUEVA.dbo.JVARTipoTrab where DNIPER = '" + vDNIPer + "' and FechaTrab = '" + vFechaTrab + "'";
            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text;

            SqlDataAdapter adapter2 = new SqlDataAdapter(); adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            if (objdataset.Tables[0].Rows.Count > 0)
            {
                gHTML = "Discorde";
            }
        }
        catch (Exception ex)
        {
            gHTML += "-" + "-" + ex.Message.ToString();
        }
        return gHTML;
    }


    protected void bntBuscar_Click(object sender, EventArgs e)
    {
        CargaTablaDT();
    }

    public void CargaTablaDT()
    {
        ClassGlobal.varGlobalTmp = "";
        string vEESS = txtEESS.Text;
        string vDato = txtDNI.Text;
        try
        {
            //con.Open();
            string qSql = "select * from RRHH.dbo.PERSONAL " +
                "where DNI_PER + NOM_PERSONAL like '%' + @dato + '%' ";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@dato", vDato);

            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];

            //GVtable.DataSource = dtDato;
            //GVtable.DataBind();
            CargaTabla(dtDato);
        }
        catch (Exception ex)
        {
            //Label1.Text = "Error";
            ex.Message.ToString();
            LitTABL1.Text = ex.Message.ToString();
        }
    }

    public void CargaTabla(DataTable dtDato)
    {
        string html = "";
        if (dtDato.Rows.Count > 0)
        {
            html += Environment.NewLine + "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
            html += "<tr>";
            html += "<th class=''>Accion</th>";
            html += "<th class=''>ID</th>";
            html += "<th class=''>Personal</th>";
            html += "<th class=''>Habilita</th>";
            html += "<th class=''>InHabil.</th>";
            html += "<th class=''>DNI</th>";
            html += "<th class=''>Cargo</th>";
            html += "<th class=''>Regimen</th>";
            html += "</tr>" + Environment.NewLine;
            int nroitem = 0;
            foreach (DataRow dbRow in dtDato.Rows)
            {
                nroitem += 1;
                string vhDisable = "";
                if (dbRow["REMOTO_PER"].ToString() == "0") { vhDisable = "disabled='true'"; }

                html += "<tr >";
                html += "<td class='text-left' >"
                    + "<button " + vhDisable + " type='button' class='btn bg-navy'><i class='fa fa-fw fa-eye'></i></button>"
                    + "</td>";
                html += "<td class='text-left' >" + dbRow["ID_PER"].ToString() + "</td>";
                html += "<td class='text-left' >" + dbRow["NOM_PERSONAL"].ToString() + "</td>";
                html += "<td class='text-left' >"
                    + "<button type='button' class='btn bg-olive' data-toggle='modal' data-target='#modalPerDet' onclick=\"fHabilitaRem('" + dbRow["DNI_PER"].ToString() + "')\"><i class='fa fa-fw fa-edit'></i></button>"
                    + "</td>";
                html += "<td class='text-left' >"
                    + "<button type='button' class='btn bg-orange' data-toggle='modal' data-target='#modalPerDet' onclick=\"fInHabilitaRem('" + dbRow["DNI_PER"].ToString() + "')\"><i class='fa fa-fw fa-edit'></i></button>"
                    + "</td>";
                html += "<td class='text-left' >" + dbRow["DNI_PER"].ToString() + "</td>";
                html += "<td class='text-left' >" + dbRow["CARGO_PER"].ToString() + "</td>";
                html += "<td class='text-left' >" + dbRow["COND_PER"].ToString() + "</td>";
                html += "</tr>" + Environment.NewLine;

                //CargaDTAtenciones(DDLAnio.SelectedValue, DDLMes.SelectedValue, dbRow["DNI"].ToString(), dbRow["Usuario"].ToString(), codtmp_modal);
            }

            html += "</table><hr style='border-top: 1px solid blue'>";
        }
        else
        {
            html += "<table>";
            html += "<tr><td class='FieldCaption' colspan=3>Sin registros encontrados</td></tr>";
            html += "</table><hr>";
        }
        LitTABL1.Text = html;
        //LitAtenciones.Text = htmlAtenciones;
        //LitDetAtenciones.Text = htmlDetAtenciones;
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
        response.Write(LitTABL1.Text);
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