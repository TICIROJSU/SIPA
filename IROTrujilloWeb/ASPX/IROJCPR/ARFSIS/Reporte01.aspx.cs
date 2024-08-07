using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ASPX_IROJCPR_ARFSIS_Reporte01 : System.Web.UI.Page
{
    string html = "";
    string htmlAtenciones = "";
    string htmlDetAtenciones = "";
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        if (!Page.IsPostBack)
        {
            int vyear = DateTime.Now.Year;
            DDLAnio.Text = vyear.ToString();
            int vmonth = DateTime.Now.Month;
            DDLMes.SelectedIndex = vmonth;

        }

    }

    [WebMethod]
    public static string GetDetAtenciones( string vdia, string vusuario)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetAtenciones = "";
        try
        {
            SqlCommand cmd = new SqlCommand("NUEVA.dbo.SP_JVAR_ARFSIS", conSAP00i);
            cmd.CommandType = CommandType.StoredProcedure;
            //creamos los parametros que usaremos
            cmd.Parameters.Add("@vdia", SqlDbType.VarChar);
            cmd.Parameters.Add("@vusuario", SqlDbType.VarChar);
            cmd.Parameters.Add("@CUENTA", SqlDbType.Int);
            cmd.Parameters["@vdia"].Value = vdia;
            cmd.Parameters["@vusuario"].Value = vusuario;
           // cmd.Parameters["@CUENTA"].Value = 0;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00i.Close();

            DataTable dtDatoDetAt = objdataset.Tables[0];

            //gDetAtenciones = CargaTablaDetAtenciones(dtDatoDetAt, vanio, vmes, vdia, turno, vplaza);
            if (dtDatoDetAt.Rows.Count > 0)
            {
                gDetAtenciones += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetAtenciones += "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
                gDetAtenciones += "<tr>";
                gDetAtenciones += "<th class=''>N° </th>";
                gDetAtenciones += "<th class=''>FUA</th>";
                gDetAtenciones += "<th class=''>SIPA</th>";
                gDetAtenciones += "<th class=''>ARFSIS</th>";
                gDetAtenciones += "<th class=''>SERVICIO</th>";
                gDetAtenciones += "<th class=''>PRESTACION</th>";
                gDetAtenciones += "<th class=''>RETRASO</th>";
                gDetAtenciones += "<th class=''>HC</th>";
                gDetAtenciones += "<th class=''>PACIENTE</th>";
                gDetAtenciones += "<th class=''>TIPPAC</th>";
                gDetAtenciones += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    gDetAtenciones += "<tr>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + nroitem + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["FUA"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["SIPA"].ToString().Substring(0, 10) + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["ARFSIS"].ToString().Substring(0, 10) + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["SERVICIO"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["PRESTACION"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["RETRASO"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["HC"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["PACIENTE"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: center;'>" + dbRow["TIPPAC"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + "<button type='button' class='btn bg-navy' data-toggle='modal' data-target='#modalDetDx'  onclick=\"DetDx('" + dbRow["FUA"].ToString() + "')\"><i class='fa fa-fw fa-eye'></i></button>" + "</td>";

                    //gDetAtenciones += "<td class='' style='text-align: center;'>" + dbRow["Fin1"].ToString() + "</td>";
                    gDetAtenciones += "</tr>" + Environment.NewLine;
                }

                gDetAtenciones += "</table></div></div><hr style='border-top: 1px solid blue'>";

            }
            //gDetAtenciones - FINAL
        }
        catch (Exception ex)
        {
            gDetAtenciones += ex.Message.ToString();
        }
        return gDetAtenciones;
    }

    [WebMethod]
    public static string GetDetDx(string dat1)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetAtenciones = "";
        try
        {
            string qSql = "select ID_HIS, ET, DES_ET, COD_SERVSA1, DSC_SER, CODI1, enf1.DESC_ENF as Enf1, CODI2, enf2.DESC_ENF as Enf2, CODI3, enf3.DESC_ENF as Enf3, ";
            qSql += "CODI4, enf4.DESC_ENF as Enf4, CODI5, enf5.DESC_ENF as Enf5, CODI6, enf6.DESC_ENF as Enf6 from NUEVA.dbo.CHEQ2011 ";
            qSql += "left join NUEVA.dbo.enfermedades enf1 on DX1=enf1.Codigo left join NUEVA.dbo.enfermedades enf2 on DX2=enf2.Codigo ";
            qSql += "left join NUEVA.dbo.enfermedades enf3 on DX3=enf3.Codigo left join NUEVA.dbo.enfermedades enf4 on DX4=enf4.Codigo ";
            qSql += "left join NUEVA.dbo.enfermedades enf5 on DX5=enf5.Codigo left join NUEVA.dbo.enfermedades enf6 on DX6=enf6.Codigo ";
            qSql += "left join NUEVA.dbo.ETNIA on ET=COD_ET left join NUEVA.dbo.SERVICIO on COD_SERVSA1=COD_SER ";
            qSql += "where ID_HIS='" + dat1 + "' ";
            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            DataTable dtDatoDetAt = objdataset.Tables[0];

            //gDetAtenciones = CargaTablaDetAtenciones(dtDatoDetAt, vanio, vmes, vdia, turno, vplaza);
            if (dtDatoDetAt.Rows.Count > 0)
            {
                gDetAtenciones += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetAtenciones += "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
                gDetAtenciones += "<tr>";
                gDetAtenciones += "<th class=''></th>";
                gDetAtenciones += "<th class=''>N°|Cod </th>";
                gDetAtenciones += "<th class=''>Descripcion</th>";
                //gDetAtenciones += "<th class=''>DX1</th>";
                //gDetAtenciones += "<th class=''>dx2</th>";
                //gDetAtenciones += "<th class=''>Dx3</th>";
                //gDetAtenciones += "<th class=''>Dx4</th>";
                //gDetAtenciones += "<th class=''>Etnia</th>";
                //gDetAtenciones += "<th class=''>Cod_Servsa1</th>";
                gDetAtenciones += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    gDetAtenciones += "<tr>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>Item</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + nroitem + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["ID_HIS"].ToString() + "</td>";
                    gDetAtenciones += "</tr><tr>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>Etnia</td>";
                    gDetAtenciones += "<td class='' style='text-align: center;'>" + dbRow["ET"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["DES_ET"].ToString() + "</td>";
                    gDetAtenciones += "</tr><tr>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>Servicio</td>";
                    gDetAtenciones += "<td class='' style='text-align: center;'>" + dbRow["Cod_Servsa1"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["DSC_SER"].ToString() + "</td>";
                    gDetAtenciones += "</tr><tr>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>Dx1</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["CODI1"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["Enf1"].ToString() + "</td>";
                    gDetAtenciones += "</tr><tr>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>Dx2</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["CODI2"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["Enf2"].ToString() + "</td>";
                    gDetAtenciones += "</tr><tr>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>Dx3</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["CODI3"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["Enf3"].ToString() + "</td>";
                    gDetAtenciones += "</tr><tr>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>Dx4</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["CODI4"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["Enf4"].ToString() + "</td>";
                    gDetAtenciones += "</tr><tr>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>Dx5</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["CODI5"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["Enf5"].ToString() + "</td>";
                    gDetAtenciones += "</tr><tr>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>Dx6</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["CODI6"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["Enf6"].ToString() + "</td>";
                    gDetAtenciones += "</tr>" + Environment.NewLine;
                }

                gDetAtenciones += "</table></div></div><hr style='border-top: 1px solid blue'>";

            }
            //gDetAtenciones - FINAL
        }
        catch (Exception ex)
        {
            gDetAtenciones += "-" + "-" + ex.Message.ToString();
        }
        return gDetAtenciones;
    }

    [WebMethod]
    public static string fShowProfesionales(string vanio, string vmes)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetAtenciones = "";
        try
        {
            string qSql = "select PER.COD_PRO, PROF.DES_PRO as Profesion from NUEVA.dbo.CHEQ2011 HIS left join NUEVA.dbo.PERSONAL PER on HIS.PLAZA = PER.HIS_PER left join NUEVA.dbo.PROFESION PROF on PER.COD_PRO = PROF.COD_PRO where ANO='" + vanio + "' and MES ='" + vmes + "' group by PER.COD_PRO, PROF.DES_PRO";
            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            DataTable dtDatoDetAt = objdataset.Tables[0];

            //gDetAtenciones = CargaTablaDetAtenciones(dtDatoDetAt, vanio, vmes, vdia, turno, vplaza);
            if (dtDatoDetAt.Rows.Count > 0)
            {
                gDetAtenciones += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetAtenciones += "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
                gDetAtenciones += "<tr>";
                gDetAtenciones += "<th class=''>N° </th>";
                gDetAtenciones += "<th class=''>Cod</th>";
                gDetAtenciones += "<th class=''>Profesion</th>";
                gDetAtenciones += "<th onclick=\"fSelProf('', 'Profesional')\" data-dismiss='modal'>Sel</th>";
                gDetAtenciones += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    gDetAtenciones += "<tr>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + nroitem + "</td>";
                    gDetAtenciones += "<td class=''>" + dbRow["COD_PRO"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["Profesion"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + "<button type='button' class='btn bg-navy' onclick=\"fSelProf('" + dbRow["COD_PRO"].ToString() + "', '" + dbRow["Profesion"].ToString() + "')\" data-dismiss='modal'><i class='fa fa-fw fa-eye'></i></button>" + "</td>";
                    gDetAtenciones += "</tr>" + Environment.NewLine;
                }

                gDetAtenciones += "</table></div></div><hr style='border-top: 1px solid blue'>";

            }
            //gDetAtenciones - FINAL
        }
        catch (Exception ex)
        {
            //LitErrores.Text += vplaza + "-" + "-" + ex.Message.ToString();
        }
        return gDetAtenciones;
    }

    protected void bntBuscar_Click(object sender, EventArgs e)
    {
        CargaTablaDT();
    }

    public void CargaTablaDT()
    {
        string vAnio = DDLAnio.SelectedValue;
        string vMes = DDLMes.SelectedValue;
        ClassGlobal.varGlobalTmp = vMes;
        string vEESS = txtEESS.Text;
        string vCodProf = txtIdProf.Value;
        try
        {
            //con.Open();
            string qSql = "select (CASE WHEN ARFSIS.DNI IS NOT NULL THEN ARFSIS.DNI ELSE SIS.DNI END) DNI, (CASE WHEN ARFSIS.Usuario IS NOT NULL THEN ARFSIS.Usuario ELSE SIS.APE_PER END) Usuario, ARFSIS.MES, ARFSIS.PRODUCCION ARFSIS, SIS.Produccion Ventanilla, SIS.Anulados, (SIS.Produccion - SIS.Anulados) Validos from ( select DIGITADOR DNI, USUARIO, month(fechaReg) MES,count(*) PRODUCCION from IROf.dbo.Arfsis WHERE month(fechaReg)= '" + vMes + "' and YEAR(fechaReg)= '" + vAnio + "' group by digitador,Usuario,month(fechaReg) ) ARFSIS full outer join ( select  SUBSTRING(HIS_PER, 2, 8) as DNI, APE_PER, count(*) Produccion, sum(case NUE_SIS when '0' then 1 else 0 end) Anulados from NUEVA.dbo.sis inner join NUEVA.dbo.PERSONAL on USU_PER = USU_SIS where USU_SIS in (118, 165, 163, 161, 162, 100, 4,14,64,6) and month(FEC_SIS)= '" + vMes + "' and YEAR(FEC_SIS)= '" + vAnio + "' group by SUBSTRING(HIS_PER, 2, 8), APE_PER ) SIS on ARFSIS.DNI = SIS.DNI";
            //string qSql = "select HIS.PLAZA, PER.APE_PER as Profesional, PROF.DES_PRO as Profesion, HIS.MES, COUNT(HIS.ID_HIS) as Atenciones, " +
            //    "sum(case when HIS.FI='2' then 1 else 0 end) as SIS, sum(case when HIS.FI='1' then 1 else 0 end) as Usu, sum(case when HIS.FI='11' then 1 else 0 end) as Exo, sum(case when (HIS.FI='1' or HIS.FI='2' or HIS.FI='11') then 0 else 1 end) as otr " +
            //    "from NUEVA.dbo.CHEQ2011 HIS left join NUEVA.dbo.PERSONAL PER on HIS.PLAZA = PER.HIS_PER left join NUEVA.dbo.PROFESION PROF on PER.COD_PRO = PROF.COD_PRO " +
            //    "where HIS.ANO = '" + vAnio + "' and HIS.MES = '" + vMes + "' and PER.COD_PRO like '%" + vCodProf + "%' " +
            //    "group by HIS.PLAZA, HIS.MES, PER.APE_PER, PROF.DES_PRO " +
            //    "order by PROF.DES_PRO; ";

            //----//-/-/---/-/-/-/---//-/--///----/-/---///--
            //string qSql = "select DIGITADOR DNI, USUARIO, month(fechaReg) MES,count(*) PRODUCCION from IROf.dbo.Arfsis  WHERE month                     (fechaReg) = '" + vMes + "' AND YEAR(fechaReg)= '" + vAnio + "'    group by digitador,Usuario,month(fechaReg)     union all " +
            //            "select 'Ventanilla', Des_Usu, month(FEC_SIS) MES, count(*) from NUEVA.dbo.sis  inner join NUEVA.dbo.USUARIO  on Cod_Usu = USU_SIS  where month(FEC_SIS) = '" + vMes + "' and USU_SIS in (118, 165, 163, 161, 162, 100, 4,14,64,6) and YEAR(FEC_SIS)= '" + vAnio + "' group by Des_Usu,month(FEC_SIS) " +
            //           " ORDER BY USUARIO,PRODUCCION";
            //----//-/-/---/-/-/-/---//-/--///----/-/---///--

            //"" +
            //"" +
            //"select DIGITADOR DNI, USUARIO, month(fechaReg) MES,count(*) PRODUCCION from IROf.dbo.Arfsis " +
            //             "WHERE month(fechaReg) = '" + vMes + "' AND YEAR(fechaReg)= '" + vAnio + "' "+
            //            "group by digitador,Usuario,month(fechaReg) "+
            //            "ORDER BY PRODUCCION DESC";

            //select DIA, MT, COUNT(ID_HIS) as Atenciones from NUEVA.dbo.CHEQ2011 where ANO = '2020' and MES = '4' and PLAZA = '10640796401' group by DIA, MT;
            //select NUM_REG, DNI, EDAD, SEXO from NUEVA.dbo.CHEQ2011 where ANO = '2020' and MES = '4' and DIA = '17' and MT = 'M' and PLAZA = '10640796401';

            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];

            //GVtable.DataSource = dtDato;
            //GVtable.DataBind();

            CargaTabla(dtDato);
        }
        catch (Exception ex)
        {
            //Label1.Text = "Error";
            ex.Message.ToString();
            LitTABL1.Text = ex.Message.ToString();
        }
    }

    public void CargaTabla(DataTable dtDato)
    {
        html = "";
        if (dtDato.Rows.Count > 0)
        {
            
            html += Environment.NewLine + "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
            html += "<tr>";
            html += "<th class=''></th>";
            html += "<th class=''>DNI</th>";
            html += "<th class=''>USUARIO</th>";
            html += "<th class=''>MES</th>";
            html += "<th class='' style='text-align: center;'>ARFSIS</th>";
            html += "<th class='' style='text-align: center;'>VENTANILLA</th>";
            html += "<th class='' style='text-align: center;'>ANULADOS</th>";
            html += "<th class='' style='text-align: center;'>VALIDOS</th>";
            html += "<th class=''>Accion</th>";
            html += "</tr>" + Environment.NewLine;
            html += "<tr>";
            html += "<td class=''></td>";
            html += "<td class=''></td>";
            html += "<td class=''></td>";
            html += "<td class=''></td>";
            html += "<th class='' style='text-align: center;'>" + ClassGlobal.formatoMillar(dtDato.Compute("SUM(ARFSIS)", "").ToString()) + "</th>";
            html += "<th class='' style='text-align: center;'>" + dtDato.Compute("SUM(Ventanilla)", "").ToString() + "</th>";
            html += "<th class='' style='text-align: center;'>" + dtDato.Compute("SUM(Anulados)", "").ToString() + "</th>";
            html += "<th class='' style='text-align: center;'>" + dtDato.Compute("SUM(Validos)", "").ToString() + "</th>";
            html += "<td class=''></td>";
            html += "</tr>" + Environment.NewLine;
            int nroitem = 0;
            foreach (DataRow dbRow in dtDato.Rows)
            {
                nroitem += 1;
                html += "<tr>";
                html += "<td class='' style='text-align: left;'>" + nroitem + "</td>";
                html += "<td class='' style='text-align: left;'>" + dbRow["DNI"].ToString() + "</td>";
                html += "<td class='' style='text-align: left;'>" + dbRow["Usuario"].ToString() + "</td>";
                html += "<td class='' style='text-align: left;'>" + dbRow["MES"].ToString() + "</td>";
                html += "<td class='bg-navy color-palette' style='text-align: center;'><b>" + dbRow["ARFSIS"].ToString() + "</b></td>";
                html += "<td class='bg-navy color-palette' style='text-align: center;'><b>" + dbRow["Ventanilla"].ToString() + "</b></td>";
                html += "<td class='bg-navy color-palette' style='text-align: center;'><b>" + dbRow["Anulados"].ToString() + "</b></td>";
                html += "<td class='bg-navy color-palette' style='text-align: center;'><b>" + dbRow["Validos"].ToString() + "</b></td>";
                string codtmp_modal = nroitem.ToString();
                html += "<td class='' style='text-align: left;'>" + "<button type='button' class='btn bg-navy' data-toggle='modal' data-target='#modAtenc-" + codtmp_modal + "'><i class='fa fa-fw fa-medkit'></i></button>" + "</td>";

                //html += "<td class='' style='text-align: center;'>" + dbRow["SIS"].ToString() + "</td>";
                //html += "<td class='' style='text-align: center;'>" + dbRow["USu"].ToString() + "</td>";
                //html += "<td class='' style='text-align: center;'>" + dbRow["Exo"].ToString() + "</td>";
                html += "</tr>" + Environment.NewLine;

                CargaDTAtenciones(DDLAnio.SelectedValue, DDLMes.SelectedValue, dbRow["DNI"].ToString(), dbRow["Usuario"].ToString(), codtmp_modal);
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
        LitAtenciones.Text = htmlAtenciones;
        //LitDetAtenciones.Text = htmlDetAtenciones;
    }

    public void CargaDTAtenciones(string vanio, string vmes, string DNI, string USUARIO, string codModal)
    {
        try
        {
            string qSql = "";
            if (DNI== "Ventanilla")
            {
                qSql = "select CAST(FEC_SIS AS DATE) ATENCION ,COUNT(*) TOTAL, 0 RETRASO, 0 mayor, 0 menor from nueva.dbo.sis INNER JOIN nueva.dbo.USUARIO ON Cod_Usu=USU_SIS where year(FEC_SIS)= '" + vanio + "'   and month(FEC_SIS)= '" + vmes + "'  and Des_Usu = '" + USUARIO + "' GROUP BY FEC_SIS ORDER BY FEC_SIS";
            }
            else
            {
                qSql = "select CAST(fechaReg AS DATE) ATENCION ,COUNT(*) TOTAL, AVG(CAST(fechaReg-FEC_SIS AS INT)) RETRASO ,max(cast(fechaReg-FEC_SIS AS INT)) mayor,min(cast(fechaReg-FEC_SIS AS INT)) menor from nueva.dbo.sis left join IROf.dbo.Arfsis on NFA_SIS=right(nroformato,6) where YEAR(fechaReg)= '" + vanio + "'  and month(fechaReg)= '" + vmes + "'  and Usuario = '" + USUARIO + "' GROUP BY fechaReg ORDER BY fechaReg";
            } 

                //select DIA, MT, COUNT(ID_HIS) as Atenciones, " +
                //"sum(case when FI='2' then 1 else 0 end) as SIS, sum(case when FI='1' then 1 else 0 end) as Usu, sum(case when FI='11' then 1 else 0 end) as Exo, sum(case when (FI='1' or FI='2' or FI='11') then 0 else 1 end) as otr " +
                //"from NUEVA.dbo.CHEQ2011 where ANO = '" + vanio + "' and MES = '" + vmes + "' and PLAZA = '" + vplaza + "' group by DIA, MT order by DIA";
                //select NUM_REG, DNI, EDAD, SEXO from NUEVA.dbo.CHEQ2011 where ANO = '2020' and MES = '4' and DIA = '17' and MT = 'M' and PLAZA = '10640796401';
            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text; SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet(); adapter.Fill(objdataset);
            conSAP00.Close();
            DataTable dtDatoAt = objdataset.Tables[0];

            CargaTablaAtenciones(dtDatoAt, vanio, vmes, DNI, USUARIO, codModal);
        }
        catch (Exception ex)
        {
            LitErrores.Text += ex.Message.ToString();
        }
    }

    public void CargaTablaAtenciones(DataTable dtDato, string vanio, string vmes, string DNI, string USUARIO,  string codModal)
    {
        if (dtDato.Rows.Count > 0)
        {
            htmlAtenciones += Environment.NewLine + "<div class='modal modal-info fade' id='modAtenc-" + codModal + "'>   ";
            htmlAtenciones += "  <div class='modal-dialog'>";
            htmlAtenciones += "    <div class='modal-content'>";
            htmlAtenciones += "      <div class='modal-header'>";
            htmlAtenciones += "        <button type='button' class='close' data-dismiss='modal' aria-label='Close'> ";
            htmlAtenciones += "          <span aria-hidden='true'>&times;</span></button>";
            htmlAtenciones += "        <h4 class='modal-title'>Año: " + vanio + " - Mes: " + vmes + " - | - Profesional: " + DNI + "-" + USUARIO + " </h4>";
            htmlAtenciones += "      </div>";
            htmlAtenciones += "      <div class='modal-body'>";
            htmlAtenciones += "        <p>";

            htmlAtenciones += "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
            htmlAtenciones += "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
            htmlAtenciones += "<tr>";
            htmlAtenciones += "<th class=''>N°</th>";
            htmlAtenciones += "<th class=''>ATENCION</th>";
            htmlAtenciones += "<th class=''>TOTAL</th>";
            htmlAtenciones += "<th class=''>RETRASO</th>";
            htmlAtenciones += "<th class='' style='text-align: center;'>MAYOR</th>";
            htmlAtenciones += "<th class='' style='text-align: center;'>MENOR</th>";
            htmlAtenciones += "<th class=''>Detalle</th>";

            //htmlAtenciones += "<th class=''>SIS</th>";
            //htmlAtenciones += "<th class=''>USU</th>";
            //htmlAtenciones += "<th class=''>EXO</th>";
            htmlAtenciones += "</tr>" + Environment.NewLine;
            int nroitem = 0;
            foreach (DataRow dbRow in dtDato.Rows)
            {
                nroitem += 1;
                htmlAtenciones += "<tr>";
                htmlAtenciones += "<td class='' style='text-align: left;'>" + nroitem + "</td>";
                htmlAtenciones += "<td class='' style='text-align: left;'>" + dbRow["ATENCION"].ToString().Substring(0,10) + "</td>";
                htmlAtenciones += "<td class='' style='text-align: left;'>" + dbRow["TOTAL"].ToString() + "</td>";
                htmlAtenciones += "<td class='' style='text-align: left;'>" + dbRow["RETRASO"].ToString() + "</td>";
                htmlAtenciones += "<td class='bg-navy color-palette' style='text-align: center;'><b>" + dbRow["MAYOR"].ToString() + "</b></td>";
                htmlAtenciones += "<td class='bg-navy color-palette' style='text-align: center;'><b>" + dbRow["MENOR"].ToString() + "</b></td>";
                string codtmp_modal = codModal + "-" + nroitem.ToString();
                htmlAtenciones += "<td class='' style='text-align: left;'>" + "<button type='button' class='btn bg-navy' data-toggle='modal' data-target='#modalDetAtenciones'  onclick=\"DetAtenciones('" + dbRow["ATENCION"].ToString().Substring(0, 10) + "', '" + USUARIO + "')\"><i class='fa fa-fw fa-medkit'></i></button>" + "</td>";
                //htmlAtenciones += "<td class='' style='text-align: left;'>" + "<button type='button' class='btn bg-navy' data-toggle='modal' onclick=\"cargaContenido('" + vanio + "', '" + vmes + "', '" + dbRow["DIA"].ToString() + "', '" + dbRow["MT"].ToString() + "', '" + dbRow["PLAZA"].ToString() + "')\"><i class='fa fa-fw fa-medkit'></i></button>" + "</td>";
                //DetAtenciones(vanio, vmes, vdia, turno, vplaza)

                //htmlAtenciones += "<td class='' style='text-align: center;'>" + dbRow["SIS"].ToString() + "</td>";
                //htmlAtenciones += "<td class='' style='text-align: center;'>" + dbRow["Usu"].ToString() + "</td>";
                //htmlAtenciones += "<td class='' style='text-align: center;'>" + dbRow["Exo"].ToString() + "</td>";

                htmlAtenciones += "</tr>" + Environment.NewLine;

                //CargaDTDetAtenciones(vanio, vmes, dbRow["DIA"].ToString(), dbRow["MT"].ToString(), vplaza, codtmp_modal);
            }

            htmlAtenciones += "</table></div></div><hr style='border-top: 1px solid blue'>";

            htmlAtenciones += "		</p>";
            htmlAtenciones += "      </div>";
            htmlAtenciones += "      <div class='modal-footer'>";
            htmlAtenciones += "        <button type='button' class='btn btn-outline pull-left' data-dismiss='modal'>Close</button> ";
            htmlAtenciones += "      </div>";
            htmlAtenciones += "    </div>";
            htmlAtenciones += "  </div>";
            htmlAtenciones += "</div>" + Environment.NewLine;
        }
        //// cargando aparte los datos
        //if (dtDato.Rows.Count > 0)
        //{
        //    int nroitem = 0;
        //    foreach (DataRow dbRow in dtDato.Rows)
        //    {
        //        nroitem += 1;
        //        string codtmp_modal = codModal + "-" + nroitem.ToString();

        //        LitErrores.Text += Environment.NewLine + codtmp_modal + "|" + vplaza + "-" + codModal + "-";

        //        CargaDTDetAtenciones(vanio, vmes, dbRow["DIA"].ToString(), dbRow["MT"].ToString(), dbRow["PLAZA"].ToString(), codtmp_modal);
        //    }
        //}
    }

    public void CargaDTDetAtenciones(string vanio, string vmes, string vdia, string turno, string vplaza, string codModal)
    {
        LitErrores.Text += Environment.NewLine + vplaza + "-" + codModal + "-";
        try
        {
            SqlCommand cmd = new SqlCommand("NUEVA.dbo.SP_JVAR_HISMINSA_DETATENCIONES", conSAP00);
            cmd.CommandType = CommandType.StoredProcedure;
            //creamos los parametros que usaremos
            cmd.Parameters.Add("@vanio", SqlDbType.VarChar);
            cmd.Parameters.Add("@vmes", SqlDbType.VarChar);
            cmd.Parameters.Add("@vdia", SqlDbType.VarChar);
            cmd.Parameters.Add("@vturno", SqlDbType.VarChar);
            cmd.Parameters.Add("@vplaza", SqlDbType.VarChar);
            //asignamos el valor de los textbox a los parametros
            cmd.Parameters["@vanio"].Value = vanio;
            cmd.Parameters["@vmes"].Value = vmes;
            cmd.Parameters["@vdia"].Value = vdia;
            cmd.Parameters["@vturno"].Value = turno;
            cmd.Parameters["@vplaza"].Value = vplaza;
            //adapter, para asignarle el cmd, command

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDatoDetAt = objdataset.Tables[0];

            //CargaTablaDetAtenciones(dtDatoDetAt, vanio, vmes, vdia, turno, vplaza, codModal, "");
        }
        catch (Exception ex)
        {
            LitErrores.Text += vplaza + "-" + codModal + "-" + ex.Message.ToString();
        }
    }

    public void CargaDTDetAtenciones2(string vanio, string vmes, string vdia, string turno, string vplaza, string codModal)
    {
        LitErrores.Text += Environment.NewLine + vplaza + "-" + codModal + "-";
        try
        {
            string qSql = "select NUM_FRT, NUM_PAG, NUM_REG, SUBSTRING(DNI, 5, 8) as DNI, IFN, IAP + ' ' + IAM + ' ' + INO as Nombres, EDAD, SEXO from NUEVA.dbo.CHEQ2011 left join NUEVA.dbo.HISTORIA on FICHAFAM = IHC where ANO = '" + vanio + "' and MES = '" + vmes + "' and DIA = '" + vdia + "' and MT = '" + turno + "' and PLAZA = '" + vplaza + "' order by NUM_FRT, NUM_PAG, NUM_REG; ";
            //select NUM_FRT, NUM_PAG, NUM_REG, SUBSTRING(DNI, 5, 8) as DNI, IFN, IAP + IAM + INO as Nombres, EDAD, SEXO from NUEVA.dbo.CHEQ2011 left join NUEVA.dbo.HISTORIA on FICHAFAM = IHC where ANO = '2020' and MES = '5' and DIA = '20' and MT = 'M' and PLAZA = '11814837906' order by NUM_FRT, NUM_PAG, NUM_REG;
            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = cmd2;

            conSAP00.Open();
            DataSet objdataset2 = new DataSet(); adapter2.Fill(objdataset2);
            conSAP00.Close();
            DataTable dtDatoDetAt = objdataset2.Tables[0];

            qSql = "select NUM_FRT, NUM_PAG, NUM_REG, SUBSTRING(DNI, 5, 8) as DNI, IFN, IAP + IAM + INO as Nombres, EDAD, SEXO from NUEVA.dbo.CHEQ2011 left join NUEVA.dbo.HISTORIA on FICHAFAM = IHC where where ANO = " + vanio + " and MES = " + vmes + " and DIA = " + vdia + " and MT = " + turno + " and PLAZA = " + vplaza + " order by NUM_FRT, NUM_PAG, NUM_REG; ";

            //CargaTablaDetAtenciones(dtDatoDetAt, vanio, vmes, vdia, turno, vplaza, codModal, "");
        }
        catch (Exception ex)
        {
            LitErrores.Text += vplaza + "-" + codModal + "-" + ex.Message.ToString();
        }
    }

    public string CargaTablaDetAtenciones(DataTable dtDato, string vanio, string vmes, string vdia, string turno, string vplaza)
    {
        //LitDetAtenciones.Text = "";
        if (dtDato.Rows.Count > 0)
        {
            htmlDetAtenciones += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
            htmlDetAtenciones += "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
            htmlDetAtenciones += "<tr>";
            htmlDetAtenciones += "<th class=''>N° </th>";
            htmlDetAtenciones += "<th class=''>FRT</th>";
            htmlDetAtenciones += "<th class=''>Pag</th>";
            htmlDetAtenciones += "<th class=''>Reg</th>";
            htmlDetAtenciones += "<th class=''>DNI</th>";
            htmlDetAtenciones += "<th class=''>Cliente</th>";
            htmlDetAtenciones += "<th class=''>Edad</th>";
            htmlDetAtenciones += "<th class=''>Sexo</th>";
            htmlDetAtenciones += "</tr>" + Environment.NewLine;
            int nroitem = 0;
            foreach (DataRow dbRow in dtDato.Rows)
            {
                nroitem += 1;
                htmlDetAtenciones += "<tr>";
                htmlDetAtenciones += "<td class='' style='text-align: left;'>" + nroitem + "</td>";
                htmlDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["NUM_FRT"].ToString() + "</td>";
                htmlDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["NUM_PAG"].ToString() + "</td>";
                htmlDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["NUM_REG"].ToString() + "</td>";
                htmlDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["DNI"].ToString() + "</td>";
                htmlDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["Nombres"].ToString() + "</td>";
                htmlDetAtenciones += "<td class='' style='text-align: center;'>" + dbRow["EDAD"].ToString() + "</td>";
                htmlDetAtenciones += "<td class='' style='text-align: center;'>" + dbRow["SEXO"].ToString() + "</td>";
                htmlDetAtenciones += "</tr>" + Environment.NewLine;
            }

            htmlDetAtenciones += "</table></div></div><hr style='border-top: 1px solid blue'>";

        }
        //LitDetAtenciones.Text += htmlDetAtenciones;
        return htmlDetAtenciones;
    }

    protected void ExportarExcel2_Click(object sender, EventArgs e)
    {
        HttpResponse response = Response;
        response.Clear();
        response.Buffer = true;
        response.ContentType = "application/vnd.ms-excel";
        response.AddHeader("Content-Disposition", "attachment;filename=Reporte.xls");
        response.Charset = "UTF-8";
        response.ContentEncoding = Encoding.Default;
        response.Write(LitTABL1.Text);
        response.End();
    }

    protected void ExportarExcel_Click(object sender, EventArgs e)
    {
        HttpResponse response = Response;
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        Page pageToRender = new Page();
        HtmlForm form = new HtmlForm();
        form.Controls.Add(GVtable);
        pageToRender.Controls.Add(form);
        response.Clear();
        response.Buffer = true;
        response.ContentType = "application/vnd.ms-excel";
        response.AddHeader("Content-Disposition", "attachment;filename=Reporte.xls");
        response.Charset = "UTF-8";
        response.ContentEncoding = Encoding.Default;
        pageToRender.RenderControl(htw);
        response.Write(sw.ToString());
        response.End();
    }

}