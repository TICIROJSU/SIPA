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

public partial class ASPX_IROJVAR_Optica_RptProductos : System.Web.UI.Page
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
			CargaTablaDTIni();
		}

		if (Page.IsPostBack)
		{
			GVtable.DataSource = null;
			GVtable.DataBind();
		}

	}

	[WebMethod]
	public static string GetStock(string vAlmacen, string vAlmacenNombre)
	{
		DateTimeFormatInfo formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;
		SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
		string gDetH = "";
		try
		{
			string qSql = "Select Alm.idalmacen, Alm.Nombre as Almacen, codigoprod, PO.Nombre as Producto, sum(Stock) as Stock from IROBDOptica.dbo.Almacen Alm left join IROBDOptica.dbo.stkProductoAlmacen StkAlm on Alm.idalmacen = StkAlm.idalmacen left join IROBDOptica.dbo.ProductosOptica PO on StkAlm.codigoprod = PO.Codigo where Alm.idalmacen='" + vAlmacen + "' group by Alm.idalmacen, Alm.Nombre, codigoprod, PO.Nombre ";

			SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
			cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
			adapter2.SelectCommand = cmd2;

			conSAP00i.Open();
			DataSet objdataset = new DataSet();
			adapter2.Fill(objdataset);
			conSAP00i.Close();

			DataTable dtDatoDetAt = objdataset.Tables[0];

			//gDetAtenciones = CargaTablaDetAtenciones(dtDatoDetAt, vanio, vmes, vdia, turno, vplaza);
			if (dtDatoDetAt.Rows.Count > 0)
			{
				gDetH += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
				gDetH += "<table id='tbldscrg' class='table table-hover' style='text-align: right; font-size: 14px; '>";
				gDetH += "<caption>Stock de Almacen " + vAlmacenNombre + "</caption>";
				gDetH += "<tr>";
				gDetH += "<th class=''>N° </th>";
				gDetH += "<th class=''>Producto</th>";
				gDetH += "<th class=''>Stock</th>";
				gDetH += "</tr>" + Environment.NewLine;
				int nroitem = 0;
				foreach (DataRow dbRow in dtDatoDetAt.Rows)
				{
					nroitem += 1;
					gDetH += "<tr data-toggle='modal' data-target='#modalstkprod' onclick=\"DetMontura('" + vAlmacen + "', '" + dbRow["codigoprod"].ToString() + "')\">";
					gDetH += "<td class='' style='text-align: left;'>" + nroitem + "</td>";
					gDetH += "<td class='' style='text-align: left;'>" + dbRow["Producto"].ToString() + "</td>";
					gDetH += "<td class='' style='text-align: left;'>" + dbRow["Stock"].ToString() + "</td>";

					gDetH += "</tr>" + Environment.NewLine;
				}

				gDetH += "</table></div></div><hr style='border-top: 1px solid blue'>";

			}
			//gDetAtenciones - FINAL
		}
		catch (Exception ex)
		{
			//LitErrores.Text += vplaza + "-" + "-" + ex.Message.ToString();
		}
		return gDetH;
	}

	[WebMethod]
	public static string GetDetMontura(string vAlmacen, string vProducto)
	{
		DateTimeFormatInfo formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;
		SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
		string gDetH = "";
		try
		{
			string qSql = "Select StkAlm.codigoprod, PO.Nombre as Producto, sPD.codmontura, sPD.Stock " +
				"from IROBDOptica.dbo.stkProductoAlmacen StkAlm " +
				"left join IROBDOptica.dbo.ProductosOptica PO on StkAlm.codigoprod = PO.Codigo " +
				"left join IROBDOptica.dbo.stkProductoDet sPD on StkAlm.idproddet = sPD.idproddet " +
				"where StkAlm.idalmacen='" + vAlmacen + "' and StkAlm.codigoprod = '" + vProducto + "'; ";

			SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
			cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
			adapter2.SelectCommand = cmd2;

			conSAP00i.Open();
			DataSet objdataset = new DataSet();
			adapter2.Fill(objdataset);
			conSAP00i.Close();

			DataTable dtDatoDetAt = objdataset.Tables[0];

			//gDetAtenciones = CargaTablaDetAtenciones(dtDatoDetAt, vanio, vmes, vdia, turno, vplaza);
			if (dtDatoDetAt.Rows.Count > 0)
			{
				gDetH += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
				gDetH += "<table id='tbldscrg' class='table table-hover' style='text-align: right; font-size: 14px; '>";
				gDetH += "<tr>";
				gDetH += "<th class=''>N° </th>";
				gDetH += "<th class=''>Producto</th>";
				gDetH += "<th class=''>Montura</th>";
				gDetH += "<th class=''>Stock</th>";
				gDetH += "</tr>" + Environment.NewLine;
				int nroitem = 0;
				foreach (DataRow dbRow in dtDatoDetAt.Rows)
				{
					nroitem += 1;
					gDetH += "<tr >";
					gDetH += "<td class='' style='text-align: left;'>" + nroitem + "</td>";
					gDetH += "<td style='text-align: left;'>" + dbRow["Producto"].ToString() + "</td>";
					gDetH += "<td style='text-align: left;'>" + dbRow["codmontura"].ToString() + "</td>";
					gDetH += "<td class='' style='text-align: left;'>" + dbRow["Stock"].ToString() + "</td>";

					gDetH += "</tr>" + Environment.NewLine;
				}

				gDetH += "</table></div></div><hr style='border-top: 1px solid blue'>";

			}
			//gDetAtenciones - FINAL
		}
		catch (Exception ex)
		{
			//LitErrores.Text += vplaza + "-" + "-" + ex.Message.ToString();
		}
		return gDetH;
	}



	public void CargaTablaDTIni()
	{
		try
		{
			//con.Open();
			string qSql = "Select Alm.idalmacen, Alm.Nombre, Count(StkAlm.idproddet) as Items " +
				"from IROBDOptica.dbo.Almacen Alm " +
				"left join IROBDOptica.dbo.stkProductoAlmacen StkAlm on Alm.idalmacen = StkAlm.idalmacen " +
				"Group By Alm.idalmacen, Alm.Nombre; ";

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
			//CargaTabla(dtDato);

			html = "";
			if (dtDato.Rows.Count > 0)
			{
				html += Environment.NewLine + "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
				html += "<caption style='text-align: center;'>Stock de Productos</caption>";
				html += "<tr>";
				html += "<th style='text-align: center;'>Id</th>";
				html += "<th style='text-align: center;'>Almacen</th>";
				html += "<th style='text-align: left;'>Items</th>";
				html += "</tr>" + Environment.NewLine;
				int nroitem = 0;
				foreach (DataRow dbRow in dtDato.Rows)
				{
					nroitem += 1;
					html += "<tr onclick=\"DetStock('" + dbRow["idalmacen"].ToString() + "', '" + dbRow["Nombre"].ToString() + "')\">";
					html += "<td class='' style='text-align: center;'>" + dbRow["idalmacen"].ToString() + "</td>";
					html += "<td class='' style='text-align: center;'>" + dbRow["Nombre"].ToString() + "</td>";
					html += "<td class='' style='text-align: left;'>" + dbRow["Items"].ToString() + "</td>";
					//html += "<td class='' style='text-align: left;'>" + "<button type='button' class='btn bg-navy' data-toggle='modal' data-target='#modalAtenciones'><i class='fa fa-fw fa-medkit'></i></button>" + "</td>";
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

}