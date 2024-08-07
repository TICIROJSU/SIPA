using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ASPX_IROJVAR_HISMINSA_AtCirugVsCons : System.Web.UI.Page
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
            //int vmonth = DateTime.Now.Month;
            //DDLMes.SelectedIndex = vmonth;
            int vyear = DateTime.Now.Year;
            DDLAnio.Text = vyear.ToString();
            int vmonth = DateTime.Now.Month;
            //DDLMes.SelectedIndex = vmonth;
            CargaTablaDTIni();
        }

    }

    [WebMethod]
    public static string GetDetEspecialidadxMes(string vanio, string vmes, string vmesnom)
    {
        DateTimeFormatInfo formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetHTML = "";
        try
        {
            string vMesHIS = "AND MES=@vMes ", vMesUPE = "AND MONTH(FEC_UPE)=@vMes ", vMesCx = "AND MES=@vMes ";
            if (vmes == "13")
            {
                vMesHIS = ""; vMesUPE = ""; vMesCx = "";
            }
            string qSql = "select COD_PER, HIS_PER, APE_PER, " +
                    "	sum(case when Area = 'HIS' then TOTAL else 0 end) HIS, " +
                    "	sum(case when Area = 'UPE' then TOTAL else 0 end) UPE, " +
                    "	sum(case when Area = 'Cx' then TOTAL else 0 end) Cx, " +
                    "	sum(TOTAL) as Total " +
                    "from ( " +
                    "	SELECT 'HIS' Area, COD_PER, HIS_PER, APE_PER,COUNT(*) TOTAL FROM NUEVA.dbo.CHEQ2011 " +
                    "	INNER JOIN NUEVA.dbo.PERSONAL ON HIS_PER=PLAZA " +
                    "	WHERE ANO=@vAnio " + vMesHIS + " AND COD_PRO IN ('01','02') " +
                    "	GROUP BY DIA, APE_PER, COD_PER, HIS_PER " +
                    "	union " +
                    "	SELECT 'UPE' Area, COD_PER, HIS_PER, APE_PER ,COUNT(*) TOTAL FROM NUEVA.dbo.UPE " +
                    "	INNER JOIN NUEVA.dbo.PERSONAL ON COD_PER=RES_UPE " +
                    "	WHERE YEAR(FEC_UPE)=@vAnio " + vMesUPE + " AND COD_PRO IN ('01','02') " +
                    "	GROUP BY DAY(FEC_UPE), APE_PER, COD_PER, HIS_PER " +
                    "	union " +
                    "	SELECT 'Cx' Area, PERSONAL.COD_PER, PERSONAL.HIS_PER, APE_PER, COUNT(Op_Realizada) AS Total " +
                    "	FROM Estadistica.dbo.Librosop " +
                    "	left join NUEVA.dbo.PERSONAL ON Librosop.coD_per=PERSONAL.COD_PER " +
                    "	WHERE ANO = @vAnio " + vMesCx + " and COD_PRO IN ('01','02') " +
                    "	GROUP BY PERSONAL.COD_PER, PERSONAL.HIS_PER, APE_PER " +
                    ") T01 " +
                    "GROUP BY COD_PER, APE_PER, HIS_PER " +
                    "order by APE_PER ";
            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
            cmd2.Parameters.AddWithValue("@vAnio", vanio);
            cmd2.Parameters.AddWithValue("@vMes", vmes);

            adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            DataTable dtDato = objdataset.Tables[0];

            if (dtDato.Rows.Count > 0)
            {
                gDetHTML += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetHTML += "<table class='table table-hover' style='text-align: right; font-size: 14px; ' id='tblEspecialid'>";
                gDetHTML += "<caption>" + vanio + " | " + formatoFecha.GetMonthName(Convert.ToInt32(vmes)) + "</caption>";
                gDetHTML += "<tr>";
                gDetHTML += "<th class=''>Especialidad </th>";
                gDetHTML += "<th class=''>HIS</th>";
                gDetHTML += "<th class=''>UPE</th>";
                gDetHTML += "<th class=''>Cx</th>";
                gDetHTML += "<th class=''>Total</th>";
                gDetHTML += "</tr>" + Environment.NewLine;
                gDetHTML += "<tr>";
                double vt_totHIS = Convert.ToDouble(dtDato.Compute("sum(HIS)", String.Empty));
                double vt_totUPE = Convert.ToDouble(dtDato.Compute("sum(UPE)", String.Empty));
                double vt_totCx = Convert.ToDouble(dtDato.Compute("sum(Cx)", String.Empty));
                double vt_total = Convert.ToDouble(dtDato.Compute("sum(total)", String.Empty));
                gDetHTML += "<td class='text-center'><b>Todos</b></td>";
                gDetHTML += "<td class='text-center'><b>" + vt_totHIS + "</b></td>";
                gDetHTML += "<td class='text-center'><b>" + vt_totUPE + "</b></td>";
                gDetHTML += "<td class='text-center'><b>" + vt_totCx + "</b></td>";
                gDetHTML += "<td class='text-center'><b>" + vt_total + "</b></td>";
                gDetHTML += "</tr>" + Environment.NewLine;
                foreach (DataRow dbRow in dtDato.Rows)
                {
                    double v_CantHIS = Convert.ToDouble(dbRow["HIS"]);
                    double v_CantUPE = Convert.ToDouble(dbRow["UPE"]);
                    double v_CantCx = Convert.ToDouble(dbRow["Cx"]);
                    double v_CantTotal = Convert.ToDouble(dbRow["total"]);
                    string v_MesTotalHIS = ClassGlobal.formatoMillarDec((v_CantHIS / vt_totHIS * 100).ToString());
                    string v_MesTotalUPE = ClassGlobal.formatoMillarDec((v_CantUPE / vt_totUPE * 100).ToString());
                    string v_MesTotalCx = ClassGlobal.formatoMillarDec((v_CantCx / vt_totCx * 100).ToString());
                    string v_MesTotal = ClassGlobal.formatoMillarDec((v_CantTotal / vt_total * 100).ToString());
                    gDetHTML += "<tr onclick=\"fPacEspeMes('" + vanio + "', '" + vmes + "', '" + vmesnom + "', '" + dbRow["COD_PER"].ToString() + "', this, '" + dbRow["HIS_PER"].ToString() + "', '" + dbRow["APE_PER"].ToString() + "')\">";
                    gDetHTML += "<td class='' style='text-align: left;'>" + dbRow["APE_PER"].ToString() + "</td>";
                    gDetHTML += "<td class='' style='text-align: right;'>" + dbRow["HIS"].ToString() + " ( " + v_MesTotalHIS + "% ) " + "</td>";
                    gDetHTML += "<td class='' style='text-align: right;'>" + dbRow["UPE"].ToString() + " ( " + v_MesTotalUPE + "% ) " + "</td>";
                    gDetHTML += "<td class='' style='text-align: right;'>" + dbRow["Cx"].ToString() + " ( " + v_MesTotalCx + "% ) " + "</td>";
                    gDetHTML += "<td class='' style='text-align: right;'>" + dbRow["total"].ToString() + " ( " + v_MesTotal + "% ) " + "</td>";
                    gDetHTML += "</tr>" + Environment.NewLine;
                }

                gDetHTML += "</table></div></div><hr style='border-top: 1px solid blue'>";

            }
            //gDetAtenciones - FINAL
        }
        catch (Exception ex)
        {
            gDetHTML += "-" + "-" + ex.Message.ToString();
        }
        return gDetHTML;
    }

    [WebMethod]
    public static string GetDetPacientexEspexMes(string vanio, string vmes, string vmesnom, string vespec, string vhisper, string vnomper)
    {
        DateTimeFormatInfo formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetHTML = "";
        try
        {
            string qSql = "select DIA, FORMAT(cast(Dia as int), '00') as DiaFormat, COD_PER, HIS_PER, APE_PER, " +
                    "	sum(case when Area = 'HIS' then TOTAL else 0 end) HIS, " +
                    "	sum(case when Area = 'UPE' then TOTAL else 0 end) UPE, " +
                    "	sum(case when Area = 'Cx' then TOTAL else 0 end) Cx, " +
                    "	sum(TOTAL) as Total " +
                    "from ( " +
                    "	SELECT 'HIS' Area, DIA, COD_PER, HIS_PER, APE_PER,COUNT(*) TOTAL FROM NUEVA.dbo.CHEQ2011 " +
                    "	INNER JOIN NUEVA.dbo.PERSONAL ON HIS_PER=PLAZA " +
                    "	WHERE ANO=@vAnio AND MES=@vMes AND COD_PER = @vCodPer " +
                    "	GROUP BY DIA, APE_PER, COD_PER, HIS_PER " +
                    "	union " +
                    "	SELECT 'UPE' Area, DAY(FEC_UPE) DIA, COD_PER, HIS_PER, APE_PER ,COUNT(*) TOTAL FROM NUEVA.dbo.UPE " +
                    "	INNER JOIN NUEVA.dbo.PERSONAL ON COD_PER=RES_UPE " +
                    "	WHERE YEAR(FEC_UPE)=@vAnio AND MONTH(FEC_UPE)=@vMes AND COD_PER = @vCodPer " +
                    "	GROUP BY DAY(FEC_UPE), APE_PER, COD_PER, HIS_PER " +
                    "	union " +
                    "	SELECT 'Cx' Cx, Dia, PERSONAL.COD_PER, PERSONAL.HIS_PER, APE_PER, COUNT(Op_Realizada) AS TOTAL " +
                    "	FROM Estadistica.dbo.Librosop " +
                    "	left join NUEVA.dbo.PERSONAL ON Librosop.coD_per=PERSONAL.COD_PER " +
                    "	WHERE ANO = @vAnio AND MES=@vMes AND PERSONAL.COD_PER = @vCodPer  " +
                    "	GROUP BY PERSONAL.COD_PER, Dia, PERSONAL.HIS_PER, APE_PER " +
                    ") T01 " +
                    "GROUP BY DIA, COD_PER, APE_PER, HIS_PER ";
            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
            cmd2.Parameters.AddWithValue("@vAnio", vanio);
            cmd2.Parameters.AddWithValue("@vMes", vmes);
            cmd2.Parameters.AddWithValue("@vCodPer", vespec);

            adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            DataTable dtDato = objdataset.Tables[0];
            DataTable dtDia = dtDato.AsEnumerable().GroupBy(r => r.Field<int>("Dia")).Select(g => g.First()).CopyToDataTable();

            if (dtDato.Rows.Count > 0)
            {
                gDetHTML += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetHTML += "<table class='table table-hover' style='text-align: right; font-size: 14px; ' id='tblPaciente'>";
                gDetHTML += "<caption>" + vanio + " | " + formatoFecha.GetMonthName(Convert.ToInt32(vmes)) + " | " + vnomper + "</caption>";
                gDetHTML += "<tr>";
                gDetHTML += "   <th></th>";
                gDetHTML += "   <th>Dia</th>";
                gDetHTML += "   <th>HIS</th>";
                gDetHTML += "   <th>UPE</th>";
                gDetHTML += "<th class=''>Cx</th>";
                gDetHTML += "<th class=''>Total</th>";
                gDetHTML += "</tr>" + Environment.NewLine;
                gDetHTML += "<tr>";
                int nroitem = 0;
                foreach (DataRow dbRow in dtDato.Rows)
                {
                    nroitem += 1;
                    gDetHTML += "<tr>";
                    gDetHTML += "<td style='text-align: center; color: #CCD1D1'>" + nroitem + "</td>";
                    gDetHTML += "<td style='text-align: left;'>" + dbRow["DiaFormat"].ToString() + "</td>";
                    gDetHTML += "<td onclick=\"fPacEspeMesSelFilaModal(this, 'HIS', '" + vanio + "', '" + vmes + "', '" + vmesnom + "', '" + dbRow["DIA"].ToString() + "', '" + dbRow["COD_PER"].ToString() + "', '" + dbRow["HIS_PER"].ToString() + "', '" + dbRow["APE_PER"].ToString() + "')\" style='text-align: left;'>" + dbRow["HIS"].ToString() + "</td>";
                    gDetHTML += "<td onclick=\"fPacEspeMesSelFilaModal(this, 'UPE', '" + vanio + "', '" + vmes + "', '" + vmesnom + "', '" + dbRow["DIA"].ToString() + "', '" + dbRow["COD_PER"].ToString() + "', '" + dbRow["HIS_PER"].ToString() + "', '" + dbRow["APE_PER"].ToString() + "')\" style='text-align: left;'>" + dbRow["UPE"].ToString() + "</td>";
                    gDetHTML += "<td onclick=\"fPacEspeMesSelFilaModal(this, 'CX', '" + vanio + "', '" + vmes + "', '" + vmesnom + "', '" + dbRow["DIA"].ToString() + "', '" + dbRow["COD_PER"].ToString() + "', '" + dbRow["HIS_PER"].ToString() + "', '" + dbRow["APE_PER"].ToString() + "')\" style='text-align: left;'>" + dbRow["Cx"].ToString() + "</td>";
                    gDetHTML += "<td onclick=\"fPacEspeMesSelFilaModal(this, 'TOTAL', '" + vanio + "', '" + vmes + "', '" + vmesnom + "', '" + dbRow["DIA"].ToString() + "', '" + dbRow["COD_PER"].ToString() + "', '" + dbRow["HIS_PER"].ToString() + "', '" + dbRow["APE_PER"].ToString() + "')\" style='text-align: left;'>" + dbRow["Total"].ToString() + "</td>";
                    gDetHTML += "</tr>" + Environment.NewLine;
                }
                gDetHTML += "</table></div></div><hr style='border-top: 1px solid blue'>";
                gDetHTML += "||sep||";
                gDetHTML += "<option value=''></option>";
                foreach (DataRow dbRowDia in dtDia.Rows)
                {
                    gDetHTML += "<option value='" + dbRowDia["DiaFormat"].ToString() + "'>" + dbRowDia["DiaFormat"].ToString() + "</option>";
                }
                //gDetHTML += "||sep||";
                //gDetHTML += "<option value=''></option>";
                //foreach (DataRow dbRowCir in dtCirujano.Rows)
                //{
                //    gDetHTML += "<option value='" + dbRowCir["Cirujano"].ToString() + "'>" + dbRowCir["Cirujano"].ToString() + "</option>";
                //}

            }
            //gDetAtenciones - FINAL
        }
        catch (Exception ex)
        {
            gDetHTML += "- Seleccione Mes o Verifique Informacion " + "-"; // + ex.Message.ToString();
        }
        return gDetHTML;
    }

    [WebMethod]
    public static string GetDetxProfesionalxDia(string oper, string vanio, string vmes, string vmesnom, string DIA, string COD_PER, string HIS_PER, string APE_PER)
    {
        DateTimeFormatInfo formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetHTML = "";
        try
        {
            string qSql = "";

            if (oper == "HIS")
            {
                qSql = "SELECT DIA, APE_PER as Medico, COD_PER, DES_SER as Especialidad, DSC_SER as Procedimiento, " +
                    "	IAP + ' ' + IAM + ' ' + INO as Paciente, Edad, ISE as Sexo " +
                    "FROM NUEVA.dbo.CHEQ2011 " +
                    "left JOIN NUEVA.dbo.PERSONAL ON HIS_PER=PLAZA " +
                    "left JOIN NUEVA.dbo.HISTORIA on FICHAFAM = IHC " +
                    "left JOIN NUEVA.dbo.SERVICIO on COD_SERVSA1 = COD_SER " +
                    "WHERE DIA=@vDia and MES=@vMes AND ANO=@vAnio AND COD_PER = @vCodPer ";
            }
            if (oper == "UPE")
            {
                qSql = "SELECT DAY(FEC_UPE) as Dia, APE_PER as Medico, COD_PER, 'UPE' as Especialidad, 'UPE' as Procedimiento, " +
                    "	IAP + ' ' + IAM + ' ' + INO as Paciente, Edad, ISE as Sexo " +
                    "FROM NUEVA.dbo.UPE " +
                    "left JOIN NUEVA.dbo.PERSONAL ON COD_PER=RES_UPE " +
                    "left JOIN NUEVA.dbo.HISTORIA on HIC_UPE = IHC " +
                    "WHERE DAY(FEC_UPE)=@vDia AND MONTH(FEC_UPE)=@vMes AND YEAR(FEC_UPE)=@vAnio AND COD_PER = @vCodPer ";
            }
            if (oper == "CX")
            {
                qSql = "SELECT Dia, CIRUJANO AS Medico, coD_per, Especialidad, Op_Realizada as Procedimiento, " +
                    "	Ap_Paterno + ' ' + Ap_Materno + ' ' + Nombres as Paciente, Edad + LOWER(Tipo_Edad) as Edad, Sexo " +
                    "FROM Estadistica.dbo.Librosop " +
                    "WHERE Dia = @vDia AND MES = @vMes AND ANO = @vAnio AND cod_per = @vCodPer " +
                    "ORDER BY  Cirujano, Dia; ";
            }

            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
            cmd2.Parameters.AddWithValue("@vDia", DIA);
            cmd2.Parameters.AddWithValue("@vMes", vmes);
            cmd2.Parameters.AddWithValue("@vAnio", vanio);
            cmd2.Parameters.AddWithValue("@vCodPer", COD_PER);

            adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            DataTable dtDato = objdataset.Tables[0];

            if (dtDato.Rows.Count > 0)
            {
                gDetHTML += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetHTML += "<table class='table table-hover' style='text-align: right; font-size: 14px; ' id='tblEspecialid'>";
                gDetHTML += "<caption>" + vanio + " | " + formatoFecha.GetMonthName(Convert.ToInt32(vmes)) + "</caption>";
                gDetHTML += "<tr>";
                gDetHTML += "<th class=''>Especialidad </th>";
                gDetHTML += "<th class=''>Procedimiento</th>";
                gDetHTML += "<th class=''>Paciente</th>";
                gDetHTML += "<th class=''>Edad</th>";
                gDetHTML += "<th class=''>Sexo</th>";
                gDetHTML += "</tr>" + Environment.NewLine;
                foreach (DataRow dbRow in dtDato.Rows)
                {
                    gDetHTML += "<tr>";
                    gDetHTML += "<td class='' style='text-align: left;'>" + dbRow["Especialidad"].ToString() + "</td>";
                    gDetHTML += "<td class='' style='text-align: left;'>" + dbRow["Procedimiento"].ToString() + "</td>";
                    gDetHTML += "<td class='' style='text-align: left;'>" + dbRow["Paciente"].ToString() + "</td>";
                    gDetHTML += "<td class='' style='text-align: right;'>" + dbRow["Edad"].ToString() + "</td>";
                    gDetHTML += "<td class='' style='text-align: left;'>" + dbRow["Sexo"].ToString() + "</td>";
                    gDetHTML += "</tr>" + Environment.NewLine;
                }

                gDetHTML += "</table></div></div><hr style='border-top: 1px solid blue'>";

            }
            //gDetAtenciones - FINAL
        }
        catch (Exception ex)
        {
            gDetHTML += "-" + "-" + ex.Message.ToString();
        }
        return gDetHTML;
    }

    protected void bntBuscar_Click(object sender, EventArgs e)
    {
        CargaTablaDTIni();
    }

    public void CargaTablaDTIni()
    {
        string vAnio = DDLAnio.SelectedValue;
        //string vServ = ddlServicio.SelectedValue;
        try
        {
            //con.Open();
            string qSql = "select MES, NUEVA.dbo.fnMesTexto(MES) as MesNom, " +
                    "	sum(case when Area = 'HIS' then TOTAL else 0 end) HIS, " +
                    "	sum(case when Area = 'UPE' then TOTAL else 0 end) UPE, " +
                    "	sum(case when Area = 'Cx' then TOTAL else 0 end) Cx, " +
                    "   sum(TOTAL) as Total " +
                    "from ( " +
                    "	SELECT 'HIS' Area, MES, COUNT(*) TOTAL FROM NUEVA.dbo.CHEQ2011 " +
                    "	INNER JOIN NUEVA.dbo.PERSONAL ON HIS_PER=PLAZA " +
                    "	WHERE ANO=@vAnio AND COD_PRO IN ('01','02') " +
                    "	GROUP BY MES, APE_PER, COD_PER, HIS_PER " +
                    "	union " +
                    "	SELECT 'UPE' Area, MONTH(FEC_UPE) MES, COUNT(*) TOTAL FROM NUEVA.dbo.UPE " +
                    "	INNER JOIN NUEVA.dbo.PERSONAL ON COD_PER=RES_UPE " +
                    "	WHERE YEAR(FEC_UPE)=@vAnio AND COD_PRO IN ('01','02') " +
                    "	GROUP BY MONTH(FEC_UPE) " +
                    "	union " +
                    "	SELECT 'Cx' Area, Mes, COUNT(Op_Realizada) AS CANTIDAD " +
                    "	FROM Estadistica.dbo.Librosop " +
                    "	left join NUEVA.dbo.PERSONAL ON Librosop.coD_per=PERSONAL.COD_PER " +
                    "	WHERE ANO = @vAnio and COD_PRO IN ('01','02') " +
                    "	GROUP BY Mes " +
                    ") T01 " +
                    "GROUP BY MES " +
                    "Order By cast(MES as int) ";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            cmd.Parameters.AddWithValue("@vAnio", vAnio);

            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];

            //GVtable.DataSource = dtDato;
            //GVtable.DataBind();
            //CargaTabla(dtDato);

            html = "";
            if (dtDato.Rows.Count > 0)
            {
                html += Environment.NewLine + "<table class='table table-hover' style='text-align: right; font-size: 14px; ' id='tblAnioMes'>";
                //html += "<caption>" + DDLAnio.SelectedItem.Text + "</caption>";
                html += "<tr>";
                html += "<th class=''>Mes</th>";
                html += "<th class='text-center'>CE</th>";
                html += "<th class='text-center'>UPE</th>";
                html += "<th class='text-center'>Cx</th>";
                html += "<th class='text-center'>Total</th>";
                html += "</tr>" + Environment.NewLine;
                html += "<tr onclick=\"fEspeMes('" + vAnio + "', '13', '13', this)\">";
                double vt_totHIS = Convert.ToDouble(dtDato.Compute("sum(HIS)", String.Empty));
                double vt_totUPE = Convert.ToDouble(dtDato.Compute("sum(UPE)", String.Empty));
                double vt_totCx = Convert.ToDouble(dtDato.Compute("sum(Cx)", String.Empty));
                double vt_total = Convert.ToDouble(dtDato.Compute("sum(total)", String.Empty));
                html += "<td class='text-right'><b>Todos</b></td>";
                html += "<td class='text-center'><b>" + vt_totHIS + "</b></td>";
                html += "<td class='text-center'><b>" + vt_totUPE + "</b></td>";
                html += "<td class='text-center'><b>" + vt_totCx + "</b></td>";
                html += "<td class='text-center'><b>" + vt_total + "</b></td>";
                html += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                foreach (DataRow dbRow in dtDato.Rows)
                {
                    nroitem += 1;
                    double v_CantHIS = Convert.ToDouble(dbRow["HIS"]);
                    double v_CantUPE = Convert.ToDouble(dbRow["UPE"]);
                    double v_CantCx = Convert.ToDouble(dbRow["Cx"]);
                    double v_CantTotal = Convert.ToDouble(dbRow["total"]);
                    string v_MesTotalHIS = ClassGlobal.formatoMillarDec((v_CantHIS / vt_totHIS * 100).ToString());
                    string v_MesTotalUPE = ClassGlobal.formatoMillarDec((v_CantUPE / vt_totUPE * 100).ToString());
                    string v_MesTotalCx = ClassGlobal.formatoMillarDec((v_CantCx / vt_totCx * 100).ToString());
                    string v_MesTotal = ClassGlobal.formatoMillarDec((v_CantTotal / vt_total * 100).ToString());
                    html += "<tr onclick=\"fEspeMes('" + vAnio + "', '" + dbRow["Mes"].ToString() + "', '" + dbRow["MesNom"].ToString() + "', this)\">";
                    html += "<td class='' style='text-align: left;'>" + dbRow["MesNom"].ToString() + "</td>";
                    html += "<td class='' style='text-align: right;'>" + dbRow["HIS"].ToString() + " ( " + v_MesTotalHIS + "% ) " + "</td>";
                    html += "<td class='' style='text-align: right;'>" + dbRow["UPE"].ToString() + " ( " + v_MesTotalUPE + "% ) " + "</td>";
                    html += "<td class='' style='text-align: right;'>" + dbRow["Cx"].ToString() + " ( " + v_MesTotalCx + "% ) " + "</td>";
                    html += "<td class='' style='text-align: right;'>" + dbRow["total"].ToString() + " ( " + v_MesTotal + "% ) " + "</td>";
                    //html += "<td class='' style='text-align: left;'>" + "<button type='button' class='btn bg-navy' data-toggle='modal' data-target='#modalAtenciones'><i class='fa fa-fw fa-medkit'></i></button>" + "</td>";
                    html += "</tr>" + Environment.NewLine;
                }

                html += "</table><hr style='border-top: 1px solid blue'>";
            }
            else
            {
                html += "<table>";
                html += "<tr><td class='FieldCaption' colspan=3>Sin registros encontrados0</td></tr>";
                html += "</table><hr>";
            }
            LitTABL1.Text = html;

        }
        catch (Exception ex)
        {
            //Label1.Text = "Error";
            ex.Message.ToString();
            LitTABL1.Text = ex.Message.ToString();
        }
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

    protected void ddlServicio_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargaTablaDTIni();
    }
}
