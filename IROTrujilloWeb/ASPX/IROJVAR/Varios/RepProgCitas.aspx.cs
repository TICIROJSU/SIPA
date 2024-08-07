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

public partial class ASPX_IROJVAR_Varios_RepProgCitas : System.Web.UI.Page
{
    string html = "";
    string htmlAtenciones = "";
    string htmlDetAtenciones = "";
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {Server.ScriptTimeout = 3600;
        if (!Page.IsPostBack)
        {
            //int vmonth = DateTime.Now.Month;
            //DDLMes.SelectedIndex = vmonth;
            int vyear = DateTime.Now.Year;
            DDLAnio.Text = vyear.ToString();
            int vmonth = DateTime.Now.Month;
            DDLMes.SelectedIndex = vmonth;
            CargaTablaDTIni();
            CargaTablaDT();
        }

        if (Page.IsPostBack)
        {
            GVtable.DataSource = null;
            GVtable.DataBind();
        }

    }

    [WebMethod]
    public static string GetDetAtenciones(string vanio, string vmes, string vcodserv, string vservicio)
    {
        DateTimeFormatInfo formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetHTML = "";
        try
        {
            string qSql = "select COD_SERVSA, COD_SERVSA1, DSC_SER, MES, DIA, COUNT(ID_HIS) as Aten, " +
                "    sum(case when FI='2' then 1 else 0 end) as SIS, " +
                "    sum(case when FI='1' then 1 else 0 end) as Usu, " +
                "    sum(case when FI='11' then 1 else 0 end) as Exo, " +
                "    sum(case when (FI='1' or FI='2' or FI='11') then 0 else 1 end) as otr, " +
                "   SUM(case when MT='M' then 1 else 0 end) as Manana, " +
                "   SUM(case when MT<>'M' then 1 else 0 end) as Tarde " +
                "    from NUEVA.dbo.CHEQ2011 left join NUEVA.dbo.SERVICIO on COD_SERVSA1=COD_SER " +
                "    where ANO='" + vanio + "' and MES='" + vmes + "' and COD_SERVSA1='" + vcodserv + "' " +
                "    group by COD_SERVSA, COD_SERVSA1, DSC_SER, MES, DIA " +
                "	 order by DIA  ";
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
                gDetHTML += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetHTML += "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
                gDetHTML += "<caption>" + vservicio + " | " + formatoFecha.GetMonthName(Convert.ToInt32(vmes)) + "</caption>";
                gDetHTML += "<tr>";
                gDetHTML += "<th class=''>N° </th>";
                gDetHTML += "<th class=''>Dia</th>";
                gDetHTML += "<th class=''>Total</th>";
                gDetHTML += "<th class=''>SIS</th>";
                gDetHTML += "<th class=''>Usu</th>";
                gDetHTML += "<th class=''>Exo</th>";
                gDetHTML += "<th class=''>Mañana</th>";
                gDetHTML += "<th class=''>Tarde</th>";
                gDetHTML += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    double v_total = Convert.ToDouble(dbRow["Aten"]);
                    string v_SIS = ClassGlobal.formatoMillarDec((Convert.ToDouble(dbRow["SIS"]) / v_total * 100).ToString()),
                            v_Usu = ClassGlobal.formatoMillarDec((Convert.ToDouble(dbRow["Usu"]) / v_total * 100).ToString()),
                            v_Exo = ClassGlobal.formatoMillarDec((Convert.ToDouble(dbRow["Exo"]) / v_total * 100).ToString()),
                            v_Man = ClassGlobal.formatoMillarDec((Convert.ToDouble(dbRow["Manana"]) / v_total * 100).ToString()),
                            v_Tar = ClassGlobal.formatoMillarDec((Convert.ToDouble(dbRow["Tarde"]) / v_total * 100).ToString());
                    gDetHTML += "<tr data-toggle='modal' data-target='#modalAtenCli' onclick=\"DetAtenCli('" + vanio + "', '" + vmes + "', '" + dbRow["DIA"].ToString() + "', '" + vcodserv + "', '" + vservicio + "', '')\">";
                    gDetHTML += "<td class='' style='text-align: left;'>" + nroitem + "</td>";
                    gDetHTML += "<td class='' style='text-align: left;'>" + dbRow["DIA"].ToString() + "</td>";
                    gDetHTML += "<td class='' style='text-align: left;'>" + dbRow["Aten"].ToString() + "</td>";
                    gDetHTML += "<td class='' style='text-align: left;'>" + dbRow["SIS"].ToString() + " ( " + v_SIS + "% ) " + "</td>";
                    gDetHTML += "<td class='' style='text-align: left;'>" + dbRow["Usu"].ToString() + " ( " + v_Usu + "% ) " + "</td>";
                    gDetHTML += "<td class='' style='text-align: left;'>" + dbRow["Exo"].ToString() + " ( " + v_Exo + "% ) " + "</td>";
                    gDetHTML += "<td class='' style='text-align: left;'>" + dbRow["Manana"].ToString() + " ( " + v_Man + "% ) " + "</td>";
                    gDetHTML += "<td class='' style='text-align: left;'>" + dbRow["Tarde"].ToString() + " ( " + v_Tar + "% ) " + "</td>";
                    //gDetAtenciones += "<td class='' style='text-align: left;'>" + "<button type='button' class='btn bg-navy' data-toggle='modal' data-target='#modalDetDx'  onclick=\"DetDx('" + dbRow["ID_HIS"].ToString() + "')\"><i class='fa fa-fw fa-eye'></i></button>" + "</td>";

                    gDetHTML += "</tr>" + Environment.NewLine;
                }

                gDetHTML += "</table></div></div><hr style='border-top: 1px solid blue'>";
                gDetHTML += "||sep||";
                gDetHTML += "Atenciones por Dia, Total: " + dtDatoDetAt.Compute("Sum(Aten)", String.Empty);
                gDetHTML += ", Promedio: " + dtDatoDetAt.Compute("Avg(Aten)", String.Empty);
                gDetHTML += ", Mañana: " + dtDatoDetAt.Compute("Sum(Manana)", String.Empty);
                gDetHTML += ", Tarde: " + dtDatoDetAt.Compute("Sum(Tarde)", String.Empty);

            }
            //gDetAtenciones - FINAL
        }
        catch (Exception ex)
        {
            //LitErrores.Text += vplaza + "-" + "-" + ex.Message.ToString();
        }
        return gDetHTML;
    }

    [WebMethod]
    public static string GetDetAtenCli(string vanio, string vmes, string vdia, string vcodserv, string vservicio, string vcodplaza)
    {
        DateTimeFormatInfo formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetAtenciones = "";
        try
        {
            string qSql = "select (case when FI='1' then 'Usu' else (case when FI='2' then 'SIS' else 'Exo' end) end) as Seguro, " +
                "ID_HIS, FICHAFAM, IAP + ' ' + IAM + ' ' + INO as Cliente " +
                "from NUEVA.dbo.CHEQ2011 left join NUEVA.dbo.HISTORIA on RIGHT('0000' + FICHAFAM, 6) = IHC " +
                "where ANO='" + vanio + "' and mes ='" + vmes + "' and DIA like '" + vdia + "' and COD_SERVSA1='" + vcodserv + "' and PLAZA like '" + vcodplaza + "%'" +
                "order by FI, ID_HIS ";
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
                gDetAtenciones += "<caption>" + vservicio + " | " + formatoFecha.GetMonthName(Convert.ToInt32(vmes)) + "</caption>";
                gDetAtenciones += "<tr>";
                gDetAtenciones += "<th class=''>N° </th>";
                gDetAtenciones += "<th class=''>Seguro</th>";
                gDetAtenciones += "<th class=''>HIS</th>";
                gDetAtenciones += "<th class=''>FichaFam</th>";
                gDetAtenciones += "<th class=''>Cliente</th>";
                gDetAtenciones += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    gDetAtenciones += "<tr data-toggle='modal' data-target='#modalDetDx' onclick=\"DetDx('" + dbRow["ID_HIS"].ToString() + "')\">";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + nroitem + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["Seguro"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["ID_HIS"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["FICHAFAM"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["Cliente"].ToString() + "</td>";
                    //gDetAtenciones += "<td class='' style='text-align: left;'>" + "<button type='button' class='btn bg-navy' data-toggle='modal' data-target='#modalDetDx'  onclick=\"DetDx('" + dbRow["ID_HIS"].ToString() + "')\"><i class='fa fa-fw fa-eye'></i></button>" + "</td>";

                    gDetAtenciones += "</tr>" + Environment.NewLine;
                }

                gDetAtenciones += "</table></div></div><hr style='border-top: 1px solid blue'>";

            }
            //gDetAtenciones - FINAL
        }
        catch (Exception ex)
        {
            //LitErrores.Text += vplaza + "-" + "-" + ex.Message.ToString();
            gDetAtenciones += "<tr><td>" + ex.Message.ToString() + "<td><tr>";
        }
        return gDetAtenciones;
    }

    [WebMethod]
    public static string GetDetProfesional(string vanio, string vmes, string vcodserv, string vservicio)
    {
        DateTimeFormatInfo formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetHTML = "";
        string qSql = "";
        try
        {
            qSql = "select COD_SERVSA, COD_SERVSA1, MES, COD_PER, APE_PER, PLAZA, " +
                "SUM(case when MT='M' then 1 else 0 end) as Manana, " +
                "SUM(case when MT<>'M' then 1 else 0 end) as Tarde, COUNT(MT) Total " +
                "from NUEVA.dbo.CHEQ2011 " +
                "left join NUEVA.dbo.PERSONAL on PLAZA=HIS_PER " +
                "where ANO='" + vanio + "' and MES='" + vmes + "' and COD_SERVSA1='" + vcodserv + "' " +
                "group by COD_SERVSA, COD_SERVSA1, MES, COD_PER, APE_PER, PLAZA " +
                "order by APE_PER  ";
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
                gDetHTML += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetHTML += "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
                gDetHTML += "<caption>" + vservicio + " | " + formatoFecha.GetMonthName(Convert.ToInt32(vmes)) + "</caption>";
                gDetHTML += "<tr>";
                gDetHTML += "<th class=''>N° </th>";
                gDetHTML += "<th class=''>CodPer</th>";
                gDetHTML += "<th class=''>Profesional</th>";
                gDetHTML += "<th class=''>Mañana</th>";
                gDetHTML += "<th class=''>Tarde</th>";
                gDetHTML += "<th class=''>Total</th>";
                gDetHTML += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    double v_total = Convert.ToDouble(dbRow["Total"]);
                    string v_Man = ClassGlobal.formatoMillarDec((Convert.ToDouble(dbRow["Manana"]) / v_total * 100).ToString()),
                            v_Tar = ClassGlobal.formatoMillarDec((Convert.ToDouble(dbRow["Tarde"]) / v_total * 100).ToString());
                    gDetHTML += "<tr data-toggle='modal' data-target='#mXServXProfXDia' onclick=\"fXservXprofXdia('" + vanio + "', '" + vmes + "', '', '" + vcodserv + "', '" + vservicio + "', '" + dbRow["PLAZA"].ToString() + "')\">";
                    gDetHTML += "<td class='' style='text-align: left;'>" + nroitem + "</td>";
                    gDetHTML += "<td class='' style='text-align: left;'>" + dbRow["COD_PER"].ToString() + "</td>";
                    gDetHTML += "<td class='' style='text-align: left;'>" + dbRow["APE_PER"].ToString() + "</td>";
                    gDetHTML += "<td class='' style='text-align: left;'>" + dbRow["Manana"].ToString() + " (" + v_Man + "%) " + "</td>";
                    gDetHTML += "<td class='' style='text-align: left;'>" + dbRow["Tarde"].ToString() + " (" + v_Tar + "%) " + "</td>";
                    gDetHTML += "<td class='' style='text-align: left;'>" + dbRow["Total"].ToString() + "</td>";
                    //gDetAtenciones += "<td class='' style='text-align: left;'>" + "<button type='button' class='btn bg-navy' data-toggle='modal' data-target='#modalDetDx'  onclick=\"DetDx('" + dbRow["ID_HIS"].ToString() + "')\"><i class='fa fa-fw fa-eye'></i></button>" + "</td>";

                    gDetHTML += "</tr>" + Environment.NewLine;
                }

                gDetHTML += "</table></div></div><hr style='border-top: 1px solid blue'>";
                gDetHTML += "||sep||";
                gDetHTML += "Atenciones por Profesional, Total: " + dtDatoDetAt.Compute("Sum(Total)", String.Empty);
                gDetHTML += ", Promedio: " + dtDatoDetAt.Compute("Avg(Total)", String.Empty);
                gDetHTML += ", Mañana: " + dtDatoDetAt.Compute("SUM(Manana)", String.Empty);
                gDetHTML += ", Tarde: " + dtDatoDetAt.Compute("SUM(Tarde)", String.Empty);

            }
            //gDetAtenciones - FINAL
        }
        catch (Exception ex)
        {
            gDetHTML += qSql + ex.ToString();
        }
        return gDetHTML;
    }

    [WebMethod]
    public static string GetDetServXProfXDia(string vanio, string vmes, string vcodserv, string vservicio, string vplaza)
    {
        DateTimeFormatInfo formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetHTML = "";
        try
        {
            string qSql = "select DIA, COUNT(DIA) Total, " +
                "SUM(case when FI='2' then 1 else 0 end) SIS, " +
                "SUM(case when FI='1' then 1 else 0 end) Comun, " +
                "SUM(case when FI='11' then 1 else 0 end) Exo  " +
                "from NUEVA.dbo.CHEQ2011  " +
                "    where ANO='" + vanio + "' and MES='" + vmes + "' and COD_SERVSA1='" + vcodserv + "' and PLAZA = '" + vplaza + "' " +
                "	 group by DIA order by DIA ";
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
                gDetHTML += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetHTML += "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
                gDetHTML += "<caption>" + vservicio + " | " + formatoFecha.GetMonthName(Convert.ToInt32(vmes)) + "</caption>";
                gDetHTML += "<tr>";
                gDetHTML += "<th class=''>N° </th>";
                gDetHTML += "<th class=''>Dia</th>";
                gDetHTML += "<th class=''>Total</th>";
                gDetHTML += "<th class=''>SIS</th>";
                gDetHTML += "<th class=''>Usu</th>";
                gDetHTML += "<th class=''>Exo</th>";
                gDetHTML += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    double v_total = Convert.ToDouble(dbRow["Total"]);
                    string v_SIS = ClassGlobal.formatoMillarDec((Convert.ToDouble(dbRow["SIS"]) / v_total * 100).ToString()),
                            v_Usu = ClassGlobal.formatoMillarDec((Convert.ToDouble(dbRow["Comun"]) / v_total * 100).ToString()),
                            v_Exo = ClassGlobal.formatoMillarDec((Convert.ToDouble(dbRow["Exo"]) / v_total * 100).ToString());
                    gDetHTML += "<tr data-toggle='modal' data-target='#modalAtenCli' onclick=\"DetAtenCli('" + vanio + "', '" + vmes + "', '" + dbRow["DIA"].ToString() + "', '" + vcodserv + "', '" + vservicio + "', '" + vplaza + "')\">";
                    gDetHTML += "<td class='' style='text-align: left;'>" + nroitem + "</td>";
                    gDetHTML += "<td class='' style='text-align: left;'>" + dbRow["DIA"].ToString() + "</td>";
                    gDetHTML += "<td class='' style='text-align: left;'>" + dbRow["Total"].ToString() + "</td>";
                    gDetHTML += "<td class='' style='text-align: left;'>" + dbRow["SIS"].ToString() + " ( " + v_SIS + "% ) " + "</td>";
                    gDetHTML += "<td class='' style='text-align: left;'>" + dbRow["Comun"].ToString() + " ( " + v_Usu + "% ) " + "</td>";
                    gDetHTML += "<td class='' style='text-align: left;'>" + dbRow["Exo"].ToString() + " ( " + v_Exo + "% ) " + "</td>";
                    //gDetAtenciones += "<td class='' style='text-align: left;'>" + "<button type='button' class='btn bg-navy' data-toggle='modal' data-target='#modalDetDx'  onclick=\"DetDx('" + dbRow["ID_HIS"].ToString() + "')\"><i class='fa fa-fw fa-eye'></i></button>" + "</td>";

                    gDetHTML += "</tr>" + Environment.NewLine;
                }

                gDetHTML += "</table></div></div><hr style='border-top: 1px solid blue'>";
                gDetHTML += "||sep||";
                gDetHTML += "Atenciones x Servicio x Profesiona x Dia, Total: " + dtDatoDetAt.Compute("Sum(Total)", String.Empty);
                gDetHTML += ", Promedio: " + dtDatoDetAt.Compute("Avg(Total)", String.Empty);

            }
            //gDetAtenciones - FINAL
        }
        catch (Exception ex)
        {
            //LitErrores.Text += vplaza + "-" + "-" + ex.Message.ToString();
        }
        return gDetHTML;
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

    protected void bntBuscar_Click(object sender, EventArgs e)
    {
        if (DDLMes.SelectedValue == "0")
        {
            CargaTablaDTIni();
        }
        else
        {
            CargaTablaDT();
            CargaTablaDTIni();
        }

    }

    public void CargaTablaDT()
    {
        string vAnio = DDLAnio.SelectedValue;
        string vMes = DDLMes.SelectedValue;
        string vTur = ddlTurno.SelectedValue;
        string vPer = ddlPersonal.SelectedValue;
        string vSer = ddlServicio.SelectedValue;
        try
        {
            //con.Open();
            //string qSql = "exec NUEVA.dbo.SP_JVAR_CitasProgramXServ '" + 
            //    vMes + "', '" + vAnio + "', 'M', '" + vPer + "', '" + vSer + "'";

            //SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            //cmd.CommandType = CommandType.Text;
            //SqlDataAdapter adapter = new SqlDataAdapter();
            //adapter.SelectCommand = cmd;

            //conSAP00.Open();
            //DataSet objdataset = new DataSet();
            //adapter.Fill(objdataset);
            //conSAP00.Close();

            //DataTable dtDato = objdataset.Tables[0];

            DataTable dtDatoM = GetDataTable(vAnio, vMes, "M", vPer, vSer, conSAP00);
            DataTable dtDatoT = GetDataTable(vAnio, vMes, "T", vPer, vSer, conSAP00);

            CargaTablaDetProgCita(dtDatoM, dtDatoT);
            //dtDatoM.Merge(dtDatoT);
            GVDetProgCit.DataSource = dtDatoM;
            GVDetProgCit.DataBind();
            
            ddlServicio.SelectedValue = "";
        }
        catch (Exception ex)
        {
            //Label1.Text = "Error";
            ex.Message.ToString();
            LitTABL1.Text = ex.Message.ToString();
        }
    }

    public static DataTable GetDataTable(string vAnio, string vMes, string vTur, string vPer, string vSer, SqlConnection conSAP00)
    {
        DataTable dtDato = null; 

        try
        {
            //con.Open();
            string qSql = "exec NUEVA.dbo.SP_JVAR_CitasProgramXServ '" +
                vMes + "', '" + vAnio + "', '" + vTur + "', '" + vPer + "', '" + vSer + "'";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            dtDato = objdataset.Tables[0];
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            conSAP00.Close();
        }

        return dtDato;
        
    }

    public void CargaTablaDetProgCita(DataTable dtDatoM, DataTable dtDatoT)
    {
        CuposM.InnerText = "";
        CitadM.InnerText = "";
        AtendM.InnerText = "";
        CitAteM.InnerText = "";
        CupCitM.InnerText = "";

        CuposT.InnerText = "";
        CitadT.InnerText = "";
        AtendT.InnerText = "";
        CitAteT.InnerText = "";
        CupCitT.InnerText = "";

        double vtCupoM = 0, vtTarjM = 0, vtCheqM = 0;
        double vtCupoT = 0, vtTarjT = 0, vtCheqT = 0;

        vtCupoM = Convert.ToDouble(dtDatoM.Compute("sum(TotalCupos)", String.Empty));
        vtTarjM = Convert.ToDouble(dtDatoM.Compute("sum(CantTarj)", String.Empty));
        vtCheqM = Convert.ToDouble(dtDatoM.Compute("sum(CantCheq)", String.Empty));

        CuposM.InnerText = Convert.ToString(vtCupoM);
        CitadM.InnerText = Convert.ToString(vtTarjM);
        AtendM.InnerText = Convert.ToString(vtCheqM);
        CitAteM.InnerText = Convert.ToString(vtTarjM - vtCheqM);
        CupCitM.InnerText = Convert.ToString(vtCupoM - vtTarjM);
        DetM.InnerHtml = "<button type='button' class='btn bg-navy' data-toggle='modal' data-target='#modalAtenciones' onclick=\"DetProfesional('','','', '')\"><i class='fa fa-fw fa-medkit'></i></button>";

        vtCupoT = Convert.ToDouble(dtDatoT.Compute("sum(TotalCupos)", String.Empty));
        vtTarjT = Convert.ToDouble(dtDatoT.Compute("sum(CantTarj)", String.Empty));
        vtCheqT = Convert.ToDouble(dtDatoT.Compute("sum(CantCheq)", String.Empty));

        CuposT.InnerText = Convert.ToString(vtCupoT);
        CitadT.InnerText = Convert.ToString(vtTarjT);
        AtendT.InnerText = Convert.ToString(vtCheqT);
        CitAteT.InnerText = Convert.ToString(vtTarjT - vtCheqT);
        CupCitT.InnerText = Convert.ToString(vtCupoT - vtTarjT);
        //DetT.InnerHtml = "<td><button type='button' class='btn bg-navy' data-toggle='modal' data-target='#modalAtenciones' onclick=\"DetProfesional('','','', '')\"><i class='fa fa-fw fa-medkit'></i></button></td>";

    }

    public void CargaTabla(DataTable dtDato)
    {
        html = "";
        if (dtDato.Rows.Count > 0)
        {
            html += Environment.NewLine + "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
            html += "<caption>" + DDLMes.SelectedItem.Text + "</caption>";
            html += "<tr>";
            html += "<th class=''></th><th class=''>Cod</th>" +
                "<th>xDia</th> <th>xProf</th>" +
                "<th class=''>Servicio</th>";
            html += "<th class='text-center'>Total</th><th class=''>SIS</th><th class=''>Usu</th>";
            html += "<th class=''>Exo</th>";
            html += "<th class=''>Mañana</th>";
            html += "<th class=''>Tarde</th>";
            html += "</tr>" + Environment.NewLine;
            ////////////////////////////////
            html += "<tr>";
            double vt_total = Convert.ToDouble(dtDato.Compute("sum(Aten)", String.Empty));
            double vt_SIS = Convert.ToDouble(dtDato.Compute("sum(SIS)", String.Empty));
            double vt_Usu = Convert.ToDouble(dtDato.Compute("sum(Usu)", String.Empty));
            double vt_Exo = Convert.ToDouble(dtDato.Compute("sum(Exo)", String.Empty));
            double vt_Man = Convert.ToDouble(dtDato.Compute("sum(Manana)", String.Empty));
            double vt_Tar = Convert.ToDouble(dtDato.Compute("sum(Tarde)", String.Empty));
            html += "<td></td><td></td><td></td><td class=''></td><td class='text-rigth'><b>Totales</b></td>";
            html += "<td class='text-center'><b>" + vt_total + "</b></td>" +
                "<td class='text-left'><b>" + vt_SIS + " (" + ClassGlobal.formatoMillarDec((vt_SIS / vt_total * 100).ToString()) + " %)</b></td>" +
                "<td class='text-left'><b>" + vt_Usu + " (" + ClassGlobal.formatoMillarDec((vt_Usu / vt_total * 100).ToString()) + " %)</b></td>" +
                "<td class='text-left'><b>" + vt_Exo + " (" + ClassGlobal.formatoMillarDec((vt_Exo / vt_total * 100).ToString()) + " %)</b></td>" +
                "<td class='text-left'><b>" + vt_Man + " (" + ClassGlobal.formatoMillarDec((vt_Man / vt_total * 100).ToString()) + " %)</b></td>" +
                "<td class='text-left'><b>" + vt_Tar + " (" + ClassGlobal.formatoMillarDec((vt_Tar / vt_total * 100).ToString()) + " %)</b></td>";
            html += "</tr>" + Environment.NewLine;
            int nroitem = 0;
            foreach (DataRow dbRow in dtDato.Rows)
            {
                nroitem += 1;
                double v_total = Convert.ToDouble(dbRow["Aten"]);
                string v_SIS = ClassGlobal.formatoMillarDec((Convert.ToDouble(dbRow["SIS"]) / v_total * 100).ToString()),
                        v_Usu = ClassGlobal.formatoMillarDec((Convert.ToDouble(dbRow["Usu"]) / v_total * 100).ToString()),
                        v_Exo = ClassGlobal.formatoMillarDec((Convert.ToDouble(dbRow["Exo"]) / v_total * 100).ToString());
                html += "<tr >";
                html += "<td class='' style='text-align: left;'>" + nroitem + "</td>";
                html += "<td class='' style='text-align: left;'>" + dbRow["COD_SERVSA1"].ToString() + "</td>";
                html += "<td class='' style='text-align: left;'>" +
                    "<button type='button' class='btn bg-navy' data-toggle='modal' data-target='#modalAtenciones' onclick=\"DetAtenciones('" + DDLAnio.SelectedValue + "','" + DDLMes.SelectedValue + "','" + dbRow["COD_SERVSA1"].ToString() + "', '" + dbRow["DSC_SER"].ToString() + "')\" >" +
                    "<i class='fa fa-fw fa-medkit'></i>" +
                    "</button>" +
                    "</td>";
                html += "<td class='' style='text-align: left;'>" +
                    "<button type='button' class='btn bg-navy' data-toggle='modal' data-target='#modalAtenciones' onclick=\"DetProfesional('" + DDLAnio.SelectedValue + "','" + DDLMes.SelectedValue + "','" + dbRow["COD_SERVSA1"].ToString() + "', '" + dbRow["DSC_SER"].ToString() + "')\" >" +
                    "<i class='fa fa-fw fa-medkit'></i>" +
                    "</button>" +
                    "</td>";
                html += "<td class='' style='text-align: left;'>" + dbRow["DSC_SER"].ToString() + "</td>";
                html += "<td class='' style='text-align: center;'>" + dbRow["Aten"].ToString() + "</td>";
                html += "<td class='' style='text-align: left;'>" + dbRow["SIS"].ToString() + " ( " + v_SIS + "% ) " + "</td>";
                html += "<td class='' style='text-align: left;'>" + dbRow["Usu"].ToString() + " ( " + v_Usu + "% ) " + "</td>";
                html += "<td class='' style='text-align: left;'>" + dbRow["Exo"].ToString() + " ( " + v_Exo + "% ) " + "</td>";
                html += "<td class='' style='text-align: left;'>" + dbRow["Manana"].ToString() + " ( " + v_Exo + "% ) " + "</td>";
                html += "<td class='' style='text-align: left;'>" + dbRow["Tarde"].ToString() + " ( " + v_Exo + "% ) " + "</td>";
                //html += "<td class='' style='text-align: left;'>" + "<button type='button' class='btn bg-navy' data-toggle='modal' data-target='#modalAtenciones'><i class='fa fa-fw fa-medkit'></i></button>" + "</td>";
                html += "</tr>" + Environment.NewLine;
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

    public void CargaTablaDTIni()
    {
        string vAnio = DDLAnio.SelectedValue;
        string vServ = ddlServicio.SelectedValue;
        try
        {
            //con.Open();
            string qSql = "select MES, COUNT(ID_HIS) as Aten, sum(case when FI='2' then 1 else 0 end) as SIS, " +
                "sum(case when FI='1' then 1 else 0 end) as Usu, " +
                "sum(case when FI='11' then 1 else 0 end) as Exo, " +
                "sum(case when (FI='1' or FI='2' or FI='11') then 0 else 1 end) as otr, " +
                "SUM(case when MT='M' then 1 else 0 end) as Manana, " +
                "SUM(case when MT<>'M' then 1 else 0 end) as Tarde " +
                "from NUEVA.dbo.CHEQ2011 " +
                "left join NUEVA.dbo.SERVICIO on COD_SERVSA1=COD_SER " +
                "where ANO='" + vAnio + "' and COD_SERVSA1 like '%" + vServ + "%' " +
                "group by MES order by MES";

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
            //CargaTabla(dtDato);

            html = "";
            if (dtDato.Rows.Count > 0)
            {
                html += Environment.NewLine + "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
                html += "<caption>" + DDLAnio.SelectedItem.Text + "</caption>";
                html += "<tr>";
                html += "<th class=''>Mes</th>";
                html += "<th class='text-center'>Total</th><th class=''>SIS</th><th class=''>Usu</th>";
                html += "<th class=''>Exo</th>";
                html += "<th class=''>Mañana</th>";
                html += "<th class=''>Tarde</th>";
                html += "</tr>" + Environment.NewLine;
                ////////////////////////////////
                html += "<tr>";
                double vt_total = Convert.ToDouble(dtDato.Compute("sum(Aten)", String.Empty));
                double vt_SIS = Convert.ToDouble(dtDato.Compute("sum(SIS)", String.Empty));
                double vt_Usu = Convert.ToDouble(dtDato.Compute("sum(Usu)", String.Empty));
                double vt_Exo = Convert.ToDouble(dtDato.Compute("sum(Exo)", String.Empty));
                double vt_Man = Convert.ToDouble(dtDato.Compute("sum(Manana)", String.Empty));
                double vt_Tar = Convert.ToDouble(dtDato.Compute("sum(Tarde)", String.Empty));
                html += "<td class='text-rigth'><b>Totales</b></td>";
                html += "<td class='text-center'><b>" + vt_total + "</b></td>" +
                    "<td class='text-left'><b>" + vt_SIS + " (" + ClassGlobal.formatoMillarDec((vt_SIS / vt_total * 100).ToString()) + " %)</b></td>" +
                    "<td class='text-left'><b>" + vt_Usu + " (" + ClassGlobal.formatoMillarDec((vt_Usu / vt_total * 100).ToString()) + " %)</b></td>" +
                    "<td class='text-left'><b>" + vt_Exo + " (" + ClassGlobal.formatoMillarDec((vt_Exo / vt_total * 100).ToString()) + " %)</b></td>" +
                    "<td class='text-left'><b>" + vt_Man + " (" + ClassGlobal.formatoMillarDec((vt_Man / vt_total * 100).ToString()) + " %)</b></td>" +
                    "<td class='text-left'><b>" + vt_Tar + " (" + ClassGlobal.formatoMillarDec((vt_Tar / vt_total * 100).ToString()) + " %)</b></td>";
                html += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                foreach (DataRow dbRow in dtDato.Rows)
                {
                    nroitem += 1;
                    double v_total = Convert.ToDouble(dbRow["Aten"]);
                    string v_SIS = ClassGlobal.formatoMillarDec((Convert.ToDouble(dbRow["SIS"]) / v_total * 100).ToString()),
                            v_Usu = ClassGlobal.formatoMillarDec((Convert.ToDouble(dbRow["Usu"]) / v_total * 100).ToString()),
                            v_Exo = ClassGlobal.formatoMillarDec((Convert.ToDouble(dbRow["Exo"]) / v_total * 100).ToString()),
                            v_Man = ClassGlobal.formatoMillarDec((Convert.ToDouble(dbRow["Manana"]) / v_total * 100).ToString()),
                            v_Tar = ClassGlobal.formatoMillarDec((Convert.ToDouble(dbRow["Tarde"]) / v_total * 100).ToString());
                    html += "<tr>";
                    html += "<td class='' style='text-align: center;'>" + ClassGlobal.MesNroToTexto(dbRow["MES"].ToString()) + "</td>";
                    html += "<td class='' style='text-align: center;'>" + dbRow["Aten"].ToString() + "</td>";
                    html += "<td class='' style='text-align: left;'>" + dbRow["SIS"].ToString() + " ( " + v_SIS + "% ) " + "</td>";
                    html += "<td class='' style='text-align: left;'>" + dbRow["Usu"].ToString() + " ( " + v_Usu + "% ) " + "</td>";
                    html += "<td class='' style='text-align: left;'>" + dbRow["Exo"].ToString() + " ( " + v_Exo + "% ) " + "</td>";
                    html += "<td class='' style='text-align: left;'>" + dbRow["Manana"].ToString() + " ( " + v_Man + "% ) " + "</td>";
                    html += "<td class='' style='text-align: left;'>" + dbRow["Tarde"].ToString() + " ( " + v_Tar + "% ) " + "</td>";
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

}