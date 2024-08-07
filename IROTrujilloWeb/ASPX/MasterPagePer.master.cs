using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASPX_MasterPagePer : System.Web.UI.MasterPage
{
	protected void Page_Load(object sender, EventArgs e)
	{
		//NumberFormatInfo nfi = new NumberFormatInfo();
		//nfi.NumberDecimalSeparator = ".";
		//Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");


		//if (Request.Cookies.Get("UserName") != null)
		if (Session["PerID"] != null)
		{
			//LitUserMaster.Text = Request.Cookies.Get("UserLastName").Value + ", " + Request.Cookies.Get("UserName").Value;
			LitUserMaster.Text = Session["PerID"].ToString() + " - " + Session["PerDNI"].ToString();
            Session.Timeout = 160;
            //if (Request.Cookies.Get("UserTipo").Value == "ADMINISTRADOR")
            //{
            //    LiAdmin.Attributes["style"] = "display: block";
            //}
        }
		else
		{
			Response.Cookies["IniciarSesion"].Value = "Debe Iniciar Sesion";
			Response.Cookies["IniciarSesion"].Expires = DateTime.Now.AddSeconds(5);
			Response.Redirect("../../PerLogin/");
		}
        //LitUserMaster1.Text = Request.Cookies.Get("UserName").Value;
        //--LitUserMaster1.Text = Session["UserName2"].ToString();
        Response.TrySkipIisCustomErrors = true;
        Response.StatusCode = 555;
    }
	protected void CerrarSesion_Click(object sender, EventArgs e)
	{
		//Session.Abandon();
		//Session["UserName2"] = "Cerrado";
		//Session.Timeout = 1;
		Session.Contents.RemoveAll();


		Response.Redirect("../../PerLogin/");
	}
}
