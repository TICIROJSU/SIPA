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

public partial class ASPX_IROJVAR_SISMED2_DispDisaReg : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        if (!Page.IsPostBack)
        {
            int vmonth = DateTime.Now.Month;
            DDLMes.SelectedIndex = vmonth;

        }

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
        string vEESS = txtDato.Text;
        try
        {
            //con.Open();
            string qSql = "select * from SISMED.dbo.DispDRegiones " +
                "where codigo_med + nombre_med like '%' + @Dato + '%' " +
                //"and nombre_eje like '%' + @Dato + '%' " +
                "order by nombre_eje, nomdisa, red, establec ";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@Dato", vEESS);

            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];

            GVtable.DataSource = dtDato;
            GVtable.DataBind();
            //CargaTabla(dtDato);

            ddlRed.Items.Clear();
            ddlSSO.Items.Clear();

            DataTable dtRed = dtDato.AsEnumerable().GroupBy(r => r.Field<string>("Red")).Select(g => g.First()).CopyToDataTable();
            DataTable dtSSO = dtDato.AsEnumerable().GroupBy(r => r.Field<string>("SSO")).Select(g => g.First()).CopyToDataTable();

            ListItem LisTMP = new ListItem("Todos", "", true);
            ddlRed.DataSource = dtRed;
            ddlRed.Items.Add(LisTMP);
            ddlRed.DataTextField = "Red";
            ddlRed.DataValueField = "Red";
            ddlRed.DataBind();

            ddlSSO.DataSource = dtSSO;
            ddlSSO.Items.Add(LisTMP);
            ddlSSO.DataTextField = "SSO";
            ddlSSO.DataValueField = "SSO";
            ddlSSO.DataBind();

        }
        catch (Exception ex)
        {
            //Label1.Text = "Error";
            ex.Message.ToString();
            LitTABL1.Text = ex.Message.ToString();
        }
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
