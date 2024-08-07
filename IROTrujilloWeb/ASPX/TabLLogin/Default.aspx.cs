using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASPX_TabLLogin_Default : System.Web.UI.Page
{
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

        Session["PerID"] = "0";
        Session["PerDNI"] = "0";
        Session["PerDatos"] = "0";
        Session["PerFecN"] = "0";
        Session["PerCargo"] = "0";
        Session["PerTipo"] = "0";
        Session["PerUOAbr"] = "0";
        Session.Timeout = 525600;
        Response.Redirect("../TabLJVAR/CMNRegistro/CMNRegistrar.aspx?Proc=New&id=0");

    }


    protected void LBtnPrueba_Click(object sender, EventArgs e)
    {
        string clave = ClassGlobal.DecripPass(txtcontrasena.Text);
        Literal1.Text = clave;
        this.Page.Response.Write("<script language='JavaScript'>window.alert('Clave: " + clave + "');</script>");

    }
}