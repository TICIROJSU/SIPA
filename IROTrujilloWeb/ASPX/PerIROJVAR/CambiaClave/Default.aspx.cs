using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASPX_PerIROJVAR_CambiaClave_Default : System.Web.UI.Page
{
    SqlConnection conLocal = new SqlConnection(ClassGlobal.conexion_local);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            
        }

        txtDNI.Text = Session["PerDNI"].ToString();

    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetHtml = "";

        if (txtNuevo1.Text != txtNuevo2.Text)
        {
            txtAnterior.Text = "Las Claves no son iguales";
            return;
        }

        try
        {
            string qSql = "update RRHH.dbo.PERSONAL set CLAVE_PER = @CLAVE_PER where DNI_PER = @DNI_PER;";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@CLAVE_PER", txtNuevo1.Text);
            cmd.Parameters.AddWithValue("@DNI_PER", txtDNI.Text);
            adapter.SelectCommand = cmd;

            conSAP00i.Open(); cmd.ExecuteScalar(); conSAP00i.Close();

            gDetHtml += "Edicion Correcta";
        }
        catch (Exception ex)
        {
            gDetHtml += "ERROR: " + ex.Message.ToString() + Environment.NewLine + ex.ToString();
        }
        //return gDetHtml;
    }
}