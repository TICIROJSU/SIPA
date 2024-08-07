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

public partial class SISMEDG_JVAR_Almacen_DistriPorDepe : System.Web.UI.Page
{
	SqlConnection conSISMED = new SqlConnection(ClassGlobal.conexion_EXTERNO);
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
	public static string GetGR(string vMovNumero)
	{
		SqlConnection conSISMEDi = new SqlConnection(ClassGlobal.conexion_EXTERNO);
		string gDetH = "";
		try
		{
			string qSql = "select movcoditip, MOVNUMERO, medcod, MEDCOD + ' - ' + MEDICAMENT + '-' + ISNULL(presentaci, '') + '-' + ISNULL(CONCENTRAC, '') + '-' + ISNULL(FORMA_FARM, '') Producto, MedRegSan, MedLote, MedFechVto, MOVCANTID, MOVPRECIO, MOVTOTAL from tmovimdet tmd left join catmedicam med on tmd.medcod = med.CODIGO_MED where MOVCODITIP = 'S' and CODDEP = '018A01' and movnumero = '" + vMovNumero + "';";
			SqlCommand cmd2 = new SqlCommand(qSql, conSISMEDi);
			cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
			adapter2.SelectCommand = cmd2;

			conSISMEDi.Open();
			DataSet objdataset = new DataSet();
			adapter2.Fill(objdataset);
			conSISMEDi.Close();

			DataTable dtDatoDetAt = objdataset.Tables[0];

			if (dtDatoDetAt.Rows.Count > 0)
			{
				gDetH += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
				gDetH += "<table id='tblGR' class='table table-hover' style='text-align: right; font-size: 14px; '>";
				gDetH += "<th class=''>Nro </th>";
				gDetH += "<th class=''>Producto</th>";
				gDetH += "<th class=''>Lote</th>";
				gDetH += "<th class=''>FechVto</th>";
				gDetH += "<th class=''>Cantidad</th>";
				gDetH += "</tr>" + Environment.NewLine;
				int nroitem = 0;

				foreach (DataRow dbRow in dtDatoDetAt.Rows)
				{
					nroitem += 1;
					gDetH += "<tr>" +
						"<td>" + nroitem + "</td>";
					gDetH += "<td style='text-align: left;' >" + dbRow["Producto"].ToString() + "</td>";
					gDetH += "<td style='text-align: left;' >" + dbRow["MedLote"].ToString() + "</td>";
					gDetH += "<td style='text-align: left;' >" + dbRow["MedFechVto"].ToString() + "</td>";
					gDetH += "<td style='text-align: left;' >" + dbRow["MOVCANTID"].ToString() + "</td>";
					gDetH += "</tr>";
					gDetH += Environment.NewLine;
				}

				gDetH += "</table></div></div><hr style='border-top: 1px solid blue'>";
			}
		}
		catch (Exception ex)
		{
			gDetH += "-" + "-" + ex.Message.ToString();
		}
		return gDetH;
	}

	[WebMethod]
	public static string GetMRed(string vRed)
	{
		SqlConnection conSISMEDi = new SqlConnection(ClassGlobal.conexion_EXTERNO);
		string gDetAtenciones = "";
		try
		{
			string qSql = "select Red, Microrred from CATEESS " +
				"where Red LIKE '" + vRed + "%' group by Red, Microrred; ";
			SqlCommand cmd2 = new SqlCommand(qSql, conSISMEDi);
			cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
			adapter2.SelectCommand = cmd2;

			conSISMEDi.Open();
			DataSet objdataset = new DataSet();
			adapter2.Fill(objdataset);
			conSISMEDi.Close();

			DataTable dtDatoDetAt = objdataset.Tables[0];

			if (dtDatoDetAt.Rows.Count > 0)
			{
				gDetAtenciones += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
				gDetAtenciones += "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
				gDetAtenciones += "<th class=''>Nro </th>";
				gDetAtenciones += "<th class=''>Micro Red</th>";
				gDetAtenciones += "</tr>" + Environment.NewLine;
				int nroitem = 0;

				foreach (DataRow dbRow in dtDatoDetAt.Rows)
				{
					nroitem += 1;
					gDetAtenciones += "<tr onclick=\"txtMRedCarga('" + dbRow["Microrred"].ToString() + "')\" data-dismiss='modal'>" +
						"<td>" + nroitem + "</td>";
					gDetAtenciones += "<td style='text-align: left;' >" + dbRow["Microrred"].ToString() + "</td>" +
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

	[WebMethod]
	public static string GetEESS(string vRed, string vMRed)
	{
		SqlConnection conSISMEDi = new SqlConnection(ClassGlobal.conexion_EXTERNO);
		string gDetAtenciones = "";
		try
		{
			string qSql = "select Red, Microrred, RIGHT(codEESS, 5) codEESS, max(EESSS) EESS " +
				"from CATEESS where Red LIKE '%" + vRed + "%' and Microrred like '%" + vMRed + "%' " +
				"group by Red, Microrred, RIGHT(codEESS, 5)";
			SqlCommand cmd2 = new SqlCommand(qSql, conSISMEDi);
			cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
			adapter2.SelectCommand = cmd2;

			conSISMEDi.Open();
			DataSet objdataset = new DataSet();
			adapter2.Fill(objdataset);
			conSISMEDi.Close();

			DataTable dtDatoDetAt = objdataset.Tables[0];

			if (dtDatoDetAt.Rows.Count > 0)
			{
				gDetAtenciones += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
				gDetAtenciones += "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
				gDetAtenciones += "<th class=''>Nro </th>";
				gDetAtenciones += "<th class=''>CodEESS</th>";
				gDetAtenciones += "<th class=''>Establecimiento</th>";
				gDetAtenciones += "</tr>" + Environment.NewLine;
				int nroitem = 0;

				foreach (DataRow dbRow in dtDatoDetAt.Rows)
				{
					nroitem += 1;
					gDetAtenciones += "<tr onclick=\"txtEESSCarga('" + dbRow["EESS"].ToString() + "', '" + dbRow["codEESS"].ToString() + "')\" data-dismiss='modal'>" +
						"<td>" + nroitem + "</td>";
					gDetAtenciones += "<td>" + dbRow["codEESS"].ToString() + "</td>";
					gDetAtenciones += "<td style='text-align: left;' >" + dbRow["EESS"].ToString() + "</td>" +
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
			string qSql = "select * from parametros;";

			SqlCommand cmd = new SqlCommand(qSql, conSISMED); cmd.CommandType = CommandType.Text;
			SqlDataAdapter adapter = new SqlDataAdapter(); adapter.SelectCommand = cmd;

			conSISMED.Open();
			DataSet objdataset = new DataSet(); adapter.Fill(objdataset);
			conSISMED.Close();

			DataTable dtDato = objdataset.Tables[0];
			DataRow dbRow = dtDato.Rows[0];

			//txtAnio.Text = dbRow["periododisp"].ToString();
			ClassGlobal.tmpvAnio = dbRow["periodo"].ToString();

			//CargaPorcDisp();
			//CargaPorcDisp2();
			CargaRed();
		}
		catch (Exception ex)
		{
			LitTABL1.Text = ex.Message.ToString();
		}
	}

	public void CargaRed()
	{
		try
		{
			string qSql = "select Red from CATEESS group by Red;";

			SqlCommand cmd = new SqlCommand(qSql, conSISMED); cmd.CommandType = CommandType.Text;
			SqlDataAdapter adapter = new SqlDataAdapter(); adapter.SelectCommand = cmd;

			conSISMED.Open();
			DataSet objdataset = new DataSet(); adapter.Fill(objdataset);
			conSISMED.Close();

			DataTable dt = objdataset.Tables[0];
			string gDetAtenciones = "";

			if (dt.Rows.Count > 0)
			{
				gDetAtenciones += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
				gDetAtenciones += "<table class='table table-hover' id='tblRed' style='text-align: right; font-size: 14px; '>";
				gDetAtenciones += "<th class=''>Nro </th>";
				gDetAtenciones += "<th class=''>Red </th>";

				int nroitem = 0;
				gDetAtenciones += Environment.NewLine;

				foreach (DataRow dbRow in dt.Rows)
				{
					nroitem += 1;
					gDetAtenciones += "<tr onclick=\"txtRedCarga('" + dbRow["Red"].ToString() + "')\" data-dismiss='modal'>";
					gDetAtenciones += "<td>" + nroitem + "</td>";
					gDetAtenciones += "<td style='text-align: left;'>" + dbRow["Red"].ToString() + "</td>";
					gDetAtenciones += "</tr>";
					gDetAtenciones += Environment.NewLine;
				}

				gDetAtenciones += "</table></div></div><hr style='border-top: 1px solid blue'>";
			}
			else
			{
				gDetAtenciones = "<table></table><hr style='border-top: 1px solid blue'>";
			}
			LitRed.Text = gDetAtenciones;
		}
		catch (Exception ex)
		{
			LitTABL1.Text = ex.Message.ToString();
		}

	}

	protected void bntBuscar_Click(object sender, EventArgs e)
	{
		string vRed = txtRed.Text;
		string vMRed = txtMicroRed.Text;
		string vcodEESS = txtEESSCodHide.Value;
		string gDetH = "";
		try
		{
			//con.Open();
			string qSql = "select MOVNUMERO, ALMCODIDST, es.EESSS, MOVNUMEDCO, MOVFECHREG, MOVTOT, MOVFFINAN, MOVREFE " +
				"from tmovim left join CATEESS es on ALMCODIDST = RIGHT(es.codEESS, 5) " +
				"where MOVCODITIP = 'S' and CODDEP = '018A01' " +
				"and ALMCODIDST in (select RIGHT(codEESS, 5) codEESS " +
				"from CATEESS where Red LIKE '%" + vRed + "%' and Microrred like '%" + vMRed + "%' and codEESS like '%" + vcodEESS + "%' " +
				"group by RIGHT(codEESS, 5)) order by MOVFECHREG desc;";

			SqlCommand cmd = new SqlCommand(qSql, conSISMED); cmd.CommandType = CommandType.Text;
			SqlDataAdapter adapter = new SqlDataAdapter(); adapter.SelectCommand = cmd;

			conSISMED.Open();
			DataSet objdataset = new DataSet(); adapter.Fill(objdataset);
			conSISMED.Close();

			DataTable dtDato = objdataset.Tables[0];

			//GVtable.DataSource = dtDato;
			//GVtable.DataBind();

			DataTable dtDatoDetAt = objdataset.Tables[0];

			//gDetAtenciones = CargaTablaDetAtenciones(dtDatoDetAt, vanio, vmes, vdia, turno, vplaza);
			if (dtDatoDetAt.Rows.Count > 0)
			{
				gDetH += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
				gDetH += "<table id='tbldscrg' class='table table-hover' style='text-align: right; font-size: 14px; '>";
				gDetH += "<caption>Distribucion a " + vRed + "-" + vMRed + "-" + vcodEESS + "</caption>";
				gDetH += "<tr>";
				gDetH += "<th class=''>N° </th>";
				gDetH += "<th class=''>Establecimiento</th>";
				gDetH += "<th class=''>Documento</th>";
				gDetH += "<th class=''>FechaReg</th>";
				gDetH += "<th class=''>Monto</th>";
				gDetH += "<th class=''>FF</th>";
				gDetH += "<th class=''>Referencia</th>";
				gDetH += "</tr>" + Environment.NewLine;
				int nroitem = 0;
				foreach (DataRow dbRow in dtDatoDetAt.Rows)
				{
					nroitem += 1;
					gDetH += "<tr data-toggle='modal' data-target='#modalGR' onclick=\"fShowGR('" + dbRow["MOVNUMERO"].ToString() + "')\">";
					//gDetH += "<tr data-toggle=\"modal\" data-target=\"#modalAtenCli\" onclick=\"DetAtenCli('" + vRed + "-" + vMRed + "-" + vcodEESS + "')\">";
					gDetH += "<td class='' style='text-align: left;'>" + nroitem + "</td>";
					gDetH += "<td class='' style='text-align: left;'>" + dbRow["ALMCODIDST"].ToString() + " - " + dbRow["EESSS"].ToString() + "</td>";
					gDetH += "<td class='' style='text-align: left;'>" + dbRow["MOVNUMEDCO"].ToString() + "</td>";
					gDetH += "<td class='' style='text-align: left;'>" + dbRow["MOVFECHREG"].ToString().Substring(0, 10) + "</td>";
					gDetH += "<td class='' style='text-align: left;'>" + dbRow["MOVTOT"].ToString() + "</td>";
					gDetH += "<td class='' style='text-align: left;'>" + dbRow["MOVFFINAN"].ToString() + "</td>";
					gDetH += "<td class='' style='text-align: left;'>" + dbRow["MOVREFE"].ToString() + "</td>";

					gDetH += "</tr>" + Environment.NewLine;
				}

				gDetH += "</table></div></div><hr style='border-top: 1px solid blue'>";

			}
			//gDetAtenciones - FINAL
			LitTABL1.Text = gDetH;

		}
		catch (Exception ex)
		{
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

}