using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

public partial class ASPX_ExtLogin_Registro_ConsultaCitaPac : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        if (!Page.IsPostBack)
        {
            int vmonth = DateTime.Now.Month;
        }
    }

    protected void bntBuscar_Click(object sender, EventArgs e)
    {
        CargaTablaDT();
    }


    public void CargaTablaDT()
    {
        string vDNI = txtDNI.Text;
        string vCampoDato = "CIT_TAR";
        if (vDNI.Length == 6) { vCampoDato = "HIC_TAR"; }
        if (vDNI.Length == 8) { vCampoDato = "IDNI"; }

        string gHTML = "";
        try
        {
            //con.Open();
            string qSql = "select ID_CITA, FORMAT(day(FEC_TAR), '00') + '/' + format(month(FEC_TAR), '00') + '/' + cast(year(FEC_TAR) as varchar) Fecha, HOR_TAR Hora, " +
                "ihc [HCli], idni DNI, iap + ' ' + ias + ', ' + ino Nombres, des_ser Servicio, APE_PER Medico, Des_Pg [TipoPac] " +
                "from NUEVA.dbo.TARJETON " +
                "inner join NUEVA.dbo.HIStoria on ihc=HIC_TAR " +
                "inner join NUEVA.dbo.SERVICIO on COD_SER=SER_TAR " +
                "inner join NUEVA.dbo.PERSONAL on COD_PER=COM_TAR " +
                "left join NUEVA.dbo.PROGRAMA on PRG_TAR=Cod_Pg " +
                //"where FEC_TAR<=cast(GETDATE() as date) and CIT_TAR='x' " +
                "where CIT_TAR='x' " +
                "and " + vCampoDato + " like '%' + @dni + '%' " +
                "order by FEC_TAR desc;";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@dni", vDNI);

            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            //GVtable.DataSource = objdataset.Tables[0];
            //GVtable.DataBind();
            //CargaTabla(dtDato);
            DataTable dtDatoDetAt = objdataset.Tables[0];

            if (dtDatoDetAt.Rows.Count > 0)
            {
                gHTML += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gHTML += "<table id='tblbscrJS' class='table table-bordered table-hover' style='text-align: left; font-size: 14px; '>";
                gHTML += "<tr><th class=''>Cita </th>";
                gHTML += "<th class=''>Fecha</th>";
                gHTML += "<th class=''>Hora</th>";
                gHTML += "<th class=''>H. Clinica</th>";
                gHTML += "<th class=''>DNI</th>";
                gHTML += "<th class=''>Nombres</th>";
                gHTML += "<th class=''>Servicio</th>";
                gHTML += "<th class=''>Medico</th>";
                gHTML += "<th class=''>Tipo Pac.</th>";
                gHTML += "</tr>" + Environment.NewLine;
                int nroitem = 0;

                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    string vCita, vFech, vHora, vHCli, viDNI, vNomb, vServ, vMedi, vTipP;
                    vCita = dbRow["ID_CITA"].ToString();
                    vFech = dbRow["Fecha"].ToString();
                    vHora = dbRow["Hora"].ToString();
                    vHCli = dbRow["HCli"].ToString();
                    viDNI = dbRow["DNI"].ToString();
                    vNomb = dbRow["Nombres"].ToString();
                    vServ = dbRow["Servicio"].ToString();
                    vMedi = dbRow["Medico"].ToString();
                    vTipP = dbRow["TipoPac"].ToString();

                    gHTML += "<tr>";
                    gHTML += "<td class='' ><a href='../../ExtIROJVAR/Citas/ConsultaCitaPacRep.aspx?iCita=" + vCita + "' target='_blank' class='btn bg-navy'><i class='fa fa-fw fa-eye'></i></a></td>";
                    gHTML += "<td class='' >" + vFech + "</td>";
                    gHTML += "<td class='' >" + vHora + "</td>";
                    gHTML += "<td class='' >" + vHCli + "</td>";
                    gHTML += "<td class='' >" + viDNI + "</td>";
                    gHTML += "<td class='' >" + vNomb + "</td>";
                    gHTML += "<td class='' >" + vServ + "</td>";
                    gHTML += "<td class='' >" + vMedi + "</td>";
                    gHTML += "<td class='' >" + vTipP + "</td>";
                    gHTML += "</tr>";
                    gHTML += Environment.NewLine;

                }

                gHTML += "</table></div></div><hr style='border-top: 1px solid blue'>";
            }

        }
        catch (Exception ex)
        {
            //Label1.Text = "Error";
            gHTML += ex.Message.ToString();
            LitTABL1.Text = ex.Message.ToString();
        }

        LitTABL1.Text = gHTML;

    }

}
