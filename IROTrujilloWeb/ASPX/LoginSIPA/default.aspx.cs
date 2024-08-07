using System;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Net;

public partial class ASPX_LoginSIPA_default : System.Web.UI.Page
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
            SqlCommand cmd = new SqlCommand("NUEVA.dbo.SP_JVAR_LOGIN_USUARIO", conSAP00);
            cmd.CommandType = CommandType.StoredProcedure;
            //creamos los parametros que usaremos
            cmd.Parameters.Add("@DesUsu", SqlDbType.NVarChar);
            cmd.Parameters.Add("@PasUsu", SqlDbType.NVarChar);
            //asignamos el valor de los textbox a los parametros
            cmd.Parameters["@DesUsu"].Value = usuario;
            cmd.Parameters["@PasUsu"].Value = clave;
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
                Response.Cookies["idUser"].Value = dtDato.Rows[0]["Cod_Usu"].ToString();
                Response.Cookies["idUser"].Expires = DateTime.Now.AddHours(2);

                Session["UserName2"] = dtDato.Rows[0]["Des_Usu"].ToString();
                Session["idUser2"] = dtDato.Rows[0]["Cod_Usu"].ToString();
                Session["TipoUser"] = "Admin";

                if (dtDato.Rows[0]["Cod_Usu"].ToString() == "131")
                {
                    Session["TipoUser"] = "ARFSIS";
                }

                logHistorial(dtDato.Rows[0]["Des_Usu"].ToString());

                Session.Timeout = 1200;

                HttpCookie MyCookie = new HttpCookie("UserName");
                MyCookie.Value = dtDato.Rows[0]["Des_Usu"].ToString();
                MyCookie.Expires = DateTime.Now.AddHours(2);
                Response.Cookies.Add(MyCookie);

                Response.Cookies["UserLastName"].Value = "";
                Response.Cookies["UserLastName"].Expires = DateTime.Now.AddHours(2);
                Response.Cookies["UserTipo"].Value = "";
                Response.Cookies["UserTipo"].Expires = DateTime.Now.AddHours(2);
                Response.Redirect("../Inicio/SelSIPA/");
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

    protected void LBtnPrueba_Click(object sender, EventArgs e)
    {
        string clave = ClassGlobal.DecripPass(txtcontrasena.Text);
        Literal1.Text = clave;
        this.Page.Response.Write("<script language='JavaScript'>window.alert('Clave: " + clave + "');</script>");

        //string usuario = txtusuario.Text;
        ////string clave = ClassGlobal.DecripPass(txtcontrasena.Text);
        //string clave = txtcontrasena.Text, clave2 = "";

        //try
        //{
        //    string consql = "Select * from NUEVA.dbo.USUARIO where Des_Usu = @User"; // and Pas_Usu=@Pass; ";
        //    SqlCommand cmd = new SqlCommand(consql, conSAP00);
        //    cmd.CommandType = CommandType.Text;
        //    //asignamos el valor de los textbox a los parametros
        //    cmd.Parameters.AddWithValue("@User", usuario);
        //    //cmd.Parameters.AddWithValue("@Pass", clave);
        //    //adapter, para asignarle el cmd, command
        //    SqlDataAdapter adapter = new SqlDataAdapter();
        //    adapter.SelectCommand = cmd;
        //    //abrimos conexion
        //    conSAP00.Open();
        //    DataSet objdataset = new DataSet();
        //    //Llenado un datase con el adaptador que contiene el command
        //    adapter.Fill(objdataset);
        //    //cerramos conexion
        //    conSAP00.Close();
        //    DataTable dtDato = objdataset.Tables[0];
        //    if (dtDato.Rows.Count > 0)
        //    {
        //        clave2= ClassGlobal.DecripPass(dtDato.Rows[0]["Pas_Usu"].ToString());
        //    }
        //    Literal1.Text = clave + "<br />" + clave2;

        //}
        //catch (Exception ex)
        //{
        //    //Label1.Text = "Error";
        //    ex.Message.ToString();
        //}

    }
}