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

public partial class IROTCovid19_IROJVAR_RegCovid_Default : System.Web.UI.Page
{
	SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
	protected void Page_Load(object sender, EventArgs e)
	{
		//txtPRFecha.Value = DateTime.Today.ToString();
		txtPRFecha.Value = DateTime.Today.ToString().Substring(0, 10);
		//litError.Text= DateTime.Today.ToString().Substring(0, 10) + "<BR/> " + DateTime.Today.ToString();
	}

	protected void btnBuscar_Click(object sender, EventArgs e)
	{
		try
		{
			string qSql = "select * from Prueba.dbo.InfoRhus where NumDocumento = '" + frmNroDoc.Value  + "'; ";
			SqlCommand cmd2 = new SqlCommand(qSql, conSAP00);
			cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
			adapter2.SelectCommand = cmd2;

			conSAP00.Open();
			DataSet objdataset = new DataSet();
			adapter2.Fill(objdataset);
			conSAP00.Close();

			DataTable dtDatoDetAt = objdataset.Tables[0];
			DataRow dbRow = objdataset.Tables[0].Rows[0];

			txtNombres.Value = dbRow["ApellidoPaterno"].ToString() + ", " + dbRow["ApellidoMaterno"].ToString() + ", " + dbRow["Nombres"].ToString();
			txtFecha.Value = dbRow["FechaNacimiento"].ToString().Substring(0, 10);

			DateTime birthday = Convert.ToDateTime(dbRow["FechaNacimiento"]);
			DateTime now = DateTime.Today;
			int age = now.Year - birthday.Year;
			if (now < birthday.AddYears(age)) age--;

			txtEdad.Value = age.ToString();
			txtSexo.Value = dbRow["Sexo"].ToString();
			txtCel.Value = dbRow["Telefonos"].ToString();
			txtDir.Value = dbRow["Direcciones"].ToString();
			ddlPerSalud.Text = dbRow["PerSalud"].ToString();
			ddlProf.SelectedValue = dbRow["ProfesionCOVID"].ToString();
			ddlCondRiesgo.Text = dbRow["CondRiesgo"].ToString();
			//Request.Form["txtNombres"] = dbRow["Nombres"].ToString();

		}
		catch (Exception ex)
		{

			litError.Text = "-" + "-" + ex.Message.ToString();
		}
	}

	protected void btnLimpiar_Click(object sender, EventArgs e)
	{
		Response.Redirect("../RegCovid/");

	}

	protected void btnActualizar_Click(object sender, EventArgs e)
	{
		try
		{
			string qSql = "update Prueba.dbo.InfoRhus set Telefonos=@Tel, Direcciones=@Dir, PerSalud=@Per, ProfesionCOVID=@Pro, CondRiesgo=@Cond where NumDocumento = @NumD";
			SqlCommand cmd2 = new SqlCommand(qSql, conSAP00);
			cmd2.CommandType = CommandType.Text;

			cmd2.Parameters.AddWithValue("@Tel", txtCel.Value);
			cmd2.Parameters.AddWithValue("@Dir", txtDir.Value);
			cmd2.Parameters.AddWithValue("@Per", ddlPerSalud.SelectedValue);
			cmd2.Parameters.AddWithValue("@Pro", ddlProf.Text);
			cmd2.Parameters.AddWithValue("@Cond", ddlCondRiesgo.Text);
			cmd2.Parameters.AddWithValue("@NumD", frmNroDoc.Value);

			conSAP00.Open();
			//int count = Convert.ToInt32(cmd.ExecuteScalar());
			cmd2.ExecuteScalar();
			conSAP00.Close();
			litError.Text = "Guardado Correcto";
		}
		catch (Exception ex)
		{

			litError.Text = "-" + "-" + ex.Message.ToString();
		}
	}

	protected void btnGuardar_Click(object sender, EventArgs e)
	{
		try
		{
			string qSql = "INSERT INTO Prueba.dbo.covidF100 " +
				"(perDNI, pacTipDoc, pacNroDoc, pacPerSalud, pacProfesion, pacSintomas, pacFecIniSintomas, pacATos, pacBGarganta, pacCCongNas, pacDDifResp, pacEFiebre, pacFMalestar, pacGDiarrea, pacHNauseas, pacICefalea, pacJIrritab, pacKDolor, pacLOtros, pacAMuscular, pacBAbdominal, pacCPecho, pacDArticulaciones, pacOtrosSintomas, pacPRFecha, pacPRFechaH, pacPRProcSolA, pacPRResultado, pacPRFotografia1, pacPRFotografia2, pacPRCondicion, pacAtePCR, pacAteProc, pacAteObs) " +
				"VALUES (@perDNI, @pacTipDoc, @pacNroDoc, @pacPerSalud, @pacProfesion, @pacSintomas, @pacFecIniSintomas, @pacATos, @pacBGarganta, @pacCCongNas, @pacDDifResp, @pacEFiebre, @pacFMalestar, @pacGDiarrea, @pacHNauseas, @pacICefalea, @pacJIrritab, @pacKDolor, @pacLOtros, @pacAMuscular, @pacBAbdominal, @pacCPecho, @pacDArticulaciones, @pacOtrosSintomas, @pacPRFecha, @pacPRFechaH, @pacPRProcSolA, @pacPRResultado, @pacPRFotografia1, @pacPRFotografia2, @pacPRCondicion, @pacAtePCR, @pacAteProc, @pacAteObs); SELECT @@IDENTITY; ";
			SqlCommand cmd2 = new SqlCommand(qSql, conSAP00);
			cmd2.CommandType = CommandType.Text;

			cmd2.Parameters.AddWithValue("@perDNI", txtpNroDocu.Value);
			cmd2.Parameters.AddWithValue("@pacTipDoc", selTipoDoc.Value);
			cmd2.Parameters.AddWithValue("@pacNroDoc", frmNroDoc.Value);
			cmd2.Parameters.AddWithValue("@pacPerSalud", ddlPerSalud.Text);
			cmd2.Parameters.AddWithValue("@pacProfesion", ddlProf.Text);
			cmd2.Parameters.AddWithValue("@pacSintomas", ddlSintomas.Text);
			if (txtFechaSint.Value.ToString().Length == 0)
			{
				cmd2.Parameters.AddWithValue("@pacFecIniSintomas", DBNull.Value);
			}
			else
			{
				cmd2.Parameters.AddWithValue("@pacFecIniSintomas", Convert.ToDateTime(txtFechaSint.Value));
			}
			
			cmd2.Parameters.AddWithValue("@pacATos", chksintA.Checked);
			cmd2.Parameters.AddWithValue("@pacBGarganta", chksintB.Checked);
			cmd2.Parameters.AddWithValue("@pacCCongNas", chksintC.Checked);
			cmd2.Parameters.AddWithValue("@pacDDifResp", chksintD.Checked);
			cmd2.Parameters.AddWithValue("@pacEFiebre", chksintE.Checked);
			cmd2.Parameters.AddWithValue("@pacFMalestar", chksintF.Checked);
			cmd2.Parameters.AddWithValue("@pacGDiarrea", chksintG.Checked);
			cmd2.Parameters.AddWithValue("@pacHNauseas", chksintH.Checked);
			cmd2.Parameters.AddWithValue("@pacICefalea", chksintI.Checked);
			cmd2.Parameters.AddWithValue("@pacJIrritab", chksintJ.Checked);
			cmd2.Parameters.AddWithValue("@pacKDolor", chksintK.Checked);
			cmd2.Parameters.AddWithValue("@pacLOtros", chksintL.Checked);
			cmd2.Parameters.AddWithValue("@pacAMuscular", chkdolA.Checked);
			cmd2.Parameters.AddWithValue("@pacBAbdominal", chkdolB.Checked);
			cmd2.Parameters.AddWithValue("@pacCPecho", chkdolC.Checked);
			cmd2.Parameters.AddWithValue("@pacDArticulaciones", chkdolD.Checked);
			cmd2.Parameters.AddWithValue("@pacOtrosSintomas", txtOtrosSint.Value);
			cmd2.Parameters.AddWithValue("@pacPRFecha", Convert.ToDateTime(txtPRFecha.Value));
			cmd2.Parameters.AddWithValue("@pacPRFechaH", Convert.ToDateTime(txtPRFHora.Value));
			cmd2.Parameters.AddWithValue("@pacPRProcSolA", ddlProcSolicitud.Text);
			cmd2.Parameters.AddWithValue("@pacPRResultado", ddlResultPR.Text);
			cmd2.Parameters.AddWithValue("@pacPRFotografia1", "");
			cmd2.Parameters.AddWithValue("@pacPRFotografia2", "");
			cmd2.Parameters.AddWithValue("@pacPRCondicion", ddlCondRiesgo.Text);
			cmd2.Parameters.AddWithValue("@pacAtePCR", ddlPCR.Text);
			cmd2.Parameters.AddWithValue("@pacAteProc", ddlProced.Text);
			cmd2.Parameters.AddWithValue("@pacAteObs", txtObserv.Value);

			conSAP00.Open();
			int count = Convert.ToInt32(cmd2.ExecuteScalar());
			//cmd2.ExecuteScalar();
			conSAP00.Close();
			litError.Text = "Guardado Correcto";
			Response.Write("<script>" +
				"window.open('ImprimirReg.aspx?idPruebaCovid=" + count + "' ,'_blank')" +
				"</script>");
		}
		catch (Exception ex)
		{
			litError.Text = "-" + "-" + ex.Message.ToString();
		}
	}

}