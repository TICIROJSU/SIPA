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

public partial class ASPX_IROJVAR_HISMINSA_AtencXservic4 : System.Web.UI.Page
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
            DDLMesHasta.SelectedIndex = vmonth - 1;
            CargaTablaDT();
        }

        if (Page.IsPostBack)
        {
            GVtable.DataSource = null;
            GVtable.DataBind();
        }

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
                    gDetAtenciones += "<tr>";
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
    public static string GetDetProfesional(string vanio, string vmes, string vcodserv, string vservicio, string vmesHasta)
    {
        DateTimeFormatInfo formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetHTML = "";
        try
        {
            string qSql = "select COD_SERVSA, COD_SERVSA1, COD_PER, APE_PER, PLAZA, " +
                "SUM(case when MT='M' then 1 else 0 end) as Manana, " +
                "SUM(case when MT<>'M' then 1 else 0 end) as Tarde, COUNT(MT) Total " +
                "from NUEVA.dbo.CHEQ2011 " +
                "left join NUEVA.dbo.PERSONAL on PLAZA=HIS_PER " +
                "where ANO='" + vanio + "' and (MES between '"+vmes+"' and '"+vmesHasta+"') and COD_SERVSA1='" + vcodserv + "' " +
                "group by COD_SERVSA, COD_SERVSA1, COD_PER, APE_PER, PLAZA " +
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
            //LitErrores.Text += vplaza + "-" + "-" + ex.Message.ToString();
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
                    gDetHTML += "<tr data-toggle='modal' data-target='#modalAtenCli' onclick=\"DetAtenCli('" + vanio + "', '" + vmes + "', '" + dbRow["DIA"].ToString() + "', '" + vcodserv + "', '" + vservicio + "', '')\">";
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

    protected void bntBuscar_Click(object sender, EventArgs e)
    {
        if (DDLMesHasta.SelectedValue == "0")
        {
            CargaTablaDT();
        }
        else
        {
            CargaTablaDT();
        }

    }

    public void CargaTablaDT()
    {
        string vAnio = DDLAnio.SelectedValue;
        string vMesD = DDLMesDesde.SelectedValue;
        string vMesH = DDLMesHasta.SelectedValue;
        try
        {
            //con.Open();
            string qSql = "select COD_SERVSA, COD_SERVSA1, DSC_SER, COUNT(ID_HIS) as Aten, " +
                "    sum(case when FI='2' then 1 else 0 end) as SIS, " +
                "    sum(case when FI='1' then 1 else 0 end) as Usu, " +
                "    sum(case when FI='11' then 1 else 0 end) as Exo, " +
                "    sum(case when (FI='1' or FI='2' or FI='11') then 0 else 1 end) as otr, " +
                "   SUM(case when MT='M' then 1 else 0 end) as Manana, " +
                "   SUM(case when MT<>'M' then 1 else 0 end) as Tarde " +
                "    from NUEVA.dbo.CHEQ2011 left join NUEVA.dbo.SERVICIO on COD_SERVSA1=COD_SER " +
                "    where ANO='" + vAnio + "' and (MES between '" + vMesD + "' and '" + vMesH + "') " +
                "    group by COD_SERVSA, COD_SERVSA1, DSC_SER " +
                "	 order by DSC_SER ";

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
            //ddlServicio.SelectedValue = "";
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
            html += "<caption>" + DDLMesHasta.SelectedItem.Text + "</caption>";
            html += "<tr>";
            html += "<th class=''></th>" +
                "   <th class=''>Cod</th>" +
                "   <th>xProf</th>" +
                "   <th>Servicio</th>";
            html += "<th class='text-center'>Total</th>" +
                "   <th class=''>SIS</th>" +
                "   <th class=''>Usu</th>";
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
            html += "<td></td><td></td><td class=''></td><td class='text-rigth'><b>Totales</b></td>";
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
                    "<button type='button' class='btn bg-navy' data-toggle='modal' data-target='#modalAtenciones' onclick=\"DetProfesional('" + DDLAnio.SelectedValue + "','" + DDLMesDesde.SelectedValue + "','" + dbRow["COD_SERVSA1"].ToString() + "', '" + dbRow["DSC_SER"].ToString() + "','" + DDLMesHasta.SelectedValue + "')\" >" +
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
