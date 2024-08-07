using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ASPX_IROJVAR_UPE_AtOtrosS : System.Web.UI.Page
{
    string html = "";
    string htmlAtenciones = "";
    string htmlDetAtenciones = "";
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        if (!Page.IsPostBack)
        {
            //int vmonth = DateTime.Now.Month;
            //DDLMes.SelectedIndex = vmonth;
            int vyear = DateTime.Now.Year;
            DDLAnio.Text = vyear.ToString();
            int vmonth = DateTime.Now.Month;
            //DDLMes.SelectedIndex = vmonth;
            CargaTablaDTIni();
        }

    }

    [WebMethod]
    public static string GetDetEspecialidadxMes(string vanio, string vmes, string vmesnom, string vPrg)
    {
        DateTimeFormatInfo formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetHTML = "";
        try
        {
            string qSql = "exec NUEVA.dbo.SP_JVAR_UPE_Atenciones 'OtrosxProg', '" + vanio + "', '" + vmes + "', '" + vPrg + "' ";
            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            DataTable dtDato = objdataset.Tables[0];

            if (dtDato.Rows.Count > 0)
            {
                gDetHTML += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetHTML += "<table class='table table-hover' style='text-align: right; font-size: 14px; ' id='tblEspecialid'>";
                gDetHTML += "<caption>" + vanio + " | " + formatoFecha.GetMonthName(Convert.ToInt32(vmes)) + " | " + dtDato.Rows[0]["Des_Pg"].ToString() + "</caption>";
                gDetHTML += "<tr>";
                gDetHTML += "<th class=''>HCli </th>";
                gDetHTML += "<th class=''>Paciente</th>";
                gDetHTML += "<th class=''>DNI</th>";
                gDetHTML += "<th class=''>Servicio</th>";
                gDetHTML += "<th class=''>Fecha</th>";
                gDetHTML += "</tr>" + Environment.NewLine;
                foreach (DataRow dbRow in dtDato.Rows)
                {
                    gDetHTML += "<tr onclick=\"fPacEspeMes('" + vanio + "', '" + vmes + "', '" + vmesnom + "', '" + dbRow["HCli"].ToString() + "', this, '" + dbRow["DNI"].ToString() + "')\">";
                    gDetHTML += "<td class='' style='text-align: left;'>" + dbRow["HCli"].ToString() + "</td>";
                    gDetHTML += "<td class='' style='text-align: left;'>" + dbRow["Paciente"].ToString() + "</td>";
                    gDetHTML += "<td class='' style='text-align: left;'>" + dbRow["DNI"].ToString() + "</td>";
                    gDetHTML += "<td class='' style='text-align: left;'>" + dbRow["Servicio"].ToString() + "</td>";
                    gDetHTML += "<td class='' style='text-align: left;'>" + dbRow["FechaAt"].ToString() + "</td>";
                    gDetHTML += "</tr>" + Environment.NewLine;
                }

                gDetHTML += "</table></div></div><hr style='border-top: 1px solid blue'>";

            }
            //gDetAtenciones - FINAL
        }
        catch (Exception ex)
        {
            //LitErrores.Text += vplaza + "-" + "-" + ex.Message.ToString();
        }
        return gDetHTML;
    }

    [WebMethod]
    public static string GetDetPacientexEspexMes(string vanio, string vmes, string vHCli, string vDNI)
    {
        DateTimeFormatInfo formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetHTML = "";
        try
        {
            string qSql = "exec NUEVA.dbo.SP_JVAR_AtenSeguimiento 'UPESeguimientoOtros', '" + vanio + "', '" + vmes + "', '" + vHCli + "', '" + vDNI + "'";
            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            DataTable dtDato = objdataset.Tables[0];

            if (dtDato.Rows.Count > 0)
            {
                gDetHTML += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetHTML += "<table class='table table-hover' style='text-align: right; font-size: 14px; ' id='tblPaciente'>";
                gDetHTML += "<caption>" + vanio + " | " + formatoFecha.GetMonthName(Convert.ToInt32(vmes)) + " | </caption>";
                gDetHTML += "<tr>";
                gDetHTML += "   <th>HCli</th>";
                gDetHTML += "   <th>DNI</th>";
                gDetHTML += "   <th>Ocupacion</th>";
                gDetHTML += "   <th>Paciente</th>";
                gDetHTML += "   <th>_</th>";
                gDetHTML += "</tr>" + Environment.NewLine;
                gDetHTML += "<tr>";
                int nroitem = 0;
                foreach (DataRow dbRow in dtDato.Rows)
                {
                    nroitem += 1;
                    if (dbRow["HCli"].ToString() == "----")
                    {
                        gDetHTML += "<tr style='background-color: #74992e;'>";
                    }
                    else
                    {
                        gDetHTML += "<tr onclick=\"fPacEspeMesSelFila(this)\">";
                    }
                    
                    gDetHTML += "<td style='text-align: left;'>" + dbRow["HCli"].ToString() + "</td>";
                    gDetHTML += "<td style='text-align: center;'>" + dbRow["DNI"].ToString() + "</td>";
                    gDetHTML += "<td style='text-align: left;'>" + dbRow["Ocupacion"].ToString() + "</td>";
                    gDetHTML += "<td style='text-align: left;'>" + dbRow["Paciente"].ToString() + "</td>";
                    gDetHTML += "<td style='text-align: left;'>" + dbRow["_"].ToString() + "</td>";
                    gDetHTML += "</tr>" + Environment.NewLine;
                }
                gDetHTML += "</table></div></div><hr style='border-top: 1px solid blue'>";
                gDetHTML += "||sep||";
                gDetHTML += "<option value=''></option>";

            }
            //gDetAtenciones - FINAL
        }
        catch (Exception ex)
        {
            //LitErrores.Text += vplaza + "-" + "-" + ex.Message.ToString();
        }
        return gDetHTML;
    }

    protected void bntBuscar_Click(object sender, EventArgs e)
    {
        CargaTablaDTIni();
    }

    public void CargaTablaDTIni()
    {
        string vAnio = DDLAnio.SelectedValue;
        //string vServ = ddlServicio.SelectedValue;
        try
        {
            //con.Open();
            string qSql = "exec NUEVA.dbo.SP_JVAR_UPE_Atenciones 'OtrosxAnio', '" + vAnio + "', '', '' ";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];

            //GVtable.DataSource = dtDato;
            //GVtable.DataBind();
            //CargaTabla(dtDato);

            html = "";
            if (dtDato.Rows.Count > 0)
            {
                html += Environment.NewLine + "<table class='table table-hover' style='text-align: right; font-size: 14px; ' id='tblAnioMes'>";
                //html += "<caption>" + DDLAnio.SelectedItem.Text + "</caption>";
                html += "<tr>";
                html += "<th class=''>Mes</th>";
                html += "<th class=''>Programa</th>";
                html += "<th class='text-center'>Total</th>";
                html += "</tr>" + Environment.NewLine;
                html += "<tr>";
                double vt_total = Convert.ToDouble(dtDato.Compute("sum(Cont)", String.Empty));
                html += "<td></td><td class='text-right'><b>Total</b></td>";
                html += "<td class='text-center'><b>" + vt_total + "</b></td>";
                html += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                foreach (DataRow dbRow in dtDato.Rows)
                {
                    nroitem += 1;
                    double v_Cantidad = Convert.ToDouble(dbRow["Cont"]);
                    string v_MesTotal = ClassGlobal.formatoMillarDec((v_Cantidad / vt_total * 100).ToString());
                    html += "<tr onclick=\"fEspeMes('" + vAnio + "', '" + dbRow["Mes"].ToString() + "', '" + dbRow["MesNom"].ToString() + "', this, '" + dbRow["Des_Pg"].ToString() + "')\">";
                    html += "<td class='' style='text-align: left;'>" + dbRow["MesNom"].ToString() + "</td>";
                    html += "<td class='' style='text-align: left;'>" + dbRow["Des_Pg"].ToString() + "</td>";
                    html += "<td class='' style='text-align: right;'>" + dbRow["Cont"].ToString() + "(" + v_MesTotal + "%) " + "</td>";
                    //html += "<td class='' style='text-align: left;'>" + "<button type='button' class='btn bg-navy' data-toggle='modal' data-target='#modalAtenciones'><i class='fa fa-fw fa-medkit'></i></button>" + "</td>";
                    html += "</tr>" + Environment.NewLine;
                }

                html += "</table><hr style='border-top: 1px solid blue'>";
            }
            else
            {
                html += "<table>";
                html += "<tr><td class='FieldCaption' colspan=3>Sin registros encontrados0</td></tr>";
                html += "</table><hr>";
            }
            LitTABL1.Text = html;

        }
        catch (Exception ex)
        {
            //Label1.Text = "Error";
            ex.Message.ToString();
            LitTABL1.Text = ex.Message.ToString();
        }
    }

    protected void ExportarExcel_Click(object sender, EventArgs e)
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

    protected void ExportarExcel2_Click(object sender, EventArgs e)
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

    protected void ddlServicio_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargaTablaDTIni();
    }
}
