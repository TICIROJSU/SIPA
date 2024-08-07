using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ASPX_IROJVAR_Optica_MantProductos : System.Web.UI.Page
{
	SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
	protected void Page_Load(object sender, EventArgs e)
	{
		Server.ScriptTimeout = 3600;
		if (!Page.IsPostBack)
		{
			int vmonth = DateTime.Now.Month;
			cargaCboMoneda();
			cargaCboCategoria();

		}

	}

	[WebMethod]
	public static string SetBtnGuardar(string vNombre, string vTMoneda, string vVTMoneda, string vPrecCompra, string cPrecVenta, string vCategoria, string vUser)
	{
		SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
		string gDetHtml = "";
		string qSql = "";
		try
		{
			qSql = "INSERT INTO IROBDOptica.dbo.ProductosOptica (Nombre, TipoMoneda, ValorTipoMoneda, PrecioCompra,PrecioVenta,StockActual,Estado,CodigoCategoria,FechaRegistro,UsuarioRegistro) " +
			"VALUES (@Nombre, @TipoMoneda, @ValorTipoMoneda, @PrecioCompra, @PrecioVenta, @StockActual, @Estado, @CodigoCategoria, @FechaRegistro, @UsuarioRegistro) SELECT @@Identity;";

			SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
			cmd.CommandType = CommandType.Text;
			SqlDataAdapter adapter = new SqlDataAdapter();

			cmd.Parameters.AddWithValue("@Nombre", vNombre);
			cmd.Parameters.AddWithValue("@TipoMoneda", vTMoneda);
			cmd.Parameters.AddWithValue("@ValorTipoMoneda", vVTMoneda);
			cmd.Parameters.AddWithValue("@PrecioCompra", vPrecCompra);
			cmd.Parameters.AddWithValue("@PrecioVenta", cPrecVenta);
			cmd.Parameters.AddWithValue("@CodigoCategoria", vCategoria);
			cmd.Parameters.AddWithValue("@StockActual", "0");
			cmd.Parameters.AddWithValue("@Estado", "1");
			cmd.Parameters.AddWithValue("@FechaRegistro", DateTime.Now);
			cmd.Parameters.AddWithValue("@UsuarioRegistro", vUser);

			adapter.SelectCommand = cmd;

			conSAP00i.Open();
			int count = Convert.ToInt32(cmd.ExecuteScalar());
			conSAP00i.Close();

			gDetHtml += count.ToString() + " - Registro Correcto";
		}
		catch (Exception ex)
		{
			gDetHtml += "ERROR: " + ex.Message.ToString() + Environment.NewLine + ex.ToString();
		}
		return gDetHtml;
	}

	[WebMethod]
	public static string SetBtnModificar(string vCodigo, string vNombre, string vTMoneda, string vVTMoneda, string vPrecCompra, string cPrecVenta, string vCategoria, string vUser)
	{
		SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
		string gDetHtml = "";
		string qSql = "";
		try
		{
			qSql = "UPDATE IROBDOptica.dbo.ProductosOptica " +
				"SET Nombre =@Nombre,TipoMoneda =@TipoMoneda, ValorTipoMoneda =@ValorTipoMoneda, PrecioCompra =@PrecioCompra, PrecioVenta =@PrecioVenta, CodigoCategoria = @CodigoCategoria, FechaModificacion = @FechaModificacion, UsuarioModificacion = @UsuarioModificacion " +
				"WHERE Codigo = @Codigo";

			SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
			cmd.CommandType = CommandType.Text;
			SqlDataAdapter adapter = new SqlDataAdapter();

			cmd.Parameters.AddWithValue("@Codigo", vCodigo);
			cmd.Parameters.AddWithValue("@Nombre", vNombre);
			cmd.Parameters.AddWithValue("@TipoMoneda", vTMoneda);
			cmd.Parameters.AddWithValue("@ValorTipoMoneda", vVTMoneda);
			cmd.Parameters.AddWithValue("@PrecioCompra", vPrecCompra);
			cmd.Parameters.AddWithValue("@PrecioVenta", cPrecVenta);
			cmd.Parameters.AddWithValue("@CodigoCategoria", vCategoria);
			cmd.Parameters.AddWithValue("@FechaModificacion", DateTime.Now);
			cmd.Parameters.AddWithValue("@UsuarioModificacion", vUser);

			adapter.SelectCommand = cmd;

			conSAP00i.Open();
			int count = Convert.ToInt32(cmd.ExecuteScalar());
			conSAP00i.Close();

			gDetHtml += count.ToString() + " - Modificacion Correcta";
		}
		catch (Exception ex)
		{
			gDetHtml += "ERROR: " + ex.Message.ToString() + Environment.NewLine + ex.ToString();
		}
		return gDetHtml;
	}

	[WebMethod]
	public static string SetBtnEliminar(string vCodigo)
	{
		SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
		string gDetHtml = "";
		string qSql = "";
		try
		{
			//qSql = "DELETE FROM IROBDOptica.dbo.ProductosOptica WHERE Codigo = @Codigo";
			qSql = "UPDATE IROBDOptica.dbo.ProductosOptica SET Estado = '0' WHERE Codigo = @Codigo";

			SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
			cmd.CommandType = CommandType.Text;
			SqlDataAdapter adapter = new SqlDataAdapter();

			cmd.Parameters.AddWithValue("@Codigo", vCodigo);

			adapter.SelectCommand = cmd;

			conSAP00i.Open();
			int count = Convert.ToInt32(cmd.ExecuteScalar());
			conSAP00i.Close();

			gDetHtml += count.ToString() + " - Eliminado Correctamente";
		}
		catch (Exception ex)
		{
			gDetHtml += "ERROR: " + ex.Message.ToString() + Environment.NewLine + ex.ToString();
		}
		return gDetHtml;
	}

	public void cargaCboMoneda()
	{
		try
		{
			//con.Open();
			string qSql = "select * from IROBDOptica.dbo.Maestro where CodigoSubMaestro = '4'";
			SqlCommand cmd = new SqlCommand(qSql, conSAP00);
			cmd.CommandType = CommandType.Text;
			SqlDataAdapter adapter = new SqlDataAdapter();

			adapter.SelectCommand = cmd;

			conSAP00.Open();
			DataSet objdataset = new DataSet();
			adapter.Fill(objdataset);
			conSAP00.Close();

			ListItem i;
			foreach (DataRow r in objdataset.Tables[0].Rows)
			{
				i = new ListItem(r["Valor"].ToString(), r["Codigo"].ToString());
				ddlMoneda.Items.Add(i);
			}
		}
		catch (Exception ex)
		{
			LitTABL1.Text = ex.Message.ToString();
		}
	}

	public void cargaCboCategoria()
	{
		try
		{
			//con.Open();
			string qSql = "select * from IROBDOptica.dbo.Categoria where IsActivo = '1';";
			SqlCommand cmd = new SqlCommand(qSql, conSAP00);
			cmd.CommandType = CommandType.Text;
			SqlDataAdapter adapter = new SqlDataAdapter();

			adapter.SelectCommand = cmd;

			conSAP00.Open();
			DataSet objdataset = new DataSet();
			adapter.Fill(objdataset);
			conSAP00.Close();

			ListItem i;
			foreach (DataRow r in objdataset.Tables[0].Rows)
			{
				i = new ListItem(r["Nombre"].ToString(), r["Codigo"].ToString());
				ddlCategoria.Items.Add(i);
			}
		}
		catch (Exception ex)
		{
			LitTABL1.Text = ex.Message.ToString();
		}
	}

	protected void bntBuscar_Click(object sender, EventArgs e)
	{
		CargaTablaDT();
	}

	public void CargaTablaDT()
	{
		string vNombre = txtProd.Text;
		string gHTML = "";
		try
		{
			//con.Open();
			string qSql = "Select PO.Codigo, PO.Nombre as Producto, PO.ValorTipoMoneda as Moneda, PO.PrecioCompra, PO.PrecioVenta, PO.StockActual, PO.Estado, Cat.Nombre as Categoria, Cat.Codigo CodCategoria, PO.TipoMoneda CodTMoneda " +
				"from IROBDOptica.dbo.ProductosOptica PO inner join IROBDOptica.dbo.Categoria Cat on PO.CodigoCategoria=Cat.Codigo where PO.Nombre like '%' + @vN + '%' and PO.Estado='1';";
			SqlCommand cmd = new SqlCommand(qSql, conSAP00);
			cmd.CommandType = CommandType.Text;
			SqlDataAdapter adapter = new SqlDataAdapter();

			cmd.Parameters.AddWithValue("@vN", vNombre);

			adapter.SelectCommand = cmd;

			conSAP00.Open();
			DataSet objdataset = new DataSet();
			adapter.Fill(objdataset);
			conSAP00.Close();

			DataTable dtDato = objdataset.Tables[0];

			//GVtable.DataSource = dtDato;
			//GVtable.DataBind();
			//CargaTabla(dtDato);
			DataTable dtDatoDetAt = objdataset.Tables[0];

			if (dtDatoDetAt.Rows.Count > 0)
			{
				gHTML += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
				gHTML += "<table class='table table-hover' id='tblbscrJS' style='text-align: right; font-size: 14px; '>";
				gHTML += "<th class=''>cod </th>";
				gHTML += "<th class=''>producto</th>";
				gHTML += "<th class=''>Mon</th>";
				gHTML += "<th class=''>P. Compra</th>";
				gHTML += "<th class=''>P. Venta</th>";
				gHTML += "<th class=''>Stock</th>";
				gHTML += "<th class=''>Estado</th>";
				gHTML += "<th class=''>Categoria</th>";
				gHTML += "<th class=''>Ver</th>";
				gHTML += "<th class=''>Borrar</th>";
				gHTML += "</tr>" + Environment.NewLine;
				int nroitem = 0;

				foreach (DataRow dbRow in dtDatoDetAt.Rows)
				{
					nroitem += 1;
					string vcod, vprod, vmon, vprecc, vprecv, vstk, vest, vcat, vcatcod, vmoncod;
					vcod = dbRow["Codigo"].ToString();
					vprod = dbRow["Producto"].ToString();
					vmon = dbRow["Moneda"].ToString();
					vprecc = dbRow["PrecioCompra"].ToString();
					vprecv = dbRow["PrecioVenta"].ToString();
					vstk = dbRow["StockActual"].ToString();
					vest = dbRow["Estado"].ToString();
					vcat = dbRow["Categoria"].ToString();
					vcatcod = dbRow["CodCategoria"].ToString();
					vmoncod = dbRow["CodTMoneda"].ToString();
					//gHTML += "<tr onclick=\"SelFilatbl('" + vcatcod + "', '" + vmoncod + "')\" >";
					gHTML += "<tr>";
					gHTML += "<td class='' >" + vcod + "</td>";
					gHTML += "<td style='text-align: left;' >" + vprod + "</td>";
					gHTML += "<td style='text-align: left;' >" + vmon + "</td>";
					gHTML += "<td style='text-align: left;' >" + vprecc + "</td>";
					gHTML += "<td style='text-align: left;' >" + vprecv + "</td>";
					gHTML += "<td style='text-align: left;' >" + vstk + "</td>";
					gHTML += "<td style='text-align: left;' >" + vest + "</td>";
					gHTML += "<td style='text-align: left;' >" + vcat + "</td>";
					gHTML += "<td style='text-align: left;' >" +
						"<div class='btn btn-info' onclick=\"SelFilatbl('" + vcod + "', '" + vprod + "', '" + vmon + "', '" + vprecc + "', '" + vprecv + "', '" + vstk + "', '" + vest + "', '" + vcat + "', '" + vcatcod + "', '" + vmoncod + "')\"> Ver</div>" +
						"</td>";
					gHTML += "<td style='text-align: left;' >" +
						"<div class='btn btn-info' onclick=\"BorraReg('" + vcod + "')\"> Quitar</div>" +
						"</td>";
					//gHTML += "<td style='text-align: left;' >" + vmoncod + "</td>";
					gHTML += "</tr>";
					gHTML += Environment.NewLine;
				}

				gHTML += "</table></div></div><hr style='border-top: 1px solid blue'>";
			}

		}
		catch (Exception ex)
		{
			//Label1.Text = "Error";
			gHTML += ex.Message.ToString();
			LitTABL1.Text = ex.Message.ToString();
		}

		LitTABL1.Text = gHTML;

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