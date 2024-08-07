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

public partial class ASPX_IROJVAR_SISMED_DisponibilidadIRO2 : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        if (!Page.IsPostBack)
        {
            //int vmonth = DateTime.Now.Month;
            //DDLMes.SelectedIndex = vmonth;
            txtEESS.Text = "05197-IRO";
            CargaInicial();
        }
        if (Page.IsPostBack)
        {
        }
    }

    [WebMethod]
    public static string GetDetDisponibilidad(string vIndicador, string vPeriodo, string vEESS, string vHidetipo)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        vPeriodo = ClassGlobal.tmpvAnio;
        string vCodEESS = vEESS.Substring(0, 5);
        string gDetAtenciones = "";

        string vWheresql = "SSO like '%" + vIndicador + "%' ";
        if (vIndicador== "Covid") { vWheresql = "COVID_19 is not null "; }
        string vWTipo = " AND CodMed IN (select codSISMED from SISMED.dbo.MedTipo where tipo='" + vHidetipo + "')";
        if (vHidetipo == "") { vWTipo = ""; }
        try
        {
            string qSql = "Select SSO, Establecimiento, Categoria_, COVID_19, CodMed, Medicamento, TIP, Est, PET, PRECIO, [cMes01], [cMes02], [cMes03], [cMes04], [cMes05], [cMes06], [cMes07], [cMes08], [cMes09], [cMes10], [cMes11], [cMes12], [" + vPeriodo + "], format(ConsProm, '##0.00') ConsProm, stk_fin_diario as Stock, format(COALESCE( stk_fin_diario / nullif(TotalConsumo / NULLIF(TotalConteo, 0), 0) , 0), '##0.00') as StkMes_2, CONS_MAX, CONS_MIN " +
                "from (select Disp.*, isnull(tabact.Consumo, 0) '" + vPeriodo + "', (disp.Stock - isnull(tabact.Consumo, 0)) + isnull(tabact.Ingresos, 0) stk_fin_diario, isnull(Disp.TOTAL_CONS, 0) + ISNULL(tabact.Consumo, 0) TotalConsumo, isnull(Disp.CONTEO_CONS, 0) + (case when tabact.Consumo>0 then 1 else 0 end) TotalConteo " +
                "from SISMED.dbo.Disp Disp full outer join " +
                "(select tf.codigo_pre2, sum(tf.Consumo) Consumo, sum(tf.ingre) + sum(tf.reingre) Ingresos, tf.codigo_med, sum(tabstk.stock_fin) stock_fin " +
                "from SISMED.dbo.tformdetcons tf full outer join (select codigo_pre2, periodo, codigo_med, stock_fin " +
                "from SISMED.dbo.tformdetcons where periodo = (select MAX(periodo) from SISMED.dbo.tformdetcons) ) tabstk on tf.codigo_med=tabstk.codigo_med and tf.codigo_pre=tf.codigo_pre " +
                "where tf.periodo like '" + vPeriodo + "%' " + //and tf.Consumo > 0 " +
                "group by tf.codigo_med, tf.codigo_pre2 ) tabact on Disp.CodES=tabact.codigo_pre2 and Disp.CodMed=tabact.codigo_med " +
                "where CodES='" + vCodEESS + "') tab " +
                "where " + vWheresql + vWTipo + " order by COALESCE( stk_fin_diario / nullif(TotalConsumo / NULLIF(TotalConteo, 0), 0) , 0) desc";
			gDetAtenciones += "<div style='display: none'>" + qSql + "</div>";

			SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            ClassGlobal.varDTGen = objdataset.Tables[0];

            //gvDetRecClie.DataSource = dtDato;
            //gvDetRecClie.DataBind();

            DataTable dt = objdataset.Tables[0];


            if (dt.Rows.Count > 0)
            {
                gDetAtenciones += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetAtenciones += "<table class='table table-hover' id='tbldscrg' style='text-align: right; font-size: 14px; '>";
                gDetAtenciones += "<th class=''>Nro </th>";
                for (int i = 0; i < dt.Columns.Count; i++)
                { gDetAtenciones += "<th class=''>" + dt.Columns[i].ColumnName  + " </th>"; }           
                gDetAtenciones += Environment.NewLine;
                int nroitem = 0;
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dbRow in dt.Rows)
                    {
                        nroitem += 1;
                        gDetAtenciones += "<tr>";
                        gDetAtenciones += "<td>" + nroitem + "</td>";
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            gDetAtenciones += "<td>" + dbRow[dt.Columns[i].ColumnName].ToString() + "</td>";
                        }
                        gDetAtenciones += "</tr>";
                        gDetAtenciones += Environment.NewLine;
                    }
                }
                gDetAtenciones += Environment.NewLine;
                gDetAtenciones += "</table>";
                gDetAtenciones += "||sep||";
                ///////////////////////////////////////////////////////////////////-*--///     
                gDetAtenciones += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetAtenciones += "<table class='table table-hover' id='tblbscrJS' style='text-align: right; font-size: 14px; '>";
                gDetAtenciones += "<th class=''><button class='btn btn-default' type='button' title='Max.: 3,000 Registros' onclick=\"exportTableToExcel('tbldscrg')\"><i class='fa fa-download '></i></button> </th>";
                gDetAtenciones += "<th class=''>Medicamento </th>";
                gDetAtenciones += "<th class=''>Precio</th>";
                gDetAtenciones += "<th class=''>Stock</th>";
                gDetAtenciones += "<th class=''>Consumo Promedio</th>";
                gDetAtenciones += "<th class=''>Stock por Mes</th>";
                gDetAtenciones += "<th class=''>Consumo Maximo</th>";
                gDetAtenciones += "<th class=''>Consumo Minimo</th>";
                gDetAtenciones += "</tr>" + Environment.NewLine;
                nroitem = 0;

                foreach (DataRow dbRow in dt.Rows)
                {
                    nroitem += 1;
                    gDetAtenciones += "<tr>";
                    gDetAtenciones += "<td>" + nroitem + "</td>";
                    gDetAtenciones += "<td style='text-align: left;'>" + dbRow["Medicamento"].ToString() + "</td>";
                    gDetAtenciones += "<td style='text-align: center;'>" + dbRow["Precio"].ToString() + "</td>";
                    gDetAtenciones += "<td style='text-align: center;'>" + dbRow["Stock"].ToString() + "</td>";
                    gDetAtenciones += "<td style='text-align: center;'>" + dbRow["ConsProm"].ToString() + "</td>";
                    gDetAtenciones += "<td style='text-align: center;'>" + dbRow["StkMes_2"].ToString() + "</td>";
                    gDetAtenciones += "<td style='text-align: center;'>" + dbRow["CONS_Max"].ToString() + "</td>";
                    gDetAtenciones += "<td style='text-align: center;'>" + dbRow["CONS_Min"].ToString() + "</td>";
                    gDetAtenciones += "</tr>";
                    gDetAtenciones += Environment.NewLine;
                }

                gDetAtenciones += "</table></div></div><hr style='border-top: 1px solid blue'>";
            }
            else
            {
                gDetAtenciones = "<table></table>||sep||<table></table><hr style='border-top: 1px solid blue'>";
            }

        }
        catch (Exception ex)
        {
            gDetAtenciones += "-" + "-" + ex.Message.ToString();
        }
        return gDetAtenciones;
    }

    [WebMethod]
    public static string GetEESS()
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetAtenciones = "";
        try
        {
            string qSql = "select * from SISMED.dbo.DispPorc order by ESTABLECIMIENTO";
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
                gDetAtenciones += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetAtenciones += "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
                gDetAtenciones += "<th class=''>CodEESS </th>";
                gDetAtenciones += "<th class=''>Establecimiento</th>";
                gDetAtenciones += "</tr>" + Environment.NewLine;
                int nroitem = 0;

                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    gDetAtenciones += "<tr onclick=\"txtEESSCarga('" + dbRow["CODIGO"].ToString() + "-" + dbRow["ESTABLECIMIENTO"].ToString() + "')\" data-dismiss='modal'>" +
                        "<td class='' >" + dbRow["CODIGO"].ToString() + "</td>";
                    gDetAtenciones += "<td style='text-align: left;' >" + dbRow["ESTABLECIMIENTO"].ToString() + "</td>" +
                        "</tr>";
                    gDetAtenciones += Environment.NewLine;
                }

                gDetAtenciones += "</table></div></div><hr style='border-top: 1px solid blue'>";
            }
        }
        catch (Exception ex)
        {
            gDetAtenciones += "-" + "-" + ex.Message.ToString();
        }
        return gDetAtenciones;
    }

    public void CargaInicial()
    {
        try
        {
            //con.Open();
            string qSql = "select * from SISMED.dbo.parametros;";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00); cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter(); adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet(); adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];
            DataRow dbRow = dtDato.Rows[0];

            txtAnio.Text = dbRow["periododisp"].ToString();
            ClassGlobal.tmpvAnio = dbRow["periodo"].ToString();

            //CargaPorcDisp();
            CargaPorcDisp2();
        }
        catch (Exception ex)
        {
            LitTABL1.Text = ex.Message.ToString();
        }
    }

    public void CargaPorcDisp()
    {
        string vPeriodo = txtAnio.Text;
        string vCodEESS = txtEESS.Text.Substring(0, 5);
        try
        {
            //con.Open();
            string qSql = "select *, format(DISP * 100, '##0.00') as pDISP, format(NORMO_STK * 100, '##0.00') as pNORMO_STK, format(SOB_No_Acep * 100, '##0.00') as pSOB_No_Acep, format(SUB_Critica * 100, '##0.00') as pSUB_Critica, format(SR * 100, '##0.00') as pSR, format(DES * 100, '##0.00') as pDES from SISMED.dbo.DispPorc where CODIGO='" + vCodEESS + "' and PERIODO='" + vPeriodo + "'";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00); cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter(); adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet(); adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];
            DataRow dbRow = dtDato.Rows[0];

            divDisp.InnerHtml = dbRow["pDISP"].ToString() + "<small>%</small>";
            divDispNivel.InnerHtml = dbRow["Nivel_de_Disponibilidad"].ToString();
            divNormo.InnerHtml = dbRow["pNORMO_STK"].ToString() + "<small>%</small>";
            divSobre.InnerHtml = dbRow["pSOB_No_Acep"].ToString() + "<small>%</small>";
            divSub.InnerHtml = dbRow["pSUB_Critica"].ToString() + "<small>%</small>";
            divSin.InnerHtml = dbRow["pSR"].ToString() + "<small>%</small>";
            divDes.InnerHtml = dbRow["pDES"].ToString() + "<small>%</small>";

        }
        catch (Exception ex)
        {
            LitTABL1.Text = ex.Message.ToString();
        }
    }

    /// <summary>
    /// /////////////////////////////////////////////////////////////
    public void CargaPorcDisp2()
    {
        string vPeriodo = txtAnio.Text;
        string vCodEESS = txtEESS.Text.Substring(0, 5);
        try
        {
            string qSql = "SELECT *, pOPT + pSOB + pSR AS pDISP FROM " +
                "(" +
                "SELECT sum(CANT) CANT, " +
                "	sum(case when NORMOSTK=1 then 1 else 0 end) NORMOSTK, " +
                "	sum(case when SOB=1 then 1 else 0 end) SOB, " +
                "	sum(case when SUB=1 then 1 else 0 end) SUB, " +
                "	sum(case when SR=1 then 1 else 0 end) SR, " +
                "	sum(case when [DES]=1 then 1 else 0 end) [DES], " +
                "	sum(case when NORMOSTK=1 then 1 else 0 end) / sum(CANT) * 100 AS pOPT, " +
                "	sum(case when SOB=1 then 1 else 0 end) / sum(CANT) * 100 pSOB, " +
                "	sum(case when SUB=1 then 1 else 0 end) / sum(CANT) * 100 pSUB, " +
                "	sum(case when SR=1 then 1 else 0 end) / sum(CANT) * 100 pSR, " +
                "	sum(case when [DES]=1 then 1 else 0 end) / sum(CANT) * 100 pDES " +
                "FROM SISMED.dbo.Disp " +
                "WHERE CodES='" + vCodEESS + "' " +
                ") TAB1";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00); cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter(); adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet(); adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];
            DataRow dbRow = dtDato.Rows[0];

            divDisp.InnerHtml = Math.Round(Convert.ToDouble(dbRow["pDISP"]), 2) + "<small>%</small>";
            divNormo.InnerHtml = Math.Round(Convert.ToDouble(dbRow["pOPT"]), 2) + "<small>%</small>";
            divSobre.InnerHtml = Math.Round(Convert.ToDouble(dbRow["pSOB"]), 2) + "<small>%</small>";
            divSub.InnerHtml = Math.Round(Convert.ToDouble(dbRow["pSUB"]), 2) + "<small>%</small>";
            divSin.InnerHtml = Math.Round(Convert.ToDouble(dbRow["pSR"]), 2) + "<small>%</small>";
            divDes.InnerHtml = Math.Round(Convert.ToDouble(dbRow["pDES"]), 2) + "<small>%</small>";

            string vNivelDispo = "";
            double vNivelD = Convert.ToDouble(dbRow["pDISP"]);
            if (vNivelD > 90) { vNivelDispo = "OPTIMO"; }
            else if (vNivelD > 80) { vNivelDispo = "ALTO"; }
            else if (vNivelD > 70) { vNivelDispo = "REGULAR"; }
            else { vNivelDispo = "BAJO"; }
            divDispNivel.InnerHtml = vNivelDispo;
        }
        catch (Exception ex)
        {
            divDispNivel.InnerHtml = ex.Message.ToString();
            LitTABL1.Text = ex.Message.ToString();
        }
    }
    /// /////////////////////////////////////////////////////////////
    /// </summary>

    public void CargaDispon()
    {
        string vPeriodo = ClassGlobal.tmpvAnio;
        string vCodEESS = txtEESS.Text.Substring(0, 5);
        //txtEESS.Text = vCodEESS;
        //int vAnio = DateTime.Now.Year;
        string qSql = "";
        try
        {
            //con.Open();
            qSql = "Select *, format(COALESCE( stk_fin_diario / nullif(TotalConsumo / NULLIF(TotalConteo, 0), 0) , 0), '##0.00') as StkMes_2 " +
                "from " +
                "(select Disp.*, isnull(tabact.Consumo, 0) '" + vPeriodo + "', " +
                    "disp.Stock - isnull(tabact.Consumo, 0) stk_fin_diario, " +
                    "isnull(Disp.TOTAL_CONS, 0) + ISNULL(tabact.Consumo, 0) TotalConsumo, " +
                    "isnull(Disp.CONTEO_CONS, 0) + (case when tabact.Consumo>0 then 1 else 0 end) TotalConteo " +
                "from SISMED.dbo.Disp Disp full outer join " +
                "(select tf.codigo_pre2, sum(tf.Consumo) Consumo, tf.codigo_med, sum(tabstk.stock_fin) stock_fin " +
                "from SISMED.dbo.tformdetcons tf full outer join " +
                "(select codigo_pre2, periodo, codigo_med, stock_fin " +
                "from SISMED.dbo.tformdetcons where periodo = (select MAX(periodo) from SISMED.dbo.tformdetcons) ) tabstk on tf.codigo_med=tabstk.codigo_med and tf.codigo_pre=tf.codigo_pre " +
                "where tf.periodo like '" + vPeriodo + "%' and tf.Consumo > 0 " +
                "group by tf.codigo_med, tf.codigo_pre2 ) tabact on Disp.CodES=tabact.codigo_pre2 and Disp.CodMed=tabact.codigo_med " +
                "where CodES='" + vCodEESS + "') tab ;";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];

            GVtable.DataSource = dtDato;
            GVtable.DataBind();
            //CargaTabla(dtDato);
        }
        catch (Exception ex)
        {
            //Label1.Text = "Error";
            ex.Message.ToString();
            LitTABL1.Text = ex.Message.ToString() + Environment.NewLine + qSql;
        }
    }

    protected void bntBuscar_Click(object sender, EventArgs e)
    {
        //CargaDispon();
        HideTipo.Value = "";
        DDLTipo.SelectedValue = "";
        CargaInicial();
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
        response.Write(LitTABL1_1.InnerHtml);
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

    protected void btnFiltrar_Click(object sender, EventArgs e)
    {
        string vPeriodo = txtAnio.Text;
        string vCodEESS = txtEESS.Text.Substring(0, 5);
        string vMedTipo = DDLTipo.SelectedValue;
        string vWherey = "";

        switch (vMedTipo)
        {
            case "":
                break;
            case "Petitorio":
                vWherey = " and (TIP = 'M' and Est <> 'E' and PET = 'P')";
                break;
            case "COVID-19":
                vWherey = " AND CodMed IN (select codSISMED from SISMED.dbo.MedTipo where tipo='" + vMedTipo + "')";
                break;
            default:
                break;
        }

        try
        {
            //con.Open();
            string qSql = "SELECT *, pOPT + pSOB + pSR AS pDISP FROM " +
                "(" +
                "SELECT sum(CANT) CANT, " +
                "	sum(case when NORMOSTK=1 then 1 else 0 end) NORMOSTK, " +
                "	sum(case when SOB=1 then 1 else 0 end) SOB, " +
                "	sum(case when SUB=1 then 1 else 0 end) SUB, " +
                "	sum(case when SR=1 then 1 else 0 end) SR, " +
                "	sum(case when [DES]=1 then 1 else 0 end) [DES], " +
                "	sum(case when NORMOSTK=1 then 1 else 0 end) / sum(CANT) * 100 AS pOPT, " +
                "	sum(case when SOB=1 then 1 else 0 end) / sum(CANT) * 100 pSOB, " +
                "	sum(case when SUB=1 then 1 else 0 end) / sum(CANT) * 100 pSUB, " +
                "	sum(case when SR=1 then 1 else 0 end) / sum(CANT) * 100 pSR, " +
                "	sum(case when [DES]=1 then 1 else 0 end) / sum(CANT) * 100 pDES " +
                "FROM SISMED.dbo.Disp " +
                "WHERE CodES='" + vCodEESS + "' " + vWherey + 
                ") TAB1";
            //LitTABL1.Text = "<div style='display: none'>" + qSql + "</div>";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00); cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter(); adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet(); adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];
            DataRow dbRow = dtDato.Rows[0];

            LitTABL1.Text += "<div style='display: none'>" + qSql + "</div>";

            HideTipo.Value = vMedTipo;

            divDisp.InnerHtml = Math.Round(Convert.ToDouble(dbRow["pDISP"]), 2) + "<small>%</small>";
            divNormo.InnerHtml = Math.Round(Convert.ToDouble(dbRow["pOPT"]), 2) + "<small>%</small>";
            divSobre.InnerHtml = Math.Round(Convert.ToDouble(dbRow["pSOB"]), 2) + "<small>%</small>";
            divSub.InnerHtml = Math.Round(Convert.ToDouble(dbRow["pSUB"]), 2) + "<small>%</small>";
            divSin.InnerHtml = Math.Round(Convert.ToDouble(dbRow["pSR"]), 2) + "<small>%</small>";
            divDes.InnerHtml = Math.Round(Convert.ToDouble(dbRow["pDES"]), 2) + "<small>%</small>";

            string vNivelDispo = "";
            double vNivelD = Convert.ToDouble(dbRow["pDISP"]);
            if (vNivelD > 90) { vNivelDispo = "OPTIMO"; }
            else if (vNivelD > 80) { vNivelDispo = "ALTO"; }
            else if (vNivelD > 70) { vNivelDispo = "REGULAR"; }
            else { vNivelDispo = "BAJO"; }
            divDispNivel.InnerHtml = vNivelDispo;

            //AMIGO SIY

            //this.Page.Response.Write("<script language='JavaScript'>window.alert('" + dbRow["pSOB"].ToString() + "');</script>");
        }
        catch (Exception ex)
        {
            divDispNivel.InnerHtml = ex.Message.ToString();
            LitTABL1.Text += ex.Message.ToString();
        }
    }
}