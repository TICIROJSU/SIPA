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

public partial class IROTCovid19_IROJVAR_RegCovid_ImprimirReg : System.Web.UI.Page
{
	SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
	protected void Page_Load(object sender, EventArgs e)
	{
		string idPruebaCovid = Request.QueryString["idPruebaCovid"];
		CargaInicial(idPruebaCovid);
	}

	public void CargaInicial(string idcov)
	{
		try
		{
			//con.Open();
			string qSql = "select f100.*, IR1.*, IR2.Nombres PerNom, IR2.ApellidoPaterno PerApeP, IR2.ApellidoMaterno PerApeM from Prueba.dbo.covidF100 f100 inner join Prueba.dbo.InfoRhus IR1 on f100.pacNroDoc = IR1.NumDocumento inner join Prueba.dbo.InfoRhus IR2 on f100.perDNI = IR2.NumDocumento where id='" + idcov + "'";

			SqlCommand cmd = new SqlCommand(qSql, conSAP00); cmd.CommandType = CommandType.Text;
			SqlDataAdapter adapter = new SqlDataAdapter(); adapter.SelectCommand = cmd;

			conSAP00.Open();
			DataSet objdataset = new DataSet(); adapter.Fill(objdataset);
			conSAP00.Close();

			DataTable dtDato = objdataset.Tables[0];
			DataRow dbRow = dtDato.Rows[0];

			dPerDNI.InnerHtml = "<b>" + dbRow["perDNI"].ToString() + "&nbsp;</b>";
			dPerApeP.InnerHtml = "<b>" + dbRow["PerApeP"].ToString() + "&nbsp;</b>";
			dPerApeM.InnerHtml = "<b>" + dbRow["PerApeM"].ToString() + "&nbsp;</b>";
			dPerNomb.InnerHtml = "<b>" + dbRow["PerNom"].ToString() + "&nbsp;</b>";
			dPacTipDoc.InnerHtml = "<b>" + dbRow["pacTipDoc"].ToString() + "&nbsp;</b>";
			dPacDNI.InnerHtml = "<b>" + dbRow["PacNroDoc"].ToString() + "&nbsp;</b>";
			dPacApeP.InnerHtml = "<b>" + dbRow["ApellidoPaterno"].ToString() + "&nbsp;</b>";
			dPacApeM.InnerHtml = "<b>" + dbRow["ApellidoMaterno"].ToString() + "&nbsp;</b>";
			dPacNombres.InnerHtml = "<b>" + dbRow["Nombres"].ToString() + "&nbsp;</b>";

			DateTime birthday = Convert.ToDateTime(dbRow["FechaNacimiento"]);
			DateTime now = DateTime.Today;
			int age = now.Year - birthday.Year;
			if (now < birthday.AddYears(age)) age--;
			dPacEdad.InnerHtml = "<b>" + age + "&nbsp;</b>";

			dPacSexo.InnerHtml = "<b>" + dbRow["Sexo"].ToString() + "&nbsp;</b>";
			dPacCel.InnerHtml = "<b>" + dbRow["Telefonos"].ToString() + "&nbsp;</b>";
			dPacOtroT.InnerHtml = "<b>" + "&nbsp;" + "&nbsp;</b>";
			dPacDomicA.InnerHtml = "<b>" + "X" + "&nbsp;</b>";
			dPacDomicB.InnerHtml = "<b>" + "&nbsp;" + "&nbsp;</b>";
			dPacDireccion.InnerHtml = "<b>" + dbRow["Direcciones"].ToString() + "&nbsp;</b>";
			dPacProv.InnerHtml = "<b>" + "&nbsp;" + "&nbsp;</b>";
			dPacGeoLoc.InnerHtml = "<b>" + "&nbsp;" + "&nbsp;</b>";
			if (dbRow["PerSalud"].ToString() == "SI") { dPacPerSaludA.InnerHtml = "<b>X</b>"; dPacPerSaludB.InnerHtml = "&nbsp;"; }
			else { dPacPerSaludB.InnerHtml = "<b>X</b>"; dPacPerSaludA.InnerHtml = "&nbsp;"; }
			switch (dbRow["ProfesionCOVID"].ToString())
			{
				case "A. Medico":
					dPacProfA.InnerHtml = "<b>X&nbsp;</b>";
					break;
				case "B. Enfermero(a)":
					dPacProfB.InnerHtml = "<b>X&nbsp;</b>";
					break;
				case "C. Obstetra":
					dPacProfC.InnerHtml = "<b>X&nbsp;</b>";
					break;
				case "D. Biologo":
					dPacProfD.InnerHtml = "<b>X&nbsp;</b>";
					break;
				case "E. Tecnologo Medico":
					dPacProfE.InnerHtml = "<b>X&nbsp;</b>";
					break;
				case "F. Tecnico de Enfermeria":
					dPacProfF.InnerHtml = "<b>X&nbsp;</b>";
					break;
				case "G. Otros":
					dPacProfG.InnerHtml = "<b>X&nbsp;</b>";
					break;
				default:
					break;
			}

			if (dbRow["pacSintomas"].ToString() == "SI") { dPacSintA.InnerHtml = "<b>X</b>"; dPacSintB.InnerHtml = "&nbsp;"; }
			else { dPacSintB.InnerHtml = "<b>X</b>"; dPacSintA.InnerHtml = "&nbsp;"; }

			if (dbRow["pacFecIniSintomas"] != DBNull.Value)
			{
				dPacSintFecha.InnerHtml = "<b>" + dbRow["pacFecIniSintomas"].ToString().Substring(0, 10) + "&nbsp;</b>";
			}

			if (dbRow["pacATos"].ToString() == "1") { dPacMarSintA.InnerHtml = "<b>X</b>"; }
			if (dbRow["pacBGarganta"].ToString() == "1") { dPacMarSintB.InnerHtml = "<b>X</b>"; }
			if (dbRow["pacCCongNas"].ToString() == "1") { dPacMarSintC.InnerHtml = "<b>X</b>"; }
			if (dbRow["pacDDifResp"].ToString() == "1") { dPacMarSintD.InnerHtml = "<b>X</b>"; }
			if (dbRow["pacEFiebre"].ToString() == "1") { dPacMarSintE.InnerHtml = "<b>X</b>"; }
			if (dbRow["pacFMalestar"].ToString() == "1") { dPacMarSintF.InnerHtml = "<b>X</b>"; }
			if (dbRow["pacGDiarrea"].ToString() == "1") { dPacMarSintG.InnerHtml = "<b>X</b>"; }
			if (dbRow["pacHNauseas"].ToString() == "1") { dPacMarSintH.InnerHtml = "<b>X</b>"; }
			if (dbRow["pacICefalea"].ToString() == "1") { dPacMarSintI.InnerHtml = "<b>X</b>"; }
			if (dbRow["pacJIrritab"].ToString() == "1") { dPacMarSintJ.InnerHtml = "<b>X</b>"; }
			if (dbRow["pacKDolor"].ToString() == "1") { dPacMarSintK.InnerHtml = "<b>X</b>"; }
			if (dbRow["pacLOtros"].ToString() == "1") { dPacMarSintL.InnerHtml = "<b>X</b>"; }

			if (dbRow["pacAMuscular"].ToString() == "1") { dPacDolorA.InnerHtml = "<b>X</b>"; }
			if (dbRow["pacBAbdominal"].ToString() == "1") { dPacDolorB.InnerHtml = "<b>X</b>"; }
			if (dbRow["pacCPecho"].ToString() == "1") { dPacDolorC.InnerHtml = "<b>X</b>"; }
			if (dbRow["pacDArticulaciones"].ToString() == "1") { dPacDolorD.InnerHtml = "<b>X</b>"; }

			dPacOtrSint.InnerHtml = "<b>" + dbRow["pacOtrosSintomas"].ToString() + "&nbsp;</b>";
			dPRFecha.InnerHtml = "<b>" + dbRow["pacPRFecha"].ToString().Substring(0, 10) + 
				"&nbsp;" + dbRow["pacPRFechaH"].ToString() + "</b>";

			switch (dbRow["pacPRProcSolA"].ToString())
			{
				case "Llamada al 113":
					dPRProcedA.InnerHtml = "<b>X&nbsp;</b>";
					break;
				case "Prueba de EESS":
					dPRProcedB.InnerHtml = "<b>X&nbsp;</b>";
					break;
				case "Personal de salud":
					dPRProcedC.InnerHtml = "<b>X&nbsp;</b>";
					break;
				case "Contacto con caso confirmado":
					dPRProcedD.InnerHtml = "<b>X&nbsp;</b>";
					break;
				case "Contacto con caso sospechoso":
					dPRProcedE.InnerHtml = "<b>X&nbsp;</b>";
					break;
				case "Persona proveniente del extranjero (migraciones)":
					dPRProcedF.InnerHtml = "<b>X&nbsp;</b>";
					break;
				case "Otro priorizado":
					dPRProcedG.InnerHtml = "<b>X&nbsp;</b>";
					break;
				default:
					break;
			}

			switch (dbRow["pacPRResultado"].ToString())
			{
				case "No Reactivo":
					dPRResultA.InnerHtml = "<b>X&nbsp;</b>";
					break;
				case "Indeterminado":
					dPRResultB.InnerHtml = "<b>X&nbsp;</b>";
					break;
				case "IgM Reactivo":
					dPRResultC.InnerHtml = "<b>X&nbsp;</b>";
					break;
				case "IgG Reactivo":
					dPRResultD.InnerHtml = "<b>X&nbsp;</b>";
					break;
				case "IgM e IgG Reactivo":
					dPRResultE.InnerHtml = "<b>X&nbsp;</b>";
					break;
				default:
					break;
			}

		}
		catch (Exception ex)
		{
			LitError.Text = ex.Message.ToString();
		}
	}

}