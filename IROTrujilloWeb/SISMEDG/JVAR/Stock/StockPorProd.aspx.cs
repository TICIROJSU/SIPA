using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;

public partial class SISMEDG_JVAR_Stock_StockPorProd : System.Web.UI.Page
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
	public static string GetMedicamentos(string txtFiltro)
	{
		SqlConnection conSISMEDi = new SqlConnection(ClassGlobal.conexion_EXTERNO);
		string gDetAtenciones = "";
		try
		{
			string qSql = "select '(' + isnull(ESTADO, '') + ') ' + isnull(MEDICAMENT, '') + ' ' + isnull(PRESENTACI, '') + ' ' + isnull(CONCENTRAC, '') + '-' + isnull(FORMA_FARM, '') Producto, * from CATMEDICAM where CODIGO_MED + MEDICAMENT like '%' + @txt + '%'";
			SqlCommand cmd2 = new SqlCommand(qSql, conSISMEDi);
			cmd2.CommandType = CommandType.Text;
			cmd2.Parameters.AddWithValue("@txt", txtFiltro);

			SqlDataAdapter adapter2 = new SqlDataAdapter(); adapter2.SelectCommand = cmd2;

			conSISMEDi.Open();
			DataSet objdataset = new DataSet();
			adapter2.Fill(objdataset);
			conSISMEDi.Close();

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
					gDetAtenciones += "<tr onclick=\"txtMedicamCarga('" + dbRow["CODIGO_MED"].ToString() + "', '" + dbRow["CODIGO_MED"].ToString() + "-" + dbRow["Producto"].ToString() + "')\" data-dismiss='modal'>";
					gDetAtenciones += "<td>" + nroitem + "</td>";
					gDetAtenciones += "<td style='text-align: left;'>" + dbRow["CODIGO_MED"].ToString() + "</td>";
					gDetAtenciones += "<td style='text-align: left;'>" + dbRow["Producto"].ToString() + "</td>";
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

	}

	protected void bntBuscar_Click(object sender, EventArgs e)
	{
		string vcodmed = txtHideCodMed.Value;
		try
		{
			string qSql = "Select tm.ALMCODIDST, es.EESSS, tmd.MEDCOD, tm.MOVNUMEDCO, tm.MOVFECHREG, tm.MOVREFE " +
				"from tmovim tm inner join tmovimdet tmd on tm.CODDEP = tmd.CODDEP and tm.MOVCODITIP = tmd.MOVCODITIP and tm.MOVNUMERO = tmd.MOVNUMERO " +
				"left join CATEESS es on tm.ALMCODIDST = RIGHT(es.codEESS, 5) " +
				"left join CATMEDICAM med on tmd.MEDCOD = med.CODIGO_MED " +
				"where tm.MOVSITUA = '1' and tm.MOVCODITIP = 'S' and tm.CODDEP = '018A01' and MEDCOD = '" + vcodmed + "' " +
				"order by tm.MOVFECHREG ;";

			SqlCommand cmd = new SqlCommand(qSql, conSISMED); cmd.CommandType = CommandType.Text;
			SqlDataAdapter adapter = new SqlDataAdapter(); adapter.SelectCommand = cmd;

			conSISMED.Open();
			DataSet objdataset = new DataSet(); adapter.Fill(objdataset);
			conSISMED.Close();

			DataTable dtDato = objdataset.Tables[0];

			//GVtable.DataSource = dtDato;
			//GVtable.DataBind();
			//LitTABL1.Text = qSql;

			DataTable dt = objdataset.Tables[0];
			string getHTML = "";

			if (dt.Rows.Count > 0)
			{
				getHTML += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
				getHTML += "<table class='table table-hover' id='tblproductos' style='text-align: right; font-size: 14px; '>";
				getHTML += "<th class=''>Nro </th>";
				getHTML += "<th class=''>ESCod </th>";
				getHTML += "<th class=''>Destino </th>";
				getHTML += "<th class=''>MedCod </th>";
				getHTML += "<th class=''>Documento </th>";
				getHTML += "<th class=''>Fecha Reg </th>";
				getHTML += "<th class=''>Referencia </th>";

				int nroitem = 0;
				getHTML += Environment.NewLine;

				foreach (DataRow dbRow in dt.Rows)
				{
					nroitem += 1;
					getHTML += "<tr>";
					getHTML += "<td>" + nroitem + "</td>";
					getHTML += "<td style='text-align: left;'>" + dbRow["ALMCODIDST"].ToString() + "</td>";
					getHTML += "<td style='text-align: left;'>" + dbRow["EESSS"].ToString() + "</td>";
					getHTML += "<td style='text-align: left;'>" + dbRow["MEDCOD"].ToString() + "</td>";
					getHTML += "<td style='text-align: left;'>" + dbRow["MOVNUMEDCO"].ToString() + "</td>";
					getHTML += "<td style='text-align: left;'>" + dbRow["MOVFECHREG"].ToString().Substring(0, 10) + "</td>";
					getHTML += "<td style='text-align: left;'>" + dbRow["MOVREFE"].ToString() + "</td>";
					getHTML += "</tr>";
					getHTML += Environment.NewLine;
				}

				getHTML += "</table></div></div><hr style='border-top: 1px solid blue'>";
			}
			else
			{
				getHTML = "<table></table><hr style='border-top: 1px solid blue'>";
			}
			LitTABL1.Text = getHTML;

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
		response.Write(LitTABL1_1.InnerHtml);
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