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

public partial class ASPX_IROJVAR_Cirugias_CProgramadas : System.Web.UI.Page
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
    public static string GetDetEspecialidadxMes(string vanio, string vmes, string vmesnom)
    {
        DateTimeFormatInfo formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetHTML = "";
        try
        {
            string qSql = "select LEFT(Operacion_Programada, 6) as OpProgramada, MAX(Operacion_Programada) Especialidad, count(Id_Programacion) as Cantidad " +
                "from Estadistica.dbo.Programacion " +
                "where Ano = '" + vanio + "' and Mes = '" + vmes + "' " +
                "Group by LEFT(Operacion_Programada, 6) " +
                "order by LEFT(Operacion_Programada, 6)";
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
                gDetHTML += "<caption>" + vanio + " | " + formatoFecha.GetMonthName(Convert.ToInt32(vmes)) + "</caption>";
                gDetHTML += "<tr>";
                gDetHTML += "<th class=''>Cirugia </th>";
                gDetHTML += "<th class=''>Cantidad</th>";
                gDetHTML += "</tr>" + Environment.NewLine;
                gDetHTML += "<tr>";
                double vt_total = Convert.ToDouble(dtDato.Compute("sum(Cantidad)", String.Empty));
                gDetHTML += "<td class='text-center'><b>Todos</b></td>";
                gDetHTML += "<td class='text-center'><b>" + vt_total + "</b></td>";
                gDetHTML += "</tr>" + Environment.NewLine;
                foreach (DataRow dbRow in dtDato.Rows)
                {
                    double v_Cant = Convert.ToDouble(dbRow["Cantidad"]);
                    string v_SIS = ClassGlobal.formatoMillarDec((v_Cant / vt_total * 100).ToString());
                    gDetHTML += "<tr onclick=\"fPacEspeMes('" + vanio + "', '" + vmes + "', '" + vmesnom + "', '" + dbRow["Especialidad"].ToString() + "', this)\">";
                    gDetHTML += "<td class='' style='text-align: left;'>" + dbRow["Especialidad"].ToString() + "</td>";
                    gDetHTML += "<td class='' style='text-align: center;'>" + dbRow["Cantidad"].ToString() + "</td>";
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
    public static string GetDetPacientexEspexMes(string vanio, string vmes, string vmesnom, string vespec)
    {
        DateTimeFormatInfo formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetHTML = "";
        try
        {
            string qSql = "select Ape_Paterno Ap_Paterno, Ape_Materno Ap_Materno, Nombres, HHCC, Operacion_Programada Op_Realizada, Ojo DxOjo, Cirujano, Dia, FORMAT(day(Fecha_Programada), '00') as DiaFormat " +
                "from Estadistica.dbo.Programacion " +
                "where Ano = '" + vanio + "' and Mes = '" + vmes + "' " +
                "and LEFT(Operacion_Programada, 6) = LEFT('" + vespec + "', 6) " +
                "order by cast(Dia as int), Ape_Paterno";
            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            DataTable dtDato = objdataset.Tables[0];
            DataTable dtDia = dtDato.AsEnumerable().GroupBy(r => r.Field<string>("Dia")).Select(g => g.First()).CopyToDataTable();
            DataTable dtCirujano = dtDato.AsEnumerable().GroupBy(r => r.Field<string>("Cirujano")).Select(g => g.First()).CopyToDataTable();

            if (dtDato.Rows.Count > 0)
            {
                gDetHTML += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetHTML += "<table class='table table-hover' style='text-align: right; font-size: 14px; ' id='tblPaciente'>";
                gDetHTML += "<caption>" + vanio + " | " + formatoFecha.GetMonthName(Convert.ToInt32(vmes)) + " | " + vespec + "</caption>";
                gDetHTML += "<tr>";
                gDetHTML += "   <th></th>";
                gDetHTML += "   <th>Dia</th>";
                gDetHTML += "   <th>Paciente </th>";
                gDetHTML += "   <th>HC</th>";
                gDetHTML += "   <th>Cirugia</th>";
                gDetHTML += "   <th>Ojo</th>";
                gDetHTML += "   <th>Medico</th>";
                gDetHTML += "</tr>" + Environment.NewLine;
                gDetHTML += "<tr>";
                int nroitem = 0;
                foreach (DataRow dbRow in dtDato.Rows)
                {
                    nroitem += 1;
                    gDetHTML += "<tr onclick=\"fPacEspeMesSelFila(this)\">";
                    gDetHTML += "<td style='text-align: center;'>" + nroitem + "</td>";
                    gDetHTML += "<td style='text-align: center;'>" + dbRow["DiaFormat"].ToString() + "</td>";
                    gDetHTML += "<td style='text-align: left;'>" + dbRow["Ap_Paterno"].ToString() + " " + dbRow["Ap_Materno"].ToString() + ", " + dbRow["Nombres"].ToString() + "</td>";
                    gDetHTML += "<td style='text-align: center;'>" + dbRow["HHCC"].ToString() + "</td>";
                    gDetHTML += "<td style='text-align: left;'>" + dbRow["Op_Realizada"].ToString() + "</td>";
                    gDetHTML += "<td style='text-align: left;'>" + dbRow["DxOjo"].ToString() + "</td>";
                    gDetHTML += "<td style='text-align: left;'>" + dbRow["Cirujano"].ToString() + "</td>";
                    gDetHTML += "</tr>" + Environment.NewLine;
                }
                gDetHTML += "</table></div></div><hr style='border-top: 1px solid blue'>";
                gDetHTML += "||sep||";
                gDetHTML += "<option value=''></option>";
                foreach (DataRow dbRowDia in dtDia.Rows)
                {
                    gDetHTML += "<option value='" + dbRowDia["DiaFormat"].ToString() + "'>" + dbRowDia["DiaFormat"].ToString() + "</option>";
                }
                gDetHTML += "||sep||";
                gDetHTML += "<option value=''></option>";
                foreach (DataRow dbRowCir in dtCirujano.Rows)
                {
                    gDetHTML += "<option value='" + dbRowCir["Cirujano"].ToString() + "'>" + dbRowCir["Cirujano"].ToString() + "</option>";
                }

            }
            //gDetAtenciones - FINAL
        }
        catch (Exception ex)
        {
            gDetHTML += "-" + "-" + ex.Message.ToString();
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
            string qSql = "select Mes, NUEVA.dbo.fnMesTexto(Mes) as MesNom, count(Id_Programacion) as Cantidad " +
                "from Estadistica.dbo.Programacion where Ano = '" + vAnio + "' " +
                "Group by Mes " +
                "order by cast(Mes as int) ";

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
                html += "<th class='text-center'>Total</th>";
                html += "</tr>" + Environment.NewLine;
                html += "<tr>";
                double vt_total = Convert.ToDouble(dtDato.Compute("sum(Cantidad)", String.Empty));
                html += "<td class='text-right'><b>Todos</b></td>";
                html += "<td class='text-center'><b>" + vt_total + "</b></td>";
                html += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                foreach (DataRow dbRow in dtDato.Rows)
                {
                    nroitem += 1;
                    double v_Cantidad = Convert.ToDouble(dbRow["Cantidad"]);
                    string v_MesTotal = ClassGlobal.formatoMillarDec((v_Cantidad / vt_total * 100).ToString());
                    html += "<tr onclick=\"fEspeMes('" + vAnio + "', '" + dbRow["Mes"].ToString() + "', '" + dbRow["MesNom"].ToString() + "', this)\">";
                    html += "<td class='' style='text-align: left;'>" + dbRow["MesNom"].ToString() + "</td>";
                    html += "<td class='' style='text-align: right;'>" + dbRow["Cantidad"].ToString() + " ( " + v_MesTotal + "% ) " + "</td>";
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
