using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.UI;

public partial class ASPX_SInfantil_CompG2022_CompG_SI02_02 : System.Web.UI.Page
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

    [WebMethod]
    public static string GetDetAtenciones(string vDNI)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_EXTERNO_SI);
        string gDetH = "";
        try
        {
            string qSql = "select *, " +
                "BD_COMPROMISOSGESTION.dbo.functCG2022SI04(Edad_Dias_Paciente_FechaAtencion) as GrupoEdadAtencion " +
                "from BD_COMPROMISOSGESTION.dbo.NINHOS_HISMINSA " +
                "where Numero_Documento_Paciente = '" + vDNI + "' and Codigo_Item in ('Z001', '99381.01', '99381') " +
                "order by Fecha_Atencion ";


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
                gDetH += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetH += "<table id='tbldscrg' class='table table-hover' style='text-align: right; font-size: 14px; '>";
                gDetH += "<tr>";
                gDetH += "<th class=''>Fecha Aten.</th>";
                gDetH += "<th class=''>Fecha Nac.</th>";
                gDetH += "<th class=''>Dias Aten.</th>";
                gDetH += "<th class=''>Cod. Item</th>";
                gDetH += "<th class=''>LAB</th>";
                gDetH += "<th class=''>GrupoEdad</th>";
                gDetH += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    gDetH += "<tr>";
                    gDetH += "<td class='text-left'>" + dbRow["Fecha_Atencion"].ToString().Substring(0, 10) + "</td>";
                    gDetH += "<td class='text-left'>" + dbRow["Fecha_Nacimiento_Paciente"].ToString().Substring(0, 10) + "</td>";
                    gDetH += "<td class='text-center'>" + dbRow["Edad_Dias_Paciente_FechaAtencion"].ToString() + "</td>";
                    gDetH += "<td class='text-left'>" + dbRow["Codigo_Item"].ToString() + "</td>";
                    gDetH += "<td class='text-center'>" + dbRow["Valor_Lab"].ToString() + "</td>";
                    gDetH += "<td class='text-left'>" + dbRow["GrupoEdadAtencion"].ToString() + "</td>";

                    gDetH += "</tr>" + Environment.NewLine;
                }
                gDetH += "</table></div></div><hr style='border-top: 1px solid blue'>";
            }
            else
            {
                gDetH += "<div style='color:#000000;' class='box'><table class='table table-hover'><tr><th>Sin Registros</th></tr></table></div>";
            }
            //gDetAtenciones - FINAL
        }
        catch (Exception ex)
        {
            gDetH += ex.Message.ToString() + Environment.NewLine + ex.ToString();
        }
        return gDetH;
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

            string qSql = "exec [BD_COMPROMISOSGESTION].dbo.[SP_SI2022_CG0202_NOMINAL_v2] '" +
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
            ////CargaTabla(dtDato);
            //DataTable dtDatoDetAt = objdataset.Tables[0];

            //if (dtDatoDetAt.Rows.Count > 0)
            //{
            //    gHTML += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
            //    gHTML += "<table id='tblbscrJS' class='table table-bordered table-hover' style='text-align: left; font-size: 14px; '>";
            //    gHTML += "<tr><th class=''>Aten. </th>";
            //    gHTML += "<th>CNV_DNI_Pad</th>";
            //    gHTML += "<th>PerAnMes</th>";
            //    gHTML += "<th>NinoApeP_Pad</th>";
            //    gHTML += "</tr>" + Environment.NewLine;
            //    int nroitem = 0;

            //    foreach (DataRow dbRow in dtDatoDetAt.Rows)
            //    {
            //        nroitem += 1;
            //        string vCNV_DNI_Pad, vPerAnMes, vNinoApeP_Pad, vNinoApeM_Pad,
            //            vNinoNom_Pad, vFecNac_Pad, vGrupoEdadAct, vGrEdadActRest,
            //            vAnalisisCumpl, vMadreDNI_Pad, vMadreNombre_Pad,
            //            vCm11_EESScod, vCm11_EESSnom,
            //            vCumplimientoCred04, vCumplSICumpl;
            //        vCNV_DNI_Pad = dbRow["CNV_DNI_Pad"].ToString();
            //        vPerAnMes = dbRow["PerAnMes"].ToString();
            //        vNinoApeP_Pad = dbRow["NinoApeP_Pad"].ToString();
            //        vCumplimientoCred04 = dbRow["CumplimientoCred04"].ToString();
            //        vCumplSICumpl = dbRow["CumplSICumpl"].ToString();

            //        gHTML += "<tr>";
            //        gHTML += "<td class='' ><button type='button' class='btn bg-navy' data-toggle='modal' data-target='#mDetAtenciones' onclick=\"fConsultaPac('" + vCNV_DNI_Pad + "')\"><i class='fa fa-fw fa-medkit'></i></button></td>";
            //        gHTML += "<td>" + vCNV_DNI_Pad + "</td>";
            //        gHTML += "<td>" + vPerAnMes + "</td>";
            //        gHTML += "<td>" + vCumplimientoCred04 + "</td>";
            //        gHTML += "<td>" + vCumplSICumpl + "</td>";
            //        gHTML += "</tr>";
            //        gHTML += Environment.NewLine;

            //    }

            //    gHTML += "</table></div></div><hr style='border-top: 1px solid blue'>";
            //}
            //else
            //{
            //    gHTML += "<div style='color:#000000;' class='box'><table class='table table-hover'><tr><th>Sin Registros</th></tr></table></div>";
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
