using System;
using System.Data;
using System.Data.SqlClient;

public partial class IROTWeb_Investigacion_Resoluciones_Default : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;

    }
}