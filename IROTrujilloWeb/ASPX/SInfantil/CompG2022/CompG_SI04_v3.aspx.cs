using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.UI;

public partial class ASPX_SInfantil_CompG2022_CompG_SI04_v3 : System.Web.UI.Page
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

    [WebMethod]
    public static string GetDetAtenciones(string vDNI)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_EXTERNO_SI_TICInf02PC);
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
                    gDetH += "<td class='text-left'>" + dbRow["Fecha_Atencion"].ToString().Substring(0,10) + "</td>";
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

            string qSql = "exec [BD_COMPROMISOSGESTION].dbo.[SP_SI2022_CG04_NOMINAL_v3] '" +
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

            //GVtable.DataSource = objdataset.Tables[0];
            //GVtable.DataBind();
            //CargaTabla(dtDato);
            DataTable dtDatoDetAt = objdataset.Tables[0];

            if (dtDatoDetAt.Rows.Count > 0)
            {
                gHTML += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gHTML += "<table id='tblbscrJS' class='table table-bordered table-hover' style='text-align: left; font-size: 14px; '>";
                gHTML += "<tr><th class=''>Aten. </th>";
                gHTML += "<th>CNV_DNI_Pad</th>";
                gHTML += "<th>PerAnMes</th>";
                gHTML += "<th>NinoApeP_Pad</th>";
                gHTML += "<th>NinoApeM_Pad</th>";
                gHTML += "<th>NinoNom_Pad</th>";
                gHTML += "<th>FecNac_Pad</th>";
                gHTML += "<th>GrupoEdadAct</th>";
                gHTML += "<th>GrEdadActRest</th>";
                gHTML += "<th>AnalisisCumpl</th>";
                gHTML += "<th>MadreDNI_Pad</th>";
                gHTML += "<th>MadreNombre_Pad</th>";
                gHTML += "<th>MadreSeguro</th>";
                gHTML += "<th>MadreUltFecAt</th>";
                gHTML += "<th>MadreUltRed</th>";
                gHTML += "<th>MadreUltMRed</th>";
                gHTML += "<th>MadreUltcEESS</th>";
                gHTML += "<th>MadreUltEESS</th>";
                gHTML += "<th>ContPob</th>";
                gHTML += "<th>Institucion_ren</th>";
                gHTML += "<th>Red_ren</th>";
                gHTML += "<th>MRed_ren</th>";
                gHTML += "<th>Provincia_ren</th>";
                gHTML += "<th>Distrito_ren</th>";
                gHTML += "<th>EESSCodigo</th>";
                gHTML += "<th>NomEESS_ren</th>";
                gHTML += "<th>CredRN1cumple</th>";
                gHTML += "<th>CRN1_FecAten_es</th>";
                gHTML += "<th>CRN1_EESScod</th>";
                gHTML += "<th>CRN1_EESSnom</th>";
                gHTML += "<th>CredRN2cumple</th>";
                gHTML += "<th>CRN2_FecAten_es</th>";
                gHTML += "<th>CRN2_EESScod</th>";
                gHTML += "<th>CRN2_EESSnom</th>";
                gHTML += "<th>CredRN3cumple</th>";
                gHTML += "<th>CRN3_FecAten_es</th>";
                gHTML += "<th>CRN3_EESScod</th>";
                gHTML += "<th>CRN3_EESSnom</th>";
                gHTML += "<th>CredRN4cumple</th>";
                gHTML += "<th>CRN4_FecAten_es</th>";
                gHTML += "<th>CRN4_EESScod</th>";
                gHTML += "<th>CRN4_EESSnom</th>";
                gHTML += "<th>Credm01cumple</th>";
                gHTML += "<th>Cm01_FecAten_es</th>";
                gHTML += "<th>Cm01_EESScod</th>";
                gHTML += "<th>Cm01_EESSnom</th>";
                gHTML += "<th>Credm02cumple</th>";
                gHTML += "<th>Cm02_FecAten_es</th>";
                gHTML += "<th>Cm02_EESScod</th>";
                gHTML += "<th>Cm02_EESSnom</th>";
                gHTML += "<th>Credm03cumple</th>";
                gHTML += "<th>Cm03_FecAten_es</th>";
                gHTML += "<th>Cm03_EESScod</th>";
                gHTML += "<th>Cm03_EESSnom</th>";
                gHTML += "<th>Credm04cumple</th>";
                gHTML += "<th>Cm04_FecAten_es</th>";
                gHTML += "<th>Cm04_EESScod</th>";
                gHTML += "<th>Cm04_EESSnom</th>";
                gHTML += "<th>Credm05cumple</th>";
                gHTML += "<th>Cm05_FecAten_es</th>";
                gHTML += "<th>Cm05_EESScod</th>";
                gHTML += "<th>Cm05_EESSnom</th>";
                gHTML += "<th>Credm06cumple</th>";
                gHTML += "<th>Cm06_FecAten_es</th>";
                gHTML += "<th>Cm06_EESScod</th>";
                gHTML += "<th>Cm06_EESSnom</th>";
                gHTML += "<th>Credm07cumple</th>";
                gHTML += "<th>Cm07_FecAten_es</th>";
                gHTML += "<th>Cm07_EESScod</th>";
                gHTML += "<th>Cm07_EESSnom</th>";
                gHTML += "<th>Credm08cumple</th>";
                gHTML += "<th>Cm08_FecAten_es</th>";
                gHTML += "<th>Cm08_EESScod</th>";
                gHTML += "<th>Cm08_EESSnom</th>";
                gHTML += "<th>Credm09cumple</th>";
                gHTML += "<th>Cm09_FecAten_es</th>";
                gHTML += "<th>Cm09_EESScod</th>";
                gHTML += "<th>Cm09_EESSnom</th>";
                gHTML += "<th>Credm10cumple</th>";
                gHTML += "<th>Cm10_FecAten_es</th>";
                gHTML += "<th>Cm10_EESScod</th>";
                gHTML += "<th>Cm10_EESSnom</th>";
                gHTML += "<th>Credm11cumple</th>";
                gHTML += "<th>Cm11_FecAten_es</th>";
                gHTML += "<th>Cm11_EESScod</th>";
                gHTML += "<th>Cm11_EESSnom</th>";
                gHTML += "<th>CumplimientoCred04</th>";
                gHTML += "<th>CumplSICumpl</th>";
                gHTML += "</tr>" + Environment.NewLine;
                int nroitem = 0;

                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    string vCNV_DNI_Pad, vPerAnMes, vNinoApeP_Pad, vNinoApeM_Pad, 
                        vNinoNom_Pad, vFecNac_Pad, vGrupoEdadAct, vGrEdadActRest, 
                        vAnalisisCumpl, vMadreDNI_Pad, vMadreNombre_Pad, 
                        vMadreSeguro, vMadreUltFecAt, vMadreUltRed, vMadreUltMRed, 
                        vMadreUltcEESS, vMadreUltEESS, vContPob, vInstitucion_ren, 
                        vRed_ren, vMRed_ren, vProvincia_ren, vDistrito_ren, 
                        vEESSCodigo, vNomEESS_ren, vCredRN1cumple, 
                        vCRN1_FecAten_es, vCRN1_EESScod, vCRN1_EESSnom, 
                        vCredRN2cumple, vCRN2_FecAten_es, vCRN2_EESScod, 
                        vCRN2_EESSnom, vCredRN3cumple, vCRN3_FecAten_es, 
                        vCRN3_EESScod, vCRN3_EESSnom, vCredRN4cumple, 
                        vCRN4_FecAten_es, vCRN4_EESScod, vCRN4_EESSnom, 
                        vCredm01cumple, vCm01_FecAten_es, vCm01_EESScod, 
                        vCm01_EESSnom, vCredm02cumple, vCm02_FecAten_es, 
                        vCm02_EESScod, vCm02_EESSnom, vCredm03cumple, 
                        vCm03_FecAten_es, vCm03_EESScod, vCm03_EESSnom, 
                        vCredm04cumple, vCm04_FecAten_es, vCm04_EESScod, 
                        vCm04_EESSnom, vCredm05cumple, vCm05_FecAten_es, 
                        vCm05_EESScod, vCm05_EESSnom, vCredm06cumple, 
                        vCm06_FecAten_es, vCm06_EESScod, vCm06_EESSnom, 
                        vCredm07cumple, vCm07_FecAten_es, vCm07_EESScod, 
                        vCm07_EESSnom, vCredm08cumple, vCm08_FecAten_es, 
                        vCm08_EESScod, vCm08_EESSnom, vCredm09cumple, 
                        vCm09_FecAten_es, vCm09_EESScod, vCm09_EESSnom, 
                        vCredm10cumple, vCm10_FecAten_es, vCm10_EESScod, 
                        vCm10_EESSnom, vCredm11cumple, vCm11_FecAten_es, 
                        vCm11_EESScod, vCm11_EESSnom, 
                        vCumplimientoCred04, vCumplSICumpl;
                    vCNV_DNI_Pad = dbRow["CNV_DNI_Pad"].ToString();
                    vPerAnMes = dbRow["PerAnMes"].ToString();
                    vNinoApeP_Pad = dbRow["NinoApeP_Pad"].ToString();
                    vNinoApeM_Pad = dbRow["NinoApeM_Pad"].ToString();
                    vNinoNom_Pad = dbRow["NinoNom_Pad"].ToString();
                    vFecNac_Pad = dbRow["FecNac_Pad"].ToString();
                    vGrupoEdadAct = dbRow["GrupoEdadAct"].ToString();
                    vGrEdadActRest = dbRow["GrEdadActRest"].ToString();
                    vAnalisisCumpl = dbRow["AnalisisCumpl"].ToString();
                    vMadreDNI_Pad = dbRow["MadreDNI_Pad"].ToString();
                    vMadreNombre_Pad = dbRow["MadreNombre_Pad"].ToString();
                    vMadreSeguro = dbRow["MadreSeguro"].ToString();
                    vMadreUltFecAt = dbRow["MadreUltFecAt"].ToString();
                    vMadreUltRed = dbRow["MadreUltRed"].ToString();
                    vMadreUltMRed = dbRow["MadreUltMRed"].ToString();
                    vMadreUltcEESS = dbRow["MadreUltcEESS"].ToString();
                    vMadreUltEESS = dbRow["MadreUltEESS"].ToString();
                    vContPob = dbRow["ContPob"].ToString();
                    vInstitucion_ren = dbRow["Institucion_ren"].ToString();
                    vRed_ren = dbRow["Red_ren"].ToString();
                    vMRed_ren = dbRow["MRed_ren"].ToString();
                    vProvincia_ren = dbRow["Provincia_ren"].ToString();
                    vDistrito_ren = dbRow["Distrito_ren"].ToString();
                    vEESSCodigo = dbRow["EESSCodigo"].ToString();
                    vNomEESS_ren = dbRow["NomEESS_ren"].ToString();
                    vCredRN1cumple = dbRow["CredRN1cumple"].ToString();
                    vCRN1_FecAten_es = dbRow["CRN1_FecAten_es"].ToString();
                    vCRN1_EESScod = dbRow["CRN1_EESScod"].ToString();
                    vCRN1_EESSnom = dbRow["CRN1_EESSnom"].ToString();
                    vCredRN2cumple = dbRow["CredRN2cumple"].ToString();
                    vCRN2_FecAten_es = dbRow["CRN2_FecAten_es"].ToString();
                    vCRN2_EESScod = dbRow["CRN2_EESScod"].ToString();
                    vCRN2_EESSnom = dbRow["CRN2_EESSnom"].ToString();
                    vCredRN3cumple = dbRow["CredRN3cumple"].ToString();
                    vCRN3_FecAten_es = dbRow["CRN3_FecAten_es"].ToString();
                    vCRN3_EESScod = dbRow["CRN3_EESScod"].ToString();
                    vCRN3_EESSnom = dbRow["CRN3_EESSnom"].ToString();
                    vCredRN4cumple = dbRow["CredRN4cumple"].ToString();
                    vCRN4_FecAten_es = dbRow["CRN4_FecAten_es"].ToString();
                    vCRN4_EESScod = dbRow["CRN4_EESScod"].ToString();
                    vCRN4_EESSnom = dbRow["CRN4_EESSnom"].ToString();
                    vCredm01cumple = dbRow["Credm01cumple"].ToString();
                    vCm01_FecAten_es = dbRow["Cm01_FecAten_es"].ToString();
                    vCm01_EESScod = dbRow["Cm01_EESScod"].ToString();
                    vCm01_EESSnom = dbRow["Cm01_EESSnom"].ToString();
                    vCredm02cumple = dbRow["Credm02cumple"].ToString();
                    vCm02_FecAten_es = dbRow["Cm02_FecAten_es"].ToString();
                    vCm02_EESScod = dbRow["Cm02_EESScod"].ToString();
                    vCm02_EESSnom = dbRow["Cm02_EESSnom"].ToString();
                    vCredm03cumple = dbRow["Credm03cumple"].ToString();
                    vCm03_FecAten_es = dbRow["Cm03_FecAten_es"].ToString();
                    vCm03_EESScod = dbRow["Cm03_EESScod"].ToString();
                    vCm03_EESSnom = dbRow["Cm03_EESSnom"].ToString();
                    vCredm04cumple = dbRow["Credm04cumple"].ToString();
                    vCm04_FecAten_es = dbRow["Cm04_FecAten_es"].ToString();
                    vCm04_EESScod = dbRow["Cm04_EESScod"].ToString();
                    vCm04_EESSnom = dbRow["Cm04_EESSnom"].ToString();
                    vCredm05cumple = dbRow["Credm05cumple"].ToString();
                    vCm05_FecAten_es = dbRow["Cm05_FecAten_es"].ToString();
                    vCm05_EESScod = dbRow["Cm05_EESScod"].ToString();
                    vCm05_EESSnom = dbRow["Cm05_EESSnom"].ToString();
                    vCredm06cumple = dbRow["Credm06cumple"].ToString();
                    vCm06_FecAten_es = dbRow["Cm06_FecAten_es"].ToString();
                    vCm06_EESScod = dbRow["Cm06_EESScod"].ToString();
                    vCm06_EESSnom = dbRow["Cm06_EESSnom"].ToString();
                    vCredm07cumple = dbRow["Credm07cumple"].ToString();
                    vCm07_FecAten_es = dbRow["Cm07_FecAten_es"].ToString();
                    vCm07_EESScod = dbRow["Cm07_EESScod"].ToString();
                    vCm07_EESSnom = dbRow["Cm07_EESSnom"].ToString();
                    vCredm08cumple = dbRow["Credm08cumple"].ToString();
                    vCm08_FecAten_es = dbRow["Cm08_FecAten_es"].ToString();
                    vCm08_EESScod = dbRow["Cm08_EESScod"].ToString();
                    vCm08_EESSnom = dbRow["Cm08_EESSnom"].ToString();
                    vCredm09cumple = dbRow["Credm09cumple"].ToString();
                    vCm09_FecAten_es = dbRow["Cm09_FecAten_es"].ToString();
                    vCm09_EESScod = dbRow["Cm09_EESScod"].ToString();
                    vCm09_EESSnom = dbRow["Cm09_EESSnom"].ToString();
                    vCredm10cumple = dbRow["Credm10cumple"].ToString();
                    vCm10_FecAten_es = dbRow["Cm10_FecAten_es"].ToString();
                    vCm10_EESScod = dbRow["Cm10_EESScod"].ToString();
                    vCm10_EESSnom = dbRow["Cm10_EESSnom"].ToString();
                    vCredm11cumple = dbRow["Credm11cumple"].ToString();
                    vCm11_FecAten_es = dbRow["Cm11_FecAten_es"].ToString();
                    vCm11_EESScod = dbRow["Cm11_EESScod"].ToString();
                    vCm11_EESSnom = dbRow["Cm11_EESSnom"].ToString();
                    vCumplimientoCred04 = dbRow["CumplimientoCred04"].ToString();
                    vCumplSICumpl = dbRow["CumplSICumpl"].ToString();

                    gHTML += "<tr>";
                    gHTML += "<td class='' ><button type='button' class='btn bg-navy' data-toggle='modal' data-target='#mDetAtenciones' onclick=\"fConsultaPac('" + vCNV_DNI_Pad + "')\"><i class='fa fa-fw fa-medkit'></i></button></td>";
                    gHTML += "<td>" + vCNV_DNI_Pad + "</td>";
                    gHTML += "<td>" + vPerAnMes + "</td>";
                    gHTML += "<td>" + vNinoApeP_Pad + "</td>";
                    gHTML += "<td>" + vNinoApeM_Pad + "</td>";
                    gHTML += "<td>" + vNinoNom_Pad + "</td>";
                    gHTML += "<td>" + vFecNac_Pad + "</td>";
                    gHTML += "<td>" + vGrupoEdadAct + "</td>";
                    gHTML += "<td>" + vGrEdadActRest + "</td>";
                    gHTML += "<td>" + vAnalisisCumpl + "</td>";
                    gHTML += "<td>" + vMadreDNI_Pad + "</td>";
                    gHTML += "<td>" + vMadreNombre_Pad + "</td>";
                    gHTML += "<td>" + vMadreSeguro + "</td>";
                    gHTML += "<td>" + vMadreUltFecAt + "</td>";
                    gHTML += "<td>" + vMadreUltRed + "</td>";
                    gHTML += "<td>" + vMadreUltMRed + "</td>";
                    gHTML += "<td>" + vMadreUltcEESS + "</td>";
                    gHTML += "<td>" + vMadreUltEESS + "</td>";
                    gHTML += "<td>" + vContPob + "</td>";
                    gHTML += "<td>" + vInstitucion_ren + "</td>";
                    gHTML += "<td>" + vRed_ren + "</td>";
                    gHTML += "<td>" + vMRed_ren + "</td>";
                    gHTML += "<td>" + vProvincia_ren + "</td>";
                    gHTML += "<td>" + vDistrito_ren + "</td>";
                    gHTML += "<td>" + vEESSCodigo + "</td>";
                    gHTML += "<td>" + vNomEESS_ren + "</td>";
                    gHTML += "<td>" + vCredRN1cumple + "</td>";
                    gHTML += "<td>" + vCRN1_FecAten_es + "</td>";
                    gHTML += "<td>" + vCRN1_EESScod + "</td>";
                    gHTML += "<td>" + vCRN1_EESSnom + "</td>";
                    gHTML += "<td>" + vCredRN2cumple + "</td>";
                    gHTML += "<td>" + vCRN2_FecAten_es + "</td>";
                    gHTML += "<td>" + vCRN2_EESScod + "</td>";
                    gHTML += "<td>" + vCRN2_EESSnom + "</td>";
                    gHTML += "<td>" + vCredRN3cumple + "</td>";
                    gHTML += "<td>" + vCRN3_FecAten_es + "</td>";
                    gHTML += "<td>" + vCRN3_EESScod + "</td>";
                    gHTML += "<td>" + vCRN3_EESSnom + "</td>";
                    gHTML += "<td>" + vCredRN4cumple + "</td>";
                    gHTML += "<td>" + vCRN4_FecAten_es + "</td>";
                    gHTML += "<td>" + vCRN4_EESScod + "</td>";
                    gHTML += "<td>" + vCRN4_EESSnom + "</td>";
                    gHTML += "<td>" + vCredm01cumple + "</td>";
                    gHTML += "<td>" + vCm01_FecAten_es + "</td>";
                    gHTML += "<td>" + vCm01_EESScod + "</td>";
                    gHTML += "<td>" + vCm01_EESSnom + "</td>";
                    gHTML += "<td>" + vCredm02cumple + "</td>";
                    gHTML += "<td>" + vCm02_FecAten_es + "</td>";
                    gHTML += "<td>" + vCm02_EESScod + "</td>";
                    gHTML += "<td>" + vCm02_EESSnom + "</td>";
                    gHTML += "<td>" + vCredm03cumple + "</td>";
                    gHTML += "<td>" + vCm03_FecAten_es + "</td>";
                    gHTML += "<td>" + vCm03_EESScod + "</td>";
                    gHTML += "<td>" + vCm03_EESSnom + "</td>";
                    gHTML += "<td>" + vCredm04cumple + "</td>";
                    gHTML += "<td>" + vCm04_FecAten_es + "</td>";
                    gHTML += "<td>" + vCm04_EESScod + "</td>";
                    gHTML += "<td>" + vCm04_EESSnom + "</td>";
                    gHTML += "<td>" + vCredm05cumple + "</td>";
                    gHTML += "<td>" + vCm05_FecAten_es + "</td>";
                    gHTML += "<td>" + vCm05_EESScod + "</td>";
                    gHTML += "<td>" + vCm05_EESSnom + "</td>";
                    gHTML += "<td>" + vCredm06cumple + "</td>";
                    gHTML += "<td>" + vCm06_FecAten_es + "</td>";
                    gHTML += "<td>" + vCm06_EESScod + "</td>";
                    gHTML += "<td>" + vCm06_EESSnom + "</td>";
                    gHTML += "<td>" + vCredm07cumple + "</td>";
                    gHTML += "<td>" + vCm07_FecAten_es + "</td>";
                    gHTML += "<td>" + vCm07_EESScod + "</td>";
                    gHTML += "<td>" + vCm07_EESSnom + "</td>";
                    gHTML += "<td>" + vCredm08cumple + "</td>";
                    gHTML += "<td>" + vCm08_FecAten_es + "</td>";
                    gHTML += "<td>" + vCm08_EESScod + "</td>";
                    gHTML += "<td>" + vCm08_EESSnom + "</td>";
                    gHTML += "<td>" + vCredm09cumple + "</td>";
                    gHTML += "<td>" + vCm09_FecAten_es + "</td>";
                    gHTML += "<td>" + vCm09_EESScod + "</td>";
                    gHTML += "<td>" + vCm09_EESSnom + "</td>";
                    gHTML += "<td>" + vCredm10cumple + "</td>";
                    gHTML += "<td>" + vCm10_FecAten_es + "</td>";
                    gHTML += "<td>" + vCm10_EESScod + "</td>";
                    gHTML += "<td>" + vCm10_EESSnom + "</td>";
                    gHTML += "<td>" + vCredm11cumple + "</td>";
                    gHTML += "<td>" + vCm11_FecAten_es + "</td>";
                    gHTML += "<td>" + vCm11_EESScod + "</td>";
                    gHTML += "<td>" + vCm11_EESSnom + "</td>";
                    gHTML += "<td>" + vCumplimientoCred04 + "</td>";
                    gHTML += "<td>" + vCumplSICumpl + "</td>";
                    gHTML += "</tr>";
                    gHTML += Environment.NewLine;

                }

                gHTML += "</table></div></div><hr style='border-top: 1px solid blue'>";
            }
            else
            {
                gHTML += "<div style='color:#000000;' class='box'><table class='table table-hover'><tr><th>Sin Registros</th></tr></table></div>";
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