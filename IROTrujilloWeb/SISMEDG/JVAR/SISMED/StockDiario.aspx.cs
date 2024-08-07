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

public partial class SISMEDG_JVAR_SISMED_StockDiario : System.Web.UI.Page
{
	SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
	SqlConnection conEXTER = new SqlConnection(ClassGlobal.conexion_EXTERNO);
	string vgWhere;
	protected void Page_Load(object sender, EventArgs e)
	{
		Server.ScriptTimeout = 3600;
		if (!Page.IsPostBack)
		{
			vgWhere = "Red is null and Microrred is null";
			CargaInicial();
		}
		if (Page.IsPostBack)
		{
			string vRed = txtRed.Text;
			string vMRed = txtMicroRed.Text;
			vgWhere = "Red like '%" + vRed + "%' and Microrred like '%" + vMRed + "%'";
		}
	}

	[WebMethod]
	public static string GetMRed(string vRed)
	{
		SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
		string gDetAtenciones = "";
		try
		{
			string qSql = "select Red, PtoDig " +
				"from SISMED.dbo.Disp where Red like '" + vRed + "%' group by Red, PtoDig; ";
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
				gDetAtenciones += "<th class=''>Nro </th>";
				gDetAtenciones += "<th class=''>Micro Red</th>";
				gDetAtenciones += "</tr>" + Environment.NewLine;
				int nroitem = 0;

				foreach (DataRow dbRow in dtDatoDetAt.Rows)
				{
					nroitem += 1;
					gDetAtenciones += "<tr onclick=\"txtMRedCarga('" + dbRow["PtoDig"].ToString() + "')\" data-dismiss='modal'>" +
						"<td>" + nroitem + "</td>";
					gDetAtenciones += "<td style='text-align: left;' >" + dbRow["PtoDig"].ToString() + "</td>" +
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
		SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
		string gDetAtenciones = "";
		try
		{
			string qSql = "select Red, PtoDig, CodES, Establecimiento " +
				"from SISMED.dbo.Disp where Red like '" + vRed + "%' and PtoDig like '" + vMRed + "%' " +
				"group by Red, PtoDig, CodES, Establecimiento; ";
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
				gDetAtenciones += "<th class=''>Nro </th>";
				gDetAtenciones += "<th class=''>CodEESS</th>";
				gDetAtenciones += "<th class=''>Establecimiento</th>";
				gDetAtenciones += "</tr>" + Environment.NewLine;
				int nroitem = 0;

				foreach (DataRow dbRow in dtDatoDetAt.Rows)
				{
					nroitem += 1;
					gDetAtenciones += "<tr onclick=\"txtEESSCarga('" + dbRow["Establecimiento"].ToString() + "', '" + dbRow["CodES"].ToString() + "')\" data-dismiss='modal'>" +
						"<td>" + nroitem + "</td>";
					gDetAtenciones += "<td>" + dbRow["CodES"].ToString() + "</td>";
					gDetAtenciones += "<td style='text-align: left;' >" + dbRow["Establecimiento"].ToString() + "</td>" +
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

			ClassGlobal.tmpvAnio = dbRow["periodo"].ToString();

			//CargaPorcDisp();
			//CargaPorcDisp2();
			CargaRed();
			bntBuscar_Click(1, null);
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
			string qSql = "select Red from SISMED.dbo.Disp group by Red;";

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
		string vEESS = txtEESS.Text;
		try
		{
			//con.Open();
			string qSql = "select Red, Microrred, codDEP, EESSS, count(1) Items, MAX(DATEADD(HOUR, -0, fechCarga)) as FechCarga " +
                "from mstkalmde left join CATEESS on '000' + left(codDEP, 5) = codEESS " +
				"where " + vgWhere + " group by Red, Microrred, codDEP, EESSS, cast(fechCarga as date) " +
                "order by cast(fechCarga as date) desc, Red asc, Microrred asc; ";

			SqlCommand cmd = new SqlCommand(qSql, conEXTER); cmd.CommandType = CommandType.Text;
			SqlDataAdapter adapter = new SqlDataAdapter(); adapter.SelectCommand = cmd;

			conEXTER.Open();
			DataSet objdataset = new DataSet(); adapter.Fill(objdataset);
			conEXTER.Close();

			//DataTable dtDato = objdataset.Tables[0];
			//GVtable.DataSource = dtDato;
			//GVtable.DataBind();

			DataTable dt = objdataset.Tables[0];
			string gDetAtenciones = "";

			if (dt.Rows.Count > 0)
			{
				gDetAtenciones += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
				gDetAtenciones += "<table class='table table-hover' id='tbldscrg' style='text-align: right; font-size: 14px; '>";
                gDetAtenciones += "<thead><tr>";
                gDetAtenciones += "<th class=''>Nro </th>";
                gDetAtenciones += "<th class=''>Red </th>";
				gDetAtenciones += "<th class=''>Micro Red</th>";
				gDetAtenciones += "<th class=''>CodEESS </th>";
				gDetAtenciones += "<th class=''>Estab. de Salud </th>";
				gDetAtenciones += "<th class=''>Items </th>";
				gDetAtenciones += "<th class=''>Fech Carga </th>";
                gDetAtenciones += "</tr></thead>";
                gDetAtenciones += "<tbody>";

                int nroitem = 0;
				gDetAtenciones += Environment.NewLine;

				foreach (DataRow dbRow in dt.Rows)
				{
					nroitem += 1;
					//gDetAtenciones += "<tr onclick=\"txtMedicamCarga('" + dbRow["CodMed"].ToString() + "', '" + dbRow["CodMed"].ToString() + "-" + dbRow["Medicamento"].ToString() + "')\" data-dismiss='modal'>";
					//DateTime varFCarga = (Convert.ToDateTime(dbRow["FechCarga"].ToString())).Date;
					//DateTime varFAct = (DateTime.Now).Date;
					string vColor = "";
					//if (varFCarga < varFAct) { vColor = "style='color:red'"; }

					gDetAtenciones += "<tr onclick=\"jsCargaMedicam('" + dbRow["codDEP"].ToString() + "')\" data-toggle='modal' data-target=\"#modalstkprod\" " + vColor + ">";
					gDetAtenciones += "<td>" + nroitem + "</td>";
					gDetAtenciones += "<td style='text-align: left;'>" + dbRow["Red"].ToString() + "</td>";
					gDetAtenciones += "<td style='text-align: left;'>" + dbRow["Microrred"].ToString() + "</td>";
					gDetAtenciones += "<td style='text-align: left;'>" + dbRow["codDEP"].ToString() + "</td>";
					gDetAtenciones += "<td style='text-align: left;'>" + dbRow["EESSS"].ToString() + "</td>";
					gDetAtenciones += "<td style='text-align: left;'>" + dbRow["Items"].ToString() + "</td>";
					gDetAtenciones += "<td style='text-align: left;'>" + dbRow["FechCarga"].ToString() + "</td>";
					gDetAtenciones += "</tr>";
					gDetAtenciones += Environment.NewLine;
				}

				gDetAtenciones += "</tbody></table></div></div><hr style='border-top: 1px solid blue'>";
			}
			else
			{
				gDetAtenciones = "<table></table><hr style='border-top: 1px solid blue'>";
			}
			LitTABL1.Text = gDetAtenciones;

		}
		catch (Exception ex)
		{
			LitTABL1.Text = ex.Message.ToString();
		}
	}

	[WebMethod]
	public static string GetStkProd(string vEESS)
	{
		SqlConnection conEXTERi = new SqlConnection(ClassGlobal.conexion_EXTERNO);
		string gDetstr = "";
		try
		{
			string qSql = "Select codDEP, MEDCOD, MEDICAMENT + '-' + isnull(PRESENTACI, '') + '-' + isnull(CONCENTRAC, '') + '-' + isnull(FORMA_FARM, '') as Medicamento, sum(STKSALDODE) Stock, MAX(fechCarga) Fech, Max(ESTADO) Estado " +
				"from mstkalmde left join CATMEDICAM on MEDCOD=CODIGO_MED " +
				"where codDEP = '" + vEESS + "' " +
				"group by codDEP, MEDCOD, MEDICAMENT + '-' + isnull(PRESENTACI, '') + '-' + isnull(CONCENTRAC, '') + '-' + isnull(FORMA_FARM, ''); ";
			SqlCommand cmd2 = new SqlCommand(qSql, conEXTERi);
			cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
			adapter2.SelectCommand = cmd2;

			conEXTERi.Open();
			DataSet objdataset = new DataSet();
			adapter2.Fill(objdataset);
			conEXTERi.Close();

			DataTable dtDatoDetAt = objdataset.Tables[0];

			if (dtDatoDetAt.Rows.Count > 0)
			{
				gDetstr += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
				gDetstr += "<table id='tblstkprod' class='table table-hover' style='text-align: right; font-size: 14px; '>";
				gDetstr += "<th class=''>Nro </th>";
				gDetstr += "<th class=''>codDEP</th>";
				//gDetstr += "<th class=''>MEDCOD</th>";
				gDetstr += "<th class=''>Medicamento</th>";
				gDetstr += "<th class=''>Stock</th>";
				gDetstr += "<th class=''>Fech</th>";
				gDetstr += "<th class=''>Estado</th>";
				gDetstr += "</tr>" + Environment.NewLine;
				int nroitem = 0;
				string trColor = "";
				foreach (DataRow dbRow in dtDatoDetAt.Rows)
				{
					trColor = "";
					if (dbRow["Estado"].ToString()=="I")
					{
						trColor = "style='background-color:#C97D74'";
					}
					nroitem += 1;
					//gDetstr += "<tr " + trColor + " onclick=\"txtMRedCarga('" + dbRow["codDEP"].ToString() + "')\" data-dismiss='modal'>" +
					gDetstr += "<tr " + trColor + " >" +
						"<td>" + nroitem + "</td>";
					gDetstr += "<td style='text-align: left;' >" + dbRow["codDEP"].ToString() + "</td>";
					//gDetstr += "<td style='text-align: left;' >" + dbRow["MEDCOD"].ToString() + "</td>";
					gDetstr += "<td style='text-align: left;' >" + dbRow["MEDCOD"].ToString() + " - " + dbRow["Medicamento"].ToString() + "</td>";
					gDetstr += "<td style='text-align: left;' >" + dbRow["Stock"].ToString() + "</td>";
					gDetstr += "<td style='text-align: left;' >" + dbRow["Fech"].ToString() + "</td>";
					gDetstr += "<td style='text-align: left;' >" + dbRow["Estado"].ToString() + "</td>";
					gDetstr += "</tr>";
					gDetstr += Environment.NewLine;
				}

				gDetstr += "</table></div></div><hr style='border-top: 1px solid blue'>";
			}
		}
		catch (Exception ex)
		{
			gDetstr += "-" + "-" + ex.Message.ToString();
		}
		return gDetstr;
	}

	//public void CargaMedicam(string codES)
	protected void CargaMedicam(object sender, EventArgs e)
	{

		string gDetAtenciones = "123<table>123</table><hr style='border-top: 1px solid blue'>";
		LitTABL1.Text = gDetAtenciones;
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