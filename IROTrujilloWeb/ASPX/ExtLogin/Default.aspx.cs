using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASPX_ExtLogin_Default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ClassGlobal.conexion_SAP00);
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
        try
        {
            //con.Open();
            //utilizamos el procedimiento alamacenado insertarusuarios
            SqlCommand cmd = new SqlCommand("select * from NUEVA.dbo.extUsuario where extUDNI = '" + usuario + "' and extClave = '" + clave + "'", con);
            //especificamos que el comando es un procedimiento almacenado
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            con.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            con.Close();
            DataTable dtDato = objdataset.Tables[0];
            //, , , extClave, extUFecNac, extUemail, extUTelefono, codIPRESS, extUCargo, extUsuEstado
            if (dtDato.Rows.Count > 0)
            {
                Session["extID"] = dtDato.Rows[0]["idextUsuario"].ToString();
                Session["extDNI"] = dtDato.Rows[0]["extUDNI"].ToString();
                Session["extApellidos"] = dtDato.Rows[0]["extUApellidos"].ToString();
                Session["extFecN"] = dtDato.Rows[0]["extUFecNac"].ToString();
                Session["extCargo"] = dtDato.Rows[0]["extUCargo"].ToString();
                Session["extTipo"] = dtDato.Rows[0]["extTipoUsu"].ToString();
                //logHistorial(dtDato.Rows[0]["DNI_PER"].ToString());
                Session.Timeout = 3160;

                switch (Session["extTipo"].ToString())
                {
                    case "BERMANLAB":
                        Response.Redirect("../ExtIROJVAR/BermanLab/ConsultaHC.aspx");
                        break;
                    default:
                        Response.Redirect("../ExtIROJVAR/Citas/ConsultaCitas.aspx");
                        break;
                }
                
            }
            else
            {
                //Response.Write("<SCRIPT>alert('Usuario-Clave Incorrecto')</SCRIPT>");
                //Response.Redirect("../Login/index.aspx");
                this.Page.Response.Write("<script language='JavaScript'>window.alert('Usuario/Clave Incorrecto');</script>");
                LblMensaje.Text = "Usuario Incorrecto, Vuelva a Intentar";
                txtusuario.Text = "";
                txtcontrasena.Text = "";
                txtusuario.Focus();
            }

        }
        catch (Exception ex)
        {
            //Label1.Text = "Error";
            ex.Message.ToString();
        }
    }
}