using System;
using System.Data;
using System.Data.SqlClient;

public partial class ASPX_ExtIROJVAR_Citas_ConsultaCitaPacRep : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        string idPruebaCovid = Request.QueryString["iCita"];
        CargaInicial(idPruebaCovid);
    }

    public void CargaInicial(string idcov)
    {
        try
        {
            //con.Open();
            string qSql = "select ID_CITA, Des_Pg TipCita, Day(FEC_TAR) DiaCita, Month(FEC_TAR) MesCita, Year(FEC_TAR) AnioCita, " +
                "UPPER(FORMAT(FEC_TAR, 'dddd dd MMM yyyy')) FecCita,  HOR_TAR HorCita, DSC_SER SerCita, PIS_SER PisCita, HIC_TAR HClCita, " +
                "IAP + ' ' + IAM + ' ' + INO PacCita, NUR_TAR OpeCita, FRG_TAR RegCita, Des_Usu UsuCita " +
                "from NUEVA.dbo.TARJETON " +
                "inner join NUEVA.dbo.HIStoria on ihc=HIC_TAR " +
                "inner join NUEVA.dbo.SERVICIO on COD_SER=SER_TAR " +
                "inner join NUEVA.dbo.PERSONAL on COD_PER=COM_TAR " +
                "left join NUEVA.dbo.PROGRAMA on PRG_TAR=Cod_Pg " +
                "left join NUEVA.dbo.USUARIO on USU_TAR = Cod_Usu " +
                "where ID_CITA = '" + idcov + "' " +
                "order by FEC_TAR desc";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00); cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter(); adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet(); adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];
            DataRow dbRow = dtDato.Rows[0];

            //dPerDNI.InnerHtml = "<b>" + dbRow["TipCita"].ToString() + "&nbsp;</b>";
            //dPerApeP.InnerHtml = "<b>" + dbRow["AnioCita"].ToString() + "&nbsp;</b>";

            lblTipo.Text = dbRow["TipCita"].ToString();
            //tdFecCita.InnerText = dbRow["DiaCita"].ToString() + '/' + dbRow["MesCita"].ToString() + '/' + dbRow["AnioCita"].ToString();
            tdFecCita.InnerText = dbRow["FecCita"].ToString();
            tdHorCita.InnerText = dbRow["HorCita"].ToString();
            tdSerCita.InnerText = dbRow["SerCita"].ToString();
            tdPisCita.InnerText = dbRow["PisCita"].ToString() + "° Piso";
            tdHClCita.InnerText = dbRow["HClCita"].ToString();
            tdPacCita.InnerText = dbRow["PacCita"].ToString();
            tdOpeCita.InnerText = dbRow["OpeCita"].ToString();
            tdRegCita.InnerText = dbRow["RegCita"].ToString();
            tdUsuCita.InnerText = dbRow["UsuCita"].ToString();


        }
        catch (Exception ex)
        {
            LitError.Text = ex.Message.ToString();
        }
    }

}