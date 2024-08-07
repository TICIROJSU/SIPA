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

public partial class ASPX_IROJVAR_HISMINSA_AtencXservic : System.Web.UI.Page
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
        }
    }

    protected void bntBuscar_Click(object sender, EventArgs e)
    {
        CargaTablaDT();
    }

    public void CargaTablaDT()
    {
        string vAnio = DDLAnio.SelectedValue;
        try
        {
            //con.Open();
            string qSql = "select COD_SERVSA, COD_SERVSA1, DSC_SER, " +
                "sum(case when MES='1' then Aten end) Aten1, sum(case when MES='1' then SIS end) SIS1, sum(case when MES='1' then Usu end) Usu1, sum(case when MES='1' then Exo end) Exo1, sum(case when MES='1' then otr end) otr1, " +
                "sum(case when MES='2' then Aten end) Aten2, sum(case when MES='2' then SIS end) SIS2, sum(case when MES='2' then Usu end) Usu2, sum(case when MES='2' then Exo end) Exo2, sum(case when MES='2' then otr end) otr2, " +
                "sum(case when MES='3' then Aten end) Aten3, sum(case when MES='3' then SIS end) SIS3, sum(case when MES='3' then Usu end) Usu3, sum(case when MES='3' then Exo end) Exo3, sum(case when MES='3' then otr end) otr3, " +
                "sum(case when MES='4' then Aten end) Aten4, sum(case when MES='4' then SIS end) SIS4, sum(case when MES='4' then Usu end) Usu4, sum(case when MES='4' then Exo end) Exo4, sum(case when MES='4' then otr end) otr4, " +
                "sum(case when MES='5' then Aten end) Aten5, sum(case when MES='5' then SIS end) SIS5, sum(case when MES='5' then Usu end) Usu5, sum(case when MES='5' then Exo end) Exo5, sum(case when MES='5' then otr end) otr5, " +
                "sum(case when MES='6' then Aten end) Aten6, sum(case when MES='6' then SIS end) SIS6, sum(case when MES='6' then Usu end) Usu6, sum(case when MES='6' then Exo end) Exo6, sum(case when MES='6' then otr end) otr6, " +
                "sum(case when MES='7' then Aten end) Aten7, sum(case when MES='7' then SIS end) SIS7, sum(case when MES='7' then Usu end) Usu7, sum(case when MES='7' then Exo end) Exo7, sum(case when MES='7' then otr end) otr7, " +
                "sum(case when MES='8' then Aten end) Aten8, sum(case when MES='8' then SIS end) SIS8, sum(case when MES='8' then Usu end) Usu8, sum(case when MES='8' then Exo end) Exo8, sum(case when MES='8' then otr end) otr8, " +
                "sum(case when MES='9' then Aten end) Aten9, sum(case when MES='9' then SIS end) SIS9, sum(case when MES='9' then Usu end) Usu9, sum(case when MES='9' then Exo end) Exo9, sum(case when MES='9' then otr end) otr9, " +
                "sum(case when MES='10' then Aten end) Aten10, sum(case when MES='10' then SIS end) SIS10, sum(case when MES='10' then Usu end) Usu10, sum(case when MES='10' then Exo end) Exo10, sum(case when MES='10' then otr end) otr10, " +
                "sum(case when MES='11' then Aten end) Aten11, sum(case when MES='11' then SIS end) SIS11, sum(case when MES='11' then Usu end) Usu11, sum(case when MES='11' then Exo end) Exo11, sum(case when MES='11' then otr end) otr11, " +
                "sum(case when MES='12' then Aten end) Aten12, sum(case when MES='12' then SIS end) SIS12, sum(case when MES='12' then Usu end) Usu12, sum(case when MES='12' then Exo end) Exo12, sum(case when MES='12' then otr end) otr12 " +
                "from " +
                "(select COD_SERVSA, COD_SERVSA1, DSC_SER, MES, COUNT(ID_HIS) as Aten, " +
                "sum(case when FI='2' then 1 else 0 end) as SIS, " +
                "sum(case when FI='1' then 1 else 0 end) as Usu, " +
                "sum(case when FI='11' then 1 else 0 end) as Exo, " +
                "sum(case when (FI='1' or FI='2' or FI='11') then 0 else 1 end) as otr " +
                "from NUEVA.dbo.CHEQ2011 left join NUEVA.dbo.SERVICIO on COD_SERVSA1=COD_SER " +
                "where ANO='" + vAnio + "' " +
                "group by COD_SERVSA, COD_SERVSA1, DSC_SER, MES) tab1 " +
                "group by COD_SERVSA, COD_SERVSA1, DSC_SER " +
                "order by DSC_SER";

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
            html += "<th class=''>Cod</th>";
            html += "<th class=''>Servicio</th>";
            html += "<th class=''>Enero</th><th class=''>Febrero</th><th class=''>Marzo</th>";
            html += "<th class=''>Abril</th><th class=''>Mayo</th><th class=''>Junio</th>";
            html += "<th class=''>Julio</th><th class=''>Agosto</th><th class=''>Setiembre</th>";
            html += "<th class=''>Octubre</th><th class=''>Noviembre</th><th class=''>Diciembre</th>";
            html += "<th class=''>Total</th>";
            html += "</tr>" + Environment.NewLine;
            int nroitem = 0;
            foreach (DataRow dbRow in dtDato.Rows)
            {
                nroitem += 1;
                html += "<tr>";
                html += "<td class='' style='text-align: left;'>" + nroitem + "</td>";
                html += "<td class='' style='text-align: left;'>" + dbRow["COD_SERVSA1"].ToString() + "</td>";
                html += "<td class='' style='text-align: left;'>" + dbRow["DSC_SER"].ToString() + "</td>";
                for (int i = 1; i <= 12; i++)
                {
                    html += "<td class='' style='text-align: left;'>";
                    if (dbRow["Aten" + i] != DBNull.Value)
                    {
                        html += "<table border='1' data-toggle='modal' data-target='#modalAtenciones' onclick=\"fShowAtencion('" + dbRow["SIS" + i].ToString() + "')\">" +
                            "<tr><th colspan='3' style='text-align: center;'>" + dbRow["Aten" + i].ToString() + "</th></tr>" +
                            "		  <tr><td>Sis</td>" +
                            "			<td>Usu</td>" +
                            "			<td>Exo</td></tr>" +
                            "		  <tr><td style='text-align: center;'>" + dbRow["SIS" + i].ToString() + "</td>" +
                            "			<td style='text-align: center;'>" + dbRow["Usu" + i].ToString() + "</td>" +
                            "		<td style='text-align: center;'>" + dbRow["Exo" + i].ToString() + "</td></tr>" +
                            "</table>";
                    }
                    html += "</td>";
                }

                html += "<td class='' style='text-align: left;'>" + "<button type='button' class='btn bg-navy' data-toggle='modal' data-target='#modAtenc-'><i class='fa fa-fw fa-medkit'></i></button>" + "</td>";

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