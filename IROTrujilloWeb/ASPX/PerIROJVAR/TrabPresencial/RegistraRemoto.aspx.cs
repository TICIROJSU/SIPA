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

public partial class ASPX_PerIROJVAR_TrabPresencial_RegistraRemoto : System.Web.UI.Page
{
	SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);

	protected void Page_Load(object sender, EventArgs e)
	{
		Server.ScriptTimeout = 3600;
		if (!Page.IsPostBack)
		{
			int vmonth = DateTime.Now.Month;
			DDLMes.SelectedIndex = vmonth -1;
		}

		int vd, vm, vy;
		vm = DateTime.Now.Month;
		vy = DateTime.Now.Year;

        ClassGlobal.varAnioActivo = 2021;
        ClassGlobal.varAnioActual = DateTime.Now.Year;

		CargaCalendario(vy, vm, DDLMes.SelectedItem.Text);

	}

	[WebMethod]
	public static string GetTipoTrabDescrip(string vIdPer, string vDNIPer, string vFechaTrab)
	{
        if (ClassGlobal.varAnioActual > ClassGlobal.varAnioActivo)
        {
            //return "";
        }

        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
		string vTipTrab = "";
		try
		{
			string qSql = "select * from NUEVA.dbo.JVARTipoTrabDescrip " +
                "where DNIPER = @Dni and FechaTrab = @Fec order by idTrabajoDescripcion desc;";
			SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
			cmd2.CommandType = CommandType.Text;
			cmd2.Parameters.AddWithValue("@Dni", vDNIPer);
			cmd2.Parameters.AddWithValue("@Fec", vFechaTrab);

			SqlDataAdapter adapter2 = new SqlDataAdapter(); adapter2.SelectCommand = cmd2;

			conSAP00i.Open();
			DataSet objdataset = new DataSet();
			adapter2.Fill(objdataset);
			conSAP00i.Close();

			DataTable dtDato = objdataset.Tables[0];

			if (dtDato.Rows.Count > 0)
			{
				vTipTrab += dtDato.Rows[0]["Actividad"].ToString();
                vTipTrab += "||sep||";
                vTipTrab += dtDato.Rows[0]["Ruta"].ToString();
                vTipTrab += "||sep||";
                vTipTrab += Path.GetFileName(@dtDato.Rows[0]["Ruta"].ToString());
            }
        }
		catch (Exception ex)
		{
			vTipTrab += ex.Message.ToString();
		}

		return vTipTrab;
	}

	[WebMethod]
	public static string SetTipoTrabajoInsert(string vIdPer, string vDNIPer, string vFechaTrab, string vTipoTrab)
	{
        //if (ClassGlobal.varAnioActual > ClassGlobal.varAnioActivo){return "";}

        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
		string gDetHtml = "";
		string qSql = "";
		try
		{
			qSql = "INSERT INTO NUEVA.dbo.JVARTipoTrab (IDPER, DNIPER, FechaTrab, TipoTrab, Observacion) VALUES (@IDPER, @DNIPER, @FechaTrab, @TipoTrab, @Observacion) SELECT @@Identity;";

			SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
			cmd.CommandType = CommandType.Text;
			SqlDataAdapter adapter = new SqlDataAdapter();

			cmd.Parameters.AddWithValue("@IDPER", vIdPer);
			cmd.Parameters.AddWithValue("@DNIPER", vDNIPer);
			cmd.Parameters.AddWithValue("@FechaTrab", vFechaTrab);
			cmd.Parameters.AddWithValue("@TipoTrab", vTipoTrab);
			cmd.Parameters.AddWithValue("@Observacion", "");

			adapter.SelectCommand = cmd;

			conSAP00i.Open();
			int idReg = Convert.ToInt32(cmd.ExecuteScalar());
			conSAP00i.Close();

			gDetHtml += idReg.ToString();
		}
		catch (Exception ex)
		{
			gDetHtml += "ERROR Registro: " + ex.Message.ToString() + Environment.NewLine + ex.ToString();
		}
		return gDetHtml;
	}

	[WebMethod]
	public static string SetTipoTrabajoEdit(string vIdPer, string vDNIPer, string vFechaTrab, string vTipoTrab)
	{
        //if (ClassGlobal.varAnioActual > ClassGlobal.varAnioActivo) { return ""; }

        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
		string gDetHtml = "";
		try
		{
			string qSql = "UPDATE NUEVA.dbo.JVARTipoTrab SET TipoTrab = @TipoTrab WHERE DNIPER = @DNIPER AND FechaTrab = @FechaTrab;";

			SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
			cmd.CommandType = CommandType.Text;
			SqlDataAdapter adapter = new SqlDataAdapter();

			cmd.Parameters.AddWithValue("@DNIPER", vDNIPer);
			cmd.Parameters.AddWithValue("@FechaTrab", vFechaTrab);
			cmd.Parameters.AddWithValue("@TipoTrab", vTipoTrab);
			adapter.SelectCommand = cmd;

			conSAP00i.Open(); cmd.ExecuteScalar(); conSAP00i.Close();

			gDetHtml += "Edicion Correcta";
		}
		catch (Exception ex)
		{
			gDetHtml += "ERROR: " + ex.Message.ToString() + Environment.NewLine + ex.ToString();
		}
		return gDetHtml;
	}


	[WebMethod]
	public static string SetTipoTrabajoDescripInsert(string vIdPer, string vDNIPer, string vFechaTrab, string vActividad)
	{
        //if (ClassGlobal.varAnioActual > ClassGlobal.varAnioActivo) { return ""; }

        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
		string gDetHtml = "";
		string qSql = "";

        qSql = "select * from (select cast(GETDATE() as date) fecha) F where fecha = '" + vFechaTrab + "'";
        SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
        cmd2.CommandType = CommandType.Text;
        SqlDataAdapter adapter2 = new SqlDataAdapter(); adapter2.SelectCommand = cmd2;

        conSAP00i.Open();
        DataSet objdataset = new DataSet();
        adapter2.Fill(objdataset);
        conSAP00i.Close();

        if (objdataset.Tables[0].Rows.Count >= 0)
        {
            try
		    {
			    qSql = "INSERT INTO NUEVA.dbo.JVARTipoTrabDescrip (idTipoTrabajo, IDPER, DNIPER, FechaTrab, Hora, Actividad, Ruta, Comentarios) VALUES (@idTipoTrabajo, @IDPER, @DNIPER, @FechaTrab, @Hora, @Actividad, @Ruta, @Comentarios) SELECT @@Identity;";

			    SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
			    cmd.CommandType = CommandType.Text;
			    SqlDataAdapter adapter = new SqlDataAdapter();

			    cmd.Parameters.AddWithValue("@idTipoTrabajo", "0");
			    cmd.Parameters.AddWithValue("@IDPER", vIdPer);
			    cmd.Parameters.AddWithValue("@DNIPER", vDNIPer);
			    cmd.Parameters.AddWithValue("@FechaTrab", vFechaTrab);
			    cmd.Parameters.AddWithValue("@Hora", "");
			    cmd.Parameters.AddWithValue("@Actividad", vActividad);
			    cmd.Parameters.AddWithValue("@Ruta", "");
			    cmd.Parameters.AddWithValue("@Comentarios", "");

			    adapter.SelectCommand = cmd;

			    conSAP00i.Open();
			    int idReg = Convert.ToInt32(cmd.ExecuteScalar());
			    conSAP00i.Close();

			    gDetHtml += idReg.ToString();
		    }
		    catch (Exception ex)
		    {
			    gDetHtml += "ERROR Registro: " + ex.Message.ToString() + Environment.NewLine + ex.ToString();
		    }
        }
        else
        {
            gDetHtml = "FechaIncorrecta";
        }

		return gDetHtml;
	}

	[WebMethod]
	public static string SetTipoTrabajoDescripEdit(string vIdPer, string vDNIPer, string vFechaTrab, string vActividad)
	{
        //if (ClassGlobal.varAnioActual > ClassGlobal.varAnioActivo) { return ""; }

        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
		string gDetHtml = "";
        string qSql = "";

        qSql = "select * from (select cast(GETDATE() as date) fecha) F where fecha = '" + vFechaTrab + "'";
        SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
        cmd2.CommandType = CommandType.Text;
        SqlDataAdapter adapter2 = new SqlDataAdapter(); adapter2.SelectCommand = cmd2;

        conSAP00i.Open();
        DataSet objdataset = new DataSet();
        adapter2.Fill(objdataset);
        conSAP00i.Close();

        if (objdataset.Tables[0].Rows.Count >= 0)
        {
		    try
		    {
			    qSql = "UPDATE NUEVA.dbo.JVARTipoTrabDescrip SET Actividad = @Actividad WHERE DNIPER = @DNIPER and FechaTrab = @FechaTrab;";

			    SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
			    cmd.CommandType = CommandType.Text;
			    SqlDataAdapter adapter = new SqlDataAdapter();

			    cmd.Parameters.AddWithValue("@DNIPER", vDNIPer);
			    cmd.Parameters.AddWithValue("@FechaTrab", vFechaTrab);
			    cmd.Parameters.AddWithValue("@Actividad", vActividad);
			    adapter.SelectCommand = cmd;

			    conSAP00i.Open(); cmd.ExecuteScalar(); conSAP00i.Close();

			    gDetHtml += "Edicion Correcta";
		    }
		    catch (Exception ex)
		    {
			    gDetHtml += "ERROR: " + ex.Message.ToString() + Environment.NewLine + ex.ToString();
		    }
        }
        else
        {
            gDetHtml = "FechaIncorrecta";
        }
        return gDetHtml;
	}


	public void CargaCalendario(int vAnio, int vMes, string vMesNom)
	{
		string lhtml = "";
		//vMes = 6;
		int Days = DateTime.DaysInMonth(vAnio, vMes);
		txtCantDias.Text = Days.ToString();
		txtIdPer.Text = Session["PerID"].ToString();
		txtDNI.Text = Session["PerDNI"].ToString();
		txtDatosPer.Text = Session["PerDatos"].ToString();

		try
		{

			lhtml += "<table> ";
			lhtml += "	<thead class='fc-head'> ";
			lhtml += "		<tr> ";
			lhtml += "			<th>Dom</th> ";
			lhtml += "			<th>Lun</th><th>Mar</th><th>Mie</th> ";
			lhtml += "			<th>Jue</th><th>Vie</th><th>Sab</th> ";
			lhtml += "		</tr> ";
			lhtml += "	</thead> ";
			lhtml += "	<tbody class='fc-body'> ";

			DateTime vFechaTrab = Convert.ToDateTime("01-" + vMes + "-" + vAnio);
			DateTime vFechaFor = vFechaTrab;

			for (int i = 1; i <= Days; i++)
			{
				lhtml += "<tr height='70'> ";
				for (int j = 0; j <= 6; j++)
				{
					string vDia = "", vDiachk = "", vDiamsg = "", vColorCelda = "", vLinkCelda = "";
					int diaSemNro = (int)vFechaFor.DayOfWeek;
					if (diaSemNro == j)
					{
						vDia = i.ToString();
						vDiachk = "<span><input type='checkbox' id='chkDia" + vDia + "' /></span>" +
							"<input type='hidden' id='hFecDia" + vDia + "' value='" + vFechaFor.ToShortDateString() + "' /> ";
						vDiamsg = CargaTipoTrabajo(Session["PerDNI"].ToString(), vFechaFor);
						if (vDiamsg=="PRESENCIAL")
						{ vColorCelda = "style='background-color:#81C784'"; }
						if (vDiamsg == "REMOTO")
						{ vColorCelda = "style='background-color:#F9A825'"; }

                        //if (vColorCelda != "")
                        if (vDiamsg == "REMOTO")
                        { vLinkCelda = "data-toggle='modal' data-target='#modalTrabDescrip' onclick=\"fTrabDescr('" + vDia + "', '" + vFechaFor.ToShortDateString() + "')\""; }

						vFechaFor = vFechaTrab.AddDays(i);
						i++;
					}
					if (i > Days + 1)
					{
						vDia = ""; vDiachk = ""; vDiamsg = ""; vColorCelda = ""; vLinkCelda = "";
					}
					lhtml += "	<td " + vColorCelda + " " + vLinkCelda + "> ";
					lhtml += "		<h3><span id='' class='pull-right'>" + vDiachk + vDia + "</span></h3> ";
					//lhtml += "  " + vDiachk;
					lhtml += "		<span id='spDiaMsg" + vDia + "'>" + vDiamsg + "</span> ";
					lhtml += "	</td> ";
				}
				lhtml += "</tr> ";
				i--;
			}

			lhtml += "	</tbody> ";
			lhtml += "</table> ";

			LitCalendar.Text = lhtml;
		}
		catch (Exception ex)
		{
			LitCalendar.Text += ex.Message.ToString();
		}

		LitPeriodo.Text = vMesNom + " de " + vAnio;

		//LitCalendar.Text += "<br />" + Days.ToString() + " - ";
		//LitCalendar.Text += dia1.ToString() + " - ";
		//LitCalendar.Text += DateTime.Now.ToString() + " - ";
		//LitCalendar.Text += " <br /> ";
		//LitCalendar.Text += ((byte)dia.DayOfWeek).ToString() + " - ";
		//LitCalendar.Text += dia.ToString() + " - ";

	}

	public string CargaTipoTrabajo(string vDNIPer, DateTime vFechaTrab)
	{
		string vTipTrab = "";
		try
		{
			string qSql = "select * from NUEVA.dbo.JVARTipoTrab where DNIPER = @Dni and FechaTrab = @Fec";
			SqlCommand cmd2 = new SqlCommand(qSql, conSAP00);
			cmd2.CommandType = CommandType.Text;
			cmd2.Parameters.AddWithValue("@Dni", vDNIPer);
			cmd2.Parameters.AddWithValue("@Fec", vFechaTrab);

			SqlDataAdapter adapter2 = new SqlDataAdapter(); adapter2.SelectCommand = cmd2;

			conSAP00.Open();
			DataSet objdataset = new DataSet();
			adapter2.Fill(objdataset);
			conSAP00.Close();

			DataTable dtDato = objdataset.Tables[0];

			if (dtDato.Rows.Count > 0)
			{
				vTipTrab = dtDato.Rows[0]["TipoTrab"].ToString();
			}
		}
		catch (Exception ex)
		{
			vTipTrab += ex.Message.ToString();
		}

		return vTipTrab; 
	}


	protected void bntBuscar_Click(object sender, EventArgs e)
	{
		int vAnio = Convert.ToInt32(DDLAnio.SelectedValue);
		int vMes = Convert.ToInt32(DDLMes.SelectedValue);
		CargaCalendario(vAnio, vMes, DDLMes.SelectedItem.Text);
	}

	protected void UploadButton_Click(object sender, EventArgs e)
	{
		String savePath = @"J:\IROTrujilloWeb\IROTrujilloWeb\ASPX\Carga\";
		String fileName = "";
		if (FileUpload1.HasFile)
		{
			fileName = FileUpload1.FileName;
			savePath += fileName;
			FileUpload1.SaveAs(savePath);
			SetGuardaFile(txtIdPer.Text, txtDNI.Text, txtDiaSel.Text, savePath);
			UploadStatusLabel.Text = "Tu Archivo fue guardado como " + fileName;
		}
		else
		{ UploadStatusLabel.Text = "No haz Cargado ningun Archivo."; }

		UploadStatusLabel.Text += " | Dia: " + txtDiaSel.Text;

	}

	public void SetGuardaFile(string vIdPer, string vDNIPer, string vFechaTrab, string vRuta)
	{
        ////if (ClassGlobal.varAnioActual > ClassGlobal.varAnioActivo) { return ""; }

        string gDetHtml = "";
		try
		{
			string qSql = "UPDATE NUEVA.dbo.JVARTipoTrabDescrip SET Ruta = @Ruta WHERE DNIPER = @DNIPER and FechaTrab = @FechaTrab;";

			SqlCommand cmd = new SqlCommand(qSql, conSAP00);
			cmd.CommandType = CommandType.Text;
			SqlDataAdapter adapter = new SqlDataAdapter();

			cmd.Parameters.AddWithValue("@DNIPER", vDNIPer);
			cmd.Parameters.AddWithValue("@FechaTrab", vFechaTrab);
			cmd.Parameters.AddWithValue("@Ruta", vRuta);
			adapter.SelectCommand = cmd;

			conSAP00.Open(); cmd.ExecuteScalar(); conSAP00.Close();
			gDetHtml += "Tu Archivo fue guardado";
		}
		catch (Exception ex)
		{
			gDetHtml += "No haz Cargado ningun Archivo o No haz guardado tu Actividad. ERROR: " + Environment.NewLine + ex.Message.ToString() + Environment.NewLine + ex.ToString();
		}

		UploadStatusLabel.Text = gDetHtml;
	}


	protected void btnDescarga_Click(object sender, EventArgs e)
	{
        //string vruta = @txtDiaSelRuta.Text;
        string vruta = @divDiaSelRuta.InnerText;
        string filename;
		filename = Path.GetFileName(vruta);

        //Response.ContentType = "application/" + Path.GetExtension(vruta).Substring(1);
        //Response.ContentType = "text/xml";
        Response.ContentType = "text/text";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        //Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
        Response.AppendHeader("Content-Disposition", "attachment; filename=ssedf.pdf");
        //Response.TransmitFile(Server.MapPath(@"~\ASPX\Carga\" + filename + ""));
        Response.TransmitFile(Server.MapPath(@"\Carga\123.pdf"));

    }
}
