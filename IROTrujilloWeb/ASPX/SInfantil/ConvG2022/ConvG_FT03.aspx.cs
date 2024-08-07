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

public partial class ASPX_SInfantil_ConvG2022_ConvG_FT03 : System.Web.UI.Page
{
    SqlConnection conExtSI = new SqlConnection(ClassGlobal.conexion_EXTERNO_SI_TICInf02PC);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        if (!Page.IsPostBack)
        {
            int vmonth = DateTime.Now.Month;
        }
    }

    [WebMethod]
    public static string GetDatoFiltro(string vtxtCarga, string vInstitucion, string vRed, string vMRed, string vProvincia, string vDistrito, string vEESS, string vSel)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_EXTERNO_SI_TICInf02PC);
        string gHTML = "";
        try
        {
            string qSql = "";

            if (vSel == "Institucion")
            {
                qSql = "exec [BD_GENERAL].[dbo].[sp_listar_ESRedMRedProvDist] 'Sector', '', '', '', '', ''; ";
            }
            if (vSel == "Red")
            {
                qSql = "exec [BD_GENERAL].[dbo].[sp_listar_ESRedMRedProvDist] 'Red', '" + vInstitucion + "', '', '', '', '';";
            }
            if (vSel == "MRed")
            {
                qSql = "exec [BD_GENERAL].[dbo].[sp_listar_ESRedMRedProvDist] 'MRed', '" + vInstitucion + "', '" + vRed + "', '', '', '';";
            }
            if (vSel == "Provincia")
            {
                qSql = "exec [BD_GENERAL].[dbo].[sp_listar_ESRedMRedProvDist] 'Provincia', '" + vInstitucion + "', '', '', '', '';";
            }
            if (vSel == "Distrito")
            {
                qSql = "exec [BD_GENERAL].[dbo].[sp_listar_ESRedMRedProvDist] 'Distrito', '" + vInstitucion + "', '', '', '" + vProvincia + "', '';";
            }
            if (vSel == "EESS")
            {
                qSql = "exec [BD_GENERAL].[dbo].[sp_listar_ESRedMRedProvDist] 'EESS', '" + vInstitucion + "', '" + vRed + "', '" + vMRed + "', '" + vProvincia + "', '" + vDistrito + "';";
            }


            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            DataTable dtDatoDetAt = objdataset.Tables[0];

            if (dtDatoDetAt.Rows.Count > 0)
            {
                gHTML += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gHTML += "<table id='tblTablaModal' class='table table-hover' style='text-align: right; font-size: 14px; '>";
                gHTML += "<th class=''>Nro </th>";
                gHTML += "<th class=''>Seleccionar</th>";
                gHTML += "</tr>" + Environment.NewLine;
                int nroitem = 0;

                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    string vdato = "";
                    for (int i = 0; i < dbRow["CampoDato"].ToString().Length; i++)
                    {
                        if (dbRow["CampoDato"].ToString().Substring(i, 1) == "\"") { break; }
                        vdato += dbRow["CampoDato"].ToString().Substring(i, 1);
                    }
                    gHTML += "<tr onclick=\"document.getElementById('" + vtxtCarga + "').value = '" + vdato + "';\" data-dismiss='modal'>" +
                        "<td>" + nroitem + "</td>";
                    gHTML += "<td style='text-align: left;' >" + dbRow["CampoDato"].ToString() + "</td>" +
                        "</tr>";
                    gHTML += Environment.NewLine;
                }

                gHTML += "</table></div></div><hr style='border-top: 1px solid blue'>";
            }
        }
        catch (Exception ex)
        {
            gHTML += "-" + "-" + ex.Message.ToString();
        }
        return gHTML;
    }

    protected void bntBuscar_Click(object sender, EventArgs e)
    {
        CargaTablaDT();
    }


    public void CargaTablaDT()
    {
        string vInstitucion = txtInstitucion.Text;
        string vRed = txtRed.Text;
        string vMRed = txtMicroRed.Text;
        string vProvincia = txtProvincia.Text;
        string vDistrito = txtDistrito.Text;
        string vEESS = txtEESS.Text;

        string gHTML = "";
        try
        {

            string qSql = "exec [BD_CONVENIOSGESTION].[dbo].[SP_CG2022_FT03_Nominal] '" +
                vInstitucion + "', '" + vRed + "', '" + vMRed + "', '" +
                vProvincia + "', '" + vDistrito + "', '" + vEESS + "'";
            SqlCommand cmd = new SqlCommand(qSql, conExtSI);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.SelectCommand = cmd;

            conExtSI.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conExtSI.Close();

            GVtable.DataSource = objdataset.Tables[0];
            GVtable.DataBind();
            //CargaTabla(dtDato);
            //DataTable dtDatoDetAt = objdataset.Tables[0];

            //if (dtDatoDetAt.Rows.Count > 0)
            //{
            //    gHTML += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
            //    gHTML += "<table id='tblbscrJS' class='table table-bordered table-hover' style='text-align: left; font-size: 14px; '>";
            //    gHTML += "<tr><th class=''>Cita </th>";
            //    gHTML += "<th class=''>Fecha</th>";
            //    gHTML += "<th class=''>Hora</th>";
            //    gHTML += "<th class=''>H. Clinica</th>";
            //    gHTML += "<th class=''>DNI</th>";
            //    gHTML += "<th class=''>Nombres</th>";
            //    gHTML += "<th class=''>Servicio</th>";
            //    gHTML += "<th class=''>Medico</th>";
            //    gHTML += "<th class=''>Tipo Pac.</th>";
            //    gHTML += "</tr>" + Environment.NewLine;
            //    int nroitem = 0;

            //    foreach (DataRow dbRow in dtDatoDetAt.Rows)
            //    {
            //        nroitem += 1;
            //        string vCita, vFech, vHora, vHCli, viDNI, vNomb, vServ, vMedi, vTipP;
            //        vCita = dbRow["ID_CITA"].ToString();
            //        vFech = dbRow["Fecha"].ToString();
            //        vHora = dbRow["Hora"].ToString();
            //        vHCli = dbRow["HCli"].ToString();
            //        viDNI = dbRow["DNI"].ToString();
            //        vNomb = dbRow["Nombres"].ToString();
            //        vServ = dbRow["Servicio"].ToString();
            //        vMedi = dbRow["Medico"].ToString();
            //        vTipP = dbRow["TipoPac"].ToString();

            //        gHTML += "<tr>";
            //        gHTML += "<td class='' ><a href='ConsultaCitaPacRep.aspx?iCita=" + vCita + "' target='_blank' class='btn bg-navy'><i class='fa fa-fw fa-eye'></i></a></td>";
            //        gHTML += "<td class='' >" + vFech + "</td>";
            //        gHTML += "<td class='' >" + vHora + "</td>";
            //        gHTML += "<td class='' >" + vHCli + "</td>";
            //        gHTML += "<td class='' >" + viDNI + "</td>";
            //        gHTML += "<td class='' >" + vNomb + "</td>";
            //        gHTML += "<td class='' >" + vServ + "</td>";
            //        gHTML += "<td class='' >" + vMedi + "</td>";
            //        gHTML += "<td class='' >" + vTipP + "</td>";
            //        gHTML += "</tr>";
            //        gHTML += Environment.NewLine;

            //    }

            //    gHTML += "</table></div></div><hr style='border-top: 1px solid blue'>";
            //}

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