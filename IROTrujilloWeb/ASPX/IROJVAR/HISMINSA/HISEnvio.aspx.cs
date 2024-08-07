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

public partial class ASPX_IROJVAR_HISMINSA_HISEnvio : System.Web.UI.Page
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
            //html = "";
            //htmlAtenciones = "";
            //htmlDetAtenciones = "";
            int vmonth = DateTime.Now.Month;
            int vyear = DateTime.Now.Year;
            DDLMes.SelectedIndex = vmonth;
            DDLAnio.Text = vyear.ToString();
        }

    }

    protected void bntBuscar_Click(object sender, EventArgs e)
    {
        ProcesaHISMINSA();
    }

    public void ProcesaHISMINSA()
    {
        string vAnio = DDLAnio.SelectedValue;
        string vMes = DDLMes.SelectedValue;
        try
        {
            //con.Open();
            string qSql = "select (case when diaedadant < 0 then diaedadant + 30 else diaedadant end ) diaedad, " +
                "(case when (diaedadant < 0 and mesedadant = 0) then 11 else ( case when diaedadant < 0 then mesedadant - 1 else mesedadant end ) end ) mesedad, " +
                "(case when LABCONF1 IN ('1', '2', '3', '4', '5') then LABCONF1 else '0' end ) LABCONF1m, " +
                "(case when LABCONF2 IN ('1', '2', '3', '4', '5') then LABCONF2 else '0' end ) LABCONF2m, " +
                "(case when LABCONF3 IN ('1', '2', '3', '4', '5') then LABCONF3 else '0' end ) LABCONF3m, " +
                "(case when LABCONF4 IN ('1', '2', '3', '4', '5') then LABCONF4 else '0' end ) LABCONF4m, " +
                "(case when LABCONF5 IN ('1', '2', '3', '4', '5') then LABCONF5 else '0' end ) LABCONF5m, " +
                "(case when LABCONF6 IN ('1', '2', '3', '4', '5') then LABCONF6 else '0' end ) LABCONF6m, " +
                "* " +
                "from ( " +
                "select id_his, '' numeroafiliacion, " +
                "cast(ano as varchar) + format(mes, '00') + format(dia ,'00') as fechaatencion, 'A' estadoregistro, " +
                "LABCONF1, DIAGNOST1, LEFT(CODI1, 5) CODI1, " +
                "(select top 1 (case when TABLA = 'CPT' then 'CP' else 'CX' end ) from NUEVA.dbo.enfermedades enf1 where enf1.COD_ENF = LEFT(CODI1, 5)) tipoitem1, " +
                "LABCONF2, DIAGNOST2, LEFT(CODI2, 5) CODI2, " +
                "(select top 1 (case when TABLA = 'CPT' then 'CP' else 'CX' end ) from NUEVA.dbo.enfermedades enf2 where enf2.COD_ENF = LEFT(CODI2, 5)) tipoitem2, " +
                "LABCONF3, DIAGNOST3, LEFT(CODI3, 5) CODI3, " +
                "(select top 1 (case when TABLA = 'CPT' then 'CP' else 'CX' end ) from NUEVA.dbo.enfermedades enf3 where enf3.COD_ENF = LEFT(CODI3, 5)) tipoitem3, " +
                "LABCONF4, DIAGNOST4, LEFT(CODI4, 5) CODI4, " +
                "(select top 1 (case when TABLA = 'CPT' then 'CP' else 'CX' end ) from NUEVA.dbo.enfermedades enf4 where enf4.COD_ENF = LEFT(CODI4, 5)) tipoitem4, " +
                "LABCONF5, DIAGNOST5, LEFT(CODI5, 5) CODI5, " +
                "(select top 1 (case when TABLA = 'CPT' then 'CP' else 'CX' end ) from NUEVA.dbo.enfermedades enf5 where enf5.COD_ENF = LEFT(CODI5, 5)) tipoitem5, " +
                "LABCONF6, DIAGNOST6, LEFT(CODI6, 5) CODI6, " +
                "(select top 1 (case when TABLA = 'CPT' then 'CP' else 'CX' end ) from NUEVA.dbo.enfermedades enf6 where enf6.COD_ENF = LEFT(CODI6, 5)) tipoitem6, " +
                "COD_SERVSA, CAST(COD_2000 AS int) COD_2000, " +
                "DATEPART(DAY, cast(DIA as varchar)+'/'+cast(MES as varchar)+'/'+cast(ANO as varchar)) - DATEPART(DAY, IFN) diaedadant, " +
                "DATEDIFF(MONTH, IFN, cast(DIA as varchar)+'/'+cast(MES as varchar)+'/'+cast(ANO as varchar))%12 mesedadant, " +
                "FLOOR(DATEDIFF(day, IFN, cast(DIA as varchar)+'/'+cast(MES as varchar)+'/'+cast(ANO as varchar)) / 365.25) annioedad, " +
                "year(getdate())-YEAR(IFN) edadregistro, MT idturno, TIP_EDAD idtipedadregistro, FI, " +
                "SUBSTRING(CODIF,2,8) per_nrodocumento, 'MAURO' per_nombres, 'PAZ' per_apepaterno, 'PORRAS' per_apematerno, 'PER' per_idpais, '42' per_idprofesion,'1' per_idtipodoc, 'M' per_idsexo, '2' per_idcondicion, " +
                "SUBSTRING(PLAZA,2,8) pea_nrodocumento, APP_PER pea_apepaterno, APM_PER pea_apematerno, NOM_PER pea_nombres, 'PER' pea_idpais, '13' pea_idprofesion, cast(year(FEN_PER) as varchar) + format(month(FEN_PER), '00') + format(day(FEN_PER), '00') as pea_fechanacimiento, '1' pea_idtipodoc, 'M' pea_idsexo, '2' pea_idcondicion, " +
                "SUBSTRING(dni,5,8) pac_nrodocumento, '5' pac_idflag, iap pac_apepaterno, iam pac_apematerno, ino pac_nombres, FICHAFAM pac_NHC, '1' pac_idtipodoc, '58' pac_etnia, " +
                "cast(year(ifn) as varchar) + format(month(ifn), '00') + format(day(ifn) ,'00') pac_fechanacimiento, CAST(cod_2000 AS int) pac_idestablecimiento, 'PER' pac_idpais, sexo pac_idsexo , PLAZA " +
                "from NUEVA.dbo.CHEQ2011 " +
                "LEFT JOIN NUEVA.dbo.HISTORIA ON RIGHT('000000' + Ltrim(Rtrim(FICHAFAM)),6)=IHC " +
                "LEFT JOIN NUEVA.dbo.PERSONAL ON PLAZA=HIS_PER " +
                "where ANO = " + vAnio + " and MES = " + vMes +
                ") tabHISMINSA " +
                "where pac_apepaterno is not null " +
                "and ID_HIS not in (select V_CITA_ORIGEN from NUEVA.dbo.TRAMA_INTEGRACION_HISMINSA_JSON)";


            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];

            CargaTablaDT(dtDato);
            CargaTabla(dtDato);
        }
        catch (Exception ex)
        {
            //Label1.Text = "Error";
            ex.Message.ToString();
            LitErrores.Text += ex.Message.ToString() + "<br />";
        }
    }

    public void CargaTablaDT(DataTable dtDato)
    {
        if (dtDato.Rows.Count > 0)
        {
            //LitErrores.Text += "<br />" + "Informacion: " + "<br />";
            int numrow = 0;
            foreach (DataRow dbRow in dtDato.Rows)
            {
                numrow += 1;
                //LitErrores.Text += "<br />" + numrow + "° Json: " + "<br />";
                string varJSonHISMINSA = "{ " +
                    "\"cita\": {                                     " +
                    "\"numeroafiliacion\": \"" + dbRow["numeroafiliacion"].ToString() + "\", " +
                    "\"idcita\": \"" + dbRow["id_his"].ToString() + "\", " +
                    "\"fechaatencion\": \"" + dbRow["fechaatencion"].ToString() + "\",       " +
                    "\"estadoregistro\": \"" + dbRow["estadoregistro"].ToString() + "\",     " +
                    "\"items\": [   " +
                    "{ " +
                    "\"labs\": [  ";
                if (dbRow["DIAGNOST1"].ToString().Trim() != "")
                {
                    varJSonHISMINSA += "{  " +
                    "\"codigo\": \"\",  " +
                    "\"valor\": \"" + dbRow["LABCONF1m"].ToString().Trim() + "\"  " +
                    "} ";
                }
                varJSonHISMINSA += "], " +
                    "\"tipodiagnostico\": \"" + dbRow["DIAGNOST1"].ToString() + "\", " +
                    "\"codigo\": \"" + dbRow["CODI1"].ToString() + "\",          " +
                    "\"tipoitem\": \"" + dbRow["tipoitem1"].ToString() + "\"         " +
                    "}";
                if (dbRow["DIAGNOST2"].ToString().Trim() != "")
                {
                    varJSonHISMINSA += ", {  " +
                        "\"labs\": [  ";
                    varJSonHISMINSA += " { " +
                    "\"codigo\": \"\",  " +
                    "\"valor\": \"" + dbRow["LABCONF2m"].ToString().Trim() + "\"  " +
                    "} ";
                    varJSonHISMINSA += "], " +
                        "\"tipodiagnostico\": \"" + dbRow["DIAGNOST2"].ToString() + "\", " +
                        "\"codigo\": \"" + dbRow["CODI2"].ToString() + "\",          " +
                        "\"tipoitem\": \"" + dbRow["tipoitem2"].ToString() + "\"         " +
                        "} ";
                }
                if (dbRow["DIAGNOST3"].ToString().Trim() != "")
                {
                    varJSonHISMINSA += ", {  " +
                        "\"labs\": [  ";
                    varJSonHISMINSA += " { " +
                        "\"codigo\": \"\",  " +
                        "\"valor\": \"" + dbRow["LABCONF3m"].ToString().Trim() + "\"  " +
                        "} ";
                    varJSonHISMINSA += "], " +
                    "\"tipodiagnostico\": \"" + dbRow["DIAGNOST3"].ToString() + "\", " +
                    "\"codigo\": \"" + dbRow["CODI3"].ToString() + "\",          " +
                    "\"tipoitem\": \"" + dbRow["tipoitem3"].ToString() + "\"         " +
                    "} ";
                }
                if (dbRow["DIAGNOST4"].ToString().Trim() != "")
                {
                    varJSonHISMINSA += ", {  " +
                        "\"labs\": [  ";
                    varJSonHISMINSA += " { " +
                        "\"codigo\": \"\",  " +
                        "\"valor\": \"" + dbRow["LABCONF4m"].ToString().Trim() + "\"  " +
                        "} ";
                    varJSonHISMINSA += "], " +
                        "\"tipodiagnostico\": \"" + dbRow["DIAGNOST4"].ToString() + "\", " +
                        "\"codigo\": \"" + dbRow["CODI4"].ToString() + "\",          " +
                        "\"tipoitem\": \"" + dbRow["tipoitem4"].ToString() + "\"         " +
                        "} ";
                }
                if (dbRow["DIAGNOST5"].ToString().Trim() != "")
                {
                    varJSonHISMINSA += ", {  " +
                        "\"labs\": [  ";
                    varJSonHISMINSA += " { " +
                        "\"codigo\": \"\",  " +
                        "\"valor\": \"" + dbRow["LABCONF5m"].ToString().Trim() + "\"  " +
                        "} ";
                    varJSonHISMINSA += "], " +
                        "\"tipodiagnostico\": \"" + dbRow["DIAGNOST5"].ToString() + "\", " +
                        "\"codigo\": \"" + dbRow["CODI5"].ToString() + "\",          " +
                        "\"tipoitem\": \"" + dbRow["tipoitem5"].ToString() + "\"         " +
                        "} ";
                }
                if (dbRow["DIAGNOST6"].ToString().Trim() != "")
                {
                    varJSonHISMINSA += ", {  " +
                        "\"labs\": [  ";
                    varJSonHISMINSA += " { " +
                        "\"codigo\": \"\",  " +
                        "\"valor\": \"" + dbRow["LABCONF6m"].ToString().Trim() + "\"  " +
                        "} ";
                    varJSonHISMINSA += "], " +
                        "\"tipodiagnostico\": \"" + dbRow["DIAGNOST6"].ToString() + "\", " +
                        "\"codigo\": \"" + dbRow["CODI6"].ToString() + "\",          " +
                        "\"tipoitem\": \"" + dbRow["tipoitem6"].ToString() + "\"         " +
                        "} ";
                }
                string var_idups = dbRow["COD_SERVSA"].ToString();
                if (int.Parse(var_idups) >= 303413)
                {
                    var_idups = "303408";
                }
                varJSonHISMINSA += " ], " +
        "\"idups\": \"" + var_idups + "\",               " +
        "\"idestablecimiento\": \"" + dbRow["COD_2000"].ToString() + "\",   " +
        "\"diaedad\": \"" + dbRow["diaedad"].ToString() + "\",             " +
        "\"mesedad\": \"" + dbRow["mesedad"].ToString() + "\",             " +
        "\"annioedad\": \"" + dbRow["annioedad"].ToString() + "\",           " +
        "\"edadregistro\": \"" + dbRow["edadregistro"].ToString() + "\",        " +
        "\"idturno\": \"" + dbRow["idturno"].ToString() + "\",             " +
        "\"idtipedadregistro\": \"" + dbRow["idtipedadregistro"].ToString().Trim() + "\",  " +
        "\"fgdiag\": \"21\",              " +
        "\"componente\": \"\",          " +
        "\"idfinanciador\": \"" + dbRow["FI"].ToString().Trim() + "\",       " +
        "\"examenfisico\": {" +
                "\"peso\": \"0.00\", " +
                "\"talla\": \"0.00\", " +
                "\"hemoglobina\": \"0.00\", " +
                "\"perimetrocefalico\": \"0\", " +
                "\"perimetroabdominal\":\"0\" " +
            "}" +
        "}, " +
        "\"personal_registra\": { " +
        "\"nrodocumento\": \"" + dbRow["per_nrodocumento"].ToString() + "\",   " +
        "\"nombres\": \"" + dbRow["per_nombres"].ToString() + "\",             " +
        "\"apepaterno\": \"" + dbRow["per_apepaterno"].ToString() + "\",       " +
        "\"apematerno\": \"" + dbRow["per_apematerno"].ToString() + "\",       " +
        "\"idpais\": \"" + dbRow["per_idpais"].ToString() + "\",               " +
        "\"idprofesion\": \"" + dbRow["per_idprofesion"].ToString() + "\",     " +
        "\"fechanacimiento\": \"19910314\",       " +
        "\"idtipodoc\": \"" + dbRow["per_idtipodoc"].ToString() + "\",         " +
        "\"idsexo\": \"" + dbRow["per_idsexo"].ToString() + "\",               " +
        "\"idcondicion\": \"" + dbRow["per_idcondicion"].ToString() + "\"      " +
        "},                                              " +
        "\"personal_atiende\": {                         " +
        "\"nrodocumento\": \"" + dbRow["pea_nrodocumento"].ToString() + "\",   " +
        "\"apepaterno\": \"" + dbRow["pea_apepaterno"].ToString() + "\",       " +
        "\"apematerno\": \"" + dbRow["pea_apematerno"].ToString() + "\",       " +
        "\"nombres\": \"" + dbRow["pea_nombres"].ToString() + "\",             " +
        "\"idpais\": \"" + dbRow["pea_idpais"].ToString() + "\",               " +
        "\"idprofesion\": \"" + dbRow["pea_idprofesion"].ToString() + "\",     " +
        "\"fechanacimiento\": \"" + dbRow["pea_fechanacimiento"].ToString() + "\", " +
        "\"idtipodoc\": \"" + dbRow["pea_idtipodoc"].ToString() + "\",         " +
        "\"idsexo\": \"" + dbRow["pea_idsexo"].ToString() + "\",               " +
        "\"idcondicion\": \"" + dbRow["pea_idcondicion"].ToString() + "\"      " +
        "},                                              " +
        "\"paciente\": {                                 " +
        "\"nrodocumento\": \"" + dbRow["pac_nrodocumento"].ToString() + "\",   " +
        "\"apepaterno\": \"" + dbRow["pac_apepaterno"].ToString() + "\",       " +
        "\"apematerno\": \"" + dbRow["pac_apematerno"].ToString() + "\",       " +
        "\"nombres\": \"" + dbRow["pac_nombres"].ToString() + "\",             " +
        "\"idflag\": \"5\",               " +
        "\"nrohistoriaclinica\": \"" + dbRow["pac_NHC"].ToString() + "\",     " +
        "\"idtipodoc\": \"" + dbRow["pac_idtipodoc"].ToString() + "\",         " +
        "\"idetnia\": \"" + dbRow["pac_etnia"].ToString().Trim() + "\",               " +
        "\"fechanacimiento\": \"" + dbRow["pac_fechanacimiento"].ToString() + "\", " +
        "\"idestablecimiento\": \"" + dbRow["pac_idestablecimiento"].ToString() + "\", " +
        "\"idpais\": \"" + dbRow["pac_idpais"].ToString() + "\",                 " +
        "\"idsexo\": \"" + dbRow["pac_idsexo"].ToString() + "\"                  " +
        "} " +
        "}    ";
                //LitErrores.Text += varJSonHISMINSA + "<br />";
                GuardaEnTabla(varJSonHISMINSA, dbRow["id_his"].ToString());
            }
        }
        else
        {
            LitErrores.Text += "<br />" + "Sin Informacion " + "<br />";
        }
    }

    public void GuardaEnTabla(string varJSon, string NroCita)
    {
        try
        {
            //con.Open();
            string consql = "insert into NUEVA.dbo.TRAMA_INTEGRACION_HISMINSA_JSON (V_CITA_ORIGEN, V_JSON) Values (@V_CITA_ORIGEN, @V_JSON) SELECT @@Identity; ";
            SqlCommand cmd = new SqlCommand(consql, conSAP00);
            cmd.CommandType = CommandType.Text;
            //asignamos el valor de los textbox a los parametros
            cmd.Parameters.AddWithValue("@V_CITA_ORIGEN", NroCita);
            cmd.Parameters.AddWithValue("@V_JSON", varJSon);

            conSAP00.Open();
            //cmd.ExecuteScalar();
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            conSAP00.Close();
            //LitToken.Text += " ||--> Nro Reg.: " + count + "<br />";
            //this.Page.Response.Write("<script language='JavaScript'>window.alert('Registro Guardado: " + count + "');window.location.assign('../PruSave/Guardar.aspx');</script>");
        }
        catch (Exception ex)
        {
            //Label1.Text = "Error";
            //ex.Message.ToString();
            LitErrores.Text += "<br />" + ex.Message.ToString() + "<br />";
            if (conSAP00.State == ConnectionState.Open) { conSAP00.Close(); }
        }
    }

    public void CargaTabla(DataTable dtDato)
    {
        html = "";
        if (dtDato.Rows.Count > 0)
        {
            //html += "<table class='table table-condensed table-striped table-hover' style='text-align: right; font-size: 14px; '>";
            html += Environment.NewLine + "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
            html += "<tr>";
            html += "<th class=''></th>";
            html += "<th class=''>Cant Reg.</th>";
            html += "<th class=''>Cant Hom</th>";
            html += "<th class=''>Cant Muj</th>";
            html += "<th class='' >Cant Mañ</th>";
            html += "<th class=''>Cant Tar</th>";
            html += "<th class='' >Cant FI1</th>";
            html += "<th class=''>Cant FI2</th>";

            html += "</tr>" + Environment.NewLine;

            html += "<tr>";
            html += "<td class='' ></td>";
            html += "<td class='' >" + dtDato.Compute("COUNT(id_his)", "").ToString() + "</td>";
            html += "<td>" + dtDato.Compute("COUNT(id_his)", "pac_idsexo='M'").ToString() + "</td>";
            html += "<td>" + dtDato.Compute("COUNT(id_his)", "pac_idsexo='F'").ToString() + "</td>";
            html += "<td>" + dtDato.Compute("COUNT(id_his)", "idturno='M'").ToString() + "</td>";
            html += "<td>" + dtDato.Compute("COUNT(id_his)", "idturno='T'").ToString() + "</td>";
            html += "<td>" + dtDato.Compute("COUNT(id_his)", "FI='1'").ToString() + "</td>";
            html += "<td>" + dtDato.Compute("COUNT(id_his)", "FI='2'").ToString() + "</td>";
            html += "</tr>" + Environment.NewLine;

            html += "</table><hr style='border-top: 1px solid blue'>";
        }
        else
        {
            html += "<table>";
            html += "<tr><td class='FieldCaption' colspan=3>Sin registros encontrados</td></tr>";
            html += "</table><hr>";
        }
        LitTABL1.Text = html;
    }
    
    protected void ExportarExcel_Click(object sender, EventArgs e)
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

    protected void ExportarExcel2_Click(object sender, EventArgs e)
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
