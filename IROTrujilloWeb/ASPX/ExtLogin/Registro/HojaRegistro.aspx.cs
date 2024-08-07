using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASPX_Registro_HojaRegistro : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        string iduser = Request.QueryString["Usuario"];
        string varSql = "select * from NUEVA.dbo.extUsuario where extUDNI = '" + iduser + "';";
        SqlCommand cmd = new SqlCommand(varSql, con);
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = cmd;
        //con.Open();
        DataSet objdataset = new DataSet();
        adapter.Fill(objdataset);
        //con.Close();
        DataTable dtDato = objdataset.Tables[0];
        DataRow dbRow;
        if (dtDato.Rows.Count > 0)
        {
            dbRow = dtDato.Rows[0];
            lblNombres.Text = dbRow["extUNombres"].ToString() + ", " + dbRow["extUApellidos"].ToString();
            lblNombres2.Text = lblNombres.Text;
            lblDNI.Text = dbRow["extUDNI"].ToString();
            lblDNI2.Text = lblDNI.Text;
            lblEESS.Text = dbRow["codIPRESS"].ToString();
            lblCargo.Text = dbRow["extUCargo"].ToString();
        }
    }
}