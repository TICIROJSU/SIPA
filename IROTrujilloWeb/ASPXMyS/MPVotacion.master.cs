using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASPXMyS_MPVotacion : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["eID"] != null)
        {
            Session.Timeout = 15;
        }
        else
        {
            Response.Cookies["IniciarSesion"].Value = "Sesion Cerrada por Expiracion de Tiempo";
            Response.Cookies["IniciarSesion"].Expires = DateTime.Now.AddSeconds(5);
            Response.Redirect("../Inicio/Votacion.aspx");
        }
    }

    protected void CerrarSesion_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Inicio/Votacion.aspx");
    }

}
