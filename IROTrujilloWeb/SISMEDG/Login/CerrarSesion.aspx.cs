using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASPX_Login_CerrarSesion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cookies["idUser"].Expires = DateTime.Now.AddMilliseconds(1);
        Response.Cookies["UserName"].Expires = DateTime.Now.AddMilliseconds(2);
        Response.Cookies["UserLastName"].Expires = DateTime.Now.AddMilliseconds(2);

        Response.Cookies["IniciarSesion"].Value = "Cerro Sesion";
        Response.Cookies["IniciarSesion"].Expires = DateTime.Now.AddSeconds(5);
        Response.Redirect("../Login/");
    }
}