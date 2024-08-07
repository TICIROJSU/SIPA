using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASPX_MasterPageExt : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["extDNI"] != null)
        {
            LitUserMaster.Text = Session["extDNI"].ToString() + " - " + Session["extApellidos"].ToString();
            Session.Timeout = 160;
        }
        else
        {
            Response.Cookies["IniciarSesion"].Value = "Debe Iniciar Sesion";
            Response.Cookies["IniciarSesion"].Expires = DateTime.Now.AddSeconds(5);
            Response.Redirect("../../ExtLogin/");
        }
    }

    protected void CerrarSesion_Click(object sender, EventArgs e)
    {
        Session.Contents.RemoveAll();

        Response.Redirect("../../ExtLogin/");
    }

    protected void CerrarSesion2_Click(object sender, EventArgs e)
    {
        Session.Contents.RemoveAll();

        Response.Redirect("../../ExtLogin/");
    }

}
