using System;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Net;

public partial class ASPX_PerLogin_Default : System.Web.UI.Page
{
	SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
	protected void Page_Load(object sender, EventArgs e)
	{
		if (Request.Cookies.Get("IniciarSesion") != null)
		{
			LblMensaje.Text = Request.Cookies.Get("IniciarSesion").Value;
		}
	}

	protected void enviar_Click(object sender, EventArgs e)
	{
        string usuario = txtusuario.Text;
        string clave = txtcontrasena.Text;
        //DateTime clave = Convert.ToDateTime(txtcontrasena.Text);

        //Response.Redirect("../PerIROJVAR/TrabPresencial/RegistraRemoto.aspx");
        try
		{
			string qSql = "select * from RRHH.dbo.PERSONAL where USUARIO_PER = @User and CLAVE_PER = @Pass";
			SqlCommand cmd2 = new SqlCommand(qSql, conSAP00);
			cmd2.CommandType = CommandType.Text;
			cmd2.Parameters.AddWithValue("@User", usuario);
			cmd2.Parameters.AddWithValue("@Pass", clave);

            SqlDataAdapter adapter2 = new SqlDataAdapter(); adapter2.SelectCommand = cmd2;

            conSAP00.Open();
            LblMensaje.Text = "Intento2.";
            DataSet objdataset = new DataSet();
			adapter2.Fill(objdataset);
			conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];

            if (dtDato.Rows.Count > 0)
			{
				Session["PerID"] = dtDato.Rows[0]["ID_PER"].ToString();
				Session["PerDNI"] = dtDato.Rows[0]["DNI_PER"].ToString();
				Session["PerDatos"] = dtDato.Rows[0]["NOM_PERSONAL"].ToString();
				Session["PerFecN"] = dtDato.Rows[0]["FNAC_PER"].ToString();
				Session["PerCargo"] = dtDato.Rows[0]["CARGO_PER"].ToString();
                Session["PerTipo"] = dtDato.Rows[0]["TIPOUSER_PER"].ToString();
                Session["PerUOAbr"] = dtDato.Rows[0]["UOABR_PER"].ToString();

                logHistorial(dtDato.Rows[0]["DNI_PER"].ToString());
				Session.Timeout = 1200;

                string vURLRedirect = "";
                switch (dtDato.Rows[0]["TIPOUSER_PER"].ToString())
                {
                    case "PERSONAL":
                        vURLRedirect = "../PerIROJVAR/TrabPresencial/RegistraRemoto.aspx";
                        break;
                    case "JEFE":
                        vURLRedirect = "../PerIROJVAR/TrabPresencial/ListarPersonal.aspx";
                        break;

                    default:
                        break;
                }

                Response.Redirect(vURLRedirect);
			}
			else
			{
				LblMensaje.Text = "Usuario Incorrecto, Vuelva a Intentar.";
				txtusuario.Text = "";
				txtcontrasena.Text = "";
				txtusuario.Focus();
				this.Page.Response.Write("<script language='JavaScript'>window.alert('Usuario/Clave Incorrecto');</script>");
			}

		}
		catch (Exception ex)
		{
            //Label1.Text = "Error";
            LblMensaje.Text += ex.Message.ToString();
		}
	}

	public void logHistorial(string vuser)
	{
		try
		{
			string consql = "insert into NUEVA.dbo.JVARHistorial (usuario, fecha, remote_addr, remote_host) Values (@usuario, @fecha, @remote_addr, @remote_host) SELECT @@Identity; ";
			SqlCommand cmd = new SqlCommand(consql, conSAP00);
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("@usuario", vuser);
			cmd.Parameters.AddWithValue("@fecha", DateTime.Now);
			cmd.Parameters.AddWithValue("@remote_addr", Request.ServerVariables["REMOTE_ADDR"]);
			cmd.Parameters.AddWithValue("@remote_host", Request.ServerVariables["REMOTE_host"]);

			conSAP00.Open();
			int count = Convert.ToInt32(cmd.ExecuteScalar());
			conSAP00.Close();
		}
		catch (Exception ex)
		{
			ex.Message.ToString();
			this.Page.Response.Write("<script language='JavaScript'>window.alert('" + ex.Message.ToString() + "');</script>");
		}
	}

	protected void LBtnPrueba_Click(object sender, EventArgs e)
	{
		string clave = ClassGlobal.DecripPass(txtcontrasena.Text);
		Literal1.Text = clave;
		this.Page.Response.Write("<script language='JavaScript'>window.alert('Clave: " + clave + "');</script>");

	}
}