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

public partial class ASPX_IROJVAR_Optica_MantProveedor : System.Web.UI.Page
{
	SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
	protected void Page_Load(object sender, EventArgs e)
	{
		Server.ScriptTimeout = 3600;
		if (!Page.IsPostBack)
		{
			int vmonth = DateTime.Now.Month;
		}

	}

	[WebMethod]
	public static string SetBtnGuardar(string vNDoc, string vRSoc, string vContac, string vCel, string vDir, string vMail, string vUser)
	{
		SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
		string gDetHtml = "";
		string qSql = "";
		try
		{
			qSql = "INSERT INTO IROBDOptica.dbo.Proveedor (CodigoMaestroTipoDocumento, CodigoMaestroTipoPersona, NumeroDocumento, RazonSocial, PersonaContacto, NumeroCelular, Direccion, Email, Estado, IsActivo, UsuarioRegistro, FechaRegistro) VALUES ('20', '41', @NumeroDocumento, @RazonSocial, @PersonaContacto, @NumeroCelular, @Direccion, @Email, '1', '1', @UsuarioRegistro, @FechaRegistro) SELECT @@Identity;";

			SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
			cmd.CommandType = CommandType.Text;
			SqlDataAdapter adapter = new SqlDataAdapter();

			cmd.Parameters.AddWithValue("@NumeroDocumento", vNDoc);
			cmd.Parameters.AddWithValue("@RazonSocial", vRSoc);
			cmd.Parameters.AddWithValue("@PersonaContacto", vContac);
			cmd.Parameters.AddWithValue("@NumeroCelular", vCel);
			cmd.Parameters.AddWithValue("@Direccion", vDir);
			cmd.Parameters.AddWithValue("@Email", vMail);
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
	public static string SetBtnModificar(string vCodigo, string vNDoc, string vRSoc, string vContac, string vCel, string vDir, string vMail, string vUser)
	{
		SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
		string gDetHtml = "";
		string qSql = "";
		try
		{
			qSql = "UPDATE IROBDOptica.dbo.Proveedor " +
				"SET [CodigoMaestroTipoDocumento] = '20', [CodigoMaestroTipoPersona] = '41', [NumeroDocumento] = @ndoc, [RazonSocial] = @rsoc, [PersonaContacto] = @contac, [NumeroCelular] = @cel, [Direccion] = @dir, " +
				"[Email] = @mail, [UsuarioModificacion] = @userm, [FechaModificacion] = @fmod " +
				"WHERE Codigo = @Codigo";

			SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
			cmd.CommandType = CommandType.Text;
			SqlDataAdapter adapter = new SqlDataAdapter();

			cmd.Parameters.AddWithValue("@Codigo", vCodigo);
			cmd.Parameters.AddWithValue("@ndoc", vNDoc);
			cmd.Parameters.AddWithValue("@rsoc", vRSoc);
			cmd.Parameters.AddWithValue("@contac", vContac);
			cmd.Parameters.AddWithValue("@cel", vCel);
			cmd.Parameters.AddWithValue("@dir", vDir);
			cmd.Parameters.AddWithValue("@mail", vMail);
			cmd.Parameters.AddWithValue("@fmod", DateTime.Now);
			cmd.Parameters.AddWithValue("@userm", vUser);

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
			qSql = "UPDATE IROBDOptica.dbo.Proveedor SET Estado = '0' WHERE Codigo = @Codigo";

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
			string qSql = "select * from IROBDOptica.dbo.Proveedor " +
				"where RazonSocial like '%' + @vN + '%' and Estado = '1';";
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
				gHTML += "<th class=''>RUC</th>";
				gHTML += "<th class=''>Razon Social</th>";
				gHTML += "<th class=''>Contacto</th>";
				gHTML += "<th class=''>Celular</th>";
				gHTML += "<th class=''>Direccion</th>";
				gHTML += "<th class=''>E-Mail</th>";
				gHTML += "<th class=''>Ver</th>";
				gHTML += "<th class=''>Borrar</th>";
				gHTML += "</tr>" + Environment.NewLine;
				int nroitem = 0;

				foreach (DataRow dbRow in dtDatoDetAt.Rows)
				{
					nroitem += 1;
					string vcod, vruc, vrsoc, vcont, vcel, vdir, vmail;
					vcod = dbRow["Codigo"].ToString();
					vruc = dbRow["NumeroDocumento"].ToString();
					vrsoc = dbRow["RazonSocial"].ToString();
					vcont = dbRow["PersonaContacto"].ToString();
					vcel = dbRow["NumeroCelular"].ToString();
					vdir = dbRow["Direccion"].ToString();
					vmail = dbRow["Email"].ToString();
					//gHTML += "<tr onclick=\"SelFilatbl('" + vcatcod + "', '" + vmoncod + "')\" >";
					gHTML += "<tr>";
					gHTML += "<td class='' >" + vcod + "</td>";
					gHTML += "<td style='text-align: left;' >" + vruc + "</td>";
					gHTML += "<td style='text-align: left;' >" + vrsoc + "</td>";
					gHTML += "<td style='text-align: left;' >" + vcont + "</td>";
					gHTML += "<td style='text-align: left;' >" + vcel + "</td>";
					gHTML += "<td style='text-align: left;' >" + vdir + "</td>";
					gHTML += "<td style='text-align: left;' >" + vmail + "</td>";
					gHTML += "<td style='text-align: left;' >" +
						"<div class='btn btn-info' onclick=\"SelFilatbl('" + vcod + "', '" + vruc + "', '" + vrsoc + "', '" + vcont + "', '" + vcel + "', '" + vdir + "', '" + vmail + "')\"> Ver</div>" +
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