using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class IROTWeb_MasterPageIRO : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Request.Cookies.Get("UserName") != null)
        //{
        //    LitUserMaster.Text = Request.Cookies.Get("UserLastName").Value + ", " + Request.Cookies.Get("UserName").Value;
        //    if (Request.Cookies.Get("UserTipo").Value == "ADMINISTRADOR")
        //    {
        //        LiAdmin.Attributes["style"] = "display: block";
        //    }
        //}
        //else
        //{
        //    Response.Cookies["IniciarSesion"].Value = "Debe Iniciar Sesion";
        //    Response.Cookies["IniciarSesion"].Expires = DateTime.Now.AddSeconds(5);
        //    Response.Redirect("../Login/");
        //}
        //LitUserMaster1.Text = Request.Cookies.Get("UserName").Value;
    }
    protected void CerrarSesion_Click(object sender, EventArgs e)
    {
        //Response.Cookies["idUser"].Expires = DateTime.Now.AddMilliseconds(1);
        //Response.Cookies["UserName"].Expires = DateTime.Now.AddMilliseconds(2);
        //Response.Cookies["UserLastName"].Expires = DateTime.Now.AddMilliseconds(2);

        //Response.Cookies["IniciarSesion"].Value = "Cerr&oacute Sesion";
        //Response.Cookies["IniciarSesion"].Expires = DateTime.Now.AddSeconds(5);
        //Response.Redirect("../../Login/");
    }
}
