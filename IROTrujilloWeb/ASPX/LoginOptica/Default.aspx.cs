using System;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Net;

public partial class ASPX_LoginOptica_Default : System.Web.UI.Page
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

        //Response.Redirect("../Inicio/Sel/");
        try
        {
            //con.Open();
            SqlCommand cmd = new SqlCommand("[IRO_BD_OPTICADESK].[dbo].[USUARIO_Login]", conSAP00);
            cmd.CommandType = CommandType.StoredProcedure;
            //creamos los parametros que usaremos
            cmd.Parameters.Add("@vchCuenta", SqlDbType.NVarChar);
            cmd.Parameters.Add("@vchPassWord", SqlDbType.NVarChar);
            //asignamos el valor de los textbox a los parametros
            cmd.Parameters["@vchCuenta"].Value = usuario;
            cmd.Parameters["@vchPassWord"].Value = clave;
            //adapter, para asignarle el cmd, command
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];
            if (dtDato.Rows.Count > 0)
            {
                Response.Cookies["idUser"].Value = dtDato.Rows[0]["Codigo"].ToString();
                Response.Cookies["idUser"].Expires = DateTime.Now.AddHours(2);

                Session["UserName2"] = dtDato.Rows[0]["NombreCorto"].ToString();
                Session["idUser2"] = dtDato.Rows[0]["Codigo"].ToString();
                Session["TipoUser"] = "Admin";

                logHistorial(dtDato.Rows[0]["NombreCorto"].ToString());

                Session.Timeout = 1200;

                HttpCookie MyCookie = new HttpCookie("NombreCorto");
                MyCookie.Value = dtDato.Rows[0]["Codigo"].ToString();
                MyCookie.Expires = DateTime.Now.AddHours(2);
                Response.Cookies.Add(MyCookie);

                Response.Redirect("../Inicio/SelOptica/");
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

}