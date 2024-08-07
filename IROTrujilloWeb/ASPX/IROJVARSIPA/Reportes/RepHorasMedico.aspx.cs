using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

public partial class ASPX_IROJVARSIPA_Reportes_RepHorasMedico : System.Web.UI.Page
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
            string qSql = "select APE_PER as [Apellidos y Nombres], SUM(ContCEX) as CEX_Prog, SUM(ContCEXCheq) as CEX_Cheq from " +
                "( " +
                "	select PPFechaCupos, PPER.COD_SER, PPER.Cod_Per, Tur_Ser, PPEstado, PPTipTrabajo, PPCantTurno, " +
                "	4 as ContCEX, isnull(Cheq.ContCheqAsis, 0) as ContCheqAsis, (case when Cheq.ContCheqAsis >= 11 then 4 else 3 end) as ContCEXCheq " +
                "	, Serv.DES_SER, Serv.DSC_SER, Serv.PIS_SER, Serv.HIS_SER " +
                "	, Pers.APE_PER, Pers.SEX_PER, Pers.COD_PRO, Pers.USU_PER, Pers.HIS_PER " +
                "	, Cheq.FechaAtencion, Cheq.COD_SERVSA1, Cheq.PLAZA, Cheq.MT " +
                "	from TabLibres.dbo.ProgPer PPER " +
                "	left join NUEVA.dbo.SERVICIO Serv on PPER.COD_SER COLLATE Modern_Spanish_CI_AS = Serv.COD_SER COLLATE Modern_Spanish_CI_AS " +
                "	left join NUEVA.dbo.PERSONAL Pers on PPER.Cod_Per COLLATE Modern_Spanish_CI_AS = Pers.COD_PER COLLATE Modern_Spanish_CI_AS " +
                "	full outer join " +
                "	( " +
                "		select SUM(CheqCitAsist) ContCheqAsis, FechaAtencion, COD_SERVSA1, PLAZA, MT " +
                "		from " +
                "		( " +
                "			SELECT 1 as CheqCitAsist, CAST( cast(DIA as varchar(2)) + '/' + FORMAT(MES, '00') + '/' + cast(ANO as varchar(4)) as date) as FechaAtencion, * " +
                "			FROM NUEVA.dbo.CHEQ2011 " +
                "			WHERE ANO = @Anio AND MES = @Mes " +
                "		) Cheq " +
                "		group by FechaAtencion, COD_SERVSA1, PLAZA, MT 	" +
                "	) Cheq on PPER.PPFechaCupos = Cheq.FechaAtencion " +
                "		and PPER.COD_SER COLLATE Modern_Spanish_CI_AS = Cheq.COD_SERVSA1 COLLATE Modern_Spanish_CI_AS " +
                "		and Pers.HIS_PER  COLLATE Modern_Spanish_CI_AS = Cheq.PLAZA  COLLATE Modern_Spanish_CI_AS " +
                "		and PPER.Tur_Ser COLLATE Modern_Spanish_CI_AS = Cheq.MT COLLATE Modern_Spanish_CI_AS " +
                "	where year(PPFechaCupos) = @Anio and month(PPFechaCupos) = @Mes " +
                "	and PPER.COD_SER in ('04', '20', '21', '8C', '14', '39', '82', '50', '92', '99', '16', '24', '62', '25', '26', '71', '73', '28', '80', '81', '29') " +
                ") ProgPer " +
                "group by APE_PER ";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@Anio", DDLAnio.SelectedValue);
            cmd.Parameters.AddWithValue("@Mes", DDLMes.SelectedValue);

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
        ListarPersonal();
    }

}