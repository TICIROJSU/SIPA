using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASPXMyS_IROJVAR_Votacion_Prueba01 : System.Web.UI.Page
{
    MySqlConnection conMySql = new MySqlConnection(csgMySql.conMySQL_IIS);
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnConsulta_Click(object sender, EventArgs e)
    {

        try
        {
            string qMySql = "SELECT * FROM test.tableprueba;";
            
            MySqlCommand cmd = new MySqlCommand(qMySql, conMySql);
            cmd.CommandType = CommandType.Text;
            MySqlDataAdapter MySAdapter = new MySqlDataAdapter();
            MySAdapter.SelectCommand = cmd;

            conMySql.Open();
            DataSet objdataset = new DataSet();
            MySAdapter.Fill(objdataset);
            conMySql.Close();

            //DataTable dtDatoDetAt = objdataset.Tables[0];

            gvTable.DataSource = objdataset.Tables[0];
            gvTable.DataBind();

        }
        catch (Exception)
        {

            throw;
        }
    }
}