using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASPXMyS_IROJVAR_Inicio_Default : System.Web.UI.Page
{
    MySqlConnection conMySql = new MySqlConnection(csgMySql.conMySQL_IIS);
    protected void Page_Load(object sender, EventArgs e)
    {
        string tmpVoto = Session["eVOTO"].ToString();
        if (tmpVoto == "1")
        {
            this.Page.Response.Write("<script language='JavaScript'>" +
                "window.alert('Usted ya Realizo su Voto'); " +
                "location = '../Votacion/Tablero1.aspx'; </script>");
        }


    }



}
