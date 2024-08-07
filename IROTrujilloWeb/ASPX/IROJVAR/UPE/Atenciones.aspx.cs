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

public partial class ASPX_IROJVAR_UPE_Atenciones : System.Web.UI.Page
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
            DDLMes.SelectedIndex = vmonth;
            CargaTablaDTIni();
        }

        if (Page.IsPostBack)
        {
            GVtable.DataSource = null;
            GVtable.DataBind();
        }

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
        }

    }

    public void CargaTablaDT()
    {
        string vAnio = DDLAnio.SelectedValue;
        string vMes = DDLMes.SelectedValue;
        try
        {
            //con.Open();
            string qSql = "exec NUEVA.dbo.SP_JVAR_UPE_Atenciones 'AtenxMes', @Anio, @Mes, '' ";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@Anio", vAnio);
            cmd.Parameters.AddWithValue("@Mes", vMes);

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
            html += "<caption>" + DDLAnio.SelectedItem.Text + "</caption>";
            html += "<tr>";
            html += "<th class=''>Mes</th>";
            html += "<th class='text-center'>CIEX</th>" +
                "<th class=''>Diagnostico</th>" +
                "<th class=''>Total</th>" +
                "<th class=''>SIS</th>" +
                "<th class=''>Otros</th>";
            html += "<th class=''>0ã a 15ã</th>" +
                "<th class=''>16ᾱ a 30ᾱ</th>" +
                "<th class=''>31a a 45a</th>" +
                "<th class=''>46a a 60a</th>" +
                "<th class=''>mas de 60a</th>" +
                "<th class=''>NN</th>";
            html += "</tr>" + Environment.NewLine;
            ////////////////////////////////
            html += "<tr>";
            double vt_total = Convert.ToDouble(dtDato.Compute("sum(Total)", String.Empty));
            double vt_SIS = Convert.ToDouble(dtDato.Compute("sum(SIS)", String.Empty));
            double vt_Otros = Convert.ToDouble(dtDato.Compute("sum(Otros)", String.Empty));
            double vt_00a = Convert.ToDouble(dtDato.Compute("sum([0a_15a])", String.Empty));
            double vt_16a = Convert.ToDouble(dtDato.Compute("sum([16a_30a])", String.Empty));
            double vt_31a = Convert.ToDouble(dtDato.Compute("sum([31a_45a])", String.Empty));
            double vt_46a = Convert.ToDouble(dtDato.Compute("sum([46a_60a])", String.Empty));
            double vt_60a = Convert.ToDouble(dtDato.Compute("sum([mas_60a])", String.Empty));
            double vt_NNa = Convert.ToDouble(dtDato.Compute("sum(NN)", String.Empty));
            html += "<td></td><td></td><td class='text-rigth'><b>Totales</b></td>";
            html += "<td class='text-center'><b>" + vt_total + "</b></td>" +
                "<td class='text-left'><b>" + vt_SIS + " (" + ClassGlobal.formatoMillarDec((vt_SIS / vt_total * 100).ToString()) + " %)</b></td>" +
                "<td class='text-left'><b>" + vt_Otros + " (" + ClassGlobal.formatoMillarDec((vt_Otros / vt_total * 100).ToString()) + " %)</b></td>" +
                "<td class='text-left'><b>" + vt_00a + " (" + ClassGlobal.formatoMillarDec((vt_00a / vt_total * 100).ToString()) + " %)</b></td>" +
                "<td class='text-left'><b>" + vt_16a + " (" + ClassGlobal.formatoMillarDec((vt_16a / vt_total * 100).ToString()) + " %)</b></td>" +
                "<td class='text-left'><b>" + vt_31a + " (" + ClassGlobal.formatoMillarDec((vt_31a / vt_total * 100).ToString()) + " %)</b></td>" +
                "<td class='text-left'><b>" + vt_46a + " (" + ClassGlobal.formatoMillarDec((vt_46a / vt_total * 100).ToString()) + " %)</b></td>" +
                "<td class='text-left'><b>" + vt_60a + " (" + ClassGlobal.formatoMillarDec((vt_60a / vt_total * 100).ToString()) + " %)</b></td>" +
                "<td class='text-left'><b>" + vt_NNa + " (" + ClassGlobal.formatoMillarDec((vt_NNa / vt_total * 100).ToString()) + " %)</b></td>";
            html += "</tr>" + Environment.NewLine;
            int nroitem = 0;
            foreach (DataRow dbRow in dtDato.Rows)
            {
                nroitem += 1;
                double v_total = Convert.ToDouble(dbRow["Total"]);
                string v_SIS = ClassGlobal.formatoMillarDec((Convert.ToDouble(dbRow["SIS"]) / v_total * 100).ToString()),
                        v_Otros = ClassGlobal.formatoMillarDec((Convert.ToDouble(dbRow["Otros"]) / v_total * 100).ToString()),
                        v_0a_15a = ClassGlobal.formatoMillarDec((Convert.ToDouble(dbRow["0a_15a"]) / v_total * 100).ToString()),
                        v_16a_30a = ClassGlobal.formatoMillarDec((Convert.ToDouble(dbRow["16a_30a"]) / v_total * 100).ToString()),
                        v_31a_45a = ClassGlobal.formatoMillarDec((Convert.ToDouble(dbRow["31a_45a"]) / v_total * 100).ToString()),
                        v_46a_60a = ClassGlobal.formatoMillarDec((Convert.ToDouble(dbRow["46a_60a"]) / v_total * 100).ToString()),
                        v_mas_60a = ClassGlobal.formatoMillarDec((Convert.ToDouble(dbRow["mas_60a"]) / v_total * 100).ToString()),
                        v_NN = ClassGlobal.formatoMillarDec((Convert.ToDouble(dbRow["NN"]) / v_total * 100).ToString());
                html += "<tr>";
                html += "<td style='text-align: center;'>" + dbRow["MesNom"].ToString() + "</td>";
                html += "<td style='text-align: center;'>" + dbRow["CIEX"].ToString() + "</td>";
                html += "<td style='text-align: center;'>" + dbRow["Diagnostico"].ToString() + "</td>";
                html += "<td style='text-align: center;'>" + dbRow["Total"].ToString() + "</td>";
                html += "<td style='text-align: left;'>" + dbRow["SIS"].ToString() + " ( " + v_SIS + "% ) " + "</td>";
                html += "<td style='text-align: left;'>" + dbRow["Otros"].ToString() + " ( " + v_Otros + "% ) " + "</td>";
                html += "<td style='text-align: left;'>" + dbRow["0a_15a"].ToString() + " ( " + v_0a_15a + "% ) " + "</td>";
                html += "<td style='text-align: left;'>" + dbRow["16a_30a"].ToString() + " ( " + v_16a_30a + "% ) " + "</td>";
                html += "<td style='text-align: left;'>" + dbRow["31a_45a"].ToString() + " ( " + v_31a_45a + "% ) " + "</td>";
                html += "<td style='text-align: left;'>" + dbRow["46a_60a"].ToString() + " ( " + v_46a_60a + "% ) " + "</td>";
                html += "<td style='text-align: left;'>" + dbRow["mas_60a"].ToString() + " ( " + v_mas_60a + "% ) " + "</td>";
                html += "<td style='text-align: left;'>" + dbRow["NN"].ToString() + " ( " + v_NN + "% ) " + "</td>";
                //html += "<td style='text-align: left;'>" + "<button type='button' class='btn bg-navy' data-toggle='modal' data-target='#modalAtenciones'><i class='fa fa-fw fa-medkit'></i></button>" + "</td>";
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

        try
        {
            //con.Open();
            string qSql = "exec NUEVA.dbo.SP_JVAR_UPE_Atenciones 'AtenxAnio', @Anio, '', ''";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@Anio", vAnio);

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
                html += "<th class='text-center'>Total</th>" +
                    "<th class=''>SIS</th>" +
                    "<th class=''>Otros</th>";
                html += "<th class=''>0ã a 15ã</th>" +
                    "<th class=''>16ᾱ a 30ᾱ</th>" +
                    "<th class=''>31a a 45a</th>" +
                    "<th class=''>46a a 60a</th>" +
                    "<th class=''>mas de 60a</th>" +
                    "<th class=''>NN</th>";
                html += "</tr>" + Environment.NewLine;
                ////////////////////////////////
                html += "<tr>";
                double vt_total = Convert.ToDouble(dtDato.Compute("sum(Total)", String.Empty));
                double vt_SIS = Convert.ToDouble(dtDato.Compute("sum(SIS)", String.Empty));
                double vt_Otros = Convert.ToDouble(dtDato.Compute("sum(Otros)", String.Empty));
                double vt_00a = Convert.ToDouble(dtDato.Compute("sum([0a_15a])", String.Empty));
                double vt_16a = Convert.ToDouble(dtDato.Compute("sum([16a_30a])", String.Empty));
                double vt_31a = Convert.ToDouble(dtDato.Compute("sum([31a_45a])", String.Empty));
                double vt_46a = Convert.ToDouble(dtDato.Compute("sum([46a_60a])", String.Empty));
                double vt_60a = Convert.ToDouble(dtDato.Compute("sum([mas_60a])", String.Empty));
                double vt_NNa = Convert.ToDouble(dtDato.Compute("sum(NN)", String.Empty));
                html += "<td class='text-rigth'><b>Totales</b></td>";
                html += "<td class='text-center'><b>" + vt_total + "</b></td>" +
                    "<td class='text-left'><b>" + vt_SIS + " (" + ClassGlobal.formatoMillarDec((vt_SIS / vt_total * 100).ToString()) + " %)</b></td>" +
                    "<td class='text-left'><b>" + vt_Otros + " (" + ClassGlobal.formatoMillarDec((vt_Otros / vt_total * 100).ToString()) + " %)</b></td>" +
                    "<td class='text-left'><b>" + vt_00a + " (" + ClassGlobal.formatoMillarDec((vt_00a / vt_total * 100).ToString()) + " %)</b></td>" +
                    "<td class='text-left'><b>" + vt_16a + " (" + ClassGlobal.formatoMillarDec((vt_16a / vt_total * 100).ToString()) + " %)</b></td>" +
                    "<td class='text-left'><b>" + vt_31a + " (" + ClassGlobal.formatoMillarDec((vt_31a / vt_total * 100).ToString()) + " %)</b></td>" +
                    "<td class='text-left'><b>" + vt_46a + " (" + ClassGlobal.formatoMillarDec((vt_46a / vt_total * 100).ToString()) + " %)</b></td>" +
                    "<td class='text-left'><b>" + vt_60a + " (" + ClassGlobal.formatoMillarDec((vt_60a / vt_total * 100).ToString()) + " %)</b></td>" +
                    "<td class='text-left'><b>" + vt_NNa + " (" + ClassGlobal.formatoMillarDec((vt_NNa / vt_total * 100).ToString()) + " %)</b></td>";
                html += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                foreach (DataRow dbRow in dtDato.Rows)
                {
                    nroitem += 1;
                    double v_total = Convert.ToDouble(dbRow["Total"]);
                    string v_SIS = ClassGlobal.formatoMillarDec((Convert.ToDouble(dbRow["SIS"]) / v_total * 100).ToString()),
                            v_Otros = ClassGlobal.formatoMillarDec((Convert.ToDouble(dbRow["Otros"]) / v_total * 100).ToString()),
                            v_0a_15a = ClassGlobal.formatoMillarDec((Convert.ToDouble(dbRow["0a_15a"]) / v_total * 100).ToString()),
                            v_16a_30a = ClassGlobal.formatoMillarDec((Convert.ToDouble(dbRow["16a_30a"]) / v_total * 100).ToString()),
                            v_31a_45a = ClassGlobal.formatoMillarDec((Convert.ToDouble(dbRow["31a_45a"]) / v_total * 100).ToString()),
                            v_46a_60a = ClassGlobal.formatoMillarDec((Convert.ToDouble(dbRow["46a_60a"]) / v_total * 100).ToString()),
                            v_mas_60a = ClassGlobal.formatoMillarDec((Convert.ToDouble(dbRow["mas_60a"]) / v_total * 100).ToString()),
                            v_NN = ClassGlobal.formatoMillarDec((Convert.ToDouble(dbRow["NN"]) / v_total * 100).ToString());
                    html += "<tr>";
                    html += "<td style='text-align: center;'>" + dbRow["MesNom"].ToString() + "</td>";
                    html += "<td style='text-align: center;'>" + dbRow["Total"].ToString() + "</td>";
                    html += "<td style='text-align: left;'>" + dbRow["SIS"].ToString() + " ( " + v_SIS + "% ) " + "</td>";
                    html += "<td style='text-align: left;'>" + dbRow["Otros"].ToString() + " ( " + v_Otros + "% ) " + "</td>";
                    html += "<td style='text-align: left;'>" + dbRow["0a_15a"].ToString() + " ( " + v_0a_15a + "% ) " + "</td>";
                    html += "<td style='text-align: left;'>" + dbRow["16a_30a"].ToString() + " ( " + v_16a_30a + "% ) " + "</td>";
                    html += "<td style='text-align: left;'>" + dbRow["31a_45a"].ToString() + " ( " + v_31a_45a + "% ) " + "</td>";
                    html += "<td style='text-align: left;'>" + dbRow["46a_60a"].ToString() + " ( " + v_46a_60a + "% ) " + "</td>";
                    html += "<td style='text-align: left;'>" + dbRow["mas_60a"].ToString() + " ( " + v_mas_60a + "% ) " + "</td>";
                    html += "<td style='text-align: left;'>" + dbRow["NN"].ToString() + " ( " + v_NN + "% ) " + "</td>";
                    //html += "<td style='text-align: left;'>" + "<button type='button' class='btn bg-navy' data-toggle='modal' data-target='#modalAtenciones'><i class='fa fa-fw fa-medkit'></i></button>" + "</td>";
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
