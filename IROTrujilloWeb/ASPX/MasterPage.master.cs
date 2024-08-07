using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASPX_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //NumberFormatInfo nfi = new NumberFormatInfo();
        //nfi.NumberDecimalSeparator = ".";
        //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");


        //if (Request.Cookies.Get("UserName") != null)
        //if (!string.IsNullOrEmpty(Session["idUser2"] as string))
        if (Session["UserName2"] != null || Session["idUser2"] != null)
        {
            //LitUserMaster.Text = Request.Cookies.Get("UserLastName").Value + ", " + Request.Cookies.Get("UserName").Value;
            LitUserMaster.Text = Session["UserName2"].ToString() + " - " + Session["idUser2"].ToString();
            Session.Timeout = 1200;
            //if (Request.Cookies.Get("UserTipo").Value == "ADMINISTRADOR")
            //{
            //    LiAdmin.Attributes["style"] = "display: block";
            //}

            //Variables Globales
            //lblidUser2.Text = Session["idUser2"].ToString();
        }
        else
        {
            Response.Cookies["IniciarSesion"].Value = "Debe Iniciar Sesion";
            Response.Cookies["IniciarSesion"].Expires = DateTime.Now.AddSeconds(5);
            Response.Redirect("../../Login/");
        }
        //LitUserMaster1.Text = Request.Cookies.Get("UserName").Value;
        //--LitUserMaster1.Text = Session["UserName2"].ToString();
    }
    protected void CerrarSesion_Click(object sender, EventArgs e)
    {
        Response.Cookies["idUser"].Expires = DateTime.Now.AddMilliseconds(1);
        Response.Cookies["UserName"].Expires = DateTime.Now.AddMilliseconds(2);
        Response.Cookies["UserLastName"].Expires = DateTime.Now.AddMilliseconds(2);

        Response.Cookies["IniciarSesion"].Value = "Cerr&oacute Sesion";
        Response.Cookies["IniciarSesion"].Expires = DateTime.Now.AddSeconds(5);

        //Session.Abandon();
        //Session["UserName2"] = "Cerrado";
        //Session.Timeout = 1;
        Session.Contents.RemoveAll();


        Response.Redirect("../../Login/");
    }


    public Label lblidUser
    {
        get { return lblidUser2; }
        set { lblidUser2 = value; }
    }

}
