using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class IROTWeb_wPac_SesionA_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnIniSesion_Click(object sender, EventArgs e)
    {
        Session["PacUser"] = "General";
        Session["PacTipo"] = "Comun";
        Session.Timeout = 60;
        Response.Redirect("../Citas/Confirmacion.aspx");
    }
}