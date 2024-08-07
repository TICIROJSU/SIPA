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

public partial class ASPX_IROJVAR_SISMED_DispoIROInterna : System.Web.UI.Page
{
	SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
	protected void Page_Load(object sender, EventArgs e)
	{
		Server.ScriptTimeout = 3600;
		if (!Page.IsPostBack)
		{
            int vmonth = DateTime.Now.Month;
            DDLMes.SelectedIndex = vmonth;
            int vAnio = DateTime.Now.Year;
            DDLAnio.Text = vAnio.ToString();

        }

    }

	protected void bntBuscar_Click(object sender, EventArgs e)
	{
		CargaTablaDT("601");
	}

	protected void btnPetitorio_Click(object sender, EventArgs e)
	{
		CargaTablaDT("606");
	}

	public void CargaTablaDT(string vOpt)
	{
		string vAnio = DDLAnio.SelectedValue;
		string vMes = DDLMes.SelectedValue;
		string vEESS = txtEESS.Text;
		try
		{
			//con.Open();
			//string qSql = "exec IROf.dbo.SPW_DISPONIBILIDAD_MENSUAL_MEDICAMENTOS_MMEDICAM 0, '', @Anio, @Mes, '', '', ''";
			string qSql = "exec IROf.dbo.SPW_DISPONIBILIDADMENSUALMEDICAMENTOS_FINAL @vopt, '', @Anio, @Mes, '', '', ''";
			SqlCommand cmd = new SqlCommand(qSql, conSAP00);
			cmd.CommandType = CommandType.Text;
			SqlDataAdapter adapter = new SqlDataAdapter();

			cmd.Parameters.AddWithValue("@Anio", Convert.ToString(DDLAnio.SelectedValue));
			cmd.Parameters.AddWithValue("@Mes", Convert.ToString(DDLMes.SelectedValue));
			cmd.Parameters.AddWithValue("@vopt", vOpt);

			adapter.SelectCommand = cmd;

			conSAP00.Open();
			DataSet objdataset = new DataSet();
			adapter.Fill(objdataset);
			conSAP00.Close();

			DataTable dtDato = objdataset.Tables[0];

			//GVtable.DataSource = dtDato;
			//GVtable.DataBind();

			int vNormoStk = Convert.ToInt32(dtDato.Compute("count(CODSISMED)", "SSO='NORMOSTOCK'").ToString());
			int vSobStk = Convert.ToInt32(dtDato.Compute("count(CODSISMED)", "SSO='SOBRESTOCK'").ToString());
			int vSubStk = Convert.ToInt32(dtDato.Compute("count(CODSISMED)", "SSO='SUBSTOCK'").ToString());
			int vSRStk = Convert.ToInt32(dtDato.Compute("count(CODSISMED)", "SSO='SIN ROTACION'").ToString());
			int vDesStk = Convert.ToInt32(dtDato.Compute("count(CODSISMED)", "SSO='DESABASTECIDO'").ToString());

			int vItems = vNormoStk + vSobStk + vSubStk + vSRStk + vDesStk;
			double intvNS = (double)vNormoStk / (double)vItems * 100.00;
			double intvSob = (double)vSobStk / (double)vItems * 100.00;
			double intvSub = (double)vSubStk / (double)vItems * 100.00;
			double intvSR = (double)vSRStk / (double)vItems * 100.00;
			double intvDes = (double)vDesStk / (double)vItems * 100.00;

			double intvDispo = intvNS + intvSob + intvSR;

			lblNS.Text = ClassGlobal.formatoMillarDec(intvNS.ToString()) + " %";
			lblSob.Text = ClassGlobal.formatoMillarDec(intvSob.ToString()) + " %";
			lblSub.Text = ClassGlobal.formatoMillarDec(intvSub.ToString()) + " %";
			lblSR.Text = ClassGlobal.formatoMillarDec(intvSR.ToString()) + " %";
			lblDes.Text = ClassGlobal.formatoMillarDec(intvDes.ToString()) + " %";
			lblDisp.Text = ClassGlobal.formatoMillarDec(intvDispo.ToString()) + " %";

			lblNSC.Text = vNormoStk.ToString();
			lblSobC.Text = vSobStk.ToString();
			lblSubC.Text = vSubStk.ToString();
			lblSRC.Text = vSRStk.ToString();
			lblDesC.Text = vDesStk.ToString();
			lblDispC.Text = vItems.ToString();

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
		string html = "";
		if (dtDato.Rows.Count > 0)
		{
			//html += "<table class='table table-condensed table-striped table-hover' style='text-align: right; font-size: 14px; '>";
			html += Environment.NewLine + "<table id='tblbscrJS' class='table table-hover' style='text-align: right; font-size: 14px; '>";
			html += "<tr>";
			html += "<th class=''></th>";
			html += "<th class=''>CodMED</th>";
			html += "<th class=''>CodSIGA</th>";
			html += "<th class=''>Medicamento</th>";
			html += "<th style='text-align: center;'>Cons Prom</th>";
			html += "<th class='' style='text-align: center;'>Stock</th>";
			html += "<th class=''>Stock Mes</th>";

			html += "<th class='' style='text-align: center;'>SSO</th>";
			html += "<th class='' style='text-align: center;'>Estado</th>";
			html += "</tr>" + Environment.NewLine;
			int nroitem = 0;
			foreach (DataRow dbRow in dtDato.Rows)
			{
				nroitem += 1;
				html += "<tr>";
				html += "<td class='' style='text-align: left;'>" + nroitem + "</td>";
				html += "<td class='' style='text-align: left;'>" + dbRow["CODSISMED"].ToString() + "</td>";
				html += "<td class='' style='text-align: left;'>" + dbRow["CODSIGA"].ToString() + "</td>";
				html += "<td class='' style='text-align: left;'>" + dbRow["MEDICAMENTO"].ToString() + "</td>";
				html += "<td class='' style='text-align: left;'>" + dbRow["CONSUMO_PROMEDIO"].ToString() + "</td>";
				html += "<td class='' style='text-align: center;'>" + dbRow["STOCK_TOTAL"].ToString() + "</td>";
				html += "<td class='' style='text-align: center;'>" + dbRow["STOCK_MES"].ToString() + "</td>";
				html += "<td class='' style='text-align: center;'>" + dbRow["SSO"].ToString() + "</td>";
				//html += "<td class='' style='text-align: center;'>" + dbRow["ESTADO"].ToString() + "</td>";
				html += "<td class='' style='text-align: center;'>" + "" + "</td>";
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
		//LitDetAtenciones.Text = htmlDetAtenciones;
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