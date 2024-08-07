using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASPX_MPOptica : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName2"] != null || Session["idUser2"] != null)
        {
            LitUserMaster.Text = Session["UserName2"].ToString() + " - " + Session["idUser2"].ToString();
            Session.Timeout = 1200;
        }
        else
        {
            Response.Cookies["IniciarSesion"].Value = "Debe Iniciar Sesion";
            Response.Cookies["IniciarSesion"].Expires = DateTime.Now.AddSeconds(5);
            Response.Redirect("../../LoginSIPA/");
        }
    }
    protected void CerrarSesion_Click(object sender, EventArgs e)
    {
        Response.Cookies["idUser"].Expires = DateTime.Now.AddMilliseconds(1);
        Response.Cookies["UserName"].Expires = DateTime.Now.AddMilliseconds(2);
        Response.Cookies["UserLastName"].Expires = DateTime.Now.AddMilliseconds(2);

        Response.Cookies["IniciarSesion"].Value = "Cerr&oacute Sesion";
        Response.Cookies["IniciarSesion"].Expires = DateTime.Now.AddSeconds(5);

        Session.Contents.RemoveAll();


        Response.Redirect("../../LoginOptica/");
    }

    public Label lblidUser
    {
        get { return lblidUser2; }
        set { lblidUser2 = value; }
    }

}

