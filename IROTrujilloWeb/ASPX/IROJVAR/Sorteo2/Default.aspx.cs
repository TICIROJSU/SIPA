using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.UI;

public partial class ASPX_IROJVAR_Sorteo2_Default : System.Web.UI.Page
{
	SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
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
	public static string GetSorteado(string IntentosCant)
	{
		SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
		string gHTML = "";
		string vIDPER = "";
		string vMensaje = "Ganador";
		if (Convert.ToInt32(IntentosCant) >= 1 && Convert.ToInt32(IntentosCant) <= 6)
		{
			vMensaje = "Paneton";
		}
		if (Convert.ToInt32(IntentosCant) >= 7 && Convert.ToInt32(IntentosCant) <= 8)
		{
			vMensaje = "Sorpresa";
		}
		if (Convert.ToInt32(IntentosCant) >= 9 && Convert.ToInt32(IntentosCant) <= 11)
		{
			vMensaje = "canasta";
		}
		if (Convert.ToInt32(IntentosCant) >= 12 && Convert.ToInt32(IntentosCant) <= 17)
		{
			vMensaje = "Canasta";
		}
		vMensaje = "Ganador";

		try
		{
			string qSql = "select top 1 *, DNI + ' ' + Apellidos + ' ' + Nombres Personal from Prueba.dbo.RegistroSorteo where Sorteo = '0' ORDER BY NEWID()";
			SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
			cmd2.CommandType = CommandType.Text;
			//cmd2.Parameters.AddWithValue("@txt", txtFiltro);

			SqlDataAdapter adapter2 = new SqlDataAdapter(); adapter2.SelectCommand = cmd2;

			conSAP00i.Open();
			DataSet objdataset = new DataSet();
			adapter2.Fill(objdataset);
			conSAP00i.Close();

			DataTable dt = objdataset.Tables[0];

			if (dt.Rows.Count > 0)
			{
				foreach (DataRow dbRow in dt.Rows)
				{
					gHTML += "<td>" + IntentosCant + "</td>" +
						"<td>" + dbRow["Personal"].ToString() + "</td>" +
						"<td>" + vMensaje + "</td>";
					gHTML += "||sep||" + dbRow["Personal"].ToString();
					vIDPER = dbRow["idPerSorteo"].ToString();
				}

			}
			else
			{
				gHTML += "<td>Error</td>";
			}
		}
		catch (Exception ex)
		{
			gHTML += "-" + "-" + ex.Message.ToString();
		}

		if (vMensaje == "Ganador")
		{
			try
			{
				//con.Open();
				string consql = "UPDATE Prueba.dbo.RegistroSorteo SET Sorteo = @Sorteo WHERE idPerSorteo = @idPerSorteo; ";
				SqlCommand cmd = new SqlCommand(consql, conSAP00i);
				cmd.CommandType = CommandType.Text;
				//asignamos el valor de los textbox a los parametros
				cmd.Parameters.AddWithValue("@Sorteo", vMensaje);
				cmd.Parameters.AddWithValue("@idPerSorteo", vIDPER);

				conSAP00i.Open();
				cmd.ExecuteScalar();
				conSAP00i.Close();

			}
			catch (Exception ex)
			{
				//Label1.Text = "Error";
				gHTML += "-" + "-" + ex.Message.ToString();
                conSAP00i.Close();
            }
		}

		return gHTML;
	}
	
	public void CargaInicial()
	{
		try
		{
			//con.Open();
			string qSql = "Select ROW_NUMBER() OVER(ORDER BY idPerSorteo) Nro, DNI + ' ' + Apellidos + ' ' + Nombres Personal, Sorteo Mensaje from Prueba.dbo.RegistroSorteo where Sorteo <> '0' order by 1 desc;";

			SqlCommand cmd = new SqlCommand(qSql, conSAP00); cmd.CommandType = CommandType.Text;
			SqlDataAdapter adapter = new SqlDataAdapter(); adapter.SelectCommand = cmd;

			conSAP00.Open();
			DataSet objdataset = new DataSet(); adapter.Fill(objdataset);
			conSAP00.Close();

			gvGanadores.DataSource = objdataset.Tables[0];
			//gvGanadores.Rows[0].Visible = false;
			gvGanadores.DataBind();

			lblCant.Text = objdataset.Tables[0].Rows.Count.ToString();
		}
		catch (Exception ex)
		{
			LitError.Text = ex.Message.ToString() + Environment.NewLine + ex.ToString();
		}
	}





}