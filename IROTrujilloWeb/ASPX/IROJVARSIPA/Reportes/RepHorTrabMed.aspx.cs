using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

public partial class ASPX_IROJVARSIPA_Reportes_RepHorTrabMed : System.Web.UI.Page
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
            html += "<caption>HORARIO DE TRABAJO DE MEDICOS " + vAnio + "</caption>";
            html += "<tr>";
            html += "<th class=''>N°</th>";
            html += "<th class=''>Apellidos y Nombres</th>";
            for (int i = 1; i <= cantDays; i++)
            {
                DateTime vFechaTrab = Convert.ToDateTime(i + "-" + vMes + "-" + vAnio);
                int diaSemNro = (int)vFechaTrab.DayOfWeek;
                html += "<th class=''>" + ClassGlobal.DiaSemEsp(diaSemNro) + "<br />" + i + "</th>";
            }
            html += "<th> </th>";
            html += "<th>M</th><th>T</th>";
            html += "<th>MT</th><th>TD</th>";
            html += "<th>Onom</th><th>Feri</th>";
            html += "<th>Vac</th>";
            html += "<th>Total</th>";
            html += "</tr>" + Environment.NewLine;
            int nroitem = 0;
            foreach (DataRow dbRow in dtDato.Rows)
            {
                nroitem += 1;
                string vApePer = dbRow["APE_PER"].ToString();
                string vCodPer = dbRow["Cod_Per"].ToString();
                int vM = 0, vT = 0, vMT = 0, vTD = 0, vOnom = 0, vFeri = 0, vVac = 0;

                html += "<tr >";
                html += "<td class='text-left' >" + nroitem + "</td>";
                html += "<td class='text-left' >" + vApePer + "</td>";

                for (int i = 1; i <= cantDays; i++)
                {
                    //DateTime vFechaTrab = Convert.ToDateTime(i + "-" + vMes + "-" + vAnio);
                    string vPers = GetPerSerDiaTur(vAnio, vMes, i.ToString(), vCodPer, "");
                    html += "<td class='text-left' >" + vPers + "</td>";
                    switch (vPers)
                    {
                        case "M":
                            vM++; break;
                        case "T":
                            vT++; break;
                        case "MT":
                            vMT++; break;
                        case "TD":
                            vTD++; break;
                        case "Ono":
                            vOnom++; break;
                        case "F":
                            vFeri++; break;
                        case "V":
                            vVac++; break;
                        default:
                            break;
                    }
                }

                html += "<td> </td>";
                html += "<td>" + vM + "</td><td>" + vT + "</td>";
                html += "<td>" + vMT + "</td><td>" + vTD + "</td>";
                html += "<td>" + vOnom + "</td><td>" + vFeri + "</td>";
                html += "<td>" + vVac + "</td>";
                html += "<td>" + (vM + vT + vMT + vTD + vOnom + vFeri + vVac).ToString() + "</td>";

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
    public string GetPerSerDiaTur(string vAnio, string vMes, string vDia, string vcPer, string vTur)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        DataTable dt = new DataTable();
        string resp = "";
        try
        {
            string qSql = "select Cod_Per, PPFechaCupos, " +
                "Max(case when TurTipoTrab = 'M' then 'M' else (case when TurTipoTrab <> 'T' then TurTipoTrab else '' end) end) TurM, " +
                "Max(case when TurTipoTrab = 'T' then 'T' else '' end) TurT " +
                "from " +
                "( " +
                "   select (Case when PPTipTrabajo = 'TP' then Tur_Ser else PPTipTrabajo end) TurTipoTrab, PPer.Cod_Per, PPFechaCupos " +
                "   from TabLibres.dbo.ProgPer PPer " +
                "   left join NUEVA.dbo.PERSONAL Per on PPer.Cod_Per COLLATE Modern_Spanish_CI_AS = Per.COD_PER COLLATE Modern_Spanish_CI_AS " +
                "   where year(PPFechaCupos) = '" + vAnio + "' and month(PPFechaCupos) = '" + vMes + "' and day(PPFechaCupos) = '" + vDia + "' " +
                "   and PPEstado = 'Activo' and Pper.Cod_Per = '" + vcPer + "' " +
                ") PPerTur " +
                "Group by Cod_Per, PPFechaCupos";
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
                resp = resp + dbRow["TurM"].ToString() + dbRow["TurT"].ToString();
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
            string qSql = "select Distinct PPer.Cod_Per, Per.COD_PER as CodPerPer, Per.APE_PER, Per.SEX_PER, Per.COD_PRO, Prof.DES_PRO " +
                "from TabLibres.dbo.ProgPer PPer " +
                "left join NUEVA.dbo.PERSONAL Per on PPer.Cod_Per COLLATE Modern_Spanish_CI_AS = Per.COD_PER COLLATE Modern_Spanish_CI_AS " +
                "left join NUEVA.dbo.PROFESION Prof on Per.COD_PRO = Prof.COD_PRO " +
                "where Year(PPFechaCupos) = '" + vAnio + "' and Month(PPFechaCupos) = '" + vMes + "' ";
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
