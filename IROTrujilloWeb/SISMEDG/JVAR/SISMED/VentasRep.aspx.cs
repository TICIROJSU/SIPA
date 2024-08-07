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

public partial class SISMEDG_JVAR_SISMED_VentasRep : System.Web.UI.Page
{
	string html = "";
	SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_DIRESA);
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
		string vRed = DDLRed.SelectedValue;
		string vAnio = DDLAnio.SelectedValue;
		string vMes = DDLMes.SelectedValue;
		ClassGlobal.varGlobalTmp = vMes;
		string vCodProf = txtIdProf.Value;
		try
		{
			//con.Open();
			string qSql = "SELECT (case when REN.Red is null then 'zNULL' else REN.Red end) Red, REN.Microrred, ren.[Nombre del establecimiento] EESS, REN.Categoria, " +
				"aa.EstablecimientoDescripcion, aa.EmpresaId, aa.Establecimiento, cast(cast(a.fechadocumento as date) as varchar) as fecha, b.NumeroDocumento as Desde,b.NumeroDocumentoFinal as Hasta,b.TotalImporteVenta as Total " +
				"FROM FeEstablecimientos aa INNER JOIN FeResumenDiarioDocumentosCab a ON aa.ID=a.EstablecimientoID  INNER JOIN FeResumenDiarioDocumentosDet b ON a.ID=b.ResumendiarioCabID LEFT JOIN RENIPRESS REN ON aa.Establecimiento = REN.ESCodSISMED " +
				"where year(a.fechadocumento) = " + vAnio + " and MONTH(a.fechadocumento) = " + vMes + " and (REN.Red like '%" + vRed + "%' ) " + //or REN.Red is null
				"order by 1,2,3, fecha; ";
			SqlCommand cmd = new SqlCommand(qSql, conSAP00);
			cmd.CommandType = CommandType.Text;
			SqlDataAdapter adapter = new SqlDataAdapter();
			adapter.SelectCommand = cmd;

			conSAP00.Open();
			DataSet objdataset = new DataSet();
			adapter.Fill(objdataset);
			conSAP00.Close();

			GVtable.DataSource = objdataset.Tables[0];
			GVtable.DataBind();

			double vt_total = Convert.ToDouble(objdataset.Tables[0].Compute("sum(Total)", String.Empty));
			lblRuta.Text = "Total Mostrado: S/. " + ClassGlobal.formatoMillarDec(vt_total.ToString());

			//CargaTabla(dtDato);
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
			//html += "<table class='table table-condensed table-striped table-hover' style='text-align: right; font-size: 14px; '>";
			html += Environment.NewLine + "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
			html += "<tr>";
			html += "<th class=''></th>";
			html += "<th class=''>Plaza</th>";
			html += "<th class=''>Profesional</th>";
			html += "<th class=''>Profesion</th>";
			html += "<th class=''>Mes</th>";
			html += "<th class='' style='text-align: center;'>Atenciones</th>";
			html += "<th class=''>Accion</th>";

			html += "<th class='' style='text-align: center;'>SIS</th>";
			html += "<th class='' style='text-align: center;'>USU</th>";
			html += "<th class='' style='text-align: center;'>EXO</th>";
			html += "</tr>" + Environment.NewLine;
			int nroitem = 0;
			foreach (DataRow dbRow in dtDato.Rows)
			{
				nroitem += 1;
				html += "<tr>";
				html += "<td class='' style='text-align: left;'>" + nroitem + "</td>";
				html += "<td class='' style='text-align: left;'>" + dbRow["PLAZA"].ToString() + "</td>";
				html += "<td class='' style='text-align: left;'>" + dbRow["Profesional"].ToString() + "</td>";
				html += "<td class='' style='text-align: left;'>" + dbRow["Profesion"].ToString() + "</td>";
				html += "<td class='' style='text-align: left;'>" + dbRow["MES"].ToString() + "</td>";
				html += "<td class='bg-navy color-palette' style='text-align: center;'><b>" + dbRow["Atenciones"].ToString() + "</b></td>";
				string codtmp_modal = nroitem.ToString();
				html += "<td class='' style='text-align: left;'>" + "<button type='button' class='btn bg-navy' data-toggle='modal' data-target='#modAtenc-" + codtmp_modal + "'><i class='fa fa-fw fa-medkit'></i></button>" + "</td>";

				html += "<td class='' style='text-align: center;'>" + dbRow["SIS"].ToString() + "</td>";
				html += "<td class='' style='text-align: center;'>" + dbRow["USu"].ToString() + "</td>";
				html += "<td class='' style='text-align: center;'>" + dbRow["Exo"].ToString() + "</td>";
				html += "</tr>" + Environment.NewLine;

				//CargaDTAtenciones(DDLAnio.SelectedValue, DDLMes.SelectedValue, dbRow["PLAZA"].ToString(), dbRow["Profesional"].ToString(), dbRow["Profesion"].ToString(), codtmp_modal);
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
		//LitAtenciones.Text = htmlAtenciones;
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