using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public partial class IROTCovid19_IROJVAR_RepCovid_Default : System.Web.UI.Page
{
	SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
	protected void Page_Load(object sender, EventArgs e)
	{
		//tbf100.InnerHtml = "<tr class='odd'><td>11</td><td>2</td><td>3</td><td>4</td><td>5</td><td>6</td><td>7</td>  </tr>  <tr class='odd'><td>1</td><td>2</td><td>3</td><td>4</td><td>5</td><td>6</td><td>7</td>  </tr><tr class='odd'><td>1</td><td>2</td><td>3</td><td>4</td><td>5</td><td>6</td><td>7</td>  </tr>";
		Server.ScriptTimeout = 3600;
		if (!Page.IsPostBack)
		{
			idfecha1.Text = DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString() + "-" + DateTime.Today.Day.ToString();
			idfecha2.Text = DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString() + "-" + DateTime.Today.Day.ToString();
		}
		if (Page.IsPostBack)
		{

		}
	}

	protected void btnBuscar_Click(object sender, EventArgs e)
	{
		try
		{
			string qSql = "select f100.*, IR1.* from Prueba.dbo.covidF100 f100 inner join Prueba.dbo.InfoRhus IR1 on f100.pacNroDoc = IR1.NumDocumento where cast(FecReg as date) between @fecIni and @fecFin";
			SqlCommand cmd2 = new SqlCommand(qSql, conSAP00);
			cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();

			cmd2.Parameters.AddWithValue("@fecIni", Convert.ToDateTime(idfecha1.Text));
			cmd2.Parameters.AddWithValue("@fecFin", Convert.ToDateTime(idfecha2.Text));

			adapter2.SelectCommand = cmd2;
			
			conSAP00.Open();
			DataSet objdataset = new DataSet();
			adapter2.Fill(objdataset);
			conSAP00.Close();

			DataTable dtDatoDetAt = objdataset.Tables[0];
			string html = "";
			int nroitem = 0;
			if (dtDatoDetAt.Rows.Count > 0)
			{
				foreach (DataRow dbRow in dtDatoDetAt.Rows)
				{
					nroitem += 1;
					html += "<tr class='odd'>";
					html += "<td>" + nroitem + "</td>";
					html += "<td>" + dbRow["pacNroDoc"].ToString() + "</td>";
					html += "<td>" + dbRow["ApellidoPaterno"].ToString() + " " + dbRow["ApellidoMaterno"].ToString() + ", " + dbRow["Nombres"].ToString() + "</td>";
					html += "<td>" + dbRow["Sexo"].ToString() + "</td>";

					DateTime birthday = Convert.ToDateTime(dbRow["FechaNacimiento"]);
					DateTime now = DateTime.Today;
					int age = now.Year - birthday.Year;
					if (now < birthday.AddYears(age)) age--;
					html += "<td>" + age + "</td>";

					html += "<td>" + dbRow["Telefonos"].ToString() + "</td>";
					html += "<td><a target='_blank' href='../RegCovid/ImprimirReg.aspx?idPruebaCovid=" + dbRow["id"].ToString() + "' class='btn btn-info'>Imprimir</a></td>";
					html += "</tr>";
					html += Environment.NewLine;
				}
				//html += "<hr style='border-top: 1px solid blue'>";
			}

			tbf100.InnerHtml = html;
			dCantReg.InnerHtml = nroitem.ToString();
		}
		catch (Exception ex)
		{
			litError.Text = "-" + "-" + ex.Message.ToString();
		}
	}
}