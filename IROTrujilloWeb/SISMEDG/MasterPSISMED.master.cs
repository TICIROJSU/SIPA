using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SISMEDG_MasterPSISMED : System.Web.UI.MasterPage
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (Session["UsuarioDNI"] != null)
		{
			LitUserMaster.Text = Session["UsuarioDNI"].ToString();
		}
		else
		{
			Response.Cookies["IniciarSesion"].Value = "Debe Iniciar Sesion";
			Response.Cookies["IniciarSesion"].Expires = DateTime.Now.AddSeconds(5);
			Response.Redirect("../../Login/");
		}
	}

	protected void CerrarSesion_Click(object sender, EventArgs e)
	{
		CerrarSesionU();
	}
	protected void CerrarSesion2_Click(object sender, EventArgs e)
	{
		CerrarSesionU();
	}

	public void CerrarSesionU()
	{
		Response.Cookies["UsuarioDNI"].Expires = DateTime.Now.AddMilliseconds(1);
		Response.Cookies["idUsuario"].Expires = DateTime.Now.AddMilliseconds(2);
		Response.Cookies["UsuarioNom"].Expires = DateTime.Now.AddMilliseconds(2);

		Response.Cookies["IniciarSesion"].Value = "Cerr&oacute Sesion";
		Response.Cookies["IniciarSesion"].Expires = DateTime.Now.AddSeconds(5);

		Session.Contents.RemoveAll();

		Response.Redirect("../../Login/");
	}
}
