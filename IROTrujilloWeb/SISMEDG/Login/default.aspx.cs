using System;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Net;

public partial class ASPX_Login_default : System.Web.UI.Page
{
    SqlConnection conSISMED = new SqlConnection(ClassGlobal.conSISMED);
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

        //Response.Redirect("../Inicio/Sel/");
        try
        {
			string consql = "SELECT * FROM Personal WHERE Usuario = @User and Clave = @Clave;";
			SqlCommand cmd = new SqlCommand(consql, conSISMED);
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("@User", usuario);
			cmd.Parameters.AddWithValue("@Clave", clave);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

			conSISMED.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
			conSISMED.Close();

            DataTable dtDato = objdataset.Tables[0];
            if (dtDato.Rows.Count > 0)
            {
                Response.Cookies["idUsuario"].Value = dtDato.Rows[0]["Usuario"].ToString();
                Response.Cookies["idUsuario"].Expires = DateTime.Now.AddHours(2);

                Session["UsuarioDNI"] = dtDato.Rows[0]["DNI"].ToString();
				Session["UsuarioTipo"] = dtDato.Rows[0]["Perfil"].ToString();
				Session["UsuarioTipoN"] = dtDato.Rows[0]["PerfilN"].ToString();

				logHistorial(dtDato.Rows[0]["Usuario"].ToString());

                Session.Timeout = 1200;

                //Response.Cookies["UserName"].Value = dtDato.Rows[0]["Des_Usu"].ToString();
                //Response.Cookies["UserName"].Expires = DateTime.Now.AddHours(2);

                HttpCookie MyCookie = new HttpCookie("UsuarioNom");
                MyCookie.Value = dtDato.Rows[0]["Nombres"].ToString();
                MyCookie.Expires = DateTime.Now.AddHours(2);
                Response.Cookies.Add(MyCookie);

                Response.Cookies["UserLastName"].Value = "";
                Response.Cookies["UserLastName"].Expires = DateTime.Now.AddHours(2);
                Response.Cookies["UserTipo"].Value = "";
                Response.Cookies["UserTipo"].Expires = DateTime.Now.AddHours(2);
                Response.Redirect("../JVAR/SISMED/StockDiario.aspx");
            }
            else
            {
                //Response.Write("<SCRIPT>alert('Usuario-Clave Incorrecto')</SCRIPT>");
                //Response.Redirect("../Login/index.aspx");
                LblMensaje.Text = "Usuario Incorrecto, Vuelva a Intentar." ;
                txtusuario.Text = "";
                txtcontrasena.Text = "";
                txtusuario.Focus();
                this.Page.Response.Write("<script language='JavaScript'>window.alert('Usuario/Clave Incorrecto');</script>");
            }
            /*Response.Redirect("../JADIGEMID/Produccion/");*/

        }
        catch (Exception ex)
        {
            //Label1.Text = "Error";
            ex.Message.ToString();
        }
    }

    public void logHistorial(string vuser)
    {
        try
        {
            string consql = "insert into HistorialLog (usuario, remote_addr, remote_host) Values (@usuario, @remote_addr, @remote_host) SELECT @@Identity; ";
            SqlCommand cmd = new SqlCommand(consql, conSISMED);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@usuario", vuser);
            cmd.Parameters.AddWithValue("@remote_addr", Request.ServerVariables["REMOTE_ADDR"]);
            cmd.Parameters.AddWithValue("@remote_host", Request.ServerVariables["REMOTE_host"]);

			conSISMED.Open();
            int count = Convert.ToInt32(cmd.ExecuteScalar());
			conSISMED.Close();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            this.Page.Response.Write("<script language='JavaScript'>window.alert('" + ex.Message.ToString() + "');</script>");
        }
    }

    protected void LBtnPrueba_Click(object sender, EventArgs e)
    {
        

	}
}