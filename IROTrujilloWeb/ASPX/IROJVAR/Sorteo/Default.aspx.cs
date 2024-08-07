using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.UI;

public partial class ASPX_IROJVAR_Sorteo_Default : System.Web.UI.Page
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
	public static string GetSorteado(string IntentosCant, string IntentoNro)
	{
		SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
		string gHTML = "";
		string vIDPER = "";
		string vMensaje = "Siga Intentando";
		try
		{
			string qSql = "select top 1 * from Prueba.dbo.PersonalIRO where Sorteo = '1' ORDER BY NEWID()";
			SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
			cmd2.CommandType = CommandType.Text;
			//cmd2.Parameters.AddWithValue("@txt", txtFiltro);

			SqlDataAdapter adapter2 = new SqlDataAdapter(); adapter2.SelectCommand = cmd2;

			conSAP00i.Open();
			DataSet objdataset = new DataSet();
			adapter2.Fill(objdataset);
			conSAP00i.Close();

			DataTable dt = objdataset.Tables[0];

			if (IntentosCant == IntentoNro)
			{
				vMensaje = "Ganador";
			}

			if (dt.Rows.Count > 0)
			{
				foreach (DataRow dbRow in dt.Rows)
				{
					gHTML += "<td>" + IntentoNro + "</td>" +
						"<td>" + dbRow["Personal"].ToString() + "</td>" +
						"<td>" + vMensaje + "</td>";
					vIDPER = dbRow["id"].ToString();
				}

			}
			else
			{
				gHTML = "<td>Error</td>";
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
				string consql = "UPDATE Prueba.dbo.PersonalIRO SET Sorteo = '0' WHERE ID = @IDPER; ";
				SqlCommand cmd = new SqlCommand(consql, conSAP00i);
				cmd.CommandType = CommandType.Text;
				//asignamos el valor de los textbox a los parametros
				cmd.Parameters.AddWithValue("@IDPER", vIDPER);

				conSAP00i.Open();
				cmd.ExecuteScalar();
				conSAP00i.Close();

			}
			catch (Exception ex)
			{
				//Label1.Text = "Error";
				gHTML += "-" + "-" + ex.Message.ToString();
			}
		}


		return gHTML;
	}

	public void CargaInicial()
	{
		try
		{
			//con.Open();
			string qSql = "Select ROW_NUMBER() OVER(ORDER BY id) Nro, Personal from Prueba.dbo.PersonalIRO where Sorteo = '0';";

			SqlCommand cmd = new SqlCommand(qSql, conSAP00); cmd.CommandType = CommandType.Text;
			SqlDataAdapter adapter = new SqlDataAdapter(); adapter.SelectCommand = cmd;

			conSAP00.Open();
			DataSet objdataset = new DataSet(); adapter.Fill(objdataset);
			conSAP00.Close();

			GVtable.DataSource = objdataset.Tables[0];
			GVtable.DataBind();
		}
		catch (Exception ex)
		{
			LitError.Text = ex.Message.ToString();
		}
	}

}