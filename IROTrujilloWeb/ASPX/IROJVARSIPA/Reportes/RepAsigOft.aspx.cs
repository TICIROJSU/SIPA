using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

public partial class ASPX_IROJVARSIPA_Reportes_RepAsigOft : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;

        if (!Page.IsPostBack)
        {
            int vmonth = DateTime.Now.Month;
            int vyear = DateTime.Now.Year;
            DDLMes.SelectedIndex = vmonth;
            DDLAnio.Text = vyear.ToString();
        }

        //ListarPersonal();

    }


    //public void ListarPersonal()
    //{
    //    try
    //    {
    //        GVtable.DataSource = dtDato;
    //        GVtable.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        LitTABL1.Text = ex.Message.ToString();
    //    }
    //}


    public void CargaTabla()
    {
        string vAnio = DDLAnio.SelectedValue;
        string vMes = DDLMes.SelectedValue;
        int cantDays = DateTime.DaysInMonth(Convert.ToInt32(vAnio), Convert.ToInt32(vMes));

        DataTable dtDato = GetAsignacMesAnio(vAnio, vMes);

        string html = "";
        if (dtDato.Rows.Count > 0)
        {
            html += Environment.NewLine + "<table class='table table-hover' id='tbldsc' style='text-align: right; font-size: 14px; ' border='1'>";
            html += "<caption>ASIGNACIONES OFTALMOLOGIA " + vAnio + "</caption>";
            html += "<tr>";
            html += "<th class=''>Servicio</th>";
            html += "<th class=''>Turno</th>";
            for (int i = 1; i <= cantDays; i++)
            {
                DateTime vFechaTrab = Convert.ToDateTime(i + "-" + vMes + "-" + vAnio);
                int diaSemNro = (int)vFechaTrab.DayOfWeek;
                html += "<th class=''>" + ClassGlobal.DiaSemEsp(diaSemNro) + "<br />" + i + "</th>";
            }
            html += "</tr>" + Environment.NewLine;
            int nroitem = 0;
            foreach (DataRow dbRow in dtDato.Rows)
            {
                nroitem += 1;
                string vSer = dbRow["DES_SER"].ToString();
                string vcSer = dbRow["COD_SER"].ToString();
                string vTur = dbRow["Tur_Ser"].ToString();

                html += "<tr >";
                html += "<td class='text-left' >" + vSer + "</td>";
                html += "<td class='text-left' >" + vTur + "</td>";

                for (int i = 1; i <= cantDays; i++)
                {
                    //DateTime vFechaTrab = Convert.ToDateTime(i + "-" + vMes + "-" + vAnio);
                    string vPers = GetPerSerDiaTur(vAnio, vMes, i.ToString(), vcSer, vTur);
                    html += "<td class='text-left' >" + vPers + "</td>";
                }

                html += "</tr>" + Environment.NewLine;


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

    //private static
    public string GetPerSerDiaTur(string vAnio, string vMes, string vDia, string vSer, string vTur)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        DataTable dt = new DataTable();
        string resp = "";
        try
        {
            string qSql = "select LEFT(Per.NOM_PER, 1) + '. ' + LEFT(Per.APP_PER, 12) as NomAbr " +
                "from TabLibres.dbo.ProgPer PPer left join NUEVA.dbo.PERSONAL Per on PPer.Cod_Per COLLATE Modern_Spanish_CI_AS = Per.COD_PER COLLATE Modern_Spanish_CI_AS " +
                "where year(PPFechaCupos) = '" + vAnio + "' and month(PPFechaCupos) = '" + vMes + "' and PPEstado = 'Activo' and PPTipTrabajo in ('TP', 'TR') " +
                "and PPer.COD_SER = '" + vSer + "' and day(PPFechaCupos) = '" + vDia + "' and Tur_Ser = '" + vTur + "' ";
            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            dt = objdataset.Tables[0];
            foreach (DataRow dbRow in dt.Rows)
            {
                resp = resp + dbRow["NomAbr"].ToString() + "<br>";
            }
        }
        catch (Exception ex)
        {
            LitErrores.Text += "-" + "-" + ex.Message.ToString();
        }

        return resp;
    }

    public DataTable GetAsignacMesAnio(string vAnio, string vMes)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        DataTable dt = new DataTable();

        try
        {
            string qSql = "select distinct PPer.COD_SER, SER.DES_SER, PPer.Tur_Ser " +
                "from TabLibres.dbo.ProgPer PPer left join NUEVA.dbo.SERVICIO SER on PPer.COD_SER COLLATE Modern_Spanish_CI_AS = SER.COD_SER COLLATE Modern_Spanish_CI_AS " +
                "where year(PPFechaCupos) = '" + vAnio + "' and month(PPFechaCupos) = '" + vMes + "' and PPEstado = 'Activo' and PPTipTrabajo in ('TP', 'TR') " +
                "order by SER.DES_SER, PPer.Tur_Ser ";
            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            dt = objdataset.Tables[0];
        }
        catch (Exception ex)
        {
            LitErrores.Text += "-" + "-" + ex.Message.ToString();
        }

        return dt;

    }

    protected void bntBuscar_Click(object sender, EventArgs e)
    {
        CargaTabla();
        //ListarPersonal();
    }

}