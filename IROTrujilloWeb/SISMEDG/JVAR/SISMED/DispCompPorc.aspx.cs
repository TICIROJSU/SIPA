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


public partial class SISMEDG_JVAR_SISMED_DispCompPorc : System.Web.UI.Page
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

	public class CiudadEntity
	{
		public string cod { get; set; }
		public string descripcion { get; set; }
	}

	public class LocalidadEntity
	{
		public string cod { get; set; }
		public string descripcion { get; set; }
	}

	[WebMethod]
	public static List<CiudadEntity> GetCiudadesByPais(string pais)
	{
		var query = from item in GetCiudades(pais).AsEnumerable()
					select new CiudadEntity
					{
						cod = Convert.ToString(item["PTO_DIGITACION"]),
						descripcion = Convert.ToString(item["PTO_DIGITACION"])
					};

		return query.ToList();
	}

	[WebMethod]
	public static List<CiudadEntity> GetEESSByMRed(string MRed)
	{
		var query = from item in GetCiudades(MRed).AsEnumerable()
					select new CiudadEntity
					{
						cod = Convert.ToString(item["PTO_DIGITACION"]),
						descripcion = Convert.ToString(item["PTO_DIGITACION"])
					};

		return query.ToList();
	}

	private static DataTable GetCiudades(string vRed)
	{
		SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
		DataTable dt = new DataTable();

		try
		{
			string qSql = "select PTO_DIGITACION from SISMED.dbo.DispPorc where RED like '%" + vRed + "%' group by PTO_DIGITACION";
			SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
			cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
			adapter2.SelectCommand = cmd2;

			conSAP00i.Open();
			DataSet objdataset = new DataSet();
			adapter2.Fill(objdataset);
			conSAP00i.Close();

			dt = objdataset.Tables[0];
		}
		catch (Exception ex)
		{
			//gDetAtenciones += "-" + "-" + ex.Message.ToString();
		}

		return dt;

	}

	[WebMethod]
	public static string GetMRedesLLLL()
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
			string qSql = "select RED, RED RED2 from SISMED.dbo.DispPorc group by RED order by 1;" +
				"select LEFT(PERIODO, 4) Anio, ROUND(AVG(Disp) * 100, 2) Disp from SISMED.dbo.DispPorc group by LEFT(PERIODO, 4) order by 1";

			SqlCommand cmd = new SqlCommand(qSql, conSAP00); cmd.CommandType = CommandType.Text;
			SqlDataAdapter adapter = new SqlDataAdapter(); adapter.SelectCommand = cmd;

			conSAP00.Open();
			DataSet objdataset = new DataSet(); adapter.Fill(objdataset);
			conSAP00.Close();

			DataTable dtDato = objdataset.Tables[0];
			//DataTable dtDato2 = objdataset.Tables[1];

			DDLAnio.DataSource = dtDato;
			DDLAnio.DataValueField = "RED";
			DDLAnio.DataTextField = "RED2";
			DDLAnio.DataBind();
			DDLAnio.Items.Insert(0, new ListItem("GERESA", ""));

			DDLMRed.Items.Insert(0, new ListItem("Micro Red", ""));
			DDLEESS.Items.Insert(0, new ListItem("Estab. Salud", ""));


			LitGraf1.Text = getGrafico(objdataset.Tables[1], "% Disponibilidad Anual");

		}
		catch (Exception ex)
		{
			LitErrores.Text = ex.Message.ToString();
		}
	}

	public static string getGrafico(DataTable dtDato2, string lbllabel)
	{
		string vGraf1 = "<div id='chartContainer1' style='height: 500px; width: 100%;'></div> " +
			"<script type='text/javascript'> " +
			"window.onload = function () " +
			"{ var chart = new CanvasJS.Chart('chartContainer1', " +
			"{ animationEnabled: true, theme: 'light2', " +
			"title:{text: '" + lbllabel + "'}, " +
			"axisY:[{title: 'Porcentaje', minimum: 50, maximum: 100, " +
			"valueFormatString: \"#0'%'\", interval: 10}], " +
			"toolTip:{shared:true}, " +
			"data: " +
			"[ " +
			"{type: 'column', showInLegend: true, name:'%', legendText: '%', " +
			"yValueFormatString: \"#0.00'%'\", indexLabel: '{y}'," +
			"dataPoints: [";
		int posi = 0;
		foreach (DataRow dbRow in dtDato2.Rows)
		{
			posi = posi + 1;
			vGraf1 += "{x: " + posi + ", label: '" + dbRow["Anio"].ToString() + "', y: " + dbRow["Disp"].ToString() + " }, ";
		}
		vGraf1 += "]} " +
			"] " +
			"}); chart.render(); " +
			"} " +
			"</script>";
		return vGraf1;
	}

	public void DISAMensual()
	{
		string vRED = DDLAnio.SelectedValue;
		string vMRed = DDLMRed.SelectedValue;
		string vcES = DDLEESS.SelectedValue;
		try
		{
			//con.Open();
			string qSql = "select * from (" +
					"select top 12 PERIODO Anio, ROUND(AVG(Disp) * 100, 2) Disp " +
					"from SISMED.dbo.DispPorc " +
					"where RED like '%" + vRED + "%' and PTO_DIGITACION like '%" + vMRed + "%' and CODIGO like '%" + vcES + "%' " +
					"group by PERIODO order by 1 desc" +
				") tbl order by 1 asc";

			SqlCommand cmd = new SqlCommand(qSql, conSAP00); cmd.CommandType = CommandType.Text;
			SqlDataAdapter adapter = new SqlDataAdapter(); adapter.SelectCommand = cmd;

			conSAP00.Open();
			DataSet objdataset = new DataSet(); adapter.Fill(objdataset);
			conSAP00.Close();

			//DataTable dtDato = objdataset.Tables[0];

			LitGraf1.Text = getGrafico(objdataset.Tables[0], "% Disponibilidad Mensual");

			//LitErrores.Text = qSql;
		}
		catch (Exception ex)
		{
			LitErrores.Text = ex.Message.ToString();
		}
	}

	public void DISAAnual()
	{
		string vRED = DDLAnio.SelectedValue;
		string vMRed = DDLMRed.SelectedValue;
		string vcES = DDLEESS.SelectedValue;
		try
		{
			//con.Open();
			string qSql = "select LEFT(PERIODO, 4) Anio, ROUND(AVG(Disp) * 100, 2) Disp " +
				"from SISMED.dbo.DispPorc " +
				"where RED like '%" + vRED + "%' and PTO_DIGITACION like '%" + vMRed + "%' and CODIGO like '%" + vcES + "%' " +
				"group by LEFT(PERIODO, 4) order by 1";

			SqlCommand cmd = new SqlCommand(qSql, conSAP00); cmd.CommandType = CommandType.Text;
			SqlDataAdapter adapter = new SqlDataAdapter(); adapter.SelectCommand = cmd;

			conSAP00.Open();
			DataSet objdataset = new DataSet(); adapter.Fill(objdataset);
			conSAP00.Close();

			LitGraf1.Text = getGrafico(objdataset.Tables[0], "% Disponibilidad Anual");

			//LitErrores.Text = qSql;
		}
		catch (Exception ex)
		{
			LitErrores.Text = ex.Message.ToString();
		}
	}

	protected void bntBuscar_Click(object sender, EventArgs e)
	{
		ProcesarGrafico();
	}

	public void ProcesarGrafico()
	{
		string dfiltro = DDLFiltro.SelectedValue;

		if (dfiltro == "Anual")	{	DISAAnual();	}
		if (dfiltro == "Mensual"){	DISAMensual();	}
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
		//response.Write(LitTABL1_1.InnerHtml);
		response.End();
	}

	protected void ExportarExcel_Click(object sender, EventArgs e)
	{
		HttpResponse response = Response;
		StringWriter sw = new StringWriter();
		HtmlTextWriter htw = new HtmlTextWriter(sw);
		Page pageToRender = new Page();
		HtmlForm form = new HtmlForm();
		//form.Controls.Add(GVtable);
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


	protected void DDLAnio_SelectedIndexChanged(object sender, EventArgs e)
	{
		string vRed = DDLAnio.SelectedValue;
		try
		{
			string qSql = "select PTO_DIGITACION from SISMED.dbo.DispPorc where RED like '%" + vRed + "%' group by PTO_DIGITACION";

			SqlCommand cmd = new SqlCommand(qSql, conSAP00); cmd.CommandType = CommandType.Text;
			SqlDataAdapter adapter = new SqlDataAdapter(); adapter.SelectCommand = cmd;

			conSAP00.Open();
			DataSet objdataset = new DataSet(); adapter.Fill(objdataset);
			conSAP00.Close();

			DataTable dtDato = objdataset.Tables[0];

			DDLMRed.DataSource = dtDato;
			DDLMRed.DataValueField = "PTO_DIGITACION";
			DDLMRed.DataTextField = "PTO_DIGITACION";
			DDLMRed.DataBind();
			DDLMRed.Items.Insert(0, new ListItem("Micro Red", ""));
			DDLEESS.Items.Insert(0, new ListItem("Estab. Salud", ""));

			ProcesarGrafico();
		}
		catch (Exception ex)
		{
			LitErrores.Text = ex.Message.ToString();
		}
	}

	protected void DDLMRed_SelectedIndexChanged(object sender, EventArgs e)
	{
		string vRed = DDLAnio.SelectedValue;
		string vMRed = DDLMRed.SelectedValue;
		string vAnio = Convert.ToString(DateTime.Now.Year - 1);
		try
		{
			string qSql = "select CODIGO, ESTABLECIMIENTO from SISMED.dbo.DispPorc where RED like '%" + vRed + "%' and PTO_DIGITACION like '%" + vMRed + "%' and LEFT(PERIODO, 4) = '" + vAnio + "' group by CODIGO, ESTABLECIMIENTO";

			SqlCommand cmd = new SqlCommand(qSql, conSAP00); cmd.CommandType = CommandType.Text;
			SqlDataAdapter adapter = new SqlDataAdapter(); adapter.SelectCommand = cmd;

			conSAP00.Open();
			DataSet objdataset = new DataSet(); adapter.Fill(objdataset);
			conSAP00.Close();

			DataTable dtDato = objdataset.Tables[0];

			DDLEESS.DataSource = dtDato;
			DDLEESS.DataValueField = "CODIGO";
			DDLEESS.DataTextField = "ESTABLECIMIENTO";
			DDLEESS.DataBind();
			DDLEESS.Items.Insert(0, new ListItem("Estab. Salud", ""));

			ProcesarGrafico();
		}
		catch (Exception ex)
		{
			LitErrores.Text = ex.Message.ToString();
		}
	}

	protected void DDLEESS_SelectedIndexChanged(object sender, EventArgs e)
	{
		ProcesarGrafico();
	}

	protected void DDLFiltro_SelectedIndexChanged(object sender, EventArgs e)
	{
		ProcesarGrafico();
	}
}