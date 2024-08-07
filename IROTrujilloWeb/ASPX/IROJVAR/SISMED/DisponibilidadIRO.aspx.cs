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

public partial class ASPX_IROJVAR_SISMED_DisponibilidadIRO : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        if (!Page.IsPostBack)
        {
            //int vmonth = DateTime.Now.Month;
            //DDLMes.SelectedIndex = vmonth;
            CargaInicial();
        }
    }

    [WebMethod]
    public static string GetDetDisponibilidad(string vIndicador, string vPeriodo, string vEESS)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        vPeriodo = ClassGlobal.tmpvAnio;
        string vCodEESS = vEESS.Substring(0, 5);
        string gDetAtenciones = "";
        try
        {
            string qSql = "Select SSO, Establecimiento, Categoria_, COVID_19, CodMed, Medicamento, TIP, Est, PET, PRECIO, [201907], [201908], [201909], [201910], [201911], [201912], [202001], [202002], [202003], [202004], [202005], [202006], [202007], format(ConsProm, '##0.00') ConsProm, stk_fin_diario as Stock, format(COALESCE( stk_fin_diario / nullif(TotalConsumo / NULLIF(TotalConteo, 0), 0) , 0), '##0.00') as StkMes_2 from (select Disp.*, isnull(tabact.Consumo, 0) '" + vPeriodo+ "', disp.Stock - isnull(tabact.Consumo, 0) stk_fin_diario, isnull(Disp.TOTAL_CONS, 0) + ISNULL(tabact.Consumo, 0) TotalConsumo, isnull(Disp.CONTEO_CONS, 0) + (case when tabact.Consumo>0 then 1 else 0 end) TotalConteo from SISMED.dbo.Disp Disp full outer join (select tf.codigo_pre2, sum(tf.Consumo) Consumo, tf.codigo_med, sum(tabstk.stock_fin) stock_fin from SISMED.dbo.tformdetcons tf full outer join (select codigo_pre2, periodo, codigo_med, stock_fin from SISMED.dbo.tformdetcons where periodo = (select MAX(periodo) from SISMED.dbo.tformdetcons) ) tabstk on tf.codigo_med=tabstk.codigo_med and tf.codigo_pre=tf.codigo_pre where tf.periodo like '" + vPeriodo + "%' and tf.Consumo > 0 group by tf.codigo_med, tf.codigo_pre2 ) tabact on Disp.CodES=tabact.codigo_pre2 and Disp.CodMed=tabact.codigo_med where CodES='" + vCodEESS + "') tab where SSO like '%" + vIndicador+"%' ";
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

            CargaPorcDisp();
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

    public void CargaDispon()
    {
        string vPeriodo = ClassGlobal.tmpvAnio;
        string vCodEESS = txtEESS.Text.Substring(0,5);
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
        CargaInicial();
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