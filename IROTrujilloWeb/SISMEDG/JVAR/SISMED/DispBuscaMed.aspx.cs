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

public partial class SISMEDG_JVAR_SISMED_DispBuscaMed : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        if (!Page.IsPostBack)
        {
            CargaInicial();
        }
        if (Page.IsPostBack)
        {
        }
    }
	
	[WebMethod]
	public static string GetMedicamentos(string txtFiltro)
	{
		SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
		string gDetAtenciones = "";
		try
		{
			string qSql = "select * from SISMED.dbo.DispMedicam where CodMed + Medicamento like '%' + @txt + '%'";
			SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
			cmd2.CommandType = CommandType.Text;
			cmd2.Parameters.AddWithValue("@txt", txtFiltro);

			SqlDataAdapter adapter2 = new SqlDataAdapter();	adapter2.SelectCommand = cmd2;

			conSAP00i.Open();
			DataSet objdataset = new DataSet();
			adapter2.Fill(objdataset);
			conSAP00i.Close();

			DataTable dt = objdataset.Tables[0];

			if (dt.Rows.Count > 0)
			{
				gDetAtenciones += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
				gDetAtenciones += "<table class='table table-hover' id='tblproductos' style='text-align: right; font-size: 14px; '>";
				gDetAtenciones += "<th class=''>Nro </th>";
				gDetAtenciones += "<th class=''>CodMed </th>";
				gDetAtenciones += "<th class=''>Medicamento </th>";

				int nroitem = 0;
				gDetAtenciones += Environment.NewLine;

				foreach (DataRow dbRow in dt.Rows)
				{
					nroitem += 1;
					gDetAtenciones += "<tr onclick=\"txtMedicamCarga('" + dbRow["CodMed"].ToString() + "', '" + dbRow["CodMed"].ToString() + "-" + dbRow["Medicamento"].ToString() + "')\" data-dismiss='modal'>";
					gDetAtenciones += "<td>" + nroitem + "</td>";
					gDetAtenciones += "<td style='text-align: left;'>" + dbRow["CodMed"].ToString() + "</td>";
					gDetAtenciones += "<td style='text-align: left;'>" + dbRow["Medicamento"].ToString() + "</td>";
					gDetAtenciones += "</tr>";
					gDetAtenciones += Environment.NewLine;
				}

				gDetAtenciones += "</table></div></div><hr style='border-top: 1px solid blue'>";
			}
			else
			{
				gDetAtenciones = "<table>" + "</table><hr style='border-top: 1px solid blue'>";
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
            //CargaPorcDisp2();
            //CargaProductos(); //--
        }
        catch (Exception ex)
        {
            LitTABL1.Text = ex.Message.ToString();
        }
    }

    public void CargaProductos()
    {
        try
        {
            string qSql = "exec SISMED.dbo.JVARSelMedicamento;";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00); cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter(); adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet(); adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dt = objdataset.Tables[0];
            string gDetAtenciones = "";

            if (dt.Rows.Count > 0)
            {
                gDetAtenciones += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetAtenciones += "<table class='table table-hover' id='tblproductos' style='text-align: right; font-size: 14px; '>";
                gDetAtenciones += "<th class=''>Nro </th>";
                gDetAtenciones += "<th class=''>CodMed </th>";
                gDetAtenciones += "<th class=''>Medicamento </th>";

                int nroitem = 0;
                gDetAtenciones += Environment.NewLine;

                foreach (DataRow dbRow in dt.Rows)
                {
                    nroitem += 1;
                    gDetAtenciones += "<tr onclick=\"txtMedicamCarga('" + dbRow["CodMed"].ToString() + "', '" + dbRow["CodMed"].ToString() + "-" + dbRow["Medicamento"].ToString() + "')\" data-dismiss='modal'>";
                    gDetAtenciones += "<td>" + nroitem + "</td>";
                    gDetAtenciones += "<td style='text-align: left;'>" + dbRow["CodMed"].ToString() + "</td>";
                    gDetAtenciones += "<td style='text-align: left;'>" + dbRow["Medicamento"].ToString() + "</td>";
                    gDetAtenciones += "</tr>";
                    gDetAtenciones += Environment.NewLine;
                }

                gDetAtenciones += "</table></div></div><hr style='border-top: 1px solid blue'>";
            }
            else
            {
                gDetAtenciones = "<table></table><hr style='border-top: 1px solid blue'>";
            }
            LitProductos.Text = gDetAtenciones;
        }
        catch (Exception ex)
        {
            LitTABL1.Text = ex.Message.ToString();
        }
        
    }


    protected void bntBuscar_Click(object sender, EventArgs e)
    {
        string vcodmed = txtHideCodMed.Value;
        try
        {
            //con.Open();
            string qSql = "select Red, PtoDig MicroRed, CodES, Establecimiento, cast(ConsProm as int) as ConsProm, Stock, cast(Stock_Mes as int) as Stock_Mes, SSO as Diponibilidad, cast(FEC_E as date) [F. Vencimiento] from SISMED.dbo.Disp where CodMed = '" + vcodmed + "' order by Stock_Mes desc;";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00); cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter(); adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet(); adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];

            GVtable.DataSource = dtDato;
            GVtable.DataBind();
        }
        catch (Exception ex)
        {
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

}