using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASPX_MasterPageTabLibres : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["PerID"] != null)
        {
            LitUserMaster.Text = Session["PerID"].ToString() + " - " + Session["PerDNI"].ToString();
            Session.Timeout = 525600;
        }
        else
        {
            Response.Cookies["IniciarSesion"].Value = "Debe Iniciar Sesion";
            Response.Cookies["IniciarSesion"].Expires = DateTime.Now.AddSeconds(5);
            Response.Redirect("../../TabLLogin/");
        }
        Response.TrySkipIisCustomErrors = true;
        Response.StatusCode = 555;
    }
    protected void CerrarSesion_Click(object sender, EventArgs e)
    {
        Session.Contents.RemoveAll();
        Response.Redirect("../../TabLLogin/");
    }
}
