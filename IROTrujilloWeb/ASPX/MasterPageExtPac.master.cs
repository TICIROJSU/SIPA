using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASPX_MasterPageExtPac : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

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

