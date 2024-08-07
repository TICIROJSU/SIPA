using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

public partial class ASPX_IROJVARSIPA_Estadistica_ProgHoraMed : System.Web.UI.Page
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

        ListarPersonal();

    }


    public void ListarPersonal()
    {
        try
        {
            string qSql = "select per.*, pro.DES_PRO " +
                "from NUEVA.dbo.PERSONAL per left join NUEVA.dbo.PROFESION pro on per.COD_PRO = pro.COD_PRO " +
                "where pro.COD_PRO in ('01', '07') and per.EST_PER = '1' " +
                "order by DES_PRO, APE_PER ";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            //cmd.Parameters.AddWithValue("@Cod_Usu", codUsu);

            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];

            CargaTabla(dtDato);

        }
        catch (Exception ex)
        {
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
            html += "<th class=''>ID</th>";
            html += "<th class=''>Personal</th>";
            html += "<th class=''>Ver</th>";
            html += "<th class=''>HisPer</th>";
            html += "<th class=''>Cargo</th>";
            html += "</tr>" + Environment.NewLine;
            int nroitem = 0;
            foreach (DataRow dbRow in dtDato.Rows)
            {
                nroitem += 1;

                html += "<tr >";
                html += "<td class='text-left' >" + dbRow["COD_PER"].ToString() + "</td>";
                html += "<td class='text-left' >" + dbRow["APE_PER"].ToString() + "</td>";
                html += "<td class='text-left' >"
                    + "<a class='btn bg-navy' target='_blank' href='HorarioPer.aspx?DNI=" + dbRow["COD_PER"].ToString() + "' ><i class='fa fa-fw fa-eye'></i></a>"
                    + "</td>";
                html += "<td class='text-left' >" + dbRow["HIS_PER"].ToString() + "</td>";
                html += "<td class='text-left' >" + dbRow["DES_PRO"].ToString() + "</td>";
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

    protected void bntBuscar_Click(object sender, EventArgs e)
    {
        //CargaTablaDT();
    }

}
