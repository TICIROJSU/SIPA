using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ASPX_ExtIROJVAR_Citas_ConsultaCitaFecha : System.Web.UI.Page
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

            txtProd.Text = vy + "-" + (vm).PadLeft(2, '0') + "-" + vd.PadLeft(2, '0');
        }
    }

    protected void bntBuscar_Click(object sender, EventArgs e)
    {
        CargaTablaDT();
        string JavaScript = "contarFilTbl('tblbscrJS');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", JavaScript, true);
    }


    public void CargaTablaDT()
    {
        string vHC = txtProd.Text;

        string gHTML = "";
        try
        {
            //con.Open();
            string qSql = "select ID_CITA, " +
                "FORMAT(day(FEC_TAR), '00') + '/' + format(month(FEC_TAR), '00') + '/' + cast(year(FEC_TAR) as varchar) Fecha, " +
                "HOR_TAR Hora, ihc [HCli], idni DNI, iap + ' ' + ias + ', ' + ino Nombres, " +
                "des_ser Servicio, APE_PER Medico, Des_Pg [TipoPac], TUR_TAR as Turno, ProgrSOP.SOP " +
                "from NUEVA.dbo.TARJETON " +
                "inner join NUEVA.dbo.HIStoria on ihc=HIC_TAR " +
                "inner join NUEVA.dbo.SERVICIO on COD_SER=SER_TAR " +
                "inner join NUEVA.dbo.PERSONAL on COD_PER=COM_TAR " +
                "left join NUEVA.dbo.PROGRAMA on PRG_TAR=Cod_Pg " +
                "left join ( " +
                "   SELECT 'Cirugia' as SOP, [DNI], [HHCC], [Fecha_Programada], [Operacion_Programada], [Ojo], [Tipo_Paciente], [Cirujano] " +
                "   FROM [Estadistica].[dbo].[Programacion] " +
                "   WHERE CAST(Fecha_Programada AS DATE) = @hc " +
                ") ProgrSOP on HISTORIA.IDNI = ProgrSOP.DNI " +
                "where CIT_TAR='x' and cast(FEC_TAR as date) = @hc " +
                "order by tur_tar,hor_TAR ;";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@hc", vHC);

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
                gHTML += "<table id='tblbscrJS' class='table table-bordered table-hover dataTable' role='grid' style='text-align: left; font-size: 14px; '>";
                gHTML += "<tr>" +
                    "<th class=''>Cita </th>";
                gHTML += "<th class=''>Fecha</th>";
                gHTML += "<th class='' >Hora</th>";
                gHTML += "<th class=''>" +
                    "<div onclick=\"fMostrarTXT('txtBscHC')\">H. Clinica</div> <br /> " +
                    "<input type='text' id='txtBscHC' style='width: 90px; display:none' onkeyup=\"fBscTblHTML('txtBscHC', 'tblbscrJS', 3); \" />" +
                    "</th>";
                gHTML += "<th class=''>" +
                    "<div onclick='fMostrarTXT(\"txtBscDNI\")'>DNI</div> <br /> " +
                    "<input type='text' id='txtBscDNI' style='width: 90px; display:none' onkeyup=\"fBscTblHTML('txtBscDNI', 'tblbscrJS', 4); \" />" +
                    "</th>";
                gHTML += "<th class='' >Nombres</th>";
                gHTML += "<th class=''>Servicio</th>";
                gHTML += "<th class=''>Medico</th>";
                gHTML += "<th class=''>Tipo Pac.</th>";
                gHTML += "<th class=''>T</th>";
                gHTML += "<th class=''>SOP</th>";
                gHTML += "</tr>" + Environment.NewLine;
                int nroitem = 0;

                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    string vCita, vFech, vHora, vHCli, viDNI, vNomb, vServ, vMedi, vTipP, vTurno;
                    vCita = dbRow["ID_CITA"].ToString();
                    vFech = dbRow["Fecha"].ToString();
                    vHora = dbRow["Hora"].ToString();
                    vHCli = dbRow["HCli"].ToString();
                    viDNI = dbRow["DNI"].ToString();
                    vNomb = dbRow["Nombres"].ToString();
                    vServ = dbRow["Servicio"].ToString();
                    vMedi = dbRow["Medico"].ToString();
                    vTipP = dbRow["TipoPac"].ToString();
                    vTurno = dbRow["Turno"].ToString();

                    gHTML += "<tr>";
                    gHTML += "<td class='' >" +
                        "<a href='ConsultaCitaPacRep.aspx?iCita=" + vCita + "' target='_blank' class='btn bg-navy'><i class='fa fa-fw fa-eye'></i></a> " +
                        "<a href='ConsultaSIS.aspx?iCita=" + viDNI + "' target='_blank' class='btn bg-navy'><i class='fa fa-fw fa-strikethrough'></i></a>" +
                        "</td>";
                    gHTML += "<td class='' >" + vFech + "</td>";
                    gHTML += "<td class='' >" + vHora + "</td>";
                    gHTML += "<td class='' >" + vHCli + "</td>";
                    gHTML += "<td class='' >" + viDNI + "</td>";
                    gHTML += "<td class='' >" + vNomb + "</td>";
                    gHTML += "<td class='' >" + vServ + "</td>";
                    gHTML += "<td class='' >" + vMedi + "</td>";
                    gHTML += "<td class='' >" + vTipP + "</td>";
                    gHTML += "<td class='' >" + vTurno + "</td>";
                    gHTML += "<td class='' >" + dbRow["SOP"].ToString() + "</td>";
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


    protected void btnCirugias_Click(object sender, EventArgs e)
    {
        string vHC = txtProd.Text;
        string gHTML = "";
        try
        {
            //con.Open();
            string qSql = "SELECT '' as ID_CITA, [Fecha_Programada] Fecha, [HHCC] HCli, [Edad] + [Tipo_Edad] as Edad, [DNI], " +
                "[Ape_Paterno] + ' ' + [Ape_Materno] + ' ' + [Nombres] as Nombres, " +
                "'SOP' as Servicio, [Cirujano] as Medico, [Ojo], [Tipo_Paciente] TipoPac " +
                "FROM [Estadistica].[dbo].[Programacion] " +
                "WHERE CAST(Fecha_Programada AS DATE) = @hc " +
                "ORDER BY Fecha_Programada DESC;";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@hc", vHC);

            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDatoDetAt = objdataset.Tables[0];

            if (dtDatoDetAt.Rows.Count > 0)
            {
                gHTML += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gHTML += "<table id='tblbscrJS' class='table table-bordered table-hover' style='text-align: left; font-size: 14px; '>";
                gHTML += "<tr><th class=''>Cita </th>";
                gHTML += "<th class=''>Fecha</th>";
                gHTML += "<th class=''>Edad</th>";
                gHTML += "<th class=''>H. Clinica</th>";
                gHTML += "<th class=''>DNI</th>";
                gHTML += "<th class=''>Nombres</th>";
                gHTML += "<th class=''>Servicio</th>";
                gHTML += "<th class=''>Medico</th>";
                gHTML += "<th class=''>Tipo Pac.</th>";
                gHTML += "<th class=''>Ojo</th>";
                gHTML += "<th class=''>SOP</th>";
                gHTML += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                //"SELECT '' as ID_CITA, [Fecha_Programada] Fecha, [HHCC] HCli, [Edad] + [Tipo_Edad] as Edad, [DNI], " +
                //                "[Ape_Paterno] + ' ' + [Ape_Materno] + ' ' + [Nombres] as Nombres, " +
                //                "'SOP' as Servicio, [Cirujano], [Ojo], [Tipo_Paciente] " +
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    string vCita, vFech, vHora, vHCli, viDNI, vNomb, vServ, vMedi, vTipP, vTurno;
                    vCita = dbRow["ID_CITA"].ToString();
                    vFech = dbRow["Fecha"].ToString();
                    vHora = dbRow["Edad"].ToString();
                    vHCli = dbRow["HCli"].ToString();
                    viDNI = dbRow["DNI"].ToString();
                    vNomb = dbRow["Nombres"].ToString();
                    vServ = dbRow["Servicio"].ToString();
                    vMedi = dbRow["Medico"].ToString();
                    vTipP = dbRow["TipoPac"].ToString();
                    vTurno = dbRow["Ojo"].ToString();

                    gHTML += "<tr>";
                    gHTML += "<td class='' ><a href='ConsultaCitaPacRep.aspx?iCita=" + vCita + "' target='_blank' class='btn bg-navy'><i class='fa fa-fw fa-eye'></i></a></td>";
                    gHTML += "<td class='' >" + vFech + "</td>";
                    gHTML += "<td class='' >" + vHora + "</td>";
                    gHTML += "<td class='' >" + vHCli + "</td>";
                    gHTML += "<td class='' >" + viDNI + "</td>";
                    gHTML += "<td class='' >" + vNomb + "</td>";
                    gHTML += "<td class='' >" + vServ + "</td>";
                    gHTML += "<td class='' >" + vMedi + "</td>";
                    gHTML += "<td class='' >" + vTipP + "</td>";
                    gHTML += "<td class='' >" + vTurno + "</td>";
                    gHTML += "<td class='' ></td>";
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
        string JavaScript = "contarFilTbl('tblbscrJS');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", JavaScript, true);


    }

}
