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

public partial class ASPX_IROJVAR_Optica_MantPaciente : System.Web.UI.Page
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
	public static string SetBtnGuardar(string vAPP, string vCodigo, string vCelular, string vDireccion, string vUser, string vApellidos, string vNombres, string vDNI)
	{
		SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
		string gDetHtml = "";
		string qSql = "";
		try
		{
			qSql = "UPDATE IROBDOptica.dbo.Paciente " +
				"SET Apellidos = @Ape, Nombres = @Nom, " +
				"CodigoMaestroTipoDocumento = '17', CodigoMaestroTipoPersona = '40', " +
				"NumeroDocumento = @DNI, NumeroCelular = @Cel, Direccion = @Dir, " +
				"UsuarioModificacion = @uMod, FechaModificacion = @fMod WHERE Codigo = @Codigo;";
			SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
			cmd.CommandType = CommandType.Text;
			SqlDataAdapter adapter = new SqlDataAdapter();
			cmd.Parameters.AddWithValue("@Codigo", vCodigo);
			cmd.Parameters.AddWithValue("@Cel", vCelular);
			cmd.Parameters.AddWithValue("@Dir", vDireccion);
			cmd.Parameters.AddWithValue("@fMod", DateTime.Now);
			cmd.Parameters.AddWithValue("@uMod", vUser);
			cmd.Parameters.AddWithValue("@Ape", vApellidos);
			cmd.Parameters.AddWithValue("@Nom", vNombres);
			cmd.Parameters.AddWithValue("@DNI", vDNI);
			adapter.SelectCommand = cmd;

			conSAP00i.Open();
			int count = Convert.ToInt32(cmd.ExecuteScalar());
			conSAP00i.Close();

			gDetHtml += count.ToString() + " - Modificacion Optica Correcta";
		}
		catch (Exception ex)
		{
			gDetHtml += "ERROR: " + ex.Message.ToString() + Environment.NewLine + ex.ToString();
		}
		return gDetHtml;
	}

	[WebMethod]
	public static string SetBtnModificarSIPA(string vAPP, string vCodigo, string vCelular, string vDireccion, string vUser)
	{
		SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
		string gDetHtml = "";
		string qSql = "";
		try
		{
			qSql = "UPDATE NUEVA.dbo.HISTORIA SET IDI = @IDI, ITC = @ITC WHERE IHC = @IHC;";

			SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
			cmd.CommandType = CommandType.Text;
			SqlDataAdapter adapter = new SqlDataAdapter();

			cmd.Parameters.AddWithValue("@IHC", vCodigo);
			cmd.Parameters.AddWithValue("@IDI", vDireccion);
			cmd.Parameters.AddWithValue("@ITC", vCelular);
			adapter.SelectCommand = cmd;

			conSAP00i.Open();
			int count = Convert.ToInt32(cmd.ExecuteScalar());
			conSAP00i.Close();

			gDetHtml += count.ToString() + " - Modificacion SIPA Correcta";
		}
		catch (Exception ex)
		{
			gDetHtml += "ERROR: " + ex.Message.ToString() + Environment.NewLine + ex.ToString();
		}
		return gDetHtml;
	}
	[WebMethod]
	public static string SetBtnModificarOptica(string vAPP, string vCodigo, string vCelular, string vDireccion, string vUser, string vApellidos, string vNombres, string vDNI)
	{
		SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
		string gDetHtml = "";
		string qSql = "";
		try
		{
			qSql = "UPDATE IROBDOptica.dbo.Paciente " +
				"SET Apellidos = @Ape, Nombres = @Nom, " +
				"CodigoMaestroTipoDocumento = '17', CodigoMaestroTipoPersona = '40', " +
				"NumeroDocumento = @DNI, NumeroCelular = @Cel, Direccion = @Dir, " +
				"UsuarioModificacion = @uMod, FechaModificacion = @fMod WHERE Codigo = @Codigo;";
			SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
			cmd.CommandType = CommandType.Text;
			SqlDataAdapter adapter = new SqlDataAdapter();
			cmd.Parameters.AddWithValue("@Codigo", vCodigo);
			cmd.Parameters.AddWithValue("@Cel", vCelular);
			cmd.Parameters.AddWithValue("@Dir", vDireccion);
			cmd.Parameters.AddWithValue("@fMod", DateTime.Now);
			cmd.Parameters.AddWithValue("@uMod", vUser);
			cmd.Parameters.AddWithValue("@Ape", vApellidos);
			cmd.Parameters.AddWithValue("@Nom", vNombres);
			cmd.Parameters.AddWithValue("@DNI", vDNI);
			adapter.SelectCommand = cmd;

			conSAP00i.Open();
			int count = Convert.ToInt32(cmd.ExecuteScalar());
			conSAP00i.Close();

			gDetHtml += count.ToString() + " - Modificacion Optica Correcta";
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
			qSql = "UPDATE IROBDOptica.dbo.Paciente SET Estado = '0' WHERE Codigo = @Codigo";

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
		string vNombre = txtBuscar.Text;
		string gHTML = "";
		try
		{
			//con.Open();
			//string qSql = "Select * from (" +
			//"select 'SIPA' AS APP, cast(IHC as varchar(10)) as Codigo, IAP + ' ' + IAM as Apellidos, INO as Nombres, IDNI as DNI, IDI as Direccion, ITC as Celular, '1' as Estado from NUEVA.dbo.HISTORIA where cast(IHC as varchar(10)) + IAP + ' ' + IAM + INO + IDNI + ITC like '%' + @vN + '%' " +
			//" union " +
			//"select 'SOptica' AS APP, cast(Codigo as varchar(10)) as Codigo, Apellidos, Nombres, NumeroDocumento as DNI, Direccion, NumeroCelular as Celular, Estado from IROBDOptica.dbo.Paciente where cast(Codigo as varchar(10)) + Apellidos + Nombres + NumeroDocumento + NumeroCelular like '%' + @vN + '%' and Estado = '1'  " +
			//") Paciente " +
			//"where APP + Codigo + Apellidos + Nombres + DNI + Celular like '%' + @vN + '%' and Estado = '1' " +
			//"order by DNI ;";
			string qSql = "exec IROBDOptica.dbo.spListaPaciente @vN";
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

			//GVtable.DataSource = dtDato; //GVtable.DataBind(); //CargaTabla(dtDato);
			DataTable dtDatoDetAt = objdataset.Tables[0];

			if (dtDatoDetAt.Rows.Count > 0)
			{
				gHTML += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
				gHTML += "<table class='table table-hover' id='tblbscrJS' style='text-align: right; font-size: 14px; '>";
				gHTML += "<th class=''>APP </th>";
				gHTML += "<th class=''>Codigo </th>";
				gHTML += "<th class=''>Apellidos </th>";
				gHTML += "<th class=''>Nombres </th>";
				gHTML += "<th class=''>DNI </th>";
				gHTML += "<th class=''>Direccion </th>";
				gHTML += "<th class=''>Celular </th>";
				gHTML += "<th class=''>Ver</th>";
				gHTML += "<th class=''>Borrar</th>";
				gHTML += "</tr>" + Environment.NewLine;
				int nroitem = 0;
				foreach (DataRow dbRow in dtDatoDetAt.Rows)
				{
					nroitem += 1;
					string vapp, vcod, vape, vnom, vdni, vdir, vcel;
					vapp = dbRow["APP"].ToString();
					vcod = dbRow["Codigo"].ToString();
					vape = dbRow["Apellidos"].ToString();
					vnom = dbRow["Nombres"].ToString();
					vdni = dbRow["DNI"].ToString();
					vdir = dbRow["Direccion"].ToString();
					vcel = dbRow["Celular"].ToString();
					//gHTML += "<tr onclick=\"SelFilatbl('" + vcatcod + "', '" + vmoncod + "')\" >";
					gHTML += "<tr>";
					gHTML += "<td class='' >" + vapp + "</td>";
					gHTML += "<td style='text-align: left;' >" + vcod +"</td>";
					gHTML += "<td style='text-align: left;' >" + vape +"</td>";
					gHTML += "<td style='text-align: left;' >" + vnom +"</td>";
					gHTML += "<td style='text-align: left;' >" + vdni +"</td>";
					gHTML += "<td style='text-align: left;' >" + vdir +"</td>";
					gHTML += "<td style='text-align: left;' >" + vcel +"</td>";
					gHTML += "<td style='text-align: left;' >" +
						"<div class='btn btn-info' onclick=\"SelFilatbl('" + vapp + "', '" + vcod + "', '" + vape + "', '" + vnom + "', '" + vdni + "', '" + vdir + "', '" + vcel + "')\"> Ver</div>" +
						"</td>";
					gHTML += "<td style='text-align: left;' >" +
						"<div class='btn btn-info' onclick=\"BorraReg('" + vapp + "', '" + vcod + "', )\"> Quitar</div>" +
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