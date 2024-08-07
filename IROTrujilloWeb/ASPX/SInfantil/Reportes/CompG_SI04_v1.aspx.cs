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

public partial class ASPX_SInfantil_Reportes_CompG_SI04_v1 : System.Web.UI.Page
{
    SqlConnection conExtSI = new SqlConnection(ClassGlobal.conexion_EXTERNO_SI);
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
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_EXTERNO_SI);
        string gHTML = "";
        try
        {
            string qSql = "";

            if (vSel == "Institucion")
            {
                qSql = "select Institucion_ren as dato " +
                    "from BD_COMPROMISOSGESTION.dbo.CG2022SI04Credv2 " +
                    "group by Institucion_ren; ";
            }
            if (vSel == "Red")
            {
                qSql = "select Institucion_ren, Red_ren as dato from BD_COMPROMISOSGESTION.dbo.CG2022SI04Credv2 where Institucion_ren = '" + vInstitucion + "' group by Institucion_ren, Red_ren order by Red_ren ";
            }
            if (vSel == "MRed")
            {
                qSql = "select Institucion_ren, Red_ren, MRed_ren as dato from BD_COMPROMISOSGESTION.dbo.CG2022SI04Credv2 where Institucion_ren = '" + vInstitucion + "' and Red_ren = '" + vRed + "' group by Institucion_ren, Red_ren, MRed_ren order by MRed_ren ";
            }
            if (vSel == "Provincia")
            {
                qSql = "select Institucion_ren, Provincia_ren as dato from BD_COMPROMISOSGESTION.dbo.CG2022SI04Credv2 where Institucion_ren = '" + vInstitucion + "' group by Institucion_ren, Provincia_ren order by Provincia_ren ";
            }
            if (vSel == "Distrito")
            {
                qSql = "select Institucion_ren, Provincia_ren, Distrito_ren as dato from BD_COMPROMISOSGESTION.dbo.CG2022SI04Credv2 where Institucion_ren = '" + vInstitucion + "' and Provincia_ren = '" + vProvincia + "' group by Institucion_ren, Provincia_ren, Distrito_ren order by Distrito_ren ";
            }
            if (vSel == "EESS")
            {
                qSql = "select NomEESS_ren as dato from BD_COMPROMISOSGESTION.dbo.CG2022SI04Credv2 where Institucion_ren like '%" + vInstitucion + "%' and Red_ren like '%" + vRed + "%' and MRed_ren like '%" + vMRed + "%' and Provincia_ren like '%" + vProvincia + "%' and Distrito_ren like '%" + vDistrito + "%' group by NomEESS_ren order by NomEESS_ren ";
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
                    for (int i = 0; i < dbRow["dato"].ToString().Length; i++)
                    {
                        if (dbRow["dato"].ToString().Substring(i, 1) == "\"") { break;  }
                        vdato += dbRow["dato"].ToString().Substring(i, 1);
                    }
                    gHTML += "<tr onclick=\"document.getElementById('" + vtxtCarga + "').value = '" + vdato + "';\" data-dismiss='modal'>" +
                        "<td>" + nroitem + "</td>";
                    gHTML += "<td style='text-align: left;' >" + dbRow["dato"].ToString() + "</td>" +
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
            //con.Open();
            //string qSql = "select DNIPacPNCNV, PerAnMes, ApePNinoPN, ApeMNinoPN, NomNinoPN, FecNacPac, ContPoblac, ContAtencion, FecAten_es, " +
            //    "Institucion_ren, Red_ren, MRed_ren, " +
            //    "Provincia_ren, Distrito_ren, EESSCodigo, NomEESS_ren, " +
            //    "CredRN1cumple, CRN1_FecAten_es, CRN1_EESScod, CRN1_EESSnom, " +
            //    "CredRN2cumple, CRN2_FecAten_es, CRN2_EESScod, CRN2_EESSnom, " +
            //    "CredRN3cumple, CRN3_FecAten_es, CRN3_EESScod, CRN3_EESSnom, " +
            //    "CredRN4cumple, CRN4_FecAten_es, CRN4_EESScod, CRN4_EESSnom, " +
            //    "Credm01cumple, Cm01_FecAten_es, Cm01_EESScod, Cm01_EESSnom, " +
            //    "Credm02cumple, Cm02_FecAten_es, Cm02_EESScod, Cm02_EESSnom, " +
            //    "Credm03cumple, Cm03_FecAten_es, Cm03_EESScod, Cm03_EESSnom, " +
            //    "Credm04cumple, Cm04_FecAten_es, Cm04_EESScod, Cm04_EESSnom, " +
            //    "Credm05cumple, Cm05_FecAten_es, Cm05_EESScod, Cm05_EESSnom, " +
            //    "Credm06cumple, Cm06_FecAten_es, Cm06_EESScod, Cm06_EESSnom, " +
            //    "Credm07cumple, Cm07_FecAten_es, Cm07_EESScod, Cm07_EESSnom, " +
            //    "Credm08cumple, Cm08_FecAten_es, Cm08_EESScod, Cm08_EESSnom, " +
            //    "Credm09cumple, Cm09_FecAten_es, Cm09_EESScod, Cm09_EESSnom, " +
            //    "Credm10cumple, Cm10_FecAten_es, Cm10_EESScod, Cm10_EESSnom, " +
            //    "Credm11cumple, Cm11_FecAten_es, Cm11_EESScod, Cm11_EESSnom, " +
            //    "CumplimientoCred04 " +
            //    "from BD_COMPROMISOSGESTION.dbo.CG2022SI04Credv2 " +
            //    "where Institucion_ren LIKE '%" + vInstitucion + "' AND " +
            //    "Red_ren LIKE '%" + vRed + "' AND " +
            //    "MRed_ren LIKE '%" + vMRed + "' AND " +
            //    "Provincia_ren like '%" + vProvincia + "' and " +
            //    "Distrito_ren like '%" + vDistrito + "' and " +
            //    "NomEESS_ren like '%" + vEESS + "%' ";

            string qSql = "exec[BD_COMPROMISOSGESTION].[dbo].[SP_SI2022_CG04_NOMINAL] '" + 
                vInstitucion + "', '" + vRed + "', '" + vMRed + "', '" + 
                vProvincia + "', '" + vDistrito + "', '" + vEESS + "'";
            SqlCommand cmd = new SqlCommand(qSql, conExtSI);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            //cmd.Parameters.AddWithValue("@hc", vHC);
            //cmd.Parameters.AddWithValue("@dni", vDNI);

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