using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

public partial class ASPX_IROJVARSIPA_Reportes_RepHorasRango : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;

        if (!Page.IsPostBack)
        {
            int vmonth = DateTime.Now.Month;
            string vd, vm, vy;
            vd = DateTime.Now.Day.ToString();
            vm = DateTime.Now.Month.ToString();
            vy = DateTime.Now.Year.ToString();

            txtfIni.Text = vy + "-" + (vm).PadLeft(2, '0') + "-" + vd.PadLeft(2, '0');
            txtfFin.Text = txtfIni.Text;

        }

        //ListarPersonal();

    }


    public void ListarPersonal()
    {

        try
        {
            string qSql = "exec [NUEVA].dbo.SP_JVAR_ProgTarjRangos @fINI, @fFIN, @Seg ";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@fINI", txtfIni.Text);
            cmd.Parameters.AddWithValue("@fFIN", txtfFin.Text);
            cmd.Parameters.AddWithValue("@Seg", DDLSeguro.SelectedValue);

            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];

            GVtable.DataSource = dtDato;
            GVtable.DataBind();
            //CargaTabla(dtDato);

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
            html += "<th class=''>Servicio</th>";
            html += "<th>00:00</th><th>07:00</th><th>07:30</th><th>08:00</th><th>08:30</th>";
            html += "<th>09:00</th><th>09:30</th><th>10:00</th><th>10:30</th><th>11:00</th>";
            html += "<th>11:30</th><th>12:00</th><th>12:30</th><th>13:00</th><th>13:30</th>";
            html += "<th>14:00</th><th>14:30</th><th>15:00</th><th>15:30</th><th>16:00</th>";
            html += "<th>16:30</th>";
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
        ListarPersonal();
    }

}