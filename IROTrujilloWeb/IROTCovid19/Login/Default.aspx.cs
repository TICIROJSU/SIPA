using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class IROTCovid19_Login_Default : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		
	}

	protected void botonEnviarLogin_Click(object sender, EventArgs e)
	{
		Response.Redirect("../IROJVAR/RegCovid/");
	}
}